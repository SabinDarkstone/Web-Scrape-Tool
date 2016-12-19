using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Abot.Crawler;
using Abot.Poco;

namespace Testing {

	public class Program {

		private static List<string> keywordList = new List<string>();

		public static void Main(string[] args) {

			while (true) {
				Console.WriteLine("The keyword list contains {0} items.  Do you want to add a new keyword? (y/n/list)", keywordList.Count);
				var response = Console.ReadLine().ToLower();
				if (response == null) continue;
				if (response.Equals("y")) {
					AddKeyword();
				} else if (response.Equals("n")) {
					break;
				} else if (response.Equals("list")) {
					ShowKeywordList();
				} else {
					Console.WriteLine("Invalid response, please try again.");
				}
			}

			while (true) {
				Console.WriteLine("\nEnter the root URL that you wish to crawl.");
				var url = Console.ReadLine();
				var myCrawler = new TestCrawler(url, keywordList.ToArray());

				OutputResults(myCrawler);
			}
		}

		private static void AddKeyword() {
			Console.WriteLine("Enter the new keyword to be added.");
			var response = Console.ReadLine().ToLower();
			if (keywordList.Contains(response)) {
				Console.WriteLine("Keyword already exists.");
			} else {
				keywordList.Add(response);
			}
		}

		private static void OutputResults(TestCrawler crawler) {
			Console.WriteLine("\n\nResults of crawl:");
			if (crawler.Successful) {
				if (crawler.UrlResults == null || crawler.UrlResults.Length == 0) {
					Console.WriteLine("No results were found for the given keywords on {0}", crawler.RootUri);
					return;
				}
				var streamWriter = new StreamWriter("Results.txt");
				var urls = crawler.UrlResults;
				for (var i = 0; i < urls.Length; i++) {
					Console.WriteLine("[{0}]: {1}", i + 1, urls[i]);
					streamWriter.WriteLine(urls[i]);
				}
				streamWriter.Close();
			}
		}

		private static void ShowKeywordList() {
			var keywords = "";
			foreach (var keyword in keywordList) {
				keywords += keyword + ", ";
			}
			keywords = keywords.Substring(0, keywords.Length - 2);
			Console.WriteLine(keywords);
		}
	}

	public class TestCrawler {

		private List<string> urlResults = new List<string>();
		private string[] keywordList;

		public string[] UrlResults => urlResults.ToArray();
		public bool Successful { get; }
		public string RootUri { get; }

		public TestCrawler(string url, string[] keywords) {
			if (string.IsNullOrEmpty(url)) return;

			keywordList = keywords;
			RootUri = url;
			var crawler = new PoliteWebCrawler();

			crawler.PageCrawlStartingAsync += crawler_ProcessPageCrawlStarting;
			crawler.PageCrawlCompletedAsync += crawler_ProcessPageCrawlCompleted;
			crawler.PageCrawlDisallowedAsync += crawler_PageCrawlDisallowed;
			crawler.PageLinksCrawlDisallowedAsync += crawler_PageLinksCrawlDisallowed;

			var result = crawler.Crawl(new Uri(url));
			if (result.ErrorOccurred) {
				Console.WriteLine("Crawl of {0} completed with error {1}", result.RootUri.AbsoluteUri, result.ErrorException.Message);
				Successful = false;
			} else {
				Console.WriteLine("Crawl of {0} completed without error", result.RootUri.AbsoluteUri);
				Successful = true;
			}
		}

		private bool CheckForKeywords(CrawledPage page) {
			return keywordList.Any(keyword => page.Content.Text.ToLower().Contains(keyword));
		}

		private void AddPageToList(Uri url) {
			if (!urlResults.Contains(url.AbsoluteUri)) {
				urlResults.Add(url.AbsoluteUri);
			}
		}

		private void crawler_ProcessPageCrawlStarting(object sender, PageCrawlStartingArgs e) {
			var pageToCrawl = e.PageToCrawl;
			Console.WriteLine("About to crawl link {0} which was found on page {1}", pageToCrawl.Uri.AbsoluteUri, pageToCrawl.ParentUri.AbsoluteUri);
		}

		private void crawler_ProcessPageCrawlCompleted(object sender, PageCrawlCompletedArgs e) {
			var crawledPage = e.CrawledPage;

			if (crawledPage.WebException != null || crawledPage.HttpWebResponse.StatusCode != HttpStatusCode.OK)
				Console.WriteLine("Crawl of page failed {0}", crawledPage.Uri.AbsoluteUri);
			else
				Console.WriteLine("Crawl of page succeeded {0}", crawledPage.Uri.AbsoluteUri);

			if (string.IsNullOrEmpty(crawledPage.Content.Text))
				Console.WriteLine("Page had no content {0}", crawledPage.Uri.AbsoluteUri);

			if (CheckForKeywords(crawledPage)) {
				AddPageToList(crawledPage.Uri);
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
