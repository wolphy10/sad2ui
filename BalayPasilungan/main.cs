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
        public bool maxClicked = true;
        private void btnClose_Click_1(object sender, EventArgs e)
        {
            if (maxClicked)
            {
                btnClose.Image = Properties.Resources.compress;
                this.WindowState = FormWindowState.Maximized;
                maxClicked = false;
                btnPanelExpense.Size = new Size(basePanelExpense.Size.Width, basePanelExpense.Size.Height);
                btnPanelEvent.Size = new Size(basePanelEvent.Size.Width, basePanelEvent.Size.Height);
                btnPanelCase.Size = new Size(basePanelCase.Size.Width, basePanelCase.Size.Height);
                btnPanelExpense.Location = new Point(0, -600);
                btnPanelEvent.Location = new Point(0, -600);
                btnPanelCase.Location = new Point(0, -600);
                notifPanel.Size = new Size(350, 987);
                notifPanel.Location = new Point(1919, 37);
            }
            else
            {
                btnClose.Image = Properties.Resources.expand;
                this.WindowState = FormWindowState.Normal;
                maxClicked = true;
                btnPanelExpense.Size = new Size(basePanelExpense.Size.Width, basePanelExpense.Size.Height);
                btnPanelEvent.Size = new Size(basePanelEvent.Size.Width, basePanelEvent.Size.Height);
                btnPanelCase.Size = new Size(basePanelCase.Size.Width, basePanelCase.Size.Height);
                btnPanelExpense.Location = new Point(0, -600);
                btnPanelEvent.Location = new Point(0, -600);
                btnPanelCase.Location = new Point(0, -600);
                notifPanel.Size = new Size(247, 652);
                notifPanel.Location = new Point(1303, 37);
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        public bool notifbool = true;
        private void button2_Click(object sender, EventArgs e)
        {
            if (notifbool)
            {
                timernum = 3; BorF = 0;
                timer1.Start();
                notifbool = false;
            }
            else
            {
                timernum = 3; BorF = 1;
                timer1.Start();
                notifbool = true;
            }
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }
        public int timernum, BorF;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timernum == 0)//finance
            {
                if (BorF == 0)
                {
                    btnPanelExpense.Top += 8;
                    if (btnPanelExpense.Top >= 0) timer1.Stop();
                }
                else if(BorF == 1)
                {
                    btnPanelExpense.Top -= 8;
                    if (btnPanelExpense.Top <= -392) timer1.Stop();
                }
            }else if(timernum == 1)//event
            {
                if(BorF == 0)
                {
                    btnPanelEvent.Top += 8;
                    if (btnPanelEvent.Top >= 0) timer1.Stop();
                }
                else if(BorF == 1)
                {
                    btnPanelEvent.Top -= 8;
                    if (btnPanelEvent.Top <= -392) timer1.Stop();
                }
            }
            else if (timernum == 2)//case
            {
                if (BorF == 0)
                {
                    btnPanelCase.Top += 8;
                    if (btnPanelCase.Top >= 0) timer1.Stop();
                }
                else if (BorF == 1)
                {
                    btnPanelCase.Top -= 8;
                    if (btnPanelCase.Top <= -392) timer1.Stop();
                }
            }else if(timernum == 3)//notification panel
            {
                if (maxClicked)
                {
                    if (BorF == 0)
                    {
                        notifPanel.Left -= 8;
                        if (notifPanel.Left <= 1060) timer1.Stop();
                    }
                    else if (BorF == 1)
                    {
                        notifPanel.Left += 8;
                        if (notifPanel.Left >= 1303) timer1.Stop();
                    }
                }
                else
                {
                    if (BorF == 0)
                    {
                        notifPanel.Left -= 8;
                        if (notifPanel.Left <= 1575) timer1.Stop();
                    }
                    else if (BorF == 1)
                    {
                        notifPanel.Left += 8;
                        if (notifPanel.Left >= 1919) timer1.Stop();
                    }
                }
            }
        }

        private void basePanelExpense_MouseHover(object sender, EventArgs e)
        {
            timernum = 0; BorF = 0;
            timer1.Start();
        }

        private void basePanelCase_MouseHover(object sender, EventArgs e)
        {
            timernum = 2; BorF = 0;
            timer1.Start();
        }

        private void basePanelEvent_MouseHover(object sender, EventArgs e)
        {
            timernum = 1; BorF = 0;
            timer1.Start();
        }
    }
}
