using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visco_Web_Scrape_v2 {

	internal class Utility {

		internal static string GetConnectionString() {
			string returnValue = null;

			var settings = ConfigurationManager.ConnectionStrings["Visco_Web_Scrape_v2.Properties.Settings.connString"];

			if (settings != null) {
				returnValue = settings.ConnectionString;
			}

			return returnValue;
		}
	}

}