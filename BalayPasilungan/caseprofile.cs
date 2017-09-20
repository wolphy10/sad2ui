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

        public int id, hid, fammode, famid, eid, classeid, memberid, incidid, mode, archiveid, archivemode;
        public string filename, yearlvl, section, adviser;
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
            btnCases.BackgroundImage = global::BalayPasilungan.Properties.Resources.cases_green;            
        }

        public void resetMainBTN()      // Reset Main Buttons backcolor to green
        {
            btnCases.BackColor = System.Drawing.ColorTranslator.FromHtml("#0fa868");            
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
            //txtKinder.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135); txtHS.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135); txtElementary.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);

            // Labels
            //lblFName.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42); lblLName.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42); lblNName.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42); lblAddress.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
            //kinder.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42); elementary.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42); highschool.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);

            // Lines
            //panelFName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line; panelLName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line; panelNName.BackgroundImage = global::BalayPasilungan.Properties.Resources.line;
        }

        public void errorMessage(string message)            // Error Message
        {
            error err = new error();
            dim dim = new dim();

            dim.Location = this.Location;
            err.lblError.Text = message;
            dim.refToPrev = this;
            dim.Show();

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

        public void famtypecall(int id, string text)
        {
            famtype fam = new famtype();

            fam.reftofam = this;

            fam.caseid = id;
            fam.text = text;

            fam.Show();

        }

        public void successMessage(string message)            // Success Message
        {
            success yey = new success();
            dim dim = new dim();

            dim.Location = this.Location;
            yey.lblSuccess.Text = message;
            dim.refToPrev = this;
            dim.Show();

            if (yey.ShowDialog() == DialogResult.OK) dim.Close();
        }

        public void confirmMessage(string message)            // Success Message
        {
            confirm conf = new confirm();
            dim dim = new dim();

            dim.Location = this.Location;
            conf.lblConfirm.Text = message;
            dim.refToPrev = this;
            dim.Show();

            if (conf.ShowDialog() == DialogResult.OK) confirmed = true;
            else confirmed = false;
            dim.Close();
        }
        #endregion

        #region Main            
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Case Profile Load
        private void caseprofile_Load(object sender, EventArgs e)
        {
            resetMainBTN();

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
                    dt.Rows.Add(-1, "No entries.", null, null);
                    empty = true;
                }
                else multiChild.Checked = multiChild.Enabled = false;

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

        public void addmember()
        {
            string lastname = txtmemlastname.Text, firstname = txtmemfirstname.Text, relationship = txtmemrelationship.Text,
                   gender = cbxmemgender.Text, occupation = txtmemocc.Text, dependency = cbxmemdependency.Text;
            if (string.IsNullOrEmpty(lastname) || string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(relationship) || string.IsNullOrEmpty(gender) || string.IsNullOrEmpty(occupation) || string.IsNullOrEmpty(dependency)) errorMessage("Please fill out empty fields.");

            else
            {
                try
                {
                    conn.Open();
                    MySqlCommand comm = new MySqlCommand("INSERT INTO member(familyid, firstname, lastname, gender, birthdate, relationship, dependency, occupation) VALUES(" + famid + ", '" + firstname + "', '" + lastname + "', '" + gender + "', '" + dtpmembirth.Value.Date.ToString("yyyy-MM-dd") + "', '" + relationship + "', '" + dependency + "', '" + occupation + "')", conn);
                    MessageBox.Show(famid.ToString());
                    comm.ExecuteNonQuery();
                    successMessage("Member has been added successfully!");
                    conn.Close();

                    reloadmem(famid);
                    tabCase.SelectedTab = tabInfo;
                    tabChild.SelectedTab = fourth;
                    reset8();
                }
                catch (Exception ee)
                {
                    errorMessage(ee.Message);
                }
            }
        }

        public void editmember()
        {
            string lastname = txtmemlastname.Text, firstname = txtmemfirstname.Text, relationship = txtmemrelationship.Text,
                   gender = cbxmemgender.Text, occupation = txtmemocc.Text, dependency = cbxmemdependency.Text;

            if (string.IsNullOrEmpty(lastname) || string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(relationship) || string.IsNullOrEmpty(gender) || string.IsNullOrEmpty(occupation) || string.IsNullOrEmpty(dependency)) errorMessage("Please fill out empty fields.");

            else
            {
                try
                {
                    conn.Open();
                    MySqlCommand comm = new MySqlCommand("UPDATE member SET firstname = '" + firstname + "', lastname = '" + lastname + "', gender = '" + gender + "', birthdate = '" + dtpmembirth.Value.Date.ToString("yyyy-MM-dd") + "', " +
                                        "relationship = '" + relationship + "', dependency = '" + dependency + "', occupation = '" + occupation + "' WHERE memberid = " + memberid, conn);
                    MessageBox.Show(famid.ToString());
                    comm.ExecuteNonQuery();
                    successMessage("Changes in family member has been added successfully!");
                    conn.Close();

                    reloadmem(famid);
                    tabCase.SelectedTab = tabInfo;
                    tabChild.SelectedTab = fourth;
                    reset8();
                }
                catch (Exception ee)
                {
                    errorMessage(ee.Message);
                }
            }
        }

        public void insertinvolve()
        {
            string lastname, firstname;
            try
            {
                foreach (DataGridViewRow row in dtginv.Rows)
                {
                    lastname = row.Cells["lastname"].Value.ToString();
                    firstname = row.Cells["firstname"].Value.ToString();
                    if (Convert.ToBoolean(row.Cells["check"].Value))
                    {
                        MySqlCommand comm = new MySqlCommand("INSERT INTO involvement(incidid, caseid, lastname, firstname) VALUES(" + incidid + ", '" + id + "', '" + lastname + "', '" + firstname + "')", conn);
                        comm.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ee)
            {
                errorMessage(ee.Message);
            }
        }

        public void editinvolve()
        {
            string lastname, firstname;
            try
            {
                foreach (DataGridViewRow row in dtginv.Rows)
                {
                    lastname = row.Cells["lastname"].Value.ToString();
                    firstname = row.Cells["firstname"].Value.ToString();
                    if (Convert.ToBoolean(row.Cells["check"].Value))
                    {
                        MySqlCommand comm = new MySqlCommand("INSERT INTO involvement(incidid, caseid, lastname, firstname) SELECT * FROM (SELECT " + incidid + ", '" + id + "', '" + lastname + "', '" + firstname + "') AS temp WHERE" +
                                            " NOT EXISTS (SELECT incidid, lastname, firstname FROM involvement WHERE incidid = " + incidid + " AND lastname = '" + lastname + "' AND firstname = '" + firstname + "')", conn);
                        comm.ExecuteNonQuery();
                        MessageBox.Show("ADD");
                    }

                    else
                    {
                        MySqlCommand comm = new MySqlCommand("DELETE FROM involvement WHERE lastname = '" + lastname + "' AND firstname = '" + firstname + "' AND incidid = " + incidid, conn);
                        comm.ExecuteNonQuery();
                        MessageBox.Show("DELETE");
                    }
                }
            }
            catch (Exception ee)
            {
                errorMessage(ee.Message);
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

                if (dtgcs.Columns["lolz"] != null)
                {
                    dtgcs.Columns.Remove("lolz");
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
                dt.Rows.Add(-1, "No entries.", null, null);
                empty = true;
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
            }
            else multiChild.Checked = multiChild.Enabled = false;

            btnArchive.Visible = true;
            btncancelarchive.Visible = false;
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
                try
                {
                    conn.Open();

                    MySqlCommand comm = new MySqlCommand();
                    if (cbIP.Checked)       // IP
                    {
                        comm = new MySqlCommand("INSERT INTO casestudyprofile (lastname, firstname, birthdate, alias, birthplace, civilstatus, program, dateJoined, picture, address, profilestatus, religion)"
                        + " VALUES('" + txtlname.Text + "', '" + txtfname.Text + "', '" + dtbirth.Value.Date.ToString("yyyy-MM-dd") + "','" + txtAlias.Text + "','" + txtBirthplace.Text + "', '" + cbCivilStatus.SelectedItem
                        + "', '" + cbxprogram.SelectedItem + "', '" + dtjoin.Value.ToString("yyyy-MM-dd") + "', '" + filename + "', '" + txtcaseaddress.Text + "', 1, '" + txtIP.Text + "')", conn);
                    }
                    else                    // Religion
                    {
                        comm = new MySqlCommand("INSERT INTO casestudyprofile (lastname, firstname, birthdate, alias, birthplace, civilstatus, program, dateJoined, picture, address, profilestatus, religion)"
                        + " VALUES('" + txtlname.Text + "', '" + txtfname.Text + "', '" + dtbirth.Value.Date.ToString("yyyy-MM-dd") + "','" + txtAlias.Text + "','" + txtBirthplace.Text + "', '" + cbCivilStatus.SelectedItem
                        + "', '" + cbxprogram.SelectedItem + "', '" + dtjoin.Value.ToString("yyyy-MM-dd") + "', '" + filename + "', '" + txtcaseaddress.Text + "', 1, '" + txtReligion.Text + "')", conn);
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
            DateTime now = DateTime.Today, birthyear = dtbirth.Value;
            
            if (string.IsNullOrEmpty(address) || string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(lname) || string.IsNullOrEmpty(program)) errorMessage("PLease fill out empty fields.");            
            else
            {
                age = now.Year - birthyear.Year;
                if (now < birthyear.AddYears(age)) age -= 1;
                try
                {
                    conn.Open();
                    MySqlCommand comm = new MySqlCommand("UPDATE casestudyprofile SET lastname = '" + lname + "', firstname = " +
                                        "'" + fname + "', birthdate = " + dtbirth.Value.Date.ToString("yyyyMMdd") + ", civilStatus = '" + "sample" + "', " +
                                        ", program = '" + program + "', datejoined = " + dtjoin.Value.Date.ToString("yyyyMMdd") + ", " +
                                        "picture = '" + filename + "', address = '" + address + "' WHERE caseid = " + id, conn);
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
                        successMessage("New Health Biography Added!");
                        conn.Close();

                        tabCase.SelectedTab = tabInfo;

                        existshealth(id);     
                        
                        reloadedithealth(id);

                        tabChild.SelectedTab = fifteen;

                        reset3();
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show(ee.ToString());
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

                        successMessage("Changes for Health Biography Added!");

                        conn.Close();

                        tabCase.SelectedTab = tabInfo;

                        existshealth(id);

                        reloadedithealth(id);

                        tabChild.SelectedTab = fifteen;

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
                    if (double.TryParse(txtheight.Text, out height) == false && double.TryParse(txtweight.Text, out weight) == false)
                    {
                        errorMessage("Height and Weight inputs are invalid! Use numbers!");
                    }

                    else if (double.TryParse(txtheight.Text, out height) == false)
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

        public void addeducation()
        {
            string edname = txtedname.Text, type = cbxtype.Text, level = cbxedlvl.Text;

            if (string.IsNullOrEmpty(edname) || string.IsNullOrEmpty(type) || string.IsNullOrEmpty(level))
            {
                errorMessage("Please fill out empty fields.");
            }

            else
            {

                try
                {

                    conn.Open();


                    MySqlCommand comm = new MySqlCommand("INSERT INTO education(caseid, school, eduType, level) VALUES('" + id + "', '" + edname + "', '" + type + "','" + level + "')", conn);

                    comm.ExecuteNonQuery();

                    successMessage("New Education Info Added!");
                    
                    conn.Close();

                    

                    tabCase.SelectedTab = tabInfo;

                    tabChild.SelectedTab = eighth;

                    reloaded(id);


                    reset2();

                }

                catch (Exception ee)
                {
                    MessageBox.Show("" + ee);
                    conn.Close();
                }
            }
        }

        public void editeducation()
        {
            string edname = txtedname.Text, type = cbxtype.Text, level = cbxedlvl.Text;

            if (string.IsNullOrEmpty(edname) || string.IsNullOrEmpty(type) || string.IsNullOrEmpty(level))
            {
                errorMessage("Please fill out empty fields.");
            }

            else
            {

                try
                {
                    
                    conn.Open();


                    MySqlCommand comm = new MySqlCommand("UPDATE education SET school = '" + edname + "', eduType = '" + type + "', level = '" + level + "' WHERE eid = " + eid, conn);

                    comm.ExecuteNonQuery();

                    successMessage("Changes in Education Info Added!");


                    conn.Close();

                    

                    tabCase.SelectedTab = tabInfo;

                    tabChild.SelectedTab = eighth;

                    reloaded(id);


                    reset2();

                }

                catch (Exception ee)
                {
                    MessageBox.Show("" + ee);
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

                MySqlCommand comm = new MySqlCommand("SELECT school, eduType, level FROM education WHERE eid = " + eid, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {

                    txtedname.Text = dt.Rows[0]["school"].ToString();
                    cbxtype.Text = dt.Rows[0]["eduType"].ToString();
                    cbxedlvl.Text = dt.Rows[0]["level"].ToString();
                    
                }

                else
                {
                    errorMessage("This case study has no records yet.");
                }

                conn.Close();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
            }
        }

        public void reloadeditincid(int id)
        {
           

            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT incident.incidid, type, incdate, venue, description, action FROM incident " +
                                    "WHERE incident.caseid = " + id,  conn);

                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                MessageBox.Show(id.ToString());
                adp.Fill(dt);

                
                    incidid = int.Parse(dt.Rows[0]["incidid"].ToString());

                    txttypeincid.Text = dt.Rows[0]["type"].ToString();
                    txtincidlocation.Text = dt.Rows[0]["venue"].ToString();
                    rtxtinciddesc.Text = dt.Rows[0]["description"].ToString();
                    rtxtactiontaken.Text = dt.Rows[0]["action"].ToString();

                    dateincid.Value = Convert.ToDateTime(dt.Rows[0]["incdate"]).Date;
            
                conn.Close();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
            }
        }

        public void addincidrecord()
        {
            string type = txttypeincid.Text, hour = cbxhour.Text, minute = cbxmin.Text, zone, location = txtincidlocation.Text, desc = rtxtinciddesc.Text, action = rtxtactiontaken.Text;

            if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(hour) || string.IsNullOrEmpty(minute) || string.IsNullOrEmpty(location) || string.IsNullOrEmpty(desc) || string.IsNullOrEmpty(action) || (rbam.Checked == false && rbpm.Checked == false))
            {
                errorMessage("Please fill out empty fields.");
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

                MessageBox.Show(dateincid.Value.Date.ToString("yyyy-MM-dd"));

                try
                {
                    conn.Open();


                    MySqlCommand comm = new MySqlCommand("INSERT INTO incident(caseid, type, incdate, venue, description, action, dateadded) VALUES('" + id + "', '" + type + "', '" + dateincid.Value.Date.ToString("yyyy-MM-dd ") + dt.ToString("hh:mm tt") + "','" + location + "', '" + desc + "', '" + action + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "')", conn);

                    comm.ExecuteNonQuery();

                    if (checkinv.Checked && mode == 1)
                    {
                        getincidid(id);

                        insertinvolve();
                    }

                    successMessage("Incident Record Added!");

                    conn.Close();

                    reloadincid(id);

                    tabCase.SelectedTab = tabInfo;
                    tabChild.SelectedTab = twelfth;

                    reset5();
                }

                catch (Exception ee)
                {
                    MessageBox.Show("" + ee);
                    conn.Close();

                }

            }

            
        }

        public void editincidrecord()
        {
            string type = txttypeincid.Text, hour = cbxhour.Text, minute = cbxmin.Text, zone, location = txtincidlocation.Text, desc = rtxtinciddesc.Text, action = rtxtactiontaken.Text;

            if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(hour) || string.IsNullOrEmpty(minute) || string.IsNullOrEmpty(location) || string.IsNullOrEmpty(desc) || string.IsNullOrEmpty(action) || (rbam.Checked == false && rbpm.Checked == false))
            {
                errorMessage("Please fill out empty fields.");
                //MessageBox.Show("Please fill out empty fields.");
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

                MessageBox.Show(dateincid.Value.Date.ToString("yyyy-MM-dd"));

                try
                {
                    conn.Open();


                    MySqlCommand comm = new MySqlCommand("UPDATE incident SET type = '" + type + "', incdate = '" + dateincid.Value.Date.ToString("yyyy-MM-dd ") + dt.ToString("hh:mm tt") + "', " +
                        "venue = '" + location + "', description = '" + desc + "', action = '" + action + "' WHERE incidid = " + incidid, conn);

                    comm.ExecuteNonQuery();

                    if (checkinv.Checked && mode == 1)
                    {
                        //getincidid(id);

                        editinvolve();
                    }

                    successMessage("Incident Record Added!");

                    conn.Close();

                    reloadincid(id);

                    tabCase.SelectedTab = tabInfo;
                    tabChild.SelectedTab = twelfth;

                    reset5();

                    
                }

                catch (Exception ee)
                {
                    MessageBox.Show("" + ee);
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
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

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
                MessageBox.Show(ee.ToString());
                conn.Close();
            }


        }

        

        public void reloadeditmember(int memberid)
        {
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT firstname, lastname, gender, birthdate, relationship, dependency, occupation FROM member WHERE memberid = " + memberid, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                adp.Fill(dt);


                if (dt.Rows.Count > 0)
                {

                    txtmemfirstname.Text = dt.Rows[0]["firstname"].ToString();
                    txtmemlastname.Text = dt.Rows[0]["lastname"].ToString();
                    txtmemocc.Text = dt.Rows[0]["occupation"].ToString();
                    txtmemrelationship.Text = dt.Rows[0]["relationship"].ToString();

                    cbxmemdependency.Text = dt.Rows[0]["dependency"].ToString();
                    cbxmemgender.Text = dt.Rows[0]["gender"].ToString();

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

        public void reloadinvcases()
        {

            try
            {
                conn.Open();

                MySqlDataAdapter adp = new MySqlDataAdapter("SELECT caseid, lastname, firstname, program FROM casestudyprofile WHERE caseid != " + id, conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(-1, "No entries.", null, null);
                    empty = true;
                }

                dtginv.DataSource = dt;

                // Case Profile UI Modifications
                dtginv.Columns[1].HeaderText = "LASTNAME";
                dtginv.Columns[2].HeaderText = "FIRSTNAME";
                dtginv.Columns[3].HeaderText = "PROGRAM";

                // For ID purposes (hidden from user)            
                dtginv.Columns["caseid"].Visible = false;

                // 935 WIDTH
                dtginv.Columns[1].Width = 380;
                dtginv.Columns[2].Width = 380;
                dtginv.Columns[3].Width = 175;

                if (dt.Rows.Count > 0 && !empty)
                {
                    dtginv.Columns[1].HeaderCell.Style.Padding = new Padding(10, 0, 0, 0);
                    dtginv.Columns[1].DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);

                    DataGridViewCheckBoxColumn dc = new DataGridViewCheckBoxColumn();

                    dc.Name = "check";
                    dc.Visible = true;
                    dc.TrueValue = true;
                    dc.FalseValue = false;
                    dc.ReadOnly = false;

                    if (dtginv.Columns["check"] == null)
                    {
                        dtginv.Columns.Add(dc);

                        dc.TrueValue = true;
                        dc.FalseValue = false;
                    }


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

        public void reloadeditclass(int classid)
        {
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT section, adviser, yearlevel FROM edclass WHERE edclassid = " + classid, conn);
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
                }

                else
                {
                    errorMessage("There are currently no existing health records for this case study.");
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

                    if(dt.Rows[0]["religion"].ToString() != null)
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
                
                comm = new MySqlCommand("SELECT famtype, COUNT(memberid) FROM family JOIN member ON family.familyid = member.familyid WHERE family.caseid = " + id, conn);
                adp = new MySqlDataAdapter(comm);
                dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    lblfamtypedis.Text = dt.Rows[0]["famtype"].ToString();
                    lblmemcountdis.Text = dt.Rows[0]["COUNT(memberid)"].ToString();
                }

                else
                {
                    lblfamtypedis.Text = "";
                    lblmemcountdis.Text = "";
                }

                /*comm = new MySqlCommand("SELECT incdate FROM consultation WHERE caseid = " + id, conn);
                adp = new MySqlDataAdapter(comm);
                dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                   
                }*/
                btnArchive.Visible = true;
                btncancelarchive.Visible = false;
                
                conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
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
                    dtgcon.Columns[0].Visible = false;
                }
                
                conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
            }
        }

        public void reloaded(int id)
        {
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT eid, school, level FROM education WHERE caseid = " + id + " ORDER BY school", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    errorMessage("There are no current education records for this child.");
                }

                else
                {
                    dtgeducation.DataSource = dt;

                    DataGridViewButtonColumn EditColumn = new DataGridViewButtonColumn();
                    EditColumn.Text = "Edit";
                    EditColumn.Name = "Edit";
                    EditColumn.DataPropertyName = "Edit";
                    



                    DataGridViewButtonColumn AddColumn = new DataGridViewButtonColumn();
                    AddColumn.Text = "Add";
                    AddColumn.Name = "Add";
                    AddColumn.DataPropertyName = "Add";


                    if (dtgeducation.Columns["Edit"] == null && archivemode == 0)
                    {
                        dtgeducation.Columns.Add(EditColumn);
                        
                    }

                    
                    if (dtgeducation.Columns["Add"] == null && archivemode == 0)
                    {
                        dtgeducation.Columns.Add(AddColumn);
                        
                    }

                }


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

                MySqlCommand comm = new MySqlCommand("SELECT edclassid, section, adviser, yearlevel FROM edclass WHERE eid = " + eid + " ORDER BY yearlevel", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    errorMessage("There are currently no class records for this school.");
                }



                dtgedclass.DataSource = dt;



               DataGridViewButtonColumn EditColumn = new DataGridViewButtonColumn();
               EditColumn.Text = "Edit";
               EditColumn.Name = "Edit";
                EditColumn.DataPropertyName = "Edit";

                if (dtgedclass.Columns["Edit"] == null && archivemode == 0)
                {
                    dtgedclass.Columns.Add(EditColumn);

                }

                //dtgedclass.Columns["edclassid"].Visible = false;

                conn.Close();
            }
            catch (Exception ee)
            {
                errorMessage(ee.Message);
                conn.Close();
            }
        }

        public void reloadedited(int id)
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
                }


                dtgcon.Columns[0].Visible = false;

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
            try
            {
                conn.Open();

                MySqlCommand comm = new MySqlCommand("SELECT incidid, type, incdate FROM incident WHERE caseid = " + id + " ORDER BY incdate", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    errorMessage("There are no current incident records for this case study.");
                }
                else
                {
                    dtincid.DataSource = dt;
                    dtincid.Columns[0].Visible = false;
                }
                
                conn.Close();
            }
            catch (Exception ee)
            {
                errorMessage(ee.Message);
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
                    errorMessage("There are no current incident records for this case study.");
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
                errorMessage(ee.Message);
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
                    errorMessage("There are no current family records for this case study.");
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
                MySqlCommand comm = new MySqlCommand("SELECT COUNT(memberid) FROM member WHERE familyid = " + famid, conn);

                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();

                adp.Fill(dt);



                if (dt.Rows.Count > 0)
                {
                    comm = new MySqlCommand("SELECT memberid, firstname, lastname, gender, birthdate, relationship, dependency, occupation FROM member WHERE familyid = " + famid, conn);

                    adp = new MySqlDataAdapter(comm);
                    dt = new DataTable();

                    adp.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dtfamOverview.DataSource = dt;
                        dtfamOverview.Columns["memberid"].Visible = false;

                        comm = new MySqlCommand("SELECT COUNT(memberid) FROM member WHERE familyid = " + famid, conn);
                        adp = new MySqlDataAdapter(comm);
                        dt = new DataTable();

                        adp.Fill(dt);

                        lblnummembers.Text = dt.Rows[0]["COUNT(memberid)"].ToString();


                        DataGridViewButtonColumn dc = new DataGridViewButtonColumn();

                        dc.Name = "check";
                        dc.Visible = true;

                        if (dtfamOverview.Columns["check"] == null)
                        {
                            dtfamOverview.Columns.Add(dc);


                        }

                    }
                }

                    

                else
                {
                    errorMessage("There are no current member records for this case study.");
                }
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                conn.Close();
            }
        }

        #endregion

        #region cellclicks
        private void dtgcs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id = int.Parse(dtgcs.Rows[e.RowIndex].Cells[0].Value.ToString());

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
                errorMessage(ee.ToString());
            }
        }

        
        private void dtghealth_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int checkid = int.Parse(dtghealth.Rows[e.RowIndex].Cells[0].Value.ToString());
                MessageBox.Show(checkid.ToString());
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

                tabChild.SelectedTab = thirteen;

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

                tabChild.SelectedTab = thirteen;

                conn.Close();

            }

            catch (Exception ee)
            {
                //MessageBox.Show("" + ee);
                conn.Close();
            }
        }

        private void dtgeducation_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            eid = int.Parse(dtgeducation.Rows[e.RowIndex].Cells["eid"].Value.ToString());

            if (!(senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn))
            {
                reloadedclass(eid);
            }

               
        }

        private void dtgeducation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            eid = int.Parse(dtgeducation.Rows[e.RowIndex].Cells["eid"].Value.ToString());

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)

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
            MessageBox.Show(dtgedclass.Rows[e.RowIndex].Cells[0].Value.ToString());
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)

            {
               try
                {
                    

                    int edclassid = int.Parse(dtgedclass.Rows[e.RowIndex].Cells[0].Value.ToString());

                    reloadeditclass(edclassid);

                }

                catch(Exception ee)
                {
                    MessageBox.Show(ee.ToString());
                }

            }
        }

        private void dtfamOverview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)

            {
                tabCase.SelectedTab = tabNewChild;
                tabaddchild.SelectedTab = tabNewMember;

                lbladdeditprofile.Text = "Edit Family Members Info";
                btnaddmember.Text = "ADD CHANGES";

                memberid = int.Parse(dtfamOverview.Rows[e.RowIndex].Cells["memberid"].Value.ToString());

                reloadeditmember(memberid);
            }

        }

        private void dtgarchive_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                archiveid = int.Parse(dtgarchive.Rows[e.RowIndex].Cells[0].Value.ToString());

                tabChild.SelectedTab = sixteen;
                tabCase.SelectedTab = tabInfo;

                archivemode = 1;

                reload(archiveid);
                
                existshealth(archiveid);

                hidedem();

            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
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

            rbam.Checked = rbpm.Checked = false;

            cbxhour.SelectedIndex = cbxmin.SelectedIndex = -1;

            dateincid.Value = DateTime.Now.Date;

            checkinv.Enabled = true;
            checkinv.Checked = false;
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

            cbxmemgender.SelectedIndex = cbxmemdependency.SelectedIndex = -1;

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

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 1; j < dt.Rows.Count; j++)
                        {
                            //if (dt.Rows[i]["dateadded"] != null && dt.Rows[j]["dateadded"] != null) { }

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

                else
                {
                    lbledlvl.Text = "";
                    lbledschool.Text = "";
                }
            }
            catch (Exception ee)
            {
                errorMessage(ee.Message);
                
                
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
                errorMessage(ee.Message);
                btnaddinvolve.Text = "Add Info";                
                conn.Close();
            }
        }

        public void existscon(int id)
        {
            MySqlCommand comm = new MySqlCommand("SELECT interviewdate FROM consultation WHERE caseid = " + id, conn); //
            MySqlDataAdapter adp = new MySqlDataAdapter(comm);
            DataTable dt = new DataTable();

            DateTime consuldate;

            adp.Fill(dt);

            if (dt.Rows.Count > 0)
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

            else
            {
                lblconsuldate.Text = "";
            }
        }

        public void existsfam(int id)
        {
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand("SELECT caseid FROM family WHERE caseid = " + id, conn);
                int UserExist = (int)comm.ExecuteScalar();
                btnfamtype.Text = (UserExist > 0) ? "EDIT TYPE" : "ADD TYPE"; //put add info on catch

                comm = new MySqlCommand("SELECT familyid FROM family WHERE caseid = " + id, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                famid = int.Parse(dt.Rows[0]["familyid"].ToString());
                conn.Close();
            }
            catch (Exception ee)
            {
                errorMessage(ee.Message);
                btned.Text = "ADD TYPE";           
                lblfamilytype.Text = "";
                conn.Close();
            }
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

        private void familyTS_Click(object sender, EventArgs e)
        {
            resetTS();
            familyTS.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            familyTS.ForeColor = Color.Black;
            familyTS.BackgroundImage = global::BalayPasilungan.Properties.Resources.tsLine;
            tabChild.SelectedIndex = 1;

            resetall();
            btnfover_Click(sender, e);
        }

        private void eduTS_Click(object sender, EventArgs e)
        {
            resetTS();
            eduTS.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            eduTS.ForeColor = Color.Black;
            eduTS.BackgroundImage = global::BalayPasilungan.Properties.Resources.tsLine;
            tabChild.SelectedIndex = 2;
        }

        private void healthTS_Click(object sender, EventArgs e)
        {
            resetTS();
            healthTS.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            healthTS.ForeColor = Color.Black;
            healthTS.BackgroundImage = global::BalayPasilungan.Properties.Resources.tsLine;
            tabChild.SelectedIndex = 3;
        }

        private void consulTS_Click(object sender, EventArgs e)
        {
            resetTS();
            consulTS.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            consulTS.ForeColor = Color.Black;
            consulTS.BackgroundImage = global::BalayPasilungan.Properties.Resources.tsLine;
            tabChild.SelectedIndex = 4;
        }

        private void othersTS_Click(object sender, EventArgs e)
        {
            resetTS();
            othersTS.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            othersTS.ForeColor = Color.Black;
            othersTS.BackgroundImage = global::BalayPasilungan.Properties.Resources.tsLine;
            tabChild.SelectedIndex = 5;
        }
        #endregion

        #region Main Buttons
        private void btnCases_Click(object sender, EventArgs e)
        {
            resetMainBTN();
            tabCase.SelectedTab = tabCases;    
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
                btnaddinvolve.Text = "ADD";
                reloadinvcases();
                lbladdeditprofile.Text = "New Incident Record";
            }

            else
            {
                btnaddinvolve.Text = "EDIT";
                lbladdeditprofile.Text = "Update Incident Record";
            }

            mode = 0;
            checkinv.Checked = false;
            checkinv.Enabled = true;
        }

        private void btnNewConfirm_Click(object sender, EventArgs e)
        {
            resetNewChildTS();
            tabaddchild.SelectedTab = tabNewInvolve;
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

                MessageBox.Show("" + ee);
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

                MessageBox.Show("" + ee);
             
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
            if (btnaddeditcase.Text == "ADD NEW PROFILE") addprofile();            
            else editprofile();
        }

        private void btnaddarchive_Click(object sender, EventArgs e)
        {
            int archiveid;

            confirmMessage("You are about to archive the following case studies. Once you archive, you will be no longer able to tamper with the " +
                "information pertaining to the case studies.\nContinue?");
            if (confirmed)
            {
                try
                {
                    conn.Open();
                    foreach (DataGridViewRow row in dtgcs.Rows)
                    {
                        archiveid = int.Parse(row.Cells["caseid"].Value.ToString());
                        if (Convert.ToBoolean(row.Cells["lolz"].Value))
                        {
                            MySqlCommand comm = new MySqlCommand("UPDATE casestudyprofile SET profilestatus = " + 0 + " WHERE caseid = " + archiveid, conn);
                            comm.ExecuteNonQuery();
                        }
                    }
                    conn.Close();
                    refresh();
                }
                catch (Exception ee)
                {
                    errorMessage(ee.Message);
                    conn.Close();
                }
            }
            btncancelarchive.Visible = true;
            btnArchive.Visible = false;
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

                   successMessage("Checkup Record Added!");

                    conn.Close();

                    reloadhealth(id);

                    tabChild.SelectedTab = fifteen;
                   

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

                    successMessage("Consultation Record Added!");

                    conn.Close();

                    reloadcon(id);

                    tabCase.SelectedTab = tabInfo;
                    tabChild.SelectedTab = ninth;

                    reset4();
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
            if (btnaddmember.Text == "ADD")
            {
                addmember();
            }

            else
            {
                editmember();
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

        private void btnbackfromarchive_Click(object sender, EventArgs e)
        {
            tabCase.SelectedTab = tabCases;
            reload(id);
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

        private void bttnbackfromcheckrec_Click(object sender, EventArgs e)
        {
            tabChild.SelectedTab = fifteen;
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
            tabconrecords.SelectedTab = tabrecords;
            richboxrecords.Clear();

            lblcontitle.Visible = true;
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
            tabChild.SelectedTab = sixteen;
        }

        #endregion

        #region into buttons

        private void btnhealth_Click(object sender, EventArgs e)
        {
            tabChild.SelectedTab = fifteen;

            reloadedithealth(id);

            reloadhealth(id);
        }

        private void btned_Click(object sender, EventArgs e)
        {
           
                tabChild.SelectedTab = eighth;
            
                reloaded(id);
            
        }

        private void btncon_Click(object sender, EventArgs e)
        {
            tabChild.SelectedTab = ninth;

            tabconrecords.SelectedTab = tabrecords;

            reloadcon(id);
        }

        private void btnfover_Click(object sender, EventArgs e)
        {
            tabChild.SelectedTab = fourth;
            existsfam(id);
            reloadfam(id);
        }

        private void btnincidview_Click(object sender, EventArgs e)
        {
            tabChild.SelectedTab = twelfth;       
            reloadincid(id);
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

            dtgcs.Columns[1].Width = 233;
            dtgcs.Columns[2].Width = 233;
            dtgcs.Columns[3].Width = 234;

            lulz.Name = "lolz";
            lulz.Width = 233;

            if (dtgcs.Columns["lolz"] == null)
            {
                MessageBox.Show("aaaaaaa");


                dtgcs.Columns.Add(lulz);



            }

            //dtgcs.Refresh();

            btnaddarchive.Visible = true;
            btncancelarchive.Visible = true;
        }

        private void dtginv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            btnaddinvolve.Text = "EDIT";
        }

        private void btneditincid_Click(object sender, EventArgs e)
        {
            tabCase.SelectedTab = tabNewChild;
            tabaddchild.SelectedTab = tabNewIncid;
            btnaddincidrecord.Text = "ADD CHANGES";
            lbladdeditprofile.Text = "UPDATE INCIDENT RECORD";
            checkinv.Text = "Change people involved?";
            reloadinvcases();
            reloadeditincid(id);
        }

        private void btnfamtype_Click(object sender, EventArgs e)
        {
           famtypecall(id, btnfamtype.Text);            
        }
   
        private void btninvok_Click(object sender, EventArgs e)
        {
            tabaddchild.SelectedTab = tabNewIncid;
            checkinv.Enabled = false;
        }        

        private void btnaddinvolve_Click(object sender, EventArgs e)
        {
            tabaddchild.SelectedTab = tabNewInvolve;
            reloadinvcases();
            lbladdeditprofile.Text = "People Involved";            
        }

        private void tabaddinfo_Click(object sender, EventArgs e)
        {

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

            refresh2();
        }

        private void checkinv_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkinv.Checked) btnaddinvolve.Enabled = true;
            else btnaddinvolve.Enabled = false;
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

                        successMessage("Educator's report added successfully!");
                        tabChild.SelectedTab = eighth;
                    }
                    catch(Exception ex)
                    {
                        errorMessage(ex.Message);
                    }
                }
            }
        }

        
        private void btnAddMem_Click(object sender, EventArgs e)
        {
            tabCase.SelectedTab = tabNewChild;
            tabaddchild.SelectedTab = tabNewMember;

            lbladdeditprofile.Text = "New Family Members Info";
            btnaddmember.Text = "ADD";
        }

        private void btnaddincid_Click(object sender, EventArgs e)
        {
            tabCase.SelectedTab = tabNewChild;
            tabaddchild.SelectedTab = tabNewIncid;

            btnaddinvolve.Text = "ADD";
            checkinv.Text = "Are there other people involved?";
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

            btncancelcon.Text = "CANCEL";
            btnbackfromcheck.Text = "CANCEL";
        }

