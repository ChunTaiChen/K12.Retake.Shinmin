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
    public partial class SubAddEditCourseTimetableForm : FISCA.Presentation.Controls.BaseForm
    {
        public enum EditMode { Add,Edit}
        EditMode _editMode;
        BackgroundWorker _bgWorker = new BackgroundWorker();
        UDTCourseTimetableDef _CurrentCourseTimetable;
        // 系統內所有科別
        List<string> _allDeptNameList = new List<string>();
        // 已設定科別
        List<UDTCdselectDef> _SelectDeptDefList = new List<UDTCdselectDef>();

        public SubAddEditCourseTimetableForm(EditMode editMode, UDTCourseTimetableDef data)
        {
            InitializeComponent();            
            _CurrentCourseTimetable = data;
            _editMode = editMode;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void LoadData()
        {
            lvwDept.Items.Clear();
            if (_editMode == EditMode.Edit)
            {
                txtName.Text = _CurrentCourseTimetable.Name;
                txtName.Enabled = false;                
            }
            else
                txtName.Enabled = true;

            _bgWorker.RunWorkerAsync();
            
        }

        private void SubAddEditCourseTimetableForm_Load(object sender, EventArgs e)
        {            
            _bgWorker.DoWork += new DoWorkEventHandler(_bgWorker_DoWork);
            _bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorker_RunWorkerCompleted);
            LoadData();
        }

        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<string> _SelectDeptName = (from da in _SelectDeptDefList select da.DeptName).ToList();

            // 將已經選標勾選
            foreach (string str in _allDeptNameList)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = str;
                lvi.Checked = false;
                if (_SelectDeptName.Contains(str))
                    lvi.Checked = true;

                lvwDept.Items.Add(lvi);
            }                    
        }

        void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _allDeptNameList = QueryData.GetAllDeptName();
            _SelectDeptDefList.Clear();
            if(_CurrentCourseTimetable !=null)
                _SelectDeptDefList = UDTTransfer.UDTCdselectSelectByCTimeTableID(_CurrentCourseTimetable.UID);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    FISCA.Presentation.Controls.MsgBox.Show("名稱不可空白!");
                    return;
                }

                btnSave.Enabled = false;

                if (_editMode == EditMode.Edit)
                {
                    // 先刪除原有
                    if (_SelectDeptDefList.Count > 0)
                        UDTTransfer.UDTCdselectDelete(_SelectDeptDefList);
                }
                else
                { 
                    // 新增資料
                    string newName = txtName.Text;
                    bool checkSameName = false;
                    foreach (UDTCourseTimetableDef da in UDTTransfer.UDTCourseTimetableSelectAll())
                    {
                        if (da.Name == newName)
                        {
                            checkSameName = true;
                            break;
                        }
                    }

                    if (checkSameName)
                    {
                        FISCA.Presentation.Controls.MsgBox.Show("已有相同課表名稱無法新增,請修改課表名稱.");
                        return;
                    }
                    else
                    { 
                       // 新增一筆課表
                        List<UDTCourseTimetableDef> insertDataList = new List<UDTCourseTimetableDef>();
                        UDTCourseTimetableDef newIData = new UDTCourseTimetableDef();
                        newIData.Name = newName;
                        insertDataList.Add(newIData);
                        UDTTransfer.UDTCourseTimetableInsert(insertDataList);
                    
                        // 將目前可表設成新增
                        foreach (UDTCourseTimetableDef da in UDTTransfer.UDTCourseTimetableSelectAll())
                        {
                            if (da.Name == newName)
                            {
                                _CurrentCourseTimetable = da;
                                break;
                            }
                        }
                    }
                }                

                //新增勾選
                List<UDTCdselectDef> insertDeptDataList = new List<UDTCdselectDef>();
                foreach (ListViewItem lvi in lvwDept.CheckedItems)
                {
                    UDTCdselectDef newData = new UDTCdselectDef();
                    newData.RefCourseTimetableID = int.Parse(_CurrentCourseTimetable.UID);
                    newData.DeptName = lvi.Text;
                    insertDeptDataList.Add(newData);
                }
                if (insertDeptDataList.Count > 0)
                    UDTTransfer.UDTCdselectInsert(insertDeptDataList);

                FISCA.Presentation.Controls.MsgBox.Show("儲存成功");
                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                this.Close();
            }
            catch (Exception ex)
            {
                FISCA.Presentation.Controls.MsgBox.Show("儲存失敗," + ex.Message);                
            }
            btnSave.Enabled = true;
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvwDept.Items)
                lvi.Checked = chkSelectAll.Checked;
        }
    }
}
