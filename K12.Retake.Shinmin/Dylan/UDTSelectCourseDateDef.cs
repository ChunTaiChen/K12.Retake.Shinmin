using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace K12.Retake.Shinmin.DAO
{
    /// <summary>
    /// 重補修開始選課時間
    /// </summary>
    [TableName("shinmin.retake.select_course_date")]
    public class UDTSelectCourseDateDef : ActiveRecord    
    {
        ///<summary>
        /// 開始時間
        ///</summary>
        [Field(Field = "start_date", Indexed = false)]
        public DateTime? StartDate { get; set; }

        ///<summary>
        /// 結束時間
        ///</summary>
        [Field(Field = "end_date", Indexed = false)]
        public DateTime? EndDate { get; set; }
    }
}
