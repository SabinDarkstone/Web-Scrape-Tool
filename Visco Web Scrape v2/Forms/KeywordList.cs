using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Visco_Web_Scrape_v2.Properties;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Scripts.Helpers;
using Visco_Web_Scrape_v2.Search.Items;

namespace Visco_Web_Scrape_v2.Forms {

	public partial class KeywordList : Form {

		public HashSet<Keyword> Whitelist { get; set; }
		public HashSet<Keyword> Blacklist { get; set; }

		private readonly List<Keyword> whitelistEnabled;
		private readonly List<Keyword> whitelistDisabled;
		private readonly List<Keyword> blacklistEnabled;
		private readonly List<Keyword> blacklistDisabled;

		public KeywordList(Configuration configuration) {
			InitializeComponent();

			Whitelist = configuration.PageWords ?? new HashSet<Keyword>();
			Blacklist = configuration.UrlWords ?? new HashSet<Keyword>();

			whitelistEnabled = new List<Keyword>();
			whitelistDisabled = new List<Keyword>();
			blacklistEnabled = new List<Keyword>();
			blacklistDisabled = new List<Keyword>();
		}

		private void FillLists() {
			whitelistEnabled.Clear();
			whitelistDisabled.Clear();
			blacklistEnabled.Clear();
			blacklistDisabled.Clear();

			if (Whitelist != null && Whitelist.Count > 0) {
				foreach (var keyword in Whitelist) {
					if (keyword.IsEnabled) {
						whitelistEnabled.Add(keyword);
					} else {
						whitelistDisabled.Add(keyword);
					}
				}
			}

			if (Blacklist != null && Blacklist.Count > 0) {
				foreach (var keyword in Blacklist) {
					if (keyword.IsEnabled) {
						blacklistEnabled.Add(keyword);
					} else {
						blacklistDisabled.Add(keyword);
					}
				}
			}
		}

		private void PopulateForm() {
			chklistboxKeywords.Items.Clear();
			chklistboxBlacklist.Items.Clear();

			foreach (var keyword in whitelistEnabled) {
				chklistboxKeywords.Items.Add(keyword, true);
			}
			foreach (var keyword in whitelistDisabled) {
				chklistboxKeywords.Items.Add(keyword, false);
			}

			foreach (var keyword in blacklistEnabled) {
				chklistboxBlacklist.Items.Add(keyword, true);
			}
			foreach (var keyword in blacklistDisabled) {
				chklistboxBlacklist.Items.Add(keyword, false);
			}
		}

		private void UpdateForm() {
			FillLists();
			PopulateForm();

			txtKeywordText.Text = "";
			txtUrlWord.Text = "";
		}

		private void KeywordList_Shown(object sender, System.EventArgs e) {
			LogHelper.Debug("Loading keyword lists");
			FillLists();
			LogHelper.Debug("Populating form");
			PopulateForm();
		}

		private void btnSaveKeyword_Click(object sender, System.EventArgs e) {
			var myKeyword = new Keyword(txtKeywordText.Text);

			if (Whitelist != null && Whitelist.Count > 0 && Whitelist.Any(i => i.Text == myKeyword.Text)) return;
			Debug.Assert(Whitelist != null, "Whitelist != null");
			Whitelist.Add(myKeyword);

			UpdateForm();
		}

		private void chklistboxKeywords_ItemCheck(object sender, ItemCheckEventArgs e) {
			var selectedKeyword = chklistboxKeywords.Items[e.Index] as Keyword;
			Whitelist.Remove(selectedKeyword);

			if (selectedKeyword == null) return;
			selectedKeyword.IsEnabled = e.NewValue == CheckState.Checked;
			Whitelist.Add(selectedKeyword);
		}

		private void btnRemoveKeyword_Click(object sender, System.EventArgs e) {
			if (chklistboxKeywords.SelectedIndex == -1) return;

			var selectedKeyword = chklistboxKeywords.SelectedItem as Keyword;
			if (!Whitelist.Contains(selectedKeyword)) return;

			var dialog = MessageBox.Show(Resources.ConfirmDeleteKeyword + selectedKeyword, Resources.ConfirmationRequired, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dialog == DialogResult.Yes) {
				Whitelist.Remove(selectedKeyword);
				UpdateForm();
			}
		}

		private void chklistboxKeywords_SelectedIndexChanged(object sender, System.EventArgs e) {
			if (chklistboxKeywords.SelectedIndex == -1) return;
			var selectedKeyword = chklistboxKeywords.SelectedItem as Keyword;

			Debug.Assert(selectedKeyword != null, "selectedKeyword != null");
			txtKeywordText.Text = selectedKeyword.Text;
		}

		private void btnCancel_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void btnAcceptChanges_Click(object sender, System.EventArgs e) {
			Hide();
			DialogResult = DialogResult.OK;
		}

		private void btnSaveWord_Click(object sender, System.EventArgs e) {
			var myKeyword = new Keyword(txtUrlWord.Text);

			if (Blacklist != null && Blacklist.Count > 0 && Blacklist.Any(i => i.Text == myKeyword.Text)) return;
			Debug.Assert(Blacklist != null, "Whitelist != null");
			Blacklist.Add(myKeyword);

			UpdateForm();
		}

		private void btnRemoveWord_Click(object sender, System.EventArgs e) {
			if (chklistboxBlacklist.SelectedIndex == -1) return;

			var selectedKeyword = chklistboxBlacklist.SelectedItem as Keyword;
			if (!Blacklist.Contains(selectedKeyword)) return;

			var dialog = MessageBox.Show(Resources.ConfirmDeleteKeyword + selectedKeyword, Resources.ConfirmationRequired, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dialog == DialogResult.Yes) {
				Blacklist.Remove(selectedKeyword);
				UpdateForm();
			}
		}

		private void chklistboxBlacklist_SelectedIndexChanged(object sender, System.EventArgs e) {
			if (chklistboxBlacklist.SelectedIndex == -1) return;
			var selectedKeyword = chklistboxBlacklist.SelectedItem as Keyword;

			Debug.Assert(selectedKeyword != null, "selectedKeyword != null");
			txtUrlWord.Text = selectedKeyword.Text;
		}

		private void chklistboxBlacklist_ItemCheck(object sender, ItemCheckEventArgs e) {
			var selectedKeyword = chklistboxBlacklist.Items[e.Index] as Keyword;
			Blacklist.Remove(selectedKeyword);

			if (selectedKeyword == null) return;
			selectedKeyword.IsEnabled = e.NewValue == CheckState.Checked;
			Blacklist.Add(selectedKeyword);
		}
	}

}