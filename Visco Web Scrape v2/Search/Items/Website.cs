using System;

namespace Visco_Web_Scrape_v2.Search.Items {

	[Serializable]
	public class Website : IComparable {

		public string Name { get; }
		public string Url { get; }
		public DateTime LastCrawlDate { get; set; }

		public Website(string name, string url) {
			Name = name;
			Url = url;
		}

		public override string ToString() => Name;

		protected bool Equals(Website other) {
			return string.Equals(Name, other.Name) && string.Equals(Url, other.Url);
		}

		public override int GetHashCode() {
			unchecked {
				return (Name.GetHashCode() * 397) ^ Url.GetHashCode();
			}
		}

		public int CompareTo(object obj) {
			var other = (Website) obj;
			return String.Compare(this.Name, other.Name);
		}
	}
}
