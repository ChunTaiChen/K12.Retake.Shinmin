using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.DocumentValidator;
using Campus.Import;
using Campus.Validator;
using FISCA.Data;
using FISCA.UDT;
using K12.Retake.Shinmin.DAO;

namespace K12.Retake.Shinmin
{
    /// <summary>
    /// 匯入排課課程資料
    /// </summary>
    public class ImportSCAttend : ImportWizard
    {
        private const string constCourseName = "課程名稱";
        private const string constSchoolYear = "學年度";
        private const string constSemester = "學期";
        private const string constMonth = "梯次";
        private const string constStudentNumber = "學號";
        private const string constSeatNo = "課程座號";
        private const string constCourseType = "重補修";

        private StringBuilder mstrLog = new StringBuilder();
        private ImportOption mOption;
        private Dictionary<string, string> mCourseNameIDs;
        private Dictionary<string, string> mStudentNumberIDs;
        private List<UDTScselectDef> mSCAttends = new List<UDTScselectDef>();
        private List<string> Conditions = new List<string>();
        private Task mTask;
        private AccessHelper mHelper;

        private static string SelectIDCondition(string TableName, string Condition)
        {
            QueryHelper helper = new QueryHelper();

            DataTable Table = helper.Select("select uid from " + TableName + " where " + Condition);

            List<string> IDs = new List<string>();

            foreach (DataRow Row in Table.Rows)
                IDs.Add(Row.Field<string>("uid"));

            string strUDTCondition = string.Join(",", IDs.Select(x => "'" + x + "'"));

            return string.IsNullOrWhiteSpace(strUDTCondition) ? string.Empty : "uid in (" + strUDTCondition + ")";
        }

        public ImportSCAttend()
        {
            Initial();

            this.CustomValidate = (Rows, Messages) =>
            {
                List<string> Conditions = new List<string>();

                foreach (IRowStream Row in Rows)
                {
                    string CourseName = Row.GetValue(constCourseName);
                    string SchoolYear = Row.GetValue(constSchoolYear);
                    string Semester = Row.GetValue(constSemester);
                    string Month = Row.GetValue(constMonth);

                    //根據課程名稱、學年度及學期尋找是否有對應的課程
                    string CourseKey = CourseName + "," + SchoolYear + "," + Semester + "," + Month;
                    string CourseID = mCourseNameIDs.ContainsKey(CourseKey) ? mCourseNameIDs[CourseKey] : string.Empty;

                    string StudentNumber = Row.GetValue(constStudentNumber);
                    string StudentID = mStudentNumberIDs.ContainsKey(StudentNumber) ? mStudentNumberIDs[StudentNumber] : string.Empty;

                    if (!string.IsNullOrEmpty(CourseID) && !string.IsNullOrEmpty(StudentID))
                        Conditions.Add("(ref_course_id=" + CourseID + " and ref_student_id=" + StudentID + ")");
                }

                string strCondition = SelectIDCondition("$shinmin.retake.scselect",string.Join(" or ", Conditions.ToArray()));

                if (!string.IsNullOrEmpty(strCondition))
                    mSCAttends = mHelper.Select<UDTScselectDef>(strCondition);

                foreach (IRowStream Row in Rows)
                {
                    string CourseName = Row.GetValue(constCourseName);
                    string SchoolYear = Row.GetValue(constSchoolYear);
                    string Semester = Row.GetValue(constSemester);
                    string Month = Row.GetValue(constMonth);

                    //根據課程名稱、學年度及學期尋找是否有對應的課程
                    string CourseKey = CourseName + "," + SchoolYear + "," + Semester + "," + Month;
                    int? CourseID = null;
                    if (mCourseNameIDs.ContainsKey(CourseKey))
                        CourseID = K12.Data.Int.ParseAllowNull(mCourseNameIDs[CourseKey]);
                    string StudentNumber = Row.GetValue(constStudentNumber);
                    int? StudentID = null;
                    if (mStudentNumberIDs.ContainsKey(StudentNumber))
                        StudentID = K12.Data.Int.ParseAllowNull(mStudentNumberIDs[StudentNumber]);

                    UDTScselectDef vSCAttend = null;

                    if (CourseID.HasValue && StudentID.HasValue)
                    {
                        //尋找是否有對應的課程排課資料
                        vSCAttend = mSCAttends
                        .Find(x =>
                            x.CourseID.Equals(CourseID.Value) &&
                            x.StudentID.Equals(StudentID.Value));

                        if (vSCAttend == null)
                            Messages[Row.Position].MessageItems.Add(
                                new MessageItem(
                                    Campus.Validator.ErrorType.Warning,
                                    Campus.Validator.ValidatorType.Row,
                                    "此筆修課記錄未存在系統中，將新增。"));
                    }
                }
            };
        }

