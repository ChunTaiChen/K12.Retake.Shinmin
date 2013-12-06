namespace K12.Retake.Shinmin.Form
{
    partial class CreateCourseInfoForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgData = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.txtStudMaxCount = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.dgDate = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.btnStart = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.lnkSetDate = new System.Windows.Forms.LinkLabel();
            this.lblMsg = new DevComponents.DotNetBar.LabelX();
            this.chkAll = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colSubjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDeptName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCourseTable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStudCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCourseCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDate)).BeginInit();
            this.SuspendLayout();
            // 
            // dgData
            // 
            this.dgData.AllowUserToAddRows = false;
            this.dgData.AllowUserToDeleteRows = false;
            this.dgData.AllowUserToResizeRows = false;
            this.dgData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgData.BackgroundColor = System.Drawing.Color.White;
            this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.colSubjectName,
            this.colDeptName,
            this.colCourseTable,
            this.colStudCount,
            this.colCourseCount});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgData.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgData.Location = new System.Drawing.Point(184, 12);
            this.dgData.Name = "dgData";
            this.dgData.RowHeadersVisible = false;
            this.dgData.RowTemplate.Height = 24;
            this.dgData.Size = new System.Drawing.Size(524, 353);
            this.dgData.TabIndex = 3;
            this.dgData.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgData_DataError);
            // 
            // txtStudMaxCount
            // 
            this.txtStudMaxCount.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            // 
            // 
            // 
            this.txtStudMaxCount.Border.Class = "TextBoxBorder";
            this.txtStudMaxCount.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtStudMaxCount.Location = new System.Drawing.Point(352, 381);
            this.txtStudMaxCount.Name = "txtStudMaxCount";
            this.txtStudMaxCount.Size = new System.Drawing.Size(50, 25);
            this.txtStudMaxCount.TabIndex = 21;
            this.txtStudMaxCount.Text = "48";
            this.txtStudMaxCount.TextChanged += new System.EventHandler(this.txtStudMaxCount_TextChanged);
            this.txtStudMaxCount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStudMaxCount_KeyDown);
            this.txtStudMaxCount.Leave += new System.EventHandler(this.txtStudMaxCount_Leave);
            // 
            // labelX2
            // 
            this.labelX2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelX2.AutoSize = true;
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(245, 383);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(101, 21);
            this.labelX2.TabIndex = 20;
            this.labelX2.Text = "課程人數上限：";
            // 
            // dgDate
            // 
            this.dgDate.AllowUserToAddRows = false;
            this.dgDate.AllowUserToResizeRows = false;
            this.dgDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgDate.BackgroundColor = System.Drawing.Color.White;
            this.dgDate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDate.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDate});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDate.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgDate.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgDate.Location = new System.Drawing.Point(15, 12);
            this.dgDate.Name = "dgDate";
            this.dgDate.RowTemplate.Height = 24;
            this.dgDate.Size = new System.Drawing.Size(163, 353);
            this.dgDate.TabIndex = 28;
            // 
            // btnStart
            // 
            this.btnStart.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.AutoSize = true;
            this.btnStart.BackColor = System.Drawing.Color.Transparent;
            this.btnStart.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnStart.Location = new System.Drawing.Point(552, 381);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 25);
            this.btnStart.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnStart.TabIndex = 27;
            this.btnStart.Text = "立即開課";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.AutoSize = true;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(633, 381);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 25);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 29;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lnkSetDate
            // 
            this.lnkSetDate.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lnkSetDate.AutoSize = true;
            this.lnkSetDate.BackColor = System.Drawing.Color.Transparent;
            this.lnkSetDate.Location = new System.Drawing.Point(12, 385);
            this.lnkSetDate.Name = "lnkSetDate";
            this.lnkSetDate.Size = new System.Drawing.Size(86, 17);
            this.lnkSetDate.TabIndex = 30;
            this.lnkSetDate.TabStop = true;
            this.lnkSetDate.Text = "取得上課日期";
            this.lnkSetDate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSetDate_LinkClicked);
            // 
            // lblMsg
            // 
            this.lblMsg.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblMsg.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblMsg.BackgroundStyle.Class = "";
            this.lblMsg.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblMsg.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblMsg.ForeColor = System.Drawing.Color.Red;
            this.lblMsg.Location = new System.Drawing.Point(417, 382);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(129, 23);
            this.lblMsg.TabIndex = 31;
            this.lblMsg.Text = "資料讀取中..";
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkAll.BackgroundStyle.Class = "";
            this.chkAll.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkAll.Location = new System.Drawing.Point(184, 383);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(54, 21);
            this.chkAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkAll.TabIndex = 32;
            this.chkAll.Text = "全選";
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // colCheck
            // 
            this.colCheck.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colCheck.FalseValue = "false";
            this.colCheck.HeaderText = "";
            this.colCheck.Name = "colCheck";
            this.colCheck.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCheck.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colCheck.TrueValue = "true";
            this.colCheck.Width = 19;
            // 
            // colSubjectName
            // 
            this.colSubjectName.HeaderText = "科目名稱";
            this.colSubjectName.Name = "colSubjectName";
            this.colSubjectName.ReadOnly = true;
            // 
            // colDeptName
            // 
            this.colDeptName.HeaderText = "科別";
            this.colDeptName.Name = "colDeptName";
            this.colDeptName.ReadOnly = true;
            // 
            // colCourseTable
            // 
            this.colCourseTable.HeaderText = "課表";
            this.colCourseTable.Name = "colCourseTable";
            this.colCourseTable.ReadOnly = true;
            // 
            // colStudCount
            // 
            this.colStudCount.HeaderText = "人數";
            this.colStudCount.Name = "colStudCount";
            this.colStudCount.ReadOnly = true;
            // 
            // colCourseCount
            // 
            this.colCourseCount.HeaderText = "開課數量";
            this.colCourseCount.Name = "colCourseCount";
            // 
            // colDate
            // 
            this.colDate.HeaderText = "上課日期";
            this.colDate.Name = "colDate";
            this.colDate.ReadOnly = true;
            this.colDate.Width = 120;
            // 
            // CreateCourseInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 416);
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.lnkSetDate);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.dgDate);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.dgData);
            this.Controls.Add(this.txtStudMaxCount);
            this.Controls.Add(this.labelX2);
            this.DoubleBuffered = true;
            this.Name = "CreateCourseInfoForm";
            this.Text = "重補修開課";
            this.Load += new System.EventHandler(this.CreateCourseInfoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgData;
        private DevComponents.DotNetBar.Controls.TextBoxX txtStudMaxCount;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgDate;
        private DevComponents.DotNetBar.ButtonX btnStart;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private System.Windows.Forms.LinkLabel lnkSetDate;
        private DevComponents.DotNetBar.LabelX lblMsg;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkAll;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDeptName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCourseTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStudCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCourseCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;

    }
}

