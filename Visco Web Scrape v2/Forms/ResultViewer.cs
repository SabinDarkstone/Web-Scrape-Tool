using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Visco_Web_Scrape_v2.Exporters;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Scripts.Helpers;
using Visco_Web_Scrape_v2.Search.Items;

namespace Visco_Web_Scrape_v2.Forms {

	public partial class ResultViewer : Form {

		public string[] Filenames;
		public bool IsForEmail;
		public bool IsCompleted { get; set; }

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
			LogHelper.Debug("Progress update here!");
		}

		public async Task StartExport() {
			IExportable exporter;

			switch (config.ExportMethod) {
				case Configuration.ExportType.Excel:
					exporter = new ExcelExport(config, results, this);
					LogHelper.Debug("Waiting for background process to finish");
					IsCompleted = ((ExcelExport) exporter).IsCompleted;
					LogHelper.Debug("Background process finished");
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void ResultViewer_Load(object sender, EventArgs e)
		{

		}
	}
}