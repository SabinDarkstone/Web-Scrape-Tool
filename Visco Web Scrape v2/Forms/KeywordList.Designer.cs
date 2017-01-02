namespace Visco_Web_Scrape_v2.Forms {
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
			this.label1 = new System.Windows.Forms.Label();
			this.txtKeywordText = new System.Windows.Forms.TextBox();
			this.btnSaveKeyword = new System.Windows.Forms.Button();
			this.btnRemoveKeyword = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chklistboxKeywords = new System.Windows.Forms.CheckedListBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnSaveWord = new System.Windows.Forms.Button();
			this.btnRemoveWord = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.txtUrlWord = new System.Windows.Forms.TextBox();
			this.chklistboxBlacklist = new System.Windows.Forms.CheckedListBox();
			this.btnAcceptChanges = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.groupBox1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
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
			// btnSaveKeyword
			// 
			this.btnSaveKeyword.Location = new System.Drawing.Point(34, 71);
			this.btnSaveKeyword.Name = "btnSaveKeyword";
			this.btnSaveKeyword.Size = new System.Drawing.Size(75, 23);
			this.btnSaveKeyword.TabIndex = 3;
			this.btnSaveKeyword.Text = "Save";
			this.btnSaveKeyword.UseVisualStyleBackColor = true;
			this.btnSaveKeyword.Click += new System.EventHandler(this.btnSaveKeyword_Click);
			// 
			// btnRemoveKeyword
			// 
			this.btnRemoveKeyword.Location = new System.Drawing.Point(115, 71);
			this.btnRemoveKeyword.Name = "btnRemoveKeyword";
			this.btnRemoveKeyword.Size = new System.Drawing.Size(75, 23);
			this.btnRemoveKeyword.TabIndex = 4;
			this.btnRemoveKeyword.Text = "Remove";
			this.btnRemoveKeyword.UseVisualStyleBackColor = true;
			this.btnRemoveKeyword.Click += new System.EventHandler(this.btnRemoveKeyword_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnSaveKeyword);
			this.groupBox1.Controls.Add(this.btnRemoveKeyword);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtKeywordText);
			this.groupBox1.Location = new System.Drawing.Point(218, 11);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(224, 112);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Keyword";
			// 
			// chklistboxKeywords
			// 
			this.chklistboxKeywords.FormattingEnabled = true;
			this.chklistboxKeywords.Location = new System.Drawing.Point(10, 11);
			this.chklistboxKeywords.Name = "chklistboxKeywords";
			this.chklistboxKeywords.Size = new System.Drawing.Size(190, 109);
			this.chklistboxKeywords.TabIndex = 8;
			this.chklistboxKeywords.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chklistboxKeywords_ItemCheck);
			this.chklistboxKeywords.SelectedIndexChanged += new System.EventHandler(this.chklistboxKeywords_SelectedIndexChanged);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(0, 5);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(461, 162);
			this.tabControl1.TabIndex = 9;
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.Controls.Add(this.chklistboxKeywords);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(453, 136);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Content Whitelist";
			// 
			// tabPage2
			// 
			this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
			this.tabPage2.Controls.Add(this.groupBox2);
			this.tabPage2.Controls.Add(this.chklistboxBlacklist);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(453, 136);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "URL Blacklist";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.btnSaveWord);
			this.groupBox2.Controls.Add(this.btnRemoveWord);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.txtUrlWord);
			this.groupBox2.Location = new System.Drawing.Point(218, 11);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(224, 112);
			this.groupBox2.TabIndex = 9;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "URL Component";
			// 
			// btnSaveWord
			// 
			this.btnSaveWord.Location = new System.Drawing.Point(34, 71);
			this.btnSaveWord.Name = "btnSaveWord";
			this.btnSaveWord.Size = new System.Drawing.Size(75, 23);
			this.btnSaveWord.TabIndex = 3;
			this.btnSaveWord.Text = "Save";
			this.btnSaveWord.UseVisualStyleBackColor = true;
			this.btnSaveWord.Click += new System.EventHandler(this.btnSaveWord_Click);
			// 
			// btnRemoveWord
			// 
			this.btnRemoveWord.Location = new System.Drawing.Point(115, 71);
			this.btnRemoveWord.Name = "btnRemoveWord";
			this.btnRemoveWord.Size = new System.Drawing.Size(75, 23);
			this.btnRemoveWord.TabIndex = 4;
			this.btnRemoveWord.Text = "Remove";
			this.btnRemoveWord.UseVisualStyleBackColor = true;
			this.btnRemoveWord.Click += new System.EventHandler(this.btnRemoveWord_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(17, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(28, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Text";
			// 
			// txtUrlWord
			// 
			this.txtUrlWord.Location = new System.Drawing.Point(51, 27);
			this.txtUrlWord.Name = "txtUrlWord";
			this.txtUrlWord.Size = new System.Drawing.Size(157, 20);
			this.txtUrlWord.TabIndex = 2;
			// 
			// chklistboxBlacklist
			// 
			this.chklistboxBlacklist.FormattingEnabled = true;
			this.chklistboxBlacklist.Location = new System.Drawing.Point(10, 11);
			this.chklistboxBlacklist.Name = "chklistboxBlacklist";
			this.chklistboxBlacklist.Size = new System.Drawing.Size(190, 109);
			this.chklistboxBlacklist.TabIndex = 12;
			this.chklistboxBlacklist.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chklistboxBlacklist_ItemCheck);
			this.chklistboxBlacklist.SelectedIndexChanged += new System.EventHandler(this.chklistboxBlacklist_SelectedIndexChanged);
			// 
			// btnAcceptChanges
			// 
			this.btnAcceptChanges.Location = new System.Drawing.Point(3, 3);
			this.btnAcceptChanges.Name = "btnAcceptChanges";
			this.btnAcceptChanges.Size = new System.Drawing.Size(75, 23);
			this.btnAcceptChanges.TabIndex = 10;
			this.btnAcceptChanges.Text = "Accept";
			this.btnAcceptChanges.UseVisualStyleBackColor = true;
			this.btnAcceptChanges.Click += new System.EventHandler(this.btnAcceptChanges_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(84, 3);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 11;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.Controls.Add(this.btnAcceptChanges);
			this.flowLayoutPanel1.Controls.Add(this.btnCancel);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(147, 185);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(162, 29);
			this.flowLayoutPanel1.TabIndex = 12;
			// 
			// KeywordList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(460, 226);
			this.Controls.Add(this.flowLayoutPanel1);
			this.Controls.Add(this.tabControl1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "KeywordList";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Manage PageWords";
			this.Shown += new System.EventHandler(this.KeywordList_Shown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtKeywordText;
		private System.Windows.Forms.Button btnSaveKeyword;
		private System.Windows.Forms.Button btnRemoveKeyword;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckedListBox chklistboxKeywords;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnSaveWord;
		private System.Windows.Forms.Button btnRemoveWord;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtUrlWord;
		private System.Windows.Forms.CheckedListBox chklistboxBlacklist;
		private System.Windows.Forms.Button btnAcceptChanges;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
	}
}