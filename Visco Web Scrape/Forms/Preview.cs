using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Visco_Web_Scrape.Helpers;

namespace Visco_Web_Scrape.Forms {
	public partial class Preview : Form {
		public Preview(Uri url) {
			InitializeComponent();

			LogHelper.Debug(url.AbsoluteUri);
			webBrowser1.Url = url;
			webBrowser1.Refresh();
		}
	}
}
