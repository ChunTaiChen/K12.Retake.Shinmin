using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation;
using K12.Data;
using FISCA.UDT;
using K12.Retake.Shinmin.DAO;

namespace K12.Retake.Shinmin
{
    public partial class RetakeViewTree : NavView
    {
        Dictionary<string, List<string>> _SubjectDict = new Dictionary<string, List<string>>();

        string _CurrentNode = "";

        public RetakeViewTree()
        {
            InitializeComponent();
            NavText = "重補修檢視";
            SourceChanged += new EventHandler(RetakeView_SourceChanged);
        }

        void RetakeView_SourceChanged(object sender, EventArgs e)
        {

            if (advTree1.SelectedNode !=null && advTree1.SelectedNode.Tag != null)
                _CurrentNode = "" + advTree1.SelectedNode.Tag;

            _SubjectDict.Clear();
            advTree1.Nodes.Clear();

            DevComponents.AdvTree.Node Node1 = new DevComponents.AdvTree.Node();
            Node1.Text = "所有分類(" + Source.Count + ")";
            Node1.Tag = "All";
            advTree1.Nodes.Add(Node1);

            if (Source.Count != 0)
            {
                List<string> list = new List<string>();
                List<UDTCourseDef> dataList = UDTTransfer.UDTCourseSelectUIDs(Source.ToList());

                foreach (UDTCourseDef data in dataList)
                {
                    if (string.IsNullOrWhiteSpace(data.SubjectName))
                    {
                        list.Add(data.UID);
                        continue;
                    }

                    if(!_SubjectDict.ContainsKey(data.SubjectName))
                        _SubjectDict.Add(data.SubjectName,new List<string>());

                    _SubjectDict[data.SubjectName].Add(data.UID);
                }
                _SubjectDict.Add("未分類", list);

                foreach (string str in _SubjectDict.Keys)
                {
                    DevComponents.AdvTree.Node node2 = new DevComponents.AdvTree.Node();
                    node2.Text = str + "(" + _SubjectDict[str].Count + ")";
                    node2.Tag = str;
                    Node1.Nodes.Add(node2);
                }

                if (string.IsNullOrWhiteSpace(_CurrentNode) || _CurrentNode == "All")
                {
                    advTree1.SelectedNode = Node1;
                    SetListPaneSource(Source, false, false);
                }
                else
                {
                    foreach (DevComponents.AdvTree.Node each in Node1.Nodes)
                    { 
                        if(""+each.Tag ==_CurrentNode)
                        {
                            if (_SubjectDict.ContainsKey(_CurrentNode))
                            {
                                advTree1.SelectedNode = each;
                                SetListPaneSource(_SubjectDict[_CurrentNode], false, false);
                                return;
                            }
                        }
                    }
                }
            }
            else
            {
                SetListPaneSource(Source, false, false);
            }


        }

        private void advTree1_NodeClick(object sender, DevComponents.AdvTree.TreeNodeMouseEventArgs e)
        {
            // 判斷是否按 Control,Shift
            bool SelectedAll = (Control.ModifierKeys & Keys.Control) == Keys.Control;
            bool AddToTemp = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;

            if ("" + e.Node.Tag == "All")
            {
                SetListPaneSource(Source, SelectedAll, AddToTemp);
            }
            else if (_SubjectDict.ContainsKey("" + e.Node.Tag))
            {   // 使用者選擇類別名稱
                SetListPaneSource(_SubjectDict["" + e.Node.Tag], SelectedAll, AddToTemp);
            }
            else
            {   // 都沒選
            }
        }
    }
}
