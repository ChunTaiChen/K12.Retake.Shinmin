using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.Data;
using System.Data;
using System.Xml.Linq;
using SmartSchool.Customization.Data;
using SmartSchool.Customization.Data.StudentExtension;

namespace K12.Retake.Shinmin.DAO
{
    public class QueryData
    {
        /// <summary>
        /// 取得建議重補修清單
        /// </summary>
        /// <returns></returns>
        public static DataTable GetRetakeList1()
        {

            // 重補修程式檢查原則：
            //重修：
            //科目名稱+科目級別+學分數，不及格列出需要重修。

            //補修：
            //讀取課程規劃(先找學生所屬班級課程規劃，如果沒有再找學生本身課程規劃)。
            //使用課程規劃的年級+學期+科目名稱+科目級別，和學生學期科目成績比對，課程規劃需要修的沒有修列出。

            DataTable retTable = new DataTable();
            retTable.Columns.Add("StudentID");
            retTable.Columns.Add("年級");
            retTable.Columns.Add("科別");
            retTable.Columns.Add("班級");
            retTable.Columns.Add("學號");
            retTable.Columns.Add("座號");
            retTable.Columns["座號"].DataType = System.Type.GetType("System.Int32");

            retTable.Columns.Add("姓名");
            retTable.Columns.Add("科目名稱");
            retTable.Columns.Add("必選修");
            retTable.Columns.Add("成績");
            retTable.Columns["成績"].DataType = System.Type.GetType("System.Decimal");
            retTable.Columns.Add("學分");
            retTable.Columns["學分"].DataType = System.Type.GetType("System.Int32");
                
            retTable.Columns.Add("學年度");
            retTable.Columns.Add("學期");
            retTable.Columns.Add("成績年級");
            retTable.Columns.Add("重補修");
            retTable.Columns.Add("科目");
            retTable.Columns.Add("級別");
            retTable.Columns.Add("本學期修課");
            

            // 學生使用的課程規劃id
            Dictionary<string, string> StudGraduationPlanIDDict = new Dictionary<string, string>();
           
            
            // 取得學生關聯班級課程規劃ID 放入暫存。
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

            // 取得所有在校學生
            AccessHelper accessHelper = new AccessHelper();

            List<StudentRecord> AllStudent = accessHelper.StudentHelper.GetAllStudent();
            List<StudentRecord> StudRecList = new List<StudentRecord>();

            foreach (StudentRecord stud in AllStudent)
            {
                if (stud.Status == "一般" || stud.Status == "延修")                
                    StudRecList.Add(stud);                
            }
            
            // 填入學期科目成績(有過濾重讀)
            accessHelper.StudentHelper.FillSemesterSubjectScore(true, StudRecList);

            // 學生需要重修科目
            Dictionary<string, List<SemesterSubjectScoreInfo>> StudNoPassSubjDict = new Dictionary<string, List<SemesterSubjectScoreInfo>>();

            // 學生需要補修科目
            Dictionary<string, List<GraduationPlanSubject>> StudReSubjectDict = new Dictionary<string, List<GraduationPlanSubject>>();

            // 學生本學年度學期目前修課
            Dictionary<string, string> studCurrentSelectCourse = GetStudCurrentSelectCourse();

            // 取得課程規劃
            Dictionary<string,List<GraduationPlanSubject>> GraduationPlanDict =GetGraduationPlan();

            foreach (StudentRecord studRec in StudRecList)
            {
                // 年級+學期+科目名稱+級別
                Dictionary<string,SemesterSubjectScoreInfo> studHasSubjectScoreDict = new Dictionary<string,SemesterSubjectScoreInfo> ();
                List<string> studPassSubjectNameList = new List<string>();
                List<SemesterSubjectScoreInfo> StudFailSubjectList = new List<SemesterSubjectScoreInfo>();
                Dictionary<string, SemesterSubjectScoreInfo> studFailSubjectDict = new Dictionary<string, SemesterSubjectScoreInfo>();
                List<GraduationPlanSubject> studReSubjectList = new List<GraduationPlanSubject>();
                
                foreach (SemesterSubjectScoreInfo SemesterSubjectScore in studRec.SemesterSubjectScoreList)
                { 
                    // 需要計算學分
                    if(SemesterSubjectScore.Detail.GetAttribute("不計學分") != "是")
                    {
                        // 年級+學期+科目名稱+_+級別，給補修使用。
                        string subjectNameKey = SemesterSubjectScore.GradeYear+"_"+SemesterSubjectScore.Semester+"_"+SemesterSubjectScore.Subject + "_" + SemesterSubjectScore.Level;
                        // 科目名稱+級別+學分數+，給重修使用。
                        string subjNameRe = SemesterSubjectScore.Subject + "_" + SemesterSubjectScore.Level+"_"+SemesterSubjectScore.Credit;
                        
                        // 有修過科目
                        if(!studHasSubjectScoreDict.ContainsKey(subjectNameKey))
                            studHasSubjectScoreDict.Add(subjectNameKey,SemesterSubjectScore);

                        // 有取得學分
                        if (SemesterSubjectScore.Pass)
                            studPassSubjectNameList.Add(subjNameRe);
                        else
                        {
                            if(!studFailSubjectDict.ContainsKey(subjNameRe))
                                studFailSubjectDict.Add(subjNameRe, SemesterSubjectScore);
                        }
                            
                    }
                
                }
                // 檢查沒有獲得科目是否有在獲得中，表示有修過
                foreach (KeyValuePair<string, SemesterSubjectScoreInfo> data in studFailSubjectDict)
                {
                    if (studPassSubjectNameList.Contains(data.Key))
                        continue;

                    StudFailSubjectList.Add(data.Value);
                }

                
                // 有修過的科目中檢查需要補修
                if (StudGraduationPlanIDDict.ContainsKey(studRec.StudentID))
                {                    
                    string gid = StudGraduationPlanIDDict[studRec.StudentID];
                    
                    // 取得學生班級年級
                    int gYear = 0;
                    if(studRec.RefClass !=null)
                        int.TryParse(studRec.RefClass.GradeYear, out gYear);

                    // 取得目前系統學期
                    int sSemester = int.Parse(K12.Data.School.DefaultSemester);

                    if (GraduationPlanDict.ContainsKey(gid))
                    {
                        foreach (GraduationPlanSubject gps in GraduationPlanDict[gid])
                        {
                            string key = gps.GradeYear + "_" + gps.Semester + "_" + gps.SubjectName + "_" + gps.Level;

                            if (gps.GradeYear<=gYear && gps.Semester<=sSemester)
                            {
                                if (!studHasSubjectScoreDict.ContainsKey(key))
                                    studReSubjectList.Add(gps);                          
                            }
                        }
                    
                    }
                
                }

                // 需要重修的科目
                if (StudFailSubjectList.Count > 0)
                {
                    foreach (SemesterSubjectScoreInfo sss in StudFailSubjectList)
                    {
                        DataRow row = retTable.NewRow();

                        //row["年級"] = sss.GradeYear;
                        row["StudentID"] = studRec.StudentID;
                        row["科別"] = studRec.Department;
                        if (studRec.RefClass != null)
                        {
                            row["年級"] = studRec.RefClass.GradeYear;
                            row["班級"] = studRec.RefClass.ClassName;
                        }
                        else
                        {
                            row["年級"] = "";
                            row["班級"] = "";
                        }
                                
                        row["學號"] = studRec.StudentNumber;
                        if (!string.IsNullOrEmpty(studRec.SeatNo))
                            row["座號"] = int.Parse(studRec.SeatNo);

                        row["姓名"] = studRec.StudentName;
                        row["科目名稱"] = sss.Subject+GetNumber(sss.Level);
                        if (sss.Require)
                            row["必選修"] = "必";
                        else
                            row["必選修"] = "選";

                        row["成績"] = sss.Score;
                        row["學分"] = sss.Credit;
                        row["學年度"] = sss.SchoolYear;
                        row["學期"] = sss.Semester;
                        row["重補修"] = "重";
                        row["科目"] = sss.Subject;
                        row["級別"] = sss.Level;
                        row["成績年級"] = sss.GradeYear;
                        retTable.Rows.Add(row);
                    }
                }
                                

                // 需要補修科目
                if (studReSubjectList.Count > 0)
                {                
                    foreach (GraduationPlanSubject gps in studReSubjectList)
                    {
                        
                        DataRow row = retTable.NewRow();
                        row["StudentID"] = studRec.StudentID;
                        //row["年級"] = gps.GradeYear;
                        row["科別"] = studRec.Department;
                        if (studRec.RefClass != null)
                        {
                            row["年級"] = studRec.RefClass.GradeYear;
                            row["班級"] = studRec.RefClass.ClassName;
                        }
                        else
                        {
                            row["年級"] = "";
                            row["班級"] = "";
                        }
                        row["學號"] = studRec.StudentNumber;
                        if (!string.IsNullOrEmpty(studRec.SeatNo))
                            row["座號"] = int.Parse(studRec.SeatNo);

                        row["姓名"] = studRec.StudentName;
                        row["科目名稱"] = gps.SubjectName+GetNumber(gps.Level);
                        row["必選修"] = "必";                        
                        row["學期"] = gps.Semester;
                        row["學分"] = gps.Credit;
                        row["重補修"] = "補";
                        row["科目"] = gps.SubjectName;
                        row["級別"] = gps.Level;
                        row["成績年級"] = gps.GradeYear;

                        string selKey = studRec.StudentID + studRec.RefClass.GradeYear + gps.Semester + gps.SubjectName + gps.Level;

                        if (studCurrentSelectCourse.ContainsKey(selKey))
                            row["本學期修課"] = studCurrentSelectCourse[selKey];

                        retTable.Rows.Add(row);
                    }
                }

                //// 需要補修科目
                //if (studReSubjectList.Count > 0)
                //    StudReSubjectDict.Add(studRec.StudentID, studReSubjectList);

                //// 需要重修的科目
                //if (StudFailSubjectList.Count > 0)
                //    StudNoPassSubjDict.Add(studRec.StudentID, StudFailSubjectList);

            }
            retTable.DefaultView.Sort = "年級,科別,班級,學號,座號,姓名,科目名稱,必選修,成績,學分,學年度,學期,成績年級,重補修";
            
            return retTable;
        }

