using System;

namespace Visco_Web_Scrape_v2.Scripts.Helpers {

	public static class TextHelper {

		public static string FormatTime(TimeSpan timeToFormat) {
			var hours = (timeToFormat.Hours < 10) ? "0" + timeToFormat.Hours + (timeToFormat.Days * 24) : timeToFormat.Hours + (timeToFormat.Days * 24).ToString();
			var minutes = (timeToFormat.Minutes < 10) ? "0" + timeToFormat.Minutes : timeToFormat.Minutes.ToString();
			var seconds = (timeToFormat.Seconds < 10) ? "0" + timeToFormat.Seconds : timeToFormat.Seconds.ToString();

			return hours + ":" + minutes + ":" + seconds;
		}

		public static string TrimLength(this string str, int maxLength) {
			return str.Length > maxLength ? str.Substring(0, maxLength) : str;
		}

		public static string Shorten(this string str, int maxLength) {
			// If the string is already small enough, we're all good!
			if (str.Length < maxLength) {
				return str;
			}

			// For now, I am being lazy and chopping the string down to a certain number of characters
			return str.Substring(0, maxLength);

			// If not, there is some work to do
			/*
			 * 1. Identify the location and length of the keyword in the string
			 * 2. Split the string into two segments, before and after the keyword
			 * 3. Figure out how long each segment can be
			 * 4. Find substrings of words to the nearest whole word for each segment
			 * 5. Add ellipsis before the beginning of the start string and at the end of the end string
			 * 5. Merge the string back together
			 * 6. Verify that we have the correct length
			 * 7. Return the proper length string
			 */
		}
	}
}
