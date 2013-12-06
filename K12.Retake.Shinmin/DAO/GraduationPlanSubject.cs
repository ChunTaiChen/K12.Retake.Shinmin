using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K12.Retake.Shinmin.DAO
{
    /// <summary>
    /// 解析課程規劃使用
    /// </summary>
    public class GraduationPlanSubject
    {
        /// <summary>
        /// 年級
        /// </summary>
        public int GradeYear { get; set; }
        /// <summary>
        /// 學期
        /// </summary>
        public int Semester { get; set; }
        /// <summary>
        /// 科目名稱
        /// </summary>
        public string SubjectName { get; set; }
        /// <summary>
        /// 級別
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 學分數
        /// </summary>
        public int Credit { get; set; }

        /// <summary>
        /// 必修或選修
        /// </summary>
        public string strRequire { get; set; }

        /// <summary>
        /// 校訂或部訂
        /// </summary>
        public string def1 { get; set; }
    }
}
