using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Visco_Web_Scrape_v2.Scripts;
using Excel = Microsoft.Office.Interop.Excel;

namespace Visco_Web_Scrape_v2.Forms {

	public partial class ResultViewer : Form {

		private readonly Configuration config;

		public ResultViewer(Configuration configuration) {
			InitializeComponent();
			config = configuration;
		}

		private void ResultViewer_Shown(object sender, EventArgs e) {
			ExportToFile();
		}

		// TODO: Change how the grants are outputted so that there is a website per line in the first column with the keywords found on the page in the second column
		private void ExportToFile() {
			// Initialize progress bar
			var pageCount = config.Results.SelectMany(domain => domain.ResultList).Count();
			progressbar.Maximum = pageCount;

			// Indicate the program is saving the file
			btnStatusClose.Text = "Saving...";
			btnStatusClose.Enabled = false;

			switch (config.ExportMethod) {
				case Configuration.ExportType.Excel:
					ExportToExcel();
					break;

				case Configuration.ExportType.Plain:
					ExportToPlainText();
					break;

				case Configuration.ExportType.Xml:
					ExportToXml();
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void ExportToExcel() {
			// Initialize excel interop and start application
			var excel = new Excel.Application { Visible = false };

			// Get a new workbook
			Excel._Workbook workbook = excel.Workbooks.Add(Missing.Value);

			// Go through each website and write to the file
			var results = config.Results;
			foreach (var entry in results) {
				// Add a new worksheet for each domain
				Excel._Worksheet sheet = workbook.Worksheets.Add();
				sheet.Name = entry.Name;
				var currentRow = 1;

				// Create headers
				sheet.Cells[currentRow, 1] = "Website Url";
				sheet.Cells[currentRow, 2] = "Keywords";

				// Go through the rows and add information
				foreach (var website in entry.ResultList) {
					progressbar.Value++;
					if (website.IsNew == false)
						continue;

					currentRow++;
					var range = sheet.Cells[currentRow, 1] as Excel.Range;
					range.Hyperlinks.Add(range, website.Url, Missing.Value, "Click to open", website.Url);
					var keywords = website.Keywords.Aggregate("", (current, keyword) => current + (keyword.Text + ", "));
					var substring = keywords.Substring(0, keywords.Length - 2);
					sheet.Cells[currentRow, 2] = substring;
				}

				var aRange = sheet.UsedRange;
				aRange.Columns.AutoFit();
			}

			btnStatusClose.Text = "Close";
			btnStatusClose.Enabled = true;

			excel.Visible = true;
		}

		private void ExportToPlainText() {
			throw new NotImplementedException("Plain text file export");
		}

		private void ExportToXml() {
			throw new NotImplementedException("Xml file export");
		}

		private void btnStatusClose_Click(object sender, EventArgs e) {
			Close();
		}
	}
}
