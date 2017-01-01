using System;
using Visco_Web_Scrape_v2.Properties;

namespace Visco_Web_Scrape_v2.Scripts {

	public static class Reference {

		public static string[] IgnoreWords = {
			"tags", "comment", "reply", "calendar", "admin", "legacy", "feedback"
		};

		public static string[] IgnoreExtensions = {".pdf", ".xls", ".doc", ".cfm", ".zip", ".jpg", ".dgn"};

		public static class Files {
			public static string AppFileDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
				@"\Darkstone Concepts\Visco Web Crawler\";

			public static string SettingsFile = AppFileDirectory + Resources.SettingsFileName;
		}

	}

}