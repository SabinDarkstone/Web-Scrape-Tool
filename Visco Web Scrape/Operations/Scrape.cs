using System.Collections.Generic;
using System.Linq;
using CsQuery.ExtensionMethods.Internal;
using HtmlAgilityPack;
using Visco_Web_Scrape.Forms;
using Visco_Web_Scrape.Helpers;
using Visco_Web_Scrape.Objects;

namespace Visco_Web_Scrape.Operations {

	// TODO: Start implementation of scraper
	public class Scrape {

		private readonly List<HtmlDocument> documents;
		private readonly List<Keyword> keywords;

		public Scrape() {

		}

		private static bool FindInclusion(Keyword keyword) {
			return keyword.Type == Keyword.KeywordType.Inclusion;
		}

		private void ExecuteScrape() {
			foreach (var document in documents) {
				LogHelper.Debug("Checking document " + document.DocumentNode.Name + " for structures");
				CheckForStructures(document);
				SearchViaKeywords(document);
			}

			LogHelper.Info("Search for structures completed.");
		}

		private void CheckForStructures(HtmlDocument doc) {
			// Look for tables on page
			var tables = doc.DocumentNode.SelectNodes("//table").ToArray();
			if (!tables.IsNullOrEmpty()) {
				LogHelper.Debug("Table found!");
				CheckForKeywords(tables);
			}
		}

		private void SearchViaKeywords(HtmlDocument doc) {
			LogHelper.Debug("Searching through document via keywords");
			bool foundResult = false;
			foreach (var node in doc.DocumentNode.ChildNodes) {
				foreach (var keyword in keywords.FindAll(FindInclusion)) {
					if (node.InnerText.Contains(keyword.Text)) {
						LogHelper.Debug("Found keyword " + keyword.Text + " in document: " + node.InnerText);
						foundResult = true;
					}
				}
			}
			if (!foundResult) {
				LogHelper.Debug("No keywords found");
			}
		}

		private void CheckForKeywords(IEnumerable<HtmlNode> nodes) {
			foreach (var table in nodes) {
				var text = table.InnerText;
				foreach (var keyword in keywords) {
					LogHelper.Debug("Checking for keyword " + keyword.Text);
					if (text.Contains(keyword.Text)) {
						var rows = table.SelectNodes("//tr");
						foreach (var row in rows) {
							LogHelper.Debug(row.InnerText);
						}
					}
				}
			}
		}
	}

}