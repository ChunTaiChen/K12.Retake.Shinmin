using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace K12.Retake.Shinmin.DAO
{
    /// <summary>
    /// 課程缺曠紀錄
    /// </summary>
    [TableName("shinmin.retake.attendance")]
    public class UDTAttendanceDef:ActiveRecord
    {
        ///<summary>
        /// 缺曠編號
        ///</summary>
        [Field(Field = "id", Indexed = false)]
        public int ID { get; set; }

        ///<summary>
        /// 時間表編號
        ///</summary>
        [Field(Field = "ref_time_section_id", Indexed = false)]
        public int TimeSectionID { get; set; }

        ///<summary>
        /// 課程編號
        ///</summary>
        [Field(Field = "ref_course_id", Indexed = false)]
        public int CourseID { get; set; }

        ///<summary>
        /// 學生系統編號
        ///</summary>
        [Field(Field = "ref_student_id", Indexed = false)]
        public int StudentID { get; set; }

        ///<summary>
        /// 缺曠類別
        ///</summary>
        [Field(Field = "type", Indexed = false)]
        public string Type { get; set; }
    }
}
