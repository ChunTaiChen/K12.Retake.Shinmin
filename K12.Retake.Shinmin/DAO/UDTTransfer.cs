using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;
using K12.Data;
using FISCA.DSAClient;
using FISCA.DSAUtil;

namespace K12.Retake.Shinmin.DAO
{
    /// <summary>
    /// 處理 UDT 資料交換使用
    /// </summary>
    public class UDTTransfer
    {
        /// <summary>
        /// 透過期間ID取得該期間建議重補修清單
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static List<UDTSuggestListDef> UDTSuggestListSelectByTimeListID(string UID)
        {
            List<UDTSuggestListDef> retVal = new List<UDTSuggestListDef>();
            AccessHelper accessHelper = new AccessHelper();
            string qry = "ref_time_list_id="+UID;
            retVal = accessHelper.Select<UDTSuggestListDef>(qry);
            return retVal;
        }

        /// <summary>
        /// 新增多筆建議重補修名單
        /// </summary>
        /// <param name="dataList"></param>
        public static void UDTSuggestListInsert(List<UDTSuggestListDef> dataList)
        {
            if (dataList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                accessHelper.InsertValues(dataList);
            }        
        }

        /// <summary>
        /// 刪除多筆建議重修名單
        /// </summary>
        /// <param name="dataList"></param>
        public static void UDTSuggestListDelete(List<UDTSuggestListDef> dataList)
        {
            if (dataList.Count > 0)
            {
                foreach (UDTSuggestListDef data in dataList)
                    data.Deleted = true;

                AccessHelper accessHelper = new AccessHelper();
                accessHelper.DeletedValues(dataList);
            
            }
        }

        /// <summary>
        /// 取得所以有重補修期間
        /// </summary>
        /// <returns></returns>
        public static List<UDTTimeListDef> UDTTimeListSelectAll()
        {
            List<UDTTimeListDef> retVal = new List<UDTTimeListDef>();
            AccessHelper accessHelper = new AccessHelper();
            retVal = accessHelper.Select<UDTTimeListDef>();
            return retVal;        
        }

        /// <summary>
        /// 新增多筆重補修期間
        /// </summary>
        /// <param name="dataList"></param>
        public static void UDTTimeListInsert(List<UDTTimeListDef> dataList)
        {
            if (dataList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                accessHelper.InsertValues(dataList);
            }        
        }

        /// <summary>
        /// 更新多筆重補修期間
        /// </summary>
        /// <param name="dataList"></param>
        public static void UDTTimeListUpdate(List<UDTTimeListDef> dataList)
        {
            if (dataList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                accessHelper.UpdateValues(dataList);
            }
        }

        /// <summary>
        /// 取得目前期間
        /// </summary>
        /// <returns></returns>
        public static UDTTimeListDef UDTTimeListGetActiveTrue1()
        {
            UDTTimeListDef retVal = new UDTTimeListDef();
            List<UDTTimeListDef> dataList = new List<UDTTimeListDef>();
            AccessHelper accessHelper = new AccessHelper();
            string qry = "active='true'";                
            dataList = accessHelper.Select<UDTTimeListDef>(qry);
            foreach (UDTTimeListDef data in dataList)
            {
                if (data.Active)
                {
                    retVal = data;
                    break;
                }
            }
            return retVal;
        }


        /// <summary>
        /// 刪除多筆重補修期間
        /// </summary>
        /// <param name="dataList"></param>
        public static void UDTTimeListDelete(List<UDTTimeListDef> dataList)
        {
            if (dataList.Count > 0)
            {
                foreach (UDTTimeListDef data in dataList)
                    data.Deleted = true;

                AccessHelper accessHelper = new AccessHelper();
                accessHelper.DeletedValues(dataList);
            }
        }

        /// <summary>
        /// 傳入UID設定目前學期
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public static void UDTTimeListSetActiveTrue(string UID)
        {
            // 更新目前學期：傳入需要修改UID編號，讀取所有資料將非UID編號Active全設成false，update 更新資料。
            List<UDTTimeListDef> updateVal = new List<UDTTimeListDef>();

            // select 
            AccessHelper accSelect = new AccessHelper();
            updateVal = accSelect.Select<UDTTimeListDef>();

            if (updateVal.Count > 0)
            {
                foreach (UDTTimeListDef data in updateVal)
                {
                    if (data.UID == UID)
                        data.Active = true;
                    else
                        data.Active = false;
                }
                // update
                AccessHelper accUpdate = new AccessHelper();
                accUpdate.UpdateValues(updateVal);
            }            
        }

