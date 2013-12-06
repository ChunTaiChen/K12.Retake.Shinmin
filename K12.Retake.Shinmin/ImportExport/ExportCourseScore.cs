using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using K12.Retake.Shinmin.DAO;
using SmartSchool.Customization.Data;
using SmartSchool.Customization.Data.StudentExtension;
using Aspose.Cells;
using FISCA.Data;
using System.Data;
using System.Xml;

namespace K12.Retake.Shinmin.ImportExport
{       
    public class ExportCourseScore
    {
        BackgroundWorker _bgWorker= new BackgroundWorker ();
        List<string> _CourseIDList;
        List<UDTScselectDef> _ScSelectList;

        public ExportCourseScore(List<string> courseIDList)
        {
            _CourseIDList = courseIDList;

            _bgWorker.DoWork += new DoWorkEventHandler(_bgWorker_DoWork);
            _bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorker_RunWorkerCompleted);
            _bgWorker.RunWorkerAsync();
            
        }

        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Workbook wb = (Workbook)e.Result;
                Utility.CompletedXls("學期成績檔案(重補修)", wb);
            }
            else
            {
                FISCA.Presentation.Controls.MsgBox.Show("產生成績檔案發生錯誤," + e.Error.Message);
            }
            
        }

        void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            // 取得目前重補修課程成績資料
            _ScSelectList = UDTTransfer.UDTSCSelectByCourseIDList(_CourseIDList);

            List<string> studIDList = new List<string>();
            List<string> courseIDList = new List<string>();

            // 分出重修與補休
            // 重修
            List<UDTScselectDef> score1List = new List<UDTScselectDef>();

            List<CourseScore> CourseScoreList = new List<CourseScore>();

            // 補修
            List<UDTScselectDef> score2List = new List<UDTScselectDef>();

            foreach (UDTScselectDef data in _ScSelectList)
            {
                string cid = data.CourseID.ToString();
                if (!courseIDList.Contains(cid))
                    courseIDList.Add(cid);

                string sid = data.StudentID.ToString();
                if (!studIDList.Contains(sid))
                    studIDList.Add(sid);

                if (data.Type == "重修")
                    score1List.Add(data);

                if (data.Type=="補修")
                    score2List.Add(data);
            }

            // 取得學生
            Dictionary<int, K12.Data.StudentRecord> studDict = new Dictionary<int, Data.StudentRecord>();
            foreach (K12.Data.StudentRecord data in K12.Data.Student.SelectByIDs(studIDList))
            {
                int id = int.Parse(data.ID);
                studDict.Add(id, data);
            }

            // 班級
            Dictionary<string, K12.Data.ClassRecord> classDict = new Dictionary<string, Data.ClassRecord>();
            foreach (K12.Data.ClassRecord data in K12.Data.Class.SelectAll())
                classDict.Add(data.ID, data);

            // 取得學生成績
            Dictionary<int, List<SemesterSubjectScoreInfo>> semsScoreDict = new Dictionary<int,List<SemesterSubjectScoreInfo>> ();
                      // 填入學期科目成績(有過濾重讀)
             AccessHelper accessHelper = new AccessHelper();
             List<StudentRecord> StudRecList = accessHelper.StudentHelper.GetStudents(studIDList);
            accessHelper.StudentHelper.FillSemesterSubjectScore(true,StudRecList);
            foreach (StudentRecord studRec in StudRecList)
            {
                int sid = int.Parse(studRec.StudentID);
                if (!semsScoreDict.ContainsKey(sid))
                    semsScoreDict.Add(sid, new List<SemesterSubjectScoreInfo>());
                foreach (SemesterSubjectScoreInfo SemesterSubjectScore in studRec.SemesterSubjectScoreList)
                    semsScoreDict[sid].Add(SemesterSubjectScore);
            }


            // 取得課程相關
            Dictionary<int, UDTCourseDef> courseDict = new Dictionary<int, UDTCourseDef>();
            foreach (UDTCourseDef rec in UDTTransfer.UDTCourseSelectUIDs(courseIDList))
            {
                courseDict.Add(int.Parse(rec.UID), rec);
            }

            // 學生使用的課程規劃id
            Dictionary<string, string> StudGraduationPlanIDDict = new Dictionary<string, string>();
            QueryHelper qh1 = new QueryHelper();
            string strSQL1 = "select student.id,class.ref_graduation_plan_id from student inner join class on student.ref_class_id=class.id  where student.status in(1,2) and class.ref_graduation_plan_id is not null;";
            DataTable dt1 = qh1.Select(strSQL1);
            foreach (DataRow dr in dt1.Rows)
            {
                string id = dr[0].ToString();
                string gid = dr[1].ToString();
                if (!StudGraduationPlanIDDict.ContainsKey(id))
                    StudGraduationPlanIDDict.Add(id, gid);
            }

            // 取得學生課程規劃ID，如果已有ID以學生身上為主。
            QueryHelper qh2 = new QueryHelper();
            string strSQL2 = "select id,ref_graduation_plan_id from student where student.status in(1,2) and ref_graduation_plan_id is not null;";
            DataTable dt2 = qh2.Select(strSQL2);
            foreach (DataRow dr in dt2.Rows)
            {
                string id = dr[0].ToString();
                string gid = dr[1].ToString();
                if (StudGraduationPlanIDDict.ContainsKey(id))
                    StudGraduationPlanIDDict[id] = gid;
                else
                    StudGraduationPlanIDDict.Add(id, gid);
            }





            // 重修處理
            foreach (UDTScselectDef data in score1List)
            {
                bool hasData = false;
                if (semsScoreDict.ContainsKey(data.StudentID))
                {                    
                    foreach (SemesterSubjectScoreInfo rec1 in semsScoreDict[data.StudentID])
                    {
                        string ss = rec1.Detail.ToString();                        

                        // 需要計算學分
                        if (rec1.Detail.GetAttribute("不計學分") != "是")
                        {
                                // 取得課程
                            if (courseDict.ContainsKey(data.CourseID))
                            {
                                UDTCourseDef co = courseDict[data.CourseID];
                                string lev = "";
                                if (co.SubjectLevel.HasValue)
                                    lev = co.SubjectLevel.Value.ToString();
                                if (co.SubjectName == rec1.Subject && rec1.Level == lev)
                                {
                                    CourseScore cs = new CourseScore();
                                    hasData = true;
                                    cs.GradeYear = rec1.GradeYear;
                                    if (studDict.ContainsKey(data.StudentID))
                                    {
                                        cs.Name = studDict[data.StudentID].Name;
                                        cs.StudentNumber = studDict[data.StudentID].StudentNumber;
                                    }
                                    cs.pass = false;
                                    if (data.Score.HasValue)
                                    {
                                        cs.Score = data.Score;
                                        if (data.Score.Value >= 60)
                                        {
                                            cs.pass = true;
                                            cs.Score = 60;
                                        }
                                    }
                                    cs.SchoolYear = rec1.SchoolYear;
                                    cs.Semester = rec1.Semester;
                                    cs.SubjectLevel = co.SubjectLevel;
                                    cs.SubjectName = co.SubjectName;
                                    if (rec1.Require)
                                        cs.strRequire = "必修";
                                    else
                                        cs.strRequire = "選修";
                                    cs.Def1 = rec1.Detail.GetAttribute("修課校部訂");

                                    decimal dd;
                                    if (decimal.TryParse(rec1.Detail.GetAttribute("原始成績"), out dd))
                                        cs.retSourceScore = dd;

                                    cs.Credit = co.Credit;
                                    cs.isT1 = true;
                                    CourseScoreList.Add(cs);
                                }
                            } 
                            
                        }

                    }
                }
                // 完全比不到重修成績當補修看
                if (hasData == false)
                {
                    // 放入補修處理
                    score2List.Add(data);
                }
            }//

            Dictionary<string, List<GraduationPlanSubject>> gPlanDict = QueryData.GetGraduationPlan();

            int dSchoolYear = int.Parse(K12.Data.School.DefaultSchoolYear);
            int dSemester = int.Parse(K12.Data.School.DefaultSemester);
            // 補修處理



            foreach (UDTScselectDef data in score2List)
            {                
                if (courseDict.ContainsKey(data.CourseID))
                {
                    CourseScore cs = new CourseScore();
                    UDTCourseDef co = courseDict[data.CourseID];

                    cs.isT1 = false;
                    if (studDict.ContainsKey(data.StudentID))
                    {
                        cs.Name = studDict[data.StudentID].Name;
                        cs.StudentNumber = studDict[data.StudentID].StudentNumber;
                        cs.GradeYear = 0;
                        if (classDict.ContainsKey(studDict[data.StudentID].RefClassID))
                        {
                            if (classDict[studDict[data.StudentID].RefClassID].GradeYear.HasValue)
                                cs.GradeYear = classDict[studDict[data.StudentID].RefClassID].GradeYear.Value;
                        }
                    }
                    cs.pass = false;
                    if (data.Score.HasValue)
                    {
                        cs.Score = data.Score;
                        if (data.Score.Value >= 60)                        
                            cs.pass = true;                            
                        
                    }


                    cs.SchoolYear = dSchoolYear;
                    cs.Semester = dSemester;
                    cs.SubjectLevel = co.SubjectLevel;
                    cs.SubjectName = co.SubjectName;
                    cs.Credit = co.Credit;

                    string sid = data.StudentID.ToString();
                    if (StudGraduationPlanIDDict.ContainsKey(sid))
                    {
                        string gid = StudGraduationPlanIDDict[sid];
                        if (gPlanDict.ContainsKey(gid))
                        {
                            foreach (GraduationPlanSubject gp in gPlanDict[gid])
                            {
                                string lev = "";
                                if (cs.SubjectLevel.HasValue)
                                    lev = cs.SubjectLevel.Value.ToString();

                                if (gp.SubjectName == cs.SubjectName && gp.Level == lev)
                                {
                                    cs.strRequire = gp.strRequire;
                                    cs.Def1 = gp.def1;
                                }

                            }
                        }
                    }
                    
                    CourseScoreList.Add(cs);
                }
            }

            // 處理輸出到Excel
            Workbook wb = new Workbook();
            // 學號,姓名,科目,科目級別,學年度,學期,學分數,分項類別,成績年級,原始成績,重修成績,取得學分
            wb.Worksheets[0].Cells[0, 0].PutValue("學號");
            wb.Worksheets[0].Cells[0, 1].PutValue("姓名");
            wb.Worksheets[0].Cells[0, 2].PutValue("科目");
            wb.Worksheets[0].Cells[0, 3].PutValue("科目級別");
            wb.Worksheets[0].Cells[0, 4].PutValue("學年度");
            wb.Worksheets[0].Cells[0, 5].PutValue("學期");
            wb.Worksheets[0].Cells[0, 6].PutValue("學分數");
            wb.Worksheets[0].Cells[0, 7].PutValue("分項類別");           
            wb.Worksheets[0].Cells[0, 8].PutValue("成績年級");
            wb.Worksheets[0].Cells[0, 9].PutValue("必選修");
            wb.Worksheets[0].Cells[0, 10].PutValue("校部訂");
            wb.Worksheets[0].Cells[0, 11].PutValue("原始成績");
            wb.Worksheets[0].Cells[0, 12].PutValue("重修成績");
            wb.Worksheets[0].Cells[0, 13].PutValue("取得學分");

            int rowIdx = 1;
            foreach (CourseScore cs in CourseScoreList)
            {
                wb.Worksheets[0].Cells[rowIdx, 0].PutValue(cs.StudentNumber);
                wb.Worksheets[0].Cells[rowIdx, 1].PutValue(cs.Name);
                wb.Worksheets[0].Cells[rowIdx, 2].PutValue(cs.SubjectName);
                wb.Worksheets[0].Cells[rowIdx, 3].PutValue(cs.SubjectLevel);
                wb.Worksheets[0].Cells[rowIdx, 4].PutValue(cs.SchoolYear);
                wb.Worksheets[0].Cells[rowIdx, 5].PutValue(cs.Semester);
                wb.Worksheets[0].Cells[rowIdx, 6].PutValue(cs.Credit);
                wb.Worksheets[0].Cells[rowIdx, 7].PutValue("學業");
                wb.Worksheets[0].Cells[rowIdx, 8].PutValue(cs.GradeYear);
                wb.Worksheets[0].Cells[rowIdx, 9].PutValue(cs.strRequire);
                wb.Worksheets[0].Cells[rowIdx, 10].PutValue(cs.Def1);

                if (cs.Score.HasValue)
                {
                    if (cs.isT1)
                    {
                        wb.Worksheets[0].Cells[rowIdx, 12].PutValue(cs.Score.Value);
                        if(cs.retSourceScore.HasValue)
                            wb.Worksheets[0].Cells[rowIdx, 11].PutValue(cs.retSourceScore.Value);
                    }
                    else
                        wb.Worksheets[0].Cells[rowIdx, 11].PutValue(cs.Score.Value);
                    
                }
                if(cs.pass)
                    wb.Worksheets[0].Cells[rowIdx, 13].PutValue("是");
                else
                    wb.Worksheets[0].Cells[rowIdx, 13].PutValue("否");
                rowIdx++;
            }
            wb.Worksheets[0].AutoFitColumns();
            e.Result = wb;
        }
    }
}
