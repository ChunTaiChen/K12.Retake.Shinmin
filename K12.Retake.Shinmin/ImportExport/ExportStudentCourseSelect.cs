using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using K12.Data;
using K12.Retake.Shinmin.DAO;
using Aspose.Cells;
using System.ComponentModel;
using System.IO;

namespace K12.Retake.Shinmin.ImportExport
{
    public class ExportStudentCourseSelect
    {
        BackgroundWorker _bgWork;
        List<string> _CourseIDList;
        Dictionary<string, StudentRecord> _StudentRecDict;
        Dictionary<string, ClassRecord> _ClassRecDict;
        Dictionary<int, UDTCourseDef> _CourseDefDict;
        List<UDTScselectDef> _ScselectDefList;
        List<StudentCourseSelect> _StudentCourseSelectList;
        Dictionary<string, StudentCourseSelect> _StudentCourseSelectSumDict;


        public ExportStudentCourseSelect(List<string> courseIDList)
        {
            _CourseIDList = courseIDList;
            _StudentRecDict = new Dictionary<string, StudentRecord>();
            _ClassRecDict = new Dictionary<string, ClassRecord>();
            _CourseDefDict = new Dictionary<int, UDTCourseDef>();
            _ScselectDefList = new List<UDTScselectDef>();
            _StudentCourseSelectList = new List<StudentCourseSelect>();
            _StudentCourseSelectSumDict = new Dictionary<string, StudentCourseSelect>();
            _bgWork = new BackgroundWorker();
            
            _bgWork.DoWork += new DoWorkEventHandler(_bgWork_DoWork);
            _bgWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWork_RunWorkerCompleted);
            _bgWork.WorkerReportsProgress = true;
            _bgWork.ProgressChanged += new ProgressChangedEventHandler(_bgWork_ProgressChanged);

            _bgWork.RunWorkerAsync();
        }

