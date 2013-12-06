namespace K12.Retake.Shinmin.Form
{
    partial class ReSetSubjectDate
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgData = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colSchoolYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSemester = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colmo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCourse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPer1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPer2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPer3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPer4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPer5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPer6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPer7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPer8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnNext = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
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
            this.colSchoolYear,
            this.colSemester,
            this.colmo,
            this.colCourse,
            this.colPer1,
            this.colPer2,
            this.colPer3,
            this.colPer4,
            this.colPer5,
            this.colPer6,
            this.colPer7,
            this.colPer8});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgData.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgData.Location = new System.Drawing.Point(15, 10);
            this.dgData.Name = "dgData";
            this.dgData.RowHeadersVisible = false;
            this.dgData.RowTemplate.Height = 24;
            this.dgData.Size = new System.Drawing.Size(554, 348);
            this.dgData.TabIndex = 0;
            this.dgData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewX1_KeyDown);
            // 
            // colSchoolYear
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.colSchoolYear.DefaultCellStyle = dataGridViewCellStyle1;
            this.colSchoolYear.HeaderText = "學年度";
            this.colSchoolYear.Name = "colSchoolYear";
            this.colSchoolYear.ReadOnly = true;
            this.colSchoolYear.Width = 80;
            // 
            // colSemester
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightCyan;
            this.colSemester.DefaultCellStyle = dataGridViewCellStyle2;
            this.colSemester.HeaderText = "學期";
            this.colSemester.Name = "colSemester";
            this.colSemester.ReadOnly = true;
            this.colSemester.Width = 60;
            // 
            // colmo
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightCyan;
            this.colmo.DefaultCellStyle = dataGridViewCellStyle3;
            this.colmo.HeaderText = "梯次";
            this.colmo.Name = "colmo";
            this.colmo.ReadOnly = true;
            this.colmo.Width = 60;
            // 
            // colCourse
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightCyan;
            this.colCourse.DefaultCellStyle = dataGridViewCellStyle4;
            this.colCourse.HeaderText = "課程";
            this.colCourse.Name = "colCourse";
            this.colCourse.ReadOnly = true;
            // 
            // colPer1
            // 
            this.colPer1.HeaderText = "1";
            this.colPer1.Name = "colPer1";
            this.colPer1.ReadOnly = true;
            this.colPer1.Width = 30;
            // 
            // colPer2
            // 
            this.colPer2.HeaderText = "2";
            this.colPer2.Name = "colPer2";
            this.colPer2.ReadOnly = true;
            this.colPer2.Width = 30;
            // 
            // colPer3
            // 
            this.colPer3.HeaderText = "3";
            this.colPer3.Name = "colPer3";
            this.colPer3.ReadOnly = true;
            this.colPer3.Width = 30;
            // 
            // colPer4
            // 
            this.colPer4.HeaderText = "4";
            this.colPer4.Name = "colPer4";
            this.colPer4.ReadOnly = true;
            this.colPer4.Width = 30;
            // 
            // colPer5
            // 
            this.colPer5.HeaderText = "5";
            this.colPer5.Name = "colPer5";
            this.colPer5.ReadOnly = true;
            this.colPer5.Width = 30;
            // 
            // colPer6
            // 
            this.colPer6.HeaderText = "6";
            this.colPer6.Name = "colPer6";
            this.colPer6.ReadOnly = true;
            this.colPer6.Width = 30;
            // 
            // colPer7
            // 
            this.colPer7.HeaderText = "7";
            this.colPer7.Name = "colPer7";
            this.colPer7.ReadOnly = true;
            this.colPer7.Width = 30;
            // 
            // colPer8
            // 
            this.colPer8.HeaderText = "8";
            this.colPer8.Name = "colPer8";
            this.colPer8.ReadOnly = true;
            this.colPer8.Width = 30;
            // 
            // btnNext
            // 
            this.btnNext.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.AutoSize = true;
            this.btnNext.BackColor = System.Drawing.Color.Transparent;
            this.btnNext.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnNext.Location = new System.Drawing.Point(413, 366);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 25);
            this.btnNext.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "下一步";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.AutoSize = true;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(494, 366);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 25);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 3;
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
            this.labelX1.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX1.Location = new System.Drawing.Point(15, 367);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(336, 21);
            this.labelX1.TabIndex = 4;
            this.labelX1.Text = "說明：鍵入\"V\"鍵勾選，空白鍵與Delete鍵可清除資料。";
            // 
            // ReSetSubjectDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 400);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.dgData);
            this.DoubleBuffered = true;
            this.Name = "ReSetSubjectDate";
            this.Text = "時間表設定";
            this.Load += new System.EventHandler(this.ReSetSubjectDate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgData;
        private DevComponents.DotNetBar.ButtonX btnNext;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSchoolYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSemester;
        private System.Windows.Forms.DataGridViewTextBoxColumn colmo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCourse;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPer2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPer3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPer4;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPer5;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPer6;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPer7;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPer8;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}