using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using K12.Retake.Shinmin.DAO;

namespace K12.Retake.Shinmin.Form
{
    public partial class AddCourse : FISCA.Presentation.Controls.BaseForm
    {
        BackgroundWorker _bgWorkLoad;
        List<UDTCourseDef> _AllCourseList = new List<UDTCourseDef>();        
        ErrorProvider _errorP = new ErrorProvider();
        List<UDTTimeListDef> _AllTimeList = new List<UDTTimeListDef>();
        int _DefSchoolYear=0, _DefSemester=0, _Defmot = 0;
        public AddCourse()
        {
            InitializeComponent();
            _bgWorkLoad = new BackgroundWorker();
            _bgWorkLoad.DoWork += new DoWorkEventHandler(_bgWorkLoad_DoWork);
            _bgWorkLoad.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorkLoad_RunWorkerCompleted);
            _bgWorkLoad.RunWorkerAsync();
        }
        
        void _bgWorkLoad_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           
            if (_AllCourseList.Count > 0)
            {
                SetDefaultSelectItem();
                this.ShowDialog();               
               
            }
            else
            {
                FISCA.Presentation.Controls.MsgBox.Show("請先設定目前正在學年度、學期、梯次.");                
            }
        }

        void _bgWorkLoad_DoWork(object sender, DoWorkEventArgs e)
        {
            List<string> courseIDList=QueryData.GetCourseSelectActive1();
            
            // 取得目前正在
            List<UDTCourseDef> CourseList=UDTTransfer.UDTCourseSelectUIDs(courseIDList);

            // 取得所有課程
            _AllCourseList = UDTTransfer.UDTCourseSelectAllDict().Values.ToList();
            
            // 取得所有期間
            _AllTimeList = UDTTransfer.UDTTimeListSelectAll();

            // 取得學年度、學期、梯次
            if (CourseList.Count > 0)
            {
                _DefSchoolYear = CourseList[0].SchoolYear;
                _DefSemester = CourseList[0].Semester;
                _Defmot = CourseList[0].Month;
            }
        }     

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddCourse_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.MinimumSize = this.Size;         
        }

        private void SetDefaultSelectItem()
        {
            // 處理預設學年度學期
            if (_DefSchoolYear == 0)
                iptSchoolYear.Value = int.Parse(K12.Data.School.DefaultSchoolYear);
            else
                iptSchoolYear.Value = _DefSchoolYear;

            if (_DefSemester == 0)
                iptSemester.Value = int.Parse(K12.Data.School.DefaultSemester);
            else
                iptSemester.Value = _DefSemester;

            if (_Defmot == 0)
                iptMonth.Value = 1;
            else
                iptMonth.Value = _Defmot;

            if (cbxSubjectType.Items.Count == 0)
            {
                string[] strType = new string[] { "專業", "實習", "共同" };
                cbxSubjectType.Items.AddRange(strType.ToArray());
            }
            if (cbxCourseTeacher.Items.Count == 0)
                cbxCourseTeacher.Items.AddRange(Global._TeacherNameIDDict.Keys.ToArray());
            if (cbxDeptName.Items.Count == 0)
                cbxDeptName.Items.AddRange(QueryData.GetAllDeptName().ToArray());

           
        }

        /// <summary>
        /// 檢查資料
        /// </summary>
        private bool CheckData()
        {
            bool pass = true;
            // 檢查必填

            if (iptSchoolYear.IsEmpty)
            {
                _errorP.SetError(iptSchoolYear, "學年度不可空白");
                pass = false;
            }

            if (iptSemester.IsEmpty)
            {
                _errorP.SetError(iptSemester, "學期不可空白");
                pass = false;
            }

            if (iptMonth.IsEmpty)
            {
                _errorP.SetError(iptMonth, "梯次不可空白");
                pass = false;
            }


            if (string.IsNullOrWhiteSpace(txtCourseName.Text))
            {
                _errorP.SetError(txtCourseName, "課程名稱不可空白");
                pass = false;
            }

            if (string.IsNullOrWhiteSpace(txtSubjectName.Text))
            {
                _errorP.SetError(txtSubjectName, "科目名稱不可空白");
                pass = false;
            }

            // 檢查課程名稱是否重複
            foreach (UDTCourseDef data in _AllCourseList)
            {
                if (data.SchoolYear==iptSchoolYear.Value && data.Semester==iptSemester.Value && data.Month==iptMonth.Value && data.CourseName == txtCourseName.Text)
                {
                    _errorP.SetError(txtCourseName, "學年度+學期+梯次+課程名稱，已有相同名稱無法新增!");
                    pass = false;
                }
            }          

            if (string.IsNullOrWhiteSpace(txtCredit.Text) || txtCredit.Text=="")
            {
                _errorP.SetError(txtCredit, "學分數不可空白");
                pass = false;
            }

            if (!string.IsNullOrWhiteSpace(txtSubjectLevel.Text))
            {
                int i;
                if (!int.TryParse(txtSubjectLevel.Text, out i))
                {
                    _errorP.SetError(txtSubjectLevel, "級別必須是整數");
                    pass = false;
                }
            }

            if (!string.IsNullOrWhiteSpace(txtCredit.Text))
            {
                int i;
                if (!int.TryParse(txtCredit.Text, out i))
                {
                    _errorP.SetError(txtCredit, "學分數必須是整數");
                    pass = false;
                }
            }
            return pass;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckData())
                {
                    UDTCourseDef courseData = new UDTCourseDef();
                    // 畫面上資訊
                    courseData.CourseName = txtCourseName.Text;
                    if (Global._TeacherNameIDDict.ContainsKey(cbxCourseTeacher.Text))
                        courseData.RefTeacherID = Global._TeacherNameIDDict[cbxCourseTeacher.Text];
                    courseData.SubjectName = txtSubjectName.Text;
                    if (string.IsNullOrWhiteSpace(txtSubjectLevel.Text))
                        courseData.SubjectLevel = null;
                    else
                        courseData.SubjectLevel = int.Parse(txtSubjectLevel.Text);

                    courseData.Credit = int.Parse(txtCredit.Text);
                    courseData.SubjectType = cbxSubjectType.Text;
                    courseData.DeptName = cbxDeptName.Text;
                    courseData.SchoolYear = iptSchoolYear.Value;
                    courseData.Semester = iptSemester.Value;
                    courseData.Month = iptMonth.Value;

                    List<UDTCourseDef> dataList = new List<UDTCourseDef>();
                    dataList.Add(courseData);
                    UDTTransfer.UDTCourseInsert(dataList);

                    // 檢查名冊是否已有，沒有新增一筆
                    bool addData = true;
                    foreach (UDTTimeListDef data in _AllTimeList)
                    {
                        if (data.SchoolYear == iptSchoolYear.Value && data.Semester == iptSemester.Value && data.Month == iptMonth.Value)
                        {
                            addData = false;
                            break;
                        }
                    }

                    if (addData)
                    {
                        List<UDTTimeListDef> addList = new List<UDTTimeListDef>();
                        UDTTimeListDef da = new UDTTimeListDef();
                        da.SchoolYear = iptSchoolYear.Value;
                        da.Semester = iptSemester.Value;
                        da.Month = iptMonth.Value;
                        da.Name = iptSchoolYear.Value + "學年度第" + iptSemester.Value + "學期" + iptMonth.Value + "梯次";
                        addList.Add(da);
                        UDTTransfer.UDTTimeListInsert(addList);
                    }

                    FISCA.Presentation.Controls.MsgBox.Show("儲存完成.");
                    RetakeEvents.RaiseAssnChanged();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                FISCA.Presentation.Controls.MsgBox.Show("新增課程過程發生錯誤"+ex.Message);
            }
        }

        private void txtCourseName_TextChanged(object sender, EventArgs e)
        {
            _errorP.SetError(txtCourseName, "");
        }

        private void txtSubjectName_TextChanged(object sender, EventArgs e)
        {
            _errorP.SetError(txtSubjectName, "");
        }

        private void txtSubjectLevel_TextChanged(object sender, EventArgs e)
        {
            _errorP.SetError(txtSubjectLevel, "");
        }

        private void txtCredit_TextChanged(object sender, EventArgs e)
        {
            _errorP.SetError(txtCredit, "");
        }

        private void iptSchoolYear_ValueChanged(object sender, EventArgs e)
        {
            _errorP.SetError(iptSchoolYear, "");
        }

        private void iptSemester_ValueChanged(object sender, EventArgs e)
        {
            _errorP.SetError(iptSemester, "");
        }

        private void iptMonth_ValueChanged(object sender, EventArgs e)
        {
            _errorP.SetError(iptMonth, "");
        }

    }
}
