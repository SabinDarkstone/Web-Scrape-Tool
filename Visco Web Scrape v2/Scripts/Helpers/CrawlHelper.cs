using System;

namespace Visco_Web_Scrape_v2.Scripts.Helpers {

	public static class CrawlHelper {

		[Serializable]
		public enum SearchCompletion {
			Completed, InterruptedByUser, Unsuccessful
		}

		public static int TotalPages { get; set; }
		public static int SkippedPages { get; set; }
		public static int CurrentDomain { get; set; }
	}
}
