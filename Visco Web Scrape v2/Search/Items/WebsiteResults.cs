using System;
using System.Collections.Generic;
using System.Linq;
using Visco_Web_Scrape_v2.Scripts.Helpers;

namespace Visco_Web_Scrape_v2.Search.Items {

	// TODO: Add metadata update to GrantSearch class (do I want to do this in GrantSearch or GrantCrawler?)
	[Serializable]
	public class WebsiteResults {

		[Serializable]
		public class PageCounts {
			public int SearchPages { get; set; }
			public int IgnoredPages { get; set; }
		}

		public enum Status {
			Skipped,  // Not started
			Interrupted,  // Started but not finished
			Completed  // Started and finished
		}

		public Website RootWebsite { get; set; }
		public HashSet<Result> ResultList { get; private set; }
		public TimeSpan SearchTime { get; set; }
		public PageCounts Counts { get; set; }
		public Status WebsiteStatus { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="website"></param>
		public WebsiteResults(Website website) {
			// On creation, a website has to be assigned so the collection of Result objects know what domain they are part of.
			RootWebsite = website;
			// Create an empty HashSet of Result objects
			ResultList = new HashSet<Result>();
			// Set Counts and SearchTime to zero
			SearchTime = TimeSpan.Zero;
			Counts = new PageCounts {
				IgnoredPages = 0,
				SearchPages = 0
			};
		}

		// Completely clear out ResultList
		public void ClearResults() {
			ResultList.Clear();
			ResultList = new HashSet<Result>();
		}

		/// <summary>
		/// Add a new result. Compares results before merging to prevent duplication.
		/// </summary>
		/// <param name="result">Result to add</param>
		public void AddResult(Result result) {
			var matchingResult = ResultList.FirstOrDefault(i => i.PageUrl.Equals(result.PageUrl));
			if (matchingResult == null) {
				ResultList.Add(result);
			}
		}
	}
}
