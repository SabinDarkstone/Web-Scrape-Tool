using System.Collections.Generic;
using Visco_Web_Scrape_v2.Search.Items;

namespace Visco_Web_Scrape_v2.Search.Process {

	public class Job {

		public List<Website> WebsitesToCrawl { get; }
		public List<Keyword> KeywordsToSearchFor { get; }

		public Job(List<Website> websites, List<Keyword> keywords) {
			WebsitesToCrawl = websites;
			KeywordsToSearchFor = keywords;
		}
	}
}
