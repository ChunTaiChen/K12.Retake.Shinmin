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
using Aspose.Cells;
namespace K12.Retake.Shinmin.Form
{
    public partial class StudentNotExamSearchForm : FISCA.Presentation.Controls.BaseForm
    {
        // 取得資料
        BackgroundWorker _bgLoadData;

        // 畫面上取得課程編號
        List<string> _CourseIDList;
        // 經過篩選課程資料
        Dictionary<int, string> _CourseIDDict;
        // 經過篩選缺課資料
        Dictionary<int,Dictionary<int,List<UDTAttendanceDef>>> _AttendanceDefDict;
        // 經過篩選修課資料
        Dictionary<int, Dictionary<int, UDTScselectDef>> _ScselectDefDict;
        // 經過篩選課程時間表
        Dictionary<int, List<UDTTimeSectionDef>> _TimeSectionDefDict;
        // 修課學生學生資料
        Dictionary<int, StudentRecord> _StudRecDict;
        // 需要扣考
        List<UDTScselectDef> _NotExamScList;

        // 比例
        decimal _NotExamValue = 0;

        public StudentNotExamSearchForm(List<string> CourseIDList)
        {
            InitializeComponent();
            _CourseIDList = CourseIDList;
            _CourseIDDict = new Dictionary<int, string>();
            _AttendanceDefDict = new Dictionary<int, Dictionary<int, List<UDTAttendanceDef>>>();
            _ScselectDefDict = new Dictionary<int, Dictionary<int, UDTScselectDef>>();
            _TimeSectionDefDict = new Dictionary<int, List<UDTTimeSectionDef>>();
            _StudRecDict = new Dictionary<int, StudentRecord>();
            _NotExamScList = new List<UDTScselectDef>();
            
            _bgLoadData = new BackgroundWorker();
            _bgLoadData.DoWork += new DoWorkEventHandler(_bgLoadData_DoWork);
            _bgLoadData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgLoadData_RunWorkerCompleted);
        }

