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
    public partial class eventorg : Form
    {
        public MySqlConnection conn;
        public string[] aMonths = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        public bool remindState = false, budgetState = false, allDayState = false, timeRngState = false;
        public main reftomain { get; set; }
        public int usertype { get; set; }

        public eventorg()
        {
            InitializeComponent();
            tabSecond.SelectedTab = tabCalendar;
            conn = new MySqlConnection("Server=localhost;Database=prototype_sad;Uid=root;Pwd=root;");            
            menuStrip2.Renderer = new renderer();
            menuStripEvent.Renderer = new renderer();
            ERProgress.Renderer = new renderer2();
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

        #region Error, Confirm, and Success
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

        public bool confirmed;

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

        #region Functions
        private void resetDayButtons()
        {
            btnAllDay.BackColor = Color.White;
            btnMulDay.BackColor = Color.White;
            btnAllDay.ForeColor = System.Drawing.ColorTranslator.FromHtml("#8f8f8f");
            btnMulDay.ForeColor = System.Drawing.ColorTranslator.FromHtml("#8f8f8f");
            btnAllDay.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#8f8f8f");
            btnMulDay.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#8f8f8f");
        }

        private void resetTS()
        {
            //eventTS.BackColor = System.Drawing.ColorTranslator.FromHtml("#393939");
            //attendanceTS.BackColor = System.Drawing.ColorTranslator.FromHtml("#393939");
        }

        private void resetLabelsPanels()                // Set label and panel colors to default (gray)
        {
            lblEventName.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a");            
            lblEVenue.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a");            
            lblEType.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a");
            lblEDes.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a"); 
                      
            lblEDate.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a");

            lblRequestBy.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a");  

            panelEName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
            panelEVenue.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
            panelRequestBy.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
        }        

        public void adjustCustom(int type, String now)
        {
            if(type == 0)
            {
                btnMNow.Text = now;
                int i = Array.IndexOf(aMonths, now);
                if (i == 11)   // December
                {
                    btnMNext.Text = aMonths[0];
                    btnMPrev.Text = aMonths[i - 1];
                }
                else if (i == 0)    // January
                {
                    btnMNext.Text = aMonths[i + 1];
                    btnMPrev.Text = aMonths[11];
                }
                else
                {
                    btnMNext.Text = aMonths[i + 1];
                    btnMPrev.Text = aMonths[i - 1];
                }
            }
            else
            {
                btnYNow.Text = now;
                if (int.Parse(now) == 1960)
                {
                    btnYPrev.Text = "";
                    btnYPrev.Enabled = false;
                    btnYNext.Text = (int.Parse(now) + 1).ToString();
                }
                else if(int.Parse(now) == 2099)
                {
                    btnYNext.Text = "";
                    btnYNext.Enabled = false;
                    btnYPrev.Text = (int.Parse(now) - 1).ToString();
                }
                else
                {                    
                    btnYNext.Text = (int.Parse(now) + 1).ToString();
                    btnYPrev.Text = (int.Parse(now) - 1).ToString();
                }
            }
        }

        private void btnNewEventForm_Click(object sender, EventArgs e)          // Clear textboxes and reset form  
        {
            resetLabelsPanels();
            txtEventName.ForeColor = System.Drawing.ColorTranslator.FromHtml("#878787");
            txtEventName.Text = "What is the name of the event?";
            txtEventDes.ForeColor = System.Drawing.ColorTranslator.FromHtml("#878787");
            txtEventDes.Text = "Describe the event.";
            txtVenue.ForeColor = System.Drawing.ColorTranslator.FromHtml("#878787");
            txtVenue.Text = "Where will it be held?";

            countEDes.Visible = false; countEName.Visible = false; countEVenue.Visible = false; countRequestBy.Visible = false;

            // Find code to unclick button
        }
        #endregion

        #region Main Buttons
        private void taskbar_Click(object sender, EventArgs e)
        {
            btnMain.ForeColor = btnEvent.ForeColor = btnRequest.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            logo_main.BackgroundImage = Properties.Resources.main_fade;
            logo_events.BackgroundImage = Properties.Resources.event_fade;
            logo_request.BackgroundImage = Properties.Resources.request_fade;
            ((Button)sender).ForeColor = System.Drawing.Color.FromArgb(15, 168, 104);
            if (((Button)sender).Name == "btnEvent")
            {
                logo_events.BackgroundImage = Properties.Resources._event;
                
                //menuStrip.Height = 0;
                timer1.Enabled = true;

                tabSecond.SelectedTab = tabCalendar;
                resetTS();
                //eventTS.BackColor = System.Drawing.ColorTranslator.FromHtml("#2d2d2d");
                
                allEvents();
                calendarcolor();
            }
            else if (((Button)sender).Name == "btnRequest")
            {
                logo_request.BackgroundImage = Properties.Resources.request;

                tabSecond.SelectedTab = tabRequest;
                dateFromInitial(DateTime.Now.Month, DateTime.Now.Year);
                dateToInitial(DateTime.Now.Month, DateTime.Now.Year);
                dateRemindInitial(DateTime.Now.Month, DateTime.Now.Year);
                cbEYear.SelectedIndex = cbEYear.FindStringExact(DateTime.Now.Year.ToString()); cbEMonth.SelectedIndex = DateTime.Now.Month - 1; cbEDay.SelectedIndex = DateTime.Now.Day - 1;
                cbEYear2.SelectedIndex = cbEYear2.FindStringExact(DateTime.Now.Year.ToString()); cbEMonth2.SelectedIndex = DateTime.Now.Month - 1; cbEDay2.SelectedIndex = DateTime.Now.Day - 1;
                cb_MRemind.SelectedIndex = DateTime.Now.Month - 1; cb_YRemind.SelectedIndex = cb_YRemind.FindStringExact(DateTime.Now.Year.ToString()); cb_DRemind.SelectedIndex = DateTime.Now.Day - 1;
                clrTabReqF();
            }
            else
            {
                logo_main.BackgroundImage = Properties.Resources.main;
                // BACK TO MAIN
                confirmMessage("Are you sure you want to exit?");
                if (confirmed) this.Close();
            }
        }

        private void logo_click(object sender, EventArgs e)
        {
            btnMain.ForeColor = btnEvent.ForeColor = btnRequest.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            logo_main.BackgroundImage = Properties.Resources.main_fade;
            logo_events.BackgroundImage = Properties.Resources.event_fade;
            logo_request.BackgroundImage = Properties.Resources.request_fade;
            if (((PictureBox)sender).Name == "logo_events") 
            {
                btnEvent.ForeColor = System.Drawing.Color.FromArgb(15, 168, 104);
                logo_events.BackgroundImage = Properties.Resources._event;

                //menuStrip.Height = 0;
                timer1.Enabled = true;

                tabSecond.SelectedTab = tabCalendar;
                resetTS();
                //eventTS.BackColor = System.Drawing.ColorTranslator.FromHtml("#2d2d2d");
                
                allEvents();
                calendarcolor();
            }
            else if (((PictureBox)sender).Name == "logo_request")
            {
                btnRequest.ForeColor = System.Drawing.Color.FromArgb(15, 168, 104);
                logo_request.BackgroundImage = Properties.Resources.request;
                tabSecond.SelectedTab = tabRequest;
                dateFromInitial(DateTime.Now.Month, DateTime.Now.Year);
                dateToInitial(DateTime.Now.Month, DateTime.Now.Year);
                dateRemindInitial(DateTime.Now.Month, DateTime.Now.Year);
                cbEYear.SelectedIndex = cbEYear.FindStringExact(DateTime.Now.Year.ToString()); cbEMonth.SelectedIndex = DateTime.Now.Month - 1; cbEDay.SelectedIndex = DateTime.Now.Day - 1;
                cbEYear2.SelectedIndex = cbEYear2.FindStringExact(DateTime.Now.Year.ToString()); cbEMonth2.SelectedIndex = DateTime.Now.Month - 1; cbEDay2.SelectedIndex = DateTime.Now.Day - 1;
                cb_MRemind.SelectedIndex = DateTime.Now.Month - 1; cb_YRemind.SelectedIndex = cb_YRemind.FindStringExact(DateTime.Now.Year.ToString()); cb_DRemind.SelectedIndex = DateTime.Now.Day - 1;
                clrTabReqF();
            }
            else
            {
                btnMain.ForeColor = System.Drawing.Color.FromArgb(15, 168, 104);
                logo_main.BackgroundImage = Properties.Resources.main;
                confirmMessage("Are you sure you want to exit?");
                if (confirmed) this.Close();
                // BACK TO MAIN
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
        #endregion

        #region Timers
        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (menuStrip.Height >= 35) timer1.Enabled = false;
            //else menuStrip.Height += 3;            
        }
        private void remindTimer_Tick(object sender, EventArgs e)
        {
            if (reminderPanel.Height >= 171) remindTimer.Enabled = false;
            else reminderPanel.Height += 19;
        }
        #endregion

        #region Menu Strip
        private void attendanceTS_Click(object sender, EventArgs e)
        {
            resetTS();
            //attendanceTS.BackColor = System.Drawing.ColorTranslator.FromHtml("#2d2d2d");
            tabSecond.SelectedTab = tabAttend;
        }

        private void eventTS_Click(object sender, EventArgs e)
        {
            resetTS();
            //eventTS.BackColor = System.Drawing.ColorTranslator.FromHtml("#2d2d2d");
        }

        private class renderer : ToolStripProfessionalRenderer
        {
            public renderer() : base(new cols()) { }
        }

        private class cols : ProfessionalColorTable
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
       
        private void eventTS2_Click(object sender, EventArgs e)
        {
            tabSecond.SelectedTab = tabEvent;
            resetTS();
            //eventTS.BackColor = System.Drawing.ColorTranslator.FromHtml("#2d2d2d");
        }

        private void attendanceTS2_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Event Request
        private void pendingRequestTS_Click(object sender, EventArgs e)
        {
            tabSecond.SelectedTab = tabPending;
            tabEvPending.SelectedIndex = 0;
        }

        private void addEventTS2_Click(object sender, EventArgs e)
        {
            tabSecond.SelectedTab = tabRequest;
            tabERForm.SelectedIndex = 0;
        }

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

        #region Next Back Buttons (request events)       
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (txtEventName.Text == "What is the name of the event?" || txtVenue.Text == "Where will it be held?" ||
                txtEventDes.Text == "Describe the event." || cbEType.Text == "") errorMessage("You have skipped a blank!Please answer everything.");
            else
            {
                tabERForm.SelectedIndex = 1;
                tdTS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#18764e");
                edTS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c5d9d0");
                confirm_EName.Text = txtEventName.Text;
                confirm_EVenue.Text = txtVenue.Text;
                confirm_EDes.Text = txtEventDes.Text;
                confirm_EType.Text = cbEType.Text;
            }
        }

        private void btnNext2_Click(object sender, EventArgs e)
        {//Lagyan ng checking the event schedule dito para walang conflict
            if (cbEMonth.Text == "" || cbEDay.Text == "" || cbEYear.Text == "" ||
            cbEMonth2.Text == "" || cbEDay2.Text == "" || cbEYear2.Text == "") errorMessage("You have skipped a blank!Please answer everything.");
            else
            {
                int monthfrom = Array.IndexOf(aMonths, cbEMonth.Text) + 1; int monthto = Array.IndexOf(aMonths, cbEMonth2.Text) + 1;
                DateTime timefrom = DateTime.Parse(txtEHours.Text + ":" + txtEMins.Text + " " + ampmFrom);
                DateTime timeto = DateTime.Parse(txtEHours2.Text + ":" + txtEMins2.Text + " " + ampmTo);
                string dfrom = cbEYear.Text + "-" + monthfrom.ToString("00") + "-" + cbEDay.Text + " " + timefrom.ToString("HH:mm");
                string dOneto = cbEYear.Text + "-" + monthfrom.ToString("00") + "-" + cbEDay.Text + " " + timeto.ToString("HH:mm");
                string dto = cbEYear2.Text + "-" + monthto.ToString("00") + "-" + cbEDay2.Text + " " + timeto.ToString("HH:mm");
                //MessageBox.Show(dfrom + " " + dfrom);
                if (btnRange == "multi")
                {
                    if (checkConflict(dfrom, dto)) errorMessage("Event Date might have a conflict with an approved or pending event. Please cancel or reject the conflicted event if you insist to continue.");
                    else
                    {
                        tabERForm.SelectedIndex = 2;
                        oTS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#18764e");
                        tdTS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c5d9d0");
                        confirm_EDateTime.Text = "FROM: " + cbEYear.Text + "-" + cbEMonth.Text + "-" + cbEDay.Text + " " + txtEHours.Text + ":" + txtEMins.Text + " " + ampmFrom + "\n" +
                        "TO: " + cbEYear2.Text + "-" + cbEMonth2.Text + "-" + cbEDay2.Text + " " + txtEHours2.Text + ":" + txtEMins2.Text + " " + ampmTo;
                    }
                }
                else
                {
                    if (checkConflict(dfrom, dOneto)) errorMessage("Event Date might have a conflict with an approved or pending event. Please cancel or reject the conflicted event if you insist to continue.");
                    else
                    {
                        tabERForm.SelectedIndex = 2;
                        oTS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#18764e");
                        tdTS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c5d9d0");
                        confirm_EDateTime.Text = cbEYear.Text + "-" + cbEMonth.Text + "-" + cbEDay.Text + " FROM: " + txtEHours.Text + ":" + txtEMins.Text + " " + ampmFrom + " TO: " + txtEHours2.Text + ":" + txtEMins2.Text + " " + ampmTo;
                    }
                }
                
            }
        }
        public bool checkConflict(string dfrom, string dto)//event conflict check if the doesn't conflict with other
        {
            //MessageBox.Show("if venue is the same also restrict but if different allow adding of event");
            bool check = true;
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand("SELECT * FROM event WHERE evVenue = '"+ txtVenue.Text + "' AND (status = 'Approved' OR status = 'Pending') AND (('"+ dfrom +"' >= str_to_date(CONCAT(evDateFrom,' ', evTimeFrom), '%Y-%m-%d %h:%i %p')) AND ('"+ dto + "' <= str_to_date(CONCAT(evDateTo,' ', evTimeTo), '%Y-%m-%d %h:%i %p')) OR ('" + dfrom + "' <= str_to_date(CONCAT(evDateTo,' ', evTimeTo), '%Y-%m-%d %h:%i %p')) AND ('" + dto + "' >= str_to_date(CONCAT(evDateFrom,' ', evTimeFrom), '%Y-%m-%d %h:%i %p')))", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count >= 1) check = true;
                else check = false;
                conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Nah!" + ee);
                conn.Close();
            }
            return check;
        }
        
        private void btnBack_Click(object sender, EventArgs e)
        {
            tabERForm.SelectedIndex = 0;
            edTS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#18764e");
            tdTS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c5d9d0");
        }

        private void btnBack2_Click(object sender, EventArgs e)
        {
            tabERForm.SelectedIndex = 1;
            tdTS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#18764e");
            oTS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c5d9d0");
        }

        private void btnNext3_Click(object sender, EventArgs e)
        {
            if (txtEventDes.Text.Equals("Describe the event.") ||
            txtEventName.Text.Equals("What is the name of the event?") ||
            txtVenue.Text.Equals("Where will it be held?")) errorMessage("You have skipped a blank! Please answer everything.");
            else
            {
                tabERForm.SelectedIndex = 3;
                confirmTS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#18764e");
                oTS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c5d9d0");
                if (budgetYN == "yes") confirm_EBudget.Text = "Available";
                else if (budgetYN == "no") confirm_EBudget.Text = "NONE";
                if (remindYN == "yes") confirm_ERemind.Text = "lagyan pa";
                else if (remindYN == "no") confirm_ERemind.Text = "NONE";
            }
        }
        
        private void btnBack3_Click(object sender, EventArgs e)
        {
            tabERForm.SelectedIndex = 2;
            oTS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#18764e");
            confirmTS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c5d9d0");
        }
        #endregion

        #region Event Request Form Textboxs
        
        #region Enter Textbox        
        private void cbEType_Enter(object sender, EventArgs e)
        {
            lblEType.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
        }

        private void eventDate_Enter(object sender, EventArgs e)
        {
            resetLabelsPanels();
            lblEDate.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
        }        

        private void others_Enter(object sender, EventArgs e)
        {
            lblNo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            //reminderPanel.Height = 0;
        }
        #endregion
        

        private void btnOther_MouseHover(object sender, EventArgs e)
        {
            btnOther.BackColor = Color.Transparent;
        }
        
        #endregion

        #region Textbox
        private void txtNew_Enter(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == "What is the name of the event?" || ((TextBox)sender).Text == "Where will it be held?" || ((TextBox)sender).Text == "Who requested the event?") ((TextBox)sender).Text = "";
            ((TextBox)sender).ForeColor = Color.Black;

            if (((TextBox)sender).Name == "txtEventName")
            {
                lblEventName.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                panelEName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                countEName.Visible = true;
            }
            else if (((TextBox)sender).Name == "txtVenue")
            {
                lblEVenue.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                panelEVenue.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                countEVenue.Visible = true;
            }
            else if (((TextBox)sender).Name == "txtRequestBy") 
            {
                lblRequestBy.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                panelRequestBy.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                countRequestBy.Visible = true;
            }                     
        }

        private void txtNewCount_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Name == "txtEventName") countEName.Text = ((TextBox)sender).TextLength + "/100";
            else if (((TextBox)sender).Name == "txtVenue") countEVenue.Text = ((TextBox)sender).TextLength + "/100";
            else if (((TextBox)sender).Name == "txtRequestBy") countRequestBy.Text = ((TextBox)sender).TextLength + "/100";
        }

        private void txtNew_Leave(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == "")
            {
                if (((TextBox)sender).Name == "txtEventName") ((TextBox)sender).Text = "What is the name of the event?";
                else if (((TextBox)sender).Name == "txtVenue") ((TextBox)sender).Text = "Where will it be held?";
                else if (((TextBox)sender).Name == "txtRequestBy") ((TextBox)sender).Text = "Who requested the event?";
            }
            ((TextBox)sender).ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);

            if (((TextBox)sender).Name == "txtEventName")
            {
                if (sameEvName(txtEventName.Text)) confirm_EName.Text = txtEventName.Text;
                else
                {
                    errorMessage("The event name is already present.");
                    txtEventName.Text = "What is the name of the event?";
                }
                lblEventName.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                panelEName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
                countEName.Visible = false;
            }
            else if (((TextBox)sender).Name == "txtVenue")
            {
                lblEVenue.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                panelEVenue.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
                countEVenue.Visible = false;
            }
            else if (((TextBox)sender).Name == "txtRequestBy")
            {
                lblRequestBy.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                panelRequestBy.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
                countRequestBy.Visible = false;
            }
        }
        private void txtEventDes_Enter(object sender, EventArgs e)
        {
            if(txtEventDes.Text == "Describe the event.")
            {
                txtEventDes.Text = "";
                lblEDes.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                txtEventDes.ForeColor = Color.Black;
                countEDes.Visible = true;
            }
        }
        private void txtEventDes_TextChanged(object sender, EventArgs e)
        {
            countEDes.Text = txtEventDes.TextLength + "/250";
        }
        private void txtEventDes_Leave(object sender, EventArgs e)
        {
            if (txtEventDes.Text == "")
            {
                txtEventDes.Text = "Describe the event.";
                txtEventDes.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);
                lblEDes.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                countEDes.Visible = false;
            }
        }
        #endregion

        #region Day Buttons
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
            //panelEHours2.Visible = false; panelEMins2.Visible = false; lblColon2.Visible = false; btnAM2.Visible = false; btnPM2.Visible = false;
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
            txtEHours.Text = "00"; txtEHours2.Text = "00"; txtEMins2.Text = "00";
            btnAM.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
            btnPM.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
            btnAM2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
            btnPM2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
            ampmFrom = ""; ampmTo = "";
        }

        #endregion

        #region Switches
        private void btnBudget_Click(object sender, EventArgs e)
        {            
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

        private void btnRemind_Click(object sender, EventArgs e)
        {
            if (!remindState)
            {
                lblNo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
                lblYes.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
                btnRemind.BackgroundImage = global::BalayPasilungan.Properties.Resources.on;
                remindState = true;
                remindTimer.Enabled = true;
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

        private void bntRAllDay_Click(object sender, EventArgs e)
        {
            if (!allDayState)
            {
                lbAllDay.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
                btnRAllDay.BackgroundImage = global::BalayPasilungan.Properties.Resources.on;
                allDayState = true;
                txtEHours.Text = "12"; txtEHours2.Text = "11"; txtEMins2.Text = "59";
                btnAM.PerformClick(); btnPM2.PerformClick();
                //cbEDay2.Enabled = false; cbEMonth2.Enabled = false; cbEYear2.Enabled = false;
                panelEHours2.Visible = true; panelEMins2.Visible = true; lblColon2.Visible = true; btnAM2.Visible = true; btnPM2.Visible = true;
            }
            else
            {
                lbAllDay.ForeColor = Color.FromArgb(42, 42, 42);
                btnRAllDay.BackgroundImage = global::BalayPasilungan.Properties.Resources.off;
                allDayState = false;
                cbEDay2.SelectedIndex = cbEDay.SelectedIndex;
                txtEHours.Text = "00"; txtEHours2.Text = "00"; txtEMins2.Text = "00";
                btnAM.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
                btnPM2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
                ampmFrom = ""; ampmTo = "";
                lbFrom.Visible = false; lbTo.Visible = false;
            }
        }
        #endregion

        #region Event Request Time and Date Data Validation
        //Leap Year??
        public void dateFromInitial(int month,  int year)
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
        private void cbEDay_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbEDay.SelectedItem.Equals("")) cbEDay.SelectedIndex = 0;
        }
        private void cbEMonth_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int num = Array.IndexOf(aMonths, cbEMonth.Text) + 1;
            dateFromInitial(num, int.Parse(cbEYear.Text));
        }

        private void cbEYear_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int num = Array.IndexOf(aMonths, cbEMonth.Text) + 1;
            dateFromInitial(num, int.Parse(cbEYear.Text));
        }
        private void cbEDay2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbEDay2.SelectedItem.Equals("")) cbEDay2.SelectedIndex = cbEDay.SelectedIndex;
        }
        private void cbEMonth2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int num = Array.IndexOf(aMonths, cbEMonth2.Text) + 1;
            dateToInitial(num, int.Parse(cbEYear2.Text));
        }

        private void cbEYear2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int num = Array.IndexOf(aMonths, cbEMonth2.Text) + 1;
            dateToInitial(num, int.Parse(cbEYear2.Text));
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
            else if(btnRange == "multi")
            {
                cbEDay2.SelectedIndex = cbEDay.SelectedIndex;
            }
        }

        private void cbEYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbEYear2.SelectedIndex = cbEYear.SelectedIndex;
        }
        private void cbEDay2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEDay2.SelectedItem == null) cbEDay2.SelectedIndex = cbEDay.SelectedIndex;
        }
        private void cbEMonth2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(cbEMonth2.SelectedIndex + "<"+ cbEMonth2.SelectedIndex + " " + cbEYear.SelectedIndex + "==" + cbEYear2.SelectedIndex);
            if (cbEMonth2.SelectedIndex < cbEMonth.SelectedIndex && cbEYear.SelectedIndex == cbEYear2.SelectedIndex)
            {
                errorMessage("You cannot set this to an earlier month.");
                cbEMonth2.SelectedIndex = cbEMonth.SelectedIndex;
            }
            int num = Array.IndexOf(aMonths, cbEMonth2.Text) + 1;
            dateToInitial(num, int.Parse(cbEYear2.Text));
        }

        private void cbEYear2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEYear2.SelectedIndex < cbEYear.SelectedIndex)
            {
                errorMessage("You cannot set this to an earlier year.");
                cbEYear2.SelectedIndex = cbEYear.SelectedIndex;
            }
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

        private void cb_YRemind_SelectedIndexChanged(object sender, EventArgs e)
        {
            int num = Array.IndexOf(aMonths, cb_MRemind.Text) + 1;
            //MessageBox.Show("yremind" + num);
            dateRemindInitial(num, int.Parse(cb_YRemind.Text));
        }
        #endregion

        #region Event Request Time
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

        private void txtEHours_Leave(object sender, EventArgs e)
        {
            if(txtEHours.Text == "")
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
            if(txtEMins.Text == "")
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
            if(txtEHours2.Text == "")
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
            if(txtEMins.Text == "")
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
        #endregion        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            tabSecond.SelectedTab = tabCalendar;
        }

        private void cbEType_Leave(object sender, EventArgs e)
        {

        }

        private void eventorg_Load(object sender, EventArgs e)
        {
            btnEvent.BackColor = Color.White;
            //MessageBox.Show("gawa ng dialog box for event view na edit isip din kun asan ilagay ang cancel. ui design nanaman for tabevent i'm not satisfied");
            btnMPrev.Text = aMonths[DateTime.Now.Month - 2];
            btnMNow.Text = aMonths[DateTime.Now.Month - 1];
            btnMNext.Text = aMonths[DateTime.Now.Month];
            btnYPrev.Text = (DateTime.Now.Year - 1).ToString();
            btnYNow.Text = DateTime.Now.Year.ToString();
            btnYNext.Text = (DateTime.Now.Year + 1).ToString();
            //tabSecond.SelectedTab = tabCalendar;
            int monthnum = Array.IndexOf(aMonths, btnMNow.Text) + 1;
            displayCalendar(monthnum.ToString("00"), int.Parse(btnYNow.Text));
            //initialize year and month comboboxes 
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
            displayEventApproval();
        }
        #region Custom Month and Year
        private void btnMNow_Click(object sender, EventArgs e)
        {
            others ot = new others();
            //this.Enabled = false;
            ot.tabSelection.SelectedIndex = 1;
            //ot.indextab = 1;
            ot.reftoevorg = this;
            DialogResult rest = ot.ShowDialog();
            if (rest == DialogResult.OK)
            {
                int monthnum = Array.IndexOf(aMonths, btnMNow.Text) + 1;
                displayCalendar(monthnum.ToString("00"), int.Parse(btnYNow.Text));
            }
            //ot.Show();
        }

        private void btnYNow_Click(object sender, EventArgs e)
        {
            others ot = new others();

            //this.Enabled = false;
            ot.tabSelection.SelectedIndex = 0;
            //ot.indextab = 0;
            ot.reftoevorg = this;
            DialogResult rest = ot.ShowDialog();
            if (rest == DialogResult.OK)
            {
                int monthnum = Array.IndexOf(aMonths, btnMNow.Text) + 1;
                displayCalendar(monthnum.ToString("00"), int.Parse(btnYNow.Text));
            }
            //ot.Show();
        }
        #endregion

        #region Time Buttons
        private void btnMPrev_Click(object sender, EventArgs e)
        {
            btnMNext.Text = btnMNow.Text;
            btnMNow.Text = btnMPrev.Text;
            foreach (String now in aMonths)
            {
                if (btnMNow.Text.Equals(now))
                {
                    int i = Array.IndexOf(aMonths, now);
                    if (i == 0) btnMPrev.Text = aMonths[11];
                    else btnMPrev.Text = aMonths[i - 1];
                }
            }
            int monthnum = Array.IndexOf(aMonths, btnMNow.Text) + 1;
            displayCalendar(monthnum.ToString("00"), int.Parse(btnYNow.Text));
        }

        private void btnMNext_Click(object sender, EventArgs e)
        {
            btnMPrev.Text = btnMNow.Text;
            btnMNow.Text = btnMNext.Text;
            foreach (String now in aMonths)
            {
                if (btnMNow.Text.Equals(now))
                {
                    int i = Array.IndexOf(aMonths, now);
                    if (i == 11) btnMNext.Text = aMonths[0];
                    else btnMNext.Text = aMonths[i + 1];
                }
            }
            int monthnum = Array.IndexOf(aMonths, btnMNow.Text) + 1;
            displayCalendar(monthnum.ToString("00"), int.Parse(btnYNow.Text));
        }

        private void btnYPrev_Click(object sender, EventArgs e)
        {
            btnYPrev.Enabled = true; btnYNext.Enabled = true;
            if (btnYPrev.Text.ToString().Equals("1960"))
            {
                btnYNow.Text = "1960";
                btnYPrev.Text = "";
                btnYNext.Text = "1961";
                btnYPrev.Enabled = false;
            }
            else
            {
                btnYNext.Text = btnYNow.Text;
                btnYNow.Text = btnYPrev.Text;
                btnYPrev.Text = (int.Parse(btnYNow.Text.ToString()) - 1).ToString();
            }
            int monthnum = Array.IndexOf(aMonths, btnMNow.Text) + 1;
            displayCalendar(monthnum.ToString("00"), int.Parse(btnYNow.Text));
        }

        private void btnYNext_Click(object sender, EventArgs e)
        {
            btnYNext.Enabled = true; btnYPrev.Enabled = true;
            if (btnYNext.Text.ToString().Equals("2099"))
            {
                btnYNow.Text = "2099";
                btnYNext.Text = "";
                btnYPrev.Text = "2098";
                btnYNext.Enabled = false;
            }
            else
            {
                btnYPrev.Text = btnYNow.Text;
                btnYNow.Text = btnYNext.Text;
                btnYNext.Text = (int.Parse(btnYNow.Text.ToString()) + 1).ToString();
            }
            int monthnum = Array.IndexOf(aMonths, btnMNow.Text) + 1;
            displayCalendar(monthnum.ToString("00"), int.Parse(btnYNow.Text));
        }
        #endregion

        public int evid = 0;
        #region tabEvent Function
        public void displayEvents(string dc, string mc, string yc)
        {
            eventsListView.Items.Clear();
            string date = yc + "-" + mc + "-" + dc;
            DateTime dcur = DateTime.ParseExact(date, "yyyy-M-d", CultureInfo.InvariantCulture);
            string evname, dfrom, dto, timefrom = "";
            string prog;
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT * FROM event WHERE status = 'Approved' AND ('" + date + "' >= str_to_date(evDateFrom, '%Y-%m-%d')) AND ('" + date + "' <= str_to_date(evDateTo, '%Y-%m-%d'))", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                conn.Close();
                if (dt.Rows.Count >= 1)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dfrom = dt.Rows[i]["evDateFrom"].ToString();
                        dto = dt.Rows[i]["evDateTo"].ToString();
                        DateTime datefrom = DateTime.ParseExact(dfrom, "yyyy-M-d", CultureInfo.InvariantCulture);
                        DateTime dateto = DateTime.ParseExact(dto, "yyyy-M-d", CultureInfo.InvariantCulture);
                        if (dcur == datefrom && dcur == dateto) timefrom = dt.Rows[i]["evTimeFrom"].ToString() + " - " + dt.Rows[i]["evTimeTo"].ToString();
                        else if (dcur == datefrom && dcur < dateto) timefrom = dt.Rows[i]["evTimeFrom"].ToString() + " - 11:59 PM";
                        else if (dcur == dateto)
                        {
                            if (dt.Rows[i]["evTimeTo"].ToString() == "12:00 AM") timefrom = dt.Rows[i]["evTimeTo"].ToString();
                            else timefrom = "12:00 AM - " + dt.Rows[i]["evTimeTo"].ToString();
                        }
                        else timefrom = "12:00 AM - 11:59 PM";
                        evname = dt.Rows[i]["evName"].ToString();
                        prog = dt.Rows[i]["evProgress"].ToString();
                        ListViewItem itm = new ListViewItem(timefrom);
                        itm.SubItems.Add(evname);
                        itm.SubItems.Add(prog);
                        eventsListView.Items.Add(itm);
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Nah!" + ee);
                conn.Close();
            }
            btnEvCancel.Enabled = false; btnAddAttendance.Enabled = false; btnVAttend.Enabled = false; btnViewBudget.Enabled = false;
        }
        private void eventsListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int monthnum = Array.IndexOf(aMonths, btnMNow.Text) + 1;
            EvEditDetails evedit = new EvEditDetails();
            evedit.reftoevorg = this;
            //MessageBox.Show(listPending.SelectedItems[1].SubItems[0].Text);
            evedit.evnEdit = eventsListView.SelectedItems[0].SubItems[1].Text;
            this.Hide();
            DialogResult rest = evedit.ShowDialog();
            if(rest == DialogResult.OK)
            {
                displayEvents(cellday.ToString("00"), monthnum.ToString("00"), btnYNow.Text);
            }
        }
        public string ondaytime, cellEvProg;
        private void eventsListView_MouseClick(object sender, MouseEventArgs e)
        {
            approveEventDetails(eventsListView.SelectedItems[0].SubItems[1].Text);
            ondaytime = eventsListView.SelectedItems[0].SubItems[0].Text;
            cellEvProg = eventsListView.SelectedItems[0].SubItems[2].Text;
            if (cellEvProg == "Upcoming") btnAddAttendance.Enabled = false;
            else btnAddAttendance.Enabled = true;
            btnEvCancel.Enabled = true; btnVAttend.Enabled = true; btnViewBudget.Enabled = true; 
        }

        private void btnEvCancel_Click(object sender, EventArgs e)
        {
            int monthnum = Array.IndexOf(aMonths, btnMNow.Text) + 1;
            confirmMessage("Are you sure you want to edit these records? Please double check.");
            if (confirmed) {
                statusEvent(2);
                displayEvents(cellday.ToString("00"), monthnum.ToString("00"), btnYNow.Text);
                btnEvCancel.Enabled = false;
                allEvents();
                calendarcolor();
            }
        }
        #endregion

        #region tabRequest functions
        public string ampmFrom = "", ampmTo = "", ampmR = "", remindYN = "no", budgetYN = "no";
        
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtRequestBy.Text == "Who requested the event?" || txtRequestBy.Text == "") errorMessage("You have skipped a blank! Please fill everything.");
            else
            {
                successMessage("Event request has been added successfully.");
                tabSecond.SelectedTab = tabPending;
                tabEvPending.SelectedIndex = 0;
                int insnum = 0;
                if (remindYN == "yes" && budgetYN == "yes") insnum = 1;
                else if (remindYN == "yes" && budgetYN == "no") insnum = 2;
                else if (remindYN == "no" && budgetYN == "yes") insnum = 3;
                else insnum = 0;
                insertRequest(insnum);
                displayEventApproval();
            }
        }

        public bool sameEvName(string name)
        {
            bool check = false;
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT * FROM event WHERE evname = '"+ name +"'", conn);
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
        //insert functions for requesting
        public void insertRequest(int insnum)
        {
            string app = "";
            if (usertype == 0) app = "Admin";
            else if (usertype == 1) app = "Admin or Social Worker";
            else if (usertype == 2) app = "Educator";
            else app = "All";
            int monthfrom = 0, monthto = 0;
            string eventtime = "", datefrom = "", timeTo = "", dateTo = "";
            if (btnRange == "one")
            {
                monthfrom = Array.IndexOf(aMonths, cbEMonth.Text) + 1;
                monthto = Array.IndexOf(aMonths, cbEMonth.Text) + 1;
                eventtime = txtEHours.Text + ":" + txtEMins.Text + " " + ampmFrom;
                datefrom = cbEYear.Text + "-" + monthfrom.ToString("00") + "-" + cbEDay.Text;
                timeTo = txtEHours2.Text + ":" + txtEMins2.Text + " " + ampmTo;
                dateTo = cbEYear.Text + "-" + monthto.ToString("00") + "-" + cbEDay2.Text;

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
                //MessageBox.Show(insnum + "");
                conn.Open();
                MySqlCommand comm = new MySqlCommand();
                if (insnum == 0) comm = new MySqlCommand("INSERT INTO event(evName, evDesc, evDateFrom, evTimeFrom , evVenue, evProgress, evType, attendance, status, requestedBy, evDateTo, evTimeTo, approvedBy) VALUES('" + confirm_EName.Text + "','" + confirm_EDes.Text + "','" + datefrom + "', '" + eventtime + "','" + confirm_EVenue.Text + "','" + "Pending" + "','" + confirm_EType.Text + "', 'False' , 'Pending' " + ",'" + txtRequestBy.Text + "', '" + dateTo + "', '" + timeTo + "', '" + app + "');", conn); //insert
                else if (insnum == 1) comm = new MySqlCommand("INSERT INTO event(evName, evDesc, evDateFrom, evTimeFrom, evVenue, evProgress, evType, status, reminderDate, reminderTime, attendance, requestedBy, budget, reminder, evDateTo, evTimeTo, approvedBy) VALUES('" + confirm_EName.Text + "','" + confirm_EDes.Text + "','" + datefrom + "', " + eventtime + "','" + confirm_EVenue.Text + "', 'Pending' , '" + confirm_EType.Text + "' , 'Pending' ,'" + reminddate + "','" + remindtime + "', 'False' ,'" + txtRequestBy.Text + "', 'True', 'True', '" + dateTo + "', " + timeTo + "', '" + app + "');", conn);//insert with both
                else if (insnum == 2) comm = new MySqlCommand("INSERT INTO event(evName, evDesc, evDateFrom, evTimeFrom, evVenue, evProgress, evType, status, attendance, requestedBy, reminder, evDateTo, evTimeTo, reminderDate, reminderTime, approvedBy) VALUES('" + confirm_EName.Text + "','" + confirm_EDes.Text + "','" + datefrom + "', '" + eventtime + "','" + confirm_EVenue.Text + "','" + "Pending" + "','" + confirm_EType.Text + "','" + "Pending" + "', 'False' ,'" + txtRequestBy.Text + "','True', '" + dateTo + "', '" + timeTo + "','" + reminddate + "','" + remindtime + "', '" + app + "');", conn); // insert with remind
                else if (insnum == 3) comm = new MySqlCommand("INSERT INTO event(evName, evDesc, evDateFrom, evVenue, evProgress, evType, status, attendance, requestedBy, budget, evDateTo, approvedBy) VALUES('" + confirm_EName.Text + "','" + confirm_EDes.Text + "','" + datefrom + "', '" + eventtime + "','" + confirm_EVenue.Text + "','" + "Pending" + "','" + confirm_EType.Text + "', 'Pending' , 'False' ,'" + txtRequestBy.Text + "', 'True', '" + dateTo + "', '" + timeTo + "', '" + app + "');", conn); // insert with budget
                comm.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show("" + ee);
                conn.Close();
            }
        }
        //pending and approval of events functions
        public void displayEventApproval()
        {
            listPending.Items.Clear();
            string evname, request;
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT * FROM event WHERE status='Pending'", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        request = dt.Rows[i]["requestedBy"].ToString();
                        evname = dt.Rows[i]["evName"].ToString();
                        //MessageBox.Show(evname+" "+request);
                        ListViewItem itm = new ListViewItem(evname);
                        itm.SubItems.Add(request);
                        listPending.Items.Add(itm);
                    }
                }
                conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Nah!" + ee);
                conn.Close();
            }
        }

        //public event id for the event approval only
        private void listPending_MouseClick(object sender, MouseEventArgs e)
        {
            string evn = listPending.SelectedItems[0].SubItems[0].Text;
            approveEventDetails(evn);
            tabEvPending.SelectedIndex = 1;
        }
        public string approvedby;
        public void approveEventDetails(string evn)
        {
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT * FROM event WHERE evName='" + evn + "'", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    evid = int.Parse(dt.Rows[0]["eventID"].ToString());
                    lblPEName.Text = dt.Rows[0]["evName"].ToString();
                    lblPEVenue.Text = dt.Rows[0]["evVenue"].ToString();
                    lblPEDes.Text = dt.Rows[0]["evDesc"].ToString();
                    lblPEType.Text = dt.Rows[0]["evType"].ToString();
                    lblPEDate.Text = "From: " + dt.Rows[0]["evDateFrom"].ToString() + " " + dt.Rows[0]["evTimeFrom"].ToString() + "\n" +
                                    "To: " + dt.Rows[0]["evDateTo"].ToString() + " " + dt.Rows[0]["evTimeTo"].ToString();
                    if (dt.Rows[0]["reminderDate"].ToString() == "") lblPERemind.Text = "NONE";
                    else lblPERemind.Text = dt.Rows[0]["reminderDate"].ToString() + " " + dt.Rows[0]["reminderTime"].ToString();
                    if (dt.Rows[0]["budget"].ToString() == "") lblPEBudget.Text = "NONE";
                    else lblPEBudget.Text = "Total Budget" + " " + dt.Rows[0]["budgetTotal"].ToString();
                    approvedby = dt.Rows[0]["approvedBy"].ToString();
                }
                conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Nah!" + ee);
                conn.Close();
            }
        }
        //approve and disapprove buttons
        public void statusEvent(int snum)
        {
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand();
                if ((approvedby == "Admin" && usertype == 0) || (approvedby == "Admin or Social Worker" && (usertype == 0 || usertype == 1)) || (approvedby == "Educator" && usertype == 2) || (approvedby == "All" && (usertype == 0 || usertype == 1 || usertype == 2)))
                {
                    if (snum == 0)
                    {
                        comm = new MySqlCommand("UPDATE event SET status='Approved' WHERE eventID = '" + evid + "';", conn);
                        successMessage("Successfully Approved the Event.");
                    }
                    else if (snum == 1)
                    {
                        comm = new MySqlCommand("UPDATE event SET status='Disapproved' , evProgress = 'Disapproved' WHERE eventID = '" + evid + "';", conn);
                        successMessage("Successfully Rejected the Event.");
                    }
                    else if (snum == 2)
                    {
                        comm = new MySqlCommand("UPDATE event SET status='Canceled' , evProgress = 'Canceled' WHERE eventID = '" + evid + "';", conn);
                    }
                    
                    comm.ExecuteNonQuery();
                }else errorMessage("The event should be approved by " + approvedby);
                conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show("" + ee);
                conn.Close();
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                statusEvent(0);
                displayEventApproval();
                btnEvent.PerformClick();
            }
            catch (Exception ee)
            {
                MessageBox.Show("btn reject" + ee);
                conn.Close();
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                statusEvent(1);
                success scs = new success();
                scs.reftoevorg = this;
                displayEventApproval();
                btnEvent.PerformClick();
            }
            catch (Exception ee)
            {
                MessageBox.Show("btn reject" + ee);
                conn.Close();
            }
        }
        private void btnBackP_Click(object sender, EventArgs e)
        {
            tabEvPending.SelectedIndex = 0;
        }

        public void clrTabReqF()//resets the field in tab request for every time you click the tab request btn or add through calendar; 
        {
            tabERForm.SelectedIndex = 0;
            resetLabelsPanels();
            txtEventName.Text = "What is the name of the event?";
            txtEventDes.Text = "Describe the event.";
            txtVenue.Text = "Where will it be held?";
            txtRequestBy.Text = "Who requested the event?";
            cbEType.Text = "";
            txtEHours.Text = "00"; txtEMins.Text = "00";
            btnAM.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
            btnPM.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
            txtEHours2.Text = "00"; txtEMins2.Text = "00";
            btnAM2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
            btnPM2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
            txtHrRemind.Text = "00"; txtMinRemind.Text = "00"; 
            btnAMRemind.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
            btnPMRemind.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
            lbAllDay.ForeColor = Color.FromArgb(42, 42, 42);
            btnRAllDay.BackgroundImage = global::BalayPasilungan.Properties.Resources.off;
            btnRAllDay.Visible = false;
            lbAllDay.Visible = false;
        }
        public bool showevtype = true;
        private void btnShowAdd_Click(object sender, EventArgs e)
        {
            if (showevtype)
            {
                cbEType.DropDownStyle = ComboBoxStyle.DropDown;
                btnShowAdd.BackgroundImage = global::BalayPasilungan.Properties.Resources.checked_symbol;
                btnNext.Enabled = false;
                showevtype = false;
            }
            else
            {
                cbEType.Items.Add(cbEType.Text); 
                cbEType.DropDownStyle = ComboBoxStyle.DropDownList;
                btnShowAdd.BackgroundImage = global::BalayPasilungan.Properties.Resources.addsomething;
                btnNext.Enabled = true;
                showevtype = true;
            }
        }
        private void txtEventName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {//validation kay baka mag error kung may '', ;, &&, ||, etc...
            //MessageBox.Show(e.KeyCode.ToString());
            //if (e.KeyCode.ToString() == "Oem1" || e.KeyCode.ToString() == "Oem7") 
            //else e.Handled = false;
        }
        #endregion

        #region Calendar Functions
        public void displayCalendar(string month, int yearofMonth)
        {
            // should be in the format of Jan, Feb, Mar, Apr, etc...
            string deytaym = "01/" + month + "/" + yearofMonth;
            DateTime dateTime = DateTime.ParseExact(deytaym, "d/M/yyyy", CultureInfo.InvariantCulture);
            //MessageBox.Show(dateTime + " " + deytaym);
            DataRow dr;
            DataTable dt = new DataTable();
            dt.Columns.Add("Sunday");
            dt.Columns.Add("Monday");
            dt.Columns.Add("Tuesday");
            dt.Columns.Add("Wednesday");
            dt.Columns.Add("Thursday");
            dt.Columns.Add("Friday");
            dt.Columns.Add("Saturday");
            dr = dt.NewRow();
            for (int i = 0; i < DateTime.DaysInMonth(dateTime.Year, dateTime.Month); i++)
            {
                //txtMonth.Text = Convert.ToDateTime(dateTime.AddDays(0)).ToString("dddd");
                if (dateTime.AddDays(i).ToString("dddd") == "Sunday")
                {
                    dr["Sunday"] = i + 1;

                }
                if (dateTime.AddDays(i).ToString("dddd") == "Monday")
                {
                    dr["Monday"] = i + 1;

                }
                if (dateTime.AddDays(i).ToString("dddd") == "Tuesday")
                {
                    dr["Tuesday"] = i + 1;

                }
                if (dateTime.AddDays(i).ToString("dddd") == "Wednesday")
                {
                    dr["Wednesday"] = i + 1;
                }
                if (dateTime.AddDays(i).ToString("dddd") == "Thursday")
                {
                    dr["Thursday"] = i + 1;

                }
                if (dateTime.AddDays(i).ToString("dddd") == "Friday")
                {
                    dr["Friday"] = i + 1;
                }
                if (dateTime.AddDays(i).ToString("dddd") == "Saturday")
                {
                    dr["Saturday"] = i + 1;
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                    continue;
                }
                if (i == DateTime.DaysInMonth(dateTime.Year, dateTime.Month) - 1)
                {
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();

                }

            }

            CalendarView.DataSource = dt;
            
            foreach (DataGridViewColumn ya in CalendarView.Columns)
            {
                ya.SortMode = DataGridViewColumnSortMode.NotSortable;
                ya.Width = 145;
            }
            foreach (DataGridViewRow ro in CalendarView.Rows)
            {
                ro.Height = 101;
            }
            calendarcolor();
            //MessageBox.Show("Lagyan ng dialog box sa cell click add request and view request");
            //Lagyan ng dialog box sa cell click add request and view request
        }

        public void calendarcolor()
        {
            int dfrom, mfrom, yfrom, dto, mto, yto, m = Array.IndexOf(aMonths, btnMNow.Text) + 1, year = int.Parse(btnYNow.Text);
            string prog;
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand("SELECT * FROM event WHERE status = 'Approved' OR status = 'Canceled'", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    string[] clr = new string[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        prog = dt.Rows[i]["evProgress"].ToString();
                        clr[i] = prog;
                        dfrom = int.Parse(dt.Rows[i]["evDateFrom"].ToString().Substring(8, 2));
                        mfrom = int.Parse(dt.Rows[i]["evDateFrom"].ToString().Substring(5, 2));
                        yfrom = int.Parse(dt.Rows[i]["evDateFrom"].ToString().Substring(0, 4));
                        //DateTime datefrom = Convert.ToDateTime(dfrom.ToString("00") +"/"+ mfrom.ToString("00") + "/" + yfrom);
                        DateTime datefrom = DateTime.ParseExact(dfrom.ToString("00") + "/" + mfrom.ToString("00") + "/" + yfrom, "d/M/yyyy", CultureInfo.InvariantCulture);
                        mto = int.Parse(dt.Rows[i]["evDateTo"].ToString().Substring(5, 2));
                        dto = int.Parse(dt.Rows[i]["evDateTo"].ToString().Substring(8, 2));
                        yto = int.Parse(dt.Rows[i]["evDateTo"].ToString().Substring(0, 4));
                        //DateTime dateto = Convert.ToDateTime(dto.ToString("00") + "/" + mto.ToString("00") + "/" + yto);
                        DateTime dateto = DateTime.ParseExact(dto.ToString("00") + "/" + mto.ToString("00") + "/" + yto, "d/M/yyyy", CultureInfo.InvariantCulture);

                        foreach (DataGridViewRow row in CalendarView.Rows)
                        {
                            foreach (DataGridViewCell cell in row.Cells)
                            {//do operations with cell
                                if (cell.Value.ToString() != "")
                                {
                                    //MessageBox.Show(Convert.ToInt32(cell.Value).ToString("00") + "/" + m.ToString("00") + "/" + year.ToString("0000"));
                                    DateTime datecal = DateTime.ParseExact(Convert.ToInt32(cell.Value).ToString("00") + "/" + m.ToString("00") + "/" + year.ToString("0000"), "d/M/yyyy", CultureInfo.InvariantCulture);
                                    bool pos = Array.Exists(clr, element => element == "Ongoing");
                                    if (datecal >= datefrom && datecal <= dateto)
                                    {
                                        if (pos) prog = "Ongoing";
                                        if (prog == "Ongoing")
                                        {
                                            CalendarView.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Style.BackColor = System.Drawing.Color.FromArgb(181, 224, 129);
                                            CalendarView.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Style.ForeColor = Color.White;
                                        }
                                        else if (prog == "Finished")
                                        {
                                            CalendarView.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Style.BackColor = System.Drawing.Color.FromArgb(238, 141, 125);
                                            CalendarView.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Style.ForeColor = Color.White;
                                        }
                                        else if (prog == "Upcoming")
                                        {
                                            CalendarView.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Style.BackColor = System.Drawing.Color.FromArgb(251, 211, 120);
                                            CalendarView.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Style.ForeColor = Color.Black;
                                        }
                                        else
                                        {
                                            CalendarView.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Style.BackColor = Color.White;
                                            CalendarView.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Style.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                                        }

                                    }
                                }
                            }
                        }

                    }
                }
                conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Nah!" + ee);
                conn.Close();
            }
        }

        public string option { get; set; }
        public string ifclick { get; set; }
        public int cellday;
        private void CalendarView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int monthnum = Array.IndexOf(aMonths, btnMNow.Text) + 1;
            //MessageBox.Show(CalendarView.SelectedCells[0].Value.ToString());
            if (CalendarView.SelectedCells[0].Value.ToString() != "")
            {// the plan is to check if the date picked is before the date today so that it will only show a view it cannot add but it is argueable because sometimes people want to records events from the past so it will only matter on the reminder
                DateTime datecell = DateTime.ParseExact(Convert.ToInt32(CalendarView.SelectedCells[0].Value).ToString("00") + "/" + monthnum.ToString("00") + "/" + btnYNow.Text, "d/M/yyyy", CultureInfo.InvariantCulture);
                cellday = int.Parse(CalendarView.SelectedCells[0].Value.ToString());
                if (checkEvents(cellday.ToString("00"), monthnum.ToString("00"), btnYNow.Text))
                {
                    option = "view";
                    dialogEvOptions deo = new dialogEvOptions();
                    deo.reftoevorg = this;
                    DialogResult rest = deo.ShowDialog();
                    if (rest == DialogResult.OK)
                    {
                        if (ifclick == "view")
                        {
                            tabSecond.SelectedTab = tabEvent;
                            displayEvents(cellday.ToString("00"), monthnum.ToString("00"), btnYNow.Text);
                        }
                        else if (ifclick == "add")
                        {
                            tabSecond.SelectedTab = tabRequest;
                            dateFromInitial(monthnum, int.Parse(btnYNow.Text));
                            dateToInitial(monthnum, int.Parse(btnYNow.Text));
                            dateRemindInitial(monthnum, int.Parse(btnYNow.Text));
                            cbEYear.SelectedIndex = cbEYear.FindStringExact(btnYNow.Text); cbEMonth.SelectedIndex = monthnum - 1; cbEDay.SelectedIndex = int.Parse(cellday.ToString("00")) - 1;
                            cbEYear2.SelectedIndex = cbEYear2.FindStringExact(btnYNow.Text); cbEMonth2.SelectedIndex = monthnum - 1; cbEDay2.SelectedIndex = int.Parse(cellday.ToString("00")) - 1;
                            cb_MRemind.SelectedIndex = monthnum - 1; cb_YRemind.SelectedIndex = cb_YRemind.FindStringExact(btnYNow.Text); cb_DRemind.SelectedIndex = int.Parse(cellday.ToString("00")) - 1;
                            clrTabReqF();
                        }
                    }
                }
                else
                {
                    option = "noview";
                    dialogEvOptions deo = new dialogEvOptions();
                    deo.reftoevorg = this;
                    DialogResult rest = deo.ShowDialog();
                    if (rest == DialogResult.OK)
                    {

                        //MessageBox.Show("may event ");

                        if (ifclick == "add")
                        {
                            //MessageBox.Show("no view " + monthnum);
                            tabSecond.SelectedTab = tabRequest;
                            dateFromInitial(monthnum, int.Parse(btnYNow.Text));
                            dateToInitial(monthnum, int.Parse(btnYNow.Text));
                            dateRemindInitial(monthnum, int.Parse(btnYNow.Text));
                            cbEYear.SelectedIndex = cbEYear.FindStringExact(btnYNow.Text); cbEMonth.SelectedIndex = monthnum - 1; cbEDay.SelectedIndex = int.Parse(cellday.ToString("00")) - 1;
                            cbEYear2.SelectedIndex = cbEYear2.FindStringExact(btnYNow.Text); cbEMonth2.SelectedIndex = monthnum - 1; cbEDay2.SelectedIndex = int.Parse(cellday.ToString("00")) - 1;
                            cb_MRemind.SelectedIndex = monthnum - 1; cb_YRemind.SelectedIndex = cb_YRemind.FindStringExact(btnYNow.Text); cb_DRemind.SelectedIndex = int.Parse(cellday.ToString("00")) - 1;
                            clrTabReqF();
                        }

                    }
                }
            }

            //dialog box here for add or edit options
        }

        public bool checkEvents(string dc, string mc, string yc)
        {
            string datecheck = yc + "-" + mc + "-" + dc;
            bool check = true;
            try
            {
                conn.Open();
                // cahnge query aug 21 2017 MySqlCommand comm = new MySqlCommand("SELECT * FROM event WHERE status = 'Approved' AND evDateFrom = '" + datecheck +"'", conn);
                MySqlCommand comm = new MySqlCommand("SELECT * FROM event WHERE status = 'Approved' AND ('" + datecheck + "' >= str_to_date(evDateFrom, '%Y-%m-%d')) AND ('" + datecheck + "' <= str_to_date(evDateTo, '%Y-%m-%d'))", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                conn.Close();
                if (dt.Rows.Count >= 1)
                {
                    check = true;
                }
                else
                {
                    check = false;
                }

                conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Nah!" + ee);
                conn.Close();
            }
            return check;
        }

        public void allEvents()
        {

            int evyear, evmonth, evday;
            string evname, prog = "", id;
            //MessageBox.Show(timenow);
            try
            {

                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT * FROM event", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                conn.Close();
                if (dt.Rows.Count >= 1)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string timeto = dt.Rows[i]["evTimeTo"].ToString();

                        int monthto = int.Parse(dt.Rows[i]["evDateTo"].ToString().Substring(5, 2));
                        int dayto = int.Parse(dt.Rows[i]["evDateTo"].ToString().Substring(8, 2));
                        int yearto = int.Parse(dt.Rows[i]["evDateTo"].ToString().Substring(0, 4));
                        //MessageBox.Show();
                        string timefrom = dt.Rows[i]["evTimeFrom"].ToString();

                        evmonth = int.Parse(dt.Rows[i]["evDateFrom"].ToString().Substring(5, 2));
                        evyear = int.Parse(dt.Rows[i]["evDateFrom"].ToString().Substring(0, 4));
                        evday = int.Parse(dt.Rows[i]["evDateFrom"].ToString().Substring(8, 2));
                        evname = dt.Rows[i]["evName"].ToString();
                        //DateTime datefrom = Convert.ToDateTime(evday + "/" + evmonth + "/" + evyear);
                        //MessageBox.Show(dayto + "/" + monthto + "/" + yearto);
                        //MessageBox.Show("" + evday + " " + evmonth + " " + yearto + " " + timefrom);
                        //MessageBox.Show(evday.ToString("00") + "/" + evmonth.ToString("00") + "/" + evyear + " " + timefrom);
                        DateTime datefrom = DateTime.ParseExact(evday.ToString("00") + "/" + evmonth.ToString("00") + "/" + evyear + " " + timefrom, "dd/MM/yyyy h:m tt", CultureInfo.InvariantCulture);
                        DateTime dateto = DateTime.ParseExact(dayto.ToString("00") + "/" + monthto.ToString("00") + "/" + yearto + " " + timeto, "dd/MM/yyyy h:m tt", CultureInfo.InvariantCulture);

                        id = dt.Rows[i]["eventID"].ToString();
                        if(dt.Rows[i]["status"].ToString() != "Canceled")
                        {
                            if (DateTime.Now >= datefrom && DateTime.Now <= dateto)
                            {
                                prog = "Ongoing";
                            }
                            else if (DateTime.Now < datefrom)
                            {
                                prog = "Upcoming";
                            }
                            else if (DateTime.Now > dateto)
                            {
                                prog = "Finished";
                            }
                        }
                        else
                        {
                            prog = "Cancelled";
                        }
                        //MessageBox.Show(prog + " " + id);
                        updateEventProgress(prog, id);
                    }
                }


            }
            catch (Exception ee)
            {
                MessageBox.Show("Nah!" + ee);
                conn.Close();
            }
        }

        public void updateEventProgress(string p, string id)
        {
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand("UPDATE event SET evProgress ='" + p + "' WHERE eventID = '" + id + "';", conn);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show("" + ee);
                conn.Close();
            }
        }
        private void monthSummary_Click(object sender, EventArgs e)
        {
            int monthnum = Array.IndexOf(aMonths, btnMNow.Text) + 1;
            summaryDates summary = new summaryDates();
            summary.reftoprefrom = this;
            this.Hide();
            summary.mnth = monthnum.ToString();
            summary.yr = btnYNow.Text;
            if(summary.ShowDialog() == DialogResult.OK)
            {

            }
        }
        #endregion

        #region Attendance
        //design and buttons
        private void tabAttend_Click(object sender, EventArgs e)
        {

        }

        private void tabList_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Blue, 4);
            g.DrawRectangle(p, this.tabList.Bounds);
        }

        private void btnOther_Click(object sender, EventArgs e)
        {
            tabList.SelectedTab = tabOtherList;
            tabAttendance.SelectedTab = tabOtherAttend;
        }

        private void btnChild_Click(object sender, EventArgs e)
        {
            tabList.SelectedTab = tabChildList;
            tabAttendance.SelectedTab = tabChildAttend;
        }

        private void tabAttendance_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.White, 4);
            g.DrawRectangle(p, this.tabAttendance.Bounds);
        }

        private void tabEventDetails_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Blue, 4);
            //g.DrawRectangle(p, this.tabEventDetails.Bounds);
        }

        private void tabERForm_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Blue, 4);
            //g.DrawRectangle(p, this.tabEventDetails.Bounds);
        }

        private void btnViewBudget_Click(object sender, EventArgs e)
        {
            btnViewBudget.Enabled = false;
        }

        private void btnAddAttendance_Click(object sender, EventArgs e)
        {
            CaseProfile();
            viewCaseAttendance(0);
            viewCaseAttendance(1);
            tabSecond.SelectedTab = tabAttend;
            tabAttendance.SelectedIndex = 1;
            btnAddAttendance.Enabled = false;
        }
        //functions
        public string[] casefname = new string[100];
        public string[] caselname = new string[100];
        public string clickCase, clickCAttended, clickOthers, clickOAttended;
        public void CaseProfile()
        {
            int cid; string attend = "False";
            int monthnum = Array.IndexOf(aMonths, btnMNow.Text) + 1;
            string datet = btnYNow.Text + "-" + monthnum.ToString("00") + "-" + cellday.ToString("00");
            try
            {
                conn.Open();
                MySqlCommand comm2 = new MySqlCommand("SELECT * FROM event WHERE eventID = '"+ evid +"'", conn);
                MySqlDataAdapter adp2 = new MySqlDataAdapter(comm2); DataTable dt2 = new DataTable(); adp2.Fill(dt2);
                MySqlCommand comm = new MySqlCommand("SELECT * FROM casestudyprofile", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm); DataTable dt = new DataTable(); adp.Fill(dt);
                if (dt2.Rows.Count >= 1) {
                    attend = dt2.Rows[0]["attendance"].ToString();
                }
                if (attend == "False")
                {
                    if (dt.Rows.Count >= 1)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            casefname[i] = dt.Rows[i]["lastName"].ToString();
                            caselname[i] = dt.Rows[i]["firstName"].ToString();
                            cid = int.Parse(dt.Rows[i]["caseID"].ToString());
                            conn.Close();
                            insertCaseView(casefname[i], caselname[i], cid, 0);
                        }
                    }
                }
                else
                {
                    MySqlCommand comm3 = new MySqlCommand("SELECT * FROM attendance WHERE attendDate = '" + datet + "' AND eventID = '" + evid + "'", conn);
                    MySqlDataAdapter adp3 = new MySqlDataAdapter(comm3); DataTable dt3 = new DataTable(); adp3.Fill(dt3);
                    if(dt3.Rows.Count == 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            casefname[i] = dt.Rows[i]["lastName"].ToString();
                            caselname[i] = dt.Rows[i]["firstName"].ToString();
                            cid = int.Parse(dt.Rows[i]["caseID"].ToString());
                            conn.Close();
                            insertCaseView(casefname[i], caselname[i], cid, 0);
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Nah!" + ee);
                conn.Close();
            }
        }

        public void insertCaseView(string cfn, string cln, int cid, int insnum)
        {
            int monthnum = Array.IndexOf(aMonths, btnMNow.Text) + 1;
            string cn = cln + ", " + cfn, datet = btnYNow.Text + "-" + monthnum.ToString("00") + "-" + cellday.ToString("00");
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand();
                if(insnum == 0)
                {
                    comm = new MySqlCommand("INSERT INTO attendance(eventID, caseID, attendee, status, role, attendDate) VALUES('" + evid + "','" + cid + "','" + cn + "', 'none', 'child', '" + datet + "')", conn);
                    MySqlCommand comm2 = new MySqlCommand("UPDATE event SET attendance = 'True' WHERE eventID ='" + evid + "'", conn);
                    comm.ExecuteNonQuery(); comm2.ExecuteNonQuery();
                }else if(insnum == 1)
                {
                    comm = new MySqlCommand("INSERT INTO attendance(eventID, attendee, status, role, attendDate) VALUES('" + evid + "','" + cn + "', 'none', '"+otherRole+"', '" + datet + "')", conn);
                    comm.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show("" + ee);
                conn.Close();
            }
        }

        public void viewCaseAttendance(int vnum)
        {
            int monthnum = Array.IndexOf(aMonths, btnMNow.Text) + 1;
            string datet = btnYNow.Text + "-" + monthnum.ToString("00") + "-" + cellday.ToString("00");
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM event WHERE eventID ='"+evid+"'", conn);
                MySqlDataAdapter ad = new MySqlDataAdapter(cmd); DataTable dat = new DataTable(); ad.Fill(dat);
                if(dat.Rows.Count >= 1)
                {
                    lbEvn.Text = dat.Rows[0]["evName"].ToString();
                    lbVen.Text = dat.Rows[0]["evVenue"].ToString();
                    lbDes.Text = dat.Rows[0]["evDesc"].ToString();
                    lbMon.Text = btnMNow.Text;
                    lbDay.Text = cellday.ToString("00");
                    lvTime.Text = eventsListView.Items[0].SubItems[0].Text;
                }
                MySqlCommand comm = new MySqlCommand();
                MySqlCommand comm2 = new MySqlCommand();
                if (vnum == 0)
                {
                    lvChildAttend.Items.Clear();
                    childList.Items.Clear();
                    comm = new MySqlCommand("SELECT * FROM attendance JOIN event ON attendance.eventID = event.eventID WHERE attendance.eventID = '" + evid + "' AND attendance.status = 'none' AND attendDate ='" + datet + "' AND role ='child'", conn);
                    MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    comm2 = new MySqlCommand("SELECT * FROM attendance JOIN event ON attendance.eventID = event.eventID WHERE attendance.eventID = '" + evid + "' AND role = 'child' AND attendDate ='" + datet + "' AND (attendance.status = 'Present' OR attendance.status = 'Absent')", conn);
                    MySqlDataAdapter adp2 = new MySqlDataAdapter(comm2);
                    DataTable dt2 = new DataTable();
                    adp2.Fill(dt2);
                    if (dt.Rows.Count >= 1)
                    {
                        string[] attendees = new string[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            attendees[i] = dt.Rows[i]["attendee"].ToString();
                            ListViewItem item = new ListViewItem(attendees[i]);
                            lvChildAttend.Items.Add(item);
                        }
                    }
                    if (dt2.Rows.Count >= 1)
                    {
                        string[] onlistAttendee = new string[dt2.Rows.Count];
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            onlistAttendee[i] = dt2.Rows[i]["attendee"].ToString();
                            ListViewItem itm = new ListViewItem(onlistAttendee[i]);
                            childList.Items.Add(itm);
                        }
                    }
                }
                else if(vnum == 1)
                {
                    viewOthersAttend.Items.Clear();
                    otherList.Items.Clear();
                    comm = new MySqlCommand("SELECT * FROM attendance JOIN event ON attendance.eventID = event.eventID WHERE attendance.eventID = '" + evid + "' AND attendance.status = 'none' AND attendDate ='" + datet + "' AND role != 'child'", conn);
                    MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    comm2 = new MySqlCommand("SELECT * FROM attendance JOIN event ON attendance.eventID = event.eventID WHERE attendance.eventID = '" + evid + "' AND role != 'child' AND attendDate ='" + datet + "' AND (attendance.status = 'Present' OR attendance.status = 'Absent')", conn);
                    MySqlDataAdapter adp2 = new MySqlDataAdapter(comm2);
                    DataTable dt2 = new DataTable();
                    adp2.Fill(dt2);

                    if (dt.Rows.Count >= 1)
                    {
                        string[] attendees = new string[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            attendees[i] = dt.Rows[i]["attendee"].ToString();
                            ListViewItem item = new ListViewItem(attendees[i]);
                            viewOthersAttend.Items.Add(item);
                        }
                    }
                    if (dt2.Rows.Count >= 1)
                    {
                        string[] onlistAttendee = new string[dt2.Rows.Count];
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            onlistAttendee[i] = dt2.Rows[i]["attendee"].ToString();
                            ListViewItem itm = new ListViewItem(onlistAttendee[i]);
                            otherList.Items.Add(itm);
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Nah!" + ee);
                conn.Close();
            }
        }
        public void viewAttended()
        {
            listAttendedView.Items.Clear();
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand("SELECT * FROM event JOIN attendance ON event.eventID = attendance.eventID WHERE attendance.eventID = '" + evid + "'", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm); DataTable dt = new DataTable(); adp.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    string[] datadate = new string[50];
                    string dfrom = dt.Rows[0]["evDateFrom"].ToString(), tfrom = dt.Rows[0]["evTimeFrom"].ToString();
                    string dTo = dt.Rows[0]["evDateTo"].ToString(), tto = dt.Rows[0]["evTimeTo"].ToString();
                    DateTime datefrom = DateTime.ParseExact(dfrom + " " + tfrom, "yyyy-MM-dd h:m tt", CultureInfo.InvariantCulture);
                    DateTime dateto = DateTime.ParseExact(dTo + " " + tto, "yyyy-MM-dd h:m tt", CultureInfo.InvariantCulture);
                    int days = (dateto - datefrom).Days;
                    for (int j = 0; j <= days; j++)
                    {
                        datadate[j] = datefrom.AddDays(j).ToString("yyyy-MM-dd");
                        listAttendedView.Columns.Add(datadate[j], 350);
                        MySqlCommand comm2 = new MySqlCommand("SELECT attendee, role, attendance.status FROM event JOIN attendance ON event.eventID = attendance.eventID WHERE attendance.eventID = '" + evid + "' AND attendDate = '" + datadate[j] + "' AND attendance.status != 'none'", conn);
                        MySqlDataAdapter adp2 = new MySqlDataAdapter(comm2); DataTable dt2 = new DataTable(); adp2.Fill(dt2);
                        if (dt2.Rows.Count >= 1)
                        {
                            string[] rowadd = new string[dt2.Rows.Count];
                            for (int i = 0; i < dt2.Rows.Count; i++)
                            {
                                ListViewItem itm = new ListViewItem("");
                                itm.SubItems.Add("");
                                listAttendedView.Items.Add(itm);
                                listAttendedView.Items[i].SubItems[j].Text = dt2.Rows[i]["attendee"].ToString() + " : " + dt2.Rows[i]["role"].ToString() + " : " + dt2.Rows[i]["status"].ToString();
                                //MessageBox.Show(dt2.Rows[i]["attendee"].ToString() + " : " + dt2.Rows[i]["role"].ToString() + " : " + dt2.Rows[i]["status"].ToString());
                            }
                        }
                    }
                    tabSecond.SelectedTab = tabViewAttend;
                    for (int k = 0; k < datadate.Length; k++) datadate[k] = null;
                }
                else
                {
                    errorMessage("No attendance in this event.");
                }
                conn.Close();
            }
            catch(Exception ee)
            {
                MessageBox.Show("" + ee);
                conn.Close();
            }
        }
        //others attendance
        public string othersFAttendee { get; set; }
        public string othersLAttendee { get; set; }
        public string otherRole { get; set; }

        private void btnAttendOthers_Click(object sender, EventArgs e)
        {
            OthersAttend oa = new OthersAttend();
            dim dim = new dim();
            oa.reftoEvAttend = this;
            dim.Location = this.Location;
            dim.Size = this.Size;
            dim.refToPrev = this;
            dim.Show(this);
            if (oa.ShowDialog() == DialogResult.OK)
            {
                insertCaseView(othersFAttendee, othersLAttendee, 0, 1);
                viewCaseAttendance(1);
                dim.Close();
            }
        }

        private void btnVAttend_Click(object sender, EventArgs e)
        {
            viewAttended();
        }
        private void btnPresent_Click(object sender, EventArgs e)
        {
            if (lvChildAttend.SelectedItems.Count > 0)
            {
                if(cellEvProg == "Ongoing")
                {
                    if (checkIfDoubleAttend())
                    {
                        confirmMessage("This child is currently attending in the event " + evnameconflict + ". Would you like to end the previous attendance of this child and continue?");
                        if (confirmed)
                        {
                            updateCAttend(9);
                            updateCAttend(0);
                            viewCaseAttendance(0);
                        }
                        else
                        {
                            updateCAttend(0);
                            viewCaseAttendance(0);
                        }
                    }
                    else
                    {
                        updateCAttend(0);
                        viewCaseAttendance(0);
                    }
                    
                }
                else if(cellEvProg == "Finished"){
                    updateCAttend(3);
                    viewCaseAttendance(0);
                }
            }
            else if(viewOthersAttend.SelectedItems.Count > 0)
            {
                updateCAttend(6);
                viewCaseAttendance(1);
            }
        }

        private void btnAbsent_Click(object sender, EventArgs e)
        {
            if (lvChildAttend.SelectedItems.Count > 0)
            {
                if (cellEvProg == "Ongoing")
                {
                    updateCAttend(1);
                    viewCaseAttendance(0);
                }
                else if (cellEvProg == "Finished")
                {
                    updateCAttend(4);
                    viewCaseAttendance(0);
                }
            }
            else if (viewOthersAttend.SelectedItems.Count > 0)
            {
                updateCAttend(7);
                viewCaseAttendance(1);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (childList.SelectedItems.Count > 0)
            {
                if (cellEvProg == "Ongoing")
                {
                    updateCAttend(2);
                    viewCaseAttendance(0);
                }
                else if (cellEvProg == "Finished")
                {
                    updateCAttend(5);
                    viewCaseAttendance(0);
                }
            }
            else if (otherList.SelectedItems.Count > 0)
            {
                updateCAttend(8);
                viewCaseAttendance(1);
            }
        }

        public string caseConflict, eventConflictID, evnameconflict;
        public bool checkIfDoubleAttend()//check child if already attended and prompt user if so
        {
            bool check = true;
            int monthnum = Array.IndexOf(aMonths, btnMNow.Text) + 1;
            string dnow = btnYNow.Text + "-" + monthnum.ToString("00") + "-" + cellday.ToString("00");
            string tnow = DateTime.Now.ToString("h:mm tt"); DateTime timenow = Convert.ToDateTime(tnow);
            string tto = "";
            if (ondaytime.Length == 8) tto = ondaytime;
            else tto = ondaytime.Substring(11, 8);
            DateTime timeto = Convert.ToDateTime(tto);
            string combidtF = dnow + " " + timenow.ToString("HH:mm"), combidtT = dnow + " " + timeto.ToString("HH:mm");

            try
            {
                MySqlCommand comm2 = new MySqlCommand("SELECT * FROM attendance WHERE eventID = '" + evid + "' AND role = 'child' AND attendDate ='" + dnow + "' AND (status = 'Present' OR status = 'Absent')", conn);
                MySqlDataAdapter adp2 = new MySqlDataAdapter(comm2);
                DataTable dt2 = new DataTable();
                adp2.Fill(dt2);
                if (dt2.Rows.Count == 0)
                {
                    MySqlCommand comm = new MySqlCommand("SELECT * FROM attendance JOIN event ON attendance.eventID = event.eventID WHERE attendee = '" + clickCase + "' AND attendance.status = 'Present' AND attendDate = '" + dnow + "' AND  (('" + combidtF + "' >= str_to_date(CONCAT(attendDate,' ', attendTimeIn), '%Y-%m-%d %h:%i %p')) AND ('" + combidtT + "' <= str_to_date(CONCAT(attendDate,' ', attendTimeOut), '%Y-%m-%d %h:%i %p')) OR ('" + combidtF + "' <= str_to_date(CONCAT(attendDate,' ', attendTimeOut), '%Y-%m-%d %h:%i %p')) AND ('" + combidtT + "' >= str_to_date(CONCAT(attendDate,' ', attendTimeIn), '%Y-%m-%d %h:%i %p')))", conn);
                    MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    if (dt.Rows.Count >= 1) {
                        caseConflict = dt.Rows[0]["attendee"].ToString(); eventConflictID = dt.Rows[0]["eventID"].ToString(); evnameconflict = dt.Rows[0]["evName"].ToString();
                        check = true;
                    }
                    else check = false;
                }
            }
            catch(Exception ee)
            {
                errorMessage("" + ee);
                conn.Close();
            }
            return check;
        }

        public void updateCAttend(int anum)
        {
            int monthnum = Array.IndexOf(aMonths, btnMNow.Text) + 1;
            string datet = btnYNow.Text + "-" + monthnum.ToString("00") + "-" + cellday.ToString("00");
            string tnow = DateTime.Now.ToString("hh:mm tt");
            string tto = "";
            if (ondaytime.Length == 8) tto = ondaytime;
            else tto = ondaytime.Substring(11, 8);
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand();
                if (anum == 0) comm = new MySqlCommand("UPDATE attendance SET status ='Present', attendTimeIn = '" + tnow + "', attendTimeOut='" + tto + "' WHERE attendee = '" + clickCase + "' AND eventID = '" + evid + "' AND attendDate = '" + datet + "';", conn);//present child ongoing event
                else if (anum == 1) comm = new MySqlCommand("UPDATE attendance SET status ='Absent', attendTimeIn = '" + tnow + "', attendTimeOut='" + tto + "' WHERE attendee = '" + clickCase + "' AND eventID = '" + evid + "' AND attendDate = '" + datet + "';", conn);//absent child ongoing event
                else if (anum == 2) comm = new MySqlCommand("UPDATE attendance SET status ='none' , attendTimeIn = '', attendTimeOut='' WHERE attendee = '" + clickCAttended + "' AND eventID = '" + evid + "' AND attendDate = '" + datet + "';", conn);//remove from list child ongoing event
                else if (anum == 3) comm = new MySqlCommand("UPDATE attendance SET status ='Present' WHERE attendee = '" + clickCase + "' AND eventID = '" + evid + "' AND attendDate = '" + datet + "';", conn);//present child finished event
                else if (anum == 4) comm = new MySqlCommand("UPDATE attendance SET status ='Absent' WHERE attendee = '" + clickCase + "' AND eventID = '" + evid + "' AND attendDate = '" + datet + "'", conn);//absent child finished event
                else if (anum == 5) comm = new MySqlCommand("UPDATE attendance SET status ='none' WHERE attendee = '" + clickCAttended + "' AND eventID = '" + evid + "' AND attendDate = '" + datet + "';", conn);//remove from list child finished event
                else if (anum == 6) comm = new MySqlCommand("UPDATE attendance SET status ='Present' WHERE attendee = '" + clickOthers + "' AND eventID = '" + evid + "' AND attendDate = '" + datet + "';", conn);//present others
                else if (anum == 7) comm = new MySqlCommand("UPDATE attendance SET status ='Absent' WHERE attendee = '" + clickOthers + "' AND eventID = '" + evid + "' AND attendDate = '" + datet + "';", conn);//absent others
                else if (anum == 8) comm = new MySqlCommand("UPDATE attendance SET status ='none' WHERE attendee = '" + clickOAttended + "' AND eventID = '" + evid + "' AND attendDate = '" + datet + "';", conn);//removefrom list others
                else if (anum == 9) comm = new MySqlCommand("UPDATE attendance SET status ='Present', attendTimeOut='" + tnow + "' WHERE attendee = '" + caseConflict + "' AND eventID = '" + eventConflictID + "' AND attendDate = '" + datet + "';", conn);//present child conflict ongoing event
                comm.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show("" + ee);
                conn.Close();
            }
        }
        private void lvChildAttend_MouseClick(object sender, MouseEventArgs e)
        {
            clickCase = lvChildAttend.SelectedItems[0].SubItems[0].Text;
            btnRemove.Enabled = false; btnAbsent.Enabled = true; btnPresent.Enabled = true;
        }
        private void childList_MouseClick(object sender, MouseEventArgs e)
        {
            clickCAttended = childList.SelectedItems[0].SubItems[0].Text;
            btnRemove.Enabled = true; btnAbsent.Enabled = false; btnPresent.Enabled = false;
        }
        private void viewOthersAttend_MouseClick(object sender, MouseEventArgs e)
        {
            clickOthers = viewOthersAttend.SelectedItems[0].SubItems[0].Text;
            btnRemove.Enabled = false; btnAbsent.Enabled = true; btnPresent.Enabled = true;
        }
        private void otherList_MouseClick(object sender, MouseEventArgs e)
        {
            clickOAttended = otherList.SelectedItems[0].SubItems[0].Text;
            btnRemove.Enabled = true; btnAbsent.Enabled = false; btnPresent.Enabled = false;
        }

        private void attendBack_Click(object sender, EventArgs e)
        {
            tabSecond.SelectedTab = tabEvent;
        }
        private void noFocusRec1_Click(object sender, EventArgs e)
        {
            tabSecond.SelectedTab = tabEvent;
        }
        #endregion

        #region Add Budget
        public string evNameBudget = "";
        public int bid { get; set; } 
        public decimal btotal { get; set; }
        private void btnAddBudget_Click(object sender, EventArgs e)
        {
            evNameBudget = txtEventName.Text;
            eventBudget evbudget = new eventBudget();
            evbudget.reftoeventorg = this;
            this.Hide();
            if(evbudget.ShowDialog() == DialogResult.OK) {
                confirm_EBudget.Text = btotal.ToString();
            }
        }
        #endregion 

        private void confirmTab_Enter(object sender, EventArgs e)
        {
            lblRequestBy.ForeColor = System.Drawing.ColorTranslator.FromHtml("#acacac");
            txtRequestBy.ForeColor = System.Drawing.ColorTranslator.FromHtml("#acacac");
        }
        private void eventorg_FormClosing(object sender, FormClosingEventArgs e)
        {
            reftomain.Show();
        }
    }
}
