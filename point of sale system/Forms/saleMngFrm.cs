using point_of_sale_system.Forms;
using point_of_sale_system.Models;
using point_of_sale_system.Controls;
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
        private SaleInfo sharedSaleInfo; 
        public SaleInfo SharedSaleInfo => sharedSaleInfo; 
        private List<Button> navButtons;
        private string currentUserRole; 
        public saleMngFrm(string role)
        {
            InitializeComponent();

            navButtons = new List<Button>() { btnProducts, btnInventory, btnSalesMng, mngbtn, homebtn, button1 };

            currentUserRole = role; 

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

        public void SharedSaleInfo_ClickFake()
        {
            if (sharedSaleInfo == null)
            {
                sharedSaleInfo = new SaleInfo();
            }
        }


        public void showUserControl(UserControl userControl)
        {
            mainPanel.Controls.Clear();
            userControl.Dock = DockStyle.Fill; 
            mainPanel.Controls.Add(userControl);

        }


        private void btnProducts_Click(object sender, EventArgs e)
        {
            showUserControl(new Inventory());
            SetActiveButton(btnProducts);
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            showUserControl(new ProductMng());
            SetActiveButton(btnInventory);
        }

        private void btnSalesMng_Click(object sender, EventArgs e)
        {
            if (sharedSaleInfo == null)
                sharedSaleInfo = new SaleInfo();

            showUserControl(sharedSaleInfo); 
            SetActiveButton(btnSalesMng);
        }

        private void mngbtn_Click(object sender, EventArgs e)
        {
            showUserControl(new UserMng(currentUserRole));
            SetActiveButton(mngbtn);
        }

        private void homebtn_Click(object sender, EventArgs e)
        {
            mainFrm mainForm = new mainFrm(UserSession.CurrentUserRole);
            mainForm.Show();
            this.Close();
        }

        private void btnProducts_Click_1(object sender, EventArgs e)
        {
            showUserControl(new Inventory());
            SetActiveButton(btnProducts);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showUserControl(new MessagesUC());
            SetActiveButton(button1);
        }
    }
}
