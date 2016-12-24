using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Search.Items;

namespace Visco_Web_Scrape_v2.Forms {
	public partial class KeywordList : Form {

		public List<Keyword> CurrentKeywords { get; private set; }
		public Configuration Config { get; set; }

		public KeywordList(Configuration configuration) {
			InitializeComponent();

			Config = configuration;
		}

		private void UpdateListbox() {
			listboxKeywordList.Items.Clear();
			foreach (var keyword in CurrentKeywords) {
				listboxKeywordList.Items.Add(keyword);
			}
		}

		private void Clear() {
			txtKeywordText.Text = "";
			radioExclude.Checked = false;
			radioInclude.Checked = true;
		}

		private void btnSave_Click(object sender, EventArgs e) {
			var myKeyword = new Keyword(txtKeywordText.Text,
				radioInclude.Checked ? Keyword.KeywordType.Include : Keyword.KeywordType.Exclude);
			var alreadyExists = false;

			// Loop through keywords in current list and check to see if anything matches the current name
			foreach (var keyword in CurrentKeywords) {
				// If a match is found, remove the old one and replace it with the new one
				if (keyword.Text.Equals(myKeyword.Text)) {
					CurrentKeywords.Remove(keyword);
					CurrentKeywords.Add(myKeyword);
					alreadyExists = true;
				}
			}
			// If no match is found, add the website
			if (!alreadyExists) {
				CurrentKeywords.Add(myKeyword);
			}

			UpdateListbox();
			Clear();
		}

		private void btnRemove_Click(object sender, EventArgs e) {
			// Make sure something is selected
			if (listboxKeywordList.SelectedIndex != -1) {
				var selectedKeyword = listboxKeywordList.SelectedItem as Keyword;
				// Check if the user really wants to remove the keyword
				var dialog =
					MessageBox.Show("Are you sure you want to remove " + selectedKeyword.Text + " from the list of keywords?",
						"Confirm removal", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialog == DialogResult.Yes) {
					CurrentKeywords.Remove(selectedKeyword);
					UpdateListbox();
					Clear();
				}
			}
		}

		private void listboxKeywordList_SelectedIndexChanged(object sender, EventArgs e) {
			// Check to make sure the selection is not null
			if (listboxKeywordList.SelectedIndex != -1) {
				var keyword = listboxKeywordList.SelectedItem as Keyword;
				if (keyword == null) {
					MessageBox.Show("The selected keyword does not exist in the records.", "Error", MessageBoxButtons.OK,
						MessageBoxIcon.Error);
				}
				txtKeywordText.Text = keyword.Text;
				if (keyword.Type == Keyword.KeywordType.Exclude) {
					radioInclude.Checked = false;
					radioExclude.Checked = true;
				} else {
					radioInclude.Checked = true;
					radioExclude.Checked = false;
				}
			}
		}

		private void btnAccept_Click(object sender, EventArgs e) {
			DialogResult = DialogResult.OK;
			Hide();
		}

		private void btnCancel_Click(object sender, EventArgs e) {
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void KeywordList_Shown(object sender, EventArgs e) {
			if (Config.Keywords != null) {
				CurrentKeywords = Config.Keywords;
				UpdateListbox();
			} else {
				CurrentKeywords = new List<Keyword>();
			}
		}
	}
}
