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
        public Form refToExpense { get; set; }

        public bool boolLogin { get; set; }
        public bool boolExpense { get; set; }

        public confirm()
        {
            InitializeComponent();
            btnConfirm.DialogResult = DialogResult.OK;
        }
        
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();            
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (boolLogin) refToLogin.Close();
            else if (boolExpense) refToExpense.Close();
            this.Close();                 
        }
       
    }
}
