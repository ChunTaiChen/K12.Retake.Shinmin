using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using DevComponents.DotNetBar;
using FISCA.UDT;
using K12.Retake.Shinmin.DAO;
using K12.Data;

namespace K12.Retake.Shinmin.Form
{
    public partial class AttendanceForm : BaseForm
    {
        #region 全域變數

        AccessHelper _accessHelper = new AccessHelper();

        string AttendanceName = "缺課";

        List<string> SaveListViewExList = new List<string>();

        AttendanceMeg meg { get; set; }

        /// <summary>
        /// 畫面上學生個人資料範圍
        /// </summary>
        int _startIndex = 3;

        /// <summary>
        /// 畫面已被修改
        /// </summary>
        private bool _Bool_DataListener = false;

        /// <summary>
        /// 是否重覆執行背景模式
        /// </summary>
        private bool _Bool_BGW_Click = false;

        /// <summary>
        /// 開始取得課程資料
        /// </summary>
        private BackgroundWorker BGW_Load = new BackgroundWorker();

        /// <summary>
        /// 開始取得課程相關資料
        /// </summary>
        private BackgroundWorker BGW_Click = new BackgroundWorker();

        /// <summary>
        /// 儲存此缺曠記錄
        /// </summary>
        private BackgroundWorker BGW_Save = new BackgroundWorker();

        List<UDTAttendanceDef> InsertList = new List<UDTAttendanceDef>();
        List<UDTAttendanceDef> DeltetList = new List<UDTAttendanceDef>(); 

        #endregion

        public AttendanceForm()
        {
            InitializeComponent();
        }

