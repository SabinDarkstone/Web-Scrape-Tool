using System;
using System.Windows.Forms;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Scripts.Helpers;
using Visco_Web_Scrape_v2.Search;
using Visco_Web_Scrape_v2.Search.Process;

namespace Visco_Web_Scrape_v2.Forms {

	public partial class MainForm : Form {

		public Configuration MasterConfig { get; set; }

		public MainForm() {
			InitializeComponent();
		}

		private void Form1_Shown(object sender, EventArgs e) {
			// Load Settings File
			MasterConfig = FileHelper.LoadConfiguration();
			if (MasterConfig == null) {
				MasterConfig = new Configuration();
			}
		}

		private void btnWebsiteList_Click(object sender, EventArgs e) {
			// Open website list editor window
			var websiteEditor = new WebsiteList(MasterConfig);
			websiteEditor.ShowDialog();

			// Check to see if the websites need to be saved
			if (websiteEditor.DialogResult == DialogResult.OK) {
				// Save changes to settings file
				MasterConfig.Websites = websiteEditor.CurrentWebsites;
				FileHelper.SaveConfiguration(MasterConfig);
				websiteEditor.Close();
			}
		}

		private void btnKeywordList_Click(object sender, EventArgs e) {
			// Open keyword editor window
			var keywordEditor = new KeywordList(MasterConfig);
			keywordEditor.ShowDialog();

			// Check to see if the websites need to be saved
			if (keywordEditor.DialogResult == DialogResult.OK) {
				// Save changes to settings file
				MasterConfig.Keywords = keywordEditor.CurrentKeywords;
				FileHelper.SaveConfiguration(MasterConfig);
				keywordEditor.Close();
			}
		}

		private void btnBeginSearch_Click(object sender, EventArgs e) {
			// Open grant search window
			var grantSearch = new GrantSearch(MasterConfig, new Job(MasterConfig.Websites, MasterConfig.Keywords));
			grantSearch.ShowDialog();

			// Check to see if the results need to be saved
			if (grantSearch.DialogResult == DialogResult.OK) {
				// Save results to settings file
				MasterConfig.Results = grantSearch.AllResults;
				FileHelper.SaveConfiguration(MasterConfig);
				grantSearch.Close();
			}
		}

		private void btnViewResults_Click(object sender, EventArgs e) {
			// Open result viewer window
			var resultViewer = new ResultViewer(MasterConfig);
			resultViewer.ShowDialog();
		}
	}

}