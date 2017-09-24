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
using System.Globalization;

namespace BalayPasilungan
{
    public partial class EvEditDetails : Form
    {
        public MySqlConnection conn;
        public String[] aMonths = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        public int evidEdit;
        public bool allDayState = false, remindState = false, budgetState = false;
        public string ampmFrom = "", ampmTo = "", ampmR = "", remindYN = "no", budgetYN = "no";
        public eventorg reftoevorg { get; set; }
        public EvEditDetails()
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=prototype_sad;Uid=root;Pwd=root;");

        }
        public string evnEdit { get; set; }
        public string editName, editVenue, editDes, editType, editDate, editRemind = "", editBudget = "";
        private void EvEditDetails_Load(object sender, EventArgs e)
        {
            if (cb_MRemind.Items.Count == 0)//remind month
            {
                for (int i = 0; i < 12; i++)
                {
                    cb_YRemind.Items.Add(aMonths[i]);
                }
            }
            if (cb_YRemind.Items.Count == 0)//remind year
            {
                for (int i = DateTime.Now.Year; i <= 2099; i++)
                {
                    cb_YRemind.Items.Add(i);
                }
            }
            if (cbEMonth.Items.Count == 0)//month from
            {
                for (int i = 0; i < 12; i++)
                {
                    cbEMonth.Items.Add(aMonths[i]);
                }
            }
            if (cbEYear.Items.Count == 0)//year from
            {
                for (int i = DateTime.Now.Year; i <= 2099; i++)
                {
                    cbEYear.Items.Add(i);
                }
            }
            if (cbEMonth2.Items.Count == 0)//month to
            {
                for (int i = 0; i < 12; i++)
                {
                    cbEMonth2.Items.Add(aMonths[i]);
                }
            }
            if (cbEYear2.Items.Count == 0)//year to
            {
                for (int i = DateTime.Now.Year; i <= 2099; i++)
                {
                    cbEYear2.Items.Add(i);
                }
            }
            evViewDetails(evnEdit);
        }
        #region error, success and confirm
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

        #region functions
        private void resetDayButtons()
        {
            btnAllDay.BackColor = Color.White;
            btnMulDay.BackColor = Color.White;
            btnAllDay.ForeColor = System.Drawing.ColorTranslator.FromHtml("#8f8f8f");
            btnMulDay.ForeColor = System.Drawing.ColorTranslator.FromHtml("#8f8f8f");
            btnAllDay.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#8f8f8f");
            btnMulDay.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#8f8f8f");
        }
        //design functions
        private void resetLabelsPanels()                // Set label and panel colors to default (gray)
        {
            lblEventName.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a");
            lblEVenue.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a");
            lblEType.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a");
            lblEDes.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a");

            lblEDate.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a");

            lblRDate.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a");

            //lblRequestBy.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a");

            panelEName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
            panelEVenue.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
            //panelRequestBy.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
        }

