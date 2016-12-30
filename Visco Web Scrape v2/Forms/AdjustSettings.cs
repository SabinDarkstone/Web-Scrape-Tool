using System;
using System.Windows.Forms;
using Visco_Web_Scrape_v2.Scripts;

namespace Visco_Web_Scrape_v2.Forms {

	public partial class AdjustSettings : Form {

		/// <summary>
		/// Stores the settings being modified
		/// </summary>
		public Configuration Settings { get; set; }

		/// <summary>
		/// Form that allows changing of settings on a per-user basis
		/// </summary>
		/// <param name="config"></param>
		public AdjustSettings(Configuration config) {
			InitializeComponent();
			Settings = config;
		}

		/// <summary>
		/// Transfers settings into the form so current ones can be viewed
		/// </summary>
		private void PopulateFields() {
			txtPagesPerDomain.Text = Settings.PagesToCrawlPerDomain.ToString();

			chkbxEnableUrlFilter.Checked = Settings.EnableUrlFiltering;
			chkbxAnalyzeUrl.Checked = Settings.EnableUrlAnalysis;
			chkbxDateFound.Checked = Settings.IncludeDate;
			chkbxNewResultsOnly.Checked = Settings.OnlyNewResults;

			var exportMethod = Settings.ExportMethod;
			if (exportMethod == Configuration.ExportType.Excel) radioExcel.Checked = true;
			if (exportMethod == Configuration.ExportType.Plain) radioPlainText.Checked = true;
			if (exportMethod == Configuration.ExportType.Xml) radioXml.Checked = true;

			btnClearResults.Enabled = Settings.LastCrawl.Results.Count != 0;

			if (Settings.Websites.Count > 0 || Settings.Keywords.Count > 0) {
				btnClearSearchSettings.Enabled = true;
			} else if (Settings.Websites.Count == 0 && Settings.Keywords.Count == 0) {
				btnClearSearchSettings.Enabled = false;
			}
		}

		/// <summary>
		/// Runs when the form is shown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AdjustSettings_Shown(object sender, EventArgs e) {
			PopulateFields();
		}

		/// <summary>
		/// Clears search results after confirming action with user
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnClearResults_Click(object sender, EventArgs e) {
			var sure = MessageBox.Show(Reference.Messages.ClearCrawlResults, Reference.Messages.AreYouSure,
				MessageBoxButtons.YesNo, MessageBoxIcon.Question);

			if (sure == DialogResult.Yes) {
				Settings.LastCrawl.Results.Clear();
				PopulateFields();
			}
		}

		/// <summary>
		/// Clears the website and keyword lists
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnClearSearchSettings_Click(object sender, EventArgs e) {
			var sure = MessageBox.Show(Reference.Messages.ClearSearchSettings, Reference.Messages.AreYouSure,
				MessageBoxButtons.YesNo, MessageBoxIcon.Question);

			if (sure == DialogResult.Yes) {
				Settings.Websites.Clear();
				Settings.Keywords.Clear();
				MessageBox.Show(Reference.Messages.WarnMismatchSettingsAndResults, Reference.Messages.Warning,
					MessageBoxButtons.OK, MessageBoxIcon.Warning);
				PopulateFields();
			}
		}

		/// <summary>
		/// Applies settings on form to the main application and closes the form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnApply_Click(object sender, EventArgs e) {
			Settings.EnableUrlFiltering = chkbxEnableUrlFilter.Checked;
			Settings.EnableUrlAnalysis = chkbxAnalyzeUrl.Checked;
			Settings.IncludeDate = chkbxDateFound.Checked;
			Settings.OnlyNewResults = chkbxNewResultsOnly.Checked;

			Settings.PagesToCrawlPerDomain = int.Parse(txtPagesPerDomain.Text);

			if (radioExcel.Checked) {
				Settings.ExportMethod = Configuration.ExportType.Excel;
			} else if (radioPlainText.Checked) {
				Settings.ExportMethod = Configuration.ExportType.Plain;
			} else if (radioXml.Checked) {
				Settings.ExportMethod = Configuration.ExportType.Xml;
			} else {
				throw new ArgumentNullException("Export file type");
			}

			DialogResult = DialogResult.OK;
			Hide();
		}

		/// <summary>
		/// Disregards any settings changes and returns to the main form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancel_Click(object sender, EventArgs e) {
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}

}