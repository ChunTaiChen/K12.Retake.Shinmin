using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;
using K12.Retake.Shinmin.DAO;
using K12.Data;

namespace K12.Retake.Shinmin.Form
{
    class AttendanceMeg
    {
        AccessHelper _accessHelper = new AccessHelper();

        /// <summary>
        /// 上課日期,與節次
        /// </summary>
        public List<UDTTimeSectionDef> _TimeSectionList { get; set; }

        public List<UDTAttendanceDef> AttendanceList { get; set; }

        /// <summary>
        /// 本課程的修課學生
        /// </summary>
        public List<UDTScselectDef> _StudentList { get; set; }

        List<string> _StudentIDList { get; set; }

        public Dictionary<string, StudentRecord> StudentDic { get; set; }

        /// <summary>
        /// 課程Record記錄
        /// </summary>
        public UDTCourseDef _Course { get; set; }

        public AttendanceMeg(UDTCourseDef Course)
        {
            //課程Record記錄
            _Course = Course;

            //本課程的修課學生
            _StudentList = _accessHelper.Select<UDTScselectDef>("ref_course_id=" + _Course.UID);
            _StudentList.Sort(SortScselect);

            StudentDic = GetStudentRecord();

            //上課日期,與節次
            _TimeSectionList = _accessHelper.Select<UDTTimeSectionDef>("ref_course_id=" + Course.UID);
            //取得本課程學生的所有缺曠記錄
            // 加入判斷當課程沒有修課學生處理
            if (_StudentIDList.Count > 0)
                AttendanceList = _accessHelper.Select<UDTAttendanceDef>("ref_student_id in ('" + string.Join("','", _StudentIDList) + "') and ref_course_id=" + _Course.UID);
            else
                AttendanceList = new List<UDTAttendanceDef>();
        }

        /// <summary>
        /// 取得Dictionary
        /// 學生ID:學號
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, StudentRecord> GetStudentRecord()
        {
            _StudentIDList = _StudentList.Select(x => x.StudentID.ToString()).ToList();
            Dictionary<string, StudentRecord> dic = new Dictionary<string, StudentRecord>();
            List<StudentRecord> StudentNumberList = Student.SelectByIDs(_StudentIDList);
            foreach (StudentRecord each in StudentNumberList)
            {
                if (!dic.ContainsKey(each.ID))
                {
                    dic.Add(each.ID, each);
                }
            }
            return dic;
        }

        /// <summary>
        /// 將學生修課記錄,進行排序
        /// </summary>
        private int SortScselect(UDTScselectDef a, UDTScselectDef b)
        {
            return a.SeatNo.CompareTo(b.SeatNo);
        }

        /// <summary>
        /// 取得課程名稱
        /// </summary>
        public string CourseName
        {
            get
            {
                if (_Course != null)
                {
                    return _Course.CourseName;
                }
                else
                {
                    return "(無課程)";
                }
            }
        }
    }
}
