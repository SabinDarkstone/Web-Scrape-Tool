using System;
using System.Collections.Generic;
using Abot.Poco;

namespace Visco_Web_Scrape_v2.Search.Items {

	[Serializable]
	public class SearchResults {

		public string RootUrl { get; }
		public string Name { get; }
		public List<Result> ResultList { get; set; }

		public SearchResults() {
			RootUrl = null;
			ResultList = new List<Result>();
		}

		public SearchResults(string rootUrl) {
			ResultList = new List<Result>();
			RootUrl = rootUrl;
		}

		public SearchResults(Website website) {
			ResultList = new List<Result>();
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
