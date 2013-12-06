using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using K12.Retake.Shinmin.DAO;

namespace K12.Retake.Shinmin.Form
{
    public partial class SubAddSubjectDateForm : FISCA.Presentation.Controls.BaseForm
    {
        List<SubjectDatePeriod> _SubjectDatePeriodList;
        SubjectDatePeriod _CurrentSubjDatePeriod = new SubjectDatePeriod();

        public SubAddSubjectDateForm(SubjectDatePeriod SubjDatePeriod)
        {
            InitializeComponent();
            _SubjectDatePeriodList = new List<SubjectDatePeriod>();
            _CurrentSubjDatePeriod = SubjDatePeriod;
            lblMsg.Text = "日期：" + SubjDatePeriod.BeginDate.ToShortDateString() + " ,星期：" + Utility.GetDayWeekString(SubjDatePeriod.BeginDate);
        
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void SubAddSubjectDateForm_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.MinimumSize = this.Size;
        }

        /// <summary>
        /// 取得新增科目日期節次
        /// </summary>
        /// <returns></returns>
        public List<SubjectDatePeriod> GetAdddSbjectDatePeriodList()
        {
            return _SubjectDatePeriodList;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int i = 0;
            int AddD = 7;
            _SubjectDatePeriodList.Add(_CurrentSubjDatePeriod);
            for (i=0; i < ipsNo.Value; i++)
            {
                SubjectDatePeriod sdp = new SubjectDatePeriod();
                sdp.BeginDate = _CurrentSubjDatePeriod.BeginDate.AddDays(AddD);
                sdp.BeginPeriod = _CurrentSubjDatePeriod.BeginPeriod;
                sdp.PeriodCount = _CurrentSubjDatePeriod.PeriodCount;
                _SubjectDatePeriodList.Add(sdp);
                AddD += 7;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
