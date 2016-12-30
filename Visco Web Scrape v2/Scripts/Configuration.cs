using System;
using System.Collections.Generic;
using Visco_Web_Scrape_v2.Search.Items;

namespace Visco_Web_Scrape_v2.Scripts {

	[Serializable]
	public class Configuration {

		[Serializable]
		public class LastCrawlInfo {

			public LastCrawlInfo() {
				Date = DateTime.MinValue;
				CompletionStatus = "Unknown";
			}

			public List<SearchResults> Results { get; set; }
			public DateTime Date { get; set; }
			public string CompletionStatus { get; set; }
		}

		public enum ExportType { Excel, Plain, Xml }

		public List<Website> Websites { get; set; }
		public List<Keyword> Keywords { get; set; }
		
		public int PagesToCrawlPerDomain { get; set; }
		public bool EnableUrlFiltering { get; set; }
		public bool EnableUrlAnalysis { get; set; }
		public bool IncludeDate { get; set; }
		public bool OnlyNewResults { get; set; }
		public ExportType ExportMethod { get; set; }
		public LastCrawlInfo LastCrawl { get; set; }

		public Configuration() {
			Websites = new List<Website>();
			Keywords = new List<Keyword>();

			if (LastCrawl == null) {
				LastCrawl = new LastCrawlInfo { Results = new List<SearchResults>() };
			}
		}
	}
}
