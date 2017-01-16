using System;
using System.Collections.Generic;
using System.Linq;
using Visco_Web_Scrape_v2.Search.Process;

namespace Visco_Web_Scrape_v2.Search.Items {

	[Serializable]
	public class Result {

		/// <summary>
		/// Url of webpage containing result(s)
		/// </summary>
		public string PageUrl { get; }

		/// <summary>
		/// Date and time the result was first found (in UTC)
		/// </summary>
		public DateTime DiscoveryTimeUtc { get; }

		/// <summary>
		/// Denotes whether the result is new as of the most recent search
		/// </summary>
		public bool IsNewResult { get; set; }

		/// <summary>
		/// List of results on page with surrounding text
		/// </summary>
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

		public bool IsLinkResult() {
			foreach (var hit in Hits.ToList()) {
				if (hit.IsLink) {
					Hits.Remove(hit);
				}
			}

			return Hits.Count == 0;
		}

		public override string ToString() {
			return PageUrl;
		}

		protected bool Equals(Result other) {
			return string.Equals(PageUrl, other.PageUrl);
		}

		public override int GetHashCode() {
			return (PageUrl != null ? PageUrl.GetHashCode() : 0);
		}
	}
}
