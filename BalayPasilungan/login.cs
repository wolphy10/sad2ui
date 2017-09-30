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
    public partial class login : Form
    {
        public MySqlConnection conn;
        public bool typeDone = false, pass = true;
        public int typeMember = 0;
        public string type;

        public login()
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=prototype_sad;Uid=root;Pwd=root;");
        }

        #region Movable Form
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void moveable_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        #endregion

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
        #endregion

        #region Buttons
        private void btnRegister_Click(object sender, EventArgs e)
        {
            Color set = new Color();
            error err = new error();
            register reg = new register();

            if (typeDone)
            {
                if (typeMember == 0) set = System.Drawing.ColorTranslator.FromHtml("#d34949");
                else if (typeMember == 1) set = System.Drawing.ColorTranslator.FromHtml("#c1a13a");
                else set = System.Drawing.ColorTranslator.FromHtml("#019cde");

                reg.refToLogin = this;
                reg.type = typeMember;
                reg.lblWelcome.ForeColor = reg.btnRegister.BackColor = set;                
                reg.Show();
                this.Hide();

                tabControl.SelectedIndex = 0;
            }
            else
            {
                err.lblError.Text = "Please choose a member type.";
                err.ShowDialog();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUser.Clear();
            txtPass.Text = "password";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUser.Text == "" || txtPass.Text == "" && txtUser.Text == "  username" || txtPass.Text == "  password")
            {
                errorMessage("Please enter necessary fields!");
            }
            else
            {
                try
                {
                    conn.Open();

                    MySqlCommand comm = new MySqlCommand("SELECT * FROM accounts WHERE username = '" + txtUser.Text + "' AND password = '" + txtPass.Text + "'", conn);
                    MySqlDataAdapter adp = new MySqlDataAdapter(comm); DataTable dt = new DataTable();

                    adp.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {
                        errorMessage("Incorrect username or password.");
                    }
                    else if (dt.Rows.Count == 1)
                    {
                        type = dt.Rows[0]["type"].ToString();
                        if (type == "0") type = "Admin";
                        else if (type == "1") type = "Social Worker";
                        else type = "others";

                        txtUser.Text = txtPass.Text = "";

                        main main = new main();
                        main.name = dt.Rows[0]["firstname"].ToString() + " " + dt.Rows[0]["lastname"].ToString();
                        main.refToLogin = this;
                        main.usertype = int.Parse(dt.Rows[0]["type"].ToString());
                        main.Show();
                        //MessageBox.Show(main.usertype.ToString());                        
                        this.Hide();
                    }
                    conn.Close();
                }
                catch (Exception ee)
                {
                    errorMessage(ee.Message);
                    conn.Close();
                }
            }
        }
        
        private void btnForgot_Click(object sender, EventArgs e)
        {
            forgotpass fp = new forgotpass();
            fp.refToLogin = this;
            this.Hide();
            fp.ShowDialog();       
        }

        #endregion

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
            btnRegister.Text = "CONTINUE REGISTRATION";
            typeMember = 0;
        }

        private void btnSW_Click(object sender, EventArgs e)
        {
            resetColorMember();
            btnSW.BackColor = System.Drawing.ColorTranslator.FromHtml("#e8e8e8");
            btnRegister.BackColor = System.Drawing.ColorTranslator.FromHtml("#cea728");
            btnRegister.Text = "CONTINUE REGISTRATION";
            typeMember = 1;
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            resetColorMember();
            btnStaff.BackColor = System.Drawing.ColorTranslator.FromHtml("#e8e8e8");
            btnRegister.BackColor = System.Drawing.ColorTranslator.FromHtml("#1191c7");            
            btnRegister.Text = "CONTINUE REGISTRATION";
            typeMember = 2;
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
    }
}
