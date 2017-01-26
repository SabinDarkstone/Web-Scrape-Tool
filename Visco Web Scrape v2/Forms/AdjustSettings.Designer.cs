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
			this.chkbxStrictFiltering = new System.Windows.Forms.CheckBox();
			this.chkbxEnableUrlFilter = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtPagesPerDomain = new System.Windows.Forms.TextBox();
			this.btnClearSearchSettings = new System.Windows.Forms.Button();
			this.btnClearResults = new System.Windows.Forms.Button();
			this.chkbxSendEmail = new System.Windows.Forms.CheckBox();
			this.chkbxDateFound = new System.Windows.Forms.CheckBox();
			this.chkbxNewResultsOnly = new System.Windows.Forms.CheckBox();
			this.chkbxContextSensitive = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.chkbxSunday = new System.Windows.Forms.CheckBox();
			this.chkbxMonday = new System.Windows.Forms.CheckBox();
			this.chkbxTuesday = new System.Windows.Forms.CheckBox();
			this.chkbxWednesday = new System.Windows.Forms.CheckBox();
			this.chkbxThursday = new System.Windows.Forms.CheckBox();
			this.chkbxFriday = new System.Windows.Forms.CheckBox();
			this.chkbxSaturday = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtTimeOfDayHour = new System.Windows.Forms.TextBox();
			this.txtTimeOfDayMinute = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.radioAm = new System.Windows.Forms.RadioButton();
			this.radioPm = new System.Windows.Forms.RadioButton();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label5 = new System.Windows.Forms.Label();
			this.txtRepeatWeeks = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.flowLayoutPanel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.Controls.Add(this.btnApply);
			this.flowLayoutPanel1.Controls.Add(this.btnCancel);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(231, 581);
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
			// chkbxStrictFiltering
			// 
			this.chkbxStrictFiltering.AutoSize = true;
			this.chkbxStrictFiltering.Location = new System.Drawing.Point(19, 28);
			this.chkbxStrictFiltering.Name = "chkbxStrictFiltering";
			this.chkbxStrictFiltering.Size = new System.Drawing.Size(255, 17);
			this.chkbxStrictFiltering.TabIndex = 5;
			this.chkbxStrictFiltering.Text = "Do not report results that are links to other pages";
			this.chkbxStrictFiltering.UseVisualStyleBackColor = true;
			// 
			// chkbxEnableUrlFilter
			// 
			this.chkbxEnableUrlFilter.AutoSize = true;
			this.chkbxEnableUrlFilter.Location = new System.Drawing.Point(19, 74);
			this.chkbxEnableUrlFilter.Name = "chkbxEnableUrlFilter";
			this.chkbxEnableUrlFilter.Size = new System.Drawing.Size(322, 17);
			this.chkbxEnableUrlFilter.TabIndex = 1;
			this.chkbxEnableUrlFilter.Text = "Speed up search by filtering out URLs associated with blacklist";
			this.chkbxEnableUrlFilter.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(273, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Maximum number of pages to search on a single website";
			// 
			// txtPagesPerDomain
			// 
			this.txtPagesPerDomain.Location = new System.Drawing.Point(295, 25);
			this.txtPagesPerDomain.Name = "txtPagesPerDomain";
			this.txtPagesPerDomain.Size = new System.Drawing.Size(70, 20);
			this.txtPagesPerDomain.TabIndex = 3;
			// 
			// btnClearSearchSettings
			// 
			this.btnClearSearchSettings.Location = new System.Drawing.Point(19, 51);
			this.btnClearSearchSettings.Name = "btnClearSearchSettings";
			this.btnClearSearchSettings.Size = new System.Drawing.Size(96, 43);
			this.btnClearSearchSettings.TabIndex = 1;
			this.btnClearSearchSettings.Text = "Reset Websites and Keywords";
			this.btnClearSearchSettings.UseVisualStyleBackColor = true;
			this.btnClearSearchSettings.Click += new System.EventHandler(this.btnClearSearchSettings_Click);
			// 
			// btnClearResults
			// 
			this.btnClearResults.Location = new System.Drawing.Point(19, 22);
			this.btnClearResults.Name = "btnClearResults";
			this.btnClearResults.Size = new System.Drawing.Size(96, 23);
			this.btnClearResults.TabIndex = 0;
			this.btnClearResults.Text = "Clear Result List";
			this.btnClearResults.UseVisualStyleBackColor = true;
			this.btnClearResults.Click += new System.EventHandler(this.btnClearResults_Click);
			// 
			// chkbxSendEmail
			// 
			this.chkbxSendEmail.AutoSize = true;
			this.chkbxSendEmail.Location = new System.Drawing.Point(19, 29);
			this.chkbxSendEmail.Name = "chkbxSendEmail";
			this.chkbxSendEmail.Size = new System.Drawing.Size(291, 17);
			this.chkbxSendEmail.TabIndex = 2;
			this.chkbxSendEmail.Text = "Automatically send results via emal when search finishes";
			this.chkbxSendEmail.UseVisualStyleBackColor = true;
			// 
			// chkbxDateFound
			// 
			this.chkbxDateFound.AutoSize = true;
			this.chkbxDateFound.Location = new System.Drawing.Point(19, 51);
			this.chkbxDateFound.Name = "chkbxDateFound";
			this.chkbxDateFound.Size = new System.Drawing.Size(247, 17);
			this.chkbxDateFound.TabIndex = 1;
			this.chkbxDateFound.Text = "Include when the result was found in export file";
			this.chkbxDateFound.UseVisualStyleBackColor = true;
			// 
			// chkbxNewResultsOnly
			// 
			this.chkbxNewResultsOnly.AutoSize = true;
			this.chkbxNewResultsOnly.Location = new System.Drawing.Point(19, 74);
			this.chkbxNewResultsOnly.Name = "chkbxNewResultsOnly";
			this.chkbxNewResultsOnly.Size = new System.Drawing.Size(274, 17);
			this.chkbxNewResultsOnly.TabIndex = 0;
			this.chkbxNewResultsOnly.Text = "Only export results that are new since the last search";
			this.chkbxNewResultsOnly.UseVisualStyleBackColor = true;
			// 
			// chkbxContextSensitive
			// 
			this.chkbxContextSensitive.AutoSize = true;
			this.chkbxContextSensitive.Location = new System.Drawing.Point(19, 51);
			this.chkbxContextSensitive.Name = "chkbxContextSensitive";
			this.chkbxContextSensitive.Size = new System.Drawing.Size(287, 17);
			this.chkbxContextSensitive.TabIndex = 6;
			this.chkbxContextSensitive.Text = "Compare surrounding context of results when searching";
			this.chkbxContextSensitive.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.chkbxContextSensitive);
			this.groupBox1.Controls.Add(this.txtPagesPerDomain);
			this.groupBox1.Controls.Add(this.chkbxEnableUrlFilter);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(381, 106);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Search Options";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.chkbxStrictFiltering);
			this.groupBox2.Controls.Add(this.chkbxNewResultsOnly);
			this.groupBox2.Controls.Add(this.chkbxDateFound);
			this.groupBox2.Location = new System.Drawing.Point(12, 124);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(381, 107);
			this.groupBox2.TabIndex = 8;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Export Options";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.panel2);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.panel1);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.flowLayoutPanel2);
			this.groupBox3.Controls.Add(this.chkbxSendEmail);
			this.groupBox3.Location = new System.Drawing.Point(12, 237);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(381, 262);
			this.groupBox3.TabIndex = 9;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Scheduling Options";
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.AutoSize = true;
			this.flowLayoutPanel2.Controls.Add(this.chkbxSunday);
			this.flowLayoutPanel2.Controls.Add(this.chkbxMonday);
			this.flowLayoutPanel2.Controls.Add(this.chkbxTuesday);
			this.flowLayoutPanel2.Controls.Add(this.chkbxWednesday);
			this.flowLayoutPanel2.Controls.Add(this.chkbxThursday);
			this.flowLayoutPanel2.Controls.Add(this.chkbxFriday);
			this.flowLayoutPanel2.Controls.Add(this.chkbxSaturday);
			this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(19, 86);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(90, 161);
			this.flowLayoutPanel2.TabIndex = 10;
			// 
			// chkbxSunday
			// 
			this.chkbxSunday.AutoSize = true;
			this.chkbxSunday.Location = new System.Drawing.Point(3, 3);
			this.chkbxSunday.Name = "chkbxSunday";
			this.chkbxSunday.Size = new System.Drawing.Size(62, 17);
			this.chkbxSunday.TabIndex = 0;
			this.chkbxSunday.Text = "Sunday";
			this.chkbxSunday.UseVisualStyleBackColor = true;
			// 
			// chkbxMonday
			// 
			this.chkbxMonday.AutoSize = true;
			this.chkbxMonday.Location = new System.Drawing.Point(3, 26);
			this.chkbxMonday.Name = "chkbxMonday";
			this.chkbxMonday.Size = new System.Drawing.Size(64, 17);
			this.chkbxMonday.TabIndex = 1;
			this.chkbxMonday.Text = "Monday";
			this.chkbxMonday.UseVisualStyleBackColor = true;
			// 
			// chkbxTuesday
			// 
			this.chkbxTuesday.AutoSize = true;
			this.chkbxTuesday.Location = new System.Drawing.Point(3, 49);
			this.chkbxTuesday.Name = "chkbxTuesday";
			this.chkbxTuesday.Size = new System.Drawing.Size(67, 17);
			this.chkbxTuesday.TabIndex = 2;
			this.chkbxTuesday.Text = "Tuesday";
			this.chkbxTuesday.UseVisualStyleBackColor = true;
			// 
			// chkbxWednesday
			// 
			this.chkbxWednesday.AutoSize = true;
			this.chkbxWednesday.Location = new System.Drawing.Point(3, 72);
			this.chkbxWednesday.Name = "chkbxWednesday";
			this.chkbxWednesday.Size = new System.Drawing.Size(83, 17);
			this.chkbxWednesday.TabIndex = 3;
			this.chkbxWednesday.Text = "Wednesday";
			this.chkbxWednesday.UseVisualStyleBackColor = true;
			// 
			// chkbxThursday
			// 
			this.chkbxThursday.AutoSize = true;
			this.chkbxThursday.Location = new System.Drawing.Point(3, 95);
			this.chkbxThursday.Name = "chkbxThursday";
			this.chkbxThursday.Size = new System.Drawing.Size(70, 17);
			this.chkbxThursday.TabIndex = 4;
			this.chkbxThursday.Text = "Thursday";
			this.chkbxThursday.UseVisualStyleBackColor = true;
			// 
			// chkbxFriday
			// 
			this.chkbxFriday.AutoSize = true;
			this.chkbxFriday.Location = new System.Drawing.Point(3, 118);
			this.chkbxFriday.Name = "chkbxFriday";
			this.chkbxFriday.Size = new System.Drawing.Size(54, 17);
			this.chkbxFriday.TabIndex = 5;
			this.chkbxFriday.Text = "Friday";
			this.chkbxFriday.UseVisualStyleBackColor = true;
			// 
			// chkbxSaturday
			// 
			this.chkbxSaturday.AutoSize = true;
			this.chkbxSaturday.Location = new System.Drawing.Point(3, 141);
			this.chkbxSaturday.Name = "chkbxSaturday";
			this.chkbxSaturday.Size = new System.Drawing.Size(68, 17);
			this.chkbxSaturday.TabIndex = 6;
			this.chkbxSaturday.Text = "Saturday";
			this.chkbxSaturday.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(125, 70);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(66, 13);
			this.label2.TabIndex = 11;
			this.label2.Text = "Time Of Day";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(16, 70);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 13);
			this.label3.TabIndex = 12;
			this.label3.Text = "Day of the Week";
			// 
			// txtTimeOfDayHour
			// 
			this.txtTimeOfDayHour.Location = new System.Drawing.Point(3, 3);
			this.txtTimeOfDayHour.Name = "txtTimeOfDayHour";
			this.txtTimeOfDayHour.Size = new System.Drawing.Size(35, 20);
			this.txtTimeOfDayHour.TabIndex = 13;
			// 
			// txtTimeOfDayMinute
			// 
			this.txtTimeOfDayMinute.Location = new System.Drawing.Point(50, 3);
			this.txtTimeOfDayMinute.Name = "txtTimeOfDayMinute";
			this.txtTimeOfDayMinute.Size = new System.Drawing.Size(35, 20);
			this.txtTimeOfDayMinute.TabIndex = 14;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(39, 4);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(10, 13);
			this.label4.TabIndex = 15;
			this.label4.Text = ":";
			// 
			// radioAm
			// 
			this.radioAm.AutoSize = true;
			this.radioAm.Location = new System.Drawing.Point(8, 29);
			this.radioAm.Name = "radioAm";
			this.radioAm.Size = new System.Drawing.Size(41, 17);
			this.radioAm.TabIndex = 16;
			this.radioAm.TabStop = true;
			this.radioAm.Text = "AM";
			this.radioAm.UseVisualStyleBackColor = true;
			// 
			// radioPm
			// 
			this.radioPm.AutoSize = true;
			this.radioPm.Location = new System.Drawing.Point(8, 52);
			this.radioPm.Name = "radioPm";
			this.radioPm.Size = new System.Drawing.Size(41, 17);
			this.radioPm.TabIndex = 17;
			this.radioPm.TabStop = true;
			this.radioPm.Text = "PM";
			this.radioPm.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.radioPm);
			this.panel1.Controls.Add(this.txtTimeOfDayHour);
			this.panel1.Controls.Add(this.radioAm);
			this.panel1.Controls.Add(this.txtTimeOfDayMinute);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Location = new System.Drawing.Point(128, 86);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(89, 72);
			this.panel1.TabIndex = 10;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(234, 70);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(72, 13);
			this.label5.TabIndex = 13;
			this.label5.Text = "Repeat Every";
			// 
			// txtRepeatWeeks
			// 
			this.txtRepeatWeeks.Location = new System.Drawing.Point(3, 3);
			this.txtRepeatWeeks.Name = "txtRepeatWeeks";
			this.txtRepeatWeeks.Size = new System.Drawing.Size(33, 20);
			this.txtRepeatWeeks.TabIndex = 14;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(42, 6);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(44, 13);
			this.label6.TabIndex = 15;
			this.label6.Text = "week(s)";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.label6);
			this.panel2.Controls.Add(this.txtRepeatWeeks);
			this.panel2.Location = new System.Drawing.Point(237, 86);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(90, 28);
			this.panel2.TabIndex = 10;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.btnClearResults);
			this.groupBox4.Controls.Add(this.btnClearSearchSettings);
			this.groupBox4.Location = new System.Drawing.Point(12, 505);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(134, 105);
			this.groupBox4.TabIndex = 10;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Other Options";
			// 
			// AdjustSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(406, 622);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.flowLayoutPanel1);
			this.Name = "AdjustSettings";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Change User Settings";
			this.Shown += new System.EventHandler(this.AdjustSettings_Shown);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button btnApply;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.CheckBox chkbxEnableUrlFilter;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtPagesPerDomain;
		private System.Windows.Forms.Button btnClearSearchSettings;
		private System.Windows.Forms.Button btnClearResults;
		private System.Windows.Forms.CheckBox chkbxDateFound;
		private System.Windows.Forms.CheckBox chkbxNewResultsOnly;
		private System.Windows.Forms.CheckBox chkbxSendEmail;
		private System.Windows.Forms.CheckBox chkbxStrictFiltering;
		private System.Windows.Forms.CheckBox chkbxContextSensitive;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private System.Windows.Forms.CheckBox chkbxSunday;
		private System.Windows.Forms.CheckBox chkbxMonday;
		private System.Windows.Forms.CheckBox chkbxTuesday;
		private System.Windows.Forms.CheckBox chkbxWednesday;
		private System.Windows.Forms.CheckBox chkbxThursday;
		private System.Windows.Forms.CheckBox chkbxFriday;
		private System.Windows.Forms.CheckBox chkbxSaturday;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtRepeatWeeks;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RadioButton radioPm;
		private System.Windows.Forms.TextBox txtTimeOfDayHour;
		private System.Windows.Forms.RadioButton radioAm;
		private System.Windows.Forms.TextBox txtTimeOfDayMinute;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox4;
	}
}