using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Aspose.Words;
using K12.Retake.Shinmin.DAO;
using System.IO;
using K12.Data;
using Campus.Report;

namespace K12.Retake.Shinmin.Report
{
    /// <summary>
    /// 重補修缺課(含扣考)通知單
    /// </summary>
    public partial class StudentCourseAttendanceRptForm : FISCA.Presentation.Controls.BaseForm
    {
        List<string> _StudentIDList;
        BackgroundWorker _bgWork;
        private DocumentBuilder _builder;
        private string _SelectMailName = "";
        private string _SelectMailAddress = "";
        private bool _SelectChkNotExam = false;
        private string _SelectTimeList = "";
        private int _SelSchoolYear = 0, _SelSemester = 0, _SelMonth = 0;
        private string _ReportName = "新民學生重補修缺曠通知單";

        List<UDTTimeListDef> _TimeList = new List<UDTTimeListDef>();
        /// <summary>
        /// 設定檔
        /// </summary>
        private ReportConfiguration _Config;

        /// <summary>
        /// Word 樣板
        /// </summary>
        private Document _WordTemplate;
        // 存檔路徑
        string pathW = "";

        List<UDTCourseDef> _CourseList = new List<UDTCourseDef>();


        /// <summary>
        /// 學生資料
        /// </summary>
        Dictionary<string, StudentRecord> _StudentDict = new Dictionary<string, StudentRecord>();

        /// <summary>
        /// 地址資料
        /// </summary>
        Dictionary<string, AddressRecord> _AddressDict = new Dictionary<string, AddressRecord>();

        /// <summary>
        /// 父母親資料
        /// </summary>
        Dictionary<string, ParentRecord> _ParentDict = new Dictionary<string, ParentRecord>();

        /// <summary>
        /// 上課時間
        /// </summary>
        List<UDTTimeSectionDef> _TimeSectionList = new List<UDTTimeSectionDef>();

        /// <summary>
        /// 使用者所選上課時間
        /// </summary>
        List<DateTime> _SelectDateTimeList = new List<DateTime>();

        /// <summary>
        /// 課程缺曠資料
        /// </summary>
        List<UDTAttendanceDef> _AttendanceList = new List<UDTAttendanceDef>();

        List<string> _SelectCourseIDList = new List<string>();

        /// <summary>
        /// 所有課程索引
        /// </summary>
        Dictionary<int, UDTCourseDef> _CourseAllDict = new Dictionary<int, UDTCourseDef>();

        /// <summary>
        /// 班級名稱
        /// </summary>
        Dictionary<string, string> _classNameDict = new Dictionary<string, string>();

        public StudentCourseAttendanceRptForm(List<string> StudentIDList)
        {
            InitializeComponent();
            _StudentIDList = StudentIDList;
            _TimeList = UDTTransfer.UDTTimeListSelectAll();
            _bgWork = new BackgroundWorker();
            _bgWork.DoWork += new DoWorkEventHandler(_bgWork_DoWork);
            _bgWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWork_RunWorkerCompleted);
            _bgWork.ProgressChanged += new ProgressChangedEventHandler(_bgWork_ProgressChanged);
            _bgWork.WorkerReportsProgress = true;
        }

