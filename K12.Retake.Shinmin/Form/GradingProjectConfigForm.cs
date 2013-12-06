using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using K12.Retake.Shinmin.DAO;
using FISCA.Presentation.Controls;

namespace K12.Retake.Shinmin.Form
{
    public partial class GradingProjectConfigForm : BaseForm
    {
        BackgroundWorker _bgWorker;
        List<UDTWeightProportionDef> _WeightProportionList;
        List<string> _ItemList;
        const string _Item1="期中考比例";
        const string _Item2 = "期末考比例";
        const string _Item3 = "平時成績比例";
        public GradingProjectConfigForm()
        {
            InitializeComponent();
            _ItemList = new List<string>();
            _ItemList = new string[] {_Item1,_Item2,_Item3}.ToList();
            _WeightProportionList = new List<UDTWeightProportionDef>();
            _bgWorker = new BackgroundWorker();
            _bgWorker.DoWork += new DoWorkEventHandler(_bgWorker_DoWork);
            _bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorker_RunWorkerCompleted);
            _bgWorker.RunWorkerAsync();
        }

        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgData.Rows.Clear();

            if (_WeightProportionList.Count == 0)
            {
                this.Text = "重補修評量成績項目(尚未設定)";
                foreach (string str in _ItemList)
                {
                    int rowIdx = dgData.Rows.Add();
                    dgData.Rows[rowIdx].Cells[colItem.Index].Value = str;
                    dgData.Rows[rowIdx].Cells[colPer.Index].Value = "";
                }
            }
            else
            {
                UDTWeightProportionDef data = _WeightProportionList[0];
                foreach (string str in _ItemList)
                {
                    int value = 0;
                    if (str == _Item1)
                        value = data.SS1_Weight;

                    if (str == _Item2)
                        value = data.SS2_Weight;

                    if (str == _Item3)
                        value = data.SS3_Weight;

                    int rowIdx = dgData.Rows.Add();
                    dgData.Rows[rowIdx].Cells[colItem.Index].Value = str;
                    dgData.Rows[rowIdx].Cells[colPer.Index].Value = value;
                }
            }
            
        }

        void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _WeightProportionList = UDTTransfer.UDTWeightProportionSelect();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgData_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            CheckData();
        }

        /// <summary>
        /// 檢查值是否是數字
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            bool retVal = true;
            foreach (DataGridViewRow row in dgData.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.ColumnIndex == colPer.Index)
                    {
                        int x;
                        cell.ErrorText = "";

                        if (cell.Value != null)
                        {
                            if (!int.TryParse(cell.Value.ToString(), out x))
                            {
                                retVal = false;
                                cell.ErrorText = "必須是數字";
                            }
                        }
                        else
                        {
                            retVal = false;
                            cell.ErrorText = "不允許空白";
                        }
                    }
                }
            }
            return retVal;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            // 刪除資料
            UDTTransfer.UDTWeightProportionDelete(_WeightProportionList);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 檢查資料
            if (CheckData())
            {
                UDTWeightProportionDef newData = new UDTWeightProportionDef();

                // 刪除舊資料
                UDTTransfer.UDTWeightProportionDelete(_WeightProportionList);

                // 新增新資料
                foreach (DataGridViewRow row in dgData.Rows)
                {
                    int value = int.Parse(row.Cells[colPer.Index].Value.ToString());
                    string item=row.Cells[colItem.Index].Value.ToString();
                    switch (item)
                    {
                        case _Item1: newData.SS1_Weight = value; break;
                        case _Item2: newData.SS2_Weight = value;break;
                        case _Item3: newData.SS3_Weight = value; break;
                    }
                }

                List<UDTWeightProportionDef> newDataList = new List<UDTWeightProportionDef>();
                newDataList.Add(newData);
                UDTTransfer.UDTWeightProportionInsert(newDataList);

                // 關閉
                MsgBox.Show("儲存成功!");
                this.Close();
            }
            else
            {
                MsgBox.Show("資料錯誤請修正後儲存!");
            }
        }


    }
}