        /// <summary>
        /// 透過名冊UID 取得相對學生資料
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public static DataTable GetRetakeListByTimeListUID(string UID)
        {
            DataTable retTable = new DataTable();
            retTable.Columns.Add("StudentID");
            retTable.Columns.Add("年級");
            retTable.Columns.Add("科別");
            retTable.Columns.Add("班級");
            retTable.Columns.Add("學號");
            retTable.Columns.Add("座號");
            retTable.Columns["座號"].DataType = System.Type.GetType("System.Int32");

            retTable.Columns.Add("姓名");
            retTable.Columns.Add("科目名稱");
            retTable.Columns.Add("必選修");
            retTable.Columns.Add("成績");
            retTable.Columns["成績"].DataType = System.Type.GetType("System.Decimal");
            retTable.Columns.Add("學分");
            retTable.Columns["學分"].DataType = System.Type.GetType("System.Int32");

            retTable.Columns.Add("學年度");
            retTable.Columns.Add("學期");
            retTable.Columns.Add("成績年級");
            retTable.Columns.Add("重補修");
            retTable.Columns.Add("科目");
            retTable.Columns.Add("級別");
            retTable.Columns.Add("本學期修課");
            QueryHelper qh1 = new QueryHelper();
            string strSQL1 = "select student.id,class.grade_year,dept.name as deptname,class.class_name as classname,student.student_number,student.seat_no,student.name,$shinmin.retake.suggest_list.subject_content from $shinmin.retake.suggest_list join student on $shinmin.retake.suggest_list.ref_student_id = student.id left join class on student.ref_class_id=class.id left join dept on class.ref_dept_id=dept.id where $shinmin.retake.suggest_list.ref_time_list_id='" + UID + "';";
            DataTable dt1 = qh1.Select(strSQL1);

            foreach (DataRow dr in dt1.Rows)
            {
                string sid = dr["id"].ToString();
                string gYear = dr["grade_year"].ToString();
                string deptName = dr["deptname"].ToString();
                string className = dr["classname"].ToString();
                string sNum = dr["student_number"].ToString();
                int? seatno = null;
                int s;

                if (int.TryParse(dr["seat_no"].ToString(), out s))
                    seatno = s;

                string name = dr["name"].ToString();

                string subjcontent = dr["subject_content"].ToString();
                XElement elmSubj = XElement.Parse(subjcontent);
                foreach (XElement elm in elmSubj.Elements("Subject"))
                {
                    DataRow row = retTable.NewRow();
                    row["StudentID"] = sid;
                    row["年級"] = gYear;
                    row["科別"] = deptName;
                    row["班級"] = className;
                    row["學號"] = sNum;
                    if (seatno.HasValue)
                        row["座號"] = seatno.Value;

                    row["姓名"] = name;
                    row["科目名稱"] = elm.Attribute("Name").Value + GetNumber(elm.Attribute("Level").Value);
                    row["必選修"] = elm.Attribute("Required").Value;
                    decimal dd;
                    if (decimal.TryParse(elm.Attribute("Score").Value, out dd))
                        row["成績"] = dd;
                    int cc;
                    if (int.TryParse(elm.Attribute("Credit").Value, out cc))
                        row["學分"] = cc;

                    row["學年度"] = elm.Attribute("SchoolYear").Value;
                    row["學期"] = elm.Attribute("Semester").Value;
                    
                    if(elm.Attribute("GradeYear")!=null)
                        row["成績年級"] = elm.Attribute("GradeYear").Value;
                    row["重補修"] = elm.Attribute("Type").Value;
                    row["科目"] = elm.Attribute("Name").Value;
                    row["級別"] = elm.Attribute("Level").Value;
                    if(elm.Attribute("CheckCourse1") !=null)
                        row["本學期修課"] = elm.Attribute("CheckCourse1").Value;

                    retTable.Rows.Add(row);
                }
            }
            retTable.DefaultView.Sort = "年級,科別,班級,學號,座號,姓名,科目名稱,必選修,成績,學分,學年度,學期,成績年級,重補修";
            return retTable;
        
        }
        public static string GetNumber(string str)
        {
            if (str == null || str=="")
                return "";

            string levelNumber;
            switch (int.Parse(str))
            {
                #region 對應levelNumber
                case 0:
                    levelNumber = "";
                    break;
                case 1:
                    levelNumber = "Ⅰ";
                    break;
                case 2:
                    levelNumber = "Ⅱ";
                    break;
                case 3:
                    levelNumber = "Ⅲ";
                    break;
                case 4:
                    levelNumber = "Ⅳ";
                    break;
                case 5:
                    levelNumber = "Ⅴ";
                    break;
                case 6:
                    levelNumber = "Ⅵ";
                    break;
                case 7:
                    levelNumber = "Ⅶ";
                    break;
                case 8:
                    levelNumber = "Ⅷ";
                    break;
                case 9:
                    levelNumber = "Ⅸ";
                    break;
                case 10:
                    levelNumber = "Ⅹ";
                    break;
                default:
                    levelNumber = "" + str;
                    break;
                #endregion
            }
            return levelNumber;
        }

        
        /// <summary>
        /// 取得系統內目前所在班級年級學期，學生狀態一般、延修的學生是否有修課
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetStudCurrentSelectCourse()
        {
            Dictionary<string, string> retVal = new Dictionary<string, string>();
            QueryHelper qh = new QueryHelper();
            string strSQL = "select student.id,class.grade_year,course.semester,course.subject,course.subj_level from student inner join sc_attend on student.id=sc_attend.ref_student_id inner join course on sc_attend.ref_course_id=course.id inner join class on student.ref_class_id=class.id where course.school_year=" + K12.Data.School.DefaultSchoolYear + " and course.semester=" + K12.Data.School.DefaultSemester + " and student.status in(1,2)";
            DataTable dt = qh.Select(strSQL);            
            foreach (DataRow dr in dt.Rows)
            {
                string key = dr["id"].ToString() + dr["grade_year"].ToString() + dr["semester"].ToString() + dr["subject"].ToString() + dr["subj_level"].ToString();
                if (!retVal.ContainsKey(key))
                    retVal.Add(key, "是");
            }
            return retVal;
        }


