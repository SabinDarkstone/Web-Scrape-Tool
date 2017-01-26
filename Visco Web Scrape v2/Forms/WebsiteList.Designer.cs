namespace Visco_Web_Scrape_v2.Forms {
	partial class WebsiteList {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebsiteList));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioOtherSource = new System.Windows.Forms.RadioButton();
			this.radioGrantSource = new System.Windows.Forms.RadioButton();
			this.btnRemoveWebsite = new System.Windows.Forms.Button();
			this.btnSaveWebsite = new System.Windows.Forms.Button();
			this.txtWebsiteUrl = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtWebsiteName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnAccept = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.chklistboxWebsites = new System.Windows.Forms.CheckedListBox();
			this.btnCheckAll = new System.Windows.Forms.Button();
			this.btnUncheckAll = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioOtherSource);
			this.groupBox1.Controls.Add(this.radioGrantSource);
			this.groupBox1.Controls.Add(this.btnRemoveWebsite);
			this.groupBox1.Controls.Add(this.btnSaveWebsite);
			this.groupBox1.Controls.Add(this.txtWebsiteUrl);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.txtWebsiteName);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(235, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(332, 155);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Website";
			// 
			// radioOtherSource
			// 
			this.radioOtherSource.AutoSize = true;
			this.radioOtherSource.Location = new System.Drawing.Point(87, 114);
			this.radioOtherSource.Name = "radioOtherSource";
			this.radioOtherSource.Size = new System.Drawing.Size(51, 17);
			this.radioOtherSource.TabIndex = 13;
			this.radioOtherSource.Text = "Other";
			this.radioOtherSource.UseVisualStyleBackColor = true;
			// 
			// radioGrantSource
			// 
			this.radioGrantSource.AutoSize = true;
			this.radioGrantSource.Checked = true;
			this.radioGrantSource.Location = new System.Drawing.Point(87, 91);
			this.radioGrantSource.Name = "radioGrantSource";
			this.radioGrantSource.Size = new System.Drawing.Size(88, 17);
			this.radioGrantSource.TabIndex = 12;
			this.radioGrantSource.TabStop = true;
			this.radioGrantSource.Text = "Grant Source";
			this.radioGrantSource.UseVisualStyleBackColor = true;
			// 
			// btnRemoveWebsite
			// 
			this.btnRemoveWebsite.Location = new System.Drawing.Point(191, 117);
			this.btnRemoveWebsite.Name = "btnRemoveWebsite";
			this.btnRemoveWebsite.Size = new System.Drawing.Size(75, 23);
			this.btnRemoveWebsite.TabIndex = 11;
			this.btnRemoveWebsite.Text = "Remove";
			this.btnRemoveWebsite.UseVisualStyleBackColor = true;
			this.btnRemoveWebsite.Click += new System.EventHandler(this.btnRemoveWebsite_Click);
			// 
			// btnSaveWebsite
			// 
			this.btnSaveWebsite.Location = new System.Drawing.Point(191, 88);
			this.btnSaveWebsite.Name = "btnSaveWebsite";
			this.btnSaveWebsite.Size = new System.Drawing.Size(75, 23);
			this.btnSaveWebsite.TabIndex = 10;
			this.btnSaveWebsite.Text = "Save";
			this.btnSaveWebsite.UseVisualStyleBackColor = true;
			this.btnSaveWebsite.Click += new System.EventHandler(this.btnSaveWebsite_Click);
			// 
			// txtWebsiteUrl
			// 
			this.txtWebsiteUrl.Location = new System.Drawing.Point(61, 51);
			this.txtWebsiteUrl.Name = "txtWebsiteUrl";
			this.txtWebsiteUrl.Size = new System.Drawing.Size(253, 20);
			this.txtWebsiteUrl.TabIndex = 8;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(23, 54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "URL:";
			// 
			// txtWebsiteName
			// 
			this.txtWebsiteName.Location = new System.Drawing.Point(61, 25);
			this.txtWebsiteName.Name = "txtWebsiteName";
			this.txtWebsiteName.Size = new System.Drawing.Size(169, 20);
			this.txtWebsiteName.TabIndex = 6;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(17, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Name:";
			// 
			// btnAccept
			// 
			this.btnAccept.Location = new System.Drawing.Point(347, 180);
			this.btnAccept.Name = "btnAccept";
			this.btnAccept.Size = new System.Drawing.Size(100, 23);
			this.btnAccept.TabIndex = 2;
			this.btnAccept.Text = "Accept Changes";
			this.btnAccept.UseVisualStyleBackColor = true;
			this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(347, 209);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(100, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// chklistboxWebsites
			// 
			this.chklistboxWebsites.FormattingEnabled = true;
			this.chklistboxWebsites.Location = new System.Drawing.Point(12, 12);
			this.chklistboxWebsites.Name = "chklistboxWebsites";
			this.chklistboxWebsites.Size = new System.Drawing.Size(207, 184);
			this.chklistboxWebsites.TabIndex = 4;
			this.chklistboxWebsites.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chklistboxWebsites_ItemCheck);
			this.chklistboxWebsites.SelectedIndexChanged += new System.EventHandler(this.chklistboxWebsites_SelectedIndexChanged);
			// 
			// btnCheckAll
			// 
			this.btnCheckAll.Location = new System.Drawing.Point(32, 209);
			this.btnCheckAll.Name = "btnCheckAll";
			this.btnCheckAll.Size = new System.Drawing.Size(85, 23);
			this.btnCheckAll.TabIndex = 5;
			this.btnCheckAll.Text = "Check All";
			this.btnCheckAll.UseVisualStyleBackColor = true;
			this.btnCheckAll.Click += new System.EventHandler(this.btnCheckAll_Click);
			// 
			// btnUncheckAll
			// 
			this.btnUncheckAll.Location = new System.Drawing.Point(123, 209);
			this.btnUncheckAll.Name = "btnUncheckAll";
			this.btnUncheckAll.Size = new System.Drawing.Size(85, 23);
			this.btnUncheckAll.TabIndex = 6;
			this.btnUncheckAll.Text = "Uncheck All";
			this.btnUncheckAll.UseVisualStyleBackColor = true;
			this.btnUncheckAll.Click += new System.EventHandler(this.btnUncheckAll_Click);
			// 
			// WebsiteList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(588, 243);
			this.Controls.Add(this.btnUncheckAll);
			this.Controls.Add(this.btnCheckAll);
			this.Controls.Add(this.chklistboxWebsites);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnAccept);
			this.Controls.Add(this.groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "WebsiteList";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Manage Website List";
			this.Shown += new System.EventHandler(this.WebsiteList_Shown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnRemoveWebsite;
		private System.Windows.Forms.Button btnSaveWebsite;
		private System.Windows.Forms.TextBox txtWebsiteUrl;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtWebsiteName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnAccept;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.CheckedListBox chklistboxWebsites;
		private System.Windows.Forms.RadioButton radioOtherSource;
		private System.Windows.Forms.RadioButton radioGrantSource;
		private System.Windows.Forms.Button btnCheckAll;
		private System.Windows.Forms.Button btnUncheckAll;
	}
}