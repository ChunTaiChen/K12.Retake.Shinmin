using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace K12.Retake.Shinmin.DAO
{
    /// <summary>
    /// 重補修建議名單
    /// </summary>
    [TableName("shinmin.retake.suggest_list")]
    public class UDTSuggestListDef:ActiveRecord
    {
        ///<summary>
        /// 學生系統編號
        ///</summary>
        [Field(Field = "ref_student_id", Indexed = false)]
        public int RefStudentID { get; set; }

        ///<summary>
        /// 重補修期間編號
        ///</summary>
        [Field(Field = "ref_time_list_id", Indexed = false)]
        public int RefTimeListID { get; set; }
        
        ///<summary>
        /// 科目內容
        ///</summary>
        [Field(Field = "subject_content", Indexed = false)]
        public string SubjectContent { get; set; }
    }
}
