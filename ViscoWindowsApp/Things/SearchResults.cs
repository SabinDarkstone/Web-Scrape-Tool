using System.Collections.Generic;

namespace ViscoWindowsApp.Things {

	public class SearchResults {

		public Website RootWebsite { get; }
		public List<Result> ResultList { get; set; }

		public SearchResults(Website root) {
			ResultList = new List<Result>();
			RootWebsite = root;
		}
	}
}
