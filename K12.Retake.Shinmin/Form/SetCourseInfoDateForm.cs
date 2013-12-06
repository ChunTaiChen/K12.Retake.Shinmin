using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;

namespace K12.Retake.Shinmin.Form
{
    public partial class SetCourseInfoDateForm : BaseForm
    {
        /// <summary>
        /// 勾選日期
        /// </summary>
        List<DateTime> _SelectDateList = new List<DateTime>();

        public SetCourseInfoDateForm()
        {
            InitializeComponent();
        }

        private void SetCourseInfoDateForm_Load(object sender, EventArgs e)
        {
            dtBeginDate.Value = dtEndDate.Value = DateTime.Now;
        }

        private void btnGetDate_Click(object sender, EventArgs e)
        {
            if (lvwDate.CheckedItems.Count == 0)
            {
                FISCA.Presentation.Controls.MsgBox.Show("請勾選星期");
                return;
            }
            
            // 取得開始與結束日期
            DateTime dtBegin = dtBeginDate.Value;
            DateTime dtEnd = dtEndDate.Value;

            // 當日其大小互換交換
            if (dtBeginDate.Value > dtEndDate.Value)
            {
                dtBegin = dtEndDate.Value;
                dtEnd = dtBeginDate.Value;                
            }

            // 使用者勾選星期
            List<DayOfWeek> SelectDayOfWeek = new List<DayOfWeek> ();

            foreach(ListViewItem lvi in lvwDate.Items)
            {
                if(lvi.Checked)                
                switch(lvi.Text)
                {
                    case "星期日": SelectDayOfWeek.Add(DayOfWeek.Sunday); break;
                    case "星期一": SelectDayOfWeek.Add(DayOfWeek.Monday); break;
                    case "星期二": SelectDayOfWeek.Add(DayOfWeek.Tuesday); break;
                    case "星期三": SelectDayOfWeek.Add(DayOfWeek.Wednesday); break;
                    case "星期四": SelectDayOfWeek.Add(DayOfWeek.Thursday); break;
                    case "星期五": SelectDayOfWeek.Add(DayOfWeek.Friday); break;
                    case "星期六": SelectDayOfWeek.Add(DayOfWeek.Saturday); break;                
                }            
            }
            
            // 檢查並填入有勾選的日期            
            _SelectDateList.Clear();
            TimeSpan ts1 = dtEnd.Subtract(dtBegin);
            for (int i=0;i<=ts1.Days;i++)
            {
                DateTime dtStart = new DateTime();
                dtStart = dtBegin.AddDays(i);
                if (SelectDayOfWeek.Contains(dtStart.DayOfWeek))
                    _SelectDateList.Add(dtStart);                
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 取得所選星期的相關日期
        /// </summary>
        /// <returns></returns>
        public List<DateTime> GetSelectDates()
        {
            return _SelectDateList;
        }
    }
}
