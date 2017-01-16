using System;

namespace Visco_Web_Scrape_v2.Search.Items {

	[Serializable]
	public class Recipient : IComparable {

		/// <summary>
		/// Name of recipient
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Email address of recipient
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		/// Whether to send email to this recipient
		/// </summary>
		public bool IsEnabled { get; set; }

		public Recipient(string name, string address) {
			Name = name;
			Address = address;
			IsEnabled = true;
		}

		public override string ToString() {
			return Name;
		}

		public int CompareTo(object obj) {
			var other = (Recipient)obj;
			return string.CompareOrdinal(Name, other.Name);
		}
	}
}
