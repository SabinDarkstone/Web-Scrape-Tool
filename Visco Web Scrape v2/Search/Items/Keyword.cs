using System;

namespace Visco_Web_Scrape_v2.Search.Items {

	[Serializable]
	public class Keyword {

		protected bool Equals(Keyword other) {
			return string.Equals(Text, other.Text) && Type == other.Type;
		}

		public override int GetHashCode() {
			unchecked {
				return (Text.GetHashCode() * 397) ^ (int) Type;
			}
		}

		public enum KeywordType { Include, Exclude }

		public string Text { get; private set; }
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