        /// <summary>
        /// 取得學生(一般、延修)、班級課程規劃，需要計算且必修科目
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string,List<GraduationPlanSubject>> GetGraduationPlan()
        {          
            List<string> GraduationPlanIdList = new List<string>();
            Dictionary<string, List<GraduationPlanSubject>> retVal = new Dictionary<string, List<GraduationPlanSubject>>();

            // 學生(一般、延修)課程規劃
            QueryHelper qh1 = new QueryHelper();
            string strSQL1 = "select distinct ref_graduation_plan_id from student where status in(1,2) and ref_graduation_plan_id is not null;";
            DataTable dt1 = qh1.Select(strSQL1);

            foreach (DataRow dr in dt1.Rows)
                GraduationPlanIdList.Add(dr[0].ToString());

            // 班級課程規劃 id
            QueryHelper qh2 = new QueryHelper();
            string strSQL2 = "select distinct ref_graduation_plan_id from class where ref_graduation_plan_id is not null;";
            DataTable dt2 = qh2.Select(strSQL2);
            foreach (DataRow dr in dt2.Rows)
            {
                string id = dr[0].ToString();
                if (!GraduationPlanIdList.Contains(id))
                    GraduationPlanIdList.Add(id);
            }

            // 取得課程規劃
            QueryHelper qh3 = new QueryHelper();
            List<List<string>> gpList = new List<List<string>>();
            gpList.Add(new List<string>());
            
            int idx = 0,count=1;
            foreach (string str in GraduationPlanIdList)
            {
                if (count > 30)
                {                    
                    gpList.Add(new List<string>());
                    count = 1;
                    idx++;
                }
                gpList[idx].Add(str);

                count++;
            }
            
            foreach (List<string> da in gpList)
            {
                if (da.Count > 0)
                {
                    QueryHelper qhq = new QueryHelper();
                    string strSQLq = "select id,content from graduation_plan where id in("+string.Join(",",da.ToArray())+");";
                    DataTable dtq = qhq.Select(strSQLq);
                    foreach (DataRow dr in dtq.Rows)
                    {
                        string id = dr[0].ToString();
                        string content = dr[1].ToString();
                        if (string.IsNullOrWhiteSpace(content))
                            continue;

                        XElement elmRoot = XElement.Parse(content);
                        List<GraduationPlanSubject> gpsList = new List<GraduationPlanSubject>();
                        foreach (XElement elm in elmRoot.Elements("Subject"))
                        {
                            // 要計算成績且必修
                            if (elm.Attribute("NotIncludedInCredit").Value == "False" && elm.Attribute("Required").Value == "必修")
                            {
                                GraduationPlanSubject gps = new GraduationPlanSubject();                                
                                gps.GradeYear = int.Parse(elm.Attribute("GradeYear").Value);
                                gps.Level = elm.Attribute("Level").Value;
                                gps.Semester = int.Parse(elm.Attribute("Semester").Value);
                                gps.SubjectName = elm.Attribute("SubjectName").Value;
                                gps.Credit = int.Parse(elm.Attribute("Credit").Value);
                                gps.strRequire = elm.Attribute("Required").Value;
                                gps.def1 = elm.Attribute("RequiredBy").Value;
                                gpsList.Add(gps);
                            }
                        }
                        retVal.Add(id, gpsList);                        
                    }
                }
            }
            return retVal;                   
        }


