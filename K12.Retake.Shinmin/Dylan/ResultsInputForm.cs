using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using FISCA.UDT;
using FISCA.Data;
using K12.Data;
using K12.Retake.Shinmin.DAO;
namespace K12.Retake.Shinmin.Form
{
    public partial class ResultsInputForm : BaseForm
    {
        BackgroundWorker _bgWorker;
        // 成績輸入時間
        List<UDTScoreInputDateDef> _ScoreInputDateList = new List<UDTScoreInputDateDef>();
        // 項目名稱
        List<string> _ItemsList ;
        // 日期格式
        private const string DateTimeFormat = "yyyy/MM/dd HH:mm";
        public ResultsInputForm()
        {
            InitializeComponent();
            _ItemsList = new List<string>();
            _ItemsList = new string[] {"期中考","期末考","平時成績" }.ToList();
            _bgWorker = new BackgroundWorker();
            _bgWorker.DoWork += new DoWorkEventHandler(_bgWorker_DoWork);
            _bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorker_RunWorkerCompleted);
        }

        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnSave.Enabled = true;
            // 將資料放入
            dgvTimes.Rows.Clear();

            foreach (string str in _ItemsList)
            {
                int rowIdx = dgvTimes.Rows.Add();
                // 名稱
                dgvTimes.Rows[rowIdx].Cells[colName.Index].Value = str;

                foreach (UDTScoreInputDateDef data in _ScoreInputDateList.Where(x=>x.Name==str))
                {
                    if (data.StartDate.HasValue)
                        dgvTimes.Rows[rowIdx].Cells[colStartTime.Index].Value = data.StartDate.Value.ToString(DateTimeFormat);
                    else
                        dgvTimes.Rows[rowIdx].Cells[colStartTime.Index].Value = "";

                    if (data.EndDate.HasValue)
                        dgvTimes.Rows[rowIdx].Cells[colEndTime.Index].Value = data.EndDate.Value.ToString(DateTimeFormat);
                    else
                        dgvTimes.Rows[rowIdx].Cells[colEndTime.Index].Value = "";

                    break;
                }            
            }
        }

        void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // 取得成績輸入時間
            _ScoreInputDateList = UDTTransfer.UDTScoreInputDateSelect();
        }

        private void ResultsInputDateTime_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            _bgWorker.RunWorkerAsync();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool allCellNull = true;

            foreach (DataGridViewRow row in dgvTimes.Rows)
                foreach (DataGridViewCell cell in row.Cells)
                    if(cell.ColumnIndex !=colName.Index)
                    if (cell.Value !=null)
                    {
                        allCellNull = false;
                        break;
                    }

            if (allCellNull)
            {
                MsgBox.Show("畫面中沒有輸入時間。");
                return;
            }

            // 檢查輸入資料
            if (IsDataValidity())
            {
                try
                {
                    List<UDTScoreInputDateDef> insertData = new List<UDTScoreInputDateDef>();
                    foreach (DataGridViewRow drv in dgvTimes.Rows)
                    {
                        UDTScoreInputDateDef data = new UDTScoreInputDateDef();
                        data.Name = drv.Cells[colName.Index].Value.ToString();
                        DateTime dts, dte;
                        if (DateTime.TryParse(drv.Cells[colStartTime.Index].Value.ToString(), out dts))
                            data.StartDate = dts;
                        if (DateTime.TryParse(drv.Cells[colEndTime.Index].Value.ToString(), out dte))
                            data.EndDate = dte;
                        insertData.Add(data);
                    }

                    // 刪除舊資料
                    if(_ScoreInputDateList.Count>0)
                        UDTTransfer.UDTScoreInputDateDelete(_ScoreInputDateList);
                    // 新增資料
                    UDTTransfer.UDTScoreInputDateInsert(insertData);
                    MsgBox.Show("儲存成功!");
                }
                catch (Exception ex)
                {
                    MsgBox.Show("儲存失敗! \n" + ex.Message);

                }
                this.Close();
            }
            else
            {
                MsgBox.Show("畫面中含有不正確資料。");
                return;
            }

          
        }

        private void dgvTimes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            ValidateCellData(e.ColumnIndex, e.RowIndex, dgvTimes.Rows[e.RowIndex].Cells[e.ColumnIndex].Value + "");
        }

        private void ValidateCellData(int ColumnIndex, int RowIndex, string value)
        {
            if (ColumnIndex == colStartTime.Index || ColumnIndex == colEndTime.Index)
            {
                DataGridViewCell cell = dgvTimes.Rows[RowIndex].Cells[ColumnIndex];
                cell.ErrorText = "";

                if (!string.IsNullOrEmpty(value))
                { 
                    DateTime dt;
                    if (DateTime.TryParse(value, out dt))
                    {
                        cell.Value = dt.ToString(DateTimeFormat);
                    }
                    else
                    {
                        cell.ErrorText = "日期格式錯誤。";
                    }
                }
            }
        }

       // 檢查開始日期與結束日期
        private void dgvTimes_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridViewRow row = dgvTimes.Rows[e.RowIndex];
            string startTime = row.Cells[colStartTime.Index].Value + "";
            string endTime = row.Cells[colEndTime.Index].Value + "";
            row.ErrorText = "";
            if (string.IsNullOrEmpty(startTime) && string.IsNullOrEmpty(endTime))
            { }
            else if (!string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
            {
                DateTime dtS;
                
                DateTime dtE;
                if (DateTime.TryParse(startTime, out dtS) && DateTime.TryParse(endTime, out dtE))
                {
                    if (dtS >= dtE)
                        row.ErrorText = "結束時間必須在開始時間之後。";
                }
            }
            else
                row.ErrorText = "請輸入正確的時間限制資料(必須同時有資料或沒有資料)。";
        }

        private bool IsDataValidity()
        { 
            bool valid=true;
            foreach (DataGridViewRow row in dgvTimes.Rows)
            {
                if (!string.IsNullOrEmpty(row.ErrorText))
                    valid = false;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (!string.IsNullOrEmpty(cell.ErrorText))
                        valid = false;
                }
                if (!valid) break;
            }
            return valid;
        }
     
    }

}
