using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CsQuery.ExtensionMethods.Internal;
using Visco_Web_Scrape_v2.Properties;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Scripts.Helpers;
using Visco_Web_Scrape_v2.Search.Items;

namespace Visco_Web_Scrape_v2.Forms {

	public partial class WebsiteList : Form {

		public HashSet<Website> CurrentWebsites { get; private set; }
		public Configuration Config { get; set; }

		private readonly List<Website> checkedItems;

		public WebsiteList(Configuration configuration) {
			InitializeComponent();

			Config = configuration;

			CurrentWebsites = new HashSet<Website>();
			CurrentWebsites.AddRange(Config.Websites);
			checkedItems = new List<Website>();
			if (CurrentWebsites == null || CurrentWebsites.Count == 0) return;
			checkedItems.AddRange(CurrentWebsites.ToList().FindAll(i => i.IsEnabled));
		}

		private void UpdateListbox() {
			checkedItems.Clear();
			checkedItems.AddRange(CurrentWebsites.ToList().FindAll(i => i.IsEnabled));

			chklistboxWebsites.Items.Clear();
			foreach (var checkedWebsite in checkedItems) {
				chklistboxWebsites.Items.Add(checkedWebsite, true);
			}
			foreach (var uncheckedWebsite in CurrentWebsites.ToList().FindAll(i => i.IsEnabled == false)) {
				chklistboxWebsites.Items.Add(uncheckedWebsite, false);
			}
		}

		private void ClearTextboxes() {
			txtWebsiteName.Text = "";
			txtWebsiteUrl.Text = "";

			radioGrantSource.Checked = false;
			radioOtherSource.Checked = false;
		}

		private void btnAccept_Click(object sender, EventArgs e) {
			foreach (Website checkedWebsite in chklistboxWebsites.CheckedItems) {
				LogHelper.Debug(checkedWebsite.Name);
				CurrentWebsites.First(i => i.Url.Equals(checkedWebsite.Url)).IsEnabled = true;
			}

			foreach (var website in CurrentWebsites) {
				LogHelper.Debug(website.Name + " " + website.IsEnabled);
			}

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
			ClearTextboxes();
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
				ClearTextboxes();
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
			LogHelper.Debug("Selected index: " + e.Index);
			var websiteToChange = chklistboxWebsites.Items[e.Index] as Website;

			CurrentWebsites.Remove(websiteToChange);

			if (websiteToChange == null) return;
			websiteToChange.IsEnabled = e.NewValue == CheckState.Checked;
			CurrentWebsites.Add(websiteToChange);
			LogHelper.Debug(websiteToChange.Name + " " + websiteToChange.IsEnabled);
		}

		private void chklistboxWebsites_Validated(object sender, EventArgs e) {
			
		}
	}

}