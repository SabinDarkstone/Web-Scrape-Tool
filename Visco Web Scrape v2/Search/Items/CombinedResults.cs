using System;
using System.Collections.Generic;
using System.Linq;

namespace Visco_Web_Scrape_v2.Search.Items {

	[Serializable]
	public class CombinedResults {

		public HashSet<WebsiteResults> AllResults { get; }
		public DateTime LastRan { get; private set; }
		public TimeSpan TotalSearchTime { get; private set; }

		private DateTime startTime;

		public CombinedResults() {
			AllResults = new HashSet<WebsiteResults>();
		}

		public void Begin() {
			startTime = DateTime.UtcNow;
			foreach (var website in AllResults) {
				foreach (var result in website.ResultList) {
					result.IsNewResult = false;
				}
			}
		}

		public void End() {
			TotalSearchTime = DateTime.UtcNow - startTime;
			LastRan = startTime;
		}

		public WebsiteResults GetCurrentResults(Website website) {
			return AllResults.FirstOrDefault(i => i.RootWebsite.Equals(website));
		}

		public string GetCrawlTime() {
			return TotalSearchTime.Hours + ":" + TotalSearchTime.Minutes + ":" + TotalSearchTime.Seconds;
		}

		public void UpdateResults(WebsiteResults results) {
			if (AllResults.Any(domain => domain.RootWebsite.Url.Equals(results.RootWebsite.Url))) {
				var existingResults = GetCurrentResults(results.RootWebsite);

				if (results.ResultList.Count != 0) {
					foreach (var result in results.ResultList) {
						existingResults.AddResult(result);
					}
				}

				UpdateMetadata(results);
			} else {
				AllResults.Add(results);
			}
		}

		private void UpdateMetadata(WebsiteResults results) {
			var existingResults = GetCurrentResults(results.RootWebsite);

			existingResults.Counts = results.Counts;
			existingResults.WebsiteStatus = results.WebsiteStatus;
			existingResults.SearchTime = results.SearchTime;
		}
	}
}