        private void Initial()
        {
            mHelper = new AccessHelper();
            mCourseNameIDs = new Dictionary<string, string>();
            mStudentNumberIDs = new Dictionary<string, string>();

            QueryHelper Helper = new QueryHelper();

            DataTable Table = Helper.Select("select uid,course_name,school_year,semester,month from $shinmin.retake.course");

            foreach (DataRow Row in Table.Rows)
            {
                string CourseID = Row.Field<string>("uid");
                string CourseName = Row.Field<string>("course_name");
                string SchoolYear = Row.Field<string>("school_year");
                string Semester = Row.Field<string>("semester");
                string Month = Row.Field<string>("month");
                string CourseKey = CourseName + "," + SchoolYear + "," + Semester + "," + Month;

                if (!mCourseNameIDs.ContainsKey(CourseKey))
                    mCourseNameIDs.Add(CourseKey, CourseID);
            }

            Table = Helper.Select("select id,student_number from student where status in (1,2)");

            foreach (DataRow Row in Table.Rows)
            {
                string StudentID = Row.Field<string>("id");
                string StudentNumber = Row.Field<string>("student_number");

                if (!mStudentNumberIDs.ContainsKey(StudentNumber))
                    mStudentNumberIDs.Add(StudentNumber, StudentID);
            }
        }

        /// <summary>
        /// 取得驗證規則
        /// </summary>
        /// <returns></returns>
        public override string GetValidateRule()
        {
            return Properties.Resources.RetakeSCAttend;
        }

        /// <summary>
        /// 取得支援的匯入型態
        /// </summary>
        /// <returns></returns>
        public override ImportAction GetSupportActions()
        {
            return ImportAction.Update | ImportAction.Delete;
        }

        /// <summary>
        /// 匯入前準備
        /// </summary>
        /// <param name="Option"></param>
        public override void Prepare(ImportOption Option)
        {
            mOption = Option;

            Initial();
        }

