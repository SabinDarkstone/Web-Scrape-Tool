using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Visco_Web_Scrape.Helpers;
using Visco_Web_Scrape.Objects;
using Visco_Web_Scrape.Operations;

namespace Visco_Web_Scrape.Forms {

	public partial class MainForm : Form {

		private Settings settings;

		public MainForm() {
			InitializeComponent();
		}

		private void btnModifyWebsites_Click(object sender, EventArgs e) {
			var dialog = new ModifyWebsiteList(settings);
			dialog.ShowDialog();
			settings = dialog.MySettings;
			UpdateWebsiteListbox();
		}

		private void UpdateWebsiteListbox() {
			if (settings == null) return;
			var websiteList = settings.SavedWebsites;

			listboxWebsiteList.Items.Clear();
			foreach (var website in websiteList) {
				listboxWebsiteList.Items.Add(website);
			}
		}

		private void MainForm_Load(object sender, EventArgs e) {
			settings = FileHelper.LoadSettingsFromFile("ViscoGrantSettings.bin") ?? new Settings();

			/* UNDONE: Temporary fix to resolve settings file verstion conflicts
			var websites = settings.SavedWebsites;
			settings = new Settings {SavedWebsites = websites};
			FileHelper.WriteSettingsToFile("ViscoGrantSettings.bin", settings);
			*/

			UpdateWebsiteListbox();
		}

		private void btnRunScrape_Click(object sender, EventArgs e) {
			var websiteList = listboxWebsiteList.SelectedItems.Cast<Website>().ToList();
			var searchDialog = new SearchProgress(websiteList, settings);
			searchDialog.Show();
		}

		private void btnEditFilters_Click(object sender, EventArgs e) {
			var dialog = new FilterManager(settings);

			// HACK: Object disposal exception when choosing not to create new settings file
			try {
				dialog.ShowDialog(this);
			} catch (Exception ex) {
				LogHelper.Warning(ex.Message);
			} finally {
				if (dialog.DialogResult == DialogResult.OK) {
					// Get the settings back from filter manager and saves them to the file
					settings = FileHelper.CycleSettingsFile("ViscoGrantSettings.bin", dialog.MySettings);
				}

				dialog.Close();
			}
		}

		private void MainForm_Shown(object sender, EventArgs e) {

		}
	}

}