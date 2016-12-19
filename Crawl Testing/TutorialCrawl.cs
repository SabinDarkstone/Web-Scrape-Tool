using System;
using System.Collections.Generic;
using System.Net;

using Abot.Crawler;
using HtmlAgilityPack;

namespace Crawl_Testing {

	public class TutorialCrawl {

		private readonly List<HtmlDocument> relevantDocuments;
		public readonly List<string> ResultList;

		public TutorialCrawl() {
			// Will use app.config for configuration
			var crawler = new PoliteWebCrawler();

			// Prepare list of relevant documents
			relevantDocuments = new List<HtmlDocument>();
			ResultList = new List<string>();

			// Register events
			crawler.PageCrawlStartingAsync += crawler_ProcessPageCrawlStarting;
			crawler.PageCrawlCompletedAsync += crawler_ProcessPageCrawlCompleted;
			crawler.PageCrawlDisallowedAsync += crawler_PageCrawlDisallowed;
			crawler.PageLinksCrawlDisallowedAsync += crawler_PageLinksCrawlDisallowed;

			// Run the crawler
			var result = crawler.Crawl(new Uri("http://www.dvrpc.org/TAP/"));
			if (result.ErrorOccurred) {
				Console.WriteLine("Crawl of {0} completed with error {1}", result.RootUri.AbsoluteUri, result.ErrorException.Message);
			} else {
				Console.WriteLine("Crawl of {0} completed without error.", result.RootUri.AbsoluteUri);
			}

			PrintResults();
		}

		private void PrintResults() {
			var relevantRows = new List<string>();

			// Print out titles of pages that are relevant
			Console.WriteLine("Relevant pages found: {0}", relevantDocuments.Count);
			foreach (var page in relevantDocuments) {
				var title = page.DocumentNode.SelectSingleNode("//title").InnerText;
				Console.WriteLine(title);

				if (page.DocumentNode.SelectSingleNode("//table") != null) {
					// Table found, go through each row and look for a dollar sign(?)
					// TODO: Check if dollar sign criteria is a valid approach
					var table = page.DocumentNode.SelectSingleNode("//table");
					var rows = table.SelectNodes("//tr");

					foreach (var row in rows) {
						var text = row.InnerText;
						if (text.Contains("$") && !text.Contains("Completed") && !text.Contains("Estimate") && !text.Contains("Reassigned") && !text.Contains("Canceled")) {
							relevantRows.Add(row.InnerText.Replace("&nbsp;", " ").Replace("&nbsp", " "));
						}
					}
				}
			}
			
			// Print the results of relevant rows
			foreach (var row in relevantRows) {
				Console.WriteLine(row + "\n\n");
				ResultList.Add(row);
			}

			Console.ReadLine();
		}

		private void crawler_ProcessPageCrawlStarting(object sender, PageCrawlStartingArgs e) {
			var pageToCrawl = e.PageToCrawl;
			Console.WriteLine("About to crawl link {0} which was found on page {1}", pageToCrawl.Uri.AbsoluteUri,
				pageToCrawl.ParentUri.AbsoluteUri);
		}

		private void crawler_ProcessPageCrawlCompleted(object sender, PageCrawlCompletedArgs e) {
			var crawledPage = e.CrawledPage;

			if (crawledPage.WebException != null || crawledPage.HttpWebResponse.StatusCode != HttpStatusCode.OK) {
				Console.WriteLine("Crawl of page failed {0}", crawledPage.Uri.AbsoluteUri);
			} else {
				Console.WriteLine("Crawl of page succeeded {0}", crawledPage.Uri.AbsoluteUri);
			}

			if (string.IsNullOrEmpty(crawledPage.Content.Text)) {
				Console.WriteLine("Page has no content {0}", crawledPage.Uri.AbsoluteUri);
			}

			var doc = crawledPage.HtmlDocument;
			if (doc.DocumentNode.InnerText.Contains("grant") || doc.DocumentNode.InnerText.Contains("project") || doc.DocumentNode.InnerText.Contains("Grant") || doc.DocumentNode.InnerText.Contains("Project")) {
				relevantDocuments.Add(doc);
			}
		}

		private void crawler_PageCrawlDisallowed(object sender, PageCrawlDisallowedArgs e) {
			var pageToCrawl = e.PageToCrawl;
			Console.WriteLine("Did not crawl page {0} due to {1}", pageToCrawl.Uri.AbsoluteUri, e.DisallowedReason);
		}

		private void crawler_PageLinksCrawlDisallowed(object sender, PageLinksCrawlDisallowedArgs e) {
			var crawledPage = e.CrawledPage;
			Console.WriteLine("Did not crawl the links on page {0} due to {1}", crawledPage.Uri.AbsoluteUri, e.DisallowedReason);
		}
	}
}
