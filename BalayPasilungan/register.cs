using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BalayPasilungan
{
    public partial class register : Form
    {
        public Form refToLogin { get; set; }
        public bool confirmed;
        public MySqlConnection conn;
        public MySqlCommand comm;
        public int type;
        
        public register()
        {
            InitializeComponent();
            conn = new MySqlConnection("server=localhost;user id=root;database=prototype_sad;password=root;persistsecurityinfo=False");

            dateBirth.MaxDate = DateTime.Today; dateBirth.Value = dateBirth.Value;
        }

        #region Functions
        public void errorMessage(string message)            // Error Message
        {
            error err = new error();
            dim dim = new dim();

            dim.Location = this.Location; dim.Size = this.Size;
            err.lblError.Text = message;
            dim.refToPrev = this;
            dim.Show(this);

            if (err.ShowDialog() == DialogResult.OK) dim.Close();
        }

        public void successMessage(string message)            // Success Message
        {
            success yey = new success();
            dim dim = new dim();

            dim.Location = this.Location; dim.Size = this.Size;
            yey.lblSuccess.Text = message;
            dim.refToPrev = this;
            dim.Show(this);

            if (yey.ShowDialog() == DialogResult.OK) dim.Close();
        }

        public void confirmMessage(string message)            // Success Message
        {
            confirm conf = new confirm();
            dim dim = new dim();

            dim.Location = this.Location; dim.Size = this.Size;
            dim.refToPrev = this;
            dim.Show(this);

            conf.lblConfirm.Text = message;
            if (conf.ShowDialog() == DialogResult.OK) confirmed = true;
            else confirmed = false;
            dim.Close();
        }
        #endregion

        #region Main Buttons
        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtFName.Text.Equals("") || txtLName.Equals("") || txtPass.Equals("") || txtUser.Equals("") || txtEmail.Equals("")) errorMessage("Please fill all boxes");
            else
            {
                confirmMessage("Are you sure you want to create this new profile?");
                if (confirmed)
                {
                    try
                    {
                        conn.Open();

                        MySqlCommand comm = new MySqlCommand("INSERT INTO accounts (firstname, lastname, username, password, birthdate, email, type)"
                            + " VALUES ('" + txtFName.Text + "', '" + txtLName.Text + "', '" + txtUser.Text + "', '" + txtPass.Text + "', '" + dateBirth.Value.ToString("yyyy-MM-dd") + "', '" + txtEmail.Text + "', " + type + ")", conn);
                        comm.ExecuteNonQuery();

                        successMessage("Profile added successfully!");
                        conn.Close();
                        
                        refToLogin.Show();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        errorMessage(ex.Message);
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            refToLogin.Show();
            this.Close();
        }
        #endregion        

        #region Textboxes
        private void txtNew_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).ForeColor = Color.Black;            

            if (((TextBox)sender).Name == "txtFName")
            {
                lblFName.ForeColor = countFName.ForeColor = lblWelcome.ForeColor;
                countFName.Visible = true;
            }
            else if (((TextBox)sender).Name == "txtLName")
            {
                lblLName.ForeColor = countLName.ForeColor = lblWelcome.ForeColor;
                countLName.Visible = true;
            }
            else if (((TextBox)sender).Name == "txtUser")
            {
                lblUser.ForeColor = countUser.ForeColor = lblWelcome.ForeColor;
                countUser.Visible = true;
            }
            else if (((TextBox)sender).Name == "txtPass")
            {
                lblPass.ForeColor = countPass.ForeColor = lblWelcome.ForeColor;
                countPass.Visible = true;
            }
            else if (((TextBox)sender).Name == "txtEmail") lblEmail.ForeColor = countPass.ForeColor = lblWelcome.ForeColor;            
        }

        private void txtNew_Leave(object sender, EventArgs e)
        {
            ((TextBox)sender).ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);

            if (((TextBox)sender).Name == "txtFName")
            {
                lblFName.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                countFName.Visible = false;
            }
            else if (((TextBox)sender).Name == "txtLName")
            {
                lblLName.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                countLName.Visible = false;
            }
            else if (((TextBox)sender).Name == "txtUser")
            {
                lblUser.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                countUser.Visible = false;
            }
            else if (((TextBox)sender).Name == "txtPass")
            {
                lblPass.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                countPass.Visible = false;
            }
            else if (((TextBox)sender).Name == "txtEmail") lblEmail.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Name == "txtFName") countFName.Text = ((TextBox)sender).Text.Length + "/50";
            else if (((TextBox)sender).Name == "txtLName") countLName.Text = ((TextBox)sender).Text.Length + "/50";
            else if (((TextBox)sender).Name == "txtUser") countUser.Text = ((TextBox)sender).Text.Length + "/15";
            else if (((TextBox)sender).Name == "txtPass") countPass.Text = ((TextBox)sender).Text.Length + "/15";            
        }
        #endregion
    }
}
