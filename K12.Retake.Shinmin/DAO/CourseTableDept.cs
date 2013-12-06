using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K12.Retake.Shinmin.DAO
{
    /// <summary>
    /// 儲存課表與科目
    /// </summary>
    public class CourseTableDept
    {
        /// <summary>
        /// 課表編號
        /// </summary>
        public int CourseTableID { get; set; }

        /// <summary>
        /// 課表名稱
        /// </summary>
        public string CourseTableName { get; set; }

        /// <summary>
        /// 科別名稱
        /// </summary>
        public string DeptName { get; set; }

    }
}
