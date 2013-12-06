using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using K12.Data;
using FISCA.Presentation;
using FISCA.Data;
using System.ComponentModel;
using K12.Retake.Shinmin.DAO;
using FISCA.UDT;
using Campus.Configuration;
using Campus.Windows;
using System.Text.RegularExpressions;

namespace K12.Retake.Shinmin
{
    /// <summary>
    /// 主要控制重補修資料
    /// </summary>
    public class RetakeAdmin:NLDPanel
    {
        BackgroundWorker _bgWorker = new BackgroundWorker();
        bool _isBusy = false;
        Dictionary<string, UDTCourseDef> _AllCourseDict = new Dictionary<string, UDTCourseDef>();
        Dictionary<string, List<string>> _CourseFilterDict = new Dictionary<string, List<string>>();
        
        /// <summary>
        ///搜尋
        /// </summary>
        SearchEventArgs SearEvArgs = null;

        /// <summary>
        /// 搜尋資料用
        /// </summary>
        private MenuButton SearchCourseName;
        private MenuButton SearchSubjectName;
        private MenuButton SearchSubjectType;

        private string FilterCourseItem = "";

        public RetakeAdmin()
        {
            Group = "重補修";
            UDTTimeListDef dd = UDTTransfer.UDTTimeListGetActiveTrue1();
            FilterCourseItem = dd.SchoolYear + " " + dd.Semester + " 第" + dd.Month + "梯次";

            _bgWorker.DoWork += new DoWorkEventHandler(_bgWorker_DoWork);
            _bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bgWorker_RunWorkerCompleted);
            // 註冊重補修事件
            RetakeEvents.RetakeChanged += new EventHandler(RetakeEvents_RetakeChanged);

            ListPaneField Field01 = new ListPaneField("課程名稱");
            Field01.GetVariable += delegate(object sender, GetVariableEventArgs e)
            {
                if (_AllCourseDict.ContainsKey(e.Key))
                    e.Value = _AllCourseDict[e.Key].CourseName;
            };
            this.AddListPaneField(Field01);

            ListPaneField Field02 = new ListPaneField("科目名稱");
            Field02.GetVariable += delegate(object sender, GetVariableEventArgs e)
            {
                if (_AllCourseDict.ContainsKey(e.Key))
                {
                    if (_AllCourseDict[e.Key].SubjectLevel.HasValue)
                        e.Value = _AllCourseDict[e.Key].SubjectName + QueryData.GetNumber(_AllCourseDict[e.Key].SubjectLevel.Value.ToString());
                    else
                        e.Value = _AllCourseDict[e.Key].SubjectName;
                    
                }
            };
            this.AddListPaneField(Field02);

            ListPaneField Field03 = new ListPaneField("級別");
            Field03.GetVariable += delegate(object sender, GetVariableEventArgs e)
            {
                if (_AllCourseDict.ContainsKey(e.Key))
                    if (_AllCourseDict[e.Key].SubjectLevel.HasValue)
                        e.Value = _AllCourseDict[e.Key].SubjectLevel.Value;
                    else
                        e.Value = "";
            };
            this.AddListPaneField(Field03);

            ListPaneField Field04 = new ListPaneField("學分數");
            Field04.GetVariable += delegate(object sender, GetVariableEventArgs e)
            {
                if (_AllCourseDict.ContainsKey(e.Key))
                    e.Value = _AllCourseDict[e.Key].Credit;
            };
            this.AddListPaneField(Field04);

            ListPaneField Field05 = new ListPaneField("科目類別");
            Field05.GetVariable += delegate(object sender, GetVariableEventArgs e)
            {
                if (_AllCourseDict.ContainsKey(e.Key))
                    e.Value = _AllCourseDict[e.Key].SubjectType;
            };
            this.AddListPaneField(Field05);

            ListPaneField Field06 = new ListPaneField("科別");
            Field06.GetVariable += delegate(object sender, GetVariableEventArgs e)
            {
                if (_AllCourseDict.ContainsKey(e.Key))
                    e.Value = _AllCourseDict[e.Key].DeptName;
            };
            this.AddListPaneField(Field06);

            ListPaneField Field07 = new ListPaneField("授課教師名稱");
            Field07.GetVariable += delegate(object sender, GetVariableEventArgs e)
            {
                if (_AllCourseDict.ContainsKey(e.Key))
                    if(Global._TeacherIDNameDict.ContainsKey(_AllCourseDict[e.Key].RefTeacherID))
                        e.Value = Global._TeacherIDNameDict[_AllCourseDict[e.Key].RefTeacherID];
            };
            this.AddListPaneField(Field07);

            // 設定搜尋欄位
            ConfigData cd = Config.User["K12.Retake.Shinmin.SearchOption"];
           SearchCourseName = SetSearchButton("課程名稱", "SearchCourseName", cd);
           SearchSubjectName = SetSearchButton("科目名稱","SearchSubjectName", cd);
           SearchSubjectType = SetSearchButton("科目類別", "SearchSubjectType", cd);

            FilterMenu.SupposeHasChildern = true;
            FilterMenu.PopupOpen += new EventHandler<PopupOpenEventArgs>(FilterMenu_PopupOpen);
            FilterMenu.Text = FilterCourseItem;
            this.Search += new EventHandler<SearchEventArgs>(RetakeAdmin_Search);
            _bgWorker.RunWorkerAsync();
        }

