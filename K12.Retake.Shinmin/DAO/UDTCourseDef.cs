using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;
namespace K12.Retake.Shinmin.DAO
{
    /// <summary>
    /// 重補修課程
    /// </summary>
    [TableName("shinmin.retake.course")]
    public class UDTCourseDef:ActiveRecord
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
        /// 課程名稱
        ///</summary>
        [Field(Field = "course_name", Indexed = false)]
        public string CourseName { get; set; }

        ///<summary>
        /// 科目名稱
        ///</summary>
        [Field(Field = "subject_name", Indexed = false)]
        public string SubjectName { get; set; }

        ///<summary>
        /// 科目級別
        ///</summary>
        [Field(Field = "subject_level", Indexed = false)]
        public int? SubjectLevel { get; set; }

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
        /// 授課教師名稱(存 ID)
        ///</summary>
        [Field(Field = "ref_teacher_id", Indexed = false)]
        public int RefTeacherID { get; set; }

        ///<summary>
        /// 科別名稱
        ///</summary>
        [Field(Field = "dept_name", Indexed = false)]
        public string DeptName { get; set; }

    }
}
