using System;
using Visco_Web_Scrape_v2.Search.Items;

namespace Visco_Web_Scrape_v2.Search.Process {
	public class Progress {

		public enum Status {
			Initializing,	// Crawler is initializing
			Crawling,		// Website is being crawled
			Searching,		// The current page's text is being search for keywords
			Scanning,		// Current Url is being examined after page is searched
			Cancelling,		// Cancellation is pending, not all operations have finished cancellation
			Cancelled		// Search is completely halted, okay to save
		}

		public enum PageType {
			Searched,
			Ignored,
			Result
		}

		public string CurrentUrl { get; set; }
		public Website Website { get; }
		public int SearchedPages { get; private set; }
		public int IgnoredPages { get; private set; }
		public int ResultsFound { get; private set; }
		public Status SearchStatus { get; set; }

		public Progress(Website website) {
			Website = website;
			SearchStatus = Status.Initializing;
		}

		/// <summary>
		/// Upate the information on the GrantSearch form
		/// </summary>
		/// <param name="url">The url of the page currently being searched</param>
		/// <param name="website">The website being crawled</param>
		public Progress(string url, Website website) {
			CurrentUrl = url;
			Website = website;
		}

		/// <summary>
		/// Update the status without changing the rest of the information
		/// </summary>
		/// <param name="status">The current status of the bot</param>
		public Progress(Status status) {
			SearchStatus = status;
		}

		/// <summary>
		/// Increment one of the counters
		/// </summary>
		/// <param name="counterToIncrement">The counter to be incremented</param>
		public void Increment(PageType counterToIncrement) {
			switch (counterToIncrement) {
				case PageType.Searched:
					SearchedPages++;
					break;
				case PageType.Ignored:
					IgnoredPages++;
					break;
				case PageType.Result:
					ResultsFound++;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(counterToIncrement), counterToIncrement, null);
			}
		}
	}
}
