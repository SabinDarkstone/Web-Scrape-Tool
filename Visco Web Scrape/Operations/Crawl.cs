using System.Collections.Generic;
using System.IO;
using System.Net;
using Abot.Crawler;
using Visco_Web_Scrape.Forms;
using Visco_Web_Scrape.Helpers;
using Visco_Web_Scrape.Objects;
using Visco_Web_Scrape.Operations.Configs;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace Visco_Web_Scrape.Operations {

	public class Crawl {

		public SearchProgress Progress;

		private PoliteWebCrawler crawler;
		private readonly CrawlSettings crawlSettings;

		public Crawl(CrawlSettings crawlSettings) {
			this.crawlSettings = crawlSettings;
		}

		private void RunCrawler() {

		}

		private void RegisterEvents() {
			if (crawler == null) return;

			LogHelper.Debug("Registering event handlers with crawler...");
			crawler.PageCrawlStartingAsync += crawler_OnPageCrawlStartingAsync;
			crawler.PageCrawlCompletedAsync += crawler_OnPageCrawlCompletedAsync;
			crawler.PageCrawlDisallowedAsync += crawler_OnPageCrawlDisallowedAsync;
			crawler.PageLinksCrawlDisallowedAsync += crawler_OnPageLinksCrawlDisallowedAsync;
			LogHelper.Debug("Event handler registration completed successfully");
		}

		private void ScanDocument(HtmlDocument document) {
			foreach (var keyword in crawlSettings.Keywords) {
				if (keyword.Type == Keyword.KeywordType.Relevance) {  // Is it the right kind of keyword?
					if (document.DocumentNode.InnerText.Contains(keyword.Text)) {  // Is that keyword in the document?

					}
				}
			}
		}

		private void crawler_OnPageCrawlStartingAsync(object sender, PageCrawlStartingArgs e) {
			var pageToCrawl = e.PageToCrawl;
			LogHelper.Crawl("Starting crawl on " + pageToCrawl.Uri.AbsoluteUri);
		}

		private void crawler_OnPageCrawlCompletedAsync(object sender, PageCrawlCompletedArgs e) {
			var crawledPage = e.CrawledPage;

			if (crawledPage.WebException != null || crawledPage.HttpWebResponse.StatusCode != HttpStatusCode.OK) {
				LogHelper.Crawl("Crawl of page failed " + crawledPage.Uri.AbsoluteUri);
				return;
			} else {
				LogHelper.Crawl("Crawl of page succeeded " + crawledPage.Uri.AbsoluteUri);
			}

			if (string.IsNullOrEmpty(crawledPage.Content.Text)) {
				LogHelper.Crawl("Page has no content " + crawledPage.Uri.AbsoluteUri);
				return;
			}

			var document = crawledPage.HtmlDocument;
			ScanDocument(document);
		}

		private void crawler_OnPageCrawlDisallowedAsync(object sender, PageCrawlDisallowedArgs e) {

		}

		private void crawler_OnPageLinksCrawlDisallowedAsync(object sender, PageLinksCrawlDisallowedArgs e) {
			
		}

	}

}