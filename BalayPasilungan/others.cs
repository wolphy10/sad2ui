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
    public partial class others : Form
    {
        public String[] month = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        public Form refToERF { get; set; }
        public eventorg ev = new eventorg();

        public others()
        {
            InitializeComponent();
        }

        #region Functions
        public void errorUp(int type)
        {
            error err = new error();
            if (type == 1)
            {
                err.lblError.Text = "Please enter the correct month name or number.";
                err.Show();
                txtMonth.Text = "January or 1";
            }
            else
            {
                err.lblError.Text = "Please enter year within the range from 1960 to 2099.";
                err.Show();
                txtYr.Clear();
            }
        }
        #endregion
        
        private void txtMonth_Enter(object sender, EventArgs e)
        {
            txtMonth.Text = "";             // Clear up default value
        }

        private void txtYr_Enter(object sender, EventArgs e)
        {
            txtYr.Text = "";                // Clear up default value
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            int go = 0, n = 0, m = 0;
            bool isNumeric = int.TryParse(txtMonth.Text, out n), isNumeric2 = int.TryParse(txtYr.Text, out m);            
            if (tabSelection.SelectedIndex == 0)     // Setting Custom Month
            {
                if (!isNumeric)                         //  Setting Custom Month (String input)
                {
                    foreach (String now in month)
                    {
                        if (now.Equals(txtMonth.Text, StringComparison.InvariantCultureIgnoreCase))
                        {
                            int i = Array.IndexOf(month, now);
                            lblMonth.Text = now;
                            ev.adjustCustom(0, ev.month[i]);
                            go = 1;
                        }
                    }

                    if (go == 0) errorUp(1);    
                }
                else                                    //  Setting Custom Month (Int input)
                {
                    if (int.Parse(txtMonth.Text) > 0 && int.Parse(txtMonth.Text) <= 12)
                    {
                        ev.adjustCustom(0, ev.month[int.Parse(txtMonth.Text) - 1]);
                        go = 1;
                    }
                    else errorUp(1);
                }
            }
            else                                    //  Setting Custom Year
            {
                if (isNumeric2 && int.Parse(txtYr.Text) >= 1960 && int.Parse(txtYr.Text) <= 2099)
                {
                    ev.adjustCustom(1, txtYr.Text);
                    go = 1;
                }
                else errorUp(2);
            }
            if (go == 1)
            {
                this.Hide();
                refToERF.Enabled = true;
                refToERF.Hide();
                ev.ShowDialog();
                refToERF.Close();
                this.Close();
            }            
        }

    }
}
