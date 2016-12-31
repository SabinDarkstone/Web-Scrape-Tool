namespace Visco_Web_Scrape_v2.Forms {
	partial class EmailProgress {
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
			this.label1 = new System.Windows.Forms.Label();
			this.lblCurrentStatus = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lblRecipientCount = new System.Windows.Forms.Label();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(28, 52);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Status:";
			// 
			// lblCurrentStatus
			// 
			this.lblCurrentStatus.AutoSize = true;
			this.lblCurrentStatus.Location = new System.Drawing.Point(146, 52);
			this.lblCurrentStatus.Name = "lblCurrentStatus";
			this.lblCurrentStatus.Size = new System.Drawing.Size(47, 13);
			this.lblCurrentStatus.TabIndex = 1;
			this.lblCurrentStatus.Text = "<status>";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(28, 23);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Number of Recipients:";
			// 
			// lblRecipientCount
			// 
			this.lblRecipientCount.AutoSize = true;
			this.lblRecipientCount.Location = new System.Drawing.Point(146, 23);
			this.lblRecipientCount.Name = "lblRecipientCount";
			this.lblRecipientCount.Size = new System.Drawing.Size(89, 13);
			this.lblRecipientCount.TabIndex = 3;
			this.lblRecipientCount.Text = "<recipient count>";
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(31, 88);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(204, 23);
			this.progressBar1.TabIndex = 4;
			// 
			// EmailProgress
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(270, 135);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.lblRecipientCount);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.lblCurrentStatus);
			this.Controls.Add(this.label1);
			this.Name = "EmailProgress";
			this.Text = "EmailProgress";
			this.Shown += new System.EventHandler(this.EmailProgress_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblCurrentStatus;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblRecipientCount;
		private System.Windows.Forms.ProgressBar progressBar1;
	}
}