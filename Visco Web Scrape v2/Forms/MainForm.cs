using System;
using System.Windows.Forms;
using Visco_Web_Scrape_v2.Properties;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Scripts.Helpers;
using Visco_Web_Scrape_v2.Search.Process;

namespace Visco_Web_Scrape_v2.Forms {

	public partial class MainForm : Form {

		public Configuration MasterConfig { get; set; }

		public MainForm() {
			InitializeComponent();
		}

		private void Form1_Shown(object sender, EventArgs e) {
			// Load Settings File
			MasterConfig = FileHelper.LoadConfiguration() ?? new Configuration();

			/* UNDONE: Temporarys
			var keywords = MasterConfig.Keywords;
			var websites = MasterConfig.Websites;
			MasterConfig = new Configuration {
				Keywords = keywords,
				Websites = websites
			};
			FileHelper.SaveConfiguration(MasterConfig);
			*/
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
			var sendEmail = false;

			// Check to see if the results need to be saved
			if (grantSearch.DialogResult == DialogResult.OK || grantSearch.DialogResult == DialogResult.Yes) {

				// Save results to settings file
				MasterConfig.LastCrawl.Results = grantSearch.CompareLists(MasterConfig.LastCrawl.Results, MasterConfig.OnlyNewResults);
				MasterConfig.LastCrawl.Date = grantSearch.Config.LastCrawl.Date;
				if (grantSearch.LastProgress.CurrentStatus == Progress.Status.Cancelled) {
					MasterConfig.LastCrawl.CompletionStatus = "Canceled Early";
					var response = MessageBox.Show(Resources.ConfirmSendIncompleteSearch, Resources.ConfirmationRequired,
						MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (response == DialogResult.Yes) sendEmail = true;
				} else {
					MasterConfig.LastCrawl.CompletionStatus = "Completed Successfully";
					sendEmail = true;
				}
				FileHelper.SaveConfiguration(MasterConfig);
				grantSearch.Close();
			}

			LogHelper.Debug("Completion status: " + MasterConfig.LastCrawl.CompletionStatus);

			// Send the email if we need to
			if (sendEmail) {
				var emailProgress = new EmailProgress(MasterConfig);
				emailProgress.ShowDialog();
			}
		}

		private void btnViewResults_Click(object sender, EventArgs e) {
			// Open result viewer window
			var resultViewer = new ResultViewer(MasterConfig);
			resultViewer.ShowDialog();
		}

		private void btnHelp_Click(object sender, EventArgs e) {
			// Open about window
			var about = new AboutHelp();
			about.ShowDialog();
		}

		private void MainForm_Load(object sender, EventArgs e) {
			lblVersion.Text = Resources.Version;
		}

		private void btnGrantTrack_Click(object sender, EventArgs e) {
			MessageBox.Show(Resources.FeatureNotImplemented, Resources.HeadsUp, MessageBoxButtons.OK, MessageBoxIcon.Stop);

			// Open track grants windows
			var grantTracker = new GrantTracker();
			grantTracker.ShowDialog();
		}

		private void btnChangeSettings_Click(object sender, EventArgs e) {
			// Open settings window
			var settings = new AdjustSettings(MasterConfig);
			settings.ShowDialog();

			if (settings.DialogResult == DialogResult.OK) {
				MasterConfig = settings.Settings;
				settings.Close();
				FileHelper.SaveConfiguration(MasterConfig);
			}
		}

		private void btnManageEmails_Click(object sender, EventArgs e) {
			// Open email editor window
			var emailEditor = new EmailListEditor(MasterConfig);
			emailEditor.ShowDialog();

			if (emailEditor.DialogResult == DialogResult.OK) {
				MasterConfig.Recipients = emailEditor.CurrentRecipients;
				emailEditor.Close();
				FileHelper.SaveConfiguration(MasterConfig);
			}
		}

		private void btnSendEmail_Click(object sender, EventArgs e) {
			var emailSender = new EmailProgress(MasterConfig);
			emailSender.ShowDialog();
		}
	}

}