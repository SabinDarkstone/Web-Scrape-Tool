using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading;
using Abot.Core;
using Abot.Crawler;
using Abot.Poco;
using Visco_Web_Scrape_v2.Forms;
using Visco_Web_Scrape_v2.Properties;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Scripts.Helpers;
using Visco_Web_Scrape_v2.Search.Items;

namespace Visco_Web_Scrape_v2.Search.Process {

	public class GrantCrawler {

		public bool Successful { get; private set; }
		public WebsiteResults Results { get; }

		private readonly PoliteWebCrawler crawler;
		private readonly Job job;
		private readonly Configuration config;
		private readonly BackgroundWorker worker;
		private readonly CancellationTokenSource cancelMe;
		private readonly GrantSearch parentForm;

		public struct KeywordMatch {
			
			public Keyword Keyword { get; }
			public string Context { get; }

			public KeywordMatch(Keyword keyword, string context) {
				Keyword = keyword;
				Context = context;
			}
		}

		public GrantCrawler(Job job, Configuration config, Website website, BackgroundWorker worker,
			CancellationTokenSource cancelToken, GrantSearch searchForm) {
			this.job = job;
			this.worker = worker;
			this.config = config;
			parentForm = searchForm;
			cancelMe = cancelToken;

			Results = new WebsiteResults(website);

			// Check format of URL
			var url = VerifyCrawl();

			// Read settings file and overwrite app.config settings for crawler
			var crawlConfig = AbotConfigurationSectionHandler.LoadFromXml().Convert();
			LogHelper.Debug("Respect robots.txt is " + crawlConfig.IsRespectRobotsDotTextEnabled);
			crawlConfig.MaxPagesToCrawl = config.PagesToCrawlPerDomain;

			// Initialize crawler
			crawler = new PoliteWebCrawler(crawlConfig);
			RegisterEvents();

			if (this.config.EnableUrlFiltering) {
				ConfigureDecisionMaker();
			}

			if (this.config.EnableUrlAnalysis) {
				crawlConfig.MaxConcurrentThreads = 2;
			}

			// Run Crawler
			Run(url);
		}

		private string VerifyCrawl() {
			if (job.KeywordsToSearchFor == null || job.KeywordsToSearchFor.Count == 0) {
				throw new ArgumentException("There are no active keywords to search for");
			}

			var url = Results.RootWebsite.Url;
			return url.StartsWith("www.") ? "http://" + url : url;
		}

		private void RegisterEvents() {
			crawler.PageCrawlStartingAsync += crawler_ProcessPageCrawlStarting;
			crawler.PageCrawlCompletedAsync += crawler_ProcessPageCrawlCompleted;
		}

		private void ConfigureDecisionMaker() {
			crawler.ShouldCrawlPage((pageToCrawl, crawlContext) => {
				var url = pageToCrawl.Uri.AbsoluteUri.ToLower();
				var crawlDecision = new CrawlDecision {Allow = true};

				if (Reference.IgnoreExtensions.Any(word => url.Contains(word))) {
					CrawlHelper.SkippedPages++;
					return new CrawlDecision {Allow = false, Reason = Resources.BadExtension};
				}

				if (config.UrlWords.Any(word => url.Contains(word.Text) && word.IsEnabled)) {
					CrawlHelper.SkippedPages++;
					return new CrawlDecision {Allow = false, Reason = Resources.UrlFiltered};
				}

				return crawlDecision;
			});
		}

