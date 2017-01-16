using System;

namespace Visco_Web_Scrape_v2.Search.Items {

	[Serializable]
	public class Keyword : IComparable {

		public string Text { get; }
		public bool IsEnabled { get; set; }

		protected bool Equals(Keyword other) {
			return string.Equals(Text, other.Text);
		}

		public Keyword(string text) {
			Text = text;
			IsEnabled = true;
		}

		public override string ToString() {
			return Text;
		}

		public int CompareTo(object obj) {
			var other = (Keyword)obj;
			return string.CompareOrdinal(Text, other.Text);
		}
	}
}
