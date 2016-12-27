using System;
using System.Collections.Generic;
using Visco_Web_Scrape_v2.Search.Items;

namespace Visco_Web_Scrape_v2.Scripts {

	[Serializable]
	public class Configuration {

		public enum ExportType { Excel, Plain, Xml }

		public List<Website> Websites { get; set; }
		public List<Keyword> Keywords { get; set; }
		public List<SearchResults> Results { get; set; }
		public int PagesToCrawlPerDomain { get; set; }
		public bool EnableUrlFiltering { get; set; }
		public ExportType ExportMethod { get; set; }

		public Configuration() {
			Websites = new List<Website>();
			Keywords = new List<Keyword>();
			Results = new List<SearchResults>();
		}
	}
}
