using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace K12.Retake.Shinmin.DAO
{
    /// <summary>
    /// 課表與科別對應
    /// </summary>
    [TableName("shinmin.retake.cdselect")]
    public class UDTCdselectDef:ActiveRecord
    {
        ///<summary>
        /// 課表編號
        ///</summary>
        [Field(Field = "ref_course_timetable_id", Indexed = false)]
        public int RefCourseTimetableID { get; set; }


        ///<summary>
        /// 科別名稱
        ///</summary>
        [Field(Field = "dept_name", Indexed = false)]
        public string DeptName { get; set; }
    }
}
