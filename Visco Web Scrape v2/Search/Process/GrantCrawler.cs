using System;
using System.ComponentModel;
using System.Diagnostics;
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
		private readonly Uri pageToCrawl;
		private readonly Keyword[] keywords;
		private readonly Website domain;
		private BackgroundWorker worker;
		private Website[] websiteList;
		private DoWorkEventArgs eventArgs;
		private GrantSearch parentForm;
		private CancellationTokenSource cancelPlease;

		public GrantCrawler(Configuration configuration, Website website, Keyword[] keywords, BackgroundWorker parentThread, DoWorkEventArgs e, GrantSearch parentForm) {
			Uri properUrl;
			Results = new SearchResults(website);
			var url = website.Url;
			domain = website;
			this.parentForm = parentForm;
			eventArgs = e;
			websiteList = configuration.Websites.ToArray();

			if (keywords.Length == 0)
				throw new ArgumentNullException("Keyword list cannot be empty.");
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
			pageToCrawl = properUrl;

			// Register events
			crawler.PageCrawlStartingAsync += crawler_ProcessPageCrawlStarting;
			crawler.PageCrawlCompletedAsync += crawler_ProcessPageCrawlCompleted;
			crawler.PageCrawlDisallowedAsync += crawler_PageCrawlDisallowed;
			crawler.PageLinksCrawlDisallowedAsync += crawler_PageLinksCrawlDisallowed;
		}

		public void Run(CancellationTokenSource cts) {
			cancelPlease = cts;
			CrawlHelper.TotalPages = 0;
			var results = crawler.Crawl(pageToCrawl, cancelPlease);

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
				Successful = true;
			}
		}

		private void SearchPageForKeywords(CrawledPage page) {
			var myProgress = new Progress(page.Uri.AbsoluteUri, domain.Name, CrawlHelper.TotalPages, Results.ResultList.Count, websiteList.IndexOf(domain), Progress.Status.Searching);
			worker.ReportProgress(0, myProgress);
			var pageText = page.Content.Text.ToLower();
			foreach (var keyword in keywords) {
				if (pageText.Contains(keyword.Text.ToLower())) {
					Results.AddNewResult(page, keyword);
				}
			}
		}

		private void crawler_ProcessPageCrawlStarting(object sender, PageCrawlStartingArgs e) {
			if (worker.CancellationPending) {
				worker.ReportProgress(0, new Progress(Progress.Status.Cancelling));
				Debug.WriteLine("Cancellation is pending...");
				cancelPlease.Cancel();
				return;
			}

			var myProgress = new Progress(e.PageToCrawl.Uri.AbsoluteUri, domain.Name, CrawlHelper.TotalPages, Results.ResultList.Count, websiteList.IndexOf(domain), Progress.Status.Crawling);
			worker.ReportProgress(0, myProgress);
		}

		private void crawler_ProcessPageCrawlCompleted(object sender, PageCrawlCompletedArgs e) {
			CrawlHelper.TotalPages++;
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