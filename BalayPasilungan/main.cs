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
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            
        }

        private void main_KeyDown(object sender, KeyEventArgs e)
        {
            /*confirm conf = new confirm();

            if (e.KeyCode == Keys.Escape)
            {
                conf.refToLogin = this;
                conf.boolLogin = true;
                conf.Show();
            }*/
        }    
    }
}
