using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CsQuery.ExtensionMethods.Internal;
using Visco_Web_Scrape_v2.Properties;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Search.Items;

namespace Visco_Web_Scrape_v2.Forms {

	public partial class WebsiteList : Form {

		public HashSet<Website> CurrentWebsites { get; private set; }
		public Configuration Config { get; set; }

		public WebsiteList(Configuration configuration) {
			InitializeComponent();

			Config = configuration;

			CurrentWebsites = new HashSet<Website>();
			CurrentWebsites.AddRange(Config.Websites);
		}

		private void UpdateListbox() {
			chklistboxWebsites.Items.Clear();

			foreach (var website in CurrentWebsites.ToList()) {
				chklistboxWebsites.Items.Add(website, website.IsEnabled);
			}
		}

		private void ClearForm() {
			txtWebsiteName.Text = "";
			txtWebsiteUrl.Text = "";

			radioGrantSource.Checked = false;
			radioOtherSource.Checked = false;
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
				CurrentWebsites = new HashSet<Website>();
			}
		}

		private void btnSaveWebsite_Click(object sender, EventArgs e) {
			Website myWebsite;
			if (radioGrantSource.Checked) {
				myWebsite = new Website(txtWebsiteName.Text, txtWebsiteUrl.Text, true);
			} else if (radioOtherSource.Checked) {
				myWebsite = new Website(txtWebsiteName.Text, txtWebsiteUrl.Text, false);
			} else {
				MessageBox.Show(Resources.ChooseSourceTypeText, Resources.Error, MessageBoxButtons.OK);
				return;
			}

			CurrentWebsites.Add(myWebsite);
			UpdateListbox();
			ClearForm();
		}

		private void btnRemoveWebsite_Click(object sender, EventArgs e) {
			if (chklistboxWebsites.SelectedIndex == -1) return;

			var selectedWebsite = chklistboxWebsites.SelectedItem as Website;
			if (!CurrentWebsites.Contains(selectedWebsite)) return;

			var dialog = MessageBox.Show(Resources.ConfirmDeleteWebsite + selectedWebsite, Resources.ConfirmationRequired,
				MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dialog == DialogResult.Yes) {
				CurrentWebsites.Remove(selectedWebsite);
				UpdateListbox();
				ClearForm();
			}
		}

		private void chklistboxWebsites_SelectedIndexChanged(object sender, EventArgs e) {
			if (chklistboxWebsites.SelectedIndex == -1) return;

			var selectedWebsite = chklistboxWebsites.SelectedItem as Website;
			if (selectedWebsite == null) return;
			txtWebsiteName.Text = selectedWebsite.Name;
			txtWebsiteUrl.Text = selectedWebsite.Url;

			if (selectedWebsite.IsGrantSource) {
				radioGrantSource.Checked = true;
				radioOtherSource.Checked = false;
			} else {
				radioGrantSource.Checked = false;
				radioOtherSource.Checked = true;
			}
		}

		private void chklistboxWebsites_ItemCheck(object sender, ItemCheckEventArgs e) {
			var websiteToChange = chklistboxWebsites.Items[e.Index] as Website;

			CurrentWebsites.Remove(websiteToChange);

			if (websiteToChange == null) return;
			websiteToChange.IsEnabled = e.NewValue == CheckState.Checked;
			CurrentWebsites.Add(websiteToChange);
		}

		private void btnCheckAll_Click(object sender, EventArgs e) {
			ChangeAll(true);
		}

		private void btnUncheckAll_Click(object sender, EventArgs e) {
			ChangeAll(false);
		}

		private void ChangeAll(bool newValue) {
			foreach (var website in CurrentWebsites) {
				website.IsEnabled = newValue;
			}

			UpdateListbox();
			ClearForm();
		}
	}

}