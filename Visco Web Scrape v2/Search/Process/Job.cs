using System.Collections.Generic;
using Visco_Web_Scrape_v2.Search.Items;

namespace Visco_Web_Scrape_v2.Search.Process {

	public class Job {

		public HashSet<Website> WebsitesToCrawl { get; }
		public HashSet<Keyword> KeywordsToSearchFor { get; }

		public Job(IEnumerable<Website> websites, IEnumerable<Keyword> keywords) {
			WebsitesToCrawl = new HashSet<Website>();
			KeywordsToSearchFor = new HashSet<Keyword>();

			foreach (var website in websites) {
				if (website.IsEnabled) WebsitesToCrawl.Add(website);
			}

			foreach (var keyword in keywords) {
				if (keyword.IsEnabled) KeywordsToSearchFor.Add(keyword);
			}
		}
	}
}
