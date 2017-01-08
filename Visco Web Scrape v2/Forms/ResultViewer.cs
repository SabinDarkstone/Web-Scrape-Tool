using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Visco_Web_Scrape_v2.Properties;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Scripts.Helpers;
using Visco_Web_Scrape_v2.Search.Items;
using Excel = Microsoft.Office.Interop.Excel;

namespace Visco_Web_Scrape_v2.Forms {

	public partial class ResultViewer : Form {

		public class ExportProgress {
			public int CurrentSheet { get; set; }
			public int SheetCount { get; set; }
			public int CurrentRow { get; set; }
			public int RowCount { get; set; }
		}

		private readonly Configuration config;
		private readonly CombinedResults results;
		private readonly BackgroundWorker worker;

		private Excel.Application excel;
		private Excel.Workbook grantWorkbook;

		private bool isForEmail;
		private ExportProgress progress;

		public ResultViewer(Configuration configuration, CombinedResults results) {
			InitializeComponent();

			config = configuration;
			this.results = results;

			worker = new BackgroundWorker();
		}

		private void ResultViewer_Shown(object sender, EventArgs e) {
			// Initialize the worker
			worker.WorkerSupportsCancellation = false;
			worker.WorkerReportsProgress = true;
			worker.DoWork += worker_DoWork;
			worker.ProgressChanged += worker_ProgressChanged;
			worker.RunWorkerCompleted += worker_WorkCompleted;

			ExportToFile();
		}

