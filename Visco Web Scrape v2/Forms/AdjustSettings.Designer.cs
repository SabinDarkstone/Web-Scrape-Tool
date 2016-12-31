namespace Visco_Web_Scrape_v2.Forms {
	partial class AdjustSettings {
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
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnApply = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chkbxAnalyzeUrl = new System.Windows.Forms.CheckBox();
			this.chkbxEnableUrlFilter = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtPagesPerDomain = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnClearSearchSettings = new System.Windows.Forms.Button();
			this.btnClearResults = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.radioXml = new System.Windows.Forms.RadioButton();
			this.radioPlainText = new System.Windows.Forms.RadioButton();
			this.radioExcel = new System.Windows.Forms.RadioButton();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.chkbxDateFound = new System.Windows.Forms.CheckBox();
			this.chkbxNewResultsOnly = new System.Windows.Forms.CheckBox();
			this.chkbxSendEmail = new System.Windows.Forms.CheckBox();
			this.flowLayoutPanel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.Controls.Add(this.btnApply);
			this.flowLayoutPanel1.Controls.Add(this.btnCancel);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(91, 402);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(162, 29);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// btnApply
			// 
			this.btnApply.Location = new System.Drawing.Point(3, 3);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(75, 23);
			this.btnApply.TabIndex = 0;
			this.btnApply.Text = "Apply";
			this.btnApply.UseVisualStyleBackColor = true;
			this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(84, 3);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chkbxAnalyzeUrl);
			this.groupBox1.Controls.Add(this.chkbxEnableUrlFilter);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtPagesPerDomain);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(241, 108);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Crawl Settings";
			// 
			// chkbxAnalyzeUrl
			// 
			this.chkbxAnalyzeUrl.AutoSize = true;
			this.chkbxAnalyzeUrl.Location = new System.Drawing.Point(21, 76);
			this.chkbxAnalyzeUrl.Name = "chkbxAnalyzeUrl";
			this.chkbxAnalyzeUrl.Size = new System.Drawing.Size(168, 17);
			this.chkbxAnalyzeUrl.TabIndex = 4;
			this.chkbxAnalyzeUrl.Text = "Run URL Analysis (Very Slow)";
			this.chkbxAnalyzeUrl.UseVisualStyleBackColor = true;
			// 
			// chkbxEnableUrlFilter
			// 
			this.chkbxEnableUrlFilter.AutoSize = true;
			this.chkbxEnableUrlFilter.Location = new System.Drawing.Point(21, 53);
			this.chkbxEnableUrlFilter.Name = "chkbxEnableUrlFilter";
			this.chkbxEnableUrlFilter.Size = new System.Drawing.Size(123, 17);
			this.chkbxEnableUrlFilter.TabIndex = 1;
			this.chkbxEnableUrlFilter.Text = "Enable URL Filtering";
			this.chkbxEnableUrlFilter.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(18, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(142, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Maximum Pages Per Domain";
			// 
			// txtPagesPerDomain
			// 
			this.txtPagesPerDomain.Location = new System.Drawing.Point(166, 23);
			this.txtPagesPerDomain.Name = "txtPagesPerDomain";
			this.txtPagesPerDomain.Size = new System.Drawing.Size(62, 20);
			this.txtPagesPerDomain.TabIndex = 3;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.btnClearSearchSettings);
			this.groupBox2.Controls.Add(this.btnClearResults);
			this.groupBox2.Location = new System.Drawing.Point(12, 126);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(132, 107);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "App Settings";
			// 
			// btnClearSearchSettings
			// 
			this.btnClearSearchSettings.Location = new System.Drawing.Point(19, 53);
			this.btnClearSearchSettings.Name = "btnClearSearchSettings";
			this.btnClearSearchSettings.Size = new System.Drawing.Size(96, 43);
			this.btnClearSearchSettings.TabIndex = 1;
			this.btnClearSearchSettings.Text = "Reset Websites and Keywords";
			this.btnClearSearchSettings.UseVisualStyleBackColor = true;
			this.btnClearSearchSettings.Click += new System.EventHandler(this.btnClearSearchSettings_Click);
			// 
			// btnClearResults
			// 
			this.btnClearResults.Location = new System.Drawing.Point(19, 24);
			this.btnClearResults.Name = "btnClearResults";
			this.btnClearResults.Size = new System.Drawing.Size(96, 23);
			this.btnClearResults.TabIndex = 0;
			this.btnClearResults.Text = "Clear Result List";
			this.btnClearResults.UseVisualStyleBackColor = true;
			this.btnClearResults.Click += new System.EventHandler(this.btnClearResults_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.radioXml);
			this.groupBox3.Controls.Add(this.radioPlainText);
			this.groupBox3.Controls.Add(this.radioExcel);
			this.groupBox3.Location = new System.Drawing.Point(150, 126);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(103, 107);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Export File Type";
			// 
			// radioXml
			// 
			this.radioXml.AutoSize = true;
			this.radioXml.Location = new System.Drawing.Point(18, 73);
			this.radioXml.Name = "radioXml";
			this.radioXml.Size = new System.Drawing.Size(66, 17);
			this.radioXml.TabIndex = 2;
			this.radioXml.TabStop = true;
			this.radioXml.Text = "XML File";
			this.radioXml.UseVisualStyleBackColor = true;
			// 
			// radioPlainText
			// 
			this.radioPlainText.AutoSize = true;
			this.radioPlainText.Location = new System.Drawing.Point(18, 50);
			this.radioPlainText.Name = "radioPlainText";
			this.radioPlainText.Size = new System.Drawing.Size(72, 17);
			this.radioPlainText.TabIndex = 1;
			this.radioPlainText.TabStop = true;
			this.radioPlainText.Text = "Plain Text";
			this.radioPlainText.UseVisualStyleBackColor = true;
			// 
			// radioExcel
			// 
			this.radioExcel.AutoSize = true;
			this.radioExcel.Location = new System.Drawing.Point(18, 27);
			this.radioExcel.Name = "radioExcel";
			this.radioExcel.Size = new System.Drawing.Size(70, 17);
			this.radioExcel.TabIndex = 0;
			this.radioExcel.TabStop = true;
			this.radioExcel.Text = "Excel File";
			this.radioExcel.UseVisualStyleBackColor = true;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.chkbxSendEmail);
			this.groupBox4.Controls.Add(this.chkbxDateFound);
			this.groupBox4.Controls.Add(this.chkbxNewResultsOnly);
			this.groupBox4.Location = new System.Drawing.Point(12, 239);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(241, 105);
			this.groupBox4.TabIndex = 4;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Export Content";
			// 
			// chkbxDateFound
			// 
			this.chkbxDateFound.AutoSize = true;
			this.chkbxDateFound.Location = new System.Drawing.Point(21, 51);
			this.chkbxDateFound.Name = "chkbxDateFound";
			this.chkbxDateFound.Size = new System.Drawing.Size(120, 17);
			this.chkbxDateFound.TabIndex = 1;
			this.chkbxDateFound.Text = "Include Date Found";
			this.chkbxDateFound.UseVisualStyleBackColor = true;
			// 
			// chkbxNewResultsOnly
			// 
			this.chkbxNewResultsOnly.AutoSize = true;
			this.chkbxNewResultsOnly.Location = new System.Drawing.Point(21, 28);
			this.chkbxNewResultsOnly.Name = "chkbxNewResultsOnly";
			this.chkbxNewResultsOnly.Size = new System.Drawing.Size(140, 17);
			this.chkbxNewResultsOnly.TabIndex = 0;
			this.chkbxNewResultsOnly.Text = "Show New Results Only";
			this.chkbxNewResultsOnly.UseVisualStyleBackColor = true;
			// 
			// chkbxSendEmail
			// 
			this.chkbxSendEmail.AutoSize = true;
			this.chkbxSendEmail.Location = new System.Drawing.Point(21, 74);
			this.chkbxSendEmail.Name = "chkbxSendEmail";
			this.chkbxSendEmail.Size = new System.Drawing.Size(156, 17);
			this.chkbxSendEmail.TabIndex = 2;
			this.chkbxSendEmail.Text = "Send Emails On Completion";
			this.chkbxSendEmail.UseVisualStyleBackColor = true;
			// 
			// AdjustSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(266, 443);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.flowLayoutPanel1);
			this.Name = "AdjustSettings";
			this.Text = "Change User Settings";
			this.Shown += new System.EventHandler(this.AdjustSettings_Shown);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button btnApply;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox chkbxEnableUrlFilter;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtPagesPerDomain;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnClearSearchSettings;
		private System.Windows.Forms.Button btnClearResults;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.RadioButton radioXml;
		private System.Windows.Forms.RadioButton radioPlainText;
		private System.Windows.Forms.RadioButton radioExcel;
		private System.Windows.Forms.CheckBox chkbxAnalyzeUrl;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.CheckBox chkbxDateFound;
		private System.Windows.Forms.CheckBox chkbxNewResultsOnly;
		private System.Windows.Forms.CheckBox chkbxSendEmail;
	}
}