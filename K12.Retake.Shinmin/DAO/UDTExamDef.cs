using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace K12.Retake.Shinmin.DAO
{
    /// <summary>
    /// 分項試別
    /// </summary>
    [TableName("shinmin.retake.exam")]
    public class UDTExamDef:ActiveRecord
    {
        ///<summary>
        /// 分項名稱
        ///</summary>
        [Field(Field = "name", Indexed = false)]
        public string Name { get; set; }

        ///<summary>
        /// 試別名稱
        ///</summary>
        [Field(Field = "exam_name", Indexed = false)]
        public string ExamName { get; set; }

        ///<summary>
        /// 比重
        ///</summary>
        [Field(Field = "weight", Indexed = false)]
        public decimal Weight { get; set; }

        ///<summary>
        /// 開始日期
        ///</summary>
        [Field(Field = "begin_date", Indexed = false)]
        public DateTime BeginDate { get; set; }

        ///<summary>
        /// 結束日期
        ///</summary>
        [Field(Field = "end_date", Indexed = false)]
        public DateTime EndDate { get; set; }
    }
}
