using System;
using System.Threading;
using System.Windows.Forms;
using Visco_Web_Scrape_v2.Exporters;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Scripts.Helpers;
using Visco_Web_Scrape_v2.Search.Items;

namespace Visco_Web_Scrape_v2.Forms {

	public partial class ResultViewer : Form {

		public string[] Filenames;
		public bool IsForEmail;
		public bool IsCompleted { get; private set; }
		public AutoResetEvent Completion;

		private readonly Configuration config;
		private readonly CombinedResults results;

		public ResultViewer(Configuration configuration, CombinedResults results, bool email, string[] filenames = null) {
			InitializeComponent();

			this.config = configuration;
			this.results = results;
			this.Filenames = filenames;
			this.IsForEmail = email;
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

		public void StartExport() {
			IExportable exporter;

			switch (config.ExportMethod) {
				case Configuration.ExportType.Excel:
					exporter = new ExcelExport(config, results, this);

					LogHelper.Debug("Waiting for background process to finish");
					((ExcelExport) exporter).Completion.WaitOne();
					IsCompleted = ((ExcelExport) exporter).IsCompleted;
					LogHelper.Debug("Background process finished");
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

	}
}