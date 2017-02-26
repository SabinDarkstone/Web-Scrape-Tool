using System;
using Console_Scraper.Search.Items;
using Console_Scraper.UI;

namespace Console_Scraper.Screens {

	public class KeywordEditorScreen : Screen {

		public KeywordEditorScreen() {
			ScreenMenu = new Menu(References.MenuOptions.EditKeywords, "Please Select an Option",
				MySettings.KeywordList.Count == 0 ? 1 : MySettings.KeywordList.Count);
		}

		protected override void ReloadSettings() {
			base.ReloadSettings();
			ScreenMenu.UpdateOffset(MySettings.KeywordList.Count);
		}

		private void ShowCurrentKeywords() {
			Console.ForegroundColor = ConsoleColor.Cyan;
			if (MySettings == null || MySettings.KeywordList == null || MySettings.KeywordList.Count == 0) {
				Console.WriteLine("Website list empty");
			} else {
				foreach (var keyword in MySettings.KeywordList) {
					Console.WriteLine(keyword);
				}
			}
			Console.ResetColor();
		}

		private void AddKeyword(bool isWhitelist) {
			var form = new Input("Enter New Keyword Information");
			form.AddTextbox("Keyword");
			form.Display();

			var response = form.Responses;
			if (!response.TrueForAll(i => i.Equals(""))) {
				MySettings.KeywordList.Add(new Keyword(response[0], isWhitelist));
				ReloadSettings();
			}

			Start();
		}

		public override bool Start() {
			Console.Clear();
			ShowCurrentKeywords();

			var selection = ScreenMenu.Display();

			switch (selection) {
				case 0: // Add new whitelist keyword
					Console.Clear();
					AddKeyword(true);
					break;

				case 1: // Add new blacklist keyword
					Console.Clear();
					AddKeyword(false);
					break;

				case 2: // Edit keywords
					Console.Clear();
					EditKeywords();
					break;

				case 3: // Remove keyword
					Console.Clear();
					RemoveKeyword();
					break;

				case 4: // Return
					return true;
			}

			return false;
		}

		private void RemoveKeyword() {
			var list = new string[MySettings.KeywordList.Count + 1];
			MySettings.GetKeywords().CopyTo(list, 0);
			list[MySettings.KeywordList.Count] = "Back";

			var menu = new Menu(list, "Choose a keyword to delete");
			var selected = menu.Display();

			if (selected == list.Length - 1) {
				Start();
			}

			MySettings.KeywordList.RemoveAt(selected);
			ReloadSettings();

			Start();
		}

		private void EditKeywords() {
			var list = new string[MySettings.KeywordList.Count + 1];
			MySettings.GetKeywords().CopyTo(list, 0);
			list[MySettings.KeywordList.Count] = "Back";

			var menu = new Menu(list, "Choose a keyword to edit");
			var selected = menu.Display();

			if (selected == list.Length - 1) {
				Start();
			} else {
				var form = new Input("\nEnter replacement information");
				form.AddTextbox("Text", MySettings.KeywordList[selected].Text);
				form.AddTextbox("Whitelist/Blacklist (W/B)",
					MySettings.KeywordList[selected].IsWhitelist ? "Whitelist" : "Blacklist");
				form.AddTextbox("Enabled (On/Off)", MySettings.KeywordList[selected].Enabled ? "On" : "Off");
				form.Display();

				var response = form.Responses;
				MySettings.KeywordList.RemoveAt(selected);

				var keyword = new Keyword(response[0], true) {
					IsWhitelist = response[1].ToLower().Equals("w"),
					Enabled = response[2].ToLower().Equals("on")
				};
				MySettings.KeywordList.Add(keyword);

				ReloadSettings();

				Start();
			}
		}

	}

}