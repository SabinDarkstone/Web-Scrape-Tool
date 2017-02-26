using System;
using Console_Scraper.Search.Items;

namespace Console_Scraper.Search.Findings {

	[Serializable]
	public class Hit {

		public Keyword KeywordFound { get; set; }
		public string SurroundingText { get; set; }

		public Hit(Keyword keyword, string surroundingText) {
			KeywordFound = keyword;
			SurroundingText = surroundingText;
		}

		public override bool Equals(object obj) {
			var hit = (Hit) obj;
			return Equals(hit);
		}

		protected bool Equals(Hit other) {
			return Equals(KeywordFound, other.KeywordFound) && string.Equals(SurroundingText, other.SurroundingText);
		}

		public override int GetHashCode() {
			unchecked {
				return ((KeywordFound != null ? KeywordFound.GetHashCode() : 0) * 397) ^ (SurroundingText != null ? SurroundingText.GetHashCode() : 0);
			}
		}

	}

}