using System;

namespace Visco_Web_Scrape_v2.Search.Items {

	[Serializable]
	public class Website : IComparable {

		public string Name { get; }
		public string Url { get; }
		public bool IsEnabled { get; set; }
		public bool IsGrantSource { get; set; }

		public Website(string name, string url, bool grant) {
			Name = name;
			Url = url;
			IsEnabled = true;
			IsGrantSource = grant;
		}

		public override string ToString() => Name;

		protected bool Equals(Website other) {
			return Url.Equals(other.Url) && IsGrantSource.Equals(IsGrantSource);
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
