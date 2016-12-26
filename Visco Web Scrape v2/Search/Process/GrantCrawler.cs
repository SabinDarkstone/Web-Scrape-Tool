using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Abot.Crawler;
using Abot.Poco;
using CsQuery.ExtensionMethods;
using Visco_Web_Scrape_v2.Forms;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Scripts.Helpers;
using Visco_Web_Scrape_v2.Search.Items;

namespace Visco_Web_Scrape_v2.Search.Process {

	public class GrantCrawler {

		public bool Successful { get; private set; }
		public string RootUrl { get; private set; }
		public SearchResults Results { get; private set; }

		private readonly PoliteWebCrawler crawler;
		private readonly Uri pageUri;
		private readonly Keyword[] keywords;
		private readonly Website domain;
		private BackgroundWorker worker;
		private Website[] websiteList;
		private DoWorkEventArgs eventArgs;
		private GrantSearch parentForm;
		private CancellationTokenSource cancelPlease;
		private Dictionary<string, int> badUrlComponentsList;
		private List<string> goodUrlComponentsList;

		public GrantCrawler(Configuration configuration, Website website, Keyword[] keywords, BackgroundWorker parentThread, DoWorkEventArgs e, GrantSearch parentForm) {
			Uri properUrl;
			Results = new SearchResults(website);
			var url = website.Url;
			domain = website;
			this.parentForm = parentForm;
			eventArgs = e;
			websiteList = configuration.Websites.ToArray();
			badUrlComponentsList = new Dictionary<string, int>();
			goodUrlComponentsList = new List<string>();

			if (keywords.Length == 0) throw new ArgumentNullException("Keyword list cannot be empty.");
			this.keywords = keywords;

			if (string.IsNullOrEmpty(url))
				throw new ArgumentNullException("Url cannot be blank.");

			if (url.StartsWith("www")) {
				properUrl = new Uri("http://" + url);
			} else {
				properUrl = new Uri(url);
			}

			// Create new crawler with configuration from app.config
			crawler = new PoliteWebCrawler();

			// Register thread and page
			worker = parentThread;
			pageUri = properUrl;

			// Register events
			crawler.PageCrawlStartingAsync += crawler_ProcessPageCrawlStarting;
			crawler.PageCrawlCompletedAsync += crawler_ProcessPageCrawlCompleted;
			crawler.PageCrawlDisallowedAsync += crawler_PageCrawlDisallowed;
			crawler.PageLinksCrawlDisallowedAsync += crawler_PageLinksCrawlDisallowed;

			// Register decision making
			crawler.ShouldCrawlPage((pageToCrawl, crawlContext) => {
				var uri = pageToCrawl.Uri.AbsoluteUri.ToLower();
				var crawlDecision = new CrawlDecision { Allow = true };

				if (Reference.IgnoreWords.Any(word => uri.Contains(word))) {
					CrawlHelper.SkippedPages++;
					return new CrawlDecision {Allow = false, Reason = "Skippable word found in url."};
				}

				return crawlDecision;
			});
		}

		public void Run(CancellationTokenSource cts) {
			cancelPlease = cts;
			CrawlHelper.TotalPages = 0;
			CrawlHelper.SkippedPages = 0;
			var results = crawler.Crawl(pageUri, cancelPlease);

			if (results.ErrorOccurred) {
				if (cancelPlease.IsCancellationRequested) {
					Successful = true;
					worker.ReportProgress(0, new Progress(Progress.Status.Cancelled));
					cancelPlease.Dispose();
					return;
				}

				Successful = false;
				MessageBox.Show("Crawl of " + RootUrl + " completed with error " + results.ErrorException.Message, "Crawl Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			} else {
				foreach (var badComponent in badUrlComponentsList) {
					LogHelper.Debug(badComponent.Key + ": " + badComponent.Value);
				}
				Successful = true;
			}
		}

		private void SearchPageForKeywords(CrawledPage page) {
			var myProgress = new Progress(page.Uri.AbsoluteUri, domain.Name, Results.ResultList.Count, websiteList.IndexOf(domain), Progress.Status.Searching);
			worker.ReportProgress(0, myProgress);
			var pageText = page.Content.Text.ToLower();
			bool keywordsFound = false;
			foreach (var keyword in keywords) {
				if (pageText.Contains(keyword.Text.ToLower())) {
					Results.AddNewResult(page, keyword);
					keywordsFound = true;
				}
			}

			/*
			if (!keywordsFound) {
				var progress = new Progress(page.Uri.AbsoluteUri, domain.Name, Results.ResultList.Count, websiteList.IndexOf(domain), Progress.Status.Scanning);
				worker.ReportProgress(0, progress);
				AddBadUrlComponent(page.Uri.AbsoluteUri);
			} else {
				AddGoodUrlComponent(page.Uri.AbsoluteUri);
			}
			CrosscheckComponents(page.Uri.AbsoluteUri);
			*/
		}

		private void AddBadUrlComponent(string url) {
			var components = url.Split('/').ToList();
			components.RemoveAt(components.Count - 1);

			foreach (var component in components) {
//				if (goodUrlComponentsList.Contains(component)) return;
				if (badUrlComponentsList.ContainsKey(component)) {
					badUrlComponentsList[component]++;
				} else {
					badUrlComponentsList.Add(component, 1);
					LogHelper.Debug("Added bad component: " + component);
				}
			}
		}

		private void AddGoodUrlComponent(string url) {
			var components = url.Split('/').ToList();
			components.RemoveAt(components.Count - 1);

			foreach (var component in components) {
				if (!goodUrlComponentsList.Contains(component)) {
					goodUrlComponentsList.Add(component);
					LogHelper.Debug("Added good component: " + component);
				}
			}
		}

		private void CrosscheckComponents(string url) {
			var components = url.Split('/');

			foreach (var component in components) {
				if (goodUrlComponentsList.Contains(component)) {
					if (badUrlComponentsList.ContainsKey(component)) {
						badUrlComponentsList.Remove(component);
					}
				}
			}
		}

		private void crawler_ProcessPageCrawlStarting(object sender, PageCrawlStartingArgs e) {
			CrawlHelper.TotalPages++;
			if (worker.CancellationPending) {
				worker.ReportProgress(0, new Progress(Progress.Status.Cancelling));
				Debug.WriteLine("Cancellation is pending...");
				cancelPlease.Cancel();
				return;
			}

			var myProgress = new Progress(e.PageToCrawl.Uri.AbsoluteUri, domain.Name, Results.ResultList.Count, websiteList.IndexOf(domain), Progress.Status.Crawling);
			worker.ReportProgress(0, myProgress);
		}

		private void crawler_ProcessPageCrawlCompleted(object sender, PageCrawlCompletedArgs e) {
			var crawledPage = e.CrawledPage;

			if (crawledPage.WebException != null || crawledPage.HttpWebResponse.StatusCode != HttpStatusCode.OK) {
				// TODO: Log
				return;
			} else {
				// TODO: Log
			}

			if (string.IsNullOrEmpty(crawledPage.Content.Text)) {
				// TODO: Log
				return;
			} else {
				/*
				if (!crawledPage.Content.Text.ToLower().Contains("grant")) {
					return;
				}
				*/
				SearchPageForKeywords(crawledPage);
			}
		}

		private void crawler_PageCrawlDisallowed(object sender, PageCrawlDisallowedArgs e) {
			var pageToCrawl = e.PageToCrawl;
			// TODO: Log
		}

		private void crawler_PageLinksCrawlDisallowed(object sender, PageLinksCrawlDisallowedArgs e) {
			var crawledPage = e.CrawledPage;
			// TODO: Log
		}

	}

}