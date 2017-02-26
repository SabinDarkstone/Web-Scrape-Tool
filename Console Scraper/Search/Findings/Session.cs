using System;
using System.Collections.Generic;
using System.Linq;

namespace Console_Scraper.Search.Findings {

	[Serializable]
	public class Session : IComparable<Session> {

		public List<Domain> DomainResults { get; set; }
		public DateTime DateCreated { get; set; }

		public Session() {
			DateCreated = DateTime.Now;
			DomainResults = new List<Domain>();
		}

		public int GetTotal() {
			return DomainResults.Sum(domain => domain.GetTotalCount());
		}

		public int CompareTo(Session other) {
			if (ReferenceEquals(this, other)) return 0;
			if (ReferenceEquals(null, other)) return 1;
			return DateCreated.CompareTo(other.DateCreated);
		}

		public override string ToString() {
			return string.Format("Date/Time: {0} {1}\tDomains: {2}\tResults: {3}", DateCreated.ToShortDateString(),
				DateCreated.ToShortTimeString(), DomainResults.Count, GetTotal());
		}

	}

}