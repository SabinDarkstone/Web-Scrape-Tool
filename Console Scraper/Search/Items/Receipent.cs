using System;

namespace Console_Scraper.Search.Items {

	public class Receipent : IComparable<Receipent> {

		public string Name { get; set; }
		public string Address { get; set; }
		public bool Enabled { get; set; }

		public Receipent(string name, string address) {
			Name = name;
			Address = address;
			Enabled = true;
		}

		public override string ToString()
		{
			return string.Format("[{0}] {1} - {2}", Enabled ? "On" : "Off", Name, Address);
		}

		public int CompareTo(Receipent other) {
			if (ReferenceEquals(this, other)) return 0;
			if (ReferenceEquals(null, other)) return 1;
			return string.Compare(Name, other.Name, StringComparison.Ordinal);
		}

	}

}