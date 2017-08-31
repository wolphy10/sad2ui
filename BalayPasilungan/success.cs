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
    public partial class success : Form
    {
        public eventorg reftoevorg { get; set; }
        public string message { get; set; }
        public success()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void success_Load(object sender, EventArgs e)
        {
            lblSuccess.Text = message;
        }
    }
}
