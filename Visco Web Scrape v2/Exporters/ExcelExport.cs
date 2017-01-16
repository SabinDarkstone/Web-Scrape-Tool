using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Visco_Web_Scrape_v2.Forms;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Scripts.Helpers;
using Visco_Web_Scrape_v2.Search.Items;
using static Visco_Web_Scrape_v2.Search.Items.WebsiteResults;
using Excel = Microsoft.Office.Interop.Excel;

namespace Visco_Web_Scrape_v2.Exporters {

	/// <summary>
	/// Handles the exporting of search results to an excel file format
	/// </summary>
	public class ExcelExport : BasicExport, IExportable {

		public enum BookType {
			Grants, Other
		}

		private Excel.Application excel;
		private Excel.Workbook grantWorkbook;
		private Excel.Workbook otherWorkbook;

		private BackgroundWorker worker;
		private ExportProgress progress;
		private readonly ResultViewer parentForm;

		private readonly int grantCount;
		private readonly int otherCount;

		public ExcelExport(Configuration configuration, CombinedResults results, ResultViewer resultViewer) : base(configuration, results) {
			parentForm = resultViewer;
			LogHelper.Info("Beginning excel export process");

			worker = new BackgroundWorker();
			worker.DoWork += Worker_DoWork;
			worker.ProgressChanged += Worker_OnProgressChanged;
			worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
			worker.WorkerReportsProgress = true;

			foreach (var website in ResultsToExport.AllResults.ToList()) {
				if (website == null) {
					ResultsToExport.AllResults.Remove(website);
				}
			}

			grantCount = ResultsToExport.AllResults.Count(i => i.RootWebsite.IsGrantSource);
			otherCount = ResultsToExport.AllResults.Count - grantCount;

			worker.RunWorkerAsync();
		}

		private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			LogHelper.Debug("Complete!");

			excel.Visible = true;
		}

		private void Worker_OnProgressChanged(object sender, ProgressChangedEventArgs e) {
			parentForm.ProgressUpdate(progress);
		}

		private void Worker_DoWork(object sender, DoWorkEventArgs e) {
			// Prepare excel
			excel = new Excel.Application {Visible = false};
			grantWorkbook = excel.Workbooks.Add();
			otherWorkbook = excel.Workbooks.Add();

			// Setup progress tracker
			progress = new ExportProgress(ResultsToExport.AllResults.Count);

			// Create the GRANTS workbook
			GenerateFile(BookType.Grants);
			GenerateFile(BookType.Other);
		}

		public void GenerateFile(BookType bookType) {
			// Assign workbook to use and filter results for proper list
			Excel.Workbook book;
			List<WebsiteResults> myResults;
			if (bookType == BookType.Grants) {
				book = grantWorkbook;
				myResults = ResultsToExport.AllResults.ToList().FindAll(i => i.RootWebsite.IsGrantSource);
			} else {
				book = otherWorkbook;
				myResults = ResultsToExport.AllResults.ToList().FindAll(i => !i.RootWebsite.IsGrantSource);
			}

			// Reverse the order so worksheets are in proper order
			myResults.Reverse();

			// Iterate through websites and write information to them
			foreach (var website in myResults) {
				// Website was skipped during search
				if (website.WebsiteStatus == Status.Skipped) {
					LogHelper.Debug("Because " + website.RootWebsite +
						" was skipped during search, it does not get its own results sheet.");
					// Report progress and continue along the list
					progress.CurrentSheet++;
					worker.ReportProgress(0, progress);
					continue;
				}

				// Get the number of rows needed for the sheet
				progress.RowCount = website.ResultList.Count;
				// ...and reset the counter
				progress.CurrentRow = 0;

				// Add a new sheet to the workbook
				Excel.Worksheet websiteSheet = book.Worksheets.Add(Missing.Value);
				LogHelper.Debug("Adding new worksheet to book for " + website.RootWebsite);
				websiteSheet.Name = website.RootWebsite.Name.TrimLength(30).Replace('/', ' ');

				// Create sheet title
				SetSheetTitle(websiteSheet, website.RootWebsite.Name);

				// Insert sheet headers
				if (!SetSheetHeadings(websiteSheet, website)) {
					progress.CurrentSheet++;
					worker.ReportProgress(0, progress);
					continue;
				}

				// Fill in results
				FillSheetResults(websiteSheet, website);

				progress.CurrentSheet++;
				worker.ReportProgress(0, progress);
			}

			// Create summary sheet
			CreateSummarySheet(book, bookType);

			// Finalize workbook
			book.Worksheets["Sheet1"].Delete();

			LogHelper.Debug("Excel file generated");
		}

