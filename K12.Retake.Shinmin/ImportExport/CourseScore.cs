using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K12.Retake.Shinmin.ImportExport
{
    public class CourseScore
    {
        /// <summary>
        /// 學號
        /// </summary>
        public string StudentNumber{get;set;}

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name {get;set;}

        /// <summary>
        /// 科目名稱
        /// </summary>
        public string SubjectName{get;set;}

        /// <summary>
        /// 科目級別
        /// </summary>
        public int? SubjectLevel{get;set;}

        /// <summary>
        /// 學年度
        /// </summary>
        public int SchoolYear { get; set; }

        /// <summary>
        /// 學期
        /// </summary>
        public int Semester { get; set; }

        /// <summary>
        /// 成績年級
        /// </summary>
        public int GradeYear { get; set; }

        /// <summary>
        /// 學分數
        /// </summary>
        public int Credit { get; set; }

        /// <summary>
        /// 重修原始成績
        /// </summary>
        public decimal? retSourceScore { get; set; }

        /// <summary>
        /// 成績
        /// </summary>
        public decimal? Score { get; set; }

        /// <summary>
        /// 是否取得學分
        /// </summary>
        public bool pass{get;set;}

        /// <summary>
        /// 是重修,否補修
        /// </summary>
        public bool isT1 { get; set; }

        /// <summary>
        /// 必修或選修
        /// </summary>
        public string strRequire { get; set; }

        /// <summary>
        /// 校訂、部訂
        /// </summary>
        public string Def1 { get; set; }

    }
}