        private void AttendanceForm_Load(object sender, EventArgs e)
        {
            if (RetakeAdmin.Instance.SelectedSource.Count == 0)
                return;
            //載入
            BGW_Load.DoWork += new DoWorkEventHandler(BGW_Load_DoWork);
            BGW_Load.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BGW_Load_RunWorkerCompleted);
            //選取
            BGW_Click.DoWork += new DoWorkEventHandler(BGW_Click_DoWork);
            BGW_Click.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BGW_Click_RunWorkerCompleted);
            //儲存
            BGW_Save.DoWork += new DoWorkEventHandler(BGW_Save_DoWork);
            BGW_Save.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BGW_Save_RunWorkerCompleted);

            SetBGWIsBusy = false;
            BGW_Load.RunWorkerAsync();
        }

        void BGW_Load_DoWork(object sender, DoWorkEventArgs e)
        {
            //取得使用者所選課程Record記錄
            List<UDTCourseDef> CourseList = _accessHelper.Select<UDTCourseDef>("uid in ('" + string.Join("','", RetakeAdmin.Instance.SelectedSource) + "')");
            e.Result = CourseList;
        }

        void BGW_Load_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetBGWIsBusy = true;

            if (e.Cancelled)
            {
                MsgBox.Show("作業已中止!!");
                return;
            }

            if (e.Error != null)
            {
                MsgBox.Show("取得課程資料發生錯誤!!" + e.Error.Message);
                return;
            }


            List<UDTCourseDef> CourseList = (List<UDTCourseDef>)e.Result;
            CourseList.Sort(SortCourse);

            if (CourseList.Count != 0)
            {
                itmPnlTimeName.Items.Clear();
                foreach (UDTCourseDef each in CourseList)
                {
                    ButtonItem btnItem = new ButtonItem();
                    btnItem.Text = each.CourseName;
                    btnItem.Tag = each;
                    btnItem.OptionGroup = "itmPnlTimeName";
                    btnItem.ButtonStyle = eButtonStyle.ImageAndText;
                    btnItem.Click += new EventHandler(btnItem_Click);

                    itmPnlTimeName.Items.Add(btnItem);
                }
            }

            itmPnlTimeName.ResumeLayout();
            itmPnlTimeName.Refresh();
        }

        void btnItem_Click(object sender, EventArgs e)
        {
            if (_Bool_DataListener)
            {
                DialogResult dr = MsgBox.Show("資料已修改,確認放棄?", MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2);
                if (dr != System.Windows.Forms.DialogResult.Yes)
                {
                    return;
                }
            }

            cbSelectAll.Checked = false;
            BindData();
        }

        void BindData()
        {
            if (itmPnlTimeName.SelectedItems.Count == 1)
            {
                if (BGW_Click.IsBusy)
                {
                    _Bool_BGW_Click = true;
                }
                else
                {
                    SetBGWIsBusy = false;
                    //清除日期清單
                    listViewEx1.Items.Clear();

                    //取得目前所選擇的Button
                    ButtonItem Buttonitem = itmPnlTimeName.SelectedItems[0] as ButtonItem;
                    //取得課程Record
                    UDTCourseDef course = (UDTCourseDef)Buttonitem.Tag;

                    BGW_Click.RunWorkerAsync(course);
                }
            }
        }

        void BGW_Click_DoWork(object sender, DoWorkEventArgs e)
        {
            UDTCourseDef course = (UDTCourseDef)e.Argument;
            //建立課程相關資料
            meg = new AttendanceMeg(course);
        }

        void BGW_Click_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //解除畫面鎖定
            SetBGWIsBusy = true;
            //畫面未修改
            SetButton(false);

            if (_Bool_BGW_Click)
            {
                SetBGWIsBusy = _Bool_BGW_Click = false;

                //清除日期清單
                listViewEx1.Items.Clear();
                //取得目前所選擇的Button
                ButtonItem Buttonitem = itmPnlTimeName.SelectedItems[0] as ButtonItem;
                //取得課程Record
                UDTCourseDef course = (UDTCourseDef)Buttonitem.Tag;

                BGW_Click.RunWorkerAsync(course);
                return;
            }

            if (e.Cancelled)
            {
                MsgBox.Show("作業已中止!!");
                return;
            }

            if (e.Error != null)
            {
                MsgBox.Show("建立課程相關資料發生錯誤!!" + e.Error.Message);
                return;
            }

            //將標題建立
            groupPanel1.Text = "課程名稱：" + meg.CourseName;

            //填入目前課程所需上課日期
            List<string> DateList = new List<string>();
            foreach (UDTTimeSectionDef each in meg._TimeSectionList)
            {
                if (!DateList.Contains(each.Date.ToShortDateString()))
                {
                    DateList.Add(each.Date.ToShortDateString());
                }
            }

            foreach (string each in DateList)
            {
                ListViewItem item = new ListViewItem();
                item.Text = each;
                item.Tag = each; //只存日期
                listViewEx1.Items.Add(item);
            }

            //建立Column預設欄位
            DefColumn();

            List<AttendanceRow> RowList = new List<AttendanceRow>();

            //學生資料,需排序學生座號
            foreach (UDTScselectDef each in meg._StudentList)
            {
                AttendanceRow row = new AttendanceRow();
                row.CourseSeatNo = each.SeatNo.ToString();
                row.StudentName = meg.StudentDic[each.StudentID.ToString()].Name;
                row.StudentNumber = meg.StudentDic[each.StudentID.ToString()].StudentNumber;
                row.Ref_Student_ID = each.StudentID.ToString();
                row.Ref_Course_ID = meg._Course.UID;
                RowList.Add(row);
            }

            foreach (UDTAttendanceDef each in meg.AttendanceList)
            {
                foreach (AttendanceRow row in RowList)
                {
                    if (row.Ref_Student_ID == each.StudentID.ToString())
                    {
                        if (!row.AttendanceDic.ContainsKey(each.TimeSectionID.ToString()))
                        {
                            row.AttendanceDic.Add(each.TimeSectionID.ToString(), each);
                            break;
                        }
                    }
                }
            }

            dataGridViewX1.AutoGenerateColumns = false;
            dataGridViewX1.DataSource = new BindingList<AttendanceRow>(RowList);

            //如果進行儲存動作
            if (SaveListViewExList.Count != 0)
            {
                foreach (ListViewItem item in listViewEx1.Items)
                {
                    if (SaveListViewExList.Contains(item.Text))
                    {
                        item.Checked = true;
                    }
                }
                SaveListViewExList.Clear();
            }
        }

        #region Column&ListView

        private void listViewEx1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            //建立Column預設欄位
            DefColumn();

            DefListViewItemColumn();

            DefDataGridViewValue();
        }

        private void DefColumn()
        {
            dataGridViewX1.Columns.Clear();

            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.Name = "colSeatNo";
            column.HeaderText = "課程座號";
            column.Width = 65;
            column.DefaultCellStyle.BackColor = Color.LightCyan;
            column.DataPropertyName = "CourseSeatNo";
            column.ReadOnly = true;
            dataGridViewX1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "colStudentNumber";
            column.HeaderText = "學號";
            column.Width = 80;
            column.DefaultCellStyle.BackColor = Color.LightCyan;
            column.DataPropertyName = "StudentNumber";
            column.ReadOnly = true;
            dataGridViewX1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "colStudentName";
            column.HeaderText = "姓名";
            column.Width = 80;
            column.DefaultCellStyle.BackColor = Color.LightCyan;
            column.DataPropertyName = "StudentName";
            column.ReadOnly = true;
            column.Frozen = false;
            dataGridViewX1.Columns.Add(column);
        }

        private void DefListViewItemColumn()
        {
            foreach (ListViewItem item in listViewEx1.Items)
            {
                if (item.Checked)
                {
                    foreach (UDTTimeSectionDef each in meg._TimeSectionList)
                    {
                        if (each.Date.ToShortDateString() == item.Text)
                        {
                            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                            column.Name = "col" + item.Text;
                            column.HeaderText = each.Date.ToShortDateString() + "第" + each.Period + "節";
                            column.Width = 80;
                            column.Tag = each;
                            column.ReadOnly = true;
                            //column.DataPropertyName = "CourseSeatNo";
                            dataGridViewX1.Columns.Add(column);
                        }
                    }
                }
            }
        }

        private void DefDataGridViewValue()
        {
            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                AttendanceRow attRow = (AttendanceRow)row.DataBoundItem;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.ColumnIndex < _startIndex)
                        continue;

                    UDTTimeSectionDef dtS = (UDTTimeSectionDef)cell.OwningColumn.Tag;
                    if (attRow.AttendanceDic.ContainsKey(dtS.UID))
                    {
                        cell.Value = AttendanceName;
                    }
                }
            }
        } 

        #endregion

        #region Save

        private void btnSave_Click(object sender, EventArgs e)
        {
            InsertList.Clear();
            DeltetList.Clear();

            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                AttendanceRow attRow = (AttendanceRow)row.DataBoundItem;
                if (attRow.HasChange)
                {
                    foreach (string each in attRow.AttendanceDic.Keys)
                    {
                        if (string.IsNullOrEmpty(attRow.AttendanceDic[each].UID))
                        {
                            InsertList.Add(attRow.AttendanceDic[each]);
                        }
                    }
                    if (attRow.Delete.Count != 0)
                    {
                        DeltetList.AddRange(attRow.Delete);
                    }
                }
            }

            SetBGWIsBusy = false;
            BGW_Save.RunWorkerAsync();
        }

        void BGW_Save_DoWork(object sender, DoWorkEventArgs e)
        {
            _accessHelper.DeletedValues(DeltetList);
            _accessHelper.InsertValues(InsertList);
        }

        void SetButton(bool setvalue)
        {
            _Bool_DataListener = setvalue;
            btnSave.Pulse(5);

            if (setvalue)
            {
                this.Text = "課程缺曠登錄(已修改)";
            }
            else
            {
                this.Text = "課程缺曠登錄";
            }
        }

        void BGW_Save_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetBGWIsBusy = true;
            if (e.Cancelled)
            {
                MsgBox.Show("作業已中止!!");
                return;
            }

            if (e.Error != null)
            {
                MsgBox.Show("儲存發生錯誤!!" + e.Error.Message);
                return;
            }

            MsgBox.Show("儲存成功!!");

            ChangeReNew();
        }

        private void ChangeReNew()
        {
            foreach (ListViewItem item in listViewEx1.Items)
            {
                if (item.Checked)
                {
                    SaveListViewExList.Add(item.Text);
                }
            }

            SetBGWIsBusy = false;
            //清除日期清單
            listViewEx1.Items.Clear();

            //取得目前所選擇的Button
            ButtonItem Buttonitem = itmPnlTimeName.SelectedItems[0] as ButtonItem;
            //取得課程Record
            UDTCourseDef course = (UDTCourseDef)Buttonitem.Tag;

            BGW_Click.RunWorkerAsync(course);


        }

        #endregion

        private void dataGridViewX1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (e.ColumnIndex < _startIndex) return;
            if (e.RowIndex < 0) return;

            DataGridViewCell cell = dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            UDTTimeSectionDef timeS = (UDTTimeSectionDef)cell.OwningColumn.Tag;
            AttendanceRow attRow = (AttendanceRow)cell.OwningRow.DataBoundItem;

            //畫面資料有更動
            SetButton(true);

            if ("" + cell.Value == AttendanceName)
            {
                attRow.RemoveAttendance(timeS.UID);
                cell.Value = "";
            }
            else
            {
                attRow.SetAttendance(timeS.UID);
                //在Cell填入名稱
                cell.Value = AttendanceName;
            }
        }

        private void dataGridViewX1_KeyDown(object sender, KeyEventArgs e)
        {
            if (CheckKey(e.KeyCode))
            {
                foreach (DataGridViewCell cell in dataGridViewX1.SelectedCells)
                {
                    if (cell.ColumnIndex < _startIndex)
                        continue;

                    //畫面有更動
                    SetButton(true);

                    UDTTimeSectionDef timeS = (UDTTimeSectionDef)cell.OwningColumn.Tag;
                    AttendanceRow attRow = (AttendanceRow)cell.OwningRow.DataBoundItem;

                    if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Delete)
                    {
                        attRow.RemoveAttendance(timeS.UID);
                        cell.Value = "";
                    }
                    else if (e.KeyCode == Keys.A)
                    {
                        attRow.SetAttendance(timeS.UID);
                        //在Cell填入名稱
                        cell.Value = AttendanceName;
                    }
                }
            }
        }

        private bool CheckKey(Keys keys)
        {
            if (keys == Keys.A || keys == Keys.Space || keys == Keys.Delete)
                return true;
            else
                return false;
        }

        private void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewEx1.Items)
            {
                item.Checked = cbSelectAll.Checked;
            }
        }

        private void AttendanceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_Bool_DataListener)
            {
                DialogResult dr = MsgBox.Show("資料已修改,確認放棄?", MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2);
                if (dr == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private int SortCourse(UDTCourseDef a, UDTCourseDef b)
        {
            return a.CourseName.CompareTo(b.CourseName);
        }

        private bool SetBGWIsBusy
        {
            set
            {
                listViewEx1.Enabled = value;
                dataGridViewX1.Enabled = value;
                btnSave.Enabled = value;

                if (!value)
                    this.Text = "課程缺曠登錄(取得資料中...)";
                else
                    this.Text = "課程缺曠登錄";
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
