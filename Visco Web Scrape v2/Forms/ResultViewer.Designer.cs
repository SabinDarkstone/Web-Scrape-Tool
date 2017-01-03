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
			this.progressBook = new System.Windows.Forms.ProgressBar();
			this.progressSheet = new System.Windows.Forms.ProgressBar();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(11, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(356, 115);
			this.label1.TabIndex = 0;
			this.label1.Text = "This feature is not yet fully implemented.  The results are temporarily being exp" +
    "orted to an excel file for viewing.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnStatusClose
			// 
			this.btnStatusClose.Location = new System.Drawing.Point(153, 230);
			this.btnStatusClose.Name = "btnStatusClose";
			this.btnStatusClose.Size = new System.Drawing.Size(75, 23);
			this.btnStatusClose.TabIndex = 1;
			this.btnStatusClose.Text = "Close";
			this.btnStatusClose.UseVisualStyleBackColor = true;
			this.btnStatusClose.Click += new System.EventHandler(this.btnStatusClose_Click);
			// 
			// progressBook
			// 
			this.progressBook.Location = new System.Drawing.Point(102, 156);
			this.progressBook.Name = "progressBook";
			this.progressBook.Size = new System.Drawing.Size(239, 23);
			this.progressBook.TabIndex = 2;
			// 
			// progressSheet
			// 
			this.progressSheet.Location = new System.Drawing.Point(102, 185);
			this.progressSheet.Name = "progressSheet";
			this.progressSheet.Size = new System.Drawing.Size(239, 23);
			this.progressSheet.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(21, 161);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(75, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Total Progress";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(24, 190);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Current Sheet";
			// 
			// ResultViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(379, 269);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.progressSheet);
			this.Controls.Add(this.progressBook);
			this.Controls.Add(this.btnStatusClose);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ResultViewer";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "View Search Results";
			this.Shown += new System.EventHandler(this.ResultViewer_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnStatusClose;
		private System.Windows.Forms.ProgressBar progressBook;
		private System.Windows.Forms.ProgressBar progressSheet;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
	}
}