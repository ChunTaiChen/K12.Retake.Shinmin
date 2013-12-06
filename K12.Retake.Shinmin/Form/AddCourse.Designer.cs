namespace K12.Retake.Shinmin.Form
{
    partial class AddCourse
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
            this.cbxDeptName = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cbxSubjectType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.txtCredit = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtSubjectLevel = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtSubjectName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cbxCourseTeacher = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.txtCourseName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.iptSchoolYear = new DevComponents.Editors.IntegerInput();
            this.iptSemester = new DevComponents.Editors.IntegerInput();
            this.iptMonth = new DevComponents.Editors.IntegerInput();
            ((System.ComponentModel.ISupportInitialize)(this.iptSchoolYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iptSemester)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iptMonth)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxDeptName
            // 
            this.cbxDeptName.DisplayMember = "Text";
            this.cbxDeptName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxDeptName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDeptName.FormattingEnabled = true;
            this.cbxDeptName.ItemHeight = 19;
            this.cbxDeptName.Location = new System.Drawing.Point(327, 147);
            this.cbxDeptName.Name = "cbxDeptName";
            this.cbxDeptName.Size = new System.Drawing.Size(167, 25);
            this.cbxDeptName.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbxDeptName.TabIndex = 7;
            // 
            // cbxSubjectType
            // 
            this.cbxSubjectType.DisplayMember = "Text";
            this.cbxSubjectType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxSubjectType.FormattingEnabled = true;
            this.cbxSubjectType.ItemHeight = 19;
            this.cbxSubjectType.Location = new System.Drawing.Point(328, 60);
            this.cbxSubjectType.Name = "cbxSubjectType";
            this.cbxSubjectType.Size = new System.Drawing.Size(165, 25);
            this.cbxSubjectType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbxSubjectType.TabIndex = 2;
            // 
            // txtCredit
            // 
            // 
            // 
            // 
            this.txtCredit.Border.Class = "TextBoxBorder";
            this.txtCredit.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCredit.Location = new System.Drawing.Point(444, 103);
            this.txtCredit.Name = "txtCredit";
            this.txtCredit.Size = new System.Drawing.Size(50, 25);
            this.txtCredit.TabIndex = 5;
            this.txtCredit.TextChanged += new System.EventHandler(this.txtCredit_TextChanged);
            // 
            // txtSubjectLevel
            // 
            // 
            // 
            // 
            this.txtSubjectLevel.Border.Class = "TextBoxBorder";
            this.txtSubjectLevel.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSubjectLevel.Location = new System.Drawing.Point(327, 103);
            this.txtSubjectLevel.Name = "txtSubjectLevel";
            this.txtSubjectLevel.Size = new System.Drawing.Size(50, 25);
            this.txtSubjectLevel.TabIndex = 4;
            this.txtSubjectLevel.TextChanged += new System.EventHandler(this.txtSubjectLevel_TextChanged);
            // 
            // txtSubjectName
            // 
            // 
            // 
            // 
            this.txtSubjectName.Border.Class = "TextBoxBorder";
            this.txtSubjectName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSubjectName.Location = new System.Drawing.Point(80, 103);
            this.txtSubjectName.Name = "txtSubjectName";
            this.txtSubjectName.Size = new System.Drawing.Size(165, 25);
            this.txtSubjectName.TabIndex = 3;
            this.txtSubjectName.TextChanged += new System.EventHandler(this.txtSubjectName_TextChanged);
            // 
            // cbxCourseTeacher
            // 
            this.cbxCourseTeacher.DisplayMember = "Text";
            this.cbxCourseTeacher.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxCourseTeacher.FormattingEnabled = true;
            this.cbxCourseTeacher.ItemHeight = 19;
            this.cbxCourseTeacher.Location = new System.Drawing.Point(79, 147);
            this.cbxCourseTeacher.Name = "cbxCourseTeacher";
            this.cbxCourseTeacher.Size = new System.Drawing.Size(167, 25);
            this.cbxCourseTeacher.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbxCourseTeacher.TabIndex = 6;
            // 
            // txtCourseName
            // 
            // 
            // 
            // 
            this.txtCourseName.Border.Class = "TextBoxBorder";
            this.txtCourseName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCourseName.Location = new System.Drawing.Point(80, 60);
            this.txtCourseName.Name = "txtCourseName";
            this.txtCourseName.Size = new System.Drawing.Size(165, 25);
            this.txtCourseName.TabIndex = 1;
            this.txtCourseName.TextChanged += new System.EventHandler(this.txtCourseName_TextChanged);
            // 
            // labelX10
            // 
            this.labelX10.AutoSize = true;
            this.labelX10.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX10.BackgroundStyle.Class = "";
            this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX10.Location = new System.Drawing.Point(261, 149);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(60, 21);
            this.labelX10.TabIndex = 26;
            this.labelX10.Text = "科　　別";
            // 
            // labelX9
            // 
            this.labelX9.AutoSize = true;
            this.labelX9.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX9.BackgroundStyle.Class = "";
            this.labelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX9.Location = new System.Drawing.Point(261, 62);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(60, 21);
            this.labelX9.TabIndex = 25;
            this.labelX9.Text = "科目類別";
            // 
            // labelX8
            // 
            this.labelX8.AutoSize = true;
            this.labelX8.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.Class = "";
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(391, 105);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(47, 21);
            this.labelX8.TabIndex = 24;
            this.labelX8.Text = "學分數";
            // 
            // labelX7
            // 
            this.labelX7.AutoSize = true;
            this.labelX7.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.Class = "";
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(261, 105);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(60, 21);
            this.labelX7.TabIndex = 23;
            this.labelX7.Text = "科目級別";
            // 
            // labelX6
            // 
            this.labelX6.AutoSize = true;
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.Class = "";
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(13, 105);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(60, 21);
            this.labelX6.TabIndex = 22;
            this.labelX6.Text = "科目名稱";
            // 
            // labelX5
            // 
            this.labelX5.AutoSize = true;
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(13, 149);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(60, 21);
            this.labelX5.TabIndex = 21;
            this.labelX5.Text = "授課教師";
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(13, 62);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(60, 21);
            this.labelX4.TabIndex = 20;
            this.labelX4.Text = "課程名稱";
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.AutoSize = true;
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(333, 195);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "儲存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.AutoSize = true;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(418, 195);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 25);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
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
            this.labelX1.Location = new System.Drawing.Point(26, 17);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(47, 21);
            this.labelX1.TabIndex = 27;
            this.labelX1.Text = "學年度";
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
            this.labelX2.Location = new System.Drawing.Point(161, 17);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(34, 21);
            this.labelX2.TabIndex = 28;
            this.labelX2.Text = "學期";
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(287, 17);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(34, 21);
            this.labelX3.TabIndex = 29;
            this.labelX3.Text = "梯次";
            // 
            // iptSchoolYear
            // 
            this.iptSchoolYear.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.iptSchoolYear.BackgroundStyle.Class = "DateTimeInputBackground";
            this.iptSchoolYear.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.iptSchoolYear.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.iptSchoolYear.Location = new System.Drawing.Point(79, 15);
            this.iptSchoolYear.MaxValue = 300;
            this.iptSchoolYear.MinValue = 1;
            this.iptSchoolYear.Name = "iptSchoolYear";
            this.iptSchoolYear.ShowUpDown = true;
            this.iptSchoolYear.Size = new System.Drawing.Size(69, 25);
            this.iptSchoolYear.TabIndex = 30;
            this.iptSchoolYear.Value = 1;
            this.iptSchoolYear.ValueChanged += new System.EventHandler(this.iptSchoolYear_ValueChanged);
            // 
            // iptSemester
            // 
            this.iptSemester.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.iptSemester.BackgroundStyle.Class = "DateTimeInputBackground";
            this.iptSemester.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.iptSemester.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.iptSemester.Location = new System.Drawing.Point(201, 15);
            this.iptSemester.MaxValue = 300;
            this.iptSemester.MinValue = 1;
            this.iptSemester.Name = "iptSemester";
            this.iptSemester.ShowUpDown = true;
            this.iptSemester.Size = new System.Drawing.Size(44, 25);
            this.iptSemester.TabIndex = 31;
            this.iptSemester.Value = 1;
            this.iptSemester.ValueChanged += new System.EventHandler(this.iptSemester_ValueChanged);
            // 
            // iptMonth
            // 
            this.iptMonth.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.iptMonth.BackgroundStyle.Class = "DateTimeInputBackground";
            this.iptMonth.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.iptMonth.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.iptMonth.Location = new System.Drawing.Point(328, 15);
            this.iptMonth.MaxValue = 300;
            this.iptMonth.MinValue = 1;
            this.iptMonth.Name = "iptMonth";
            this.iptMonth.ShowUpDown = true;
            this.iptMonth.Size = new System.Drawing.Size(61, 25);
            this.iptMonth.TabIndex = 32;
            this.iptMonth.Value = 1;
            this.iptMonth.ValueChanged += new System.EventHandler(this.iptMonth_ValueChanged);
            // 
            // AddCourse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 231);
            this.Controls.Add(this.iptMonth);
            this.Controls.Add(this.iptSemester);
            this.Controls.Add(this.iptSchoolYear);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbxDeptName);
            this.Controls.Add(this.cbxSubjectType);
            this.Controls.Add(this.txtCredit);
            this.Controls.Add(this.txtSubjectLevel);
            this.Controls.Add(this.txtSubjectName);
            this.Controls.Add(this.cbxCourseTeacher);
            this.Controls.Add(this.txtCourseName);
            this.Controls.Add(this.labelX10);
            this.Controls.Add(this.labelX9);
            this.Controls.Add(this.labelX8);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.labelX4);
            this.DoubleBuffered = true;
            this.Name = "AddCourse";
            this.Text = "新增課程";
            this.Load += new System.EventHandler(this.AddCourse_Load);
            ((System.ComponentModel.ISupportInitialize)(this.iptSchoolYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iptSemester)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iptMonth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx cbxDeptName;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbxSubjectType;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCredit;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSubjectLevel;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSubjectName;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbxCourseTeacher;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCourseName;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.DotNetBar.LabelX labelX9;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.Editors.IntegerInput iptSchoolYear;
        private DevComponents.Editors.IntegerInput iptSemester;
        private DevComponents.Editors.IntegerInput iptMonth;
    }
}