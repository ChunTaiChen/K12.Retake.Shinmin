using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using K12.Retake.Shinmin.DAO;
using K12.Data;
using System.Data;
using Aspose.Words;
using System.IO;

namespace K12.Retake.Shinmin.Report
{
    /// <summary>
    /// 課程缺曠統計表
    /// </summary>
    public class CourseAttendanceRpt
    {
        BackgroundWorker _bgWork;
        private DocumentBuilder _builder;

        // 存檔路徑
        string pathW = "";

        List<UDTCourseDef> _CourseList = new List<UDTCourseDef>();
        /// <summary>
        /// 修課
        /// </summary>
        List<UDTScselectDef> _ScselectList = new List<UDTScselectDef>();

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
        /// 班級名稱
        /// </summary>
        Dictionary<string, string> _classNameDict = new Dictionary<string, string>();
        public CourseAttendanceRpt(List<string> selCourseIDList)
        {
            _SelectCourseIDList = selCourseIDList;
            _bgWork = new BackgroundWorker();
            _bgWork.DoWork += new DoWorkEventHandler(_bgWork_DoWork);
            _bgWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWork_RunWorkerCompleted);
            _bgWork.ProgressChanged += new ProgressChangedEventHandler(_bgWork_ProgressChanged);
            _bgWork.WorkerReportsProgress = true;
        }

