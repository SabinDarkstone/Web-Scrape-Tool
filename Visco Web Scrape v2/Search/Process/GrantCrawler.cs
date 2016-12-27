using System;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Abot.Core;
using Abot.Crawler;
using Abot.Poco;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Scripts.Helpers;
using Visco_Web_Scrape_v2.Search.Items;

namespace Visco_Web_Scrape_v2.Search.Process {

	public class GrantCrawler {

		public bool Successful { get; private set; }
		public SearchResults Results { get; }

		private readonly PoliteWebCrawler crawler;
		private readonly Website website;
		private readonly Configuration configuration;
		private readonly BackgroundWorker worker;
		private readonly CancellationTokenSource cancelMe;

		public GrantCrawler(Configuration configuration, Website website, BackgroundWorker worker,
			CancellationTokenSource cancelToken) {
			this.website = website;
			this.configuration = configuration;
			this.worker = worker;
			this.cancelMe = cancelToken;

			Results = new SearchResults(this.website);

			// Check format of URL
			var url = VerifyCrawl();

			// Read settings file and overwrite app.config settings for crawler
			var crawlConfig = AbotConfigurationSectionHandler.LoadFromXml().Convert();
			crawlConfig.MaxPagesToCrawlPerDomain = configuration.PagesToCrawlPerDomain;

			// Initialize crawler
			crawler = new PoliteWebCrawler(crawlConfig);
			RegisterEvents();
			ConfigureDecisionMaker();

			// Run Crawler
			Run(url);
		}

		private string VerifyCrawl() {
			var keywords = this.configuration.Keywords;
			if (keywords == null || keywords.Count == 0) throw new ArgumentNullException(nameof(keywords));

			var url = website.Url;
			if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(website));

			return url.StartsWith("www.") ? "http://" + url : url;
		}

		private void RegisterEvents() {
			crawler.PageCrawlStartingAsync += crawler_ProcessPageCrawlStarting;
			crawler.PageCrawlCompletedAsync += crawler_ProcessPageCrawlCompleted;
		}

		private void ConfigureDecisionMaker() {
			if (configuration.EnableUrlFiltering) {
				crawler.ShouldCrawlPage((pageToCrawl, crawlContext) => {
					var url = pageToCrawl.Uri.AbsoluteUri.ToLower();
					var crawlDecision = new CrawlDecision {Allow = true};

					if (Reference.IgnoreExtensions.Any(word => url.Contains(word))) {
						CrawlHelper.SkippedPages++;
						return new CrawlDecision { Allow = false, Reason = "Url contains unreadable extension."};
					}

					if (Reference.IgnoreWords.Any(word => url.Contains(word))) {
						CrawlHelper.SkippedPages++;
						return new CrawlDecision {Allow = false, Reason = "Url contains irrelevant word."};
					}

					return crawlDecision;
				});
			}
		}

		private void Run(string url) {
			CrawlHelper.TotalPages = 0;
			CrawlHelper.SkippedPages = 0;

			var results = crawler.Crawl(new Uri(url), cancelMe);

			if (results.ErrorOccurred) {
				// Cancel crawl but report it as successful so that no errors are raised
				if (cancelMe.IsCancellationRequested) {
					Successful = true;
					worker.ReportProgress(0, new Progress(Progress.Status.Cancelled));
					return;
				}

				// Report that there was an error and mark crawl as unsuccessful
				Successful = false;
				MessageBox.Show("Crawl of page" + url + " completed with error: " + results.ErrorException.Message, "Crawling Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			} else {
				// Crawl was completed successfully and without cancellation
				Successful = true;
			}
		}

		private void SearchPageForKeywords(CrawledPage page) {
			var progress = new Progress(page.Uri.AbsoluteUri, website.Name, Results.ResultList.Count,
				configuration.Websites.IndexOf(website), Progress.Status.Searching);
			worker.ReportProgress(0, progress);

			var pageText = page.Content.Text.ToLower();
			foreach (var keyword in configuration.Keywords) {
				if (pageText.Contains(keyword.Text.ToLower())) {
					Results.AddNewResult(page, keyword);
				}
			}
		}

		private void crawler_ProcessPageCrawlStarting(object sender, PageCrawlStartingArgs args) {
			CrawlHelper.TotalPages++;

			if (worker.CancellationPending) {
				worker.ReportProgress(0, new Progress(Progress.Status.Cancelling));
				cancelMe.Cancel();
				return;
			}

			var progress = new Progress(args.PageToCrawl.Uri.AbsoluteUri, website.Name, Results.ResultList.Count,
				configuration.Websites.IndexOf(website), Progress.Status.Crawling);
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

			SearchPageForKeywords(crawledPage);
		}

	}

}