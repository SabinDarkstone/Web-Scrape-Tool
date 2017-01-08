using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading;
using Abot.Core;
using Abot.Crawler;
using Abot.Poco;
using CsQuery.ExtensionMethods.Internal;
using Visco_Web_Scrape_v2.Forms;
using Visco_Web_Scrape_v2.Properties;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Scripts.Helpers;
using Visco_Web_Scrape_v2.Search.Items;

namespace Visco_Web_Scrape_v2.Search.Process {

	public class GrantCrawler {

		public WebsiteResults Results { get; }

		private readonly PoliteWebCrawler crawler;
		private readonly Job job;
		private readonly Configuration config;
		private readonly BackgroundWorker worker;
		private readonly CancellationTokenSource cancelMe;
		private readonly GrantSearch parentForm;

		private readonly Progress currentProgress;

		[Serializable]
		public struct KeywordMatch {

			public Keyword Keyword { get; }
			public string Context { get; }
			public bool IsLink { get; }

			public KeywordMatch(Keyword keyword, string context, bool isLink) {
				Keyword = keyword;
				Context = context;
				IsLink = isLink;
			}
		}

		public GrantCrawler(Job job, Configuration config, Website website, BackgroundWorker worker,
			CancellationTokenSource cancelToken, GrantSearch searchForm) {
			// Initialize progress tracker
			currentProgress = new Progress(website);

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
			Run();
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
					currentProgress.Increment(Progress.PageType.Ignored);
					Results.Counts.IgnoredPages++;
					return new CrawlDecision {Allow = false, Reason = Resources.BadExtension};
				}

				if (config.UrlWords.Any(word => url.Contains(word.Text) && word.IsEnabled)) {
					currentProgress.Increment(Progress.PageType.Ignored);
					Results.Counts.IgnoredPages++;
					return new CrawlDecision {Allow = false, Reason = Resources.UrlFiltered};
				}

				return crawlDecision;
			});
		}

		private void Run() {
			// Start crawler with the cancellation token in mind
			var url = VerifyCrawl();
			var crawlResults = crawler.Crawl(new Uri(url), cancelMe);

			if (cancelMe.IsCancellationRequested) {
				LogHelper.Info("Skipping website " + currentProgress.Website.Url);
				if (Results.Counts.SearchPages > 0) {
					Results.WebsiteStatus = WebsiteResults.Status.Interrupted;
				} else {
					Results.WebsiteStatus = WebsiteResults.Status.Skipped;
				}
			} else {
				LogHelper.Info("Beginning crawl of " + currentProgress.Website.Url);
				if (crawlResults.ErrorOccurred) {
					if (cancelMe.IsCancellationRequested) {
						// No error really occurred, only a user-initiated cancel
						LogHelper.Debug("Crawl cancelled by user");
						Results.WebsiteStatus = WebsiteResults.Status.Interrupted;
						currentProgress.SearchStatus = Progress.Status.Cancelling;
						worker.ReportProgress(0, currentProgress);
					} else {
						// An error has occurred
						LogHelper.Error(crawlResults.ErrorException.Message);
						Results.WebsiteStatus = WebsiteResults.Status.Completed;
					}
				}
			}

			LogHelper.Info("Results found: " + Results.ResultList.Count);
			parentForm.Results.UpdateResults(Results);
		}

		private void SearchPageForKeywords(CrawledPage page) {
			currentProgress.SearchStatus = Progress.Status.Searching;
			worker.ReportProgress(0, currentProgress);

			var keywordsFound = new HashSet<KeywordMatch>();
			var text = page.Content.Text;
			foreach (var keyword in job.KeywordsToSearchFor) {
				if (text.Contains(keyword.Text)) {
					LogHelper.Debug("Keyword: " + keyword.Text + " found");
					keywordsFound.AddRange(AnalyzePageKeyword(page, keyword));
				}
			}

			if (keywordsFound.Count == 0) return;

			currentProgress.Increment(Progress.PageType.Result);
			var resultToAdd = new Result(page.Uri.AbsoluteUri, keywordsFound);
			LogHelper.Info(resultToAdd);
			Results.AddResult(resultToAdd);
		}

		// TODO: Cleanup
		private List<KeywordMatch> AnalyzePageKeyword(CrawledPage page, Keyword keyword) {
			var document = page.HtmlDocument.DocumentNode;
			var xpathOfTag = "//text()[contains(., '" + keyword.Text + "')]/..";
			var keywordNodes = document.SelectNodes(xpathOfTag);

			var output = new List<KeywordMatch>();
			foreach (var node in keywordNodes) {
				if (node.OuterHtml.Contains("<a")) {
					output.Add(new KeywordMatch(keyword, node.InnerText, true));
				} else {
					output.Add(new KeywordMatch(keyword, node.InnerText, false));
				}
			}

			foreach (var item in output) {
				LogHelper.Debug(item.Keyword + " " + item.Context + " " + page.Uri.AbsoluteUri);
			}
			return output;
		}

		public WebsiteResults.PageCounts GetPageCounts() {
			return new WebsiteResults.PageCounts {
				IgnoredPages = currentProgress.IgnoredPages,
				SearchPages = currentProgress.SearchedPages
			};
		}

		// TODO: Report progress
		private void crawler_ProcessPageCrawlStarting(object sender, PageCrawlStartingArgs args) {
			currentProgress.SearchStatus = Progress.Status.Crawling;
			Results.Counts.SearchPages++;
			currentProgress.Increment(Progress.PageType.Searched);
			if (worker.CancellationPending) {
				currentProgress.SearchStatus = Progress.Status.Cancelling;
				cancelMe.Cancel();
				worker.ReportProgress(0, currentProgress);
				return;
			}

			worker.ReportProgress(0, currentProgress);
		}

		private void crawler_ProcessPageCrawlCompleted(object sender, PageCrawlCompletedArgs args) {
			var crawledPage = args.CrawledPage;

			if (crawledPage.WebException != null || crawledPage.HttpWebResponse.StatusCode != HttpStatusCode.OK) {
				return;
			}

			if (string.IsNullOrEmpty(crawledPage.Content.Text)) {
				return;
			}

			SearchPageForKeywords(crawledPage);

			currentProgress.CurrentUrl = crawledPage.Uri.AbsoluteUri;
			worker.ReportProgress(0, currentProgress);
		}
	}

}