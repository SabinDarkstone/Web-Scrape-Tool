using System;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using ViscoWindowsApp.Things;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ViscoWindowsApp {

	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page {

		// TODO: Create object to save/load settings from local file

		public MainPage() {
			this.InitializeComponent();

			ApplicationView.PreferredLaunchViewSize = new Size(800, 600);
			ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
			ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(800, 600));
			ApplicationView.GetForCurrentView().TryResizeView(new Size(800, 600));

			LoadSettings();
		}

		private void LoadSettings() {
			// TODO: Write method to load settings from local file
		}

	private async void btnAddWebsite_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
			var url = txtWebsiteUrl.Text;
			var name = txtWebsiteName.Text;
			var toCrawl = chkboxToCrawlPage.IsChecked;

			if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(name)) {
				var errorDialog = new ErrorDialog("Error",
					"Please make sure both a website name and url are entered before adding to the list.");
				await errorDialog.ShowAsync();
			} else {
				listboxWebsites.Items.Add(new Website(name, url, toCrawl));
				txtWebsiteName.Text = "";
				txtWebsiteUrl.Text = "";
			}
		}

		private async void btnRemoveWebsite_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
			var selectedWebsites = listboxWebsites.SelectedItems;

			if (selectedWebsites.Count == 0) {
				var errorDialog = new ErrorDialog("Error", "No websites were selected to remove.");
				await errorDialog.ShowAsync();
			}

			if (selectedWebsites.Count > 1) {
				var errorDialog = new ErrorDialog("Error", "Please only selected one website at a time to remove.");
				await errorDialog.ShowAsync();
			}

			if (selectedWebsites.Count == 1) {
				var website = selectedWebsites[0];
				listboxWebsites.Items.Remove(website);
				txtWebsiteName.Text = "";
				txtWebsiteUrl.Text = "";
			}
		}

		private void listboxWebsites_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			if (listboxWebsites.SelectedItems.Count == 1) {
				var selectedWebsite = listboxWebsites.SelectedItems[0] as Website;
				txtWebsiteName.Text = selectedWebsite.Name;
				txtWebsiteUrl.Text = selectedWebsite.Url;
				chkboxToCrawlPage.IsChecked = selectedWebsite.DoCrawl;
			}
		}

		private async void btnModifyWebsite_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
			var selectedWebsites = listboxWebsites.SelectedItems;
			if (selectedWebsites.Count == 0) {
				var errorDialog = new ErrorDialog("Error", "No websites were selected to edit.");
				await errorDialog.ShowAsync();
			}

			if (selectedWebsites.Count > 1) {
				var errorDialog = new ErrorDialog("Error", "Please only selected one website at a time to edit.");
				await errorDialog.ShowAsync();
			}

			if (selectedWebsites.Count == 1) {
				var url = txtWebsiteUrl.Text;
				var name = txtWebsiteName.Text;
				var toCrawl = chkboxToCrawlPage.IsChecked;

				var oldWebsite = selectedWebsites[0] as Website;
				var newWebsite = new Website(name, url, toCrawl);

				if (oldWebsite == newWebsite) {
					var errorDialog = new ErrorDialog("Error", "There is no difference between the current and the new website.");
					await errorDialog.ShowAsync();
				} else {
					listboxWebsites.Items.Remove(oldWebsite);
					listboxWebsites.Items.Add(newWebsite);
					txtWebsiteName.Text = "";
					txtWebsiteUrl.Text = "";
				}
			}
		}

		private void btnStartCrawl_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
			btnCancel.IsEnabled = true;
			btnStartCrawl.IsEnabled = false;
			progressRing.IsActive = true;

			// TODO: Start crawl
		}

		private void btnCancel_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
			btnCancel.IsEnabled = false;
			btnStartCrawl.IsEnabled = true;
			progressRing.IsActive = false;

			// TODO: Cancel crawl
		}
	}

}