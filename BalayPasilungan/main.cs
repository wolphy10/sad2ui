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
            /*if (maxClicked)
            {
                btnClose.Image = Properties.Resources.compress;
                this.WindowState = FormWindowState.Maximized;
                maxClicked = false;
                rightPartMain.Size = new Size(1570, 713);
                btnPanelExpense.Size = new Size(basePanelExpense.Size.Width, basePanelExpense.Size.Height);
                btnPanelEvent.Size = new Size(basePanelEvent.Size.Width, basePanelEvent.Size.Height);
                btnPanelCase.Size = new Size(basePanelCase.Size.Width, basePanelCase.Size.Height);
                notifPanel.Size = new Size(350, 987);
                notifPanel.Location = new Point(0, 37);
                //btnPanelExpense.Location = new Point(0, -600);
                //btnPanelEvent.Location = new Point(0, -600);
                //btnPanelCase.Location = new Point(0, -600);
                //notifPanel.Location = new Point(1919, 37);
            }
            else
            {
                btnClose.Image = Properties.Resources.expand;
                this.WindowState = FormWindowState.Normal;
                maxClicked = true;
                rightPartMain.Size = new Size(1053, 713);
                btnPanelExpense.Size = new Size(basePanelExpense.Size.Width, basePanelExpense.Size.Height);
                btnPanelEvent.Size = new Size(basePanelEvent.Size.Width, basePanelEvent.Size.Height);
                btnPanelCase.Size = new Size(basePanelCase.Size.Width, basePanelCase.Size.Height);
                notifPanel.Size = new Size(247, 713);
                notifPanel.Location = new Point(0, 37);
                //btnPanelExpense.Location = new Point(0, -600);
                //btnPanelEvent.Location = new Point(0, -600);
                //btnPanelCase.Location = new Point(0, -600);
                //notifPanel.Location = new Point(1303, 37);
            }*/
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            timernum = 0; BorF = 0;
            timer1.Start();
            rightPartMain.Size = new Size(1169, rightPartMain.Height);
            btnPanelExpense.Size = new Size(basePanelExpense.Size.Width, basePanelExpense.Size.Height);
            btnPanelEvent.Size = new Size(basePanelEvent.Size.Width, basePanelEvent.Size.Height);
            btnPanelCase.Size = new Size(basePanelCase.Size.Width, basePanelCase.Size.Height);
            //MessageBox.Show("Expense: " + btnPanelExpense.Size.ToString() + " Event: " + btnPanelEvent.Size.ToString() + " Case: " + btnPanelCase.Size.ToString());
           //MessageBox.Show("Expense: " + basePanelExpense.Size.ToString() + " Event: " + basePanelEvent.Size.ToString() + " Case: " + basePanelCase.Size.ToString());
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }
        public int timernum, BorF, BorF2;

        private void button6_Click(object sender, EventArgs e)
        {
            timernum = 0; BorF = 1;
            timer1.Start();
        }
        public int widthsum;

        private void btnCase_Click(object sender, EventArgs e)
        {
            caseprofile caseprof = new caseprofile();
            caseprof.reftomain = this;
            caseprof.Show();
            this.Hide();
        }

        private void btnEvent_Click(object sender, EventArgs e)
        {
            eventorg org = new eventorg();
            org.reftomain = this;
            org.Show();
            this.Hide();
        }

        private void btnexp_Click(object sender, EventArgs e)
        {
            expense exp = new expense();
            exp.reftomain = this;
            exp.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            /*if (timernum == 0)//finance
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
            }else */
            if (timernum == 0)//notification panel
            {
                if (maxClicked)
                {
                    if (BorF == 0)
                    {
                        notifPanel.Left -= 9;
                        if (notifPanel.Left <= -277)
                        {
                            timer1.Stop();
                        }
                    }
                    else if (BorF == 1)
                    {
                        notifPanel.Left += 9;
                        if (notifPanel.Left >= 0) {
                            timer1.Stop();
                            rightPartMain.Size = new Size(976, rightPartMain.Height);
                            btnPanelExpense.Size = new Size(basePanelExpense.Size.Width, basePanelExpense.Size.Height);
                            btnPanelEvent.Size = new Size(basePanelEvent.Size.Width, basePanelEvent.Size.Height);
                            btnPanelCase.Size = new Size(basePanelCase.Size.Width, basePanelCase.Size.Height);
                            //MessageBox.Show("Expense: " + btnPanelExpense.Size.ToString() + " Event: " + btnPanelEvent.Size.ToString() + " Case: " + btnPanelCase.Size.ToString());
                        }
                    }
                }
                else
                {
                    if (BorF == 0)
                    {
                        notifPanel.Left -= 9;
                        if (notifPanel.Left <= 1575) timer1.Stop();
                    }
                    else if (BorF == 1)
                    {
                        notifPanel.Left += 9;
                        if (notifPanel.Left >= 1919) timer1.Stop();
                    }
                }
            }
        }

    }
}
