using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace K12.Retake.Shinmin.DAO
{
    /// <summary>
    /// 重補修修課紀錄
    /// </summary>
    [TableName("shinmin.retake.scselect")]
    public class UDTScselectDef:ActiveRecord
    {
        ///<summary>
        /// 課程編號
        ///</summary>
        [Field(Field = "ref_course_id", Indexed = false)]
        public int CourseID { get; set; }

        ///<summary>
        /// 學生系統編號
        ///</summary>
        [Field(Field = "ref_student_id", Indexed = false)]
        public int StudentID { get; set; }

        ///<summary>
        /// 課程成績
        ///</summary>
        [Field(Field = "score", Indexed = false)]
        public decimal? Score { get; set; }

        ///<summary>
        /// 重修/補修
        ///</summary>
        [Field(Field = "type", Indexed = false)]
        public string Type { get; set; }

        ///<summary>
        /// 課程座號
        ///</summary>
        [Field(Field = "seat_no", Indexed = false)]
        public int SeatNo { get; set; }

        ///<summary>
        /// 分項成績1
        ///</summary>
        [Field(Field = "sub_score1", Indexed = false)]
        public decimal? SubScore1 { get; set; }

        ///<summary>
        /// 分項成績2
        ///</summary>
        [Field(Field = "sub_score2", Indexed = false)]
        public decimal? SubScore2 { get; set; }

        ///<summary>
        /// 分項成績3
        ///</summary>
        [Field(Field = "sub_score3", Indexed = false)]
        public decimal? SubScore3 { get; set; }

        ///<summary>
        /// 分項成績4
        ///</summary>
        [Field(Field = "sub_score4", Indexed = false)]
        public decimal? SubScore4 { get; set; }

        ///<summary>
        /// 分項成績5
        ///</summary>
        [Field(Field = "sub_score5", Indexed = false)]
        public decimal? SubScore5 { get; set; }

        ///<summary>
        /// 扣考true) 表示不准考
        ///</summary>
        [Field(Field = "not_exam", Indexed = false)]
        public bool? NotExam { get; set; }

    }
}
