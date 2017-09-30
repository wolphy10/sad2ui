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
    public partial class OthersAttend : Form
    {
        public MySqlConnection conn;
        public eventorg reftoEvAttend { get; set; }
        public OthersAttend()
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=prototype_sad;Uid=root;Pwd=root;");
        }

        #region error and confirm and success
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

        public void successMessage(string message)            // Success Message
        {
            success yey = new success();
            dim dim = new dim();

            dim.Location = this.Location;
            dim.Size = this.Size;
            dim.refToPrev = this;
            dim.Show(this);

            if (yey.ShowDialog() == DialogResult.OK) dim.Close();
        }

        public bool confirmed;

        public void confirmMessage(string message)            // Success Message
        {
            confirm conf = new confirm();
            dim dim = new dim();

            dim.Location = this.Location;
            dim.Size = this.Size;
            conf.lblConfirm.Text = message;
            dim.refToPrev = this;
            dim.Show(this);

            if (conf.ShowDialog() == DialogResult.OK) confirmed = true;
            else confirmed = false;
            dim.Close();
        }
        #endregion

        private void OthersAttend_Load(object sender, EventArgs e)
        {
        }

        private void countEName_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void OthersAttend_FormClosing(object sender, FormClosingEventArgs e)
        {
            reftoEvAttend.Show();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (txtFAttendName.Text == "What is the first name of the attendee?" || txtLAttendName.Text == "What is the last name of the attendee?" || cbARole.Text == "") errorMessage("Please fill up necessary fields.");
            else
            {
                reftoEvAttend.othersFAttendee = txtFAttendName.Text;
                reftoEvAttend.othersLAttendee = txtLAttendName.Text;
                reftoEvAttend.otherRole = cbARole.Text;
                this.DialogResult = DialogResult.OK;
            }
        }
        public bool showevtype = true;
        private void btnShowAdd_Click(object sender, EventArgs e)
        {
            if (showevtype)
            {
                cbARole.DropDownStyle = ComboBoxStyle.DropDown;
                btnShowAdd.BackgroundImage = global::BalayPasilungan.Properties.Resources.checked_symbol;
                btnNext.Enabled = false;
                showevtype = false;
            }
            else
            {
                cbARole.Items.Add(cbARole.Text);
                cbARole.DropDownStyle = ComboBoxStyle.DropDownList;
                btnShowAdd.BackgroundImage = global::BalayPasilungan.Properties.Resources.addsomething;
                btnNext.Enabled = true;
                showevtype = true;
            }
        }


        private void txtEventName_Enter(object sender, EventArgs e)
        {
            txtFAttendName.ForeColor = Color.Black;
            if (txtFAttendName.Text.Equals("What is the first name of the attendee?")) txtFAttendName.Text = "";
            panelEName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_green;
            lblEventName.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            countEName.Visible = true;
        }

        private void txtAttendName_Leave(object sender, EventArgs e)
        {
            lblEventName.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a");
            txtFAttendName.ForeColor = System.Drawing.ColorTranslator.FromHtml("#878787");
            if (txtFAttendName.Text.Equals("")) txtFAttendName.Text = "What is the first name of the attendee?";
            panelEName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
            countEName.Visible = false;
        }

        private void txtAttendName_TextChanged(object sender, EventArgs e)
        {
            int count = txtFAttendName.Text.Length;
            countEName.Text = count + "/100";
        }

        private void txtLAttendName_Enter(object sender, EventArgs e)
        {
            txtLAttendName.ForeColor = Color.Black;
            if (txtLAttendName.Text.Equals("What is the last name of the attendee?")) txtLAttendName.Text = "";
            panel1.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_green;
            lbLAttendName.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            countEName2.Visible = true;
        }

        private void txtLAttendName_Leave(object sender, EventArgs e)
        {
            lbLAttendName.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a");
            txtFAttendName.ForeColor = System.Drawing.ColorTranslator.FromHtml("#878787");
            if (txtLAttendName.Text.Equals("")) txtLAttendName.Text = "What is the last name of the attendee?";
            panel1.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
            countEName2.Visible = false;
        }

        private void txtLAttendName_TextChanged(object sender, EventArgs e)
        {
            int count = txtLAttendName.Text.Length;
            countEName2.Text = count + "/100";
        }
    }
}
