﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Globalization;

namespace BalayPasilungan
{
    public partial class caseprofile : Form
    {
        //public Form2 ref_to_main { get; set; }
        public MySqlConnection conn;

        public int id, hid, fammode, famid;
        public string filename;
        public DataTable tblfam = new DataTable();
        public MySqlDataAdapter adpmem = new MySqlDataAdapter();
        public caseprofile()
        {
            InitializeComponent();
            profileMenu.Renderer = new BackgroundImageRenderer();
            newChildMenu.Renderer = new renderer();
            newBday.MaxDate = DateTime.Today; newDateJoin.MaxDate = DateTime.Today;
            male.Checked = true;

            conn = new MySqlConnection("Server=localhost;Database=prototype_sad;Uid=root;Pwd=root;");
        }        

        #region Functions
        public void resetTS()           // Reset Existing Child Profile ToolStrips
        {
            infoTS.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            familyTS.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            healthTS.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            eduTS.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            consulTS.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            othersTS.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);

            infoTS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c9c9c9");
            familyTS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c9c9c9");
            healthTS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c9c9c9");
            eduTS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c9c9c9");
            consulTS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c9c9c9");
            othersTS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c9c9c9");

            infoTS.BackgroundImage = null; familyTS.BackgroundImage = null; eduTS.BackgroundImage = null; healthTS.BackgroundImage = null; consulTS.BackgroundImage = null; othersTS.BackgroundImage = null;

        }

        public void resetMainColors()   // Reset taskbar and main buttons to black
        {
            taskbar.BackColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            tabSelection.SelectedTab = tabNewChild;
            btnCases.BackColor = Color.White;
            btnReport.BackColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            btnCases.BackgroundImage = global::BalayPasilungan.Properties.Resources.cases_green;
            btnReport.BackgroundImage = global::BalayPasilungan.Properties.Resources.report_white;
        }

        public void resetMainBTN()      // Reset Main Buttons backcolor to green
        {
            btnCases.BackColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
            btnReport.BackColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");
        }        

        public void resetNewChildTS()   // Reset New Child ToolStrip to Gray
        {            
            tsNewFamily.ForeColor = System.Drawing.Color.FromArgb(201, 201, 201);
            tsNewCon.ForeColor = System.Drawing.Color.FromArgb(201, 201, 201);
            tsNewEdu.ForeColor = System.Drawing.Color.FromArgb(201, 201, 201);
            tsNewHealth.ForeColor = System.Drawing.Color.FromArgb(201, 201, 201);
            tsNewInfo.ForeColor = System.Drawing.Color.FromArgb(201, 201, 201);
            tsNewIO.ForeColor = System.Drawing.Color.FromArgb(201, 201, 201);
        }

