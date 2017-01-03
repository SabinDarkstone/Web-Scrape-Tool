using System;
using System.Collections.Generic;
using System.Linq;
using Visco_Web_Scrape_v2.Scripts.Helpers;

namespace Visco_Web_Scrape_v2.Search.Items {

	[Serializable]
	public class WebsiteResults {

		[Serializable]
		public class Result {

			public string PageUrl { get; }
			public HashSet<Keyword> KeywordsOnPage { get; }
			public DateTime DiscoveryTimeUtc { get; set; }
			public string Context { get; set; }
			public bool IsNewResult { get; set; }

			public Result(string url) {
				PageUrl = url;
				KeywordsOnPage = new HashSet<Keyword>();
			}

			public void AddKeyword(Keyword keyword) {
				KeywordsOnPage.Add(keyword);
			}
		}

		public HashSet<Result> ResultList { get; }
		public Website RootWebsite { get; }

		public int SearchTimeInSeconds { get; set; }
		public bool StartedSearch { get; set; }
		public bool CompletedSearch { get; set; }
		public int CrawledPages { get; set; }
		public int SkippedPages { get; set; }

		private Result newResult;

		public WebsiteResults(Website website) {
			RootWebsite = website;
			ResultList = new HashSet<Result>();
		}

		public void AddResult(string pageUrl) {
			newResult = new Result(pageUrl);
		}

		public void AddResultKeyword(Keyword keyword, string context = "") {
			newResult.AddKeyword(keyword);
			newResult.Context = context;
		}

		public void FinalizeResult() {
			if (newResult == null || ResultList.Contains(newResult)) return;

			if (ResultList.Any(i => i.Context.Equals(newResult.Context))) {
				newResult = null;
				return;
			}

			newResult.DiscoveryTimeUtc = DateTime.UtcNow;
			newResult.IsNewResult = true;
			ResultList.Add(newResult);
			newResult = null;
		}

		public void StartNewSearch() {
			foreach (var result in ResultList) {
				result.IsNewResult = false;
			}
		}

		public void AddResult(Result result) {
			newResult = result;
			newResult.DiscoveryTimeUtc = DateTime.UtcNow;
			if (!ResultList.Any(i => i.PageUrl.Equals(result.PageUrl)) && !ResultList.Any(i => i.Context.Equals(result.Context))) {
				LogHelper.Debug("Adding new result: " + newResult.PageUrl);
				newResult.IsNewResult = true;
				ResultList.Add(newResult);
			} else {
				LogHelper.Debug("Result already exists, skipping " + newResult.PageUrl);
			}
			newResult = null;
		}

		public string GetCrawlTime() {
			var seconds = SearchTimeInSeconds % 60;
			var minutes = SearchTimeInSeconds / 60;
			var hours = SearchTimeInSeconds / 3600;

			return ((hours < 10) ? "0" + hours : hours.ToString()) + ":" +
				((minutes < 10) ? "0" + minutes : minutes.ToString()) + ":" +
				((seconds < 10) ? "0" + seconds : seconds.ToString());
		}
	}
}
