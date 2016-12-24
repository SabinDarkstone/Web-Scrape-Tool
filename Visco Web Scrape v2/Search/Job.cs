using System.Collections.Generic;

namespace Visco_Web_Scrape_v2.Search {

	public class Job {

		public List<Website> WebsitesToCrawl { get; private set; }
		public List<Keyword> KeywordsToSearchFor { get; private set; }

		public Job(List<Website> websites, List<Keyword> keywords) {
			WebsitesToCrawl = websites;
			KeywordsToSearchFor = keywords;
		}
	}
}
