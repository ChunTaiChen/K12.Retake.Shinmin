namespace K12.Retake.Shinmin.Form
{
    partial class SuggestListForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgData = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.btnExportExcel = new DevComponents.DotNetBar.ButtonX();
            this.itmPnlTimeName = new DevComponents.DotNetBar.ItemPanel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.設成目前正在期間ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAdd = new DevComponents.DotNetBar.ButtonX();
            this.btnDel = new DevComponents.DotNetBar.ButtonX();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.iptSelectSchoolYear = new DevComponents.Editors.IntegerInput();
            this.lblMsg = new DevComponents.DotNetBar.LabelX();
            this.colGradeYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDept = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colClassName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStudNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSeatNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRequied = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCredit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSchoolYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScGradeYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSemester = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRetake = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCurSel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iptSelectSchoolYear)).BeginInit();
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
            this.colGradeYear,
            this.colDept,
            this.colClassName,
            this.colStudNumber,
            this.colSeatNo,
            this.colName,
            this.colSubjectName,
            this.colRequied,
            this.colScore,
            this.colCredit,
            this.colSchoolYear,
            this.ScGradeYear,
            this.colSemester,
            this.colRetake,
            this.colCurSel});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgData.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgData.Location = new System.Drawing.Point(190, 43);
            this.dgData.Name = "dgData";
            this.dgData.ReadOnly = true;
            this.dgData.RowHeadersVisible = false;
            this.dgData.RowTemplate.Height = 24;
            this.dgData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgData.Size = new System.Drawing.Size(750, 408);
            this.dgData.TabIndex = 7;
            this.dgData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgData_CellContentClick);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExportExcel.BackColor = System.Drawing.Color.Transparent;
            this.btnExportExcel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExportExcel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExportExcel.Location = new System.Drawing.Point(3, 356);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(170, 25);
            this.btnExportExcel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExportExcel.TabIndex = 13;
            this.btnExportExcel.Text = "匯出";
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // itmPnlTimeName
            // 
            this.itmPnlTimeName.AutoScroll = true;
            this.itmPnlTimeName.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.itmPnlTimeName.BackgroundStyle.Class = "ItemPanel";
            this.itmPnlTimeName.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itmPnlTimeName.ContainerControlProcessDialogKey = true;
            this.itmPnlTimeName.ContextMenuStrip = this.contextMenuStrip1;
            this.itmPnlTimeName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itmPnlTimeName.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itmPnlTimeName.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.itmPnlTimeName.Location = new System.Drawing.Point(3, 3);
            this.itmPnlTimeName.Name = "itmPnlTimeName";
            this.itmPnlTimeName.Size = new System.Drawing.Size(170, 318);
            this.itmPnlTimeName.TabIndex = 14;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.設成目前正在期間ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(173, 26);
            this.contextMenuStrip1.Click += new System.EventHandler(this.contextMenuStrip1_Click);
            // 
            // 設成目前正在期間ToolStripMenuItem
            // 
            this.設成目前正在期間ToolStripMenuItem.Name = "設成目前正在期間ToolStripMenuItem";
            this.設成目前正在期間ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.設成目前正在期間ToolStripMenuItem.Text = "設成目前工作名單";
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Location = new System.Drawing.Point(2, 326);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(172, 25);
            this.btnAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAdd.TabIndex = 16;
            this.btnAdd.Text = "新增";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDel
            // 
            this.btnDel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDel.BackColor = System.Drawing.Color.Transparent;
            this.btnDel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDel.Location = new System.Drawing.Point(2, 386);
            this.btnDel.Margin = new System.Windows.Forms.Padding(2);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(172, 25);
            this.btnDel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDel.TabIndex = 17;
            this.btnDel.Text = "刪除";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btnAdd, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.itmPnlTimeName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnDel, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnExportExcel, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 40);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(176, 413);
            this.tableLayoutPanel1.TabIndex = 18;
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
            this.labelX1.Location = new System.Drawing.Point(11, 13);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(47, 21);
            this.labelX1.TabIndex = 19;
            this.labelX1.Text = "學年度";
            // 
            // iptSelectSchoolYear
            // 
            this.iptSelectSchoolYear.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.iptSelectSchoolYear.BackgroundStyle.Class = "DateTimeInputBackground";
            this.iptSelectSchoolYear.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.iptSelectSchoolYear.ButtonFreeText.Checked = true;
            this.iptSelectSchoolYear.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.iptSelectSchoolYear.FreeTextEntryMode = true;
            this.iptSelectSchoolYear.Location = new System.Drawing.Point(64, 11);
            this.iptSelectSchoolYear.MaxValue = 3000;
            this.iptSelectSchoolYear.MinValue = 1;
            this.iptSelectSchoolYear.Name = "iptSelectSchoolYear";
            this.iptSelectSchoolYear.ShowUpDown = true;
            this.iptSelectSchoolYear.Size = new System.Drawing.Size(109, 25);
            this.iptSelectSchoolYear.TabIndex = 20;
            this.iptSelectSchoolYear.Value = 1;
            this.iptSelectSchoolYear.ValueChanged += new System.EventHandler(this.iptSelectSchoolYear_ValueChanged);
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblMsg.BackgroundStyle.Class = "";
            this.lblMsg.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblMsg.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblMsg.Location = new System.Drawing.Point(197, 12);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(669, 23);
            this.lblMsg.TabIndex = 21;
            this.lblMsg.Text = " ";
            // 
            // colGradeYear
            // 
            this.colGradeYear.DataPropertyName = "年級";
            this.colGradeYear.HeaderText = "年級";
            this.colGradeYear.Name = "colGradeYear";
            this.colGradeYear.ReadOnly = true;
            this.colGradeYear.Width = 60;
            // 
            // colDept
            // 
            this.colDept.DataPropertyName = "科別";
            this.colDept.HeaderText = "科別";
            this.colDept.Name = "colDept";
            this.colDept.ReadOnly = true;
            // 
            // colClassName
            // 
            this.colClassName.DataPropertyName = "班級";
            this.colClassName.HeaderText = "班級";
            this.colClassName.Name = "colClassName";
            this.colClassName.ReadOnly = true;
            // 
            // colStudNumber
            // 
            this.colStudNumber.DataPropertyName = "學號";
            this.colStudNumber.HeaderText = "學號";
            this.colStudNumber.Name = "colStudNumber";
            this.colStudNumber.ReadOnly = true;
            // 
            // colSeatNo
            // 
            this.colSeatNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colSeatNo.DataPropertyName = "座號";
            this.colSeatNo.HeaderText = "座號";
            this.colSeatNo.Name = "colSeatNo";
            this.colSeatNo.ReadOnly = true;
            this.colSeatNo.Width = 59;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "姓名";
            this.colName.HeaderText = "姓名";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colSubjectName
            // 
            this.colSubjectName.DataPropertyName = "科目名稱";
            this.colSubjectName.HeaderText = "科目名稱";
            this.colSubjectName.Name = "colSubjectName";
            this.colSubjectName.ReadOnly = true;
            this.colSubjectName.Width = 120;
            // 
            // colRequied
            // 
            this.colRequied.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colRequied.DataPropertyName = "必選修";
            this.colRequied.HeaderText = "必/選";
            this.colRequied.Name = "colRequied";
            this.colRequied.ReadOnly = true;
            this.colRequied.Width = 64;
            // 
            // colScore
            // 
            this.colScore.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colScore.DataPropertyName = "成績";
            this.colScore.HeaderText = "成績";
            this.colScore.Name = "colScore";
            this.colScore.ReadOnly = true;
            this.colScore.Width = 59;
            // 
            // colCredit
            // 
            this.colCredit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colCredit.DataPropertyName = "學分";
            this.colCredit.HeaderText = "學分";
            this.colCredit.Name = "colCredit";
            this.colCredit.ReadOnly = true;
            this.colCredit.Width = 59;
            // 
            // colSchoolYear
            // 
            this.colSchoolYear.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colSchoolYear.DataPropertyName = "學年度";
            this.colSchoolYear.HeaderText = "學年度";
            this.colSchoolYear.Name = "colSchoolYear";
            this.colSchoolYear.ReadOnly = true;
            this.colSchoolYear.Width = 72;
            // 
            // ScGradeYear
            // 
            this.ScGradeYear.DataPropertyName = "成績年級";
            this.ScGradeYear.HeaderText = "成績年級";
            this.ScGradeYear.Name = "ScGradeYear";
            this.ScGradeYear.ReadOnly = true;
            // 
            // colSemester
            // 
            this.colSemester.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colSemester.DataPropertyName = "學期";
            this.colSemester.HeaderText = "學期";
            this.colSemester.Name = "colSemester";
            this.colSemester.ReadOnly = true;
            this.colSemester.Width = 59;
            // 
            // colRetake
            // 
            this.colRetake.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colRetake.DataPropertyName = "重補修";
            this.colRetake.HeaderText = "重/補";
            this.colRetake.Name = "colRetake";
            this.colRetake.ReadOnly = true;
            this.colRetake.Width = 64;
            // 
            // colCurSel
            // 
            this.colCurSel.DataPropertyName = "本學期修課";
            this.colCurSel.HeaderText = "本學期修課";
            this.colCurSel.Name = "colCurSel";
            this.colCurSel.ReadOnly = true;
            // 
            // SuggestListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 457);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.iptSelectSchoolYear);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.dgData);
            this.DoubleBuffered = true;
            this.MaximizeBox = true;
            this.Name = "SuggestListForm";
            this.Text = "建議重補修名單";
            this.Load += new System.EventHandler(this.SuggestListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iptSelectSchoolYear)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgData;
        private DevComponents.DotNetBar.ButtonX btnExportExcel;
        private DevComponents.DotNetBar.ItemPanel itmPnlTimeName;
        private DevComponents.DotNetBar.ButtonX btnAdd;
        private DevComponents.DotNetBar.ButtonX btnDel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 設成目前正在期間ToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.Editors.IntegerInput iptSelectSchoolYear;
        private DevComponents.DotNetBar.LabelX lblMsg;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGradeYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDept;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClassName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStudNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSeatNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRequied;
        private System.Windows.Forms.DataGridViewTextBoxColumn colScore;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCredit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSchoolYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScGradeYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSemester;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRetake;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCurSel;
    }
}