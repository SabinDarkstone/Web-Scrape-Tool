using System.Collections.Generic;

namespace Console_Scraper.Search.Items {

	public class Job {

		public HashSet<Keyword> Whitelist { get; }
		public HashSet<Keyword> Blacklist { get; }
		public HashSet<Website> Websites { get; }

		public Job(Settings settings) {
			Whitelist = new HashSet<Keyword>();
			Blacklist = new HashSet<Keyword>();
			Websites = new HashSet<Website>();

			foreach (var keyword in settings.KeywordList) {
				if (keyword.IsWhitelist) {
					Whitelist.Add(keyword);
				} else {
					Blacklist.Add(keyword);
				}
			}

			foreach (var website in settings.WebsiteList) {
				if (website.Enabled) {
					Websites.Add(website);
				}
			}
		}

	}

}