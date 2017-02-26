using System;
using System.Collections.Generic;
using Console_Scraper.Search.Items;

namespace Console_Scraper.Search.Findings {

	[Serializable]
	public class Domain : IComparable<Domain> {

		public Website Website { get; set; }
		public List<Result> ResultsList { get; set; }
		public Counts Metadata { get; set; }

		public Domain(Website website) {
			Website = website;
			ResultsList = new List<Result>();
			Metadata = new Counts();
		}

		public int GetTotalCount() {
			return ResultsList.Count;
		}

		public int CompareTo(Domain other) {
			if (ReferenceEquals(this, other)) return 0;
			if (ReferenceEquals(null, other)) return 1;
			return Comparer<Website>.Default.Compare(Website, other.Website);
		}

	}

}