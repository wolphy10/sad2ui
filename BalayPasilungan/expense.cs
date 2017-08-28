﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using MySql.Data.MySqlClient;

namespace BalayPasilungan
{
    public partial class expense : Form
    {
        public MySqlConnection conn;
        public int current_donorID;
        public DateTime fromDateValue;
        public bool searchDateBool;

        public expense()
        {
            InitializeComponent();
            conn = new MySqlConnection("server=localhost;user id=root;database=prototype_sad;password=root;persistsecurityinfo=False");

            // Renderers (to remove default blue hightlights or mouseovers)
            donorMenuStrip.Renderer = new renderer(); donorMenuStrip2.Renderer = new renderer();
            donorInfo.Renderer = new renderer2(); donationInfo.Renderer = new renderer2(); brInfo.Renderer = new renderer2();

            // Setting Dates
            dateBR.MaxDate = DateTime.Today; datePledge.MaxDate = DateTime.Today;
            datePledge.Value = DateTime.Today;
        }

        #region Movable Form
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void upPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void taskbar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        #endregion

        #region Functions
        public class renderer : ToolStripProfessionalRenderer
        {
            public renderer() : base(new cols()) { }
        }

        public class cols : ProfessionalColorTable
        {
            public override Color MenuItemSelected
            {
                get { return Color.Black; }
            }
            public override Color MenuItemSelectedGradientBegin
            {
                get { return Color.Black; }
            }
            public override Color MenuItemSelectedGradientEnd
            {
                get { return Color.Black; }
            }
            public override Color MenuItemBorder
            {
                get { return Color.Black; }
            }
        }

        public void resetNewDonor()                         // Clear new donor textboxes and set to default values
        {
            tabNewDonorInput.SelectedIndex = 0;
            txtDName.Text = "Juan Miguel";
            cbDType.SelectedIndex = 0; cbPledge.SelectedIndex = 0;
            txtPhone.Text = "29xxxxx"; txtMobile1.Text = "09xx"; txtMobile2.Text = "xxx"; txtMobile3.Text = "xxxx";
            txtEmail.Text = "jmiguel@example.com";
            datePledge.Value = DateTime.Today;
        }

        public void resetMainButtons()                      // Reset main buttons to default colors and image (white)
        {
            btnDonation.BackColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            btnFinance.BackColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            btnDonation.BackgroundImage = global::BalayPasilungan.Properties.Resources.donation_white;
            btnFinance.BackgroundImage = global::BalayPasilungan.Properties.Resources.finance_white;
        }

        public void resetDonorShowTS()                      // Reset buttons (acting like toolstrips)
        {
            moneyTS.BackColor = System.Drawing.Color.FromArgb(248, 248, 248);
            ikTS.BackColor = System.Drawing.Color.FromArgb(248, 248, 248);
            donorOTS.BackColor = System.Drawing.Color.FromArgb(248, 248, 248);

            moneyTS.ForeColor = System.Drawing.Color.FromArgb(201, 201, 201);
            ikTS.ForeColor = System.Drawing.Color.FromArgb(201, 201, 201);
            donorOTS.ForeColor = System.Drawing.Color.FromArgb(201, 201, 201);
        }
        #endregion

        #region Main Buttons (Taskbar and Close)
        private void btnMain_Click(object sender, EventArgs e)
        {
            tabSelection.SelectedTab = tabDonorInfo;
        }

        private void btnFinance_Click(object sender, EventArgs e)
        {
            resetMainButtons();
            btnFinance.BackColor = Color.White;
            btnFinance.BackgroundImage = global::BalayPasilungan.Properties.Resources.finance_green;
            tabSelection.SelectedTab = tabBudgetRequest;
        }

