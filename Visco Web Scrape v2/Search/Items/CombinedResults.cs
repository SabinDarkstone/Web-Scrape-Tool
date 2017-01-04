using System;
using System.Collections.Generic;
using System.Linq;
using Visco_Web_Scrape_v2.Scripts.Helpers;
using Visco_Web_Scrape_v2.Search.Process;

namespace Visco_Web_Scrape_v2.Search.Items {

	[Serializable]
	public class CombinedResults {

		[Serializable]
		public class History {

			public DateTime StartTime { get; }
			public DateTime EndTime { get; set; }
			public Job JobInformation { get; }
			public CrawlHelper.SearchCompletion Completion { get; set; }
			public HashSet<Website> CompletedWebsites { get; set; }

			public History(DateTime start, Job job) {
				StartTime = start;
				JobInformation = job;
				CompletedWebsites = new HashSet<Website>();
			}
		}

		public HashSet<WebsiteResults> AllResults { get; }
		public DateTime LastRan { get; private set; }

		public HashSet<History> SearchHistory { get; set; }

		private int totalCrawlTime;

		public CombinedResults() {
			AllResults = new HashSet<WebsiteResults>();

			/* UNDONE search history for stability
			if (SearchHistory == null) {
				SearchHistory = new HashSet<History>();
			}
			*/
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
				website.StartedSearch = resultsToAdd.StartedSearch;
				website.CompletedSearch = resultsToAdd.CompletedSearch;
				website.SkippedPages = resultsToAdd.SkippedPages;
				website.CrawledPages = resultsToAdd.CrawledPages;
				website.SearchTimeInSeconds = resultsToAdd.SearchTimeInSeconds;
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
