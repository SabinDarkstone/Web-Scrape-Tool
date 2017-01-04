using Visco_Web_Scrape_v2.Search.Items;

namespace Visco_Web_Scrape_v2.Scripts.Helpers {

	public static class TextHelper {

		public static string ShortenContext(WebsiteResults.Result result, Configuration config) {
			var longString = result.Context;
			var shortString = "";
			var startingPoint = 0;
			Keyword myKeyword = null;
			foreach (var keyword in config.PageWords) {
				if (longString.Contains(keyword.Text)) {
					myKeyword = keyword;
					startingPoint = longString.IndexOf(myKeyword.Text);
					break;
				}
			}
			if (myKeyword != null) {
				var endingPoint = 0;
				if (startingPoint - 150 < 0) {
					endingPoint = longString.IndexOf(" ", 300);
					shortString = longString.Substring(0, endingPoint) + "...";
				} else {
					endingPoint = longString.Substring(startingPoint).IndexOf(" ", 300);
					shortString = "..." + longString.Substring(startingPoint, endingPoint) + "...";
				}
			}

			return shortString;
		}
	}
}
