using System.Collections.Generic;
using Visco_Web_Scrape.Objects;

namespace Visco_Web_Scrape.Operations.Configs {

	public class CrawlSettings {

		public Keyword[] Keywords { get; }
		public Website[] Websites { get; }

		public int CurrentCrawlIndex { get; set; }

		public CrawlSettings(List<Keyword> keywords, List<Website> websites) {
			Keywords = keywords.ToArray();
			Websites = websites.ToArray();

			CurrentCrawlIndex = -1;
		}
	}
}
