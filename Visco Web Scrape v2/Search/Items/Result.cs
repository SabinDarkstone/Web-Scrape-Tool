using System;
using System.Collections.Generic;
using System.Linq;
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

		public Result(string url, IEnumerable<GrantCrawler.KeywordMatch> keywordMatches) {
			PageUrl = url;
			DiscoveryTimeUtc = DateTime.UtcNow;
			Hits = new HashSet<GrantCrawler.KeywordMatch>(keywordMatches);
			IsNewResult = true;
		}

		public override string ToString() {
			var str = PageUrl + " contains: ";
			str = Hits.Aggregate(str, (current, result) => current + (result.Keyword.Text + ", "));
			str = str.Substring(0, str.Length - 2);

			return str;
;		}

		protected bool Equals(Result other) {
			return string.Equals(PageUrl, other.PageUrl);
		}

		public override int GetHashCode() {
			return (PageUrl != null ? PageUrl.GetHashCode() : 0);
		}
	}
}
