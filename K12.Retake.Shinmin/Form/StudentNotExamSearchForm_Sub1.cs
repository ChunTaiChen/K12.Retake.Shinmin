using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using K12.Retake.Shinmin.DAO;
using K12.Data;

namespace K12.Retake.Shinmin.Form
{
    public partial class StudentNotExamSearchForm_Sub1 : FISCA.Presentation.Controls.BaseForm
    {
        // 缺曠
        List<UDTAttendanceDef> _AttendanceList;

        // 時間表
        List<UDTTimeSectionDef> _TimeSectionList;
        // 課程名稱
        string _CourseName = "";
        // 課程座號
        string _CourseSeatNo = "";
        
        // 學生資料
        StudentRecord _studRec;

        public StudentNotExamSearchForm_Sub1(List<UDTTimeSectionDef> TimeSectionList,List<UDTAttendanceDef> attendanceList,string CourseName,string CourseSeatNo,StudentRecord studRec)
        {
            InitializeComponent();
            _TimeSectionList = TimeSectionList;
            _AttendanceList = attendanceList;
            _CourseSeatNo = CourseSeatNo;            
            _studRec = studRec;
            _CourseName = CourseName;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StudentNotExamSearchForm_Sub1_Load(object sender, EventArgs e)
        {
            lblCourse.Text += _CourseName;
            lblName.Text = "課程座號：" +_CourseSeatNo+ " 學生姓名：" + _studRec.Name;

            // 時間表id
            List<string> tiList = new List<string>();
            foreach (UDTAttendanceDef da in _AttendanceList)
                tiList.Add(da.TimeSectionID.ToString());

            foreach (UDTTimeSectionDef data in _TimeSectionList)
            {
                int rowIdx = dgData.Rows.Add();
                dgData.Rows[rowIdx].Cells[colDate.Index].Value = data.Date.ToShortDateString() +"("+ Utility.GetDayWeekString(data.Date) + ")";
                dgData.Rows[rowIdx].Cells[colPeriod.Index].Value = data.Period;
                if (tiList.Contains(data.UID))
                    dgData.Rows[rowIdx].Cells[colType.Index].Value = "缺課";
            }


            lblCount.Text = "總節數："+_TimeSectionList.Count+"  缺課節數："+_AttendanceList.Count;
        }
    }
}