        private void resetCounters()
        {
            countEName.Visible = false;
            countEVenue.Visible = false;
            countEDes.Visible = false;
            //countRequestBy.Visible = false;
        }
        //list view and sql functions
        public string[] desc = { "Event Name" , "Event Venue" , "Event Description" , "Event Type", "Event Date And Time", "Event Reminder" , "Event Budget" }, value = new string[7];
        public void evViewDetails(string evnEdit)
        {
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand("SELECT * FROM event WHERE evName = '" + evnEdit + "'", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                conn.Close();
                if (dt.Rows.Count == 1)
                {
                    evidEdit = int.Parse(dt.Rows[0]["eventID"].ToString());
                    editName = dt.Rows[0]["evName"].ToString();
                    editVenue = dt.Rows[0]["evVenue"].ToString();
                    editDes = dt.Rows[0]["evDesc"].ToString();
                    editType = dt.Rows[0]["evType"].ToString();
                    editDate = "From: " + dt.Rows[0]["evDateFrom"].ToString() + " " + dt.Rows[0]["evTimeFrom"].ToString() + "\n" +
                                    "To: " + dt.Rows[0]["evDateTo"].ToString() + " " + dt.Rows[0]["evTimeTo"].ToString();
                    if (dt.Rows[0]["reminderDate"].ToString() == "") editRemind = "NONE";
                    else editRemind = dt.Rows[0]["reminderDate"].ToString() + " " + dt.Rows[0]["reminderTime"].ToString();
                    if (dt.Rows[0]["budget"].ToString() == "") editBudget = "NONE";
                    else editBudget = dt.Rows[0]["budget"].ToString();
                    value[0] = editName; value[1] = editVenue; value[2] = editDes; value[3] = editType; value[4] = editDate; value[5] = editRemind; value[6] = editBudget;
                    viewDataEdit();
                    txtEventName.Text = editName; txtVenue.Text = editVenue; txtEventDes.Text = editDes; cbEType.Text = editType;
                    //cbEMonth.SelectedIndex = int.Parse(dt.Rows[0]["evDateFrom"].ToString().Substring(5, 2)) - 1;
                    //cbEYear.SelectedIndex = cbEYear.FindStringExact(dt.Rows[0]["evDateFrom"].ToString().Substring(0, 4));
                    //cbEDay.SelectedIndex = int.Parse(dt.Rows[0]["evDateFrom"].ToString().Substring(8, 2)) - 1;
                    //cbEYear2.SelectedIndex = cbEYear2.FindStringExact(dt.Rows[0]["evDateTo"].ToString().Substring(0, 4)); cbEMonth2.SelectedIndex = int.Parse(dt.Rows[0]["evDateTo"].ToString().Substring(5, 2)); cbEDay2.SelectedIndex = int.Parse(dt.Rows[0]["evDateTo"].ToString().Substring(8, 2));
                    txtEHours.Text = dt.Rows[0]["evTimeFrom"].ToString().Substring(0, 2); txtEMins.Text = dt.Rows[0]["evTimeFrom"].ToString().Substring(3, 2);
                    txtEHours2.Text = dt.Rows[0]["evTimeTo"].ToString().Substring(0, 2); txtEMins2.Text = dt.Rows[0]["evTimeTo"].ToString().Substring(3, 2);
                    if (dt.Rows[0]["evTimeFrom"].ToString().Substring(6, 2) == "AM") btnAM.PerformClick();
                    else if (dt.Rows[0]["evTimeFrom"].ToString().Substring(6, 2) == "PM") btnPM.PerformClick();
                    if (dt.Rows[0]["evTimeTo"].ToString().Substring(6, 2) == "AM") btnAM2.PerformClick();
                    else if (dt.Rows[0]["evTimeTo"].ToString().Substring(6, 2) == "PM") btnPM2.PerformClick();
                    //cb_MRemind.SelectedIndex = cbEMonth.SelectedIndex = int.Parse(dt.Rows[0]["reminderDate"].ToString().Substring(0, 2)); cb_YRemind.SelectedIndex = cbEYear.FindStringExact(dt.Rows[0]["reminderDate"].ToString().Substring(6, 4)); cb_DRemind.SelectedIndex = int.Parse(dt.Rows[0]["reminderDate"].ToString().Substring(3, 2));
                    //txtHrRemind.Text = dt.Rows[0]["reminderTime"].ToString().Substring(0, 2); txtMinRemind.Text = dt.Rows[0]["reminderTime"].ToString().Substring(3, 2);
                    if (dt.Rows[0]["reminderTime"].ToString().Substring(6, 2) == "AM") btnAMRemind.PerformClick();
                    else if (dt.Rows[0]["reminderTime"].ToString().Substring(6, 2) == "PM") btnPMRemind.PerformClick();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("evedit Details error" + ee);
                conn.Close();
            }
        }
        public void viewDataEdit()
        {
            //MessageBox.Show(editName);
            editListView.Items.Clear();
            for (int i = 0; i < 7; i++)
            {
                ListViewItem itm = new ListViewItem(desc[i]);
                itm.SubItems.Add(value[i]);
                editListView.Items.Add(itm);
            }
        }

        public bool sameEvName(string name)
        {
            bool check = false;
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT * FROM event WHERE evname = '" + name + "'", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                conn.Close();
                if (dt.Rows.Count > 0) check = false;
                else check = true;
            }
            catch (Exception ee)
            {
                MessageBox.Show("Nah!" + ee);
                conn.Close();
            }
            return check;
        }

