using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using K12.Retake.Shinmin.DAO;
using System.IO;
using Aspose.Cells;
using Aspose.Words;
using Campus.Report;
using K12.Data;

namespace K12.Retake.Shinmin.Report
{
    /// <summary>
    /// 缺曠通知單報表
    /// </summary>
    public partial class StudentAttendanceReportForm : FISCA.Presentation.Controls.BaseForm
    {
        /// <summary>
        /// 載入資料用
        /// </summary>
        BackgroundWorker _bgLoadData;

        /// <summary>
        /// 產生資料用
        /// </summary>
        BackgroundWorker _bgPrintData;

        /// <summary>
        /// 所選課程編號
        /// </summary>
        List<string> _SelectCourseIDList = new List<string>();

        /// <summary>
        /// 所選課程
        /// </summary>
        List<UDTCourseDef> _SelectCourseList = new List<UDTCourseDef>();
        /// <summary>
        /// 所選上課時間
        /// </summary>
        List<UDTTimeSectionDef> _SelectTimeSectionList = new List<UDTTimeSectionDef>();

        /// <summary>
        /// 課程修課紀錄
        /// </summary>
        List<UDTScselectDef> _SelectSCList = new List<UDTScselectDef>();

        /// <summary>
        /// 所選缺曠
        /// </summary>
        List<UDTAttendanceDef> _SelectAttendanceList = new List<UDTAttendanceDef>();

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
        /// 教師姓名
        /// </summary>
        Dictionary<string, string> _TeacherNameDict = new Dictionary<string, string>();

        /// <summary>
        /// 班級名稱索引
        /// </summary>
        Dictionary<string, string> _ClassNameDict = new Dictionary<string, string>();
        DocumentBuilder _builder;

        /// <summary>
        /// Excel 用學生資料
        /// </summary>
        List<rpt_StudentInfo> _StudentInfoList = new List<rpt_StudentInfo>();

        DataTable _dtTable = new DataTable();
        
        /// <summary>
        /// 學生編號
        /// </summary>
        List<string> _StudentIDList = new List<string>();

        /// <summary>
        /// 使用者選的上課日期
        /// </summary>
        List<DateTime> _SelectDateTimeList = new List<DateTime>();

        private string _SelectMailName = "";
        private string _SelectMailAddress = "";

        bool _SelectExportStudInfo = false;

        private string _ReportName = "新民重補修缺曠通知單";
        /// <summary>
        /// 設定檔
        /// </summary>
        private ReportConfiguration _Config;

        /// <summary>
        /// Word 樣板
        /// </summary>
        private Document _WordTemplate;

        public StudentAttendanceReportForm(List<string> CourseIDList)
        {
            InitializeComponent();
            
            _SelectCourseIDList = CourseIDList;
            _bgLoadData = new BackgroundWorker();
            _bgLoadData.DoWork += new DoWorkEventHandler(_bgLoadData_DoWork);
            _bgLoadData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgLoadData_RunWorkerCompleted);
            _bgPrintData = new BackgroundWorker();
            _bgPrintData.DoWork += new DoWorkEventHandler(_bgPrintData_DoWork);
            _bgPrintData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgPrintData_RunWorkerCompleted);
            _bgPrintData.ProgressChanged += new ProgressChangedEventHandler(_bgPrintData_ProgressChanged);
            _bgPrintData.WorkerReportsProgress = true;

