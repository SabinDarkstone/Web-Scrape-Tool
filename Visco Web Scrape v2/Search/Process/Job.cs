using System;
using System.Collections.Generic;
using System.Linq;
using Visco_Web_Scrape_v2.Search.Items;

namespace Visco_Web_Scrape_v2.Search.Process {

	[Serializable]
	public class Job {

		/// <summary>
		/// List of selected websites to search
		/// </summary>
		public HashSet<Website> WebsitesToCrawl { get; }

		/// <summary>
		/// List of keywords to search for on webpages
		/// </summary>
		public HashSet<Keyword> KeywordsToSearchFor { get; }

		/// <summary>
		/// List of keywords to ignore in urls
		/// </summary>
		public HashSet<Keyword> UrlBlacklist { get; }

		/// <summary>
		/// Whether the job is initiated automatically by scheduler
		/// </summary>
		public bool IsScheduledJob { get; }

		public Job(IEnumerable<Website> websites, IEnumerable<Keyword> whitelist, IEnumerable<Keyword> blacklist, bool isScheduled = false) {
			WebsitesToCrawl = new HashSet<Website>(websites.Where(site => site.IsEnabled));
			KeywordsToSearchFor = new HashSet<Keyword>(whitelist.Where(keyword => keyword.IsEnabled));
			UrlBlacklist = new HashSet<Keyword>(blacklist.Where(keyword => keyword.IsEnabled));

			IsScheduledJob = isScheduled;
		}
	}
}