		public void SaveFile(BookType bookType) {
			
		}

		private void SetSheetTitle(Excel.Worksheet sheet, string title) {
			sheet.Cells[1, 1] = title;
			sheet.Range["A1"].Font.Size = 18;
		}

		private bool SetSheetHeadings(Excel.Worksheet sheet, WebsiteResults results) {
			// If no new results are found
			if (Config.OnlyNewResults && results.ResultList.Count == 0) {
				sheet.Cells[3, 1] =
					string.Format("No results were found on the {0} pages with the specified keywords since the last search.",
						results.Counts.SearchPages);

				return false;
			}

			// If no results were found at all
			if (results.ResultList.Count == 0) {
				sheet.Cells[3, 1] = string.Format("No results have ever been found on the {0} pages with specified keywords.",
					results.Counts.SearchPages);
				return false;
			}

			// If only displaying new results
			if (Config.OnlyNewResults && results.ResultList.Count > 0) {
				sheet.Cells[3, 1] = "NEW Website URL";
				sheet.Cells[3, 2] = "Keywords Found";
				if (Config.IncludeDate) {
					sheet.Cells[3, 3] = "Date Discovered";
				}
				return true;
			}

			// Showing all results
			if (results.ResultList.Count > 0) {
				sheet.Cells[3, 1] = "Website URL";
				sheet.Cells[3, 2] = "Keywords Found";
				if (Config.IncludeDate) {
					sheet.Cells[3, 3] = "Date Discovered";
				}
				return true;
			}

			return false;
		}

		private void FillSheetResults(Excel.Worksheet sheet, WebsiteResults results) {
			var excelRow = 3;
			foreach (var result in results.ResultList) {
				excelRow++;
				var url = sheet.Cells[excelRow, 1] as Excel.Range;
				string context = "";
				string keywords = "";

				if (result.Hits.Count == 1) {
					context = result.Hits.First().Context.TrimLength(300);
					keywords = result.Hits.First().Keyword.Text;
				} else if (result.Hits.Count > 1 && result.Hits.Count < 4) {
					foreach (var hit in result.Hits) {
						context += hit.Context.TrimLength(100) + "\n\n";
					}
					keywords = GetKeywordList(result);
				} else if (result.Hits.Count > 3) {
					context = "Too many results for preview";
					keywords = GetKeywordList(result);
				} else {
					context = "Error";
					keywords = "Error";
				}

				url.Hyperlinks.Add(url, result.PageUrl, Missing.Value, context, result.PageUrl);
				sheet.Cells[excelRow, 2] = keywords;
				if (Config.IncludeDate) {
					sheet.Cells[excelRow, 3] = result.DiscoveryTimeUtc.ToLocalTime().ToShortDateString();
				}

				progress.CurrentRow++;
				worker.ReportProgress(0, progress);
			}

			sheet.Range["A1:J1"].Merge();
			sheet.Range["A:C"].Columns.AutoFit();
		}

