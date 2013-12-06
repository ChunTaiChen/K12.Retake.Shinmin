using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Campus.DocumentValidator;
using Campus.Import;
using FISCA.Data;
using FISCA.UDT;
using K12.Retake.Shinmin.DAO;

namespace K12.Retake.Shinmin
{
    /// <summary>
    /// 匯入排課課程資料
    /// </summary>
    public class ImportCourseExtension : ImportWizard
    {
        private const string constCourseName = "課程名稱";
        private const string constSchoolYear = "學年度";
        private const string constSemester = "學期";
        private const string constMonth = "梯次";
        private const string constSubjectType = "科目類別";
        private const string constSubject = "科目名稱";
        private const string constSubjectLevel = "科目級別";
        private const string constCredit = "學分數";
        private const string constDept = "科別";
        private const string constTeacherName = "授課教師";

        private StringBuilder mstrLog = new StringBuilder();
        private ImportOption mOption;
        private Dictionary<string, string> mCourseNameIDs;
        private Dictionary<string, string> mTeacherNameIDs = new Dictionary<string, string>();
        private Task mTask;
        private AccessHelper mHelper;

        /// <summary>
        /// 取得驗證規則
        /// </summary>
        /// <returns></returns>
        public override string GetValidateRule()
        {
            return Properties.Resources.RetakeCourse;
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
            mHelper = new AccessHelper();
            mOption = Option;
            mCourseNameIDs = new Dictionary<string, string>();
            mTask = Task.Factory.StartNew
            (() =>
            {
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

                Table = Helper.Select("select id,teacher_name,nickname from teacher");

                foreach (DataRow Row in Table.Rows)
                {
                    string TeacherID = Row.Field<string>("id");
                    string TeacherName = Row.Field<string>("teacher_name");
                    string TeacherNickname = Row.Field<string>("nickname");
                    string TeacherKey = string.IsNullOrWhiteSpace(TeacherNickname) ? TeacherName : TeacherName + "(" + TeacherNickname + ")";

                    if (!mTeacherNameIDs.ContainsKey(TeacherKey))
                        mTeacherNameIDs.Add(TeacherKey, TeacherID);
                }
            }
            );
        }

