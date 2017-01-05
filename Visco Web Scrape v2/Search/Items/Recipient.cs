using System;

namespace Visco_Web_Scrape_v2.Search.Items {

	[Serializable]
	public class Recipient : IComparable {

		public string Name { get; set; }
		public string Address { get; set; }
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
			return String.Compare(this.Name, other.Name);
		}
	}
}
