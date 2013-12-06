namespace K12.Retake.Shinmin.DetailContent
{
    partial class CourseStudentContent
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lvStudentList = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.colOClassName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOSeatNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSeatNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStudentNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colGr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMark = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmClearTemp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAddStudentTemp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmWMark = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsCMark = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmRemoveStudent = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddTempStudent = new DevComponents.DotNetBar.ButtonX();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.btnRemoveStudent = new DevComponents.DotNetBar.ButtonX();
            this.lblStudentCount = new DevComponents.DotNetBar.LabelX();
            this.設成重修ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.設成補修ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvStudentList
            // 
            // 
            // 
            // 
            this.lvStudentList.Border.Class = "ListViewBorder";
            this.lvStudentList.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lvStudentList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colOClassName,
            this.colOSeatNo,
            this.colSeatNo,
            this.colName,
            this.colStudentNumber,
            this.colGr,
            this.colMark,
            this.colStatus,
            this.colType});
            this.lvStudentList.ContextMenuStrip = this.contextMenuStrip1;
            this.lvStudentList.FullRowSelect = true;
            this.lvStudentList.HideSelection = false;
            this.lvStudentList.Location = new System.Drawing.Point(13, 8);
            this.lvStudentList.Name = "lvStudentList";
            this.lvStudentList.Size = new System.Drawing.Size(524, 215);
            this.lvStudentList.TabIndex = 0;
            this.lvStudentList.UseCompatibleStateImageBehavior = false;
            this.lvStudentList.View = System.Windows.Forms.View.Details;
            // 
            // colOClassName
            // 
            this.colOClassName.Text = "班級";
            this.colOClassName.Width = 70;
            // 
            // colOSeatNo
            // 
            this.colOSeatNo.Text = "座號";
            this.colOSeatNo.Width = 50;
            // 
            // colSeatNo
            // 
            this.colSeatNo.Text = "課程座號";
            this.colSeatNo.Width = 80;
            // 
            // colName
            // 
            this.colName.Text = "姓名";
            this.colName.Width = 80;
            // 
            // colStudentNumber
            // 
            this.colStudentNumber.Text = "學號";
            this.colStudentNumber.Width = 70;
            // 
            // colGr
            // 
            this.colGr.Text = "性別";
            // 
            // colMark
            // 
            this.colMark.Text = "扣考";
            // 
            // colStatus
            // 
            this.colStatus.Text = "狀態";
            this.colStatus.Width = 55;
            // 
            // colType
            // 
            this.colType.Text = "重修/補修";
            this.colType.Width = 100;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmClearTemp,
            this.tsmAddStudentTemp,
            this.toolStripSeparator1,
            this.tsmWMark,
            this.tmsCMark,
            this.toolStripSeparator2,
            this.tsmRemoveStudent,
            this.toolStripSeparator3,
            this.設成重修ToolStripMenuItem,
            this.設成補修ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(173, 198);
            // 
            // tsmClearTemp
            // 
            this.tsmClearTemp.Name = "tsmClearTemp";
            this.tsmClearTemp.Size = new System.Drawing.Size(172, 22);
            this.tsmClearTemp.Text = "清空待處理";
            this.tsmClearTemp.Click += new System.EventHandler(this.tsmClearTemp_Click);
            // 
            // tsmAddStudentTemp
            // 
            this.tsmAddStudentTemp.Name = "tsmAddStudentTemp";
            this.tsmAddStudentTemp.Size = new System.Drawing.Size(172, 22);
            this.tsmAddStudentTemp.Text = "將學生加入待處理";
            this.tsmAddStudentTemp.Click += new System.EventHandler(this.tsmAddStudentTemp_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(169, 6);
            // 
            // tsmWMark
            // 
            this.tsmWMark.Name = "tsmWMark";
            this.tsmWMark.Size = new System.Drawing.Size(172, 22);
            this.tsmWMark.Text = "執行扣考";
            this.tsmWMark.Click += new System.EventHandler(this.tsmWMark_Click);
            // 
            // tmsCMark
            // 
            this.tmsCMark.Name = "tmsCMark";
            this.tmsCMark.Size = new System.Drawing.Size(172, 22);
            this.tmsCMark.Text = "解除扣考";
            this.tmsCMark.Click += new System.EventHandler(this.tmsCMark_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(169, 6);
            // 
            // tsmRemoveStudent
            // 
            this.tsmRemoveStudent.Name = "tsmRemoveStudent";
            this.tsmRemoveStudent.Size = new System.Drawing.Size(172, 22);
            this.tsmRemoveStudent.Text = "移除選擇學生";
            this.tsmRemoveStudent.Click += new System.EventHandler(this.tsmRemoveStudent_Click);
            // 
            // btnAddTempStudent
            // 
            this.btnAddTempStudent.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddTempStudent.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAddTempStudent.Location = new System.Drawing.Point(131, 229);
            this.btnAddTempStudent.Name = "btnAddTempStudent";
            this.btnAddTempStudent.Size = new System.Drawing.Size(112, 25);
            this.btnAddTempStudent.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem2});
            this.btnAddTempStudent.TabIndex = 7;
            this.btnAddTempStudent.Text = "加入待處理學生";
            this.btnAddTempStudent.PopupOpen += new System.EventHandler(this.btnAddTempStudent_PopupOpen);
            this.btnAddTempStudent.Click += new System.EventHandler(this.btnAddTempStudent_Click);
            // 
            // buttonItem2
            // 
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.Text = "New Item";
            // 
            // btnRemoveStudent
            // 
            this.btnRemoveStudent.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRemoveStudent.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRemoveStudent.Location = new System.Drawing.Point(13, 229);
            this.btnRemoveStudent.Name = "btnRemoveStudent";
            this.btnRemoveStudent.Size = new System.Drawing.Size(112, 25);
            this.btnRemoveStudent.TabIndex = 6;
            this.btnRemoveStudent.Text = "移除所選學生";
            this.btnRemoveStudent.Click += new System.EventHandler(this.btnRemoveStudent_Click);
            // 
            // lblStudentCount
            // 
            this.lblStudentCount.AutoSize = true;
            // 
            // 
            // 
            this.lblStudentCount.BackgroundStyle.Class = "";
            this.lblStudentCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblStudentCount.Location = new System.Drawing.Point(384, 231);
            this.lblStudentCount.Name = "lblStudentCount";
            this.lblStudentCount.Size = new System.Drawing.Size(13, 21);
            this.lblStudentCount.TabIndex = 8;
            this.lblStudentCount.Text = " ";
            // 
            // 設成重修ToolStripMenuItem
            // 
            this.設成重修ToolStripMenuItem.Name = "設成重修ToolStripMenuItem";
            this.設成重修ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.設成重修ToolStripMenuItem.Text = "設成重修";
            this.設成重修ToolStripMenuItem.Click += new System.EventHandler(this.設成重修ToolStripMenuItem_Click);
            // 
            // 設成補修ToolStripMenuItem
            // 
            this.設成補修ToolStripMenuItem.Name = "設成補修ToolStripMenuItem";
            this.設成補修ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.設成補修ToolStripMenuItem.Text = "設成補修";
            this.設成補修ToolStripMenuItem.Click += new System.EventHandler(this.設成補修ToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(169, 6);
            // 
            // CourseStudentContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblStudentCount);
            this.Controls.Add(this.btnAddTempStudent);
            this.Controls.Add(this.btnRemoveStudent);
            this.Controls.Add(this.lvStudentList);
            this.Name = "CourseStudentContent";
            this.Size = new System.Drawing.Size(550, 265);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ListViewEx lvStudentList;
        private DevComponents.DotNetBar.ButtonX btnAddTempStudent;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private DevComponents.DotNetBar.ButtonX btnRemoveStudent;
        private System.Windows.Forms.ColumnHeader colSeatNo;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colStudentNumber;
        private System.Windows.Forms.ColumnHeader colStatus;
        private DevComponents.DotNetBar.LabelX lblStudentCount;
        private System.Windows.Forms.ColumnHeader colOClassName;
        private System.Windows.Forms.ColumnHeader colOSeatNo;
        private System.Windows.Forms.ColumnHeader colGr;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmAddStudentTemp;
        private System.Windows.Forms.ToolStripMenuItem tsmClearTemp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmWMark;
        private System.Windows.Forms.ToolStripMenuItem tmsCMark;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmRemoveStudent;
        private System.Windows.Forms.ColumnHeader colMark;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem 設成重修ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 設成補修ToolStripMenuItem;
    }
}
