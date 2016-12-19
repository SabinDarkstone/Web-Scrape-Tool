namespace Visco_Web_Scrape.Forms {
	partial class MainForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.listboxWebsiteList = new System.Windows.Forms.ListBox();
			this.btnModifyWebsites = new System.Windows.Forms.Button();
			this.btnRunScrape = new System.Windows.Forms.Button();
			this.btnEditFilters = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listboxWebsiteList
			// 
			this.listboxWebsiteList.FormattingEnabled = true;
			this.listboxWebsiteList.Location = new System.Drawing.Point(32, 46);
			this.listboxWebsiteList.Name = "listboxWebsiteList";
			this.listboxWebsiteList.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
			this.listboxWebsiteList.Size = new System.Drawing.Size(208, 108);
			this.listboxWebsiteList.TabIndex = 0;
			// 
			// btnModifyWebsites
			// 
			this.btnModifyWebsites.Location = new System.Drawing.Point(48, 174);
			this.btnModifyWebsites.Name = "btnModifyWebsites";
			this.btnModifyWebsites.Size = new System.Drawing.Size(75, 23);
			this.btnModifyWebsites.TabIndex = 1;
			this.btnModifyWebsites.Text = "Modify List";
			this.btnModifyWebsites.UseVisualStyleBackColor = true;
			this.btnModifyWebsites.Click += new System.EventHandler(this.btnModifyWebsites_Click);
			// 
			// btnRunScrape
			// 
			this.btnRunScrape.Location = new System.Drawing.Point(81, 203);
			this.btnRunScrape.Name = "btnRunScrape";
			this.btnRunScrape.Size = new System.Drawing.Size(75, 23);
			this.btnRunScrape.TabIndex = 2;
			this.btnRunScrape.Text = "Run";
			this.btnRunScrape.UseVisualStyleBackColor = true;
			this.btnRunScrape.Click += new System.EventHandler(this.btnRunScrape_Click);
			// 
			// btnEditFilters
			// 
			this.btnEditFilters.Location = new System.Drawing.Point(129, 174);
			this.btnEditFilters.Name = "btnEditFilters";
			this.btnEditFilters.Size = new System.Drawing.Size(86, 23);
			this.btnEditFilters.TabIndex = 3;
			this.btnEditFilters.Text = "Modify Filters";
			this.btnEditFilters.UseVisualStyleBackColor = true;
			this.btnEditFilters.Click += new System.EventHandler(this.btnEditFilters_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Controls.Add(this.btnEditFilters);
			this.Controls.Add(this.btnRunScrape);
			this.Controls.Add(this.btnModifyWebsites);
			this.Controls.Add(this.listboxWebsiteList);
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox listboxWebsiteList;
		private System.Windows.Forms.Button btnModifyWebsites;
		private System.Windows.Forms.Button btnRunScrape;
		private System.Windows.Forms.Button btnEditFilters;
	}
}