        /// <summary>
        /// 取得目前期間內資料庫建議科別人數統計
        /// </summary>
        /// <returns></returns>
        public static List<SuggestSubjectCount> GetSuggestSubjectCountList()
        {
            Dictionary<string, SuggestSubjectCount> ValDict = new Dictionary<string, SuggestSubjectCount>();
            QueryHelper qh1 = new QueryHelper();
            string strSQL1 = "select dept.name,$shinmin.retake.suggest_list.subject_content from $shinmin.retake.time_list inner join $shinmin.retake.suggest_list on $shinmin.retake.time_list.uid=$shinmin.retake.suggest_list.ref_time_list_id  inner join student on $shinmin.retake.suggest_list.ref_student_id = student.id left join class on student.ref_class_id=class.id left join dept on class.ref_dept_id=dept.id where $shinmin.retake.time_list.active='true';";
            DataTable dt1 = qh1.Select(strSQL1);
            foreach (DataRow dr in dt1.Rows)
            {
                string dpName = dr[0].ToString();
                string subjcontent = dr["subject_content"].ToString();
                XElement elmSubj = XElement.Parse(subjcontent);
                foreach (XElement elm in elmSubj.Elements("Subject"))
                {
                    SuggestSubjectCount ssc = new SuggestSubjectCount();
                    ssc.DeptName = dpName;
                    
                    int cc;
                    if (int.TryParse(elm.Attribute("Credit").Value, out cc))
                        ssc.Credit = cc;

                    ssc.SubjectName = elm.Attribute("Name").Value;
                    int ll;
                    if(int.TryParse(elm.Attribute("Level").Value,out ll))
                        ssc.Level=ll;

                    string key=ssc.GetKey();

                    if(ValDict.ContainsKey(key))
                        ValDict[key].Count++;
                    else
                        ValDict.Add(key,ssc);
                }
            }
            return ValDict.Values.ToList();
        }


