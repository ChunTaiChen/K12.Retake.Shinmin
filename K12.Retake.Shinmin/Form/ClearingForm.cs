using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using K12.Retake.Shinmin.DAO;
using FISCA.Presentation.Controls;

namespace K12.Retake.Shinmin.Form
{
    /// <summary>
    /// 學期成績結算
    /// </summary>
    public partial class ClearingForm : BaseForm
    {
        BackgroundWorker _bgWorker;        
        List<UDTScselectDef> _ScSelectList;
        List<UDTWeightProportionDef> _WeightProportion;
        List<string> _CourseIDList;
        public ClearingForm()
        {
            InitializeComponent();
            _bgWorker = new BackgroundWorker();
            _bgWorker.DoWork += new DoWorkEventHandler(_bgWorker_DoWork);
            _bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorker_RunWorkerCompleted);
            
        }

        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Text = "學期成績結算";
            btnStart.Enabled = true;

            if (e.Cancelled)
            {
                MsgBox.Show("沒有修課資料，結算操作已被中止!");
            }
            else
            {
                if (e.Error == null)
                    MsgBox.Show("結算完成! 共結算 " + _ScSelectList.Count + " 筆資料。");
                else
                    MsgBox.Show("結算發生錯誤! \n" + e.Error.Message);
            }
        }

        void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //_CourseIDList = QueryData.GetCourseSelectActive1();
            // 使用者所選擇
            _CourseIDList = RetakeAdmin.Instance.SelectedSource;
            if (_CourseIDList.Count == 0)
                e.Cancel = true;
            else
            {
                // 取得目前重補修課程成績資料
                _ScSelectList = UDTTransfer.UDTSCSelectByCourseIDList(_CourseIDList);
                
                // 取得評量比例
                UDTWeightProportionDef wp = _WeightProportion[0];

                // 計算成績
                foreach (UDTScselectDef data in _ScSelectList)
                {
                    decimal? score1=null,score2=null,score3=null;


                    if (data.SubScore1.HasValue)
                        score1 = wp.SS1_Weight * data.SubScore1.Value / 100;

                    if (data.SubScore2.HasValue)
                        score2 = wp.SS2_Weight * data.SubScore2.Value / 100;

                    if (data.SubScore3.HasValue)
                        score3 = wp.SS3_Weight * data.SubScore3.Value / 100;


                    decimal? results = 0;

                    // 處理百分比
                    if (score1.HasValue)
                        results += score1.Value;

                    if (score2.HasValue)
                        results += score2.Value;

                    if (score3.HasValue)
                        results += score3.Value;
                    // 先清空再重算填入
                    data.Score = null;
                    if (score1.HasValue || score2.HasValue || score3.HasValue)
                    {  
                        data.Score = Math.Round(results.Value, MidpointRounding.AwayFromZero);                       
                    }
                        bool notExam = false;
                        if (data.NotExam.HasValue)
                            if (data.NotExam.Value)
                                notExam = true;

                        // 扣考結算時給0分
                        if (notExam)
                        {
                            data.Score = 0;
                        }
                }

                // 更新資料至結算後成績
                UDTTransfer.UDTSCSelectUpdate(_ScSelectList);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!_bgWorker.IsBusy)
            {
                btnStart.Enabled = false;
                this.Text = "學期成績結算中(系統結算中)..";
                _bgWorker.RunWorkerAsync();
            }
            else
            {
                MsgBox.Show("系統忙碌中，請稍候再試..");
            }
        }

        private void ClearingForm_Load(object sender, EventArgs e)
        {
            _WeightProportion = UDTTransfer.UDTWeightProportionSelect();
            if (_WeightProportion.Count == 0)
            {
                MsgBox.Show("請先設定評量比例再結算成績!");
                this.Close();
            }
        }
    }
}
