using Console_Scraper.UI;

namespace Console_Scraper.Screens {

	public class MainScreen : Screen {

		public MainScreen() {
			ScreenMenu = new Menu(References.MenuOptions.MainMenu, "Main Menu");
		}

		public override bool Start() {
			var selection = ScreenMenu.Display();

			switch (selection) {
				case 0:  // Websites
					new WebsiteEditorScreen().Start();
					break;

				case 1:  // Keywords
					new KeywordEditorScreen().Start();
					break;

				case 2:  // Email Addresses
					break;

				case 3:  // Sessions
					break;

				case 4:  // Start Search
					break;

				case 5:  // Export Results
					break;

				case 6:  // Settings
					break;

				case 7:  // Quit
					return true;
			}

			return false;
		}

	}

}