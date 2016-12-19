using System;
using System.Collections.Generic;

namespace Visco_Web_Scrape.Helpers {

	public static class TextHelper {

		public static List<int> AllIndicesOf(this string content, string search) {
			if (string.IsNullOrEmpty(search)) throw new ArgumentException("The string to find may not be empty", nameof(search));
			var indices = new List<int>();
			for (var index = 0;; index += search.Length) {
				index = content.IndexOf(content, index);
				if (index == -1) {
					return indices;
				}
				indices.Add(index);
			}
		}
	}
}