        void _bgWork_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            FISCA.Presentation.MotherForm.SetStatusBarMessage("重補修課程缺曠統計表產生中..", e.ProgressPercentage);
        }

        void _bgWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FISCA.Presentation.MotherForm.SetStatusBarMessage("重補修課程缺曠統計表產生完成");
            System.Diagnostics.Process.Start(pathW);
        }

        public void Run()
        {
            _bgWork.RunWorkerAsync();
        }

        void _bgWork_DoWork(object sender, DoWorkEventArgs e)
        {

            #region 讀取資料並整理
            // 取得所選課程資料
            _CourseList = UDTTransfer.UDTCourseSelectUIDs(_SelectCourseIDList);
            // 取得所選課程上課時間表            
            _TimeSectionList = UDTTransfer.UDTTimeSectionSelectByCourseIDList(_SelectCourseIDList);

            _classNameDict.Clear();
            foreach (ClassRecord cr in Class.SelectAll())
                _classNameDict.Add(cr.ID, cr.Name);

            // 取得所選課程修課學生資料
            _ScselectList = UDTTransfer.UDTSCSelectByCourseIDList(_SelectCourseIDList);

            // 取得所選課程缺曠資料
            _AttendanceList = UDTTransfer.UDTAttendanceSelectByCourseIDList(_SelectCourseIDList);


            _bgWork.ReportProgress(30);
            //取得修課學生ID
            List<string> studIDList = new List<string>();

            foreach (UDTScselectDef data in _ScselectList)
            {
                string sid = data.StudentID.ToString();
                if (!studIDList.Contains(sid))
                    studIDList.Add(sid);
            }
            Dictionary<int, StudentRecord> studRecDict = new Dictionary<int, StudentRecord>();
            foreach (StudentRecord stud in Student.SelectByIDs(studIDList))
            {
                int sid = int.Parse(stud.ID);
                studRecDict.Add(sid, stud);
            } 
            #endregion

            #region 填值並合併樣板處理
            Document docTemplate = new Document(new MemoryStream(Properties.Resources.重補修課程缺曠樣板));
            
            // 最終樣板
            Document doc = new Document();
            // 處理缺曠用
            DataTable dtAtt = new DataTable();            
            List<Document> docList = new List<Document>();

            DataTable dt = new DataTable();
            foreach (UDTCourseDef courseData in _CourseList)
            {
                int coid = int.Parse(courseData.UID);

                dt.Clear();
                dt.Columns.Clear();
                // 放入欄位
                dt.Columns.Add("學校名稱");
                dt.Columns.Add("課程名稱");
                dt.Columns.Add("缺曠總計");
                dt.Columns.Add("學生人數");
                int Max = 100;
                for (int i = 1; i <= Max; i++)
                {
                    dt.Columns.Add("班級" + i);
                    dt.Columns.Add("座號" + i);
                    dt.Columns.Add("課程座號" + i);
                    dt.Columns.Add("姓名" + i);
                    dt.Columns.Add("總計" + i);
                }

                DataRow dr = dt.NewRow();
                dr["學校名稱"] = K12.Data.School.ChineseName;
                dr["課程名稱"] = courseData.CourseName;
                
                dtAtt.Clear();
                dtAtt.Columns.Clear();
                dtAtt.Columns.Add("缺曠日期");
                // 缺曠資料
                for (int i = 1; i <= Max; i++)
                    dtAtt.Columns.Add("缺曠紀錄" + i);

                DataRow drTT = dtAtt.NewRow();

                // 清空與重設相關需要資料
                Global._CourseStudentAttendanceIdxDict.Clear();
                Global._CousreAttendList.Clear();
                Global._CourseTimeSectionList.Clear();

                // 取得課程修課學生
                List<UDTScselectDef> coSelList=(from data in _ScselectList where data.CourseID==coid orderby data.SeatNo select data).ToList();
                
                // 整理課程上課時間表
                foreach (UDTTimeSectionDef data in _TimeSectionList)
                {
                    if (data.CourseID == coid)
                        Global._CourseTimeSectionList.Add(data);
                }

                // 整理課程修課缺曠記錄
                foreach (UDTAttendanceDef data in _AttendanceList)
                {
                    if (data.CourseID == coid)
                        Global._CousreAttendList.Add(data);
                }

                int cot = 1, SumCount = 0;
                // 使用課程修課學生為主填入資料
                foreach (UDTScselectDef coSel in coSelList)
                {
                    dr["課程座號" + cot] = coSel.SeatNo;
                    if((studRecDict.ContainsKey(coSel.StudentID)))
                    {
                        if (_classNameDict.ContainsKey(studRecDict[coSel.StudentID].RefClassID))
                        {
                            dr["班級" + cot] = _classNameDict[studRecDict[coSel.StudentID].RefClassID];
                        }

                        dr["姓名"+cot]=studRecDict[coSel.StudentID].Name;
                        if (studRecDict[coSel.StudentID].SeatNo.HasValue)
                            dr["座號" + cot] = studRecDict[coSel.StudentID].SeatNo.Value;

                        // 計算學生缺曠統計
                        int atCount = 0;
                        foreach (UDTAttendanceDef data in Global._CousreAttendList)
                        {
                            if (data.StudentID == coSel.StudentID)
                                atCount++;
                        }
                        dr["總計" + cot] = atCount;
                        SumCount += atCount;
                    }
                    string ssid = coSel.StudentID.ToString();
                    if (!Global._CourseStudentAttendanceIdxDict.ContainsKey(ssid))
                        Global._CourseStudentAttendanceIdxDict.Add(ssid, cot);
                    drTT["缺曠紀錄" + cot] = coSel.StudentID;                    
                    cot++;
                }
                dr["學生人數"] = coSelList.Count;
                dr["缺曠總計"] = SumCount;
                dt.Rows.Add(dr);
                dtAtt.Rows.Add(drTT);
                // 處理動態處理(缺曠)
                Document docAtt = new Document();
                docAtt.Sections.Clear();
                docAtt.Sections.Add(docAtt.ImportNode(docTemplate.Sections[0], true));
                _builder = new DocumentBuilder(docAtt);
                docAtt.MailMerge.MergeField += new Aspose.Words.Reporting.MergeFieldEventHandler(MailMerge_MergeField);
                docAtt.MailMerge.Execute(dtAtt);
                
                Document doc1 = new Document();
                doc1.Sections.Clear();
                doc1.Sections.Add(doc1.ImportNode(docAtt.Sections[0], true));
                doc1.MailMerge.Execute(dt);
                doc1.MailMerge.RemoveEmptyParagraphs = true;
                doc1.MailMerge.DeleteFields();
                // 清除多餘欄位
                int TabRowCount = doc1.Sections[0].Body.Tables[0].Rows.Count-1;
                for (int idx = TabRowCount; idx >=cot; idx--)
                {
                    doc1.Sections[0].Body.Tables[0].Rows.RemoveAt(idx);
                }
                docList.Add(doc1);

            }

            doc.Sections.Clear();
            foreach (Document doc1 in docList)
                doc.Sections.Add(doc.ImportNode(doc1.Sections[0], true));

            string reportNameW = "課程缺曠統計表";
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
                            _builder.RowFormat.HeightRule = HeightRule.Exactly;
                            _builder.RowFormat.Height = 18.0;

                            Dictionary<string, int> dataDict = new Dictionary<string, int>();

                             // 缺曠日期
                            foreach (string name in colNameList)
                            {
                                dataDict.Add(name, 0);
                            }
                            int id=int.Parse(sid);
                            Dictionary<int, string> timeSecDateStrDict = new Dictionary<int, string>();
                            Dictionary<string, int> timeSecCountDict = new Dictionary<string, int>();
                            foreach (UDTTimeSectionDef tt in Global._CourseTimeSectionList)
                            {
                                int ttid = int.Parse(tt.UID);
                                if (!timeSecDateStrDict.ContainsKey(ttid))
                                    timeSecDateStrDict.Add(ttid, tt.Date.Month + "/" + tt.Date.Day);
                            }

                            foreach (UDTAttendanceDef data in Global._CousreAttendList)
                            {
                                if (data.StudentID == id)
                                {
                                    if (timeSecDateStrDict.ContainsKey(data.TimeSectionID))
                                    {
                                        string tKey = timeSecDateStrDict[data.TimeSectionID];
                                        if (!timeSecCountDict.ContainsKey(tKey))
                                            timeSecCountDict.Add(tKey, 0);

                                        timeSecCountDict[tKey]++;
                                    }
                                }                            
                            }

                            // 缺曠統計
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
                            if(!colNameList.Contains(dtName))
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
                            _builder.RowFormat.HeightRule = HeightRule.Exactly;
                            _builder.RowFormat.Height = 30.0;                           

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
    }
}
