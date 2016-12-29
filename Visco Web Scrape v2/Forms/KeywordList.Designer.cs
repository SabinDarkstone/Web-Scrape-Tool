﻿namespace Visco_Web_Scrape_v2.Forms {
	partial class KeywordList {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeywordList));
			this.listboxKeywordList = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtKeywordText = new System.Windows.Forms.TextBox();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioExclude = new System.Windows.Forms.RadioButton();
			this.radioInclude = new System.Windows.Forms.RadioButton();
			this.btnAccept = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listboxKeywordList
			// 
			this.listboxKeywordList.FormattingEnabled = true;
			this.listboxKeywordList.Location = new System.Drawing.Point(12, 12);
			this.listboxKeywordList.Name = "listboxKeywordList";
			this.listboxKeywordList.Size = new System.Drawing.Size(202, 225);
			this.listboxKeywordList.TabIndex = 0;
			this.listboxKeywordList.SelectedIndexChanged += new System.EventHandler(this.listboxKeywordList_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(17, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(28, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Text";
			// 
			// txtKeywordText
			// 
			this.txtKeywordText.Location = new System.Drawing.Point(51, 27);
			this.txtKeywordText.Name = "txtKeywordText";
			this.txtKeywordText.Size = new System.Drawing.Size(157, 20);
			this.txtKeywordText.TabIndex = 2;
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(34, 110);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 3;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnRemove
			// 
			this.btnRemove.Location = new System.Drawing.Point(115, 110);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(75, 23);
			this.btnRemove.TabIndex = 4;
			this.btnRemove.Text = "Remove";
			this.btnRemove.UseVisualStyleBackColor = true;
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioExclude);
			this.groupBox1.Controls.Add(this.radioInclude);
			this.groupBox1.Controls.Add(this.btnSave);
			this.groupBox1.Controls.Add(this.btnRemove);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtKeywordText);
			this.groupBox1.Location = new System.Drawing.Point(220, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(224, 145);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Keyword";
			// 
			// radioExclude
			// 
			this.radioExclude.AutoSize = true;
			this.radioExclude.Location = new System.Drawing.Point(81, 81);
			this.radioExclude.Name = "radioExclude";
			this.radioExclude.Size = new System.Drawing.Size(63, 17);
			this.radioExclude.TabIndex = 6;
			this.radioExclude.Text = "Exclude";
			this.radioExclude.UseVisualStyleBackColor = true;
			// 
			// radioInclude
			// 
			this.radioInclude.AutoSize = true;
			this.radioInclude.Checked = true;
			this.radioInclude.Location = new System.Drawing.Point(81, 58);
			this.radioInclude.Name = "radioInclude";
			this.radioInclude.Size = new System.Drawing.Size(60, 17);
			this.radioInclude.TabIndex = 5;
			this.radioInclude.TabStop = true;
			this.radioInclude.Text = "Include";
			this.radioInclude.UseVisualStyleBackColor = true;
			// 
			// btnAccept
			// 
			this.btnAccept.Location = new System.Drawing.Point(296, 173);
			this.btnAccept.Name = "btnAccept";
			this.btnAccept.Size = new System.Drawing.Size(75, 23);
			this.btnAccept.TabIndex = 6;
			this.btnAccept.Text = "Accept";
			this.btnAccept.UseVisualStyleBackColor = true;
			this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(296, 202);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// KeywordList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(463, 252);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnAccept);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.listboxKeywordList);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "KeywordList";
			this.Text = "Manage Keywords";
			this.Shown += new System.EventHandler(this.KeywordList_Shown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox listboxKeywordList;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtKeywordText;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioExclude;
		private System.Windows.Forms.RadioButton radioInclude;
		private System.Windows.Forms.Button btnAccept;
		private System.Windows.Forms.Button btnCancel;
	}
}