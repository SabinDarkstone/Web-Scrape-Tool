using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Visco_Web_Scrape_v2.Properties;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Search.Items;

namespace Visco_Web_Scrape_v2.Forms {

	public partial class KeywordList : Form {

		public HashSet<Keyword> Whitelist { get; set; }
		public HashSet<Keyword> Blacklist { get; set; }

		public KeywordList(Configuration configuration) {
			InitializeComponent();

			Whitelist = configuration.PageWords ?? new HashSet<Keyword>();
			Blacklist = configuration.UrlWords ?? new HashSet<Keyword>();
		}

		private void PopulateForm() {
			chklistboxKeywords.Items.Clear();
			chklistboxBlacklist.Items.Clear();

			var sortedWhitelist = Whitelist.ToList();
			sortedWhitelist.Sort();
			foreach (var keyword in sortedWhitelist) {
				chklistboxKeywords.Items.Add(keyword, keyword.IsEnabled);
			}

			var sortedBlacklist = Blacklist.ToList();
			sortedBlacklist.Sort();
			foreach (var keyword in sortedBlacklist) {
				chklistboxBlacklist.Items.Add(keyword, keyword.IsEnabled);
			}
		}

		private void UpdateForm() {
			PopulateForm();

			txtKeywordText.Text = "";
			txtUrlWord.Text = "";
		}

		private void KeywordList_Shown(object sender, System.EventArgs e) {
			UpdateForm();
		}

		private void btnSaveKeyword_Click(object sender, System.EventArgs e) {
			var myKeyword = new Keyword(txtKeywordText.Text);
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
			if (selectedKeyword == null) return;

			txtKeywordText.Text = selectedKeyword.ToString();
		}

		private void btnCancel_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void btnAcceptChanges_Click(object sender, System.EventArgs e) {
			Hide();
			DialogResult = DialogResult.OK;
		}

		private void btnSaveWord_Click(object sender, System.EventArgs e) {
			var myKeyword = new Keyword(txtKeywordText.Text);
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
			if (selectedKeyword == null) return;

			txtUrlWord.Text = selectedKeyword.ToString();
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