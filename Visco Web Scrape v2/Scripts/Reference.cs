using System;
using Visco_Web_Scrape_v2.Properties;

namespace Visco_Web_Scrape_v2.Scripts {

	public static class Reference {

		public static string[] IgnoreWords = {
			"tags", "comment", "reply", "calendar", "admin", "legacy", "feedback", "cgi-bin"
		};

		public static string[] IgnoreExtensions = {".doc", ".xls", ".pdf", ".cfm", ".zip", ".jpg", ".dgn", ".dwg"};

		public static class Files {
			public static string AppFileDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
				@"\Darkstone Concepts\Visco Web Crawler\";

			public static string SettingsFile = AppFileDirectory + Resources.SettingsFileName;
			public static string ResultsFile = AppFileDirectory + Resources.ResultsFileName;
			public static string ExportDirectory = AppFileDirectory + @"Exports\";
		}

	}

}