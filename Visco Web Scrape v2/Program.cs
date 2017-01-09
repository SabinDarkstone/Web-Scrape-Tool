﻿using System;
using System.Windows.Forms;
using Visco_Web_Scrape_v2.Forms;

namespace Visco_Web_Scrape_v2 {

	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}

}