using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace K12.Retake.Shinmin.DAO
{
    /// <summary>
    /// 重補修科目選擇清單
    /// </summary>
    [TableName("shinmin.retake.ssselect")]
    public class UDTSsselectDef:ActiveRecord
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
        /// 學生系統編號
        ///</summary>
        [Field(Field = "ref_student_id", Indexed = false)]
        public int StudentID { get; set; }

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
        /// 科目類別
        ///</summary>
        [Field(Field = "subject_type", Indexed = false)]
        public string SubjectType { get; set; }

        ///<summary>
        /// 學分數
        ///</summary>
        [Field(Field = "credit", Indexed = false)]
        public int Credit { get; set; }

        ///<summary>
        /// 星期
        ///</summary>
        [Field(Field = "weekly", Indexed = false)]
        public int Weekly { get; set; }

        ///<summary>
        /// 節次
        ///</summary>
        [Field(Field = "period", Indexed = false)]
        public int Period { get; set; }

        ///<summary>
        /// 節數
        ///</summary>
        [Field(Field = "period_count", Indexed = false)]
        public int PeriodCount { get; set; }

        ///<summary>
        /// 科目編號
        ///</summary>
        [Field(Field = "ref_subject_id", Indexed = false)]
        public int RefSubjectID { get; set; }
    }
}
