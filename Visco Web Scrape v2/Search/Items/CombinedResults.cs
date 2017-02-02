using System;
using System.Collections.Generic;
using System.Linq;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Scripts.Helpers;

namespace Visco_Web_Scrape_v2.Search.Items {

	[Serializable]
	public class CombinedResults {

		/// <summary>
		/// Complete list of all results, organized by website
		/// </summary>
		public HashSet<WebsiteResults> AllResults { get; }

		/// <summary>
		/// Date and time the last search was started
		/// </summary>
		public DateTime LastRan { get; set; }

		/// <summary>
		/// Length of time previous search ran for
		/// </summary>
		public TimeSpan TotalSearchTime { get; set; }

		private DateTime startTime;

		public CombinedResults() {
			AllResults = new HashSet<WebsiteResults>();
		}

		public static HashSet<WebsiteResults> FillWithBlanks(Configuration configToFollow) {
			var blankSet = new HashSet<WebsiteResults>();
			foreach (var website in configToFollow.Websites) {
				var blankResultsList = new WebsiteResults(website);
				blankSet.Add(blankResultsList);
			}

			return blankSet;
		}

		public void Begin() {
			startTime = DateTime.UtcNow;
			foreach (var website in AllResults) {
				website.Counts.Reset();
				website.SearchTime = TimeSpan.Zero;
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
//			return AllResults.FirstOrDefault(i => i.RootWebsite.Equals(website));
			foreach (var websiteResults in this.AllResults) {
				if (websiteResults.RootWebsite.Url.Equals(website.Url)) {
					return websiteResults;
				}
			}

			LogHelper.Debug("No matching website found");
			return null;
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

		public void AddResults(WebsiteResults results, bool includeData) {
			if (includeData) {
				AllResults.Add(results);
			} else {
				var website = new WebsiteResults(results.RootWebsite) {
					Counts = results.Counts,
					SearchTime = results.SearchTime,
					WebsiteStatus = results.WebsiteStatus
				};
				AllResults.Add(website);
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
