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
    public partial class summaryDates : Form
    {
        public MySqlConnection conn;
        public Form reftoprefrom { get; set; }
        public string[] aMonths = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        public string[] rowSummary;
        public summaryDates()
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=prototype_sad;Uid=root;Pwd=root;");
        }
        public string mnth { get; set; }
        public string yr { get; set; }
        private void summaryDates_Load(object sender, EventArgs e)
        {
            evSummary(mnth, yr);
        }
        public void evSummary(string month, string year)
        {
            string datecurfrom = year + "-" + month + "-" + "1";
            DateTime datefrom = DateTime.ParseExact(datecurfrom, "yyyy-M-d", CultureInfo.InvariantCulture);
            string datecurto = year + "-" + month + "-" + DateTime.DaysInMonth(datefrom.Year, datefrom.Month);
            DateTime dateto = DateTime.ParseExact(datecurto, "yyyy-M-d", CultureInfo.InvariantCulture);
            DataRow dr;
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("Date");
            dt2.Columns.Add("Event Name");
            dt2.Columns.Add("Event Type");
            dt2.Columns.Add("Description");
            dt2.Columns.Add("Venue");
            dt2.Columns.Add("Status");
            dt2.Columns.Add("Progress");
            dt2.Columns.Add("Requested By");
            dr = dt2.NewRow();
            try
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand("SELECT * FROM event WHERE ('" + datecurfrom + "' >= str_to_date(evDateFrom, '%Y-%m-%d')) AND ('" + datecurto + "' <= str_to_date(evDateTo, '%Y-%m-%d')) OR ('" + datecurfrom + "' <= str_to_date(evDateTo, '%Y-%m-%d')) AND ('" + datecurto + "' >= str_to_date(evDateFrom, '%Y-%m-%d'))", conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr["Date"] = dt.Rows[i]["evDateFrom"].ToString() + " " + dt.Rows[i]["evTimeFrom"].ToString() + " - " + dt.Rows[i]["evDateTo"].ToString() + " " + dt.Rows[i]["evTimeTo"].ToString();
                        dr["Event Name"] = dt.Rows[i]["evName"].ToString();
                        dr["Event Type"] = dt.Rows[i]["evType"].ToString();
                        dr["Description"] = dt.Rows[i]["evDesc"].ToString();
                        dr["Venue"] = dt.Rows[i]["evVenue"].ToString();
                        dr["Status"] = dt.Rows[i]["status"].ToString();
                        dr["Progress"] = dt.Rows[i]["evProgress"].ToString();
                        dr["Requested By"] = dt.Rows[i]["requestedBy"].ToString();
                        dt2.Rows.Add(dr);
                        dr = dt2.NewRow();
                    }
                }
                summaryView.DataSource = dt2;
                conn.Close();
                foreach (DataGridViewColumn ya in summaryView.Columns)
                {
                    ya.Width = 160;
                }
                DataGridViewColumn wo = summaryView.Columns[0];
                wo.Width = 400;
                foreach (DataGridViewRow ro in summaryView.Rows)
                {
                    ro.Height = 50;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Nah!" + ee);
                conn.Close();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void summaryDates_FormClosing(object sender, FormClosingEventArgs e)
        {
            reftoprefrom.Show();
        }
    }
}