        /// <summary>
        /// 執行分批匯入
        /// </summary>
        /// <param name="Rows">IRowStream集合</param>
        /// <returns>回傳分批匯入執行完成訊息</returns>
        public override string Import(List<IRowStream> Rows)
        {
            mstrLog.Clear();

            if (mOption.SelectedKeyFields.Count == 5 &&
                mOption.SelectedKeyFields.Contains(constSchoolYear) &&
                mOption.SelectedKeyFields.Contains(constSemester) &&
                mOption.SelectedKeyFields.Contains(constCourseName) &&
                mOption.SelectedKeyFields.Contains(constMonth) &&
                mOption.SelectedKeyFields.Contains(constStudentNumber))
            {
   
                if (mOption.Action == ImportAction.Update)
                {
                    #region Step2:針對每筆匯入每筆資料檢查，判斷是新增或是更新
                    List<UDTScselectDef> InsertRecords = new List<UDTScselectDef>();
                    List<UDTScselectDef> UpdateRecords = new List<UDTScselectDef>();

                    foreach (IRowStream Row in Rows)
                    {
                        string CourseName = Row.GetValue(constCourseName);
                        string SchoolYear = Row.GetValue(constSchoolYear);
                        string Semester = Row.GetValue(constSemester);
                        string Month = Row.GetValue(constMonth);

                        //根據課程名稱、學年度及學期尋找是否有對應的課程
                        string CourseKey = CourseName + "," + SchoolYear + "," + Semester+","+Month;
                        int? CourseID = null;
                        if (mCourseNameIDs.ContainsKey(CourseKey))
                            CourseID = K12.Data.Int.ParseAllowNull(mCourseNameIDs[CourseKey]);
                        string StudentNumber = Row.GetValue(constStudentNumber);
                        int? StudentID = null;
                        if (mStudentNumberIDs.ContainsKey(StudentNumber))
                           StudentID = K12.Data.Int.ParseAllowNull(mStudentNumberIDs[StudentNumber]);

                        UDTScselectDef vSCAttend = null;

                        if (CourseID.HasValue && StudentID.HasValue)
                        {
                            //尋找是否有對應的課程排課資料
                             vSCAttend = mSCAttends
                                .Find(x => 
                                    x.CourseID.Equals(CourseID.Value) &&  
                                    x.StudentID.Equals(StudentID.Value));

                            if (vSCAttend != null)
                            {
                                if (mOption.SelectedFields.Contains(constSeatNo))
                                    vSCAttend.SeatNo = K12.Data.Int.Parse(Row.GetValue(constSeatNo));

                                if (mOption.SelectedFields.Contains(constCourseType))
                                    vSCAttend.Type = Row.GetValue(constCourseType);

                                UpdateRecords.Add(vSCAttend);
                        }
                        else
                        {
                            vSCAttend = new UDTScselectDef();
                            vSCAttend.CourseID = CourseID.Value;
                            vSCAttend.StudentID = StudentID.Value;

                            if (mOption.SelectedFields.Contains(constSeatNo))
                                vSCAttend.SeatNo = K12.Data.Int.Parse(Row.GetValue(constSeatNo));

                            if (mOption.SelectedFields.Contains(constCourseType))
                            vSCAttend.Type = Row.GetValue(constCourseType);
                            
                            InsertRecords.Add(vSCAttend);
                        }
                        #endregion
                        }
                    }

                    #region Step3:實際新增或更新資料
                    if (InsertRecords.Count > 0)
                    {
                        List<string> NewIDs = mHelper.InsertValues(InsertRecords);
                        mstrLog.AppendLine("已新增" + InsertRecords.Count + "筆課程資料。");
                        //在新增完後不需更新mCourseExtensions變數，因為來源資料不允許相同的課程重覆做新增
                    }
                    if (UpdateRecords.Count > 0)
                    {
                        mHelper.UpdateValues(UpdateRecords);
                        mstrLog.AppendLine("已更新" + UpdateRecords.Count + "筆課程資料。");
                        //在更新完後不需更新mCourseExtensions變數，因為來源資料不允許相同的課程重覆做更新
                    }
                    #endregion
                }
                else if (mOption.Action == ImportAction.Delete)
                {
                    #region 刪除資料
                    ////要刪除的排課課程資料
                    //List<CourseExtension> DeleteRecords = new List<CourseExtension>();

                    ////針對每筆記錄
                    //foreach (IRowStream Row in Rows)
                    //{
                    //    //取得鍵值為課程名稱、學年度及學期
                    //    string CourseName = Row.GetValue(constCourseName);
                    //    string SchoolYear = Row.GetValue(constSchoolYear);
                    //    string Semester = Row.GetValue(constSemester);

                    //    //根據課程名稱、學年度及學期尋找是否有對應的課程
                    //    string CourseKey = CourseName + "," + SchoolYear + "," + Semester;
                    //    string CourseID = mCourseNameIDs.ContainsKey(CourseKey) ? mCourseNameIDs[CourseKey] : string.Empty;

                    //    //若有找到課程資料
                    //    if (!string.IsNullOrEmpty(CourseID))
                    //    {
                    //        //尋找是否有對應的排課課程資料
                    //        CourseExtension vCourseExtension = mCourseExtensions
                    //            .Find(x => x.CourseID.Equals(K12.Data.Int.Parse(CourseID)));
                    //        //若有找到則加入到刪除的集合中
                    //        if (vCourseExtension != null)
                    //            DeleteRecords.Add(vCourseExtension);
                    //    }
                    //}

                    ////若是要刪除的集合大於0才執行
                    //if (DeleteRecords.Count > 0)
                    //{
                    //    //mHelper.DeletedValues(DeleteRecords);                      
                    //    //mstrLog.AppendLine("已刪除"+DeleteRecords.Count+"筆排課課程資料。");
                    //    //在刪除完後不需更新mCourseExtensions變數，因為來源資料不允許相同的課程重覆做刪除
                    //}
                    #endregion
                }
            }

            return mstrLog.ToString();
        }
    }
}