        private void btnDonation_Click(object sender, EventArgs e)
        {
            resetMainButtons();
            tabInnerDonors.SelectedIndex = 0;
            btnDonation.BackColor = Color.White;
            btnDonation.BackgroundImage = global::BalayPasilungan.Properties.Resources.donation_green;
            tabSelection.SelectedTab = tabDonors;
            loadDonorList();
            current_donorID = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region SQL Connections
        public void loadDonorList()
        {
            tabSelection.SelectedTab = tabDonors;

            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT donorID, donorName, pledge, datePledge FROM donor", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    donorsGV.DataSource = dt;

                    donorsGV.AutoResizeColumns();

                    // Donors Grid View UI Modifications
                    donorsGV.Columns[1].HeaderText = "Donor Name"; donorsGV.Columns[2].HeaderText = "Pledge"; donorsGV.Columns[3].HeaderText = "Date of Pledge";

                    donorsGV.Columns[0].Visible = false;

                    donorsGV.Columns[1].HeaderCell.Style.Padding = new Padding(10, 0, 0, 0);
                    donorsGV.Columns[1].DefaultCellStyle.Padding = new Padding(15, 0, 0, 0);

                    donorsGV.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    donorsGV.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    donorsGV.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                else
                {
                    // No donors add here
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
            }
        }

        public void loadDonorInfo(int id)
        {
            tabSelection.SelectedTab = tabDonorInfo;

            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT donorID, type, donorName, telephone, mobile, email, pledge, datePledge FROM donor WHERE donorID = " + id, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    current_donorID = int.Parse(dt.Rows[0]["donorID"].ToString());
                    lblDonorName.Text = dt.Rows[0]["donorName"].ToString();

                    if (dt.Rows[0]["type"].ToString() == "1") txtDType.Text = "Individual";
                    else txtDType.Text = "Organization";

                    txtDPhone.Text = dt.Rows[0]["telephone"].ToString();
                    txtDMobile.Text = dt.Rows[0]["mobile"].ToString();
                    txtDEmail.Text = dt.Rows[0]["email"].ToString();
                    txtDPledge.Text = dt.Rows[0]["pledge"].ToString();
                    txtDDatePledge.Text = dt.Rows[0]["datePledge"].ToString();
                }
                else
                {
                    btnDelMoneyD.Enabled = false; btnEditMoneyD.Enabled = false;
                    dt.Rows.Add(0, "No donors.");
                    donationMoney.DataSource = dt;
                }

                conn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                conn.Close();
            }
        }

