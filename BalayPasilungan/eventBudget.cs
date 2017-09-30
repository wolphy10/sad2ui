using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace BalayPasilungan
{
    public partial class eventBudget : Form
    {
        public MySqlConnection conn;
        public MySqlCommand comm;
        public MySqlDataAdapter adp;
        public DataTable dt;
        public eventorg reftoeventorg { get; set; }
        public bool confirmed;
        public bool empty;
        public int current_item, current_budgetID;

        public eventBudget()
        {
            InitializeComponent();
            conn = new MySqlConnection("server=localhost;user id=root;database=prototype_sad;password=root;persistsecurityinfo=False");

            brInfo.Renderer = new renderer2();
        }

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

        #region Functions
        public void confirmMessage(string message)            // Success Message
        {
            confirm conf = new confirm();
            dim dim = new dim();

            dim.Location = this.Location;
            dim.Size = this.Size;
            dim.refToPrev = this;
            dim.Show(this);

            conf.lblConfirm.Text = message;
            if (conf.ShowDialog() == DialogResult.OK) confirmed = true;
            else confirmed = false;
            dim.Close();
        }

        public void successMessage(string message)            // Success Message
        {
            success yey = new success();
            dim dim = new dim();

            dim.Location = this.Location;
            dim.Size = this.Size;
            yey.lblSuccess.Text = message;
            dim.refToPrev = this;
            dim.Show(this);

            if (yey.ShowDialog() == DialogResult.OK) dim.Close();
        }

        public void errorMessage(string message)            // Error Message
        {
            error err = new error();
            dim dim = new dim();

            dim.Location = this.Location;
            dim.Size = this.Size;
            err.lblError.Text = message;
            dim.refToPrev = this;
            dim.Show(this);

            if (err.ShowDialog() == DialogResult.OK) dim.Close();
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

        public void get(int type)
        {
            try
            {
                if (type == 1)           // Get Budget ID
                {
                    conn.Open();
                    comm = new MySqlCommand("INSERT INTO budget (status) VALUES ('Pending');", conn);
                    comm.ExecuteNonQuery();

                    adp = new MySqlDataAdapter("SELECT budgetID FROM budget ORDER BY budgetID DESC LIMIT 1", conn);
                    dt = new DataTable();
                    adp.Fill(dt);

                    current_budgetID = int.Parse(dt.Rows[0]["budgetID"].ToString());                    
                    
                    comm = new MySqlCommand("SELECT * FROM item WHERE budgetID = " + current_budgetID, conn);
                    conn.Close();
                    loadTable(comm, 1);
                }
            }
            catch (Exception ex)
            {
                errorMessage(ex.Message);
            }
        }
        #endregion

        #region SQL Connections

        public void loadTable(MySqlCommand comm, int type)
        {
            try
            {
                conn.Open();

                adp = new MySqlDataAdapter(comm);
                dt = new DataTable();
                adp.Fill(dt);

                if (type == 1)          // Budget Request Particular Details
                {
                    if (dt.Rows.Count == 0)
                    {
                        dt.Rows.Add(-1, "No entries.", null, null, null, null);
                        empty = true;
                    }

                    BRDetails.DataSource = dt;

                    // Donation In Kind UI Modifications
                    BRDetails.Columns[1].HeaderText = "PARTICULAR";
                    BRDetails.Columns[2].HeaderText = "QUANTITY";
                    BRDetails.Columns[3].HeaderText = "UNIT PRICE";
                    BRDetails.Columns[4].HeaderText = "AMOUNT";

                    // For ID purposes (hidden from user)            
                    BRDetails.Columns[0].Visible = BRDetails.Columns[5].Visible = false;

                    // 633 TOTAL WIDTH
                    BRDetails.Columns[1].Width = 320;
                    BRDetails.Columns[2].Width = 73;
                    BRDetails.Columns[3].Width = BRDetails.Columns[4].Width = 120;

                    if (dt.Rows.Count > 0 && !empty)
                    {
                        BRDetails.Columns[1].HeaderCell.Style.Padding = BRDetails.Columns[1].DefaultCellStyle.Padding = new Padding(15, 0, 0, 0);
                        btnDelBR.Enabled = btnEditBR.Enabled = true;
                    }
                    else btnDelBR.Enabled = btnEditBR.Enabled = empty = false;
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

                    BRDetails.Columns["particular"].HeaderCell.Style.Padding = BRDetails.Columns["particular"].DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
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
                // Delete item
                if (type == 1) comm = new MySqlCommand("DELETE FROM item WHERE itemID = " + ID, conn);
                                
                comm.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                errorMessage(ex.Message);
            }
        }
        #endregion
        
        #region Buttons
        private void btn_Click(object sender, EventArgs e)
        {
            if (rbOthers.Checked) lblBRCategory.Text = cbExpCat.SelectedItem.ToString();
            if (lblBRCategory.Text != "" && txtBRRequest.Text != "Name." && txtPurpose.Text != "Name of purpose.")
            {
                MySqlCommand comm = new MySqlCommand("SELECT * FROM item WHERE budgetID = " + current_budgetID, conn);
                loadTable(comm, 5);

                tabBR.SelectedIndex = 1;
                lblBRPurpose.Text = txtPurpose.Text;
                lblBRBy.Text = txtBRRequest.Text;

                brparTS.Font = new System.Drawing.Font("Segoe UI", 20.25F);
                brTS.Font = new System.Drawing.Font("Segoe UI Semilight", 20.25F);
                brTS.ForeColor = System.Drawing.Color.FromArgb(197, 217, 208);

                String dateRequested = DateTime.Today.ToString();
            }
            else errorMessage("Please fill up all fields or choose a category.");
        }

        private void btn_Close(object sender, EventArgs e)
        {
            confirmMessage("Are you sure you want to cancel this request?");
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
                this.Close();
            }
            else this.Enabled = true;
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
                                + "', budgetTotal = " + sum + ", dateRequested = '" + DateTime.Today.ToString("yyyy-MM-dd") + "', requestedBy = '" + txtBRRequest.Text + "' WHERE budgetID = " + current_budgetID, conn);
                            comm.ExecuteNonQuery();

                            conn.Close();
                            this.Close();
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
            }
        }
        #endregion

        #region Textboxes
        private void txtNew_Enter(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == "Name of donor." || ((TextBox)sender).Text == "Name of purpose." || ((TextBox)sender).Text == "Name." || ((TextBox)sender).Text == "29xxxxx" || ((TextBox)sender).Text == "example@example.com" || ((TextBox)sender).Text == "09xx" || ((TextBox)sender).Text == "xxx" || ((TextBox)sender).Text == "xxxx") ((TextBox)sender).Text = "";
            ((TextBox)sender).ForeColor = Color.Black;
            
            if (((TextBox)sender).Name == "txtPurpose")
            {
                lblPurpose.ForeColor = countPurpose.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                panel.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                countPurpose.Visible = true;
            }
            else if (((TextBox)sender).Name == "txtBRRequest")
            {
                lblBRRequest.ForeColor = countBRRequest.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                panel.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                countBRRequest.Visible = true;
            }
        }

        private void txtNewCount_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Name == "txtPurpose") countPurpose.Text = ((TextBox)sender).TextLength + "/100";
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
            if (((TextBox)sender).Name == "txtPurpose")
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
        #endregion

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
            else if (((RadioButton)sender).Name == "rbOthers") cbExpCat.SelectedIndex = 0;
            if (rbOthers.Checked) lblOthers.Visible = cbExpCat.Visible = true;
        }

        #region Details
        private void BRDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && !empty) current_item = int.Parse(((DataGridView)sender).Rows[e.RowIndex].Cells[0].FormattedValue.ToString());
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
        #endregion

        private void eventBudget_Load(object sender, EventArgs e)
        {
            MessageBox.Show("ok lng ba kung walang multiple selection sa category? wala bang instances na may mag sabay? or kung magsabay yung sa catergories kay others nlng ??");
            lblEventName.Text = reftoeventorg.evNameBudget;
        }

        private void catType_Click(object sender, EventArgs e)
        {
            btnCatSingle.BackColor = btnCatMultiple.BackColor = Color.White;
            ((Button)sender).BackColor = System.Drawing.Color.FromArgb(146, 211, 202);
            if (((Button)sender).Name == "btnCatSingle")
            {
                lblBRCategory.Text = "";
                panelCat.Visible = lblCategory.Visible = true;
                if (rbOthers.Checked) lblBRCategory.Text = cbExpCat.SelectedItem.ToString();
                else rbClothing.Checked = true;
            }
            else
            {
                lblBRCategory.Text = "Multiple";
                panelCat.Visible = lblCategory.Visible = false;
            }
        }

        private void btnEditBR_Click(object sender, EventArgs e)
        {
            int row = BRDetails.CurrentCell.RowIndex;

            moneyDonate mD = overlay();
            mD.itemID = int.Parse(BRDetails.Rows[row].Cells[0].Value.ToString());
            if (lblBRCategory.Text == "Multiple")
            {
                mD.tabSelection.SelectedIndex = 15;
                mD.txtBRC_Part2.Text = BRDetails.Rows[row].Cells[1].Value.ToString();
                mD.cbBRC_Cat2.SelectedItem = BRDetails.Rows[row].Cells[5].Value.ToString();
                mD.txtBRC_Quantity2.Value = Decimal.Parse(BRDetails.Rows[row].Cells[2].Value.ToString());
                mD.txtBRC_UP2.Text = BRDetails.Rows[row].Cells[3].Value.ToString();
                mD.txtBRC_total2.Text = BRDetails.Rows[row].Cells[4].Value.ToString();
            }
            else
            {
                mD.tabSelection.SelectedIndex = 10;
                mD.txtBRPart2.Text = BRDetails.Rows[row].Cells[1].Value.ToString();
                mD.txtBRQuantity2.Value = Decimal.Parse(BRDetails.Rows[row].Cells[2].Value.ToString());
                mD.txtBRUP2.Text = BRDetails.Rows[row].Cells[3].Value.ToString();
                mD.txtBRTotal2.Text = BRDetails.Rows[row].Cells[4].Value.ToString();
            }
            mD.ShowDialog();
            MySqlCommand comm = new MySqlCommand("SELECT * FROM item WHERE budgetID = " + current_budgetID, conn);
            loadTable(comm, 3);
        }

        private void eventBudget_FormClosing(object sender, FormClosingEventArgs e)
        {
            reftoeventorg.Show();
        }
    }
}
