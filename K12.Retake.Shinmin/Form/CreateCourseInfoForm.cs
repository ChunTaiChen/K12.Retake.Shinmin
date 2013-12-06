using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using K12.Retake.Shinmin.DAO;
using System.Xml.Linq;

namespace K12.Retake.Shinmin.Form
{
    public partial class CreateCourseInfoForm : BaseForm
    {
        /// <summary>
        /// 取得資料用
        /// </summary>
        BackgroundWorker _bgWorkerLoadData = new BackgroundWorker();
        /// <summary>
        /// 建立資料用
        /// </summary>
        BackgroundWorker _bgWorkerCreateData = new BackgroundWorker();

        /// <summary>
        /// 科目課程基本
        /// </summary>
        List<SubjectCourseBase> _SubjectCourseBaseList = new List<SubjectCourseBase>();

        /// <summary>
        /// 目前已有課程UID
        /// </summary>
        List<string> _hasCourseIDList = new List<string>();
        
        /// <summary>
        /// 已有課程資料
        /// </summary>
        List<UDTCourseDef> _hasCourseDefList = new List<UDTCourseDef>();

        /// <summary>
        /// 已有修課資料
        /// </summary>
        List<UDTScselectDef> _hasScselectDefList = new List<UDTScselectDef>();

        /// <summary>
        /// 已有時間區間
        /// </summary>
        List<UDTTimeSectionDef> _hasTimeSectionDefList = new List<UDTTimeSectionDef>();

        /// <summary>
        /// 已有缺曠
        /// </summary>
        List<UDTAttendanceDef> _hasAttendanceDefList = new List<UDTAttendanceDef>();
        bool _isStudMaxCountChange = false;

        /// <summary>
        /// 需要刪除課程ID
        /// </summary>
        List<int> _DelCourseIDList = new List<int>();
        

