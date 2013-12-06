using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K12.Retake.Shinmin.DAO
{
    /// <summary>
    /// 開課與修課學生使用
    /// </summary>
    public class SubjectCourseStudentBase
    {
        /// <summary>
        /// 學生系統編號
        /// </summary>
        public int StudentID { get; set; }

        /// <summary>
        /// 重修/補修
        /// </summary>
        public string Type { get; set; }
    }
}
