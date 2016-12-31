using System;

namespace Visco_Web_Scrape_v2.Search {

	[Serializable]
	public class Recipient {

		public string Name { get; }
		public string Address { get; }

		public Recipient(string name, string address) {
			Name = name;
			Address = address;
		}

		public override string ToString() {
			return Name;
		}
	}
}
