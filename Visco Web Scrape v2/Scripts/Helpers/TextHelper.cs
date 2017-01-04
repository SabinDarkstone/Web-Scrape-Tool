using System;
using Visco_Web_Scrape_v2.Search.Items;

namespace Visco_Web_Scrape_v2.Scripts.Helpers {

	public static class TextHelper {

		public static string ShortenContext(WebsiteResults.Result result, Configuration config) {
			var shortString = result.Context.Substring(0, 300);
			return shortString;
		}
	}
}
