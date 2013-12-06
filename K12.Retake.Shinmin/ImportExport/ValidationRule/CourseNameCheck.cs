using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Campus.DocumentValidator;
using FISCA.Data;

namespace K12.Retake.Shinmin
{
    /// <summary>
    /// 檢查系統中是否有對應的學年度、學期及課程名稱
    /// </summary>
    public class CourseNameCheck : IRowVaildator
    {
        private List<string> mCourseNames;
        private Task mTask;

        /// <summary>
        /// 建構式，取得系統中的課程名稱及學年度學期組合
        /// </summary>
        public CourseNameCheck()
        {
            mTask = Task.Factory.StartNew(() =>
            {
                mCourseNames = new List<string>();

                QueryHelper Helper = new QueryHelper();

                DataTable Table = Helper.Select("select course_name,school_year,semester,month from $shinmin.retake.course");

                foreach (DataRow Row in Table.Rows)
                {
                    string CourseName = Row.Field<string>("course_name").Trim();
                    string SchoolYear = Row.Field<string>("school_year");
                    string Semester = Row.Field<string>("semester");
                    string Month = Row.Field<string>("month");
                    string CourseKey = CourseName + "," + SchoolYear + "," + Semester + "," + Month;

                    if (!mCourseNames.Contains(CourseKey))
                        mCourseNames.Add(CourseKey);
                }
            });
        }

        #region IRowVaildator 成員

        /// <summary>
        /// 傳入要驗證的資料列，依課程名稱、學年度及學期檢查系統中是否有對應的課程
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public bool Validate(IRowStream Value)
        {
            if (Value.Contains("課程名稱") && Value.Contains("學年度") && Value.Contains("學期") && Value.Contains("梯次"))
            {
                string CourseName = Value.GetValue("課程名稱");
                string SchoolYear = Value.GetValue("學年度");
                string Semester = Value.GetValue("學期");
                string Month = Value.GetValue("梯次");
                string CourseKey = CourseName + "," + SchoolYear + "," + Semester + "," + Month;

                mTask.Wait();

                return mCourseNames.Contains(CourseKey);
            }

            return false;
        }

        /// <summary>
        /// 沒有提供自動修正
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public string Correct(IRowStream Value)
        {
            return string.Empty;
        }

        /// <summary>
        /// 傳回預設樣版
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public string ToString(string template)
        {
            return template;
        }

        #endregion
    }
}