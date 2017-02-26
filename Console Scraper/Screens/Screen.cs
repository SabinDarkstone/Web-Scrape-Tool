using System;
using Console_Scraper.Helpers;
using Console_Scraper.UI;

namespace Console_Scraper.Screens {

	public abstract class Screen {

		protected Menu ScreenMenu;
		protected Settings MySettings;

		protected Screen() {
			Console.Clear();
			MySettings = FileHelper.LoadSettings();
		}

		protected virtual void ReloadSettings() {
			FileHelper.SaveSettings(MySettings);
			MySettings = FileHelper.LoadSettings();
		}

		public abstract bool Start();

	}

}