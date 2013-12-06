using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using K12.Retake.Shinmin.DAO;

namespace K12.Retake.Shinmin.Form
{
    class AttendanceRow
    {
        /// <summary>
        /// 課程座號
        /// </summary>
        public string CourseSeatNo { get; set; }

        /// <summary>
        /// 學號
        /// </summary>
        public string StudentNumber { get; set; }

        /// <summary>
        /// 學生姓名
        /// </summary>
        public string StudentName { get; set; }

        /// <summary>
        /// 學生缺曠資料
        /// </summary>
        public Dictionary<string, UDTAttendanceDef> AttendanceDic { get; set; }

        public List<UDTAttendanceDef> Delete = new List<UDTAttendanceDef>();

        public bool HasChange = false;


        public AttendanceRow()
        {
            AttendanceDic = new Dictionary<string, UDTAttendanceDef>();
        }

        public void SetAttendance(string TimeSectionID)
        {
            if (!AttendanceDic.ContainsKey(TimeSectionID))
            {
                UDTAttendanceDef att = new UDTAttendanceDef();
                att.CourseID = int.Parse(Ref_Course_ID);
                att.StudentID = int.Parse(Ref_Student_ID);
                att.TimeSectionID = int.Parse(TimeSectionID);
                att.Type = "缺課";
                AttendanceDic.Add(TimeSectionID, att);

                HasChange = true;
            }


        }

        public void RemoveAttendance(string TimeSectionID)
        {
            if (AttendanceDic.ContainsKey(TimeSectionID))
            {
                if (!string.IsNullOrEmpty(AttendanceDic[TimeSectionID].UID))
                {
                    Delete.Add(AttendanceDic[TimeSectionID]);
                }
                AttendanceDic.Remove(TimeSectionID);

                HasChange = true;
            }
        }

        /// <summary>
        /// 課程 - ID
        /// </summary>
        public string Ref_Course_ID { get; set; }

        /// <summary>
        /// 學生 - ID
        /// </summary>
        public string Ref_Student_ID { get; set; }

        /// <summary>
        /// TimeSection - ID
        /// </summary>
        public string Ref_TimeSection_ID { get; set; }


    }
}
