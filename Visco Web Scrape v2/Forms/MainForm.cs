using System;
using System.Linq;
using System.Windows.Forms;
using Visco_Web_Scrape_v2.Properties;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Scripts.Helpers;
using Visco_Web_Scrape_v2.Search.Items;
using Visco_Web_Scrape_v2.Search.Process;

namespace Visco_Web_Scrape_v2.Forms {

	public partial class MainForm : Form {

		public Configuration MasterConfig { get; set; }
		public CombinedResults MasterResults { get; set; }
		public double PercentSearchComplete { get; set; }

		private bool isSearchRunning;

		public MainForm() {
			InitializeComponent();

			InitializeTrayIcon();
		}

		private void InitializeTrayIcon() {
			notifyIcon.BalloonTipText = "The searcher is still running...";
			notifyIcon.BalloonTipTitle = "VISCO, Inc. Web Scraper";
			notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
		}

		private void Form1_Shown(object sender, EventArgs e) {
			// Load Settings File
			MasterConfig = FileHelper.LoadConfiguration() ?? new Configuration();
			MasterResults = FileHelper.LoadResults() ?? new CombinedResults();

			PrintDebugInfo();

			/* UNDONE: Temporarys
			// Copy over settings
			var keywords = MasterConfig.PageWords;
			var websites = MasterConfig.Websites;
			MasterConfig = new Configuration {
				PageWords = keywords,
				Websites = websites
			};
			FileHelper.SaveConfiguration(MasterConfig);
			
			// Manually assign all results as strict
			foreach (var website in MasterResults.AllResults) {
				foreach (var result in website.ResultList) {
					result.IsStrict = true;
				}
			}

			// Set all current website to grant source
			foreach (var website in MasterConfig.Websites) {
				website.IsGrantSource = true;
			}
			*/
		}

		private void PrintDebugInfo() {
			LogHelper.Debug("Number of websites in results file: " + MasterResults.AllResults.Count);
			foreach (var website in MasterResults.AllResults) {
				LogHelper.Debug(website.RootWebsite.Name + " " + website.RootWebsite.Url);
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
				MasterConfig.PageWords = keywordEditor.Whitelist;
				MasterConfig.UrlWords = keywordEditor.Blacklist;
				FileHelper.SaveConfiguration(MasterConfig);
				keywordEditor.Close();
			}
		}

		private void btnBeginSearch_Click(object sender, EventArgs e) {
			// Open grant search window
			isSearchRunning = true;
			var grantSearch = new GrantSearch(MasterConfig, MasterResults, new Job(MasterConfig.Websites, MasterConfig.PageWords), this);
			grantSearch.ShowDialog();

			// Check to see if the results need to be saved
			if (grantSearch.DialogResult == DialogResult.OK) {

				// Save results to settings file
				LogHelper.Debug("Retrieving results from search");
				MasterResults = grantSearch.Results;
				LogHelper.Debug("Results retrieved");
				LogHelper.Debug("Sending command to save results");
				FileHelper.SaveResults(MasterResults);
				LogHelper.Debug("Command sent");
				grantSearch.Close();

				// Send Email if requested
				if (MasterConfig.EnableSendEmail) {
					var emailSender = new EmailProgress(MasterConfig, MasterResults);
					emailSender.ShowDialog();
				}
			}

			LogHelper.Debug("Searches started: " +
				MasterResults.AllResults.Count(i => i.WebsiteStatus != WebsiteResults.Status.Skipped));
			isSearchRunning = false;
		}

		private void btnViewResults_Click(object sender, EventArgs e) {
			// Open result viewer window
			var resultViewer = new ResultViewer(MasterConfig, MasterResults);
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
			var settings = new AdjustSettings(MasterConfig, MasterResults);
			settings.ShowDialog();

			if (settings.DialogResult == DialogResult.OK) {
				MasterConfig = settings.Settings;
				MasterResults = settings.Results;
				settings.Close();
				FileHelper.SaveConfiguration(MasterConfig);
				FileHelper.SaveResults(MasterResults);
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
			var emailSender = new EmailProgress(MasterConfig, MasterResults);
			emailSender.ShowDialog();
		}

		private void MainForm_Resize(object sender, EventArgs e) {
			if (WindowState == FormWindowState.Minimized) {
				notifyIcon.Visible = true;
				notifyIcon.ShowBalloonTip(500);
				Hide();
			} else if (WindowState == FormWindowState.Normal) {
				notifyIcon.Visible = false;
			}
		}

		private void notifyIcon_DoubleClick(object sender, EventArgs e) {

		}

		private void notifyIcon_Click(object sender, EventArgs e) {
			if (isSearchRunning) {
				notifyIcon.BalloonTipTitle = "VISCO Search Progress";
				notifyIcon.BalloonTipText = "Search Running:\n" + Math.Round(PercentSearchComplete, 2) + "% Complete";
				notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
				notifyIcon.ShowBalloonTip(1000);
			} else {
				notifyIcon.BalloonTipTitle = "VISCO Search Program";
				notifyIcon.BalloonTipText = "Program is currently idle";
				notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
				notifyIcon.ShowBalloonTip(1000);
			}
		}
		
		private void notifyIcon_BalloonTipClicked(object sender, EventArgs e) {
			// TODO: Make this come to front
			notifyIcon.Visible = false;
			this.Show();
			this.BringToFront();
		}
	}

}