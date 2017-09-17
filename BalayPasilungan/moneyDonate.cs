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
        public int donorID, donationID, budgetID;
        
        public Form refToDim { get; set; }
        public bool hasExpense;
        public bool dot = true;                    // True - if user entered '.'
        public int existingExpenseID;

        public moneyDonate()
        {
            InitializeComponent();
            conn = new MySqlConnection("server=localhost;user id=root;database=prototype_sad;password=root;persistsecurityinfo=False");
            
            // Add
            dateCash.MaxDate = DateTime.Now; dateCash.Value = dateCash.MaxDate;
            dateCheck.MaxDate = DateTime.Now; dateCheck.Value = DateTime.Now; dateOfCheck.MaxDate = DateTime.Now.AddMonths(3); dateOfCheck.Value = DateTime.Now;
            dateIK.MaxDate = DateTime.Now; dateIK.Value = dateCash.MaxDate;
            dateExp.MaxDate = DateTime.Now; dateExp.Value = dateCash.MaxDate;
        }
        
        private void moneyDonate_FormClosing(object sender, FormClosingEventArgs e)
        {
            refToDim.Close();
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
        #endregion

        #region Functions
        public void toGreen()
        {
            txtCashAmount.ForeColor = txtCashCent.ForeColor = lblCashAmount.ForeColor = lblDot1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            txtCheckAmount.ForeColor = txtCheckCent.ForeColor = lblCheckAmount.ForeColor = lblDot2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            txtExpAmt.ForeColor = txtExpCent.ForeColor = lblAddExp.ForeColor = lblDotExp.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
        }

        public void toDefault()
        {
            // ADD
            txtCashAmount.ForeColor = txtCashCent.ForeColor = lblDot1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dfdfdf");            
            lblCashAmount.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);

            txtCheckAmount.ForeColor = txtCheckCent.ForeColor = lblDot2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dfdfdf");            
            lblCheckAmount.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);

            txtExpAmt.ForeColor = txtExpCent.ForeColor = lblDotExp.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dfdfdf");
            lblAddExp.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);

            // EDIT
            txtCashAmount2.ForeColor = txtCashCent2.ForeColor = lblDot3.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dfdfdf");            
            lblCashAmount2.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);

            txtCheckAmount2.ForeColor = txtCheckCent2.ForeColor = lblDot4.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dfdfdf");            
            lblCheckAmount2.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);
        }

        public void toYellow()
        {
            txtCashAmount2.ForeColor = txtCashCent2.ForeColor = lblCashAmount2.ForeColor = lblDot3.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c4b617");
            txtCheckAmount2.ForeColor = txtCheckCent2.ForeColor = lblCheckAmount2.ForeColor = lblDot4.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c4b617");            
        }

        public void errorMessage(string message)            // Error Message
        {
            error err = new error();
            err.lblError.Text = message;
            err.ShowDialog();
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!(e.KeyChar != '.')) if (((TextBox)sender).Text.Contains(".")) e.Handled = true;                   
        }

        private void noDot_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !(e.KeyChar != '.')) e.Handled = true;
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
            {

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

        private void btn_Close(object sender, EventArgs e)
        {

            this.Close();
        }
        #endregion

        #region Cash
        private void txtCashAmount_Leave(object sender, EventArgs e)
        {
            if (txtCashAmount.Text == "") txtCashAmount.Text = "0,000,000,000";
            toDefault();
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
                errorMessage(ex.Message);
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
                errorMessage(ex.Message);
            }
        }

        private void txtCashAmount2_Leave(object sender, EventArgs e)
        {
            if (txtCashAmount2.Text == "") txtCashAmount2.Text = "0,000,000,000";
            toDefault();
        }

        private void txtCashCent2_Leave(object sender, EventArgs e)
        {
            if (txtCashCent2.Text == "") txtCashCent2.Text = "00";
            toDefault();
        }
        #endregion
        
        #region Check
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
                errorMessage(ex.Message);
            }
        }

        private void txtCheckAmount_Leave(object sender, EventArgs e)
        {
            if (txtCheckAmount.Text == "") txtCheckAmount.Text = "0,000,000,000";
            toDefault();
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
                errorMessage(ex.Message);
            }
        }

        private void txtCheckAmount2_Leave(object sender, EventArgs e)
        {
            if (txtCheckAmount2.Text == "") txtCheckAmount2.Text = "0,000,000,000";
            toDefault();
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
                errorMessage(ex.Message);
            }
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
                errorMessage(ex.Message);
            }
        }
        #endregion

        #region Budget Request
        private void btnAddBR_Click(object sender, EventArgs e)
        {
            if (decimal.Parse(txtBRTotal.Text).ToString() != "0.00")
            {
                try
                {
                    conn.Open();
                    // ADD BUDGET REQUEST ITEM
                    MySqlCommand comm = new MySqlCommand("INSERT INTO item (particular, quantity, unitPrice, amount, budgetID)"
                        + " VALUES ('" + txtBRPart.Text + "', " + int.Parse(txtBRQuantity.Value.ToString()) + ", "
                        + decimal.Parse(txtBRUP.Text) + ", " + decimal.Parse(txtBRTotal.Text) + ", " + budgetID + ");", conn);
                    comm.ExecuteNonQuery();
                    conn.Close();
                    this.Close();
                }
                catch (Exception ex)
                {
                    errorMessage(ex.Message);
                }
            }
            else errorMessage("You cannot have a total of 0.");
        }
        
        private void btnBREdit_Click(object sender, EventArgs e)
        {
            if (decimal.Parse(txtBRTotal2.Text).ToString() != "0.00")
            {
                try
                {
                    conn.Open();
                    // EDIT BUDGET REQUEST ITEM
                    MySqlCommand comm = new MySqlCommand("UPDATE item SET particular = '" + txtBRPart2.Text 
                        + "', quantity = " + int.Parse(txtBRQuantity2.Value.ToString()) + ", unitPrice = " + decimal.Parse(txtBRUP.Text)
                        + ", amount = " + decimal.Parse(txtBRTotal.Text) + ", budgetID = " + budgetID + ");", conn);
                    comm.ExecuteNonQuery();
                    conn.Close(); this.Close();
                }
                catch (Exception ex)
                {
                    errorMessage(ex.Message);
                }
            }
            else errorMessage("You cannot have a total of 0.");
        }

        private void txtBRUP_Leave(object sender, EventArgs e)
        {
            if (((TextBox)sender).Name == "txtBRUP") txtBRTotal.Text = (decimal.Parse(txtBRQuantity.Value.ToString()) * decimal.Parse(txtBRUP.Text)).ToString("n2");
            else if (((TextBox)sender).Name == "txtBRUP2") txtBRTotal2.Text = (decimal.Parse(txtBRQuantity2.Value.ToString()) * decimal.Parse(txtBRUP2.Text)).ToString("n2");
        }

        private void txtBRUP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (((TextBox)sender).Name == "txtBRUP") txtBRTotal.Text = (decimal.Parse(txtBRQuantity.Value.ToString()) * decimal.Parse(txtBRUP.Text)).ToString("n2");
                else if (((TextBox)sender).Name == "txtBRUP2") txtBRTotal2.Text = (decimal.Parse(txtBRQuantity2.Value.ToString()) * decimal.Parse(txtBRUP2.Text)).ToString("n2");
            }
        }

        private void txtBRUP_TextChanged(object sender, EventArgs e)
        {
            if (!dot)
            {
                if (((TextBox)sender).Name == "txtBRUP") txtBRTotal.Text = (decimal.Parse(txtBRQuantity.Value.ToString()) * decimal.Parse(txtBRUP.Text)).ToString("n2");
                else if (((TextBox)sender).Name == "txtBRUP2") txtBRTotal2.Text = (decimal.Parse(txtBRQuantity2.Value.ToString()) * decimal.Parse(txtBRUP2.Text)).ToString("n2");
            }                
        }
        #endregion

        #region Expense   
        private void cbExpCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                MySqlDataAdapter adp = new MySqlDataAdapter("", conn);
                if (thisMonth.Checked) adp = new MySqlDataAdapter("SELECT COUNT(expenseID), SUM(amount), expenseID FROM expense WHERE MONTH(dateExpense) = '" + DateTime.Now.Month + "' AND category = '" + cbExpCat.SelectedItem.ToString() + "'", conn);
                else adp = new MySqlDataAdapter("SELECT COUNT(expenseID), SUM(amount), expenseID FROM expense WHERE MONTH(dateExpense) = '" + dateExp.Value.Month + "' AND category = '" + cbExpCat.SelectedItem.ToString() + "'", conn);
                DataTable dt = new DataTable(); adp.Fill(dt);

                if (decimal.Parse(dt.Rows[0]["count(expenseID)"].ToString()) == 0) txtExpCurrent.Text = "0.00";
                else
                {
                    txtExpCurrent.Text = dt.Rows[0]["SUM(amount)"].ToString();
                    existingExpenseID = int.Parse(dt.Rows[0]["expenseID"].ToString());
                }

                if (cbExpCat.SelectedItem.ToString() == "") btnAddExp.Enabled = false;
                else btnAddExp.Enabled = true;
                conn.Close();
            }
            catch (Exception ex)
            {
                errorMessage(ex.Message);
            }
        }

        private void dateExp_Leave(object sender, EventArgs e)
        {
            MySqlDataAdapter adp = new MySqlDataAdapter("SELECT expenseID, amount FROM expense WHERE MONTH(dateExpense) = " + int.Parse(dateExp.Value.Month.ToString()) + " AND category = '" + cbExpCat.SelectedItem.ToString() + "'", conn);
            DataTable dt = new DataTable(); adp.Fill(dt);
            if (dt.Rows.Count == 0) txtExpCurrent.Text = "0.00";
            else
            {
                txtExpCurrent.Text = dt.Rows[0]["amount"].ToString();
                existingExpenseID = int.Parse(dt.Rows[0]["expenseID"].ToString());
            }
        }

        private void txtExpAmt_Leave(object sender, EventArgs e)
        {
            decimal amount = decimal.Parse(txtExpAmt.Text + "." + txtExpCent.Text);
            if (txtExpAmt.Text != "0,000,000,000" || amount != 0)
            {
                txtExpTotal.Text = (amount + decimal.Parse(txtExpCurrent.Text)).ToString();
                txtExpTotal.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            }
        }
        
        private void btnAddExp_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand("", conn);
                if (thisMonth.Checked)
                    {
                        comm = new MySqlCommand("INSERT INTO expense (dateExpense, category, amount)"
                            + " VALUES ('" + DateTime.Now.ToString("yyyy-MM-dd") + "', '" + cbExpCat.SelectedItem.ToString() + "', " + decimal.Parse(txtExpTotal.Text) + ");", conn);
                    }
                else
                    {
                        comm = new MySqlCommand("INSERT INTO expense (dateExpense, category, amount)"
                            + " VALUES ('" + dateExp.Value.ToString("yyyy-MM-dd") + "', '" + cbExpCat.SelectedItem.ToString() + "', " + decimal.Parse(txtExpTotal.Text) + ");", conn);
                    }
                comm.ExecuteNonQuery();
                conn.Close(); this.Close();
            }
            catch(Exception ex)
            {
                errorMessage(ex.Message);
            }
        }

        private void thisMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (thisMonth.Checked) dateExp.Enabled = false;
            else dateExp.Enabled = true;
        }

        private void tabExp_Click(object sender, EventArgs e)
        {
            tabExp.Focus();
            decimal amount = decimal.Parse(txtExpAmt.Text + "." + txtExpCent.Text);
            if (txtExpAmt.Text != "0,000,000,000" || amount != 0)
            {
                txtExpTotal.Text = (amount + decimal.Parse(txtExpCurrent.Text)).ToString();
                txtExpTotal.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            }
        }
        
        private void total_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                decimal amount = decimal.Parse(txtExpAmt.Text + "." + txtExpCent.Text);
                if (((TextBox)sender).Text == "0,000,000,000" || ((TextBox)sender).Text == "00" || amount != 0)
                {
                    txtExpTotal.Text = (amount + decimal.Parse(txtExpCurrent.Text)).ToString();
                    txtExpTotal.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
                }
            }
        }
        #endregion

        #region Textboxes
        private void txt_Enter(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == "0,000,000,000") ((TextBox)sender).Text = "";
            if (((TextBox)sender).Name != "txtExpAmt") toGreen();
        }

        private void txtEdit_Enter(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == "0,000,000,000") ((TextBox)sender).Text = "";
            toYellow();
        }

        private void txtCent_Enter(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == "00") ((TextBox)sender).Text = "";
            toGreen();
        }

        private void txtCentEdit_Enter(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == "00") ((TextBox)sender).Text = "";
            toYellow();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {

        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            string value = ((TextBox)sender).Text.Replace(",", "");
            ulong ul;
            if (ulong.TryParse(value, out ul))
            {
                ((TextBox)sender).TextChanged -= txtAmount_TextChanged;
                ((TextBox)sender).Text = string.Format("{0:#,#}", ul);
                ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
                ((TextBox)sender).TextChanged += txtAmount_TextChanged;
            }
        }
        #endregion

        #region Expense Options
        private void ExpMode_CheckedChanged(object sender, EventArgs e)
        {
            btnReport.Enabled = true;
            if (((RadioButton)sender).Name == "rbMonth" || ((RadioButton)sender).Name == "rbAnnual") dateFrom.Enabled = dateTo.Enabled = false;
            else if (((RadioButton)sender).Name == "rbMonths") dateFrom.Enabled = dateTo.Enabled = true;
        }
        #endregion
    }
}