        /// <summary>
        /// 新增多筆科目
        /// </summary>
        /// <param name="dataList"></param>
        public static void UDTSubjectInsert(List<UDTSubjectDef> dataList)
        {
            if (dataList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                accessHelper.InsertValues(dataList);
            }
        }

        /// <summary>
        /// 更新多筆科目
        /// </summary>
        /// <param name="dataList"></param>
        public static void UDTSubjectUpdate(List<UDTSubjectDef> dataList)
        {
            if (dataList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                accessHelper.UpdateValues(dataList);
            }        
        }

        /// <summary>
        /// 刪除多筆科目
        /// </summary>
        /// <param name="dataList"></param>
        public static void UDTSubjectDelete(List<UDTSubjectDef> dataList)
        {
            if (dataList.Count > 0)
            {
                foreach (UDTSubjectDef data in dataList)
                    data.Deleted = true;

                AccessHelper accessHelper = new AccessHelper();
                accessHelper.DeletedValues(dataList);
            }
        }

        /// <summary>
        /// 透過學年度、學期、月份，取得相關科目
        /// </summary>
        /// <param name="SchoolYear"></param>
        /// <param name="Semester"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public static List<UDTSubjectDef> UDTSubjectSelectByP1(int SchoolYear, int Semester, int Month)
        {
            List<UDTSubjectDef> retVal = new List<UDTSubjectDef>();
            AccessHelper accessHelper = new AccessHelper();
            string qry = "school_year="+SchoolYear+" and semester="+Semester+" and month="+Month;
            retVal = accessHelper.Select<UDTSubjectDef>(qry);
            return retVal;
        }

        /// <summary>
        /// 取得所有課表資名稱
        /// </summary>
        /// <returns></returns>
        public static List<UDTCourseTimetableDef> UDTCourseTimetableSelectAll()
        {
            List<UDTCourseTimetableDef> retVal = new List<UDTCourseTimetableDef>();
            AccessHelper accessHelper = new AccessHelper();
            retVal = accessHelper.Select<UDTCourseTimetableDef>();
            return retVal;
        }

        /// <summary>
        /// 新增多筆課表
        /// </summary>
        /// <param name="dataList"></param>
        public static void UDTCourseTimetableInsert(List<UDTCourseTimetableDef> dataList)
        {
            if (dataList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                accessHelper.InsertValues(dataList);
            }
        }

        /// <summary>
        /// 更新多筆課表
        /// </summary>
        /// <param name="dataList"></param>
        public static void UDTCourseTimetableUpdate(List<UDTCourseTimetableDef> dataList)
        {
            if (dataList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                accessHelper.UpdateValues(dataList);
            }
        }

        /// <summary>
        /// 刪除多筆課表
        /// </summary>
        /// <param name="dataList"></param>
        public static void UDTCourseTimetableDelete(List<UDTCourseTimetableDef> dataList)
        {
            if (dataList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                foreach (UDTCourseTimetableDef data in dataList)
                    data.Deleted = true;
                    
                accessHelper.DeletedValues(dataList);
            }
        }

        /// <summary>
        /// 透過課表ID取得課表與科別資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<UDTCdselectDef> UDTCdselectSelectByCTimeTableID(string id)
        { 
            List<UDTCdselectDef> retVal = new List<UDTCdselectDef> ();
            AccessHelper accessHelper = new AccessHelper();
            string qry="ref_course_timetable_id='"+id+"'";
            retVal = accessHelper.Select<UDTCdselectDef>(qry);
            return retVal;
        }

        /// <summary>
        /// 取得所有課表與科別資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<UDTCdselectDef> UDTCdselectSelectAll()
        {
            List<UDTCdselectDef> retVal = new List<UDTCdselectDef>();
            AccessHelper accessHelper = new AccessHelper();            
            retVal = accessHelper.Select<UDTCdselectDef>();
            return retVal;
        }

