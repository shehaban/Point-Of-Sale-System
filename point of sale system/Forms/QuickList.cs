using point_of_sale_system.DAL;
using point_of_sale_system.Models;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace point_of_sale_system
{
    public partial class QuickList : Form
    {
        private ProductDAL productDAL = new ProductDAL();
        private DataTable products;
        private saleFrm mainSaleForm;

        public QuickList(saleFrm saleForm)
        {
            if (saleForm == null || saleForm.IsDisposed)
            {
                throw new ArgumentException("Main sale form must be provided and valid");
            }

            InitializeComponent();
            mainSaleForm = saleForm;
            LoadProducts();
            SetupForm();
        }

        private void SetupForm()
        {
            this.Text = "Quick Product List";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new Size(1000, 635);
            this.BackColor = Color.FromArgb(246, 246, 248);

            flowLayoutPanel1.BackColor = Color.White;
            flowLayoutPanel1.BorderStyle = BorderStyle.None;
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.WrapContents = true;
            flowLayoutPanel1.Padding = new Padding(15);
            flowLayoutPanel1.Margin = new Padding(0);
        }

        private void LoadProducts()
        {
            try
            {
                products = productDAL.GetAllProducts();
                CreateProductButtons();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void CreateProductButtons()
        {
            flowLayoutPanel1.Controls.Clear();

            foreach (DataRow row in products.Rows)
            {
                Button productButton = new Button
                {
                    Text = $"{row["name"]}\n{Convert.ToDecimal(row["unit_price"]):0.00}",
                    Tag = row["id"],
                    Size = new Size(180, 110),
                    Margin = new Padding(12),
                    Font = new Font("Segoe UI Semibold", 11, FontStyle.Bold),
                    ForeColor = Color.FromArgb(30, 30, 40), 
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand,
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.White,
                    FlatAppearance = { BorderSize = 0 }
                };

                productButton.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, productButton.Width, productButton.Height, 25, 25));

                productButton.Paint += (sender, e) =>
                {
                    Button btn = (Button)sender;
                    var rect = e.ClipRectangle;

                    using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(rect,
                        Color.FromArgb(48, 63, 159),     
                        Color.FromArgb(0, 188, 212),     
                        90f))
                    {
                        e.Graphics.FillRectangle(brush, rect);
                    }

                    using (var textBrush = new SolidBrush(Color.White))
                    {
                        e.Graphics.DrawString(btn.Text.Split('\n')[0],
                            new Font(btn.Font.FontFamily, btn.Font.Size, FontStyle.Bold),
                            textBrush,
                            new RectangleF(0, 15, btn.Width, btn.Height / 2),
                            new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    }

                    using (var priceBrush = new SolidBrush(Color.FromArgb(178, 235, 242))) 
                    {
                        e.Graphics.DrawString(btn.Text.Split('\n')[1],
                            btn.Font,
                            priceBrush,
                            new RectangleF(0, btn.Height / 2 + 10, btn.Width, btn.Height / 2),
                            new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    }
                };

                productButton.MouseEnter += (s, e) =>
                {
                    productButton.BackColor = Color.FromArgb(224, 247, 250); 
                };
                productButton.MouseLeave += (s, e) =>
                {
                    productButton.BackColor = Color.White;
                };

                productButton.Click += ProductButton_Click;

                flowLayoutPanel1.Controls.Add(productButton);
            }
        }

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private void ProductButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            int productId = (int)clickedButton.Tag;

            DataRow[] productRows = products.Select($"id = {productId}");
            if (productRows.Length > 0)
            {
                DataRow productRow = productRows[0];
                Product selectedProduct = new Product
                {
                    id = (int)productRow["id"],
                    name = productRow["name"].ToString(),
                    unit_price = Convert.ToDecimal(productRow["unit_price"])
                };

                clickedButton.BackColor = Color.FromArgb(220, 235, 255);
                clickedButton.Refresh();
                System.Threading.Thread.Sleep(100);
                clickedButton.BackColor = Color.White;

                mainSaleForm.SetSelectedProduct(selectedProduct);
                mainSaleForm.AddToCart(1);
                this.Close();
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
    }
}