        /// <summary>
        /// 取得所有科別名稱
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllDeptName()
        {
            List<string> retVal = new List<string>();
            QueryHelper qh1 = new QueryHelper();
            string strSQL1 = "select name from dept order by name asc";
            DataTable dt1 = qh1.Select(strSQL1);
            foreach (DataRow dr in dt1.Rows)
            {
                string deptName=dr[0].ToString();
                if (!retVal.Contains(deptName))
                    retVal.Add(deptName);
            }
            return retVal;
        }

        /// <summary>
        /// 取得目開課科目
        /// </summary>
        /// <returns></returns>
        public static List<SubjectCourseBase> GetSubjectCourseBaseList()
        {
            List<SubjectCourseBase> retVal = new List<SubjectCourseBase>();
            // 課表名稱對照
            Dictionary<string, string> CourseTimeTableNameDict = new Dictionary<string, string>();
            foreach (UDTCourseTimetableDef data in UDTTransfer.UDTCourseTimetableSelectAll())
                CourseTimeTableNameDict.Add(data.UID, data.Name);

            Dictionary<string, SubjectCourseBase> dataDict = new Dictionary<string, SubjectCourseBase>();
            // 比對使用科目名稱+級別
            // 取得正在期間的科目資料
            QueryHelper qh1 = new QueryHelper();
            string strQry1 = "select $shinmin.retake.subject.school_year,$shinmin.retake.subject.semester,$shinmin.retake.subject.month,subject_name,subject_level,dept_name,subject_type,course_timetable_id,credit,period_content from $shinmin.retake.subject inner join $shinmin.retake.time_list on $shinmin.retake.subject.school_year=$shinmin.retake.time_list.school_year and $shinmin.retake.subject.semester=$shinmin.retake.time_list.semester and $shinmin.retake.subject.month=$shinmin.retake.time_list.month where $shinmin.retake.time_list.active='true';";
            DataTable dt1 = qh1.Select(strQry1);
            foreach (DataRow dr in dt1.Rows)
            {
                SubjectCourseBase scb = new SubjectCourseBase();
                scb.SchoolYear = int.Parse(dr["school_year"].ToString());
                scb.Semester = int.Parse(dr["semester"].ToString());
                scb.Month = int.Parse(dr["month"].ToString());
                scb.SubjectName = dr["subject_name"].ToString();
                if (dr["subject_level"] != null && dr["subject_level"].ToString() != "")
                    scb.SubjectLevel = int.Parse(dr["subject_level"].ToString());
                scb.DeptName = dr["dept_name"].ToString();

                string cbid = dr["course_timetable_id"].ToString();
                if (CourseTimeTableNameDict.ContainsKey(cbid))
                    scb.CourseTimeTable = CourseTimeTableNameDict[cbid];

                scb.SubjectType = dr["subject_type"].ToString();
                scb.Credit = int.Parse(dr["credit"].ToString());

                if (!string.IsNullOrWhiteSpace(dr["period_content"].ToString()))
                    scb.PeriodXml = XElement.Parse(dr["period_content"].ToString());

                string key1="";
                if (scb.SubjectLevel.HasValue)
                    key1 = scb.SubjectName + "_" + scb.SubjectLevel.Value;
                else
                    key1 = scb.SubjectName;

                if (!dataDict.ContainsKey(key1))
                    dataDict.Add(key1, scb);
            }

            
            // 取得正在期間學生建議名單
            QueryHelper qh2 = new QueryHelper();
            string strQry2 = "select ref_student_id,$shinmin.retake.ssselect.subject_name,$shinmin.retake.ssselect.subject_level from $shinmin.retake.ssselect inner join $shinmin.retake.time_list on $shinmin.retake.ssselect.school_year=$shinmin.retake.time_list.school_year and $shinmin.retake.ssselect.semester=$shinmin.retake.time_list.semester and $shinmin.retake.ssselect.month=$shinmin.retake.time_list.month where $shinmin.retake.time_list.active='true';";
            DataTable dt2 = qh2.Select(strQry2);
            // 檢查是否重複加入
            Dictionary<string, List<int>> checkStudDict = new Dictionary<string, List<int>>();
            foreach (DataRow dr in dt2.Rows)
            {
                int sid = int.Parse(dr["ref_student_id"].ToString());
            

                // 比對資料後加入StudentID                
                string key2 =dr["subject_name"].ToString() +"_"+ dr["subject_level"].ToString();

                        if (dataDict.ContainsKey(key2))
                        {
                            if (!checkStudDict.ContainsKey(key2))
                                checkStudDict.Add(key2, new List<int>());

                            if (!checkStudDict[key2].Contains(sid))
                            {
                                SubjectCourseStudentBase scsb = new SubjectCourseStudentBase();
                                scsb.StudentID = sid;
                                dataDict[key2].StudentIDList.Add(scsb);
                                checkStudDict[key2].Add(sid);
                            }
                        }
            }

            foreach (SubjectCourseBase scb in dataDict.Values)
                retVal.Add(scb);

            return retVal;
        }