        /// <summary>
        /// 新增多筆課表與科別資料
        /// </summary>
        /// <param name="dataList"></param>
        public static void UDTCdselectInsert(List<UDTCdselectDef> dataList)
        {
            if (dataList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                accessHelper.InsertValues(dataList);
            }
        }

        /// <summary>
        /// 更新多筆課表與科別資料
        /// </summary>
        /// <param name="dataList"></param>
        public static void UDTCdselectUpdate(List<UDTCdselectDef> dataList)
        {
            if (dataList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                accessHelper.UpdateValues(dataList);
            }
        }

        /// <summary>
        /// 刪除多筆課表與科別資料
        /// </summary>
        /// <param name="dataList"></param>
        public static void UDTCdselectDelete(List<UDTCdselectDef> dataList)
        {
            if (dataList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                foreach (UDTCdselectDef data in dataList)
                    data.Deleted = true;

                accessHelper.DeletedValues(dataList);
            }
        }

        /// <summary>
        /// 取得所有課表與科別對照
        /// </summary>
        /// <returns></returns>
        public static List<CourseTableDept> GetAllCourseTableDeptList()
        {
            List<CourseTableDept> retVal = new List<CourseTableDept>();

            List<UDTCourseTimetableDef> data1 = UDTCourseTimetableSelectAll();
            Dictionary<int, string> dict1 = new Dictionary<int, string>();
            foreach (UDTCourseTimetableDef data in data1)
                dict1.Add(int.Parse(data.UID), data.Name);

            foreach (UDTCdselectDef data in UDTCdselectSelectAll())
            {
                CourseTableDept ctd = new CourseTableDept();
                ctd.CourseTableID = data.RefCourseTimetableID;
                if (dict1.ContainsKey(data.RefCourseTimetableID))
                    ctd.CourseTableName = dict1[data.RefCourseTimetableID];
                ctd.DeptName = data.DeptName;
                retVal.Add(ctd);
            }           

            return retVal;
        }

        /// <summary>
        /// 取得課程資料
        /// </summary>
        /// <param name="IDList"></param>
        /// <returns></returns>
        public static List<UDTCourseDef> UDTCourseSelectUIDs(List<string> IDList)
        {
            List<UDTCourseDef> retVal = new List<UDTCourseDef>();
            if (IDList.Count > 0)
            {
                AccessHelper accessHepler = new AccessHelper();
                string qry = "uid in("+string.Join(",",IDList.ToArray())+")";
                retVal=accessHepler.Select<UDTCourseDef>(qry);
            }
            return retVal;
        }

        /// <summary>
        /// 取得所有課程
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, UDTCourseDef> UDTCourseSelectAllDict()
        {
            Dictionary<string, UDTCourseDef> retVal = new Dictionary<string, UDTCourseDef>();
            AccessHelper accessHepler = new AccessHelper();
            List<UDTCourseDef> dataList = accessHepler.Select<UDTCourseDef>();
            foreach (UDTCourseDef data in dataList)
                retVal.Add(data.UID, data);

            return retVal;
        }

        /// <summary>
        /// 取得課程,依學年度、學期、梯次
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, UDTCourseDef> UDTCourseSelectBySchoolYearSMDict(int SchoolYear,int Semester,int Month)
        {
            Dictionary<string, UDTCourseDef> retVal = new Dictionary<string, UDTCourseDef>();
            AccessHelper accessHepler = new AccessHelper();
            string qry = "school_year="+SchoolYear+" and semester="+Semester+" and month="+Month;
            List<UDTCourseDef> dataList = accessHepler.Select<UDTCourseDef>(qry);
            foreach (UDTCourseDef data in dataList)
                retVal.Add(data.UID, data);

            return retVal;
        }
        
        /// <summary>
        /// 新增多筆課程
        /// </summary>
        /// <param name="DataList"></param>
        public static List<string> UDTCourseInsert(List<UDTCourseDef> DataList)
        {
            List<string> retVal = new List<string>();
            if (DataList.Count > 0)
            {
                AccessHelper accessHepler = new AccessHelper();
                retVal= accessHepler.InsertValues(DataList);
            }
            return retVal;
        }

        /// <summary>
        /// 更新多筆課程
        /// </summary>
        /// <param name="DataList"></param>
        public static void UDTCourseUpdate(List<UDTCourseDef> DataList)
        {
            if (DataList.Count > 0)
            {
                AccessHelper accessHepler = new AccessHelper();
                accessHepler.UpdateValues(DataList);
            }
        }

