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
    public partial class dim : Form
    {
        public Form refToExpense { get; set; }

        public dim()
        {
            InitializeComponent();
        }

        private void dim_Load(object sender, EventArgs e)
        {
            refToExpense.Enabled = false;
        }

        private void dim_FormClosing(object sender, FormClosingEventArgs e)
        {
            refToExpense.Enabled = true;            
            refToExpense.Focus();
        }
    }
}