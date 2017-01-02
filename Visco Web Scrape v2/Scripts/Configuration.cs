using System;
using System.Collections.Generic;
using Visco_Web_Scrape_v2.Search.Items;

namespace Visco_Web_Scrape_v2.Scripts {

	[Serializable]
	public class Configuration {

		public enum ExportType { Excel, Plain, Xml }

		public HashSet<Website> Websites { get; set; }
		public HashSet<Keyword> PageWords { get; set; }
		public HashSet<Keyword> UrlWords { get; set; }
		public HashSet<Recipient> Recipients { get; set; }
		
		public int PagesToCrawlPerDomain { get; set; }
		public bool EnableUrlFiltering { get; set; }
		public bool EnableUrlAnalysis { get; set; }
		public bool IncludeDate { get; set; }
		public bool OnlyNewResults { get; set; }
		public bool EnableSendEmail { get; set; }
		public ExportType ExportMethod { get; set; }

		public Configuration() {
			Websites = new HashSet<Website>();
			PageWords = new HashSet<Keyword>();
			UrlWords = new HashSet<Keyword>();
			Recipients = new HashSet<Recipient>();
		}
	}
}
