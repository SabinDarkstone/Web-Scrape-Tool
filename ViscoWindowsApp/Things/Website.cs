namespace ViscoWindowsApp.Things {

	public class Website {


		public string Name { get; set; }
		public string Url { get; set; }
		public bool DoCrawl { get; set; }

		public Website(string name, string url, bool? crawl = true) {
			Name = name;
			Url = url;

			if (crawl == null) {
				DoCrawl = true;
			} else {
				DoCrawl = (bool) crawl;
			}
		}

		public override string ToString() {
			var output = "";
			output += DoCrawl ? "(Yes) " : "(No) ";

			return output + Name;
		}

		protected bool Equals(Website other) {
			return string.Equals(Name, other.Name) && string.Equals(Url, other.Url) && DoCrawl == other.DoCrawl;
		}

		public override int GetHashCode() {
			unchecked {
				var hashCode = (Name != null ? Name.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (Url != null ? Url.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ DoCrawl.GetHashCode();
				return hashCode;
			}
		}
	}

}