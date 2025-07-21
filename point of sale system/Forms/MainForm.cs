using point_of_sale_system.Forms;
using point_of_sale_system.Models;
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
    public partial class mainFrm : Form
    {
        private SaleInfo sharedSaleInfo;
        private string userRole;
        public mainFrm()
        {
            InitializeComponent();
            InitializeComponent();
            wlcmlbl.Text = UserSession.CurrentUsername ?? "Guest";
        }

        public mainFrm(string role)
        {
            InitializeComponent();
            userRole = role;
            wlcmlbl.Text = UserSession.CurrentUsername.ToString();
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
           

            var saleForm = new saleFrm(sharedSaleInfo);
            saleForm.Show();
            this.Hide();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {

            saleMngFrm saleMng = new saleMngFrm(UserSession.CurrentUserRole);
            saleMng.Show();
            this.Hide();

        }

        private void exitbtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mainFrm_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LogInFrm login = new LogInFrm();
            login.Show();
            this.Close();
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AboutUS about = new AboutUS();
            about.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Contact contact = new Contact();
            contact.Show();
            this.Hide();
            this.Hide();
        }
    }
}
