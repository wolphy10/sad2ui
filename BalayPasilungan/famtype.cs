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
    public partial class famtype : Form
    {
        public MySqlConnection conn;
        public bool confirmed;

        public caseprofile reftofam { get; set; }

        public famtype()
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=prototype_sad;Uid=root;Pwd=root;");
        }

        public string familytypegiven { get; set; }
        public int familyid { get; set; }
        public int caseid { get; set; }

        public string text {get; set;}

        #region messagefunctions

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

        private void btncanceledclass_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnaddedclass_Click(object sender, EventArgs e)
        {
            if (btnaddedclass.Text == "ADD")
            {
                
                addtype();
            }

            else
            {
                
                edittype();
            }

        }

        public void addtype()
        {
            string type = cbxtype.Text;

            if (string.IsNullOrEmpty(type))
            {
                errorMessage("Please fill out empty fields.");
            }

            else
            {

                try
                {

                    conn.Open();


                    MySqlCommand comm = new MySqlCommand("INSERT INTO family(famtype, caseid) VALUES('" + type + "', '" + caseid + "')", conn);

                    comm.ExecuteNonQuery();

                    successMessage("New Family Type Added!");


                    conn.Close();

                    reftofam.existsfam(reftofam.id);
                    reftofam.reloadfam(reftofam.id);

                    this.Close();


                }

                catch (Exception ee)
                {
                    MessageBox.Show("" + ee);
                    conn.Close();
                }
            }
        }

        public void edittype()
        {
            string type = cbxtype.Text;

            if (string.IsNullOrEmpty(type))
            {
                errorMessage("Please fill out empty fields.");
            }

            else
            {

                try
                {

                    conn.Open();


                    MySqlCommand comm = new MySqlCommand("UPDATE family SET famtype = '" + type + "' WHERE familyID = " + familyid, conn);

                    comm.ExecuteNonQuery();

                    successMessage("Family Type Edited!");


                    conn.Close();

                    reftofam.existsfam(reftofam.id);
                    reftofam.reloadfam(reftofam.id);

                    this.Close();


                }

                catch (Exception ee)
                {
                    MessageBox.Show("" + ee);
                    conn.Close();
                }
            }
        }

        private void famtype_Load(object sender, EventArgs e)
        {
            if (text == "ADD TYPE")
            {

                btnaddedclass.Text = "ADD";
            }

            else
            {

                btnaddedclass.Text = "ADD CHANGES";
            }
        }
    }
}
