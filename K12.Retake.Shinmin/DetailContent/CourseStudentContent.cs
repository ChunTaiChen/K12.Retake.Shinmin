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
using DevComponents.DotNetBar;

namespace K12.Retake.Shinmin.DetailContent
{
    [FISCA.Permission.FeatureCode("K12.Retake.Shinmin.DetailContent.CourseStudentContent", "課程修課學生")]
    public partial class CourseStudentContent : FISCA.Presentation.DetailContent
    {
        List<UDTScselectDef> _CourseStudentList = new List<UDTScselectDef>();
        Dictionary<int, StudentRecord> _StudDict = new Dictionary<int, StudentRecord>();
        BackgroundWorker _bgWorker = new BackgroundWorker();
        bool _isBusy = false;
        List<ListViewItem> _lviList= new List<ListViewItem> ();
        List<string> _CourseIDList = new List<string>();
        int _MaxSeatNo = 1;
        public CourseStudentContent()
        {
            InitializeComponent();
            this.Group = "修課學生";
            _bgWorker.DoWork += new DoWorkEventHandler(_bgWorker_DoWork);
            _bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorker_RunWorkerCompleted);
            RetakeEvents.RetakeChanged += new EventHandler(RetakeEvents_RetakeChanged);
        }

        void RetakeEvents_RetakeChanged(object sender, EventArgs e)
        {           
            _BGRun();
        }

        protected override void OnPrimaryKeyChanged(EventArgs e)
        {
            this.Loading = true;
            _CourseIDList.Clear();
            _CourseIDList.Add(PrimaryKey);
          
            _BGRun();
        }

