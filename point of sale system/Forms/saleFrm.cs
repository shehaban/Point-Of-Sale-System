using point_of_sale_system.Forms;
using System;
using System.Windows.Forms;

namespace point_of_sale_system
{
    public partial class saleFrm : Form
    {
        public saleFrm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Set form to full screen without changing any other properties
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
        }

        // Keep all your existing methods exactly as they are
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