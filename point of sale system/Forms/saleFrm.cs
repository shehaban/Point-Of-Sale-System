using point_of_sale_system.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace point_of_sale_system
{
    public partial class saleFrm : Form
    {
        public saleFrm()
        {
            InitializeComponent();
        }

        private void homebtn_Click(object sender, EventArgs e)
        {
            mainFrm mainForm = new mainFrm();
            mainForm.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            QuickList quickList = new QuickList();
            quickList.ShowDialog();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            InvoiceForm invoice = new InvoiceForm();
            invoice.ShowDialog();
        }
    }
}
