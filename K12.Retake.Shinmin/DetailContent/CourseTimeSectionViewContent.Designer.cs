namespace K12.Retake.Shinmin.DetailContent
{
    partial class CourseTimeSectionViewContent
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
            this.lvwTimeSection = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.colDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvwTimeSection
            // 
            // 
            // 
            // 
            this.lvwTimeSection.Border.Class = "ListViewBorder";
            this.lvwTimeSection.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lvwTimeSection.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDate,
            this.col1,
            this.col2,
            this.col3,
            this.col4,
            this.col5,
            this.col6,
            this.col7,
            this.col8});
            this.lvwTimeSection.FullRowSelect = true;
            this.lvwTimeSection.Location = new System.Drawing.Point(14, 14);
            this.lvwTimeSection.MultiSelect = false;
            this.lvwTimeSection.Name = "lvwTimeSection";
            this.lvwTimeSection.Size = new System.Drawing.Size(523, 123);
            this.lvwTimeSection.TabIndex = 0;
            this.lvwTimeSection.UseCompatibleStateImageBehavior = false;
            this.lvwTimeSection.View = System.Windows.Forms.View.Details;
            // 
            // colDate
            // 
            this.colDate.Text = "日期";
            this.colDate.Width = 140;
            // 
            // col1
            // 
            this.col1.Text = "1";
            this.col1.Width = 42;
            // 
            // col2
            // 
            this.col2.Text = "2";
            this.col2.Width = 42;
            // 
            // col3
            // 
            this.col3.Text = "3";
            this.col3.Width = 42;
            // 
            // col4
            // 
            this.col4.Text = "4";
            this.col4.Width = 42;
            // 
            // col5
            // 
            this.col5.Text = "5";
            this.col5.Width = 42;
            // 
            // col6
            // 
            this.col6.Text = "6";
            this.col6.Width = 42;
            // 
            // col7
            // 
            this.col7.Text = "7";
            this.col7.Width = 42;
            // 
            // col8
            // 
            this.col8.Text = "8";
            this.col8.Width = 42;
            // 
            // CourseTimeSectionViewContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvwTimeSection);
            this.Name = "CourseTimeSectionViewContent";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ListViewEx lvwTimeSection;
        private System.Windows.Forms.ColumnHeader colDate;
        private System.Windows.Forms.ColumnHeader col1;
        private System.Windows.Forms.ColumnHeader col2;
        private System.Windows.Forms.ColumnHeader col3;
        private System.Windows.Forms.ColumnHeader col4;
        private System.Windows.Forms.ColumnHeader col5;
        private System.Windows.Forms.ColumnHeader col6;
        private System.Windows.Forms.ColumnHeader col7;
        private System.Windows.Forms.ColumnHeader col8;
    }
}
