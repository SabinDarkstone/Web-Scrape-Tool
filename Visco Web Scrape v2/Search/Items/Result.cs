using System;
using System.Collections.Generic;
using Visco_Web_Scrape_v2.Search.Process;

namespace Visco_Web_Scrape_v2.Search.Items {

	[Serializable]
	public class Result {

		public string PageUrl { get; }
		public DateTime DiscoveryTimeUtc { get; }
		public bool IsNewResult { get; set; }
		public HashSet<GrantCrawler.KeywordMatch> Hits { get; }

		public Result(string url) {
			PageUrl = url;
			Hits = new HashSet<GrantCrawler.KeywordMatch>();
		}

		public Result(string url, GrantCrawler.KeywordMatch[] keywordMatches, DateTime date) {
			PageUrl = url;
			DiscoveryTimeUtc = date.ToUniversalTime();
			Hits = new HashSet<GrantCrawler.KeywordMatch>(keywordMatches);
			IsNewResult = true;
		}
	}
}
