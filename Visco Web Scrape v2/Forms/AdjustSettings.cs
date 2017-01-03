using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Vbe.Interop;
using Visco_Web_Scrape_v2.Properties;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Search.Items;
using Reference = Visco_Web_Scrape_v2.Scripts.Reference;

namespace Visco_Web_Scrape_v2.Forms {

	public partial class AdjustSettings : Form {

		public Configuration Settings { get; set; }
		public CombinedResults Results { get; set; }

		public AdjustSettings(Configuration config, CombinedResults res) {
			InitializeComponent();
			Settings = config;
			Results = res;
		}

		private void PopulateFields() {
			txtPagesPerDomain.Text = Settings.PagesToCrawlPerDomain.ToString();

			chkbxEnableUrlFilter.Checked = Settings.EnableUrlFiltering;
			chkbxAnalyzeUrl.Checked = Settings.EnableUrlAnalysis;
			chkbxDateFound.Checked = Settings.IncludeDate;
			chkbxNewResultsOnly.Checked = Settings.OnlyNewResults;
			chkbxSendEmail.Checked = Settings.EnableSendEmail;

			var exportMethod = Settings.ExportMethod;
			if (exportMethod == Configuration.ExportType.Excel) radioExcel.Checked = true;
			if (exportMethod == Configuration.ExportType.Plain) radioPlainText.Checked = true;
			if (exportMethod == Configuration.ExportType.Xml) radioXml.Checked = true;

			if (Settings.Websites.Count > 0 || Settings.PageWords.Count > 0) {
				btnClearSearchSettings.Enabled = true;
			} else if (Settings.Websites.Count == 0 && Settings.PageWords.Count == 0) {
				btnClearSearchSettings.Enabled = false;
			}
		}

		private void AdjustSettings_Shown(object sender, EventArgs e) {
			PopulateFields();
		}

		private void btnClearResults_Click(object sender, EventArgs e) {
			var sure = MessageBox.Show(Resources.ConfirmClearResults, Resources.ConfirmationRequired,
				MessageBoxButtons.YesNo, MessageBoxIcon.Question);

			if (sure == DialogResult.Yes) {
				File.Delete(Reference.Files.ResultsFile);
				Results = new CombinedResults();
				PopulateFields();
			}
		}

		private void btnClearSearchSettings_Click(object sender, EventArgs e) {
			var sure = MessageBox.Show(Resources.ConfirmClearWebAndKeys, Resources.ConfirmationRequired,
				MessageBoxButtons.YesNo, MessageBoxIcon.Question);

			if (sure == DialogResult.Yes) {
				Settings.Websites.Clear();
				Settings.PageWords.Clear();
				MessageBox.Show(Resources.MismatchWarning, Resources.HeadsUp,
					MessageBoxButtons.OK, MessageBoxIcon.Warning);
				PopulateFields();
			}
		}

		private void btnApply_Click(object sender, EventArgs e) {
			Settings.EnableUrlFiltering = chkbxEnableUrlFilter.Checked;
			Settings.EnableUrlAnalysis = chkbxAnalyzeUrl.Checked;
			Settings.IncludeDate = chkbxDateFound.Checked;
			Settings.OnlyNewResults = chkbxNewResultsOnly.Checked;
			Settings.EnableSendEmail = chkbxSendEmail.Checked;

			Settings.PagesToCrawlPerDomain = int.Parse(txtPagesPerDomain.Text);

			if (radioExcel.Checked) {
				Settings.ExportMethod = Configuration.ExportType.Excel;
			} else if (radioPlainText.Checked) {
				Settings.ExportMethod = Configuration.ExportType.Plain;
			} else if (radioXml.Checked) {
				Settings.ExportMethod = Configuration.ExportType.Xml;
			} else {
				throw new ArgumentNullException(nameof(sender));
			}

			DialogResult = DialogResult.OK;
			Hide();
		}

		private void btnCancel_Click(object sender, EventArgs e) {
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void radioPlainText_CheckedChanged(object sender, EventArgs e) {
			if (radioPlainText.Checked) {
				MessageBox.Show(Resources.FeatureNotImplemented, Resources.HeadsUp, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void radioXml_CheckedChanged(object sender, EventArgs e) {
			if (radioXml.Checked) {
				MessageBox.Show(Resources.FeatureNotImplemented, Resources.HeadsUp, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
	}

}