        /// <summary>
        /// 取得目前已有課程名稱
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetCourseNameNowDict()
        {
            Dictionary<string, string> retVal = new Dictionary<string, string>();
            QueryHelper qh = new QueryHelper();
            string strQry = "select $shinmin.retake.course.course_name,$shinmin.retake.course.uid from $shinmin.retake.time_list inner join $shinmin.retake.course on $shinmin.retake.time_list.school_year=$shinmin.retake.course.school_year and $shinmin.retake.time_list.semester=$shinmin.retake.course.semester and $shinmin.retake.time_list.month=$shinmin.retake.course.month where active='true';";
            DataTable dt = qh.Select(strQry);
            foreach (DataRow dr in dt.Rows)
            { 
                string key=dr[0].ToString();
                if (!retVal.ContainsKey(key))
                    retVal.Add(key, dr[1].ToString());
            }
            return retVal;
        }

        /// <summary>
        /// 取得目前學年度、學期、月分所屬課程UID
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCourseSelectActive1()
        {
            List<string> retVal = new List<string>();
            QueryHelper qh = new QueryHelper();            
            string strQry = "select $shinmin.retake.course.uid from $shinmin.retake.time_list inner join $shinmin.retake.course on $shinmin.retake.time_list.school_year=$shinmin.retake.course.school_year and $shinmin.retake.time_list.semester=$shinmin.retake.course.semester and $shinmin.retake.time_list.month=$shinmin.retake.course.month where active='true';";
            DataTable dt = qh.Select(strQry);
            foreach (DataRow dr in dt.Rows)
                retVal.Add(dr[0].ToString());

            return retVal;
        }

