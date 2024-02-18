namespace SimpleFileSearcher {
    partial class Form1 {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbContentPattern = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbFilePatterns = new System.Windows.Forms.ComboBox();
            this.bwSearcher = new System.ComponentModel.BackgroundWorker();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.btnShowExplorer = new System.Windows.Forms.Button();
            this.cbBasePath = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ffrControl = new SimpleFileSearcher.FindFilesResultControl();
            this.btnRemoveBasePathItem = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSearch.Location = new System.Drawing.Point(421, 494);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 28);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Find what:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 78);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Look in:";
            // 
            // tbContentPattern
            // 
            this.tbContentPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbContentPattern.Location = new System.Drawing.Point(20, 31);
            this.tbContentPattern.Margin = new System.Windows.Forms.Padding(4);
            this.tbContentPattern.Name = "tbContentPattern";
            this.tbContentPattern.Size = new System.Drawing.Size(903, 22);
            this.tbContentPattern.TabIndex = 3;
            this.tbContentPattern.Text = "test";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 144);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Look at these file types:";
            // 
            // cbFilePatterns
            // 
            this.cbFilePatterns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFilePatterns.FormattingEnabled = true;
            this.cbFilePatterns.Items.AddRange(new object[] {
            "*.cs;*.resx;*.xsd;*.wsdl;*.xaml;*.xml;*.htm;*.html;*.css"});
            this.cbFilePatterns.Location = new System.Drawing.Point(20, 164);
            this.cbFilePatterns.Margin = new System.Windows.Forms.Padding(4);
            this.cbFilePatterns.Name = "cbFilePatterns";
            this.cbFilePatterns.Size = new System.Drawing.Size(903, 24);
            this.cbFilePatterns.TabIndex = 6;
            // 
            // bwSearcher
            // 
            this.bwSearcher.WorkerReportsProgress = true;
            this.bwSearcher.WorkerSupportsCancellation = true;
            this.bwSearcher.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwSearcher_DoWork);
            this.bwSearcher.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwSearcher_RunWorkerCompleted);
            this.bwSearcher.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwSearcher_ProgressChanged);
            // 
            // pbProgress
            // 
            this.pbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbProgress.Location = new System.Drawing.Point(108, 196);
            this.pbProgress.Margin = new System.Windows.Forms.Padding(4);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(816, 28);
            this.pbProgress.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 209);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Find results:";
            // 
            // btnShowExplorer
            // 
            this.btnShowExplorer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowExplorer.Location = new System.Drawing.Point(891, 97);
            this.btnShowExplorer.Margin = new System.Windows.Forms.Padding(4);
            this.btnShowExplorer.Name = "btnShowExplorer";
            this.btnShowExplorer.Size = new System.Drawing.Size(33, 25);
            this.btnShowExplorer.TabIndex = 10;
            this.btnShowExplorer.Text = "...";
            this.btnShowExplorer.UseVisualStyleBackColor = true;
            this.btnShowExplorer.Click += new System.EventHandler(this.btnShowExplorer_Click);
            // 
            // cbBasePath
            // 
            this.cbBasePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbBasePath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbBasePath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.cbBasePath.FormattingEnabled = true;
            this.cbBasePath.Location = new System.Drawing.Point(20, 98);
            this.cbBasePath.Name = "cbBasePath";
            this.cbBasePath.Size = new System.Drawing.Size(823, 24);
            this.cbBasePath.TabIndex = 12;
            this.cbBasePath.Text = "C:\\";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.Location = new System.Drawing.Point(421, 494);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ffrControl
            // 
            this.ffrControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ffrControl.CurrentSearchString = null;
            this.ffrControl.Location = new System.Drawing.Point(20, 231);
            this.ffrControl.Name = "ffrControl";
            this.ffrControl.Size = new System.Drawing.Size(903, 252);
            this.ffrControl.TabIndex = 11;
            // 
            // btnRemoveBasePathItem
            // 
            this.btnRemoveBasePathItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveBasePathItem.Location = new System.Drawing.Point(850, 97);
            this.btnRemoveBasePathItem.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemoveBasePathItem.Name = "btnRemoveBasePathItem";
            this.btnRemoveBasePathItem.Size = new System.Drawing.Size(33, 25);
            this.btnRemoveBasePathItem.TabIndex = 14;
            this.btnRemoveBasePathItem.Text = "X";
            this.btnRemoveBasePathItem.UseVisualStyleBackColor = true;
            this.btnRemoveBasePathItem.Click += new System.EventHandler(this.btnRemoveBasePathItem_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 533);
            this.Controls.Add(this.btnRemoveBasePathItem);
            this.Controls.Add(this.cbBasePath);
            this.Controls.Add(this.ffrControl);
            this.Controls.Add(this.btnShowExplorer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pbProgress);
            this.Controls.Add(this.cbFilePatterns);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbContentPattern);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(530, 400);
            this.Name = "Form1";
            this.Text = "SimpleFileSearcher";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbContentPattern;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbFilePatterns;
        private System.ComponentModel.BackgroundWorker bwSearcher;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnShowExplorer;
        private FindFilesResultControl ffrControl;
        private System.Windows.Forms.ComboBox cbBasePath;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRemoveBasePathItem;
    }
}

