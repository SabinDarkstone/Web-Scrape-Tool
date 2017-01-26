using System;
using System.IO;
using System.Windows.Forms;
using Visco_Web_Scrape_v2.Properties;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Search.Items;
using Reference = Visco_Web_Scrape_v2.Scripts.Reference;

namespace Visco_Web_Scrape_v2.Forms {

	public partial class AdjustSettings : Form {

		public Configuration Settings { get; set; }
		public CombinedResults Results { get; set; }

		public AdjustSettings(Configuration config, CombinedResults results) {
			InitializeComponent();
			Settings = config;
			Results = results;
		}

		private void PopulateFields() {
			txtPagesPerDomain.Text = Settings.PagesToCrawlPerDomain.ToString();
			chkbxContextSensitive.Checked = Settings.EnableContextSensitiveFilter;
			chkbxEnableUrlFilter.Checked = Settings.EnableUrlFiltering;

			chkbxStrictFiltering.Checked = Settings.EnableLinkResultFilter;
			chkbxDateFound.Checked = Settings.IncludeDate;
			chkbxNewResultsOnly.Checked = Settings.OnlyNewResults;

			chkbxSendEmail.Checked = Settings.EnableSendEmail;

			if (Settings.ScheduleDaysOfWeek[DayOfWeek.Sunday])
				chkbxSunday.Checked = true;
			if (Settings.ScheduleDaysOfWeek[DayOfWeek.Monday])
				chkbxMonday.Checked = true;
			if (Settings.ScheduleDaysOfWeek[DayOfWeek.Tuesday])
				chkbxTuesday.Checked = true;
			if (Settings.ScheduleDaysOfWeek[DayOfWeek.Wednesday])
				chkbxWednesday.Checked = true;
			if (Settings.ScheduleDaysOfWeek[DayOfWeek.Thursday])
				chkbxThursday.Checked = true;
			if (Settings.ScheduleDaysOfWeek[DayOfWeek.Friday])
				chkbxFriday.Checked = true;
			if (Settings.ScheduleDaysOfWeek[DayOfWeek.Saturday])
				chkbxSaturday.Checked = true;

			/*
			txtTimeOfDayHour.Text = (Settings.ScheduleSearchTime.Hours % 12).ToString();
			txtTimeOfDayMinute.Text = Settings.ScheduleSearchTime.Minutes.ToString();
			if (Settings.ScheduleSearchTime.Hours > 0 && Settings.ScheduleSearchTime.Hours < 12) {
				radioAm.Checked = true;
				radioPm.Checked = false;
			} else {
				radioAm.Checked = false;
				radioPm.Checked = true;
			}
			*/

			txtTimeOfDayHour.Text = (Settings.SearchHour % 12).ToString();
			txtTimeOfDayMinute.Text = "00";
			if (Settings.SearchHour > 0 && Settings.SearchHour < 12) {
				radioAm.Checked = true;
				radioPm.Checked = false;
			} else {
				radioAm.Checked = false;
				radioPm.Checked = true;
			}

			txtRepeatWeeks.Text = Settings.RepeatWeekCount.ToString();
		}

		private void btnApply_Click(object sender, EventArgs e) {
			Settings.PagesToCrawlPerDomain = int.Parse(txtPagesPerDomain.Text);
			Settings.EnableContextSensitiveFilter = chkbxContextSensitive.Checked;
			Settings.EnableUrlFiltering = chkbxEnableUrlFilter.Checked;

			Settings.EnableLinkResultFilter = chkbxStrictFiltering.Checked;
			Settings.IncludeDate = chkbxDateFound.Checked;
			Settings.OnlyNewResults = chkbxNewResultsOnly.Checked;

			Settings.ScheduleDaysOfWeek[DayOfWeek.Sunday] = chkbxSunday.Checked;
			Settings.ScheduleDaysOfWeek[DayOfWeek.Monday] = chkbxMonday.Checked;
			Settings.ScheduleDaysOfWeek[DayOfWeek.Tuesday] = chkbxTuesday.Checked;
			Settings.ScheduleDaysOfWeek[DayOfWeek.Wednesday] = chkbxWednesday.Checked;
			Settings.ScheduleDaysOfWeek[DayOfWeek.Thursday] = chkbxThursday.Checked;
			Settings.ScheduleDaysOfWeek[DayOfWeek.Friday] = chkbxFriday.Checked;
			Settings.ScheduleDaysOfWeek[DayOfWeek.Saturday] = chkbxSaturday.Checked;

			DialogResult = DialogResult.OK;
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

		private void btnCancel_Click(object sender, EventArgs e) {
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}

}