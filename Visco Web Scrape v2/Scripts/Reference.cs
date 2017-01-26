using System;
using Visco_Web_Scrape_v2.Properties;

namespace Visco_Web_Scrape_v2.Scripts {

	public static class Reference {

		/// <summary>
		/// URL blacklists
		/// </summary>
		public static string[] IgnoreWords = {
			"tags", "comment", "reply", "calendar", "admin", "legacy", "feedback", "cgi-bin"
		};

		/// <summary>
		/// URL extensions to ignore
		/// </summary>
		public static string[] IgnoreExtensions = {".doc", ".xls", ".pdf", ".cfm", ".zip", ".jpg", ".dgn", ".dwg"};

		public static class Files {
			/// <summary>
			/// Directory of app files on a per user basis
			/// </summary>
			public static string AppFileDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
				@"\Darkstone Concepts\Visco Web Crawler\";

			/// <summary>
			/// Location of settings file
			/// </summary>
			public static string SettingsFile = AppFileDirectory + Resources.SettingsFileName;

			/// <summary>
			/// Location of results file
			/// </summary>
			public static string ResultsFile = AppFileDirectory + Resources.ResultsFileName;

			/// <summary>
			/// Directory where exported files are saved for email
			/// </summary>
			public static string ExportDirectory = AppFileDirectory + @"Exports\";

			/// <summary>
			/// Location of excel template file
			/// </summary>
			public static string GrantTemplateFile = AppFileDirectory + @"ExcelResultsTemplate.xlsx";

			/// <summary>
			/// Location of the email body text that can be changed
			/// </summary>
			public static string EmailBodyFile = AppFileDirectory + @"emailBody.txt";

			/// <summary>
			/// Location of the email subject text that can be changed
			/// </summary>
			public static string EmailSubjectFile = AppFileDirectory + @"emailSubject.txt";
		}

	}

}