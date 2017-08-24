using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BalayPasilungan
{
    public partial class eventorg : Form
    {
        public String[] month = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        public bool remindState = false, budgetState = false;

        public eventorg()
        {
            InitializeComponent();
            Load += btnEvent_Click;
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

        private void resetLabels()
        {
            lblEventName.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a");
            panelEName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
            lblEVenue.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a");
            panelEVenue.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
            lblEType.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a");
            lblEDes.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a");
            lblEDate.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a");
            lblRequestBy.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a");
            panelRequestBy.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
            lblRDate.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a");
        }

        private void resetCounters()
        {
            countEName.Visible = false;
            countEVenue.Visible = false;
            countEDes.Visible = false;
        }

        public void adjustCustom(int type, String now)
        {
            if(type == 0)
            {
                btnMonNow.Text = now;
                int i = Array.IndexOf(month, now);
                if (i == 11)   // December
                {
                    btnMonNext.Text = month[0];
                    btnMonPrev.Text = month[i - 1];
                }
                else if (i == 0)    // January
                {
                    btnMonNext.Text = month[i + 1];
                    btnMonPrev.Text = month[11];
                }
                else
                {
                    btnMonNext.Text = month[i + 1];
                    btnMonPrev.Text = month[i - 1];
                }
            }
            else
            {
                btnYrNow.Text = now;
                if (int.Parse(now) == 1960)
                {
                    btnYrPrev.Text = "";
                    btnYrPrev.Enabled = false;
                    btnYrNext.Text = (int.Parse(now) + 1).ToString();
                }
                else if(int.Parse(now) == 2099)
                {
                    btnYrNext.Text = "";
                    btnYrNext.Enabled = false;
                    btnYrPrev.Text = (int.Parse(now) - 1).ToString();
                }
                else
                {                    
                    btnYrNext.Text = (int.Parse(now) + 1).ToString();
                    btnYrPrev.Text = (int.Parse(now) - 1).ToString();
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
        }

        private void btnEvent_Click(object sender, EventArgs e)
        {
            menuStrip.Height = 0;
            timer1.Enabled = true;

            tabSecond.SelectedTab = tabEvent;
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

        #region Time Buttons
        private void btnMonPrev_Click(object sender, EventArgs e)
        {
            btnMonNext.Text = btnMonNow.Text;
            btnMonNow.Text = btnMonPrev.Text;            
            foreach (String now in month)
            {
                if (btnMonNow.Text.Equals(now))
                {
                    int i = Array.IndexOf(month, now);
                    if(i == 0) btnMonPrev.Text = month[11];                   
                    else btnMonPrev.Text = month[i - 1];                    
                }
            }            
        }

        private void btnMonNext_Click(object sender, EventArgs e)
        {
            btnMonPrev.Text = btnMonNow.Text;
            btnMonNow.Text = btnMonNext.Text;
            foreach (String now in month)
            {
                if (btnMonNow.Text.Equals(now))
                {
                    int i = Array.IndexOf(month, now);
                    if (i == 11) btnMonNext.Text = month[0];                    
                    else btnMonNext.Text = month[i + 1];                    
                }
            }
        }

        private void btnYrPrev_Click(object sender, EventArgs e)
        {
            btnYrPrev.Enabled = true; btnYrNext.Enabled = true;
            if (btnYrPrev.Text.ToString().Equals("1960"))
            {
                btnYrNow.Text = "1960";
                btnYrPrev.Text = "";
                btnYrNext.Text = "1961";
                btnYrPrev.Enabled = false;
            }            
            else
            {
                btnYrNext.Text = btnYrNow.Text;
                btnYrNow.Text = btnYrPrev.Text;
                btnYrPrev.Text = (int.Parse(btnYrNow.Text.ToString()) - 1 ).ToString();
            }             
        }

        private void btnYrNext_Click(object sender, EventArgs e)
        {
            btnYrNext.Enabled = true; btnYrPrev.Enabled = true;
            if (btnYrNext.Text.ToString().Equals("2099"))
            {
                btnYrNow.Text = "2099";                
                btnYrNext.Text = "";
                btnYrPrev.Text = "2098";
                btnYrNext.Enabled = false;
            }
            else
            {
                btnYrPrev.Text = btnYrNow.Text;
                btnYrNow.Text = btnYrNext.Text;
                btnYrNext.Text = (int.Parse(btnYrNow.Text.ToString()) + 1).ToString();
            }            
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

        #region Next Back Buttons        
        private void btnNext_Click(object sender, EventArgs e)
        {
            tabERForm.SelectedIndex = 1;
            tdTS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#18764e");
            edTS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c5d9d0");
        }

        private void btnNext2_Click(object sender, EventArgs e)
        {
            tabERForm.SelectedIndex = 2;
            oTS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#18764e");
            tdTS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c5d9d0");           
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
                resetLabels(); resetCounters();
                txtEventName.ForeColor = Color.Black;            
                if (txtEventName.Text.Equals("What is the name of the event?")) txtEventName.Text = "";
                panelEName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_green;
                lblEventName.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
                countEName.Visible = true;       
            }

            private void txtVenue_Enter(object sender, EventArgs e)
            {
                resetLabels(); resetCounters();
                txtVenue.ForeColor = Color.Black;
                if (txtVenue.Text.Equals("Where will it be held?")) txtVenue.Text = "";
                panelEVenue.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_green;
                lblEVenue.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
                countEVenue.Visible = true;
            }

            private void txtEventDes_Enter(object sender, EventArgs e)
            {
                resetLabels(); resetCounters();
                txtEventDes.ForeColor = Color.Black;
                if (txtEventDes.Text.Equals("Describe the event.")) txtEventDes.Text = "";
                lblEDes.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
                countEDes.Visible = true;
            }
        
            private void cbEType_Enter(object sender, EventArgs e)
            {
                resetLabels();
                lblEType.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            }

            private void eventDate_Enter(object sender, EventArgs e)
            {
                resetLabels();
                lblEDate.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            }

            private void txtRequestBy_Enter(object sender, EventArgs e)
            {
                txtRequestBy.ForeColor = Color.Black;
                lblQuestion.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2a2a2a");
                if (txtRequestBy.Text.Equals("Who requested the event?")) txtRequestBy.Text = "";
                resetLabels();
                panelRequestBy.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_green;
                lblRequestBy.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
                countRequestBy.Visible = true;
            }

            private void reminderDate_Enter(object sender, EventArgs e)
            {
                resetLabels();
                lblRDate.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            }

            private void others_Enter(object sender, EventArgs e)
            {
                lblQuestion.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
                lblNo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
                reminderPanel.Height = 0;
            }
            #endregion

            #region Leave Textbox
            private void txtRequestBy_Leave(object sender, EventArgs e)
            {
                resetLabels();
                panelRequestBy.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
                countRequestBy.Visible = false;
            }
        
            private void txtEventName_Leave(object sender, EventArgs e) //BOOK2
            {
                txtEventName.ForeColor = System.Drawing.ColorTranslator.FromHtml("#878787");
                if (txtEventName.Text.Equals("")) txtEventName.Text = "What is the name of the event?";
            }

            private void txtEventDes_Leave(object sender, EventArgs e)
            {
                txtEventDes.ForeColor = System.Drawing.ColorTranslator.FromHtml("#878787");
                if (txtEventDes.Text.Equals("")) txtEventDes.Text = "Describe the event.";
            }

            private void txtVenue_Leave(object sender, EventArgs e)
            {
                txtVenue.ForeColor = System.Drawing.ColorTranslator.FromHtml("#878787");
                if (txtVenue.Text.Equals("")) txtVenue.Text = "Where will it be held?";
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
            cbEMonth2.Visible = false; cbEDay2.Visible = false; cbEYear2.Visible = false;
            panelEHours2.Visible = false; panelEMins2.Visible = false; lblColon2.Visible = false; btnAM2.Visible = false; btnPM2.Visible = false;
        }

        private void btnMulDay_Click(object sender, EventArgs e)
        {
            resetDayButtons();
            btnMulDay.ForeColor = Color.White;
            btnMulDay.BackColor = System.Drawing.ColorTranslator.FromHtml("#5ea6e9");
            btnMulDay.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#5ea6e9");
            cbEMonth.Enabled = true; cbEDay.Enabled = true; cbEYear.Enabled = true;
            cbEMonth2.Visible = true; cbEDay2.Visible = true; cbEYear2.Visible = true;
            panelEHours2.Visible = true; panelEMins2.Visible = true; lblColon2.Visible = true;  btnAM2.Visible = true; btnPM2.Visible = true;
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
            }
            else
            {
                lblYes2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
                lblNo2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
                btnBudget.BackgroundImage = global::BalayPasilungan.Properties.Resources.off;
                budgetState = false;
                btnAddBudget.Visible = false;
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

                reminderDate.Enabled = true;
            }
            else
            {
                lblYes.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
                lblNo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
                btnRemind.BackgroundImage = global::BalayPasilungan.Properties.Resources.off;
                remindState = false;
                reminderPanel.Height = 0;

                reminderDate.Enabled = false;
            }
        }



        #endregion

        #region Event Request Time and Date Data Validation
        private void timeanddate_Enter(object sender, EventArgs e)
        {
            int last = 0;
            cbEMonth.SelectedIndex = 0; cbEMonth2.SelectedIndex = 0;

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

            cbEDay.SelectedIndex = 0; cbEYear.SelectedIndex = 0; cbEDay2.SelectedIndex = 0; cbEYear2.SelectedIndex = 0;
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
            btnAM.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            btnPM.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
        }

        private void btnPM_Click(object sender, EventArgs e)
        {
            btnPM.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            btnAM.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
        }

        private void btnAM2_Click(object sender, EventArgs e)
        {
            btnAM2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            btnPM2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dcdcdc");
        }

        private void btnPM2_Click(object sender, EventArgs e)
        {
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

        private void txtEMins2_TextChanged(object sender, EventArgs e)
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

        private void btnNewEventForm_Click(object sender, EventArgs e)
        {                       
            txtEventName.ForeColor = System.Drawing.ColorTranslator.FromHtml("#878787");
            if (txtEventName.Text.Equals("")) txtEventName.Text = "What is the name of the event?";
            txtEventDes.ForeColor = System.Drawing.ColorTranslator.FromHtml("#878787");
            if (txtEventDes.Text.Equals("")) txtEventDes.Text = "Describe the event.";
            txtVenue.ForeColor = System.Drawing.ColorTranslator.FromHtml("#878787");
            if (txtVenue.Text.Equals("")) txtVenue.Text = "Where will it be held?";
            // BOOK1
            // Find code to unclick button
        }

        #region Custom Month and Year
        private void btnYrNow_Click(object sender, EventArgs e)
        {
            others ot = new others();

            this.Enabled = false;
            ot.tabSelection.SelectedIndex = 1;
            ot.refToERF = this;
            ot.Show();
        }

        private void btnMonNow_Click(object sender, EventArgs e)
        {
            others ot = new others();

            this.Enabled = false;
            ot.tabSelection.SelectedIndex = 0;
            ot.refToERF = this;
            ot.Show();
        }
        #endregion

        private void eventdetails_Click(object sender, EventArgs e)
        {

        }

        private void confirmTab_Enter(object sender, EventArgs e)
        {
            lblRequestBy.ForeColor = System.Drawing.ColorTranslator.FromHtml("#acacac");
            txtRequestBy.ForeColor = System.Drawing.ColorTranslator.FromHtml("#acacac");
        }
    }
}
