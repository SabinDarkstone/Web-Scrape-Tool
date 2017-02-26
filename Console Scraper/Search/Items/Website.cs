using System;

namespace Console_Scraper.Search.Items {

	[Serializable]
	public class Website : IComparable<Website> {

		public string Name { get; set; }
		public string Url { get; set; }
		public bool Enabled { get; set; }

		public Website(string name, string url) {
			Name = name;
			Url = url;
			Enabled = true;
		}

		public override string ToString() {
			return string.Format("[{0}] {1} - {2}", Enabled ? "On" : "Off", Name, Url);
		}

		public int CompareTo(Website other) {
			if (ReferenceEquals(this, other)) return 0;
			if (ReferenceEquals(null, other)) return 1;
			var nameComparison = string.Compare(Name, other.Name, StringComparison.Ordinal);
			if (nameComparison != 0) return nameComparison;
			return Enabled.CompareTo(other.Enabled);
		}

	}

}