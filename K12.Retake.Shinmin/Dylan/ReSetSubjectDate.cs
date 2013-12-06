using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using FISCA.UDT;
using K12.Retake.Shinmin.DAO;

namespace K12.Retake.Shinmin.Form
{
    public partial class ReSetSubjectDate : BaseForm
    {
        AccessHelper _accessHelper = new AccessHelper();

        List<UDTAttendanceDef> _hasAttendanceDefList = new List<UDTAttendanceDef>();
        List<UDTTimeSectionDef> _hasTimeSectionDef = new List<UDTTimeSectionDef>();
        public ReSetSubjectDate()
        {
            InitializeComponent();
        }

        private void ReSetSubjectDate_Load(object sender, EventArgs e)
        {
            //選擇一群課程
            List<UDTCourseDef> CourseList = _accessHelper.Select<UDTCourseDef>(RetakeAdmin.Instance.SelectedSource);
            foreach (UDTCourseDef def in CourseList)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgData);
                row.Tag = def;
                row.Cells[0].Value = "" + def.SchoolYear;
                row.Cells[1].Value = "" + def.Semester;
                row.Cells[2].Value = "" + def.Month;
                row.Cells[3].Value = "" + def.CourseName;
                dgData.Rows.Add(row);
            }

            //*如果這些課程已經有上課日期與節次資訊
            //則警告此操作,將會清除原有設定
            //List<UDTTimeSectionDef> TimeList = _accessHelper.Select<UDTTimeSectionDef>(RetakeAdmin.Instance.SelectedSource);
            // 上課時間區間
            _hasTimeSectionDef = UDTTransfer.UDTTimeSectionSelectByCourseIDList(RetakeAdmin.Instance.SelectedSource);
            // 課程缺曠
            _hasAttendanceDefList = UDTTransfer.UDTAttendanceSelectByCourseIDList(RetakeAdmin.Instance.SelectedSource);

            if (_hasTimeSectionDef.Count > 0)
            {
                MsgBox.Show("部份課程已有時間表\n使用本功能將會把時間表重設\n相關缺曠資料也將會遺失!!", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
                     

            // 比對課程節次
            foreach (DataGridViewRow drv in dgData.Rows)
            {
                if (drv.IsNewRow)
                    continue;

                UDTCourseDef data = drv.Tag as UDTCourseDef;
                if (data != null)
                { 
                    int coid=int.Parse(data.UID);
                    foreach(UDTTimeSectionDef sec in _hasTimeSectionDef.Where(x=>x.CourseID== coid))
                    {
                        int colIdx = sec.Period + 3;
                        drv.Cells[colIdx].Value = "V";
                    }
                
                }
            }
        }
           

        private void dataGridViewX1_KeyDown(object sender, KeyEventArgs e)
        {
            // 使用者所選範圍內
            foreach (DataGridViewCell cell in dgData.SelectedCells)
            {
                // 當不在節次範圍內
                if (cell.ColumnIndex >= colPer1.Index && cell.ColumnIndex <= colPer8.Index)
                {
                    // 除了按A,否則都會清空資料
                    if (e.KeyCode == Keys.Tab)
                        continue;

                    if (e.KeyCode == Keys.V)
                    {
                        cell.Value = "V";
                    }
                    else if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Space)
                    {
                        cell.Value = "";
                    }
                    else
                    {
                        //不動作
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            // 取得畫面上節次
            Dictionary<int,List<int>> courseDict = new Dictionary<int,List<int>> ();

            foreach (DataGridViewRow drv in dgData.Rows)
            {
                UDTCourseDef data = drv.Tag as UDTCourseDef;
                
                if (data != null)
                {
                    int coid = int.Parse(data.UID);
                    int colIdx = 0;
                    List<int> intList= new List<int> ();
                    for (int i = 1; i <= 8; i++)
                    {
                        colIdx = i + 3;
                        if (drv.Cells[colIdx].Value != null)
                            if (drv.Cells[colIdx].Value.ToString() == "V")
                                intList.Add(i);                    
                    }
                    courseDict.Add(coid, intList);
                }            
            }

            // 呼叫下個畫面並傳入資料
            K12.Retake.Shinmin.Dylan.ReSetSubjectDateStep2 formNext = new Dylan.ReSetSubjectDateStep2(_hasTimeSectionDef,_hasAttendanceDefList,courseDict);
            formNext.ShowDialog();
            this.Close();
        }

    }
}