		private void CreateSummarySheet(Excel.Workbook book, BookType type) {
			// Create the sheet
			Excel.Worksheet summarySheet = book.Worksheets.Add(Missing.Value);
			summarySheet.Name = "Summary";
			int lastRow = 0;

			// Title
			summarySheet.Cells[1, 1] = "VISCO, Inc. Web Scrape Results (" + (type == BookType.Grants ? "Grants" : "Other") + ")";
			summarySheet.Cells[2, 1] = "Auto-generated by search application";

			// Settings file information
			// Date
			summarySheet.Cells[4, 1] = "Run Date";
			summarySheet.Cells[4, 2] = ResultsToExport.LastRan.ToShortDateString();
			// Whether to include all results or just new ones
			summarySheet.Cells[5, 1] = "Results Included";
			summarySheet.Cells[5, 2] = Config.OnlyNewResults ? "Only New Results" : "All Results";
			// Whether link results are reported
			summarySheet.Cells[6, 1] = "Strict Filtering";
			summarySheet.Cells[6, 2] = Config.EnableStrictFilter ? "Enabled" : "Disabled";
			
			// List of websites searched
			lastRow = ListWebsites(summarySheet, type);

			// List keywords
			ListKeywords(summarySheet, lastRow);

			// Complete formatting
			summarySheet.Range["A1:J1"].Merge();
			summarySheet.Range["A1:J1"].Merge();
			summarySheet.Range["A:D"].Columns.AutoFit();
			summarySheet.Range["A1"].Font.Size = 28;
		}

		private int ListWebsites(Excel.Worksheet sheet, BookType type) {
			sheet.Cells[8, 1] = "Websites Searched";
			sheet.Cells[9, 1] = "Name";
			sheet.Cells[9, 2] = "Results";
			sheet.Cells[9, 3] = "Searched";
			sheet.Cells[9, 4] = "Ignored";
			sheet.Cells[9, 5] = "Elapsed Time";
			sheet.Cells[9, 6] = "Status";
			sheet.Range["A8"].Font.Bold = true;

			int excelRow = 9;
			foreach (var website in ResultsToExport.AllResults.ToList()) {
				// Grant websites
				if (type == BookType.Grants && website.RootWebsite.IsGrantSource) {
					excelRow++;
					sheet.Cells[excelRow, 1] = website.RootWebsite.Name;
					sheet.Cells[excelRow, 3] = website.Counts.SearchPages;
					sheet.Cells[excelRow, 4] = website.Counts.IgnoredPages;
					sheet.Cells[excelRow, 5] = website.SearchTime.ToString();

					if (website.RootWebsite.IsEnabled) {
						sheet.Cells[excelRow, 6] = website.WebsiteStatus.ToString();
					} else {
						sheet.Cells[excelRow, 6] = "Disabled";
					}

					sheet.Cells[excelRow, 2] = website.ResultList.Count;
				}

				// Other websites
				if (type == BookType.Other && !website.RootWebsite.IsGrantSource) {
					excelRow++;
					sheet.Cells[excelRow, 1] = website.RootWebsite.Name;
					sheet.Cells[excelRow, 3] = website.Counts.SearchPages;
					sheet.Cells[excelRow, 4] = website.Counts.IgnoredPages;
					sheet.Cells[excelRow, 5] = website.SearchTime.ToString();

					if (website.RootWebsite.IsEnabled) {
						sheet.Cells[excelRow, 6] = website.WebsiteStatus.ToString();
					} else {
						sheet.Cells[excelRow, 6] = "Disabled";
					}

					sheet.Cells[excelRow, 2] = website.ResultList.Count;
				}
			}

			return excelRow;
		}

		private void ListKeywords(Excel.Worksheet sheet, int lastRow) {
			int excelRow = lastRow + 2;
			sheet.Cells[excelRow, 1] = "Keyword List";
			sheet.Cells[++excelRow, 1] = "Keyword";
			sheet.Cells[excelRow, 3] = "Status";
			sheet.Range["A" + excelRow].Font.Bold = true;

			foreach (var keyword in Config.PageWords) {
				excelRow++;
				sheet.Cells[excelRow, 1] = keyword.Text;
				sheet.Cells[excelRow, 2] = keyword.IsEnabled ? "Enabled" : "Disabled";
			}
		}
	}
}
