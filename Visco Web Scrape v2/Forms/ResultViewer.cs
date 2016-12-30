﻿using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Scripts.Helpers;
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
			var pageCount = config.LastCrawl.Results.SelectMany(domain => domain.ResultList).Count();
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
			var results = config.LastCrawl.Results;
			int currentRow = 1;
			for (var index = results.Count - 1; index >= 0; index--) {
				var entry = results[index];

				// Add a new worksheet for each domain
				Excel._Worksheet sheet = workbook.Worksheets.Add();
				sheet.Name = entry.Name;

				// Create headers
				sheet.Cells[currentRow, 1] = "Website Url";
				sheet.Cells[currentRow, 2] = "Keywords";
				if (config.IncludeDate) {
					sheet.Cells[currentRow, 3] = "Date Discovered";
				}

				// TODO: Make this into a separate excel file
				if (config.EnableUrlAnalysis) {
					// Create additional headers
					sheet.Cells[currentRow, 5] = "Url Part";
					sheet.Cells[currentRow, 6] = "With Keyword";
					sheet.Cells[currentRow, 7] = "Without Keyword";

					// Go through URL parts and add information to sheet
					foreach (var part in entry.UrlDetails.WithKeyword) {
						currentRow++;
						sheet.Cells[currentRow, 4] = part.Key;
						sheet.Cells[currentRow, 5] = part.Value;

						try {
							var val = entry.UrlDetails.WithoutKeyword[part.Key];
							sheet.Cells[currentRow, 6] = val;
						} catch (Exception ex) {
							LogHelper.Warn(ex.Message);
						}
					}
				}

				// Go through the rows and add information
				currentRow = 1;
				foreach (var website in entry.ResultList) {
					progressbar.Value++;
					currentRow++;

					if (config.OnlyNewResults && !website.DateFound.Equals(DateTime.Today)) continue;

					var range = sheet.Cells[currentRow, 1] as Excel.Range;
					range.Hyperlinks.Add(range, website.Url, Missing.Value, "Click to open", website.Url);
					var keywords = website.Keywords.Aggregate("", (current, keyword) => current + (keyword.Text + ", "));
					var substring = keywords.Substring(0, keywords.Length - 2);
					sheet.Cells[currentRow, 2] = substring;

					if (config.IncludeDate) {
						var date = "";
						date += website.DateFound.Month + "/";
						date += website.DateFound.Day + "/";
						date += website.DateFound.Year;
						sheet.Cells[currentRow, 3] = date;
					}
				}

				var aRange = sheet.UsedRange;
				aRange.Columns.AutoFit();
			}

			// Write text tocells
			Excel._Worksheet titleSheet = workbook.Worksheets.Add(Missing.Value);
			titleSheet.Cells[1, 1] = "Visco Lighting Grant Search Results";
			titleSheet.Cells[2, 1] = "Auto-generated by Visco Grant Search Tool";
			titleSheet.Cells[4, 1] = "Run Date";
			titleSheet.Cells[4, 2] = config.LastCrawl.Date.ToShortDateString();
			titleSheet.Cells[5, 1] = "Completion Status";
			titleSheet.Cells[5, 2] = config.LastCrawl.CompletionStatus;

			titleSheet.Cells[7, 1] = "Websites Searched";
			currentRow = 7;
			foreach (var website in config.Websites) {
				currentRow++;
				titleSheet.Cells[currentRow, 1] = website.Name;
//				titleSheet.Cells[currentRow, 2] = website.Url;
			}

			currentRow += 2;
			titleSheet.Cells[currentRow, 1] = "Keywords Used";
			titleSheet.Range["A" + currentRow].Font.Bold = true;
			foreach (var keyword in config.Keywords) {
				currentRow++;
				titleSheet.Cells[currentRow, 1] = keyword.Text;
			}

			titleSheet.Range["A1"].Font.Size = "28";
			titleSheet.Range["A7"].Font.Bold = true;

			titleSheet.Range["A1:H1"].Merge();
			titleSheet.Range["A2:H2"].Merge();

			titleSheet.Range["A1:A200"].Columns.AutoFit();
			titleSheet.Range["B1:B200"].Columns.AutoFit();

			titleSheet.Name = "Summary";

			((Excel.Worksheet) excel.ActiveWorkbook.Sheets["Sheet1"]).Delete();

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
