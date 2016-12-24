using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ViscoWindowsApp {

	public sealed partial class ErrorDialog : ContentDialog {

		public ErrorDialog(string title, string text) {
			this.InitializeComponent();

			this.Title = title;
			this.lblMessage.Text = text;
		}

		private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args) {}

		private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args) {}
	}

}