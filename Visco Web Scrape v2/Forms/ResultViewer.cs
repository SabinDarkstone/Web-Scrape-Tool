using System;
using System.Windows.Forms;
using Visco_Web_Scrape_v2.Exporters;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Search.Items;

namespace Visco_Web_Scrape_v2.Forms {

	public partial class ResultViewer : Form {

		private readonly Configuration config;
		private readonly CombinedResults results;

		public ResultViewer(Configuration configuration, CombinedResults results) {
			InitializeComponent();

			this.config = configuration;
			this.results = results;
		}

		private void ResultViewer_Shown(object sender, EventArgs e) {
			StartExport();
		}

		public void ProgressUpdate(ExportProgress progress) {
			progressBook.Maximum = progress.SheetCount;
			progressBook.Value = progress.CurrentSheet;

			progressSheet.Maximum = progress.RowCount;
			progressSheet.Value = progress.CurrentRow;
		}

		private void StartExport() {
			IExportable exporter;

			switch (config.ExportMethod) {
				case Configuration.ExportType.Excel:
					exporter = new ExcelExport(config, results, this);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

		}

	}

}