        /// <summary>
        /// 執行分批匯入
        /// </summary>
        /// <param name="Rows">IRowStream集合</param>
        /// <returns>回傳分批匯入執行完成訊息</returns>
        public override string Import(List<IRowStream> Rows)
        {
            mTask.Wait();

            mstrLog.Clear();

            if (mOption.SelectedKeyFields.Count == 4 &&
                mOption.SelectedKeyFields.Contains(constSchoolYear) &&
                mOption.SelectedKeyFields.Contains(constSemester) &&
                mOption.SelectedKeyFields.Contains(constCourseName) &&
                mOption.SelectedKeyFields.Contains(constMonth))
            {
                #region 取得已存在的排課課程資料
                List<UDTCourseDef> mCourseExtensions = new List<UDTCourseDef>();
                List<string> CourseIDs = new List<string>();

                foreach (IRowStream Row in Rows)
                {
                    string CourseName = Row.GetValue(constCourseName);
                    string SchoolYear = Row.GetValue(constSchoolYear);
                    string Semester = Row.GetValue(constSemester);
                    string Month = Row.GetValue(constMonth);

                    //根據課程名稱、學年度及學期尋找是否有對應的課程
                    string CourseKey = CourseName + "," + SchoolYear + "," + Semester + "," + Month;
                    string CourseID = mCourseNameIDs.ContainsKey(CourseKey) ? mCourseNameIDs[CourseKey] : string.Empty;

                    if (!string.IsNullOrEmpty(CourseID))
                        CourseIDs.Add(CourseID);
                }

                string CourseIDsCondition = string.Join(",", CourseIDs.ToArray());

                if (!string.IsNullOrEmpty(CourseIDsCondition))
                    mCourseExtensions = mHelper.Select<UDTCourseDef>("uid in (" + CourseIDsCondition + ")");
                #endregion

                if (mOption.Action == ImportAction.Update)
                {
                    #region Step2:針對每筆匯入每筆資料檢查，判斷是新增或是更新
                    List<UDTCourseDef> InsertRecords = new List<UDTCourseDef>();
                    List<UDTCourseDef> UpdateRecords = new List<UDTCourseDef>();

                    foreach (IRowStream Row in Rows)
                    {
                        string CourseName = Row.GetValue(constCourseName);
                        string SchoolYear = Row.GetValue(constSchoolYear);
                        string Semester = Row.GetValue(constSemester);
                        string Month = Row.GetValue(constMonth);

                        //根據課程名稱、學年度及學期尋找是否有對應的課程
                        string CourseKey = CourseName + "," + SchoolYear + "," + Semester + "," + Month;
                        int? CourseID = null;
                        UDTCourseDef vCourseExtension = null;
                        if (mCourseNameIDs.ContainsKey(CourseKey))
                            CourseID = K12.Data.Int.ParseAllowNull(mCourseNameIDs[CourseKey]);

                        if (CourseID.HasValue)
                        {
                            //尋找是否有對應的課程排課資料
                            vCourseExtension = mCourseExtensions
                               .Find(x => x.UID.Equals(K12.Data.Int.GetString(CourseID)));

                            if (vCourseExtension != null)
                            {
                                if (mOption.SelectedFields.Contains(constSubjectType))
                                    vCourseExtension.SubjectType = Row.GetValue(constSubjectType);

                                if (mOption.SelectedFields.Contains(constSubject))
                                    vCourseExtension.SubjectName = Row.GetValue(constSubject);

                                if (mOption.SelectedFields.Contains(constSubjectLevel))
                                    vCourseExtension.SubjectLevel = K12.Data.Int.ParseAllowNull(Row.GetValue(constSubjectLevel));

                                if (mOption.SelectedFields.Contains(constCredit))
                                    vCourseExtension.Credit = K12.Data.Int.Parse(Row.GetValue(constCredit));

                                if (mOption.SelectedFields.Contains(constDept))
                                    vCourseExtension.DeptName = Row.GetValue(constDept);

                                if (mOption.SelectedFields.Contains(constTeacherName))
                                {
                                    string TeacherName = Row.GetValue(constTeacherName);
                                    if (mTeacherNameIDs.ContainsKey(TeacherName))
                                        vCourseExtension.RefTeacherID = K12.Data.Int.Parse(mTeacherNameIDs[TeacherName]);
                                }

                                UpdateRecords.Add(vCourseExtension);
                            }
                    #endregion
                        }
                        #region 新增CourseExtension
                        else
                        {
                            vCourseExtension = new UDTCourseDef();
                            vCourseExtension.SchoolYear = K12.Data.Int.Parse(Row.GetValue(constSchoolYear));
                            vCourseExtension.Semester = K12.Data.Int.Parse(Row.GetValue(constSemester));
                            vCourseExtension.Month = K12.Data.Int.Parse(Row.GetValue(constMonth));
                            vCourseExtension.CourseName = Row.GetValue(constCourseName);

                            if (mOption.SelectedFields.Contains(constSubjectType))
                                vCourseExtension.SubjectType = Row.GetValue(constSubjectType);

                            if (mOption.SelectedFields.Contains(constSubject))
                                vCourseExtension.SubjectName = Row.GetValue(constSubject);

                            if (mOption.SelectedFields.Contains(constSubjectLevel))
                                vCourseExtension.SubjectLevel = K12.Data.Int.ParseAllowNull(Row.GetValue(constSubjectLevel));

                            if (mOption.SelectedFields.Contains(constCredit))
                                vCourseExtension.Credit = K12.Data.Int.Parse(Row.GetValue(constCredit));

                            if (mOption.SelectedFields.Contains(constDept))
                                vCourseExtension.DeptName = Row.GetValue(constDept);

                            if (mOption.SelectedFields.Contains(constTeacherName))
                            {
                                string TeacherName = Row.GetValue(constTeacherName);
                                if (mTeacherNameIDs.ContainsKey(TeacherName))
                                    vCourseExtension.RefTeacherID = K12.Data.Int.Parse(mTeacherNameIDs[TeacherName]);
                            }

                            InsertRecords.Add(vCourseExtension);
                        }
                        #endregion
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
            }
            //    else if (mOption.Action == ImportAction.Delete)
            //    {
            //        #region 刪除資料
            //        ////要刪除的排課課程資料
            //        //List<CourseExtension> DeleteRecords = new List<CourseExtension>();

            //        ////針對每筆記錄
            //        //foreach (IRowStream Row in Rows)
            //        //{
            //        //    //取得鍵值為課程名稱、學年度及學期
            //        //    string CourseName = Row.GetValue(constCourseName);
            //        //    string SchoolYear = Row.GetValue(constSchoolYear);
            //        //    string Semester = Row.GetValue(constSemester);

            //        //    //根據課程名稱、學年度及學期尋找是否有對應的課程
            //        //    string CourseKey = CourseName + "," + SchoolYear + "," + Semester;
            //        //    string CourseID = mCourseNameIDs.ContainsKey(CourseKey) ? mCourseNameIDs[CourseKey] : string.Empty;

            //        //    //若有找到課程資料
            //        //    if (!string.IsNullOrEmpty(CourseID))
            //        //    {
            //        //        //尋找是否有對應的排課課程資料
            //        //        CourseExtension vCourseExtension = mCourseExtensions
            //        //            .Find(x => x.CourseID.Equals(K12.Data.Int.Parse(CourseID)));
            //        //        //若有找到則加入到刪除的集合中
            //        //        if (vCourseExtension != null)
            //        //            DeleteRecords.Add(vCourseExtension);
            //        //    }
            //        //}

            //        ////若是要刪除的集合大於0才執行
            //        //if (DeleteRecords.Count > 0)
            //        //{
            //        //    //mHelper.DeletedValues(DeleteRecords);                      
            //        //    //mstrLog.AppendLine("已刪除"+DeleteRecords.Count+"筆排課課程資料。");
            //        //    //在刪除完後不需更新mCourseExtensions變數，因為來源資料不允許相同的課程重覆做刪除
            //        //}
            //        #endregion
            //    }
            //}

            return mstrLog.ToString();
        }
    }
}