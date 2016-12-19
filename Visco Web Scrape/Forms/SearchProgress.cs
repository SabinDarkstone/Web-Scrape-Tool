using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Visco_Web_Scrape.Objects;
using Visco_Web_Scrape.Operations;
using Visco_Web_Scrape.Operations.Configs;

namespace Visco_Web_Scrape.Forms {

	// TODO: Get BackgroundWorker to work (hah)
	// TODO: Find a way to show progess of crawl through (sub)domain
	public partial class SearchProgress : Form {

		private readonly List<Website> websitesToCrawl;
		private readonly Settings currentSettings;

		public SearchProgress(List<Website> websites, Settings settings) {
			InitializeComponent();

			// Initialize stuff
			websitesToCrawl = websites;
			currentSettings = settings;
		}

		private void ScrapeProgress_Shown(object sender, EventArgs e) {
			var crawlSettings = new CrawlSettings(currentSettings.MasterKeywordList, websitesToCrawl);
			new Crawl(crawlSettings);
		}

		private void btnCancelScrape_Click(object sender, EventArgs e) {

		}

		private void SearchProgress_Load(object sender, EventArgs e) {
			
		}

		private void btnExportDocuments_Click(object sender, EventArgs e) {

		}
	}
}
