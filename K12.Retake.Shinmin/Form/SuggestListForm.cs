using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using K12.Data;
using Aspose.Cells;
using K12.Retake.Shinmin.DAO;
using System.Xml.Linq;
using DevComponents.DotNetBar;

namespace K12.Retake.Shinmin.Form
{
    /// <summary>
    /// 重補修建議名單
    /// </summary>
    public partial class SuggestListForm : FISCA.Presentation.Controls.BaseForm
    {
        BackgroundWorker _bgWorker;
        bool _bgBusy = false;
        /// <summary>
        /// 存放查詢資料
        /// </summary> 
        DataTable _dtTable;

        /// <summary>
        /// 名冊
        /// </summary>
        List<UDTTimeListDef> _UDTTimeListList = new List<UDTTimeListDef>();

        /// <summary>
        /// UDT 內有符合期間的建議重修名單
        /// </summary>
        List<UDTSuggestListDef> _CurrentHasSuggestList = new List<UDTSuggestListDef>();
        UDTTimeListDef _currentTimeList = new UDTTimeListDef();
        public SuggestListForm()
        {
            InitializeComponent();
            _dtTable = new DataTable();
            // 不直接使用 Data Table 來的欄位
            dgData.AutoGenerateColumns = false;

            _bgWorker = new BackgroundWorker();
            _bgWorker.DoWork += new DoWorkEventHandler(_bgWorker_DoWork);
            _bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorker_RunWorkerCompleted);
        }

        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_bgBusy)
            {
                _bgBusy = false;
                _bgWorker.RunWorkerAsync();
                return;
            }
                dgData.DataSource = _dtTable;
                lblMsg.Text = _currentTimeList.Name+" (共有" + dgData.Rows.Count + "筆)";
        }


        void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _dtTable.Clear();
            _dtTable = QueryData.GetRetakeListByTimeListUID(_currentTimeList.UID);
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     
       

        private void SuggestListForm_Load(object sender, EventArgs e)
        {
            int defSchoolYear = int.Parse(K12.Data.School.DefaultSchoolYear);
            iptSelectSchoolYear.Value = defSchoolYear;
            lblMsg.Text = "(尚未選擇名單)";
            LoadPanelData(defSchoolYear,true);
        }

        /// <summary>
        /// 取得 panel名冊內容
        /// </summary>
        private void LoadPanelData(int SchoolYear,bool clearRows)
        {
            this.Text = "建議重補修名單";
            itmPnlTimeName.SuspendLayout();
            itmPnlTimeName.Items.Clear();
            
            if (clearRows)
            {
                dgData.DataSource = null;
                dgData.Rows.Clear();
            }
            _UDTTimeListList = UDTTransfer.UDTTimeListSelectAll();
            // 排序
            //_UDTTimeListList = (from data in _UDTTimeListList where data.SchoolYear==SchoolYear orderby data.Active descending,data.SchoolYear, data.Semester, data.Month select data).ToList();
            _UDTTimeListList = (from data in _UDTTimeListList where data.SchoolYear == SchoolYear orderby  data.SchoolYear, data.Semester, data.Month select data).ToList();
            foreach (UDTTimeListDef data in _UDTTimeListList)
            {
                ButtonItem btnItem = new ButtonItem();
                btnItem.Name = data.UID;
                if (data.Active)
                {
                    btnItem.Text = data.Name;
                    btnItem.FontBold = true;
                    btnItem.ForeColor = Color.Red;
                }
                else
                    btnItem.Text = data.Name;
                btnItem.Tag = data;
                btnItem.OptionGroup = "itmPnlTimeName";
                btnItem.ButtonStyle = eButtonStyle.ImageAndText;
                btnItem.Click += new EventHandler(btnItem_Click);
                itmPnlTimeName.Items.Add(btnItem);
            }
            itmPnlTimeName.ResumeLayout();
            itmPnlTimeName.Refresh();
        }

        void btnItem_Click(object sender, EventArgs e)
        {
            if (itmPnlTimeName.SelectedItems.Count == 1)
            {
                dgData.DataSource = null;
                dgData.Rows.Clear();
                ButtonItem item = itmPnlTimeName.SelectedItems[0] as ButtonItem;

                UDTTimeListDef data = item.Tag as UDTTimeListDef;
                lblMsg.Text = "資料讀取中..";

                // 讀取元資料庫內資料
                if (data != null)
                {
                    _currentTimeList = data;
                    if (_bgWorker.IsBusy)
                    {
                        _bgBusy = true;
                    }
                    else
                        _bgWorker.RunWorkerAsync();                    
               
                }
                else
                    lblMsg.Text = "";
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (_dtTable.Rows.Count == 0)
            {
                FISCA.Presentation.Controls.MsgBox.Show("沒有資料");
                return;
            }
            btnExportExcel.Enabled = false;
            Workbook wb = new Workbook();
            int MaxRow = 65000;
            
            if (_dtTable.Rows.Count > MaxRow)
            {
                wb.Worksheets.Add();
                wb.Worksheets.Add();
                DataTable dts1 = new DataTable();
                DataTable dts2 = new DataTable();
                DataTable dts3 = new DataTable();
                List<string> ColumnsList = new List<string>();

                foreach (DataColumn dc in _dtTable.Columns)
                {
                    ColumnsList.Add(dc.Caption);
                    dts1.Columns.Add(dc.Caption);
                    dts2.Columns.Add(dc.Caption);
                    dts3.Columns.Add(dc.Caption);
                }

                int rowIdx = 0;
                int secRow = MaxRow * 2;
                foreach (DataRow dr in _dtTable.Rows)
                {
                    if (rowIdx <= MaxRow)
                    {
                        DataRow dr1 = dts1.NewRow();
                        foreach (string str in ColumnsList)
                        {
                            dr1[str] = dr[str];                            
                        }
                        dts1.Rows.Add(dr1);
                       
                    }
                    if (rowIdx > MaxRow && rowIdx <= secRow)
                    {
                        DataRow dr2 = dts2.NewRow();
                        foreach (string str in ColumnsList)
                        {
                            dr2[str] = dr[str];
                        }
                        dts2.Rows.Add(dr2);
                    }
                    if (rowIdx > secRow)
                    {
                        DataRow dr3 = dts3.NewRow();
                        foreach (string str in ColumnsList)
                        {
                            dr3[str] = dr[str];
                        }
                        dts3.Rows.Add(dr3);
                    }
                    rowIdx++;
                }

                wb.Worksheets[0].Cells.ImportDataTable(dts1, true, "A1");
                wb.Worksheets[1].Cells.ImportDataTable(dts2, true, "A1");
                wb.Worksheets[2].Cells.ImportDataTable(dts3, true, "A1");
            }
            else
            {
                wb.Worksheets[0].Cells.ImportDataTable(_dtTable, true, "A1");
            }

            Utility.CompletedXls("重補修建議名單", wb);
            btnExportExcel.Enabled = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;
            Form.AddTimeListForm atlf = new AddTimeListForm();
            atlf.ShowDialog();
            LoadPanelData(iptSelectSchoolYear.Value,true);
            btnAdd.Enabled = true;
        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
            if (itmPnlTimeName.SelectedItems.Count == 1)
            {
                ButtonItem item = itmPnlTimeName.SelectedItems[0] as ButtonItem;
                UDTTimeListDef data = item.Tag as UDTTimeListDef;
                UDTTransfer.UDTTimeListSetActiveTrue(data.UID);
                LoadPanelData(iptSelectSchoolYear.Value,false);
                FISCA.Presentation.Controls.MsgBox.Show("將工作名單設定為： " + data.Name);
            }
            else
                FISCA.Presentation.Controls.MsgBox.Show("請選擇工作名單");
            
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (itmPnlTimeName.SelectedItems.Count > 0)
            {
                btnDel.Enabled = false;
                ButtonItem item = itmPnlTimeName.SelectedItems[0] as ButtonItem;
                if (item != null)
                {
                    // 確認刪除資料
                    if (FISCA.Presentation.Controls.MsgBox.Show("請問是否刪除["+item.Text+"]?","刪除期間名冊", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                    { 
                        // 刪除建議名單
                        List<UDTSuggestListDef> delSuggestData = UDTTransfer.UDTSuggestListSelectByTimeListID(item.Name);
                        UDTTransfer.UDTSuggestListDelete(delSuggestData);

                        // 刪除名冊內容
                        List<UDTTimeListDef> delList = new List<UDTTimeListDef>();
                        UDTTimeListDef delData = item.Tag as UDTTimeListDef;
                        delList.Add(delData);
                        UDTTransfer.UDTTimeListDelete(delList);

                        // 資料重整
                        LoadPanelData(iptSelectSchoolYear.Value,true);
                    }
                
                }            
            }
            btnDel.Enabled = true;
        }

        private void dgData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void iptSelectSchoolYear_ValueChanged(object sender, EventArgs e)
        {
            lblMsg.Text = "(尚未選擇名單)";
            LoadPanelData(iptSelectSchoolYear.Value,true);
        }

    }
}
