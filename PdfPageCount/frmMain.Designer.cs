namespace PdfPageCount
{
    partial class frmMain
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnStart = new System.Windows.Forms.Button();
            this.lblPath = new System.Windows.Forms.LinkLabel();
            this.grvFiles = new System.Windows.Forms.DataGridView();
            this.staProcess = new System.Windows.Forms.StatusStrip();
            this.staFolderPath = new System.Windows.Forms.ToolStripStatusLabel();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.btnCopyAll = new System.Windows.Forms.Button();
            this.chkIsFilterDate = new System.Windows.Forms.CheckBox();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.extension = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PageNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Uploader = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreatedOn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.a4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.a3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.a2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.a1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.a0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grvFiles)).BeginInit();
            this.staProcess.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(17, 8);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(132, 29);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(156, 11);
            this.lblPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(29, 17);
            this.lblPath.TabIndex = 7;
            this.lblPath.TabStop = true;
            this.lblPath.Text = "D:\\";
            this.lblPath.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblPath_LinkClicked);
            // 
            // grvFiles
            // 
            this.grvFiles.AllowUserToAddRows = false;
            this.grvFiles.AllowUserToDeleteRows = false;
            this.grvFiles.AllowUserToResizeRows = false;
            this.grvFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grvFiles.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.grvFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileName,
            this.extension,
            this.FilePath,
            this.PageNumber,
            this.Uploader,
            this.CreatedOn,
            this.a4,
            this.a3,
            this.a2,
            this.a1,
            this.a0});
            this.grvFiles.Location = new System.Drawing.Point(17, 71);
            this.grvFiles.Margin = new System.Windows.Forms.Padding(4);
            this.grvFiles.Name = "grvFiles";
            this.grvFiles.RowHeadersVisible = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvFiles.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grvFiles.Size = new System.Drawing.Size(1320, 425);
            this.grvFiles.TabIndex = 8;
            this.grvFiles.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvFiles_CellContentClick);
            // 
            // staProcess
            // 
            this.staProcess.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.staProcess.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.staFolderPath});
            this.staProcess.Location = new System.Drawing.Point(0, 539);
            this.staProcess.Name = "staProcess";
            this.staProcess.Size = new System.Drawing.Size(1350, 22);
            this.staProcess.TabIndex = 9;
            this.staProcess.Text = "statusStrip1";
            // 
            // staFolderPath
            // 
            this.staFolderPath.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold);
            this.staFolderPath.Name = "staFolderPath";
            this.staFolderPath.Size = new System.Drawing.Size(0, 17);
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(217, 41);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(132, 23);
            this.dtpFromDate.TabIndex = 10;
            this.dtpFromDate.Value = new System.DateTime(2018, 1, 1, 0, 0, 0, 0);
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(377, 41);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(125, 23);
            this.dtpToDate.TabIndex = 11;
            // 
            // btnCopyAll
            // 
            this.btnCopyAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopyAll.Location = new System.Drawing.Point(17, 503);
            this.btnCopyAll.Name = "btnCopyAll";
            this.btnCopyAll.Size = new System.Drawing.Size(132, 33);
            this.btnCopyAll.TabIndex = 12;
            this.btnCopyAll.Text = "Copy tất cả";
            this.btnCopyAll.UseVisualStyleBackColor = true;
            this.btnCopyAll.Click += new System.EventHandler(this.btnCopyAll_Click);
            // 
            // chkIsFilterDate
            // 
            this.chkIsFilterDate.AutoSize = true;
            this.chkIsFilterDate.Checked = true;
            this.chkIsFilterDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsFilterDate.Location = new System.Drawing.Point(17, 43);
            this.chkIsFilterDate.Name = "chkIsFilterDate";
            this.chkIsFilterDate.Size = new System.Drawing.Size(194, 21);
            this.chkIsFilterDate.TabIndex = 13;
            this.chkIsFilterDate.Text = "Lọc File theo ngày tạo:";
            this.chkIsFilterDate.UseVisualStyleBackColor = true;
            this.chkIsFilterDate.CheckedChanged += new System.EventHandler(this.chkIsFilterDate_CheckedChanged);
            // 
            // FileName
            // 
            this.FileName.HeaderText = "Tên file";
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            this.FileName.Width = 200;
            // 
            // extension
            // 
            this.extension.HeaderText = "extension";
            this.extension.Name = "extension";
            this.extension.ReadOnly = true;
            // 
            // FilePath
            // 
            this.FilePath.HeaderText = "Đường dẫn";
            this.FilePath.Name = "FilePath";
            this.FilePath.ReadOnly = true;
            this.FilePath.Width = 600;
            // 
            // PageNumber
            // 
            this.PageNumber.HeaderText = "Tổng số trang tính ra A4";
            this.PageNumber.Name = "PageNumber";
            this.PageNumber.ReadOnly = true;
            this.PageNumber.Width = 150;
            // 
            // Uploader
            // 
            this.Uploader.HeaderText = "Uploader";
            this.Uploader.Name = "Uploader";
            this.Uploader.ReadOnly = true;
            // 
            // CreatedOn
            // 
            this.CreatedOn.HeaderText = "Ngày tạo";
            this.CreatedOn.Name = "CreatedOn";
            this.CreatedOn.ReadOnly = true;
            this.CreatedOn.Width = 200;
            // 
            // a4
            // 
            this.a4.HeaderText = "Số trang A4";
            this.a4.Name = "a4";
            this.a4.ReadOnly = true;
            this.a4.Visible = false;
            this.a4.Width = 90;
            // 
            // a3
            // 
            this.a3.HeaderText = "Số trang A3";
            this.a3.Name = "a3";
            this.a3.ReadOnly = true;
            this.a3.Visible = false;
            this.a3.Width = 90;
            // 
            // a2
            // 
            this.a2.HeaderText = "Số trang A2";
            this.a2.Name = "a2";
            this.a2.ReadOnly = true;
            this.a2.Visible = false;
            this.a2.Width = 90;
            // 
            // a1
            // 
            this.a1.HeaderText = "Số trang A1";
            this.a1.Name = "a1";
            this.a1.ReadOnly = true;
            this.a1.Visible = false;
            this.a1.Width = 90;
            // 
            // a0
            // 
            this.a0.HeaderText = "Số trang A0";
            this.a0.Name = "a0";
            this.a0.ReadOnly = true;
            this.a0.Visible = false;
            this.a0.Width = 90;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1350, 561);
            this.Controls.Add(this.chkIsFilterDate);
            this.Controls.Add(this.btnCopyAll);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.staProcess);
            this.Controls.Add(this.grvFiles);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.btnStart);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(16, 300);
            this.Name = "frmMain";
            this.Text = "Phần mềm đếm trang - Trung tâm Công nghệ thông tin - Sở Tài nguyên và Môi trưởng " +
    "tỉnh Tiền Giang";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grvFiles)).EndInit();
            this.staProcess.ResumeLayout(false);
            this.staProcess.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.LinkLabel lblPath;
        private System.Windows.Forms.DataGridView grvFiles;
        private System.Windows.Forms.StatusStrip staProcess;
        private System.Windows.Forms.ToolStripStatusLabel staFolderPath;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Button btnCopyAll;
        private System.Windows.Forms.CheckBox chkIsFilterDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn extension;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn PageNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Uploader;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreatedOn;
        private System.Windows.Forms.DataGridViewTextBoxColumn a4;
        private System.Windows.Forms.DataGridViewTextBoxColumn a3;
        private System.Windows.Forms.DataGridViewTextBoxColumn a2;
        private System.Windows.Forms.DataGridViewTextBoxColumn a1;
        private System.Windows.Forms.DataGridViewTextBoxColumn a0;
    }
}