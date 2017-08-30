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
        public String[] aMonths = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        public bool remindState = false, budgetState = false, allDayState = false, timeRngState = false;

        public eventorg()
        {
            InitializeComponent();
            tabSecond.SelectedTab = tabCalendar;
            conn = new MySqlConnection("Server=localhost;Database=prototype_sad;Uid=root;Pwd=root;");
            //Load += btnEvent_Click; onload automatically click the btnevent which instantiating it always on tabevent onload
            menuStrip.Renderer = new renderer();
            menuStrip1.Renderer = new renderer();
            menuStripEvent.Renderer = new renderer();
            ERProgress.Renderer = new renderer2();
        }
        
        #region Functions
        private void resetButtons()
        {
            btnEvent.BackColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            btnRequest.BackColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
        }

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
            eventTS.BackColor = System.Drawing.ColorTranslator.FromHtml("#393939");
            attendanceTS.BackColor = System.Drawing.ColorTranslator.FromHtml("#393939");
        }

        private void resetLabelsPanels()                // Set label and panel colors to default (gray)
        {
            lblEventName.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a");            
            lblEVenue.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a");            
            lblEType.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a");
            lblEDes.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a"); 
                      
            lblEDate.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a");

            lblRDate.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a");

            lblRequestBy.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a");  

            panelEName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
            panelEVenue.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
            panelRequestBy.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
        }

        private void resetCounters()
        {
            countEName.Visible = false;
            countEVenue.Visible = false;
            countEDes.Visible = false;
            countRequestBy.Visible = false;
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
        #endregion

        #region Main Buttons
        private void btnRequest_Click(object sender, EventArgs e)
        {
            tabSecond.SelectedTab = tabRequest;
            resetButtons();
            btnEvent.BackgroundImage = global::BalayPasilungan.Properties.Resources.events_white;
            btnRequest.BackgroundImage = global::BalayPasilungan.Properties.Resources.request_green;
            btnRequest.BackColor = Color.White;
            timeDateCombo(DateTime.Now.Day - 1, DateTime.Now.Month - 1, DateTime.Now.Year.ToString());
        }

        private void btnEvent_Click(object sender, EventArgs e)
        {
            menuStrip.Height = 0;
            timer1.Enabled = true;

            tabSecond.SelectedTab = tabCalendar;
            resetTS();
            eventTS.BackColor = System.Drawing.ColorTranslator.FromHtml("#2d2d2d");

            resetButtons();
            btnRequest.BackgroundImage = global::BalayPasilungan.Properties.Resources.request_white;
            btnEvent.BackgroundImage = global::BalayPasilungan.Properties.Resources.events_green;
            btnEvent.BackColor = Color.White;
        }

        private void btnMain_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Timers
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (menuStrip.Height >= 35) timer1.Enabled = false;
            else menuStrip.Height += 3;            
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
            attendanceTS.BackColor = System.Drawing.ColorTranslator.FromHtml("#2d2d2d");
            tabSecond.SelectedTab = tabAttend;
        }

        private void eventTS_Click(object sender, EventArgs e)
        {
            resetTS();
            eventTS.BackColor = System.Drawing.ColorTranslator.FromHtml("#2d2d2d");
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
            eventTS.BackColor = System.Drawing.ColorTranslator.FromHtml("#2d2d2d");
        }

        private void tabSecond_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Blue, 4);
            g.DrawRectangle(p, this.tabSecond.Bounds);
        }

        private void attendanceTS2_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Event Request
        private void pendingRequestTS_Click(object sender, EventArgs e)
        {
            tabSecond.SelectedTab = tabPending;
        }

        private void addEventTS2_Click(object sender, EventArgs e)
        {
            tabSecond.SelectedTab = tabRequest;
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

        #region Attendance
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
            g.DrawRectangle(p, this.tabEventDetails.Bounds);
        }

        private void tabERForm_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Blue, 4);
            g.DrawRectangle(p, this.tabEventDetails.Bounds);
        }

        private void btnAddAttendance_Click(object sender, EventArgs e)
        {
            tabSecond.SelectedTab = tabAttend;
        }
        #endregion

        #region Next Back Buttons (request events)       
        private void btnNext_Click(object sender, EventArgs e)
        {
            tabERForm.SelectedIndex = 1;
            tdTS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#18764e");
            edTS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c5d9d0");
        }

        private void btnNext2_Click(object sender, EventArgs e)
        {
            error err = new error();
            if (cbEMonth.Text == "" || cbEDay.Text == "" || cbEYear.Text == "" ||
            cbEMonth2.Text == "" || cbEDay2.Text == "" || cbEYear2.Text == "")
            {
                err.refToERF = this;
                err.lblError.Text = "You have skipped a blank! Please answer everything.";
                err.ShowDialog();
            }
            else
            {
                tabERForm.SelectedIndex = 2;
                oTS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#18764e");
                tdTS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c5d9d0");
                confirm_EDateTime.Text = "FROM: " + cbEYear.Text + "-" + cbEMonth.Text + "-" + cbEDay.Text + " " + txtEHours.Text + ":" + txtEMins + " " + ampmFrom + "\n" +
                                         "TO: " + cbEYear2.Text + "-" + cbEMonth2.Text + "-" + cbEDay2.Text + " " + txtEHours2.Text + ":" + txtEMins2.Text + " " + ampmTo;
            }
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
            error err = new error();
            if(txtEventDes.Text.Equals("Describe the event.") ||
            txtEventName.Text.Equals("What is the name of the event?") ||
            txtVenue.Text.Equals("Where will it be held?"))
            {
                err.refToERF = this;
                err.lblError.Text = "You have skipped a blank! Please answer everything.";
                err.ShowDialog();
            }
            else
            {
                tabERForm.SelectedIndex = 3;
                confirmTS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#18764e");
                oTS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c5d9d0");
                if(budgetYN == "yes")
                {

                }
                else if(budgetYN == "no")
                {
                    confirm_EBudget.Text = "NONE";
                }
                if(remindYN == "yes")
                {

                }
                else if(remindYN == "no")
                {
                    confirm_ERemind.Text = "NONE";
                }
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
        private void txtEventName_Enter(object sender, EventArgs e)
            {
                resetLabelsPanels(); resetCounters();
                txtEventName.ForeColor = Color.Black;            
                if (txtEventName.Text.Equals("What is the name of the event?")) txtEventName.Text = "";
                panelEName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_green;
                lblEventName.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
                countEName.Visible = true;       
            }

            private void txtVenue_Enter(object sender, EventArgs e)
            {
                resetLabelsPanels(); resetCounters();
                txtVenue.ForeColor = Color.Black;
                if (txtVenue.Text.Equals("Where will it be held?")) txtVenue.Text = "";
                panelEVenue.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_green;
                lblEVenue.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
                countEVenue.Visible = true;
            }

            private void txtEventDes_Enter(object sender, EventArgs e)
            {
                resetLabelsPanels(); resetCounters();
                txtEventDes.ForeColor = Color.Black;
                if (txtEventDes.Text.Equals("Describe the event.")) txtEventDes.Text = "";
                lblEDes.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
                countEDes.Visible = true;
            }
        
            private void cbEType_Enter(object sender, EventArgs e)
            {
                resetLabelsPanels();
                lblEType.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            }

            private void eventDate_Enter(object sender, EventArgs e)
            {
                resetLabelsPanels();
                lblEDate.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            }

            private void txtRequestBy_Enter(object sender, EventArgs e)
            {
                txtRequestBy.ForeColor = Color.Black;
                lblQuestion.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a");
                if (txtRequestBy.Text.Equals("Who requested the event?")) txtRequestBy.Text = "";
                resetLabelsPanels();
                panelRequestBy.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_green;
                lblRequestBy.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
                countRequestBy.Visible = true;
            }

            private void reminderDate_Enter(object sender, EventArgs e)
            {
                resetLabelsPanels();
                lblRDate.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            }

            private void others_Enter(object sender, EventArgs e)
            {
                lblQuestion.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
                lblNo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
                reminderPanel.Height = 0;
            }
            #endregion

            #region Leave Methods
            private void txtRequestBy_Leave(object sender, EventArgs e)
            {
                resetLabelsPanels(); resetCounters();
            }
        
            private void txtEventName_Leave(object sender, EventArgs e) //BOOK2
            {
                resetLabelsPanels(); resetCounters();
                txtEventName.ForeColor = System.Drawing.ColorTranslator.FromHtml("#878787");
                if (txtEventName.Text.Equals("")) txtEventName.Text = "What is the name of the event?";
            }

            private void txtEventDes_Leave(object sender, EventArgs e)
            {
                resetLabelsPanels(); resetCounters();
                txtEventDes.ForeColor = System.Drawing.ColorTranslator.FromHtml("#878787");
                if (txtEventDes.Text.Equals("")) txtEventDes.Text = "Describe the event.";
            }

            private void txtVenue_Leave(object sender, EventArgs e)
            {
                resetLabelsPanels(); resetCounters();
                txtVenue.ForeColor = System.Drawing.ColorTranslator.FromHtml("#878787");
                if (txtVenue.Text.Equals("")) txtVenue.Text = "Where will it be held?";
            }

            private void eventdetails_Click(object sender, EventArgs e)
            {
               resetLabelsPanels();          
            }        
        #endregion

        private void btnOther_MouseHover(object sender, EventArgs e)
        {
            btnOther.BackColor = Color.Transparent;
        }
        
        #endregion

        #region Event Request Form Counters        
        private void txtEventName_TextChanged(object sender, EventArgs e)
        {            
            int count = txtEventName.Text.Length;            
            countEName.Text = count + "/100";
            confirm_EName.Text = txtEventName.Text;
        }

        private void txtVenue_TextChanged(object sender, EventArgs e)
        {
            int count = txtVenue.Text.Length;
            countEVenue.Text = count + "/100";
            confirm_EVenue.Text = txtVenue.Text;
        }

        private void txtEventDes_TextChanged(object sender, EventArgs e)
        {
            int count = txtEventDes.Text.Length;
            countEDes.Text = count + "/100";
            confirm_EDes.Text = txtEventDes.Text;
        }

        private void txtRequestBy_TextChanged(object sender, EventArgs e)
        {
            int count = txtRequestBy.Text.Length;
            countRequestBy.Text = count + "/100";            
        }
        #endregion

        #region Day Buttons
        private void btnAllDay_Click(object sender, EventArgs e)
        {
            resetDayButtons();
            btnAllDay.ForeColor = Color.White;            
            btnAllDay.BackColor = System.Drawing.ColorTranslator.FromHtml("#5ea6e9");            
            btnAllDay.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#5ea6e9");
            cbEMonth.Enabled = true; cbEDay.Enabled = true; cbEYear.Enabled = true;
            cbEMonth2.Enabled = true; cbEDay2.Enabled = true; cbEYear2.Enabled = true;
            //cbEMonth2.Visible = false; cbEDay2.Visible = false; cbEYear2.Visible = false;
            lbAllDay.Visible = true; btnRAllDay.Visible = true;
            lbBlock.Visible = false; lbStraight.Visible = false;
            btnTmRng.Visible = false;
            //panelEHours2.Visible = true; panelEMins2.Visible = true; lblColon2.Visible = true; btnAM2.Visible = true; btnPM2.Visible = true;
        }

        private void btnMulDay_Click(object sender, EventArgs e)
        {
            resetDayButtons();
            btnMulDay.ForeColor = Color.White;
            btnMulDay.BackColor = System.Drawing.ColorTranslator.FromHtml("#5ea6e9");
            btnMulDay.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#5ea6e9");
            cbEMonth.Enabled = true; cbEDay.Enabled = true; cbEYear.Enabled = true;
            cbEMonth2.Enabled = true; cbEDay2.Enabled = true; cbEYear2.Enabled = true;
            //cbEMonth2.Visible = true; cbEDay2.Visible = true; cbEYear2.Visible = true;
            lbAllDay.Visible = false; btnRAllDay.Visible = false;
            lbBlock.Visible = true; lbStraight.Visible = true;
            btnTmRng.Visible = true;
            //panelEHours2.Visible = true; panelEMins2.Visible = true; lblColon2.Visible = true;  btnAM2.Visible = true; btnPM2.Visible = true;
        }

        #endregion

        #region Switches
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
                reminderDate.Enabled = true;
            }
            else
            {
                lblYes.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
                lblNo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
                btnRemind.BackgroundImage = global::BalayPasilungan.Properties.Resources.off;
                remindState = false;
                reminderPanel.Height = 0;
                remindYN = "no";
                reminderDate.Enabled = false;
            }
        }

        private void bntRAllDay_Click(object sender, EventArgs e)
        {
            if (!allDayState)
            {
                lbAllDay.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
                btnRAllDay.BackgroundImage = global::BalayPasilungan.Properties.Resources.on;
                allDayState = true;
                txtEHours.Text = "12"; txtEHours2.Text = "12";
                btnAM.PerformClick(); btnAM2.PerformClick();
            }
            else
            {
                lbAllDay.ForeColor = Color.FromArgb(42, 42, 42);
                btnRAllDay.BackgroundImage = global::BalayPasilungan.Properties.Resources.off;
                allDayState = false;
                txtEHours.Text = "00"; txtEHours2.Text = "00";
                btnAM.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
                btnAM2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");

            }
        }

        private void btnTmRng_Click(object sender, EventArgs e)
        {
            if (!timeRngState)
            {
                lbBlock.ForeColor = Color.FromArgb(42, 42, 42);
                lbStraight.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
                btnTmRng.BackgroundImage = global::BalayPasilungan.Properties.Resources.on;
                timeRngState = true;
            }
            else
            {
                lbBlock.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
                lbStraight.ForeColor = Color.FromArgb(42, 42, 42);
                btnTmRng.BackgroundImage = global::BalayPasilungan.Properties.Resources.on2;
                timeRngState = false;
            }
        }
        #endregion

        #region Event Request Time and Date Data Validation
        public void timeDateCombo(int day, int month,  string year)
        {
            int last = 0;
            cbEMonth.SelectedIndex = month; cbEMonth2.SelectedIndex = month;

            // Days (in relation to months)
            if (cbEDay.Items.Count == 0 && cbEDay2.Items.Count == 0)
            {
                if (cbEMonth.SelectedIndex == 3 || cbEMonth.SelectedIndex == 5 || cbEMonth.SelectedIndex == 8 || cbEMonth.SelectedIndex == 10
                    ||
                    cbEMonth2.SelectedIndex == 3 || cbEMonth2.SelectedIndex == 5 || cbEMonth2.SelectedIndex == 8 || cbEMonth2.SelectedIndex == 10) last = 30;
                else if (cbEMonth.SelectedIndex == 0 || cbEMonth.SelectedIndex == 2 || cbEMonth.SelectedIndex == 4 ||
                        cbEMonth.SelectedIndex == 6 || cbEMonth.SelectedIndex == 7 || cbEMonth.SelectedIndex == 9 || cbEMonth.SelectedIndex == 11
                        ||
                        cbEMonth2.SelectedIndex == 0 || cbEMonth2.SelectedIndex == 2 || cbEMonth2.SelectedIndex == 4 ||
                        cbEMonth2.SelectedIndex == 6 || cbEMonth2.SelectedIndex == 7 || cbEMonth2.SelectedIndex == 9 || cbEMonth2.SelectedIndex == 11) last = 31;
                else last = 28;

                for (int i = 1; i <= last; i++)
                {
                    if (i < 10)
                    {
                        cbEDay.Items.Add("0" + i); cbEDay2.Items.Add("0" + i);
                    }
                    else
                    {
                        cbEDay.Items.Add(i); cbEDay2.Items.Add(i);
                    }
                }
            }

            // Years
            if (cbEYear.Items.Count == 0)
            {
                for (int i = DateTime.Now.Year; i <= 2099; i++)
                {
                    cbEYear.Items.Add(i); cbEYear2.Items.Add(i);
                }
            }

            // Hours
            /*for (int i = 1; i <= 12; i++)
            {
                if (i < 10)
                {
                    cbEHours.Items.Add("0" + i);
                    //cbEDay2.Items.Add("0" + i);
                }
                else
                {
                    cbEHours.Items.Add(i);
                    //cbEDay2.Items.Add(i);
                }
            }

            // Minutes
            for (int i = 0; i <= 59; i++)
            {
                if (i < 10)
                {
                    cbEMins.Items.Add("0" + i);
                    //cbEDay2.Items.Add("0" + i);
                }
                else
                {
                    cbEMins.Items.Add(i);
                    //cbEDay2.Items.Add(i);
                }
            }*/

            cbEDay.SelectedIndex = day;
            cbEYear.Text = year;
            cbEDay2.SelectedIndex = day;
            cbEYear2.Text = year;
            //cbEHours.SelectedIndex = 0; cbEMins.SelectedIndex = 0; 
        }

        private void cbEMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbEMonth2.SelectedIndex = cbEMonth.SelectedIndex;
        }

        private void cbEDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbEDay2.SelectedIndex = cbEDay.SelectedIndex;
        }

        private void cbEYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbEYear2.SelectedIndex = cbEYear.SelectedIndex;
        }

        private void cbEMonth2_SelectedIndexChanged(object sender, EventArgs e)
        {
            error err = new error();
            err.refToERF = this;

            if ((cbEMonth2.SelectedIndex < cbEMonth.SelectedIndex) &&
                (cbEYear.SelectedIndex == cbEYear2.SelectedIndex))
            {
                err.lblError.Text = "You cannot set this to an earlier month.";
                err.ShowDialog();
                cbEMonth2.SelectedIndex = cbEMonth.SelectedIndex;
            }
        }

        private void cbEYear2_SelectedIndexChanged(object sender, EventArgs e)
        {
            error err = new error();
            err.refToERF = this;

            if (cbEYear2.SelectedIndex < cbEYear.SelectedIndex)
            {
                err.lblError.Text = "You cannot set this to an earlier year.";
                err.ShowDialog();
                cbEYear2.SelectedIndex = cbEYear.SelectedIndex;
            }
        }

        private void cbEDay_Enter(object sender, EventArgs e)
        {
            int last = 0;
            cbEDay.Items.Clear();
            if (cbEMonth.SelectedIndex == 3 || cbEMonth.SelectedIndex == 5 || cbEMonth.SelectedIndex == 8 || cbEMonth.SelectedIndex == 10) last = 30;
            else if (cbEMonth.SelectedIndex == 0 || cbEMonth.SelectedIndex == 2 || cbEMonth.SelectedIndex == 4 ||
                    cbEMonth.SelectedIndex == 6 || cbEMonth.SelectedIndex == 7 || cbEMonth.SelectedIndex == 9 || cbEMonth.SelectedIndex == 11) last = 31;
            else last = 28;

            for (int i = 1; i <= last; i++)
            {
                if (i < 10) cbEDay.Items.Add("0" + i);
                else cbEDay.Items.Add(i);
            }
        }

        private void cbEDay2_Enter(object sender, EventArgs e)
        {
            int last = 0;
            cbEDay2.Items.Clear();
            if (cbEMonth2.SelectedIndex == 3 || cbEMonth2.SelectedIndex == 5 || cbEMonth2.SelectedIndex == 8 || cbEMonth2.SelectedIndex == 10) last = 30;
            else if (cbEMonth2.SelectedIndex == 0 || cbEMonth2.SelectedIndex == 2 || cbEMonth2.SelectedIndex == 4 ||
                    cbEMonth2.SelectedIndex == 6 || cbEMonth2.SelectedIndex == 7 || cbEMonth2.SelectedIndex == 9 || cbEMonth2.SelectedIndex == 11) last = 31;
            else last = 28;

            for (int i = 1; i <= last; i++)
            {
                if (i < 10) cbEDay2.Items.Add("0" + i);
                else cbEDay2.Items.Add(i);
            }
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

        private void txtEHours_Leave(object sender, EventArgs e)
        {
            error err = new error();
            err.refToERF = this;
            if (int.Parse(txtEHours.Text) > 12 || int.Parse(txtEHours.Text) <= 0)
            {                
                err.lblError.Text = "You cannot set the hours beyond 12 or less than 0.";
                err.ShowDialog();
                txtEHours.Text = "00";
                txtEHours.Focus();
            }
            else
            {
                if (int.Parse(txtEHours.Text) < 10 && txtEHours.TextLength < 2) txtEHours.Text = "0" + txtEHours.Text;
            }            
        }

        private void txtEMins_Leave(object sender, EventArgs e)
        {
            error err = new error();
            err.refToERF = this;
            if (int.Parse(txtEMins.Text) > 59 || int.Parse(txtEMins.Text) < 0)
            {
                err.lblError.Text = "You cannot set the minutes beyond 59 or less than 0.";
                err.ShowDialog();                
                txtEMins.Text = "00";
                txtEMins.Focus();
            }
            else
            {
                if (int.Parse(txtEMins.Text) < 10 && txtEMins.TextLength < 2) txtEMins.Text = "0" + txtEMins.Text;
            }
        }

        private void txtEHours2_Leave(object sender, EventArgs e)
        {
            error err = new error();
            err.refToERF = this;
            if (int.Parse(txtEHours2.Text) > 12 || int.Parse(txtEHours2.Text) <= 0)
            {
                err.lblError.Text = "You cannot set the hours beyond 12 or less than 0.";
                err.ShowDialog();
                txtEHours2.Text = "00";
                txtEHours2.Focus();
            }
            else
            {
                if (int.Parse(txtEHours2.Text) < 10 && txtEHours2.TextLength < 2) txtEHours2.Text = "0" + txtEHours2.Text;
            }
        }

        private void txtEMins2_Leave(object sender, EventArgs e)
        {
            error err = new error();
            err.refToERF = this;
            if (int.Parse(txtEMins2.Text) > 59 || int.Parse(txtEMins2.Text) < 0)
            {
                err.lblError.Text = "You cannot set the minutes beyond 59 or less than 0.";
                err.ShowDialog();
                txtEMins2.Text = "00";
                txtEMins2.Focus();
            }
            else
            {
                if (int.Parse(txtEMins2.Text) < 10 && txtEMins2.TextLength < 2) txtEMins2.Text = "0" + txtEMins2.Text;
            }
        }
        private void cbEDay2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEDay2.SelectedItem == null) cbEDay2.SelectedIndex = cbEDay.SelectedIndex;            
        }

        #endregion

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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            tabSecond.SelectedTab = tabCalendar;
        }

        private void cbEType_Leave(object sender, EventArgs e)
        {

        }

        private void eventorg_Load(object sender, EventArgs e)
        {
            btnMPrev.Text = aMonths[DateTime.Now.Month - 2];
            btnMNow.Text = aMonths[DateTime.Now.Month - 1];
            btnMNext.Text = aMonths[DateTime.Now.Month];
            btnYPrev.Text = (DateTime.Now.Year - 1).ToString();
            btnYNow.Text = DateTime.Now.Year.ToString();
            btnYNext.Text = (DateTime.Now.Year + 1).ToString();
            //tabSecond.SelectedTab = tabCalendar;
            int monthnum = Array.IndexOf(aMonths, btnMNow.Text) + 1;
            displayCalendar(monthnum.ToString("00"), int.Parse(btnYNow.Text));
           
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

        #region tabEvent Funtions
        public void displayEvents(string dc, string mc, string yc)
        {
            string date = yc + "-" + mc + "-" + dc;
            //int evyear;
            string evname, day, timefrom;
            //MessageBox.Show(m + " "+ yearnav);
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
                        //int dateto = int.Parse(dt.Rows[i]["evDateTo"].ToString());
                        //MessageBox.Show();
                        //evmonth = int.Parse(dt.Rows[i]["evDateFrom"].ToString().Substring(5, 2));
                        //evday = int.Parse(dt.Rows[i]["evDateFrom"].ToString().Substring(8, 2));
                        timefrom = dt.Rows[i]["evTimeFrom"].ToString();
                        evname = dt.Rows[i]["evName"].ToString();
                        ListViewItem itm = new ListViewItem(timefrom);
                        itm.SubItems.Add(evname);
                        eventsListView.Items.Add(itm);
                    }
                }


            }
            catch (Exception ee)
            {
                MessageBox.Show("Nah!" + ee);
                conn.Close();
            }
        }


        #endregion

        #region tabRequest functions
        public string ampmFrom = "", ampmTo = "", remindYN = "", budgetYN = "";
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            error err = new error();
            if(txtRequestBy.Text == "")
            {
                err.refToERF = this;
                err.lblError.Text = "You have skipped a blank! Please answer everything.";
                err.ShowDialog();
            }
            else
            {

            }
        }
        //insert functions for requesting
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
                ya.Width = 132;
            }
            foreach (DataGridViewRow ro in CalendarView.Rows)
            {
                ro.Height = 106;
            }
            calendarcolor();
            //MessageBox.Show("Lagyan ng dialog box sa cell click add request and view request");
            //Lagyan ng dialog box sa cell click add request and view request
        }

        public void calendarcolor()
        {
            int dfrom, mfrom, yfrom;
            int dto, mto, yto;
            int m = Array.IndexOf(aMonths, btnMNow.Text) + 1;
            int year = int.Parse(btnYNow.Text);
            string prog;
            try
            {

                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT * FROM event WHERE status = 'Approved' ", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        prog = dt.Rows[i]["evProgress"].ToString();
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
                        //MessageBox.Show(""+ datefrom + "-----" + dateto);
                        //MessageBox.Show("" + dto);

                        foreach (DataGridViewRow row in CalendarView.Rows)
                        {
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                //do operations with cell
                                //MessageBox.Show(""+cell.Value);

                                if (cell.Value.ToString() != "")
                                {
                                    //MessageBox.Show(Convert.ToInt32(cell.Value).ToString("00") + "/" + m.ToString("00") + "/" + year.ToString("0000"));
                                    //DateTime datecal = Convert.ToDateTime(Convert.ToInt32(cell.Value).ToString("00") + "/" + m.ToString("00") + "/" + year.ToString("0000"));
                                    DateTime datecal = DateTime.ParseExact(Convert.ToInt32(cell.Value).ToString("00") + "/" + m.ToString("00") + "/" + year.ToString("0000"), "d/M/yyyy", CultureInfo.InvariantCulture);
                                    if (datecal >= datefrom && datecal <= dateto)
                                    {
                                        if (prog == "Ongoing")
                                        {
                                            CalendarView.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Style.BackColor = Color.Green;
                                        }
                                        else if (prog == "Finished")
                                        {
                                            CalendarView.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Style.BackColor = Color.Red;
                                        }
                                        else if (prog == "Upcoming")
                                        {
                                            CalendarView.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Style.BackColor = Color.Yellow;
                                        }
                                        else
                                        {
                                            CalendarView.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Style.BackColor = Color.White;
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

        private void CalendarView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int monthnum = Array.IndexOf(aMonths, btnMNow.Text) + 1;
            //MessageBox.Show(Convert.ToInt32(CalendarView.SelectedCells[0].Value).ToString("00") + "/" + m.ToString("00") + "/" + year.ToString("0000"));
            //DateTime datecell = Convert.ToDateTime(Convert.ToInt32(CalendarView.SelectedCells[0].Value).ToString("00") + "/" + m.ToString("00") + "/" + year.ToString("0000"));
            DateTime datecell = DateTime.ParseExact(Convert.ToInt32(CalendarView.SelectedCells[0].Value).ToString("00") + "/" + monthnum.ToString("00") + "/" + btnYNow.Text, "d/M/yyyy", CultureInfo.InvariantCulture);
            int cellday = int.Parse(CalendarView.SelectedCells[0].Value.ToString());
            if (CalendarView.SelectedCells[0].Value.ToString() != "")
            {// the plan is to check if the date picked is before the date today so that it will only show a view it cannot add but it is argueable because sometimes people want to records events from the past so it will only matter on the reminder
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
                            //MessageBox.Show(CalendarView.SelectedCells[0].Value.ToString());
                            displayEvents(cellday.ToString("00"), monthnum.ToString("00"), btnYNow.Text);
                        }
                        else if (ifclick == "add")
                        {
                            tabSecond.SelectedTab = tabRequest;
                            timeDateCombo(int.Parse(cellday.ToString("00")) - 1, monthnum - 1, btnYNow.Text);
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
                            tabSecond.SelectedTab = tabRequest;
                            timeDateCombo(int.Parse(cellday.ToString("00")) - 1, monthnum - 1, btnYNow.Text);
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
        #endregion


        private void confirmTab_Enter(object sender, EventArgs e)
        {
            lblRequestBy.ForeColor = System.Drawing.ColorTranslator.FromHtml("#acacac");
            txtRequestBy.ForeColor = System.Drawing.ColorTranslator.FromHtml("#acacac");
        }
    }
}
