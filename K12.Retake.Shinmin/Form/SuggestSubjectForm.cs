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
    public partial class SuggestSubjectForm : FISCA.Presentation.Controls.BaseForm
    {
        List<SuggestSubjectCount> _SuggestSubjectCountList = new List<SuggestSubjectCount>();
        BackgroundWorker _bgWorker = new BackgroundWorker();
        SubjectListForm _ss;
        
        public SuggestSubjectForm(SubjectListForm ss)
        {
            InitializeComponent();
            _ss = ss;
            _bgWorker.DoWork += new DoWorkEventHandler(_bgWorker_DoWork);
            _bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorker_RunWorkerCompleted);
            _bgWorker.RunWorkerAsync();
        }

        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (cbxDeptName.Items.Count == 0)
            {
                List<string> dpNameList = (from da in _SuggestSubjectCountList orderby da.DeptName select da.DeptName).Distinct().ToList();
                cbxDeptName.Items.Add("");
                foreach (string name in dpNameList)
                    if(!cbxDeptName.Items.Contains(name))
                        cbxDeptName.Items.Add(name);
            }
            LoadDataToGridView();         
        }

        private void LoadDataToGridView()
        {
            dgData.Rows.Clear();
            List<SuggestSubjectCount> dataList = new List<SuggestSubjectCount>();
            if (string.IsNullOrWhiteSpace(cbxDeptName.Text))
                dataList = _SuggestSubjectCountList;
            else
                dataList = _SuggestSubjectCountList.Where(x => x.DeptName == cbxDeptName.Text).ToList();

            foreach (SuggestSubjectCount sss in dataList)
            {
                int rowIdx = dgData.Rows.Add();
                dgData.Rows[rowIdx].Cells[colSubjectName.Index].Value = sss.SubjectName;
                if (sss.Level.HasValue)
                    dgData.Rows[rowIdx].Cells[colSubjectLevel.Index].Value = sss.Level.Value;
                dgData.Rows[rowIdx].Cells[colCredit.Index].Value = sss.Credit;
                dgData.Rows[rowIdx].Cells[colStudCount.Index].Value = sss.Count;
                dgData.Rows[rowIdx].Cells[colDept.Index].Value = sss.DeptName;
                dgData.Rows[rowIdx].Tag = sss;
            }
            lblMsg.Text = "科目總數： " + dgData.Rows.Count;
        }

        void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _SuggestSubjectCountList = (from data in QueryData.GetSuggestSubjectCountList() orderby data.DeptName,data.SubjectName,data.Level select data ).ToList();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SuggestSubjectForm_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.MinimumSize = this.Size;
            lblMsg.Text = "資料讀取中..";
        }

        private void dgData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 點 link
            if (e.ColumnIndex == colLnk.Index)
            { 
                // 取得前所在 Row Tag
                SuggestSubjectCount ssc = dgData.Rows[e.RowIndex].Tag as SuggestSubjectCount;
                _ss.QuickAdd(ssc);    
            
            }
        }

        private void lblMsg_Click(object sender, EventArgs e)
        {

        }

        private void cbxDeptName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataToGridView();
        }
    }
}
