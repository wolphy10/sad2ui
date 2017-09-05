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
        public bool empty, confirmed;

        public caseprofile()
        {
            InitializeComponent();
            profileMenu.Renderer = new BackgroundImageRenderer();
            newChildMenu.Renderer = new renderer();
            //newBday.MaxDate = DateTime.Today; newDateJoin.MaxDate = DateTime.Today;
            //male.Checked = true;

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
            tabCase.SelectedTab = tabNewChild;
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
            //txtNewFName.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135); txtNewLName.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135); txtNewNName.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135); txtAddress.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);
            txtKinder.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135); txtHS.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135); txtElementary.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);

            // Labels
            //lblFName.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42); lblLName.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42); lblNName.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42); lblAddress.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
            kinder.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42); elementary.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42); highschool.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);

            // Lines
            //panelFName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line; panelLName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line; panelNName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
        }  
        
        public void blackTheme()        // Black taskbar and all
        {
            taskbar.BackColor = System.Drawing.ColorTranslator.FromHtml("#2d2d2d");
            btnReport.BackColor = System.Drawing.ColorTranslator.FromHtml("#2d2d2d");
            tabCase.SelectedTab = tabCases;
            btnCases.BackColor = Color.White;
            btnCases.BackgroundImage = global::BalayPasilungan.Properties.Resources.cases_black;
            btnReport.BackgroundImage = global::BalayPasilungan.Properties.Resources.report_white;
        }

        public void errorMessage(string message)            // Error Message
        {
            error err = new error();
            dim dim = new dim();

            dim.Location = this.Location;
            err.lblError.Text = message;
            dim.Show();

            if (err.ShowDialog() == DialogResult.OK) dim.Close();
        }

        public void successMessage(string message)            // Success Message
        {
            success yey = new success();
            dim dim = new dim();

            dim.Location = this.Location;
            yey.lblSuccess.Text = message;
            dim.Show();

            if (yey.ShowDialog() == DialogResult.OK) dim.Close();
        }

        public void confirmMessage(string message)            // Success Message
        {
            confirm conf = new confirm();
            dim dim = new dim();

            dim.Location = this.Location;
            conf.lblConfirm.Text = message;
            dim.Show();

            if (conf.ShowDialog() == DialogResult.OK) confirmed = true;
            else confirmed = false;
            dim.Close();
        }
        #endregion

        #region Main
        private void tabSelection_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.White, 500);
            g.DrawRectangle(p, this.tabCase.Bounds);
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

            lbladdeditprofile.Text = "New Case Profile";
            btnaddeditcase.Text = "Add New Profile";

            dtbirth.MaxDate = DateTime.Now;
            dtjoin.MaxDate = DateTime.Now;
            condate.MaxDate = DateTime.Now;
            dtpcheck.MaxDate = DateTime.Now;
            dateincid.MaxDate = DateTime.Now;
            dtpmembirth.MaxDate = DateTime.Now;

            try
            {
                conn.Open();

                MySqlDataAdapter adp = new MySqlDataAdapter("SELECT caseid, lastname, firstname, program FROM casestudyprofile", conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(-1, "No entries.", null, null);
                    empty = true;
                }

                dtgcs.DataSource = dt;

                // Case Profile UI Modifications
                dtgcs.Columns[1].HeaderText = "LASTNAME";
                dtgcs.Columns[2].HeaderText = "FIRSTNAME";
                dtgcs.Columns[3].HeaderText = "PROGRAM";

                // For ID purposes (hidden from user)            
                dtgcs.Columns[0].Visible = false;

                // 935 WIDTH
                dtgcs.Columns[1].Width = 380;
                dtgcs.Columns[2].Width = 380;
                dtgcs.Columns[3].Width = 175;

                if (dt.Rows.Count > 0 && !empty)
                {
                    dtgcs.Columns[1].HeaderCell.Style.Padding = new Padding(10, 0, 0, 0);
                    dtgcs.Columns[1].DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);

                    getdrop(); getresidential(); getcount();
                }
                else empty = false;

                conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show("" + ee);
                conn.Close();
            }
        }

        private void tabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(System.Drawing.ColorTranslator.FromHtml("#bebebe"), 1000);
            g.DrawRectangle(p, this.tabControl.Bounds);
        }
        #endregion

        #region homemade functions

        private void pbox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog rest = new OpenFileDialog();

            rest.Filter = "images| *.JPG; *.PNG; *.GIF"; // you can add any other image type 

            if (rest.ShowDialog() == DialogResult.OK)
            {
                pbox1.Image = Image.FromFile(rest.FileName);

                filename = Path.GetFullPath(rest.FileName).Replace(@"\", @"\\");

            }



        }
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
                    txtcaseaddress.Text = dt.Rows[0]["address"].ToString();
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
            string lname = txtlname.Text, fname = txtfname.Text, status = cbxcasestatus.Text, program = cbxprogram.Text, address = txtcaseaddress.Text;
            int age;

            DateTime now = DateTime.Today, birthyear = dtbirth.Value;

            if (string.IsNullOrEmpty(address) || string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(lname) || string.IsNullOrEmpty(program) || string.IsNullOrEmpty(status)) errorMessage("Please fill out empty fields.");                

            else if (pbox1.Image == null) errorMessage("Please insert a proper picture.");

            else
            {
                age = now.Year - birthyear.Year;

                if (now < birthyear.AddYears(age)) age -= 1;
            
                try
                    {
                        conn.Open();
                        MySqlCommand comm = new MySqlCommand("INSERT INTO casestudyprofile(lastname, firstname, birthdate, status, caseage, program, dateJoined, picture, address) VALUES('" + lname + "', '" + fname + "', '" + dtbirth.Value.Date.ToString("yyyyMMdd") + "','" + status + "','" + age + "','" + program + "','" + dtjoin.Value.Date.ToString("yyyy/MM/dd") + "', '" + filename + "', '" + address + "')", conn);

                        comm.ExecuteNonQuery();
                        conn.Close();
                        tabCase.SelectedTab = tabCases;

                        successMessage("New Profile Added!");
    
                        reset();
                        refresh();

                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show("" + ee);
                        conn.Close();
                    }

            }
        }

        public void editprofile()
        {
            string lname = txtlname.Text, fname = txtfname.Text, status = cbxstatus.Text, program = cbxprogram.Text, address = txtcaseaddress.Text;
            int age;

            DateTime now = DateTime.Today, birthyear = dtbirth.Value;
            
            if (string.IsNullOrEmpty(address) || string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(lname) || string.IsNullOrEmpty(program) || string.IsNullOrEmpty(status)) errorMessage("PLease fill out empty fields.");

            else if (pbox1.Image == null) errorMessage("Please insert proper picture.");

            else
            {

                age = now.Year - birthyear.Year;

                if (now < birthyear.AddYears(age)) age -= 1;

                try
                {

                    conn.Open();
                    MySqlCommand comm = new MySqlCommand("UPDATE casestudyprofile SET lastname = '" + lname + "', firstname = " +
                                        "'" + fname + "', birthdate = " + dtbirth.Value.Date.ToString("yyyyMMdd") + ", status = '" + status + "', " +
                                        "caseage = " + age + ", program = '" + program + "', datejoined = " + dtjoin.Value.Date.ToString("yyyyMMdd") + ", " +
                                        "picture = '" + filename + "', address = '" + address + "' WHERE caseid = " + id, conn);

                    comm.ExecuteNonQuery();

                    //MessageBox.Show("Profile Edited!");

                    conn.Close();

                    successMessage("Profile Edited!");

                    tabControl.SelectedTab = sixteen;
                    tabCase.SelectedTab = tabInfo;

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

        }

        public void addhealth()
        {
            string blood = cbxbloodtype.Text, allergy = rtxtall.Text, condition = rtxtcondition.Text;
            int height, weight;

            if (string.IsNullOrEmpty(blood) || string.IsNullOrEmpty(txtheight.Text) || string.IsNullOrEmpty(txtweight.Text) || string.IsNullOrEmpty(allergy) || string.IsNullOrEmpty(condition))
            {
                errorMessage("Please fill out empty fields.");
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

                        successMessage("New Health Biography Added!");

                        conn.Close();

                        tabCase.SelectedTab = tabInfo;

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
                        errorMessage("Height and Weight inputs are invalid! Use numbers!");
                    }

                    else if (Int32.TryParse(txtheight.Text, out height) == false)
                    {
                        errorMessage("Height input is invalid! Use numbers!");
                    }

                    else
                    {
                        errorMessage("Weight input is invalid! Use numbers!");
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
                errorMessage("Please fill out empty fields.");
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

                        successMessage("Changes for Health Biography Added!");

                        conn.Close();

                        tabCase.SelectedTab = tabInfo;

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
                        errorMessage("Height and Weight inputs are invalid! Use numbers!");
                    }

                    else if (Int32.TryParse(txtheight.Text, out height) == false)
                    {
                        errorMessage("Height input is invalid! Use numbers!");
                    }

                    else
                    {
                        errorMessage("Weight input is invalid! Use numbers!");
                    }
                }
            }
        }

        #endregion

        #region reloadfunctions

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

        public void reload(int id)
        {
            //for (int m = 0; m <= dtgcs.ColumnCount - 1; m++)
            //dtgcs.Columns[m].SortMode = DataGridViewColumnSortMode.NotSortable;

            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT lastname, firstname, birthdate, caseAge, program, status, address, datejoined, picture FROM casestudyprofile WHERE caseid = " + id, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {

                    lblcasename.Text = dt.Rows[0]["firstname"].ToString() + " " + dt.Rows[0]["lastname"].ToString();
                    lblcaseaddress.Text = dt.Rows[0]["address"].ToString();
                    lblcaseage.Text = dt.Rows[0]["caseAge"].ToString() + " years old";
                    lblcaseprogram.Text = dt.Rows[0]["program"].ToString();
                    lblcasestatus.Text = dt.Rows[0]["status"].ToString();

                    lbldate.Text = Convert.ToDateTime(dt.Rows[0]["birthdate"]).ToString("MMMM dd, yyyy");
                    lbljoined.Text = Convert.ToDateTime(dt.Rows[0]["datejoined"]).ToString("MMMM dd, yyyy");


                    pbox2.ImageLocation = dt.Rows[0]["picture"].ToString();

                }

                comm = new MySqlCommand("SELECT school, edutype, level FROM education WHERE caseid = " + id, conn);
                adp = new MySqlDataAdapter(comm);
                dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {

                    lbledlvl.Text = dt.Rows[0]["level"].ToString();
                    lbledtype.Text = dt.Rows[0]["edutype"].ToString();
                    lbledschool.Text = dt.Rows[0]["school"].ToString();

                }

                conn.Close();
            }




            catch (Exception ee)
            {
                MessageBox.Show("" + ee);
                conn.Close();
            }
        }

        public void reloadcon(int id)
        {
            //MessageBox.Show(id.ToString());

            //for (int m = 0; m <= dtgcon.ColumnCount - 1; m++)
            //  dtgcon.Columns[m].SortMode = DataGridViewColumnSortMode.NotSortable;

            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT cid, interviewdate, interviewer FROM consultation WHERE caseid = " + id + " ORDER BY interviewdate", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("There are no current consultation records for this case study.");
                }

                else

                {
                    dtgcon.DataSource = dt;

                }


                dtgcon.Columns[0].Visible = false;

                conn.Close();

            }


            catch (Exception ee)
            {

                //MessageBox.Show("" + ee);
                conn.Close();
            }
        }

        public void reloadincid(int id)
        {
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT incidid, type, incdate FROM incident WHERE caseid = " + id + " ORDER BY incdate", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("There are no current incident records for this case study.");
                }

                else

                {
                    dtincid.DataSource = dt;

                }


                dtincid.Columns[0].Visible = false;

                conn.Close();

            }


            catch (Exception ee)
            {

                //MessageBox.Show("" + ee);
                conn.Close();
            }
        }

        public void reloadhealth(int id)
        {
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT checkid, checkupdate, checkuplocation FROM checkup JOIN health ON health.hid = checkup.hid WHERE health.caseid = " + id + " ORDER BY checkupdate", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("There are no current incident records for this case study.");
                }

                else

                {
                    dtghealth.DataSource = dt;
                    dtghealth.Columns[0].Visible = false;
                    //dtincid.Columns[1].Visible = false;

                    //hid = int.Parse(dt.Rows[0]["health.hid"].ToString());
                    //MessageBox.Show(hid.ToString());
                }




                conn.Close();

            }


            catch (Exception ee)
            {

                MessageBox.Show("" + ee);
                conn.Close();
            }
        }

        public void reloadfam(int id)
        {

            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT familyid, famtype FROM family WHERE caseid = " + id, conn);

                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    famid = int.Parse(dt.Rows[0]["familyid"].ToString());

                    lblfamilytype.Text = dt.Rows[0]["famtype"].ToString();
                    MessageBox.Show(famid.ToString());
                    reloadmem(famid);

                }

                else
                {
                    MessageBox.Show("There are no current family records for this case study.");
                }


                conn.Close();

            }


            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString() + "reloadfam");

                conn.Close();
            }

        }


        public void reloadmem(int famid)
        {
            try
            {
                MySqlCommand comm = new MySqlCommand("SELECT memberid, firstname, lastname, gender, birthdate, relationship, dependency, occupation FROM member WHERE familyid = " + famid, conn);

                adpmem = new MySqlDataAdapter(comm);
                tblfam = new DataTable();

                adpmem.Fill(tblfam);



                if (tblfam.Rows.Count > 0)
                {
                    dtfamOverview.DataSource = tblfam;
                    dtfamOverview.Columns[0].Visible = false;


                    DataGridViewCheckBoxColumn dc = new DataGridViewCheckBoxColumn();

                    dc.Name = "check";
                    dc.Visible = true;
                    dc.TrueValue = true;
                    dc.FalseValue = false;



                    if (dtfamOverview.Columns["check"] == null)
                    {
                        dtfamOverview.Columns.Add(dc);

                        dtfamOverview.Columns["check"].DisplayIndex = dtfamOverview.ColumnCount - 1;
                    }

                    foreach (DataGridViewColumn ds in dtfamOverview.Columns)
                    {
                        if (ds.Index.Equals(8))
                        {
                            ds.ReadOnly = false;
                        }
                        else
                        {
                            ds.ReadOnly = true;
                        }
                    }


                }

                else
                {
                    MessageBox.Show("There are no current member records for this case study.");
                }
            }

            catch (Exception ee)
            {
                //MessageBox.Show("There are no current member records for this case study.");
                MessageBox.Show(ee.ToString());

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
                tabCase.SelectedTab = tabInfo;

                reload(id);

                existsed(id);

                existshealth(id);

            }

            catch (Exception ee)
            {

            }
        }

        private void dtghealth_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int checkid = int.Parse(dtghealth.Rows[e.RowIndex].Cells[0].Value.ToString());
                MessageBox.Show(checkid.ToString());
                tabControl.SelectedTab = nineteen;

                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT checkupdetails, checkupdate, checkuplocation FROM checkup WHERE checkid = " + checkid, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    rvcheckdetails.Text = dt.Rows[0]["checkupdetails"].ToString();

                    lblcheckdate.Text = Convert.ToDateTime(dt.Rows[0]["checkupdate"]).ToString("MMMM dd, yyyy");
                    lbllocationcheck.Text = dt.Rows[0]["checkuplocation"].ToString();


                }

                conn.Close();

            }

            catch (Exception ee)
            {
                MessageBox.Show("" + ee);
                conn.Close();
            }
        }

        private void dtgcon_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                conn.Open();

                int cid = int.Parse(dtgcon.Rows[e.RowIndex].Cells[0].Value.ToString());

                MySqlCommand comm = new MySqlCommand("SELECT condes, interviewdate, interviewer FROM consultation WHERE cid = " + cid, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {

                    richboxrecords.Text = dt.Rows[0]["condes"].ToString();

                    lbldatecon.Text = Convert.ToDateTime(dt.Rows[0]["interviewdate"]).ToString("MMMM dd, yyyy");

                    lblintcon.Text = dt.Rows[0]["interviewer"].ToString();

                }

                tabconrecords.SelectedTab = document;

                lblcontitle.Visible = false;

                conn.Close();

            }

            catch (Exception ee)
            {
                //MessageBox.Show("" + ee);
                conn.Close();
            }
        }

        private void dtincid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int incid = int.Parse(dtincid.Rows[e.RowIndex].Cells[0].Value.ToString());

                tabControl.SelectedTab = thirteen;

                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT type, incdate, venue, description, action FROM incident WHERE incidid = " + incid, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    inctype.Text = dt.Rows[0]["type"].ToString();
                    incidlocation.Text = dt.Rows[0]["venue"].ToString();
                    repinciddesc.Text = dt.Rows[0]["description"].ToString();
                    repincidaction.Text = dt.Rows[0]["action"].ToString();

                    lbldateincid.Text = dt.Rows[0]["incdate"].ToString();


                }

                tabControl.SelectedTab = thirteen;

                conn.Close();

            }

            catch (Exception ee)
            {
                //MessageBox.Show("" + ee);
                conn.Close();
            }
        }

        #endregion

        #region reset functions

        public void reset()
        {
            pbox1.Image = null;

            txtlname.Clear();
            txtfname.Clear();
            txtcaseaddress.Clear();

            cbxprogram.SelectedIndex = -1;
            cbxstatus.SelectedIndex = -1;

            dtbirth.Value = DateTime.Now.Date;
            dtjoin.Value = DateTime.Now.Date;
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

        public void resetall()
        {
            reset();
            reset2();
            reset3();
            reset4();
            reset5();
            reset6();
            reset7();
            reset8();
        }

        #endregion

        #region existsfunctions

        public void existsed(int id)
        {

            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT caseid FROM education WHERE caseid = " + id, conn);

                int UserExist = (int)comm.ExecuteScalar();

                btned.Text = (UserExist > 0) ? "View Info" : "Add Info"; //put add info on catch



                conn.Close();

            }


            catch (Exception ee)
            {
                btned.Text = "Add Info";

                lbledlvl.Text = "";
                lbledtype.Text = "";
                lbledschool.Text = "";

                conn.Close();
            }


        }

        public void existsinvolve(int incidid)
        {
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT involveid FROM involvement WHERE caseid = " + id, conn);



                int UserExist = (int)comm.ExecuteScalar();

                btnaddinvolve.Text = (UserExist > 0) ? "View Info" : "Add Info"; //put add info on catch

                conn.Close();

            }


            catch (Exception ee)
            {
                btnaddinvolve.Text = "Add Info";

                conn.Close();
            }

        }

        public void existsfam(int id)
        {

            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT caseid FROM family WHERE caseid = " + id, conn);

                int UserExist = (int)comm.ExecuteScalar();

                btned.Text = (UserExist > 0) ? "Edit Info" : "Add Info"; //put add info on catch

                comm = new MySqlCommand("SELECT familyid FROM family WHERE caseid = " + id, conn);

                MySqlDataAdapter adp = new MySqlDataAdapter(comm);

                DataTable dt = new DataTable();

                adp.Fill(dt);

                famid = int.Parse(dt.Rows[0]["familyid"].ToString());

                conn.Close();

            }


            catch (Exception ee)
            {
                btned.Text = "Add Info";

                lblfamilytype.Text = "";

                conn.Close();
            }


        }

        public void existshealth(int id)
        {

            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT caseid FROM health WHERE caseid = " + id, conn);

                int UserExist = (int)comm.ExecuteScalar();

                btnhealth.Text = (UserExist > 0) ? "View Info" : "Add Info"; //put add info on catch



                conn.Close();

            }


            catch (Exception ee)
            {
                btnhealth.Text = "Add Info";

                lblblood.Text = "";
                lblheight.Text = "";
                lblweight.Text = "";

                conn.Close();
            }


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
            tabControl.SelectedTab = sixteen;

            resetall();
        }

        private void familyTS_Click(object sender, EventArgs e)
        {
            resetTS();
            familyTS.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            familyTS.ForeColor = Color.Black;
            familyTS.BackgroundImage = global::BalayPasilungan.Properties.Resources.tsLine;
            tabControl.SelectedIndex = 1;

            resetall();
            btnfover_Click(sender, e);
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

            lbladdeditprofile.Text = "New Case Profile";

            tabCase.SelectedTab = tabNewChild;
            tabaddchild.SelectedTab = tabaddinfo;
        }

        private void male_CheckedChanged(object sender, EventArgs e)
        {
            //if (male.Checked) confGender.Text = "Male";
            //else confGender.Text = "Female";
        }

        #region Next Back Buttons
        private void btnNextFamily_Click(object sender, EventArgs e)
        {
            resetNewChildTS();
            tsNewFamily.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            tabaddchild.SelectedTab = tabNewFamily;
        }
        
        private void btnBackInfo_Click(object sender, EventArgs e) // Current tab: family
        {
            resetNewChildTS();
            tsNewInfo.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            tabaddchild.SelectedTab = tabaddinfo;
        }
        
        private void btnNextEdu_Click(object sender, EventArgs e)
        {
            resetNewChildTS();
            tsNewEdu.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            tabaddchild.SelectedTab = tabNewEdu;
        }

        private void btnBackFamily_Click(object sender, EventArgs e) // Current tab: education
        {
            resetNewChildTS();
            tsNewFamily.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            tabaddchild.SelectedTab = tabNewFamily;
        }

        private void btnNextHealth_Click(object sender, EventArgs e)
        {
            resetNewChildTS();
            tsNewHealth.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            tabaddchild.SelectedTab = tabNewHealth;
        }

        private void btnBackEdu_Click(object sender, EventArgs e) // Current tab: health
        {
            resetNewChildTS();
            tsNewEdu.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            tabaddchild.SelectedTab = tabNewEdu;
        }

        private void btnNextCon_Click(object sender, EventArgs e)
        {
            resetNewChildTS();
            tsNewCon.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            tabaddchild.SelectedTab = tabNewCon;
        }

        private void btnBackHealth_Click(object sender, EventArgs e)    // Current tab: consultation
        {
            resetNewChildTS();
            tsNewHealth.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            tabaddchild.SelectedTab = tabNewHealth;
        }

        private void btnNextIO_Click(object sender, EventArgs e)
        {
            resetNewChildTS();
            tsNewIO.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            tabaddchild.SelectedTab = tabNewIO;
        }

        private void btnBackCon_Click(object sender, EventArgs e)   // Current tab: IO
        {
            resetNewChildTS();
            tsNewCon.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            tabaddchild.SelectedTab = tabNewCon;
        }

        private void btnBackIO_Click(object sender, EventArgs e)    // Current tab: Confirmation
        {
            resetNewChildTS();
            tsNewIO.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            tabaddchild.SelectedTab = tabNewIO;
        }

        private void btnNewConfirm_Click(object sender, EventArgs e)
        {
            resetNewChildTS();
            tabaddchild.SelectedTab = tabNewConfirm;
        }

        #endregion

        #region New Child Info Form
        private void txtNewFName_Enter(object sender, EventArgs e)
        {
            /*resetTextboxes();
            countFName.Visible = true; lblFName.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141); txtNewFName.ForeColor = Color.Black;
            if (txtNewFName.Text == "Juan Miguel") txtNewFName.Clear();
            panelFName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;*/
        }

        private void panelLName_Enter(object sender, EventArgs e)
        {
            /*resetTextboxes();
            countLName.Visible = true; lblLName.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141); txtNewLName.ForeColor = Color.Black;
            if (txtNewLName.Text == "dela Cruz") txtNewLName.Clear();
            panelLName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;*/
        }

        private void txtNewNName_Enter(object sender, EventArgs e)
        {
            /*resetTextboxes();
            countNName.Visible = true; lblNName.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141); txtNewNName.ForeColor = Color.Black;
            if (txtNewNName.Text == "Juan") txtNewNName.Clear();
            panelNName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;*/
        }

        private void txtAddress_Enter(object sender, EventArgs e)
        {
            /*resetTextboxes();
            countAddress.Visible = true; lblAddress.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141); txtAddress.ForeColor = Color.Black;
            if (txtAddress.Text == "Where does the child live?") txtAddress.Clear();*/
        }

        /*private void txtNewFName_TextChanged(object sender, EventArgs e)
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
        }*/
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

        #region getid functions

        public void gethid(int id)
        {
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT hid FROM health WHERE caseid = " + id, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                hid = int.Parse(dt.Rows[0]["hid"].ToString());

                conn.Close();

            }


            catch (Exception ee)
            {

                MessageBox.Show("" + ee);
                conn.Close();
            }
        }

        #endregion

        private void btnMain_Click(object sender, EventArgs e)
        {
            tabCase.SelectedTab = tabInfo;
        }

        #region add/edit buttons

        private void btnaddeditcase_Click(object sender, EventArgs e)
        {
            if (btnaddeditcase.Text == "Add New Profile")
            {
                addprofile();
            }

            else
            {

                editprofile();
            }
            
        }

        private void btnaddhealth_Click(object sender, EventArgs e)
        {
            if (btnaddhealth.Text == "ADD")
            {
                addhealth();
            }

            else
            {
                edithealth();
            }

            //tabCase.SelectedTab = tabCases;
        }

        private void btnaddcheckuprec_Click(object sender, EventArgs e)
        {
            string location = txtlocationcheck.Text, details = rcheckdetails.Text;

            gethid(id);

            if (string.IsNullOrEmpty(location) || string.IsNullOrEmpty(details))
            {
                MessageBox.Show("Please fill out empty fields.");
            }

            else
            {
                try
                {

                    conn.Open();


                    MySqlCommand comm = new MySqlCommand("INSERT INTO checkup(hid, checkupdetails, checkupdate, checkuplocation) VALUES('" + hid + "', '" + details + "', '" + dtpcheck.Value.Date.ToString("yyyyMMdd") + "', '" + location + "')", conn);
                    MessageBox.Show(hid.ToString());
                    comm.ExecuteNonQuery();

                    MessageBox.Show("Checkup Record Added!");

                    conn.Close();

                    reloadhealth(id);

                    tabControl.SelectedTab = fifteen;
                   

                    reset6();
                }

                catch (Exception ee)
                {
                    MessageBox.Show("" + ee);
                    conn.Close();
                }
            }
        }

        private void btnadded_Click(object sender, EventArgs e)
        {
            string edname = txtedname.Text, type = cbxtype.Text, level = cbxlevel.Text;

            if (string.IsNullOrEmpty(edname) || string.IsNullOrEmpty(type) || string.IsNullOrEmpty(level))
            {
                MessageBox.Show("Please fill out empty fields.");
            }

            else
            {

                try
                {

                    conn.Open();


                    MySqlCommand comm = new MySqlCommand("INSERT INTO education(caseid, school, eduType, level) VALUES('" + id + "', '" + edname + "', '" + type + "','" + level + "')", conn);

                    comm.ExecuteNonQuery();

                    MessageBox.Show("New Info Added!");


                    conn.Close();

                    existsed(id);

                    lbledtypeview.Text = lbledtype.Text = type;
                    lblschool.Text = lbledschool.Text = edname;
                    lbllevel.Text = lbledlvl.Text = level;

                    tabControl.SelectedTab = eighth;
                    

                    reset2();

                }

                catch (Exception ee)
                {
                    MessageBox.Show("" + ee);
                    conn.Close();
                }
            }
        }

        private void btnaddcon_Click(object sender, EventArgs e)
        {
            string interviewer = txtintname.Text, condes = richconbox.Text;

            if (string.IsNullOrEmpty(interviewer) || string.IsNullOrEmpty(condes))
            {
                MessageBox.Show("Please fill out empty fields.");
            }

            else
            {
                try
                {

                    conn.Open();


                    MySqlCommand comm = new MySqlCommand("INSERT INTO consultation(caseid, condes, interviewdate, interviewer) VALUES('" + id + "', '" + condes + "', '" + condate.Value.Date.ToString("yyyyMMdd") + "','" + interviewer + "')", conn);

                    comm.ExecuteNonQuery();

                    MessageBox.Show("Consultation Record Added!");

                    conn.Close();

                    reloadcon(id);

                    tabControl.SelectedTab = ninth;

                    reset4();
                }

                catch (Exception ee)
                {
                    MessageBox.Show("" + ee);
                    conn.Close();
                }
            }
        }

        private void btnaddfamtype_Click(object sender, EventArgs e)
        {
            string famtype = cbxfamtype.Text;

            if (string.IsNullOrEmpty(famtype))
            {
                MessageBox.Show("Fill in the empty fields.");
            }

            else
            {
                try
                {
                    conn.Open();


                    MySqlCommand comm = new MySqlCommand("INSERT INTO family(caseid, famtype) VALUES('" + id + "', '" + famtype + "')", conn);

                    comm.ExecuteNonQuery();

                    MessageBox.Show("Family Type Added!");

                    conn.Close();

                    existsfam(id);

                    reloadfam(id);

                    tabControl.SelectedTab = fourth;

                    cbxfamtype.SelectedIndex = -1;
                }

                catch (Exception ee)
                {
                    MessageBox.Show("" + ee);
                    conn.Close();
                }
            }
        }

        private void btnaddmember_Click(object sender, EventArgs e)
        {
            string lastname = txtmemlastname.Text, firstname = txtmemfirstname.Text, relationship = txtmemrelationship.Text,
                   gender = cbxmemgender.Text, occupation = txtmemocc.Text, dependency = cbxmemdependency.Text;

            if (string.IsNullOrEmpty(lastname) || string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(relationship) || string.IsNullOrEmpty(gender) || string.IsNullOrEmpty(occupation) || string.IsNullOrEmpty(dependency))
            {
                MessageBox.Show("Please fill out empty fields.");
            }

            else
            {
                try
                {

                    conn.Open();


                    MySqlCommand comm = new MySqlCommand("INSERT INTO member(familyid, firstname, lastname, gender, birthdate, relationship, dependency, occupation) VALUES('" + famid + "', '" + firstname + "', '" + lastname + "', '" + gender + "', '" + dtpmembirth.Value.Date.ToString("yyyy-MM-dd") + "', '" + relationship + "', '" + dependency + "', '" + occupation + "')", conn);
                    MessageBox.Show(famid.ToString());
                    comm.ExecuteNonQuery();

                    MessageBox.Show("Member Added!");

                    conn.Close();

                    reloadmem(famid);

                    tabControl.SelectedTab = fourth;

                    reset8();
                }

                catch (Exception ee)
                {
                    MessageBox.Show("" + ee);
                    conn.Close();
                }
            }
        }

        private void btndeletefam_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dtfamOverview.Rows)
            {
                DataGridViewCheckBoxCell chk = row.Cells[8] as DataGridViewCheckBoxCell;

                if (Convert.ToBoolean(chk.Value) == true)
                {
                    dtfamOverview.Rows.Remove(row);
                }
            }
        }

        private void btnaddincidrecord_Click(object sender, EventArgs e)
        {
            string type = txttypeincid.Text, hour = cbxhour.Text, minute = cbxmin.Text, zone, location = txtincidlocation.Text, desc = rtxtinciddesc.Text, action = rtxtactiontaken.Text;

            if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(hour) || string.IsNullOrEmpty(minute) || string.IsNullOrEmpty(location) || string.IsNullOrEmpty(desc) || string.IsNullOrEmpty(action) || (rbam.Checked == false && rbpm.Checked == false))
            {
                MessageBox.Show("Please fill out empty fields.");
            }

            else
            {
                if (rbam.Checked == true)
                {
                    zone = "AM";
                }

                else
                {
                    zone = "PM";
                }

                DateTime dt = DateTime.Parse(hour + ":" + minute + " " + zone);

                //dt.ToString("hh:mm tt");

                //DateTime wut = DateTime.ParseExact(dateincid.Value.Date.ToString("yyyy-MM-dd") + " " + dt.ToString(), "yyyy-MM-dd hh:mm tt", CultureInfo.InvariantCulture);
                MessageBox.Show(dateincid.Value.Date.ToString("yyyy-MM-dd"));

                try
                {
                    conn.Open();


                    MySqlCommand comm = new MySqlCommand("INSERT INTO incident(caseid, type, incdate, venue, description, action, dateadded) VALUES('" + id + "', '" + type + "', '" + dateincid.Value.Date.ToString("yyyy-MM-dd ") + dt.ToString("hh:mm tt") + "','" + location + "', '" + desc + "', '" + action + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "')", conn);

                    comm.ExecuteNonQuery();

                    MessageBox.Show("Incident Record Added!");

                    conn.Close();

                    reloadincid(id);

                    tabControl.SelectedTab = twelfth;

                    reset5();
                }

                catch (Exception ee)
                {
                    MessageBox.Show("" + ee);
                    conn.Close();

                }

            }
        }

        #endregion

        #region back buttons
        private void btncancel_Click(object sender, EventArgs e)
        {
            if (btnaddeditcase.Text == "Add Changes")
            {
                tabCase.SelectedTab = tabInfo;
            }

            else
            {
                tabCase.SelectedTab = tabCases;
            }

            reset();

            
        }

        private void btnbackcasestud_Click(object sender, EventArgs e)
        {
            tabCase.SelectedTab = tabCases;
        }

        private void btncancelhealth_Click(object sender, EventArgs e)
        {
            if (btnaddhealth.Text == "ADD")
            {

                tabCase.SelectedTab = tabInfo;

            }

            else
            {
                tabCase.SelectedTab = tabInfo;
            }

            reset3();
        }

        private void btnbackfromhealthview_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = sixteen;
        }

        private void btnbackfromhealth_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = seventeen;
        }

        private void btnbackfromcheck_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = fifteen;

            reset6();
        }

        private void bttnbackfromcheckrec_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = fifteen;
        }

        private void btncanceled_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = sixteen;
            reset2();
        }

        private void btnedback_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = sixteen;
        }

        private void btncancelcon_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = sixteen;
        }

        private void btncancon_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = ninth;
        }

        private void btncancelviewrec_Click(object sender, EventArgs e)
        {
            tabconrecords.SelectedTab = tabrecords;
            richboxrecords.Clear();

            lblcontitle.Visible = true;
        }

        private void btncanfamtype_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = fourth;
        }

        private void btnbackfam_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = sixteen;

            reset7();
        }

        private void btnbacktofamoverview_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = fourth;

            reset8();
        }

        private void btnbackmainincid_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = sixteen;
        }

        private void btnbackincidrec_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = twelfth;

            reset5();
        }

        private void btnbackfrominc_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = twelfth;
        }

        #endregion

        #region into buttons

        private void btnhealth_Click(object sender, EventArgs e)
        {
            if (btnhealth.Text == "Add Info")
            {
                tabCase.SelectedTab = tabNewChild;
                tabaddchild.SelectedTab = tabNewHealth;

                lbladdeditprofile.Text = "Add New Health Biography";
            }

            else
            {
                tabControl.SelectedTab = seventeen;

                reloadedithealth(id);
            }
        }

        private void btned_Click(object sender, EventArgs e)
        {
            lblnamed.Text = lblnamedrpt.Text = lblcasename.Text;

            if (btned.Text == "Add Info")
            {
                tabControl.SelectedTab = seventh;
            }

            else
            {
                tabControl.SelectedTab = eighth;

                try
                {
                    conn.Open();

                    string[] data = lblnamedrpt.Text.Split(' ');

                    MySqlCommand comm = new MySqlCommand("SELECT school, edutype, level FROM education WHERE caseid = " + id, conn);
                    MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                    DataTable dt = new DataTable();

                    adp.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {

                        lblschool.Text = dt.Rows[0]["school"].ToString();
                        lbledtypeview.Text = dt.Rows[0]["edutype"].ToString();
                        lbllevel.Text = dt.Rows[0]["level"].ToString();

                        lbledlvl.Text = dt.Rows[0]["level"].ToString();
                        lbledtype.Text = dt.Rows[0]["edutype"].ToString();
                        lbledschool.Text = dt.Rows[0]["school"].ToString();

                    }

                    conn.Close();
                }




                catch (Exception ee)
                {
                    MessageBox.Show("" + ee);
                    conn.Close();
                }
            }
        }

        private void btncon_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = ninth;
            tabconrecords.SelectedTab = tabrecords;

            reloadcon(id);
        }

        private void btnfover_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = fourth;

            existsfam(id);

            reloadfam(id);
        }

        private void btnincidview_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = twelfth;

            reloadincid(id);
        }

        private void btneditprofile_Click(object sender, EventArgs e)
        {

            tabCase.SelectedTab = tabNewChild;

            btnaddeditcase.Text = "Add Changes";
            lbladdeditprofile.Text = "Edit Profile";

            reloadeditinfo(id);
        }

        private void btngotocheckup_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = fifteen;

            reloadhealth(id);
        }

        private void btnedithealth_Click(object sender, EventArgs e)
        {
            btnaddhealth.Text = "Add Changes";

            txtheight.Text = lblvheight.Text;
            txtweight.Text = lblvweight.Text;
            cbxbloodtype.Text = lblvblood.Text;
            rtxtall.Text = rviewall.Text;
            rtxtcondition.Text = rviewcondition.Text;

            tabCase.SelectedTab = tabNewChild;
            tabaddchild.SelectedTab = tabNewHealth;

            lbladdeditprofile.Text = "Edit Health Biography";
        }

        private void btngotohealth_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = eighteen;
        }
        private void btnaddconrec_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = sixth;
        }

        private void btnfamtype_Click(object sender, EventArgs e)
        {
            if (btnfamtype.Text == "add")
            {
                tabControl.SelectedTab = fifth;
            }
        }

        private void btnAddMem_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = twenty;
        }

        private void btnaddincid_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tenth;
        }

        #endregion

        private void noFocusRec1_Click(object sender, EventArgs e)
        {

        }

        private void tabNewInfo_Click(object sender, EventArgs e)
        {

        }

        private void newprofilepic_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";
                dlg.Filter = "All image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png| JPEG Files (*.jpg, *.jpeg, *.jpe)|*.jpg; *.jpeg; *.jpe|PNG files (*.png)|*.png|BMP files (*.bmp)|*.bmp";

                if (dlg.ShowDialog() == DialogResult.OK)
                {                    
                    //newprofilepic.Image = new Bitmap(dlg.FileName);
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
