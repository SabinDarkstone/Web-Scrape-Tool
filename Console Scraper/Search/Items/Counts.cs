using System;

namespace Console_Scraper.Search.Items {

	[Serializable]
	public class Counts {

		public enum CountType { Searched, Ignored }

		public int SearchPages { get; private set; }
		public int IgnoredPages { get; private set; }
		public TimeSpan SearchTime { get; private set; }

		private readonly DateTime startTime;

		public Counts() {
			SearchPages = 0;
			IgnoredPages = 0;
			SearchTime = TimeSpan.Zero;
			startTime = DateTime.Now;
		}

		public void Increment(CountType type) {
			if (type == CountType.Ignored) IgnoredPages++;
			if (type == CountType.Searched) SearchPages++;
		}

		public void Finish() {
			SearchTime = DateTime.Now - startTime;
		}
	}

}