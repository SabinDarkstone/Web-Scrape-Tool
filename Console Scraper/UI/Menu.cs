using System;

namespace Console_Scraper.UI {

	public class Menu : IDisplayable {

		private int topOffset;
		private int bottomOffset;
		private ConsoleKeyInfo kb;
		private readonly string title;

		public string[] Options { get; }

		public Menu(string[] options, string title = "Please Select an Option", int offset = 0) {
			Options = options;
			this.title = title;

			if ((options.Length) > Console.WindowHeight) {
				throw new Exception("Too many items in array to display");
			}

			topOffset = Console.CursorTop + offset + 1;
			bottomOffset = 0;
			
			Console.CursorVisible = false;
		}

		public void UpdateOffset(int offset) {
			topOffset = offset + 1;
		}

		public int Display() {
			var selectedItem = 0;
			var loopComplete = false;

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(title);
			Console.ResetColor();

			while (!loopComplete) {
				for (int i = 0; i < Options.Length; i++) {
					if (i == selectedItem) {
						Console.BackgroundColor = ConsoleColor.Gray;
						Console.ForegroundColor = ConsoleColor.Black;
						Console.WriteLine("-" + Options[i]);
						Console.ResetColor();
					} else {
						Console.WriteLine("-" + Options[i]);
					}
				}

				bottomOffset = Console.CursorTop + 1;

				kb = Console.ReadKey(true);

				switch (kb.Key) {
					case ConsoleKey.UpArrow:
						if (selectedItem > 0) {
							selectedItem--;
						} else {
							selectedItem = (Options.Length - 1);
						}
						break;

					case ConsoleKey.DownArrow:
						if (selectedItem < (Options.Length - 1)) {
							selectedItem++;
						} else {
							selectedItem = 0;
						}
						break;

					case ConsoleKey.Enter:
						loopComplete = true;
						break;
				}

				Console.SetCursorPosition(0, topOffset);
			}

			Console.SetCursorPosition(0, bottomOffset);

			Console.CursorVisible = true;
			return selectedItem;
		}

	}

}