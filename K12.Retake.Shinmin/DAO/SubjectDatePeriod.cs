using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K12.Retake.Shinmin.DAO
{
    /// <summary>
    /// 新增科目的日期使用
    /// </summary>
    public class SubjectDatePeriod
    {
        /// <summary>
        /// 開始日期
        /// </summary>
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// 開始節次
        /// </summary>
        public int BeginPeriod { get; set; }

        /// <summary>
        /// 節數
        /// </summary>
        public int PeriodCount { get; set; }
    }
}
