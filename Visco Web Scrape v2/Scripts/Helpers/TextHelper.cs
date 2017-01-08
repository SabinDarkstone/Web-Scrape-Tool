using System;

namespace Visco_Web_Scrape_v2.Scripts.Helpers {
	public static class TextHelper {

		public static string FormatTime(TimeSpan timeToFormat) {
			var hours = (timeToFormat.Hours < 10) ? "0" + timeToFormat.Hours : timeToFormat.Hours.ToString();
			var minutes = (timeToFormat.Minutes < 10) ? "0" + timeToFormat.Minutes : timeToFormat.Minutes.ToString();
			var seconds = (timeToFormat.Seconds < 10) ? "0" + timeToFormat.Seconds : timeToFormat.Seconds.ToString();

			return hours + ":" + minutes + ":" + seconds;
		}
	}
}
