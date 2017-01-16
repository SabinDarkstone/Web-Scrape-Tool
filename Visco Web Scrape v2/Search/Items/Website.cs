using System;

namespace Visco_Web_Scrape_v2.Search.Items {

	[Serializable]
	public class Website : IComparable {
		
		/// <summary>
		/// Name of website as defiend by user
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Absolute url of website
		/// </summary>
		public string Url { get; }

		/// <summary>
		/// Whether the website will be searched
		/// </summary>
		public bool IsEnabled { get; set; }

		/// <summary>
		/// Whether the website is included on the grant workbook or other workbook during export
		/// </summary>
		public bool IsGrantSource { get; set; }

		public Website(string name, string url, bool grant) {
			Name = name;
			Url = url;
			IsEnabled = true;
			IsGrantSource = grant;
		}

		public override string ToString() => Name;

		protected bool Equals(Website other) {
			return this.Url.Equals(other.Url);
		}

		public override int GetHashCode() {
			unchecked {
				return (Name.GetHashCode() * 397) ^ Url.GetHashCode();
			}
		}

		public int CompareTo(object obj) {
			var other = (Website) obj;
			return string.CompareOrdinal(Name, other.Name);
		}
	}
}
