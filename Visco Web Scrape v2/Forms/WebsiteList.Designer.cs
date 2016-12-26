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
			this.listboxWebsites = new System.Windows.Forms.ListBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnRemoveWebsite = new System.Windows.Forms.Button();
			this.btnSaveWebsite = new System.Windows.Forms.Button();
			this.txtWebsiteUrl = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtWebsiteName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnAccept = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listboxWebsites
			// 
			this.listboxWebsites.FormattingEnabled = true;
			this.listboxWebsites.Location = new System.Drawing.Point(12, 12);
			this.listboxWebsites.Name = "listboxWebsites";
			this.listboxWebsites.Size = new System.Drawing.Size(207, 212);
			this.listboxWebsites.TabIndex = 0;
			this.listboxWebsites.SelectedIndexChanged += new System.EventHandler(this.listboxWebsites_SelectedIndexChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnRemoveWebsite);
			this.groupBox1.Controls.Add(this.btnSaveWebsite);
			this.groupBox1.Controls.Add(this.txtWebsiteUrl);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.txtWebsiteName);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(235, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(332, 130);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Website";
			// 
			// btnRemoveWebsite
			// 
			this.btnRemoveWebsite.Location = new System.Drawing.Point(157, 87);
			this.btnRemoveWebsite.Name = "btnRemoveWebsite";
			this.btnRemoveWebsite.Size = new System.Drawing.Size(75, 23);
			this.btnRemoveWebsite.TabIndex = 11;
			this.btnRemoveWebsite.Text = "Remove";
			this.btnRemoveWebsite.UseVisualStyleBackColor = true;
			this.btnRemoveWebsite.Click += new System.EventHandler(this.btnRemoveWebsite_Click);
			// 
			// btnSaveWebsite
			// 
			this.btnSaveWebsite.Location = new System.Drawing.Point(76, 87);
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
			this.btnAccept.Location = new System.Drawing.Point(349, 159);
			this.btnAccept.Name = "btnAccept";
			this.btnAccept.Size = new System.Drawing.Size(100, 23);
			this.btnAccept.TabIndex = 2;
			this.btnAccept.Text = "Accept Changes";
			this.btnAccept.UseVisualStyleBackColor = true;
			this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(349, 188);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(100, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// WebsiteList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(588, 238);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnAccept);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.listboxWebsites);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "WebsiteList";
			this.Text = "Manage Website List";
			this.Shown += new System.EventHandler(this.WebsiteList_Shown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox listboxWebsites;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnRemoveWebsite;
		private System.Windows.Forms.Button btnSaveWebsite;
		private System.Windows.Forms.TextBox txtWebsiteUrl;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtWebsiteName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnAccept;
		private System.Windows.Forms.Button btnCancel;
	}
}