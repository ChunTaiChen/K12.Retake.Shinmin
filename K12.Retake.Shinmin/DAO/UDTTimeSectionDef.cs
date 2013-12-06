using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace K12.Retake.Shinmin.DAO
{
    /// <summary>
    /// 時間區間
    /// </summary>
    [TableName("shinmin.retake.time_section")]
    public class UDTTimeSectionDef:ActiveRecord
    {
        ///<summary>
        /// 課程編號
        ///</summary>
        [Field(Field = "ref_course_id", Indexed = false)]
        public int CourseID { get; set; }

        ///<summary>
        /// 日期
        ///</summary>
        [Field(Field = "date", Indexed = false)]
        public DateTime Date { get; set; }

        ///<summary>
        /// 節次
        ///</summary>
        [Field(Field = "period", Indexed = false)]
        public int Period { get; set; }
    }
}
