namespace K12.Retake.Shinmin.Form
{
    partial class SuggestSubjectForm
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
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.lblMsg = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cbxDeptName = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.colLnk = new System.Windows.Forms.DataGridViewLinkColumn();
            this.colSubjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubjectLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCredit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStudCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDept = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.SuspendLayout();
            // 
            // dgData
            // 
            this.dgData.AllowUserToAddRows = false;
            this.dgData.AllowUserToDeleteRows = false;
            this.dgData.AllowUserToResizeRows = false;
            this.dgData.BackgroundColor = System.Drawing.Color.White;
            this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLnk,
            this.colSubjectName,
            this.colSubjectLevel,
            this.colCredit,
            this.colStudCount,
            this.colDept});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgData.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgData.Location = new System.Drawing.Point(12, 53);
            this.dgData.MultiSelect = false;
            this.dgData.Name = "dgData";
            this.dgData.RowHeadersVisible = false;
            this.dgData.RowTemplate.Height = 24;
            this.dgData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgData.Size = new System.Drawing.Size(585, 193);
            this.dgData.TabIndex = 0;
            this.dgData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgData_CellContentClick);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(522, 254);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 25);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblMsg.BackgroundStyle.Class = "";
            this.lblMsg.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblMsg.Location = new System.Drawing.Point(12, 254);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(453, 25);
            this.lblMsg.TabIndex = 2;
            this.lblMsg.Text = "labelX1";
            this.lblMsg.Click += new System.EventHandler(this.lblMsg_Click);
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
            this.labelX1.Location = new System.Drawing.Point(16, 10);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(34, 21);
            this.labelX1.TabIndex = 3;
            this.labelX1.Text = "科別";
            // 
            // cbxDeptName
            // 
            this.cbxDeptName.DisplayMember = "Text";
            this.cbxDeptName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxDeptName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDeptName.FormattingEnabled = true;
            this.cbxDeptName.ItemHeight = 19;
            this.cbxDeptName.Location = new System.Drawing.Point(56, 10);
            this.cbxDeptName.Name = "cbxDeptName";
            this.cbxDeptName.Size = new System.Drawing.Size(237, 25);
            this.cbxDeptName.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbxDeptName.TabIndex = 4;
            this.cbxDeptName.SelectedIndexChanged += new System.EventHandler(this.cbxDeptName_SelectedIndexChanged);
            // 
            // colLnk
            // 
            this.colLnk.HeaderText = "";
            this.colLnk.Name = "colLnk";
            this.colLnk.Text = "加入";
            this.colLnk.UseColumnTextForLinkValue = true;
            this.colLnk.Width = 50;
            // 
            // colSubjectName
            // 
            this.colSubjectName.HeaderText = "科目名稱";
            this.colSubjectName.Name = "colSubjectName";
            this.colSubjectName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colSubjectName.Width = 155;
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
            this.colCredit.HeaderText = "學分數";
            this.colCredit.Name = "colCredit";
            // 
            // colStudCount
            // 
            this.colStudCount.HeaderText = "人數統計";
            this.colStudCount.Name = "colStudCount";
            // 
            // colDept
            // 
            this.colDept.HeaderText = "科別";
            this.colDept.Name = "colDept";
            // 
            // SuggestSubjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 288);
            this.Controls.Add(this.cbxDeptName);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.dgData);
            this.Name = "SuggestSubjectForm";
            this.Text = "建議重補修科目";
            this.Load += new System.EventHandler(this.SuggestSubjectForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgData;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.LabelX lblMsg;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbxDeptName;
        private System.Windows.Forms.DataGridViewLinkColumn colLnk;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubjectLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCredit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStudCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDept;
    }
}