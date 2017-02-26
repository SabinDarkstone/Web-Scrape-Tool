using System;
using System.Collections.Generic;

namespace Console_Scraper.Search.Findings {

	[Serializable]
	public class Result {

		public string Url { get; set; }
		public List<Hit> HitsOnPage { get; set; }
		public DateTime DiscoveryDate { get; }

		public Result(string url) {
			Url = url;
			HitsOnPage = new List<Hit>();
			DiscoveryDate = DateTime.Now;
		}

	}

}