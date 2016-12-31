namespace Visco_Web_Scrape_v2.Forms {
	partial class ResultViewer {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultViewer));
			this.label1 = new System.Windows.Forms.Label();
			this.btnStatusClose = new System.Windows.Forms.Button();
			this.progressbar = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(11, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(281, 139);
			this.label1.TabIndex = 0;
			this.label1.Text = "This feature is not yet fully implemented.  The results are temporarily being exp" +
    "orted to an excel file for viewing.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnStatusClose
			// 
			this.btnStatusClose.Location = new System.Drawing.Point(114, 215);
			this.btnStatusClose.Name = "btnStatusClose";
			this.btnStatusClose.Size = new System.Drawing.Size(75, 23);
			this.btnStatusClose.TabIndex = 1;
			this.btnStatusClose.Text = "Close";
			this.btnStatusClose.UseVisualStyleBackColor = true;
			this.btnStatusClose.Click += new System.EventHandler(this.btnStatusClose_Click);
			// 
			// progressbar
			// 
			this.progressbar.Location = new System.Drawing.Point(32, 174);
			this.progressbar.Name = "progressbar";
			this.progressbar.Size = new System.Drawing.Size(239, 23);
			this.progressbar.TabIndex = 2;
			// 
			// ResultViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(304, 250);
			this.Controls.Add(this.progressbar);
			this.Controls.Add(this.btnStatusClose);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ResultViewer";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "View Search Results";
			this.Shown += new System.EventHandler(this.ResultViewer_Shown);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnStatusClose;
		private System.Windows.Forms.ProgressBar progressbar;
	}
}