        void _bgLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RunningButtonEnable(true);
            LoadDataToDataGridView();
        }

        // 載入資料到畫面
        private void LoadDataToDataGridView()
        {
            dgData.Rows.Clear();
            // 取得比例


            // 載資料
            foreach (KeyValuePair<int, Dictionary<int, List<UDTAttendanceDef>>> data in _AttendanceDefDict)
            {
                foreach (KeyValuePair<int, List<UDTAttendanceDef>> data2 in data.Value)
                {
                    decimal a =(decimal) data2.Value.Count;
                    decimal b = (decimal)_TimeSectionDefDict[data.Key].Count;
                    
                    decimal value =0;

                    if (a > 0 && b > 0)
                        value = a / b;

                    // 超過比例再顯示
                    if (value > _NotExamValue)
                    {                        
                        int rowIdx = dgData.Rows.Add();
                        // 將缺曠資料填入 Tag
                        dgData.Rows[rowIdx].Tag = data2.Value;
                        // 課程名稱
                        if (_CourseIDDict.ContainsKey(data.Key))
                            dgData.Rows[rowIdx].Cells[ColCourseName.Index].Value = _CourseIDDict[data.Key];

                        // 課程編號
                        if(_ScselectDefDict.ContainsKey(data.Key))
                            if (_ScselectDefDict[data.Key].ContainsKey(data2.Key))
                            {
                                dgData.Rows[rowIdx].Cells[colCoSeatNo.Index].Value = _ScselectDefDict[data.Key][data2.Key].SeatNo;
                                // 加入修課至扣考
                                _NotExamScList.Add(_ScselectDefDict[data.Key][data2.Key]);

                               // 加入該生是否被標扣考
                                dgData.Rows[rowIdx].Cells[colNotExam.Index].Value = "";
                                if (_ScselectDefDict[data.Key][data2.Key].NotExam.HasValue)
                                    if(_ScselectDefDict[data.Key][data2.Key].NotExam.Value)
                                        dgData.Rows[rowIdx].Cells[colNotExam.Index].Value = "是";
                                
                                    
                            }
                        // 學生名稱
                        if (_StudRecDict.ContainsKey(data2.Key))
                            dgData.Rows[rowIdx].Cells[ColStudName.Index].Value = _StudRecDict[data2.Key].Name;

                        // 缺課數
                        dgData.Rows[rowIdx].Cells[ColAttendCount.Index].Value = a;

                        // 上課總數
                        if (_TimeSectionDefDict.ContainsKey(data.Key))
                            dgData.Rows[rowIdx].Cells[ColCourseScCount.Index].Value = b;
                    }
                }
            }
        }

        void _bgLoadData_DoWork(object sender, DoWorkEventArgs e)
        {
            _CourseIDDict.Clear();
            _AttendanceDefDict.Clear();
            _ScselectDefDict.Clear();
            _TimeSectionDefDict.Clear();
            _StudRecDict.Clear();
            _NotExamScList.Clear();
            if (_CourseIDList.Count > 0)
            {   
                // 課程             
                foreach (UDTCourseDef data in UDTTransfer.UDTCourseSelectUIDs(_CourseIDList))
                {
                    int uid = int.Parse(data.UID);

                    if (!_CourseIDDict.ContainsKey(uid))
                        _CourseIDDict.Add(uid, data.CourseName);
                }

                // 缺課
                foreach (UDTAttendanceDef data in UDTTransfer.UDTAttendanceSelectByCourseIDList(_CourseIDList))
                {
                    if (!_AttendanceDefDict.ContainsKey(data.CourseID))
                        _AttendanceDefDict.Add(data.CourseID, new Dictionary<int, List<UDTAttendanceDef>>());

                    if (!_AttendanceDefDict[data.CourseID].ContainsKey(data.StudentID))
                        _AttendanceDefDict[data.CourseID].Add(data.StudentID, new List<UDTAttendanceDef>());

                    _AttendanceDefDict[data.CourseID][data.StudentID].Add(data);
                }

                List<string> StudIDList = new List<string>();
                // 修課
                foreach (UDTScselectDef data in UDTTransfer.UDTSCSelectByCourseIDList(_CourseIDList))
                {
                    if (!_ScselectDefDict.ContainsKey(data.CourseID))
                        _ScselectDefDict.Add(data.CourseID, new Dictionary<int,UDTScselectDef>());

                    if (!_ScselectDefDict[data.CourseID].ContainsKey(data.StudentID))
                        _ScselectDefDict[data.CourseID].Add(data.StudentID, data);

                    // 做修課學生載存學生資料
                    string sid = data.StudentID.ToString();
                    
                    if (!StudIDList.Contains(sid))
                        StudIDList.Add(sid);


                    _ScselectDefDict[data.CourseID][data.StudentID]=data;
                }

                // 學生資料
                foreach (StudentRecord stud in Student.SelectByIDs(StudIDList))
                {
                    int isid = int.Parse(stud.ID);
                    if (!_StudRecDict.ContainsKey(isid))
                        _StudRecDict.Add(isid, stud);
                }

                // 課程時間表,先依日期,Period 排序
                List<UDTTimeSectionDef> TimeSectionList = (from data in UDTTransfer.UDTTimeSectionSelectByCourseIDList(_CourseIDList) orderby data.Date ascending, data.Period ascending select data).ToList();
                foreach (UDTTimeSectionDef data in TimeSectionList)
                {
                    if (!_TimeSectionDefDict.ContainsKey(data.CourseID))
                        _TimeSectionDefDict.Add(data.CourseID, new List<UDTTimeSectionDef>());

                    _TimeSectionDefDict[data.CourseID].Add(data);
                }
            }
        }


        // 取得畫面上所選,從修課找到相對資料
        private void GetFormNotExamStudent()
        {
            _NotExamScList.Clear();                        
            foreach (DataGridViewRow drv in dgData.SelectedRows)
            {
                List<UDTAttendanceDef> data = drv.Tag as List<UDTAttendanceDef>;
                
                if (data != null)
                {
                    if (data.Count == 0)
                        continue;                    

                    // 比對課程修課加入扣考
                    if(_ScselectDefDict.ContainsKey(data[0].CourseID))
                        if (_ScselectDefDict[data[0].CourseID].ContainsKey(data[0].StudentID))
                        {
                            UDTScselectDef uData = _ScselectDefDict[data[0].CourseID][data[0].StudentID];
                            uData.NotExam = true;
                            _NotExamScList.Add(uData);
                        }
                }
            }        
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgData.Rows.Count > 0)
            {
                Workbook wb = new Workbook();
                // 標題
                for (int col = 0; col < dgData.Columns.Count; col++)
                    wb.Worksheets[0].Cells[0,col].PutValue(dgData.Columns[col].HeaderText);
            
                // 內容
                int rowIdx = 1;
                foreach (DataGridViewRow drv in dgData.Rows)
                {

                    for (int col = 0; col < dgData.Columns.Count; col++)
                    {
                        if (drv.Cells[col].Value != null)
                            wb.Worksheets[0].Cells[rowIdx, col].PutValue(drv.Cells[col].Value.ToString());
                    }

                    rowIdx++;
                }
                Utility.CompletedXls("學生扣可查詢結果", wb);
            }
        }

        private void RunningButtonEnable(bool value)
        {
            btnQry.Enabled = value;
            btnRun.Enabled = value;
            btnExport.Enabled = value;
        }

        private void btnQry_Click(object sender, EventArgs e)
        {
            dgData.Rows.Clear();
            if (string.IsNullOrWhiteSpace(txtCal.Text))
            {
                FISCA.Presentation.Controls.MsgBox.Show("畫面上輸入比例不能空白!");
                return;            
            }

            // 比例,例如 1/3
            _NotExamValue = GetNotExamValue(txtCal.Text);

            if (_NotExamValue == 0)
            {
                FISCA.Presentation.Controls.MsgBox.Show("畫面比例格式有錯，無法解析比例，請修正後再查詢");
                return;
            }

            // 按紐不給按
            RunningButtonEnable(false);

            // 重新讀取資料
            _bgLoadData.RunWorkerAsync();

        }

        // 取得畫面上不能可比例值
        private decimal GetNotExamValue(string strVal)
        {
            decimal retVal = 0;
            List<string> valList = strVal.Split('/').ToList();
            if (valList.Count == 2)
            { 
                // 解析
                decimal a, b;
                decimal.TryParse(valList[0], out a);
                decimal.TryParse(valList[1], out b);
                if (a > 0 && b > 0)
                    retVal = a / b;            
            }
            return retVal;
        }

        private void dgData_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // 打開畫面，傳入課程時間、缺曠資料、學生資料
            if (dgData.SelectedRows.Count == 1)
            {
                List<UDTAttendanceDef> data = dgData.CurrentRow.Tag as List<UDTAttendanceDef>;

                if (data != null)
                {
                    List<UDTTimeSectionDef> tis = null;
                    string CourseName="";
                    string CourseSeatNo = "";
                    StudentRecord studRec = null;
                    int sid = data[0].StudentID;
                    if (_StudRecDict.ContainsKey(sid))
                        studRec = _StudRecDict[sid];
                    

                    CourseName = dgData.CurrentRow.Cells[ColCourseName.Index].Value.ToString();
                    CourseSeatNo = dgData.CurrentRow.Cells[colCoSeatNo.Index].Value.ToString();
                    

                    if (_TimeSectionDefDict.ContainsKey(data[0].CourseID))
                        tis = _TimeSectionDefDict[data[0].CourseID];
                    if (tis != null && studRec != null)
                    {
                        StudentNotExamSearchForm_Sub1 snefs = new StudentNotExamSearchForm_Sub1(tis, data, CourseName, CourseSeatNo, studRec);
                        snefs.ShowDialog();
                    }
                }
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            
            // 取得畫面上需要扣考
            GetFormNotExamStudent();
            if (_NotExamScList.Count == 0)
            {
                FISCA.Presentation.Controls.MsgBox.Show("請選擇需要扣考學生");
                return;
            }

            btnRun.Enabled = false;

            if (FISCA.Presentation.Controls.MsgBox.Show("畫面上所選 "+_NotExamScList.Count+" 筆資料將執行扣考，請問確定執行?", "執行扣考", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                UDTTransfer.UDTSCSelectUpdate(_NotExamScList);
                FISCA.Presentation.Controls.MsgBox.Show("執行扣考完成");
                RetakeEvents.RaiseAssnChanged();
            }

            btnRun.Enabled = true;
            this.Close();
        }

        private void dgData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
