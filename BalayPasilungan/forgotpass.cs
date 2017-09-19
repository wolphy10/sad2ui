using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using MySql.Data.MySqlClient;

namespace BalayPasilungan
{
    public partial class forgotpass : Form
    {
        public bool confirmed;
        public MySqlConnection conn;
        public MySqlCommand comm;
        public Form refToLogin { get; set; }

        public forgotpass()
        {
            conn = new MySqlConnection("server=localhost;user id=root;database=prototype_sad;password=root;persistsecurityinfo=False");
            InitializeComponent();
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

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text != "")
            {
                try
                {
                    MySqlCommand comm = new MySqlCommand("SELECT username, password FROM accounts WHERE email = '" + txtEmail.Text + "'", conn);
                    MySqlDataAdapter adp = new MySqlDataAdapter(comm); DataTable dt = new DataTable(); adp.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {                        
                        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                        client.EnableSsl = true;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential("balaypasilunganroot@gmail.com", "b@laYp@s1");

                        string body =
                            "[DO NOT REPLY]\n\nA request was made to send you your username and password for the Balay Pasilungan application."
                            + "\n\n\nUSERNAME: " + dt.Rows[0]["username"].ToString()
                            + "\nPASSWORD : " + dt.Rows[0]["password"].ToString()
                            + "\n\n\nIf you wish to change your password, please go to 'new member' in login page and select change password.";

                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        MailMessage message = new MailMessage(
                        "aclan@gmail.com", // From field
                        txtEmail.Text, // Recipient field
                        "Password Verification for Balay Pasilungan", // Subject of the email message
                        body // Email message body
                        );

                        client.Send(message);
                        successMessage("Password verification has been sent!");
                        refToLogin.Show();
                        this.Close();
                    }
                    else errorMessage("Email address does not exist.");
                }
                catch (Exception ex)
                {
                    errorMessage(ex.Message);
                }
            }
            else errorMessage("Empty email address!");
            
        }
    }
}
