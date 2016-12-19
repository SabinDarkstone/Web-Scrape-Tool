using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Visco_Web_Scrape.Helpers;
using Visco_Web_Scrape.Objects;
using Visco_Web_Scrape.Properties;
using Settings = Visco_Web_Scrape.Objects.Settings;

namespace Visco_Web_Scrape.Forms {

	// QUESTION: Can this be cleaned up any better?
	// TODO: Implement AND/OR for step one keywords
	public partial class FilterManager : Form {

		/// <summary>
		/// Settings file carried over from the main form.  Any changes made and accepted by user will be saved here for access from the main form.
		/// </summary>
		public Settings MySettings { get; }

		private List<Keyword> currentKeywordList;

		private enum Action {
			Add,
			Remove,
			Help
		}

		public FilterManager(Settings settings) {
			MySettings = settings;

			InitializeComponent();
			InitializeManager();
		}

		/// <summary>
		/// Initialized the listboxes and loads existing keywords.
		/// </summary>
		private void InitializeManager() {
			// Assign listboxes their respective functionality
			listboxKeywordsStepOne.Tag = Keyword.KeywordType.Relevance;
			listboxKeywordsStepTwo.Tag = Keyword.KeywordType.Inclusion;
			listboxKeywordsStepThree.Tag = Keyword.KeywordType.Exclusion;

			// Check for a keyword list in the settings file
			if (CheckForKeywordList()) {
				LogHelper.Info("Filters loaded from last session.");
			} else {
				LogHelper.Error("An error occured while loading the filter from the previous session.");
			}

			UpdateListboxes();
		}

		/// <summary>
		/// Retrieves previous keywords from settings.
		/// </summary>
		/// <returns></returns>
		private bool CheckForKeywordList() {
			currentKeywordList = MySettings.MasterKeywordList;
			LogHelper.Debug("Found keyword list with " + currentKeywordList.Count + " keyword(s).");

			if (currentKeywordList == null) {
				CreateNewKeywordList();
				return false;
			}
			if (currentKeywordList.Count == 0) {
				CreateNewKeywordList();
				return false;
			}

			// Add all keywords in master list from settings file to current list in the manager
			foreach (var keyword in MySettings.MasterKeywordList) {
				AddItem(keyword);
			}
			return true; // And return true to indicate success
		}

		/// <summary>
		/// Asks the user if they want to create a new settins file when the old one is not found or improperly formatted.
		/// </summary>
		private void CreateNewKeywordList() {
			var response = MessageBox.Show(Resources.KeywordListErrorContent, Resources.KeywordListErrorTitle, MessageBoxButtons.YesNo,
				MessageBoxIcon.Error);

			if (response == DialogResult.Yes) {
				currentKeywordList = new List<Keyword>();
			} else {
				Close();
			}
		}

		/// <summary>
		/// Determines flow of code execution based on which button was clicked.
		/// </summary>
		/// <param name="step"></param>
		/// <param name="action"></param>
		private void ButtonAction(Keyword.KeywordType step, Action action) {
			var listbox = new ListBox();
			var textbox = new TextBox();

			if (step == Keyword.KeywordType.Relevance) {
				listbox = listboxKeywordsStepOne;
				textbox = txtStepOneKeyword;
			}
			if (step == Keyword.KeywordType.Inclusion) {
				listbox = listboxKeywordsStepTwo;
				textbox = txtStepTwoKeyword;
			}
			if (step == Keyword.KeywordType.Exclusion) {
				listbox = listboxKeywordsStepThree;
				textbox = txtStepThreeKeyword;
			}

			if (listbox == null || textbox == null) return;

			if (action == Action.Add) AddItem(textbox, listbox);
			if (action == Action.Remove) RemoveItem(listbox);
			if (action == Action.Help) ShowHelp(step);

			UpdateListboxes();
			UpdateTextboxes();
		}

		/// <summary>
		/// Add a keyword to the a list based on what step the keyword was added to.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="destination"></param>
		private void AddItem(TextBox source, ListBox destination) {
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (destination == null) throw new ArgumentNullException(nameof(destination));

			var keyword = new Keyword(source.Text, (Keyword.KeywordType) destination.Tag);
			if (!currentKeywordList.Contains(keyword)) {
				LogHelper.Debug("Adding \"" + keyword.Text + " \" to the " + keyword.Type + " list.");
				currentKeywordList.Add(keyword);
			}
		}

		/// <summary>
		/// Add a (previously used) keyword to the proper step.
		/// </summary>
		/// <param name="keyword"></param>
		private void AddItem(Keyword keyword) {
			if (currentKeywordList.Contains(keyword)) return;
			currentKeywordList.Add(keyword);
		}

