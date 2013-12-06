﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using FISCA.UDT;
using FISCA.Data;
using K12.Data;
using K12.Retake.Shinmin.DAO;

namespace K12.Retake.Shinmin.Form
{
    public partial class RetakeJoinForm : BaseForm
    {
        //2012/7/23
        //group by出目前系統的年級資料
        //取得 DTClub ,並且判斷其開始與結束時間
        private const string DateTimeFormat = "yyyy/MM/dd HH:mm";
        AccessHelper _AccessHelper = new AccessHelper();
        QueryHelper _QueryHelper = new QueryHelper();
        List<UDTSelectCourseDateDef> Low_DTClubList = new List<UDTSelectCourseDateDef>();

        public RetakeJoinForm()
        {
            InitializeComponent();
        }

        private void RetakeJoinDateTime_Load(object sender, EventArgs e)
        {       

            //將對應年級的時間填入。
            FillTimes();
        }

        private void FillTimes()
        {
            Low_DTClubList = _AccessHelper.Select<UDTSelectCourseDateDef>();

            if (Low_DTClubList.Count >= 1)
            {
                UDTSelectCourseDateDef each = Low_DTClubList[0];

                string startTime = each.StartDate.HasValue ? each.StartDate.Value.ToString(DateTimeFormat) : "";
                string endTime = each.EndDate.HasValue ? each.EndDate.Value.ToString(DateTimeFormat) : "";

                tbStartDateTime.Text = startTime;
                tbEndDateTime.Text = endTime;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (DateTimeParse())
            {
                if (!Compare())
                {
                    //刪掉原有資料
                    _AccessHelper.DeletedValues(Low_DTClubList);

                    List<UDTSelectCourseDateDef> list = new List<UDTSelectCourseDateDef>();
                    UDTSelectCourseDateDef each = new UDTSelectCourseDateDef();
                    each.StartDate = DateTime.Parse(tbStartDateTime.Text);
                    each.EndDate = DateTime.Parse(tbEndDateTime.Text);
                    list.Add(each);
                    _AccessHelper.InsertValues(list);
                    MsgBox.Show("儲存成功!!");
                    this.Close();
                }
                else
                {
                    MsgBox.Show("[結束時間]不可小於[開始時間]!!");
                    return;
                }
            }
            else
            {
                MsgBox.Show("請輸入正確資料\n再進行儲存動作!!");
                return;
            }

        }

        private bool Compare()
        {
            bool a = false;
            DateTime? objStart = DateTimeHelper.Parse(tbStartDateTime.Text);
            DateTime? objEnd = DateTimeHelper.Parse(tbEndDateTime.Text);

            if (objStart.HasValue && objEnd.HasValue)
            {
                if (objStart.Value >= objEnd.Value)
                {
                    errorProvider1.SetError(tbStartDateTime, "結束時間必須在開始時間之後。");
                    errorProvider2.SetError(tbEndDateTime, "結束時間必須在開始時間之後。");
                    a = true;
                }
                else
                {
                    errorProvider1.Clear();
                    errorProvider2.Clear();

                }
            }
            return a;

        }

        private bool DateTimeParse()
        {
            return (DateTimeParseStart() & DateTimeParseEnd());
        }

        private bool DateTimeParseStart()
        {
            bool a = false;

            DateTime dt1;
            if (DateTime.TryParse(tbStartDateTime.Text, out dt1))
            {
                a = true;
                errorProvider1.Clear();
            }
            else
            {
                errorProvider1.SetError(tbStartDateTime, "請輸入正確日期格式");
            }

            return a;
        }

        private bool DateTimeParseEnd()
        {
            bool a = false;

            DateTime dt2;
            if (DateTime.TryParse(tbEndDateTime.Text, out dt2))
            {
                a = true;
                errorProvider2.Clear();
            }
            else
            {
                errorProvider2.SetError(tbEndDateTime, "請輸入正確日期格式");
            }

            return a;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbStartDateTime_Validated(object sender, EventArgs e)
        {
            if (DateTimeParseStart())
            {
                DateTime? objStart = DateTimeHelper.ParseGregorian(tbStartDateTime.Text, PaddingMethod.First);
                tbStartDateTime.Text = objStart.Value.ToString(DateTimeFormat);
            }
        }

        private void tbEndDateTime_Validated(object sender, EventArgs e)
        {
            if (DateTimeParseEnd())
            {
                DateTime? objStart = DateTimeHelper.ParseGregorian(tbEndDateTime.Text, PaddingMethod.First);
                tbEndDateTime.Text = objStart.Value.ToString(DateTimeFormat);
            }
        }

        private void tbStartDateTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbEndDateTime.Focus();
            }
        }

        private void tbEndDateTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();
            }
        }
    }
}
