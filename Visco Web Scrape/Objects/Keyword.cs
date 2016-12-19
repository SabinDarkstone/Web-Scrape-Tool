using System;

namespace Visco_Web_Scrape.Objects {

	/// <summary>
	/// Used to help with crawling and scraping of website data.
	/// </summary>
	[Serializable]
	public class Keyword {

		public enum KeywordType {
			Relevance,
			Inclusion,
			Exclusion
		}

		public string Text { get; }
		public KeywordType Type { get; private set; }

		public Keyword(string text, KeywordType type) {
			Text = text;
			Type = type;
		}

		public override string ToString() {
			return Text;
		}
	}
}