        public void loadMonetary(int id)
        {
            try
            {
                conn.Open();

                // GET MONETARY INFO
                MySqlCommand comm = new MySqlCommand("SELECT monetaryID, amount, ORno, TIN, dateDonated FROM monetary WHERE donationID in (SELECT donation.donationID FROM donation INNER JOIN donor ON donation.donorID = donor.donorID WHERE donor.donorID = " + id + ")", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    donationMoney.DataSource = dt;

                    // Donors Grid View UI Modifications
                    donationMoney.Columns[1].HeaderText = "AMOUNT"; donationMoney.Columns[2].HeaderText = "OR NO"; donationMoney.Columns[3].HeaderText = "TIN"; donationMoney.Columns[4].HeaderText = "DATE DONATED";

                    donationMoney.Columns[0].Visible = false;

                    donationMoney.Columns[1].HeaderCell.Style.Padding = new Padding(10, 0, 0, 0);
                    donationMoney.Columns[1].DefaultCellStyle.Format = "#,0.00##";
                    donationMoney.Columns[1].DefaultCellStyle.Padding = new Padding(15, 0, 0, 0);
                    donationMoney.Columns[4].DefaultCellStyle.Format = "MMMM dd, yyyy";
                    donationMoney.Columns[4].HeaderCell.Style.Padding = new Padding(10, 0, 0, 0);
                    donationMoney.Columns[4].DefaultCellStyle.Padding = new Padding(15, 0, 0, 0);

                    donationMoney.Columns[1].Width = 350;
                    donationMoney.Columns[2].Width = 126;
                    donationMoney.Columns[3].Width = 200;
                    donationMoney.Columns[4].Width = 250;
                    btnDelMoneyD.Enabled = true; btnEditMoneyD.Enabled = true;
                }
                else
                {
                    btnDelMoneyD.Enabled = false; btnEditMoneyD.Enabled = false;
                    dt.Rows.Add(0, null, "No donations.");
                    donationMoney.DataSource = dt;
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
            }
        }

        public void searchMonetary(int id, string search)
        {
            try
            {
                conn.Open();

                decimal n; string query = "";
                bool isNumeric = decimal.TryParse(search, out n);                

                if (isNumeric)
                {
                    n = decimal.Parse(search);                                          // Convert search keyword to double
                    string doubs = n.ToString("F2", CultureInfo.InvariantCulture);      // Convert to string with 2 decimal places

                    query = "SELECT monetaryID, amount, ORno, TIN, dateDonated FROM monetary WHERE donationID in (SELECT donation.donationID FROM donation INNER JOIN donor ON donation.donorID = donor.donorID WHERE donor.donorID = " + id + ")"
                    + " AND (amount LIKE " + decimal.Parse(doubs) + " OR ORno LIKE '%" + search + "%' OR TIN LIKE '%" + search + "%')";                    
                }
                else if (searchDateBool)
                {
                    searchDateBool = false;
                    query = "SELECT monetaryID, amount, ORno, TIN, dateDonated FROM monetary WHERE donationID in (SELECT donation.donationID FROM donation INNER JOIN donor ON donation.donorID = donor.donorID WHERE donor.donorID = " + id + ")"
                    + " AND (dateDonated LIKE '" + fromDateValue + "' OR MONTH(dateDonated) = " + fromDateValue.ToString("MM") + " OR YEAR(dateDonated) = " + fromDateValue.ToString("yyyy") + ")";                 
                }

                else
                {
                    query = "SELECT monetaryID, amount, ORno, TIN, dateDonated FROM monetary WHERE donationID in (SELECT donation.donationID FROM donation INNER JOIN donor ON donation.donorID = donor.donorID WHERE donor.donorID = " + id + ")"
                    + " AND (ORno LIKE '%" + search + "%' OR TIN LIKE '%" + search + "%')";                    
                }
                
                MySqlCommand comm = new MySqlCommand(query, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                // Donors Grid View UI Modifications
                donationMoney.Columns[1].HeaderText = "AMOUNT"; donationMoney.Columns[2].HeaderText = "OR NO"; donationMoney.Columns[3].HeaderText = "TIN"; donationMoney.Columns[4].HeaderText = "DATE DONATED";

                donationMoney.Columns[0].Visible = false;

                donationMoney.Columns[1].HeaderCell.Style.Padding = new Padding(10, 0, 0, 0);
                donationMoney.Columns[1].DefaultCellStyle.Padding = new Padding(15, 0, 0, 0);
                donationMoney.Columns[4].DefaultCellStyle.Format = "MMMM dd, yyyy";
                donationMoney.Columns[4].HeaderCell.Style.Padding = new Padding(10, 0, 0, 0);
                donationMoney.Columns[4].DefaultCellStyle.Padding = new Padding(15, 0, 0, 0);

                donationMoney.Columns[4].ToString();

                donationMoney.Columns[1].Width = 350;
                donationMoney.Columns[2].Width = 126;
                donationMoney.Columns[3].Width = 200;
                donationMoney.Columns[4].Width = 250;                

                if (dt.Rows.Count > 0) donationMoney.DataSource = dt;                
                else
                {
                    dt.Rows.Add(0, null, "No entries found.");
                    donationMoney.DataSource = dt;
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
            }
        }

        public void delDonation(int donationid)
        {
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("DELETE FROM monetary WHERE donationID = " + donationid, conn);            // Delete from monetary
                comm.ExecuteNonQuery();

                comm = new MySqlCommand("DELETE FROM donation WHERE donationID = " + donationid, conn);                         // Delete from donation
                comm.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
            }
        }
        #endregion

        #region Renderers
        private class renderer2 : ToolStripProfessionalRenderer
        {
            public renderer2() : base(new cols2()) { }
        }

        private class cols2 : ProfessionalColorTable
        {
            Color normal = System.Drawing.ColorTranslator.FromHtml("#f8f8f8");
            public override Color MenuItemSelected
            {
                get { return normal; }
            }
            public override Color MenuItemSelectedGradientBegin
            {
                get { return normal; }
            }
            public override Color MenuItemSelectedGradientEnd
            {
                get { return normal; }
            }
            public override Color MenuItemBorder
            {
                get { return normal; }
            }
        }
        #endregion

        #region Donors
        private void btnAddDonor_Click(object sender, EventArgs e)
        {
            resetNewDonor();
            tabSelection.SelectedTab = tabNewDonor;
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Find a donor") txtSearch.Text = "";
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text == "") txtSearch.Text = "Find a donor";
        }

        private void donorTS2_Click(object sender, EventArgs e)
        {
            tabInnerDonors.SelectedIndex = 0;
            loadDonorList();
        }

        private void donationsTS_Click(object sender, EventArgs e)
        {
            tabSelection.SelectedTab = tabDonations;
        }
        #endregion

        #region Donations
        private void txtSearch2_Enter(object sender, EventArgs e)
        {
            if (txtSearch2.Text == "Find a donation") txtSearch2.Text = "";
        }

        private void txtSearch2_Leave(object sender, EventArgs e)
        {
            if (txtSearch2.Text == "") txtSearch2.Text = "Find a donation";
        }
        #endregion

        #region New Donor Form
        private void btnDonorConfirm_Click(object sender, EventArgs e)
        {
            tabNewDonorInput.SelectedTab = tabDonorConfirm;
            donorCTS.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141); donorTS.ForeColor = System.Drawing.Color.FromArgb(197, 217, 208);

            //Transferring confirm
            if (txtPhone.Text == "29xxxxx") conf_phone.Text = "N/A";
            else conf_phone.Text = txtPhone.Text;
            if (txtMobile1.Text == "09xx") conf_mobile.Text = "N/A";
            else conf_mobile.Text = txtMobile1.Text + "-" + txtMobile2.Text + "-" + txtMobile3.Text;
            if (txtEmail.Text == "jmiguel@example.com") conf_email.Text = "N/A";
            else conf_email.Text = txtEmail.Text;

            conf_donorName.Text = txtDName.Text;
            conf_donorType.Text = cbDType.Text;


            conf_pledge.Text = cbPledge.Text;
            conf_datePledge.Text = datePledge.Text;
        }

