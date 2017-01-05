using System;
using System.Collections.Generic;
using System.Linq;

namespace Visco_Web_Scrape_v2.Search.Items {

	[Serializable]
	public class CombinedResults {

		public HashSet<WebsiteResults> AllResults { get; }
		public DateTime LastRan { get; private set; }
		public TimeSpan TotalSearchTime;

		private DateTime startTime;

		public CombinedResults() {
			AllResults = new HashSet<WebsiteResults>();
		}

		public void Begin() {
			startTime = DateTime.UtcNow;
		}

		public void End() {
			TotalSearchTime = DateTime.UtcNow - startTime;
		}

		public void StartNewSearch() {
			LastRan = DateTime.UtcNow;

			if (AllResults.Count == 0) return;
			foreach (var websiteResult in AllResults) {
				websiteResult.StartNewSearch();
			}
		}

		public HashSet<WebsiteResults> GetNewResults() {
			var newResults = new HashSet<WebsiteResults>();

			foreach (var websiteResult in AllResults) {
				var innerList = new WebsiteResults(websiteResult.RootWebsite);
				foreach (var result in websiteResult.ResultList) {
					if (!result.IsNewResult) {
						innerList.AddResult(result);
					}
				}
				newResults.Add(innerList);
			}

			return newResults;
		}

		public void AddWebsiteResults(WebsiteResults resultsToAdd) {
			if (AllResults.Any(results => results.RootWebsite.Url.Equals(resultsToAdd.RootWebsite.Url))) {
				var website = AllResults.FirstOrDefault(i => i.RootWebsite.Url.Equals(resultsToAdd.RootWebsite.Url));
				if (website != null) {
					website.AddResultRange(resultsToAdd.ResultList);
					website.UpdateMetadata(resultsToAdd);
				}
			}
		}

		public string GetCrawlTime() {
			return TotalSearchTime.Hours + ":" + TotalSearchTime.Minutes + ":" + TotalSearchTime.Seconds;
		}
	}
}
