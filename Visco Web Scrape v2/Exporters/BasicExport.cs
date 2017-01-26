using System.Collections.Generic;
using System.Linq;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Scripts.Helpers;
using Visco_Web_Scrape_v2.Search.Items;

namespace Visco_Web_Scrape_v2.Exporters {

	public class BasicExport {

		/// <summary>
		/// Current configuration
		/// </summary>
		protected Configuration Config { get; }

		/// <summary>
		/// Results used by exporter after filtering is complete
		/// </summary>
		protected CombinedResults ResultsToExport { get; }

		public BasicExport(Configuration configuration, CombinedResults results) {
			this.Config = configuration;

			ResultsToExport = new CombinedResults();

			// Allocate spots for each website that needs to be exported
			ResultsToExport = PrepareResults(results);

			// Fill in the allocated spots with the requisite data
			ResultsToExport = FilterResults(ResultsToExport, results);
		}

		private CombinedResults PrepareResults(CombinedResults unfilteredResults) {
			LogHelper.Debug("Creating blank CombinedResults for exported data");
			var myResults = new CombinedResults();

			foreach (var website in unfilteredResults.AllResults) {
				LogHelper.Debug("Making a spot for " + website.RootWebsite);
				myResults.AddResults(website, true);
			}

			return myResults;
		}

		private CombinedResults FilterResults(CombinedResults blankResults, CombinedResults unfilteredResults) {
			LogHelper.Debug("Filling in CombinedResults with properly filtered data");
			var myResults = blankResults;

			foreach (var website in myResults.AllResults.ToList()) {
				if (website == null) {
					LogHelper.Error("No matching website found for " + website.RootWebsite);
					continue;
				}

				// If the website is disabled, remove it
				if (!website.RootWebsite.IsEnabled) {
					LogHelper.Debug("Removing disabled website " + website.RootWebsite);
					myResults.AllResults.Remove(website);
				}

				// Filter pages in WebsiteResults
				foreach (var page in website.ResultList.ToList()) {
					// Remove pages that are not new (if option is selected)
					if (Config.OnlyNewResults && !page.IsNewResult) {
						LogHelper.Debug(page + " is not a new result, removing from exported results");
						website.ResultList.Remove(page);
						continue;
					}

					// Remove pages with only links as results (if option is selected)
					if (Config.EnableLinkResultFilter && page.IsLinkResult()) {
						LogHelper.Debug(page + " contains only links as results, removing from exported results");
						website.ResultList.Remove(page);
						continue;
					}

					// Keep page
					LogHelper.Debug(page + " is a valid page to keep with selected export options");
				}
			}

			LogHelper.Debug("WEBSITE LIST:");
			foreach (var website in myResults.AllResults) {
				LogHelper.Debug(website.RootWebsite.Name);
			}

			return myResults;
		}

		protected string GetKeywordList(Result result) {
			string keywords = "";
			var keywordCountTracker = new Dictionary<string, int>();

			LogHelper.Debug("Getting keywords for " + result.PageUrl);
			foreach (var matchInfo in result.Hits) {

				if (Config.EnableLinkResultFilter && matchInfo.IsLink) {
					continue;
				}

				if (keywordCountTracker.ContainsKey(matchInfo.Keyword.Text)) {
					keywordCountTracker[matchInfo.Keyword.Text]++;
				} else {
					keywordCountTracker.Add(matchInfo.Keyword.Text, 1);
				}
			}

			foreach (var keyword in keywordCountTracker.Keys) {
				if (keywordCountTracker[keyword] > 1) {
					keywords += keyword + " (x" + keywordCountTracker[keyword] + "), ";
				} else {
					keywords += keyword + ", ";
				}
			}

			if (keywords.Length == 0) {
				return "";
			} else {
				LogHelper.Debug(keywords);
				return keywords.Substring(0, keywords.Length - 2);
			}
		}
	}
}
