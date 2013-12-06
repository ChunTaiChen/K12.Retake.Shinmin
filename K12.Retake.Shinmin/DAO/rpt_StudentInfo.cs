using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K12.Retake.Shinmin.DAO
{
    /// <summary>
    /// 報表使用學生資料
    /// </summary>
    public class rpt_StudentInfo
    {
        // 班級、座號、學號、學生姓名、收件人姓名、地址

        /// <summary>
        /// 班級
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 座號
        /// </summary>
        public int? SeatNo { get; set; }

        /// <summary>
        /// 學號
        /// </summary>
        public string StudentNumber { get; set; }

        /// <summary>
        /// 學生姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 收件人姓名
        /// </summary>
        public string MailName { get; set; }

        /// <summary>
        /// 收件人地址
        /// </summary>
        public string MailAddress { get; set; }
    }
}
