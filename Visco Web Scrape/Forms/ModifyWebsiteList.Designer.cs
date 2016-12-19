namespace Visco_Web_Scrape.Forms {
	partial class ModifyWebsiteList {
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
			this.listboxWebsites = new System.Windows.Forms.ListBox();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.groupboxWebsiteDetails = new System.Windows.Forms.GroupBox();
			this.txtWebsiteName = new System.Windows.Forms.TextBox();
			this.txtWebsiteUrl = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnModify = new System.Windows.Forms.Button();
			this.groupboxWebsiteDetails.SuspendLayout();
			this.SuspendLayout();
			// 
			// listboxWebsites
			// 
			this.listboxWebsites.FormattingEnabled = true;
			this.listboxWebsites.Location = new System.Drawing.Point(12, 12);
			this.listboxWebsites.Name = "listboxWebsites";
			this.listboxWebsites.Size = new System.Drawing.Size(175, 95);
			this.listboxWebsites.TabIndex = 0;
			this.listboxWebsites.SelectedIndexChanged += new System.EventHandler(this.listboxWebsites_SelectedIndexChanged);
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(199, 20);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(75, 23);
			this.btnAdd.TabIndex = 1;
			this.btnAdd.Text = "Add";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnRemove
			// 
			this.btnRemove.Location = new System.Drawing.Point(199, 49);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(75, 23);
			this.btnRemove.TabIndex = 2;
			this.btnRemove.Text = "Remove";
			this.btnRemove.UseVisualStyleBackColor = true;
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// groupboxWebsiteDetails
			// 
			this.groupboxWebsiteDetails.Controls.Add(this.txtWebsiteName);
			this.groupboxWebsiteDetails.Controls.Add(this.txtWebsiteUrl);
			this.groupboxWebsiteDetails.Controls.Add(this.label2);
			this.groupboxWebsiteDetails.Controls.Add(this.label1);
			this.groupboxWebsiteDetails.Location = new System.Drawing.Point(12, 119);
			this.groupboxWebsiteDetails.Name = "groupboxWebsiteDetails";
			this.groupboxWebsiteDetails.Size = new System.Drawing.Size(273, 89);
			this.groupboxWebsiteDetails.TabIndex = 3;
			this.groupboxWebsiteDetails.TabStop = false;
			this.groupboxWebsiteDetails.Text = "Edit Website";
			// 
			// txtWebsiteName
			// 
			this.txtWebsiteName.Location = new System.Drawing.Point(55, 51);
			this.txtWebsiteName.Name = "txtWebsiteName";
			this.txtWebsiteName.Size = new System.Drawing.Size(139, 20);
			this.txtWebsiteName.TabIndex = 3;
			// 
			// txtWebsiteUrl
			// 
			this.txtWebsiteUrl.Location = new System.Drawing.Point(55, 26);
			this.txtWebsiteUrl.Name = "txtWebsiteUrl";
			this.txtWebsiteUrl.Size = new System.Drawing.Size(201, 20);
			this.txtWebsiteUrl.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(14, 54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Name";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(17, 29);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "URL:";
			// 
			// btnModify
			// 
			this.btnModify.Location = new System.Drawing.Point(199, 78);
			this.btnModify.Name = "btnModify";
			this.btnModify.Size = new System.Drawing.Size(75, 23);
			this.btnModify.TabIndex = 4;
			this.btnModify.Text = "Modify";
			this.btnModify.UseVisualStyleBackColor = true;
			this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
			// 
			// ModifyWebsiteList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(295, 220);
			this.Controls.Add(this.btnModify);
			this.Controls.Add(this.groupboxWebsiteDetails);
			this.Controls.Add(this.btnRemove);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.listboxWebsites);
			this.Name = "ModifyWebsiteList";
			this.Text = "ModifyWebsiteList";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModifyWebsiteList_FormClosing);
			this.Load += new System.EventHandler(this.ModifyWebsiteList_Load);
			this.groupboxWebsiteDetails.ResumeLayout(false);
			this.groupboxWebsiteDetails.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox listboxWebsites;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.GroupBox groupboxWebsiteDetails;
		private System.Windows.Forms.TextBox txtWebsiteName;
		private System.Windows.Forms.TextBox txtWebsiteUrl;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnModify;
	}
}