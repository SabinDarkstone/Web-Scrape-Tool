﻿using System;

namespace Visco_Web_Scrape_v2.Scripts {

	public static class Reference {

		public static string Version = "0.2.0 alpha";

		public static string[] IgnoreWords = {
			"comment", "reply", "civil-rights", "feedback", "admin", "tags", "fastlane"
		};

		public static string[] IgnoreExtensions = {".pdf", ".xls"};

		public static class Files {
			public static string AppFileDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
				@"\Darkstone Concepts\Visco Web Crawler\";

			public static string SettingsFile = AppFileDirectory + "Settings.bin";
		}

		public static class Messages {
			public static string NoSettingsFileFound = "No settings file was found, would you like to create a new one?";
			public static string ClearCrawlResults = "Are you sure you want to clear the results of previous web crawls?";
			public static string ClearSearchSettings = "Are you sure you want to clear the website and keyword lists?";
			public static string WarnMismatchSettingsAndResults = "Are you sure you want to clear the website and keyword lists?";
			public static string CancelCrawl = "Are you sure you want to cancel the proess? Doing so and saving afterwards will clear your previous results and replace them with only the results found so far.";

			public static string Warning = "Warning";
			public static string AreYouSure = "Are You Sure?";
			public static string CancelQuestion = "Really Cancel?";
		}
	}

}