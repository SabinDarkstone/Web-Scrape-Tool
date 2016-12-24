using System.Collections.Generic;

namespace ViscoWindowsApp.Things {

	public class Result {

		public string Url { get; set; }
		public List<Keyword> Keywords { get; set; }

		public Result(string url, string keyword) {
			
		}

		public Result(string url) {
			Url = url;
		}
	}
}
