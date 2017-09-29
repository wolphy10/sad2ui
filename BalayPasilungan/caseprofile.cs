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

using iTextSharp.text;
using iTextSharp.text.pdf;

namespace BalayPasilungan
{
    public partial class caseprofile : Form
    {
        //public Form2 ref_to_main { get; set; }
        public MySqlConnection conn;
        public Form reftomain { get; set; }

        public int id, hid, fammode, famid, eid, classeid, memberid, incidid, mode, archiveid, archivemode;
        public string filename, yearlvl, section, adviser;
        public bool empty, confirmed, dot;

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

        public void resetNewChildTS()   // Reset New Child ToolStrip to Gray
        {
            tsNewFamily.ForeColor = System.Drawing.Color.FromArgb(201, 201, 201);
            tsNewCon.ForeColor = System.Drawing.Color.FromArgb(201, 201, 201);
            tsNewEdu.ForeColor = System.Drawing.Color.FromArgb(201, 201, 201);
            tsNewHealth.ForeColor = System.Drawing.Color.FromArgb(201, 201, 201);
            tsNewInfo.ForeColor = System.Drawing.Color.FromArgb(201, 201, 201);
            tsNewIO.ForeColor = System.Drawing.Color.FromArgb(201, 201, 201);
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

        public void edclass(int classeid, string yearlvl)
        {
            edclass ed = new edclass();

            ed.reftocase = this;

            ed.classeid = classeid;
            ed.level = yearlvl;

            ed.Show();
        }

        public void edclass(int classeid, string yearlvl, string section, string adviser)
        {
            edclass ed = new edclass();

            ed.reftocase = this;

            ed.lbladdeditprofile.Text = "Edit Class";
            ed.btnaddedclass.Text = "ADD CHANGES";

            ed.classeid = classeid;
            ed.level = yearlvl;
            ed.txtedadviser.Text = adviser;
            ed.txtedsection.Text = section;
            ed.cbxedyear.Text = ed.level;
            ed.Show();
        }

        public void famtypecall(int x, string text)
        {
            famtype fam = new famtype();
            fam.reftofam = this;
            fam.caseid = x;
            fam.text = text;
            fam.Show();
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
        #endregion

        #region Main Buttons        
        private void btnClose_Click(object sender, EventArgs e)
        {
            confirm conf = new confirm();
            dim dim = new dim();

            dim.Location = this.Location; dim.Size = this.Size;
            dim.refToPrev = this;
            dim.Show(this);
            conf.lblConfirm.Text = "Are you sure you want to leave?";
            conf.refToPrev = this;

            if (conf.ShowDialog() == DialogResult.OK)
            {
                conf.Close();
                this.Close();
            }
            dim.Close();
        }
        
        private void taskbar_Click(object sender, EventArgs e)
        {
            btnMain.ForeColor = btnCases.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            logo_main.BackgroundImage = Properties.Resources.main_fade;
            logo_cases.BackgroundImage = Properties.Resources.case_fade;            
            ((Button)sender).ForeColor = System.Drawing.Color.FromArgb(15, 168, 104);
            if (((Button)sender).Name == "btnCases")
            {
                logo_cases.BackgroundImage = Properties.Resources._case;
                tabCase.SelectedTab = tabCases;
            }
            else
            {
                logo_main.BackgroundImage = Properties.Resources.main;
            }
        }
        
        private void logo_click(object sender, EventArgs e)
        {
            btnMain.ForeColor = btnCases.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            logo_main.BackgroundImage = Properties.Resources.main_fade;
            logo_cases.BackgroundImage = Properties.Resources.case_fade;            
            if (((PictureBox)sender).Name == "logo_cases") 
            {
                btnCases.ForeColor = System.Drawing.Color.FromArgb(15, 168, 104);
                logo_cases.BackgroundImage = Properties.Resources._case;
                tabCase.SelectedTab = tabCases;
            }
            else
            {
                btnMain.ForeColor = System.Drawing.Color.FromArgb(15, 168, 104);
                logo_main.BackgroundImage = Properties.Resources.main;
            }
        }
        #endregion


        
        #region Case Profile Load
        private void caseprofile_Load(object sender, EventArgs e)
        {
            lbladdeditprofile.Text = "NEW CASE PROFILE";
            btnaddeditcase.Text = "ADD NEW PROFILE";

            dtbirth.MaxDate = dtjoin.MaxDate = condate.MaxDate = dtpcheck.MaxDate = dateincid.MaxDate = dtpmembirth.MaxDate = DateTime.Now;
            tabCase.SelectedTab = tabCases;
            try
            {
                conn.Open();

                MySqlDataAdapter adp = new MySqlDataAdapter("SELECT caseid, lastname, firstname, program FROM casestudyprofile WHERE profilestatus = " + 1, conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    btnArchive.Enabled = false;
                    dt.Rows.Add(-1, "No entries.", null, null);
                    empty = true;
                }
                else
                {
                    multiChild.Checked = multiChild.Enabled = false;
                    btnArchive.Enabled = true;
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
                    dtgcs.Columns[1].HeaderCell.Style.Padding = dtgcs.Columns[1].DefaultCellStyle.Padding = new Padding(15, 0, 0, 0);
                    getdrop(); getresidential(); getcount();
                    multiChild.Enabled = true;
                }
                conn.Close();
            }
            catch (Exception ee)
            {
                errorMessage(ee.Message);
                conn.Close();
            }
        }
        #endregion

        #region homemade functions
        private void pbox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog rest = new OpenFileDialog();
            rest.Filter = "images| *.JPG; *.PNG; *.GIF"; // you can add any other image type 

            if (rest.ShowDialog() == DialogResult.OK)
            {
                pbox1.Image = System.Drawing.Image.FromFile(rest.FileName);
                filename = Path.GetFullPath(rest.FileName).Replace(@"\", @"\\");
            }
        }

        public void addmember(int x)
        {
            string lastname = txtmemlastname.Text, firstname = txtmemfirstname.Text, relationship = txtmemrelationship.Text,
                   status = cbxmemstatus.Text, occupation = txtmemocc.Text, remarks = rtremarks.Text, eduattain = cbxmemeduattain.Text;

            double income;

            DateTime birthdate = dtpmembirth.Value;

            int age = DateTime.Now.Year - birthdate.Year;

            if (birthdate.DayOfYear > DateTime.Now.DayOfYear)
                age--;

            if (string.IsNullOrEmpty(lastname) || string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(relationship) || string.IsNullOrEmpty(status) || string.IsNullOrEmpty(occupation) || string.IsNullOrEmpty(remarks)
                || string.IsNullOrEmpty(eduattain) || string.IsNullOrEmpty(txtmemincome.Text)) errorMessage("Please fill out empty fields.");            
            else
            {
                if (double.TryParse(txtmemincome.Text, out income))
                {
                    income = double.Parse(txtmemincome.Text);
                    try
                    {
                        conn.Open();
                        MySqlCommand comm = new MySqlCommand("INSERT INTO member(familyid, firstname, lastname, civilstatus, birthdate, relationship, remarks, occupation, age, eduattain, monthlyincome) " +
                            "VALUES(" + x + ", '" + firstname + "', '" + lastname + "', '" + status + "', '" + dtpmembirth.Value.Date.ToString("yyyy-MM-dd") + "', '" + relationship + "', '" + remarks + "'" +
                            ", '" + occupation + "' , '" + age + "', '" + eduattain + "', '" + income + "')", conn);
                        
                        comm.ExecuteNonQuery();
                        successMessage("Member has been added successfully!");
                        conn.Close();

                        reloadmem(x);
                        existsfam(id);

                        tabCase.SelectedTab = tabInfo;
                        tabChild.SelectedTab = fourth;
                        reset8();
                    }
                    catch (Exception ee)
                    {
                        errorMessage(ee.Message);
                        conn.Close();                    
                    }
                }
                else errorMessage("Income input is not numeric!");                    
            }
        }

        public void editmember(int x)
        {
            string lastname = txtmemlastname.Text, firstname = txtmemfirstname.Text, relationship = txtmemrelationship.Text,
                   status = cbxmemstatus.Text, occupation = txtmemocc.Text, remarks = rtremarks.Text, eduattain = cbxmemeduattain.Text;

            double income;

            DateTime birthdate = dtpmembirth.Value;

            int age = DateTime.Now.Year - birthdate.Year;

            if (birthdate.DayOfYear > DateTime.Now.DayOfYear)
                age--;

            if (string.IsNullOrEmpty(lastname) || string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(relationship) || string.IsNullOrEmpty(status) || string.IsNullOrEmpty(occupation) || string.IsNullOrEmpty(remarks)
                || string.IsNullOrEmpty(eduattain) || string.IsNullOrEmpty(txtmemincome.Text))
            {
                errorMessage("Please fill out empty fields.");
            }
            else
            {
                if (double.TryParse(txtmemincome.Text, out income))
                {
                    income = double.Parse(txtmemincome.Text);

                    try
                    {
                        conn.Open();
                        MySqlCommand comm = new MySqlCommand("UPDATE member SET firstname = '" + firstname + "', lastname = '" + lastname + "', civilstatus = '" + status + "', birthdate = '" + dtpmembirth.Value.Date.ToString("yyyy-MM-dd") + "', " +
                                            "relationship = '" + relationship + "', remarks = '" + remarks + "', occupation = '" + occupation + "', eduattain = '" + eduattain + "', monthlyincome = '" + income + "'" +
                                            ", age = '" + age + "'WHERE memberid = " + memberid, conn);

                        comm.ExecuteNonQuery();

                        successMessage("Family member details have been modified successfully!");

                        conn.Close();

                        reloadmem(x);
                        existsfam(id);

                        tabCase.SelectedTab = tabInfo;
                        tabChild.SelectedTab = fourth;

                        reset8();
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show(ee.ToString());
                        conn.Close();
                    }

                }

                else
                {
                    errorMessage("Income input is not numeric!");
                    conn.Close();
                }

            }
        }

        public void addcon()
        {
            string interviewer = txtintname.Text, condes = richconbox.Text;

            if (string.IsNullOrEmpty(interviewer) || string.IsNullOrEmpty(condes)) errorMessage("Please fill out empty fields.");            
            else
            {
                try
                {
                    conn.Open();
                    
                    MySqlCommand comm = new MySqlCommand("INSERT INTO consultation(caseid, condes, interviewdate, interviewer) VALUES('" + id + "', '" + condes + "', '" + condate.Value.Date.ToString("yyyyMMdd") + "','" + interviewer + "')", conn);
                    comm.ExecuteNonQuery();                
                    successMessage("New consultation record has been added successfully!");
                    conn.Close();

                    reloadcon(id);
                    existscon(id);

                    tabCase.SelectedTab = tabInfo;
                    tabChild.SelectedTab = ninth;

                    reset4();
                }
                catch (Exception ee)
                {
                    errorMessage(ee.Message);                    
                }
            }
        }

        public void editcon()
        {
            string interviewer = txtintname.Text, condes = richconbox.Text;

            if (string.IsNullOrEmpty(interviewer) || string.IsNullOrEmpty(condes))
            {
                errorMessage("Please fill out empty fields.");
            }

            else
            {
                try
                {
                    conn.Open();
                    
                    MySqlCommand comm = new MySqlCommand("INSERT INTO consultation(caseid, condes, interviewdate, interviewer) VALUES('" + id + "', '" + condes + "', '" + condate.Value.Date.ToString("yyyyMMdd") + "','" + interviewer + "')", conn);
                    comm.ExecuteNonQuery();
                    successMessage("New consultation record has been added successfully!");
                    conn.Close();

                    reloadcon(id);
                    existscon(id);

                    tabCase.SelectedTab = tabInfo;
                    tabChild.SelectedTab = ninth;

                    reset4();
                }

                catch (Exception ee)
                {
                    errorMessage(ee.Message);
                }
            }
        }

        public void refresh()
        {
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT caseid, lastname, firstname, program FROM casestudyprofile WHERE profilestatus = " + 1, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                
                    adp.Fill(dt);

                    dtgcs.DataSource = dt;

                    dtgcs.Columns["caseid"].Visible = false;

                    dtgcs.Columns[1].Width = 380;
                    dtgcs.Columns[2].Width = 380;
                    dtgcs.Columns[3].Width = 175;

                    getdrop();
                    getresidential();
                    getcount();

                    if (dtgcs.Columns["Discharge"] != null)
                    {
                        dtgcs.Columns.Remove("Discharge");
                    }

                    btncancelarchive.Visible = false;
                    btnaddarchive.Visible = false;

                    if (dt.Rows.Count > 0)
                {
                    btnArchive.Enabled = true;
                    dtgcs.Enabled = true;
                }

                    else
                {
                    btnArchive.Enabled = false;
                    dtgcs.Enabled = false;
                }
                conn.Close();
            }
            catch (Exception ee)
            {
                errorMessage(ee.Message);
            }

            //tabControl.SelectedTab = first;
        }

        public void refresh2()
        {
            MySqlDataAdapter adp = new MySqlDataAdapter("SELECT caseid, lastname, firstname FROM casestudyprofile WHERE profilestatus = " + 0, conn);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(-1, "No entries.", null);
                empty = true;

                btnrestorecaseprof.Enabled = false;
                dtgarchive.Enabled = false;
            }

            else
            {
                btnrestorecaseprof.Enabled = true;
                dtgarchive.Enabled = true;
            }

            dtgarchive.DataSource = dt;
            // Case Profile UI Modifications
            dtgarchive.Columns[1].HeaderText = "LASTNAME";
            dtgarchive.Columns[2].HeaderText = "FIRSTNAME";

            // For ID purposes (hidden from user)            
            dtgarchive.Columns["caseid"].Visible = false;

            // 935 WIDTH
            dtgarchive.Columns[1].Width = 380;
            dtgarchive.Columns[2].Width = 380;


            if (dt.Rows.Count > 0 && !empty)
            {
                dtgarchive.Columns[1].HeaderCell.Style.Padding = dtgarchive.Columns[1].DefaultCellStyle.Padding = new Padding(15, 0, 0, 0);
                getcount2();
                multiChild.Enabled = true;
                dtgarchive.Enabled = true;
            }
            else multiChild.Checked = multiChild.Enabled = false;

            btncancelrestore.Visible = false;
            btnrestore.Visible = false;

            if (dtgarchive.Columns["Restore"] != null)
            {
                dtgarchive.Columns.Remove("Restore");
            }

            getcount2();
        }

