using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace K12.Retake.Shinmin.DAO
{
    /// <summary>
    /// 儲存科目轉課程暫存基本資料
    /// </summary>
    public class SubjectCourseBase
    {
        /// <summary>
        /// 學年度
        /// </summary>
        public int SchoolYear { get; set; }

        /// <summary>
        /// 學期
        /// </summary>
        public int Semester { get; set; }

        /// <summary>
        /// 月份
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// 科目名稱
        /// </summary>
        public string SubjectName { get; set; }

        /// <summary>
        /// 級別
        /// </summary>
        public int? SubjectLevel { get; set; }

        /// <summary>
        /// 學分數
        /// </summary>
        public int Credit { get; set; }

        /// <summary>
        /// 科別
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 科目類別
        /// </summary>
        public string SubjectType { get; set; }

        /// <summary>
        /// 課表名稱
        /// </summary>
        public string CourseTimeTable { get; set; }

        /// <summary>
        /// 節次
        /// </summary>
        public XElement PeriodXml{ get; set; }

        /// <summary>
        /// 每班最多學生數
        /// </summary>
        public int MaxStudentCount { get; set; }
        
        /// <summary>
        /// StudentID List
        /// </summary>
        public List<SubjectCourseStudentBase> StudentIDList = new List<SubjectCourseStudentBase>();

        /// <summary>
        /// 開課數量
        /// </summary>
        public int CreateCount { get; set; }

        /// <summary>
        /// 產生後課程名稱
        /// </summary>
        public List<string> CourseNameList = new List<string>();
      
    }
}