        public CreateCourseInfoForm()
        {
            InitializeComponent();
            _bgWorkerLoadData.DoWork += new DoWorkEventHandler(_bgWorkerLoadData_DoWork);
            _bgWorkerLoadData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorkerLoadData_RunWorkerCompleted);
            _bgWorkerCreateData.DoWork += new DoWorkEventHandler(_bgWorkerCreateData_DoWork);
            _bgWorkerCreateData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorkerCreateData_RunWorkerCompleted);
        }

        void _bgWorkerCreateData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnStart.Enabled = true;
            FISCA.Presentation.Controls.MsgBox.Show("開課完成");
            RetakeEvents.RaiseAssnChanged();
            this.Close();
            //btnStart.Enabled = false;
            //lblMsg.Visible = true;
            //_bgWorkerLoadData.RunWorkerAsync();
        }
        
        
        
        void _bgWorkerCreateData_DoWork(object sender, DoWorkEventArgs e)
        {
            // 清空原有舊資料
            // 課程
            if (_DelCourseIDList.Count > 0)
            {
                List<UDTCourseDef> delCourseList = new List<UDTCourseDef>();
                foreach (UDTCourseDef data in _hasCourseDefList)
                {
                    int coid = int.Parse(data.UID);
                    if (_DelCourseIDList.Contains(coid))
                        delCourseList.Add(data);
                }

                UDTTransfer.UDTCourseDelete(delCourseList);

                // 課程修課
                List<UDTScselectDef> delScselect = new List<UDTScselectDef>();
                foreach (UDTScselectDef data in _hasScselectDefList)
                    if (_DelCourseIDList.Contains(data.CourseID))
                        delScselect.Add(data);

                UDTTransfer.UDTSCSelectDelete(delScselect);
                
                // 課程時間區間
                List<UDTTimeSectionDef> delTimeSectionList = new List<UDTTimeSectionDef>();
                foreach (UDTTimeSectionDef data in _hasTimeSectionDefList)
                    if (_DelCourseIDList.Contains(data.CourseID))
                        delTimeSectionList.Add(data);

                UDTTransfer.UDTTimeSectionDelete(delTimeSectionList);
                
                // 課程缺曠
                List<UDTAttendanceDef> delAttendanceList = new List<UDTAttendanceDef>();
                foreach (UDTAttendanceDef data in _hasAttendanceDefList)
                    if (_DelCourseIDList.Contains(data.CourseID))
                        delAttendanceList.Add(data);

                UDTTransfer.UDTAttendanceDelete(delAttendanceList);
            }


            // 新增課程使用
            List<UDTCourseDef> InsertCourseList = new List<UDTCourseDef>();
            // 取得新增後課程使用
            List<UDTCourseDef> NewInsertedCourseList = new List<UDTCourseDef>();
            // 新增時間區間使用
            List<UDTTimeSectionDef> InsertTimeSectionList = new List<UDTTimeSectionDef>();
            // 新增修課學生
            List<UDTScselectDef> InsertSCSelectList = new List<UDTScselectDef>();

            List<string> courseNameAdd = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J","L","M","N","O","P" }.ToList();
            // 產生課程資料
            foreach (SubjectCourseBase scb in _SubjectCourseBaseList)
            {
                for (int i = 0; i < scb.CreateCount; i++)
                {
                    UDTCourseDef data = new UDTCourseDef();
                    data.SchoolYear = scb.SchoolYear;
                    data.Semester = scb.Semester;
                    data.Month = scb.Month;
                    data.Credit = scb.Credit;
                    data.SubjectType = scb.SubjectType;
                    data.SubjectName = scb.SubjectName;
                    data.SubjectLevel = scb.SubjectLevel;
                    data.DeptName = scb.DeptName;
                    // 課程名稱使用科目名稱加級別
                    if (data.SubjectLevel.HasValue)
                        data.CourseName = scb.SubjectName + QueryData.GetNumber(scb.SubjectLevel.Value.ToString()) +courseNameAdd[i];
                    else
                        data.CourseName = scb.SubjectName + courseNameAdd[i];
                                 
                        InsertCourseList.Add(data);

                    scb.CourseNameList.Add(data.CourseName);
                }
            }

            // 課程名稱與ID對照
            Dictionary<string, int> courseIDDict = new Dictionary<string, int>();

            if (InsertCourseList.Count > 0)
            {
                // 新增課程並取回新增的課程資料
                List<string> CIDList = UDTTransfer.UDTCourseInsert(InsertCourseList);
                NewInsertedCourseList = UDTTransfer.UDTCourseSelectUIDs(CIDList);

                // 課程名稱與ID對照
                foreach (UDTCourseDef data in NewInsertedCourseList)
                {
                    if (!courseIDDict.ContainsKey(data.CourseName))
                        courseIDDict.Add(data.CourseName, int.Parse(data.UID));
                }
                
                // 建立時間區間
                // 取得日期
                List<DateTime> dateList = new List<DateTime>();
                foreach (DataGridViewRow drv in dgDate.Rows)
                    dateList.Add((DateTime)drv.Tag);


                // 處理時間區間
                foreach (SubjectCourseBase scb in _SubjectCourseBaseList)
                {
                    if (scb.PeriodXml == null)
                        continue;

                    List<int> PeriodList = new List<int>();
                    foreach (XElement elm in scb.PeriodXml.Elements("Period"))
                        PeriodList.Add(int.Parse(elm.Value));

                    
                    // 課程名稱
                    foreach (string courseName in scb.CourseNameList)
                    {
                        if (courseIDDict.ContainsKey(courseName))
                        {
                            
                            int cid = courseIDDict[courseName];

                            foreach (DateTime dt in dateList)
                            {
                                foreach (int per in PeriodList)
                                {
                                    UDTTimeSectionDef dataTs = new UDTTimeSectionDef();
                                    dataTs.CourseID = cid;
                                    dataTs.Period = per;
                                    dataTs.Date = dt;
                                    InsertTimeSectionList.Add(dataTs);
                                }
                            }
                        }                    
                    }                        
                }
                // 新增寫入時間區間
                if (InsertTimeSectionList.Count > 0)
                    UDTTransfer.UDTTimeSectionInsert(InsertTimeSectionList);
                
                // 新增修課學生
                foreach (SubjectCourseBase scb in _SubjectCourseBaseList)
                {
                    if (scb.CreateCount < 1)
                        continue;

                    int MaxStudCount = scb.MaxStudentCount;

                    int NewCount = (int)Math.Round(((decimal)scb.StudentIDList.Count / (decimal)scb.CreateCount), 0);
                    if (NewCount > MaxStudCount)
                        MaxStudCount = NewCount;

                    List<int> cousreIDList = new List<int>();
                    foreach (string name in scb.CourseNameList)
                    {
                        if (courseIDDict.ContainsKey(name))
                            cousreIDList.Add(courseIDDict[name]);
                    }
                
                    // 修課學生
                    int courseIdx = 0, seatno = 1;
                    if (cousreIDList.Count > 0)
                    {
                        foreach (SubjectCourseStudentBase stud in scb.StudentIDList)
                        {

                            if (seatno > MaxStudCount)
                            {
                                seatno = 1;
                                courseIdx++;
                            }

                            UDTScselectDef scselect = new UDTScselectDef();
                            scselect.CourseID = cousreIDList[courseIdx];
                            scselect.SeatNo = seatno;
                            scselect.StudentID = stud.StudentID;
                            scselect.Type = stud.Type;
                            InsertSCSelectList.Add(scselect);
                            seatno++;
                        }
                    }
                }
                // 新增修課學生
                if (InsertSCSelectList.Count > 0)
                {
                    UDTTransfer.UDTSCSelectInsert(InsertSCSelectList);
                }
            }
   
        }

        /// <summary>
        /// 將資料填入畫面
        /// </summary>
        private void LoadDataToGridView()
        {
            dgData.Rows.Clear();
            int MaxCount=1;
            if (!string.IsNullOrWhiteSpace(txtStudMaxCount.Text))
                int.TryParse(txtStudMaxCount.Text, out MaxCount);

            dgData.Columns[colCheck.Index].ValueType = typeof(System.Boolean);


            foreach (SubjectCourseBase scb in _SubjectCourseBaseList)
            {
                int rowIdx = dgData.Rows.Add();
                dgData.Rows[rowIdx].Tag = scb;



                dgData.Rows[rowIdx].Cells[colCheck.Index].Value = true;
                
                // 檢查是否已開過課，開過勾選取消
                foreach (UDTCourseDef data in _hasCourseDefList)
                    if (data.SubjectName == scb.SubjectName && data.SubjectLevel == scb.SubjectLevel && data.DeptName == scb.DeptName)
                        dgData.Rows[rowIdx].Cells[colCheck.Index].Value = false;
                
                
                if(scb.SubjectLevel.HasValue)
                    dgData.Rows[rowIdx].Cells[colSubjectName.Index].Value=scb.SubjectName+QueryData.GetNumber(scb.SubjectLevel.Value.ToString());
                else
                    dgData.Rows[rowIdx].Cells[colSubjectName.Index].Value=scb.SubjectName;

                dgData.Rows[rowIdx].Cells[colDeptName.Index].Value = scb.DeptName;
                dgData.Rows[rowIdx].Cells[colCourseTable.Index].Value = scb.CourseTimeTable;
                dgData.Rows[rowIdx].Cells[colStudCount.Index].Value = scb.StudentIDList.Count;
                scb.CreateCount=(int)Math.Ceiling((double)scb.StudentIDList.Count / (double)MaxCount);
                dgData.Rows[rowIdx].Cells[colCourseCount.Index].Value = scb.CreateCount;
                if (scb.StudentIDList.Count == 0)
                {
                    dgData.Rows[rowIdx].Cells[colCheck.Index].Value = false;
                    foreach (DataGridViewCell cell in dgData.Rows[rowIdx].Cells)
                        cell.Style.BackColor = Color.LightGray;
                }
                scb.MaxStudentCount = MaxCount;                
            }        
        }

        void _bgWorkerLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // 將資料填入畫面
            LoadDataToGridView();
            dgDate.Focus();
                    
            btnStart.Enabled = true;
            lblMsg.Visible = false;
        }

        void _bgWorkerLoadData_DoWork(object sender, DoWorkEventArgs e)
        {
            _hasCourseIDList = QueryData.GetCourseSelectActive1();

            // 透過取得課程資料課程ID取得缺曠資料
            _hasAttendanceDefList = UDTTransfer.UDTAttendanceSelectByCourseIDList(_hasCourseIDList);
            // 取得目前所屬學年度+學期+月份，課程資料
            _hasCourseDefList = UDTTransfer.UDTCourseSelectUIDs(_hasCourseIDList);
            // 透過取得課程資料課程ID取得修課學生資料
            _hasScselectDefList = UDTTransfer.UDTSCSelectByCourseIDList(_hasCourseIDList);
            // 透過取得課程資料課程ID取得時間區間資料
            _hasTimeSectionDefList = UDTTransfer.UDTTimeSectionSelectByCourseIDList(_hasCourseIDList);
            
            // 取得開課科目與課程
            _SubjectCourseBaseList = QueryData.GetSubjectCourseBaseList();
            UDTTransfer.UDTSsselectSelectAll();
        }

        private void CreateCourseInfoForm_Load(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            lblMsg.Visible = true;
            _bgWorkerLoadData.RunWorkerAsync();
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            // 檢查上課日期與人數上限是否輸入
            if (dgDate.Rows.Count == 0)
            {
                FISCA.Presentation.Controls.MsgBox.Show("請設定上課日期");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtStudMaxCount.Text))
            {
                FISCA.Presentation.Controls.MsgBox.Show("請輸入課程人數上限");
                return;
            }

            // 檢查是否有勾選
            int cocount = 0;
            foreach (DataGridViewRow drv in dgData.Rows)
            {
                bool bb;
                if(bool.TryParse(drv.Cells[colCheck.Index].Value.ToString(),out bb))
                    cocount++;
            }

            if (cocount == 0)
            {
                FISCA.Presentation.Controls.MsgBox.Show("請勾選開課課程");
                return;
            }
            _DelCourseIDList.Clear();

            // 檢查是否已有課程存在
            if (_hasCourseIDList.Count > 0)
                FISCA.Presentation.Controls.MsgBox.Show("已有課程存在，立即開課將會刪除原有課程相關資料，包含：課程資料、課程修課學生、課程上課時間、課程學生缺曠資料。", "立即開課", MessageBoxButtons.OK, MessageBoxIcon.Warning);      

            btnStart.Enabled = false;

            // 重新讀取畫面開課數量,只開有勾選
            _SubjectCourseBaseList.Clear();
            foreach (DataGridViewRow drv in dgData.Rows)
            {
                if (drv.IsNewRow)
                    continue;
             

                // 沒有勾選跳過
                bool bb;
                if (bool.TryParse(drv.Cells[colCheck.Index].Value.ToString(), out bb))
                {
                    if(bb==false )
                        continue;
                }
                SubjectCourseBase scb = drv.Tag as SubjectCourseBase;
                if (scb != null)
                {

                    foreach (UDTCourseDef data in _hasCourseDefList)
                    {
                        if (data.SubjectName == scb.SubjectName && data.SubjectLevel == scb.SubjectLevel)
                            _DelCourseIDList.Add(int.Parse(data.UID));
                    }

                    int co;
                    if (drv.Cells[colCourseCount.Index].Value != null)
                        if (int.TryParse(drv.Cells[colCourseCount.Index].Value.ToString(), out co))
                            scb.CreateCount = co;

                    _SubjectCourseBaseList.Add(scb);
                }
            }

            _bgWorkerCreateData.RunWorkerAsync();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtStudMaxCount_TextChanged(object sender, EventArgs e)
        {
            _isStudMaxCountChange = true;
        }

        private void lnkSetDate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            SetCourseInfoDateForm scdf = new SetCourseInfoDateForm();
            if (scdf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dgDate.Rows.Clear();
                foreach (DateTime data in scdf.GetSelectDates())
                {
                    int rowIdx = dgDate.Rows.Add();
                    dgDate.Rows[rowIdx].Tag = data;
                    dgDate.Rows[rowIdx].Cells[colDate.Index].Value = data.Date.ToShortDateString()+" ("+Utility.GetDayWeekString(data)+")";
                }
            }
        }

        private void txtStudMaxCount_Leave(object sender, EventArgs e)
        {
            StudMaxCountChangeMessage();
        }

        private void StudMaxCountChangeMessage()
        {
            if (_isStudMaxCountChange)
            {
                FISCA.Presentation.Controls.MsgBox.Show("開課數量將重新計算!");
                LoadDataToGridView();
                _isStudMaxCountChange = false;
            }        
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                foreach (DataGridViewRow drv in dgData.Rows)
                    drv.Cells[colCheck.Index].Value = true;
            }
            else
            {
                foreach (DataGridViewRow drv in dgData.Rows)
                    drv.Cells[colCheck.Index].Value = false;
            }
        }

        private void txtStudMaxCount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                StudMaxCountChangeMessage();
        }

        private void dgData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            
        }    
    }
}
