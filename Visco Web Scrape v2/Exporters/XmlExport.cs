using System.Xml;
using Visco_Web_Scrape_v2.Forms;
using Visco_Web_Scrape_v2.Scripts;
using Visco_Web_Scrape_v2.Search.Items;

namespace Visco_Web_Scrape_v2.Exporters {

	public class XmlExport : BasicExport, IExportable {

		private ResultViewer parentForm;
		private XmlDocument xmlDoc;

		public XmlExport(Configuration configuration, CombinedResults results, ResultViewer resultViewer) : base(configuration, results) {
			parentForm = resultViewer;
			xmlDoc = new XmlDocument();
		}

		public void GenerateFile(ExcelExport.BookType bookType) {
			throw new System.NotImplementedException();
		}

		public void SaveFile(ExcelExport.BookType bookType) {
			throw new System.NotImplementedException();
		}
	}

}