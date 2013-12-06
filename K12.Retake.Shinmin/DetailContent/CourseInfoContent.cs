using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.UDT;
using FISCA.Permission;
using K12.Data;
using K12.Retake.Shinmin.DAO;
using Campus.Windows;

namespace K12.Retake.Shinmin.DetailContent
{
    /// <summary>
    /// 課程基本料
    /// </summary>
    [FISCA.Permission.FeatureCode("K12.Retake.Shinmin.DetailContent.CourseInfoContent", "課程基本資料")]
    public partial class CourseInfoContent : FISCA.Presentation.DetailContent
    {
        BackgroundWorker _bgWorker = new BackgroundWorker();
        bool _isBusy = false;
        UDTCourseDef _CourseData = new UDTCourseDef();
        List<string> _CourseID = new List<string>();
        List<UDTCourseDef> CourseList;

        private ChangeListener _ChangeListener = new ChangeListener();
        ErrorProvider _errorP = new ErrorProvider();
        public CourseInfoContent()
        {            
            InitializeComponent();
            this.Group = "課程基本資料";
            CourseList = new List<UDTCourseDef>();
            _bgWorker.DoWork += new DoWorkEventHandler(_bgWorker_DoWork);
            _bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorker_RunWorkerCompleted);
            RetakeEvents.RetakeChanged += new EventHandler(RetakeEvents_RetakeChanged);         

            // 加入控制項變動檢查            
            _ChangeListener.Add(new TextBoxSource(txtCourseName));
            _ChangeListener.Add(new TextBoxSource(txtSubjectName));
            _ChangeListener.Add(new TextBoxSource(txtSubjectLevel));
            _ChangeListener.Add(new TextBoxSource(txtCredit));
            _ChangeListener.Add(new ComboBoxSource(cbxCourseTeacher,ComboBoxSource.ListenAttribute.Text));
            _ChangeListener.Add(new ComboBoxSource(cbxDeptName, ComboBoxSource.ListenAttribute.Text));
            _ChangeListener.Add(new ComboBoxSource(cbxSubjectType, ComboBoxSource.ListenAttribute.Text));


            _ChangeListener.StatusChanged += new EventHandler<ChangeEventArgs>(_ChangeListener_StatusChanged);
        }

        void _ChangeListener_StatusChanged(object sender, ChangeEventArgs e)
        {
            this.CancelButtonVisible = (e.Status == ValueStatus.Dirty);
            this.SaveButtonVisible = (e.Status == ValueStatus.Dirty);
        }

        private void SetDefaultSelectItem()
        {
            if (cbxSubjectType.Items.Count == 0)
            {
                string[] strType = new string[] { "專業", "實習", "共同" };
                cbxSubjectType.Items.AddRange(strType.ToArray());
            }
            if(cbxCourseTeacher.Items.Count==0)
                cbxCourseTeacher.Items.AddRange(Global._TeacherNameIDDict.Keys.ToArray());
            if(cbxDeptName.Items.Count==0)
                cbxDeptName.Items.AddRange(QueryData.GetAllDeptName().ToArray());
        }

        void RetakeEvents_RetakeChanged(object sender, EventArgs e)
        {
            _BGRun();
        }

        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_isBusy)
            {
                _isBusy = false;
                _bgWorker.RunWorkerAsync();
                return;
            }
            LoadData();    
        }

        protected override void OnPrimaryKeyChanged(EventArgs e)
        {
            this.Loading = true;
            this.CancelButtonVisible = false;
            this.SaveButtonVisible = false;
            _CourseID.Clear();
            _CourseID.Add(PrimaryKey);
            _BGRun();
        }

        private void LoadData()
        {
            _ChangeListener.SuspendListen();
            // 設定預設選項
            SetDefaultSelectItem();
            lblMsg.Text = _CourseData.SchoolYear + "學年度　第" + _CourseData.Semester + "學期　" + _CourseData.Month + "梯次";
            txtCourseName.Text = _CourseData.CourseName;
            cbxCourseTeacher.Text = "";
            if (Global._TeacherIDNameDict.ContainsKey(_CourseData.RefTeacherID))
                cbxCourseTeacher.Text = Global._TeacherIDNameDict[_CourseData.RefTeacherID];
            txtSubjectName.Text = _CourseData.SubjectName;
            if (_CourseData.SubjectLevel.HasValue)
                txtSubjectLevel.Text = _CourseData.SubjectLevel.Value.ToString();
            else
                txtSubjectLevel.Text = "";
            txtCredit.Text = _CourseData.Credit.ToString();
            cbxSubjectType.Text = _CourseData.SubjectType;
            cbxDeptName.Text = _CourseData.DeptName;

            _ChangeListener.Reset();
            _ChangeListener.ResumeListen();
            this.Loading = false;

        
        }

        protected override void OnCancelButtonClick(EventArgs e)
        {
            this.CancelButtonVisible = false;
            this.SaveButtonVisible = false;
            LoadData();
        }

        protected override void OnSaveButtonClick(EventArgs e)
        {
            if (CheckData())
            {
                _CourseData.CourseName = txtCourseName.Text;
                if (Global._TeacherNameIDDict.ContainsKey(cbxCourseTeacher.Text))
                    _CourseData.RefTeacherID = Global._TeacherNameIDDict[cbxCourseTeacher.Text];
                _CourseData.SubjectName = txtSubjectName.Text;
                if (string.IsNullOrWhiteSpace(txtSubjectLevel.Text))
                    _CourseData.SubjectLevel = null;
                else
                    _CourseData.SubjectLevel = int.Parse(txtSubjectLevel.Text);

                _CourseData.Credit = int.Parse(txtCredit.Text);
                _CourseData.SubjectType = cbxSubjectType.Text;
                _CourseData.DeptName = cbxDeptName.Text;

                List<UDTCourseDef> dataList = new List<UDTCourseDef>();
                dataList.Add(_CourseData);
                UDTTransfer.UDTCourseUpdate(dataList);

                this.CancelButtonVisible = false;
                this.SaveButtonVisible = false;
                RetakeEvents.RaiseAssnChanged();
            }
        }

        /// <summary>
        /// 檢查資料
        /// </summary>
        private bool CheckData()
        {
            bool pass = true;
            // 檢查必填
            if (string.IsNullOrWhiteSpace(txtCourseName.Text))
            {
                _errorP.SetError(txtCourseName, "課程名稱不可空白");
                pass = false;
            }

            // 檢查課程名稱是否重複
            if(_CourseData.CourseName!=txtCourseName.Text)
            foreach (UDTCourseDef data in CourseList)
            {
                if (data.SchoolYear == _CourseData.SchoolYear && data.Semester == _CourseData.Semester && data.Month ==_CourseData.Month && data.CourseName == txtCourseName.Text)
                {
                    _errorP.SetError(txtCourseName, "已有相同課程名稱無法儲存!");
                    pass = false;
                }
            }          

            if (string.IsNullOrWhiteSpace(txtSubjectName.Text))
            {
                _errorP.SetError(txtSubjectName, "科目名稱不可空白");
                pass = false;
            }

            if (string.IsNullOrWhiteSpace(txtCredit.Text))
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

        void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (UDTCourseDef data in UDTTransfer.UDTCourseSelectUIDs(_CourseID))
                _CourseData = data;
                       

            // 取得課程
            CourseList = UDTTransfer.UDTCourseSelectAllDict().Values.ToList();
        }

        private void _BGRun()
        {
            if (_bgWorker.IsBusy)
                _isBusy = true;
            else
            {
                this.Loading = true;
                _bgWorker.RunWorkerAsync();
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
             
    }
}
