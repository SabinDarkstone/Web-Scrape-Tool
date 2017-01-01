using System;
using System.Collections.Generic;
using System.Linq;
using Visco_Web_Scrape_v2.Scripts.Helpers;

namespace Visco_Web_Scrape_v2.Search.Items {

	[Serializable]
	public class CombinedResults {

		public HashSet<WebsiteResults> AllResults { get; }
		public DateTime LastRan { get; private set; }

		private int totalCrawlTime;

		public CombinedResults() {
			AllResults = new HashSet<WebsiteResults>();
		}

		public void StartNewSearch() {
			LastRan = DateTime.UtcNow;

			if (AllResults.Count == 0) return;
			foreach (var website in AllResults) {
				website.StartNewSearch();
			}
		}

		public HashSet<WebsiteResults> GetNewResults() {
			var onlyNewResults = new HashSet<WebsiteResults>();
			foreach (var website in AllResults) {

				var thisList = new WebsiteResults(website.RootWebsite);
				foreach (var result in website.ResultList) {
					if (result.IsNewResult) thisList.AddResult(result);
				}
				onlyNewResults.Add(thisList);

			}

			return onlyNewResults;
		}

		public void AddWebsiteResults(WebsiteResults resultsToAdd) {
			if (AllResults.Any(i => i.RootWebsite.Url.Equals(resultsToAdd.RootWebsite.Url))) {
				LogHelper.Debug("Website results with same url already exists, merging lists...");
				var website = AllResults.FirstOrDefault(i => i.RootWebsite.Url.Equals(resultsToAdd.RootWebsite.Url));
				LogHelper.Debug("Website found: " + website.RootWebsite.Url);

				foreach (var result in resultsToAdd.ResultList) {
					website.AddResult(result);
				}
			} else {
				AllResults.Add(resultsToAdd);
			}
		}

		public string GetCrawlTime() {
			var seconds = totalCrawlTime % 60;
			var minutes = totalCrawlTime / 60;
			var hours = totalCrawlTime / 3600;

			return ((hours < 10) ? "0" + hours : hours.ToString()) +
				((minutes < 10) ? "0" + minutes : minutes.ToString()) +
				((seconds < 10) ? "0" + seconds : seconds.ToString());
		}

		public void SetCrawlTime(int seconds) => totalCrawlTime = seconds;
	}
}