            _bgLoadData.RunWorkerAsync();
        }

        void _bgPrintData_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            FISCA.Presentation.MotherForm.SetStatusBarMessage("缺曠通知單產生中", e.ProgressPercentage);
        }

        void _bgPrintData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnPrint.Enabled = true;

            FISCA.Presentation.MotherForm.SetStatusBarMessage("缺曠通知單產生完成", 100);
            Document document = (Document)e.Result;
            string reportName = "缺曠通知單(重補修)";

            string path = Path.Combine(System.Windows.Forms.Application.StartupPath, "Reports");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path = Path.Combine(path, reportName + ".doc");

            if (File.Exists(path))
            {
                int i = 1;
                while (true)
                {
                    string newPath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + (i++) + Path.GetExtension(path);
                    if (!File.Exists(newPath))
                    {
                        path = newPath;
                        break;
                    }
                }
            }

            try
            {
                document.Save(path, Aspose.Words.SaveFormat.Doc);
                System.Diagnostics.Process.Start(path);


            }
            catch
            {
                System.Windows.Forms.SaveFileDialog sd = new System.Windows.Forms.SaveFileDialog();
                sd.Title = "另存新檔";
                sd.FileName = reportName + ".doc";
                sd.Filter = "Word檔案 (*.doc)|*.doc|所有檔案 (*.*)|*.*";
                if (sd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        document.Save(sd.FileName, Aspose.Words.SaveFormat.Doc);
                    }
                    catch
                    {
                        FISCA.Presentation.Controls.MsgBox.Show("指定路徑無法存取。", "建立檔案失敗", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            // 需要產生 Excel 學生名單
            if (chkExportStudInfo.Checked)
            { 
                // 班座排序
                _StudentInfoList = (from data in _StudentInfoList orderby data.ClassName, data.SeatNo select data).ToList();
                // 班級、座號、學號、學生姓名、收件人姓名、地址
                Workbook wb = new Workbook();
                wb.Open(new MemoryStream(Properties.Resources.缺曠通知單學生名單範本));
                int rowIdx = 1;

                foreach (rpt_StudentInfo data in _StudentInfoList)
                {
                    wb.Worksheets[0].Cells[rowIdx, 0].PutValue(data.ClassName);
                    if(data.SeatNo.HasValue)
                        wb.Worksheets[0].Cells[rowIdx, 1].PutValue(data.SeatNo.Value);

                    wb.Worksheets[0].Cells[rowIdx, 2].PutValue(data.StudentNumber);
                    wb.Worksheets[0].Cells[rowIdx, 3].PutValue(data.Name);
                    wb.Worksheets[0].Cells[rowIdx, 4].PutValue(data.MailName);
                    wb.Worksheets[0].Cells[rowIdx, 5].PutValue(data.MailAddress);

                    rowIdx++;
                }

                Utility.CompletedXls("f缺曠通知單學生清單(重補修)", wb);
            }
        }

        void _bgPrintData_DoWork(object sender, DoWorkEventArgs e)
        {
            _bgPrintData.ReportProgress(1);
            AddTableColumn();
            Document doc = new Document();
            doc.Sections.Clear();
            
            // 取得缺曠資料
            _SelectAttendanceList = UDTTransfer.UDTAttendanceSelectByCourseIDList(_SelectCourseIDList);
            // 取得課程修課
            _SelectSCList = UDTTransfer.UDTSCSelectByCourseIDList(_SelectCourseIDList);
            Dictionary<string, List<string>> _SCStudernDict = new Dictionary<string, List<string>>();
            foreach (UDTScselectDef data in _SelectSCList)
            { 
                string cid=data.CourseID.ToString();

                if (!_SCStudernDict.ContainsKey(cid))
                    _SCStudernDict.Add(cid, new List<string>());
                _SCStudernDict[cid].Add(data.StudentID.ToString());
            }

            _StudentIDList.Clear();
            _StudentDict.Clear();
            _AddressDict.Clear();
            _ParentDict.Clear();
            
            // 取得有缺曠學生id
            foreach (UDTAttendanceDef data in _SelectAttendanceList)
            {
                string sid = data.StudentID.ToString();
                if (!_StudentIDList.Contains(sid))
                    _StudentIDList.Add(sid);
            }

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

            // 教師姓名
            _TeacherNameDict.Clear();
            foreach (TeacherRecord rec in Teacher.SelectAll())
            {
                if (rec.Status == TeacherRecord.TeacherStatus.刪除)
                    continue;

                string name = rec.Name;
                if(!string.IsNullOrWhiteSpace(rec.Nickname))
                    name="("+rec.Nickname+")";
                
                _TeacherNameDict.Add(rec.ID, name);
            }

            // 班級名稱
            _ClassNameDict.Clear();
            foreach (ClassRecord cr in Class.SelectAll())
                _ClassNameDict.Add(cr.ID, cr.Name);

            string SchoolName = School.ChineseName;
            string SchoolAddress = School.Address;
            string SchoolTel = School.Telephone;

            _bgPrintData.ReportProgress(20);
            // 整理資料並過濾畫面上已修課學生
            // 學校名稱、學校地址、學校電話、收件人、收件地址、學年度、學期、梯次、班級、座號、學號、
            // 學生姓名、教師、課程名稱、缺曠統計、缺曠資料。
            int idx = 1;
            foreach (string StudID in _StudentIDList)
            {
                _dtTable.Clear();
                DataRow row = _dtTable.NewRow();
                
           // 處理 Word 資料合併
            Document document = new Document();
            document.Sections[0].PageSetup.PaperSize = PaperSize.A4;
            NodeCollection doctables = _WordTemplate.GetChildNodes(NodeType.Table, true);
            Node docdstNode = document.ImportNode(doctables[0], true, ImportFormatMode.KeepSourceFormatting);
            document.LastSection.Body.AppendChild(docdstNode);

            
                // 學校名稱
                row["學校名稱"] = SchoolName;
                // 學校地址
                row["學校地址"] = SchoolAddress;
                // 學校電話
                row["學校電話"] = SchoolTel;
                if (_StudentDict.ContainsKey(StudID))
                {
                    rpt_StudentInfo StudentInfoData = new rpt_StudentInfo();

                    // 收件人姓名
                    row["收件人姓名"] = _StudentDict[StudID].Name;
                    StudentInfoData.MailName = _StudentDict[StudID].Name;
                    if (_ParentDict.ContainsKey(StudID))
                    {
                        string name = _StudentDict[StudID].Name;
                        if (_SelectMailName == "監護人姓名")
                            name = _ParentDict[StudID].CustodianName;

                        if (_SelectMailName == "父親姓名")
                            name = _ParentDict[StudID].FatherName;

                        if (_SelectMailName == "母親姓名")
                            name = _ParentDict[StudID].MotherName;

                        row["收件人姓名"] = name;
                        StudentInfoData.MailName = name;
                    }

                    // 收件人地址
                    row["收件人地址"] = "";
                    if (_AddressDict.ContainsKey(StudID))
                    {
                        string address = "";
                        if (_SelectMailAddress == "戶籍地址")
                            address = _AddressDict[StudID].PermanentAddress;

                        if (_SelectMailAddress == "聯絡地址")
                            address = _AddressDict[StudID].MailingAddress;

                        if (_SelectMailAddress == "其他地址")
                            address = _AddressDict[StudID].Address1Address;

                        row["收件人地址"] = address;
                        StudentInfoData.MailAddress = address;
                    }

                    // 班級
                    if (_ClassNameDict.ContainsKey(_StudentDict[StudID].RefClassID))
                    {
                        row["班級"] = _ClassNameDict[_StudentDict[StudID].RefClassID];
                        StudentInfoData.ClassName = _ClassNameDict[_StudentDict[StudID].RefClassID];
                    }

                    row["座號"] = "";
                    // 座號
                    if (_StudentDict[StudID].SeatNo.HasValue)
                        row["座號"] = _StudentDict[StudID].SeatNo.Value;

                    StudentInfoData.SeatNo = _StudentDict[StudID].SeatNo.Value;

                    // 學號
                    row["學號"] = _StudentDict[StudID].StudentNumber;
                    StudentInfoData.StudentNumber = _StudentDict[StudID].StudentNumber;

                    // 學生姓名
                    row["學生姓名"] = _StudentDict[StudID].Name;
                    StudentInfoData.Name = _StudentDict[StudID].Name;

                    int sid = int.Parse(StudID);
                    // 分課程
                    Dictionary<int, List<UDTAttendanceDef>> coAttendDict = new Dictionary<int, List<UDTAttendanceDef>>();
                    foreach (UDTAttendanceDef atData in _SelectAttendanceList.Where(x => x.StudentID == sid))
                    {
                        if (!coAttendDict.ContainsKey(atData.CourseID))
                            coAttendDict.Add(atData.CourseID, new List<UDTAttendanceDef>());

                        coAttendDict[atData.CourseID].Add(atData);
                    }

                    // 處理課程細項
                    foreach (KeyValuePair<int, List<UDTAttendanceDef>> atData in coAttendDict)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("學年度");
                        dt.Columns.Add("學期");
                        dt.Columns.Add("梯次");
                        dt.Columns.Add("班級");
                        dt.Columns.Add("座號");
                        dt.Columns.Add("學號");
                        dt.Columns.Add("學生姓名");
                        dt.Columns.Add("教師");
                        dt.Columns.Add("課程名稱");
                        dt.Columns.Add("缺曠統計");
                        dt.Columns.Add("缺曠明細");
                        
                        string coid = atData.Key.ToString();

                        foreach (UDTCourseDef coData in _SelectCourseList.Where(x => x.UID == coid))
                        {
                            // 檢查有修課學生
                            if(_SCStudernDict.ContainsKey(coid))
                                if (_SCStudernDict[coid].Contains(StudID))
                                {


                                    DataRow dt_row = dt.NewRow();

                                    dt_row["學年度"] = coData.SchoolYear;
                                    dt_row["學期"] = coData.Semester;
                                    dt_row["梯次"] = coData.Month;
                                    // 班級
                                    if (_ClassNameDict.ContainsKey(_StudentDict[StudID].RefClassID))
                                        dt_row["班級"] = _ClassNameDict[_StudentDict[StudID].RefClassID];

                                    dt_row["座號"] = "";
                                    // 座號
                                    if (_StudentDict[StudID].SeatNo.HasValue)
                                        dt_row["座號"] = _StudentDict[StudID].SeatNo.Value;

                                    // 學號
                                    dt_row["學號"] = _StudentDict[StudID].StudentNumber;

                                    // 學生姓名
                                    dt_row["學生姓名"] = _StudentDict[StudID].Name;

                                    string tid = coData.RefTeacherID.ToString();
                                    // 教師
                                    if (_TeacherNameDict.ContainsKey(tid))
                                        dt_row["教師"] = _TeacherNameDict[tid];

                                    // 課程名稱
                                    dt_row["課程名稱"] = coData.CourseName;




                                    dt_row["缺曠統計"] = atData.Value.Count;

                                    // 存放上課時間與缺曠比對後
                                    Dictionary<DateTime, List<int>> atDict = new Dictionary<DateTime, List<int>>();

                                    foreach (UDTAttendanceDef dd in atData.Value)
                                    {
                                        foreach (UDTTimeSectionDef tt in _SelectTimeSectionList.Where(x => x.UID == dd.TimeSectionID.ToString()))
                                        {
                                            if (!atDict.ContainsKey(tt.Date))
                                                atDict.Add(tt.Date, new List<int>());

                                            atDict[tt.Date].Add(tt.Period);
                                        }
                                    }

                                    if (Global._TempStudentTimeSectionDict.ContainsKey(StudID))
                                        Global._TempStudentTimeSectionDict[StudID] = atDict;
                                    else
                                        Global._TempStudentTimeSectionDict.Add(StudID, atDict);

                                    dt_row["缺曠明細"] = StudID;
                                    // 缺曠統計
                                    dt.Rows.Add(dt_row);

                                    NodeCollection tables = _WordTemplate.GetChildNodes(NodeType.Table, true);
                                    Document subDoc = new Document();
                                    subDoc.Sections[0].PageSetup.PaperSize = PaperSize.A4;

                                    subDoc.MailMerge.MergeField += new Aspose.Words.Reporting.MergeFieldEventHandler(MailMerge_MergeField);
                                    Node dstNode = subDoc.ImportNode(tables[1], true, ImportFormatMode.KeepSourceFormatting);
                                    _builder = new DocumentBuilder(subDoc);
                                    subDoc.LastSection.Body.AppendChild(dstNode);
                                    subDoc.MailMerge.Execute(dt);
                                    subDoc.MailMerge.RemoveEmptyParagraphs = true;
                                    subDoc.MailMerge.DeleteFields();
                                    document.Sections.Add(document.ImportNode(subDoc.Sections[0], true));
                                }
                        }
                    }

                    _StudentInfoList.Add(StudentInfoData);
                    // 加入 DatatTablle
                    _dtTable.Rows.Add(row);
                }


                document.MailMerge.Execute(_dtTable);
                document.MailMerge.RemoveEmptyParagraphs = true;
                document.MailMerge.DeleteFields();

                // 處理 section break
                for (int i = document.Sections.Count - 2; i >= 0; i--)
                {
                    document.LastSection.PrependContent(document.Sections[i]);
                    document.Sections[i].Remove();
                }


                for (int i = 0; i < document.Sections.Count; i++)                                    
                    doc.Sections.Add(doc.ImportNode(document.Sections[i], true));
                
            }            

            //doc.MailMerge.MergeField += new Aspose.Words.Reporting.MergeFieldEventHandler(MailMerge_MergeField);
            //doc.MailMerge.Execute(_dtTable);
            //doc.MailMerge.RemoveEmptyParagraphs = true;
            //doc.MailMerge.DeleteFields();
            _bgPrintData.ReportProgress(95);
            e.Result = doc;            
        }

        void MailMerge_MergeField(object sender, Aspose.Words.Reporting.MergeFieldEventArgs e)
        {
            if (e.FieldName == "缺曠明細")
            {
                if (_builder.MoveToMergeField(e.FieldName))
                {
                    string sid = e.FieldValue.ToString();
                    Dictionary<DateTime, List<int>> data = null;

                    if (Global._TempStudentTimeSectionDict.ContainsKey(sid))
                        data = Global._TempStudentTimeSectionDict[sid];

                    if (data != null)
                    {
                        Aspose.Words.Cell cell = _builder.CurrentParagraph.ParentNode as Aspose.Words.Cell;
                        cell.CellFormat.LeftPadding = 0;
                        cell.CellFormat.RightPadding = 0;
                        double width = cell.CellFormat.Width - 60;
                        double perWidth = width / (double)8;
                        double li = _builder.RowFormat.LeftIndent;
                        Table table = _builder.StartTable();
                        

                        //_builder.RowFormat.HeightRule = HeightRule.Exactly;
                        //_builder.RowFormat.Height = 18.0;

                        _builder.RowFormat.LeftIndent = 0;
                        // 欄位名稱
                        Aspose.Words.Cell tc1 = _builder.InsertCell();
                        tc1.CellFormat.Borders.Left.LineWidth = 1.0;
                        tc1.CellFormat.Borders.Top.LineWidth = 1.0;
                        tc1.CellFormat.Borders.Right.LineWidth = 1.0;
                        tc1.CellFormat.Borders.Bottom.LineWidth = 1.0;
                        tc1.CellFormat.Width = 60;
                      
                        tc1.Paragraphs[0].ParagraphFormat.Alignment = ParagraphAlignment.Center;
                        _builder.Write("日期");
                        for (int i = 1; i <= 8; i++)
                        {
                            Aspose.Words.Cell cc = _builder.InsertCell();
                            cc.CellFormat.Borders.Left.LineWidth = 1.0;
                            cc.CellFormat.Borders.Top.LineWidth = 1.0;
                            cc.CellFormat.Borders.Right.LineWidth = 1.0;
                            cc.CellFormat.Borders.Bottom.LineWidth = 1.0;
                            cc.CellFormat.Width = perWidth;
                        
                            cc.Paragraphs[0].ParagraphFormat.Alignment = ParagraphAlignment.Center;
                            _builder.Write(i.ToString());
                        }
                        _builder.EndRow();

                        // 填資料並整理
                        foreach (KeyValuePair<DateTime, List<int>> d1 in data)
                        {

                            Aspose.Words.Cell dc1 = _builder.InsertCell();
                            dc1.CellFormat.Width = 60;
                            dc1.CellFormat.Borders.Left.LineWidth = 1.0;
                            dc1.CellFormat.Borders.Top.LineWidth = 1.0;
                            dc1.CellFormat.Borders.Right.LineWidth = 1.0;
                            dc1.CellFormat.Borders.Bottom.LineWidth = 1.0;
                            dc1.Paragraphs[0].ParagraphFormat.Alignment = ParagraphAlignment.Center;
                            _builder.Write(d1.Key.ToShortDateString());

                            for (int i = 1; i <= 8; i++)
                            {
                                Aspose.Words.Cell cc = _builder.InsertCell();
                                cc.CellFormat.Width = perWidth;
                                cc.CellFormat.Borders.Left.LineWidth = 1.0;
                                cc.CellFormat.Borders.Top.LineWidth = 1.0;
                                cc.CellFormat.Borders.Right.LineWidth = 1.0;
                                cc.CellFormat.Borders.Bottom.LineWidth = 1.0;
                                cc.Paragraphs[0].ParagraphFormat.Alignment = ParagraphAlignment.Center;
                                if(d1.Value.Contains(i))
                                    _builder.Write("缺課");
                                else
                                    _builder.Write("");
                            }
                            _builder.EndRow();
                        }

                        _builder.EndTable();

                        
                        // 清邊框
                        foreach (Aspose.Words.Cell c in table.FirstRow.Cells)
                            c.CellFormat.Borders.Top.LineStyle = LineStyle.None;

                        foreach (Aspose.Words.Cell c in table.LastRow.Cells)
                            c.CellFormat.Borders.Bottom.LineStyle = LineStyle.None;

                        foreach (Aspose.Words.Row r in table.Rows)
                        {
                            r.FirstCell.CellFormat.Borders.Left.LineStyle = LineStyle.None;
                            r.LastCell.CellFormat.Borders.Right.LineStyle = LineStyle.None;
                        }

                        _builder.RowFormat.LeftIndent = li;
                    }
                }
            }
        }

        /// <summary>
        /// 設定預設樣版
        /// </summary>
        private void SetDefaultTemplate()
        {
            if (_Config.Template == null)
            {
                ReportTemplate rptTmp = new ReportTemplate(Properties.Resources.缺曠通知單範本, TemplateType.Word);
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

            chkExportStudInfo.Checked = _Config.GetBoolean("匯出學生清單", false);             
        }

        void _bgLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnPrint.Enabled = true;
            // 填入收件人預設值
            cboMName.Items.AddRange(new string[] { "學生姓名","監護人姓名","父親姓名","母親姓名"}.ToArray());
            cboMAddress.Items.AddRange(new string[] { "戶籍地址","聯絡地址","其他地址" }.ToArray());
            // 讀取畫面設定預設值與設定畫面
            _Config = new ReportConfiguration(_ReportName);
            // 讀取並設定預設值
            SetDefaultTemplate();
            
            // 讀取樣板
            // 使用預設
            if (rbDefault.Checked)
                _WordTemplate = new Document(new MemoryStream(Properties.Resources.缺曠通知單範本));

            // 使用者自訂
            if (rbUserDef.Checked)
                _WordTemplate = _Config.Template.ToDocument();

            // 取讀上課日期
            lvwDate.Items.Clear();
            List<DateTime> dtDateList = new List<DateTime>();
            foreach (UDTTimeSectionDef data in _SelectTimeSectionList)
                if (!dtDateList.Contains(data.Date))
                    dtDateList.Add(data.Date);

            // 日期排序
            dtDateList.Sort();

            foreach (DateTime dt in dtDateList)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Tag = dt;
                lvi.Text = dt.ToShortDateString();
                lvwDate.Items.Add(lvi);
            }
        }

        void _bgLoadData_DoWork(object sender, DoWorkEventArgs e)
        {
            btnPrint.Enabled = false;
            // 取得所選課程資料
            _SelectCourseList = UDTTransfer.UDTCourseSelectUIDs(_SelectCourseIDList);
            // 取得所選課程上課時間
            _SelectTimeSectionList = UDTTransfer.UDTTimeSectionSelectByCourseIDList(_SelectCourseIDList);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (lvwDate.CheckedItems.Count == 0)
            {
                FISCA.Presentation.Controls.MsgBox.Show("請選擇上課日期");
                return;
            }

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

            // 取得所選上課時間
            foreach (ListViewItem lvi in lvwDate.CheckedItems)
            {
                DateTime dt = (DateTime)lvi.Tag;
                _SelectDateTimeList.Add(dt);
            }
            
            // 檢查列印所需資料
            _SelectMailName = cboMName.Text;
            _SelectMailAddress = cboMAddress.Text;
            _SelectExportStudInfo = chkExportStudInfo.Checked;
            
            _StudentInfoList.Clear();
            _dtTable.Clear();
            btnPrint.Enabled = false;

            // 記錄面上所選
            _Config.SetString("收件人", cboMName.Text);
            _Config.SetString("收件地址", cboMAddress.Text);
            if(rbDefault.Checked)
                _Config.SetString("範本", "預設");

            if(rbUserDef.Checked)
                _Config.SetString("範本", "自訂");

            _Config.SetBoolean("匯出學生清單", chkExportStudInfo.Checked);
            _Config.Save();
            Global._TempStudentTimeSectionDict.Clear();
            _bgPrintData.RunWorkerAsync();
        }

        private void AddTableColumn()
        {
            _dtTable.Columns.Clear();
            _dtTable.Columns.Add("學校名稱");
            _dtTable.Columns.Add("學校地址");
            _dtTable.Columns.Add("學校電話");
            _dtTable.Columns.Add("收件人姓名");
            _dtTable.Columns.Add("收件人地址");
            _dtTable.Columns.Add("學年度");
            _dtTable.Columns.Add("學期");
            _dtTable.Columns.Add("梯次");
            _dtTable.Columns.Add("班級");
            _dtTable.Columns.Add("座號");
            _dtTable.Columns.Add("學號");
            _dtTable.Columns.Add("學生姓名");
            _dtTable.Columns.Add("教師");
            _dtTable.Columns.Add("課程名稱");
            _dtTable.Columns.Add("缺曠統計");
            _dtTable.Columns.Add("缺曠明細");
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

        /// <summary>
        /// 下載預設樣板
        /// </summary>
        private void DownloadDefaultTemplate()
        {
            Document DefaultDoc = new Document(new MemoryStream(Properties.Resources.缺曠通知單範本));

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

        private void lnkDefault_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DownloadDefaultTemplate();
        }

        private void lnkUserDef_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DownloadUserDefTemplate();
        }

        private void chkDateAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDateAll.Checked)
                foreach (ListViewItem lvi in lvwDate.Items)
                    lvi.Checked = true;
            else
                foreach (ListViewItem lvi in lvwDate.Items)
                    lvi.Checked = false;
        }

        private void StudentAttendanceReportForm_Load(object sender, EventArgs e)
        {

        }
    }
}
