using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace K12.Retake.Shinmin.DAO
{
    /// <summary>
    /// 需要重修或補修清單
    /// </summary>
    [TableName("shinmin.retake.fail_subject_list")]
    public class UDTFailSubjectListDef:ActiveRecord
    {
        ///<summary>
        /// 科目名稱
        ///</summary>
        [Field(Field = "subejct_name", Indexed = false)]
        public string SubejctName { get; set; }

        ///<summary>
        /// 科目級別
        ///</summary>
        [Field(Field = "subject_level", Indexed = false)]
        public int SubjectLevel { get; set; }

        ///<summary>
        /// 科目成績
        ///</summary>
        [Field(Field = "score", Indexed = false)]
        public decimal Score { get; set; }

        ///<summary>
        /// 學分數
        ///</summary>
        [Field(Field = "credit", Indexed = false)]
        public int Credit { get; set; }

        ///<summary>
        /// 重修/補修
        ///</summary>
        [Field(Field = "type", Indexed = false)]
        public string Type { get; set; }

        ///<summary>
        /// 學生系統編號
        ///</summary>
        [Field(Field = "ref_student_id", Indexed = false)]
        public int StudentID { get; set; }
    }
}
