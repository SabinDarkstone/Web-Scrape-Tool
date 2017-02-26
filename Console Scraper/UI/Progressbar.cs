using System;

namespace Console_Scraper.UI {

	public class Progressbar : IDisplayable {

		private readonly int maximum;
		private readonly int length;
		private readonly char emptySpace;
		private readonly char filledSpace;
		private int currentValue;
		private double percentage;
		private string barString;
		private bool displayBookends;

		public Progressbar(int maximum, int length, char empty, char filled) {
			this.maximum = maximum;
			this.length = length;
			emptySpace = empty;
			filledSpace = filled;
		}

		public void SetBookends(bool value) {
			displayBookends = value;
		}

		public int Display() {
			Console.SetCursorPosition(0, 0);
			Console.ForegroundColor  = ConsoleColor.Green;
			Console.Write(displayBookends ? "\r[{0}] {1}%" : "\r{0} {1}%", barString, (int) (percentage * 100));
			Console.ResetColor();

			return (int) (percentage * 100);
		}

		private void PrepareBar() {
			if (currentValue > maximum) return;
			barString = "";
			for (var i = 0; i < length; i++) {
				barString += percentage * length >= i + 1 ? filledSpace : emptySpace;
			}
		}

		public void Update() {
			currentValue++;
			percentage = (currentValue * 1.0) / maximum;
			PrepareBar();
		}

	}

}