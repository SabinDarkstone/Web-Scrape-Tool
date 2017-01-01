using Visco_Web_Scrape_v2.Scripts.Helpers;

namespace Visco_Web_Scrape_v2.Search.Process {

	public class Progress {

		public enum Status { Crawling, Searching, Scanning, Cancelling, Cancelled }

		public string Url { get; private set; }
		public string Domain { get; private set; }
		public int? TotalPageCount { get; }
		public int? SkippedPageCount { get; }
		public int? RelevantPageCount { get; private set; }
		public int DomainNumber { get; private set; }
		public Status CurrentStatus { get; set; }

		public Progress(string url, string domain, int? foundPages, int index, Status status) {
			Url = url;
			Domain = domain;
			TotalPageCount = CrawlHelper.TotalPages;
			SkippedPageCount = CrawlHelper.SkippedPages;
			RelevantPageCount = foundPages;
			CurrentStatus = status;
			DomainNumber = index;
		}

		public Progress(Status status) {
			CurrentStatus = status;
		}

	}
}
