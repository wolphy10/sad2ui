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

    public partial class moneyDonate : Form
    {
        public MySqlConnection conn;
        public int donorID, donationID;

        public Form refToExpense { get; set; }
        public bool hasExpense;

        public moneyDonate()
        {
            InitializeComponent();
            conn = new MySqlConnection("server=localhost;user id=root;database=prototype_sad;password=root;persistsecurityinfo=False");
            dateCash.MaxDate = DateTime.Now; dateCash.Value = DateTime.Today;           
        }

        #region Movable Form
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void moneyDonate_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void tabChoice_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        #endregion

        #region Functions
        public void toGreen()
        {
            txtCashAmount.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            txtCashCent.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            lblCashAmount.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            lblDot1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
        }

        public void toDefault()
        {
            txtCashAmount.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dfdfdf");
            txtCashCent.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dfdfdf");
            lblCashAmount.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2d2d2d");
            lblDot1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dfdfdf");
        }

        public void toYellow()
        {
            txtCashAmount2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c4b617");
            txtCashCent2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c4b617");
            lblCashAmount2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c4b617");
            lblDot3.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c4b617");
        }
        #endregion

        #region Buttons
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (hasExpense)
            {
                hasExpense = false;
                refToExpense.Show();                
            }
            this.Close();
        }

        private void btnCash_Click(object sender, EventArgs e)
        {
            tabSelection.SelectedTab = tabCash;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            tabSelection.SelectedTab = tabCheck;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            tabSelection.SelectedIndex = 0;
        }
        #endregion

        #region Cash
        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) e.Handled = true;
        }

        private void txtCashAmount_Enter(object sender, EventArgs e)
        {
            if (txtCashAmount.Text == "0,000,000,000") txtCashAmount.Text = "";
            toGreen();
        }

        private void txtCashAmount_TextChanged(object sender, EventArgs e)
        {
            string value = txtCashAmount.Text.Replace(",", "");
            ulong ul;
            if (ulong.TryParse(value, out ul))
            {
                txtCashAmount.TextChanged -= txtCashAmount_TextChanged;
                txtCashAmount.Text = string.Format("{0:#,#}", ul);
                txtCashAmount.SelectionStart = txtCashAmount.Text.Length;
                txtCashAmount.TextChanged += txtCashAmount_TextChanged;
            }
        }

        private void txtCashAmount_Leave(object sender, EventArgs e)
        {
            if (txtCashAmount.Text == "") txtCashAmount.Text = "0,000,000,000";
            toDefault();
        }

        private void txtCashCent_Enter(object sender, EventArgs e)
        {
            if (txtCashAmount.Text == "00") txtCashAmount.Text = "";
            toGreen();
        }

        private void txtCashCent_Leave(object sender, EventArgs e)
        {
            if (txtCashAmount.Text == "00") txtCashAmount.Text = "";
            toDefault();
        }

        private void btnCashAdd_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                
                // ADD NEW DONATION
                MySqlCommand comm = new MySqlCommand("INSERT INTO donation (donationType, donorID, dateAdded)"
                    + " VALUES (1, " + donorID + ", + '" + DateTime.Now.ToString("yyyy-MM-dd") + "');", conn);

                comm.ExecuteNonQuery();

                // GET THAT DONATION ID
                comm = new MySqlCommand("SELECT donationID FROM donation ORDER BY donationID DESC LIMIT 1", conn);                // Get latest donation ID (previous addition)
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                int c_donationID = int.Parse(dt.Rows[0]["donationID"].ToString());                                                // Get current donation ID
                decimal amount = decimal.Parse(txtCashAmount.Text + "." + txtCashCent.Text);
               
                // ADD SQL COMMAND
                comm = new MySqlCommand("INSERT INTO monetary (paymentType, ORno, amount, TIN, dateDonated, donationID)"
                    + " VALUES (1, '" + txtOR.Text + "', " + amount + ", '" + txtTIN.Text + "', '" + dateCash.Value.Date.ToString("yyyyMMdd") + "', " + c_donationID + ");", conn);

                comm.ExecuteNonQuery();
                conn.Close();

                success yey = new success();                                
                yey.lblSuccess.Text = "Donation has been added successfully!";
                yey.ShowDialog();
                this.Close();            
                refToExpense.Show();      
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Cash Edit
        private void txtCashAmount2_Enter(object sender, EventArgs e)
        {
            if (txtCashAmount2.Text == "0,000,000,000") txtCashAmount2.Text = "";
            toYellow();
        }
        private void txtCashAmount2_TextChanged(object sender, EventArgs e)
        {
            string value = txtCashAmount2.Text.Replace(",", "");
            ulong ul;
            if (ulong.TryParse(value, out ul))
            {
                txtCashAmount2.TextChanged -= txtCashAmount2_TextChanged;
                txtCashAmount2.Text = string.Format("{0:#,#}", ul);
                txtCashAmount2.SelectionStart = txtCashAmount2.Text.Length;
                txtCashAmount2.TextChanged += txtCashAmount2_TextChanged;
            }
        }

        private void txtCashAmount2_Leave(object sender, EventArgs e)
        {
            if (txtCashAmount2.Text == "") txtCashAmount2.Text = "0,000,000,000";
            toDefault();
        }

        private void txtCashCent2_Enter(object sender, EventArgs e)
        {
            if (txtCashCent2.Text == "00") txtCashCent2.Text = "";
            toYellow();
        }

        private void txtCashCent2_Leave(object sender, EventArgs e)
        {
            if (txtCashCent2.Text == "") txtCashCent2.Text = "00";
            toDefault();
        }

        private void btnEditCash_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                // EDIT
                MySqlCommand comm = new MySqlCommand("UPDATE monetary SET ORno = '" + txtOR2.Text
                    + "', amount = " + decimal.Parse(txtCashAmount2.Text + "." + txtCashCent2.Text) + ", TIN = '" + txtTIN2.Text + "', dateDonated = '" + dateCash2.Value.Date.ToString("yyyyMMdd")
                    + "' WHERE donationID = " + donationID, conn);

                comm.ExecuteNonQuery();

                conn.Close();

                success yey = new success();
                yey.lblSuccess.Text = "Donation has been edited successfully!";
                yey.ShowDialog();
                this.Close();
                refToExpense.Show();
            }
            catch (Exception ex)
            {
                // do nothing
            }
        }
        #endregion


    }
}
