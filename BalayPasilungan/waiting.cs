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
    public partial class waiting : Form
    {
        public int timeLeft = 50;
        public waiting()
        {
            InitializeComponent();
        }

        private void waiting_Activated(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timeLeft == 0)
            {
                timer1.Stop();
                this.Close();
            }
            else timeLeft -= 25;
        }
    }
}
