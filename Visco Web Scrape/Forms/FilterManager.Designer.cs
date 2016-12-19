namespace Visco_Web_Scrape.Forms {
	partial class FilterManager {
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.btnAcceptChanges = new System.Windows.Forms.Button();
			this.btnCancelChanges = new System.Windows.Forms.Button();
			this.listboxKeywordsStepOne = new System.Windows.Forms.ListBox();
			this.btnStepOneAdd = new System.Windows.Forms.Button();
			this.btnStepOneRemove = new System.Windows.Forms.Button();
			this.txtStepOneKeyword = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtStepTwoKeyword = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtStepThreeKeyword = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.btnStepOneHelp = new System.Windows.Forms.Button();
			this.btnStepTwoHelp = new System.Windows.Forms.Button();
			this.btnStepTwoRemove = new System.Windows.Forms.Button();
			this.btnStepTwoAdd = new System.Windows.Forms.Button();
			this.listboxKeywordsStepTwo = new System.Windows.Forms.ListBox();
			this.btnStepThreeHelp = new System.Windows.Forms.Button();
			this.btnStepThreeRemove = new System.Windows.Forms.Button();
			this.btnStepThreeAdd = new System.Windows.Forms.Button();
			this.listboxKeywordsStepThree = new System.Windows.Forms.ListBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnStepOneHelp);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtStepOneKeyword);
			this.groupBox1.Controls.Add(this.btnStepOneRemove);
			this.groupBox1.Controls.Add(this.btnStepOneAdd);
			this.groupBox1.Controls.Add(this.listboxKeywordsStepOne);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(358, 153);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Step 1: Relevancy";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.btnStepTwoHelp);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.btnStepTwoRemove);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.btnStepTwoAdd);
			this.groupBox2.Controls.Add(this.txtStepTwoKeyword);
			this.groupBox2.Controls.Add(this.listboxKeywordsStepTwo);
			this.groupBox2.Location = new System.Drawing.Point(12, 171);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(358, 153);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Step 2: Inclusion";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.btnStepThreeHelp);
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Controls.Add(this.btnStepThreeRemove);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this.btnStepThreeAdd);
			this.groupBox3.Controls.Add(this.txtStepThreeKeyword);
			this.groupBox3.Controls.Add(this.listboxKeywordsStepThree);
			this.groupBox3.Location = new System.Drawing.Point(12, 330);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(358, 153);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Step 3: Exclusion";
			// 
			// btnAcceptChanges
			// 
			this.btnAcceptChanges.Location = new System.Drawing.Point(111, 499);
			this.btnAcceptChanges.Name = "btnAcceptChanges";
			this.btnAcceptChanges.Size = new System.Drawing.Size(75, 23);
			this.btnAcceptChanges.TabIndex = 3;
			this.btnAcceptChanges.Text = "Accept";
			this.btnAcceptChanges.UseVisualStyleBackColor = true;
			this.btnAcceptChanges.Click += new System.EventHandler(this.btnAcceptChanges_Click);
			// 
			// btnCancelChanges
			// 
			this.btnCancelChanges.Location = new System.Drawing.Point(192, 499);
			this.btnCancelChanges.Name = "btnCancelChanges";
			this.btnCancelChanges.Size = new System.Drawing.Size(75, 23);
			this.btnCancelChanges.TabIndex = 4;
			this.btnCancelChanges.Text = "Cancel";
			this.btnCancelChanges.UseVisualStyleBackColor = true;
			this.btnCancelChanges.Click += new System.EventHandler(this.btnCancelChanges_Click);
			// 
			// listboxKeywordsStepOne
			// 
			this.listboxKeywordsStepOne.FormattingEnabled = true;
			this.listboxKeywordsStepOne.Location = new System.Drawing.Point(6, 29);
			this.listboxKeywordsStepOne.Name = "listboxKeywordsStepOne";
			this.listboxKeywordsStepOne.Size = new System.Drawing.Size(138, 108);
			this.listboxKeywordsStepOne.TabIndex = 0;
			this.listboxKeywordsStepOne.Tag = "KeywordType.Relevance";
			// 
			// btnStepOneAdd
			// 
			this.btnStepOneAdd.Location = new System.Drawing.Point(150, 42);
			this.btnStepOneAdd.Name = "btnStepOneAdd";
			this.btnStepOneAdd.Size = new System.Drawing.Size(22, 23);
			this.btnStepOneAdd.TabIndex = 1;
			this.btnStepOneAdd.Text = "+";
			this.btnStepOneAdd.UseVisualStyleBackColor = true;
			this.btnStepOneAdd.Click += new System.EventHandler(this.button_Click);
			// 
			// btnStepOneRemove
			// 
			this.btnStepOneRemove.Location = new System.Drawing.Point(150, 71);
			this.btnStepOneRemove.Name = "btnStepOneRemove";
			this.btnStepOneRemove.Size = new System.Drawing.Size(22, 23);
			this.btnStepOneRemove.TabIndex = 2;
			this.btnStepOneRemove.Text = "-";
			this.btnStepOneRemove.UseVisualStyleBackColor = true;
			this.btnStepOneRemove.Click += new System.EventHandler(this.button_Click);
			// 
			// txtStepOneKeyword
			// 
			this.txtStepOneKeyword.Location = new System.Drawing.Point(189, 117);
			this.txtStepOneKeyword.Name = "txtStepOneKeyword";
			this.txtStepOneKeyword.Size = new System.Drawing.Size(133, 20);
			this.txtStepOneKeyword.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(186, 101);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(76, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Enter Keyword";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(189, 102);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(76, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Enter Keyword";
			// 
			// txtStepTwoKeyword
			// 
			this.txtStepTwoKeyword.Location = new System.Drawing.Point(192, 118);
			this.txtStepTwoKeyword.Name = "txtStepTwoKeyword";
			this.txtStepTwoKeyword.Size = new System.Drawing.Size(133, 20);
			this.txtStepTwoKeyword.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(192, 101);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(76, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "Enter Keyword";
			// 
			// txtStepThreeKeyword
			// 
			this.txtStepThreeKeyword.Location = new System.Drawing.Point(195, 117);
			this.txtStepThreeKeyword.Name = "txtStepThreeKeyword";
			this.txtStepThreeKeyword.Size = new System.Drawing.Size(133, 20);
			this.txtStepThreeKeyword.TabIndex = 7;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(189, 20);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(159, 81);
			this.label4.TabIndex = 5;
			this.label4.Text = "This first step is used by the crawler at a cursory level to descide which pages " +
    "are relevant to the search and which ones are not.";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(192, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(156, 81);
			this.label5.TabIndex = 6;
			this.label5.Text = "The second step is used by the scraper to determine what content in the page is s" +
    "pecifically useful.";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(195, 16);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(156, 81);
			this.label6.TabIndex = 7;
			this.label6.Text = "The third step takes all of the information collected by the scraper and remove a" +
    "nything that is not of interest.";
			// 
			// btnStepOneHelp
			// 
			this.btnStepOneHelp.Location = new System.Drawing.Point(150, 100);
			this.btnStepOneHelp.Name = "btnStepOneHelp";
			this.btnStepOneHelp.Size = new System.Drawing.Size(22, 23);
			this.btnStepOneHelp.TabIndex = 6;
			this.btnStepOneHelp.Text = "?";
			this.btnStepOneHelp.UseVisualStyleBackColor = true;
			this.btnStepOneHelp.Click += new System.EventHandler(this.button_Click);
			// 
			// btnStepTwoHelp
			// 
			this.btnStepTwoHelp.Location = new System.Drawing.Point(150, 101);
			this.btnStepTwoHelp.Name = "btnStepTwoHelp";
			this.btnStepTwoHelp.Size = new System.Drawing.Size(22, 23);
			this.btnStepTwoHelp.TabIndex = 10;
			this.btnStepTwoHelp.Text = "?";
			this.btnStepTwoHelp.UseVisualStyleBackColor = true;
			this.btnStepTwoHelp.Click += new System.EventHandler(this.button_Click);
			// 
			// btnStepTwoRemove
			// 
			this.btnStepTwoRemove.Location = new System.Drawing.Point(150, 72);
			this.btnStepTwoRemove.Name = "btnStepTwoRemove";
			this.btnStepTwoRemove.Size = new System.Drawing.Size(22, 23);
			this.btnStepTwoRemove.TabIndex = 9;
			this.btnStepTwoRemove.Text = "-";
			this.btnStepTwoRemove.UseVisualStyleBackColor = true;
			this.btnStepTwoRemove.Click += new System.EventHandler(this.button_Click);
			// 
			// btnStepTwoAdd
			// 
			this.btnStepTwoAdd.Location = new System.Drawing.Point(150, 43);
			this.btnStepTwoAdd.Name = "btnStepTwoAdd";
			this.btnStepTwoAdd.Size = new System.Drawing.Size(22, 23);
			this.btnStepTwoAdd.TabIndex = 8;
			this.btnStepTwoAdd.Text = "+";
			this.btnStepTwoAdd.UseVisualStyleBackColor = true;
			this.btnStepTwoAdd.Click += new System.EventHandler(this.button_Click);
			// 
			// listboxKeywordsStepTwo
			// 
			this.listboxKeywordsStepTwo.FormattingEnabled = true;
			this.listboxKeywordsStepTwo.Location = new System.Drawing.Point(6, 30);
			this.listboxKeywordsStepTwo.Name = "listboxKeywordsStepTwo";
			this.listboxKeywordsStepTwo.Size = new System.Drawing.Size(138, 108);
			this.listboxKeywordsStepTwo.TabIndex = 7;
			this.listboxKeywordsStepTwo.Tag = "KeywordType.Inclusion";
			// 
			// btnStepThreeHelp
			// 
			this.btnStepThreeHelp.Location = new System.Drawing.Point(150, 100);
			this.btnStepThreeHelp.Name = "btnStepThreeHelp";
			this.btnStepThreeHelp.Size = new System.Drawing.Size(22, 23);
			this.btnStepThreeHelp.TabIndex = 14;
			this.btnStepThreeHelp.Text = "?";
			this.btnStepThreeHelp.UseVisualStyleBackColor = true;
			this.btnStepThreeHelp.Click += new System.EventHandler(this.button_Click);
			// 
			// btnStepThreeRemove
			// 
			this.btnStepThreeRemove.Location = new System.Drawing.Point(150, 71);
			this.btnStepThreeRemove.Name = "btnStepThreeRemove";
			this.btnStepThreeRemove.Size = new System.Drawing.Size(22, 23);
			this.btnStepThreeRemove.TabIndex = 13;
			this.btnStepThreeRemove.Text = "-";
			this.btnStepThreeRemove.UseVisualStyleBackColor = true;
			this.btnStepThreeRemove.Click += new System.EventHandler(this.button_Click);
			// 
			// btnStepThreeAdd
			// 
			this.btnStepThreeAdd.Location = new System.Drawing.Point(150, 42);
			this.btnStepThreeAdd.Name = "btnStepThreeAdd";
			this.btnStepThreeAdd.Size = new System.Drawing.Size(22, 23);
			this.btnStepThreeAdd.TabIndex = 12;
			this.btnStepThreeAdd.Text = "+";
			this.btnStepThreeAdd.UseVisualStyleBackColor = true;
			this.btnStepThreeAdd.Click += new System.EventHandler(this.button_Click);
			// 
			// listboxKeywordsStepThree
			// 
			this.listboxKeywordsStepThree.FormattingEnabled = true;
			this.listboxKeywordsStepThree.Location = new System.Drawing.Point(6, 29);
			this.listboxKeywordsStepThree.Name = "listboxKeywordsStepThree";
			this.listboxKeywordsStepThree.Size = new System.Drawing.Size(138, 108);
			this.listboxKeywordsStepThree.TabIndex = 11;
			this.listboxKeywordsStepThree.Tag = "KeywordType.Exclusion";
			// 
			// FilterManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(381, 536);
			this.Controls.Add(this.btnCancelChanges);
			this.Controls.Add(this.btnAcceptChanges);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "FilterManager";
			this.Text = "FilterManager";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListBox listboxKeywordsStepOne;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button btnAcceptChanges;
		private System.Windows.Forms.Button btnCancelChanges;
		private System.Windows.Forms.Button btnStepOneHelp;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtStepOneKeyword;
		private System.Windows.Forms.Button btnStepOneRemove;
		private System.Windows.Forms.Button btnStepOneAdd;
		private System.Windows.Forms.Button btnStepTwoHelp;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnStepTwoRemove;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnStepTwoAdd;
		private System.Windows.Forms.TextBox txtStepTwoKeyword;
		private System.Windows.Forms.ListBox listboxKeywordsStepTwo;
		private System.Windows.Forms.Button btnStepThreeHelp;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button btnStepThreeRemove;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnStepThreeAdd;
		private System.Windows.Forms.TextBox txtStepThreeKeyword;
		private System.Windows.Forms.ListBox listboxKeywordsStepThree;
	}
}