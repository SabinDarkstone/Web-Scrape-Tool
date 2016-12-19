using System;
using System.Collections.Generic;
using System.Linq;

namespace Visco_Web_Scrape.Objects {

	[Serializable]
	public class Settings {

		public List<Website> SavedWebsites { get; set; }
		public List<Keyword> MasterKeywordList { get; set; }

		public Settings() {
			SavedWebsites = new List<Website>();
			MasterKeywordList = new List<Keyword>();
		}

		public List<Keyword> GetRelevancyKeywords() {
			return MasterKeywordList.Where(keyword => keyword.Type == Keyword.KeywordType.Relevance).ToList();
		}

		public List<Keyword> GetScrapingKeywords() {
			return MasterKeywordList.Where(keyword => keyword.Type == Keyword.KeywordType.Inclusion || keyword.Type == Keyword.KeywordType.Exclusion).ToList();
		}
	}
}
