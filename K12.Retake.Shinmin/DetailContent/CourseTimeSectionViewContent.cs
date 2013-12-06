using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using K12.Retake.Shinmin.DAO;
using System.Xml.Linq;

namespace K12.Retake.Shinmin.DetailContent
{
    /// <summary>
    /// 課程時間表
    /// </summary>
    [FISCA.Permission.FeatureCode("K12.Retake.Shinmin.DetailContent.CourseTimeSectionViewContent", "課程時間表")]
    public partial class CourseTimeSectionViewContent : FISCA.Presentation.DetailContent
    {
        BackgroundWorker _bgWorker;
        bool _isBusy = false;
        List<UDTTimeSectionDef> _TimeSectionList;
        List<string> _CourseID = new List<string>();
        Dictionary<string, List<int>> _PeriodDict = new Dictionary<string, List<int>>();

        public CourseTimeSectionViewContent()
        {
            InitializeComponent();
            this.Group = "課程時間表";
            _bgWorker = new BackgroundWorker();
            _bgWorker.DoWork += new DoWorkEventHandler(_bgWorker_DoWork);
            _bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorker_RunWorkerCompleted);
            _TimeSectionList = new List<UDTTimeSectionDef>();
            RetakeEvents.RetakeChanged += new EventHandler(RetakeEvents_RetakeChanged);
        }

        void RetakeEvents_RetakeChanged(object sender, EventArgs e)
        {
            _BGRun();
        }

        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadData();
            this.Loading = false;
        }

        /// <summary>
        /// 載入資料到畫面
        /// </summary>
        private void LoadData()
        {
            // 清除ListView
            lvwTimeSection.Items.Clear();
            _PeriodDict.Clear();
            
            // 填值至ListView,先整理資料
            foreach (UDTTimeSectionDef data in _TimeSectionList)
            {
                string key = data.Date.ToShortDateString() + " (" + Utility.GetDayWeekString(data.Date) + ")";
                if (!_PeriodDict.ContainsKey(key))
                    _PeriodDict.Add(key, new List<int>());

                _PeriodDict[key].Add(data.Period);
            }

            foreach (KeyValuePair<string, List<int>> data in _PeriodDict)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = data.Key;

                for (int i = 1; i <= 8; i++)
                {
                    if (data.Value.Contains(i))
                        lvi.SubItems.Add("V");
                    else
                        lvi.SubItems.Add("");
                }
                lvwTimeSection.Items.Add(lvi);
            }
        }

        void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // 取得課程上課時間
            _TimeSectionList = UDTTransfer.UDTTimeSectionSelectByCourseIDList(_CourseID);

            // 排序
            _TimeSectionList = (from data in _TimeSectionList orderby data.Date select data).ToList();          
        }

        protected override void OnPrimaryKeyChanged(EventArgs e)
        {
            this.Loading = true;
            _CourseID.Clear();
            _CourseID.Add(PrimaryKey);
            _BGRun();    
        }

        private void _BGRun()
        {
            if (_bgWorker.IsBusy)
                _isBusy = true;
            else
            {
                this.Loading = true;
                _bgWorker.RunWorkerAsync();
            }        
        }

    }
}
