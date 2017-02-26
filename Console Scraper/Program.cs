using System;
using System.Collections.Generic;
using Console_Scraper.Helpers;
using Console_Scraper.Screens;
using Console_Scraper.Search.Items;

namespace Console_Scraper {

	public class Program {

		private static void CheckSettingsFile() {
			var settings = FileHelper.LoadSettings();
			if (settings.KeywordList == null)
				settings.KeywordList = new List<Keyword>();
			if (settings.WebsiteList == null)
				settings.WebsiteList = new List<Website>();

			FileHelper.SaveSettings(settings);
		}

		public static void Main(string[] args) {
			CheckSettingsFile();

			while (true) {
				Console.Clear();
				if (new MainScreen().Start()) {
					break;
				}
			}

			Environment.Exit(0);
		}

	}

}