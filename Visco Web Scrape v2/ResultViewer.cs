﻿using System;
using System.Reflection;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Visco_Web_Scrape_v2 {

	public partial class ResultViewer : Form {

		private readonly Configuration config;

		public ResultViewer(Configuration configuration) {
			InitializeComponent();
			config = configuration;
		}

		private void ResultViewer_Shown(object sender, EventArgs e) {
			ExportToFile();
		}

		private void ExportToFile() {
			// Indicate the program is saving the file
			btnStatusClose.Text = "Saving...";
			btnStatusClose.Enabled = false;

			// Initialize excel
			Excel.Application excel;

			// Save the file
			var results = config.Results;

			// Start new excel application
			excel = new Excel.Application();
			excel.Visible = true;

			// Get a new workbook
			Excel._Workbook workbook = excel.Workbooks.Add(Missing.Value);

			foreach (var entry in results) {
				var currentRow = 1;
				try {
					// Get a new worksheet
					Excel._Worksheet sheet = workbook.Worksheets.Add();
					sheet.Name = entry.Name;

					// Add keyword
					foreach (var keyword in config.Keywords) {
						sheet.Cells[currentRow, 1] = keyword.Text;
						currentRow++;
						foreach (var hit in entry.ResultList) {
							if (hit.Keywords.Contains(keyword)) {
//								sheet.Cells[currentRow, 1] = hit.Url;
								var rng = sheet.Cells[currentRow, 1] as Excel.Range;
								rng.Hyperlinks.Add(rng, hit.Url, Missing.Value, "Click to open", hit.Url);
								currentRow++;
							}
						}
						currentRow += 2;
					}

				} catch (Exception ex) {
					MessageBox.Show(ex.Message + "\n" + ex.InnerException + "\n" + ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			/*
			var filename = "Results";
			MessageBox.Show(filename);
			excel.Save(filename);
			*/

			// Indicate the program is finished saving
			btnStatusClose.Text = "Close";
			btnStatusClose.Enabled = true;
		}

		private void btnStatusClose_Click(object sender, EventArgs e) {
			Close();
		}
	}
}
