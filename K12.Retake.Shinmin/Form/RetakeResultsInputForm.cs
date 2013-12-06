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
using Aspose.Cells;

namespace K12.Retake.Shinmin.Form
{
    public partial class RetakeResultsInputForm : BaseForm
    {
        BackgroundWorker _bgLoadData;
        BackgroundWorker _bgSaveData;
        Campus.Windows.ChangeListener _ChangeListener;
        bool IsChangeNow = false;
        /// <summary>
        /// 評量比例
        /// </summary>
        UDTWeightProportionDef _wp=new UDTWeightProportionDef ();

        List<StudentResult> _RowDataList = new List<StudentResult>();

        List<string> _SelectCourseIDList;

        public RetakeResultsInputForm(List<string> CourseIDList)
        {
            if (RetakeAdmin.Instance.SelectedSource.Count == 0)
                return;

            InitializeComponent();
            _SelectCourseIDList = CourseIDList;
            _bgLoadData = new BackgroundWorker();
            _bgLoadData.DoWork += new DoWorkEventHandler(_bgLoadData_DoWork);
            _bgLoadData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgLoadData_RunWorkerCompleted);
            _bgSaveData = new BackgroundWorker();
            _bgSaveData.DoWork += new DoWorkEventHandler(_bgSaveData_DoWork);
            _bgSaveData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgSaveData_RunWorkerCompleted);
            _ChangeListener = new Campus.Windows.ChangeListener();
            _ChangeListener.StatusChanged += new EventHandler<Campus.Windows.ChangeEventArgs>(_ChangeListener_StatusChanged);
            _ChangeListener.Add(new Campus.Windows.DataGridViewSource(dgData));
            dgData.DataError += new DataGridViewDataErrorEventHandler(dgData_DataError);
            btnExport.Enabled = false;
            btnSave.Enabled = false;
            this.Text = "成績輸入(資料讀取中..)";

