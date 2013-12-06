using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace K12.Retake.Shinmin.DAO
{
    /// <summary>
    /// 重補修成績計算比例原則
    /// </summary>
    [TableName("shinmin.retake.weight_proportion")]
    public class UDTWeightProportionDef : ActiveRecord
    {
        /// <summary>
        /// 期中考比例
        /// </summary>
        [Field(Field = "ss1_weight", Indexed = false)]
        public int SS1_Weight { get; set; }

        /// <summary>
        /// 期末考比例
        /// </summary>
        [Field(Field = "ss2_weight", Indexed = false)]
        public int SS2_Weight { get; set; }

        /// <summary>
        /// 平時成績比例
        /// </summary>
        [Field(Field = "ss3_weight", Indexed = false)]
        public int SS3_Weight { get; set; }
    }
}
