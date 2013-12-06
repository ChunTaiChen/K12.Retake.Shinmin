namespace K12.Retake.Shinmin.Report
{
    partial class StudentAttendanceReportForm
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.lvwDate = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.lnkUpload = new System.Windows.Forms.LinkLabel();
            this.lnkUserDef = new System.Windows.Forms.LinkLabel();
            this.lnkDefault = new System.Windows.Forms.LinkLabel();
            this.rbUserDef = new System.Windows.Forms.RadioButton();
            this.rbDefault = new System.Windows.Forms.RadioButton();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cboMAddress = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboMName = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.chkDateAll = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkExportStudInfo = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.btnPrint = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            this.SuspendLayout();
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
            this.labelX1.Location = new System.Drawing.Point(13, 121);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(60, 21);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "上課日期";
            // 
            // lvwDate
            // 
            this.lvwDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.lvwDate.Border.Class = "ListViewBorder";
            this.lvwDate.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lvwDate.CheckBoxes = true;
            this.lvwDate.Location = new System.Drawing.Point(11, 148);
            this.lvwDate.Name = "lvwDate";
            this.lvwDate.Size = new System.Drawing.Size(493, 88);
            this.lvwDate.TabIndex = 1;
            this.lvwDate.UseCompatibleStateImageBehavior = false;
            this.lvwDate.View = System.Windows.Forms.View.List;
            // 
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.lnkUpload);
            this.groupPanel1.Controls.Add(this.lnkUserDef);
            this.groupPanel1.Controls.Add(this.lnkDefault);
            this.groupPanel1.Controls.Add(this.rbUserDef);
            this.groupPanel1.Controls.Add(this.rbDefault);
            this.groupPanel1.Location = new System.Drawing.Point(13, 8);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(224, 107);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.Class = "";
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.Class = "";
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.Class = "";
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 2;
            this.groupPanel1.Text = "範本設定";
            // 
            // lnkUpload
            // 
            this.lnkUpload.AutoSize = true;
            this.lnkUpload.BackColor = System.Drawing.Color.Transparent;
            this.lnkUpload.Location = new System.Drawing.Point(154, 47);
            this.lnkUpload.Name = "lnkUpload";
            this.lnkUpload.Size = new System.Drawing.Size(34, 17);
            this.lnkUpload.TabIndex = 4;
            this.lnkUpload.TabStop = true;
            this.lnkUpload.Text = "上傳";
            this.lnkUpload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUpload_LinkClicked);
            // 
            // lnkUserDef
            // 
            this.lnkUserDef.AutoSize = true;
            this.lnkUserDef.Location = new System.Drawing.Point(52, 47);
            this.lnkUserDef.Name = "lnkUserDef";
            this.lnkUserDef.Size = new System.Drawing.Size(86, 17);
            this.lnkUserDef.TabIndex = 3;
            this.lnkUserDef.TabStop = true;
            this.lnkUserDef.Text = "檢視自訂範本";
            this.lnkUserDef.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUserDef_LinkClicked);
            // 
            // lnkDefault
            // 
            this.lnkDefault.AutoSize = true;
            this.lnkDefault.Location = new System.Drawing.Point(52, 17);
            this.lnkDefault.Name = "lnkDefault";
            this.lnkDefault.Size = new System.Drawing.Size(86, 17);
            this.lnkDefault.TabIndex = 2;
            this.lnkDefault.TabStop = true;
            this.lnkDefault.Text = "檢視預設範本";
            this.lnkDefault.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDefault_LinkClicked);
            // 
            // rbUserDef
            // 
            this.rbUserDef.AutoSize = true;
            this.rbUserDef.Location = new System.Drawing.Point(31, 49);
            this.rbUserDef.Name = "rbUserDef";
            this.rbUserDef.Size = new System.Drawing.Size(14, 13);
            this.rbUserDef.TabIndex = 1;
            this.rbUserDef.TabStop = true;
            this.rbUserDef.UseVisualStyleBackColor = true;
            // 
            // rbDefault
            // 
            this.rbDefault.AutoSize = true;
            this.rbDefault.Location = new System.Drawing.Point(31, 19);
            this.rbDefault.Name = "rbDefault";
            this.rbDefault.Size = new System.Drawing.Size(14, 13);
            this.rbDefault.TabIndex = 0;
            this.rbDefault.TabStop = true;
            this.rbDefault.UseVisualStyleBackColor = true;
            // 
            // groupPanel2
            // 
            this.groupPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPanel2.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.cboMAddress);
            this.groupPanel2.Controls.Add(this.cboMName);
            this.groupPanel2.Controls.Add(this.labelX3);
            this.groupPanel2.Controls.Add(this.labelX2);
            this.groupPanel2.Location = new System.Drawing.Point(261, 8);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(244, 107);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.Class = "";
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseDown.Class = "";
            this.groupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseOver.Class = "";
            this.groupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel2.TabIndex = 3;
            this.groupPanel2.Text = "收件設定";
            // 
            // cboMAddress
            // 
            this.cboMAddress.DisplayMember = "Text";
            this.cboMAddress.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboMAddress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMAddress.FormattingEnabled = true;
            this.cboMAddress.ItemHeight = 19;
            this.cboMAddress.Location = new System.Drawing.Point(86, 43);
            this.cboMAddress.Name = "cboMAddress";
            this.cboMAddress.Size = new System.Drawing.Size(132, 25);
            this.cboMAddress.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboMAddress.TabIndex = 3;
            // 
            // cboMName
            // 
            this.cboMName.DisplayMember = "Text";
            this.cboMName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboMName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMName.FormattingEnabled = true;
            this.cboMName.ItemHeight = 19;
            this.cboMName.Location = new System.Drawing.Point(86, 12);
            this.cboMName.Name = "cboMName";
            this.cboMName.Size = new System.Drawing.Size(132, 25);
            this.cboMName.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboMName.TabIndex = 2;
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(20, 46);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(60, 21);
            this.labelX3.TabIndex = 1;
            this.labelX3.Text = "收件地址";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(20, 16);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(47, 21);
            this.labelX2.TabIndex = 0;
            this.labelX2.Text = "收件人";
            // 
            // chkDateAll
            // 
            this.chkDateAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkDateAll.AutoSize = true;
            this.chkDateAll.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkDateAll.BackgroundStyle.Class = "";
            this.chkDateAll.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkDateAll.Location = new System.Drawing.Point(12, 247);
            this.chkDateAll.Name = "chkDateAll";
            this.chkDateAll.Size = new System.Drawing.Size(54, 21);
            this.chkDateAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkDateAll.TabIndex = 4;
            this.chkDateAll.Text = "全選";
            this.chkDateAll.CheckedChanged += new System.EventHandler(this.chkDateAll_CheckedChanged);
            // 
            // chkExportStudInfo
            // 
            this.chkExportStudInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkExportStudInfo.AutoSize = true;
            this.chkExportStudInfo.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkExportStudInfo.BackgroundStyle.Class = "";
            this.chkExportStudInfo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkExportStudInfo.Location = new System.Drawing.Point(89, 247);
            this.chkExportStudInfo.Name = "chkExportStudInfo";
            this.chkExportStudInfo.Size = new System.Drawing.Size(147, 21);
            this.chkExportStudInfo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkExportStudInfo.TabIndex = 5;
            this.chkExportStudInfo.Text = "另存學生清單(Excel)";
            // 
            // btnPrint
            // 
            this.btnPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.AutoSize = true;
            this.btnPrint.BackColor = System.Drawing.Color.Transparent;
            this.btnPrint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPrint.Location = new System.Drawing.Point(348, 247);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 25);
            this.btnPrint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPrint.TabIndex = 6;
            this.btnPrint.Text = "列印";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.AutoSize = true;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(429, 247);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 25);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // StudentAttendanceReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 280);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.chkExportStudInfo);
            this.Controls.Add(this.chkDateAll);
            this.Controls.Add(this.groupPanel2);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.lvwDate);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.Name = "StudentAttendanceReportForm";
            this.Text = "課程缺曠通知單";
            this.Load += new System.EventHandler(this.StudentAttendanceReportForm_Load);
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            this.groupPanel2.ResumeLayout(false);
            this.groupPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ListViewEx lvwDate;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private System.Windows.Forms.LinkLabel lnkUpload;
        private System.Windows.Forms.LinkLabel lnkUserDef;
        private System.Windows.Forms.LinkLabel lnkDefault;
        private System.Windows.Forms.RadioButton rbUserDef;
        private System.Windows.Forms.RadioButton rbDefault;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboMAddress;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboMName;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkDateAll;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkExportStudInfo;
        private DevComponents.DotNetBar.ButtonX btnPrint;
        private DevComponents.DotNetBar.ButtonX btnExit;
    }
}