		private void worker_DoWork(object sender, DoWorkEventArgs args) {
			if (worker == null) {
				MessageBox.Show(Resources.ExcelWorkerIsNull, Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// Initialize excel
			excel = new Excel.Application { Visible = false };

			#region Grant Workbook

			// Initialize progress tracker with expected sheet count
			progress = new ExportProgress {
				SheetCount =
					results.AllResults.Count(
						website =>
							website.WebsiteStatus == WebsiteResults.Status.Completed ||
							website.WebsiteStatus == WebsiteResults.Status.Interrupted)
			};
			LogHelper.Debug("Website count: " + progress.SheetCount);

			// Create a new workbook
			grantWorkbook = excel.Workbooks.Add();

			// Iterate through WebsiteResults of GRANT SOURCES
			int excelRow;
			foreach (
				var website in
				results.AllResults.Where(Comparisons.SearchStarted).Where(i => i.RootWebsite.IsGrantSource)) {
				// Check if displaying all or only new results and set sheet progress bar maximum accordingly
				progress.RowCount = config.OnlyNewResults ? website.ResultList.Count(i => i.IsNewResult) : website.ResultList.Count;

				// Reset row counter
				progress.CurrentRow = 0;

				// Add a new sheet for the website
				Excel.Worksheet sheet = grantWorkbook.Worksheets.Add(Missing.Value);
				sheet.Name = website.RootWebsite.Name;

				// Create sheet title
				sheet.Cells[1, 1] = website.RootWebsite.Name;
				sheet.Cells[2, 1] = website.RootWebsite.Url;
				sheet.Range["A1"].Font.Size = 18;
				sheet.Range["A2"].Font.Size = 12;
				sheet.Range["A1:F1"].Merge();
				sheet.Range["A2:F2"].Merge();

				// If no new results are found
				if (config.OnlyNewResults && website.ResultList.Count(i => i.IsNewResult) == 0) {
					sheet.Cells[4, 1] = "No new results were found on the " + website.Counts.IgnoredPages +
						" pages with the specified keywords since the last search";
					// Sheet is finished, increment the counter and move to the next one
					worker.ReportProgress(0, ++progress.CurrentSheet);
					continue;
				}

				// If no results were found at all
				if (website.ResultList.Count == 0) {
					sheet.Cells[4, 1] = "No results were found on the " + website.Counts.SearchPages +
						" pages with the specified keywords";
					// Sheet is finished, increment the counter and move to the next one
					worker.ReportProgress(0, ++progress.CurrentSheet);
					continue;
				}

				// If only displaying new results
				if (config.OnlyNewResults && website.ResultList.Count(i => i.IsNewResult) > 0) {
					// Create sheet headers
					sheet.Cells[4, 1] = "NEW Website Url";
					sheet.Cells[4, 2] = "Keywords Found";
					if (config.IncludeDate)
						sheet.Cells[4, 3] = "Date Discovered";
				}

				// Showing all results
				if (website.ResultList.Count > 0) {
					// Create sheet headers
					sheet.Cells[4, 1] = "Website Url";
					sheet.Cells[4, 2] = "Keywords Found";
					if (config.IncludeDate)
						sheet.Cells[4, 3] = "Date Discovered";
				}

				excelRow = 4;
				foreach (var result in website.ResultList) {
					if (config.OnlyNewResults && !result.IsNewResult) {
						LogHelper.Debug(result.PageUrl + " is not new, skipping...");
						continue;
					}

					progress.CurrentRow++;

					// Go to the next blank row and fill in informations
					excelRow++;
					var rng = sheet.Cells[excelRow, 1] as Excel.Range;
					var context = "";
					var keywords = "";
					// TODO: Fix context shortening method
					if (result.Hits.Count == 1) {
						if (config.EnableStrictFilter && result.Hits.Any(i => i.IsLink)) {
							excelRow--;
							continue;
						}

						context = result.Hits.First().Context;
						keywords = result.Hits.First().Keyword.Text;
					} else if (result.Hits.Count > 1) {
						var keywordCountTracker = new Dictionary<string, int>();
						context = "<MULTIPLE RESULTS FOUND>";
						foreach (var keyword in result.Hits) {
							if (config.EnableStrictFilter && keyword.IsLink)
								continue;

							if (keywordCountTracker.ContainsKey(keyword.Keyword.Text)) {
								keywordCountTracker[keyword.Keyword.Text]++;
							} else {
								keywordCountTracker.Add(keyword.Keyword.Text, 1);
							}
						}
						foreach (var keyword in keywordCountTracker.Keys) {
							if (keywordCountTracker[keyword] > 1) {
								keywords += keyword + " (x" + keywordCountTracker[keyword] + "), ";
							} else {
								keywords += keyword + ", ";
							}
						}

						keywords = keywords.Substring(0, keywords.Length - 2);
					} else {
						context = "<ERROR>";
					}

					if (context.Length > 300) {
						context = context.Substring(0, 300);
					}
					rng.Hyperlinks.Add(rng, result.PageUrl, Missing.Value, context, result.PageUrl);
					sheet.Cells[excelRow, 2] = keywords;
					if (config.IncludeDate) {
						var date = "";
						var discoveryDate = result.DiscoveryTimeUtc.ToLocalTime();
						date += discoveryDate.Month + "/";
						date += discoveryDate.Day + "/";
						date += discoveryDate.Year;
						sheet.Cells[excelRow, 3] = date;
					}

					// Regardless of whether the result is new, increment the row counter and report progress
					worker.ReportProgress(0, progress.CurrentRow);

					Thread.Sleep(20);
				}

				// If there were results, they have been added at this point
				worker.ReportProgress(0, ++progress.CurrentSheet);
			}

			// Create summary sheet
			Excel.Worksheet summary = grantWorkbook.Worksheets.Add(Missing.Value);
			summary.Name = "Summary";

			// General information
			summary.Cells[1, 1] = "VISCO, Inc. Search Results (Grant Sources)";
			summary.Cells[2, 1] = "Auto-generated by search application";

			// Settings file information
			summary.Cells[4, 1] = "Run Date";
			summary.Cells[4, 2] = results.LastRan.ToLocalTime().ToShortTimeString() +
				" on " + results.LastRan.ToLocalTime().ToShortDateString();
			summary.Cells[5, 1] = "Results Included";
			summary.Cells[5, 2] = (config.OnlyNewResults) ? "New Only" : "All Found";
			summary.Cells[6, 1] = "Stict Filtering";
			summary.Cells[6, 2] = (config.EnableStrictFilter) ? "On" : "Off";

			// Websites searched
			summary.Cells[8, 1] = "Websites Searched";
			summary.Cells[9, 1] = "Name";
			summary.Cells[9, 2] = "Results";
			summary.Cells[9, 3] = "Searched";
			summary.Cells[9, 4] = "Ignored";
			summary.Cells[9, 5] = "Elapsed Time";
			summary.Cells[9, 6] = "Status";
			summary.Range["A8"].Font.Bold = true;
			excelRow = 9;
			foreach (var website in results.AllResults) {
				excelRow++;
				summary.Cells[excelRow, 1] = website.RootWebsite.Name;
				summary.Cells[excelRow, 3] = website.Counts.SearchPages;
				summary.Cells[excelRow, 4] = website.Counts.IgnoredPages;
				summary.Cells[excelRow, 5] = website.SearchTime.ToString();
				summary.Cells[excelRow, 6] = website.WebsiteStatus.ToString();

				int resultCount;
				if (config.EnableStrictFilter && config.OnlyNewResults) {
					resultCount = website.ResultList.Count(i => i.IsNewResult && i.Hits.Any(j => !j.IsLink));
				} else if (config.EnableStrictFilter) {
					resultCount = website.ResultList.Count(i => i.Hits.Any(j => !j.IsLink));
				} else if (config.OnlyNewResults) {
					resultCount = website.ResultList.Count(i => i.IsNewResult);
				} else {
					resultCount = website.ResultList.Count;
				}

				summary.Cells[excelRow, 2] = resultCount;
			}

			// Keywords in search
			excelRow += 2;
			summary.Cells[excelRow, 1] = "Keywords Used";
			summary.Range["A" + excelRow].Font.Bold = true;
			foreach (var keyword in config.PageWords) {
				excelRow++;
				summary.Cells[excelRow, 1] = keyword.Text;
				summary.Cells[excelRow, 2] = (keyword.IsEnabled) ? "Enabled" : "Disabled";
			}

			// Finish formatting
			summary.Range["A1"].Font.Size = 28;
			summary.Range["A1:H1"].Merge();
			summary.Range["A2:H2"].Merge();
			summary.Range["B4:D4"].Merge();
			summary.Range["A:D"].Columns.AutoFit();
			summary.Name = "Summary";

			// Autofit all the columns of every sheet in workbook
			foreach (Excel.Worksheet currSheet in grantWorkbook.Worksheets) {
				var aRng = currSheet.UsedRange;
				aRng.Columns.AutoFit();
			}

			// Delete pre-existing "Sheet1"
			grantWorkbook.Worksheets["Sheet1"].Delete();

			#endregion
		}

		private void worker_ProgressChanged(object sender, ProgressChangedEventArgs args) {
			if (progress == null) {
				LogHelper.Warn("progress is null");
				return;
			}

			LogHelper.Debug("Sheets: " + progress.CurrentSheet + "/" + progress.SheetCount + "     Rows: " + progress.CurrentRow +
				"/" + progress.RowCount);

			progressBook.Minimum = 0;
			progressSheet.Minimum = 0;

			progressBook.Maximum = progress.SheetCount;
			progressSheet.Maximum = progress.RowCount;

			if (progress.CurrentRow <= progressSheet.Maximum) {
				progressSheet.Value = progress.CurrentRow;
			} else {
				LogHelper.Warn("Progress bar for sheet overflow");
			}

			if (progress.CurrentSheet <= progressBook.Maximum) {
				progressBook.Value = progress.CurrentSheet;
			} else {
				LogHelper.Warn("Progress bar for book overflow");
			}
		}

		private void worker_WorkCompleted(object sender, RunWorkerCompletedEventArgs args) {
			btnStatusClose.Text = "Close";
			btnStatusClose.Enabled = true;

			if (isForEmail) {
				excel.Visible = false;
				var date = results.LastRan;

				// Save Grant workbook
				var filename = Reference.Files.ExportDirectory + "Grant-Results_" + date.Month + date.Day + date.Year;
				if (File.Exists(filename + ".xlsx"))
					File.Delete(filename + ".xlsx");

				grantWorkbook.SaveAs(filename, Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, false,
					Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				grantWorkbook.Close();
			} else {
				excel.Visible = true;
			}
		}

		private void ExportToFile() {
			// Indicate the program is saving the file
			btnStatusClose.Text = "Saving...";
			btnStatusClose.Enabled = false;

			switch (config.ExportMethod) {
				case Configuration.ExportType.Excel:
					ExportToExcel(false);
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

		public void ExportToExcel(bool email) {
			isForEmail = email;

			worker.RunWorkerAsync();
		}

		private void ExportToPlainText() {
			throw new NotImplementedException("Plain text file export");
		}

		private void ExportToXml() {
			throw new NotImplementedException("Xml file export");
		}

		private void btnStatusClose_Click(object sender, EventArgs e) {
			Marshal.ReleaseComObject(grantWorkbook);
			Marshal.ReleaseComObject(excel);

			Close();
		}
	}

}