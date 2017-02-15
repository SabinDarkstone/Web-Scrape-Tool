namespace Visco_Web_Scrape_v2.Exporters {

	public class ExportProgress {

		public ExportProgress(int sheets) {
			this.SheetCount = sheets;
		}

		public int CurrentSheet { get; set; }
		public int SheetCount { get; }
	}

}