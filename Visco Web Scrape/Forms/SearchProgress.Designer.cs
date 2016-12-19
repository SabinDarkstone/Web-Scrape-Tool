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
			this.lblProgressStatus = new System.Windows.Forms.Label();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.btnExportDocuments = new System.Windows.Forms.Button();
			this.lblFoundPagesCount = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnCancelScrape
			// 
			this.btnCancelScrape.Location = new System.Drawing.Point(106, 193);
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
			this.label1.Location = new System.Drawing.Point(40, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(332, 24);
			this.label1.TabIndex = 3;
			this.label1.Text = "Website Crawl and Scrape In Progress";
			// 
			// lblProgressStatus
			// 
			this.lblProgressStatus.Location = new System.Drawing.Point(9, 93);
			this.lblProgressStatus.Name = "lblProgressStatus";
			this.lblProgressStatus.Size = new System.Drawing.Size(388, 51);
			this.lblProgressStatus.TabIndex = 4;
			this.lblProgressStatus.Text = "<Progress Status>";
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(12, 54);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(385, 23);
			this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progressBar1.TabIndex = 5;
			// 
			// btnExportDocuments
			// 
			this.btnExportDocuments.Location = new System.Drawing.Point(187, 193);
			this.btnExportDocuments.Name = "btnExportDocuments";
			this.btnExportDocuments.Size = new System.Drawing.Size(91, 23);
			this.btnExportDocuments.TabIndex = 6;
			this.btnExportDocuments.Text = "Export Results";
			this.btnExportDocuments.UseVisualStyleBackColor = true;
			this.btnExportDocuments.Click += new System.EventHandler(this.btnExportDocuments_Click);
			// 
			// lblFoundPagesCount
			// 
			this.lblFoundPagesCount.AutoSize = true;
			this.lblFoundPagesCount.Location = new System.Drawing.Point(154, 171);
			this.lblFoundPagesCount.Name = "lblFoundPagesCount";
			this.lblFoundPagesCount.Size = new System.Drawing.Size(82, 13);
			this.lblFoundPagesCount.TabIndex = 7;
			this.lblFoundPagesCount.Text = "<Found Pages>";
			this.lblFoundPagesCount.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// SearchProgress
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(409, 228);
			this.Controls.Add(this.lblFoundPagesCount);
			this.Controls.Add(this.btnExportDocuments);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.lblProgressStatus);
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
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Button btnExportDocuments;
		public System.Windows.Forms.Label lblProgressStatus;
		public System.Windows.Forms.Label lblFoundPagesCount;
	}
}