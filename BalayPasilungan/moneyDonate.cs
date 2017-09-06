using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
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
        public Form refToDim { get; set; }
        public bool hasExpense;

        public moneyDonate()
        {
            InitializeComponent();
            conn = new MySqlConnection("server=localhost;user id=root;database=prototype_sad;password=root;persistsecurityinfo=False");

            // Add
            dateCash.MaxDate = DateTime.Now; dateCash.Value = DateTime.Today;
            dateCheck.MaxDate = DateTime.Now; dateCheck.Value = DateTime.Today; dateOfCheck.MaxDate = DateTime.Now; dateOfCheck.Value = DateTime.Today;
            dateIK.MaxDate = DateTime.Now; dateIK.Value = DateTime.Now;
        }
        
        private void moneyDonate_FormClosing(object sender, FormClosingEventArgs e)
        {
            refToDim.Close();
            refToExpense.Enabled = true;
            refToExpense.Focus();
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

            txtCheckAmount.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            txtCheckCent.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            lblCheckAmount.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            lblDot2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
        }

        public void toDefault()
        {
            // ADD
            txtCashAmount.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dfdfdf");
            txtCashCent.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dfdfdf");
            lblCashAmount.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);
            lblDot1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dfdfdf");

            txtCheckAmount.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dfdfdf");
            txtCheckCent.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dfdfdf");
            lblCheckAmount.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);
            lblDot2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dfdfdf");

            // EDIT
            txtCashAmount2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dfdfdf");
            txtCashCent2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dfdfdf");
            lblCashAmount2.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);
            lblDot3.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dfdfdf");

            txtCheckAmount2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dfdfdf");
            txtCheckCent2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dfdfdf");
            lblCheckAmount2.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);
            lblDot4.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dfdfdf");
        }

        public void toYellow()
        {
            txtCashAmount2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c4b617");
            txtCashCent2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c4b617");
            lblCashAmount2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c4b617");
            lblDot3.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c4b617");

            txtCheckAmount2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c4b617");
            txtCheckCent2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c4b617");
            lblCheckAmount2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c4b617");
            lblDot4.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c4b617");
        }

        
        #endregion

        #region Buttons
        private void btnClose_Click(object sender, EventArgs e)
        {
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
        #endregion

        #region SQL Connection
        public void addSQL(MySqlCommand comm, int type)
        {
            MySqlDataAdapter adp = new MySqlDataAdapter(comm);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            int c_donationID = int.Parse(dt.Rows[0]["donationID"].ToString());                                                // Get current donation ID            

            // ADD SQL COMMAND
            if(type == 1)
            {   // lmao

                decimal amount = decimal.Parse(txtCashAmount.Text + "." + txtCashCent.Text);
                comm = new MySqlCommand("INSERT INTO monetary (paymentType, ORno, amount, dateDonated, donationID)"
                + " VALUES ('Cash', '" + txtOR.Text + "', " + amount + ", '" + dateCash.Value.Date.ToString("yyyyMMdd") + "', " + c_donationID + ");", conn);
            }
            else if(type == 2)
            {
                decimal amount = decimal.Parse(txtCheckAmount.Text + "." + txtCheckCent.Text);
                comm = new MySqlCommand("INSERT INTO monetary (paymentType, ORno, amount, checkNO, bankName, dateCheck, dateDonated, donationID)"
                + " VALUES ('Check', '" + txtOR.Text + "', " + amount + ", '" + txtCheckNo.Text + "', '" + txtBank.Text + "', '"
                + dateOfCheck.Value.Date.ToString("yyyyMMdd") + "', '" + dateCheck.Value.Date.ToString("yyyyMMdd") + "', " + c_donationID + ");", conn);
            }
            else if (type == 3)
            {
                comm = new MySqlCommand("INSERT INTO inkind (particular, quantity, dateDonated, donationID)"
                + " VALUES ('" + txtPart.Text + "', " + txtQuantity.Text + ", '" + dateIK.Value.Date.ToString("yyyyMMdd") + "', " + c_donationID + ");", conn);
            }

            comm.ExecuteNonQuery();
            conn.Close();

            success yey = new success();
            yey.lblSuccess.Text = "Donation has been added successfully!";
            yey.ShowDialog();
            this.Close();
        }

        public void editSQL(MySqlCommand comm)
        {
            comm.ExecuteNonQuery();
            conn.Close();

            success yey = new success();
            yey.lblSuccess.Text = "Donation has been edited successfully!";
            yey.ShowDialog();
            this.Close();
        }
        #endregion

        #region Back Buttons
        private void btnCashBack_Click(object sender, EventArgs e)
        {
            tabSelection.SelectedTab = tabChoice;
            txtBank.Clear(); txtCashAmount.Text = "0,000,000,000"; txtCashAmount2.Text = "0,000,000,000";
            txtOR.Clear(); txtCheckOR.Clear(); txtCheckNo.Clear();
        }

        private void btnCashBack2_Click(object sender, EventArgs e)
        {
            this.Close();
            refToExpense.Enabled = true;
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
                addSQL(comm, 1);    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Cash Edit
        private void btnEditCash_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand("UPDATE monetary SET ORno = '" + txtOR2.Text
                        + "', amount = " + decimal.Parse(txtCashAmount2.Text + "." + txtCashCent2.Text) + ", dateDonated = '" + dateCash2.Value.Date.ToString("yyyyMMdd")
                        + "' WHERE donationID = " + donationID, conn);
                editSQL(comm);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

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
        #endregion
        
        #region Check
        private void txtCheckAmount_TextChanged(object sender, EventArgs e)
        {
            string value = txtCheckAmount.Text.Replace(",", "");
            ulong ul;
            if (ulong.TryParse(value, out ul))
            {
                txtCheckAmount.TextChanged -= txtCheckAmount_TextChanged;
                txtCheckAmount.Text = string.Format("{0:#,#}", ul);
                txtCheckAmount.SelectionStart = txtCheckAmount.Text.Length;
                txtCheckAmount.TextChanged += txtCheckAmount_TextChanged;
            }
        }

        private void btnCheckAdd_Click(object sender, EventArgs e)
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
                addSQL(comm, 2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void txtCheckAmount_Enter(object sender, EventArgs e)
        {
            if (txtCheckAmount.Text == "0,000,000,000") txtCheckAmount.Text = "";
            toGreen();
        }

        private void txtCheckAmount_Leave(object sender, EventArgs e)
        {
            if (txtCheckAmount.Text == "") txtCheckAmount.Text = "0,000,000,000";
            toDefault();
        }
        private void txtCheckCent_Enter(object sender, EventArgs e)
        {
            if (txtCheckCent.Text == "00") txtCheckCent.Text = "";
            toGreen();
        }

        private void txtCheckCent_Leave(object sender, EventArgs e)
        {
            if (txtCheckCent.Text == "00") txtCheckCent.Text = "";
            toDefault();
        }

        #endregion

        #region Check Edit
        private void btnEditCheck_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand("UPDATE monetary SET ORno = '" + txtCheckOR2.Text + "', checkNo = '" + txtCheckNo2.Text
                    + "', amount = " + decimal.Parse(txtCheckAmount2.Text + "." + txtCheckCent2.Text) + ", bankName = '" + txtBank2.Text
                    + "', dateDonated = '" + dateCheck2.Value.Date.ToString("yyyyMMdd") + "', dateCheck = '" + dateOfCheck2.Value.Date.ToString("yyyyMMdd")
                    + "' WHERE donationID = " + donationID, conn);
                editSQL(comm);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtCheckAmount2_Enter(object sender, EventArgs e)
        {
            if (txtCheckAmount2.Text == "0,000,000,000") txtCheckAmount2.Text = "";
            toYellow();
        }

        private void txtCheckAmount2_TextChanged(object sender, EventArgs e)
        {
            string value = txtCheckAmount2.Text.Replace(",", "");
            ulong ul;
            if (ulong.TryParse(value, out ul))
            {
                txtCheckAmount2.TextChanged -= txtCheckAmount2_TextChanged;
                txtCheckAmount2.Text = string.Format("{0:#,#}", ul);
                txtCheckAmount2.SelectionStart = txtCheckAmount2.Text.Length;
                txtCheckAmount2.TextChanged += txtCheckAmount2_TextChanged;
            }
        }

        private void txtCheckAmount2_Leave(object sender, EventArgs e)
        {
            if (txtCheckAmount2.Text == "") txtCheckAmount2.Text = "0,000,000,000";
            toDefault();
        }

        private void txtCheckCent2_Enter(object sender, EventArgs e)
        {
            if (txtCheckCent2.Text == "00") txtCheckCent2.Text = "";
            toYellow();
        }

        private void txtCheckCent2_Leave(object sender, EventArgs e)
        {
            if (txtCheckCent2.Text == "") txtCheckCent2.Text = "00";
            toDefault();
        }
        #endregion

        #region In-Kind Donation Add and Edit
        private void btnAddIK_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                // ADD NEW DONATION
                MySqlCommand comm = new MySqlCommand("INSERT INTO donation (donationType, donorID, dateAdded)"
                    + " VALUES (1, " + donorID + ", '" + DateTime.Now.ToString("yyyy-MM-dd") + "');", conn);

                comm.ExecuteNonQuery();

                // GET THAT DONATION ID
                comm = new MySqlCommand("SELECT donationID FROM donation ORDER BY donationID DESC LIMIT 1", conn);                // Get latest donation ID (previous addition)
                addSQL(comm, 3);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnIKBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditIK_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand("UPDATE inkind SET particular = '" + txtPart2.Text
                    + "', quantity = " + txtQuantity2.Text + ", dateDonated = '" + dateIK2.Value.Date.ToString("yyyyMMdd")
                    + "' WHERE donationID = " + donationID, conn);
                editSQL(comm);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Budget Request
        private void btnAddBR_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                // ADD BUDGET REQUEST ITEM
                MySqlCommand comm = new MySqlCommand("INSERT INTO item (particular, quantity, unitPrice, amount)"
                    + " VALUES ('" + txtBRPart.Text + "', " + int.Parse(txtBRQuantity.Value.ToString()) + ", "
                    + decimal.Parse(txtBRUP.Text) + ", " + decimal.Parse(txtBRTotal.Text) + ");", conn);

                comm.ExecuteNonQuery();                
                addSQL(comm, 4);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtBRUP_Leave(object sender, EventArgs e)
        {
            txtBRTotal.Text = (decimal.Parse(txtBRQuantity.Value.ToString()) * decimal.Parse(txtBRUP.Text)).ToString("n2");
        }
        #endregion
    }
}
