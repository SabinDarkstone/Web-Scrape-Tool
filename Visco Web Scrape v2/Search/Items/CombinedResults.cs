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
			foreach (var result in results.ResultList) {
				var firstOrDefault = AllResults.FirstOrDefault(i => i.RootWebsite.Url.Equals(results.RootWebsite.Url));
				if (firstOrDefault != null) {
					firstOrDefault.AddResult(result);
				} else {
					AllResults.Add(results);
				}
			}
		}
	}
}