#endregion

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

        #region Textbox
        private void txtNew_Enter(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == "Enter first name." || ((TextBox)sender).Text == "Enter last name.") ((TextBox)sender).Text = "";
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
                panelHeight.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;                
            }
            else if (((TextBox)sender).Name == "txtweight")
            {
                lblWeight.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                panelWeight.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
            }
            else if (((TextBox)sender).Name == "txtAlias")
            {
                lblAlias.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
                panelAlias.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
                countAlias.Visible = true;
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
        }

        private void txtNewCount_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Name == "txtfname") countFName.Text = ((TextBox)sender).TextLength + "/100";
            else if (((TextBox)sender).Name == "txtlname") countLName.Text = ((TextBox)sender).TextLength + "/100";
            else if (((TextBox)sender).Name == "txtedname") countED.Text = ((TextBox)sender).TextLength + "/100";
            else if (((TextBox)sender).Name == "txtedname") countED.Text = ((TextBox)sender).TextLength + "/100";
        }

        private void txtNew_Leave(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == "")
            {
                if (((TextBox)sender).Name == "txtfname") ((TextBox)sender).Text = "Enter first name.";
                else if (((TextBox)sender).Name == "txtlname") ((TextBox)sender).Text = "Enter last name.";
                else if (((TextBox)sender).Name == "txtedname") ((TextBox)sender).Text = "Enter name of school.";
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
                panelHeight.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
            }
            else if (((TextBox)sender).Name == "txtweight")
            {
                lblWeight.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
                panelWeight.BackgroundImage = global::BalayPasilungan.Properties.Resources.line_blue;
            }
        }

        private void richTxt_Enter(object sender, EventArgs e)
        {
            if (((RichTextBox)sender).Text == "Enter address." || ((RichTextBox)sender).Text == "Enter allergies." || ((RichTextBox)sender).Text == "Enter condition.") ((RichTextBox)sender).Text = "";
            ((RichTextBox)sender).ForeColor = Color.Black;

            if (((RichTextBox)sender).Name == "txtcaseaddress") lblAd.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            else if (((RichTextBox)sender).Name == "rtxtall") lblAllergies.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
            else if (((RichTextBox)sender).Name == "rtxtcondition") lblCondition.ForeColor = System.Drawing.Color.FromArgb(62, 153, 141);
        }

        private void richTxt_TextChanged(object sender, EventArgs e)
        {
            //if (((RichTextBox)sender).Name == "txtcaseaddress") countAd.Text = ((RichTextBox)sender).TextLength + "/100";
        }

        private void richTxt_Leave(object sender, EventArgs e)
        {
            if (((RichTextBox)sender).Text == "")
            {
                if (((RichTextBox)sender).Name == "txtcaseaddress") ((RichTextBox)sender).Text = "Enter name of school.";
                else if (((RichTextBox)sender).Name == "rtxtall") ((RichTextBox)sender).Text = "Enter allergies.";
                else if (((RichTextBox)sender).Name == "rtxtcondition") ((RichTextBox)sender).Text = "Enter condition.";
            }
            ((RichTextBox)sender).ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);

            if (((RichTextBox)sender).Name == "txtcaseaddress") lblAd.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
            else if (((RichTextBox)sender).Name == "rtxtall") lblAllergies.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
            else if (((RichTextBox)sender).Name == "rtxtcondition") lblCondition.ForeColor = System.Drawing.Color.FromArgb(42, 42, 42);
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) e.Handled = true;
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
                    successMessage("Admission slip exported successfully!");
                }
            }
        }
        #endregion
    }
}
