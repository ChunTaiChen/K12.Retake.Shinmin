using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace K12.Retake.Shinmin.DAO
{
    /// <summary>
    /// 課表
    /// </summary>
    [TableName("shinmin.retake.course_timetable")]
    public class UDTCourseTimetableDef:ActiveRecord
    {
        ///<summary>
        /// 課表名稱
        ///</summary>
        [Field(Field = "name", Indexed = false)]
        public string Name { get; set; }
    }
}