        /// <summary>
        /// 取得學生科別
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> GetStudentAllDeptName()
        {
            Dictionary<int, string> retVal = new Dictionary<int, string>();
            QueryHelper qh = new QueryHelper();
            string strQry = "select student.id,dept.name from student inner join class on student.ref_class_id=class.id inner join dept on class.ref_dept_id=dept.id;";
            DataTable dt = qh.Select(strQry);
            foreach (DataRow dr in dt.Rows)
            {
                int sid = int.Parse(dr[0].ToString());
                string deptName = dr[1].ToString();               

                retVal.Add(sid, deptName);
            }
            return retVal;
        }

        /// <summary>
        /// 取得學生年級科別名稱
        /// </summary>
        /// <param name="StudentIDList"></param>
        /// <returns></returns>
        public static Dictionary<string, DataRow> GetStudentGradeDeptDictByStudentIDList(List<string> StudentIDList)
        {
            Dictionary<string, DataRow> retVal = new Dictionary<string, DataRow>();
            QueryHelper qh = new QueryHelper();
            if (StudentIDList.Count > 0)
            {
                string strQry = "select student.id,class.grade_year,dept.name from student left join class on  student.ref_class_id=class.id left join dept on class.ref_dept_id=dept.id where class.grade_year is not null and Dept.name <>'' and student.id in(" + string.Join(",", StudentIDList.ToArray()) + ")";
                DataTable dt = qh.Select(strQry);
                foreach (DataRow dr in dt.Rows)
                {
                    string sid = dr[0].ToString();
                    if (!retVal.ContainsKey(sid))
                        retVal.Add(sid, dr);
                }

            }
            return retVal;
        }

        /// <summary>
        /// 扣過學生ID取得扣考課程ID
        /// </summary>
        /// <param name="StudentIDList"></param>
        /// <returns></returns>
        public static Dictionary<string, List<int>> GetStudentNotExamCoID(List<string> StudentIDList)
        {
            Dictionary<string, List<int>> retVal = new Dictionary<string, List<int>>();
            if (StudentIDList.Count > 0)
            {
                QueryHelper qh = new QueryHelper();
                string query = "select ref_student_id,ref_course_id  from $shinmin.retake.scselect where ref_student_id in("+string.Join(",",StudentIDList.ToArray())+") and not_exam=true;";
                DataTable dt = qh.Select(query);
                foreach (DataRow dr in dt.Rows)
                {
                    string sid = dr["ref_student_id"].ToString();
                    int cid = int.Parse(dr["ref_course_id"].ToString());
                    if (!retVal.ContainsKey(sid))
                        retVal.Add(sid, new List<int>());

                    retVal[sid].Add(cid);
                }
            }
            return retVal;
        }

    }
}
