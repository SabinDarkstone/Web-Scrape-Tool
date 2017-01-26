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
using Visco_Web_Scrape_v2.Properties;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Scripts.Helpers;
using Visco_Web_Scrape_v2.Search.Items;
using static Visco_Web_Scrape_v2.Search.Items.WebsiteResults;

namespace Visco_Web_Scrape_v2.Search.Process {

	public class GrantCrawler {

		/// <summary>
		/// Results for specified website
		/// </summary>
		public WebsiteResults Results { get; }

		private PoliteWebCrawler crawler;

		private readonly Job job;
		private readonly Configuration config;
		private readonly BackgroundWorker worker;
		private readonly CancellationTokenSource cancelMe;
		private readonly Progress currentProgress;

		/// <summary>
		/// Contains information about hits on page
		/// </summary>
		[Serializable]
		public struct KeywordMatch {

			/// <summary>
			/// Keyword found on page
			/// </summary>
			public Keyword Keyword { get; }

			/// <summary>
			/// Text that the keyword appeared in
			/// </summary>
			public string Context { get; }

			/// <summary>
			/// Whether the result was found in a link
			/// </summary>
			public bool IsLink { get; }

			/// <summary>
			/// Save the data from the webpage about the hit on the page
			/// </summary>
			/// <param name="keyword">Keyword found on page</param>
			/// <param name="context">Surrounding text the keyword was found in</param>
			/// <param name="isLink">Whether the result is a link</param>
			public KeywordMatch(Keyword keyword, string context, bool isLink) {
				Keyword = keyword;
				Context = context;
				IsLink = isLink;
			}
		}

		public GrantCrawler(Job job, Configuration config, Website website, BackgroundWorker worker,
			CancellationTokenSource cancelToken) {
			// Initialize progress tracker
			currentProgress = new Progress(website);

			this.job = job;
			this.worker = worker;
			this.config = config;
			cancelMe = cancelToken;

			// Load crawl settings and events
			InitializeCrawl();
			RegisterEvents();

			// Create a new list to store results from this search
			Results = new WebsiteResults(website);

			// Run Crawler
			Run();
		}

		private void InitializeCrawl() {
			// Read settings file and overwrite app.config settings for crawler
			var crawlConfig = AbotConfigurationSectionHandler.LoadFromXml().Convert();
			crawlConfig.MaxPagesToCrawl = config.PagesToCrawlPerDomain;

			// Initialize crawler
			crawler = new PoliteWebCrawler(crawlConfig);

			if (config.EnableUrlFiltering) {
				ConfigureDecisionMaker();
			}

			if (config.EnableUrlAnalysis) {
				crawlConfig.MaxConcurrentThreads = 2;
			}
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

			// Cancellation requested
			if (cancelMe.IsCancellationRequested || crawlResults.ErrorOccurred) {
				Results.WebsiteStatus = Status.Interrupted;
				currentProgress.SearchStatus = Progress.Status.Cancelling;
			}

			// Crawl ran to completion
			if (!crawlResults.ErrorOccurred) {
				LogHelper.Debug("Reached domain page crawl limit or crawl is completed.");
				Results.WebsiteStatus = Status.Completed;
			}

			// Regardless of result, send the results off to the parent form so nothing is lost
			worker.ReportProgress(0, currentProgress);
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

			var resultToAdd = new Result(page.Uri.AbsoluteUri, keywordsFound);
			if (Results.AddResult(resultToAdd)) {
				currentProgress.Increment(Progress.PageType.Result);
				LogHelper.Info(resultToAdd);
			}
		}

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

		public PageCounts GetPageCounts() {
			return new PageCounts {
				IgnoredPages = currentProgress.IgnoredPages,
				SearchPages = currentProgress.SearchedPages
			};
		}

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