namespace TNCSearch
{
    partial class TNCSearchMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TNCSearchMainForm));
            this.countWordsButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.indexStorageFolderGroupBox = new System.Windows.Forms.GroupBox();
            this.indexStorageFolderComboBox = new System.Windows.Forms.ComboBox();
            this.isIndexShrinkedCheckBox = new System.Windows.Forms.CheckBox();
            this.bookFolderGroupBox = new System.Windows.Forms.GroupBox();
            this.useFilesCheckBox = new System.Windows.Forms.CheckBox();
            this.isPuncdBooksDocumentsCheckBox = new System.Windows.Forms.CheckBox();
            this.booksDocumentsFolderComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.openResultsButton = new System.Windows.Forms.Button();
            this.runModeComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.minWordsTextBox = new System.Windows.Forms.TextBox();
            this.pauseCheckBox = new System.Windows.Forms.CheckBox();
            this.exactMatchCheckBox = new System.Windows.Forms.CheckBox();
            this.searchLableStatus = new System.Windows.Forms.Label();
            this.goButton = new System.Windows.Forms.Button();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.outputRichTextBox = new System.Windows.Forms.RichTextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.indexerOperationsGroupBox = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblIndexer = new System.Windows.Forms.Label();
            this.resultsTabControl = new System.Windows.Forms.TabControl();
            this.hebNumCheckBox = new System.Windows.Forms.CheckBox();
            this.versesReadNTextBox = new System.Windows.Forms.TextBox();
            this.avgVerPerSecTextBox = new System.Windows.Forms.TextBox();
            this.indexStorageFolderGroupBox.SuspendLayout();
            this.bookFolderGroupBox.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.indexerOperationsGroupBox.SuspendLayout();
            this.resultsTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // countWordsButton
            // 
            this.countWordsButton.Enabled = false;
            this.countWordsButton.Location = new System.Drawing.Point(273, 17);
            this.countWordsButton.Name = "countWordsButton";
            this.countWordsButton.Size = new System.Drawing.Size(91, 23);
            this.countWordsButton.TabIndex = 10;
            this.countWordsButton.Text = "Count Words";
            this.countWordsButton.UseVisualStyleBackColor = true;
            this.countWordsButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(272, 44);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(91, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "Free Memory";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // indexStorageFolderGroupBox
            // 
            this.indexStorageFolderGroupBox.Controls.Add(this.indexStorageFolderComboBox);
            this.indexStorageFolderGroupBox.Controls.Add(this.isIndexShrinkedCheckBox);
            this.indexStorageFolderGroupBox.Controls.Add(this.countWordsButton);
            this.indexStorageFolderGroupBox.Controls.Add(this.button3);
            this.indexStorageFolderGroupBox.Location = new System.Drawing.Point(3, 3);
            this.indexStorageFolderGroupBox.Name = "indexStorageFolderGroupBox";
            this.indexStorageFolderGroupBox.Size = new System.Drawing.Size(369, 73);
            this.indexStorageFolderGroupBox.TabIndex = 0;
            this.indexStorageFolderGroupBox.TabStop = false;
            this.indexStorageFolderGroupBox.Text = "Index Storage Folder";
            // 
            // indexStorageFolderComboBox
            // 
            this.indexStorageFolderComboBox.FormattingEnabled = true;
            this.indexStorageFolderComboBox.Location = new System.Drawing.Point(6, 19);
            this.indexStorageFolderComboBox.Name = "indexStorageFolderComboBox";
            this.indexStorageFolderComboBox.Size = new System.Drawing.Size(189, 21);
            this.indexStorageFolderComboBox.TabIndex = 3;
            this.indexStorageFolderComboBox.SelectedIndexChanged += new System.EventHandler(this.index_Storage_Directory_ComboBox_SelectedIndexChanged);
            // 
            // isIndexShrinkedCheckBox
            // 
            this.isIndexShrinkedCheckBox.AutoSize = true;
            this.isIndexShrinkedCheckBox.Enabled = false;
            this.isIndexShrinkedCheckBox.Location = new System.Drawing.Point(201, 21);
            this.isIndexShrinkedCheckBox.Name = "isIndexShrinkedCheckBox";
            this.isIndexShrinkedCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.isIndexShrinkedCheckBox.Size = new System.Drawing.Size(66, 17);
            this.isIndexShrinkedCheckBox.TabIndex = 22;
            this.isIndexShrinkedCheckBox.Text = "shrinked";
            this.isIndexShrinkedCheckBox.UseVisualStyleBackColor = true;
            // 
            // bookFolderGroupBox
            // 
            this.bookFolderGroupBox.Controls.Add(this.useFilesCheckBox);
            this.bookFolderGroupBox.Controls.Add(this.isPuncdBooksDocumentsCheckBox);
            this.bookFolderGroupBox.Controls.Add(this.booksDocumentsFolderComboBox);
            this.bookFolderGroupBox.Location = new System.Drawing.Point(3, 82);
            this.bookFolderGroupBox.Name = "bookFolderGroupBox";
            this.bookFolderGroupBox.Size = new System.Drawing.Size(267, 45);
            this.bookFolderGroupBox.TabIndex = 3;
            this.bookFolderGroupBox.TabStop = false;
            this.bookFolderGroupBox.Text = "Books Documents Folder";
            // 
            // useFilesCheckBox
            // 
            this.useFilesCheckBox.AutoSize = true;
            this.useFilesCheckBox.Location = new System.Drawing.Point(193, 9);
            this.useFilesCheckBox.Name = "useFilesCheckBox";
            this.useFilesCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.useFilesCheckBox.Size = new System.Drawing.Size(64, 17);
            this.useFilesCheckBox.TabIndex = 26;
            this.useFilesCheckBox.Text = "use files";
            this.useFilesCheckBox.UseVisualStyleBackColor = true;
            // 
            // isPuncdBooksDocumentsCheckBox
            // 
            this.isPuncdBooksDocumentsCheckBox.AutoSize = true;
            this.isPuncdBooksDocumentsCheckBox.Enabled = false;
            this.isPuncdBooksDocumentsCheckBox.Location = new System.Drawing.Point(201, 26);
            this.isPuncdBooksDocumentsCheckBox.Name = "isPuncdBooksDocumentsCheckBox";
            this.isPuncdBooksDocumentsCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.isPuncdBooksDocumentsCheckBox.Size = new System.Drawing.Size(56, 17);
            this.isPuncdBooksDocumentsCheckBox.TabIndex = 23;
            this.isPuncdBooksDocumentsCheckBox.Text = "puncd";
            this.isPuncdBooksDocumentsCheckBox.UseVisualStyleBackColor = true;
            // 
            // booksDocumentsFolderComboBox
            // 
            this.booksDocumentsFolderComboBox.FormattingEnabled = true;
            this.booksDocumentsFolderComboBox.Location = new System.Drawing.Point(6, 19);
            this.booksDocumentsFolderComboBox.Name = "booksDocumentsFolderComboBox";
            this.booksDocumentsFolderComboBox.Size = new System.Drawing.Size(181, 21);
            this.booksDocumentsFolderComboBox.TabIndex = 4;
            this.booksDocumentsFolderComboBox.SelectedIndexChanged += new System.EventHandler(this.booksDocumentsFolderComboBox_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.openResultsButton);
            this.groupBox4.Controls.Add(this.runModeComboBox);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.minWordsTextBox);
            this.groupBox4.Controls.Add(this.pauseCheckBox);
            this.groupBox4.Controls.Add(this.exactMatchCheckBox);
            this.groupBox4.Controls.Add(this.searchLableStatus);
            this.groupBox4.Controls.Add(this.goButton);
            this.groupBox4.Controls.Add(this.searchTextBox);
            this.groupBox4.Location = new System.Drawing.Point(3, 131);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(565, 71);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "4. Search";
            // 
            // openResultsButton
            // 
            this.openResultsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openResultsButton.Location = new System.Drawing.Point(394, 42);
            this.openResultsButton.Name = "openResultsButton";
            this.openResultsButton.Size = new System.Drawing.Size(75, 23);
            this.openResultsButton.TabIndex = 24;
            this.openResultsButton.Text = "open results";
            this.openResultsButton.UseVisualStyleBackColor = true;
            this.openResultsButton.Click += new System.EventHandler(this.openResultsButton_Click);
            // 
            // runModeComboBox
            // 
            this.runModeComboBox.FormattingEnabled = true;
            this.runModeComboBox.Items.AddRange(new object[] {
            "Search",
            "Compare books"});
            this.runModeComboBox.Location = new System.Drawing.Point(10, 19);
            this.runModeComboBox.Name = "runModeComboBox";
            this.runModeComboBox.Size = new System.Drawing.Size(100, 21);
            this.runModeComboBox.TabIndex = 23;
            this.runModeComboBox.TextChanged += new System.EventHandler(this.runModeComboBox_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(475, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "min_words:";
            // 
            // minWordsTextBox
            // 
            this.minWordsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minWordsTextBox.Location = new System.Drawing.Point(538, 44);
            this.minWordsTextBox.Name = "minWordsTextBox";
            this.minWordsTextBox.Size = new System.Drawing.Size(21, 20);
            this.minWordsTextBox.TabIndex = 20;
            this.minWordsTextBox.Text = "4";
            // 
            // pauseCheckBox
            // 
            this.pauseCheckBox.AutoSize = true;
            this.pauseCheckBox.Location = new System.Drawing.Point(116, 22);
            this.pauseCheckBox.Name = "pauseCheckBox";
            this.pauseCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.pauseCheckBox.Size = new System.Drawing.Size(55, 17);
            this.pauseCheckBox.TabIndex = 19;
            this.pauseCheckBox.Text = "pause";
            this.pauseCheckBox.UseVisualStyleBackColor = true;
            // 
            // exactMatchCheckBox
            // 
            this.exactMatchCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exactMatchCheckBox.AutoSize = true;
            this.exactMatchCheckBox.Checked = true;
            this.exactMatchCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.exactMatchCheckBox.Location = new System.Drawing.Point(506, 21);
            this.exactMatchCheckBox.Name = "exactMatchCheckBox";
            this.exactMatchCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.exactMatchCheckBox.Size = new System.Drawing.Size(52, 17);
            this.exactMatchCheckBox.TabIndex = 17;
            this.exactMatchCheckBox.Text = "exact";
            this.exactMatchCheckBox.UseVisualStyleBackColor = true;
            // 
            // searchLableStatus
            // 
            this.searchLableStatus.AutoSize = true;
            this.searchLableStatus.Location = new System.Drawing.Point(7, 44);
            this.searchLableStatus.Name = "searchLableStatus";
            this.searchLableStatus.Size = new System.Drawing.Size(0, 13);
            this.searchLableStatus.TabIndex = 2;
            // 
            // goButton
            // 
            this.goButton.Location = new System.Drawing.Point(177, 17);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(64, 23);
            this.goButton.TabIndex = 14;
            this.goButton.Text = "Go";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // searchTextBox
            // 
            this.searchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchTextBox.Location = new System.Drawing.Point(247, 19);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.searchTextBox.Size = new System.Drawing.Size(254, 20);
            this.searchTextBox.TabIndex = 15;
            this.searchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.outputRichTextBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(557, 164);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Output";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // outputRichTextBox
            // 
            this.outputRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputRichTextBox.Location = new System.Drawing.Point(3, 3);
            this.outputRichTextBox.Name = "outputRichTextBox";
            this.outputRichTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.outputRichTextBox.Size = new System.Drawing.Size(551, 158);
            this.outputRichTextBox.TabIndex = 0;
            this.outputRichTextBox.Text = "";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(557, 164);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(3, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.listBox1.Size = new System.Drawing.Size(551, 158);
            this.listBox1.TabIndex = 16;
            this.listBox1.DoubleClick += new System.EventHandler(this.listBox_DoubleClick);
            // 
            // indexerOperationsGroupBox
            // 
            this.indexerOperationsGroupBox.Controls.Add(this.button4);
            this.indexerOperationsGroupBox.Controls.Add(this.btnStop);
            this.indexerOperationsGroupBox.Controls.Add(this.btnStart);
            this.indexerOperationsGroupBox.Controls.Add(this.lblIndexer);
            this.indexerOperationsGroupBox.Location = new System.Drawing.Point(386, 6);
            this.indexerOperationsGroupBox.Name = "indexerOperationsGroupBox";
            this.indexerOperationsGroupBox.Size = new System.Drawing.Size(179, 103);
            this.indexerOperationsGroupBox.TabIndex = 6;
            this.indexerOperationsGroupBox.TabStop = false;
            this.indexerOperationsGroupBox.Text = "Indexer Operations";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(75, 48);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(91, 23);
            this.button4.TabIndex = 11;
            this.button4.Text = "Save Index";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(91, 19);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 9;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(10, 19);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "Create";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblIndexer
            // 
            this.lblIndexer.AutoSize = true;
            this.lblIndexer.Location = new System.Drawing.Point(6, 76);
            this.lblIndexer.Name = "lblIndexer";
            this.lblIndexer.Size = new System.Drawing.Size(37, 13);
            this.lblIndexer.TabIndex = 2;
            this.lblIndexer.Text = "Status";
            // 
            // resultsTabControl
            // 
            this.resultsTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultsTabControl.Controls.Add(this.tabPage1);
            this.resultsTabControl.Controls.Add(this.tabPage2);
            this.resultsTabControl.Location = new System.Drawing.Point(3, 208);
            this.resultsTabControl.Name = "resultsTabControl";
            this.resultsTabControl.SelectedIndex = 0;
            this.resultsTabControl.Size = new System.Drawing.Size(565, 190);
            this.resultsTabControl.TabIndex = 18;
            this.resultsTabControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.resultsTabControl_MouseDown);
            // 
            // hebNumCheckBox
            // 
            this.hebNumCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hebNumCheckBox.AutoSize = true;
            this.hebNumCheckBox.Checked = true;
            this.hebNumCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.hebNumCheckBox.Location = new System.Drawing.Point(491, 115);
            this.hebNumCheckBox.Name = "hebNumCheckBox";
            this.hebNumCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.hebNumCheckBox.Size = new System.Drawing.Size(70, 17);
            this.hebNumCheckBox.TabIndex = 24;
            this.hebNumCheckBox.Text = "heb_num";
            this.hebNumCheckBox.UseVisualStyleBackColor = true;
            // 
            // versesReadNTextBox
            // 
            this.versesReadNTextBox.Location = new System.Drawing.Point(276, 89);
            this.versesReadNTextBox.Name = "versesReadNTextBox";
            this.versesReadNTextBox.Size = new System.Drawing.Size(104, 20);
            this.versesReadNTextBox.TabIndex = 24;
            // 
            // avgVerPerSecTextBox
            // 
            this.avgVerPerSecTextBox.Location = new System.Drawing.Point(276, 112);
            this.avgVerPerSecTextBox.Name = "avgVerPerSecTextBox";
            this.avgVerPerSecTextBox.Size = new System.Drawing.Size(209, 20);
            this.avgVerPerSecTextBox.TabIndex = 25;
            this.avgVerPerSecTextBox.Text = "0";
            // 
            // TNCSearchMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 393);
            this.Controls.Add(this.avgVerPerSecTextBox);
            this.Controls.Add(this.versesReadNTextBox);
            this.Controls.Add(this.hebNumCheckBox);
            this.Controls.Add(this.indexerOperationsGroupBox);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.bookFolderGroupBox);
            this.Controls.Add(this.indexStorageFolderGroupBox);
            this.Controls.Add(this.resultsTabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TNCSearchMainForm";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "TNC Desktop Search";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.TNCSearchMainForm_Load);
            this.indexStorageFolderGroupBox.ResumeLayout(false);
            this.indexStorageFolderGroupBox.PerformLayout();
            this.bookFolderGroupBox.ResumeLayout(false);
            this.bookFolderGroupBox.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.indexerOperationsGroupBox.ResumeLayout(false);
            this.indexerOperationsGroupBox.PerformLayout();
            this.resultsTabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button countWordsButton;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox indexStorageFolderGroupBox;
        private System.Windows.Forms.GroupBox bookFolderGroupBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label searchLableStatus;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.CheckBox exactMatchCheckBox;
        private System.Windows.Forms.CheckBox pauseCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox minWordsTextBox;
        private System.Windows.Forms.CheckBox isIndexShrinkedCheckBox;
        private System.Windows.Forms.ComboBox runModeComboBox;
        private System.Windows.Forms.ComboBox indexStorageFolderComboBox;
        private System.Windows.Forms.ComboBox booksDocumentsFolderComboBox;
        private System.Windows.Forms.CheckBox isPuncdBooksDocumentsCheckBox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox outputRichTextBox;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox indexerOperationsGroupBox;
        private System.Windows.Forms.Label lblIndexer;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TabControl resultsTabControl;
        private System.Windows.Forms.CheckBox hebNumCheckBox;
        private System.Windows.Forms.TextBox versesReadNTextBox;
        private System.Windows.Forms.TextBox avgVerPerSecTextBox;
        private System.Windows.Forms.CheckBox useFilesCheckBox;
        private System.Windows.Forms.Button openResultsButton;
    }
}

