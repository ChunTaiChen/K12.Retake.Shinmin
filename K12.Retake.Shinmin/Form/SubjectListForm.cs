using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using K12.Retake.Shinmin.DAO;
using System.Xml.Linq;


namespace K12.Retake.Shinmin.Form
{
    public partial class SubjectListForm : FISCA.Presentation.Controls.BaseForm, ISubjecAdd
    {
        BackgroundWorker _bgWorker = new BackgroundWorker();
        List<UDTSubjectDef> _UDTSubjectList = new List<UDTSubjectDef>();
        int _SchoolYear = 0, _Semester = 0, _Month = 0;
        Dictionary<string, UDTSubjectDef> _checkCanAddDict = new Dictionary<string, UDTSubjectDef>();
        List<string> _AllDeptNameList = new List<string>();
        List<CourseTableDept> _AllCourseTableDeptList = new List<CourseTableDept>();
        List<UDTSubjectDef> _InsertDataList = new List<UDTSubjectDef>();
        List<UDTSubjectDef> _UpdateDataList = new List<UDTSubjectDef>();
        List<UDTSubjectDef> _DeleteDataList = new List<UDTSubjectDef>();
        Dictionary<string, bool> _checkSameDict = new Dictionary<string, bool>();

        //科目類別
        string[] strSubjType = new string[] { "專業", "實習", "共同" };