        /// <summary>
        /// 設定搜尋欄位
        /// </summary>
        /// <param name="name"></param>
        /// <param name="BoolName"></param>
        /// <param name="cd"></param>
        /// <returns></returns>
        private MenuButton SetSearchButton(string name, string BoolName, ConfigData cd)
        {
            MenuButton SearchName = SearchConditionMenu[name];
            SearchName.AutoCheckOnClick = true;
            SearchName.AutoCollapseOnClick = false;
            bool bo = false;
            bool.TryParse(cd[BoolName], out bo);
            SearchName.Checked = bo;
            SearchName.Click += delegate
            {
                cd[BoolName] = SearchName.Checked.ToString();
                BackgroundWorker async = new BackgroundWorker();
                async.DoWork += delegate(object sender, DoWorkEventArgs e)
                {
                    (e.Argument as ConfigData).Save();
                    async.RunWorkerAsync(cd);
                };
            
            };
            return SearchName;
        }

        void RetakeAdmin_Search(object sender, SearchEventArgs e)
        {
            SearEvArgs = e;
            BlockMessage.Display("資料搜尋中，請稍候 ...", new ProcessInvoker(ProcessSearch));
        }

        /// <summary>
        /// 搜尋資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FilterMenu_PopupOpen(object sender, PopupOpenEventArgs e)
        {
            if (!_bgWorker.IsBusy)
            {
                List<string> list = new List<string>();
                foreach (string str in _CourseFilterDict.Keys)
                    list.Add(str);
                list.Sort();


                foreach (string item in list)
                {
                    MenuButton mb = e.VirtualButtons[item];
                    mb.AutoCheckOnClick = true;
                    mb.AutoCollapseOnClick = true;
                    mb.Checked = (item == FilterCourseItem);
                    mb.Tag = item;
                    mb.CheckedChanged += delegate(object sender1, EventArgs e1)
                    {
                        MenuButton mb1 = sender1 as MenuButton;
                        SetRetakeList(mb1.Text);
                        FilterCourseItem = FilterMenu.Text = mb1.Text;
                        mb1.Checked = true;
                    };
                }
            }
            else 
            {
                e.Cancel = true;
                e.VirtualButtons.Text = "資料下載中..";
            }
         
        }

        private void ProcessSearch(MessageArgs args)
        {
            List<string> results = new List<string>();
            Regex rx = new Regex(SearEvArgs.Condition, RegexOptions.IgnoreCase);

            // 課程名稱
            if (SearchCourseName.Checked)
            {
                foreach (string key in _AllCourseDict.Keys)
                {
                    if (rx.Match(_AllCourseDict[key].CourseName).Success)
                        if (!results.Contains(key))
                            results.Add(key);
                }
            }

            // 科目名稱
            if (SearchSubjectName.Checked)
            {
                foreach (string key in _AllCourseDict.Keys)
                {
                    if (rx.Match(_AllCourseDict[key].SubjectName).Success)
                        if (!results.Contains(key))
                            results.Add(key);
                }
            }
            // 科目類別
            if (SearchSubjectType.Checked)
            {
                foreach (string key in _AllCourseDict.Keys)
                {
                    if (rx.Match(_AllCourseDict[key].SubjectType).Success)
                        if (!results.Contains(key))
                            results.Add(key);
                }
            }
           
        }

        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_isBusy)
            {
                _bgWorker.RunWorkerAsync();
                return;
            }
            //SetFilteredSource(_AllCourseDict.Keys.ToList());
            SetRetakeList(FilterMenu.Text);
        }

        private void SetRetakeList(string name)
        {
            if (_CourseFilterDict.ContainsKey(name))
            {
                SetFilteredSource(_CourseFilterDict[name]);
            }
            else
            {
                SetFilteredSource(new List<string>());
            }

        }

        void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ReloadRetakeData();
        }

        void RetakeEvents_RetakeChanged(object sender, EventArgs e)
        {
            if (_bgWorker.IsBusy)
            {
                _isBusy = true;
            }
            else
                _bgWorker.RunWorkerAsync();
            
        }

        /// <summary>
        /// 重新讀取重補修課程資料
        /// </summary>
        private void ReloadRetakeData()
        {
            // 取得教師對照
            Global.RelaodTeacherDict();
            // 科別對照
            Global.ReloadDeptNameList();

            // 取得重補修課程資料
            _AllCourseDict = UDTTransfer.UDTCourseSelectAllDict();
            
          
            _CourseFilterDict.Clear();
            foreach (UDTCourseDef data in _AllCourseDict.Values)
            {   
                string key = data.SchoolYear + " " + data.Semester + " 第" + data.Month + "梯次";
                if (!_CourseFilterDict.ContainsKey(key))
                    _CourseFilterDict.Add(key, new List<string>());

                _CourseFilterDict[key].Add(data.UID);
            }

        }

        private static RetakeAdmin _RetakeAdmin;

        public static RetakeAdmin Instance
        {
            get 
            {
                if (_RetakeAdmin == null)
                    _RetakeAdmin = new RetakeAdmin();

                return _RetakeAdmin;
            }
        }
    }
}
