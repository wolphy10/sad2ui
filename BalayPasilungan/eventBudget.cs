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
            if (((Button)sender).Name == "btnBRNext" && txtPurpose.Text != "Name of purpose.")
            {
                tabBR.SelectedIndex = 1;
                lblBRPurpose.Text = txtPurpose.Text;
                lblBREvent.Text = lblEventName.Text;

                brparTS.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                brparTS.Font = new System.Drawing.Font("Segoe UI", 20.25F);
                brTS.ForeColor = System.Drawing.Color.FromArgb(197, 217, 208);
                brTS.Font = new System.Drawing.Font("Segoe UI Semilight", 20.25F);
                
                get(1);
            }
            else if (((Button)sender).Name == "btnBRNext" && txtPurpose.Text == "Name of purpose.") errorMessage("Please enter name of purpose.");

            if (((Button)sender).Name == "btnBRBack") tabBR.SelectedIndex = 0;
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
                confirmMessage("Are you sure you want to request this budget?");
                if (confirmed)
                {
                    conn.Open();

                    comm = new MySqlCommand("SELECT SUM(amount) as SUM FROM item WHERE budgetID = " + current_budgetID, conn);
                    adp = new MySqlDataAdapter(comm);
                    dt = new DataTable();
                    adp.Fill(dt);

                    decimal sum = Decimal.Round(decimal.Parse(dt.Rows[0]["SUM"].ToString()), 2);

                    comm = new MySqlCommand("UPDATE budget SET purpose = '" + lblBRPurpose.Text + "', category = '"
                        + lblBRCategory.Text + "', budgetTotal = " + sum + ", dateRequested = '" + DateTime.Today.ToString("yyyy-MM-dd") + "', eventName = '" + lblEventName.Text + "' WHERE budgetID = " + current_budgetID, conn);
                    comm.ExecuteNonQuery();

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                errorMessage(ex.Message);
            }
        }
        #endregion

        #region Textboxes
        private void txtNew_Enter(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == "Name of purpose.") ((TextBox)sender).Text = "";
            ((TextBox)sender).ForeColor = Color.Black;

            if (((TextBox)sender).Name == "txtPurpose")
            {
                lblPurpose.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                panelPurpose.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                countPurpose.Visible = true;
            }
            else if (((TextBox)sender).Name == "txtBROthers")
            {
                lblOthers.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                panelOthers.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;                
            }
        }

        private void txtNewCount_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Name == "txtPurpose") countPurpose.Text = ((TextBox)sender).TextLength + "/250";
        }

        private void txtNew_Leave(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == "")
            {
                if (((TextBox)sender).Name == "txtPurpose") ((TextBox)sender).Text = "Name of purpose.";
                else if (((TextBox)sender).Name == "txtBROthers") ((TextBox)sender).Text = "";
            }
            ((TextBox)sender).ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);

            if (((TextBox)sender).Name == "txtPurpose")
            {
                lblPurpose.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                panelPurpose.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
                countPurpose.Visible = false;
            }
            else if (((TextBox)sender).Name == "txtBROthers")
            {
                lblOthers.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                panelOthers.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
            }
        }
        #endregion

        private void category_CheckedChanged(object sender, EventArgs e)
        {
            if (rbClothing.Checked) lblBRCategory.Text = "Clothing";
            else if (rbFood.Checked) lblBRCategory.Text = "Food";
            else if (rbHouse.Checked) lblBRCategory.Text = "Repair and Maintenance";
            else if (rbMeds.Checked) lblBRCategory.Text = "Medical Supplies";
            else if (rbOffice.Checked) lblBRCategory.Text = "Office Supplies";
            else if (rbSchool.Checked) lblBRCategory.Text = "Education";
            else if (rbSkills.Checked) lblBRCategory.Text = "Skills and Development";
            else if (rbSocial.Checked) lblBRCategory.Text = "Recreation";
            else if (rbSpiritual.Checked) lblBRCategory.Text = "Spiritual Formation";
            else if (rbTranspo.Checked) lblBRCategory.Text = "Transportation";
            else if (((RadioButton)sender).Name == "rbOthers")
            {
                if (rbOthers.Checked) panelOthers.Enabled = true;
                else panelOthers.Enabled = false;
                lblBRCategory.Text = txtBROthers.Text;
            }
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
            mD.tabSelection.SelectedIndex = 7;
            mD.ShowDialog();

            comm = new MySqlCommand("SELECT * FROM item WHERE budgetID = " + current_budgetID, conn);
            loadTable(comm, 1);
        }

        private void btnDelBR_Click(object sender, EventArgs e)
        {
            int row = BRDetails.CurrentCell.RowIndex;

            if (BRDetails.Rows[row].Cells[1].Value.ToString() != "No entries.")
            {
                confirmMessage("Are you sure you want to remove this item?");
                if (confirmed)
                {
                    del(current_item, 1);
                    MySqlCommand comm = new MySqlCommand("SELECT * FROM item WHERE budgetID = " + current_budgetID, conn);
                    loadTable(comm, 1);
                }
            }            
        }
        #endregion
    }
}
