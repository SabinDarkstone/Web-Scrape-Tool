using System;
using System.Collections.Generic;
using System.Linq;
using Abot.Poco;
using Visco_Web_Scrape_v2.Scripts.Helpers;

namespace Visco_Web_Scrape_v2.Search.Items {

	[Serializable]
	public class SearchResults {

		[Serializable]
		public class UrlParts {

			public UrlParts() {
				WithKeyword = new Dictionary<string, int>();
				WithoutKeyword = new Dictionary<string, int>();
			}

			public Dictionary<string, int> WithKeyword { get; set;}
			public Dictionary<string, int> WithoutKeyword { get; set; }
		}

		public string RootUrl { get; }
		public string Name { get; }
		public List<Result> ResultList { get; set; }
		public UrlParts UrlDetails { get; set; }

		public SearchResults() {
			RootUrl = null;
			ResultList = new List<Result>();
			UrlDetails = new UrlParts();
		}

		public SearchResults(string rootUrl) {
			ResultList = new List<Result>();
			UrlDetails = new UrlParts();
			RootUrl = rootUrl;
		}

		public SearchResults(Website website) {
			ResultList = new List<Result>();
			UrlDetails = new UrlParts();
			RootUrl = website.Url;
			Name = website.Name;
		}

		public void AddNewResult(CrawledPage page, Keyword keyword) {
			var index = CheckForDuplicates(page);
			if (index == -1) {
				ResultList.Add(new Result(page.Uri.AbsoluteUri, keyword));
			} else {
				var result = ResultList[index];
				result.Keywords.Add(keyword);
			}
		}

		public void AddNewResult(Result result) {
			ResultList.Add(result);
		}

		public void AddUrlParts(string[] parts, bool keywordsFound) {
			foreach (var part in parts) {
				if (part.Contains("www.")) continue;
				if (string.IsNullOrWhiteSpace(part)) continue;
				if (part.Contains("title=")) continue;
				if (part.Contains("feedback?")) continue;
				if (part.Length == 1) continue;
				if (part.Contains("=")) continue;

				if (!UrlDetails.WithKeyword.Keys.Contains(part)) {
					LogHelper.Debug("Adding url part: " + part);
					UrlDetails.WithKeyword.Add(part, 0);
				}

				if (!UrlDetails.WithoutKeyword.Keys.Contains(part)) {
					UrlDetails.WithoutKeyword.Add(part, 0);
				}

				if (keywordsFound) {
					UrlDetails.WithKeyword[part]++;
				} else {
					UrlDetails.WithoutKeyword[part]++;
				}
			}
		}

		private int CheckForDuplicates(CrawledPage page) {
			var myResult = new Result(page.Uri.AbsoluteUri);

			foreach (var result in ResultList) {
				if (myResult.Url.Equals(result.Url)) {
					return ResultList.IndexOf(result); // Matching url found
				}
			}
			return -1; // No matching url found
		}

		public bool CheckExists(Result result) {
			return ResultList.Any(website => website.Url.Equals(result.Url));
		}

	}

}