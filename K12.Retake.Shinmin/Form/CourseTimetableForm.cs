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
    public partial class CourseTimetableForm : FISCA.Presentation.Controls.BaseForm
    {
        Dictionary<string, List<string>> _CourseDeptDict = new Dictionary<string, List<string>>();

        public CourseTimetableForm()
        {
            InitializeComponent();
        }

        private void CourseTimetableForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
             _CourseDeptDict = GetDeptDict();

            dgData.Rows.Clear();
            foreach (UDTCourseTimetableDef data in UDTTransfer.UDTCourseTimetableSelectAll())
            {
                int rowIdx = dgData.Rows.Add();
                dgData.Rows[rowIdx].Tag = data;

                dgData.Rows[rowIdx].Cells[colName.Index].Value = data.Name;
                if (_CourseDeptDict.ContainsKey(data.UID))
                {
                    dgData.Rows[rowIdx].Cells[colDeptName.Index].Value = string.Join(",", _CourseDeptDict[data.UID].ToArray());
                }
            }
        }

        /// <summary>
        /// 建立課表與科別對照
        /// </summary>
        private Dictionary<string, List<string>> GetDeptDict()
        {
            Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
            List<UDTCdselectDef> dataList = UDTTransfer.UDTCdselectSelectAll();
            foreach (UDTCdselectDef data in dataList)
            {
                string key = data.RefCourseTimetableID.ToString();
                if (!dic.ContainsKey(key))
                {
                    dic.Add(key, new List<string>());
                }

                dic[key].Add(data.DeptName);
            }

            return dic;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgData.SelectedRows.Count == 1)
                {
                    UDTCourseTimetableDef data = dgData.SelectedCells[0].OwningRow.Tag as UDTCourseTimetableDef;
                    if (data != null)
                    {
                        // 刪除課表與科別
                        if (FISCA.Presentation.Controls.MsgBox.Show("請問是否刪除課表[ " + data.Name + " ],將會一併刪除課表內設定科別資料?", "刪除課表", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                        {
                            // 先刪除課表與科別對照
                            List<UDTCdselectDef> delDataList = UDTTransfer.UDTCdselectSelectByCTimeTableID(data.UID);
                            UDTTransfer.UDTCdselectDelete(delDataList);

                            // 刪除課表
                            List<UDTCourseTimetableDef> delCdataList = new List<UDTCourseTimetableDef>();
                            delCdataList.Add(data);
                            UDTTransfer.UDTCourseTimetableDelete(delCdataList);

                            LoadData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FISCA.Presentation.Controls.MsgBox.Show("處理過程發生錯誤," + ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgData.SelectedRows.Count == 1)
            {
                UDTCourseTimetableDef data = dgData.SelectedCells[0].OwningRow.Tag as UDTCourseTimetableDef;
                Form.SubAddEditCourseTimetableForm saect = new SubAddEditCourseTimetableForm(SubAddEditCourseTimetableForm.EditMode.Edit, data);
                if (saect.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                    LoadData();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form.SubAddEditCourseTimetableForm saect = new SubAddEditCourseTimetableForm(SubAddEditCourseTimetableForm.EditMode.Add, null);
            if (saect.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                LoadData();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
