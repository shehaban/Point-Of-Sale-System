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
    public partial class saleMngFrm : Form
    {
        private List<Button> navButtons;
        public saleMngFrm()
        {
            InitializeComponent();

            navButtons = new List<Button>() { btnProducts, btnInventory, btnSalesMng, mngbtn, homebtn };
            
            btnProducts.Click += btnProducts_Click;
        }

        private void SetActiveButton(Button activeButton)
        {
            foreach (Button btn in navButtons)
            {
                btn.BackColor = Color.LightGray; 
            }

            activeButton.BackColor = Color.SteelBlue; 
        }

        public void showUserControl(UserControl userControl)
        {
            mainPanel.Controls.Clear();
            userControl.Dock = DockStyle.Fill; 
            mainPanel.Controls.Add(userControl);

        }


        private void btnProducts_Click(object sender, EventArgs e)
        {
            showUserControl(new Products());
            SetActiveButton(btnProducts);
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            showUserControl(new Inventory());
            SetActiveButton(btnInventory);
        }

        private void btnSalesMng_Click(object sender, EventArgs e)
        {
            showUserControl(new SaleInfo());
            SetActiveButton(btnSalesMng);
        }

        private void mngbtn_Click(object sender, EventArgs e)
        {
            showUserControl(new passwordMng());
            SetActiveButton(mngbtn);
        }

        private void homebtn_Click(object sender, EventArgs e)
        {
            mainFrm mainForm = new mainFrm();
            mainForm.Show();
            this.Close();
        }

        private void btnProducts_Click_1(object sender, EventArgs e)
        {
            showUserControl(new Products());
            SetActiveButton(btnProducts);
        }
    }
}
