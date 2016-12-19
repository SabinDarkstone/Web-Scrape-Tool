using System;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace Visco_Web_Scrape.Operations {
	public static class ConvertFile {

		public static string ExtractTextFromPdf(Uri uri) {
			using (var reader = new PdfReader(uri.AbsoluteUri)) {
				var text = new StringBuilder();
				for (int i = 0; i < reader.NumberOfPages; i++) {
					text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
				}
				return text.ToString();
			}
		}
	}
}
