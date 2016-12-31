using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Visco_Web_Scrape_v2.Properties;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Search.Items;

namespace Visco_Web_Scrape_v2.Forms {
	public partial class WebsiteList : Form {

		public List<Website> CurrentWebsites { get; private set; }
		public Configuration Config { get; set; }

		public WebsiteList(Configuration configuration) {
			InitializeComponent();

			Config = configuration;
		}

		private void UpdateListbox() {
			CurrentWebsites.Sort();
			listboxWebsites.Items.Clear();
			foreach (var website in CurrentWebsites) {
				listboxWebsites.Items.Add(website);
			}
		}

		private void ClearTextboxes() {
			txtWebsiteName.Text = "";
			txtWebsiteUrl.Text = "";
		}

		private void btnAccept_Click(object sender, EventArgs e) {
			DialogResult = DialogResult.OK;
			Hide();
		}

		private void btnCancel_Click(object sender, EventArgs e) {
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void WebsiteList_Shown(object sender, EventArgs e) {
			if (Config.Websites != null) {
				CurrentWebsites = Config.Websites;
				UpdateListbox();
			} else {
				CurrentWebsites = new List<Website>();
			}
		}

		private void btnSaveWebsite_Click(object sender, EventArgs e) {
			var myWebsite = new Website(txtWebsiteName.Text, txtWebsiteUrl.Text);
			var selectedWebsite = listboxWebsites.SelectedItem as Website;

			// Check to make sure the recipient does not already exist
			if (selectedWebsite != null && (selectedWebsite.Name.Equals(txtWebsiteName.Text) ||
				selectedWebsite.Url.Equals(txtWebsiteUrl.Text))) {
				CurrentWebsites.Remove(selectedWebsite);
				CurrentWebsites.Add(myWebsite);
			} else {
				CurrentWebsites.Add(myWebsite);
			}

			UpdateListbox();
			ClearTextboxes();
		}

		private void btnRemoveWebsite_Click(object sender, EventArgs e) {
			// Make sure something is selected
			if (listboxWebsites.SelectedIndex != -1) {
				var selectedWebsite = listboxWebsites.SelectedItem as Website;
				// Check if the user really wants to remove the website
				var dialog =
					MessageBox.Show(Resources.ConfirmDeleteWebsite + selectedWebsite,
						Resources.ConfirmationRequired, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialog == DialogResult.Yes) {
					CurrentWebsites.Remove(selectedWebsite);
					UpdateListbox();
					ClearTextboxes();
				}
			}
		}

		private void listboxWebsites_SelectedIndexChanged(object sender, EventArgs e) {
			// Check to make sure the selection is not null
			if (listboxWebsites.SelectedIndex != -1) {
				var website = listboxWebsites.SelectedItem as Website;
				if (website == null) {
					MessageBox.Show(Resources.WebsiteNotFound, Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				txtWebsiteName.Text = website.Name;
				txtWebsiteUrl.Text = website.Url;
			}
		}
	}
}
