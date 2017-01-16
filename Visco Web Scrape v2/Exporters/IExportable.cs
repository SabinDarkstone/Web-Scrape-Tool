﻿namespace Visco_Web_Scrape_v2.Exporters {

	public interface IExportable {

		/// <summary>
		/// Generates the file contents and save the stream in memory
		/// </summary>
		/// <param name="bookType">Type of workbook to save to</param>
		void GenerateFile(ExcelExport.BookType bookType);

		/// <summary>
		/// Saves the memory stream to a file for the user later on
		/// </summary>
		/// <param name="bookType">Type of workbook to save to</param>
		void SaveFile(ExcelExport.BookType bookType);
	}

}