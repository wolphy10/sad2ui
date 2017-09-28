using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using MySql.Data.MySqlClient;
using Excel = Microsoft.Office.Interop.Excel;

using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace BalayPasilungan
{
    public partial class expense : Form
    {
        public MySqlConnection conn;
        public MySqlCommand comm;
        public int current_donorID, current_budgetID, current_item;
        public DateTime fromDateValue;
        public bool editDonor;                  // True - Donor for edit
        public bool confirmed;                  // True - clicked OK in confirm window
        public bool empty;                      // True - Table is empty
        public bool aBR;                        // True - Table is for approved budget list
        public bool allMoneyDonation = false;   // True - Table is for all monetary donation
        public bool backMoneyDonation = false;  // True - Go back to all monetary donation
        public bool brEdit = false;             // True - Budget request for edit colors
        public static int expMonth;
        public main reftomain { get; set; }
        // For dates
        public bool searchDateBool, searchMonthDay, searchMonth, searchMonthYr, searchYr;

        public expense()
        {
            InitializeComponent();
            conn = new MySqlConnection("server=localhost;user id=root;database=prototype_sad;password=root;persistsecurityinfo=False");

            // Renderers (to remove default blue hightlights or mouseovers)
            donorInfo.Renderer = new renderer2(); brInfo.Renderer = new renderer2();

            // Setting Dates
            datePledge.Value = dateBR.MaxDate = datePledge.MaxDate = datePledgeEdit.MaxDate = DateTime.Today;
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
            tabNewDonorInput.SelectedIndex = cbDType.SelectedIndex = cbPledge.SelectedIndex = 0;
            txtDName.Text = "Name of donor.";            
            txtPhone.Text = "29xxxxx"; txtMobile1.Text = "09xx"; txtMobile2.Text = "xxx"; txtMobile3.Text = "xxxx";
            txtEmail.Text = "example@example.com"; txtDonorAd.Text = "Enter address.";
            datePledge.Value = DateTime.Today;
        }

        public void resetDonorShowTS()                      // Reset buttons (acting like toolstrips)
        {
            moneyTS.ForeColor = ikTS.ForeColor = donorOTS.ForeColor = Color.Gainsboro;            
        }

        public void resetEditColorDefault()                 // Reset to default colors on Edit Donor Profile (gray)
        {
            lblDNameEdit.ForeColor = lblDTypeEdit.ForeColor = lblPledgeEdit.ForeColor = lblPhoneEdit.ForeColor = lblMobileEdit.ForeColor = lblEmailEdit.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);
            panelDNameEdit.BackgroundImage = panelPhoneEdit.BackgroundImage = panelEmailEdit.BackgroundImage = panelMobileEdit.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
            countDNameEdit.Visible = countEmailEdit.Visible = countPhoneEdit.Visible = false;
        }

        public void resetBudgetRequest()
        {
            brTS.ForeColor = btnBRNext.BackColor = btnBRConfirm.BackColor = System.Drawing.Color.FromArgb(62, 153, 141);
            btnBRNext.ForeColor = btnBRConfirm.ForeColor = Color.White;
        }

        public moneyDonate overlay()
        {
            moneyDonate mD = new moneyDonate();
            dim dim = new dim();

            dim.Location = this.Location;
            dim.refToPrev = this;
            dim.Show(this);
            mD.refToDim = dim;

            return mD;
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

        private void numOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) e.Handled = true;
        }       // Numbers only

        private void tableNoSelect_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ((DataGridView)sender).ClearSelection();
        }
        #endregion

        #region Main Buttons (Taskbar and Close)
        private void taskbar_Click(object sender, EventArgs e)
        {            
            btnDonation.ForeColor = btnFinance.ForeColor = btnMain.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            logo_finance.BackgroundImage = Properties.Resources.finance_fade;
            logo_donation.BackgroundImage = Properties.Resources.donation_fade;
            logo_main.BackgroundImage = Properties.Resources.main_fade;
            if(((Button)sender).Name != "btnExBack" || ((Button)sender).Name != "btnDonorInfoBack" || ((Button)sender).Name != "btnDonorsActive") ((Button)sender).ForeColor = System.Drawing.Color.FromArgb(15, 168, 104);
            if (((Button)sender).Name == "btnFinance" || ((Button)sender).Name == "btnExBack")
            {
                logo_finance.BackgroundImage = Properties.Resources.finance;
                get(2); get(3); get(4); get(5);
                tabSelection.SelectedTab = tabFinance;
            }
            else if(((Button)sender).Name == "btnDonation" || ((Button)sender).Name == "btnDonorInfoBack" || ((Button)sender).Name == "btnDonorsActive")            
            {
                btnDonation.ForeColor = System.Drawing.Color.FromArgb(15, 168, 104);
                if (!backMoneyDonation)
                {
                    btnDonateAllBack.Visible = false;
                    logo_donation.BackgroundImage = Properties.Resources.donation;
                    tabInnerDonors.SelectedIndex = 0;
                    tabSelection.SelectedTab = tabDonors;
                    lblListOfDonors.Text = "List of Donors";
                    panelListChild.BackColor = System.Drawing.Color.FromArgb(15, 168, 104);
                    loadDonorList();
                    current_donorID = 0;
                }
                else
                {
                    backMoneyDonation = true;
                    btnViewMD_Click(sender, e);
                }
            }
            else
            {
                logo_main.BackgroundImage = Properties.Resources.main;
            }
        }
        
        private void taskbar_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(System.Drawing.Color.FromArgb(240, 240, 240), 1);
            e.Graphics.DrawRectangle(p,
              e.ClipRectangle.Left,
              e.ClipRectangle.Top,
              e.ClipRectangle.Width - 1,
              e.ClipRectangle.Height - 1);
            base.OnPaint(e);
        }

        private void logo_click(object sender, EventArgs e)
        {
            btnDonation.ForeColor = btnFinance.ForeColor = btnMain.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            logo_finance.BackgroundImage = Properties.Resources.finance_fade;
            logo_donation.BackgroundImage = Properties.Resources.donation_fade;
            logo_main.BackgroundImage = Properties.Resources.main_fade;
            if (((PictureBox)sender).Name == "logo_finance")
            {
                btnFinance.ForeColor = System.Drawing.Color.FromArgb(15, 168, 104);
                logo_finance.BackgroundImage = Properties.Resources.finance;
                get(2); get(3); get(4); get(5);
                tabSelection.SelectedTab = tabFinance;
            }
            else if (((PictureBox)sender).Name == "logo_donation")
            {
                btnDonateAllBack.Visible = false;
                btnDonation.ForeColor = System.Drawing.Color.FromArgb(15, 168, 104);
                logo_donation.BackgroundImage = Properties.Resources.donation;
                tabInnerDonors.SelectedIndex = 0;
                tabSelection.SelectedTab = tabDonors;
                lblListOfDonors.Text = "List of Donors";
                panelListChild.BackColor = System.Drawing.Color.FromArgb(15, 168, 104);
                loadDonorList();
                current_donorID = 0;
            }
            else
            {
                btnMain.ForeColor = System.Drawing.Color.FromArgb(15, 168, 104);
                logo_main.BackgroundImage = Properties.Resources.main;
            }
        }
        #endregion

        #region SQL Connections
        public void loadTable(MySqlCommand comm, int type)
        {
            try
            {
                conn.Open();

                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (type == 1)          // Monetary donation
                {
                    if (dt.Rows.Count == 0)
                    {
                        dt.Rows.Add(-1, "No entries.", null, null, null, null, null, null, -1);
                        empty = true;
                    }

                    DataGridView table;
                    if (allMoneyDonation)
                    {
                        table = allMD;
                        lblListOfDonors.Text = "List of Monetary Donations";
                    }
                    else table = donationMoney;

                    table.DataSource = dt;

                    // Donation Money UI Modifications
                    table.Columns[1].HeaderText = "TYPE";
                    table.Columns[2].HeaderText = "AMOUNT";
                    table.Columns[3].HeaderText = "OR NO";
                    table.Columns[4].HeaderText = "CHECK NO";
                    table.Columns[5].HeaderText = "BANK NAME";
                    table.Columns[6].HeaderText = "DATE CHECK";
                    table.Columns[7].HeaderText = "DATE DONATED";
                    table.Columns[8].HeaderText = "DONATION ID";

                    // For ID purposes (hidden from user)            
                    table.Columns[0].Visible = table.Columns[4].Visible = table.Columns[5].Visible = table.Columns[8].Visible = false;

                    // 930 WIDTH
                    table.Columns[1].Width = 90;
                    table.Columns[2].Width = 300;
                    table.Columns[3].Width = 110;
                    table.Columns[6].Width = table.Columns[7].Width = 215;
                    table.Columns[1].HeaderCell.Style.Padding = table.Columns[1].DefaultCellStyle.Padding = new Padding(5, 0, 0, 0);

                    if (dt.Rows.Count > 0 && !empty)
                    {                        
                        table.Columns[2].DefaultCellStyle.Format = "#,0.00##";
                        table.Columns[6].DefaultCellStyle.Format = table.Columns[7].DefaultCellStyle.Format = "MMMM dd, yyyy";

                        btnDelMoneyD.Enabled = btnEditMoneyD.Enabled = multiSelect.Enabled = true;
                    }
                    else btnDelMoneyD.Enabled = btnEditMoneyD.Enabled = empty = multiSelect.Enabled = false;
                }
                else if (type == 2)          // In kind donation
                {
                    if (dt.Rows.Count == 0)
                    {
                        dt.Rows.Add(-1, "No entries.", null, null, -1);
                        empty = true;
                    }

                    donationIK.DataSource = dt;

                    // Donation In Kind UI Modifications
                    donationIK.Columns[1].HeaderText = "PARTICULAR";
                    donationIK.Columns[2].HeaderText = "QUANTITY";
                    donationIK.Columns[3].HeaderText = "DATE DONATED";
                    donationIK.Columns[4].HeaderText = "DONATION ID";

                    // For ID purposes (hidden from user)            
                    donationIK.Columns[0].Visible = donationIK.Columns[4].Visible = false;

                    // MONETARY TABLE COLUMNS
                    donationIK.Columns[1].Width = 550;
                    donationIK.Columns[2].Width = 150;
                    donationIK.Columns[3].Width = 230;
                    donationIK.Columns[1].HeaderCell.Style.Padding = donationIK.Columns[1].DefaultCellStyle.Padding = new Padding(15, 0, 0, 0);

                    if (dt.Rows.Count > 0 && !empty)
                    {                        
                        donationIK.Columns[3].DefaultCellStyle.Format = "MMMM dd, yyyy";
                        btnDelIK.Enabled = btnEditIK.Enabled = multiSelect2.Enabled = true;
                    }
                    else btnDelIK.Enabled = btnEditIK.Enabled = empty = multiSelect2.Enabled = false;
                }
                else if (type == 3)          // Budget Request Particular Details
                {
                    if (dt.Rows.Count == 0)
                    {
                        dt.Rows.Add(-1, "No entries.", null, null, null, null, null);
                        empty = true; btnEditBR.Enabled = btnDelBR.Enabled = false;
                    }
                    else empty = false; btnDelBR.Enabled = btnEditBR.Enabled = true;

                    BRDetails.DataSource = dt;

                    // Donation In Kind UI Modifications
                    BRDetails.Columns["particular"].HeaderText = "PARTICULAR";
                    BRDetails.Columns["quantity"].HeaderText = "QUANTITY";
                    BRDetails.Columns["unitPrice"].HeaderText = "UNIT PRICE";
                    BRDetails.Columns["amount"].HeaderText = "AMOUNT";
                    BRDetails.Columns["category"].HeaderText = "CATEGORY";

                    // For ID purposes (hidden from user)            
                    BRDetails.Columns["itemID"].Visible = BRDetails.Columns["budgetID"].Visible = false;

                    // 743 TOTAL WIDTH
                    BRDetails.Columns["particular"].Width = 320;
                    BRDetails.Columns["quantity"].Width = 73;
                    BRDetails.Columns["unitPrice"].Width = BRDetails.Columns["amount"].Width = 110;
                    BRDetails.Columns["category"].Width = 130;
                    BRDetails.Columns["particular"].HeaderCell.Style.Padding = BRDetails.Columns["particular"].DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
                }
                else if (type == 4)          // List of budget requests
                {
                    if (dt.Rows.Count == 0)
                    {
                        dt.Rows.Add(-1, null, "No entries.", null, null, null, null);
                        empty = true;
                    }
                    else empty = false;

                    DataGridView table;
                    if (aBR) table = approvedBRList;
                    else table = BRList;

                    table.DataSource = dt;

                    // BR LIST In Kind UI Modifications
                    table.Columns[2].HeaderText = "PURPOSE";
                    table.Columns[5].HeaderText = "DATE REQUESTED";
                    table.Columns[6].HeaderText = "REQUESTED BY";

                    // For ID purposes (hidden from user)            
                    table.Columns[0].Visible = table.Columns[1].Visible = table.Columns[3].Visible = table.Columns[4].Visible = table.Columns[7].Visible = false;

                    // 935 TOTAL WIDTH
                    table.Columns[2].Width = 505;
                    table.Columns[5].Width = table.Columns[6].Width = 215;
                    table.Columns[2].HeaderCell.Style.Padding = table.Columns[2].DefaultCellStyle.Padding = new Padding(15, 0, 0, 0);

                    if (dt.Rows.Count > 0 && !empty)
                    {                        
                        table.Columns[5].DefaultCellStyle.Format = "MMMM dd, yyyy";
                        btnViewBR.Enabled = notifBR.Visible = btnApprovedBR.Enabled = multiABR.Enabled = true;
                    }
                    if (empty && table == BRList) btnViewBR.Enabled = notifBR.Visible = false;
                }
                else if (type == 5)          // Selected budget request details load
                {
                    PBRDetails.DataSource = dt;

                    // BR LIST In Kind UI Modifications
                    PBRDetails.Columns[1].HeaderText = "PARTICULAR";
                    PBRDetails.Columns[2].HeaderText = "QUANTITY";
                    PBRDetails.Columns[3].HeaderText = "UNIT PRICE";
                    PBRDetails.Columns[4].HeaderText = "AMOUNT";

                    // For ID purposes (hidden from user)            
                    PBRDetails.Columns[0].Visible = PBRDetails.Columns[5].Visible = false;

                    // 935 TOTAL WIDTH
                    PBRDetails.Columns[1].Width = 485;
                    PBRDetails.Columns[2].Width = PBRDetails.Columns[3].Width = PBRDetails.Columns[4].Width = 150;
                    PBRDetails.Columns[1].HeaderCell.Style.Padding = PBRDetails.Columns[1].DefaultCellStyle.Padding = new Padding(15, 0, 0, 0);
                }
                else if (type == 6)          // Expenses Table
                {
                    if (dt.Rows.Count == 0)
                    {
                        dt.Rows.Add(-1, "No entries.", null, null, null, null);
                        empty = true; btnExpOp.Enabled = multiExp.Enabled = false;
                    }
                    else empty = false; btnExpOp.Enabled = multiExp.Enabled = true;                                           

                    expList.DataSource = dt;

                    // ADD COLUMN TO CHECK BUDGET
                    DataColumn hasBudget = new System.Data.DataColumn("hasBudget", typeof(System.String));
                    hasBudget.DefaultValue = "";
                    dt.Columns.Add(hasBudget);                                        

                    // EXP LIST In Kind UI Modifications
                    expList.Columns[1].HeaderText = "EXPENSE DATE";
                    expList.Columns[2].HeaderText = "CATEGORY";
                    expList.Columns[3].HeaderText = "AMOUNT";
                    expList.Columns[5].HeaderText = "FROM BUDGET";

                    // For ID purposes (hidden from user)            
                    expList.Columns[0].Visible = expList.Columns[4].Visible = false;

                    // 935 TOTAL WIDTH
                    expList.Columns[1].Width = 220;
                    expList.Columns[2].Width = 380;
                    expList.Columns[3].Width = 240;
                    expList.Columns[5].Width = 95;
                    expList.Columns[1].HeaderCell.Style.Padding = expList.Columns[1].DefaultCellStyle.Padding = new Padding(15, 0, 0, 0);

                    if (dt.Rows.Count > 0 && !empty)
                    {
                        for(int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["budgetID"].ToString() != "") dt.Rows[i]["hasBudget"] = "Yes";   
                            else dt.Rows[i]["hasBudget"] = "No";
                        }                        
                        expList.Columns[1].DefaultCellStyle.Format = "MMMM dd, yyyy";
                        btnExpOp.Enabled = true;
                        if (dt.Rows.Count == 1) multiExp.Enabled = false;
                        else multiExp.Enabled = true;
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                errorMessage(ex.Message);
            }
        }

        public void loadDonorList()
        {
            tabSelection.SelectedTab = tabDonors; empty = false;
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT donorID, donorName, pledge, datePledge, dateAdded, address FROM donor WHERE status = 1 ORDER BY dateAdded DESC", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                lblListOfDonors.Text = "List of Donors";

                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(-1, "No entries.", null, null, null);
                    empty = true; multiDonor.Enabled = false;
                }
                else empty = false;

                donorsGV.DataSource = dt;

                // Donors Grid View UI Modificationsddddd
                donorsGV.Columns[1].HeaderText = "DONOR NAME";
                donorsGV.Columns[2].HeaderText = "PLEDGE";
                donorsGV.Columns[3].HeaderText = "DATE OF PLEDGE";
                donorsGV.Columns[4].HeaderText = "ADDED ON";
                donorsGV.Columns[0].Visible = donorsGV.Columns[5].Visible = false;

                // 935 WIDTH
                donorsGV.Columns[1].Width = 403;
                donorsGV.Columns[2].Width = 132;
                donorsGV.Columns[3].Width = donorsGV.Columns[4].Width = 200;

                donorsGV.Columns[1].HeaderCell.Style.Padding = new Padding(10, 0, 0, 0);
                donorsGV.Columns[1].DefaultCellStyle.Padding = new Padding(15, 0, 0, 0);

                if (dt.Rows.Count > 0 && !empty)
                {
                    donorsGV.Columns[3].DefaultCellStyle.Format = "MMMM dd, yyyy";
                    donorsGV.Columns[4].DefaultCellStyle.Format = "MMMM dd, yyyy hh:mm tt";
                    multiDonor.Enabled = true;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                errorMessage(ex.Message);
                conn.Close();
            }
        }

        public void loadArchiveDonor()
        {
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT donorID, donorName, pledge, datePledge, dateArchived FROM donor WHERE status = 0 ORDER BY dateArchived DESC", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(-1, "No entries.", null, null);
                    empty = true; multiArchiveD.Enabled = false;
                }
                else empty = false; multiArchiveD.Enabled = true;

                archiveDonors.DataSource = dt;

                // Donors Grid View UI Modificationsddddd
                archiveDonors.Columns[1].HeaderText = "DONOR NAME";
                archiveDonors.Columns[2].HeaderText = "PLEDGE";
                archiveDonors.Columns[3].HeaderText = "DATE OF PLEDGE";
                archiveDonors.Columns[4].HeaderText = "INACTIVE SINCE";
                archiveDonors.Columns[0].Visible = false;

                // 935 WIDTH
                archiveDonors.Columns[1].Width = 403;
                archiveDonors.Columns[2].Width = 132;
                archiveDonors.Columns[3].Width = archiveDonors.Columns[4].Width = 200;

                archiveDonors.Columns[1].HeaderCell.Style.Padding = new Padding(10, 0, 0, 0);
                archiveDonors.Columns[1].DefaultCellStyle.Padding = new Padding(15, 0, 0, 0);

                if (dt.Rows.Count > 0 && !empty)
                {
                    archiveDonors.Columns[3].DefaultCellStyle.Format = "MMMM dd, yyyy";
                    archiveDonors.Columns[4].DefaultCellStyle.Format = "MMMM dd, yyyy hh:mm tt";
                    multiDonor.Enabled = true;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                errorMessage(ex.Message);
            }
        }

        public void loadDonorInfo(int id)
        {
            tabSelection.SelectedTab = tabDonorInfo;
            tabDonorDetails.SelectedIndex = 0;
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
                }
                else
                {
                    comm = new MySqlCommand("SELECT monetaryID, paymentType, amount, ORno, checkNo, bankName, dateCheck, dateDonated, donationID FROM monetary WHERE donationID in (SELECT donation.donationID FROM donation INNER JOIN donor ON donation.donorID = donor.donorID WHERE donor.donorID = " + current_donorID + ")", conn);
                    loadTable(comm, 1);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                errorMessage(ex.Message);
            }
        }

        public void loadIK(int id)
        {
            try
            {
                MySqlCommand comm = new MySqlCommand("SELECT inKindID, particular, quantity, dateDonated, donationID FROM inkind WHERE donationID in (SELECT donation.donationID FROM donation INNER JOIN donor ON donation.donorID = donor.donorID WHERE donor.donorID = " + id + ")", conn);
                loadTable(comm, 2);
            }
            catch (Exception ex)
            {
                errorMessage(ex.Message);
            }
        }

        public void searchMonetary(int id, string search)
        {
            try
            {
                decimal n; string query = "", searchWords = "";
                bool isNumeric = decimal.TryParse(search, out n);

                if (isNumeric && !searchDateBool)
                {
                    n = decimal.Parse(search);                                          // Convert search keyword to double
                    string doubs = n.ToString("F2", CultureInfo.InvariantCulture);      // Convert to string with 2 decimal places                    
                    if (id != -10)
                    {
                        query = "SELECT monetaryID, paymentType, amount, ORno, checkNo, bankName, dateCheck, dateDonated, donationID FROM monetary WHERE donationID in (SELECT donation.donationID FROM donation INNER JOIN donor ON donation.donorID = donor.donorID WHERE donor.donorID = " + id + ")"
                        + " AND (amount LIKE " + decimal.Parse(doubs) + " OR ORno LIKE '%" + search + "%' OR checkNo LIKE '%" + search + "%' OR bankName LIKE '%" + search + "%') ORDER BY dateDonated DESC";
                    }
                    else if (id == -20) query = "SELECT * FROM expense WHERE amount LIKE " + decimal.Parse(doubs) + " OR category = '%" + search + "%' ORDER BY dateExpense DESC)";
                    else query = "SELECT monetaryID, paymentType, amount, ORno, checkNo, bankName, dateCheck, dateDonated, donationID FROM monetary WHERE amount LIKE " + decimal.Parse(doubs) + " OR ORno LIKE '%" + search + "%' OR checkNo LIKE '%" + search + "%' OR bankName LIKE '%" + search + "%' ORDER BY dateDonated DESC";
                }
                else if (searchDateBool)
                {
                    searchDateBool = false;

                    if (searchMonthDay)
                    {
                        searchMonthDay = false;
                        searchWords = "((MONTH(dateDonated) = " + fromDateValue.ToString("MM") + " AND DAY(dateDonated) = " + fromDateValue.ToString("dd") + ") OR (MONTH(dateCheck) = " + fromDateValue.ToString("MM") + " AND DAY(dateCheck) = " + fromDateValue.ToString("dd")
                            + ") OR (MONTH(dateDonated) = " + fromDateValue.ToString("MMM") + " AND DAY(dateDonated) = " + fromDateValue.ToString("dd") + ") OR (MONTH(dateDonated) = " + fromDateValue.ToString("MMMM") + " AND DAY(dateDonated) = " + fromDateValue.ToString("dd") + "))";
                    }
                    else if (searchMonthYr)
                    {
                        searchMonthYr = false;
                        searchWords = "((MONTH(dateDonated) = " + fromDateValue.ToString("MM") + " AND YEAR(dateDonated) = " + fromDateValue.ToString("yyyy") + ") OR (MONTH(dateCheck) = " + fromDateValue.ToString("MM") + " AND YEAR(dateCheck) = " + fromDateValue.ToString("yyyy") + "))";
                    }
                    else if (searchMonth)
                    {
                        searchMonth = false;
                        searchWords = "(MONTH(dateDonated) = " + fromDateValue.ToString("MM") + " OR MONTH(dateCheck) = " + fromDateValue.ToString("MM") + ")";
                    }
                    else if (searchYr)
                    {
                        searchYr = false;
                        searchWords = "(YEAR(dateDonated) = " + fromDateValue.ToString("yyyy") + " OR YEAR(dateCheck) = " + fromDateValue.ToString("yyyy") + ")";
                    }
                    else
                    {
                        searchWords = "(dateDonated LIKE '" + fromDateValue + "' OR MONTH(dateDonated) = " + fromDateValue.ToString("MM") + " OR YEAR(dateDonated) = " + fromDateValue.ToString("yyyy")
                            + "(dateCheck LIKE '" + fromDateValue + "' OR MONTH(dateCheck) = " + fromDateValue.ToString("MM") + " OR YEAR(dateCheck) = " + fromDateValue.ToString("yyyy") + ")";
                    }

                    if (id != -10) query = "SELECT monetaryID, paymentType, amount, ORno, checkNo, bankName, dateCheck, dateDonated, donationID FROM monetary WHERE donationID in (SELECT donation.donationID FROM donation INNER JOIN donor ON donation.donorID = donor.donorID WHERE donor.donorID = " + id + ") AND " + searchWords + " ORDER BY dateDonated DESC";
                    else if (id == -20) query = "SELECT * FROM expense WHERE " + searchWords + " ORDER BY dateExpense DESC";
                    else query = "SELECT monetaryID, paymentType, amount, ORno, checkNo, bankName, dateCheck, dateDonated, donationID FROM monetary WHERE " + searchWords + " ORDER BY dateDonated DESC";
                }
                else
                {
                    if (id != -10) query = "SELECT monetaryID, paymentType, amount, ORno, checkNo, bankName, dateCheck, dateDonated, donationID FROM monetary WHERE donationID in (SELECT donation.donationID FROM donation INNER JOIN donor ON donation.donorID = donor.donorID WHERE donor.donorID = " + id + ") AND (ORno LIKE '%" + search + "%' OR checkNo LIKE '%" + search + "%' OR bankName LIKE '%" + search + "%')";
                    else if (id != -20) query = "SELECT * FROM expense WHERE catergory LIKE '%" + search + "%' ORDER BY dateExpense DESC";
                    else query = "SELECT monetaryID, paymentType, amount, ORno, checkNo, bankName, dateCheck, dateDonated, donationID FROM monetary WHERE ORno LIKE '%" + search + "%' OR checkNo LIKE '%" + search + "%' OR bankName LIKE '%" + search + "%'";
                }
                MySqlCommand comm = new MySqlCommand(query, conn);
                if (id != -20) loadTable(comm, 1);
                else loadTable(comm, 6);                
            }
            catch (Exception ex)
            {
                errorMessage(ex.Message);
            }
        }

        public void delDonation(int donationid, int type)
        {
            try
            {
                conn.Open();

                if (type == 1)               // Monetary Table Delete
                {
                    MySqlCommand comm = new MySqlCommand("DELETE FROM monetary WHERE donationID = " + donationid, conn);
                    comm.ExecuteNonQuery();

                    comm = new MySqlCommand("DELETE FROM donation WHERE donationID = " + donationid, conn);
                    comm.ExecuteNonQuery();
                }
                else if (type == 2)               // In-kind Table Delete
                {
                    MySqlCommand comm = new MySqlCommand("DELETE FROM inkind WHERE donationID = " + donationid, conn);
                    comm.ExecuteNonQuery();

                    comm = new MySqlCommand("DELETE FROM donation WHERE donationID = " + donationid, conn);
                    comm.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                errorMessage(ex.Message);
            }
        }

        public void del(int ID, int type)
        {
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand("");
                // Delete donor
                if (type == 1) comm = new MySqlCommand("DELETE FROM donor WHERE donorID = " + ID, conn);
                // Delete item
                else if (type == 2) comm = new MySqlCommand("DELETE FROM item WHERE itemID = " + ID, conn);
                // Delete expense record
                else if (type == 3) comm = new MySqlCommand("DELETE FROM expense WHERE expenseID = " + ID, conn);
                // Delete budget record
                else if (type == 4) comm = new MySqlCommand("DELETE FROM budget WHERE budgetID = " + ID, conn);                

                comm.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                errorMessage(ex.Message);
            }
        }

        public void get(int type)
        {
            try
            {
                DataTable dt = new DataTable();
                MySqlDataAdapter adp = new MySqlDataAdapter();

                if (type == 1)           // Get Budget ID
                {
                    conn.Open();
                    MySqlCommand comm = new MySqlCommand("INSERT INTO budget (status) VALUES ('Pending');", conn);
                    comm.ExecuteNonQuery();

                    adp = new MySqlDataAdapter("SELECT budgetID FROM budget ORDER BY budgetID DESC LIMIT 1", conn);
                    adp.Fill(dt);

                    current_budgetID = int.Parse(dt.Rows[0]["budgetID"].ToString());

                    comm = new MySqlCommand("SELECT * FROM item WHERE budgetID = " + current_budgetID, conn);
                    conn.Close();
                    loadTable(comm, 3);
                }
                else if (type == 2)      // Get Notif
                {
                    adp = new MySqlDataAdapter("SELECT COUNT(budgetID) as COUNT FROM budget WHERE status = 'Pending'", conn);
                    dt = new DataTable();
                    adp.Fill(dt);

                    if (dt.Rows[0]["COUNT"].ToString() != "0")
                    {
                        notifBR.Visible = btnViewBR.Enabled = true;
                        if (int.Parse(dt.Rows[0]["COUNT"].ToString()) < 10) notifBR.Text = "0" + dt.Rows[0]["COUNT"].ToString();
                        else notifBR.Text = dt.Rows[0]["COUNT"].ToString();
                    }
                    else notifBR.Visible = btnViewBR.Enabled = false;
                    conn.Close();
                }
                else if (type == 3)      // Enable button if existing approved budget requests
                {
                    adp = new MySqlDataAdapter("SELECT COUNT(budgetID) as COUNT FROM budget WHERE status = 'Approved'", conn);
                    dt = new DataTable();
                    adp.Fill(dt);

                    if (dt.Rows[0]["COUNT"].ToString() != "0") btnApprovedBR.Enabled = true;
                    else btnApprovedBR.Enabled = false;
                    conn.Close();
                }
                else if (type == 4)     // Get latest dates for finance viewing
                {
                    adp = new MySqlDataAdapter("SELECT dateRequested FROM budget ORDER BY dateRequested DESC LIMIT 1", conn);
                    dt = new DataTable();
                    adp.Fill(dt);

                    if (dt.Rows.Count != 0) lbllastDateBR.Text = DateTime.Parse(dt.Rows[0]["dateRequested"].ToString()).ToString("MMMM dd, yyyy");
                    else lbllastDateBR.Text = "None";

                    adp = new MySqlDataAdapter("SELECT dateDonated FROM monetary ORDER BY dateDonated DESC LIMIT 1", conn);
                    dt = new DataTable();
                    adp.Fill(dt);

                    if (dt.Rows.Count != 0) lblLastDonate.Text = DateTime.Parse(dt.Rows[0]["dateDonated"].ToString()).ToString("MMMM dd, yyyy");
                    else lblLastDonate.Text = "None";

                    adp = new MySqlDataAdapter("SELECT dateExpense FROM expense ORDER BY dateExpense DESC LIMIT 1", conn);
                    dt = new DataTable();
                    adp.Fill(dt);

                    if (dt.Rows.Count != 0) lblLastExpense.Text = DateTime.Parse(dt.Rows[0]["dateExpense"].ToString()).ToString("MMMM dd, yyyy");
                    else lblLastExpense.Text = "None";

                    conn.Close();
                }
                else if (type == 5)      // Get total sum for this month
                {
                    adp = new MySqlDataAdapter("SELECT SUM(amount) as total FROM monetary WHERE MONTH(dateDonated) = " + int.Parse(DateTime.Today.Month.ToString()), conn);
                    dt = new DataTable();
                    adp.Fill(dt);

                    if (dt.Rows.Count != 0) lblTotalDonation.Text = string.Format("{0:#,##0.00}", double.Parse(dt.Rows[0]["total"].ToString()));
                    else lblTotalDonation.Text = "0.00";                   

                    adp = new MySqlDataAdapter("SELECT SUM(amount) as total FROM expense WHERE MONTH(dateExpense) = " + int.Parse(DateTime.Today.Month.ToString()), conn);
                    dt = new DataTable();
                    adp.Fill(dt);

                    if (dt.Rows.Count != 0) lblTotalExp.Text = dt.Rows[0]["total"].ToString();
                    else lblTotalExp.Text = "0.00";

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                errorMessage(ex.Message);
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
            btnMain.Enabled = btnDonation.Enabled = btnFinance.Enabled = false;
            logo_main.Enabled = logo_finance.Enabled = logo_donation.Enabled = false;
            resetNewDonor(); editDonor = false;
            tabSelection.SelectedTab = tabNewDonor;
            donorTS.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141); donorCTS.ForeColor = System.Drawing.Color.FromArgb(197, 217, 208);
            btnDonorConfirm.ForeColor = btnDonorFinal.ForeColor = Color.White;

            conf1.ForeColor = conf2.ForeColor = conf3.ForeColor = conf4.ForeColor = conf5.ForeColor = conf6.ForeColor = conf7.ForeColor = btnDonorConfirm.BackColor = System.Drawing.Color.FromArgb(62, 153, 141);
        }

        private void btnBackDonorList_Click(object sender, EventArgs e)
        {
            btnDonateAllBack.Visible = false;
            tabInnerDonors.SelectedIndex = 0;
            loadDonorList();
        }

        private void multiDonor_CheckedChanged(object sender, EventArgs e)
        {
            if (multiDonor.Checked) donorsGV.MultiSelect = true;
            else donorsGV.MultiSelect = false;
        }

        private void btnRemoveDonor_Click(object sender, EventArgs e)
        {
            confirmMessage("Are you sure you want to make this donor inactive?");
            if (confirmed)
            {
                try
                {
                    conn.Open();
                    MySqlCommand comm = new MySqlCommand("UPDATE donor SET status = 0, dateArchived = '" + DateTime.Today.ToString("yyyy-MM-dd") + "' WHERE donorID = " + current_donorID, conn);
                    comm.ExecuteNonQuery();
                    conn.Close();
                    successMessage("Donor has been changed to inactive.");
                }
                catch (Exception ex)
                {
                    errorMessage(ex.Message);
                }
                resetDonorShowTS();
                btnDonateAllBack.Visible = false;
                loadDonorList();
                tabSelection.SelectedTab = tabDonors;
            }
        }
        #endregion

        #region New Donor Form
        private void btnDonorConfirm_Click(object sender, EventArgs e)
        {
            if (txtDName.Text == "" || txtMobile1.TextLength + txtMobile2.TextLength + txtMobile3.TextLength != 11 || txtPhone.TextLength != 7) errorMessage("You have an error in at least one of the fields.");
            else
            {
                tabNewDonorInput.SelectedTab = tabDonorConfirm;
                donorCTS.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141); donorTS.ForeColor = System.Drawing.Color.FromArgb(197, 217, 208);
                donorCTS.Font = new System.Drawing.Font("Segoe UI", 20.25f); donorTS.Font = new System.Drawing.Font("Segoe UI Semilight", 20.25f);
                
                btnDonorFinal.BackColor = btnDonorBack2.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                btnDonorFinal.ForeColor = Color.White;

                conf1.ForeColor = conf2.ForeColor = conf3.ForeColor = conf4.ForeColor = conf5.ForeColor = conf6.ForeColor = conf7.ForeColor = conf10.ForeColor = btnDonorFinal.BackColor = System.Drawing.Color.FromArgb(113, 176, 168);
                conf_header.Text = "CONFIRM NEW DONOR"; conf_header.ForeColor = System.Drawing.Color.FromArgb(197, 217, 208);

                if (txtPhone.Text == "29xxxxx") conf_phone.Text = "N/A";
                else conf_phone.Text = txtPhone.Text;
                if (txtMobile1.Text == "09xx") conf_mobile.Text = "N/A";
                else conf_mobile.Text = txtMobile1.Text + "-" + txtMobile2.Text + "-" + txtMobile3.Text;
                if (txtEmail.Text == "example@example.com") conf_email.Text = "N/A";
                else conf_email.Text = txtEmail.Text;
                if (txtDonorAd.Text == "Enter address.") conf_DonorAd.Text = "N/A";
                else conf_DonorAd.Text = txtDonorAd.Text;

                conf_donorName.Text = txtDName.Text;
                conf_donorType.Text = cbDType.Text; 
                conf_pledge.Text = cbPledge.Text;
                conf_datePledge.Text = datePledge.Text;
            }
        }

        private void btnDonorBack_Click(object sender, EventArgs e)
        {
            donorTS.Font = new System.Drawing.Font("Segoe UI", 20.25f); donorCTS.Font = new System.Drawing.Font("Segoe UI Semilight", 20.25f);
            donorCTS.ForeColor = System.Drawing.Color.FromArgb(197, 217, 208);
            if (!editDonor)     // New Donor
            {
                tabNewDonorInput.SelectedTab = tabNewInfo;
                donorTS.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            }
            else                // Edit Donor
            {
                tabNewDonorInput.SelectedTab = tabEditDonor;
                donorTS.ForeColor = System.Drawing.Color.FromArgb(219, 209, 92);

            }
        }

        private void btnDonorFinal_Click(object sender, EventArgs e)
        {
            try
            {
                btnMain.Enabled = btnDonation.Enabled = btnFinance.Enabled = true;
                logo_main.Enabled = logo_finance.Enabled = logo_donation.Enabled = true;
                conn.Open();
                MySqlCommand comm = new MySqlCommand("");

                if (!editDonor)     // ADD SQL COMMAND
                {
                    String mobile = "N/A";
                    String datePledgeSTR = datePledge.Value.Year.ToString() + "-" + datePledge.Value.Month.ToString() + "-" + datePledge.Value.Day.ToString();
                    if (conf_mobile.Text != "N/A") mobile = txtMobile1.Text + txtMobile2.Text + txtMobile3.Text;

                    comm = new MySqlCommand("INSERT INTO donor (type, donorName, telephone, mobile, email, pledge, datePledge, dateAdded, status, address)"
                        + " VALUES (" + (int.Parse(cbDType.SelectedIndex.ToString()) + 1) + ", '" + conf_donorName.Text + "', '" + conf_phone.Text
                        + "', '" + mobile + "', '" + conf_email.Text + "', '" + conf_pledge.Text + "', '" + datePledge.Value.ToString("yyyy-MM-dd") + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 1, '" + conf_DonorAd.Text + "');", conn);

                    successMessage("Donor profile has been added successfully.");
                }
                else
                {
                    String mobile = "N/A";
                    String datePledgeSTR = datePledgeEdit.Value.Year.ToString() + "-" + datePledgeEdit.Value.Month.ToString() + "-" + datePledge.Value.Day.ToString();
                    if (conf_mobile.Text != "N/A") mobile = txtMobile1Edit.Text + txtMobile2Edit.Text + txtMobile3Edit.Text;                   

                    comm = new MySqlCommand("UPDATE donor SET type = " + (int.Parse(cbDTypeEdit.SelectedIndex.ToString()) + 1) + ", donorName = '" + conf_donorName.Text
                        + "', telephone = '" + conf_phone.Text + "', mobile = '" + mobile + "', email = '" + conf_email.Text
                        + "', pledge = '" + conf_pledge.Text + "', address = '" + conf_DonorAd.Text + "' WHERE donorID = " + current_donorID, conn);

                    successMessage("Donor profile has been changed successfully.");
                }
                comm.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                errorMessage(ex.Message);
            }
            btnDonateAllBack.Visible = false;
            loadDonorList();
        }

        private void btnDonorCancel_Click(object sender, EventArgs e)
        {
            tabSelection.SelectedTab = tabDonors;
            btnMain.Enabled = btnDonation.Enabled = btnFinance.Enabled = true;
            logo_main.Enabled = logo_finance.Enabled = logo_donation.Enabled = true;
        }

        private void btnDonorBack2_Click(object sender, EventArgs e)
        {            
            if(btnDonorFinal.ForeColor == Color.White)
            {
                tabSelection.SelectedTab = tabNewDonor;
                tabNewDonorInput.SelectedTab = tabNewInfo;
            }
            else
            {
                tabSelection.SelectedTab = tabNewDonor;
                tabNewDonorInput.SelectedTab = tabEditDonor;
            }
        }
        #endregion

        #region Donor Edit
        private void btnEditDonor_Click(object sender, EventArgs e)
        {
            btnMain.Enabled = btnDonation.Enabled = btnFinance.Enabled = false;
            logo_main.Enabled = logo_finance.Enabled = logo_donation.Enabled = false;

            tabSelection.SelectedTab = tabNewDonor;
            tabNewDonorInput.SelectedTab = tabEditDonor;
            tabDonorDetails.SelectedIndex = 0;

            donorTS.ForeColor = System.Drawing.Color.FromArgb(219, 209, 92);
            donorCTS.ForeColor = System.Drawing.Color.FromArgb(197, 217, 208);
            donorOTS.BackColor = Color.White; donorOTS.ForeColor = Color.Gainsboro;
            moneyTS.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);

            txtDNameEdit.Text = lblDonorName.Text;
            cbDTypeEdit.SelectedText = txtDType.Text;
            cbPledgeEdit.SelectedText = txtDPledge.Text;
            if (txtDPhone.Text != "N/A") txtPhoneEdit.Text = txtDPhone.Text;
            if (txtDMobile.Text != "N/A")
            {
                txtMobile1Edit.Text = txtDMobile.Text[0].ToString() + txtDMobile.Text[1].ToString() + txtDMobile.Text[2].ToString() + txtDMobile.Text[3].ToString();
                txtMobile2Edit.Text = txtDMobile.Text[4].ToString() + txtDMobile.Text[5].ToString() + txtDMobile.Text[6].ToString();
                txtMobile3Edit.Text = txtDMobile.Text[7].ToString() + txtDMobile.Text[8].ToString() + txtDMobile.Text[9].ToString() + txtDMobile.Text[10].ToString();
            }

            datePledgeEdit.MaxDate = DateTime.Today;
            var formats = new[] { "MMMM dd, yyyy" };
            if (DateTime.TryParseExact(txtDDatePledge.Text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue)) datePledgeEdit.Value = fromDateValue;
            else datePledgeEdit.Value = dateBR.MaxDate;

            txtEmailEdit.Text = txtDEmail.Text;
            txtDonorAdEdit.Text = txtDAddress.Text;
        }

        private void btnDonorEditCancel_Click(object sender, EventArgs e)
        {
            btnMain.Enabled = btnDonation.Enabled = btnFinance.Enabled = true;
            logo_main.Enabled = logo_finance.Enabled = logo_donation.Enabled = true;
            tabSelection.SelectedTab = tabDonorInfo;
            tabDonorDetails.SelectedTab = tabMoney;
        }

        private void editDonor_Enter(object sender, EventArgs e)
        {            
            if (((TextBox)sender).Text == "Name of donor." || ((TextBox)sender).Text == "29xxxxx" || ((TextBox)sender).Text == "example@example.com" || ((TextBox)sender).Text == "09xx" || ((TextBox)sender).Text == "xxx" || ((TextBox)sender).Text == "xxxx") ((TextBox)sender).Text = "";
            ((TextBox)sender).ForeColor = Color.Black;

            if (((TextBox)sender).Name == "txtMobile1Edit" || ((TextBox)sender).Name == "txtMobile2Edit" || ((TextBox)sender).Name == "txtMobile3Edit")
            {
                lblMobileEdit.ForeColor = System.Drawing.Color.FromArgb(218, 195, 87);
                panelMobileEdit.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
            }

            if (((TextBox)sender).Name == "txtDNameEdit") 
            {
                lblDNameEdit.ForeColor = System.Drawing.Color.FromArgb(218, 195, 87);
                panelDNameEdit.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                countDNameEdit.Visible = true;
            }
            else if (((TextBox)sender).Name == "txtPhoneEdit") 
            {
                lblPhoneEdit.ForeColor = System.Drawing.Color.FromArgb(218, 195, 87);
                panelPhoneEdit.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                countPhoneEdit.Visible = true;
            }
            else if (((TextBox)sender).Name == "txtEmailEdit") 
            {
                lblEmailEdit.ForeColor = System.Drawing.Color.FromArgb(218, 195, 87);
                panelEmailEdit.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                countEmailEdit.Visible = true;
            }
        }

        private void editDonor_TextChanged(object sender, EventArgs e)
        {
            if(((TextBox)sender).Name == "txtDNameEdit") countDNameEdit.Text = ((TextBox)sender).TextLength + "/250";            
            else if (((TextBox)sender).Name == "txtPhoneEdit") countPhoneEdit.Text = ((TextBox)sender).TextLength + "/7";
            else if (((TextBox)sender).Name == "txtEmailEdit") countEmailEdit.Text = ((TextBox)sender).TextLength + "/100";
        }

        private void editDonor_Leave(object sender, EventArgs e)
        {
            resetEditColorDefault();
        }

        private void btnDonorEditConf_Click(object sender, EventArgs e)
        {
            editDonor = true;
            if (txtDNameEdit.Text == "" || txtMobile1Edit.TextLength + txtMobile2Edit.TextLength + txtMobile3Edit.TextLength != 11 || txtPhoneEdit.TextLength != 7) errorMessage("You have an error in at least one of the fields.");
            else
            {
                tabNewDonorInput.SelectedTab = tabDonorConfirm;
                donorCTS.ForeColor = System.Drawing.Color.FromArgb(218, 195, 87);
                donorTS.ForeColor = System.Drawing.Color.FromArgb(197, 217, 208);
                donorCTS.Font = new System.Drawing.Font("Segoe UI", 20.25f);
                donorTS.Font = new System.Drawing.Font("Segoe UI Semilight", 20.25f);

                conf1.ForeColor = conf2.ForeColor = conf3.ForeColor = conf4.ForeColor = conf5.ForeColor = conf6.ForeColor = conf7.ForeColor = conf10.ForeColor = System.Drawing.Color.FromArgb(218, 195, 87);

                btnDonorFinal.ForeColor = btnDonorBack2.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
                btnDonorFinal.BackColor = System.Drawing.Color.FromArgb(218, 195, 87);

                conf_header.Text = "CONFIRM DONOR CHANGES"; conf_header.ForeColor = System.Drawing.ColorTranslator.FromHtml("#eae6b4");

                conf_donorName.Text = txtDNameEdit.Text; conf_donorType.Text = cbDTypeEdit.Text; conf_pledge.Text = cbPledgeEdit.Text;
                conf_datePledge.Text = datePledgeEdit.Value.ToString("MMMM dd, yyyy");
                conf_phone.Text = txtPhoneEdit.Text; conf_mobile.Text = txtMobile1Edit.Text + txtMobile2Edit.Text + txtMobile3Edit.Text;
                conf_email.Text = txtEmailEdit.Text; conf_DonorAd.Text = txtDonorAdEdit.Text;
            }
        }
        
        private void btnActiveAgain_Click(object sender, EventArgs e)
        {
            confirmMessage("Are you sure you want to activate this donor again?");
            if (confirmed)
            {                
                if (!multiArchiveD.Checked)
                {
                    int row = archiveDonors.CurrentCell.RowIndex;
                    conn.Open();
                    comm = new MySqlCommand("UPDATE donor SET status = 1 WHERE donorID = " + archiveDonors.Rows[row].Cells[0].Value, conn);
                    comm.ExecuteNonQuery();
                    conn.Close();
                    successMessage("Selected donor has been activated successfullly.");
                }
                else
                {
                    foreach (DataGridViewRow r in archiveDonors.SelectedRows)
                    {
                        int row = archiveDonors.CurrentCell.RowIndex;
                        conn.Open();                       
                        comm = new MySqlCommand("UPDATE donor SET status = 1 WHERE donorID = " + archiveDonors.Rows[row].Cells[0].Value, conn);
                        comm.ExecuteNonQuery();
                        conn.Close();
                        loadArchiveDonor();
                    }
                    multiArchiveD.Checked = false;
                    successMessage("Selected donors have been activated successfullly.");
                }
                loadArchiveDonor();
            }
        }
        
        private void multiArchiveD_CheckedChanged(object sender, EventArgs e)
        {
            if (multiArchiveD.Checked) archiveDonors.MultiSelect = true;
            else archiveDonors.MultiSelect = false;
        }
        #endregion

        #region Display Donor Info
        private void tabDonorInfo_Click(object sender, EventArgs e)                                     // To remove mouse cursor over selected donor profile textboxes
        {
            tabDonorInfo.Focus();
        }

        private void donorsGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && !empty)
            {
                cbFilter.SelectedIndex = 0;
                current_donorID = int.Parse(donorsGV.Rows[e.RowIndex].Cells[0].FormattedValue.ToString());
                loadDonorInfo(current_donorID);
                txtDDatePledge.Text = donorsGV.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
                txtDAddress.Text = donorsGV.Rows[e.RowIndex].Cells[5].FormattedValue.ToString();

                allMoneyDonation = false;
                comm = new MySqlCommand("SELECT monetaryID, paymentType, amount, ORno, checkNo, bankName, dateCheck, dateDonated, donationID FROM monetary WHERE donationID in (SELECT donation.donationID FROM donation INNER JOIN donor ON donation.donorID = donor.donorID WHERE donor.donorID = " + current_donorID + ")", conn);
                loadTable(comm, 1);
            }
        }
        
        private void donorInfo_TS(object sender, EventArgs e)
        {
            resetDonorShowTS(); donorOTS.BackColor = Color.White;
            ((Button)sender).ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            if (((Button)sender).Name == "moneyTS")
            {
                cbFilter.SelectedIndex = 0;
                tabDonorDetails.SelectedIndex = 0;
                allMoneyDonation = false;
                comm = new MySqlCommand("SELECT monetaryID, paymentType, amount, ORno, checkNo, bankName, dateCheck, dateDonated, donationID FROM monetary WHERE donationID in (SELECT donation.donationID FROM donation INNER JOIN donor ON donation.donorID = donor.donorID WHERE donor.donorID = " + current_donorID + ")", conn);
                loadTable(comm, 1);
            }   
            else if (((Button)sender).Name == "ikTS")
            {
                tabDonorDetails.SelectedIndex = 1;
                loadIK(current_donorID);
            }
            else
            {
                tabDonorDetails.SelectedIndex = 2;
                ((Button)sender).ForeColor = Color.White;
                ((Button)sender).BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            }           
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedItem.ToString() == "Both")
            {
                if (((ComboBox)sender).Name == "cbFilter") allMoneyDonation = false;
                else allMoneyDonation = true;
                comm = new MySqlCommand("SELECT monetaryID, paymentType, amount, ORno, checkNo, bankName, dateCheck, dateDonated, donationID FROM monetary WHERE donationID in (SELECT donation.donationID FROM donation INNER JOIN donor ON donation.donorID = donor.donorID WHERE donor.donorID = " + current_donorID + ")", conn);
                loadTable(comm, 1);
            }
            else if (((ComboBox)sender).SelectedItem.ToString() == "Cash")
            {
                if (((ComboBox)sender).Name == "cbFilter") allMoneyDonation = false;
                else allMoneyDonation = true;
                comm = new MySqlCommand("SELECT monetaryID, paymentType, amount, ORno, checkNo, bankName, dateCheck, dateDonated, donationID FROM monetary WHERE donationID in (SELECT donation.donationID FROM donation INNER JOIN donor ON donation.donorID = donor.donorID WHERE donor.donorID = " + current_donorID + ") AND paymentType = 'Cash'", conn);
                loadTable(comm, 1);
            }
            else if (((ComboBox)sender).SelectedItem.ToString() == "Check")
            {
                if (((ComboBox)sender).Name == "cbFilter") allMoneyDonation = false;
                else allMoneyDonation = true;
                comm = new MySqlCommand("SELECT monetaryID, paymentType, amount, ORno, checkNo, bankName, dateCheck, dateDonated, donationID FROM monetary WHERE donationID in (SELECT donation.donationID FROM donation INNER JOIN donor ON donation.donorID = donor.donorID WHERE donor.donorID = " + current_donorID + ") AND paymentType = 'Check'", conn);
                loadTable(comm, 1);
            }
        }
        #endregion

        #region Search
        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == "search here") ((TextBox)sender).Text = "";
            if (!searchDate.Checked) searchOthers.Checked = true;
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == "") ((TextBox)sender).Text = "search here";
            searchOthers.Checked = searchDate.Checked = false;                   // Uncheck search options
        }

        private void txtSearchMoney_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cbFilter.SelectedIndex = 0;
                if (searchDate.Checked)
                {
                    allMoneyDonation = false;
                    var mon = new[] { "MM dd", "MMM dd", "MMMM dd" };
                    var mononly = new[] { "MM", "MMM", "MMMM" };
                    var monyr = new[] { "MM yyyy", "MMM yyyy", "MMMM yyyy" };
                    var yronly = new[] { "yyyy" };
                    var formats = new[] { "dd/MM/yyyy", "MM/dd/yyyy", "yyyy-dd-MM", "yyyy-MM-dd", "MMM dd yyyy", "MM dd yyyy", "MM dd, yyyy", "MMMM dd, yyyy",
                                            "MM dd", "MMM dd", "MMMM dd", "MM", "MMM", "MMMM", "MM yyyy", "MMM yyyy", "MMMM yyyy", "yyyy" };

                    if (DateTime.TryParseExact(txtSearchMoney.Text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue))
                    {
                        if (DateTime.TryParseExact(txtSearchMoney.Text, monyr, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue)) searchMonthYr = true;
                        else if (DateTime.TryParseExact(txtSearchMoney.Text, mon, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue)) searchMonthDay = true;
                        else if (DateTime.TryParseExact(txtSearchMoney.Text, yronly, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue)) searchYr = true;
                        else if (DateTime.TryParseExact(txtSearchMoney.Text, mononly, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue)) searchMonth = true;
                        searchDateBool = true;
                        searchMonetary(current_donorID, null);
                    }
                    else searchMonetary(current_donorID, "error");
                }
                else searchMonetary(current_donorID, txtSearchMoney.Text);
            }
        }

        private void txtMDSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    var mon = new[] { "MM dd", "MMM dd", "MMMM dd" };
                    var mononly = new[] { "MM", "MMM", "MMMM" }; var monyr = new[] { "MM yyyy", "MMM yyyy", "MMMM yyyy" }; var yronly = new[] { "yyyy" };
                    var formats = new[] { "MM", "MMM", "MMMM", "MM yyyy", "MMM yyyy", "MMMM yyyy", "yyyy" };
                    allMoneyDonation = true;

                    if (DateTime.TryParseExact(txtMDSearch.Text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue))
                    {
                        if (DateTime.TryParseExact(txtMDSearch.Text, mon, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue)) searchMonthDay = true;
                        else if (DateTime.TryParseExact(txtMDSearch.Text, monyr, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue)) searchMonthYr = true;
                        else if (DateTime.TryParseExact(txtMDSearch.Text, yronly, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue)) searchYr = true;
                        else if (DateTime.TryParseExact(txtMDSearch.Text, mononly, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue)) searchMonth = true;
                        searchDateBool = true;
                    }
                    else searchDateBool = false;
                    searchMonetary(-10, txtMDSearch.Text);
                }
                catch (Exception ex)
                {
                    errorMessage(ex.Message);
                }
            }
        }

        private void txtExpSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    var mon = new[] { "MM dd", "MMM dd", "MMMM dd" };
                    var mononly = new[] { "MM", "MMM", "MMMM" }; var monyr = new[] { "MM yyyy", "MMM yyyy", "MMMM yyyy" }; var yronly = new[] { "yyyy" };
                    var formats = new[] { "MM", "MMM", "MMMM", "MM yyyy", "MMM yyyy", "MMMM yyyy", "yyyy" };
                    allMoneyDonation = true;

                    if (DateTime.TryParseExact(txtExpSearch.Text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue))
                    {
                        if (DateTime.TryParseExact(txtExpSearch.Text, mon, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue)) searchMonthDay = true;
                        else if (DateTime.TryParseExact(txtExpSearch.Text, monyr, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue)) searchMonthYr = true;
                        else if (DateTime.TryParseExact(txtExpSearch.Text, yronly, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue)) searchYr = true;
                        else if (DateTime.TryParseExact(txtExpSearch.Text, mononly, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue)) searchMonth = true;
                        searchDateBool = true;
                    }
                    else searchDateBool = false;
                    searchMonetary(-20, txtExpSearch.Text);
                }
                catch (Exception ex)
                {
                    errorMessage(ex.Message);
                }
            }
        }
        #endregion

        #region Monetary Donation
        private void btnAddMoneyD_Click(object sender, EventArgs e)                 // Add New Donation
        {
            moneyDonate mD = overlay();
            mD.donorID = current_donorID;
            mD.hasExpense = true;
            mD.tabSelection.SelectedIndex = 0;
            mD.ShowDialog();
            comm = new MySqlCommand("SELECT monetaryID, paymentType, amount, ORno, checkNo, bankName, dateCheck, dateDonated, donationID FROM monetary WHERE donationID in (SELECT donation.donationID FROM donation INNER JOIN donor ON donation.donorID = donor.donorID WHERE donor.donorID = " + current_donorID + ")", conn);
            loadTable(comm, 1);
        }

        private void multiSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (multiSelect.Checked)
            {
                btnEditMoneyD.Enabled = false;
                donationMoney.MultiSelect = true;
            }
            else
            {
                btnEditMoneyD.Enabled = true;
                donationMoney.MultiSelect = false;
            }
        }

        private void btnEditMoneyD_Click(object sender, EventArgs e)
        {
            moneyDonate mD = overlay();

            int row = donationMoney.CurrentCell.RowIndex;

            if (donationMoney.Rows[row].Cells[4].Value.ToString() != null || donationMoney.Rows[row].Cells[1].Value.ToString() != null)
            {
                if (donationMoney.Rows[row].Cells[1].Value.ToString() == "Cash")            // Cash Donation Edit
                {
                    string[] parts = donationMoney.Rows[row].Cells[2].Value.ToString().Split('.');
                    DateTime dateDonate = Convert.ToDateTime(donationMoney.Rows[row].Cells[7].Value.ToString());

                    mD.tabSelection.SelectedIndex = 3;              // Tab for Edit Check selected

                    mD.donationID = int.Parse(donationMoney.Rows[row].Cells[8].Value.ToString());
                    mD.txtCashAmount2.Text = parts[0]; mD.txtCashCent2.Text = parts[1];
                    mD.txtOR2.Text = donationMoney.Rows[row].Cells[3].Value.ToString();
                    // Dates                   
                    mD.dateCash2.MaxDate = DateTime.Now; mD.dateCash2.Value = dateDonate;
                }
                else if (donationMoney.Rows[row].Cells[1].Value.ToString() == "Check")      // Check Donation Edit
                {
                    string[] parts = donationMoney.Rows[row].Cells[2].Value.ToString().Split('.');
                    DateTime dateCheck = Convert.ToDateTime(donationMoney.Rows[row].Cells[6].Value.ToString());
                    DateTime dateDonate = Convert.ToDateTime(donationMoney.Rows[row].Cells[7].Value.ToString());

                    mD.tabSelection.SelectedIndex = 4;              // Tab for Edit Check selected

                    mD.donationID = int.Parse(donationMoney.Rows[row].Cells[8].Value.ToString());
                    mD.txtCheckAmount2.Text = parts[0]; mD.txtCheckCent2.Text = parts[1];
                    mD.txtBank2.Text = donationMoney.Rows[row].Cells[5].Value.ToString();
                    // Dates
                    mD.txtCheckOR2.Text = donationMoney.Rows[row].Cells[3].Value.ToString(); mD.txtCheckNo2.Text = donationMoney.Rows[row].Cells[4].Value.ToString();
                    mD.dateOfCheck2.MaxDate = mD.dateCheck2.MaxDate = DateTime.Now;
                    mD.dateOfCheck2.Value = dateCheck; mD.dateCheck2.Value = dateDonate;
                }
                mD.ShowDialog();
                comm = new MySqlCommand("SELECT monetaryID, paymentType, amount, ORno, checkNo, bankName, dateCheck, dateDonated, donationID FROM monetary WHERE donationID in (SELECT donation.donationID FROM donation INNER JOIN donor ON donation.donorID = donor.donorID WHERE donor.donorID = " + current_donorID + ")", conn);
                loadTable(comm, 1);
            }
        }

        private void btnDelMoneyD_Click(object sender, EventArgs e)
        {            
            confirmMessage("Are you sure you want to delete this donation? You cannot undo this action.");
            if (multiSelect.Checked && confirmed)
            {
                foreach (DataGridViewRow r in donationMoney.SelectedRows)
                {
                    int row = donationMoney.CurrentCell.RowIndex;
                    delDonation(int.Parse(donationMoney.Rows[row].Cells[8].Value.ToString()), 1);
                    comm = new MySqlCommand("SELECT monetaryID, paymentType, amount, ORno, checkNo, bankName, dateCheck, dateDonated, donationID FROM monetary WHERE donationID in (SELECT donation.donationID FROM donation INNER JOIN donor ON donation.donorID = donor.donorID WHERE donor.donorID = " + current_donorID + ")", conn);
                    loadTable(comm, 1);
                }
            }
            else if (!multiSelect.Checked && confirmed)
            {
                int row = donationMoney.CurrentCell.RowIndex;
                delDonation(int.Parse(donationMoney.Rows[row].Cells[8].Value.ToString()), 1);
                comm = new MySqlCommand("SELECT monetaryID, paymentType, amount, ORno, checkNo, bankName, dateCheck, dateDonated, donationID FROM monetary WHERE donationID in (SELECT donation.donationID FROM donation INNER JOIN donor ON donation.donorID = donor.donorID WHERE donor.donorID = " + current_donorID + ")", conn);
                loadTable(comm, 1);
            }
            multiSelect.Checked = false;
        }

        private void cbFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbFilter_DropDownClosed(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => { ((ComboBox)sender).Select(0, 0); }));
        }
        
        private void donationMoney_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {            
            int row = donationMoney.CurrentCell.RowIndex;        
            if (donationMoney.Rows[row].Cells[4].Value.ToString() != null || donationMoney.Rows[row].Cells[1].Value.ToString() != null)
            {                
                if (donationMoney.Rows[row].Cells[1].Value.ToString() == "Check")      // Check Donation Info
                {
                    moneyDonate mD = overlay();
                    mD.tabSelection.SelectedIndex = 11;                                 
                    mD.txtBankInfo.Text = donationMoney.Rows[row].Cells[5].Value.ToString();                    
                    mD.txtCheckInfo.Text = donationMoney.Rows[row].Cells[3].Value.ToString();
                    mD.txtCheckNo2.Text = donationMoney.Rows[row].Cells[4].Value.ToString();                    
                    mD.txtDateCheckInfo.Text = DateTime.Parse(donationMoney.Rows[row].Cells[6].Value.ToString()).ToString("MMMM dd, yyyy");
                    mD.txtDonateCheckInfo.Text = DateTime.Parse(donationMoney.Rows[row].Cells[7].Value.ToString()).ToString("MMMM dd, yyyy");
                    mD.ShowDialog();
                }                
            }
        }
        
        private void allMD_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {          
            int row = allMD.CurrentCell.RowIndex;
            if (allMD.Rows[row].Cells[1].Value.ToString() != "No entries.")
            {
                moneyDonate mD = overlay();
                mD.tabSelection.SelectedIndex = 12;
                
                comm = new MySqlCommand("SELECT * FROM donor WHERE donorID in (SELECT donation.donorID FROM donation INNER JOIN donor ON donation.donorID = donor.donorID WHERE donationID = " + int.Parse(allMD.Rows[row].Cells[8].Value.ToString()) + ")", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm); DataTable dt = new DataTable(); adp.Fill(dt);
                mD.donorID = current_donorID = int.Parse(dt.Rows[0]["donorID"].ToString());
                mD.txtAllDName.Text = dt.Rows[0]["donorName"].ToString();
                
                if (allMD.Rows[row].Cells[1].Value.ToString() == "Check")
                {
                    
                    mD.txtAllCheckNo.Text = allMD.Rows[row].Cells[4].Value.ToString();
                    mD.txtAllBank.Text = allMD.Rows[row].Cells[5].Value.ToString();
                    mD.txtAllDateCheck.Text = DateTime.Parse(allMD.Rows[row].Cells[6].Value.ToString()).ToString("MMMM dd, yyyy");                    
                }
                else
                {
                    mD.txtAllCheckNo.Text = allMD.Rows[row].Cells[4].Value.ToString();
                    mD.txtAllBank.Text = "---";
                    mD.txtAllDateCheck.Text = "---";
                }

                mD.txtAllORNO.Text = allMD.Rows[row].Cells[3].Value.ToString();
                mD.txtAllDateDonate.Text = DateTime.Parse(allMD.Rows[row].Cells[7].Value.ToString()).ToString("MMMM dd, yyyy");

                if (mD.ShowDialog() == DialogResult.OK)      // VIEW DONOR
                {
                    cbFilter.SelectedIndex = 0;
                    loadDonorInfo(current_donorID);

                    if (dt.Rows[0]["type"].ToString() == "1") txtDType.Text = "Individual";
                    else txtDType.Text = "Organization";

                    txtDPhone.Text = dt.Rows[0]["telephone"].ToString();
                    txtDMobile.Text = dt.Rows[0]["mobile"].ToString();
                    txtDEmail.Text = dt.Rows[0]["email"].ToString();
                    txtDPledge.Text = dt.Rows[0]["pledge"].ToString();

                    backMoneyDonation = true;
                    comm = new MySqlCommand("SELECT monetaryID, paymentType, amount, ORno, checkNo, bankName, dateCheck, dateDonated, donationID FROM monetary WHERE donationID in (SELECT donation.donationID FROM donation INNER JOIN donor ON donation.donorID = donor.donorID WHERE donor.donorID = " + current_donorID + ")", conn);
                    loadTable(comm, 1);
                }                
            }
        }
        #endregion

        #region In Kind Donation
        private void btnAddIK_Click(object sender, EventArgs e)
        {
            moneyDonate mD = overlay();
            mD.donorID = current_donorID;
            mD.tabSelection.SelectedIndex = 5;
            mD.ShowDialog();
            loadIK(current_donorID);
        }

        private void btnEditIK_Click(object sender, EventArgs e)
        {
            moneyDonate mD = overlay();
            int row = donationIK.CurrentCell.RowIndex;
            if (donationIK.Rows[row].Cells[1].Value.ToString() != null || donationIK.Rows[row].Cells[2].Value.ToString() != null)
            {
                DateTime dateDonate = Convert.ToDateTime(donationIK.Rows[row].Cells[3].Value.ToString());

                mD.tabSelection.SelectedIndex = 6;              // Tab for Edit In-kind Donation selected

                mD.donationID = int.Parse(donationIK.Rows[row].Cells[4].Value.ToString());
                mD.txtPart2.Text = donationIK.Rows[row].Cells[1].Value.ToString();
                mD.txtQuantity2.Text = donationIK.Rows[row].Cells[2].Value.ToString();
                mD.dateIK2.MaxDate = DateTime.Now; mD.dateIK2.Value = dateDonate;

                mD.ShowDialog();
                loadIK(current_donorID);
            }
        }

        private void btnDelIK_Click(object sender, EventArgs e)
        {
            confirmMessage("Are you sure you want to delete this donation? You cannot undo this action.");
            if (confirmed && multiSelect2.Checked)
            {
                foreach (DataGridViewRow r in donationIK.SelectedRows)
                {
                    int row = donationMoney.CurrentCell.RowIndex;
                    delDonation(int.Parse(donationIK.Rows[row].Cells[4].Value.ToString()), 2);
                    loadIK(current_donorID);
                }
            }
            else if (confirmed && !multiSelect2.Checked)
            {
                int row = donationMoney.CurrentCell.RowIndex;
                delDonation(int.Parse(donationIK.Rows[row].Cells[4].Value.ToString()), 2);
                loadIK(current_donorID);
            }
            multiSelect2.Checked = false;
        }

        private void multiSelect2_CheckedChanged(object sender, EventArgs e)
        {
            if (multiSelect2.Checked)
            {
                btnEditIK.Enabled = false;
                donationIK.MultiSelect = true;
            }
            else
            {
                btnEditIK.Enabled = true;
                donationIK.MultiSelect = false;
            }
        }
        #endregion

        #region Finance
        private void btnViewBR_Click(object sender, EventArgs e)
        {
            lblBRHeader.Text = "Pending Budget Requests"; aBR = false;
            panelPBR.BackColor = System.Drawing.Color.FromArgb(62, 153, 141);
            lblBudgetTotal.Text = "Budget Total (PHP)"; lblOn.Text = "on";
           
            tabSelection.SelectedTab = tabBRList;
            MySqlCommand comm = new MySqlCommand("SELECT * FROM budget WHERE status = 'Pending' ORDER BY budgetID ASC", conn);
            loadTable(comm, 4);
            tabPBR.SelectedIndex = 0;
        }

        private void btnViewEx_Click(object sender, EventArgs e)
        {
            panelExpOp.Visible = false;
            tabSelection.SelectedTab = tabEx;
            MySqlCommand comm = new MySqlCommand("SELECT * FROM expense ORDER BY dateExpense DESC", conn);
            loadTable(comm, 6); cbAll.Checked = true;
        }

        private void btnViewMD_Click(object sender, EventArgs e)
        {
            allMoneyDonation = btnDonateAllBack.Visible = true; 
            tabSelection.SelectedTab = tabDonors;
            tabInnerDonors.SelectedTab = tabDonorsAll;
            MySqlCommand comm = new MySqlCommand("SELECT monetaryID, paymentType, amount, ORno, checkNo, bankName, dateCheck, dateDonated, donationID FROM monetary ORDER BY donationID DESC", conn);
            loadTable(comm, 1);
        }
        #endregion

        #region New Budget Request
        private void btnNewBR_Click(object sender, EventArgs e)
        {
            btnMain.Enabled = btnFinance.Enabled = btnDonation.Enabled = false;
            logo_main.Enabled = logo_finance.Enabled = logo_donation.Enabled = false;
            resetBudgetRequest(); brEdit = false;

            txtPurpose.Text = "Name of purpose.";
            txtBRRequest.Text = "Name.";            
            lblOthers.Visible = cbExpCat.Visible = false; cbExpCat.SelectedIndex = 0;
            rbClothing.Checked = rbFood.Checked = rbHouse.Checked = rbMeds.Checked = rbOffice.Checked = rbSchool.Checked = rbSkills.Checked = rbSocial.Checked = rbSpiritual.Checked = rbTranspo.Checked = rbOthers.Checked = false;
            dateBR.MaxDate = dateBR.Value = DateTime.Today;

            get(1);
            tabSelection.SelectedTab = tabBudgetRequest;
            tabBR.SelectedIndex = 0;           
        }

        private void btnAddBR_Click(object sender, EventArgs e)
        {           
            moneyDonate mD = overlay();
            mD.budgetID = current_budgetID;
            if (lblBRCategory.Text == "Multiple")
            {                
                mD.tabSelection.SelectedIndex = 13;
                mD.cbBRC_Cat.SelectedIndex = 0;
                mD.ShowDialog();
            }
            else
            {
                mD.category = lblBRCategory.Text;             
                mD.tabSelection.SelectedIndex = 7;
                mD.ShowDialog();
            }
            MySqlCommand comm = new MySqlCommand("SELECT * FROM item WHERE budgetID = " + current_budgetID, conn);
            loadTable(comm, 3);
        }

        private void btnEditBR_Click(object sender, EventArgs e)
        {
            int row = BRDetails.CurrentCell.RowIndex;
            moneyDonate mD = overlay();
            mD.tabSelection.SelectedIndex = 10;
            mD.txtBRPart2.Text = BRDetails.Rows[row].Cells[1].Value.ToString();
            mD.txtBRQuantity2.Value = Decimal.Parse(BRDetails.Rows[row].Cells[2].Value.ToString());
            mD.txtBRUP2.Text = BRDetails.Rows[row].Cells[3].Value.ToString();
            mD.txtBRTotal2.Text = BRDetails.Rows[row].Cells[4].Value.ToString();
            mD.ShowDialog();
        }

        private void btnDelBR_Click(object sender, EventArgs e)
        {
            int row = BRDetails.CurrentCell.RowIndex;

            if (BRDetails.Rows[row].Cells[1].Value.ToString() != "No entries.")
            {
                confirmMessage("Are you sure you want to remove this item?");
                if (confirmed)
                {
                    del(current_item, 2);
                    MySqlCommand comm = new MySqlCommand("SELECT * FROM item WHERE budgetID = " + current_budgetID, conn);
                    loadTable(comm, 3);
                }
            }
        }

        private void btnBRConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlCommand comm = new MySqlCommand("SELECT SUM(amount) as SUM FROM item WHERE budgetID = " + current_budgetID, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm); DataTable dt = new DataTable(); adp.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    if (dt.Rows[0]["SUM"].ToString() != "")
                    {
                        confirmMessage("Are you sure you want to request this budget?");
                        if (confirmed)
                        {
                            conn.Open();

                            comm = new MySqlCommand("SELECT SUM(amount) as SUM FROM item WHERE budgetID = " + current_budgetID, conn);
                            adp = new MySqlDataAdapter(comm); dt = new DataTable(); adp.Fill(dt);

                            decimal sum = Decimal.Round(decimal.Parse(dt.Rows[0]["SUM"].ToString()), 2);

                            comm = new MySqlCommand("UPDATE budget SET purpose = '" + lblBRPurpose.Text + "', category = '" + lblBRCategory.Text
                                + "', budgetTotal = " + sum + ", dateRequested = '" + dateBR.Value.ToString("yyyy-MM-dd") + "', requestedBy = '" + txtBRRequest.Text + "' WHERE budgetID = " + current_budgetID, conn);
                            comm.ExecuteNonQuery();

                            conn.Close();
                            btnMain.Enabled = btnFinance.Enabled = btnDonation.Enabled = true;
                            logo_main.Enabled = logo_finance.Enabled = logo_donation.Enabled = true;
                            get(2); get(4);
                            tabSelection.SelectedTab = tabFinance;
                        }
                    }
                    else errorMessage("Please do not submit empty details.");
                }
                else errorMessage("Please do not submit empty details.");
            }
            catch (Exception ex)
            {
                errorMessage(ex.Message);
            }
        }

        private void BRDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && !empty) current_item = int.Parse(((DataGridView)sender).Rows[e.RowIndex].Cells[0].FormattedValue.ToString());
        }

        private void btnBRNext_Click(object sender, EventArgs e)
        {
            if (lblBRCategory.Text != "" && txtBRRequest.Text != "Name." && txtPurpose.Text != "Name of purpose.")
            {
                if (panelCat.Visible)
                {                    
                    MySqlCommand comm = new MySqlCommand("SELECT * FROM item WHERE budgetID = " + current_budgetID, conn);
                    loadTable(comm, 5);
                }
                else                // MULTIPLE
                {
                    
                }
                tabBR.SelectedIndex = 1;
                lblBRdateRequested.Text = dateBR.Value.ToString("MMMM dd, yyyy");
                lblBRPurpose.Text = txtPurpose.Text;
                lblBRBy.Text = txtBRRequest.Text;

                brparTS.Font = new System.Drawing.Font("Segoe UI", 20.25F);
                brTS.Font = new System.Drawing.Font("Segoe UI Semilight", 20.25F);
                brTS.ForeColor = System.Drawing.Color.FromArgb(197, 217, 208);

                if (!brEdit) brparTS.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                else brparTS.ForeColor = System.Drawing.Color.FromArgb(219, 209, 92);

                String dateRequested = dateBR.Value.Year.ToString() + "-" + dateBR.Value.Month.ToString() + "-" + dateBR.Value.Day.ToString();
            }
            else errorMessage("Please fill up all fields or choose a category.");                       
        }

        private void btnBRCancel_Click(object sender, EventArgs e)
        {
            if (((Button)sender).Name == "btnBRCancel") confirmMessage("Are you sure you want to cancel this request?");
            else if (((Button)sender).Name == "btnBRBack")
            {
                tabBR.SelectedIndex = 0; confirmed = false;
                brTS.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                brTS.Font = new System.Drawing.Font("Segoe UI", 20.25F);
                brparTS.ForeColor = System.Drawing.Color.FromArgb(197, 217, 208);
                brparTS.Font = new System.Drawing.Font("Segoe UI Semilight", 20.25F);
            }
            if (confirmed)
            {
                try
                {
                    conn.Open();

                    MySqlCommand comm = new MySqlCommand("DELETE FROM budget WHERE budgetID = " + current_budgetID, conn);
                    comm.ExecuteNonQuery();

                    comm = new MySqlCommand("ALTER TABLE budget AUTO_INCREMENT = " + current_budgetID, conn);
                    comm.ExecuteNonQuery();

                    conn.Close();
                }
                catch (Exception ex)
                {
                    errorMessage(ex.Message);
                }
                if (((Button)sender).Name == "btnBRCancel")
                {
                    if (!brEdit)
                    {
                        txtPurpose.Text = "Name of purpose.";
                        txtBRRequest.Text = "Name.";
                        dateBR.MaxDate = dateBR.Value = DateTime.Today;
                        lblOthers.Visible = cbExpCat.Visible = false; cbExpCat.SelectedIndex = 0;
                        rbClothing.Checked = rbFood.Checked = rbHouse.Checked = rbMeds.Checked = rbOffice.Checked = rbSchool.Checked = rbSkills.Checked = rbSocial.Checked = rbSpiritual.Checked = rbTranspo.Checked = rbOthers.Checked = false;

                        tabSelection.SelectedTab = tabFinance;
                    }
                    else
                    {
                        tabSelection.SelectedTab = tabBRList;
                        tabPBR.SelectedIndex = 1;
                    }
                }
                btnMain.Enabled = btnFinance.Enabled = btnDonation.Enabled = true;
                logo_main.Enabled = logo_finance.Enabled = logo_donation.Enabled = true;
            }                                  
        }

        private void catType_Click(object sender, EventArgs e)
        {
            btnCatSingle.BackColor = btnCatMultiple.BackColor = Color.White;
            ((Button)sender).BackColor = System.Drawing.Color.FromArgb(146, 211, 202);
            if (((Button)sender).Name == "btnCatSingle")
            {
                lblCategory.Text = "";
                panelCat.Visible = lblCategory.Visible = true;
                if (rbOthers.Checked) lblBRCategory.Text = cbExpCat.SelectedItem.ToString();
            }
            else
            {
                lblBRCategory.Text = "Multiple";
                panelCat.Visible = lblCategory.Visible = false;
            }
        }

        private void category_CheckedChanged(object sender, EventArgs e)
        {
            lblOthers.Visible = cbExpCat.Visible = false;
            if (rbClothing.Checked) lblBRCategory.Text = "Clothing";
            else if (rbFood.Checked) lblBRCategory.Text = "Food";
            else if (rbHouse.Checked) lblBRCategory.Text = "Repair and Maintenance";
            else if (rbMeds.Checked) lblBRCategory.Text = "Medical Supplies";
            else if (rbOffice.Checked) lblBRCategory.Text = "Office Supplies";
            else if (rbSchool.Checked) lblBRCategory.Text = "Education";
            else if (rbSkills.Checked) lblBRCategory.Text = "Skills and Development";
            else if (rbSocial.Checked) lblBRCategory.Text = "Recreation";
            else if (rbSpiritual.Checked) lblBRCategory.Text = "Spiritual Value Formation";
            else if (rbTranspo.Checked) lblBRCategory.Text = "Transportation";
            else if (((RadioButton)sender).Name == "rbOthers")
            {
                cbExpCat.SelectedIndex = 0;
                if (rbOthers.Checked) lblOthers.Visible = cbExpCat.Visible = true;                
            }
        }
        #endregion

        #region Budget Request List
        private void BRList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && !empty)
            {
                expList.Size = new System.Drawing.Size(935, 324); panelExpOp.Visible = true;
                current_budgetID = int.Parse(BRList.Rows[e.RowIndex].Cells[0].FormattedValue.ToString());
                lblPBRPurpose.Text = BRList.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
                lblPBRCategory.Text = BRList.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
                lblPBRBy.Text = BRList.Rows[e.RowIndex].Cells[6].FormattedValue.ToString();
                lblPBRdate.Text = BRList.Rows[e.RowIndex].Cells[5].FormattedValue.ToString();
                lblPBRTotal.Text = BRList.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();

                lblBRHeader.Text = "Pending Budget Requests"; aBR = false;
                panelPBR.BackColor = System.Drawing.Color.FromArgb(62, 153, 141);
                lblBudgetTotal.Text = "Budget Total (PHP)"; lblOn.Text = "on";

                MySqlCommand comm = new MySqlCommand("SELECT * FROM item WHERE budgetID = " + current_budgetID, conn);
                loadTable(comm, 5);

                PBRDetails.Size = new System.Drawing.Size(935, 324);
                panelOptions.Visible = true;

                tabPBR.SelectedIndex = 1;
            }
        }

        private void btnPBRBack_Click(object sender, EventArgs e)
        {
            if (tabPBR.SelectedIndex == 1 && lblBRHeader.Text == "Pending Budget Requests") tabPBR.SelectedIndex = 0;
            else if (lblBRHeader.Text == "Pending Budget Requests" || lblBRHeader.Text == "Approved Budget Requests")
            {                
                lblBRHeader.Text = "Pending Budget Requests"; aBR = false;
                panelPBR.BackColor = System.Drawing.Color.FromArgb(62, 153, 141);
                lblBudgetTotal.Text = "Budget Total (PHP)"; lblOn.Text = "on";
                tabSelection.SelectedTab = tabFinance;
            }
            else tabSelection.SelectedTab = tabEx;
        }

        private void btnBRApprove_Click(object sender, EventArgs e)
        {
            confirmMessage("Are you sure you want to approve this budget?");
            if (confirmed)
            {
                try
                {
                    conn.Open();
                    MySqlCommand comm = new MySqlCommand("UPDATE budget SET status = 'Approved' WHERE budgetID = " + current_budgetID, conn);
                    comm.ExecuteNonQuery();
                    conn.Close();

                    successMessage("Budget has been approved successfully!");

                    tabSelection.SelectedTab = tabBRList;
                    comm = new MySqlCommand("SELECT * FROM budget WHERE status = 'Pending' ORDER BY budgetID ASC", conn);
                    loadTable(comm, 4);
                    tabPBR.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    errorMessage(ex.Message);
                }
            }
        }

        private void btnBRDisapprove_Click(object sender, EventArgs e)
        {
            confirmMessage("Are you sure you want to disapprove this request?");
            if (confirmed)
            {
                moneyDonate mD = overlay();
                mD.Size = new System.Drawing.Size(510, 235);
                mD.tabSelection.SelectedIndex = 9;
                DialogResult choice = mD.ShowDialog();
                if (choice == DialogResult.Yes)            // DELETE BUDGET REQUEST
                {
                    try
                    {
                        del(current_budgetID, 4);
                        successMessage("Budget has been removed successfully.");
                        tabSelection.SelectedTab = tabBRList;
                        comm = new MySqlCommand("SELECT * FROM budget WHERE status = 'Pending' ORDER BY budgetID ASC", conn);
                        loadTable(comm, 4);
                        tabPBR.SelectedIndex = 0;
                    }
                    catch (Exception ex)
                    {
                        errorMessage(ex.Message);
                    }
                }
                else if (choice == DialogResult.No)         // EDIT BUDGET REQUEST
                {
                    btnMain.Enabled = btnFinance.Enabled = btnDonation.Enabled = false;
                    logo_main.Enabled = logo_finance.Enabled = logo_donation.Enabled = false;
                    tabSelection.SelectedTab = tabBudgetRequest;
                    tabBR.SelectedIndex = 0;
                    
                    txtPurpose.Text = lblPBRPurpose.Text;
                    txtBRRequest.Text = lblPBRBy.Text;

                    dateBR.MaxDate = DateTime.Today;
                    var formats = new[] { "MMMM dd, yyyy" };
                    if (DateTime.TryParseExact(txtSearchMoney.Text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue)) dateBR.Value = fromDateValue;
                    else dateBR.Value = dateBR.MaxDate;
                   
                    if (lblPBRCategory.Text == "Clothing") rbClothing.Checked = true;
                    else if (lblPBRCategory.Text == "Food") rbFood.Checked = true;
                    else if (lblPBRCategory.Text == "Repair and Maintenance") rbHouse.Checked = true;
                    else if (lblPBRCategory.Text == "Medical Supplies") rbMeds.Checked = true;
                    else if (lblPBRCategory.Text == "Office Supplies") rbOffice.Checked = true;
                    else if (lblPBRCategory.Text == "Education") rbSchool.Checked = true;
                    else if (lblPBRCategory.Text == "Skills and Development") rbSkills.Checked = true;
                    else if (lblPBRCategory.Text == "Recreation") rbSocial.Checked = true;
                    else if (lblPBRCategory.Text == "Spiritual Value Formation") rbSpiritual.Checked = true;
                    else if (lblPBRCategory.Text == "Transportation") rbTranspo.Checked = true;
                    else
                    {
                        rbOthers.Checked = true;
                        for(int i = 0; i < cbExpCat.Items.Count; i++)
                        {
                            cbExpCat.SelectedIndex = i;
                            if (cbExpCat.SelectedItem.ToString() == lblPBRCategory.Text) break;
                        }
                    }
                    comm = new MySqlCommand("SELECT * FROM item WHERE budgetID = " + current_budgetID, conn);
                    loadTable(comm, 3);

                    brTS.ForeColor = btnBRNext.BackColor = btnBRConfirm.BackColor = System.Drawing.Color.FromArgb(219, 209, 92);
                    btnBRNext.ForeColor = btnBRConfirm.ForeColor = System.Drawing.Color.FromArgb(45, 45, 45);
                    brEdit = true;
                }
            }
        }

        private void btnApprovedBR_Click(object sender, EventArgs e)
        {
            lblBRHeader.Text = "Approved Budget Requests";
            tabSelection.SelectedTab = tabBRList; aBR = true;
            MySqlCommand comm = new MySqlCommand("SELECT * FROM budget WHERE status = 'Approved' ORDER BY budgetID ASC", conn);
            loadTable(comm, 4);
            tabPBR.SelectedIndex = 2;
        }

        private void cbExpCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblBRCategory.Text = cbExpCat.SelectedItem.ToString();
        }

        private void btnCancelABR_Click(object sender, EventArgs e)
        {
            confirmMessage("Are you sure you want to cancel this approved budget request?\n\nIt will be deleted permanently.");
            if (confirmed)
            {
                if (!multiABR.Checked)
                {
                    int row = approvedBRList.CurrentCell.RowIndex;
                    del(int.Parse(approvedBRList.Rows[row].Cells[0].Value.ToString()), 4);
                    successMessage("Selected request has been deleted successfullly.");                                        
                }
                else
                {
                    foreach (DataGridViewRow r in approvedBRList.SelectedRows)
                    {
                        int row = approvedBRList.CurrentCell.RowIndex;
                        del(int.Parse(approvedBRList.Rows[row].Cells[0].Value.ToString()), 4);
                        comm = new MySqlCommand("SELECT * FROM budget WHERE status = 'Approved' ORDER BY budgetID ASC", conn);
                        loadTable(comm, 4);
                    }
                    multiABR.Checked = false;
                    successMessage("Selected requests have been deleted successfullly.");                    
                }
            }
            comm = new MySqlCommand("SELECT * FROM budget WHERE status = 'Approved' ORDER BY budgetID ASC", conn);
            loadTable(comm, 4);
        }
        #endregion

        #region Expense
        private void btnExpLoad_Click(object sender, EventArgs e)
        {
            panelExpOp.Visible = false;
            int i = 0; MySqlCommand comm = new MySqlCommand("SELECT * FROM expense ORDER BY dateExpense DESC", conn);
            string[] load = new string[50];

            if (cbClothing.Checked) load[i++] = "Clothing"; if (cbCLW.Checked) load[i++] = "Communications, Lights, and Water"; if (cbDep.Checked) load[i++] = "Depreciation Expenses"; if (cbEdu.Checked) load[i++] = "Education";
            if (cbFood.Checked) load[i++] = "Food"; if (cbGC.Checked) load[i++] = "Guidance and Counselling"; if (cbHonor.Checked) load[i++] = "Honorarium"; if (cbHouse.Checked) load[i++] = "Household Expenses"; if (cbInsurance.Checked) load[i++] = "Insurance Expense";
            if (cbMed.Checked) load[i++] = "Medical and Dental Supplies"; if (cbMeeting.Checked) load[i++] = "Meeting and Coferences"; if (cbOffice.Checked) load[i++] = "Office Supplies"; if (cbPrintAd.Checked) load[i++] = "Printing and Advertising";
            if (cbProf.Checked) load[i++] = "Professional Fees"; if (cbRec.Checked) load[i++] = "Recreation"; if (cbRepair.Checked) load[i++] = "Repair and Maintenance"; if (cbSal.Checked) load[i++] = "Salary"; if (cbSD.Checked) load[i++] = "Skills and Development";
            if (cbSSS.Checked) load[i++] = "SSS, PHIC, and HMDF"; if (cbSVF.Checked) load[i++] = "Spiritual Value Formation"; if (cbTax.Checked) load[i++] = "Taxes and Licenses"; if (cbTranspo.Checked) load[i++] = "Transportation";

            if (i >= 1) cbAll.Checked = false;
            else cbAll.Checked = true;

            string final = string.Empty;
            for (int n = 0; n < i; n++)
            {
                if (load[n + 1] != null) final += "'" + load[n] + "' OR category = ";
                else
                {
                    final += "'" + load[n] + "'";
                    break;
                }
            }

            if (cbAll.Checked) cbClothing.Checked = cbCLW.Checked = cbDep.Checked = cbEdu.Checked = cbFood.Checked = cbGC.Checked = cbHonor.Checked = cbHouse.Checked = cbInsurance.Checked = cbMed.Checked = cbMeeting.Checked = cbOffice.Checked = cbPrintAd.Checked = cbProf.Checked = cbRec.Checked = cbRepair.Checked = cbSal.Checked = cbSD.Checked = cbSSS.Checked = cbSVF.Checked = cbTax.Checked = cbTranspo.Checked = false;
            else comm = new MySqlCommand("SELECT * FROM expense WHERE category = " + final + " ORDER BY dateExpense DESC", conn);
            loadTable(comm, 6);
        }        

        private void btnResetCategory_Click(object sender, EventArgs e)
        {
            panelExpOp.Visible = false;
            cbClothing.Checked = cbCLW.Checked = cbDep.Checked = cbEdu.Checked = cbFood.Checked = cbGC.Checked = cbHonor.Checked = cbHouse.Checked = cbInsurance.Checked = cbMed.Checked = cbMeeting.Checked = cbOffice.Checked = cbPrintAd.Checked = cbProf.Checked = cbRec.Checked = cbRepair.Checked = cbSal.Checked = cbSD.Checked = cbSSS.Checked = cbSVF.Checked = cbTax.Checked = cbTranspo.Checked = false;
            cbAll.Checked = true;
            MySqlCommand comm = new MySqlCommand("SELECT * FROM expense ORDER BY dateExpense DESC", conn);
            loadTable(comm, 6);
        }

        private void multiExp_CheckedChanged(object sender, EventArgs e)
        {
            panelExpOp.Visible = false;
            if (multiExp.Checked) expList.MultiSelect = true;
            else expList.MultiSelect = false;
        }

        private void multiABR_CheckedChanged(object sender, EventArgs e)
        {
            if (multiABR.Checked) approvedBRList.MultiSelect = true;
            else approvedBRList.MultiSelect = false;
        }

        private void btnBtoE_Click(object sender, EventArgs e)
        {
            try
            {
                if (multiABR.Checked)
                {
                    foreach (DataGridViewRow r in approvedBRList.SelectedRows)
                    {
                        int row = expList.CurrentCell.RowIndex;
                        del(int.Parse(expList.Rows[row].Cells[0].Value.ToString()), 3);
                        comm = new MySqlCommand("SELECT * FROM budget WHERE status = 'Pending' ORDER BY budgetID ASC", conn);
                        loadTable(comm, 4);
                    }
                }
                else
                {
                    conn.Open();
                    int row = approvedBRList.CurrentCell.RowIndex;
                    comm = new MySqlCommand("SELECT category, budgetTotal, budgetID FROM budget WHERE budgetID = " + int.Parse(approvedBRList.Rows[row].Cells[0].Value.ToString()), conn);
                    MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);

                    comm = new MySqlCommand("INSERT INTO expense (dateExpense, category, amount, budgetID) VALUES ('" + DateTime.Now.ToString("yyyy-MM-dd") + "', '" + dt.Rows[0]["category"].ToString() + "', " + decimal.Parse(dt.Rows[0]["budgetTotal"].ToString()) + ", " + int.Parse(dt.Rows[0]["budgetID"].ToString()) + ")", conn);
                    comm.ExecuteNonQuery();

                    comm = new MySqlCommand("UPDATE budget SET status = 'Expense' WHERE budgetID = " + int.Parse(dt.Rows[0]["budgetID"].ToString()), conn);
                    comm.ExecuteNonQuery();
                    conn.Close();

                    comm = new MySqlCommand("SELECT * FROM budget WHERE status = 'Pending' ORDER BY budgetID ASC", conn);
                    loadTable(comm, 4);
                }
                successMessage("Budget has been exported successfully as expense record.");
            }
            catch (Exception ex)
            {
                errorMessage(ex.Message);
            }
        }
        #endregion

        #region Expense Options
        private void btnDelExp_Click(object sender, EventArgs e)
        {
            if (expList.Rows[0].Cells[2].Value.ToString() != "No entries." && expList.SelectedRows.Count != 0)
            {
                int row = expList.CurrentCell.RowIndex;
                if (multiExp.Checked) confirmMessage("Are you sure you want to delete this expense record?\nThe existing budget will be deleted as well.");
                else confirmMessage("Are you sure you want to delete these expense records?\nThe existing budget will be deleted as well.");

                if (confirmed && !multiExp.Checked)
                {
                    del(int.Parse(expList.Rows[row].Cells[0].Value.ToString()), 3);
                    MySqlCommand comm = new MySqlCommand("SELECT * FROM expense", conn);
                    loadTable(comm, 6);
                }
                else if (confirmed && multiExp.Checked)
                {
                    foreach (DataGridViewRow r in expList.SelectedRows)
                    {
                        row = expList.CurrentCell.RowIndex;
                        del(int.Parse(expList.Rows[row].Cells[0].Value.ToString()), 3);
                        MySqlCommand comm = new MySqlCommand("SELECT * FROM expense", conn);
                        loadTable(comm, 6);
                    }
                }
            }
            panelExpOp.Visible = multiExp.Checked = false;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            moneyDonate mD = overlay();
            mD.refToExpense = this;
            mD.tabSelection.SelectedIndex = 8;

            DialogResult result = mD.ShowDialog();

            if (result == DialogResult.OK || result == DialogResult.Yes || result == DialogResult.Retry)
            {                   
                Microsoft.Office.Interop.Excel._Application excel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook workbook = excel.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                try
                {
                    conn.Open();
                    worksheet = workbook.ActiveSheet;
                    worksheet.Name = "Expenses";

                    string[] category = {"Food", "Salary", "Communication, Lights, and Water", "Household Expenses", "Repair and Maintenance", "Depreciation Expenses",
                    "SSS, PHIC, and HMDF", "Education", "Meeting and Conferences", "Transportation", "Spiritual Value Formation", "Medical and Dental Supplies",
                    "Recreation", "Clothing", "Office Supplies", "Skills and Development", "Taxes and Licenses", "Guidance and Counselling", "Professional Fees",
                    "Honorarium", "Printing and Advertising", "Insurance Expense"};

                    waiting wait = new waiting();
                    dim dim = new dim();
                    dim.Location = this.Location; dim.Size = this.Size; dim.refToPrev = this;
                    
                    int month = int.Parse(DateTime.Today.Month.ToString()); int year = DateTime.Today.Year; string monthname = DateTime.Today.Month.ToString("MMMM");
                    MySqlDataAdapter adp = new MySqlDataAdapter();
                    if (result == DialogResult.OK || result == DialogResult.Yes)
                    {
                        if (result == DialogResult.Yes)
                        {
                            month = (mD.dateFrom.SelectedIndex + 1);
                            monthname = mD.dateFrom.GetItemText(mD.dateFrom.SelectedItem);
                            year = int.Parse(mD.year.SelectedItem.ToString());                            
                        }
                        adp = new MySqlDataAdapter("SELECT dateExpense, expenseID FROM expense WHERE MONTH(dateExpense) = " + month + " AND YEAR(dateExpense) = " + year + " ORDER BY dateExpense ASC", conn);
                        worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 48]].Merge();
                        worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 48]].Cells.WrapText = true;
                        worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 48]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 48]].Font.Bold = true;
                        worksheet.Cells[1, 1] = "EXPENSES FROM " + monthname + " " + year;                        
                    }
                    else
                    {
                        year = int.Parse(mD.year.SelectedItem.ToString());
                        adp = new MySqlDataAdapter("SELECT dateExpense, expenseID FROM expense WHERE YEAR(dateExpense) = " + year + " ORDER BY dateExpense ASC", conn);
                        worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 48]].Merge();
                        worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 48]].Cells.WrapText = true;
                        worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 48]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 48]].Font.Bold = true;
                        worksheet.Cells[1, 1] = "EXPENSES FOR " + year;
                    }

                    DataTable dt1 = new DataTable(); adp.Fill(dt1); int limit = dt1.Rows.Count;

                    if (dt1.Rows.Count > 0)
                    {
                        wait.lblMsg.Text = "Exporting to Excel..."; dim.Show(this); wait.Show(); 
                                               
                        // HEADERS
                        worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[2, 4]].Merge();
                        worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[2, 4]].Cells.WrapText = true;
                        worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[2, 4]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[2, 4]].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[2, 4]].Borders.Weight = 2d;
                        worksheet.Cells[2, 1] = "MONTH";

                        for (int k = 5, cat = 0; cat < 22; k = k + 2, cat++)
                        {
                            worksheet.Range[worksheet.Cells[2, k], worksheet.Cells[2, k + 1]].Merge();
                            worksheet.Range[worksheet.Cells[2, k], worksheet.Cells[2, k + 1]].Cells.WrapText = true;
                            worksheet.Range[worksheet.Cells[2, k], worksheet.Cells[2, k + 1]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            worksheet.Range[worksheet.Cells[2, k], worksheet.Cells[2, k + 1]].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                            worksheet.Range[worksheet.Cells[2, k], worksheet.Cells[2, k + 1]].Borders.Weight = 2d;
                            worksheet.Cells[2, k] = category[cat];
                        } // END OF HEADERS

                        int last = 0;
                        for (int count = 0, mon = 3; count < limit; mon++, count++)
                        {
                            worksheet.Range[worksheet.Cells[mon, 1], worksheet.Cells[mon, 4]].Merge();
                            worksheet.Range[worksheet.Cells[mon, 1], worksheet.Cells[mon, 4]].Cells.WrapText = true;
                            worksheet.Range[worksheet.Cells[mon, 1], worksheet.Cells[mon, 4]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            worksheet.Cells[mon, 1] = dt1.Rows[count]["dateExpense"].ToString();                            
                            worksheet.Range[worksheet.Cells[mon, 1], worksheet.Cells[mon, 4]].NumberFormat = "mmmm dd, yyyy";

                            for (int i = 5, cat = 0; cat < 22; i = i + 2, cat++)
                            {
                                adp = new MySqlDataAdapter("SELECT category, amount, expenseID FROM expense WHERE category = '" + category[cat] + "' AND dateExpense = '" + DateTime.Parse(dt1.Rows[count]["dateExpense"].ToString()).ToString("yyyy-MM-dd") +"' AND expenseID = " + dt1.Rows[count]["expenseID"].ToString(), conn);
                                DataTable dt = new DataTable();
                                adp.Fill(dt);
                                
                                worksheet.Range[worksheet.Cells[mon, i], worksheet.Cells[mon, i + 1]].Merge();
                                worksheet.Range[worksheet.Cells[mon, i], worksheet.Cells[mon, i + 1]].Cells.WrapText = true;
                                worksheet.Range[worksheet.Cells[mon, i], worksheet.Cells[mon, i + 1]].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;                                
                                
                                if (dt.Rows.Count > 0)
                                {
                                    if (decimal.Parse(dt.Rows[0]["amount"].ToString()) != 0)
                                    {
                                        worksheet.Cells[mon, i].NumberFormat = "#,##0.00";
                                        worksheet.Cells[mon, i] = decimal.Parse(dt.Rows[0]["amount"].ToString()).ToString("0.##");
                                    }
                                    else worksheet.Cells[mon, i] = "-";                                  
                                }
                                else worksheet.Cells[mon, i] = "-";                                
                            }
                            last = mon;
                        }
                        last += 1;
                        worksheet.Range[worksheet.Cells[last, 1], worksheet.Cells[last, 4]].Merge();
                        worksheet.Range[worksheet.Cells[last, 1], worksheet.Cells[last, 4]].Cells.WrapText = true;
                        worksheet.Range[worksheet.Cells[last, 1], worksheet.Cells[last, 4]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        worksheet.Range[worksheet.Cells[last, 1], worksheet.Cells[last, 4]].Font.Bold = true;
                        worksheet.Cells[last, 1] = "TOTAL";

                        for (int i = 5, cat = 0; cat < 22; i = i + 2, cat++)        // TOTAL
                        {                                                            
                            adp = new MySqlDataAdapter("SELECT category, amount FROM expense WHERE category = '" + category[cat] + "' AND MONTH(dateExpense) = " + month, conn);
                            DataTable dt = new DataTable();
                            adp.Fill(dt);

                            worksheet.Range[worksheet.Cells[last, i], worksheet.Cells[last, i + 1]].Merge();
                            worksheet.Range[worksheet.Cells[last, i], worksheet.Cells[last, i + 1]].Cells.WrapText = true;
                            worksheet.Range[worksheet.Cells[last, i], worksheet.Cells[last, i + 1]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            worksheet.Range[worksheet.Cells[last, i], worksheet.Cells[last, i + 1]].NumberFormat = "#,##0.00";
                            worksheet.Range[worksheet.Cells[last, i], worksheet.Cells[last, i + 1]].Font.Bold = true;

                            if (dt.Rows.Count > 0)
                            {
                                if (decimal.Parse(dt.Rows[0]["amount"].ToString()) != 0) worksheet.Cells[last, i] = decimal.Parse(dt.Rows[0]["amount"].ToString()).ToString("0.##");                                
                                else worksheet.Cells[last, i] = "0";
                            }
                            else worksheet.Cells[last, i] = "0";
                        }
                        dim.Close();
                    }
                    else errorMessage("Cannot create an empty file.");

                    SaveFileDialog saveDialog = new SaveFileDialog();
                    saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    saveDialog.FilterIndex = 1;

                    if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        workbook.SaveAs(saveDialog.FileName);
                        successMessage("Export as excel has been successful.");
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    errorMessage(ex.Message);                    
                }
                finally
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    Marshal.FinalReleaseComObject(worksheet);

                    workbook.Close(Type.Missing, Type.Missing, Type.Missing);
                    Marshal.FinalReleaseComObject(workbook);

                    excel.Quit();
                    Marshal.FinalReleaseComObject(excel);
                    workbook = null; excel = null;
                }
            }
            panelExpOp.Visible = false;
        }
    
        private void btnExpOp_Click(object sender, EventArgs e)
        {
            if (panelExpOp.Visible == false) panelExpOp.Visible = true;
            else panelExpOp.Visible = false;
        }        

        #endregion

        #region Reports
        private void exportPDF_Click(object sender, EventArgs e)
        {
            confirmMessage("Please close all PDF applications before exporting.");
            if (confirmed)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                saveDialog.FilterIndex = 1;

                if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Document doc = new Document(iTextSharp.text.PageSize.A4, 50, 50, 40, 40);
                    PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(saveDialog.FileName, FileMode.Create));
                    doc.Open();

                    System.Drawing.Image image = Properties.Resources.login_logo;
                    iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Png);
                    pdfImage.ScalePercent(25F); pdfImage.Alignment = Element.ALIGN_CENTER;
                    doc.Add(pdfImage);

                    iTextSharp.text.Font title = FontFactory.GetFont(iTextSharp.text.FontFactory.TIMES, 12, 5, BaseColor.BLACK);
                    iTextSharp.text.Font bold = FontFactory.GetFont(iTextSharp.text.FontFactory.TIMES, 12, 1, BaseColor.BLACK);
                    iTextSharp.text.Font underline = FontFactory.GetFont(iTextSharp.text.FontFactory.TIMES, 12, 4, BaseColor.BLACK);
                    iTextSharp.text.Font normal = FontFactory.GetFont(iTextSharp.text.FontFactory.TIMES, 11, BaseColor.BLACK);

                    Phrase phrase = new Phrase();
                    phrase.Add(new Chunk("BUDGET REQUEST FORM", title));
                    Paragraph par = new Paragraph(); par.Alignment = Element.ALIGN_CENTER; par.Add(phrase); doc.Add(par);

                    phrase = new Phrase();
                    phrase.Add(new Chunk("\nPURPOSE: ", normal));
                    phrase.Add(new Chunk(lblPBRPurpose.Text, underline));
                    phrase.Add(new Chunk("\nDATE REQUESTED: ", normal));
                    phrase.Add(new Chunk(lblPBRdate.Text, underline));
                    phrase.Add(new Chunk("\nCATEGORY: ", normal));
                    phrase.Add(new Chunk(lblPBRCategory.Text, underline));
                    par = new Paragraph(); par.Add(phrase); doc.Add(par);

                    phrase = new Phrase(); phrase.Add(new Chunk("\n")); par = new Paragraph(); par.Add(phrase); doc.Add(par); // NEWLINE

                    PdfPTable pdfTable = new PdfPTable(4);
                    float[] widths = new float[] { 4f, 2f, 2f, 2f };
                    pdfTable.WidthPercentage = 100; pdfTable.SetWidths(widths);

                    phrase = new Phrase(); phrase.Add(new Chunk("\nPARTICULAR\n", bold)); PdfPCell cell = new PdfPCell(phrase);
                    cell.HorizontalAlignment = 1; pdfTable.AddCell(cell);
                    phrase = new Phrase(); phrase.Add(new Chunk("\nQUANTITY\n", bold)); cell = new PdfPCell(phrase);
                    cell.HorizontalAlignment = 1; pdfTable.AddCell(cell);
                    phrase = new Phrase(); phrase.Add(new Chunk("\nUNIT PRICE\n", bold)); cell = new PdfPCell(phrase);
                    cell.HorizontalAlignment = 1; pdfTable.AddCell(cell);
                    phrase = new Phrase(); phrase.Add(new Chunk("\nAMOUNT\n", bold)); cell = new PdfPCell(phrase);
                    cell.HorizontalAlignment = 1; pdfTable.AddCell(cell);

                    int count = 0;
                    foreach (DataGridViewRow r in PBRDetails.Rows)
                    {
                        try
                        {
                            pdfTable.AddCell(r.Cells[1].Value.ToString());
                            pdfTable.AddCell(r.Cells[2].Value.ToString());
                            pdfTable.AddCell(r.Cells[3].Value.ToString());
                            pdfTable.AddCell(r.Cells[4].Value.ToString());
                            count++;
                        }
                        catch { }
                    }
                    for(int i = count; i < 24; i++)
                    {                       
                        pdfTable.AddCell(" ");
                        pdfTable.AddCell(" ");
                        pdfTable.AddCell(" ");
                        pdfTable.AddCell(" ");
                    }
                                  
                    // TOTAL
                    comm = new MySqlCommand("SELECT SUM(amount) as TOTAL FROM item WHERE budgetID = " + current_budgetID, conn);
                    MySqlDataAdapter adp = new MySqlDataAdapter(comm); DataTable dt = new DataTable(); adp.Fill(dt);

                    phrase = new Phrase(); phrase.Add(new Chunk("TOTAL", bold)); cell = new PdfPCell(phrase);
                    cell.HorizontalAlignment = 1; pdfTable.AddCell(cell);
                    pdfTable.AddCell(" ");
                    pdfTable.AddCell(" ");
                    phrase = new Phrase(); phrase.Add(new Chunk(dt.Rows[0][0].ToString(), bold)); cell = new PdfPCell(phrase);
                    cell.HorizontalAlignment = 1; pdfTable.AddCell(cell);

                    doc.Add(pdfTable);

                    PdfContentByte cb = wri.DirectContent;
                    ColumnText ct = new ColumnText(cb); Phrase right = new Phrase();
                    right.Add(new Chunk("Prepared by: ", normal));
                    right.Add(new Chunk(lblPBRBy.Text, underline));
                    ct.SetSimpleColumn(right, 65, 25, 65 * 4, 25 * 4, 15, Element.ALIGN_LEFT); ct.Go();

                    right = new Phrase(); right.Add(new Chunk("Approved by: Fr. Lionel R. Mechavez, SM", normal)); // BOOK LMAO change director
                    ct.SetSimpleColumn(right, 300, 25, 560, 25 * 4, 15, Element.ALIGN_CENTER); ct.Go();
                    right = new Phrase(); right.Add(new Chunk("Executive Director", normal));
                    ct.SetSimpleColumn(right, 310, 20, 570, 20 * 4, 15, Element.ALIGN_CENTER); ct.Go();

                    doc.Close();
                    successMessage("Budget request exported successfully!");
                }
            }            
        }
        
        private void btnDonationReport_Click(object sender, EventArgs e)
        {
            moneyDonate mD = overlay();
            mD.refToExpense = this;
            mD.tabSelection.SelectedIndex = 14;

            DialogResult result = mD.ShowDialog();

            if (result == DialogResult.OK || result == DialogResult.Yes || result == DialogResult.Retry)
            {
                Microsoft.Office.Interop.Excel._Application excel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook workbook = excel.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                try
                {
                    conn.Open();
                    worksheet = workbook.ActiveSheet;
                    worksheet.Name = "Donations";                    

                    waiting wait = new waiting();
                    dim dim = new dim();
                    dim.Location = this.Location; dim.Size = this.Size; dim.refToPrev = this;

                    // Week
                    DateTime today = DateTime.Today;
                    DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(today);
                    if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday) today = today.AddDays(3);
                    int week = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(today, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                    int week2 = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(today, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                    
                    int year = DateTime.Today.Year; string monthname = DateTime.Now.ToString("MMMM");
                    MySqlDataAdapter adp = new MySqlDataAdapter(); MySqlDataAdapter adp2 = new MySqlDataAdapter(); MySqlDataAdapter datebase = new MySqlDataAdapter();
                    /*
                     * OK - this week
                     * YES - one week select
                     * RETRY - selected week
                     * 
                     * */
                    if (result == DialogResult.OK || result == DialogResult.Yes)
                    {
                        if (result == DialogResult.Yes)
                        {
                            week = int.Parse(mD.week1.Text);                                                
                            year = int.Parse(mD.dateFrom2.Value.ToString("yyyy"));
                            monthname = mD.dateFrom2.Value.ToString("MMMM");
                        }
                        week2 = int.Parse(mD.week1.Text);
                        
                        worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 3]].Merge();
                        worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 3]].Cells.WrapText = true;                        
                        worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 3]].Font.Bold = true;
                        worksheet.Cells[1, 1] = "DONATIONS SUMMARY REPORT";
                        worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[2, 5]].Merge();
                        worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[2, 5]].Cells.WrapText = true;
                        worksheet.Cells[2, 1] = "From " + monthname + ", Week " + week + " of " + year;                        
                    }
                    else
                    {
                        week = int.Parse(mD.week1.Text);
                        week2 = int.Parse(mD.week2.Text);
                        year = int.Parse(mD.dateFrom2.Value.ToString("yyyy"));
                                                
                        worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 3]].Merge();
                        worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 3]].Cells.WrapText = true;
                        worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 3]].Font.Bold = true;
                        worksheet.Cells[1, 1] = "DONATIONS SUMMARY REPORT";
                        worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[2, 5]].Merge();
                        worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[2, 5]].Cells.WrapText = true;
                        worksheet.Cells[2, 1] = "From " + monthname + ", Week " + week + " to Week " + week2 + " of " + year;                        
                    }                    

                    int counter = 0; DataTable dt_datebase = new DataTable();                                        
                    datebase = new MySqlDataAdapter("SELECT * FROM (SELECT inkind.dateDonated FROM prototype_sad.inkind UNION SELECT monetary.dateDonated FROM prototype_sad.monetary) AS combine WHERE (WEEK(combine.dateDonated) >= " + week + " AND WEEK(combine.dateDonated) <= " + week2 + " AND YEAR(combine.dateDonated) = " + year + ") OR (WEEK(combine.dateDonated) >= " + week + " AND WEEK(combine.dateDonated) <= " + week2 + ") ORDER BY dateDonated ASC", conn);
                    datebase.Fill(dt_datebase);
                    counter = dt_datebase.Rows.Count;

                    if (counter > 0)
                    {
                        wait.lblMsg.Text = "Exporting to Excel..."; dim.Show(this); wait.Show();

                        // HEADERS
                        worksheet.Range[worksheet.Cells[4, 1], worksheet.Cells[4, 4]].Merge();
                        worksheet.Range[worksheet.Cells[4, 1], worksheet.Cells[4, 4]].Cells.WrapText = true;
                        worksheet.Range[worksheet.Cells[4, 1], worksheet.Cells[4, 4]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        worksheet.Range[worksheet.Cells[4, 1], worksheet.Cells[4, 4]].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        worksheet.Range[worksheet.Cells[4, 1], worksheet.Cells[4, 4]].Borders.Weight = 2d;
                        worksheet.Cells[4, 1] = "DATE";

                        worksheet.Range[worksheet.Cells[4, 5], worksheet.Cells[4, 5 + 1]].Merge();
                        worksheet.Range[worksheet.Cells[4, 5], worksheet.Cells[4, 5 + 1]].Cells.WrapText = true;
                        worksheet.Range[worksheet.Cells[4, 5], worksheet.Cells[4, 5 + 1]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        worksheet.Range[worksheet.Cells[4, 5], worksheet.Cells[4, 5 + 1]].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        worksheet.Range[worksheet.Cells[4, 5], worksheet.Cells[4, 5 + 1]].Borders.Weight = 2d;
                        worksheet.Cells[4, 5] = "MONETARY";

                        worksheet.Range[worksheet.Cells[4, 7], worksheet.Cells[4, 10]].Merge();
                        worksheet.Range[worksheet.Cells[4, 7], worksheet.Cells[4, 10]].Cells.WrapText = true;
                        worksheet.Range[worksheet.Cells[4, 7], worksheet.Cells[4, 10]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        worksheet.Range[worksheet.Cells[4, 7], worksheet.Cells[4, 10]].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        worksheet.Range[worksheet.Cells[4, 7], worksheet.Cells[4, 10]].Borders.Weight = 2d;
                        worksheet.Cells[4, 7] = "IN-KIND";
                        // END OF HEADERS

                        int last = 0;
                        for (int count = 0, mon = 5; count < counter; count++)
                        {
                            worksheet.Range[worksheet.Cells[mon, 1], worksheet.Cells[mon, 4]].Merge();
                            worksheet.Range[worksheet.Cells[mon, 1], worksheet.Cells[mon, 4]].Cells.WrapText = true;
                            worksheet.Range[worksheet.Cells[mon, 1], worksheet.Cells[mon, 4]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            worksheet.Cells[mon, 1] = dt_datebase.Rows[count]["dateDonated"].ToString();
                            worksheet.Range[worksheet.Cells[mon, 1], worksheet.Cells[mon, 4]].NumberFormat = "mmmm dd, yyyy";                            

                            adp = new MySqlDataAdapter("SELECT dateDonated, ORno, amount, donationID FROM monetary WHERE dateDonated = '" + DateTime.Parse(dt_datebase.Rows[count][0].ToString()).ToString("yyyy-MM-dd") + "' ORDER BY dateDonated ASC", conn);
                            adp2 = new MySqlDataAdapter("SELECT dateDonated, particular, quantity, donationID FROM inkind WHERE dateDonated = '" + DateTime.Parse(dt_datebase.Rows[count][0].ToString()).ToString("yyyy-MM-dd") + "' ORDER BY dateDonated ASC", conn);

                            DataTable money = new DataTable(); DataTable ik = new DataTable();
                            adp.Fill(money);      // MONEY
                            adp2.Fill(ik);     // INKIND

                            // MONETARY
                            worksheet.Range[worksheet.Cells[mon, 5], worksheet.Cells[mon, 6]].Merge();
                            worksheet.Range[worksheet.Cells[mon, 5], worksheet.Cells[mon, 6]].Cells.WrapText = true;
                            worksheet.Range[worksheet.Cells[mon, 5], worksheet.Cells[mon, 6]].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

                            int lastmon = mon, lastmoney = 0, lastik = 0;
                            if (money.Rows.Count > 0)
                            {
                                for(int i = 0; i < money.Rows.Count; i++, mon++)
                                {
                                    if (decimal.Parse(money.Rows[i]["amount"].ToString()) != 0)
                                    {
                                        worksheet.Range[worksheet.Cells[mon, 1], worksheet.Cells[mon, 4]].Merge();
                                        worksheet.Range[worksheet.Cells[mon, 5], worksheet.Cells[mon, 6]].Merge();
                                        worksheet.Range[worksheet.Cells[mon, 5], worksheet.Cells[mon, 6]].Cells.WrapText = true;
                                        worksheet.Range[worksheet.Cells[mon, 5], worksheet.Cells[mon, 6]].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                                        worksheet.Cells[mon, 5].NumberFormat = "#,##0.00";
                                        worksheet.Cells[mon, 5] = decimal.Parse(money.Rows[i]["amount"].ToString()).ToString("0.##");

                                        worksheet.Range[worksheet.Cells[mon, 7], worksheet.Cells[mon, 10]].Merge();
                                        worksheet.Range[worksheet.Cells[mon, 7], worksheet.Cells[mon, 10]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                        worksheet.Cells[mon, 7] = "-";
                                    }                                    
                                }
                                lastmoney = mon;
                            }
                            else worksheet.Cells[mon, 5] = "-";

                            // IN-KIND
                            mon = lastmon;
                            worksheet.Range[worksheet.Cells[mon, 7], worksheet.Cells[mon, 10]].UnMerge();
                            worksheet.Range[worksheet.Cells[mon, 7], worksheet.Cells[mon, 9]].Merge();
                            worksheet.Range[worksheet.Cells[mon, 7], worksheet.Cells[mon, 9]].Cells.WrapText = true;
                            worksheet.Range[worksheet.Cells[mon, 7], worksheet.Cells[mon, 10]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            if (ik.Rows.Count > 0)
                            {
                                for (int i = 0; i < ik.Rows.Count; i++, mon++)
                                {
                                    if (decimal.Parse(ik.Rows[i]["quantity"].ToString()) != 0)
                                    {
                                        worksheet.Range[worksheet.Cells[mon, 1], worksheet.Cells[mon, 4]].Merge();
                                        worksheet.Range[worksheet.Cells[mon, 7], worksheet.Cells[mon, 9]].Merge();
                                        worksheet.Range[worksheet.Cells[mon, 7], worksheet.Cells[mon, 9]].Cells.WrapText = true;
                                        worksheet.Range[worksheet.Cells[mon, 7], worksheet.Cells[mon, 10]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                        worksheet.Cells[mon, 7] = ik.Rows[0]["particular"].ToString();
                                        worksheet.Cells[mon, 10] = decimal.Parse(ik.Rows[0]["quantity"].ToString());
                                    }
                                }
                                lastik = mon;
                            }
                            else
                            {
                                worksheet.Range[worksheet.Cells[mon, 7], worksheet.Cells[mon, 10]].Merge();
                                worksheet.Cells[mon, 7] = "-";
                            }

                            if (lastik > lastmoney) mon = lastik;
                            else if (lastik < lastmoney) mon = lastmoney;
                            else if (lastik == lastmoney && lastik != 0) mon = lastik;
                            else mon = lastmon;

                            last = mon;
                        }
                        /*last += 1;
                        worksheet.Range[worksheet.Cells[last, 1], worksheet.Cells[last, 4]].Merge();
                        worksheet.Range[worksheet.Cells[last, 1], worksheet.Cells[last, 4]].Cells.WrapText = true;
                        worksheet.Range[worksheet.Cells[last, 1], worksheet.Cells[last, 4]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        worksheet.Range[worksheet.Cells[last, 1], worksheet.Cells[last, 4]].Font.Bold = true;
                        worksheet.Cells[last, 1] = "TOTAL";

                        for (int i = 5, cat = 0; cat < 22; i = i + 2, cat++)        // TOTAL
                        {
                            //adp = new MySqlDataAdapter("SELECT category, amount FROM expense WHERE category = '" + category[cat] + "' AND MONTH(dateDonated) = " + month, conn);
                            DataTable dt = new DataTable();
                            adp.Fill(dt);

                            worksheet.Range[worksheet.Cells[last, i], worksheet.Cells[last, i + 1]].Merge();
                            worksheet.Range[worksheet.Cells[last, i], worksheet.Cells[last, i + 1]].Cells.WrapText = true;
                            worksheet.Range[worksheet.Cells[last, i], worksheet.Cells[last, i + 1]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            worksheet.Range[worksheet.Cells[last, i], worksheet.Cells[last, i + 1]].NumberFormat = "#,##0.00";
                            worksheet.Range[worksheet.Cells[last, i], worksheet.Cells[last, i + 1]].Font.Bold = true;

                            if (dt.Rows.Count > 0)
                            {
                                if (decimal.Parse(dt.Rows[0]["amount"].ToString()) != 0) worksheet.Cells[last, i] = decimal.Parse(dt.Rows[0]["amount"].ToString()).ToString("0.##");
                                else worksheet.Cells[last, i] = "0";
                            }
                            else worksheet.Cells[last, i] = "0";
                        }*/
                        dim.Close();
                        conn.Close();

                        SaveFileDialog saveDialog = new SaveFileDialog();
                        saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                        saveDialog.FilterIndex = 1;

                        if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            workbook.SaveAs(saveDialog.FileName);
                            successMessage("Export as excel has been successful.");
                        }
                        else errorMessage("Cancelled saving.");
                    }
                    else errorMessage("Cannot create empty file.");                    
                }
                catch (Exception ex)
                {
                    errorMessage(ex.Message);
                }
                finally
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    Marshal.FinalReleaseComObject(worksheet);

                    workbook.Close(Type.Missing, Type.Missing, Type.Missing);
                    Marshal.FinalReleaseComObject(workbook);

                    excel.Quit();
                    Marshal.FinalReleaseComObject(excel);
                    workbook = null; excel = null;
                }
            }            
        }
        #endregion

        #region Textboxes
        private void txtNew_Enter(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == "Name of donor." || ((TextBox)sender).Text == "Name of purpose." || ((TextBox)sender).Text == "Name." || ((TextBox)sender).Text == "29xxxxx" || ((TextBox)sender).Text == "example@example.com" || ((TextBox)sender).Text == "09xx" || ((TextBox)sender).Text == "xxx" || ((TextBox)sender).Text == "xxxx") ((TextBox)sender).Text = "";
            ((TextBox)sender).ForeColor = Color.Black;

            if (((TextBox)sender).Name == "txtMobile1" || ((TextBox)sender).Name == "txtMobile2" || ((TextBox)sender).Name == "txtMobile3")
            {
                lblMobile.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                panelMobile.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
            }

            if (((TextBox)sender).Name == "txtDName")
            {
                lblDName.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                panelDName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                countDName.Visible = true;
            }
            else if (((TextBox)sender).Name == "txtPhone")
            {
                lblPhone.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                panelPhone.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                countPhone.Visible = true;
            }
            else if (((TextBox)sender).Name == "txtEmail")
            {
                lblEmail.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                panelEmail.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                countEmail.Visible = true;
            }
            else if (((TextBox)sender).Name == "txtPurpose")
            {
                if (!brEdit)
                {
                    lblPurpose.ForeColor = countPurpose.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                    panel.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                    countPurpose.Visible = true;
                }
                else
                {
                    lblPurpose.ForeColor = countPurpose.ForeColor = System.Drawing.Color.FromArgb(219, 209, 92);
                    panel.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_yellow;
                    countPurpose.Visible = true;
                }
            }
            else if (((TextBox)sender).Name == "txtBRRequest") 
            {
                if (!brEdit)
                {
                    lblBRRequest.ForeColor = countPurpose.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                    panel.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                    countBRRequest.Visible = true;
                }
                else
                {
                    lblBRRequest.ForeColor = countPurpose.ForeColor = System.Drawing.Color.FromArgb(219, 209, 92);
                    panel.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_yellow;
                    countBRRequest.Visible = true;
                }
            }
        }

        private void txtNewCount_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Name == "txtDName") countDName.Text = ((TextBox)sender).TextLength + "/250";
            else if (((TextBox)sender).Name == "txtPhone") countPhone.Text = ((TextBox)sender).TextLength + "/7";
            else if (((TextBox)sender).Name == "txtEmail") countEmail.Text = ((TextBox)sender).TextLength + "/100";
            else if (((TextBox)sender).Name == "txtPurpose") countPurpose.Text = ((TextBox)sender).TextLength + "/100";
            else if (((TextBox)sender).Name == "txtBRRequest") countBRRequest.Text = ((TextBox)sender).TextLength + "/100";
        }

        private void txtNew_Leave(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == "")
            {
                if (((TextBox)sender).Name == "txtDName") ((TextBox)sender).Text = "Name of donor.";
                else if (((TextBox)sender).Name == "txtPhone") ((TextBox)sender).Text = "29xxxxx";
                else if (((TextBox)sender).Name == "txtEmail") ((TextBox)sender).Text = "example@example.com";
                else if (((TextBox)sender).Name == "txtMobile1") ((TextBox)sender).Text = "09xx";
                else if (((TextBox)sender).Name == "txtMobile2") ((TextBox)sender).Text = "xxx";
                else if (((TextBox)sender).Name == "txtMobile3") ((TextBox)sender).Text = "xxxx";
                else if (((TextBox)sender).Name == "txtPurpose") ((TextBox)sender).Text = "Name of purpose.";
                else if (((TextBox)sender).Name == "txtBRRequest") ((TextBox)sender).Text = "Name.";
            }
            ((TextBox)sender).ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);

            if (((TextBox)sender).Name == "txtDName")
            {
                lblDName.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                panelDName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
                countDName.Visible = false;
            }
            else if (((TextBox)sender).Name == "txtPhone")
            {
                lblPhone.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                panelPhone.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
                countPhone.Visible = false;
            }
            else if (((TextBox)sender).Name == "txtEmail")
            {
                lblEmail.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                panelEmail.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
                countEmail.Visible = false;
            }
            else if (((TextBox)sender).Name == "txtMobile1" || ((TextBox)sender).Name == "txtMobile2" || ((TextBox)sender).Name == "txtMobile3")
            {
                lblMobile.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                panelMobile.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
            }
            else if (((TextBox)sender).Name == "txtPurpose")
            {
                lblPurpose.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                panel.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
                countPurpose.Visible = false;
            }
            else if (((TextBox)sender).Name == "txtBRRequest")
            {
                lblBRRequest.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                panel.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
                countBRRequest.Visible = false;
            }
        }

        private void btnArchive_Click(object sender, EventArgs e)
        {
            tabInnerDonors.SelectedIndex = 2;
            panelListChild.BackColor = System.Drawing.Color.FromArgb(60, 60, 60);
            lblListOfDonors.Text = "List of Inactive Donors";
            loadArchiveDonor();
        }

        private void richNew_Enter(object sender, EventArgs e)
        {
            if (((RichTextBox)sender).Text == "Enter address.") ((RichTextBox)sender).Text = "";
            ((RichTextBox)sender).ForeColor = Color.Black;
            ((RichTextBox)sender).BackColor = System.Drawing.Color.FromArgb(250, 250, 250);

            if (((RichTextBox)sender).Name == "txtDonorAd")
            {
                lblDonorAd.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                countAd.Visible = true;
            }
            else if (((RichTextBox)sender).Name == "txtDonorAdEdit")
            {
                lblDonorAdEdit.ForeColor = System.Drawing.Color.FromArgb(219, 209, 92);
                countAdEdit.Visible = true;
            }
        }

        private void richNew_TextChanged(object sender, EventArgs e)
        {
            if (((RichTextBox)sender).Name == "txtDonorAd") countAd.Text = ((RichTextBox)sender).TextLength + "/250";
            else if (((RichTextBox)sender).Name == "txtDonorAdEdit") countAdEdit.Text = ((RichTextBox)sender).TextLength + "/250";
        }

        private void expense_FormClosing(object sender, FormClosingEventArgs e)
        {
            reftomain.Show();
        }

        private void richNew_Leave(object sender, EventArgs e)
        {
            if (((RichTextBox)sender).Text == "")
            {
                ((RichTextBox)sender).Text = "Address of donor.";
            }
            ((RichTextBox)sender).ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);
            ((RichTextBox)sender).BackColor = System.Drawing.Color.FromArgb(250, 250, 250);

            if (((RichTextBox)sender).Name == "txtDonorAd")
            {
                lblDonorAd.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);                
                countAd.Visible = false;
            }
            else if (((RichTextBox)sender).Name == "txtDonorAdEdit")
            {
                resetEditColorDefault();
                countAdEdit.Visible = true;
            }
        }

        private void expList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {            
            int row = expList.CurrentCell.RowIndex;
            if (expList.Rows[row].Cells[5].FormattedValue.ToString() == "Yes")
            {
                expList.Size = new System.Drawing.Size(935, 324); panelExpOp.Visible = false;
                current_budgetID = int.Parse(expList.Rows[row].Cells[4].FormattedValue.ToString());                

                conn.Open();
                MySqlCommand comm = new MySqlCommand("SELECT * FROM budget WHERE budgetID = " + current_budgetID, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm); DataTable dt = new DataTable(); adp.Fill(dt);
                comm = new MySqlCommand("SELECT * FROM expense WHERE budgetID = " + current_budgetID, conn);
                MySqlDataAdapter adp2 = new MySqlDataAdapter(comm); DataTable dt2 = new DataTable(); adp2.Fill(dt2);                

                lblPBRPurpose.Text = dt.Rows[0]["purpose"].ToString();
                lblPBRCategory.Text = dt2.Rows[0]["category"].ToString();
                lblPBRBy.Text = dt.Rows[0]["requestedBy"].ToString();
                lblPBRdate.Text = DateTime.Parse(dt2.Rows[0]["dateExpense"].ToString()).ToString("MMMM dd, yyyy").ToString();
                lblPBRTotal.Text = dt2.Rows[0]["amount"].ToString();

                conn.Close();

                lblBRHeader.Text = "Expense Details";
                panelPBR.BackColor = System.Drawing.Color.FromArgb(65, 64, 77);
                lblBudgetTotal.Text = "Expense Total (PHP)"; lblOn.Text = "converted to expense on";

                comm = new MySqlCommand("SELECT * FROM item WHERE budgetID = " + current_budgetID, conn);
                loadTable(comm, 5);

                PBRDetails.Size = new System.Drawing.Size(935, 409);
                panelOptions.Visible = false;
                tabSelection.SelectedTab = tabBRList; tabPBR.SelectedTab = PBRDecide;
            }
        }        
        
        private void txtMobile1_TextChanged(object sender, EventArgs e)
        {
            if (txtMobile1.TextLength == 4) txtMobile2.Focus();
        }

        private void txtMobile2_TextChanged(object sender, EventArgs e)
        {
            if (txtMobile2.TextLength == 3) txtMobile3.Focus();
        }

        private void txtMobile3_TextChanged(object sender, EventArgs e)
        {
            if (txtMobile3.TextLength == 4)
            {
                // Leave textbox     
                // LMAO          
            }
        }

        private void tabNewInfo_Click(object sender, EventArgs e)
        {
            tabNewInfo.Focus();
        }
        #endregion
    }
}