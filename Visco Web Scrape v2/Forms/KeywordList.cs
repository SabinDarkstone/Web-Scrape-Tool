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

	public partial class KeywordList : Form {
		public HashSet<Keyword> CurrentKeywords { get; private set; }
		public Configuration Config { get; set; }

		private readonly List<Keyword> checkedItems;

		public KeywordList(Configuration configuration) {
			InitializeComponent();

			Config = configuration;

			CurrentKeywords = new HashSet<Keyword>();
			CurrentKeywords.AddRange(Config.Keywords);
			checkedItems = new List<Keyword>();
			if (CurrentKeywords == null || CurrentKeywords.Count == 0)
				return;
			checkedItems.AddRange(CurrentKeywords.ToList().FindAll(i => i.IsEnabled));
		}

		private void UpdateListbox() {
			checkedItems.Clear();
			checkedItems.AddRange(CurrentKeywords.ToList().FindAll(i => i.IsEnabled));

			chklistboxKeywords.Items.Clear();
			foreach (var checkedKeyword in checkedItems) {
				chklistboxKeywords.Items.Add(checkedKeyword, true);
			}
			foreach (var uncheckedKeyword in CurrentKeywords.ToList().FindAll(i => i.IsEnabled == false)) {
				chklistboxKeywords.Items.Add(uncheckedKeyword, false);
			}
		}

		private void ClearTextboxes() {
			txtKeywordText.Text = "";
		}

		private void btnAccept_Click(object sender, EventArgs e) {
			foreach (Keyword checkedKeyword in chklistboxKeywords.CheckedItems) {
				LogHelper.Debug(checkedKeyword.Text);
				CurrentKeywords.First(i => i.Text.Equals(checkedKeyword.Text)).IsEnabled = true;
			}

			foreach (var keyword in CurrentKeywords) {
				LogHelper.Debug(keyword.Text + " " + keyword.IsEnabled);
			}

			DialogResult = DialogResult.OK;
			Hide();
		}

		private void btnCancel_Click(object sender, EventArgs e) {
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void KeywordList_Shown(object sender, EventArgs e) {
			if (Config.Websites != null) {
				CurrentKeywords = Config.Keywords;
				UpdateListbox();
			} else {
				CurrentKeywords = new HashSet<Keyword>();
			}
		}

		private void btnSave_Click(object sender, EventArgs e) {
			var myKeyword = new Keyword(txtKeywordText.Text);

			CurrentKeywords.Add(myKeyword);
			UpdateListbox();
			ClearTextboxes();
		}

		private void btnRemove_Click(object sender, EventArgs e) {
			if (chklistboxKeywords.SelectedIndex == -1)
				return;

			var selectedKeyword = chklistboxKeywords.SelectedItem as Keyword;
			if (!CurrentKeywords.Contains(selectedKeyword))
				return;

			var dialog = MessageBox.Show(Resources.ConfirmDeleteKeyword + selectedKeyword, Resources.ConfirmationRequired,
				MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dialog == DialogResult.Yes) {
				CurrentKeywords.Remove(selectedKeyword);
				UpdateListbox();
				ClearTextboxes();
			}
		}

		private void chklistboxKeywords_SelectedIndexChanged(object sender, EventArgs e) {
			if (chklistboxKeywords.SelectedIndex == -1)
				return;

			var selectedKeyword = chklistboxKeywords.SelectedItem as Keyword;
			if (selectedKeyword == null)
				return;
			txtKeywordText.Text = selectedKeyword.Text;
		}

		private void chklistboxKeywords_ItemCheck(object sender, ItemCheckEventArgs e) {
			LogHelper.Debug("Selected index: " + e.Index);
			var keywordToChange = chklistboxKeywords.Items[e.Index] as Keyword;

			CurrentKeywords.Remove(keywordToChange);

			if (keywordToChange == null)
				return;
			keywordToChange.IsEnabled = e.NewValue == CheckState.Checked;
			CurrentKeywords.Add(keywordToChange);
			LogHelper.Debug(keywordToChange.Text + " " + keywordToChange.IsEnabled);
		}
	}

}