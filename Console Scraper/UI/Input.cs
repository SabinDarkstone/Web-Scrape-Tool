using System;
using System.Collections.Generic;

namespace Console_Scraper.UI {

	public class Input : IDisplayable {

		private readonly List<string> textboxList;
		private readonly string title;

		public List<string> Responses;

		public Input(string title = "Please answer in the following questions") {
			this.title = title;
			textboxList = new List<string>();
			Responses = new List<string>();
		}

		public void AddTextbox(string label, string response = "") {
			textboxList.Add(label);
			Responses.Add(response);
		}

		public int Display() {
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(title);
			Console.ResetColor();

			var i = 0;
			foreach (var textbox in textboxList) {
				if (Responses[i] != "") {
					Console.Write(textbox + ": [OLD: " + Responses[i] + "] ");
				} else {
					Console.Write(textbox + " ");
				}
				string resp = Console.ReadLine();
				if (resp != "" && resp != "\n") {
					Responses[i] = resp;
				}
				i++;
			}

			return 0;
		}
	}

}