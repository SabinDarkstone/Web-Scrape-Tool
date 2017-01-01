namespace Visco_Web_Scrape_v2.Forms {
	partial class GrantSearch {
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GrantSearch));
			this.label1 = new System.Windows.Forms.Label();
			this.lblCurrentDomain = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lblCurrentUrl = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.lblPagesCrawledCount = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.lblResultsFound = new System.Windows.Forms.Label();
			this.progressbar = new System.Windows.Forms.ProgressBar();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lblCurrentDomainTime = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.lblTotalTime = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.lblPagesSkippedCount = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.lblCurrentStatus = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.btnCancelCrawl = new System.Windows.Forms.Button();
			this.btnSaveResults = new System.Windows.Forms.Button();
			this.timerTotal = new System.Windows.Forms.Timer(this.components);
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(42, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(83, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Current Domain:";
			// 
			// lblCurrentDomain
			// 
			this.lblCurrentDomain.AutoSize = true;
			this.lblCurrentDomain.Location = new System.Drawing.Point(131, 26);
			this.lblCurrentDomain.Name = "lblCurrentDomain";
			this.lblCurrentDomain.Size = new System.Drawing.Size(53, 13);
			this.lblCurrentDomain.TabIndex = 2;
			this.lblCurrentDomain.Text = "<domain>";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(52, 50);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(73, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Crawled URL:";
			// 
			// lblCurrentUrl
			// 
			this.lblCurrentUrl.AutoEllipsis = true;
			this.lblCurrentUrl.Location = new System.Drawing.Point(131, 50);
			this.lblCurrentUrl.Name = "lblCurrentUrl";
			this.lblCurrentUrl.Size = new System.Drawing.Size(350, 13);
			this.lblCurrentUrl.TabIndex = 4;
			this.lblCurrentUrl.Text = "<current url>";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(44, 74);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(81, 13);
			this.label5.TabIndex = 5;
			this.label5.Text = "Pages Crawled:";
			// 
			// lblPagesCrawledCount
			// 
			this.lblPagesCrawledCount.AutoSize = true;
			this.lblPagesCrawledCount.Location = new System.Drawing.Point(131, 74);
			this.lblPagesCrawledCount.Name = "lblPagesCrawledCount";
			this.lblPagesCrawledCount.Size = new System.Drawing.Size(73, 13);
			this.lblPagesCrawledCount.TabIndex = 6;
			this.lblPagesCrawledCount.Text = "<page count>";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(47, 98);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(78, 13);
			this.label7.TabIndex = 7;
			this.label7.Text = "Results Found:";
			// 
			// lblResultsFound
			// 
			this.lblResultsFound.AutoSize = true;
			this.lblResultsFound.Location = new System.Drawing.Point(131, 98);
			this.lblResultsFound.Name = "lblResultsFound";
			this.lblResultsFound.Size = new System.Drawing.Size(74, 13);
			this.lblResultsFound.TabIndex = 8;
			this.lblResultsFound.Text = "<result count>";
			// 
			// progressbar
			// 
			this.progressbar.Location = new System.Drawing.Point(6, 153);
			this.progressbar.Name = "progressbar";
			this.progressbar.Size = new System.Drawing.Size(485, 23);
			this.progressbar.TabIndex = 9;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lblCurrentDomainTime);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.lblTotalTime);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.lblPagesSkippedCount);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.lblCurrentStatus);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.progressbar);
			this.groupBox1.Controls.Add(this.lblResultsFound);
			this.groupBox1.Controls.Add(this.lblCurrentDomain);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.lblPagesCrawledCount);
			this.groupBox1.Controls.Add(this.lblCurrentUrl);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(497, 189);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Progress";
			// 
			// lblCurrentDomainTime
			// 
			this.lblCurrentDomainTime.AutoSize = true;
			this.lblCurrentDomainTime.Location = new System.Drawing.Point(345, 123);
			this.lblCurrentDomainTime.Name = "lblCurrentDomainTime";
			this.lblCurrentDomainTime.Size = new System.Drawing.Size(41, 13);
			this.lblCurrentDomainTime.TabIndex = 17;
			this.lblCurrentDomainTime.Text = "<timer>";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(230, 123);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(109, 13);
			this.label9.TabIndex = 16;
			this.label9.Text = "Current Domain Time:";
			// 
			// lblTotalTime
			// 
			this.lblTotalTime.AutoSize = true;
			this.lblTotalTime.Location = new System.Drawing.Point(131, 123);
			this.lblTotalTime.Name = "lblTotalTime";
			this.lblTotalTime.Size = new System.Drawing.Size(41, 13);
			this.lblTotalTime.TabIndex = 15;
			this.lblTotalTime.Text = "<timer>";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(24, 123);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(101, 13);
			this.label8.TabIndex = 14;
			this.label8.Text = "Total Elapsed Time:";
			// 
			// lblPagesSkippedCount
			// 
			this.lblPagesSkippedCount.AutoSize = true;
			this.lblPagesSkippedCount.Location = new System.Drawing.Point(345, 74);
			this.lblPagesSkippedCount.Name = "lblPagesSkippedCount";
			this.lblPagesSkippedCount.Size = new System.Drawing.Size(73, 13);
			this.lblPagesSkippedCount.TabIndex = 13;
			this.lblPagesSkippedCount.Text = "<page count>";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(257, 74);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(82, 13);
			this.label6.TabIndex = 12;
			this.label6.Text = "Pages Skipped:";
			// 
			// lblCurrentStatus
			// 
			this.lblCurrentStatus.AutoSize = true;
			this.lblCurrentStatus.Location = new System.Drawing.Point(345, 98);
			this.lblCurrentStatus.Name = "lblCurrentStatus";
			this.lblCurrentStatus.Size = new System.Drawing.Size(47, 13);
			this.lblCurrentStatus.TabIndex = 11;
			this.lblCurrentStatus.Text = "<status>";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(299, 98);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 13);
			this.label4.TabIndex = 10;
			this.label4.Text = "Status:";
			// 
			// btnCancelCrawl
			// 
			this.btnCancelCrawl.Location = new System.Drawing.Point(252, 220);
			this.btnCancelCrawl.Name = "btnCancelCrawl";
			this.btnCancelCrawl.Size = new System.Drawing.Size(75, 23);
			this.btnCancelCrawl.TabIndex = 11;
			this.btnCancelCrawl.Text = "Cancel";
			this.btnCancelCrawl.UseVisualStyleBackColor = true;
			this.btnCancelCrawl.Click += new System.EventHandler(this.btnCancelCrawl_Click);
			// 
			// btnSaveResults
			// 
			this.btnSaveResults.Enabled = false;
			this.btnSaveResults.Location = new System.Drawing.Point(171, 220);
			this.btnSaveResults.Name = "btnSaveResults";
			this.btnSaveResults.Size = new System.Drawing.Size(75, 23);
			this.btnSaveResults.TabIndex = 12;
			this.btnSaveResults.Text = "Save Results";
			this.btnSaveResults.UseVisualStyleBackColor = true;
			this.btnSaveResults.Click += new System.EventHandler(this.btnSaveResults_Click);
			// 
			// timerTotal
			// 
			this.timerTotal.Enabled = true;
			this.timerTotal.Interval = 1000;
			this.timerTotal.Tick += new System.EventHandler(this.timerTotal_Tick);
			// 
			// GrantSearch
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(521, 255);
			this.Controls.Add(this.btnSaveResults);
			this.Controls.Add(this.btnCancelCrawl);
			this.Controls.Add(this.groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "GrantSearch";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Web Crawler and Search";
			this.Load += new System.EventHandler(this.GrantSearch_Load);
			this.Shown += new System.EventHandler(this.GrantSearch_Shown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblCurrentDomain;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lblCurrentUrl;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label lblPagesCrawledCount;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label lblResultsFound;
		private System.Windows.Forms.ProgressBar progressbar;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnCancelCrawl;
		private System.Windows.Forms.Button btnSaveResults;
		private System.Windows.Forms.Label lblCurrentStatus;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lblPagesSkippedCount;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label lblTotalTime;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Timer timerTotal;
		private System.Windows.Forms.Label lblCurrentDomainTime;
		private System.Windows.Forms.Label label9;
	}
}