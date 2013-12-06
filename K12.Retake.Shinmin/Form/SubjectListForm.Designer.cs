namespace K12.Retake.Shinmin.Form
{
    partial class SubjectListForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgData = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colSubjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubjectLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCredit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDept = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
            this.colCourseTimetable = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
            this.colSubjectType = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
            this.colWp1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWp2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWp3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWp4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWp5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWp6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWp7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWp8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.批次修改科別ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.批次修改所屬課表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.批次修改科目類別ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTitle = new DevComponents.DotNetBar.LabelX();
            this.btnGetSuggestSubject = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.lblCount = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgData
            // 
            this.dgData.AllowUserToResizeRows = false;
            this.dgData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgData.BackgroundColor = System.Drawing.Color.White;
            this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSubjectName,
            this.colSubjectLevel,
            this.colCredit,
            this.colDept,
            this.colCourseTimetable,
            this.colSubjectType,
            this.colWp1,
            this.colWp2,
            this.colWp3,
            this.colWp4,
            this.colWp5,
            this.colWp6,
            this.colWp7,
            this.colWp8});
            this.dgData.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgData.Location = new System.Drawing.Point(7, 39);
            this.dgData.Name = "dgData";
            this.dgData.RowTemplate.Height = 24;
            this.dgData.Size = new System.Drawing.Size(817, 314);
            this.dgData.TabIndex = 0;
            this.dgData.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgData_CellEndEdit);
            this.dgData.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgData_CellMouseDoubleClick);
            this.dgData.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgData_UserDeletingRow);
            this.dgData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgData_KeyDown);
            // 
            // colSubjectName
            // 
            this.colSubjectName.HeaderText = "科目名稱";
            this.colSubjectName.Name = "colSubjectName";
            // 
            // colSubjectLevel
            // 
            this.colSubjectLevel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colSubjectLevel.HeaderText = "級別";
            this.colSubjectLevel.Name = "colSubjectLevel";
            this.colSubjectLevel.Width = 59;
            // 
            // colCredit
            // 
            this.colCredit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colCredit.HeaderText = "學分數";
            this.colCredit.Name = "colCredit";
            this.colCredit.Width = 72;
            // 
            // colDept
            // 
            this.colDept.DisplayMember = "Text";
            this.colDept.DropDownHeight = 106;
            this.colDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colDept.DropDownWidth = 121;
            this.colDept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colDept.HeaderText = "科別";
            this.colDept.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.colDept.IntegralHeight = false;
            this.colDept.ItemHeight = 17;
            this.colDept.Name = "colDept";
            this.colDept.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colDept.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.colDept.Width = 115;
            // 
            // colCourseTimetable
            // 
            this.colCourseTimetable.DisplayMember = "Text";
            this.colCourseTimetable.DropDownHeight = 106;
            this.colCourseTimetable.DropDownWidth = 121;
            this.colCourseTimetable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colCourseTimetable.HeaderText = "所屬課表";
            this.colCourseTimetable.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.colCourseTimetable.IntegralHeight = false;
            this.colCourseTimetable.ItemHeight = 20;
            this.colCourseTimetable.Name = "colCourseTimetable";
            this.colCourseTimetable.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCourseTimetable.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // colSubjectType
            // 
            this.colSubjectType.DisplayMember = "Text";
            this.colSubjectType.DropDownHeight = 106;
            this.colSubjectType.DropDownWidth = 121;
            this.colSubjectType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colSubjectType.HeaderText = "科目類別";
            this.colSubjectType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.colSubjectType.IntegralHeight = false;
            this.colSubjectType.ItemHeight = 17;
            this.colSubjectType.Name = "colSubjectType";
            this.colSubjectType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colSubjectType.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.colSubjectType.Width = 85;
            // 
            // colWp1
            // 
            this.colWp1.FillWeight = 50F;
            this.colWp1.HeaderText = "一";
            this.colWp1.Name = "colWp1";
            this.colWp1.ReadOnly = true;
            this.colWp1.Width = 30;
            // 
            // colWp2
            // 
            this.colWp2.HeaderText = "二";
            this.colWp2.Name = "colWp2";
            this.colWp2.ReadOnly = true;
            this.colWp2.Width = 30;
            // 
            // colWp3
            // 
            this.colWp3.HeaderText = "三";
            this.colWp3.Name = "colWp3";
            this.colWp3.ReadOnly = true;
            this.colWp3.Width = 30;
            // 
            // colWp4
            // 
            this.colWp4.HeaderText = "四";
            this.colWp4.Name = "colWp4";
            this.colWp4.ReadOnly = true;
            this.colWp4.Width = 30;
            // 
            // colWp5
            // 
            this.colWp5.HeaderText = "五";
            this.colWp5.Name = "colWp5";
            this.colWp5.ReadOnly = true;
            this.colWp5.Width = 30;
            // 
            // colWp6
            // 
            this.colWp6.HeaderText = "六";
            this.colWp6.Name = "colWp6";
            this.colWp6.ReadOnly = true;
            this.colWp6.Width = 30;
            // 
            // colWp7
            // 
            this.colWp7.HeaderText = "七";
            this.colWp7.Name = "colWp7";
            this.colWp7.ReadOnly = true;
            this.colWp7.Width = 30;
            // 
            // colWp8
            // 
            this.colWp8.HeaderText = "八";
            this.colWp8.Name = "colWp8";
            this.colWp8.ReadOnly = true;
            this.colWp8.Width = 30;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.批次修改科別ToolStripMenuItem,
            this.批次修改所屬課表ToolStripMenuItem,
            this.批次修改科目類別ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(167, 92);
            // 
            // 批次修改科別ToolStripMenuItem
            // 
            this.批次修改科別ToolStripMenuItem.Name = "批次修改科別ToolStripMenuItem";
            this.批次修改科別ToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.批次修改科別ToolStripMenuItem.Text = "批次修改科別";
            this.批次修改科別ToolStripMenuItem.Click += new System.EventHandler(this.批次修改科別ToolStripMenuItem_Click);
            // 
            // 批次修改所屬課表ToolStripMenuItem
            // 
            this.批次修改所屬課表ToolStripMenuItem.Name = "批次修改所屬課表ToolStripMenuItem";
            this.批次修改所屬課表ToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.批次修改所屬課表ToolStripMenuItem.Text = "批次修改所屬課表";
            this.批次修改所屬課表ToolStripMenuItem.Click += new System.EventHandler(this.批次修改所屬課表ToolStripMenuItem_Click);
            // 
            // 批次修改科目類別ToolStripMenuItem
            // 
            this.批次修改科目類別ToolStripMenuItem.Name = "批次修改科目類別ToolStripMenuItem";
            this.批次修改科目類別ToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.批次修改科目類別ToolStripMenuItem.Text = "批次修改科目類別";
            this.批次修改科目類別ToolStripMenuItem.Click += new System.EventHandler(this.批次修改科目類別ToolStripMenuItem_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblTitle.BackgroundStyle.Class = "";
            this.lblTitle.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTitle.Location = new System.Drawing.Point(7, 7);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(156, 26);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "學年度　學期　梯次";
            // 
            // btnGetSuggestSubject
            // 
            this.btnGetSuggestSubject.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnGetSuggestSubject.BackColor = System.Drawing.Color.Transparent;
            this.btnGetSuggestSubject.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnGetSuggestSubject.Location = new System.Drawing.Point(654, 8);
            this.btnGetSuggestSubject.Name = "btnGetSuggestSubject";
            this.btnGetSuggestSubject.Size = new System.Drawing.Size(170, 25);
            this.btnGetSuggestSubject.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnGetSuggestSubject.TabIndex = 2;
            this.btnGetSuggestSubject.Text = "取得建議重修科目";
            this.btnGetSuggestSubject.Click += new System.EventHandler(this.btnGetSuggestSubject_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.AutoSize = true;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(742, 363);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(82, 25);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.AutoSize = true;
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(654, 363);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(82, 25);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "儲存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCount.AutoSize = true;
            this.lblCount.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblCount.BackgroundStyle.Class = "";
            this.lblCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblCount.Location = new System.Drawing.Point(7, 364);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(74, 21);
            this.lblCount.TabIndex = 8;
            this.lblCount.Text = "資料筆數：";
            // 
            // labelX1
            // 
            this.labelX1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelX1.AutoSize = true;
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(190, 364);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(336, 21);
            this.labelX1.TabIndex = 9;
            this.labelX1.Text = "節次：鍵入\"V\"鍵勾選，空白鍵與Delete鍵可清除資料。";
            // 
            // SubjectListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 395);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnGetSuggestSubject);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dgData);
            this.DoubleBuffered = true;
            this.MaximizeBox = true;
            this.Name = "SubjectListForm";
            this.Text = "重補修科目管理";
            this.Load += new System.EventHandler(this.SubjectListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgData;
        private DevComponents.DotNetBar.LabelX lblTitle;
        private DevComponents.DotNetBar.ButtonX btnGetSuggestSubject;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.LabelX lblCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubjectLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCredit;
        private DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn colDept;
        private DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn colCourseTimetable;
        private DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn colSubjectType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWp1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWp2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWp3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWp4;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWp5;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWp6;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWp7;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWp8;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 批次修改科別ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 批次修改所屬課表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 批次修改科目類別ToolStripMenuItem;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}