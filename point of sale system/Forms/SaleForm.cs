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
    public partial class SaleForm : UserControl
    {
        public event EventHandler ReturnToMainForm;

        public SaleForm()
        {
            InitializeComponent();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            QuickList quickList = new QuickList();
            quickList.ShowDialog();
        }

        private void SaleForm_Load(object sender, EventArgs e)
        {

        }

        
    }
}
