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
        public mainFrm()
        {
            InitializeComponent();
            
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            saleFrm sale = new saleFrm();
            sale.Show();
            this.Hide();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            saleMngFrm saleMng = new saleMngFrm();
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
    }
}
