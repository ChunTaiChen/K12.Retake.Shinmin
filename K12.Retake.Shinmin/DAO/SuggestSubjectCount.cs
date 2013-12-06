using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K12.Retake.Shinmin.DAO
{
    /// <summary>
    /// 用在儲存建議重修科目統計
    /// </summary>
    public class SuggestSubjectCount
    {
        /// <summary>
        /// 科目名稱
        /// </summary>
        public string SubjectName { get; set; }

        /// <summary>
        /// 級別
        /// </summary>
        public int? Level { get; set; }

        /// <summary>
        /// 學分數
        /// </summary>
        public int Credit { get; set; }

        /// <summary>
        /// 人數
        /// </summary>
        public int Count = 1;

        /// <summary>
        /// 科別名稱
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// Key
        /// </summary>
        /// <returns></returns>
        public string GetKey()
        {
            string Key = "";
            if (Level.HasValue)
                Key = DeptName + SubjectName +"_"+ Level.Value +"_"+ Credit;
            else
                Key = DeptName + SubjectName + "__" + Credit;

            return Key;
        }    
    }
}