        public void getdrop()
        {
            MySqlCommand comm = new MySqlCommand("SELECT COUNT(*) FROM casestudyprofile WHERE program = 'Drop-In' AND profilestatus = " + 1, conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(comm);
            DataTable dt = new DataTable();

            adp.Fill(dt);

            lbldrop.Text = dt.Rows[0]["count(*)"].ToString();
        }

        public void getresidential()
        {
            MySqlCommand comm = new MySqlCommand("SELECT COUNT(*) FROM casestudyprofile WHERE program = 'Residential' AND profilestatus = " + 1, conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(comm);
            DataTable dt = new DataTable();

            adp.Fill(dt);

            lblresidential.Text = dt.Rows[0]["count(*)"].ToString();
        }

        public void getcount()
        {
            MySqlCommand comm = new MySqlCommand("SELECT COUNT(caseid) FROM casestudyprofile WHERE profilestatus = " + 1, conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(comm);
            DataTable dt = new DataTable();

            adp.Fill(dt);

            lbltotalcase.Text = dt.Rows[0]["count(caseid)"].ToString();
        }

        public void getcount2()
        {
            MySqlCommand comm = new MySqlCommand("SELECT COUNT(caseid) FROM casestudyprofile WHERE profilestatus = " + 0, conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(comm);
            DataTable dt = new DataTable();

            adp.Fill(dt);

            lblnumberofarchive.Text = dt.Rows[0]["count(caseid)"].ToString();
        }

        public void addprofile()
        {            
            if (string.IsNullOrEmpty(txtcaseaddress.Text) || string.IsNullOrEmpty(txtfname.Text) || string.IsNullOrEmpty(txtlname.Text) || string.IsNullOrEmpty(txtReligion.Text)) errorMessage("Please fill out empty fields.");
            else
            {
                DateTime birthdate = dtbirth.Value;

                int age = DateTime.Now.Year - birthdate.Year;

                if (birthdate.DayOfYear > DateTime.Now.DayOfYear)
                    age--;

               
                try
                {
                    conn.Open();

                    MySqlCommand comm = new MySqlCommand();
                    if (cbIP.Checked)       // IP
                    {
                        comm = new MySqlCommand("INSERT INTO casestudyprofile (lastname, firstname, birthdate, alias, birthplace, civilstatus, program, dateJoined, picture, address, profilestatus, religion, age)"
                        + " VALUES('" + txtlname.Text + "', '" + txtfname.Text + "', '" + dtbirth.Value.Date.ToString("yyyy-MM-dd") + "','" + txtAlias.Text + "','" + txtBirthplace.Text + "', '" + cbCivilStatus.SelectedItem
                        + "', '" + cbxprogram.SelectedItem + "', '" + dtjoin.Value.ToString("yyyy-MM-dd") + "', '" + filename + "', '" + txtcaseaddress.Text + "', 1, '" + txtIP.Text + "', '" + age + "')", conn);
                    }
                    else                    // Religion
                    {
                        comm = new MySqlCommand("INSERT INTO casestudyprofile (lastname, firstname, birthdate, alias, birthplace, civilstatus, program, dateJoined, picture, address, profilestatus, religion, age)"
                        + " VALUES('" + txtlname.Text + "', '" + txtfname.Text + "', '" + dtbirth.Value.Date.ToString("yyyy-MM-dd") + "','" + txtAlias.Text + "','" + txtBirthplace.Text + "', '" + cbCivilStatus.SelectedItem
                        + "', '" + cbxprogram.SelectedItem + "', '" + dtjoin.Value.ToString("yyyy-MM-dd") + "', '" + filename + "', '" + txtcaseaddress.Text + "', 1, '" + txtReligion.Text + "', '" + age + "')", conn);
                    }
                    
                    comm.ExecuteNonQuery();
                    conn.Close();
                    tabCase.SelectedTab = tabCases;
                    successMessage("New profile has been added successfully!");    
                    reset(); refresh();
                }
                catch (Exception ee)
                {
                    errorMessage(ee.Message);
                }
            }
        }

        public void editprofile()
        {
            string lname = txtlname.Text, fname = txtfname.Text, program = cbxprogram.Text, address = txtcaseaddress.Text;
            int age;
            DateTime birthdate = dtbirth.Value;
            
            if (string.IsNullOrEmpty(address) || string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(lname) || string.IsNullOrEmpty(program)) errorMessage("PLease fill out empty fields.");            
            else
            {
               

                age = DateTime.Now.Year - birthdate.Year;

                if (birthdate.DayOfYear > DateTime.Now.DayOfYear)
                    age--;
                try
                {
                    conn.Open();
                    MySqlCommand comm = new MySqlCommand("UPDATE casestudyprofile SET lastname = '" + lname + "', firstname = " +
                                        "'" + fname + "', birthdate = " + dtbirth.Value.Date.ToString("yyyyMMdd") + ", civilStatus = '" + "sample" + "', " +
                                        "program = '" + program + "', datejoined = " + dtjoin.Value.Date.ToString("yyyyMMdd") + ", " +
                                        "picture = '" + filename + "', address = '" + address + "', age = " + age + "  WHERE caseid = " + id, conn);
                    comm.ExecuteNonQuery();
                    conn.Close();
                    successMessage("Profile details has been changed successfully!");
                    tabChild.SelectedTab = sixteen;
                    tabCase.SelectedTab = tabInfo;
                    reset(); refresh();

                    reload(id);                
                    
                    
                }
                catch (Exception ee)
                {
                    errorMessage(ee.Message);
                    conn.Close();
                }                
            }
        }

        public void addhealth()
        {
            string blood = cbxbloodtype.Text, allergy = rtxtall.Text, condition = rtxtcondition.Text;
            double height, weight;

            if (string.IsNullOrEmpty(blood) || string.IsNullOrEmpty(txtheight.Text) || string.IsNullOrEmpty(txtweight.Text) || string.IsNullOrEmpty(allergy) || string.IsNullOrEmpty(condition)) errorMessage("Please fill out empty fields.");            
            else
            {
                if (double.TryParse(txtheight.Text, out height) && double.TryParse(txtweight.Text, out weight))
                {
                    height = double.Parse(txtheight.Text); weight = double.Parse(txtweight.Text);
                    try
                    {                    
                        conn.Open();
                        MySqlCommand comm = new MySqlCommand("INSERT INTO health(caseid, height, weight, bloodtype, allergies, hecondition) VALUES('" + id + "', '" + height + "', '" + weight + "','" + blood + "','" + allergy + "','" + condition + "')", conn);
                        comm.ExecuteNonQuery();
                        successMessage("New health biography has been added successfully!");
                        conn.Close();

                        tabCase.SelectedTab = tabInfo;
                        tabChild.SelectedTab = fifteen;

                        existshealth(id);                             
                        reloadedithealth(id);                        
                        reset3();
                    }
                    catch (Exception ee)
                    {
                        errorMessage(ee.Message);
                    }                
                }
                else
                {
                    if (double.TryParse(txtheight.Text, out height) == false && double.TryParse(txtweight.Text, out weight) == false) errorMessage("Invalid weight and height.");                    
                    else if (double.TryParse(txtheight.Text, out height) == false) errorMessage("Height input is invalid! Use numbers!");                    
                    else errorMessage("Weight input is invalid! Use numbers!");                    
                }
            }
        }

        public void edithealth()
        {
            string blood = cbxbloodtype.Text, allergy = rtxtall.Text, condition = rtxtcondition.Text;
            double height, weight;

            if (string.IsNullOrEmpty(blood) || string.IsNullOrEmpty(txtheight.Text) || string.IsNullOrEmpty(txtweight.Text) || string.IsNullOrEmpty(allergy) || string.IsNullOrEmpty(condition))
            {
                errorMessage("Please fill out empty fields.");
            }

            else
            {

                if (double.TryParse(txtheight.Text, out height) && double.TryParse(txtweight.Text, out weight))
                {
                    height = double.Parse(txtheight.Text); weight = double.Parse(txtweight.Text);

                    try
                    {                    
                        conn.Open();
                        MySqlCommand comm = new MySqlCommand("UPDATE health SET height = '" + height + "', weight = '" + weight + "', bloodtype = '" + blood + "', allergies = '" + allergy + "', hecondition = '" + condition + "' WHERE caseid = " + id, conn);
                        comm.ExecuteNonQuery();
                        successMessage("Health biography details have been modified successfully!");
                        conn.Close();                       

                        existshealth(id);
                        reloadedithealth(id);

                        tabCase.SelectedTab = tabInfo;
                        tabChild.SelectedTab = fifteen;

                        reset3();
                    }
                    catch (Exception ee)
                    {
                        errorMessage(ee.Message);
                    }
                }
                else
                {
                    if (double.TryParse(txtheight.Text, out height) == false && double.TryParse(txtweight.Text, out weight) == false) errorMessage("Height and Weight inputs are invalid! Use numbers!");                    
                    else if (double.TryParse(txtheight.Text, out height) == false) errorMessage("Height input is invalid! Use numbers!");
                    else errorMessage("Weight input is invalid! Use numbers!");                    
                }
            }
        }

        public void addeducation()
        {
            string edname = txtedname.Text, type = cbxtype.Text, level = cbxedlvl.Text;
            if (string.IsNullOrEmpty(edname) || string.IsNullOrEmpty(type) || string.IsNullOrEmpty(level)) errorMessage("Please fill out empty fields.");            
            else
            {
                try
                {
                    conn.Open();
                    MySqlCommand comm = new MySqlCommand("INSERT INTO education(caseid, school, program, level) VALUES('" + id + "', '" + edname + "', '" + type + "','" + level + "')", conn);
                    comm.ExecuteNonQuery();                
                    successMessage("New education information has been added successfully!");                    
                    conn.Close();
                    
                    tabCase.SelectedTab = tabInfo;
                    tabChild.SelectedTab = eighth;

                    reloaded(id);
                    existsed(id);

                    reset2();
                }
                catch (Exception ee)
                {
                    errorMessage(ee.Message);
                    conn.Close();
                }
            }
        }

        public void editeducation()
        {
            string edname = txtedname.Text, type = cbxtype.Text, level = cbxedlvl.Text;       
            if (string.IsNullOrEmpty(edname) || string.IsNullOrEmpty(type) || string.IsNullOrEmpty(level)) errorMessage("Please fill out empty fields.");            
            else
            {
                try
                {                    
                    conn.Open();
                    MySqlCommand comm = new MySqlCommand("UPDATE education SET school = '" + edname + "', program = '" + type + "', level = '" + level + "' WHERE eid = " + eid, conn);
                    comm.ExecuteNonQuery();
                    successMessage("Education information has been modified successfully!");                    
                    conn.Close();
                    
                    tabCase.SelectedTab = tabInfo;
                    tabChild.SelectedTab = eighth;

                    reloaded(id);
                    existsed(id);
                    reset2();                
                }
                catch (Exception ee)
                {
                    errorMessage(ee.Message);
                    conn.Close();
                }
            }
        }
        
        #endregion

        #region reloadfunctions

        public void reloadediteducation(int eid) //LOAD DATAGRIDVIEW CONCERNING SCHOOL
        {
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT school, program, level FROM education WHERE eid = " + eid, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm); DataTable dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    txtedname.Text = dt.Rows[0]["school"].ToString();
                    cbxtype.Text = dt.Rows[0]["program"].ToString();
                    cbxedlvl.Text = dt.Rows[0]["level"].ToString();                    
                }
                else errorMessage("This case study has no records yet.");                

                conn.Close();
            }

            catch (Exception ee)
            {
                errorMessage(ee.Message);
                conn.Close();
            }
        }

        public void reloadeditincid(int id)
        {
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT incident.incidid, type, incdate, venue, description, action, witnesses FROM incident " +
                                    "WHERE incident.caseid = " + id,  conn);

                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                //MessageBox.Show(id.ToString());
                adp.Fill(dt);

                incidid = int.Parse(dt.Rows[0]["incidid"].ToString());

                txttypeincid.Text = dt.Rows[0]["type"].ToString();
                txtincidlocation.Text = dt.Rows[0]["venue"].ToString();
                rtxtinciddesc.Text = dt.Rows[0]["description"].ToString();
                rtxtactiontaken.Text = dt.Rows[0]["action"].ToString();
                rtinvolve.Text = dt.Rows[0]["witnesses"].ToString();

                dateincid.Value = Convert.ToDateTime(dt.Rows[0]["incdate"]).Date;
            
                conn.Close();
            }

            catch (Exception ee)
            {
                errorMessage(ee.Message);
                conn.Close();
            }
        }

