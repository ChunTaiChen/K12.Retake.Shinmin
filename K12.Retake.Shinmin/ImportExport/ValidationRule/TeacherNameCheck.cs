using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Campus.DocumentValidator;
using FISCA.Data;

namespace K12.Retake.Shinmin
{
    /// <summary>
    /// 授課教師檢查
    /// </summary>
    public class TeacherNameCheck : IFieldValidator
    {
        private List<string> mTeacherNames;
        private Task mTask;

        /// <summary>
        /// 建構式，取得所有教師
        /// </summary>
        public TeacherNameCheck()
        {
            mTeacherNames = new List<string>();
            mTask = Task.Factory.StartNew
            (() =>
            {
                QueryHelper Helper = new QueryHelper();

                DataTable Table = Helper.Select("select teacher_name,nickname from teacher where status=1");

                foreach (DataRow Row in Table.Rows)
                {
                    string TeacherName = Row.Field<string>("teacher_name");
                    string TeacherNickname = Row.Field<string>("nickname");
                    string TeacherKey = string.IsNullOrWhiteSpace(TeacherNickname) ? TeacherName : TeacherName + "(" + TeacherNickname + ")";

                    if (!mTeacherNames.Contains(TeacherKey))
                        mTeacherNames.Add(TeacherKey);
                }
            }
            );
        }

        #region IFieldValidator 成員

        /// <summary>
        /// 自動修正
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public string Correct(string Value)
        {
            return string.Empty;
        }

        /// <summary>
        /// 回傳訊息
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public string ToString(string template)
        {
            return template;
        }

        /// <summary>
        /// 驗證
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public bool Validate(string Value)
        {
            mTask.Wait();

            return mTeacherNames.Contains(Value);
        }

        #endregion
    }
}