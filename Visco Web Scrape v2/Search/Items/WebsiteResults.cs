using System;
using System.Collections.Generic;
using System.Linq;
using Visco_Web_Scrape_v2.Scripts.Helpers;
using Visco_Web_Scrape_v2.Search.Process;

namespace Visco_Web_Scrape_v2.Search.Items {

	[Serializable]
	public class WebsiteResults {

		public enum Status {
			Interrupted,		// Search on website was started but never finished
			Completed,			// Search was started and finished successfully
			Skipped,			// Search never started website
			Disabled			// Search skipped due to IsEnabled being false
		}

		public class PageCounts {
			public int SearchPages { get; set; }
			public int IgnoredPaged { get; set; }
		}

		public HashSet<Result> ResultList { get; }
		public Website RootWebsite { get; }
		public TimeSpan SearchTime { get; set; }
		public Status SearchStatus { get; set; }
		public PageCounts Counts { get; set; }

		public WebsiteResults(Website website) {
			RootWebsite = website;
			ResultList = new HashSet<Result>();
		}

		public void AddResult(string url, GrantCrawler.KeywordMatch[] hits) {
			if (!ResultList.Any(i => i.PageUrl.Equals(url))) {
				ResultList.Add(new Result(url, hits, DateTime.Now));
			}
		}

		public void AddResult(Result result) {
			ResultList.Add(result);
		}

		public void AddResultRange(IEnumerable<Result> results) {
			// AddRange is not usable since we only want to look at the url and nothing else
			foreach (var page in results) {
				if (!ResultList.Any(result => result.PageUrl.Equals(page.PageUrl))) {
					ResultList.Add(page);
				}
			}
		}

		public void UpdateMetadata(WebsiteResults results) {
			if (ResultList.Any(result => result.IsNewResult)) {
				LogHelper.Debug("Updating metadata for " + RootWebsite.Name);
				Counts = results.Counts;
				SearchStatus = results.SearchStatus;
				SearchTime = results.SearchTime;
			} else {
				LogHelper.Debug("No new results were found, skipping metadata update");
			}
		}

		public void SetPageCounts(int searched, int ignored) {
			Counts = new PageCounts {
				SearchPages = searched,
				IgnoredPaged = ignored
			};
		}

		public void StartNewSearch() {
			foreach (var result in ResultList) {
				result.IsNewResult = false;
			}
		}

		public string GetCrawlTime() {
			return SearchTime.Hours + ":" + SearchTime.Minutes + ":" + SearchTime.Seconds;
		}
	}
}
