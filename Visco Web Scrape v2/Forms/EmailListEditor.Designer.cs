namespace Visco_Web_Scrape_v2.Forms {
	partial class EmailListEditor {
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
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnAccept = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnRemoveRecipient = new System.Windows.Forms.Button();
			this.btnSaveRecipient = new System.Windows.Forms.Button();
			this.txtRecipientAddress = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtRecipientName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.listboxEmailAddresses = new System.Windows.Forms.ListBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(349, 188);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(100, 23);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnAccept
			// 
			this.btnAccept.Location = new System.Drawing.Point(349, 159);
			this.btnAccept.Name = "btnAccept";
			this.btnAccept.Size = new System.Drawing.Size(100, 23);
			this.btnAccept.TabIndex = 6;
			this.btnAccept.Text = "Accept Changes";
			this.btnAccept.UseVisualStyleBackColor = true;
			this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnRemoveRecipient);
			this.groupBox1.Controls.Add(this.btnSaveRecipient);
			this.groupBox1.Controls.Add(this.txtRecipientAddress);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.txtRecipientName);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(235, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(332, 130);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Recipient";
			// 
			// btnRemoveRecipient
			// 
			this.btnRemoveRecipient.Location = new System.Drawing.Point(157, 87);
			this.btnRemoveRecipient.Name = "btnRemoveRecipient";
			this.btnRemoveRecipient.Size = new System.Drawing.Size(75, 23);
			this.btnRemoveRecipient.TabIndex = 11;
			this.btnRemoveRecipient.Text = "Remove";
			this.btnRemoveRecipient.UseVisualStyleBackColor = true;
			this.btnRemoveRecipient.Click += new System.EventHandler(this.btnRemoveRecipient_Click);
			// 
			// btnSaveRecipient
			// 
			this.btnSaveRecipient.Location = new System.Drawing.Point(76, 87);
			this.btnSaveRecipient.Name = "btnSaveRecipient";
			this.btnSaveRecipient.Size = new System.Drawing.Size(75, 23);
			this.btnSaveRecipient.TabIndex = 10;
			this.btnSaveRecipient.Text = "Save";
			this.btnSaveRecipient.UseVisualStyleBackColor = true;
			this.btnSaveRecipient.Click += new System.EventHandler(this.btnSaveRecipient_Click);
			// 
			// txtRecipientAddress
			// 
			this.txtRecipientAddress.Location = new System.Drawing.Point(61, 51);
			this.txtRecipientAddress.Name = "txtRecipientAddress";
			this.txtRecipientAddress.Size = new System.Drawing.Size(253, 20);
			this.txtRecipientAddress.TabIndex = 8;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(20, 54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Email:";
			// 
			// txtRecipientName
			// 
			this.txtRecipientName.Location = new System.Drawing.Point(61, 25);
			this.txtRecipientName.Name = "txtRecipientName";
			this.txtRecipientName.Size = new System.Drawing.Size(169, 20);
			this.txtRecipientName.TabIndex = 6;
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
			// listboxEmailAddresses
			// 
			this.listboxEmailAddresses.FormattingEnabled = true;
			this.listboxEmailAddresses.Location = new System.Drawing.Point(12, 12);
			this.listboxEmailAddresses.Name = "listboxEmailAddresses";
			this.listboxEmailAddresses.Size = new System.Drawing.Size(207, 212);
			this.listboxEmailAddresses.TabIndex = 4;
			this.listboxEmailAddresses.SelectedIndexChanged += new System.EventHandler(this.listboxEmailAddresses_SelectedIndexChanged);
			// 
			// EmailListEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(578, 236);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnAccept);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.listboxEmailAddresses);
			this.Name = "EmailListEditor";
			this.Text = "Email List Editor";
			this.Shown += new System.EventHandler(this.EmailListEditor_Shown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnAccept;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnRemoveRecipient;
		private System.Windows.Forms.Button btnSaveRecipient;
		private System.Windows.Forms.TextBox txtRecipientAddress;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtRecipientName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox listboxEmailAddresses;
	}
}