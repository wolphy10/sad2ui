using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BalayPasilungan
{
    public partial class dialogEvOptions : Form
    {
        public eventorg reftoevorg { get; set; }
        public dialogEvOptions()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dialogEvOptions_Load(object sender, EventArgs e)
        {
            string options = reftoevorg.option;
            //MessageBox.Show(options);
            if (options == "view")
            {
                tabControl1.SelectedTab = tabPage1;
            }
            else if (options == "noview")
            {
                tabControl1.SelectedTab = tabPage2;
            }
        }

        private void dialogEvOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            reftoevorg.Show();
        }

        private void add2_Click(object sender, EventArgs e)
        {
            reftoevorg.ifclick = "add";
            this.DialogResult = DialogResult.OK;
        }

        private void add1_Click(object sender, EventArgs e)
        {
            reftoevorg.ifclick = "add";
            this.DialogResult = DialogResult.OK;
        }

        private void view_Click(object sender, EventArgs e)
        {
            reftoevorg.ifclick = "view";
            this.DialogResult = DialogResult.OK;
        }
    }
}
