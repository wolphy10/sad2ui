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
    public partial class login : Form
    {
        public bool typeDone = false;
        public int typeMember = 0;

        public login()
        {
            InitializeComponent();
        }

        #region  Functions
        private void login_KeyDown(object sender, KeyEventArgs e)
        {
            confirm conf = new confirm();

            if (e.KeyCode == Keys.Escape)
            {
                conf.refToLogin = this;
                conf.boolLogin = true;
                conf.Show();
            }
        }

        private void resetColorMember()
        {
            btnRegister.Enabled = true; typeDone = true;
            btnRegister.ForeColor = Color.White;
            btnAdmin.BackColor = Color.White; btnSW.BackColor = Color.White; btnStaff.BackColor = Color.White;
        }
        #endregion

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUser.Clear();
            txtPass.Text = "password";     
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            main main = new main();

            //main.refToLogin = this;
            main.Show();
            this.Close();
        }

        #region Password
        private void btnHide_Click(object sender, EventArgs e)
        {
            txtPass.Font = new System.Drawing.Font("Segoe UI", 12.00F, System.Drawing.FontStyle.Bold);
            txtPass.PasswordChar = '·';
            btnHide.Hide();
            btnPeek.Show();
        }

        private void btnPeek_Click(object sender, EventArgs e)
        {
            txtPass.Font = new System.Drawing.Font("Segoe UI Semilight", 12.00F);
            txtPass.PasswordChar = '\0';
            btnPeek.Hide();
            btnHide.Show();
        }

        #endregion

        #region Member Type
        private void btnAdmin_Click(object sender, EventArgs e)
        {
            resetColorMember();
            btnAdmin.BackColor = System.Drawing.ColorTranslator.FromHtml("#e8e8e8");
            btnRegister.BackColor = System.Drawing.ColorTranslator.FromHtml("#d34949");
            btnRegister.Text = "Continue registration";
            typeMember = 1;
        }

        private void btnSW_Click(object sender, EventArgs e)
        {
            resetColorMember();
            btnSW.BackColor = System.Drawing.ColorTranslator.FromHtml("#e8e8e8");
            btnRegister.BackColor = System.Drawing.ColorTranslator.FromHtml("#cea728");
            btnRegister.Text = "Continue registration";
            typeMember = 2;
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            resetColorMember();
            btnStaff.BackColor = System.Drawing.ColorTranslator.FromHtml("#e8e8e8");
            btnRegister.BackColor = System.Drawing.ColorTranslator.FromHtml("#1191c7");            
            btnRegister.Text = "Continue registration";
            typeMember = 3;
        }
        #endregion

        #region Member or New Buttons
        private void btnMember_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = member;
            btnMember.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            btnMember.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            btnMember.BackColor = Color.White;

            btnNew.Font = new System.Drawing.Font("Segoe UI", 12F);
            btnNew.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c3c3c3");
            btnNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#e8e8e8");

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = newUser;
            btnNew.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            btnNew.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            btnNew.BackColor = Color.White;

            btnMember.Font = new System.Drawing.Font("Segoe UI", 12F);
            btnMember.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c3c3c3");
            btnMember.BackColor = System.Drawing.ColorTranslator.FromHtml("#e8e8e8");
        }
        #endregion

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Color set = new Color();
            error err = new error();
            register reg = new register();

            if (typeDone)
            {
                if(typeMember == 1) set = System.Drawing.ColorTranslator.FromHtml("#d34949");
                else if(typeMember == 2) set = System.Drawing.ColorTranslator.FromHtml("#c1a13a");
                else set = System.Drawing.ColorTranslator.FromHtml("#019cde");

                reg.refToLogin = this;
                reg.lblWelcome.ForeColor = set;
                reg.btnRegister.BackColor = set;
                reg.Show();
                this.Hide();
            }
            else
            {
                err.lblError.Text = "Please choose a member type.";
                err.Show();
            }            
        }
    }
}
