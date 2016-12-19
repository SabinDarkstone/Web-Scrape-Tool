using System;
using System.Windows.Forms;
using Visco_Web_Scrape.Helpers;
using Visco_Web_Scrape.Objects;

namespace Visco_Web_Scrape.Forms {

	public partial class ModifyWebsiteList : Form {

		public Settings MySettings { get; }

		public ModifyWebsiteList(Settings settings) {
			InitializeComponent();

			MySettings = settings;
		}

		private void ModifyWebsiteList_Load(object sender, System.EventArgs e) {
			if (MySettings != null) {
				var websiteList = MySettings.SavedWebsites;
				if (websiteList == null || websiteList.Count == 0) {
					return;
				}

				foreach (var website in MySettings.SavedWebsites) {
					listboxWebsites.Items.Add(website);
				}
			}
		}

		private void btnAdd_Click(object sender, System.EventArgs e) {
			AddWebsite(txtWebsiteName.Text, txtWebsiteUrl.Text);
		}

		private void AddWebsite(string name, string url) {
			var newWebsite = new Website {
				Name = name,
				Url = url
			};

			if (listboxWebsites.Items.Contains(newWebsite)) return;

			listboxWebsites.Items.Add(newWebsite);
			MySettings.SavedWebsites.Add(newWebsite);
		}

		private void ModifyWebsiteList_FormClosing(object sender, FormClosingEventArgs e) {
			FileHelper.WriteSettingsToFile("ViscoGrantSettings.bin", MySettings);
		}

		private void listboxWebsites_SelectedIndexChanged(object sender, System.EventArgs e) {
			if (listboxWebsites.SelectedIndex == -1) return;

			var selectedWebsite = listboxWebsites.SelectedItem as Website;

			txtWebsiteName.Text = selectedWebsite.Name;
			txtWebsiteUrl.Text = selectedWebsite.Url;
		}

		private void btnRemove_Click(object sender, System.EventArgs e) {
			if (listboxWebsites.SelectedIndex == -1) return;

			var selectedWebsite = listboxWebsites.SelectedItem as Website;
			listboxWebsites.Items.Remove(selectedWebsite);
			MySettings.SavedWebsites.Remove(selectedWebsite);
		}

		private void btnModify_Click(object sender, System.EventArgs e) {
			throw new NotImplementedException("Not yet implemented.  Please remove and re-add instead.");
		}
	}

}