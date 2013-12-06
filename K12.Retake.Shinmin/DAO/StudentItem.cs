using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K12.Retake.Shinmin.DAO
{
    /// <summary>
    /// 學生及格人數統計報表用
    /// </summary>
    public class StudentItem
    {
        /// <summary>
        /// 班級年級
        /// </summary>
        public int GradeYear { get; set; }

        /// <summary>
        /// 科別名稱
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// Student ID
        /// </summary>
        public string StudentID {get;set;}

        /// <summary>
        /// 學生修課 UID
        /// </summary>
        public string SCUID {get;set;}
        
        /// <summary>
        /// 是否扣考
        /// </summary>
        public bool isNotExam=false;

        /// <summary>
        /// 是否及格
        /// </summary>
        public bool isPass=false;

    }
}
