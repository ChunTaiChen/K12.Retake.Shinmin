using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using K12.Retake.Shinmin.DAO;

namespace K12.Retake.Shinmin.Dylan
{
    public partial class ReSetSubjectDateStep2 : FISCA.Presentation.Controls.BaseForm
    {
        /// <summary>
        /// 課程課上節次
        /// </summary>
        Dictionary<int, List<int>> _CoursePeriodDict = new Dictionary<int, List<int>>();
        
        /// <summary>
        /// 舊課程上課時間
        /// </summary>
        List<UDTTimeSectionDef> _OldTimeSectionList = new List<UDTTimeSectionDef>();

        /// <summary>
        /// 舊課程缺曠
        /// </summary>
        List<UDTAttendanceDef> _OldAttendanceList = new List<UDTAttendanceDef>();

        /// <summary>
        /// 檢查所選的日期
        /// </summary>
        private Dictionary<string, DateTime> _TempSelectDateDict = new Dictionary<string, DateTime>();

        public ReSetSubjectDateStep2(List<UDTTimeSectionDef> TimeSectionList, List<UDTAttendanceDef> AttendanceList,Dictionary<int,List<int>> CoursePeriod)
        {
            InitializeComponent();

            // 原課程上課時間
            _OldTimeSectionList = TimeSectionList;
            
            // 缺曠資料
            _OldAttendanceList = AttendanceList;

            // 畫面上設定節次
            _CoursePeriodDict = CoursePeriod;
        }

        private void btnGetDate_Click(object sender, EventArgs e)
        {
            if (lvwDate.CheckedItems.Count == 0)
            {
                FISCA.Presentation.Controls.MsgBox.Show("請勾選星期");
                return;
            }

            if (dtBeginDate.IsEmpty || dtEndDate.IsEmpty)
            {
                FISCA.Presentation.Controls.MsgBox.Show("請選擇日期");
                return;
            }

            List<DateTime> _SelectDateList = new List<DateTime>();

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
            List<DayOfWeek> SelectDayOfWeek = new List<DayOfWeek>();

            foreach (ListViewItem lvi in lvwDate.Items)
            {
                if (lvi.Checked)
                    switch (lvi.Text)
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
            TimeSpan ts1 = dtEnd.Subtract(dtBegin);
            for (int i = 0; i <= ts1.Days; i++)
            {
                DateTime dtStart = new DateTime();
                dtStart = dtBegin.AddDays(i);
                if (SelectDayOfWeek.Contains(dtStart.DayOfWeek))
                    _SelectDateList.Add(dtStart);
            }

            // 已有資料
            _TempSelectDateDict.Clear();
            foreach (DataGridViewRow dr in dgDate.Rows)
            {
                DateTime dd = (DateTime)dr.Tag;
                if (dd != null)
                    _TempSelectDateDict.Add(dd.ToShortDateString(), dd);

            }

            foreach (DateTime dt in _SelectDateList)
            {
                if (!_TempSelectDateDict.ContainsKey(dt.ToShortDateString()))
                    _TempSelectDateDict.Add(dt.ToShortDateString(),dt);
            }

            List<DateTime> dtList = _TempSelectDateDict.Values.ToList();
            dtList.Sort();
            // 放到畫面
            dgDate.Rows.Clear();
            foreach (DateTime dt in dtList)
            {
                int RowIdx = dgDate.Rows.Add();
                dgDate.Rows[RowIdx].Tag = dt;
                dgDate.Rows[RowIdx].Cells[0].Value = dt.ToShortDateString() +" ("+ Utility.GetDayWeekString(dt) + ")";
            }
        }

        private void ReSetSubjectDateStep2_Load(object sender, EventArgs e)
        {
            
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            // 取得畫面上上課日期
            List<DateTime> dtList = new List<DateTime>();
            foreach (DataGridViewRow drv in dgDate.Rows)
            {
                DateTime dt = (DateTime)drv.Tag;
                dtList.Add(dt);
            }

            // 刪除課程缺曠
            UDTTransfer.UDTAttendanceDelete(_OldAttendanceList);
            // 刪除課程上課時間
            UDTTransfer.UDTTimeSectionDelete(_OldTimeSectionList);
            // 新增新課程上課時間
            List<UDTTimeSectionDef> InsertTimeSectionList = new List<UDTTimeSectionDef>();

            // 組合新增資料
            foreach (KeyValuePair<int, List<int>> data in _CoursePeriodDict)
            {
                foreach (DateTime dt in dtList)
                {
                    foreach (int period in data.Value)
                    {
                        UDTTimeSectionDef ts = new UDTTimeSectionDef();
                        ts.CourseID = data.Key;
                        ts.Date = dt;
                        ts.Period = period;
                        InsertTimeSectionList.Add(ts);
                    }                
                }            
            }

            if (InsertTimeSectionList.Count > 0)
            {
                UDTTransfer.UDTTimeSectionInsert(InsertTimeSectionList);             
                FISCA.Presentation.Controls.MsgBox.Show("更新成功");
                RetakeEvents.RaiseAssnChanged();
            }
            // 關閉畫面
            this.Close();
        }
    }
}
