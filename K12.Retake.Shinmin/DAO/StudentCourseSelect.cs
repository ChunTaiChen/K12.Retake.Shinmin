using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K12.Retake.Shinmin.DAO
{
    /// <summary>
    /// 學生選課
    /// </summary>
    public class StudentCourseSelect
    {
        /// <summary>
        /// 學生系統編號
        /// </summary>
        public string StudentID { get; set; }

        /// <summary>
        /// 學生姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 學年度
        /// </summary>
        public int SchoolYear { get; set; }

        /// <summary>
        /// 學期
        /// </summary>
        public int Semester { get; set; }

        /// <summary>
        /// 課程名稱
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// 課程座號
        /// </summary>
        public int CourseSeatNo { get; set; }

        /// <summary>
        /// 班級名稱
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 座號
        /// </summary>
        public int? SeatNo { get; set; }

        /// <summary>
        /// 學分數
        /// </summary>
        public int Credit { get; set; }

        /// <summary>
        /// 學號
        /// </summary>
        public string StudentNumber { get; set; }

    }
}