        void _bgWork_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }

        void _bgWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                Workbook wb = (Workbook)e.Result;
                Utility.CompletedXls("學生選課清單", wb);
            }
        }

        void _bgWork_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // 班級
                _ClassRecDict.Clear();
                foreach (ClassRecord rec in Class.SelectAll())
                    _ClassRecDict.Add(rec.ID, rec);

                // 課程
                _CourseDefDict.Clear();
                foreach (UDTCourseDef rec in UDTTransfer.UDTCourseSelectUIDs(_CourseIDList))
                {
                    int id = int.Parse(rec.UID);
                    if (!_CourseDefDict.ContainsKey(id))
                        _CourseDefDict.Add(id, rec);
                }

                // 修課
                _ScselectDefList.Clear();
                _ScselectDefList = UDTTransfer.UDTSCSelectByCourseIDList(_CourseIDList);

                // 取得學生ID
                List<string> StudIDList = new List<string>();
                foreach (UDTScselectDef rec in _ScselectDefList)
                {
                    string sid = rec.StudentID.ToString();
                    if (!StudIDList.Contains(sid))
                        StudIDList.Add(sid);
                }

                // 學生
                _StudentRecDict.Clear();
                foreach (StudentRecord rec in Student.SelectByIDs(StudIDList))
                    _StudentRecDict.Add(rec.ID, rec);

                // 產生學生修課明細
                _StudentCourseSelectList.Clear();
                foreach (UDTScselectDef rec in (from data in _ScselectDefList orderby data.StudentID select data))
                {
                    StudentCourseSelect scs = new StudentCourseSelect();
                    scs.StudentID = rec.StudentID.ToString();
                    scs.CourseSeatNo = rec.SeatNo;
                    if (_CourseDefDict.ContainsKey(rec.CourseID))
                    {
                        scs.CourseName = _CourseDefDict[rec.CourseID].CourseName;
                        scs.Credit = _CourseDefDict[rec.CourseID].Credit;
                        scs.SchoolYear = _CourseDefDict[rec.CourseID].SchoolYear;
                        scs.Semester = _CourseDefDict[rec.CourseID].Semester;
                    }

                    if (_StudentRecDict.ContainsKey(scs.StudentID))
                    {
                        scs.Name = _StudentRecDict[scs.StudentID].Name;
                        scs.SeatNo = _StudentRecDict[scs.StudentID].SeatNo;
                        scs.StudentNumber = _StudentRecDict[scs.StudentID].StudentNumber;
                        if (_ClassRecDict.ContainsKey(_StudentRecDict[scs.StudentID].RefClassID))
                        {
                            scs.ClassName = _ClassRecDict[_StudentRecDict[scs.StudentID].RefClassID].Name;
                        }
                    }
                    _StudentCourseSelectList.Add(scs);
                }

                // 產生學生修課總計
                _StudentCourseSelectSumDict.Clear();
                foreach (StudentCourseSelect rec in _StudentCourseSelectList)
                {
                    if (!_StudentCourseSelectSumDict.ContainsKey(rec.StudentID))
                    {
                        StudentCourseSelect scs = new StudentCourseSelect();
                        scs.StudentID = rec.StudentID;
                        scs.SeatNo = rec.SeatNo;
                        scs.ClassName = rec.ClassName;
                        scs.Name = rec.Name;
                        scs.StudentNumber = rec.StudentNumber;
                        scs.Credit = rec.Credit;
                        scs.SchoolYear = rec.SchoolYear;
                        scs.Semester = rec.Semester;
                        _StudentCourseSelectSumDict.Add(rec.StudentID, scs);
                    }
                    else
                    {
                        _StudentCourseSelectSumDict[rec.StudentID].Credit += rec.Credit;
                    }
                }


                Workbook wb = new Workbook();
                wb.Open(new MemoryStream(Properties.Resources.學生選課清單));

                List<StudentCourseSelect> sc1List = (from data in _StudentCourseSelectList orderby data.StudentNumber select data).ToList();

                // 填值
                // 學號	姓名	班級	座號	課程座號	課程名稱	學分數
                int row1 = 1;
                foreach (StudentCourseSelect scs in sc1List)
                {
                    wb.Worksheets[0].Cells[row1, 0].PutValue(scs.StudentNumber);
                    wb.Worksheets[0].Cells[row1, 1].PutValue(scs.Name);
                    wb.Worksheets[0].Cells[row1, 2].PutValue(scs.ClassName);
                    wb.Worksheets[0].Cells[row1, 3].PutValue(scs.SeatNo);
                    wb.Worksheets[0].Cells[row1, 4].PutValue(scs.CourseSeatNo);
                    wb.Worksheets[0].Cells[row1, 5].PutValue(scs.CourseName);
                    wb.Worksheets[0].Cells[row1, 6].PutValue(scs.Credit);
                    row1++;
                }
                List<StudentCourseSelect> stemp = _StudentCourseSelectSumDict.Values.ToList();
                List<StudentCourseSelect> sc2List = (from data in stemp orderby data.StudentNumber select data).ToList();

                // 學號	姓名	班級	座號	學分數
                int row2 = 1;
                foreach (StudentCourseSelect scs in sc2List)
                {
                    wb.Worksheets[1].Cells[row2, 0].PutValue(scs.StudentNumber);
                    wb.Worksheets[1].Cells[row2, 1].PutValue(scs.Name);
                    wb.Worksheets[1].Cells[row2, 2].PutValue(scs.ClassName);
                    wb.Worksheets[1].Cells[row2, 3].PutValue(scs.SeatNo);
                    wb.Worksheets[1].Cells[row2, 4].PutValue(scs.Credit);
                    row2++;
                }

                wb.Worksheets[0].AutoFitColumns();
                wb.Worksheets[1].AutoFitColumns();
                e.Result = wb;
            }
            catch (Exception ex)
            {
                e.Cancel = true;
            }
        }
    }
}
