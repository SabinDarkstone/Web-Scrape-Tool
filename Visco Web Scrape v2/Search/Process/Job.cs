using System;
using System.Collections.Generic;
using System.Linq;
using Visco_Web_Scrape_v2.Search.Items;

namespace Visco_Web_Scrape_v2.Search.Process {

	[Serializable]
	public class Job {

		public HashSet<Website> WebsitesToCrawl { get; }
		public HashSet<Keyword> KeywordsToSearchFor { get; }

		public Job(IEnumerable<Website> websites, IEnumerable<Keyword> keywords) {
			WebsitesToCrawl = new HashSet<Website>(websites.Where(site => site.IsEnabled));
			KeywordsToSearchFor = new HashSet<Keyword>(keywords.Where(keyword => keyword.IsEnabled));
		}
	}
}
