using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;

namespace K12.Retake.Shinmin.Dylan
{
    public partial class phChangeData : BaseForm
    {
        List<string> _list { get; set; }

        public string selectName = "";

        public phChangeData(string title, List<string> list)
        {
            InitializeComponent();

            lbHelp.Text = title;
            _list = list;
        }

        private void phChangeData_Load(object sender, EventArgs e)
        {
            cbSelectItem.Items.AddRange(_list.ToArray());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbSelectItem.Text))
            {
                selectName = cbSelectItem.Text;
                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.No;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
        }
    }
}
