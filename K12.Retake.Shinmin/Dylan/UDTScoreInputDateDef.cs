using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace K12.Retake.Shinmin.DAO
{
    /// <summary>
    /// 課程成績輸入時間
    /// </summary>
    [TableName("shinmin.retake.course_score_input_date")]
    public class UDTScoreInputDateDef : ActiveRecord
    {
        ///<summary>
        /// 名稱(期中考、期末考、平時成績)
        /// /// 
        ///</summary>
        [Field(Field = "name", Indexed = false)]
        public string Name { get; set; }

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
