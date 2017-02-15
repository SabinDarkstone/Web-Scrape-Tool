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
			this.btnStatusClose = new System.Windows.Forms.Button();
			this.progressBook = new System.Windows.Forms.ProgressBar();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnStatusClose
			// 
			this.btnStatusClose.Location = new System.Drawing.Point(149, 71);
			this.btnStatusClose.Name = "btnStatusClose";
			this.btnStatusClose.Size = new System.Drawing.Size(75, 23);
			this.btnStatusClose.TabIndex = 1;
			this.btnStatusClose.Text = "Close";
			this.btnStatusClose.UseVisualStyleBackColor = true;
			// 
			// progressBook
			// 
			this.progressBook.Location = new System.Drawing.Point(29, 42);
			this.progressBook.Name = "progressBook";
			this.progressBook.Size = new System.Drawing.Size(314, 23);
			this.progressBook.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progressBook.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(127, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(119, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Exporting, please wait...";
			// 
			// ResultViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(379, 106);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.progressBook);
			this.Controls.Add(this.btnStatusClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ResultViewer";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "View Search Results";
			this.Load += new System.EventHandler(this.ResultViewer_Load);
			this.Shown += new System.EventHandler(this.ResultViewer_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button btnStatusClose;
		private System.Windows.Forms.Label label1;
		public System.Windows.Forms.ProgressBar progressBook;
	}
}