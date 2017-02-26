using System;
using System.Collections.Generic;
using Console_Scraper.Search.Findings;
using Console_Scraper.Search.Items;

namespace Console_Scraper {

	[Serializable]
	public class Settings {

		public List<Website> WebsiteList { get; set; }
		public List<Keyword> KeywordList { get; set; }
		public List<Receipent> SendToList { get; set; }
		public List<Session> PreviousSessions { get; set; }
		public Session CurrentSession { get; set; }
		public string EmailSubject { get; set; }
		public string EmailBody { get; set; }

		public Settings() {
			WebsiteList = new List<Website>();
			KeywordList = new List<Keyword>();
		}

		public string[] GetWebsites() {
			var array = new string[WebsiteList.Count];
			for (int i = 0; i < WebsiteList.Count; i++) {
				array[i] = WebsiteList[i].ToString();
			}

			return array;
		}

		public string[] GetKeywords() {
			var array = new string[KeywordList.Count];
			for (int i = 0; i < KeywordList.Count; i++) {
				array[i] = KeywordList[i].ToString();
			}

			return array;
		}

		public string[] GetReceipients() {
			var array = new string[SendToList.Count];
			for (int i = 0; i < SendToList.Count; i++) {
				array[i] = SendToList[i].ToString();
			}

			return array;
		}
	}

}