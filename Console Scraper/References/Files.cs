using System;

namespace Console_Scraper.References {

	public static class Files {

		public static readonly string AppFileDirectory =
			Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Darkstone Concepts\Visco Web Crawler\";

		public static readonly string SettingsFile = AppFileDirectory + "NewSettings.bin";

	}

}