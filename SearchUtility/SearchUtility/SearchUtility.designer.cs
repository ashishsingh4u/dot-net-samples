namespace TechieNotes
{
    partial class SearchUtility
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblFilesFound = new System.Windows.Forms.Label();
            this.lblFiles = new System.Windows.Forms.Label();
            this.chkRecursive = new System.Windows.Forms.CheckBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblSearchText = new System.Windows.Forms.Label();
            this.lblFileType = new System.Windows.Forms.Label();
            this.lblDirectoryName = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.txtFileType = new System.Windows.Forms.TextBox();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.lstSearchFile = new System.Windows.Forms.ListBox();
            this.rtFile = new System.Windows.Forms.RichTextBox();
            this.splSearch = new System.Windows.Forms.Splitter();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblFilesFound);
            this.groupBox1.Controls.Add(this.lblFiles);
            this.groupBox1.Controls.Add(this.chkRecursive);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.lblSearchText);
            this.groupBox1.Controls.Add(this.lblFileType);
            this.groupBox1.Controls.Add(this.lblDirectoryName);
            this.groupBox1.Controls.Add(this.txtSearch);
            this.groupBox1.Controls.Add(this.txtFileType);
            this.groupBox1.Controls.Add(this.txtFileName);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 398);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(652, 83);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Criteria";
            // 
            // lblFilesFound
            // 
            this.lblFilesFound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFilesFound.AutoSize = true;
            this.lblFilesFound.Location = new System.Drawing.Point(190, 57);
            this.lblFilesFound.Name = "lblFilesFound";
            this.lblFilesFound.Size = new System.Drawing.Size(0, 13);
            this.lblFilesFound.TabIndex = 7;
            // 
            // lblFiles
            // 
            this.lblFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFiles.AutoSize = true;
            this.lblFiles.Location = new System.Drawing.Point(104, 57);
            this.lblFiles.Name = "lblFiles";
            this.lblFiles.Size = new System.Drawing.Size(80, 13);
            this.lblFiles.TabIndex = 6;
            this.lblFiles.Text = "Files Found :";
            // 
            // chkRecursive
            // 
            this.chkRecursive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkRecursive.AutoSize = true;
            this.chkRecursive.Checked = true;
            this.chkRecursive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRecursive.Location = new System.Drawing.Point(15, 54);
            this.chkRecursive.Name = "chkRecursive";
            this.chkRecursive.Size = new System.Drawing.Size(83, 17);
            this.chkRecursive.TabIndex = 5;
            this.chkRecursive.Text = "Recursive";
            this.chkRecursive.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(553, 50);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(87, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearchClick);
            // 
            // lblSearchText
            // 
            this.lblSearchText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSearchText.AutoSize = true;
            this.lblSearchText.Location = new System.Drawing.Point(435, 24);
            this.lblSearchText.Name = "lblSearchText";
            this.lblSearchText.Size = new System.Drawing.Size(76, 13);
            this.lblSearchText.TabIndex = 2;
            this.lblSearchText.Text = "Search Text";
            // 
            // lblFileType
            // 
            this.lblFileType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFileType.AutoSize = true;
            this.lblFileType.Location = new System.Drawing.Point(241, 24);
            this.lblFileType.Name = "lblFileType";
            this.lblFileType.Size = new System.Drawing.Size(59, 13);
            this.lblFileType.TabIndex = 2;
            this.lblFileType.Text = "File Type";
            // 
            // lblDirectoryName
            // 
            this.lblDirectoryName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDirectoryName.AutoSize = true;
            this.lblDirectoryName.Location = new System.Drawing.Point(12, 24);
            this.lblDirectoryName.Name = "lblDirectoryName";
            this.lblDirectoryName.Size = new System.Drawing.Size(94, 13);
            this.lblDirectoryName.TabIndex = 2;
            this.lblDirectoryName.Text = "Directory Name";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
            this.txtSearch.Location = new System.Drawing.Point(511, 24);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(129, 20);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.Text = "System";
            // 
            // txtFileType
            // 
            this.txtFileType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileType.Location = new System.Drawing.Point(300, 24);
            this.txtFileType.Name = "txtFileType";
            this.txtFileType.Size = new System.Drawing.Size(129, 20);
            this.txtFileType.TabIndex = 2;
            this.txtFileType.Text = "*.cs";
            // 
            // txtFileName
            // 
            this.txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileName.BackColor = System.Drawing.SystemColors.Menu;
            this.txtFileName.Location = new System.Drawing.Point(106, 24);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(129, 20);
            this.txtFileName.TabIndex = 1;
            this.txtFileName.Text = "c:\\Projects";
            // 
            // lstSearchFile
            // 
            this.lstSearchFile.Dock = System.Windows.Forms.DockStyle.Left;
            this.lstSearchFile.FormattingEnabled = true;
            this.lstSearchFile.Location = new System.Drawing.Point(0, 0);
            this.lstSearchFile.Name = "lstSearchFile";
            this.lstSearchFile.Size = new System.Drawing.Size(194, 398);
            this.lstSearchFile.TabIndex = 5;
            this.lstSearchFile.Click += new System.EventHandler(this.LstSearchFileClick);
            // 
            // rtFile
            // 
            this.rtFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtFile.Location = new System.Drawing.Point(194, 0);
            this.rtFile.Name = "rtFile";
            this.rtFile.Size = new System.Drawing.Size(458, 398);
            this.rtFile.TabIndex = 6;
            this.rtFile.Text = "";
            // 
            // splSearch
            // 
            this.splSearch.Location = new System.Drawing.Point(194, 0);
            this.splSearch.Name = "splSearch";
            this.splSearch.Size = new System.Drawing.Size(2, 398);
            this.splSearch.TabIndex = 7;
            this.splSearch.TabStop = false;
            // 
            // SearchUtility
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 481);
            this.Controls.Add(this.splSearch);
            this.Controls.Add(this.rtFile);
            this.Controls.Add(this.lstSearchFile);
            this.Controls.Add(this.groupBox1);
            this.Name = "SearchUtility";
            this.Text = "Search";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(SearchUtilityFormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblSearchText;
        private System.Windows.Forms.Label lblFileType;
        private System.Windows.Forms.Label lblDirectoryName;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.TextBox txtFileType;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.ListBox lstSearchFile;
        private System.Windows.Forms.RichTextBox rtFile;
        private System.Windows.Forms.CheckBox chkRecursive;
        private System.Windows.Forms.Splitter splSearch;
        private System.Windows.Forms.Label lblFilesFound;
        private System.Windows.Forms.Label lblFiles;
    }
}

