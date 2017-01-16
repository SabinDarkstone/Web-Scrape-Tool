using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;

namespace Visco_Web_Scrape_v2.Scripts.Helpers {

	public partial class ExcelHelper {

		private static readonly string[] Fields = {
			"<id>", "<starttime>", "<endtime>", "<runtime>", "<blacklistenabled>", "<resultsshown>", "<linkresultsenabled>", "<contextualenabled>", "<keywordstart>", "<websitestart>", "<date>"
		};

		/// <summary>
		/// Finds the location of a field on a specified worksheet.
		/// </summary>
		/// <param name="field">Field to search the page for</param>
		/// <param name="sheet">Sheet to run the search on</param>
		/// <returns>Cell that contains the specified field</returns>
		public static Excel.Range FindCell(FieldChoices field, Excel.Worksheet sheet) {
			Excel.Range currentFind = null;
			Excel.Range firstFind = null;

			var rangeToSearch = sheet.UsedRange;
			currentFind = rangeToSearch.Find(Fields[(int) field], Missing.Value, Excel.XlFindLookIn.xlValues,
				Excel.XlLookAt.xlPart, Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Missing.Value,
				Missing.Value);

			while (currentFind != null) {
				if (firstFind == null) {
					firstFind = currentFind;
				} else if (currentFind.get_Address(Excel.XlReferenceStyle.xlA1) == firstFind.get_Address(Excel.XlReferenceStyle.xlA1)) {
					break;
				}
			}

			return currentFind.FindNext(currentFind);
		}
	}

}