        void _bgWork_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            FISCA.Presentation.MotherForm.SetStatusBarMessage("重補修缺課(含扣考)通知單產生中..", e.ProgressPercentage);
        }

        void _bgWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnPrint.Enabled = true;
            FISCA.Presentation.MotherForm.SetStatusBarMessage("重補修缺課(含扣考)通知單產生完成");
            System.Diagnostics.Process.Start(pathW);
        }

        void _bgWork_DoWork(object sender, DoWorkEventArgs e)
        {
            #region 讀取資料並整理
            // 取得所選課程資料
            _CourseAllDict.Clear();
            _StudentDict.Clear();
            _AddressDict.Clear();
            _ParentDict.Clear();

            // 取得學生扣考
            Global._StudentNotExamDict = QueryData.GetStudentNotExamCoID(_StudentIDList);

            // 取得學生資料
            foreach (StudentRecord rec in Student.SelectByIDs(_StudentIDList))
                _StudentDict.Add(rec.ID, rec);

            // 地址資料
            foreach (AddressRecord rec in Address.SelectByStudentIDs(_StudentIDList))
            {
                if (!_AddressDict.ContainsKey(rec.RefStudentID))
                    _AddressDict.Add(rec.RefStudentID, rec);
            }

            // 家長資料
            foreach (ParentRecord rec in K12.Data.Parent.SelectByStudentIDs(_StudentIDList))
            {
                if (!_ParentDict.ContainsKey(rec.RefStudentID))
                    _ParentDict.Add(rec.RefStudentID, rec);
            }

            Dictionary<string, UDTCourseDef> SelCourseDict = UDTTransfer.UDTCourseSelectBySchoolYearSMDict(_SelSchoolYear, _SelSemester, _SelMonth);
            
            _CourseAllDict.Clear();
            foreach (KeyValuePair<string, UDTCourseDef> data in SelCourseDict)
            {
                int cid = int.Parse(data.Key);
                _CourseAllDict.Add(cid, data.Value);
            }
            
            // 取得所選課程上課時間表            
            _TimeSectionList = UDTTransfer.UDTTimeSectionSelectByCourseIDList(SelCourseDict.Keys.ToList());

            _classNameDict.Clear();
            foreach (ClassRecord cr in Class.SelectAll())
                _classNameDict.Add(cr.ID, cr.Name);

            // 取得學生重補修課程缺曠
            _AttendanceList.Clear();
            List<UDTAttendanceDef> StudAddtendList= UDTTransfer.UDTAttendanceSelectByStudentIDList(_StudentIDList);
            foreach (UDTAttendanceDef data in StudAddtendList)
            {
                // 屬於該梯次才加入
                if (_CourseAllDict.ContainsKey(data.CourseID))
                    _AttendanceList.Add(data);
            }

            // 整理學生缺課資料
            Dictionary<string, List<UDTAttendanceDef>> StudAttendanceDict = new Dictionary<string, List<UDTAttendanceDef>>();
            foreach (UDTAttendanceDef data in _AttendanceList)
            { 
                string sid=data.StudentID.ToString();
                if (!StudAttendanceDict.ContainsKey(sid))
                    StudAttendanceDict.Add(sid, new List<UDTAttendanceDef>());

                StudAttendanceDict[sid].Add(data);
            }


            _bgWork.ReportProgress(30);
         
            #endregion

            #region 填值並合併樣板處理

            Document docTemplate=null;
            if(_SelectChkNotExam)
                docTemplate = new Document(new MemoryStream(Properties.Resources.學生重補修缺曠通知單扣考範本));
            else
                docTemplate = new Document(new MemoryStream(Properties.Resources.學生重補修缺曠通知單範本));

            // 最終樣板
            Document doc = new Document();
            // 處理缺曠用
            DataTable dtAtt = new DataTable();
            List<Document> docList = new List<Document>();

            DataTable dt = new DataTable();
            
            List<int> courseIDList = new List<int>();

            string SchoolName = School.ChineseName;
            string SchoolAddress = School.Address;
            string SchoolTel = School.Telephone;

            // 以學生為主產生資料
            foreach(string studID in _StudentIDList)
            {
                // 沒有缺課學生跳過
                if (!StudAttendanceDict.ContainsKey(studID))
                    continue;

                // 檢查當勾選只產生扣考，只顯示有扣考，完全沒扣考跳過
                if (_SelectChkNotExam)
                {
                    if (!Global._StudentNotExamDict.ContainsKey(studID))
                        continue;
                }

                courseIDList.Clear();
                // 學生修課課程
                List<UDTAttendanceDef> sadList = StudAttendanceDict[studID];
                foreach (UDTAttendanceDef data in sadList)
                {
                    if (!courseIDList.Contains(data.CourseID))
                        courseIDList.Add(data.CourseID);
                }


                dt.Clear();
                dt.Columns.Clear();
                // 放入欄位
                dt.Columns.Add("學校名稱");
                dt.Columns.Add("學校地址");
                dt.Columns.Add("學校電話");
                dt.Columns.Add("收件人姓名");
                dt.Columns.Add("收件人地址");
                dt.Columns.Add("學年度");
                dt.Columns.Add("學期");
                dt.Columns.Add("梯次");
                dt.Columns.Add("班級");
                dt.Columns.Add("座號");
                dt.Columns.Add("學號");
                dt.Columns.Add("學生姓名");                

                // 課程名稱
                for (int i = 1; i <= 6; i++)
                {
                    dt.Columns.Add("課程名稱" + i);
                    dt.Columns.Add("缺曠紀錄" + i);
                    dt.Columns.Add("小計" + i);
                    dt.Columns.Add("扣考" + i);
                }

                DataRow dr = dt.NewRow();
                // 學校名稱
                dr["學校名稱"] = SchoolName;
                // 學校地址
                dr["學校地址"] = SchoolAddress;
                // 學校電話
                dr["學校電話"] = SchoolTel;

                // 收件人姓名
                dr["收件人姓名"] = _StudentDict[studID].Name;
                    
                if (_ParentDict.ContainsKey(studID))
                {
                    string name = _StudentDict[studID].Name;
                    if (_SelectMailName == "監護人姓名")
                        name = _ParentDict[studID].CustodianName;

                    if (_SelectMailName == "父親姓名")
                        name = _ParentDict[studID].FatherName;

                    if (_SelectMailName == "母親姓名")
                        name = _ParentDict[studID].MotherName;

                    dr["收件人姓名"] = name;
                        
                }

                // 收件人地址
                dr["收件人地址"] = "";
                if (_AddressDict.ContainsKey(studID))
                {
                    string address = "";
                    if (_SelectMailAddress == "戶籍地址")
                        address = _AddressDict[studID].PermanentAddress;

                    if (_SelectMailAddress == "聯絡地址")
                        address = _AddressDict[studID].MailingAddress;

                    if (_SelectMailAddress == "其他地址")
                        address = _AddressDict[studID].Address1Address;

                    dr["收件人地址"] = address;
                }

                // 班級
                if (_classNameDict.ContainsKey(_StudentDict[studID].RefClassID))
                {
                    dr["班級"] = _classNameDict[_StudentDict[studID].RefClassID];                    
                }

                dr["座號"] = "";
                // 座號
                if (_StudentDict[studID].SeatNo.HasValue)
                    dr["座號"] = _StudentDict[studID].SeatNo.Value;                

                // 學號
                dr["學號"] = _StudentDict[studID].StudentNumber;                

                // 學生姓名
                dr["學生姓名"] = _StudentDict[studID].Name;

                // 清空與重設相關需要資料
                Global._CourseTimeSectionList.Clear();
                Global._CousreAttendList.Clear();
                Global._CourseStudentAttendanceIdxDict.Clear();

                dtAtt.Clear();
                dtAtt.Columns.Clear();
                dtAtt.Columns.Add("缺曠日期");
                // 缺曠資料
                for (int i = 1; i <= 100; i++)
                    dtAtt.Columns.Add("缺曠紀錄" + i);

                DataRow drTT = dtAtt.NewRow();
                // 填入課程名稱
                int coIdx = 1;
                // 收集學年度學期
                List<int> syL = new List<int>();
                List<int> ssL = new List<int>();
                List<int> smL = new List<int>();


                foreach (int cid in courseIDList)
                {
                    if (_CourseAllDict.ContainsKey(cid))
                    {
                        // 勾選只產生扣考，沒有扣考不顯示出來
                        if (_SelectChkNotExam)
                        {
                            if (!Global._StudentNotExamDict.ContainsKey(studID))
                                continue;
                            else
                            {
                                if (!Global._StudentNotExamDict[studID].Contains(cid))
                                    continue;
                            }
                        }
                        dr["課程名稱" + coIdx] = _CourseAllDict[cid].CourseName;

                        if (!syL.Contains(_CourseAllDict[cid].SchoolYear))
                            syL.Add(_CourseAllDict[cid].SchoolYear);

                        if (!ssL.Contains(_CourseAllDict[cid].Semester))
                            ssL.Add(_CourseAllDict[cid].Semester);

                        if (!smL.Contains(_CourseAllDict[cid].Month))
                            smL.Add(_CourseAllDict[cid].Month);


                        int SumCount = 0;

                        // 整理課程上課時間表
                        foreach (UDTTimeSectionDef data in _TimeSectionList)
                        {
                            if (data.CourseID == cid)
                                Global._CourseTimeSectionList.Add(data);
                        }

                        // 整理課程修課缺曠記錄
                        foreach (UDTAttendanceDef data in StudAttendanceDict[studID])
                        {
                            if (data.CourseID == cid)
                            {
                                Global._CousreAttendList.Add(data);
                                SumCount++;
                            }
                        }

                        // 計算學生缺曠統計
                        dr["小計" + coIdx] = SumCount;

                        // 扣考
                        if (Global._StudentNotExamDict.ContainsKey(studID))
                        {
                            if (Global._StudentNotExamDict[studID].Contains(cid))
                                dr["扣考" + coIdx] = "扣考";
                        }

                        string ssid = cid.ToString();
                        if (!Global._CourseStudentAttendanceIdxDict.ContainsKey(ssid))
                            Global._CourseStudentAttendanceIdxDict.Add(ssid, coIdx);
                        drTT["缺曠紀錄" + coIdx] = ssid;   
                        coIdx++;
                    }
                }

                dr["學年度"] = string.Join(",", syL.ToArray());
                dr["學期"] = string.Join(",", ssL.ToArray());
                dr["梯次"] = string.Join(",", smL.ToArray());

                    dt.Rows.Add(dr);
                    dtAtt.Rows.Add(drTT);
                    // 處理動態處理(缺曠)
                    Document docAtt = new Document();
                    docAtt.Sections.Clear();
                    docAtt.Sections.Add(docAtt.ImportNode(docTemplate.Sections[0], true));
                    _builder = new DocumentBuilder(docAtt);
                    docAtt.MailMerge.MergeField += new Aspose.Words.Reporting.MergeFieldEventHandler(MailMerge_MergeField);
                    docAtt.MailMerge.Execute(dtAtt);
                    int cot = 1;
                    Document doc1 = new Document();
                    doc1.Sections.Clear();
                    doc1.Sections.Add(doc1.ImportNode(docAtt.Sections[0], true));
                    doc1.MailMerge.Execute(dt);
                    doc1.MailMerge.RemoveEmptyParagraphs = true;
                    doc1.MailMerge.DeleteFields();
                    //// 清除多餘欄位
                    //int TabRowCount = doc1.Sections[0].Body.Tables[0].Rows.Count - 1;
                    //for (int idx = TabRowCount; idx >= cot; idx--)
                    //{
                    //    doc1.Sections[0].Body.Tables[0].Rows.RemoveAt(idx);
                    //}
                    docList.Add(doc1);
            }

            doc.Sections.Clear();
            foreach (Document doc2 in docList)
                doc.Sections.Add(doc.ImportNode(doc2.Sections[0], true));

            string reportNameW = "重補修缺課通知單";

            if(_SelectChkNotExam)
                reportNameW = "重補修缺課扣考通知單";
            else
                reportNameW = "重補修缺課通知單";

            pathW = Path.Combine(System.Windows.Forms.Application.StartupPath + "\\Reports", "");
            if (!Directory.Exists(pathW))
                Directory.CreateDirectory(pathW);
            pathW = Path.Combine(pathW, reportNameW + ".doc");

            if (File.Exists(pathW))
            {
                int i = 1;
                while (true)
                {
                    string newPathW = Path.GetDirectoryName(pathW) + "\\" + Path.GetFileNameWithoutExtension(pathW) + (i++) + Path.GetExtension(pathW);
                    if (!File.Exists(newPathW))
                    {
                        pathW = newPathW;
                        break;
                    }
                }
            }

            try
            {
                doc.Save(pathW, Aspose.Words.SaveFormat.Doc);

            }
            catch (Exception exow)
            {

            }

            doc = null;
            docList.Clear();

            GC.Collect();
            _bgWork.ReportProgress(100);
            #endregion
        }

        void MailMerge_MergeField(object sender, Aspose.Words.Reporting.MergeFieldEventArgs e)
        {
            if (e.FieldValue != null)
            {
                string fName = "";
                string sid = e.FieldValue.ToString();
                if (Global._CourseStudentAttendanceIdxDict.ContainsKey(sid))
                    fName = "缺曠紀錄" + Global._CourseStudentAttendanceIdxDict[sid];
                if (e.FieldName == fName)
                {
                    if (_builder.MoveToMergeField(e.FieldName))
                    {
                        int courseid = int.Parse(sid);
                        List<DateTime> dataList = new List<DateTime>();

                        // 取得課程 TimeSection
                        List<UDTTimeSectionDef> CourseTimeSectionList=(from da in Global._CourseTimeSectionList where da.CourseID== courseid select da).ToList();

                        foreach (UDTTimeSectionDef data in CourseTimeSectionList)
                        {
                            if(data.CourseID==courseid)
                                dataList.Add(data.Date);
                        }
                        dataList.Sort();
                        List<string> colNameList = new List<string>();

                        foreach (DateTime data in dataList)
                        {
                            string dtName = data.Date.Month + "/" + data.Date.Day;
                            if (!colNameList.Contains(dtName))
                                colNameList.Add(dtName);
                        }
                        int colCount = colNameList.Count;

                        if (colCount > 0)
                        {
                            Cell cell = _builder.CurrentParagraph.ParentNode as Cell;
                            cell.CellFormat.LeftPadding = 0;
                            cell.CellFormat.RightPadding = 0;
                            double width = cell.CellFormat.Width;
                            int columnCount = colCount;
                            double miniUnitWitdh = width / (double)columnCount;

                            Table table = _builder.StartTable();

                            //(table.ParentNode.ParentNode as Row).RowFormat.LeftIndent = 0;
                            double p = _builder.RowFormat.LeftIndent;
                            _builder.RowFormat.LeftIndent = 0;
                            //_builder.RowFormat.HeightRule = HeightRule.Exactly;
                            //_builder.RowFormat.Height = 18.0;

                            Dictionary<string, int> dataDict = new Dictionary<string, int>();

                            // 缺曠日期
                            foreach (string name in colNameList)
                            {
                                dataDict.Add(name, 0);
                            }
                            int id = int.Parse(sid);
                            Dictionary<int, string> timeSecDateStrDict = new Dictionary<int, string>();
                            Dictionary<string, int> timeSecCountDict = new Dictionary<string, int>();
                            
                            // 課程timesection
                            foreach (UDTTimeSectionDef tt in CourseTimeSectionList)
                            {
                                
                                int ttid = int.Parse(tt.UID);
                                if (!timeSecDateStrDict.ContainsKey(ttid))
                                    timeSecDateStrDict.Add(ttid, tt.Date.Month + "/" + tt.Date.Day);
                            }

                            // 課程缺曠
                            foreach (UDTAttendanceDef data in Global._CousreAttendList.Where(x=>x.CourseID== courseid))
                            {
                             
                                    if (timeSecDateStrDict.ContainsKey(data.TimeSectionID))
                                    {
                                        string tKey = timeSecDateStrDict[data.TimeSectionID];
                                        if (!timeSecCountDict.ContainsKey(tKey))
                                            timeSecCountDict.Add(tKey, 0);

                                        timeSecCountDict[tKey]++;
                                    }                               
                            }

                             //缺曠統計
                            foreach (string name in colNameList)
                            {
                                Cell c1 = _builder.InsertCell();
                                c1.CellFormat.Width = miniUnitWitdh;
                                c1.Paragraphs[0].ParagraphFormat.Alignment = ParagraphAlignment.Center;
                                c1.CellFormat.WrapText = true;
                                if (timeSecCountDict.ContainsKey(name))
                                    _builder.Write(timeSecCountDict[name].ToString());
                                else
                                    _builder.Write("");
                            }
                            _builder.EndRow();
                            _builder.EndTable();

                            //去除表格四邊的線
                            foreach (Cell c in table.FirstRow.Cells)
                                c.CellFormat.Borders.Top.LineStyle = LineStyle.None;

                            foreach (Cell c in table.LastRow.Cells)
                                c.CellFormat.Borders.Bottom.LineStyle = LineStyle.None;

                            foreach (Row r in table.Rows)
                            {
                                r.FirstCell.CellFormat.Borders.Left.LineStyle = LineStyle.None;
                                r.LastCell.CellFormat.Borders.Right.LineStyle = LineStyle.None;
                            }

                            _builder.RowFormat.LeftIndent = p;
                        }
                    }
                }

                if (e.FieldName == "缺曠日期")
                {
                    if (_builder.MoveToMergeField(e.FieldName))
                    {
                        List<DateTime> dataList = new List<DateTime>();
                        foreach (UDTTimeSectionDef data in Global._CourseTimeSectionList)
                        {
                            dataList.Add(data.Date);
                        }
                        dataList.Sort();
                        List<string> colNameList = new List<string>();

                        foreach (DateTime data in dataList)
                        {
                            string dtName = data.Date.Month + "/" + data.Date.Day;
                            if (!colNameList.Contains(dtName))
                                colNameList.Add(dtName);
                        }
                        int colCount = colNameList.Count;

                        if (colCount > 0)
                        {
                            Cell cell = _builder.CurrentParagraph.ParentNode as Cell;
                            cell.CellFormat.LeftPadding = 0;
                            cell.CellFormat.RightPadding = 0;
                            double width = cell.CellFormat.Width;
                            int columnCount = colCount;
                            double miniUnitWitdh = width / (double)columnCount;

                            Table table = _builder.StartTable();

                            //(table.ParentNode.ParentNode as Row).RowFormat.LeftIndent = 0;
                            double p = _builder.RowFormat.LeftIndent;
                            _builder.RowFormat.LeftIndent = 0;
                            //_builder.RowFormat.HeightRule = HeightRule.Exactly;
                            //_builder.RowFormat.Height = 16.0;

                            // 缺曠日期
                            foreach (string name in colNameList)
                            {
                                Cell c1 = _builder.InsertCell();
                                c1.CellFormat.Width = miniUnitWitdh;
                                c1.Paragraphs[0].ParagraphFormat.Alignment = ParagraphAlignment.Center;
                                c1.CellFormat.WrapText = true;
                                _builder.Write(name);
                            }
                            _builder.EndRow();
                            _builder.EndTable();

                            //去除表格四邊的線
                            foreach (Cell c in table.FirstRow.Cells)
                                c.CellFormat.Borders.Top.LineStyle = LineStyle.None;

                            foreach (Cell c in table.LastRow.Cells)
                                c.CellFormat.Borders.Bottom.LineStyle = LineStyle.None;

                            foreach (Row r in table.Rows)
                            {
                                r.FirstCell.CellFormat.Borders.Left.LineStyle = LineStyle.None;
                                r.LastCell.CellFormat.Borders.Right.LineStyle = LineStyle.None;
                            }

                            _builder.RowFormat.LeftIndent = p;
                        }
                    }
                }
            }
        }

  
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(cboMName.Text))
            {
                FISCA.Presentation.Controls.MsgBox.Show("請選擇收件人");
                return;
            }

            if (string.IsNullOrEmpty(cboMAddress.Text))
            {
                FISCA.Presentation.Controls.MsgBox.Show("請選擇收件地址");
                return;
            }

            if (string.IsNullOrEmpty(cboTimeList.Text))
            {
                FISCA.Presentation.Controls.MsgBox.Show("請選擇梯次");
                return;
            }

            // 解析所選梯次
            _SelSchoolYear = _SelSemester = _SelMonth = 0;

            foreach (UDTTimeListDef data in _TimeList)
            {
                if (data.Name == cboTimeList.Text)
                {
                    _SelSchoolYear = data.SchoolYear;
                    _SelSemester = data.Semester;
                    _SelMonth = data.Month;
                    break;
                }            
            }

            if (_SelSchoolYear==0)
            {
                FISCA.Presentation.Controls.MsgBox.Show("解析梯次失敗無法產生資料!");
                return;
            }

            // 檢查列印所需資料
            _SelectMailName = cboMName.Text;
            _SelectMailAddress = cboMAddress.Text;
            _SelectTimeList = cboTimeList.Text;
            _SelectChkNotExam = chkNotExam.Checked;
            // 記錄面上所選
            _Config.SetString("收件人", cboMName.Text);
            _Config.SetString("收件地址", cboMAddress.Text);
            if (rbDefault.Checked)
                _Config.SetString("範本", "預設");

            if (rbUserDef.Checked)
                _Config.SetString("範本", "自訂");

            _Config.SetBoolean("只產生扣考", chkNotExam.Checked);
            _Config.SetString("梯次名稱", cboTimeList.Text);
            _Config.Save();
            btnPrint.Enabled = false;

            _bgWork.RunWorkerAsync();
        }

        private void lnkDefault_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DownloadDefaultTemplate();
        }

        /// <summary>
        /// 下載預設樣板
        /// </summary>
        private void DownloadDefaultTemplate()
        {
            Document DefaultDoc=null;

            if(_SelectChkNotExam)
                DefaultDoc = new Document(new MemoryStream(Properties.Resources.學生重補修缺曠通知單扣考範本));
            else
                DefaultDoc = new Document(new MemoryStream(Properties.Resources.學生重補修缺曠通知單範本));

            if (DefaultDoc == null)
            {
                FISCA.Presentation.Controls.MsgBox.Show("預設範本發生錯誤無法產生.");
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Word (*.doc)|*.doc";
            saveDialog.FileName = "缺曠通知單範本";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DefaultDoc.Save(saveDialog.FileName);
                }
                catch (Exception ex)
                {
                    FISCA.Presentation.Controls.MsgBox.Show("儲存失敗。" + ex.Message);
                    return;
                }

                try
                {
                    System.Diagnostics.Process.Start(saveDialog.FileName);
                }
                catch (Exception ex)
                {
                    FISCA.Presentation.Controls.MsgBox.Show("開啟失敗。" + ex.Message);
                    return;
                }
            }
        }

        private void lnkUserDef_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DownloadUserDefTemplate();
        }

        /// <summary>
        /// 下載使用者自訂範本
        /// </summary>
        private void DownloadUserDefTemplate()
        {
            if (_Config.Template == null)
            {
                FISCA.Presentation.Controls.MsgBox.Show("目前沒有任何範本");
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Word (*.doc)|*.doc";
            saveDialog.FileName = "缺曠通知單自訂範本";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _Config.Template.ToDocument().Save(saveDialog.FileName);
                }
                catch (Exception ex)
                {
                    FISCA.Presentation.Controls.MsgBox.Show("儲存失敗。" + ex.Message);
                    return;
                }

                try
                {
                    System.Diagnostics.Process.Start(saveDialog.FileName);
                }
                catch (Exception ex)
                {
                    FISCA.Presentation.Controls.MsgBox.Show("開啟失敗。" + ex.Message);
                    return;
                }
            }
        }

        private void lnkUpload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //if (rbUserDef.Checked)
                UploadUserDefTemplate();
        }

        /// <summary>
        /// 上傳使用者自定範本
        /// </summary>
        private void UploadUserDefTemplate()
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Word (*.doc)|*.doc";
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(openDialog.FileName);
                    TemplateType type = TemplateType.Word;
                    ReportTemplate template = new ReportTemplate(fileInfo, type);
                    _Config.Template = template;
                    _Config.Save();
                    FISCA.Presentation.Controls.MsgBox.Show("上傳範本成功");
                }
                catch (Exception ex)
                {
                    FISCA.Presentation.Controls.MsgBox.Show("上傳範本失敗" + ex.Message);
                }
            }
        }

        private void StudentCourseAttendanceRptForm_Load(object sender, EventArgs e)
        {

            this.MaximumSize = this.MinimumSize = this.Size;

            string tiName = "";
            // 讀取所有梯次
            List<string> tName = new List<string>();

            foreach (UDTTimeListDef da in (from data in _TimeList orderby data.SchoolYear, data.Semester, data.Month, data.Name select data))
            {
                tName.Add(da.Name);
                if (da.Active)
                    tiName = da.Name;
            }

            if(tiName!="")
                this.Text=this.Text+" (目前梯次："+tiName+")";

            cboTimeList.Items.AddRange(tName.ToArray());            

            // 填入收件人預設值
            cboMName.Items.AddRange(new string[] { "學生姓名", "監護人姓名", "父親姓名", "母親姓名" }.ToArray());
            cboMAddress.Items.AddRange(new string[] { "戶籍地址", "聯絡地址", "其他地址" }.ToArray());
            // 讀取畫面設定預設值與設定畫面
            _Config = new ReportConfiguration(_ReportName);
            // 讀取並設定預設值
            SetDefaultTemplate();

            // 讀取樣板
            // 使用預設
            if (rbDefault.Checked)
            {
                if(_SelectChkNotExam)
                    _WordTemplate = new Document(new MemoryStream(Properties.Resources.學生重補修缺曠通知單扣考範本));
                else
                    _WordTemplate = new Document(new MemoryStream(Properties.Resources.學生重補修缺曠通知單範本));
            }

            // 使用者自訂
            if (rbUserDef.Checked)
                _WordTemplate = _Config.Template.ToDocument();
        }

        /// <summary>
        /// 設定預設樣版
        /// </summary>
        private void SetDefaultTemplate()
        {
            if (_Config.Template == null)
            {
                ReportTemplate rptTmp = new ReportTemplate(Properties.Resources.學生重補修缺曠通知單範本, TemplateType.Word);
                _Config.Template = rptTmp;
                _Config.Save();
            }

            // 讀取設定
            cboMName.Text = _Config.GetString("收件人", "");
            cboMAddress.Text = _Config.GetString("收件地址", "");
            string strTemp = _Config.GetString("範本", "預設");
            if (strTemp == "預設")
                rbDefault.Checked = true;
            else
                rbUserDef.Checked = true;

            chkNotExam.Checked = _Config.GetBoolean("只產生扣考", false);
            cboTimeList.Text = _Config.GetString("梯次名稱", "");
        }
    }
}
