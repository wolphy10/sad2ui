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
    public partial class register : Form
    {
        public Form refToLogin { get; set; }

        public register()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            refToLogin.Show();
            this.Close();
        }

        #region Textboxes
        private void txtFName_Enter(object sender, EventArgs e)
        {
            lblFName.ForeColor = lblWelcome.ForeColor;
            countFName.Visible = true; countFName.ForeColor = lblWelcome.ForeColor;
        }

        private void txtFName_TextChanged(object sender, EventArgs e)
        {
            countFName.Text = txtFName.Text.Length + "/50";
        }

        private void txtFName_Leave(object sender, EventArgs e)
        {
            txtFName.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);
            lblFName.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
            countFName.Visible = false;
        }

        private void txtLName_Enter(object sender, EventArgs e)
        {
            lblLName.ForeColor = lblWelcome.ForeColor;
            countLName.Visible = true; countLName.ForeColor = lblWelcome.ForeColor;
        }

        private void txtLName_TextChanged(object sender, EventArgs e)
        {
            countLName.Text = txtLName.Text.Length + "/50";
        }

        private void txtLName_Leave(object sender, EventArgs e)
        {
            txtLName.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);
            lblLName.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
            countLName.Visible = false;
        }

        private void txtUser_Enter(object sender, EventArgs e)
        {
            lblUser.ForeColor = lblWelcome.ForeColor;
            countUser.Visible = true; countUser.ForeColor = lblWelcome.ForeColor;
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {
            countUser.Text = txtUser.Text.Length + "/15";
        }

        private void txtUser_Leave(object sender, EventArgs e)
        {
            txtUser.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);
            lblUser.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
            countUser.Visible = false;
        }

        private void txtPass_Enter(object sender, EventArgs e)
        {
            lblPass.ForeColor = lblWelcome.ForeColor;
            countPass.Visible = true; countPass.ForeColor = lblWelcome.ForeColor;
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            countPass.Text = txtPass.Text.Length + "/15";
        }

        private void txtPass_Leave(object sender, EventArgs e)
        {
            txtPass.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);
            lblPass.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
            countPass.Visible = false;
        }
        #endregion

        private void btnRegister_Click(object sender, EventArgs e)
        {
            error err = new error();

            if(txtFName.Text.Equals("") || txtLName.Equals("") || txtPass.Equals("") || txtUser.Equals(""))
            {
                err.lblError.Text = "Please fill all information.";
                err.Show();              
            }
        }
    }
}
