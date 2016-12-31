using System;

namespace Visco_Web_Scrape_v2.Search.Items {

	[Serializable]
	public class Keyword {

		public string Text { get; }

		protected bool Equals(Keyword other) => string.Equals(Text, other.Text);

		public Keyword(string text) {
			Text = text;
		}

		public override string ToString() {
			return Text;
		}
	}
}
