using System.Linq;
using Visco_Web_Scrape_v2.Scripts.Helpers;

namespace Visco_Web_Scrape_v2.Search.Items {

	public static class SessionManager {

		public static CombinedResults CurrentSession;
		public static CombinedResults PreviousSessionsCombined;

		public static void StartNewSession() {
			CurrentSession = new CombinedResults();
		}

		public static void MergeSession() {
			foreach (var domain in CurrentSession.AllResults) {
				if (!PreviousSessionsCombined.AllResults.Any(i => i.RootWebsite.Equals(domain.RootWebsite))) {
					PreviousSessionsCombined.AddResults(domain, false);
				}

				PreviousSessionsCombined.UpdateResults(domain);
			}
		}

		public static CombinedResults GetNewResults() {
			var newResults = new CombinedResults {
				LastRan = CurrentSession.LastRan,
				TotalSearchTime = CurrentSession.TotalSearchTime
			};

			foreach (var domain in CurrentSession.AllResults) {
				LogHelper.Debug("Current domain for comparison " + domain.RootWebsite.Name);
				if (PreviousSessionsCombined.AllResults.Any(i => i.RootWebsite.Equals(domain.RootWebsite))) {
					var resultsList = PreviousSessionsCombined.AllResults.First(i => i.RootWebsite.Equals(domain.RootWebsite));
					if (resultsList != null && (resultsList.ResultList == null || resultsList.ResultList.Count == 0)) {
						// Website searched in previous session but not results were ever found.
						// Act as if the domain was never searched before and add them all to the new results list
						newResults.AddResults(domain, false);
					} else {
						// Compare results inside domain
						var resultsToAdd = new WebsiteResults(domain.RootWebsite) {
							WebsiteStatus = domain.WebsiteStatus,
							Counts = domain.Counts,
							SearchTime = domain.SearchTime
						};
						foreach (var result in domain.ResultList) {
							if (resultsList != null && !resultsList.ResultList.Contains(result)) {
								resultsToAdd.AddResult(result);
							}
						}
						newResults.AllResults.Add(resultsToAdd);
					}
				} else {
					// The domain did not exist in previous sessions, add all results to new results list
					newResults.AddResults(domain, false);
				}
			}

			return newResults;
		}
	}

}