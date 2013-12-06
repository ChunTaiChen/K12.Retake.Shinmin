using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K12.Retake.Shinmin.DAO
{
    /// <summary>
    /// 學生及格人數報表項目統計用
    /// </summary>
    public class StudentPassGroup
    {
        /// <summary>
        /// 群組
        /// </summary>
        public string GroupName {get;set;}

        /// <summary>
        /// 重補修人數
        /// </summary>
        public int retakeCount =0;

        /// <summary>
        /// 扣考人數
        /// </summary>
        public int notExamCount=0;
        
        /// <summary>
        /// 不及格人數
        /// </summary>
        public int noPassCount=0;
        
        /// <summary>
        /// 及格人數
        /// </summary>
        public int PassCount=0;
                
        /// <summary>
        /// 取得及格率
        /// </summary>
        public decimal GetPassRate()
        {
            // 計算方式：及格人數/(重補修人數-扣考人數)
            decimal retVal = 0;
            // 四捨五入到小數下一位
            int bC = retakeCount - notExamCount;
            if(bC>0)            
                retVal= Math.Round(((decimal)PassCount/(decimal)bC)*100,1,MidpointRounding.AwayFromZero);

            return retVal;
        }

        /// <summary>
        /// 所屬學生資料統計用
        /// </summary>
        List<StudentItem> _StudentItemList = new List<StudentItem>();
    }
}
