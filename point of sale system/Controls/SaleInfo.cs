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
    public partial class SaleInfo : UserControl
    {
        public SaleInfo()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            mainFrm mainFrm = new mainFrm();
            mainFrm.ShowDialog();
            this.Hide();
        }
    }
}