        public bool _isShowForm = true;
        Dictionary<int, string> _CourseTableNameDict = new Dictionary<int, string>();
        public SubjectListForm()
        {
            InitializeComponent();
            _isShowForm = true;
            if (string.IsNullOrEmpty(GetCurrentTimeList()))
            {
                FISCA.Presentation.Controls.MsgBox.Show("你必須由功能[建議重補修名單]\n指定一個目前工作名單!!");
                _isShowForm = false;
            }
            else
            {
                colSubjectType.Items.AddRange(strSubjType.ToArray());
                _bgWorker.DoWork += new DoWorkEventHandler(_bgWorker_DoWork);
                _bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorker_RunWorkerCompleted);
                _bgWorker.RunWorkerAsync();
            }
        }

        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            colCourseTimetable.Items.Clear();
            foreach (string each in _CourseTableNameDict.Values)
            {
                colCourseTimetable.Items.Add(each);
            }
            LoadDataToDataGrid();
            colDept.Items.AddRange(_AllDeptNameList.ToArray());
        }

        private void LoadDataToDataGrid()
        {
            dgData.Rows.Clear();


            foreach (UDTSubjectDef data in _UDTSubjectList)
            {
                int rowIdx = dgData.Rows.Add();
                dgData.Rows[rowIdx].Tag = data;
                dgData.Rows[rowIdx].Cells[colSubjectName.Index].Value = data.SubjectName;
                if (data.SubjecLevel.HasValue)
                    dgData.Rows[rowIdx].Cells[colSubjectLevel.Index].Value = data.SubjecLevel.Value;

                dgData.Rows[rowIdx].Cells[colCredit.Index].Value = data.Credit;
                dgData.Rows[rowIdx].Cells[colDept.Index].Value = data.DeptName;
                if (_CourseTableNameDict.ContainsKey(data.CourseTimetableID))
                    dgData.Rows[rowIdx].Cells[colCourseTimetable.Index].Value = _CourseTableNameDict[data.CourseTimetableID];

                dgData.Rows[rowIdx].Cells[colSubjectType.Index].Value = data.SubjectType;
                // 解析節次
                if (!string.IsNullOrWhiteSpace(data.PeriodContent))
                {
                    XElement elmRoot = XElement.Parse(data.PeriodContent);
                    foreach (XElement elm in elmRoot.Elements("Period"))
                    {
                        switch (elm.Value)
                        {
                            case "1": dgData.Rows[rowIdx].Cells[colWp1.Index].Value = "V"; break;
                            case "2": dgData.Rows[rowIdx].Cells[colWp2.Index].Value = "V"; break;
                            case "3": dgData.Rows[rowIdx].Cells[colWp3.Index].Value = "V"; break;
                            case "4": dgData.Rows[rowIdx].Cells[colWp4.Index].Value = "V"; break;
                            case "5": dgData.Rows[rowIdx].Cells[colWp5.Index].Value = "V"; break;
                            case "6": dgData.Rows[rowIdx].Cells[colWp6.Index].Value = "V"; break;
                            case "7": dgData.Rows[rowIdx].Cells[colWp7.Index].Value = "V"; break;
                            case "8": dgData.Rows[rowIdx].Cells[colWp8.Index].Value = "V"; break;
                        }
                    }
                }


                CheckCanAddData(data);
            }
            ReloadRowCount();
        }

        void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _UDTSubjectList = UDTTransfer.UDTSubjectSelectByP1(_SchoolYear, _Semester, _Month);
            _AllDeptNameList = QueryData.GetAllDeptName();

            _CourseTableNameDict.Clear();
            // 取得課表名稱
            foreach (UDTCourseTimetableDef data in UDTTransfer.UDTCourseTimetableSelectAll())
                _CourseTableNameDict.Add(int.Parse(data.UID), data.Name);

            _AllCourseTableDeptList = UDTTransfer.GetAllCourseTableDeptList();
        }

        private void btnGetSuggestSubject_Click(object sender, EventArgs e)
        {
            SuggestSubjectForm ssf = new SuggestSubjectForm(this);
            ssf.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SubjectListForm_Load(object sender, EventArgs e)
        {
                lblTitle.Text = GetCurrentTimeList();
                //
        }

        /// <summary>
        /// 檢查是否可新增資料
        /// </summary>
        private bool CheckCanAddData(UDTSubjectDef data)
        {
            bool retVal = true;
            //if (data != null)
            //{
            //    string key = "";
            //    if (data.SubjecLevel.HasValue)
            //        key = data.SubjectName + "_" + data.SubjecLevel.Value + "_" + data.Credit;
            //    else
            //        key = data.SubjectName + "_" + data.Credit;
            //    if (_checkCanAddDict.ContainsKey(key))
            //        retVal = false;
            //    else
            //        _checkCanAddDict.Add(key, data);
            //}
            return retVal;
        }

        /// <summary>
        /// 取得目前期間,學年度： 學期： 月份：
        /// </summary>
        private string GetCurrentTimeList()
        {
            string retVal = "";
            UDTTimeListDef data = UDTTransfer.UDTTimeListGetActiveTrue1();
            if (!string.IsNullOrEmpty(data.UID))
            {
                retVal = data.SchoolYear + "學年度　";
                retVal += "第" + data.Semester + "學期　";
                retVal += data.Month + "梯次";

                _SchoolYear = data.SchoolYear;
                _Semester = data.Semester;
                _Month = data.Month;
            }


            return retVal;
        }

        public void QuickAdd(SuggestSubjectCount sData)
        {
            UDTSubjectDef newData = new UDTSubjectDef();
            newData.SchoolYear = _SchoolYear;
            newData.Semester = _Semester;
            newData.Month = _Month;
            newData.Credit = sData.Credit;
            newData.SubjectName = sData.SubjectName;
            newData.SubjecLevel = sData.Level;
            newData.DeptName = sData.DeptName;

            if (CheckCanAddData(newData))
            {
                int rowIdx = dgData.Rows.Add();
                dgData.Rows[rowIdx].Cells[colCredit.Index].Value = newData.Credit;
                dgData.Rows[rowIdx].Cells[colDept.Index].Value = newData.DeptName;
                dgData.Rows[rowIdx].Cells[colSubjectName.Index].Value = newData.SubjectName;
                if (newData.SubjecLevel.HasValue)
                    dgData.Rows[rowIdx].Cells[colSubjectLevel.Index].Value = newData.SubjecLevel.Value;
                dgData.Rows[rowIdx].Tag = newData;
                ReloadRowCount();
            }
        }

        /// <summary>
        /// 計算資料筆數
        /// </summary>
        private void ReloadRowCount()
        {
            int rowCount = 0;
            foreach (DataGridViewRow dr in dgData.Rows)
            {
                if (dr.IsNewRow)
                    continue;
                rowCount++;
            }
            lblCount.Text = "資料筆數：" + rowCount + "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                #region Save

                btnSave.Enabled = false;

                // 檢查必填
                foreach (DataGridViewRow dr in dgData.Rows)
                {
                    if (dr.IsNewRow)
                        continue;
                    foreach (DataGridViewCell cell in dr.Cells)
                    {
                        // 級別、科目類別、課表
                        if (cell.ColumnIndex == colSubjectLevel.Index || cell.ColumnIndex == colSubjectType.Index)
                            if (cell.Value == null)
                                cell.Value = "";

                        // 科目名稱與學分數
                        if (cell.ColumnIndex == colSubjectName.Index || cell.ColumnIndex == colCredit.Index)
                        {
                            if (cell.Value == null || cell.Value.ToString().Trim() == "")
                                cell.ErrorText = "請輸入資料!";

                        }

                        // 檢查課表
                        if (cell.ColumnIndex == colCourseTimetable.Index)
                        {
                            if (cell.Value == null || cell.Value.ToString() == "")
                                dgData.Rows[cell.RowIndex].ErrorText = "所屬課表必填!";
                        }

                    }

                }

                // 檢查資料是否有Error
                foreach (DataGridViewRow dr in dgData.Rows)
                {
                    if (dr.ErrorText != "")
                    {
                        FISCA.Presentation.Controls.MsgBox.Show("資料檢查有錯誤請修改!");
                        btnSave.Enabled = true;
                        return;
                    }

                    foreach (DataGridViewCell cell in dr.Cells)
                    {
                        if (cell.ErrorText != "")
                        {
                            FISCA.Presentation.Controls.MsgBox.Show("資料檢查有錯誤請修改!");
                            btnSave.Enabled = true;
                            return;
                        }
                    }
                }


                // 讀取需要新增或更新，並轉換資料
                foreach (DataGridViewRow row in dgData.Rows)
                {
                    if (row.IsNewRow)
                        continue;

                    UDTSubjectDef data = row.Tag as UDTSubjectDef;
                    if (data == null)
                        data = new UDTSubjectDef();

                    data.SchoolYear = _SchoolYear;
                    data.Semester = _Semester;
                    data.Month = _Month;

                    if (row.Cells[colDept.Index].Value != null)
                        data.DeptName = row.Cells[colDept.Index].Value.ToString();
                    else
                        data.DeptName = "";

                    data.SubjectName = row.Cells[colSubjectName.Index].Value.ToString();
                    if (row.Cells[colSubjectLevel.Index].Value != null && row.Cells[colSubjectLevel.Index].Value.ToString() == "")
                        data.SubjecLevel = null;
                    else
                        data.SubjecLevel = int.Parse(row.Cells[colSubjectLevel.Index].Value.ToString());

                    if(row.Cells[colCredit.Index].Value !=null)
                        data.Credit = int.Parse(row.Cells[colCredit.Index].Value.ToString());

                    if(row.Cells[colSubjectType.Index].Value!=null)
                        data.SubjectType = row.Cells[colSubjectType.Index].Value.ToString();

                    if (row.Cells[colCourseTimetable.Index].Value != null)
                        foreach (CourseTableDept cc in _AllCourseTableDeptList.Where(x => x.CourseTableName == row.Cells[colCourseTimetable.Index].Value.ToString()))
                            data.CourseTimetableID = cc.CourseTableID;

                    // 處理節次
                    XElement elmRoot = new XElement("Periods");
                    int per = 1;
                    for (int i = colWp1.Index; i <= colWp8.Index; i++)
                    {
                        if (row.Cells[i].Value != null && row.Cells[i].Value.ToString() != "")
                        {
                            XElement elm = new XElement("Period");
                            elm.SetValue(per);
                            elmRoot.Add(elm);
                        }
                        per++;
                    }
                    data.PeriodContent = "";
                    if (elmRoot.Elements().Count() > 0)
                    {
                        data.PeriodContent = elmRoot.ToString();
                    }

                    if (string.IsNullOrEmpty(data.UID))
                        _InsertDataList.Add(data);
                    else
                        _UpdateDataList.Add(data);
                }

                if (_DeleteDataList.Count > 0)
                    UDTTransfer.UDTSubjectDelete(_DeleteDataList);

                if (_InsertDataList.Count > 0)
                    UDTTransfer.UDTSubjectInsert(_InsertDataList);

                if (_UpdateDataList.Count > 0)
                    UDTTransfer.UDTSubjectUpdate(_UpdateDataList);

                btnSave.Enabled = true;
                _InsertDataList.Clear();
                _UpdateDataList.Clear();
                _DeleteDataList.Clear();
                FISCA.Presentation.Controls.MsgBox.Show("儲存完成");
                this.Close();

                #endregion
            }
            catch (Exception ex)
            {
                FISCA.Presentation.Controls.MsgBox.Show("儲存失敗，" + ex.Message);
                btnSave.Enabled = true;
            }
        }

        private void dgData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!dgData.Rows[e.RowIndex].IsNewRow)
            {
                if (e.ColumnIndex >= colWp1.Index && e.ColumnIndex <= colWp8.Index)
                {
                    DataGridViewCell cell = dgData.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    if (cell.Value != null)
                    {
                        if (!string.IsNullOrWhiteSpace(cell.Value.ToString()))
                            cell.Value = "V";
                    }
                    else
                        cell.Value = "V";
                }
                dgData.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "";
                dgData.Rows[e.RowIndex].ErrorText = "";
                // 檢查必填
                if (e.ColumnIndex == colSubjectName.Index || e.ColumnIndex == colCredit.Index)
                {
                    if (dgData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null || dgData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "")
                    {
                        dgData.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "必填!";
                        return;
                    }
                }

                // 檢查課表必填
                if (e.ColumnIndex == colCourseTimetable.Index)
                {
                    if (dgData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null || dgData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "")
                    {
                        dgData.Rows[e.RowIndex].ErrorText = "所屬課表必填!";
                        return;
                    }
                }

                // 檢查是否是數字
                if (e.ColumnIndex == colCredit.Index)
                {
                    int d;
                    if (!int.TryParse(dgData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out d))
                    {
                        dgData.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "必須是整數!";
                        return;
                    }
                }

                // 檢查數字或空值
                if (e.ColumnIndex == colSubjectLevel.Index)
                {
                    if (dgData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                    {
                        int ii;
                        if (!int.TryParse(dgData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out ii))
                        {
                            dgData.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "必須是整數!";
                            return;
                        }
                    }
                }

                // 檢查資料是否重複
                if (e.ColumnIndex == colSubjectName.Index || e.ColumnIndex == colSubjectLevel.Index || e.ColumnIndex == colCredit.Index)
                {
                    _checkSameDict.Clear();
                    foreach (DataGridViewRow dr in dgData.Rows)
                    {
                        string key = "";
                        if (dr.Cells[colSubjectName.Index].Value != null)
                            key += dr.Cells[colSubjectName.Index].Value.ToString();

                        if (dr.Cells[colSubjectLevel.Index].Value != null)
                            key += "_" + dr.Cells[colSubjectLevel.Index].Value.ToString();

                        if (dr.Cells[colCredit.Index].Value != null)
                            key += "_" + dr.Cells[colCredit.Index].Value.ToString();

                        if (!_checkSameDict.ContainsKey(key))
                            _checkSameDict.Add(key, false);
                        else
                        {
                            dr.Cells[e.ColumnIndex].ErrorText = "資料有重複!";
                            return;
                        }
                    }
                }
            }

        }

        private void dgData_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // 註解是因操作方式與時間設定功能一致
            //if (e.Button != System.Windows.Forms.MouseButtons.Left)
            //    return;
            //if (e.ColumnIndex < colWp1.Index)
            //    return;
            //if (e.ColumnIndex >= colWp1.Index && e.ColumnIndex <= colWp8.Index)
            //{
            //    DataGridViewCell cell = dgData.Rows[e.RowIndex].Cells[e.ColumnIndex];
            //    if (cell.Value != null)
            //    {
            //        if (string.IsNullOrWhiteSpace(cell.Value.ToString()))
            //            cell.Value = "V";
            //        else
            //            cell.Value = "";
            //    }
            //    else
            //        cell.Value = "V";
            //}
        }

        private void dgData_KeyDown(object sender, KeyEventArgs e)
        {
            // 使用者所選範圍內
            foreach (DataGridViewCell cell in dgData.SelectedCells)
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                    return;
                // 當不在節次範圍內
                if (cell.ColumnIndex >= colWp1.Index && cell.ColumnIndex <= colWp8.Index)
                {
                    // 按 tab 鍵跳過
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
                        // 不處理
                    }                    
                }
            }

        }

        private void dgData_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (FISCA.Presentation.Controls.MsgBox.Show("請問是否刪除?", "刪除科目", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                UDTSubjectDef delSubj = e.Row.Tag as UDTSubjectDef;
                if (delSubj != null)
                {
                    _DeleteDataList.Add(delSubj);
                }
            }
            else
                e.Cancel = true;
        }

        private void 批次修改科別ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PubSetValue("請選擇科別名稱", _AllDeptNameList, colDept.Index);
        }

        private void 批次修改所屬課表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PubSetValue("請選擇所屬課表", _CourseTableNameDict.Values.ToList(), colCourseTimetable.Index);
        }

        private void 批次修改科目類別ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PubSetValue("請選擇科目類別", strSubjType.ToList(), colSubjectType.Index);
        }

        private void PubSetValue(string name, List<string> list, int Columnindex)
        {
            Dylan.phChangeData phD = new Dylan.phChangeData(name, list);
            if (phD.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {
                //填入選取資料
                foreach (DataGridViewCell cell in dgData.SelectedCells)
                {
                    if (cell.ColumnIndex == Columnindex)
                    {
                        cell.Value = phD.selectName;
                    }
                }
            }
        }
    }
}
