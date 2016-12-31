using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Search;

namespace Visco_Web_Scrape_v2.Forms {

	public partial class EmailListEditor : Form {

		public List<Recipient> CurrentRecipients { get; private set; }
		public Configuration Config { get; set; }

		public EmailListEditor(Configuration configuration) {
			InitializeComponent();

			Config = configuration;
		}

		private void UpdateListbox() {
			listboxEmailAddresses.Items.Clear();

			foreach (var recipient in CurrentRecipients) {
				listboxEmailAddresses.Items.Add(recipient);
			}
		}

		private void ClearTextboxes() {
			txtRecipientName.Text = "";
			txtRecipientAddress.Text = "";
		}

		private void btnAccept_Click(object sender, EventArgs e) {
			DialogResult = DialogResult.OK;
			Hide();
		}

		private void btnCancel_Click(object sender, EventArgs e) {
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void EmailListEditor_Shown(object sender, EventArgs e) {
			if (Config.Recipients != null) {
				CurrentRecipients = Config.Recipients;
				UpdateListbox();
			} else {
				CurrentRecipients = new List<Recipient>();
			}
		}

		private void btnSaveRecipient_Click(object sender, EventArgs e) {
			var myRecipient = new Recipient(txtRecipientName.Text, txtRecipientAddress.Text);
			var alreadyExists = false;

			// Loop through recipients in current list and check to see if anything matches the current name
			foreach (var recipient in CurrentRecipients) {
				// If a match is found, remove the old one and replace it with the new one
				if (recipient.Name.Equals(myRecipient.Name)) {
					CurrentRecipients.Remove(recipient);
					CurrentRecipients.Add(myRecipient);
					alreadyExists = true;
				}
			}
			// If no match is found, add the recipient
			if (!alreadyExists) {
				CurrentRecipients.Add(myRecipient);
			}

			UpdateListbox();
			ClearTextboxes();
		}

		private void btnRemoveRecipient_Click(object sender, EventArgs e) {
			// Make sure something is selected
			if (listboxEmailAddresses.SelectedIndex != -1) {
				var selectedRecipient = listboxEmailAddresses.SelectedItem as Recipient;
				// Check if the user really wants to remove the recipient
				var dialog =
					MessageBox.Show("Are you sure you want to remove " + selectedRecipient.Name + " from the list of recipients?",
						"Confirm removal", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialog == DialogResult.Yes) {
					CurrentRecipients.Remove(selectedRecipient);
					UpdateListbox();
					ClearTextboxes();
				}
			}
		}

		private void listboxEmailAddresses_SelectedIndexChanged(object sender, EventArgs e) {
			// Check to make sure the selection is not null
			if (listboxEmailAddresses.SelectedIndex != -1) {
				var recipient = listboxEmailAddresses.SelectedItem as Recipient;
				if (recipient == null) {
					MessageBox.Show("The selected recipient does not exist in the records.", "Error", MessageBoxButtons.OK,
						MessageBoxIcon.Error);
				}
				txtRecipientName.Text = recipient.Name;
				txtRecipientAddress.Text = recipient.Address;
			}
		}
	}
}