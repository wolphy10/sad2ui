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

namespace BalayPasilungan
{
    public partial class edclass : Form
    {
        public MySqlConnection conn;
        public bool confirmed;

        public caseprofile reftocase { get; set; }
        public edclass()
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=prototype_sad;Uid=root;Pwd=root;");
        }

        public int classeid { get; set; }
        public string level { get; set; }

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
                addclass();
            }

            else
            {
                editclass();
            }
            
        }

        public void addclass()
        {
            string section = txtedsection.Text, year = cbxedyear.Text, adviser = txtedadviser.Text;

            if (string.IsNullOrEmpty(section) || string.IsNullOrEmpty(year) || string.IsNullOrEmpty(adviser))
            {
                errorMessage("Please fill out empty fields.");
            }

            else
            {

                try
                {

                    conn.Open();


                    MySqlCommand comm = new MySqlCommand("INSERT INTO edclass(eid, section, adviser, yearlevel) VALUES('" + classeid + "', '" + section + "', '" + adviser + "','" + year + "')", conn);

                    comm.ExecuteNonQuery();

                    successMessage("New Class Info Added!");


                    conn.Close();

                    reftocase.reloaded(reftocase.id);
                    reftocase.reloadedclass(reftocase.eid);

                    //MessageBox.Show(reftocase.eid.ToString());

                    this.Close();


                }

                catch (Exception ee)
                {
                    MessageBox.Show("" + ee);
                    conn.Close();
                }
            }
        }

        public void editclass()
        {
            string section = txtedsection.Text, year = cbxedyear.Text, adviser = txtedadviser.Text;

            if (string.IsNullOrEmpty(section) || string.IsNullOrEmpty(year) || string.IsNullOrEmpty(adviser))
            {
                errorMessage("Please fill out empty fields.");
            }

            else
            {

                try
                {

                    conn.Open();


                    MySqlCommand comm = new MySqlCommand("INSERT INTO edclass(eid, section, adviser, yearlevel) VALUES('" + classeid + "', '" + section + "', '" + adviser + "','" + year + "')", conn);

                    comm.ExecuteNonQuery();

                    successMessage("New Class Info Added!");


                    conn.Close();

                    reftocase.reloaded(reftocase.id);
                    reftocase.reloadedclass(reftocase.eid);

                    //MessageBox.Show(reftocase.eid.ToString());

                    this.Close();


                }

                catch (Exception ee)
                {
                    MessageBox.Show("" + ee);
                    conn.Close();
                }
            }
        }

        private void edclass_Load(object sender, EventArgs e)
        {
            int counter;

            if (level == "Preschool")
            {
                counter = 3;
            }

            else if (level == "Elementary")
            {
                counter = 6;
            }

            else
            {
                counter = 4;
            }

            for (int i = 0; i < counter; i++)
            {
                cbxedyear.Items.Add(i);
            }
        }
    }
}
