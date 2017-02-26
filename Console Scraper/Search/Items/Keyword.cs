using System;

namespace Console_Scraper.Search.Items {

	[Serializable]
	public class Keyword : IComparable<Keyword> {

		public string Text { get; set; }
		public bool IsWhitelist { get; set; }
		public bool Enabled { get; set; }

		public Keyword(string text, bool whitelist) {
			Text = text;
			IsWhitelist = whitelist;
			Enabled = true;
		}

		public override string ToString() {
			return string.Format("[{0}] {1}: {2}", Enabled ? "On" : "Off", IsWhitelist ? "Whitelist" : "Blacklist", Text);
		}

		public int CompareTo(Keyword other) {
			if (ReferenceEquals(this, other)) return 0;
			if (ReferenceEquals(null, other)) return 1;
			var textComparison = string.Compare(Text, other.Text, StringComparison.Ordinal);
			if (textComparison != 0) return textComparison;
			var isWhitelistComparison = IsWhitelist.CompareTo(other.IsWhitelist);
			if (isWhitelistComparison != 0) return isWhitelistComparison;
			return Enabled.CompareTo(other.Enabled);
		}

	}

}