		private void Run(string url) {
			CrawlHelper.TotalPages = 0;
			CrawlHelper.SkippedPages = 0;

			LogHelper.Info("BEGINNING CRAWL OF " + url);
			Results.StartedSearch = true;
			var results = crawler.Crawl(new Uri(url), cancelMe);
			LogHelper.Info("CRAWL OF " + url + " COMPLETED");

			// Save crawl metadata to Results object before sending it over to GrantSearch
			Results.CrawledPages = CrawlHelper.TotalPages;
			Results.SkippedPages = CrawlHelper.SkippedPages;
			Results.SearchTimeInSeconds = GrantSearch.ElapsedTime.CurrentTime;

			// Add results to CombinedResults object in GrantSearch form
			LogHelper.Debug("Adding results to parent form...");
			parentForm.Results.AddWebsiteResults(Results);

			if (results.ErrorOccurred) {
				if (cancelMe.IsCancellationRequested) {
					LogHelper.Debug("Crawl was interrupted early by user request.");
					if (CrawlHelper.TotalPages == 0) {
						Results.StartedSearch = false;
					}
					Successful = false;
					return;
				}

				LogHelper.Error(results.ErrorException.Message);
			} else {
				Successful = true;
			}
		}

		private bool SearchPageForKeywords(CrawledPage page) {
			worker.ReportProgress(0, new Progress(Progress.Status.Searching));

			var keywordsFound = new HashSet<KeywordMatch>();
			var text = page.Content.Text;
			foreach (var keyword in job.KeywordsToSearchFor) {
				if (text.Contains(keyword.Text)) {
					// Check to see if the text is inside a link
					string context;
					LogHelper.Debug("Keyword: " + keyword.Text + " found");
					if (AnalyzePageKeyword(page, keyword, out context)) {
						keywordsFound.Add(new KeywordMatch(keyword, context));
					}
				}
			}

			if (keywordsFound.Count == 0) return false;

			Results.AddResult(page.Uri.AbsoluteUri);
			foreach (var match in keywordsFound) {
				Results.AddResultKeyword(match.Keyword, match.Context);
			}
			Results.FinalizeResult();
			return true;
		}

		private bool AnalyzePageKeyword(CrawledPage page, Keyword keyword, out string context) {
			try {
				var document = page.HtmlDocument.DocumentNode;
//				var xpathOfText = "//*[text()[contains(., '" + keyword.Text + "')]]";
				var xpathOfTag = "//text()[contains(., '" + keyword.Text + "')]/..";
				var keywordNodes = document.SelectSingleNode(xpathOfTag);

				if (keywordNodes.OuterHtml.Contains("<a")) {
					context = keywordNodes.OuterHtml;
					return false;
				}

				context = keywordNodes.OuterHtml;
				return true;
			} catch (Exception e) {
				LogHelper.Error(e.Message);
				context = "<ERROR>";
				return true;
			}

		}

		/* UNDONE: For now
		private void ExamineUrl(CrawledPage page, bool relevance) {
			var fullUrl = page.Uri.AbsoluteUri.ToLower();

			var url = fullUrl.Split('/').ToList();
			url.RemoveAt(0);
			url.RemoveAt(url.Count - 1);

			Results.AddUrlParts(url.ToArray(), relevance);
		}
		*/

		private void crawler_ProcessPageCrawlStarting(object sender, PageCrawlStartingArgs args) {
			CrawlHelper.TotalPages++;

			if (worker.CancellationPending) {
				worker.ReportProgress(0, new Progress(Progress.Status.Cancelling));
				cancelMe.Cancel();
				return;
			}

			var progress = new Progress(args.PageToCrawl.Uri.AbsoluteUri, Results.RootWebsite.Name, Results.ResultList.Count,
				CrawlHelper.CurrentDomain, Progress.Status.Crawling);
			worker.ReportProgress(0, progress);
		}

		private void crawler_ProcessPageCrawlCompleted(object sender, PageCrawlCompletedArgs args) {
			var crawledPage = args.CrawledPage;

			if (crawledPage.WebException != null || crawledPage.HttpWebResponse.StatusCode != HttpStatusCode.OK) {
				return;
			}

			if (string.IsNullOrEmpty(crawledPage.Content.Text)) {
				return;
			}

			var keywordsFound = SearchPageForKeywords(crawledPage);

			/* UNDONE: For now
			if (configuration.EnableUrlAnalysis) {
				ExamineUrl(crawledPage, keywordsFound);
			}
			*/
		}
	}
}