        private void LoadDataToListView()
        {
            lvStudentList.Items.Clear();

            ListViewItem lvi = null;
            _lviList.Clear();
            foreach (UDTScselectDef rec in _CourseStudentList)
            {
                if (_StudDict.ContainsKey(rec.StudentID))
                {
                    lvi = new ListViewItem();
                    StudentRecord studrec = _StudDict[rec.StudentID];
                    lvi.Tag = rec;
                    
                    if (studrec.Class != null)
                        lvi.Text = studrec.Class.Name;
                    else
                       lvi.Text = "";
                    if (studrec.SeatNo.HasValue)
                        lvi.SubItems.Add(studrec.SeatNo.Value.ToString());
                    else
                        lvi.SubItems.Add("");
                    
                    lvi.SubItems.Add(rec.SeatNo.ToString());
                    lvi.SubItems.Add(studrec.Name);
                    lvi.SubItems.Add(studrec.StudentNumber);
                    lvi.SubItems.Add(studrec.Gender);
                    if (rec.NotExam.HasValue && rec.NotExam.Value)
                        lvi.SubItems.Add("是");
                    else                        
                        lvi.SubItems.Add(""); // 否
                    lvi.SubItems.Add(studrec.Status.ToString());
                    lvi.SubItems.Add(rec.Type);
                   

                    _lviList.Add(lvi);
                }
            }
            // sort
            int num;
            //_lviList = (from data in _lviList orderby int.Parse(data.SubItems[colSeatNo.Index].Text) ascending, data.SubItems[colName.Index].Text ascending select data).ToList();
            lvStudentList.Items.AddRange((from data in _lviList orderby int.Parse(data.SubItems[colSeatNo.Index].Text) ascending, data.SubItems[colName.Index].Text ascending select data).ToArray());

            lblStudentCount.Text = "學生人數：" + lvStudentList.Items.Count;

            this.Loading = false;
        }

        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_isBusy)
            {
                _isBusy = false;
                _bgWorker.RunWorkerAsync();
                return;
            }
            LoadDataToListView();
        }

        void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _CourseStudentList = UDTTransfer.UDTSCSelectByCourseIDList(_CourseIDList);

            foreach (UDTScselectDef data in _CourseStudentList)
            {
                if (data.SeatNo > _MaxSeatNo)
                    _MaxSeatNo = data.SeatNo;
                
            }
            _StudDict.Clear();
            List<string> sidList = (from data in _CourseStudentList select data.StudentID.ToString()).Distinct().ToList();
            foreach (StudentRecord rec in Student.SelectByIDs(sidList))
            {
                int sid = int.Parse(rec.ID);
                if(!_StudDict.ContainsKey(sid))
                    _StudDict.Add(sid, rec);
            }
        }

        // 移除所選擇學生
        private void RemoveSelectStudent()
        {
            List<UDTScselectDef> DelList = new List<UDTScselectDef>();
            if (lvStudentList.SelectedItems.Count > 0)
            {
                foreach (ListViewItem lvi in lvStudentList.SelectedItems)
                {
                    UDTScselectDef data = lvi.Tag as UDTScselectDef;
                    if (data == null)
                        continue;

                    DelList.Add(data);
                }
                if (DelList.Count > 0)
                {
                    if (FISCA.Presentation.Controls.MsgBox.Show("確定移除" + DelList.Count + "位學生?", "移除所選學生", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        UDTTransfer.UDTSCSelectDelete(DelList);
                        _BGRun();
                    }
                }
            }
            else
                FISCA.Presentation.Controls.MsgBox.Show("請選擇資料.");
        }

        private void btnRemoveStudent_Click(object sender, EventArgs e)
        {
            RemoveSelectStudent();   
        }

        private void btnAddTempStudent_Click(object sender, EventArgs e)
        {
            CreateStudentMenuItem();

            List<UDTScselectDef> dataList = new List<UDTScselectDef>();

            foreach (object obj in btnAddTempStudent.SubItems)
            {
                StudentRecord stud = null;
                ButtonItem bt = obj as ButtonItem;
                if (bt != null)
                    stud = bt.Tag as StudentRecord;

                if (stud != null)
                {
                    // 當已經加入跳過
                    int sid = int.Parse(stud.ID);
                    if (_StudDict.ContainsKey(sid))
                        continue;

                    UDTScselectDef data = new UDTScselectDef();
                    data.StudentID = sid;
                    data.CourseID = int.Parse(PrimaryKey);
                    _MaxSeatNo++;
                    data.SeatNo = _MaxSeatNo;
                    dataList.Add(data);
                }
            }
            UDTTransfer.UDTSCSelectInsert(dataList);
            _BGRun();
        }

        private void CreateStudentMenuItem()
        {
            btnAddTempStudent.SubItems.Clear();
            if (K12.Presentation.NLDPanels.Student.TempSource.Count == 0)
            {
                LabelItem item = new LabelItem("No", "沒有任何學生在待處理");
                btnAddTempStudent.SubItems.Add(item);
                return;
            }

            List<StudentRecord> studTempRecList = Student.SelectByIDs(K12.Presentation.NLDPanels.Student.TempSource);

            foreach (StudentRecord stud in studTempRecList)
            {
                string strclassname = "", strSeatNo = ""; ;
                if (stud.Class != null)
                {
                    strclassname = stud.Class.Name;
                }
                if (stud.SeatNo.HasValue)
                    strSeatNo = stud.SeatNo.Value.ToString();
                ButtonItem item = new ButtonItem(stud.ID, stud.Name + "(" + strclassname + ")");
                item.Tooltip = "班級：" + strclassname + "\n座號：" + strSeatNo + "\n學號：" + stud.StudentNumber;
                item.Tag = stud;
                item.Click += new EventHandler(item_Click);
                btnAddTempStudent.SubItems.Add(item);
            }

        }

        private void _BGRun()
        {
            if (_bgWorker.IsBusy)
                _isBusy = true;
            else
                _bgWorker.RunWorkerAsync();
        }

        void item_Click(object sender, EventArgs e)
        {
            StudentRecord stud = null;
            ButtonItem bt = sender as ButtonItem;
            if (bt != null)
                stud = bt.Tag as StudentRecord;
            if (stud != null)
            {
                // 檢查是否加入
                int sid = int.Parse(stud.ID);
                if (!_StudDict.ContainsKey(sid))
                {
                    List<UDTScselectDef> dataList = new List<UDTScselectDef>();
                    UDTScselectDef data = new UDTScselectDef();
                    data.StudentID = sid;
                    data.CourseID = int.Parse(PrimaryKey);
                    _MaxSeatNo++;
                    data.SeatNo = _MaxSeatNo;
                    dataList.Add(data);
                    UDTTransfer.UDTSCSelectInsert(dataList);
                    _BGRun();
                }
            }

        }

        private void btnAddTempStudent_PopupOpen(object sender, EventArgs e)
        {
            CreateStudentMenuItem();
        }

        private void tsmAddStudentTemp_Click(object sender, EventArgs e)
        {
            // 加學生加入待處理
            List<string> StudIDList = new List<string>();
            foreach (ListViewItem lvi in lvStudentList.SelectedItems)
            {
                if (lvi.Tag == null)
                    continue;
                UDTScselectDef data = lvi.Tag as UDTScselectDef;
                StudIDList.Add(data.StudentID.ToString());
            }

            if (StudIDList.Count > 0)
                K12.Presentation.NLDPanels.Student.AddToTemp(StudIDList);
        }

        private void tsmClearTemp_Click(object sender, EventArgs e)
        {
            // 清空待處理學生
            K12.Presentation.NLDPanels.Student.RemoveFromTemp(K12.Presentation.NLDPanels.Student.TempSource);
        }

        private void tsmWMark_Click(object sender, EventArgs e)
        {
            RunNotExam(true);
        }

        /// <summary>
        /// 執行扣考 傳入 true 是,false 否
        /// </summary>
        private void RunNotExam(bool value)
        {
            // 執行扣考
            List<UDTScselectDef> updateList = new List<UDTScselectDef>();
            foreach (ListViewItem lvi in lvStudentList.SelectedItems)
            {
                if (lvi.Tag == null)
                    continue;

                UDTScselectDef data = lvi.Tag as UDTScselectDef;
                data.NotExam = value;
                updateList.Add(data);
            }

            if (updateList.Count > 0)
            {
                UDTTransfer.UDTSCSelectUpdate(updateList);
                _BGRun();
            }        
        }

        private void tmsCMark_Click(object sender, EventArgs e)
        {
            // 解除扣考
            RunNotExam(false);
        }

        private void tsmRemoveStudent_Click(object sender, EventArgs e)
        {
            // 移除所選學生
            RemoveSelectStudent();
        }

        private void 設成重修ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunRetake(true);
        }

        private void 設成補修ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunRetake(false);
        }

        /// <summary>
        /// 設成重修或補修 傳入 true 重修 ,false 補修
        /// </summary>
        private void RunRetake(bool value)
        {
            // 執行扣考
            List<UDTScselectDef> updateList = new List<UDTScselectDef>();
            foreach (ListViewItem lvi in lvStudentList.SelectedItems)
            {
                if (lvi.Tag == null)
                    continue;

                UDTScselectDef data = lvi.Tag as UDTScselectDef;
                if (value)
                    data.Type = "重修";
                else
                    data.Type = "補修";
                updateList.Add(data);
            }

            if (updateList.Count > 0)
            {
                UDTTransfer.UDTSCSelectUpdate(updateList);
                _BGRun();
            }
        }
    }
}
