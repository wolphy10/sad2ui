using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
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
        public Form refToExpense { get; set; }
        public bool hasExpense;
        public bool confirmed;
        public bool dot = true;                    // True - if user entered '.'
        public int existingExpenseID;
        public int itemID;
        public string category;        

        public moneyDonate()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(510, 376);
            conn = new MySqlConnection("server=localhost;user id=root;database=prototype_sad;password=root;persistsecurityinfo=False");
                        
            // Add
            dateCash.MaxDate = DateTime.Now; dateCash.Value = dateCash.MaxDate;
            dateCheck.MaxDate = DateTime.Now; dateCheck.Value = DateTime.Now; dateOfCheck.MaxDate = DateTime.Now.AddMonths(3); dateOfCheck.Value = DateTime.Now;
            dateIK.MaxDate = DateTime.Now; dateIK.Value = dateCash.MaxDate;
            dateFrom2.MaxDate = DateTime.Now; dateFrom2.Value = dateFrom2.MaxDate; dateTo2.MaxDate = DateTime.Now; dateTo2.Value = dateFrom2.MaxDate;   
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
        }

        public void toDefault()
        {
            // ADD
            txtCashAmount.ForeColor = txtCashCent.ForeColor = lblDot1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dfdfdf");
            lblCashAmount.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);

            txtCheckAmount.ForeColor = txtCheckCent.ForeColor = lblDot2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dfdfdf");
            lblCheckAmount.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);          

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

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.')
            {
                if (((TextBox)sender).Text.Contains(".")) e.Handled = dot = true;
                else dot = false;
            }
            else if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)) e.Handled = false;
            else if (char.IsLetter(e.KeyChar) || !char.IsDigit(e.KeyChar)) e.Handled = true;  
        }

        private void noDot_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !(e.KeyChar != '.')) e.Handled = true;
        }

        private void cbFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbFilter_DropDownClosed(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => { ((ComboBox)sender).Select(0, 0); }));
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
            if (type == 1)
            {

                decimal amount = decimal.Parse(txtCashAmount.Text + "." + txtCashCent.Text);
                comm = new MySqlCommand("INSERT INTO monetary (paymentType, ORno, amount, dateDonated, donationID)"
                + " VALUES ('Cash', '" + txtOR.Text + "', " + amount + ", '" + dateCash.Value.Date.ToString("yyyy-MM-dd") + "', " + c_donationID + ");", conn);
            }
            else if (type == 2)
            {
                decimal amount = decimal.Parse(txtCheckAmount.Text + "." + txtCheckCent.Text);
                comm = new MySqlCommand("INSERT INTO monetary (paymentType, ORno, amount, checkNO, bankName, dateCheck, dateDonated, donationID)"
                + " VALUES ('Check', '" + txtOR.Text + "', " + amount + ", '" + txtCheckNo.Text + "', '" + txtBank.Text + "', '"
                + dateOfCheck.Value.Date.ToString("yyyy-MM-dd") + "', '" + dateCheck.Value.Date.ToString("yyyy-MM-dd") + "', " + c_donationID + ");", conn);
            }
            else if (type == 3)
            {
                comm = new MySqlCommand("INSERT INTO inkind (particular, quantity, dateDonated, donationID)"
                + " VALUES ('" + txtPart.Text + "', " + txtQuantity.Text + ", '" + dateIK.Value.Date.ToString("yyyy-MM-dd") + "', " + c_donationID + ");", conn);
            }

            comm.ExecuteNonQuery();
            conn.Close();

            successMessage("Donation has been added successfully!");            
            this.Close();
        }

        public void editSQL(MySqlCommand comm)
        {
            comm.ExecuteNonQuery();
            conn.Close();
            successMessage("Donation has been edited successfully!");            
            this.Close();
        }
        #endregion

        #region Back Buttons
        private void btnCashBack_Click(object sender, EventArgs e)
        {
            tabSelection.SelectedTab = tabChoice0;
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
                        + "', amount = " + decimal.Parse(txtCashAmount2.Text + "." + txtCashCent2.Text) + ", dateDonated = '" + dateCash2.Value.Date.ToString("yyyy-MM-dd")
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
            if (txtCheckNo.MaskFull)
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
            else errorMessage("Invalid check no.");
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
            if (txtCheckNo2.MaskFull)
            {
                try
                {
                    conn.Open();
                    MySqlCommand comm = new MySqlCommand("UPDATE monetary SET ORno = '" + txtCheckOR2.Text + "', checkNo = '" + txtCheckNo2.Text
                        + "', amount = " + decimal.Parse(txtCheckAmount2.Text + "." + txtCheckCent2.Text) + ", bankName = '" + txtBank2.Text
                        + "', dateDonated = '" + dateCheck2.Value.Date.ToString("yyyy-MM-dd") + "', dateCheck = '" + dateOfCheck2.Value.Date.ToString("yyyy-MM-dd")
                        + "' WHERE donationID = " + donationID, conn);
                    editSQL(comm);
                }
                catch (Exception ex)
                {
                    errorMessage(ex.Message);
                }
            }
            else errorMessage("Invalid check no.");
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

        private void btnEncash_Click(object sender, EventArgs e)
        {
            confirmMessage("You are about to encash a check. Are you sure you want to continue?");
            if (confirmed)
            {
                try
                {
                    decimal amount = decimal.Parse(txtCheckAmount2.Text + "." + txtCheckCent2.Text);
                    conn.Open();

                    // ENCASH CHECK
                    MySqlCommand comm = new MySqlCommand("UPDATE monetary SET encash = 0 WHERE donationID = " + donationID, conn);
                    comm.ExecuteNonQuery();

                    // NEW DONATION ID
                    comm = new MySqlCommand("INSERT INTO donation (donationType, donorID, dateAdded)"
                    + " VALUES (1, " + donorID + ", '" + DateTime.Now.ToString("yyyy-MM-dd") + "')", conn);
                    comm.ExecuteNonQuery();

                    // GET THAT DONATION ID
                    comm = new MySqlCommand("SELECT donationID FROM donation ORDER BY donationID DESC LIMIT 1", conn);
                    MySqlDataAdapter adp = new MySqlDataAdapter(comm); DataTable dt = new DataTable(); adp.Fill(dt);               
                    int c_donationID = int.Parse(dt.Rows[0]["donationID"].ToString());

                    // ADD CASH FROM CHECK
                    comm = new MySqlCommand("INSERT INTO monetary (paymentType, ORNo, amount, dateDonated, encash, donationID)"
                        + " VALUES ('Cash', '" + txtCheckOR2.Text + "', " + amount + ", '" + DateTime.Now.ToString("yyyy-MM-dd") + "', 1, " + c_donationID + ")", conn);
                    comm.ExecuteNonQuery();

                    // LMAO

                    successMessage("Check has been encashed successfully!");
                    conn.Close();
                    this.Close();
                }
                catch(Exception ex)
                {
                    errorMessage(ex.Message);
                }
            }
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
                    + "', quantity = " + txtQuantity2.Text + ", dateDonated = '" + dateIK2.Value.Date.ToString("yyyy-MM-dd")
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
            if (tabSelection.SelectedIndex == 7)
            {
                if (decimal.Parse(txtBRTotal.Text).ToString() == "0.00") errorMessage("Cannot have a total of 0.");
                else if (txtBRPart.Text == "") errorMessage("Please enter the name of particular.");
                else if (decimal.Parse(txtBRTotal.Text).ToString() != "0.00" || txtBRPart.Text != "")
                {
                    try
                    {
                        conn.Open();
                        // ADD BUDGET REQUEST ITEM
                        MySqlCommand comm = new MySqlCommand("INSERT INTO item (particular, quantity, unitPrice, amount, budgetID, category)"
                            + " VALUES ('" + txtBRPart.Text + "', " + int.Parse(txtBRQuantity.Value.ToString()) + ", "
                            + decimal.Parse(txtBRUP.Text) + ", " + decimal.Parse(txtBRTotal.Text) + ", " + budgetID + ", '" + category + "');", conn);
                        comm.ExecuteNonQuery();
                        conn.Close();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        errorMessage(ex.Message);
                    }
                }
            } 
            else 
            {
                if (decimal.Parse(txtBRC_total.Text).ToString() == "0.00") errorMessage("Cannot have a total of 0.");
                else if (txtBRC_Part.Text == "") errorMessage("Please enter the name of particular.");
                else if (decimal.Parse(txtBRC_total.Text).ToString() != "0.00" && txtBRC_Part.Text != "")
                {
                    try
                    {
                        conn.Open();
                        // ADD BUDGET REQUEST ITEM WITH CATEGORY
                        MySqlCommand comm = new MySqlCommand("INSERT INTO item (particular, quantity, unitPrice, amount, budgetID, category)"
                            + " VALUES ('" + txtBRC_Part.Text + "', " + int.Parse(txtBRC_Quantity.Value.ToString()) + ", "
                            + decimal.Parse(txtBRC_UP.Text) + ", " + decimal.Parse(txtBRC_total.Text) + ", " + budgetID + ", '" + cbBRC_Cat.SelectedItem.ToString() + "');", conn);
                        comm.ExecuteNonQuery();
                        conn.Close();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        errorMessage(ex.Message);
                    }
                }
            }                            
        }

        private void btnBREdit_Click(object sender, EventArgs e)
        {
            if (decimal.Parse(txtBRTotal2.Text).ToString() != "0.00" && txtBRPart2.Text != "")
            {
                try
                {
                    conn.Open();
                    // EDIT BUDGET REQUEST ITEM
                    MySqlCommand comm = new MySqlCommand("UPDATE item SET particular = '" + txtBRPart2.Text
                        + "', quantity = " + int.Parse(txtBRQuantity2.Value.ToString()) + ", unitPrice = " + decimal.Parse(txtBRUP2.Text)
                        + ", amount = " + decimal.Parse(txtBRTotal2.Text) + " WHERE itemID = " + itemID, conn);
                    comm.ExecuteNonQuery();
                    conn.Close();
                    this.Close();
                }
                catch (Exception ex)
                {
                    errorMessage(ex.Message);
                }
            }
            else errorMessage("You cannot have a total of 0 or particular is blank.");
        }

        private void txtBRUP_Leave(object sender, EventArgs e)
        {
            if (((TextBox)sender).Name == "txtBRUP") txtBRTotal.Text = (decimal.Parse(txtBRQuantity.Value.ToString()) * decimal.Parse(txtBRUP.Text)).ToString("n2");
            else if (((TextBox)sender).Name == "txtBRUP2") txtBRTotal2.Text = (decimal.Parse(txtBRQuantity2.Value.ToString()) * decimal.Parse(txtBRUP2.Text)).ToString("n2");
            else if (((TextBox)sender).Name == "txtBRC_UP") txtBRC_total.Text = (decimal.Parse(txtBRC_Quantity.Value.ToString()) * decimal.Parse(txtBRC_UP.Text)).ToString("n2");
            else if (((TextBox)sender).Name == "txtBRC_UP2") txtBRC_total2.Text = (decimal.Parse(txtBRC_Quantity2.Value.ToString()) * decimal.Parse(txtBRC_UP2.Text)).ToString("n2");
        }

        private void txtBRUP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (((TextBox)sender).Name == "txtBRUP") txtBRTotal.Text = (decimal.Parse(txtBRQuantity.Value.ToString()) * decimal.Parse(txtBRUP.Text)).ToString("n2");
                else if (((TextBox)sender).Name == "txtBRUP2") txtBRTotal2.Text = (decimal.Parse(txtBRQuantity2.Value.ToString()) * decimal.Parse(txtBRUP2.Text)).ToString("n2");
                else if (((TextBox)sender).Name == "txtBRC_UP") txtBRC_total.Text = (decimal.Parse(txtBRC_Quantity.Value.ToString()) * decimal.Parse(txtBRC_UP.Text)).ToString("n2");
                else if (((TextBox)sender).Name == "txtBRC_UP2") txtBRC_total2.Text = (decimal.Parse(txtBRC_Quantity2.Value.ToString()) * decimal.Parse(txtBRC_UP2.Text)).ToString("n2");
            }
        }

        private void txtBRUP_TextChanged(object sender, EventArgs e)
        {
            if (dot && (((TextBox)sender).Text != "" || ((TextBox)sender).Text != "0" || ((TextBox)sender).Text != null))
            {
                if (((TextBox)sender).Name == "txtBRUP" && txtBRUP.Text != "") txtBRTotal.Text = (decimal.Parse(txtBRQuantity.Value.ToString()) * decimal.Parse(txtBRUP.Text)).ToString("n2");
                else if (((TextBox)sender).Name == "txtBRUP2" && txtBRUP2.Text != "") txtBRTotal2.Text = (decimal.Parse(txtBRQuantity2.Value.ToString()) * decimal.Parse(txtBRUP2.Text)).ToString("n2");
                else if (((TextBox)sender).Name == "txtBRC_UP" && txtBRC_UP.Text != "") txtBRC_total.Text = (decimal.Parse(txtBRC_Quantity.Value.ToString()) * decimal.Parse(txtBRC_UP.Text)).ToString("n2");
                else if (((TextBox)sender).Name == "txtBRC_UP2" && txtBRC_UP2.Text != "") txtBRC_total2.Text = (decimal.Parse(txtBRC_Quantity2.Value.ToString()) * decimal.Parse(txtBRC_UP2.Text)).ToString("n2");
            }
        }

        private void txtBRC_Quantity2_Leave(object sender, EventArgs e)
        {
            txtBRC_total2.Text = (decimal.Parse(txtBRC_Quantity2.Value.ToString()) * decimal.Parse(txtBRC_UP2.Text)).ToString("n2");
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
            btnReport.Enabled = true; dateFrom.Enabled = year.Enabled = false;

            if (rbMonth.Checked) btnReport.DialogResult = DialogResult.OK;
            else if (rbMonthSelect.Checked)
            {
                dateFrom.Enabled = true;
                btnReport.DialogResult = DialogResult.Yes;
            }
            else if (rbAnnual.Checked) btnReport.DialogResult = DialogResult.Retry;
        
            if (rbMonthSelect.Checked || rbAnnual.Checked)
            {
                year.Items.Clear();
                year.Enabled = true; int count = 0;
                for (int i = 2000; i <= DateTime.Now.Year; i++, count++)
                {
                    year.Items.Add(i);
                }
                year.SelectedIndex = count - 1;
            }
        }

        private void donateWeeks_CheckedChanged(object sender, EventArgs e)
        {
            dateTo2.MinDate = dateFrom2.Value; dateTo2.MaxDate = DateTime.Now;
        }

        private void txtAllORNO_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBRC_UP2_Leave(object sender, EventArgs e)
        {

        }

        private void btnEditBR2_Click(object sender, EventArgs e)
        {
            if (decimal.Parse(txtBRC_total2.Text).ToString() != "0.00" && txtBRC_Part2.Text != "")
            {
                try
                {conn
                    .Open();
                    // EDIT BUDGET REQUEST ITEM
                    MySqlCommand comm = new MySqlCommand("UPDATE item SET particular = '" + txtBRC_Part2.Text
                        + "', quantity = " + int.Parse(txtBRC_Quantity2.Value.ToString()) + ", unitPrice = " + decimal.Parse(txtBRC_UP2.Text)
                        + ", amount = " + decimal.Parse(txtBRC_total2.Text) + " WHERE itemID = " + itemID, conn);
                    comm.ExecuteNonQuery();
                    conn.Close();
                    this.Close();
                }
                catch (Exception ex)
                {
                    errorMessage(ex.Message);
                }
            }
            else errorMessage("You cannot have a total of 0 particular is blank.");
        }

        private void DonationMode_CheckedChanged(object sender, EventArgs e)
        {
            btnReport2.Enabled = true; dateFrom2.Enabled = dateTo2.Enabled = false;

            if (donateThisWeek.Checked) btnReport2.DialogResult = DialogResult.OK;
            else if (donateWeek.Checked)
            {
                dateFrom2.Enabled = true;
                btnReport2.DialogResult = DialogResult.Yes;
            }
            else if (donateWeeks.Checked)
            {
                dateFrom2.Enabled = dateTo2.Enabled = true;
                btnReport2.DialogResult = DialogResult.Retry;
            }
        }
        #endregion
        
        private void dateWeek_ValueChanged(object sender, EventArgs e)
        {
            dateTo2.MaxDate = DateTime.Now; dateTo2.MinDate = dateFrom2.Value;

            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(((DateTimePicker)sender).Value);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday) ((DateTimePicker)sender).Value = ((DateTimePicker)sender).Value.AddDays(3);                       
            int day2 = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(((DateTimePicker)sender).Value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            if (((DateTimePicker)sender).Name == "dateFrom2") week1.Text = day2.ToString();
            else if (((DateTimePicker)sender).Name == "dateTo2") week2.Text = day2.ToString();            
        }
    }
}