            _bgLoadData.RunWorkerAsync();
        }

        void _ChangeListener_StatusChanged(object sender, Campus.Windows.ChangeEventArgs e)
        {
            IsChangeNow = (e.Status == Campus.Windows.ValueStatus.Dirty);
        }

        void dgData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MsgBox.Show("輸入資料錯誤!");
            e.Cancel = false;
        }

        void _bgSaveData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnSave.Enabled = true;
            if (e.Cancelled)
            {
                MsgBox.Show("資料取得被中止");
            }
            else
            {
                if (e.Error == null)
                {
                    MsgBox.Show("資料儲存成功");
                    if (!_bgLoadData.IsBusy)
                    {
                        dgData.DataSource = null;
                        _bgLoadData.RunWorkerAsync();
                    }
                }
                else
                {
                    MsgBox.Show("發生錯誤：\n" + e.Error.Message);
                }
            }
        }

        void _bgSaveData_DoWork(object sender, DoWorkEventArgs e)
        {
            // 只更新有修改修課成績
            List<UDTScselectDef> updateData = new List<UDTScselectDef>();
            foreach (StudentResult sr in _RowDataList)
            {
                if (sr.HasChange)
                {
                    sr.ScselectRec.SubScore1 = sr.Score1;
                    sr.ScselectRec.SubScore2 = sr.Score2;
                    sr.ScselectRec.SubScore3 = sr.Score3;
                    sr.ScselectRec.Score = sr.ResultScore;
                    updateData.Add(sr.ScselectRec);
                }
            }
            // 更新修課資料，因成績分項放在修課紀錄
            if (updateData.Count > 0)
                UDTTransfer.UDTSCSelectUpdate(updateData);            
        }

        void _bgLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnExport.Enabled = true;
            btnSave.Enabled = true;
            this.Text = "成績輸入";

            if (e.Cancelled)
            {
                MsgBox.Show("資料取得被中止");
            }
            else
            {
                if (e.Error == null)
                {
                    dgData.AutoGenerateColumns = false;

                    dgData.DataSource = _RowDataList;
                    

                    // 畫面%處理
                    if (_wp != null)
                    {
                        dgData.Columns[ColScore1.Index].HeaderText ="期中考 (" + _wp.SS1_Weight + "%)";
                        dgData.Columns[ColScore2.Index].HeaderText ="期末考 (" + _wp.SS2_Weight + "%)";
                        dgData.Columns[ColScore3.Index].HeaderText ="平時成績 (" + _wp.SS3_Weight + "%)";

                        foreach (DataGridViewRow row in dgData.Rows)
                            SetRowResults(row);
                    }
                    else
                    {
                        MsgBox.Show("尚未設定評量比例\n將無法試算出總成績資料!");
                    }
                    _ChangeListener.Reset();
                    _ChangeListener.ResumeListen();
                    IsChangeNow = false;
                }
                else
                {
                    MsgBox.Show("發生錯誤:\n" + e.Error.Message);
                }
            }
        }

        void _bgLoadData_DoWork(object sender, DoWorkEventArgs e)
        {
            // 取的比重資料
            foreach (UDTWeightProportionDef data in UDTTransfer.UDTWeightProportionSelect())
                _wp = data;
            
            _RowDataList.Clear();
            _RowDataList =(from data in  UDTTransfer.GetStudentResultListByCourseIDList(_SelectCourseIDList) orderby data.CourseName,data.ScselectRec.SeatNo select data).ToList();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_bgSaveData.IsBusy)
                return;

            if (!CheckDataGridValue())
            {
                MsgBox.Show("請修正資料錯誤後儲存!");
                return;
            }

            btnSave.Enabled = false;
            _bgSaveData.RunWorkerAsync();
        }

        private bool CheckDataGridValue()
        {
            bool retVal = true;

            foreach (DataGridViewRow row in dgData.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.RowIndex < 0)
                        continue;
                    
                    if (cell.ColumnIndex < 6 || cell.ColumnIndex > 10)
                        continue;

                    if (!CheckCellValue(cell))
                        retVal = false;
                
                }
            }

            return retVal;
        }

        private bool CheckCellValue(DataGridViewCell cell)
        {
            if (string.IsNullOrEmpty("" + cell.Value))
                return true;

            decimal dd;
            if (!decimal.TryParse("" + cell.Value, out dd))            
            {
                cell.ErrorText = "必須是數字";
                cell.Style.BackColor = Color.Red;
                return false;
            }

            // 大於 100
            if (dd > 100)
            {
                cell.Style.BackColor = Color.Yellow;
                return true;
            }

            // 有小數點
            if (dd.ToString().Contains('.'))
            {
                cell.Style.BackColor = Color.Yellow;
                return true;
            }

            return true;
        }

        private void RetakeResultsInputForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsChangeNow)
            {
                if (MsgBox.Show("確認放棄?","尚未儲存資料",MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                    e.Cancel = true;
            }
        }

        private decimal? ParseValue(DataGridViewCell cell)
        {
            if (!string.IsNullOrEmpty("" + cell.Value))
            {
                decimal dc = 0;
                decimal.TryParse("" + cell.Value, out dc);
                return dc;
            }
            else
                return null;
        }

        private void SetRowResults(DataGridViewRow row)
        {
            // 取得是否扣考
            bool NotExam = false;
            bool.TryParse(row.Cells[colNotExam.Index].Value.ToString(), out NotExam);

            if (NotExam)
            {
                // 表示扣考
                foreach (DataGridViewCell cell in row.Cells)
                    cell.Style.BackColor = Color.LightBlue;
            }

            // 取得畫面上成績
            // 期中考
            decimal? score1 = ParseValue(row.Cells[ColScore1.Index]);
            
            // 期末考
            decimal? score2 = ParseValue(row.Cells[ColScore2.Index]);

            // 平時評量
            decimal? score3 = ParseValue(row.Cells[ColScore3.Index]);

            if (score1.HasValue)
                score1 = _wp.SS1_Weight * score1 / 100;

            if (score2.HasValue)
                score2 = _wp.SS2_Weight * score2 / 100;

            if (score3.HasValue)
                score3 = _wp.SS3_Weight * score3 / 100;


            decimal? results = 0;

            // 處理百分比
            if (score1.HasValue)
                results += score1.Value;

            if (score2.HasValue)
                results += score2.Value;

            if (score3.HasValue)
                results += score3.Value;

            // 計算後回傳到畫面
            if (score1.HasValue || score2.HasValue || score3.HasValue)
                row.Cells[colSResults.Index].Value = Math.Round(results.Value, MidpointRounding.AwayFromZero);
            else
                row.Cells[colSResults.Index].Value = "";
        }

        private void dgData_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgData.CurrentCell.RowIndex < 0)
                return;

            if (dgData.CurrentCell.ColumnIndex < 6 || dgData.CurrentCell.ColumnIndex > 10)
                return;

            dgData.CurrentCell.ErrorText = "";
            dgData.CurrentCell.Style.BackColor = Color.White;

            if (!CheckCellValue(dgData.CurrentCell))
                return;

            // 進行成績即時計算
            if (_wp != null)
                SetRowResults(dgData.CurrentRow);
        }

        private void dgData_SelectionChanged(object sender, EventArgs e)
        {
            dgData.BeginEdit(true);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            Workbook wb = new Workbook();
            // 標頭
            int colIdx=0;
            foreach (DataGridViewColumn col in dgData.Columns)
            {
                wb.Worksheets[0].Cells[0, colIdx].PutValue(col.HeaderText);
                colIdx++;
            }

            int rowIdx = 1;
            foreach (DataGridViewRow row in dgData.Rows)
            {
                int coIdx = 0;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if(cell.Value!=null)
                        wb.Worksheets[0].Cells[rowIdx, coIdx].PutValue(cell.Value.ToString());

                    coIdx++;
                }
                rowIdx++;
            }

            Utility.CompletedXls("重補修成績輸入匯出檔案", wb);
            btnExport.Enabled = true;
        }

        private void RetakeResultsInputForm_Load(object sender, EventArgs e)
        {

        }
    }
}
