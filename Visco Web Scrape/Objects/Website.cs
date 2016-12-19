using System;

namespace Visco_Web_Scrape.Objects {

	[Serializable]
	public class Website {

		public Website() {
			Url = "";
			Name = "";
		}

		/// <summary>
		/// The fully qualified URL of the website
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// User-friendly name of website for displaying in listboxes, etc.
		/// </summary>
		public string Name { get; set; }

		public override string ToString() {
			return Name;
		}
	}
}
