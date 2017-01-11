using System;
using Visco_Web_Scrape_v2.Search.Items;

namespace Visco_Web_Scrape_v2.Scripts.Helpers {

	public static class Comparisons {

		public static Func<WebsiteResults, bool> SearchStarted =
			results => results.WebsiteStatus != WebsiteResults.Status.Skipped;
	}

}