        public void resetTextboxes()    // Reset unfocused textbox, line and label colors to default dark
        {
            // Textboxes
            txtNewFName.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135); txtNewLName.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135); txtNewNName.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135); txtAddress.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);
            txtKinder.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135); txtHS.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135); txtElementary.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);

            // Labels
            lblFName.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42); lblLName.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42); lblNName.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42); lblAddress.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
            kinder.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42); elementary.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42); highschool.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);

            // Lines
            panelFName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line; panelLName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line; panelNName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
        }  
        
        public void blackTheme()        // Black taskbar and all
        {
            taskbar.BackColor = System.Drawing.ColorTranslator.FromHtml("#2d2d2d");
            btnReport.BackColor = System.Drawing.ColorTranslator.FromHtml("#2d2d2d");
            tabSelection.SelectedTab = tabCases;
            btnCases.BackColor = Color.White;
            btnCases.BackgroundImage = global::BalayPasilungan.Properties.Resources.cases_black;
            btnReport.BackgroundImage = global::BalayPasilungan.Properties.Resources.report_white;
        }
        #endregion

        #region Main
        private void tabSelection_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.White, 500);
            g.DrawRectangle(p, this.tabSelection.Bounds);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Case Profile Load
        private void caseprofile_Load(object sender, EventArgs e)
        {
            resetMainBTN();
            blackTheme();

            lbladdeditprofile.Text = "New Profile";
            btnaddeditcase.Text = "Add Profile";

            dtbirth.MaxDate = DateTime.Now;
            dtjoin.MaxDate = DateTime.Now;
            condate.MaxDate = DateTime.Now;
            dtpcheck.MaxDate = DateTime.Now;
            dateincid.MaxDate = DateTime.Now;
            dtpmembirth.MaxDate = DateTime.Now;

            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT caseid, lastname, firstname, program FROM casestudyprofile", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);

                DataTable dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    dtgcs.DataSource = dt;
                    dtgcs.Columns[0].Visible = false;

                    getdrop();
                    getresidential();
                    getcount();
                }

                else
                {
                    MessageBox.Show("There are no case studies currently profiled.");
                }

                conn.Close();
            }




            catch (Exception ee)
            {
                MessageBox.Show("" + ee);
                conn.Close();
            }

            tabControl.SelectedTab = first;
        }

        private void tabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(System.Drawing.ColorTranslator.FromHtml("#bebebe"), 1000);
            g.DrawRectangle(p, this.tabControl.Bounds);
        }
        #endregion
        
        #region homemade functions
        public void refresh()
        {
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT caseid, lastname, firstname, program FROM casestudyprofile", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                dtgcs.DataSource = dt;

                dtgcs.Columns[0].Visible = false;

                getdrop();
                getresidential();
                getcount();

                conn.Close();
            }
            
            catch (Exception ee)
            {
                MessageBox.Show("" + ee);
                conn.Close();
            }

            //tabControl.SelectedTab = first;
        }

        public void getdrop()
        {
            MySqlCommand comm = new MySqlCommand("SELECT COUNT(*) FROM casestudyprofile WHERE program = 'Drop-In'", conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(comm);
            DataTable dt = new DataTable();

            adp.Fill(dt);

            lbldrop.Text = dt.Rows[0]["count(*)"].ToString();
        }

        public void getresidential()
        {
            MySqlCommand comm = new MySqlCommand("SELECT COUNT(*) FROM casestudyprofile WHERE program = 'Residential'", conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(comm);
            DataTable dt = new DataTable();

            adp.Fill(dt);

            lblresidential.Text = dt.Rows[0]["count(*)"].ToString();
        }

        public void getcount()
        {
            MySqlCommand comm = new MySqlCommand("SELECT COUNT(caseid) FROM casestudyprofile", conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(comm);
            DataTable dt = new DataTable();

            adp.Fill(dt);

            lbltotalcase.Text = dt.Rows[0]["count(caseid)"].ToString();
        }

        public void reloadeditinfo(int id)
        {
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT lastname, firstname, birthdate, caseAge, program, status, address, datejoined, picture FROM casestudyprofile WHERE caseid = " + id, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {

                    txtlname.Text = dt.Rows[0]["lastname"].ToString();
                    txtfname.Text = dt.Rows[0]["firstname"].ToString();
                    txtaddress.Text = dt.Rows[0]["address"].ToString();
                    txtage.Text = dt.Rows[0]["caseAge"].ToString();
                    cbxprogram.Text = dt.Rows[0]["program"].ToString();
                    cbxstatus.Text = dt.Rows[0]["status"].ToString();

                    dtbirth.Value = Convert.ToDateTime(dt.Rows[0]["birthdate"]);
                    dtjoin.Value = Convert.ToDateTime(dt.Rows[0]["datejoined"]);


                    pbox1.ImageLocation = dt.Rows[0]["picture"].ToString();

                    filename = dt.Rows[0]["picture"].ToString().Replace(@"\", @"\\");

                }

                conn.Close();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
            }


        }

        public void addprofile()
        {
            string lname = txtlname.Text, fname = txtfname.Text, status = cbxstatus.Text, program = cbxprogram.Text, address = txtaddress.Text;
            int age;

            if (string.IsNullOrEmpty(address) || string.IsNullOrEmpty(txtage.Text) || string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(lname) || string.IsNullOrEmpty(program) || string.IsNullOrEmpty(status))
            {
                MessageBox.Show("Please fill out empty fields.");
            }

            else if (pbox1.Image == null)
            {
                MessageBox.Show("Please insert a proper picture.");
            }

            else
            {

                if (Int32.TryParse(txtage.Text, out age))
                {
                    age = int.Parse(txtage.Text);

                    try
                    {
                        conn.Open();
                        MySqlCommand comm = new MySqlCommand("INSERT INTO casestudyprofile(lastname, firstname, birthdate, status, caseage, program, dateJoined, picture, address) VALUES('" + lname + "', '" + fname + "', '" + dtbirth.Value.Date.ToString("yyyyMMdd") + "','" + status + "','" + age + "','" + program + "','" + dtjoin.Value.Date.ToString("yyyy/MM/dd") + "', '" + filename + "', '" + address + "')", conn);

                        comm.ExecuteNonQuery();

                        MessageBox.Show("New Profile Added!");

                        conn.Close();

                        tabControl.SelectedTab = first;

                        reset();
                        refresh();

                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show("" + ee);
                        conn.Close();
                    }
                }

                else
                {
                    MessageBox.Show("Age input is invalid! Please input a proper input!");
                }

            }
        }

        public void editprofile()
        {
            string lname = txtlname.Text, fname = txtfname.Text, status = cbxstatus.Text, program = cbxprogram.Text, address = txtaddress.Text;
            int age;


            if (string.IsNullOrEmpty(address) || string.IsNullOrEmpty(txtage.Text) || string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(lname) || string.IsNullOrEmpty(program) || string.IsNullOrEmpty(status))
            {
                MessageBox.Show("Please fill out empty fields.");
            }

            else if (pbox1.Image == null)
            {
                MessageBox.Show("Please insert a proper picture.");
            }

            else
            {

                if (Int32.TryParse(txtage.Text, out age))
                {
                    age = int.Parse(txtage.Text);

                    try
                    {
                        MessageBox.Show(filename);
                        conn.Open();
                        MySqlCommand comm = new MySqlCommand("UPDATE casestudyprofile SET lastname = '" + lname + "', firstname = " +
                                            "'" + fname + "', birthdate = " + dtbirth.Value.Date.ToString("yyyyMMdd") + ", status = '" + status + "', " +
                                            "caseage = " + age + ", program = '" + program + "', datejoined = " + dtjoin.Value.Date.ToString("yyyyMMdd") + ", " +
                                            "picture = '" + filename + "', address = '" + address + "' WHERE caseid = " + id, conn);

                        comm.ExecuteNonQuery();

                        MessageBox.Show("Profile Edited!");

                        conn.Close();

                        tabControl.SelectedTab = sixteen;

                        reset();
                        refresh();

                        reload(id);

                        existsed(id);

                        existshealth(id);

                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show("" + ee);
                        conn.Close();
                    }
                }

                else
                {
                    MessageBox.Show("Age input is invalid! Please input a proper input!");
                }

            }
        }

        public void reloadedithealth(int id)
        {
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT height, weight, bloodtype, allergies, hecondition FROM health WHERE caseid = " + id, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {

                    lblvheight.Text = dt.Rows[0]["height"].ToString();
                    lblvweight.Text = dt.Rows[0]["weight"].ToString();
                    lblvblood.Text = dt.Rows[0]["bloodtype"].ToString();
                    rviewall.Text = dt.Rows[0]["allergies"].ToString();
                    rviewcondition.Text = dt.Rows[0]["hecondition"].ToString();

                }

                conn.Close();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
            }


        }

        public void addhealth()
        {
            string blood = cbxbloodtype.Text, allergy = rtxtall.Text, condition = rtxtcondition.Text;
            int height, weight;

            if (string.IsNullOrEmpty(blood) || string.IsNullOrEmpty(txtheight.Text) || string.IsNullOrEmpty(txtweight.Text) || string.IsNullOrEmpty(allergy) || string.IsNullOrEmpty(condition))
            {
                MessageBox.Show("Please fill out empty fields.");
            }

            else
            {

                if (Int32.TryParse(txtheight.Text, out height) && Int32.TryParse(txtweight.Text, out weight))
                {
                    height = int.Parse(txtheight.Text); weight = int.Parse(txtweight.Text);

                    try
                    {

                        conn.Open();


                        MySqlCommand comm = new MySqlCommand("INSERT INTO health(caseid, height, weight, bloodtype, allergies, hecondition) VALUES('" + id + "', '" + height + "', '" + weight + "','" + blood + "','" + allergy + "','" + condition + "')", conn);

                        comm.ExecuteNonQuery();

                        MessageBox.Show("New Info Added!");

                        conn.Close();

                        existshealth(id);

                        reloadedithealth(id);

                        lblblood.Text = blood;
                        lblheight.Text = height.ToString();
                        lblweight.Text = weight.ToString();

                        tabControl.SelectedTab = sixteen;

                        reset3();

                    }

                    catch (Exception ee)
                    {
                        MessageBox.Show("" + ee);
                        conn.Close();
                    }

                }

                else
                {
                    if (Int32.TryParse(txtheight.Text, out height) == false && Int32.TryParse(txtweight.Text, out weight) == false)
                    {
                        MessageBox.Show("Height and Weight inputs are invalid! Use numbers!");
                    }

                    else if (Int32.TryParse(txtheight.Text, out height) == false)
                    {
                        MessageBox.Show("Height input is invalid! Use numbers!");
                    }

                    else
                    {
                        MessageBox.Show("Weight input is invalid! Use numbers!");
                    }
                }
            }
        }

        public void edithealth()
        {
            string blood = cbxbloodtype.Text, allergy = rtxtall.Text, condition = rtxtcondition.Text;
            int height, weight;

            if (string.IsNullOrEmpty(blood) || string.IsNullOrEmpty(txtheight.Text) || string.IsNullOrEmpty(txtweight.Text) || string.IsNullOrEmpty(allergy) || string.IsNullOrEmpty(condition))
            {
                MessageBox.Show("Please fill out empty fields.");
            }

            else
            {

                if (Int32.TryParse(txtheight.Text, out height) && Int32.TryParse(txtweight.Text, out weight))
                {
                    height = int.Parse(txtheight.Text); weight = int.Parse(txtweight.Text);

                    try
                    {

                        conn.Open();


                        MySqlCommand comm = new MySqlCommand("UPDATE health SET height = '" + height + "', weight = '" + weight + "', bloodtype = '" + blood + "', allergies = '" + allergy + "', hecondition = '" + condition + "' WHERE caseid = " + id, conn);

                        comm.ExecuteNonQuery();

                        MessageBox.Show("New Info Added!");

                        conn.Close();

                        existshealth(id);

                        reloadedithealth(id);

                        lblblood.Text = blood;
                        lblheight.Text = height.ToString();
                        lblweight.Text = weight.ToString();

                        tabControl.SelectedTab = seventeen;

                        reset3();

                    }

                    catch (Exception ee)
                    {
                        MessageBox.Show("" + ee);
                        conn.Close();
                    }

                }

                else
                {
                    if (Int32.TryParse(txtheight.Text, out height) == false && Int32.TryParse(txtweight.Text, out weight) == false)
                    {
                        MessageBox.Show("Height and Weight inputs are invalid! Use numbers!");
                    }

                    else if (Int32.TryParse(txtheight.Text, out height) == false)
                    {
                        MessageBox.Show("Height input is invalid! Use numbers!");
                    }

                    else
                    {
                        MessageBox.Show("Weight input is invalid! Use numbers!");
                    }
                }
            }
        }

        #endregion

        #region cellclicks
        private void dtgcs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id = int.Parse(dtgcs.Rows[e.RowIndex].Cells[0].Value.ToString());

                tabControl.SelectedTab = sixteen;

                reload(id);

                existsed(id);

                existshealth(id);

            }

            catch (Exception ee)
            {

            }
        }

        #endregion

        #region reset functions

        public void reset()
        {
            pbox1.Image = null;

            txtlname.Clear();
            txtfname.Clear();
            txtage.Clear();
            txtaddress.Clear();

            cbxprogram.SelectedIndex = -1;
            cbxstatus.SelectedIndex = -1;

            dtbirth.Value = DateTime.Now.Date;
            dtjoin.Value = DateTime.Now.Date;

            if (btnaddeditcase.Text == "Add Profile")
            {
                tabControl.SelectedTab = first;
            }

            else
            {
                tabControl.SelectedTab = sixteen;
            }

            resetColor();
        }

        public void reset2()
        {
            txtedname.Clear();
            cbxlevel.SelectedIndex = -1;
            cbxtype.SelectedIndex = -1;
        }

        public void reset3()
        {
            txtheight.Clear();
            txtweight.Clear();
            cbxbloodtype.SelectedIndex = -1;

            rtxtcondition.Clear();
            rtxtall.Clear();

        }

        public void reset4()
        {
            condate.Value = DateTime.Now.Date;
            txtintname.Clear();
            richconbox.Clear();

        }

        public void reset5()
        {
            txttypeincid.Clear();
            txtincidlocation.Clear();
            rtxtactiontaken.Clear();
            rtxtinciddesc.Clear();

            rbam.Checked = false;
            rbpm.Checked = false;

            cbxhour.SelectedIndex = -1;
            cbxmin.SelectedIndex = -1;

            dateincid.Value = DateTime.Now.Date;
        }

        public void reset6()
        {
            dtpcheck.Value = DateTime.Now.Date;
            txtlocationcheck.Clear();
            rcheckdetails.Clear();
        }

        public void reset7()
        {
            //dtfamOverview.DataSource = null;
        }

        public void reset8()
        {
            txtmemfirstname.Clear();
            txtmemlastname.Clear();
            txtmemocc.Clear();
            txtmemrelationship.Clear();

            cbxmemgender.SelectedIndex = -1;
            cbxmemdependency.SelectedIndex = -1;

            dtpmembirth.Value = DateTime.Now.Date;
        }

        #endregion

        #region  Profile Tool Strip 
        private class BackgroundImageRenderer : ToolStripProfessionalRenderer
        {
            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                if (!e.Item.Selected) base.OnRenderMenuItemBackground(e);
                else
                {
                    if (e.Item.Name == "infoTS" || e.Item.Name == "familyTS" || e.Item.Name == "eduTS" || e.Item.Name == "healthTS" || e.Item.Name == "consulTS" || e.Item.Name == "othersTS")
                    {
                        Image backgroundImage = global::BalayPasilungan.Properties.Resources.tsLineHover;
                        e.Graphics.DrawImage(backgroundImage, 0, 0, e.Item.Width, e.Item.Height);
                    }
                    else base.OnRenderMenuItemBackground(e);
                }
            }
        }

        private void infoTS_Click(object sender, EventArgs e)
        {
            resetTS();
            infoTS.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            infoTS.ForeColor = Color.Black;
            infoTS.BackgroundImage = global::BalayPasilungan.Properties.Resources.tsLine;
            tabControl.SelectedIndex = 0;
        }

        private void familyTS_Click(object sender, EventArgs e)
        {
            resetTS();
            familyTS.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            familyTS.ForeColor = Color.Black;
            familyTS.BackgroundImage = global::BalayPasilungan.Properties.Resources.tsLine;
            tabControl.SelectedIndex = 1;
        }

        private void eduTS_Click(object sender, EventArgs e)
        {
            resetTS();
            eduTS.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            eduTS.ForeColor = Color.Black;
            eduTS.BackgroundImage = global::BalayPasilungan.Properties.Resources.tsLine;
            tabControl.SelectedIndex = 2;
        }

        private void healthTS_Click(object sender, EventArgs e)
        {
            resetTS();
            healthTS.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            healthTS.ForeColor = Color.Black;
            healthTS.BackgroundImage = global::BalayPasilungan.Properties.Resources.tsLine;
            tabControl.SelectedIndex = 3;
        }

        private void consulTS_Click(object sender, EventArgs e)
        {
            resetTS();
            consulTS.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            consulTS.ForeColor = Color.Black;
            consulTS.BackgroundImage = global::BalayPasilungan.Properties.Resources.tsLine;
            tabControl.SelectedIndex = 4;
        }

        private void othersTS_Click(object sender, EventArgs e)
        {
            resetTS();
            othersTS.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            othersTS.ForeColor = Color.Black;
            othersTS.BackgroundImage = global::BalayPasilungan.Properties.Resources.tsLine;
            tabControl.SelectedIndex = 5;
        }
        #endregion

        #region Main Buttons
        private void btnCases_Click(object sender, EventArgs e)
        {
            resetMainBTN();
            blackTheme();            
        }
        #endregion

        #region New Child Profile Form

        private class renderer : ToolStripProfessionalRenderer
        {
            public renderer() : base(new cols()) { }
        }

        private class cols : ProfessionalColorTable
        {
            public override Color MenuItemSelected
            {
                get { return System.Drawing.Color.FromArgb(248, 248, 248); }
            }
            public override Color MenuItemSelectedGradientBegin
            {
                get { return System.Drawing.Color.FromArgb(248, 248, 248); }
            }
            public override Color MenuItemSelectedGradientEnd
            {
                get { return System.Drawing.Color.FromArgb(248, 248, 248); }
            }
            public override Color MenuItemBorder
            {
                get { return System.Drawing.Color.FromArgb(248, 248, 248); }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            resetMainColors();
            tabNewChildInput.SelectedIndex = 0;
        }

        private void male_CheckedChanged(object sender, EventArgs e)
        {
            if (male.Checked) confGender.Text = "Male";
            else confGender.Text = "Female";
        }

        #region Next Back Buttons
        private void btnNextFamily_Click(object sender, EventArgs e)
        {
            resetNewChildTS();
            tsNewFamily.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            tabNewChildInput.SelectedTab = tabNewFamily;
        }
        
        private void btnBackInfo_Click(object sender, EventArgs e) // Current tab: family
        {
            resetNewChildTS();
            tsNewInfo.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            tabNewChildInput.SelectedTab = tabNewInfo;
        }
        
        private void btnNextEdu_Click(object sender, EventArgs e)
        {
            resetNewChildTS();
            tsNewEdu.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            tabNewChildInput.SelectedTab = tabNewEdu;
        }

        private void btnBackFamily_Click(object sender, EventArgs e) // Current tab: education
        {
            resetNewChildTS();
            tsNewFamily.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            tabNewChildInput.SelectedTab = tabNewFamily;
        }

        private void btnNextHealth_Click(object sender, EventArgs e)
        {
            resetNewChildTS();
            tsNewHealth.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            tabNewChildInput.SelectedTab = tabNewHealth;
        }

        private void btnBackEdu_Click(object sender, EventArgs e) // Current tab: health
        {
            resetNewChildTS();
            tsNewEdu.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            tabNewChildInput.SelectedTab = tabNewEdu;
        }

        private void btnNextCon_Click(object sender, EventArgs e)
        {
            resetNewChildTS();
            tsNewCon.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            tabNewChildInput.SelectedTab = tabNewCon;
        }

        private void btnBackHealth_Click(object sender, EventArgs e)    // Current tab: consultation
        {
            resetNewChildTS();
            tsNewHealth.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            tabNewChildInput.SelectedTab = tabNewHealth;
        }

        private void btnNextIO_Click(object sender, EventArgs e)
        {
            resetNewChildTS();
            tsNewIO.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            tabNewChildInput.SelectedTab = tabNewIO;
        }

        private void btnBackCon_Click(object sender, EventArgs e)   // Current tab: IO
        {
            resetNewChildTS();
            tsNewCon.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            tabNewChildInput.SelectedTab = tabNewCon;
        }

        private void btnBackIO_Click(object sender, EventArgs e)    // Current tab: Confirmation
        {
            resetNewChildTS();
            tsNewIO.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            tabNewChildInput.SelectedTab = tabNewIO;
        }

        private void btnNewConfirm_Click(object sender, EventArgs e)
        {
            resetNewChildTS();
            tabNewChildInput.SelectedTab = tabNewConfirm;
        }

        #endregion

        #region New Child Info Form
        private void txtNewFName_Enter(object sender, EventArgs e)
        {
            resetTextboxes();
            countFName.Visible = true; lblFName.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141); txtNewFName.ForeColor = Color.Black;
            if (txtNewFName.Text == "Juan Miguel") txtNewFName.Clear();
            panelFName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
        }

        private void panelLName_Enter(object sender, EventArgs e)
        {
            resetTextboxes();
            countLName.Visible = true; lblLName.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141); txtNewLName.ForeColor = Color.Black;
            if (txtNewLName.Text == "dela Cruz") txtNewLName.Clear();
            panelLName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
        }

        private void txtNewNName_Enter(object sender, EventArgs e)
        {
            resetTextboxes();
            countNName.Visible = true; lblNName.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141); txtNewNName.ForeColor = Color.Black;
            if (txtNewNName.Text == "Juan") txtNewNName.Clear();
            panelNName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
        }

        private void txtAddress_Enter(object sender, EventArgs e)
        {
            resetTextboxes();
            countAddress.Visible = true; lblAddress.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141); txtAddress.ForeColor = Color.Black;
            if (txtAddress.Text == "Where does the child live?") txtAddress.Clear();
        }

        private void txtNewFName_TextChanged(object sender, EventArgs e)
        {
            countFName.Text = txtNewFName.Text.Length + "/100";
            confFName.Text = txtNewFName.Text;
        }

        private void txtNewLName_TextChanged(object sender, EventArgs e)
        {
            countLName.Text = txtNewLName.Text.Length + "/100";
            confLName.Text = txtNewLName.Text;
        }

        private void txtNewNName_TextChanged(object sender, EventArgs e)
        {
            countNName.Text = txtNewNName.Text.Length + "/25";
            confNName.Text = txtNewNName.Text;
        }
        
        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
            countAddress.Text = txtAddress.Text.Length + "/100";
            //confNName.Text = txtNewNName.Text;        ADDRESS CONFIRM
        }

        private void txtNewFName_Leave(object sender, EventArgs e)
        {
            if (txtNewFName.Text == "") txtNewFName.Text = "Juan Miguel";
            resetTextboxes(); countFName.Visible = false;
        }

        private void txtNewLName_Leave(object sender, EventArgs e)
        {
            if (txtNewLName.Text == "") txtNewLName.Text = "dela Cruz";
            resetTextboxes(); countLName.Visible = false;
        }

        private void txtNewNName_Leave(object sender, EventArgs e)
        {
            if (txtNewNName.Text == "") txtNewNName.Text = "Juan";
            resetTextboxes(); countNName.Visible = false;
        }

        private void txtAddress_Leave(object sender, EventArgs e)
        {
            if (txtAddress.Text == "") txtAddress.Text = "Where does the child live?";
            resetTextboxes(); countAddress.Visible = false;
        }
        #endregion

        #region New Child Education Form
        private void kinder_CheckedChanged(object sender, EventArgs e)
        {
            if (kinder.Checked) { panelKinder.Enabled = true; lvlKinder.Visible = true; cbKinder.Visible = true; }
            else { panelKinder.Enabled = false; lvlKinder.Visible = false; cbKinder.Visible = false; }
        }

        private void elementary_CheckedChanged(object sender, EventArgs e)
        {
            if (elementary.Checked) { panelElementary.Enabled = true; lvlElementary.Visible = true; cbElementary.Visible = true; }
            else { panelElementary.Enabled = false; lvlElementary.Visible = false; cbElementary.Visible = false; }
        }

        private void highschool_CheckedChanged(object sender, EventArgs e)
        {
            if (highschool.Checked) { panelHS.Enabled = true; lvlHS.Visible = true; cbHS.Visible = true; }
            else { panelHS.Enabled = false; lvlHS.Visible = false; cbHS.Visible = false; }
        }

        private void txtKinder_TextChanged(object sender, EventArgs e)
        {
            countKinder.Text = txtKinder.Text.Length + "/100";
        }

        private void txtElementary_TextChanged(object sender, EventArgs e)
        {
            countElementary.Text = txtElementary.Text.Length + "/100";
        }

        private void txtHS_TextChanged(object sender, EventArgs e)
        {
            countHS.Text = txtHS.Text.Length + "/100";
        }

        private void txtKinder_Enter(object sender, EventArgs e)
        {
            resetTextboxes();
            countKinder.Visible = true; kinder.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141); txtKinder.ForeColor = Color.Black;
            if (txtKinder.Text == "Name of Preschool") txtKinder.Clear();
            panelKinder.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
        }

        private void txtElementary_Enter(object sender, EventArgs e)
        {
            resetTextboxes();
            countElementary.Visible = true; elementary.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141); txtElementary.ForeColor = Color.Black;
            if (txtElementary.Text == "Name of Elementary") txtElementary.Clear();
            panelElementary.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
        }

        private void txtHS_Enter(object sender, EventArgs e)
        {
            resetTextboxes();
            countHS.Visible = true; highschool.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141); txtHS.ForeColor = Color.Black;
            if (txtHS.Text == "Name of High School") txtHS.Clear();
            panelHS.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
        }

        private void txtKinder_Leave(object sender, EventArgs e)
        {
            resetTextboxes();
            if (txtKinder.Text == "")
            {
                txtKinder.Text = "Name of Preschool";
                // confirm here
            }
            countKinder.Visible = false; panelKinder.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
        }

        private void txtElementary_Leave(object sender, EventArgs e)
        {
            resetTextboxes();
            if (txtElementary.Text == "")
            {
                txtElementary.Text = "Name of Elementary School";
                // confirm here
            }
            countElementary.Visible = false; panelElementary.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
        }

        private void txtHS_Leave(object sender, EventArgs e)
        {
            resetTextboxes();
            if (txtHS.Text == "")
            {
                txtHS.Text = "Name of High School";
                // confirm here
            }
            countHS.Visible = false; panelHS.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
        }
        
        private void btnEdu_Click(object sender, EventArgs e)
        {
            if (lblEduNo.ForeColor == System.Drawing.Color.FromArgb(62, 153, 141))
            {
                lblEduNo.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                lblEduYes.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                btnEdu.BackgroundImage = global::BalayPasilungan.Properties.Resources.on;
            }
            else
            {
                lblEduNo.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                lblEduYes.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                btnEdu.BackgroundImage = global::BalayPasilungan.Properties.Resources.off;
            }
        }
        #endregion

        #endregion

        private void newprofilepic_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";
                dlg.Filter = "All image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png| JPEG Files (*.jpg, *.jpeg, *.jpe)|*.jpg; *.jpeg; *.jpe|PNG files (*.png)|*.png|BMP files (*.bmp)|*.bmp";

                if (dlg.ShowDialog() == DialogResult.OK)
                {                    
                    newprofilepic.Image = new Bitmap(dlg.FileName);
                }
            }
        }

        #region Search
        private void txtSearch_Enter(object sender, EventArgs e)
        {            
            txtSearch.ForeColor = Color.White;
            searchlogo.BackgroundImage = global::BalayPasilungan.Properties.Resources.search_thin;
            txtSearch.Clear();
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                txtSearch.ForeColor = System.Drawing.Color.FromArgb(81, 210, 156);
                searchlogo.BackgroundImage = global::BalayPasilungan.Properties.Resources.search_notfocus;
                txtSearch.Text = "Find a child";
            }
        }




        #endregion

    }
}
