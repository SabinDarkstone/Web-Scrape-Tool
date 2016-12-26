using System;

namespace Visco_Web_Scrape_v2.Scripts {

	public static class Reference {

		public static string Version = "0.1.1.3 alpha";


		public static class Files {
			public static string AppFileDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
				@"\Darkstone Concepts\Visco Web Crawler\";

			public static string SettingsFile = AppFileDirectory + "Settings.bin";
		}

		public static class Messages {
			public static string NoSettingsFileFound = "No settings file was found, would you like to create a new one?";
		}
	}

}