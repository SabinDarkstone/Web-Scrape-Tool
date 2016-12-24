namespace Visco_Web_Scrape_v2.Search.Process {

	public class Progress {

		public enum Status { Crawling, Searching, Cancelling, Cancelled }

		public string Url { get; private set; }
		public string Domain { get; private set; }
		public int? TotalPageCount { get; private set; }
		public int? RelevantPageCount { get; private set; }
		public int DomainNumber { get; private set; }
		public Status CurrentStatus { get; private set; }

		public Progress(string url, string domain, int? totalPages, int? foundPages, int index, Status status) {
			Url = url;
			Domain = domain;
			TotalPageCount = totalPages;
			RelevantPageCount = foundPages;
			CurrentStatus = status;
			DomainNumber = index;
		}

		public Progress(Status status) {
			CurrentStatus = status;
		}

	}
}
