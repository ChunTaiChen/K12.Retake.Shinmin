using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using K12.Retake.Shinmin.DAO;
using Aspose.Cells;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
namespace K12.Retake.Shinmin.Report
{
    /// <summary>
    /// 學生及格人數報表
    /// </summary>
    public class StudentPassReport
    {
        BackgroundWorker _bgWorker;
        List<string> _CourseIDList;
        Dictionary<string, StudentPassGroup> _StudentPassGroupDict = new Dictionary<string, StudentPassGroup>();
        // 修課資料
        List<UDTScselectDef> _ScselectList;
        public void Main()
        {
            _bgWorker = new BackgroundWorker();
            _bgWorker.DoWork += new DoWorkEventHandler(_bgWorker_DoWork);
            _bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorker_RunWorkerCompleted);
            _bgWorker.WorkerReportsProgress = true;
            _bgWorker.ProgressChanged += new ProgressChangedEventHandler(_bgWorker_ProgressChanged);
            // 取得目前課程
            _CourseIDList = QueryData.GetCourseSelectActive1();
            _ScselectList = new List<UDTScselectDef>();

            if (_CourseIDList.Count > 0)
            {
                _bgWorker.RunWorkerAsync();
            }
            else
            {
                FISCA.Presentation.Controls.MsgBox.Show("沒有課程!");
                return;
            }
        }

