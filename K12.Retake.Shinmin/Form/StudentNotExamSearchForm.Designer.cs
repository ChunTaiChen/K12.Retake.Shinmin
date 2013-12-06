namespace K12.Retake.Shinmin.Form
{
    partial class StudentNotExamSearchForm
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
            this.dgData = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.btnExport = new DevComponents.DotNetBar.ButtonX();
            this.btnQry = new DevComponents.DotNetBar.ButtonX();
            this.btnRun = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.txtCal = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.ColCourseName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCoSeatNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColStudName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColAttendCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCourseScCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNotExam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
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
            this.ColCourseName,
            this.colCoSeatNo,
            this.ColStudName,
            this.ColAttendCount,
            this.ColCourseScCount,
            this.colNotExam});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgData.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgData.Location = new System.Drawing.Point(13, 41);
            this.dgData.Name = "dgData";
            this.dgData.RowTemplate.Height = 24;
            this.dgData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgData.Size = new System.Drawing.Size(599, 233);
            this.dgData.TabIndex = 0;
            this.dgData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgData_CellContentClick);
            this.dgData.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgData_CellContentDoubleClick);
            // 
            // btnExport
            // 
            this.btnExport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.AutoSize = true;
            this.btnExport.BackColor = System.Drawing.Color.Transparent;
            this.btnExport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExport.Location = new System.Drawing.Point(13, 280);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 25);
            this.btnExport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "匯出";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnQry
            // 
            this.btnQry.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnQry.AutoSize = true;
            this.btnQry.BackColor = System.Drawing.Color.Transparent;
            this.btnQry.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnQry.Location = new System.Drawing.Point(139, 10);
            this.btnQry.Name = "btnQry";
            this.btnQry.Size = new System.Drawing.Size(75, 25);
            this.btnQry.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnQry.TabIndex = 2;
            this.btnQry.Text = "查詢";
            this.btnQry.Click += new System.EventHandler(this.btnQry_Click);
            // 
            // btnRun
            // 
            this.btnRun.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.AutoSize = true;
            this.btnRun.BackColor = System.Drawing.Color.Transparent;
            this.btnRun.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRun.Location = new System.Drawing.Point(456, 280);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 25);
            this.btnRun.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRun.TabIndex = 3;
            this.btnRun.Text = "執行扣考";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.AutoSize = true;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(537, 280);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 25);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtCal
            // 
            // 
            // 
            // 
            this.txtCal.Border.Class = "TextBoxBorder";
            this.txtCal.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCal.Location = new System.Drawing.Point(62, 10);
            this.txtCal.Name = "txtCal";
            this.txtCal.Size = new System.Drawing.Size(62, 25);
            this.txtCal.TabIndex = 5;
            this.txtCal.Text = "1/3";
            this.txtCal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(13, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(34, 21);
            this.labelX1.TabIndex = 6;
            this.labelX1.Text = "比例";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(229, 12);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(167, 21);
            this.labelX2.TabIndex = 7;
            this.labelX2.Text = "說明：預設比例為三分之一";
            // 
            // ColCourseName
            // 
            this.ColCourseName.HeaderText = "課程名稱";
            this.ColCourseName.Name = "ColCourseName";
            this.ColCourseName.ReadOnly = true;
            this.ColCourseName.Width = 150;
            // 
            // colCoSeatNo
            // 
            this.colCoSeatNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colCoSeatNo.HeaderText = "課程座號";
            this.colCoSeatNo.Name = "colCoSeatNo";
            this.colCoSeatNo.ReadOnly = true;
            this.colCoSeatNo.Width = 85;
            // 
            // ColStudName
            // 
            this.ColStudName.HeaderText = "學生姓名";
            this.ColStudName.Name = "ColStudName";
            this.ColStudName.ReadOnly = true;
            // 
            // ColAttendCount
            // 
            this.ColAttendCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ColAttendCount.HeaderText = "缺課節數";
            this.ColAttendCount.Name = "ColAttendCount";
            this.ColAttendCount.ReadOnly = true;
            this.ColAttendCount.Width = 85;
            // 
            // ColCourseScCount
            // 
            this.ColCourseScCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ColCourseScCount.HeaderText = "總節數";
            this.ColCourseScCount.Name = "ColCourseScCount";
            this.ColCourseScCount.ReadOnly = true;
            this.ColCourseScCount.Width = 72;
            // 
            // colNotExam
            // 
            this.colNotExam.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colNotExam.HeaderText = "扣考";
            this.colNotExam.Name = "colNotExam";
            this.colNotExam.ReadOnly = true;
            this.colNotExam.Width = 59;
            // 
            // StudentNotExamSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 315);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.txtCal);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.btnQry);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.dgData);
            this.DoubleBuffered = true;
            this.Name = "StudentNotExamSearchForm";
            this.Text = "學生扣考查詢";
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgData;
        private DevComponents.DotNetBar.ButtonX btnExport;
        private DevComponents.DotNetBar.ButtonX btnQry;
        private DevComponents.DotNetBar.ButtonX btnRun;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCal;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCourseName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCoSeatNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColStudName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColAttendCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCourseScCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNotExam;
    }
}