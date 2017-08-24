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
    public partial class confirm : Form
    {
        public Form refToLogin { get; set; }

        public bool boolLogin { get; set; }

        public confirm()
        {
            InitializeComponent();
        }
        
        private void confirm_Load(object sender, EventArgs e)
        {
            if (boolLogin)
            {
                lblConfirm.Text = "Are you sure you want to leave?";
            }
        }        

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.Close();
            refToLogin.Close();                    
        }
       
    }
}
