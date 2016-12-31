using System;

namespace Visco_Web_Scrape_v2.Search {

	[Serializable]
	public class Recipient {

		public string Name { get; set; }
		public string Address { get; set; }

		public Recipient(string name, string address) {
			Name = name;
			Address = address;
		}

		public override string ToString() {
			return Name;
		}
	}
}
