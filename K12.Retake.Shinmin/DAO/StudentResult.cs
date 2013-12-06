using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using K12.Data;

namespace K12.Retake.Shinmin.DAO
{
    /// <summary>
    /// 學生學期成績
    /// </summary>
    public class StudentResult
    {
        /// <summary>
        /// 學生系統編號
        /// </summary>
        public string StudentID { get; set; }

        /// <summary>
        /// 學生系統編號數字
        /// </summary>
        public int StudentIDi { get; set; }

        /// <summary>
        /// 學生紀錄
        /// </summary>
        public StudentRecord StudentRec { get; set; }

        /// <summary>
        /// 班級紀錄
        /// </summary>
        public ClassRecord ClassRec { get; set; }

        /// <summary>
        /// 課程紀錄
        /// </summary>
        public UDTCourseDef CourseRec { get; set; }

        /// <summary>
        /// 修課紀錄
        /// </summary>
        public UDTScselectDef ScselectRec { get; set; }   

        /// <summary>
        /// 資料是否修改
        /// </summary>
        public bool HasChange { get; set; }

        /// <summary>
        /// 期中考
        /// </summary>
        public decimal? Score1
        {
            get { return ScselectRec.SubScore1; }
            set { HasChange = true; ScselectRec.SubScore1 = value; }
        }

        /// <summary>
        /// 期末考
        /// </summary>
        public decimal? Score2
        {
            get { return ScselectRec.SubScore2; }
            set { HasChange = true; ScselectRec.SubScore2 = value; }
        }

        /// <summary>
        /// 平時成績
        /// </summary>
        public decimal? Score3
        {
            get { return ScselectRec.SubScore3; }
            set { HasChange = true; ScselectRec.SubScore3 = value; }
        }

        /// <summary>
        /// 課程結算成績
        /// </summary>
        public decimal? ResultScore
        {
            get 
            {
                if (ScselectRec != null)
                    return ScselectRec.Score;
                else
                    return null;
            }
            set { HasChange = true; ScselectRec.Score = value; }
        }

        /// <summary>
        /// 課程名稱
        /// </summary>
        public string CourseName
        {
            get 
            {
                return CourseRec.CourseName;
            }
        }

        /// <summary>
        /// 班級名稱
        /// </summary>
        public string ClassName
        {
            get
            {
                if (ClassRec != null)
                    return ClassRec.Name;
                else
                    return "";
            }
        }

        /// <summary>
        /// 班級座號
        /// </summary>
        public string SeatNo
        {
            get 
            {
                if (StudentRec.SeatNo.HasValue)
                    return StudentRec.SeatNo.Value.ToString();
                else
                    return "";

            }        
        }

        /// <summary>
        /// 學生姓名
        /// </summary>
        public string StudentName
        {
            get { return StudentRec.Name; }
        }

        /// <summary>
        /// 學生學號
        /// </summary>
        public string StudentNumber
        {
            get { return StudentRec.StudentNumber; }
        }

        /// <summary>
        /// 課程座號
        /// </summary>
        public string CourseSeatNo
        {
            get { return ScselectRec.SeatNo.ToString(); }
        }

        /// <summary>
        /// 是否扣考 (true 表示扣考)
        /// </summary>
        public bool isNotExam
        {
            get
            {
                bool retVal = false;
                if (ScselectRec.NotExam.HasValue)
                    if (ScselectRec.NotExam.Value)
                        retVal = true;

                return retVal;
            }        
        }
        
    }
}