        public void addincidrecord()
        {
            string type = txttypeincid.Text, location = txtincidlocation.Text, desc = rtxtinciddesc.Text, action = rtxtactiontaken.Text, witnesses = rtinvolve.Text;
            if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(location) || string.IsNullOrEmpty(desc) || string.IsNullOrEmpty(action) || string.IsNullOrEmpty(witnesses)) errorMessage("Please fill out empty fields.");            
            else
            {                
                //MessageBox.Show(dateincid.Value.Date.ToString("yyyy-MM-dd"));            
                try
                {
                    conn.Open();
                    
                    MySqlCommand comm = new MySqlCommand("INSERT INTO incident(caseid, type, incdate, venue, description, action, dateadded, witnesses) VALUES('" + id + "', '" + type + "', '" + dateincid.Value.Date.ToString("yyyy-MM-dd ") + "','" + location + "', '" + desc + "', '" + action + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "', '" + witnesses + "')", conn);
                    comm.ExecuteNonQuery();
                    successMessage("New incident record has been added successfully!");
                    conn.Close();

                    reloadincid(id);
                    existsincid(id);

                    tabCase.SelectedTab = tabInfo;
                    tabChild.SelectedTab = twelfth;

                    reset5();
                }

                catch (Exception ee)
                {
                    errorMessage(ee.Message);
                    conn.Close();
                }

            }

            
        }