		/// <summary>
		/// If the item exits in the current list, remove it.
		/// </summary>
		/// <param name="source"></param>
		private void RemoveItem(ListBox source) {
			if (source == null) throw new ArgumentNullException(nameof(source));

			Keyword keyword = null;
			foreach (var keywordFromList in currentKeywordList) {
				if (keywordFromList.Text.Equals(source.SelectedItem.ToString())) {
					keyword = keywordFromList;
				}
			}
			LogHelper.Debug("Removing keyword " + keyword.Text + " from " + source.Name);
			if (currentKeywordList.Contains(keyword)) {
				currentKeywordList.Remove(keyword);
			}

			string list = "";
			foreach (var item in source.Items) {
				list += item + " ";
			}
			LogHelper.Debug("Keywords remaining " + list);
		}

		/// <summary>
		/// Show a more detailed explanation of what each step is.
		/// </summary>
		/// <param name="step"></param>
		private void ShowHelp(Keyword.KeywordType step) {
			switch (step) {
				case Keyword.KeywordType.Relevance:
					MessageBox.Show(Resources.StepOneHelpContent, Resources.StepOneHelpTitle, MessageBoxButtons.OK,
						MessageBoxIcon.Information);
					break;
				case Keyword.KeywordType.Inclusion:
					MessageBox.Show(Resources.StepTwoHelpContent, Resources.StepTwoHelpTitle, MessageBoxButtons.OK,
						MessageBoxIcon.Information);
					break;
				case Keyword.KeywordType.Exclusion:
					throw new NotImplementedException();
				default:
					throw new ArgumentOutOfRangeException(nameof(step), step, null);
			}
		}

		/// <summary>
		/// Updates the listbox contents based on the current keyword list.
		/// </summary>
		private void UpdateListboxes() {
			listboxKeywordsStepOne.Items.Clear();
			listboxKeywordsStepTwo.Items.Clear();
			listboxKeywordsStepThree.Items.Clear();

			foreach (var item in currentKeywordList) {
				switch (item.Type) {
					case Keyword.KeywordType.Relevance:
						listboxKeywordsStepOne.Items.Add(item.Text);
						break;
					case Keyword.KeywordType.Inclusion:
						listboxKeywordsStepTwo.Items.Add(item.Text);
						break;
					case Keyword.KeywordType.Exclusion:
						listboxKeywordsStepThree.Items.Add(item.Text);
						break;
					default:
						throw new ArgumentOutOfRangeException("Unknown keyword type");
				}
			}
		}
		
		/// <summary>
		/// Clears out content in all textboxes
		/// </summary>
		private void UpdateTextboxes() {
			txtStepOneKeyword.Text = "";
			txtStepTwoKeyword.Text = "";
			txtStepThreeKeyword.Text = "";
		}

		/// <summary>
		/// Don't even ask.
		/// Takes the name of the button and transfer it into a computer-usable format for further code execution.
		/// Trust me, it is cleaner to do it this way.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_Click(object sender, EventArgs e) {
			var target = sender as Button;
			if (target?.Name == null) throw new ArgumentOutOfRangeException("Button has unexpected action assigned to it.");

			switch (target.Name) {
				case "btnStepOneAdd":
					ButtonAction(Keyword.KeywordType.Relevance, Action.Add);
					break;
				case "btnStepOneRemove":
					ButtonAction(Keyword.KeywordType.Relevance, Action.Remove);
					break;
				case "btnStepOneHelp":
					ButtonAction(Keyword.KeywordType.Relevance, Action.Help);
					break;
				case "btnStepTwoAdd":
					ButtonAction(Keyword.KeywordType.Inclusion, Action.Add);
					break;
				case "btnStepTwoRemove":
					ButtonAction(Keyword.KeywordType.Inclusion, Action.Remove);
					break;
				case "btnStepTwoHelp":
					ButtonAction(Keyword.KeywordType.Inclusion, Action.Help);
					break;
				case "btnStepThreeAdd":
					ButtonAction(Keyword.KeywordType.Exclusion, Action.Add);
					break;
				case "btnStepThreeRemove":
					ButtonAction(Keyword.KeywordType.Exclusion, Action.Remove);
					break;
				case "btnStepThreeHelp":
					ButtonAction(Keyword.KeywordType.Exclusion, Action.Help);
					break;
				default:
					LogHelper.Error("Button clicked has an unexpected action");
					throw new ArgumentOutOfRangeException("Button selected has an unexpected action.");
			}
		}

		/// <summary>
		/// Accept the changes to the filters and allows the settings file to be updated.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAcceptChanges_Click(object sender, EventArgs e) {
			MySettings.MasterKeywordList = currentKeywordList;
			DialogResult = DialogResult.OK;
		}

		/// <summary>
		/// Cancels any changes made to the filters, does not update settings file.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancelChanges_Click(object sender, EventArgs e) {
			DialogResult = DialogResult.Cancel;
		}
	}

}