        /// <summary>
        /// 刪除多筆課程
        /// </summary>
        /// <param name="DataList"></param>
        public static void UDTCourseDelete(List<UDTCourseDef> DataList)
        {
            if (DataList.Count > 0)
            {
                AccessHelper accessHepler = new AccessHelper();
                foreach (UDTCourseDef data in DataList)
                    data.Deleted = true;
                accessHepler.DeletedValues(DataList);
            }
        }

        /// <summary>
        /// 透過課程編號取得時間區間
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static List<UDTTimeSectionDef> UDTTimeSectionSelectByCourseIDList(List<string> CourseIDList)
        {
            List<UDTTimeSectionDef> retVal = new List<UDTTimeSectionDef>();
            if (CourseIDList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                string qry = "ref_course_id in(" + string.Join(",", CourseIDList.ToArray()) + ")";
                retVal = accessHelper.Select<UDTTimeSectionDef>(qry);
            }
            return retVal;
        }

        /// <summary>
        /// 新增多筆時間區間
        /// </summary>
        /// <param name="DataList"></param>
        public static void UDTTimeSectionInsert(List<UDTTimeSectionDef> DataList)
        {
            if (DataList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                accessHelper.InsertValues(DataList);
            }
        }

        /// <summary>
        /// 刪除多筆時間區間
        /// </summary>
        /// <param name="DataList"></param>
        public static void UDTTimeSectionDelete(List<UDTTimeSectionDef> DataList)
        {
            if (DataList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                foreach (UDTTimeSectionDef data in DataList)
                    data.Deleted = true;

                accessHelper.DeletedValues(DataList);
            }
        }

        /// <summary>
        /// 新增多筆修課
        /// </summary>
        /// <param name="DataList"></param>
        public static void UDTSCSelectInsert(List<UDTScselectDef> DataList)
        {            
            if (DataList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                accessHelper.InsertValues(DataList);
            }
         
        }

        /// <summary>
        /// 更新多筆修課
        /// </summary>
        /// <param name="DataList"></param>
        public static void UDTSCSelectUpdate(List<UDTScselectDef> DataList)
        {
            if (DataList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                accessHelper.UpdateValues(DataList);
            }
        }

        /// <summary>
        /// 刪除多筆修課
        /// </summary>
        /// <param name="DataList"></param>
        public static void UDTSCSelectDelete(List<UDTScselectDef> DataList)
        {
            if (DataList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                foreach (UDTScselectDef data in DataList)
                    data.Deleted = true;
                accessHelper.DeletedValues(DataList);
            }
        }

        /// <summary>
        /// 透過課程ID取得多筆修課資料
        /// </summary>
        /// <param name="DataList"></param>
        /// <returns></returns>
        public static List<UDTScselectDef> UDTSCSelectByCourseIDList(List<string> DataList)
        {
            List<UDTScselectDef> retVal = new List<UDTScselectDef>();
            if (DataList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                string qry = "ref_course_id in("+string.Join(",",DataList.ToArray())+")";
                retVal = accessHelper.Select<UDTScselectDef>(qry);
            }
            return retVal;
        }


        /// <summary>
        /// 取得所有選學生選修科目清單
        /// </summary>
        /// <returns></returns>
        public static List<UDTSsselectDef> UDTSsselectSelectAll()
        {
            List<UDTSsselectDef> retVal = new List<UDTSsselectDef>();
            AccessHelper accessHelper = new AccessHelper();
            retVal = accessHelper.Select<UDTSsselectDef>();
            return retVal;
        }

        /// <summary>
        /// 學生選修科目清單(預設載入)
        /// </summary>
        public static void UDTSsselectLoad()
        {            
            AccessHelper accessHelper = new AccessHelper();
            string qry = "uid=1";
            accessHelper.Select<UDTSsselectDef>(qry);        
        }

        /// <summary>
        /// 新增學生選修科目清單
        /// </summary>
        /// <param name="dataList"></param>
        public static void UDTSsselectInsert(List<UDTSsselectDef> dataList)
        {
            if (dataList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                accessHelper.InsertValues(dataList);
            }        
        }

