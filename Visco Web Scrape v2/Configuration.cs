using System;
using System.Collections.Generic;
using Visco_Web_Scrape_v2.Search;

namespace Visco_Web_Scrape_v2 {

	[Serializable]
	public class Configuration {

		public List<Website> Websites { get; set; }
		public List<Keyword> Keywords { get; set; }
		public List<SearchResults> Results { get; set; }

		public Configuration() {
			Websites = new List<Website>();
			Keywords = new List<Keyword>();
			Results = new List<SearchResults>();
		}
	}
}
