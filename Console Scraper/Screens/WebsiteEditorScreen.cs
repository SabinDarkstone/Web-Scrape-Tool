using System;
using Console_Scraper.Search.Items;
using Console_Scraper.UI;

namespace Console_Scraper.Screens {

	public class WebsiteEditorScreen : Screen {

		public WebsiteEditorScreen() {
			ScreenMenu = new Menu(References.MenuOptions.EditWebsites, "Please Select an Option", MySettings.WebsiteList.Count);
		}

		protected override void ReloadSettings() {
			base.ReloadSettings();
			ScreenMenu.UpdateOffset(MySettings.WebsiteList.Count);
		}

		private void ShowCurrentWebsites() {
			Console.ForegroundColor = ConsoleColor.Cyan;
			if (MySettings == null || MySettings.WebsiteList == null || MySettings.WebsiteList.Count == 0) {
				Console.WriteLine("Website list empty");
			} else {
				foreach (var website in MySettings.WebsiteList)
				{
					Console.WriteLine(website);
				}
			}
			Console.ResetColor();
		}

		private void AddWebsite() {
			var form = new Input("Enter website information");
			form.AddTextbox("Website Name");
			form.AddTextbox("Website URL");
			form.Display();

			var response = form.Responses;
			if (!response.TrueForAll(i => i.Equals(""))) {
				MySettings.WebsiteList.Add(new Website(response[0], response[1]));
				ReloadSettings();
			}

			Start();
		}

		public override bool Start() {
			Console.Clear();
			ShowCurrentWebsites();

			var selection = ScreenMenu.Display();

			switch (selection) {
				case 0:  // Add new website
					Console.Clear();
					AddWebsite();
					break;

				case 1:  // Edit websites
					Console.Clear();
					EditWebsites();
					break;

				case 2:  // Remove website
					Console.Clear();
					RemoveWebsite();
					break;

				case 3:  // Return
					return true;
			}

			return false;
		}

		private void RemoveWebsite() {
			var list = new string[MySettings.WebsiteList.Count + 1];
			MySettings.GetWebsites().CopyTo(list, 0);
			list[MySettings.WebsiteList.Count] = "Back";

			var menu = new Menu(list, "Choose a website to delete");
			var selected = menu.Display();

			if (selected == list.Length - 1) {
				Start();
			}

			MySettings.WebsiteList.RemoveAt(selected);
			ReloadSettings();

			Start();
		}

		private void EditWebsites() {
			var list = new string[MySettings.WebsiteList.Count + 1];
			MySettings.GetWebsites().CopyTo(list, 0);
			list[MySettings.WebsiteList.Count] = "Back";

			var menu = new Menu(list, "Choose a website to edit");
			var selected = menu.Display();

			if (selected == list.Length - 1) {
				Start();
			} else {
				var form = new Input("\nEnter replacement information");
				form.AddTextbox("Website Name", MySettings.WebsiteList[selected].Name);
				form.AddTextbox("Website URL", MySettings.WebsiteList[selected].Url);
				form.Display();

				var response = form.Responses;
				MySettings.WebsiteList.RemoveAt(selected);
				MySettings.WebsiteList.Add(new Website(response[0], response[1]));
				ReloadSettings();

				Start();
			}
		}

	}

}