        /// <summary>
        /// 更新學生選修科目清單
        /// </summary>
        /// <param name="dataList"></param>
        public static void UDTSsselectUpdate(List<UDTSsselectDef> dataList)
        {
            if (dataList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                accessHelper.UpdateValues(dataList);
            }
        }

        /// <summary>
        /// 刪除學生選修科目清單
        /// </summary>
        /// <param name="dataList"></param>
        public static void UDTSsselectDelete(List<UDTSsselectDef> dataList)
        {
            if (dataList.Count > 0)
            {
                foreach (UDTSsselectDef data in dataList)
                    data.Deleted = true;

                AccessHelper accessHelper = new AccessHelper();
                accessHelper.DeletedValues(dataList);
            }
        }



        /// <summary>
        /// 透過課程ID取得課程缺曠
        /// </summary>
        /// <param name="CourseIDList"></param>
        /// <returns></returns>
        public static List<UDTAttendanceDef> UDTAttendanceSelectByCourseIDList(List<string> CourseIDList)
        {
            List<UDTAttendanceDef> retVal = new List<UDTAttendanceDef>();            
            if (CourseIDList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                string query = "ref_course_id in ("+string.Join(",",CourseIDList.ToArray())+")";
                retVal = accessHelper.Select<UDTAttendanceDef>(query);
            }
            return retVal;        
        }

        /// <summary>
        /// 透過學生ID取得課程缺曠
        /// </summary>
        /// <param name="CourseIDList"></param>
        /// <returns></returns>
        public static List<UDTAttendanceDef> UDTAttendanceSelectByStudentIDList(List<string> StudentIDList)
        {
            List<UDTAttendanceDef> retVal = new List<UDTAttendanceDef>();
            if (StudentIDList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                string query = "ref_student_id in (" + string.Join(",", StudentIDList.ToArray()) + ")";
                retVal = accessHelper.Select<UDTAttendanceDef>(query);
            }
            return retVal;
        }

        /// <summary>
        /// 刪除多筆課程缺曠資料
        /// </summary>
        /// <param name="dataList"></param>
        public static void UDTAttendanceDelete(List<UDTAttendanceDef> dataList)
        {
            if (dataList.Count > 0)
            {
                foreach (UDTAttendanceDef data in dataList)
                    data.Deleted = true;

                AccessHelper accessHelper = new AccessHelper();
                accessHelper.DeletedValues(dataList);
            }        
        }

        /// <summary>
        /// 新增成績計算比例原則
        /// </summary>
        /// <param name="dadaList"></param>
        public static void UDTWeightProportionInsert(List<UDTWeightProportionDef> dataList)
        {
            AccessHelper _accessHelper = new AccessHelper();
            _accessHelper.InsertValues(dataList);
        }

        /// <summary>
        /// 取得成績計算比例原則
        /// </summary>
        /// <param name="dadaList"></param>
        public static List<UDTWeightProportionDef>  UDTWeightProportionSelect()
        {
            List<UDTWeightProportionDef> retVal = new List<UDTWeightProportionDef>();
            AccessHelper _accessHelper = new AccessHelper();
            retVal = _accessHelper.Select<UDTWeightProportionDef>();
            return retVal;
        }

        /// <summary>
        /// 刪除成績計算比例原則
        /// </summary>
        /// <param name="dataList"></param>
        public static void UDTWeightProportionDelete(List<UDTWeightProportionDef> dataList)
        {
            foreach (UDTWeightProportionDef data in dataList)
                data.Deleted = true;

            AccessHelper _accessHelper = new AccessHelper();
                _accessHelper.DeletedValues(dataList);
        }

