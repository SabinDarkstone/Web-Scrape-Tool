using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Abot.Crawler;
using Abot.Poco;

namespace Testing {

	public class Program {

		private static readonly List<string> KeywordList = new List<string>();

		public static void Main(string[] args) {

			while (true) {
				Console.WriteLine("The keyword list contains {0} items.  Do you want to add a new keyword? (y/n/list)", KeywordList.Count);
				var readLine = Console.ReadLine();
				if (readLine == null) continue;

				var response = readLine.ToLower();
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
				try {
					var myCrawler = new TestCrawler(url, KeywordList.ToArray());
					OutputResults(myCrawler);
				} catch (ArgumentNullException ex) {
					Console.WriteLine("[Error]: {0}", ex.Message);
				}

			}
		}

		private static void AddKeyword() {
			Console.WriteLine("Enter the new keyword to be added.");
			var readLine = Console.ReadLine();
			if (readLine == null) throw new ArgumentNullException("Blank keywords are not allowed");

			var response = readLine.ToLower();
			if (KeywordList.Contains(response)) {
				Console.WriteLine("Keyword already exists.");
			} else {
				KeywordList.Add(response);
			}
		}

		private static void OutputResults(TestCrawler crawler) {
			Console.WriteLine("\n\nResults of crawl:");
			if (!crawler.Successful) return;

			var results = crawler.SearchResults;

			// Output to console and file
			var streamWriter = new StreamWriter("Result.txt");
			foreach (var keyword in KeywordList) {
				Console.WriteLine(keyword + ":");
				streamWriter.WriteLine(keyword + ":");
				foreach (var result in results.ResultList) {
					foreach (var keywordInResult in result.Keywords) {
						if (keywordInResult.Equals(keyword)) {
							Console.WriteLine("\t" + result.Url);
							streamWriter.WriteLine("\t" + result.Url);
						}
					}
				}
			}
			streamWriter.Close();
		}

		private static void ShowKeywordList() {
			var keywords = KeywordList.Aggregate("", (current, keyword) => current + (keyword + ", "));
			keywords = keywords.Substring(0, keywords.Length - 2);
			Console.WriteLine(keywords);
		}
	}

	public class TestCrawler {

		private readonly string[] keywordList;

		public bool Successful { get; }
		public string RootUri { get; }
		public SearchResults SearchResults { get; }

		public TestCrawler(string url, string[] keywords) {
			if (string.IsNullOrEmpty(url)) throw new ArgumentNullException("You cannot have a blank URL.");
			if (keywords.Length == 0) throw new ArgumentNullException("The keyword list cannbot be empty.");
			if (url.StartsWith("www")) {
				Console.WriteLine("[Warning]: Malformed url entered, fixing...");
				var myUrl = "http://" + url;
				url = myUrl;
				Console.WriteLine("[Info]: Url format corrected successfully.");
			}

			keywordList = keywords;
			RootUri = url;
			SearchResults = new SearchResults(RootUri);
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

		private void SearchPageForKeywords(CrawledPage page) {
			var pageText = page.Content.Text.ToLower();
			foreach (var keyword in keywordList) {
				if (pageText.Contains(keyword)) {
					SearchResults.AddNewResult(page, keyword);
				}
			}
		}

		#region Crawler Events
		private void crawler_ProcessPageCrawlStarting(object sender, PageCrawlStartingArgs e) {
			var pageToCrawl = e.PageToCrawl;
			Console.WriteLine("About to crawl link {0} which was found on page {1}", pageToCrawl.Uri.AbsoluteUri, pageToCrawl.ParentUri.AbsoluteUri);
		}

		private void crawler_ProcessPageCrawlCompleted(object sender, PageCrawlCompletedArgs e) {
			var crawledPage = e.CrawledPage;

			if (crawledPage.WebException != null || crawledPage.HttpWebResponse.StatusCode != HttpStatusCode.OK) {
				Console.WriteLine("Crawl of page failed {0}", crawledPage.Uri.AbsoluteUri);
				return;
			} else {
				Console.WriteLine("Crawl of page succeeded {0}", crawledPage.Uri.AbsoluteUri);
			}

			if (string.IsNullOrEmpty(crawledPage.Content.Text)) {
				Console.WriteLine("Page had no content {0}", crawledPage.Uri.AbsoluteUri);
				return;
			} else {
				SearchPageForKeywords(crawledPage);
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
		#endregion
	}

	public class Result {

		public string Url { get; set; }
		public List<string> Keywords { get; set; }

		public Result(string url, string keyword) {
			Url = url;
			Keywords = new List<string> {keyword};
		}

		public Result(string url) {
			Url = url;
		}

		public override string ToString() {
			var myString = Url + " contains ";
			myString = Keywords.Aggregate(myString, (current, keyword) => current + (keyword + ", "));
			return myString.Substring(0, myString.Length - 2);
		}
	}

	public class SearchResults {
		
		public string RootUrl { get; }
		public List<Result> ResultList { get; set; }

		public SearchResults(string rootUrl) {
			ResultList = new List<Result>();
			RootUrl = rootUrl;
		}

		public void AddNewResult(CrawledPage page, string keyword) {
			var index = CheckForDuplicates(page);
			if (index == -1) {
				ResultList.Add(new Result(page.Uri.AbsoluteUri, keyword));
			} else {
				var result = ResultList[index];
				result.Keywords.Add(keyword);
			}
		}

		private int CheckForDuplicates(CrawledPage page) {
			var myResult = new Result(page.Uri.AbsoluteUri);

			foreach (var result in ResultList) {
				if (myResult.Url.Equals(result.Url)) {
					return ResultList.IndexOf(result);  // Matching url found
				}
			}
			return -1; // No matching url found
		}
	}
}
