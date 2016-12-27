using System;
using System.Collections.Generic;
using System.Linq;

namespace Visco_Web_Scrape_v2.Search.Items {

	[Serializable]
	public class Result {

		public string Url { get; set; }
		public List<Keyword> Keywords { get; set; }
		public bool IsNew { get; set; }
		public DateTime DateFound { get; set; }

		public Result(string url, Keyword keyword) {
			Url = url;
			Keywords = new List<Keyword> { keyword };
			DateFound = DateTime.Today;
			IsNew = true;
		}

		public Result(string url) {
			Url = url;
			DateFound = DateTime.Today;
			IsNew = true;
		}

		public void MarkAsNew() {
			this.IsNew = true;
		}

		public override string ToString() {
			var myString = Url + " contains ";
			myString = Keywords.Aggregate(myString, (current, keyword) => current + (keyword + ", "));
			return myString.Substring(0, myString.Length - 2);
		}
	}
}