        /// <summary>
        /// 透過重補修課程ID取得成績相關資料
        /// </summary>
        /// <param name="CourseIDList"></param>
        /// <returns></returns>
        public static List<StudentResult> GetStudentResultListByCourseIDList(List<string> CourseIDList)
        {
            List<StudentResult> retVal = new List<StudentResult>();
            if (CourseIDList.Count > 0)
            {
                // 取得課程
                Dictionary<int, UDTCourseDef> CourseDict = new Dictionary<int, UDTCourseDef>();                
                foreach (UDTCourseDef data in UDTCourseSelectUIDs(CourseIDList))
                    CourseDict.Add(int.Parse(data.UID), data);
                
                // 取得修課
                List<UDTScselectDef> SSelectList = UDTSCSelectByCourseIDList(CourseIDList);

                // 取得修課學生ID
                List<string> StudIDList = new List<string>();
                foreach (UDTScselectDef data in SSelectList)
                {
                    string sid = data.StudentID.ToString();
                    if (!StudIDList.Contains(sid))
                        StudIDList.Add(sid);
                }

                // 取得學生基本資料
                Dictionary<string, StudentRecord> StudRecDict = new Dictionary<string, StudentRecord>();
                foreach (StudentRecord data in Student.SelectByIDs(StudIDList))
                    StudRecDict.Add(data.ID, data);

                // 取得班級資料
                Dictionary<string, ClassRecord> ClassRecDict = new Dictionary<string, ClassRecord>();
                foreach (ClassRecord data in Class.SelectAll())
                    ClassRecDict.Add(data.ID, data);
                
                // 組成資料
                foreach (UDTScselectDef data in SSelectList)
                {
                    StudentResult sr = new StudentResult();

                    // 課程資料
                    if (CourseDict.ContainsKey(data.CourseID))
                        sr.CourseRec = CourseDict[data.CourseID];

                    // 修課
                    sr.ScselectRec = data;
                    sr.StudentID = data.StudentID.ToString();
                    sr.StudentIDi = data.StudentID;
                    
                    // 學生資料
                    if (StudRecDict.ContainsKey(sr.StudentID))
                        sr.StudentRec = StudRecDict[sr.StudentID];
                    
                    // 班級
                    if(ClassRecDict.ContainsKey(StudRecDict[sr.StudentID].RefClassID))
                        sr.ClassRec = ClassRecDict[StudRecDict[sr.StudentID].RefClassID];

                    sr.Score1 = data.SubScore1;
                    sr.Score2 = data.SubScore2;
                    sr.Score3 = data.SubScore3;                    

                    retVal.Add(sr);
                }
            }
            return retVal;
        }

        /// <summary>
        /// 新增 成績輸入時間
        /// </summary>
        /// <param name="dataList"></param>
        public static void UDTScoreInputDateInsert(List<UDTScoreInputDateDef> dataList)
        {
            AccessHelper accessHelper = new AccessHelper();
            accessHelper.InsertValues(dataList);
        }

        /// <summary>
        /// 取得 成績輸入時間
        /// </summary>
        /// <param name="dataList"></param>
        public static List<UDTScoreInputDateDef> UDTScoreInputDateSelect()
        {
            List<UDTScoreInputDateDef> retVal = new List<UDTScoreInputDateDef> ();
            AccessHelper accessHelper = new AccessHelper();
            retVal = accessHelper.Select<UDTScoreInputDateDef>();
            return retVal;
        }


        /// <summary>
        /// 刪除 成績輸入時間
        /// </summary>
        /// <param name="dataList"></param>
        public static void UDTScoreInputDateDelete(List<UDTScoreInputDateDef> dataList)
        {
            AccessHelper accessHelper = new AccessHelper();
            foreach (UDTScoreInputDateDef data in dataList)
                data.Deleted = true;

            accessHelper.DeletedValues(dataList);
        }

           /// <summary>
        /// 建立使用到的 UDT Table
        /// </summary>
        public static void CreateRetakeUDTTable()
        {
            // 重補修
            FISCA.UDT.SchemaManager Manager = new SchemaManager(new DSConnection(FISCA.Authentication.DSAServices.DefaultDataSource));

            Manager.SyncSchema(new UDTAttendanceDef());
            Manager.SyncSchema(new UDTCdselectDef());
            Manager.SyncSchema(new UDTCourseDef());
            Manager.SyncSchema(new UDTCourseTimetableDef());
            Manager.SyncSchema(new UDTExamDef());
            Manager.SyncSchema(new UDTFailSubjectListDef());

            Manager.SyncSchema(new UDTScselectDef());
            Manager.SyncSchema(new UDTSsselectDef());
            Manager.SyncSchema(new UDTSubjectDef());
            Manager.SyncSchema(new UDTSuggestListDef());
            Manager.SyncSchema(new UDTTimeListDef());
            Manager.SyncSchema(new UDTTimeSectionDef());
            Manager.SyncSchema(new UDTWeightProportionDef());            
        }
    }
}