        public void editincidrecord()
        {
            string type = txttypeincid.Text, location = txtincidlocation.Text, desc = rtxtinciddesc.Text, action = rtxtactiontaken.Text, witnessses = rtinvolve.Text;

            if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(location) || string.IsNullOrEmpty(desc) || string.IsNullOrEmpty(action) || string.IsNullOrEmpty(witnessses)) errorMessage("Please fill out empty fields.");             
            else
            {                
                //MessageBox.Show(dateincid.Value.Date.ToString("yyyy-MM-dd"));
                try
                {
                    conn.Open();
                    MySqlCommand comm = new MySqlCommand("UPDATE incident SET type = '" + type + "', incdate = '" + dateincid.Value.Date.ToString("yyyy-MM-dd ") + "', " +
                        "venue = '" + location + "', description = '" + desc + "', action = '" + action + "', witnesses = '" + witnessses + "' WHERE incidid = " + incidid, conn);
                    comm.ExecuteNonQuery();
                    successMessage("Incident record has been modified successfully!");
                    conn.Close();

                    reloadincid(id);
                    existsincid(id);

                    tabCase.SelectedTab = tabInfo;
                    tabChild.SelectedTab = twelfth;

                    reset5();                    
                }

                catch (Exception ee)
                {
                    errorMessage(ee.Message);
                    conn.Close();
                }
            }            
        }

        public void reloadeditinfo(int id)
        {
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT lastname, firstname, birthdate, program, civilStatus, address, datejoined, picture FROM casestudyprofile WHERE caseid = " + id, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm); DataTable dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    txtlname.Text = dt.Rows[0]["lastname"].ToString();
                    txtfname.Text = dt.Rows[0]["firstname"].ToString();
                    txtcaseaddress.Text = dt.Rows[0]["address"].ToString();
                    cbxprogram.Text = dt.Rows[0]["program"].ToString();

                    dtbirth.Value = Convert.ToDateTime(dt.Rows[0]["birthdate"]).Date;
                    dtjoin.Value = Convert.ToDateTime(dt.Rows[0]["datejoined"]).Date;
                    
                    pbox1.ImageLocation = dt.Rows[0]["picture"].ToString();

                    filename = dt.Rows[0]["picture"].ToString().Replace(@"\", @"\\");
                }
                conn.Close();
            }
            catch (Exception ee)
            {
                errorMessage(ee.Message);
                conn.Close();
            }            
        }        

        public void reloadeditmember(int memberid)
        {
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT firstname, lastname, civilstatus, birthdate, relationship, remarks, occupation, eduattain, monthlyincome FROM member WHERE memberid = " + memberid, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm); DataTable dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    txtmemfirstname.Text = dt.Rows[0]["firstname"].ToString();
                    txtmemlastname.Text = dt.Rows[0]["lastname"].ToString();
                    txtmemocc.Text = dt.Rows[0]["occupation"].ToString();
                    txtmemrelationship.Text = dt.Rows[0]["relationship"].ToString();

                    rtremarks.Text = dt.Rows[0]["remarks"].ToString();
                    cbxmemstatus.Text = dt.Rows[0]["civilstatus"].ToString();

                    cbxmemeduattain.Text = dt.Rows[0]["eduattain"].ToString();
                    txtmemincome.Text = dt.Rows[0]["monthlyincome"].ToString();

                    dtpmembirth.Value = Convert.ToDateTime(dt.Rows[0]["birthdate"]).Date;
                }
                conn.Close();
            }
            catch (Exception ee)
            {
                errorMessage(ee.Message);
                conn.Close();
            }
        }

       
        public void reloadeditclass(int classid)
        {
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT section, adviser, yearlevel FROM edclass WHERE classeid = " + classid, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                adp.Fill(dt);
                
                if (dt.Rows.Count > 0)
                {
                    
                    section = dt.Rows[0]["section"].ToString();
                    adviser = dt.Rows[0]["adviser"].ToString();
                    yearlvl = dt.Rows[0]["yearlevel"].ToString();

                    edclass(classid, yearlvl, section, adviser);

                }
                conn.Close();
            }
            catch (Exception ee)
            {
                errorMessage(ee.Message);
                conn.Close();
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
                    btnedithealth.Enabled = true;
                    addhrecord.Enabled = false;
                }
                else
                {
                    errorMessage("There are currently no existing health records for this case study.");
                    btnedithealth.Enabled = false;
                    addhrecord.Enabled = true;
                }
                conn.Close();
            }

            catch (Exception ee)
            {
                errorMessage(ee.Message);
            }


        }

        public void reload(int id)
        {
           
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT * FROM casestudyprofile WHERE caseid = " + id, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    lblcasename.Text = dt.Rows[0]["firstname"].ToString() + " " + dt.Rows[0]["lastname"].ToString(); //Basic Info
                    lblcaseaddress.Text = dt.Rows[0]["address"].ToString();
                    lblcaseprogram.Text = dt.Rows[0]["program"].ToString();
                    lblcasestatus.Text = dt.Rows[0]["civilstatus"].ToString();
                    lblcaseage.Text = dt.Rows[0]["age"].ToString() + "years old";

                    if (dt.Rows[0]["religion"].ToString() != null)
                    {
                        lblReligion.Text = "RELIGION";
                        lblrel.Text = dt.Rows[0]["religion"].ToString();
                    }
                    else if(dt.Rows[0]["ip"].ToString() != null)
                    {
                        lblReligion.Text = "IP";
                        lblrel.Text = dt.Rows[0]["ip"].ToString();
                    }

                    lblrel.Text = dt.Rows[0]["religion"].ToString();

                    lbldate.Text = Convert.ToDateTime(dt.Rows[0]["birthdate"]).ToString("MMMM dd, yyyy");
                    lbljoined.Text = Convert.ToDateTime(dt.Rows[0]["dateJoined"]).ToString("MMMM dd, yyyy");

                    if(dt.Rows[0]["picture"].ToString() != null) pbox2.ImageLocation = dt.Rows[0]["picture"].ToString();
                }

                //-------------------------------------------------------------------------------------------------------------
                // Educational Background

                existsed(id);

                //-------------------------------------------------------------------------------------------------------------
                // Health

                existshealth(id);

                //-------------------------------------------------------------------------------------------------------------
                // Consultation

                existscon(id);

                //-------------------------------------------------------------------------------------------------------------
                // Family + Members

                existsfam(id);

                //-------------------------------------------------------------------------------------------------------------
                // Incident

                existsincid(id);


                
                btnArchive.Visible = true;
                btncancelarchive.Visible = false;
                
                conn.Close();
            }
            catch (Exception ee)
            {
                errorMessage(ee.Message);
                conn.Close();
            }
        }

        public void reloadcon(int id)
        {
            try
            {
                
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT cid, interviewdate, interviewer FROM consultation WHERE caseid = " + id + " ORDER BY interviewdate", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    errorMessage("There are no current consultation records for this case study.");
                }
                else
                {
                    dtgcon.DataSource = dt;
                    dtgcon.Columns["cid"].Visible = false;
                }
                
                conn.Close();
            }
            catch (Exception ee)
            {
                errorMessage(ee.Message);
            }
        }

        public void reloaded(int id)
        {
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT eid, school, level FROM education WHERE caseid = " + id + " ORDER BY school", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm); DataTable dt = new DataTable();

                adp.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(-1, "No entries.", null);
                    empty = true;

                    dtgeducation.DataSource = dt;
                }
                else
                {
                    empty = false;

                    dtgeducation.DataSource = dt;

                    // For ID purposes (hidden from user)            
                    

                    dtgeducation.Columns["school"].HeaderText = "SCHOOL";
                    dtgeducation.Columns["level"].HeaderText = "LEVEL";

                    // 486 WIDTH
                    dtgeducation.Columns["school"].Width = 200;
                    dtgeducation.Columns["level"].Width = 100;
                    dtgeducation.Columns["school"].HeaderCell.Style.Padding = dtgeducation.Columns["school"].DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);

                    DataGridViewImageColumn EditColumn = new DataGridViewImageColumn();
                    //EditColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
                    EditColumn.Image = Properties.Resources.editrmm;
                    EditColumn.Name = "EDIT";
                    EditColumn.Width = 93;
                    EditColumn.DataPropertyName = "EDIT";

                    DataGridViewImageColumn AddColumn = new DataGridViewImageColumn();
                    //AddColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
                    AddColumn.Image = Properties.Resources.add;
                    AddColumn.Name = "ADD";
                    AddColumn.Width = 93;
                    AddColumn.DataPropertyName = "ADD";

                    if (dtgeducation.Columns["EDIT"] == null && archivemode == 0)
                    {
                        dtgeducation.Columns.Add(EditColumn);
                        dtgeducation.Columns["EDIT"].ReadOnly = false;
                    }
                    if (dtgeducation.Columns["ADD"] == null && archivemode == 0)
                    {
                        dtgeducation.Columns.Add(AddColumn);
                        dtgeducation.Columns["ADD"].ReadOnly = false;
                    }
                }
                dtgeducation.Columns["eid"].Visible = false;

                conn.Close();
            }
            catch (Exception ee)
            {                
                errorMessage(ee.Message);
                conn.Close();
            }
        }

        public void reloadedclass(int eid)
        {
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT classeid, section, adviser, yearlevel FROM edclass WHERE eid = " + eid + " ORDER BY yearlevel", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm); DataTable dt = new DataTable();

                adp.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(-1, "No entries.", null, null);
                    empty = true;
                }
                else empty = false;
                
                dtgedclass.DataSource = dt;

                // For ID purposes (hidden from user)            
                dtgedclass.Columns["classeid"].Visible = false;

                dtgedclass.Columns["section"].HeaderText = "SECTION";
                dtgedclass.Columns["adviser"].HeaderText = "ADVISER";
                dtgedclass.Columns["yearlevel"].HeaderText = "YEAR LEVEL";

                // 555 WIDTH
                dtgedclass.Columns["section"].Width = dtgedclass.Columns[2].Width = 227;
                dtgedclass.Columns["adviser"].Width = 50;
                dtgedclass.Columns["yearlevel"].HeaderCell.Style.Padding = dtgedclass.Columns[1].DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);

                DataGridViewImageColumn EditColumn = new DataGridViewImageColumn();
                //EditColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
                EditColumn.Image = Properties.Resources.editrmm;
                EditColumn.Name = "EDIT";
                EditColumn.Width = 50;
                EditColumn.DataPropertyName = "EDIT";

                if (dtgedclass.Columns["EDIT"] == null && archivemode == 0)
                {
                    dtgedclass.Columns.Add(EditColumn);
                    dtgedclass.Columns["EDIT"].ReadOnly = false;
                }
                
                conn.Close();
            }
            catch (Exception ee)
            {
                errorMessage(ee.Message);
                conn.Close();
            }
        }
        
        public void reloadincid(int id)
        {
            string[] involve;
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT incidid, type, incdate, witnesses FROM incident WHERE caseid = " + id + " ORDER BY incdate", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable(); adp.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(-1, "No entries.", null, null);
                    empty = true;
                }
                else empty = false;
                
                dtincid.DataSource = dt;
                dtincid.Columns["incidid"].Visible = false;
                dtincid.Columns["witnesses"].Visible = false;

                involve = dt.Rows[0]["witnesses"].ToString().Split('\n');

                foreach (string words in involve)
                {
                    rtwitnesses.AppendText(words + "\n");
                }

                conn.Close();
            }
            catch (Exception ee)
            {
                errorMessage(ee.Message);
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
                    errorMessage("There are no current checkup records for this case study.");
                }

                else

                {
                    dtghealth.DataSource = dt;
                    dtghealth.Columns["checkid"].Visible = false;
                    //dtincid.Columns[1].Visible = false;
                    //hid = int.Parse(dt.Rows[0]["health.hid"].ToString());
                    //MessageBox.Show(hid.ToString());
                }
                conn.Close();
            }
            catch (Exception ee)
            {
                errorMessage(ee.Message);
            }
        }

        public void reloadfam(int id)
        {

            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand("SELECT familyid, famtype, famposition, CASE famcurrent WHEN 0 THEN 'Former' ELSE 'Current' END AS 'FAMILY STATUS' FROM family WHERE caseid = " + id, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm); DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(-1, "No entries.", null);
                    empty = true;
                }

                 

                dtgfamily.DataSource = dt;

                // UI Modifications
                dtgfamily.Columns["familyid"].Visible = false;
                dtgfamily.Columns[1].HeaderText = "FAMILY TYPE";
                dtgfamily.Columns[2].HeaderText = "FAMILY POSITION";

                
                conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
                //MessageBox.Show(ee.ToString() + "reloadfam");                
            }
        }

        public void reloadfamtype(int x)
        {
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand("SELECT famtype, CASE famcurrent WHEN 0 THEN 'Former' ELSE 'Current' END AS 'status' FROM family WHERE familyid = " + x, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm); DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    lblfamilytype.Text = dt.Rows[0]["famtype"].ToString();
                    lblfamstatus.Text = dt.Rows[0]["status"].ToString();

                    /*if (int.Parse(dt.Rows[0]["famtype"].ToString()) == 1)
                    {
                        lblfamstatus.Text = "CURRENT FAMILY";
                    }

                    else
                    {
                        lblfamstatus.Text = "FORMER FAMILY";
                    }*/

                }

                conn.Close();

                reloadmem(x);
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
            }
        }

        public void reloadmem(int y)
        {
            try
            {
                conn.Open();
                
                MySqlCommand comm = new MySqlCommand("SELECT memberid, firstname, lastname, civilstatus, age, birthdate, relationship, occupation, eduattain, monthlyincome, remarks FROM member WHERE familyid = " + y, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm); DataTable dt = new DataTable();
                adp.Fill(dt);                

                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(-1, "No entries.", null, null, null, null, null, null, null, null, null);
                    empty = true;



                    dtgmembers.DataSource = dt;

                    dtgmembers.Columns["memberid"].Visible = false;
                }

                else
                {
                    dtgmembers.DataSource = dt;

                    // UI Modifications
                    dtgmembers.Columns["firstname"].HeaderText = "FIRST NAME";
                    dtgmembers.Columns["lastname"].HeaderText = "LAST NAME";
                    dtgmembers.Columns["civilstatus"].HeaderText = "CIVIL STATUS";
                    dtgmembers.Columns["age"].HeaderText = "AGE";
                    dtgmembers.Columns["birthdate"].HeaderText = "BIRTHDATE";
                    dtgmembers.Columns["relationship"].HeaderText = "RELATIONSHIP";
                    dtgmembers.Columns["occupation"].HeaderText = "OCCUPATION";
                    dtgmembers.Columns["eduattain"].HeaderText = "EDUCATIONAL ATTAINMENT";
                    dtgmembers.Columns["monthlyincome"].HeaderText = "MONTHLY INCOME";
                    dtgmembers.Columns["remarks"].HeaderText = "REMARKS";

                    // WIDTH
                    dtgmembers.Columns["firstname"].Width = dtgmembers.Columns["lastname"].Width = dtgmembers.Columns["relationship"].Width = dtgmembers.Columns["eduattain"].Width = 150;
                    dtgmembers.Columns["civilstatus"].Width = 100;
                    dtgmembers.Columns["remarks"].Width = 300;

                    // For ID purposes (hidden from user)     

                    DataGridViewImageColumn dc = new DataGridViewImageColumn();
                    dc.ImageLayout = DataGridViewImageCellLayout.Stretch;
                    dc.Image = Properties.Resources.editrmm;
                    dc.Name = "CHECK";
                    dc.Visible = true;

                    if (dtgmembers.Columns["CHECK"] == null)
                    {

                        dtgmembers.Columns.Add(dc);

                    }

                    dtgmembers.Columns["memberid"].Visible = false;

                    
                        comm = new MySqlCommand("SELECT COUNT(memberid) FROM member WHERE familyid = " + y, conn);
                        adp = new MySqlDataAdapter(comm); dt = new DataTable(); adp.Fill(dt);

                        lblnummembers.Text = dt.Rows[0]["count(memberid)"].ToString();

                        
                    
                }
                
                

                conn.Close();           
            }
            catch (Exception ee)
            {
                errorMessage(ee.Message);
                conn.Close();
            }
        }

        #endregion

        #region cellclicks
        private void dtgcs_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (!(senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn))
            {
                try
                {
                    id = int.Parse(dtgcs.Rows[e.RowIndex].Cells["caseid"].Value.ToString());

                    tabChild.SelectedTab = sixteen;
                    tabCase.SelectedTab = tabInfo;

                    btnArchive.Visible = true;
                    btncancelarchive.Visible = false;
                    archivemode = 0;

                    reload(id);


                    showdem();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.ToString());
                }
            }
            
        }

        private void dtgfamily_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                famid = int.Parse(dtgfamily.Rows[e.RowIndex].Cells[0].Value.ToString());

                tabChild.SelectedTab = fourth;

                reloadfamtype(famid);
            }
            catch (Exception ee)
            {
                errorMessage(ee.ToString());
            }
        }



        private void dtghealth_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int checkid = int.Parse(dtghealth.Rows[e.RowIndex].Cells[0].Value.ToString());
                //MessageBox.Show(checkid.ToString());
                tabChild.SelectedTab = nineteen;

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
                errorMessage(ee.Message);
            }
        }

        private void dtgcon_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                conn.Open();

                int cid = int.Parse(dtgcon.Rows[e.RowIndex].Cells["cid"].Value.ToString());

                MySqlCommand comm = new MySqlCommand("SELECT condes, interviewdate, interviewer FROM consultation WHERE cid = " + cid, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm); DataTable dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    richboxrecords.Text = dt.Rows[0]["condes"].ToString();
                    lbldatecon.Text = Convert.ToDateTime(dt.Rows[0]["interviewdate"]).ToString("MMMM dd, yyyy");
                    lblintcon.Text = dt.Rows[0]["interviewer"].ToString();
                }
                tabChild.SelectedTab = seventeen;
                conn.Close();
            }
            catch (Exception ee)
            {
                errorMessage(ee.Message);
            }
        }

        private void dtincid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                tabChild.SelectedTab = thirteen;
                int incid = int.Parse(dtincid.Rows[e.RowIndex].Cells[0].Value.ToString());
                          
                conn.Open();
                MySqlCommand comm = new MySqlCommand("SELECT type, incdate, venue, description, action FROM incident WHERE incidid = " + incid, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm); DataTable dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    inctype.Text = dt.Rows[0]["type"].ToString();
                    incidlocation.Text = dt.Rows[0]["venue"].ToString();
                    repinciddesc.Text = dt.Rows[0]["description"].ToString();
                    repincidaction.Text = dt.Rows[0]["action"].ToString();
                    lbldateincid.Text = Convert.ToDateTime(dt.Rows[0]["incdate"]).ToString("MMMM, dd, yyyy");                
                
                }
                tabChild.SelectedTab = thirteen;
                conn.Close();
            }
            catch (Exception ee)
            {
                //MessageBox.Show("" + ee);
                errorMessage(ee.Message);
                conn.Close();
            }
        }

        private void dtgeducation_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            eid = int.Parse(dtgeducation.Rows[e.RowIndex].Cells["eid"].Value.ToString());
            if (!(senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn)) reloadedclass(eid);
        }

        private void dtgeducation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            eid = int.Parse(dtgeducation.Rows[e.RowIndex].Cells["eid"].Value.ToString());
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.RowIndex >= 0)
            {
                if (senderGrid.Columns[e.ColumnIndex] == senderGrid.Columns["Edit"])
                {
                    tabCase.SelectedTab = tabNewChild;
                    tabaddchild.SelectedTab = tabNewEdu;
                    
                    lbladdeditprofile.Text = "Edit Education Info";

                    btnadded.Text = "ADD CHANGES";

                    //MessageBox.Show(dtgeducation.Rows[e.RowIndex].Cells[0].Value.ToString());
                    reloadediteducation(eid);
                }
                else
                {
                    classeid = int.Parse(dtgeducation.Rows[e.RowIndex].Cells["eid"].Value.ToString());

                    yearlvl = dtgeducation.Rows[e.RowIndex].Cells[2].Value.ToString();

                    edclass(classeid, yearlvl);
                }
            }
        }

        private void dtgedclass_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            //MessageBox.Show(dtgedclass.Rows[e.RowIndex].Cells[0].Value.ToString());
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.RowIndex >= 0)
            {
               try
                {                   
                    int edclassid = int.Parse(dtgedclass.Rows[e.RowIndex].Cells["classeid"].Value.ToString());
                    reloadeditclass(edclassid);
                }
                catch(Exception ee)
                {
                    errorMessage(ee.Message);
                    conn.Close();
                }
            }
        }

        private void dtfamOverview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.RowIndex >= 0)
            {
                tabCase.SelectedTab = tabNewChild;
                tabaddchild.SelectedTab = tabNewMember;

                lbladdeditprofile.Text = "Edit Family Members Info";
                btnaddmember.Text = "ADD CHANGES";

                memberid = int.Parse(dtgmembers.Rows[e.RowIndex].Cells["memberid"].Value.ToString());

                reloadeditmember(memberid);
            }
        }

        private void dtgarchive_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (!(senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn))
            {
                try
                {
                    archiveid = int.Parse(dtgarchive.Rows[e.RowIndex].Cells["caseid"].Value.ToString());

                    tabChild.SelectedTab = sixteen;
                    tabCase.SelectedTab = tabInfo;

                    archivemode = 1;
                    reload(archiveid);

                    //existshealth(archiveid);

                    hidedem();
                }
                catch (Exception ee)
                {
                    errorMessage(ee.Message);
                    conn.Close();
                }
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

            //cbxprogram.SelectedIndex = cbxcasestatus.SelectedIndex = -1;
            dtbirth.Value = dtjoin.Value = DateTime.Now.Date;
        }

        public void reset2()
        {
            txtedname.Clear();
            cbxedlvl.SelectedIndex = cbxtype.SelectedIndex = -1;
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

            cbxmemstatus.SelectedIndex = -1;

            rtremarks.Clear();

            dtpmembirth.Value = DateTime.Now.Date;
        }

        public void reset9()
        {

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
            string school, level;

            try
            {
                MySqlCommand comm = new MySqlCommand("SELECT school, level, dateadded FROM education WHERE caseid = " + id, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                //MessageBox.Show(dt.Rows[0]["school"].ToString());
                //MessageBox.Show(dt.Rows.Count.ToString());

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows.Count == 1)
                    {
                        lbledschool.Text = dt.Rows[0]["school"].ToString();
                        lbledlvl.Text = dt.Rows[0]["level"].ToString();
                       
                    }

                    else
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            for (int j = 1; j < dt.Rows.Count; j++)
                            {
                           
                                if (Convert.ToDateTime(dt.Rows[i]["dateadded"]).Date > Convert.ToDateTime(dt.Rows[j]["dateadded"]).Date)
                                {
                                    school = dt.Rows[i]["school"].ToString();
                                    level = dt.Rows[i]["level"].ToString();

                                }

                                else
                                {
                                    school = dt.Rows[j]["school"].ToString();
                                    level = dt.Rows[j]["level"].ToString();
                                }

                                lbledlvl.Text = level;
                                lbledschool.Text = school;

                            }

                        }
                    }
                   
                }

                else
                {
                    lbledlvl.Text = "";
                    lbledschool.Text = "";
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
            }
        }

        

        public void existsincid(int id)
        {
            try
            {
                MySqlCommand comm = new MySqlCommand("SELECT incdate FROM incident WHERE caseid = " + id, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                DateTime incdate;

                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows.Count == 1)
                    {
                        incdate = Convert.ToDateTime(dt.Rows[0]["incdate"]).Date;
                        lblincdatedis.Text = incdate.ToString("MMMM dd, yyyy");
                    }

                    else
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            for (int j = 1; j < dt.Rows.Count; j++)
                            {
                                if (dt.Rows.Count == 1)
                                {
                                    incdate = Convert.ToDateTime(dt.Rows[0]["incdate"]).Date;

                                    

                                }

                                else
                                {
                                    if (Convert.ToDateTime(dt.Rows[i]["incdate"]).Date > Convert.ToDateTime(dt.Rows[j]["incdate"]).Date)
                                    {
                                        incdate = Convert.ToDateTime(dt.Rows[i]["incdate"]).Date;

                                    }

                                    else
                                    {
                                        incdate = Convert.ToDateTime(dt.Rows[j]["incdate"]).Date;
                                    }

                                    
                                }

                                lblincdatedis.Text = incdate.ToString("MMMM dd, yyyy");
                            }

                        }
                    }


                }

                else
                {
                    lblincdatedis.Text = "";
                }
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
            }
        } 

        public void existscon(int id)
        {
            try
            {
                MySqlCommand comm = new MySqlCommand("SELECT interviewdate FROM consultation WHERE caseid = " + id, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                DateTime consuldate;

                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows.Count == 1)
                    {
                        consuldate = Convert.ToDateTime(dt.Rows[0]["interviewdate"]).Date;
                        lblconsuldate.Text = consuldate.ToString("MMMM dd, yyyy");
                    }

                    else
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            for (int j = 1; j < dt.Rows.Count; j++)
                            {

                                if (Convert.ToDateTime(dt.Rows[i]["interviewdate"]).Date > Convert.ToDateTime(dt.Rows[j]["interviewdate"]).Date)
                                {
                                    consuldate = Convert.ToDateTime(dt.Rows[i]["interviewdate"]).Date;

                                }

                                else
                                {
                                    consuldate = Convert.ToDateTime(dt.Rows[j]["interviewdate"]).Date;
                                }

                                lblconsuldate.Text = consuldate.ToString("MMMM dd, yyyy");

                            }

                        }
                    }
                    

                }

                else
                {
                    lblconsuldate.Text = "";
                }
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
            }

        }

        public void existsfam(int id)
        {
            
            try
            {
                //MessageBox.Show(id.ToString());
                MySqlCommand comm = new MySqlCommand("SELECT familyid, famtype FROM family WHERE caseid = " + id + " AND famcurrent = " + 1, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    
                        lblfamtypedis.Text = dt.Rows[0]["famtype"].ToString();
                        btnfamtype.Text = "CHANGE FAMILY TYPE";

                        famid = int.Parse(dt.Rows[0]["familyid"].ToString());

                    existsmem(famid);
                }

                else
                {
                    lblfamtypedis.Text = "";
                    lblmemcountdis.Text = "";
                }
                
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
            }
        }

        public void existsmem(int x)
        {
            MySqlCommand comm = new MySqlCommand("SELECT COUNT(memberid) FROM member JOIN family ON member.familyid = family.familyid WHERE member.familyid = " + x, conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(comm);
            DataTable dt = new DataTable();

            adp.Fill(dt);

            lblmemcountdis.Text = dt.Rows[0]["COUNT(memberid)"].ToString();
        }

        public void existshealth(int id)
        {
            try
            {
                MySqlCommand comm = new MySqlCommand("SELECT bloodtype, height, weight FROM health WHERE caseid = " + id, conn); //Heatlh & Checkup
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    lblblood.Text = dt.Rows[0]["bloodtype"].ToString();
                    lblH.Text = dt.Rows[0]["height"].ToString();
                    lblW.Text = dt.Rows[0]["weight"].ToString();

                }

                else
                {
                    lblblood.Text = "";
                    lblH.Text = "";
                    lblW.Text = "";
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
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
                        System.Drawing.Image backgroundImage = global::BalayPasilungan.Properties.Resources.tsLineHover;
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
            tabChild.SelectedTab = sixteen;

            resetall();
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
            cbxprogram.SelectedIndex = cbCivilStatus.SelectedIndex = 0;
            lbladdeditprofile.Text = "NEW CASE PROFILE";
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
            tabaddchild.SelectedTab = tabNewMember;
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
            tabaddchild.SelectedTab = tabNewMember;
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
            tabaddchild.SelectedTab = tabNewIncid;
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
            tabaddchild.SelectedTab = tabNewIncid;

            if (btnaddincidrecord.Text == "ADD")
            {                
               
                lbladdeditprofile.Text = "New Incident Record";
            }
            else lbladdeditprofile.Text = "Update Incident Record";            
            mode = 0;
        }

        private void btnNewConfirm_Click(object sender, EventArgs e)
        {
            resetNewChildTS();
            
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
       /* private void kinder_CheckedChanged(object sender, EventArgs e)
        {
            if (kinder.Checked) { panelKinder.Enabled = true; lvlKinder.Visible = true; cbxedlvl.Visible = true; }
            else { panelKinder.Enabled = false; lvlKinder.Visible = false; cbxedlvl.Visible = false; }
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
        }*/
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
                errorMessage(ee.Message);
                conn.Close();
            }
        }

 

        public void getincidid(int id)
        {
            try
            {
                

                MySqlCommand comm = new MySqlCommand("SELECT incidid FROM incident WHERE caseid = " + id, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                incidid = int.Parse(dt.Rows[0]["incidid"].ToString());
                
            }


            catch (Exception ee)
            {
                errorMessage(ee.Message);
                conn.Close();
            }
        }
        #endregion
        
        #region add/edit buttons

        private void btnaddeditcase_Click(object sender, EventArgs e)
        {
            if (btnaddeditcase.Text == "ADD NEW PROFILE") addprofile();            
            else editprofile();
        }

        private void btnaddarchive_Click(object sender, EventArgs e)
        {
            int archiveid;

            confirmMessage("You are about to discharge the following case studies. Discharging a case study means the case study is no longer being" +
                "supported by the shelter either of graduating from or voluntarily leaving the program, and therefore all information regarding the case studies will" +
                "not be updated anymore. \nContinue?");
            if (confirmed)
            {
                try
                {
                    conn.Open();
                    foreach (DataGridViewRow row in dtgcs.Rows)
                    {
                        archiveid = int.Parse(row.Cells["caseid"].Value.ToString());
                        if (Convert.ToBoolean(row.Cells["Discharge"].Value))
                        {
                            MySqlCommand comm = new MySqlCommand("UPDATE casestudyprofile SET profilestatus = " + 0 + " WHERE caseid = " + archiveid, conn);
                            comm.ExecuteNonQuery();
                        }
                    }
                    conn.Close();
                    refresh();
                    refresh2();
                }
                catch (Exception ee)
                {
                    errorMessage(ee.Message);
                    conn.Close();
                }
            }

            else
            {
                refresh();
                refresh2();
            }
            btncancelarchive.Visible = false;
            btnaddarchive.Visible = false;
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
               errorMessage("Please fill out empty fields.");
            }

            else
            {
                try
                {

                    conn.Open();                    
                    MySqlCommand comm = new MySqlCommand("INSERT INTO checkup(hid, checkupdetails, checkupdate, checkuplocation) VALUES('" + hid + "', '" + details + "', '" + dtpcheck.Value.Date.ToString("yyyyMMdd") + "', '" + location + "')", conn);                    
                    comm.ExecuteNonQuery();
                    successMessage("New checkup record has been added successfully!");
                    conn.Close();

                    reloadhealth(id);
                    tabChild.SelectedTab = fifteen;                   
                    reset6();
                }
                catch (Exception ee)
                {
                    errorMessage(ee.Message);
                    conn.Close();
                }
            }
        }

        private void btnadded_Click(object sender, EventArgs e)
        {
            if (btnadded.Text == "ADD")
            {
                addeducation();
            }

            else
            {

                editeducation();

            }
            
        }

        private void btnaddcon_Click(object sender, EventArgs e)
        {
            if (btnaddcon.Text == "ADD")
            {
                addcon();
            }

            else
            {
                editcon();
            }
        }

        private void btnaddmember_Click(object sender, EventArgs e)
        {
            if (btnaddmember.Text == "ADD")
            {
                addmember(famid);
            }

            else
            {
                editmember(famid);
            }
            
        }

        private void btndeletefam_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dtgmembers.Rows)
            {
                DataGridViewCheckBoxCell chk = row.Cells[8] as DataGridViewCheckBoxCell;

                if (Convert.ToBoolean(chk.Value) == true)
                {
                    dtgmembers.Rows.Remove(row);
                }
            }
        }

        private void btnaddincidrecord_Click(object sender, EventArgs e)
        {
            if (btnaddincidrecord.Text == "ADD")
            {
                addincidrecord();
            }

            else
            {
                editincidrecord();
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

        private void btnbackfromfamrec_Click(object sender, EventArgs e)
        {
            tabChild.SelectedTab = sixteen;
        }


        private void btnbackfromarchive_Click(object sender, EventArgs e)
        {
            tabCase.SelectedTab = tabCases;
            refresh();
            refresh2();
        }
        
        private void btncancelarchive_Click(object sender, EventArgs e)
        {
            refresh();
           
            btncancelarchive.Visible = false;
            btnaddarchive.Visible = false;
        }
        
        private void btnbackcasestud_Click(object sender, EventArgs e)
        {
            tabCase.SelectedTab = tabCases;            
        }

        private void btncancelhealth_Click(object sender, EventArgs e)
        {
           tabCase.SelectedTab = tabInfo;
           reset3();
        }        

        private void btnbackfromcheck_Click(object sender, EventArgs e)
        {
            tabChild.SelectedTab = fifteen;
            reset6();
        }        

        private void btncanceled_Click(object sender, EventArgs e)
        {
            tabCase.SelectedTab = tabInfo;
            tabChild.SelectedTab = eighth;
            reset2();
        }

        private void btnedback_Click(object sender, EventArgs e)
        {
            tabChild.SelectedTab = sixteen;
        }

        private void btncancelcon_Click(object sender, EventArgs e)
        {
            tabChild.SelectedTab = sixteen;
        }

        private void btncancon_Click(object sender, EventArgs e)
        {
            tabChild.SelectedTab = ninth;
        }

        private void btncancelviewrec_Click(object sender, EventArgs e)
        {
            tabChild.SelectedTab = thirteen;
            richboxrecords.Clear();
        }

        private void btncanfamtype_Click(object sender, EventArgs e)
        {
            tabChild.SelectedTab = fourth;
        }

        private void btnbackfam_Click(object sender, EventArgs e)
        {
            tabChild.SelectedTab = sixteen;

            reset7();
        }

        private void btnbacktofamoverview_Click(object sender, EventArgs e)
        {
            tabCase.SelectedTab = tabInfo;
            tabChild.SelectedTab = fourth;

            reset8();
        }

        private void btnbackmainincid_Click(object sender, EventArgs e)
        {
            tabChild.SelectedTab = sixteen;
        }

        private void btnbackincidrec_Click(object sender, EventArgs e)
        {
            tabCase.SelectedTab = tabInfo;
            tabChild.SelectedTab = twelfth;

            reset5();
        }

        private void btnbackfrominc_Click(object sender, EventArgs e)
        {
            tabChild.SelectedTab = twelfth;
        }

        private void btnbackfrommember_Click(object sender, EventArgs e)
        {
            tabChild.SelectedTab = five;
        }

        #endregion

        #region into buttons

        private void btnhealth_Click(object sender, EventArgs e)
        {
            resetTS();
            healthTS.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            healthTS.ForeColor = Color.Black;
            healthTS.BackgroundImage = global::BalayPasilungan.Properties.Resources.tsLine;            
            tabChild.SelectedTab = fifteen;
            if (archivemode == 0)
            {
                reloadedithealth(id);
                reloadhealth(id);
            }
            else
            {
                reloadedithealth(archiveid);
                reloadhealth(archiveid);
            }            
        }

        private void btned_Click(object sender, EventArgs e)
        {
            resetTS();
            eduTS.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            eduTS.ForeColor = Color.Black;
            eduTS.BackgroundImage = global::BalayPasilungan.Properties.Resources.tsLine;            
            tabChild.SelectedTab = eighth;
            if (archivemode == 0) reloaded(id);
            else reloaded(archiveid);            
        }

        private void btncon_Click(object sender, EventArgs e)
        {
            resetTS();
            consulTS.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            consulTS.ForeColor = Color.Black;
            consulTS.BackgroundImage = global::BalayPasilungan.Properties.Resources.tsLine;            
            tabChild.SelectedTab = ninth;
            if (archivemode == 0) reloadcon(id);
            else  reloadcon(archiveid);            
        }

        private void btnfover_Click(object sender, EventArgs e)
        {
            resetTS();
            familyTS.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            familyTS.ForeColor = Color.Black;
            familyTS.BackgroundImage = global::BalayPasilungan.Properties.Resources.tsLine;            
            tabChild.SelectedTab = fourth;           
            if (archivemode == 0)
            {
                reloadfam(id);
                //reloadmem(id);
            }
            else
            {
                reloadfam(archiveid);
                //reloadmem(archiveid);
            }

            tabChild.SelectedTab = five;
        }

        private void btnincidview_Click(object sender, EventArgs e)
        {
            resetTS();
            othersTS.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            othersTS.ForeColor = Color.Black;
            othersTS.BackgroundImage = global::BalayPasilungan.Properties.Resources.tsLine;            
            tabChild.SelectedTab = twelfth;       
            if (archivemode == 0) reloadincid(id);            
            else reloadincid(archiveid);            
        }

        private void btnEditProfile_Click(object sender, EventArgs e)
        {
            tabCase.SelectedTab = tabNewChild;

            btnaddeditcase.Text = "ADD CHANGES";
            lbladdeditprofile.Text = "EDIT PROFILE";

            if (archivemode == 0) reloadeditinfo(id);            
            else reloadeditinfo(archiveid);
        }

        private void btngotocheckup_Click(object sender, EventArgs e)
        {
            tabChild.SelectedTab = fifteen;
            
        }

        private void btnedithealth_Click(object sender, EventArgs e)
        {
            btnaddhealth.Text = "ADD CHANGES";

            txtheight.Text = lblvheight.Text;
            txtweight.Text = lblvweight.Text;
            cbxbloodtype.Text = lblvblood.Text;
            rtxtall.Text = rviewall.Text;
            rtxtcondition.Text = rviewcondition.Text;

            tabCase.SelectedTab = tabNewChild;
            tabaddchild.SelectedTab = tabNewHealth;

            lbladdeditprofile.Text = "EDIT HEALTH BIOGRAPHY";
        }

        private void btngotohealth_Click(object sender, EventArgs e)
        {
            tabChild.SelectedTab = eighteen;
        }

        private void btnaddconrec_Click(object sender, EventArgs e)
        {
            tabCase.SelectedTab = tabNewChild;
            tabaddchild.SelectedTab = tabNewCon;
            lbladdeditprofile.Text = "NEW CONSULTATION RECORD";
        }

        private void btnArchive_Click(object sender, EventArgs e)
        {
            DataGridViewCheckBoxColumn lulz = new DataGridViewCheckBoxColumn();

            dtgcs.Columns[1].Width = dtgcs.Columns[2].Width = 233;
            dtgcs.Columns[3].Width = 234;

            lulz.Name = "Discharge";
            lulz.Width = 233;
            lulz.ReadOnly = false;
            

            if (dtgcs.Columns["Discharge"] == null)
            {
                //MessageBox.Show("aaaaaaa");                
                dtgcs.Columns.Add(lulz);
            }

            //dtgcs.Refresh();

            btnaddarchive.Visible = btncancelarchive.Visible = true;
        }

        private void btneditincid_Click(object sender, EventArgs e)
        {
            tabCase.SelectedTab = tabNewChild;
            tabaddchild.SelectedTab = tabNewIncid;
            btnaddincidrecord.Text = "ADD CHANGES";
            lbladdeditprofile.Text = "UPDATE INCIDENT RECORD";            
           

            reloadeditincid(id);
        }

        private void btnfamtype_Click(object sender, EventArgs e)
        {
           famtypecall(famid, btnfamtype.Text);            
        }
   
        private void btninvok_Click(object sender, EventArgs e)
        {
            tabaddchild.SelectedTab = tabNewIncid;
           
        }        

        
        private void cbIP_CheckedChanged(object sender, EventArgs e)
        {
            if (cbIP.Checked) panelIP.Visible = txtIP.Visible = true;
            else panelIP.Visible = txtIP.Visible = false;                
        }

        private void btnChildReport_Click(object sender, EventArgs e)
        {

        }

        private void multiChild_CheckedChanged(object sender, EventArgs e)
        {
            if (multiChild.Checked) dtgcs.MultiSelect = true;
            else dtgcs.MultiSelect = false;
        }        

        private void btnseearchive_Click(object sender, EventArgs e)
        {
            tabCase.SelectedTab = tabArchive;

            refresh();
            refresh2();
        }
        

        private void addhrecord_Click(object sender, EventArgs e)
        {
            tabCase.SelectedTab = tabNewChild;
            tabaddchild.SelectedTab = tabNewHealth;                
            lbladdeditprofile.Text = "Add Health Record";
            
            //reloadedithealth(id);
        }

        private void btnEduRepro_Click(object sender, EventArgs e)
        {
            tabChild.SelectedTab = eduReport1;
        }

        private void btnEduR_Click(object sender, EventArgs e)
        {
            if(((Button)sender).Name == "btnER1" || ((Button)sender).Name == "btnER2Back") tabChild.SelectedTab = eduReport2;
            else if (((Button)sender).Name == "btnER0") tabChild.SelectedTab = eduReport1;
            else if (((Button)sender).Name == "btnER2") tabChild.SelectedTab = eduReport3;
            else if (((Button)sender).Name == "btnERsubmit")
            {
                confirmMessage("Are you sure you want to submit this report?");
                if (confirmed)
                {
                    try
                    {
                        int term = 0, perf = 0;
                        if (rb1G.Checked) term = 1;
                        else if (rb2G.Checked) term = 2;
                        else if (rb3G.Checked) term = 3;
                        else if (rb4G.Checked) term = 4;

                        if (rbVG.Checked) perf = 5;
                        else if (rbG.Checked) perf = 4;
                        else if (rbS.Checked) perf = 3;
                        else if (rbM.Checked) perf = 2;
                        else if (rbU.Checked) perf = 1;

                        conn.Open();
                        MySqlCommand comm = new MySqlCommand("INSERT INTO education (caseID, level, section, adviser, term, performance, comments, strength, improvement, activities, awards, behavior, recommendations, dateAdded)"
                            + " VALUES(" + id  + ",'" + erLevel.Text + "', '" + erSection.Text + "', '" + erAdviser.Text + "', " + term + ", " + perf + ", '" + erComments.Text + "', '"
                            + erStrength.Text + "', '" + erImprove.Text + "', '" + erActs.Text + "', '" + erAwards.Text + "', '" + erBehavior.Text + "', '" + erRec.Text + "', '" + DateTime.Today.ToString("yyyy-MM-dd") + "')", conn);
                        comm.ExecuteNonQuery();
                        conn.Close();

                        successMessage("Educator's report has been added successfully!");
                        tabChild.SelectedTab = eighth;
                    }
                    catch(Exception ex)
                    {
                        errorMessage(ex.Message);
                    }
                }
            }
        }

        private void tabChild_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabChild.SelectedTab != sixteen) btnEditProfile.Visible = false;
            else btnEditProfile.Visible = true;
        }

        private void btnrestorecaseprof_Click(object sender, EventArgs e)
        {
            DataGridViewCheckBoxColumn lulz = new DataGridViewCheckBoxColumn();

            dtgarchive.Columns[1].Width = dtgarchive.Columns[2].Width = 233;
            

            lulz.Name = "Restore";
            lulz.Width = 233;
            lulz.ReadOnly = false;

            

            if (dtgarchive.Columns["Restore"] == null)
            {
                //MessageBox.Show("aaaaaaa");                
                dtgarchive.Columns.Add(lulz);
                dtgarchive.Columns["Restore"].Width = 234;
            }

            //dtgarchive.Refresh();

            btnrestore.Visible = btncancelrestore.Visible = true;
        }

        private void btncancelrestore_Click(object sender, EventArgs e)
        {
            refresh2();

            btnrestore.Visible = false;
            btncancelrestore.Visible = false;
        }

        private void btnrestore_Click(object sender, EventArgs e)
        {
            int restoreid;

            confirmMessage("Restoring case profile will bring back the authority to update selected case profile/s' information. This is usually done" +
                " when case profile voluntarily left the program but returned again. \nContinue?");
            if (confirmed)
            {
                try
                {
                    conn.Open();
                    foreach (DataGridViewRow row in dtgarchive.Rows)
                    {
                        restoreid = int.Parse(row.Cells["caseid"].Value.ToString());
                        if (Convert.ToBoolean(row.Cells["Restore"].Value))
                        {
                            MySqlCommand comm = new MySqlCommand("UPDATE casestudyprofile SET profilestatus = " + 1 + " WHERE caseid = " + restoreid, conn);
                            comm.ExecuteNonQuery();
                        }
                    }
                    conn.Close();
                    refresh();
                    refresh2();
                }
                catch (Exception ee)
                {
                    errorMessage(ee.Message);
                    conn.Close();
                }
            }

            else
            {
                refresh2();
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

        
        private void all_cond_Click(object sender, EventArgs e)
        {
            btnAllergy.BackColor = btnCondition.BackColor = btnDescription.BackColor = btnActions.BackColor = Color.Transparent;
            btnAllergy.ForeColor = btnCondition.ForeColor = btnDescription.ForeColor = btnActions.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            ((Button)sender).ForeColor = Color.Black;
            ((Button)sender).BackColor = Color.White;
            if (((Button)sender).Name == "btnAllergy") panelAll.Visible = true;
            else if (((Button)sender).Name == "btnCondition") panelAll.Visible = false;
            else if (((Button)sender).Name == "btnDescription") panelDesc.Visible = true;
            else if (((Button)sender).Name == "btnActions") panelDesc.Visible = false;
        }

        private void btnaddfam_Click(object sender, EventArgs e)
        {
            famtypecall(id, btnaddfam.Text);
        }

        private void bttnbackfromcheckrec_Click(object sender, EventArgs e)
        {
            tabChild.SelectedTab = fifteen;
        }

        private void btnbackfromhealth_Click(object sender, EventArgs e)
        {
            tabChild.SelectedTab = sixteen;
        }

        private void btnAddMem_Click(object sender, EventArgs e)
        {
            tabCase.SelectedTab = tabNewChild;
            tabaddchild.SelectedTab = tabNewMember;
            cbxmemstatus.SelectedIndex = cbxmemeduattain.SelectedIndex = 0;

            lbladdeditprofile.Text = "New Family Members Info";
            btnaddmember.Text = "ADD";
        }

        private void btnaddincid_Click(object sender, EventArgs e)
        {
            tabCase.SelectedTab = tabNewChild;
            tabaddchild.SelectedTab = tabNewIncid;
        }

        private void btnaddedclass_Click(object sender, EventArgs e)
        {
            tabCase.SelectedTab = tabNewChild;
            tabaddchild.SelectedTab = tabNewEdu;

            lbladdeditprofile.Text = "New Education Info";
            btnadded.Text = "ADD";
        }
        #endregion

        #region hide functions

        public void hidedem()
        {
            btnfamtype.Visible = false;
            btnAddMem.Visible = false;
            btnaddedclass.Visible = false;
            btnaddconrec.Visible = false;
            btnaddincid.Visible = false;
            btneditincid.Visible = false;
            btngotohealthreports.Visible = false;
            btnedithealth.Visible = false;
            btnaddcheckuprec.Visible = false;
            addhrecord.Visible = false;
            btngotohealth.Visible = false;
            btnEditProfile.Visible = false;

            btncancelcon.Text = "BACK";
            btnbackfromcheck.Text = "BACK";
        }

        public void showdem()
        {
            btnfamtype.Visible = true;
            btnAddMem.Visible = true;
            btnaddedclass.Visible = true;
            btnaddconrec.Visible = true;
            btnaddincid.Visible = true;
            btneditincid.Visible = true;
            btngotohealthreports.Visible = true;
            btnedithealth.Visible = true;
            btnaddcheckuprec.Visible = true;
            addhrecord.Visible = true;
            btngotohealth.Visible = true;
            btnEditProfile.Visible = true;

            btncancelcon.Text = "CANCEL";
            btnbackfromcheck.Text = "CANCEL";
        }

#endregion

        #region Textbox
        private void txtNew_Enter(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == "Name of member." || ((TextBox)sender).Text == "Relationship to donor." || ((TextBox)sender).Text == "Occupation of member." || ((TextBox)sender).Text == "Enter first name."
                || ((TextBox)sender).Text == "Enter last name." || ((TextBox)sender).Text == "Enter alias." || ((TextBox)sender).Text == "Enter place of birth." || ((TextBox)sender).Text == "Enter religion." || ((TextBox)sender).Text == "Specify IP."
                || ((TextBox)sender).Text == "Enter incident type." || ((TextBox)sender).Text == "Enter incident location.") ((TextBox)sender).Text = "";
            ((TextBox)sender).ForeColor = Color.Black;

            if (((TextBox)sender).Name == "txtfname")
            {
                lblFName.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                panelFName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                countFName.Visible = true;
            }
            else if (((TextBox)sender).Name == "txtlname")
            {
                lblLName.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                panelLName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                countLName.Visible = true;
            }
            else if (((TextBox)sender).Name == "txtedname")
            {
                lblED.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                panelED.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                countED.Visible = true;
            }
            else if (((TextBox)sender).Name == "txtheight")
            {
                lblHeight.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                     
            }
            else if (((TextBox)sender).Name == "txtweight")
            {
                lblWeight.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                
            }
            else if (((TextBox)sender).Name == "txtAlias")
            {
                lblAlias.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                panelAlias.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                countAlias.Visible = true;
            }
            else if (((TextBox)sender).Name == "txtBirthplace")
            {
                lblBirthPlace.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                panelBirthPlace.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                countBirthPlace.Visible = true;
            }
            else if (((TextBox)sender).Name == "txtReligion")
            {
                lblReligion.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                panelReligion.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                countReligion.Visible = true;
            }
            else if (((TextBox)sender).Name == "txtIP")
            {
                cbIP.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                panelIP.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                countIP.Visible = true;
            }
            else if (((TextBox)sender).Name == "txtmemfirstname")
            {
                lblMFName.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                panelMFName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                countMFName.Visible = true;
            }
            else if (((TextBox)sender).Name == "txtmemlastname")
            {
                lblMLName.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                panelMLName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                countMLName.Visible = true;
            }
            else if (((TextBox)sender).Name == "txtmemrelationship")
            {
                lblRelationship.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                panelRelationship.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                countRelationship.Visible = true;
            }
            else if (((TextBox)sender).Name == "txtmemocc")
            {
                lblOccupation.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                panelOccupation.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                countOccupation.Visible = true;
            }
            else if (((TextBox)sender).Name == "txtmemincome")
            {
                lblMI.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                panelMI.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;                
            }
            else if (((TextBox)sender).Name == "txttypeincid")
            {
                lblIncType.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                panelIncType.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                countIncType.Visible = true;
            }
            else if (((TextBox)sender).Name == "txtincidlocation")
            {
                lblIncLocation.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                panelIncLocation.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                countIncLocation.Visible = true;
            }
        }

        private void txtNewCount_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Name == "txtfname") countFName.Text = ((TextBox)sender).TextLength + "/100";
            else if (((TextBox)sender).Name == "txtlname") countLName.Text = ((TextBox)sender).TextLength + "/100";
            else if (((TextBox)sender).Name == "txtedname") countED.Text = ((TextBox)sender).TextLength + "/100";
            else if (((TextBox)sender).Name == "txtedname") countED.Text = ((TextBox)sender).TextLength + "/100";
            else if (((TextBox)sender).Name == "txtmemfirstname") countMFName.Text = ((TextBox)sender).TextLength + "/250";
            else if (((TextBox)sender).Name == "txtmemlastname") countMLName.Text = ((TextBox)sender).TextLength + "/250";
            else if (((TextBox)sender).Name == "txtAlias") countAlias.Text = ((TextBox)sender).TextLength + "/50";
            else if (((TextBox)sender).Name == "txtBirthplace") countBirthPlace.Text = ((TextBox)sender).TextLength + "/100";
            else if (((TextBox)sender).Name == "txtReligion") countBirthPlace.Text = ((TextBox)sender).TextLength + "/50";
            else if (((TextBox)sender).Name == "txtIP") countIP.Text = ((TextBox)sender).TextLength + "/50";
            else if (((TextBox)sender).Name == "txtmemrelationship") countRelationship.Text = ((TextBox)sender).TextLength + "/45";
            else if (((TextBox)sender).Name == "txtmemocc") countOccupation.Text = ((TextBox)sender).TextLength + "/100";
            else if (((TextBox)sender).Name == "txttypeincid") countIncType.Text = ((TextBox)sender).TextLength + "/45";
            else if (((TextBox)sender).Name == "txtincidlocation") countIncLocation.Text = ((TextBox)sender).TextLength + "/250";
        }

        private void txtNew_Leave(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == "")
            {
                if (((TextBox)sender).Name == "txtfname") ((TextBox)sender).Text = "Enter first name.";
                else if (((TextBox)sender).Name == "txtlname") ((TextBox)sender).Text = "Enter last name.";
                else if (((TextBox)sender).Name == "txtAlias") ((TextBox)sender).Text = "Enter alias.";
                else if (((TextBox)sender).Name == "txtBirthplace") ((TextBox)sender).Text = "Enter place of birth.";
                else if (((TextBox)sender).Name == "txtReligion") ((TextBox)sender).Text = "Enter religion.";
                else if (((TextBox)sender).Name == "txtIP") ((TextBox)sender).Text = "Specify IP.";
                else if (((TextBox)sender).Name == "txtedname") ((TextBox)sender).Text = "Enter name of school.";
                else if (((TextBox)sender).Name == "txtmemfirstname" || ((TextBox)sender).Name == "txtmemlastname") ((TextBox)sender).Text = "Name of member.";
                else if (((TextBox)sender).Name == "txtmemrelationship") ((TextBox)sender).Text = "Relationship to donor.";
                else if (((TextBox)sender).Name == "txtmemocc") ((TextBox)sender).Text = "Occupation of member.";
                else if (((TextBox)sender).Name == "txttypeincid") ((TextBox)sender).Text = "Enter incident type.";
                else if (((TextBox)sender).Name == "txtincidlocation") ((TextBox)sender).Text = "Enter incident location.";
            }
            ((TextBox)sender).ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);

            if (((TextBox)sender).Name == "txtfname")
            {
                lblFName.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                panelFName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
                countFName.Visible = false;
            }
            else if (((TextBox)sender).Name == "txtlname")
            {
                lblLName.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                panelLName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
                countLName.Visible = false;
            }
            else if (((TextBox)sender).Name == "txtedname")
            {
                lblED.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                panelED.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                countED.Visible = true;
            }
            else if (((TextBox)sender).Name == "txtheight")
            {
                lblHeight.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                
            }
            else if (((TextBox)sender).Name == "txtweight")
            {
                lblWeight.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                
            }
            else if (((TextBox)sender).Name == "txtmemfirstname")
            {
                lblMFName.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                panelMFName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
            }
            else if (((TextBox)sender).Name == "txtmemlastname")
            {
                lblMLName.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                panelMLName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
            }
            else if (((TextBox)sender).Name == "txtmemrelationship")
            {
                lblRelationship.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                panelRelationship.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
            }
            else if (((TextBox)sender).Name == "txtmemocc")
            {
                lblOccupation.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                panelOccupation.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
            }
            else if (((TextBox)sender).Name == "txtmemincome")
            {
                lblMI.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                panelMI.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
            }
            else if (((TextBox)sender).Name == "txttypeincid")
            {
                lblIncType.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                panelIncType.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                countIncType.Visible = true;
            }
            else if (((TextBox)sender).Name == "txtincidlocation")
            {
                lblIncLocation.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                panelIncLocation.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                countIncLocation.Visible = true;
            }
        }

        private void richTxt_Enter(object sender, EventArgs e)
        {
            if (((RichTextBox)sender).Text == "Enter address." || ((RichTextBox)sender).Text == "Enter allergies." || ((RichTextBox)sender).Text == "Enter condition." || ((RichTextBox)sender).Text == "Enter remarks."
                || ((RichTextBox)sender).Text == "Enter incident description." || ((RichTextBox)sender).Text == "Enter actions taken." || ((RichTextBox)sender).Text == "Enter involved people."
                || ((RichTextBox)sender).Text == "Enter allergies." || ((RichTextBox)sender).Text == "Enter condition.") ((RichTextBox)sender).Text = "";
            ((RichTextBox)sender).ForeColor = Color.Black;

            if (((RichTextBox)sender).Name == "txtcaseaddress") lblAd.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            else if (((RichTextBox)sender).Name == "rtxtall") lblAllergies.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            else if (((RichTextBox)sender).Name == "rtxtcondition") lblCondition.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            else if (((RichTextBox)sender).Name == "rtremarks")
            {
                lblRemarks.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                countRemarks.Visible = true;
            }
            else if (((RichTextBox)sender).Name == "rtxtinciddesc")
            {
                lblIncDesc.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                countIncDesc.Visible = true;
            }
            else if (((RichTextBox)sender).Name == "rtxtactiontaken")
            {
                lblIncAct.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                countIncAct.Visible = true;
            }
            else if (((RichTextBox)sender).Name == "rtinvolve")
            {
                lblIncInvolve.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                countIncInvolve.Visible = true;
            }
            else if (((RichTextBox)sender).Name == "rtxtall")
            {
                lblAllergies.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                countAllergies.Visible = true;
            }
            else if (((RichTextBox)sender).Name == "rtxtcondition")
            {
                lblCondition.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                countCondition.Visible = true;
            }

        }

        private void richTxt_TextChanged(object sender, EventArgs e)
        {
            if (((RichTextBox)sender).Name == "rtremarks") countRemarks.Text = ((RichTextBox)sender).TextLength + "/1000";
            else if (((RichTextBox)sender).Name == "txtcaseaddress") countAd.Text = ((RichTextBox)sender).TextLength + "/250";
            else if (((RichTextBox)sender).Name == "rtxtinciddesc") countIncDesc.Text = ((RichTextBox)sender).TextLength + "/1000";
            else if (((RichTextBox)sender).Name == "rtxtactiontaken") countIncAct.Text = ((RichTextBox)sender).TextLength + "/1000";
            else if (((RichTextBox)sender).Name == "rtinvolve") countIncInvolve.Text = ((RichTextBox)sender).TextLength + "/1000";
            else if (((RichTextBox)sender).Name == "rtxtall") countAllergies.Text = ((RichTextBox)sender).TextLength + "/1000";
            else if (((RichTextBox)sender).Name == "rtxtcondition") countCondition.Text = ((RichTextBox)sender).TextLength + "/1000";
        }

        private void richTxt_Leave(object sender, EventArgs e)
        {
            if (((RichTextBox)sender).Text == "")
            {
                if (((RichTextBox)sender).Name == "txtcaseaddress") ((RichTextBox)sender).Text = "Enter address.";
                else if (((RichTextBox)sender).Name == "rtxtall") ((RichTextBox)sender).Text = "Enter allergies.";
                else if (((RichTextBox)sender).Name == "rtxtcondition") ((RichTextBox)sender).Text = "Enter condition.";
                else if (((RichTextBox)sender).Name == "rtremarks") ((RichTextBox)sender).Text = "Enter remarks.";
                else if (((RichTextBox)sender).Name == "rtxtinciddesc") ((RichTextBox)sender).Text = "Enter incident description.";
                else if (((RichTextBox)sender).Name == "rtxtactiontaken") ((RichTextBox)sender).Text = "Enter actions taken.";
                else if (((RichTextBox)sender).Name == "rtinvolve") ((RichTextBox)sender).Text = "Enter involved people.";
                else if (((RichTextBox)sender).Name == "rtxtall") ((RichTextBox)sender).Text = "Enter allergies.";
                else if (((RichTextBox)sender).Name == "rtxtcondition") ((RichTextBox)sender).Text = "Enter condition.";
            }
            ((RichTextBox)sender).ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);

            if (((RichTextBox)sender).Name == "txtcaseaddress") lblAd.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
            else if (((RichTextBox)sender).Name == "rtxtall") lblAllergies.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
            else if (((RichTextBox)sender).Name == "rtxtcondition") lblCondition.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
            else if (((RichTextBox)sender).Name == "rtremarks") lblRemarks.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
            else if (((RichTextBox)sender).Name == "rtxtinciddesc") lblIncDesc.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
            else if (((RichTextBox)sender).Name == "rtxtactiontaken") lblIncAct.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
            else if (((RichTextBox)sender).Name == "rtinvolve") lblIncInvolve.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
            else if (((RichTextBox)sender).Name == "rtxtall") lblAllergies.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
            else if (((RichTextBox)sender).Name == "rtxtcondition") lblCondition.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar)) e.Handled = true;
            else if (char.IsDigit(e.KeyChar)) e.Handled = false;
            else if (e.KeyChar == '.')
            {
                if (((TextBox)sender).Text.Contains(".")) e.Handled = dot = true;
                else dot = false;
            }
        }
        #endregion

        #region Reports        
        private void btnAdmitSlip_Click(object sender, EventArgs e)
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

                    System.Drawing.Image image = Properties.Resources.logo_paper;
                    iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Png);
                    pdfImage.ScalePercent(7F); doc.Add(pdfImage);
                    
                    iTextSharp.text.Font normal = FontFactory.GetFont(iTextSharp.text.FontFactory.TIMES, 11, BaseColor.BLACK);
                    iTextSharp.text.Font normal_small = FontFactory.GetFont(iTextSharp.text.FontFactory.TIMES, 9, BaseColor.BLACK);
                    iTextSharp.text.Font bold_small = FontFactory.GetFont(iTextSharp.text.FontFactory.TIMES, 9, 1, BaseColor.BLACK);
                    iTextSharp.text.Font underline = FontFactory.GetFont(iTextSharp.text.FontFactory.TIMES, 12, 4, BaseColor.BLACK);

                    PdfContentByte cb = wri.DirectContent;
                    ColumnText ct = new ColumnText(cb); Phrase right = new Phrase();
                    right.Add(new Chunk("Foundation of Balay Pasilungan, Inc.", bold_small));
                    right.Add(new Chunk("\n817 Datu Bago Street\n(P.O. Box 80718\nBankerohan, Davao City - Philippines\nTel.  221-2922    Email:  balaypasilungan@yahoo.com", normal_small));                    
                    ct.SetSimpleColumn(right, 10, 780, 550, 180, 10, Element.ALIGN_RIGHT); ct.Go();

                    Chunk chunk = new Chunk("ADMISSION SLIP", FontFactory.GetFont(iTextSharp.text.FontFactory.TIMES, 12, 1, BaseColor.BLACK));
                    Paragraph par = new Paragraph(chunk); par.Alignment = Element.ALIGN_CENTER; doc.Add(par);

                    Phrase phrase = new Phrase();
                    phrase.Add(new Chunk("\nName: ", normal));
                    phrase.Add(new Chunk(lblcasename.Text, underline));
                    
                    DateTime now = DateTime.Today, birthyear = dtbirth.Value;
                    int age = now.Year - birthyear.Year;
                    if (now < birthyear.AddYears(age)) age -= 1;

                    ct = new ColumnText(cb); right = new Phrase();
                    right.Add(new Chunk("Age: ", normal)); right.Add(new Chunk("   " + age.ToString() + "   ", underline));
                    right.Add(new Chunk(" Sex: ", normal)); right.Add(new Chunk("Male", underline));
                    ct.SetSimpleColumn(right, 10, 678, 520, 180, 10, Element.ALIGN_RIGHT); ct.Go();

                    phrase.Add(new Chunk("\nAddress: ", normal));
                    phrase.Add(new Chunk(lblcaseaddress.Text, underline));
                    phrase.Add(new Chunk("\nDate/Place of Birth: ", normal));
                    phrase.Add(new Chunk("         " + lbldate.Text + " / " + lbldate.Text + "                                  " , underline));
                    phrase.Add(new Chunk("\nDate Admitted: ", normal));
                    phrase.Add(new Chunk("         " + lbljoined.Text + "                                  ", underline));
                    phrase.Add(new Chunk("\nDistinguising Marks: a. Tattoo / Scars ", normal)); phrase.Add(new Chunk("                                                                                 ", underline));
                    phrase.Add(new Chunk("\n                                   b. Height: ", normal));
                    phrase.Add(new Chunk("    " + lblH.Text + "    ", underline));
                    phrase.Add(new Chunk("c. Weight: ", normal));
                    phrase.Add(new Chunk("    " + lblW.Text + "    ", underline));                
                    phrase.Add(new Chunk("\nPut (/) on documents submitted:", normal));
                    phrase.Add(new Chunk("\n                       1.  SCSR                                      (   )         3.  School Records                                (   )", normal));
                    phrase.Add(new Chunk("\n                       2.  Medical Certificate                   (   )                  4.  Others                                          (   )", normal));                
                    phrase.Add(new Chunk("\n\nGeneral Impression upon admission:", normal));
                    phrase.Add(new Chunk("\n", normal));
                    phrase.Add(new Chunk("\nAction/s Taken:", normal));
                    phrase.Add(new Chunk("\n", normal));
                    par = new Paragraph(); par.Add(phrase); doc.Add(par);
                    
                    ct = new ColumnText(cb); right = new Phrase();
                    right.Add(new Chunk("___________________________________", normal));
                    right.Add(new Chunk("\nName and Signature of Referring Party", normal));
                    ct.SetSimpleColumn(right, 65, 70, 65*4, 70*4, 15, Element.ALIGN_CENTER); ct.Go();
                    right = new Phrase(); right.Add(new Chunk("___________________________________", normal));
                    right.Add(new Chunk("\nAdmitting Staff Party", normal));
                    ct.SetSimpleColumn(right, 300, 70, 560, 70*4, 15, Element.ALIGN_CENTER); ct.Go();

                    right = new Phrase(); right.Add(new Chunk("___________________________________", normal));
                    right.Add(new Chunk("\nDesignature/ID No./Contact No.", normal));
                    ct.SetSimpleColumn(right, 65, 60, 65*4, 60*4, 15, Element.ALIGN_CENTER); ct.Go();
                    right = new Phrase(); right.Add(new Chunk("___________________________________", normal));
                    right.Add(new Chunk("\nDesignation", normal));
                    ct.SetSimpleColumn(right, 300, 60, 560, 60*4, 15, Element.ALIGN_CENTER); ct.Go();

                    right = new Phrase(); right.Add(new Chunk("___________________________________", normal));
                    right.Add(new Chunk("\nComplete Address", normal));                                        
                    ct.SetSimpleColumn(right, 65, 50, 65 * 4, 50 * 4, 15, Element.ALIGN_CENTER); ct.Go();
                    right = new Phrase(); right.Add(new Chunk("___________________________________", normal));
                    right.Add(new Chunk("\nDate/Time", normal));                    
                    ct.SetSimpleColumn(right, 300, 50, 560, 50 * 4, 15, Element.ALIGN_CENTER); ct.Go();

                    right = new Phrase(); right.Add(new Chunk("Noted By: ", normal));
                    right.Add(new Chunk("\n\nDIRECTOR NAME", underline));
                    right.Add(new Chunk("\nExecutive Director", normal));
                    ct.SetSimpleColumn(right, 65, 35, 65 * 4, 35 * 4, 15, Element.ALIGN_LEFT); ct.Go();

                    doc.Close();
                    successMessage("Admission slip has been exported successfully!");
                }
            }
        }
        #endregion
    }
}
