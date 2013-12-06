using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace K12.Retake.Shinmin.DAO
{
    /// <summary>
    /// 科目選擇清單
    /// </summary>
    [TableName("shinmin.retake.subject")]
    public class UDTSubjectDef:ActiveRecord
    {
        ///<summary>
        /// 學年度
        ///</summary>
        [Field(Field = "school_year", Indexed = false)]
        public int SchoolYear { get; set; }

        ///<summary>
        /// 學期
        ///</summary>
        [Field(Field = "semester", Indexed = false)]
        public int Semester { get; set; }

        ///<summary>
        /// 月份
        ///</summary>
        [Field(Field = "month", Indexed = false)]
        public int Month { get; set; }

        ///<summary>
        /// 科目名稱
        ///</summary>
        [Field(Field = "subject_name", Indexed = false)]
        public string SubjectName { get; set; }

        ///<summary>
        /// 科目級別
        ///</summary>
        [Field(Field = "subject_level", Indexed = false)]
        public int? SubjecLevel { get; set; }

        ///<summary>
        /// 科別
        ///</summary>
        [Field(Field = "dept_name", Indexed = false)]
        public string DeptName { get; set; }

        ///<summary>
        /// 科目類別
        ///</summary>
        [Field(Field = "subject_type", Indexed = false)]
        public string SubjectType { get; set; }

        /////<summary>
        ///// 所屬課別
        /////</summary>
        //[Field(Field = "course_timetable", Indexed = false)]
        //public string CourseTimetable { get; set; }

        ///<summary>
        /// 所屬課別編號
        ///</summary>
        [Field(Field = "course_timetable_id", Indexed = false)]
        public int CourseTimetableID { get; set; }

        
        ///<summary>
        /// 學分數
        ///</summary>
        [Field(Field = "credit", Indexed = false)]
        public int Credit { get; set; }

        ///// <summary>
        ///// 日期內容(XML)
        ///// </summary>
        //[Field(Field = "date_content", Indexed = false)]
        //public string DateContent { get; set; }

        /// <summary>
        /// 節次內容(XML)
        /// </summary>
        [Field(Field = "period_content", Indexed = false)]
        public string PeriodContent { get; set; }

    }
}
