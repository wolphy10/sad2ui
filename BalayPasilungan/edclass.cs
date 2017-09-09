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
    public partial class edclass : Form
    {
        public caseprofile reftocase { get; set; }
        public edclass()
        {
            InitializeComponent();
        }

        public string level { get; set; }

        private void btncanceledclass_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnaddedclass_Click(object sender, EventArgs e)
        {

        }
    }
}
