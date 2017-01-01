﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
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
			crawlConfig.MaxPagesToCrawlPerDomain = config.PagesToCrawlPerDomain;

			// Initialize crawler
			crawler = new PoliteWebCrawler(crawlConfig);
			RegisterEvents();

			if (config.EnableUrlFiltering) {
				ConfigureDecisionMaker();
			}
			if (config.EnableUrlAnalysis) {
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

				/*
				if (Reference.IgnoreWords.Any(word => url.Contains(word))) {
					CrawlHelper.SkippedPages++;
					return new CrawlDecision {Allow = false, Reason = Resources.UrlFiltered};
				}
				*/

				return crawlDecision;
			});
		}

		private void Run(string url) {
			CrawlHelper.TotalPages = 0;
			CrawlHelper.SkippedPages = 0;

			var results = crawler.Crawl(new Uri(url), cancelMe);

			parentForm.Results.AddWebsiteResults(Results);
			LogHelper.Debug("Adding results to parent form...");

			if (results.ErrorOccurred) {
				// Cancel crawl but report it as successful so that no errors are raised
				if (cancelMe.IsCancellationRequested) {
					Successful = true;
					worker.ReportProgress(0, new Progress(Progress.Status.Cancelling));
					return;
				}

				// Report that there was an error and mark crawl as unsuccessful
				Successful = false;
				MessageBox.Show(Resources.DomainCrawlError + results.ErrorException.Message, Resources.Error,
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			} else {
				// Crawl was completed successfully and without cancellation
				Successful = true;
			}
		}

		private bool SearchPageForKeywords(CrawledPage page) {
			worker.ReportProgress(0, new Progress(Progress.Status.Searching));

			var keywordsFound = new HashSet<Keyword>();
			var text = page.Content.Text;
			foreach (var keyword in job.KeywordsToSearchFor) {
				if (text.Contains(keyword.Text)) {
					keywordsFound.Add(keyword);
				}
			}

			if (keywordsFound.Count == 0) return false;

			Results.AddResult(page.Uri.AbsoluteUri);
			foreach (var keyword in keywordsFound) {
				Results.AddResultKeyword(keyword);
			}
			Results.FinalizeResult();
			return true;
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