        void _bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            FISCA.Presentation.MotherForm.SetStatusBarMessage("重補修及格人數產生中..", e.ProgressPercentage);
        }

        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
         
            // 會出Excel
            if (e.Error != null)
            {
                FISCA.Presentation.Controls.MsgBox.Show("重補修及格人數報表產生發生錯誤:"+e.Error.Message);
                return;
            }
            Workbook wb = (Workbook)e.Result;
            if (wb != null)
            {
                Utility.CompletedXls("及格人數(重補修)", wb);
            }
            else
            {
                FISCA.Presentation.Controls.MsgBox.Show("重補修及格人數報表產生Excel檔案發生錯誤");
                return;
            }
            FISCA.Presentation.MotherForm.SetStatusBarMessage("重補修及格人數產生完成。", 100);
        }

        void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // 取得修課
            _bgWorker.ReportProgress(1);
            _ScselectList = UDTTransfer.UDTSCSelectByCourseIDList(_CourseIDList);
            _StudentPassGroupDict.Clear();

            // 取得修課學生的學生系統編號
            List<string> studIDList = new List<string>();
            foreach (UDTScselectDef data in _ScselectList)
            {
                string sid = data.StudentID.ToString();
                if (!studIDList.Contains(sid))
                    studIDList.Add(sid);
            }

            // 取得學生年級科別名稱
            Dictionary<string, DataRow> sDataDict = QueryData.GetStudentGradeDeptDictByStudentIDList(studIDList);
            _bgWorker.ReportProgress(20);
            List<StudentItem> siList = new List<StudentItem>();
            // 處理資料
            foreach (UDTScselectDef data in _ScselectList)
            {
                StudentItem si = new StudentItem();
                si.SCUID = data.UID;
                si.StudentID = data.StudentID.ToString();
                si.GradeYear=0;
                si.DeptName="";
                if (sDataDict.ContainsKey(si.StudentID))
                {
                    si.GradeYear = int.Parse(sDataDict[si.StudentID][1].ToString());
                    si.DeptName = sDataDict[si.StudentID][2].ToString();
                }
                if (data.Score >= 0)
                    si.isPass = true;
                else
                    si.isPass = false;

                si.isNotExam=false;
                if (data.NotExam.HasValue)
                    if (data.NotExam.Value)
                        si.isNotExam = true;

                siList.Add(si);
            }

            // 取得所有年級
            List<int> grYearList = new List<int>();
            // 科別名稱
            List<string> deptNameList = new List<string>();
            _bgWorker.ReportProgress(50);
           // 解析與計算組合資料
            foreach (StudentItem si in siList)
            {
                string key1 = si.GradeYear+"_"+si.DeptName;
                string key2 = si.GradeYear + "年級";
                string key3 = "總計";

                if (!grYearList.Contains(si.GradeYear))
                    grYearList.Add(si.GradeYear);

                if (!deptNameList.Contains(si.DeptName))
                    deptNameList.Add(si.DeptName);

                if (!_StudentPassGroupDict.ContainsKey(key1))
                {
                    StudentPassGroup spg1 = new StudentPassGroup();
                    spg1.GroupName = key1;
                    _StudentPassGroupDict.Add(key1, spg1);
                }

                if (!_StudentPassGroupDict.ContainsKey(key2))
                {
                    StudentPassGroup spg2 = new StudentPassGroup();
                    spg2.GroupName = key2;
                    _StudentPassGroupDict.Add(key2, spg2);
                }

                if (!_StudentPassGroupDict.ContainsKey(key3))
                {
                    StudentPassGroup spg3 = new StudentPassGroup();
                    spg3.GroupName = key3;
                    _StudentPassGroupDict.Add(key3, spg3);
                }

                // 重補修人數
                _StudentPassGroupDict[key1].retakeCount++;
                _StudentPassGroupDict[key2].retakeCount++;
                _StudentPassGroupDict[key3].retakeCount++;

                // 扣考人數
                if (si.isNotExam)
                {
                    _StudentPassGroupDict[key1].notExamCount++;
                    _StudentPassGroupDict[key2].notExamCount++;
                    _StudentPassGroupDict[key3].notExamCount++;
                }

                // 不及格與及格人數
                if (si.isPass)
                {
                    _StudentPassGroupDict[key1].PassCount++;
                    _StudentPassGroupDict[key2].PassCount++;
                    _StudentPassGroupDict[key3].PassCount++;
                }
                else
                {
                    _StudentPassGroupDict[key1].noPassCount++;
                    _StudentPassGroupDict[key2].noPassCount++;
                    _StudentPassGroupDict[key3].noPassCount++;
                }
            }

            grYearList.Sort();

            _bgWorker.ReportProgress(70);
            // 產生 Excel 報表
            Workbook wb = new Workbook();
            wb.Open(new MemoryStream(Properties.Resources.及格人數樣板));

            // 樣板複製表格
            Range R_Value = wb.Worksheets[1].Cells.CreateRange(0, 1, false);
            int f = _StudentPassGroupDict.Count;
            for (int rowV = 2; rowV <= f; rowV++)
                wb.Worksheets[0].Cells.CreateRange(rowV, 1, false).Copy(R_Value);            

            // 填值
            int rowIdx = 1;
            
            // 科別,重補修人次,1/3不予考核人次,不及格人次,及格人次,及格率

            foreach(int gr in grYearList)
            {
                // 科別
                foreach (string dpName in deptNameList)
                {             
                    string keyIdx1 = gr + "_" + dpName;
                    if (_StudentPassGroupDict.ContainsKey(keyIdx1))
                    {
                        wb.Worksheets[0].Cells[rowIdx, 0].PutValue(dpName);
                        wb.Worksheets[0].Cells[rowIdx, 1].PutValue(_StudentPassGroupDict[keyIdx1].retakeCount);
                        wb.Worksheets[0].Cells[rowIdx, 2].PutValue(_StudentPassGroupDict[keyIdx1].notExamCount);
                        wb.Worksheets[0].Cells[rowIdx, 3].PutValue(_StudentPassGroupDict[keyIdx1].noPassCount);
                        wb.Worksheets[0].Cells[rowIdx, 4].PutValue(_StudentPassGroupDict[keyIdx1].PassCount);
                        wb.Worksheets[0].Cells[rowIdx, 5].PutValue(_StudentPassGroupDict[keyIdx1].GetPassRate()+"%");
                        rowIdx++;
                    }
                }

                // 小計
                string keyIdx2 = gr + "年級";
                if (_StudentPassGroupDict.ContainsKey(keyIdx2))
                {
                    Range R_Value1 = wb.Worksheets[1].Cells.CreateRange(2, 1, false);
                    wb.Worksheets[0].Cells.CreateRange(rowIdx, 1, false).Copy(R_Value1);                                
                    
                    wb.Worksheets[0].Cells[rowIdx,0].PutValue(keyIdx2+"小計");
                    wb.Worksheets[0].Cells[rowIdx,1].PutValue(_StudentPassGroupDict[keyIdx2].retakeCount);
                    wb.Worksheets[0].Cells[rowIdx,2].PutValue(_StudentPassGroupDict[keyIdx2].notExamCount);
                    wb.Worksheets[0].Cells[rowIdx,3].PutValue(_StudentPassGroupDict[keyIdx2].noPassCount);
                    wb.Worksheets[0].Cells[rowIdx,4].PutValue(_StudentPassGroupDict[keyIdx2].PassCount);
                    wb.Worksheets[0].Cells[rowIdx,5].PutValue(_StudentPassGroupDict[keyIdx2].GetPassRate()+"%");
                    rowIdx++;
                }

            }

            string key3Idx3 = "總計";
            if (_StudentPassGroupDict.ContainsKey(key3Idx3))
            {
                Range R_Value2 = wb.Worksheets[1].Cells.CreateRange(2, 1, false);
                wb.Worksheets[0].Cells.CreateRange(rowIdx, 1, false).Copy(R_Value2);                                
            
                wb.Worksheets[0].Cells[rowIdx, 0].PutValue(key3Idx3);
                wb.Worksheets[0].Cells[rowIdx, 1].PutValue(_StudentPassGroupDict[key3Idx3].retakeCount);
                wb.Worksheets[0].Cells[rowIdx, 2].PutValue(_StudentPassGroupDict[key3Idx3].notExamCount);
                wb.Worksheets[0].Cells[rowIdx, 3].PutValue(_StudentPassGroupDict[key3Idx3].noPassCount);
                wb.Worksheets[0].Cells[rowIdx, 4].PutValue(_StudentPassGroupDict[key3Idx3].PassCount);
                wb.Worksheets[0].Cells[rowIdx, 5].PutValue(_StudentPassGroupDict[key3Idx3].GetPassRate()+"%");
                rowIdx++;
            }
            _bgWorker.ReportProgress(90);
            // 回傳資料
            wb.Worksheets[0].AutoFitColumns();
            wb.Worksheets.RemoveAt("A");
            e.Result = wb;
        }
    }
}