        private void btnDonorBack_Click(object sender, EventArgs e)
        {
            tabNewDonorInput.SelectedTab = tabNewInfo;
            donorTS.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141); donorCTS.ForeColor = System.Drawing.Color.FromArgb(197, 217, 208);
        }

        private void btnDonorFinal_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                String mobile = "N/A";
                String datePledgeSTR = datePledge.Value.Year.ToString() + "-" + datePledge.Value.Month.ToString() + "-" + datePledge.Value.Day.ToString();
                if (conf_mobile.Text != "N/A") mobile = txtMobile1.Text + txtMobile2.Text + txtMobile3.Text;

                // ADD SQL COMMAND
                MySqlCommand comm = new MySqlCommand("INSERT INTO donor (type, donorName, telephone, mobile, email, pledge, datePledge)"
                    + " VALUES (1, '" + conf_donorName.Text + "', '" + conf_phone.Text + "', '" + mobile + "', '" + conf_email.Text + "', '" + conf_pledge.Text + "', '" + datePledgeSTR + "');", conn);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            loadDonorList();
        }

        private void btnDonorCancel_Click(object sender, EventArgs e)
        {
            tabSelection.SelectedTab = tabDonors;
        }

        #region New Donor Textboxes
        private void txtDName_Enter(object sender, EventArgs e)
        {
            if (txtDName.Text == "Juan Miguel") txtDName.Text = "";
            txtDName.ForeColor = Color.Black;
            lblDName.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            panelDName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
            countDName.Visible = true;
        }

        private void txtDName_TextChanged(object sender, EventArgs e)
        {
            countDName.Text = txtDName.TextLength + "/250";
        }

        private void txtDName_Leave(object sender, EventArgs e)
        {
            if (txtDName.Text == "") txtDName.Text = "Juan Miguel";
            txtDName.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);
            lblDName.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
            panelDName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
            countDName.Visible = false;
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) e.Handled = true;
        }

        private void txtPhone_Enter(object sender, EventArgs e)
        {
            if (txtPhone.Text == "29xxxxx") txtPhone.Text = "";
            txtPhone.ForeColor = Color.Black;
            lblPhone.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
            panelPhone.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
            countPhone.Visible = true;
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            countPhone.Text = txtPhone.TextLength + "/7";
        }

        private void txtPhone_Leave(object sender, EventArgs e)
        {
            if (txtPhone.Text == "") txtPhone.Text = "29xxxxx";
            txtPhone.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);
            lblPhone.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
            panelPhone.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
            countPhone.Visible = false;
        }

        private void txtMobile1_Enter(object sender, EventArgs e)
        {
            if (txtMobile1.Text == "09xx") txtMobile1.Text = "";
            txtMobile1.ForeColor = Color.Black;
            lblMobile.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            panelMobile.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
        }

        private void txtMobile1_TextChanged(object sender, EventArgs e)
        {
            if (txtMobile1.TextLength == 4) txtMobile2.Focus();
        }

        private void txtMobile1_Leave(object sender, EventArgs e)
        {
            if (txtMobile1.Text == "") txtMobile1.Text = "09xx";
            else if (txtMobile1.TextLength != 4 && txtMobile1.Text != "")
            {
                //yea
            }
        }

        private void txtMobile1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) e.Handled = true;
        }

        private void txtMobile2_Enter(object sender, EventArgs e)
        {
            if (txtMobile2.Text == "xxx") txtMobile2.Text = "";
            txtMobile2.ForeColor = Color.Black;
            lblMobile.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            panelMobile.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
        }

        private void txtMobile2_TextChanged(object sender, EventArgs e)
        {
            if (txtMobile2.TextLength == 3) txtMobile3.Focus();
        }

        private void txtMobile2_Leave(object sender, EventArgs e)
        {
            if (txtMobile2.Text == "") txtMobile2.Text = "xxx";
            else if (txtMobile2.TextLength != 3 && txtMobile2.Text != "")
            {
                //error
            }
        }

        private void txtMobile2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) e.Handled = true;
        }

        private void txtMobile3_Enter(object sender, EventArgs e)
        {
            if (txtMobile3.Text == "xxxx") txtMobile3.Text = "";
            txtMobile3.ForeColor = Color.Black;
            lblMobile.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            panelMobile.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
        }

        private void txtMobile3_TextChanged(object sender, EventArgs e)
        {
            if (txtMobile3.TextLength == 4)
            {
                //Leave textbox               
            }
        }

        private void txtMobile3_Leave(object sender, EventArgs e)
        {
            Color back = System.Drawing.Color.FromArgb(135, 135, 135);
            if (txtMobile3.Text == "") txtMobile3.Text = "xxxx";
            else if (txtMobile3.TextLength != 4 && txtMobile3.Text != "")
            {
                //error
            }
            txtMobile1.ForeColor = back; txtMobile2.ForeColor = back; txtMobile3.ForeColor = back;
            lblMobile.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
            panelMobile.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
        }

        private void txtMobile3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) e.Handled = true;
        }

        private void txtEmail_Enter(object sender, EventArgs e)
        {
            if (txtEmail.Text == "jmiguel@example.com") txtEmail.Text = "";
            txtEmail.ForeColor = Color.Black;
            lblEmail.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            panelEmail.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
            countEmail.Visible = true;
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            countEmail.Text = txtEmail.TextLength + "/100";
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (txtEmail.Text == "") txtEmail.Text = "jmiguel@example.com";
            txtEmail.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);
            lblEmail.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
            panelEmail.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
            countEmail.Visible = false;
        }

        private void tabNewInfo_Click(object sender, EventArgs e)
        {
            tabNewInfo.Focus();
        }
        #endregion

        #endregion

        #region New Donation Form
        private void btnDonationCancel_Click(object sender, EventArgs e)
        {
            tabNewDonorInput.SelectedTab = tabNewInfo;
            donationTS.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141); donationCTS.ForeColor = System.Drawing.Color.FromArgb(197, 217, 208);
        }
        #endregion

        #region New Budget Request
        private void cbOthers_CheckedChanged(object sender, EventArgs e)
        {
            if (cbOthers.Checked) panelOthers.Visible = true;
            else panelOthers.Visible = false;
        }

        private void clbCategory_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbCategory.Items.Count; i++)
            {
                if (clbCategory.GetItemRectangle(i).Contains(clbCategory.PointToClient(MousePosition)))
                {
                    switch (clbCategory.GetItemCheckState(i))
                    {
                        case CheckState.Checked:
                            clbCategory.SetItemCheckState(i, CheckState.Unchecked);
                            break;
                        case CheckState.Indeterminate:
                        case CheckState.Unchecked:
                            clbCategory.SetItemCheckState(i, CheckState.Checked);
                            break;
                    }

                }
            }
        }

        private void btnBRCancel_Click(object sender, EventArgs e)
        {
            tabSelection.SelectedTab = tabFinance;
        }
        #endregion

        #region Display Donor
        private void tabDonorInfo_Click(object sender, EventArgs e)                                     // To remove mouse cursor over selected donor profile textboxes
        {
            tabDonorInfo.Focus();
        }

        private void donorsGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                current_donorID = int.Parse(donorsGV.Rows[e.RowIndex].Cells[0].FormattedValue.ToString());
                loadMonetary(current_donorID);
                loadDonorInfo(current_donorID);
            }
        }

        private void moneyTS_Click(object sender, EventArgs e)
        {
            resetDonorShowTS();
            tabDonorDetails.SelectedIndex = 0;
            moneyTS.BackColor = Color.White;
            moneyTS.ForeColor = Color.Black;
        }

        private void ikTS_Click(object sender, EventArgs e)
        {
            resetDonorShowTS();
            tabDonorDetails.SelectedIndex = 1;
            ikTS.BackColor = Color.White;
            ikTS.ForeColor = Color.Black;
        }

        private void donorOTS_Click(object sender, EventArgs e)
        {
            resetDonorShowTS();
            tabDonorDetails.SelectedIndex = 2;
            donorOTS.BackColor = Color.White;
            donorOTS.ForeColor = Color.Black;
        }
        #endregion

        #region Monetary Donation
        private void btnAddMoneyD_Click(object sender, EventArgs e)                 // Add New Donation
        {
            moneyDonate mD = new moneyDonate();
            mD.donationID = current_donorID;
            mD.refToExpense = this;
            mD.hasExpense = true;
            this.Hide();
            mD.ShowDialog();
            loadMonetary(current_donorID);
        }

        private void txtSearchMoney_Enter(object sender, EventArgs e)
        {
            if (txtSearchMoney.Text == "search here") txtSearchMoney.Text = "";
            if (!searchDate.Checked) searchOthers.Checked = true;
        }

        private void txtSearchMoney_Leave(object sender, EventArgs e)
        {
            if (txtSearchMoney.Text == "") txtSearchMoney.Text = "search here";
            searchOthers.Checked = false; searchDate.Checked = false;                   // Uncheck search options
        }

        private void txtSearchMoney_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (searchDate.Checked)
                {
                    var formats = new[] { "dd/MM/yyyy", "MM/dd/yyyy", "yyyy-MM-dd", "yyyy", "MMM dd", "MM/dd/yyyy", "MM/dd", "MMMM dd", "MMM dd yyyy", "MMMM dd, yyyy", "MMM", "MMMM" };
                    var m = new[] { "MMM", "MMMM" }; var y = new[] { "yyyy"};

                    if (DateTime.TryParseExact(txtSearchMoney.Text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue))
                    {                        
                        fromDateValue = DateTime.Parse(fromDateValue.ToString("yyyy-MM-dd"));                        
                        searchDateBool = true;
                        searchMonetary(current_donorID, null);
                    }
                    else searchMonetary(current_donorID, "error");
                }
                else searchMonetary(current_donorID, txtSearchMoney.Text);
            }
        }

        private void btnEditMoneyD_Click(object sender, EventArgs e)
        {
            int row = donationMoney.CurrentCell.RowIndex;

            if (donationMoney.Rows[row].Cells[4].Value.ToString() != null || donationMoney.Rows[row].Cells[1].Value.ToString() != null)
            {
                string[] parts = donationMoney.Rows[row].Cells[1].Value.ToString().Split('.');
                DateTime dateDonate = Convert.ToDateTime(donationMoney.Rows[row].Cells[4].Value.ToString());

                moneyDonate mD = new moneyDonate();

                mD.donationID = int.Parse(donationMoney.Rows[row].Cells[0].Value.ToString());
                mD.tabSelection.SelectedIndex = 3;
                mD.txtCashAmount2.Text = parts[0];
                mD.txtCashCent2.Text = parts[1];
                mD.txtOR2.Text = donationMoney.Rows[row].Cells[2].Value.ToString();
                mD.txtTIN2.Text = donationMoney.Rows[row].Cells[3].Value.ToString();
                mD.dateCash2.MaxDate = dateDonate; mD.dateCash2.Value = dateDonate;
                mD.ShowDialog();

                loadMonetary(current_donorID);
            }
        }

        private void btnDelMoneyD_Click(object sender, EventArgs e)
        {
            int row = donationMoney.CurrentCell.RowIndex;

            confirm conf = new confirm();
            conf.lblConfirm.Text = "Are you sure you want to delete this?";

            if (conf.ShowDialog() == DialogResult.OK)
            {
                delDonation(int.Parse(donationMoney.Rows[row].Cells[0].Value.ToString()));
                loadMonetary(current_donorID);
            }
        }
        #endregion
        
        private void btnSetDonor_Click(object sender, EventArgs e)
        {
            tabInnerDonors.SelectedIndex = 1;
        }

        private void btnBackDonorList_Click(object sender, EventArgs e)
        {
            tabInnerDonors.SelectedIndex = 0;
            loadDonorList();
        }
    }
}