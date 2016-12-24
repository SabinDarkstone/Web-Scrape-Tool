namespace Visco_Web_Scrape.Forms {
	partial class SearchProgress {
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
			this.btnCancelScrape = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.btnExportDocuments = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.lblCurrentDomain = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lblCurrentPage = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.lblPageCrawledCount = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnCancelScrape
			// 
			this.btnCancelScrape.Location = new System.Drawing.Point(199, 239);
			this.btnCancelScrape.Name = "btnCancelScrape";
			this.btnCancelScrape.Size = new System.Drawing.Size(75, 23);
			this.btnCancelScrape.TabIndex = 1;
			this.btnCancelScrape.Text = "Cancel";
			this.btnCancelScrape.UseVisualStyleBackColor = true;
			this.btnCancelScrape.Click += new System.EventHandler(this.btnCancelScrape_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(81, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(332, 24);
			this.label1.TabIndex = 3;
			this.label1.Text = "Website Crawl and Scrape In Progress";
			// 
			// btnExportDocuments
			// 
			this.btnExportDocuments.Location = new System.Drawing.Point(280, 239);
			this.btnExportDocuments.Name = "btnExportDocuments";
			this.btnExportDocuments.Size = new System.Drawing.Size(91, 23);
			this.btnExportDocuments.TabIndex = 6;
			this.btnExportDocuments.Text = "Export Results";
			this.btnExportDocuments.UseVisualStyleBackColor = true;
			this.btnExportDocuments.Click += new System.EventHandler(this.btnExportDocuments_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(71, 91);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(83, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Current Domain:";
			// 
			// lblCurrentDomain
			// 
			this.lblCurrentDomain.AutoSize = true;
			this.lblCurrentDomain.Location = new System.Drawing.Point(160, 91);
			this.lblCurrentDomain.Name = "lblCurrentDomain";
			this.lblCurrentDomain.Size = new System.Drawing.Size(53, 13);
			this.lblCurrentDomain.TabIndex = 8;
			this.lblCurrentDomain.Text = "<domain>";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(82, 123);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 13);
			this.label3.TabIndex = 9;
			this.label3.Text = "Current Page:";
			// 
			// lblCurrentPage
			// 
			this.lblCurrentPage.AutoSize = true;
			this.lblCurrentPage.Location = new System.Drawing.Point(160, 123);
			this.lblCurrentPage.Name = "lblCurrentPage";
			this.lblCurrentPage.Size = new System.Drawing.Size(57, 13);
			this.lblCurrentPage.TabIndex = 10;
			this.lblCurrentPage.Text = "<page url>";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(73, 164);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(81, 13);
			this.label4.TabIndex = 11;
			this.label4.Text = "Pages Crawled:";
			// 
			// lblPageCrawledCount
			// 
			this.lblPageCrawledCount.AutoSize = true;
			this.lblPageCrawledCount.Location = new System.Drawing.Point(160, 164);
			this.lblPageCrawledCount.Name = "lblPageCrawledCount";
			this.lblPageCrawledCount.Size = new System.Drawing.Size(46, 13);
			this.lblPageCrawledCount.TabIndex = 12;
			this.lblPageCrawledCount.Text = "<count>";
			// 
			// SearchProgress
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(699, 299);
			this.Controls.Add(this.lblPageCrawledCount);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.lblCurrentPage);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lblCurrentDomain);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnExportDocuments);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnCancelScrape);
			this.Name = "SearchProgress";
			this.Text = "ScrapeProgress";
			this.Load += new System.EventHandler(this.SearchProgress_Load);
			this.Shown += new System.EventHandler(this.ScrapeProgress_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button btnCancelScrape;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnExportDocuments;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblCurrentDomain;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lblCurrentPage;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lblPageCrawledCount;
	}
}