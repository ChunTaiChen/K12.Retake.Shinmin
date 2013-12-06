using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using K12.Data;

namespace K12.Retake.Shinmin
{
    public class Global
    {
        /// <summary>
        /// 教師對照用一般狀態(id,TeacherName)
        /// </summary>
        public static Dictionary<int, string> _TeacherIDNameDict = new Dictionary<int, string>();

        /// <summary>
        /// 教師對照用一般狀態(TeacherName,id)
        /// </summary>
        public static Dictionary<string, int> _TeacherNameIDDict = new Dictionary<string, int>();

        /// <summary>
        /// 科別名稱
        /// </summary>
        public static List<string> _AllDeptNameList = new List<string>();
        
        /// <summary>
        /// 重新整理取得科別名稱
        /// </summary>
        public static void ReloadDeptNameList()
        {
            _AllDeptNameList = DAO.QueryData.GetAllDeptName();
        }

        /// <summary>
        /// 重新整理教師對照用
        /// </summary>
        public static void RelaodTeacherDict()
        {
            _TeacherIDNameDict.Clear();
            _TeacherNameIDDict.Clear();
            // 取一般狀態
            foreach (TeacherRecord rec in Teacher.SelectAll())
            {
                if (rec.Status == TeacherRecord.TeacherStatus.刪除)
                    continue;

                int id = int.Parse(rec.ID);
                string name=rec.Name;

                if (!string.IsNullOrWhiteSpace(rec.Nickname))
                    name = rec.Name + "(" + rec.Nickname + ")";

                _TeacherIDNameDict.Add(id, name);

                if (!_TeacherNameIDDict.ContainsKey(name))
                    _TeacherNameIDDict.Add(name,id);
            }
        }

        /// <summary>
        /// 暫存學生上課時間與缺曠資料
        /// </summary>
        public static Dictionary<string, Dictionary<DateTime, List<int>>> _TempStudentTimeSectionDict = new Dictionary<string, Dictionary<DateTime, List<int>>>();
        
        /// <summary>
        /// 暫存課程缺曠學生索引,StudentID,idx
        /// </summary>
        public static Dictionary<string, int> _CourseStudentAttendanceIdxDict = new Dictionary<string, int>();

        /// <summary>
        /// 課程缺曠資料
        /// </summary>
        public static List<DAO.UDTAttendanceDef> _CousreAttendList = new List<DAO.UDTAttendanceDef>();

        /// <summary>
        /// 課程時間表
        /// </summary>
        public static List<DAO.UDTTimeSectionDef> _CourseTimeSectionList = new List<DAO.UDTTimeSectionDef>();

        /// <summary>
        /// 學生扣考，StudentID,CourseID
        /// </summary>
        public static Dictionary<string, List<int>> _StudentNotExamDict = new Dictionary<string, List<int>>();
    }
}
