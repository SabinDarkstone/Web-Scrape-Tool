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

		/// <summary>
		/// Configuration information loaded from file to be used by the rest of the application
		/// </summary>
		public Configuration MasterConfig { get; set; }

		/// <summary>
		/// Results information loaded from file to be used by the rest of the application
		/// </summary>
		public CombinedResults MasterResults { get; set; }

		/// <summary>
		/// Completion percentage of search
		/// </summary>
		public double PercentSearchComplete { get; set; }

		private bool isSearchRunning;

		public MainForm() {
			InitializeComponent();
		}

		#region FormEvents
		private void Form1_Shown(object sender, EventArgs e) {
			LoadSettings();

			int count = 0;
			foreach (var website in MasterResults.AllResults)
			{
				foreach (var result in website.ResultList)
				{
					count++;
				}
			}
			LogHelper.Debug("Number of results found: " + count);
		}

		private void btnWebsiteList_Click(object sender, EventArgs e) {
			EditWebsites();
		}

		private void btnKeywordList_Click(object sender, EventArgs e) {
			EditKeywords();
		}

		private void btnBeginSearch_Click(object sender, EventArgs e) {
			StartSearch(false);
		}

		private void btnViewResults_Click(object sender, EventArgs e) {
			ShowResults();
		}

		private void btnHelp_Click(object sender, EventArgs e) {
			ViewHelp();
		}

		private void btnSendEmail_Click(object sender, EventArgs e) {
			SendEmail();
		}

		private void btnManageEmails_Click(object sender, EventArgs e) {
			EditEmailAddresses();
		}

		private void btnChangeSettings_Click(object sender, EventArgs e) {
			OpenSettings();
		}

		private void MainForm_Load(object sender, EventArgs e) {
			lblVersion.Text = Resources.Version;
			LoadSettings();
		}

		private void scheduleTimer_Tick(object sender, EventArgs e) {
			if (IsCorrectDay() && IsCorrectHour()) {
				StartSearch(true);
			}
		}
		#endregion

		/// <summary>
		/// Load settings and results from binary files
		/// </summary>
		private void LoadSettings() {
			// Load Settings File
			MasterConfig = FileHelper.LoadConfiguration() ?? new Configuration();
			MasterResults = FileHelper.LoadResults() ?? new CombinedResults();

			if (MasterConfig.EnableScheduler)
				scheduleTimer.Enabled = true;
		}

		/// <summary>
		/// Open result viewer to export results to excel file
		/// </summary>
		private void ShowResults() {
			var resultViewer = new ResultViewer(MasterConfig, MasterResults, true);
			resultViewer.ShowDialog();
		}

		/// <summary>
		/// Send an email with the results
		/// </summary>
		private void SendEmail() {
			var emailSender = new EmailProgress(MasterConfig, MasterResults, false);
			emailSender.ShowDialog();
		}

		/// <summary>
		/// Open the about window
		/// </summary>
		private void ViewHelp() {
			var about = new AboutHelp();
			about.ShowDialog();
		}

		/// <summary>
		/// Open keyword list editor
		/// </summary>
		private void EditKeywords() {
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

		/// <summary>
		/// Open website list editor
		/// </summary>
		private void EditWebsites() {
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

		/// <summary>
		/// Start web scrape and grant search
		/// </summary>
		/// <param name="isScheduled">Whether the search is initiated by the scheduler</param>
		private void StartSearch(bool isScheduled) {
			isSearchRunning = true;
			var grantSearch = new GrantSearch(MasterConfig, MasterResults,
				new Job(MasterConfig.Websites, MasterConfig.PageWords, MasterConfig.UrlWords));
			grantSearch.ShowDialog();

			// Check to see if the results need to be saved
			if (grantSearch.DialogResult == DialogResult.OK) {

				// Save results to settings file
				MasterResults = grantSearch.Results;
				FileHelper.SaveResults(MasterResults);
				grantSearch.Close();

				// Send Email if requested
				if (MasterConfig.EnableSendEmail) {
					var emailSender = new EmailProgress(MasterConfig, MasterResults, false);
					emailSender.ShowDialog();
				}
			}

			LogHelper.Debug("Searches started: " +
				MasterResults.AllResults.Count(i => i.WebsiteStatus != WebsiteResults.Status.Skipped));
			isSearchRunning = false;
		}

		/// <summary>
		/// Open settings window
		/// </summary>
		private void OpenSettings() {
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

		/// <summary>
		/// Open email address list editor
		/// </summary>
		private void EditEmailAddresses() {
			var emailEditor = new EmailListEditor(MasterConfig);
			emailEditor.ShowDialog();

			if (emailEditor.DialogResult == DialogResult.OK) {
				MasterConfig.Recipients = emailEditor.CurrentRecipients;
				emailEditor.Close();
				FileHelper.SaveConfiguration(MasterConfig);
			}
		}

		/// <summary>
		/// Checks if "today" is one of the days of the week
		/// </summary>
		/// <returns>Whether it is the correct day of the weekkk</returns>
		private bool IsCorrectDay() {
			return MasterConfig.ScheduleDaysOfWeek.Where(i => i.Value).Any(day => DateTime.Today.DayOfWeek.Equals(day.Key));
		}

		/// <summary>
		/// Check if it is the correct hour of the day
		/// </summary>
		/// <returns>Whether it is currently or after the current hour</returns>
		private bool IsCorrectHour() {
			return MasterConfig.SearchHour <= DateTime.Now.Hour;
		}
	}

}