        public void updateEvent(int insnum)
        {
            int monthfrom = 0, monthto = 0;
            string eventtime = "", datefrom = "", timeTo = "", dateTo = "";
            if (btnRange == "one")
            {
                if (!allDayState)
                {
                    monthfrom = Array.IndexOf(aMonths, cbEMonth.Text) + 1;
                    monthto = Array.IndexOf(aMonths, cbEMonth2.Text) + 1;
                    eventtime = txtEHours.Text + ":" + txtEMins.Text + " " + ampmFrom;
                    datefrom = cbEYear.Text + "-" + monthfrom.ToString("00") + "-" + cbEDay.Text;
                    timeTo = txtEHours2.Text + ":" + txtEMins2.Text + " " + ampmTo;
                    dateTo = cbEYear2.Text + "-" + monthto.ToString("00") + "-" + cbEDay2.Text;
                }
                else
                {
                    monthfrom = Array.IndexOf(aMonths, cbEMonth.Text) + 1;
                    monthto = Array.IndexOf(aMonths, cbEMonth.Text) + 1;
                    eventtime = txtEHours.Text + ":" + txtEMins.Text + " " + ampmFrom;
                    datefrom = cbEYear.Text + "-" + monthfrom.ToString("00") + "-" + cbEDay.Text;
                    timeTo = txtEHours2.Text + ":" + txtEMins2.Text + " " + ampmTo;
                    dateTo = cbEYear.Text + "-" + monthto.ToString("00") + "-" + cbEDay.Text;
                }
            }
            else if (btnRange == "multi")
            {
                monthfrom = Array.IndexOf(aMonths, cbEMonth.Text) + 1;
                monthto = Array.IndexOf(aMonths, cbEMonth2.Text) + 1;
                eventtime = txtEHours.Text + ":" + txtEMins.Text + " " + ampmFrom;
                datefrom = cbEYear.Text + "-" + monthfrom.ToString("00") + "-" + cbEDay.Text;
                timeTo = txtEHours2.Text + ":" + txtEMins2.Text + " " + ampmTo;
                dateTo = cbEYear2.Text + "-" + monthto.ToString("00") + "-" + cbEDay2.Text;
            }
            int monthR = Array.IndexOf(aMonths, cb_MRemind.Text) + 1;
            string reminddate = monthR.ToString("00") + "/" + cb_DRemind.Text + "/" + cb_YRemind.Text;
            string remindtime = txtHrRemind.Text + ":" + txtMinRemind.Text + " " + ampmR;
            try
            {
                MessageBox.Show(insnum + " " + evidEdit);
                conn.Open();
                MySqlCommand comm = new MySqlCommand();
                if (insnum == 0) comm = new MySqlCommand("UPDATE event SET evName ='" + txtEventName.Text + "', evDesc ='" + txtEventDes.Text + "', evDateFrom ='" + datefrom + "', evTimeFrom ='" + eventtime + "', evVenue ='" + txtVenue.Text + "', evProgress ='Pending', evType ='" + cbEType.Text + "', attendance ='False' , status ='Pending' " + "', evDateTo ='" + dateTo + "', evTimeTo='" + timeTo + "' WHERE eventID = '"+ evidEdit +"';", conn); //insert
                else if (insnum == 1) comm = new MySqlCommand("UPDATE event SET evName ='" + txtEventName.Text + "', evDesc ='" + txtEventDes.Text + "', evDateFrom ='" + datefrom + "', evTimeFrom ='" + eventtime + "', evVenue ='" + txtVenue.Text + "', evProgress ='Pending', evType ='" + cbEType.Text + "' , status ='Pending', reminderDate ='" + reminddate + "', reminderTime='" + remindtime + "', attendance='False' , budget='True', reminder='True', evDateTo='" + dateTo + "', evTimeTo='" + timeTo + "' WHERE eventID = '" + evidEdit + "';", conn);//insert with both
                else if (insnum == 2) comm = new MySqlCommand("UPDATE event SET evName ='" + txtEventName.Text + "', evDesc ='" + txtEventDes.Text + "', evDateFrom ='" + datefrom + "', evTimeFrom ='" + eventtime + "', evVenue ='" + txtVenue.Text + "', evProgress ='Pending', evType ='" + cbEType.Text + "', status ='Pending', attendance ='False' , reminder='True', evDateTo='" + dateTo + "', evTimeTo='" + timeTo + "', reminderDate ='" + reminddate + "', reminderTime='" + remindtime + "' WHERE eventID = '" + evidEdit + "';", conn); // insert with remind
                else if (insnum == 3) comm = new MySqlCommand("UPDATE event SET evName ='" + txtEventName.Text + "', evDesc ='" + txtEventDes.Text + "', evDateFrom ='" + datefrom + "', evTimeFrom ='" + eventtime + "', evVenue ='" + txtVenue.Text + "', evProgress ='Pending', evType ='" + cbEType.Text + "', status ='Pending', attendance ='False' , budget='True', evDateTo='" + dateTo + "', evTimeTo='" + timeTo + "' WHERE eventID = '" + evidEdit + "';", conn); // insert with budget
                comm.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show("" + ee);
                conn.Close();
            }
        }
        private void editListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (editListView.SelectedItems[0].SubItems[0].Text == "Event Name")
            {
                txtEventName.Enabled = true;
                txtVenue.Enabled = false;
                txtEventDes.Enabled = false;
                cbEType.Enabled = false;
                tcEdit.SelectedTab = txtEdit;
            }
            else if (editListView.SelectedItems[0].SubItems[0].Text == "Event Venue")
            {
                txtEventName.Enabled = false;
                txtVenue.Enabled = true;
                txtEventDes.Enabled = false;
                cbEType.Enabled = false;
                tcEdit.SelectedTab = txtEdit;
            }
            else if (editListView.SelectedItems[0].SubItems[0].Text == "Event Description")
            {
                txtEventName.Enabled = false;
                txtVenue.Enabled = false;
                txtEventDes.Enabled = true;
                cbEType.Enabled = false;
                tcEdit.SelectedTab = txtEdit;
            }
            else if (editListView.SelectedItems[0].SubItems[0].Text == "Event Type")
            {
                txtEventName.Enabled = false;
                txtVenue.Enabled = false;
                txtEventDes.Enabled = false;
                cbEType.Enabled = true;
                tcEdit.SelectedTab = txtEdit;
            }
            else if (editListView.SelectedItems[0].SubItems[0].Text == "Event Date And Time") tcEdit.SelectedTab = dateEdit;
            else tcEdit.SelectedTab = rebudgEdit;
        }
        #endregion 

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EvEditDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            reftoevorg.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            confirmMessage("Are you sure you want to edit records? Please double check edited records.");
            if (confirmed)
            {
                //update function
                int insnum = 0;
                if (remindYN == "yes" && budgetYN == "yes") insnum = 1;
                else if (remindYN == "yes" && budgetYN == "no") insnum = 2;
                else if (remindYN == "no" && budgetYN == "yes") insnum = 3;
                else insnum = 0;
                updateEvent(insnum);
                this.DialogResult = DialogResult.OK;
            }
        }

        
        #region button edit 
        //name, venue, description type
        private void btnEName_Click(object sender, EventArgs e)
        {
            txtEventName.Enabled = true;
        }

        private void btnEVen_Click(object sender, EventArgs e)
        {
            txtVenue.Enabled = true;
        }

        private void btnEDes_Click(object sender, EventArgs e)
        {
            txtEventDes.Enabled = true;
        }

        private void btnEType_Click(object sender, EventArgs e)
        {
            cbEType.Enabled = true;
        }
        #endregion

        #region textbox counters
        private void txtEventName_TextChanged(object sender, EventArgs e)
        {
            int count = txtEventName.Text.Length;
            countEName.Text = count + "/100";
        }

        private void txtVenue_TextChanged(object sender, EventArgs e)
        {
            int count = txtVenue.Text.Length;
            countEVenue.Text = count + "/100";
        }

        private void txtEventDes_TextChanged(object sender, EventArgs e)
        {
            int count = txtEventDes.Text.Length;
            countEDes.Text = count + "/100";
        }
        #endregion

        #region textboxes set
        private void txtEventName_Enter(object sender, EventArgs e)
        {
            resetLabelsPanels(); resetCounters();
            txtEventName.ForeColor = Color.Black;
            txtEventName.Text = "";
            panelEName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_green;
            lblEventName.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            countEName.Visible = true;
        }

        private void txtVenue_Enter(object sender, EventArgs e)
        {
            resetLabelsPanels(); resetCounters();
            txtVenue.ForeColor = Color.Black;
            txtVenue.Text = "";
            panelEVenue.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_green;
            lblEVenue.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            countEVenue.Visible = true;
        }

        private void txtEventDes_Enter(object sender, EventArgs e)
        {
            resetLabelsPanels(); resetCounters();
            txtEventDes.ForeColor = Color.Black;
            txtEventDes.Text = "";
            lblEDes.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            countEDes.Visible = true;
        }

        private void txtEventName_Leave(object sender, EventArgs e)
        {
            resetLabelsPanels(); resetCounters();
            if (txtEventName.Text.Equals("")) txtEventName.Text = editName;
            else
            {
                if (!sameEvName(txtEventName.Text)) 
                {
                    errorMessage("The event name is already present");
                    txtEventName.Text = "What is the name of the event?";
                }
            }
            txtEventName.ForeColor = System.Drawing.ColorTranslator.FromHtml("#878787");
        }

        private void txtVenue_Leave(object sender, EventArgs e)
        {
            resetLabelsPanels(); resetCounters();
            txtVenue.ForeColor = System.Drawing.ColorTranslator.FromHtml("#878787");
            if (txtVenue.Text.Equals("")) txtVenue.Text = editVenue;
        }

        private void txtEventDes_Leave(object sender, EventArgs e)
        {
            resetLabelsPanels(); resetCounters();
            txtEventDes.ForeColor = System.Drawing.ColorTranslator.FromHtml("#878787");
            if (txtEventDes.Text.Equals("")) txtEventDes.Text = editDes;
        }
        private void cbEType_Enter(object sender, EventArgs e)
        {
            resetLabelsPanels();
            lblEType.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
        }
        #endregion

        #region date combobox validation
        //leap year
        public void dateFromInitial(int month, int year)
        {
            cbEDay.Items.Clear();

            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                cbEDay.Items.Add("" + i.ToString("00") + "");
            }
        }

        public void dateToInitial(int month, int year)
        {
            cbEDay2.Items.Clear();

            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                cbEDay2.Items.Add("" + i.ToString("00") + "");
            }
        }
        private void cbEMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbEMonth2.SelectedIndex = cbEMonth.SelectedIndex;
        }

        private void cbEDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEDay.SelectedItem == null) cbEDay.SelectedIndex = 0;
            if (btnRange == "one")
            {
                //MessageBox.Show("" + (cbd2 - cbd).TotalDays);//datetime datatype can be subtracted and get tht total days or month or years or the difference
                if (cbEMonth.SelectedIndex == cbEMonth.Items.Count - 1 && cbEDay.SelectedIndex == cbEDay.Items.Count - 1)
                {
                    cbEYear2.SelectedIndex = cbEYear.SelectedIndex + 1;
                    cbEMonth2.SelectedIndex = 0;
                    cbEDay2.SelectedIndex = 0;
                }
                else if (cbEDay.SelectedIndex == cbEDay.Items.Count - 1)
                {
                    cbEMonth2.SelectedIndex = cbEMonth.SelectedIndex + 1;
                    cbEDay2.SelectedIndex = 0;
                }
                else
                {
                    cbEYear2.SelectedIndex = cbEYear.SelectedIndex;
                    cbEMonth2.SelectedIndex = cbEMonth.SelectedIndex;
                    cbEDay2.SelectedIndex = cbEDay.SelectedIndex + 1;
                }
            }
            else if (btnRange == "multi")
            {
                cbEDay2.SelectedIndex = cbEDay.SelectedIndex;
            }
        }

        private void cbEYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbEYear2.SelectedIndex = cbEYear.SelectedIndex;
        }

        private void cbEMonth2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEMonth2.SelectedIndex < cbEMonth.SelectedIndex && cbEYear.SelectedIndex == cbEYear2.SelectedIndex)
            {
                errorMessage("You cannot set this to an earlier month.");
                cbEMonth2.SelectedIndex = cbEMonth.SelectedIndex;
            }
            int num = Array.IndexOf(aMonths, cbEMonth2.Text) + 1;
            dateToInitial(num, int.Parse(cbEYear2.Text));
        }

        private void cbEDay2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEDay2.SelectedItem == null) cbEDay2.SelectedIndex = cbEDay.SelectedIndex;
        }

        private void cbEYear2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEYear2.SelectedIndex < cbEYear.SelectedIndex)
            {
                errorMessage("You cannot set this to an earlier year.");
                cbEYear2.SelectedIndex = cbEYear.SelectedIndex;
            }
        }
        //change committed
        private void cbEMonth_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int num = Array.IndexOf(aMonths, cbEMonth.Text) + 1;
            dateFromInitial(num, int.Parse(cbEYear.Text));
        }

        private void cbEDay_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbEDay.SelectedItem.Equals("")) cbEDay.SelectedIndex = 0;
        }

        private void cbEYear_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int num = Array.IndexOf(aMonths, cbEMonth.Text) + 1;
            dateFromInitial(num, int.Parse(cbEYear.Text));
        }

        private void cbEMonth2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int num = Array.IndexOf(aMonths, cbEMonth2.Text) + 1;
            dateToInitial(num, int.Parse(cbEYear2.Text));
        }

        private void cbEDay2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbEDay2.SelectedItem.Equals("")) cbEDay2.SelectedIndex = cbEDay.SelectedIndex;
        }

        private void cbEYear2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int num = Array.IndexOf(aMonths, cbEMonth2.Text) + 1;
            dateToInitial(num, int.Parse(cbEYear2.Text));
        }
        public void dateRemindInitial(int month, int year)
        {
            //MessageBox.Show("m " +  month + " y " + year);
            cb_DRemind.Items.Clear();
            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                cb_DRemind.Items.Add("" + i.ToString("00") + "");
            }
        }
        private void cb_MRemind_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int num = Array.IndexOf(aMonths, cb_MRemind.Text) + 1;
            //MessageBox.Show("mremind" + num);
            dateRemindInitial(num, int.Parse(cb_YRemind.Text));
        }

        private void cb_YRemind_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int num = Array.IndexOf(aMonths, cb_MRemind.Text) + 1;
            //MessageBox.Show("yremind" + num);
            dateRemindInitial(num, int.Parse(cb_YRemind.Text));
        }
        #endregion

        #region time textbox
        private void txtEHours_Leave(object sender, EventArgs e)
        {
            if (txtEHours.Text == "")
            {
                txtEHours.Text = "00";
                txtEHours.Focus();
            }
            else if (int.Parse(txtEHours.Text) > 12 || int.Parse(txtEHours.Text) <= 0) // bai what if nagleave tapos empty ang textbox magerror ang int.parse
            {
                errorMessage("You cannot set the hours beyond 12 or less than 0.");
                txtEHours.Text = "00";
                txtEHours.Focus();
            }
            else if (int.Parse(txtEHours.Text) < 10 && txtEHours.TextLength < 2) txtEHours.Text = "0" + txtEHours.Text;
        }

        private void txtEMins_Leave(object sender, EventArgs e)
        {
            if (txtEMins.Text == "")
            {
                txtEMins.Text = "00";
                txtEMins.Focus();
            }
            else if (int.Parse(txtEMins.Text) > 59 || int.Parse(txtEMins.Text) < 0)
            {
                errorMessage("You cannot set the minutes beyond 59 or less than 0.");
                txtEMins.Text = "00";
                txtEMins.Focus();
            }
            else if (int.Parse(txtEMins.Text) < 10 && txtEMins.TextLength < 2) txtEMins.Text = "0" + txtEMins.Text;
        }

        private void txtEHours2_Leave(object sender, EventArgs e)
        {
            if (txtEHours2.Text == "")
            {
                txtEHours2.Text = "00";
                txtEHours2.Focus();
            }
            else if (int.Parse(txtEHours2.Text) > 12 || int.Parse(txtEHours2.Text) <= 0)
            {
                errorMessage("You cannot set the hours beyond 12 or less than 0.");
                txtEHours2.Text = "00";
                txtEHours2.Focus();
            }
            else if (int.Parse(txtEHours2.Text) < 10 && txtEHours2.TextLength < 2) txtEHours2.Text = "0" + txtEHours2.Text;
        }

        private void txtEMins2_Leave(object sender, EventArgs e)
        {
            if (txtEMins.Text == "")
            {
                txtEMins2.Text = "00";
                txtEMins2.Focus();
            }
            else if (int.Parse(txtEMins2.Text) > 59 || int.Parse(txtEMins2.Text) < 0)
            {
                errorMessage("You cannot set the minutes beyond 59 or less than 0.");
                txtEMins2.Text = "00";
                txtEMins2.Focus();
            }
            else if (int.Parse(txtEMins2.Text) < 10 && txtEMins2.TextLength < 2) txtEMins2.Text = "0" + txtEMins2.Text;
        }

        private void btnAM_Click(object sender, EventArgs e)
        {
            ampmFrom = "AM";
            btnAM.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            btnPM.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
        }

        private void btnPM_Click(object sender, EventArgs e)
        {
            ampmFrom = "PM";
            btnPM.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            btnAM.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
        }

        private void btnAM2_Click(object sender, EventArgs e)
        {
            ampmTo = "AM";
            btnAM2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            btnPM2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
        }

        private void btnPM2_Click(object sender, EventArgs e)
        {
            ampmTo = "PM";
            btnPM2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            btnAM2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
        }

        private void txtEdit_Click(object sender, EventArgs e)
        {

        }

        private void txtHrRemind_Leave(object sender, EventArgs e)
        {
            if (int.Parse(txtHrRemind.Text) > 12 || int.Parse(txtHrRemind.Text) <= 0)
            {
                errorMessage("You cannot set the hours beyond 12 or less than 0.");
                txtHrRemind.Text = "00";
                txtHrRemind.Focus();
            }
            else if (int.Parse(txtHrRemind.Text) < 10 && txtHrRemind.TextLength < 2) txtHrRemind.Text = "0" + txtHrRemind.Text;

        }

        private void txtMinRemind_Leave(object sender, EventArgs e)
        {
            if (int.Parse(txtMinRemind.Text) > 59 || int.Parse(txtMinRemind.Text) < 0)
            {
                errorMessage("You cannot set the minutes beyond 59 or less than 0.");
                txtMinRemind.Text = "00";
                txtMinRemind.Focus();
            }
            else if (int.Parse(txtMinRemind.Text) < 10 && txtMinRemind.TextLength < 2) txtMinRemind.Text = "0" + txtMinRemind.Text;
        }

        private void btnAMRemind_Click(object sender, EventArgs e)
        {
            ampmR = "AM";
            btnAMRemind.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            btnPMRemind.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
        }

        private void btnPMRemind_Click(object sender, EventArgs e)
        {
            ampmR = "PM";
            btnPMRemind.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            btnAMRemind.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
        }
        #endregion 

        #region back buttons
        private void btnBack_Click(object sender, EventArgs e)
        {
            editName = txtEventName.Text; value[0] = txtEventName.Text;
            editVenue = txtVenue.Text; value[1] = txtVenue.Text;
            editDes = txtEventDes.Text; value[2] = txtEventDes.Text;
            editType = cbEType.Text; value[3] = cbEType.Text;
            viewDataEdit();
            tcEdit.SelectedIndex = 0;
        }
        private void btnBack2_Click(object sender, EventArgs e)
        {
            editDate = cbEYear.Text + "-" + cbEMonth.Text + "-" + cbEDay.Text + " " + txtEHours.Text + ":" + txtEMins.Text + " " + ampmFrom +
                        cbEYear2.Text + "-" + cbEMonth2.Text + "-" + cbEDay2.Text + " " + txtEHours2.Text + ":" + txtEMins2.Text + " " + ampmTo;
            value[4] = editDate;
            viewDataEdit();
            tcEdit.SelectedIndex = 1;
            
        }
        private void btnBack3_Click(object sender, EventArgs e)
        {
            editRemind = cb_YRemind.Text + "-" + cb_MRemind.Text + "-" + cb_DRemind.Text + " " + txtHrRemind.Text + ":" + txtMinRemind.Text + " " + ampmR;
            value[5] = editRemind;
            viewDataEdit();
            tcEdit.SelectedIndex = 2;
        }
        #endregion 

        #region day buttons
        public string btnRange = "";
        private void btnAllDay_Click(object sender, EventArgs e)
        {
            btnRange = "one";
            resetDayButtons();
            btnAllDay.ForeColor = Color.White;
            btnAllDay.BackColor = System.Drawing.ColorTranslator.FromHtml("#5ea6e9");
            btnAllDay.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#5ea6e9");
            cbEMonth.Enabled = true; cbEDay.Enabled = true; cbEYear.Enabled = true;
            cbEMonth2.Visible = false; cbEDay2.Visible = false; cbEYear2.Visible = false;
            lbAllDay.Visible = true; btnRAllDay.Visible = true;
            lbFrom.Visible = false; lbTo.Visible = false;
        }
        private void btnMulDay_Click(object sender, EventArgs e)
        {
            btnRange = "multi";
            resetDayButtons();
            btnMulDay.ForeColor = Color.White;
            btnMulDay.BackColor = System.Drawing.ColorTranslator.FromHtml("#5ea6e9");
            btnMulDay.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#5ea6e9");
            cbEMonth.Enabled = true; cbEDay.Enabled = true; cbEYear.Enabled = true;
            cbEMonth2.Enabled = true; cbEDay2.Enabled = true; cbEYear2.Enabled = true;
            cbEMonth2.Visible = true; cbEDay2.Visible = true; cbEYear2.Visible = true;
            lbAllDay.Visible = false; btnRAllDay.Visible = false;
            lbFrom.Visible = true; lbTo.Visible = true;
            panelEHours2.Visible = true; panelEMins2.Visible = true; lblColon2.Visible = true; btnAM2.Visible = true; btnPM2.Visible = true;
            //btnRAllDay
            lbAllDay.ForeColor = Color.FromArgb(42, 42, 42);
            btnRAllDay.BackgroundImage = global::BalayPasilungan.Properties.Resources.off;
            allDayState = false;
            cbEDay2.SelectedIndex = cbEDay.SelectedIndex;
            txtEHours.Text = "00"; txtEHours2.Text = "00";
            btnAM.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
            btnAM2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
            ampmFrom = ""; ampmTo = "";
        }
        #endregion

        #region btn switches
        private void btnRAllDay_Click(object sender, EventArgs e)
        {
            if (!allDayState)
            {
                lbAllDay.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
                btnRAllDay.BackgroundImage = global::BalayPasilungan.Properties.Resources.on;
                allDayState = true;
                if (cbEMonth.SelectedIndex == cbEMonth.Items.Count - 1 && cbEDay.SelectedIndex == cbEDay.Items.Count - 1)
                {
                    cbEYear2.SelectedIndex = cbEYear.SelectedIndex + 1;
                    cbEMonth2.SelectedIndex = 0;
                    cbEDay2.SelectedIndex = 0;
                }
                else if (cbEDay.SelectedIndex == cbEDay.Items.Count - 1)
                {
                    cbEMonth2.SelectedIndex = cbEMonth.SelectedIndex + 1;
                    cbEDay2.SelectedIndex = 0;
                }
                else
                {
                    cbEYear2.SelectedIndex = cbEYear.SelectedIndex;
                    cbEMonth2.SelectedIndex = cbEMonth.SelectedIndex;
                    cbEDay2.SelectedIndex = cbEDay.SelectedIndex + 1;
                }
                txtEHours.Text = "12"; txtEHours2.Text = "12";
                btnAM.PerformClick(); btnAM2.PerformClick();
                lbFrom.Visible = true; lbTo.Visible = true;
                cbEDay2.Visible = true; cbEMonth2.Visible = true; cbEYear2.Visible = true;
                cbEDay2.Enabled = false; cbEMonth2.Enabled = false; cbEYear2.Enabled = false;
                panelEHours2.Visible = true; panelEMins2.Visible = true; lblColon2.Visible = true; btnAM2.Visible = true; btnPM2.Visible = true;
            }
            else
            {
                lbAllDay.ForeColor = Color.FromArgb(42, 42, 42);
                btnRAllDay.BackgroundImage = global::BalayPasilungan.Properties.Resources.off;
                allDayState = false;
                cbEDay2.SelectedIndex = cbEDay.SelectedIndex;
                txtEHours.Text = "00"; txtEHours2.Text = "00";
                btnAM.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
                btnAM2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
                ampmFrom = ""; ampmTo = "";
                lbFrom.Visible = false; lbTo.Visible = false;
                cbEDay2.Visible = false; cbEMonth2.Visible = false; cbEYear2.Visible = false;
            }
        }
        private void btnRemind_Click(object sender, EventArgs e)
        {
            if (!remindState)
            {
                lblNo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
                lblYes.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
                btnRemind.BackgroundImage = global::BalayPasilungan.Properties.Resources.on;
                remindState = true;
                remindYN = "yes";
                reminderPanel.Visible = true;
            }
            else
            {
                lblYes.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
                lblNo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
                btnRemind.BackgroundImage = global::BalayPasilungan.Properties.Resources.off;
                remindState = false;
                remindYN = "no";
                reminderPanel.Visible = false;
            }
        }
        private void btnBudget_Click(object sender, EventArgs e)
        {
            lblQuestion.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a");
            lblBudgetAsk.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            if (!budgetState)
            {
                lblNo2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
                lblYes2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
                btnBudget.BackgroundImage = global::BalayPasilungan.Properties.Resources.on;
                budgetState = true;
                btnAddBudget.Visible = true;
                budgetYN = "yes";
            }
            else
            {
                lblYes2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
                lblNo2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
                btnBudget.BackgroundImage = global::BalayPasilungan.Properties.Resources.off;
                budgetState = false;
                btnAddBudget.Visible = false;
                budgetYN = "no";
            }
        }
        #endregion

        #region btn next
        private void btnNext_Click(object sender, EventArgs e)
        {
            tcEdit.SelectedIndex = 2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tcEdit.SelectedIndex = 3;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            editName = txtEventName.Text; value[0] = txtEventName.Text;
            editVenue = txtVenue.Text; value[1] = txtVenue.Text;
            editDes = txtEventDes.Text; value[2] = txtEventDes.Text;
            editType = cbEType.Text; value[3] = cbEType.Text;
            editDate = cbEYear.Text + "-" + cbEMonth.Text + "-" + cbEDay.Text + " " + txtEHours.Text + ":" + txtEMins.Text + " " + ampmFrom +
                        cbEYear2.Text + "-" + cbEMonth2.Text + "-" + cbEDay2.Text + " " + txtEHours2.Text + ":" + txtEMins2.Text + " " + ampmTo;
            value[4] = editDate;
            editRemind = cb_YRemind.Text + "-" + cb_MRemind.Text + "-" + cb_DRemind.Text + " " + txtHrRemind.Text + ":" + txtMinRemind.Text + " " + ampmR;
            value[5] = editRemind;
            viewDataEdit();
            tcEdit.SelectedIndex = 0;
        }
        #endregion
    }
}
