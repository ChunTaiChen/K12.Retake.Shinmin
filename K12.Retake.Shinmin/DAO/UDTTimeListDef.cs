using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace K12.Retake.Shinmin.DAO
{
    /// <summary>
    /// 重補修期間
    /// </summary>
    [TableName("shinmin.retake.time_list")]
    public class UDTTimeListDef:ActiveRecord
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
        /// 名稱
        ///</summary>
        [Field(Field = "name", Indexed = false)]
        public string Name { get; set; }

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

        ///<summary>
        /// 正在開放
        ///</summary>
        [Field(Field = "active", Indexed = false)]
        public Boolean Active { get; set; }

    }
}
