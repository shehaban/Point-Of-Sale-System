using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;

namespace point_of_sale_system
{
    partial class mainFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainFrm));
            this.mainPanel = new System.Windows.Forms.Panel();
            this.featurePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSale = new System.Windows.Forms.Button();
            this.btnSaleMng = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnSupport = new System.Windows.Forms.Button();
            this.sidebarPanel = new System.Windows.Forms.Panel();
            this.userProfilePanel = new System.Windows.Forms.Panel();
            this.wlcmlbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.profilePicture = new System.Windows.Forms.PictureBox();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.exitbtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.topPanel = new System.Windows.Forms.Panel();
            this.logoPanel = new System.Windows.Forms.Panel();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.mainPanel.SuspendLayout();
            this.featurePanel.SuspendLayout();
            this.sidebarPanel.SuspendLayout();
            this.userProfilePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profilePicture)).BeginInit();
            this.bottomPanel.SuspendLayout();
            this.topPanel.SuspendLayout();
            this.logoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.mainPanel.Controls.Add(this.featurePanel);
            this.mainPanel.Controls.Add(this.sidebarPanel);
            this.mainPanel.Controls.Add(this.topPanel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1200, 800);
            this.mainPanel.TabIndex = 0;
            // 
            // featurePanel
            // 
            this.featurePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.featurePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.featurePanel.Controls.Add(this.btnSale);
            this.featurePanel.Controls.Add(this.btnSaleMng);
            this.featurePanel.Controls.Add(this.btnAbout);
            this.featurePanel.Controls.Add(this.btnSupport);
            this.featurePanel.Location = new System.Drawing.Point(250, 100);
            this.featurePanel.Name = "featurePanel";
            this.featurePanel.Padding = new System.Windows.Forms.Padding(50);
            this.featurePanel.Size = new System.Drawing.Size(950, 700);
            this.featurePanel.TabIndex = 2;
            // 
            // btnSale
            // 
            this.btnSale.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(64)))));
            this.btnSale.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSale.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnSale.FlatAppearance.BorderSize = 2;
            this.btnSale.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnSale.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(64)))));
            this.btnSale.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSale.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSale.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnSale.Image = ((System.Drawing.Image)(resources.GetObject("btnSale.Image")));
            this.btnSale.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSale.Location = new System.Drawing.Point(53, 53);
            this.btnSale.Name = "btnSale";
            this.btnSale.Size = new System.Drawing.Size(250, 250);
            this.btnSale.TabIndex = 0;
            this.btnSale.Text = "POINT OF SALE";
            this.btnSale.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSale.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSale.UseVisualStyleBackColor = false;
            this.btnSale.Click += new System.EventHandler(this.btnSale_Click);
            // 
            // btnSaleMng
            // 
            this.btnSaleMng.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(64)))));
            this.btnSaleMng.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaleMng.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnSaleMng.FlatAppearance.BorderSize = 2;
            this.btnSaleMng.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnSaleMng.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(64)))));
            this.btnSaleMng.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaleMng.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaleMng.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnSaleMng.Image = ((System.Drawing.Image)(resources.GetObject("btnSaleMng.Image")));
            this.btnSaleMng.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSaleMng.Location = new System.Drawing.Point(359, 53);
            this.btnSaleMng.Name = "btnSaleMng";
            this.btnSaleMng.Size = new System.Drawing.Size(250, 250);
            this.btnSaleMng.TabIndex = 1;
            this.btnSaleMng.Text = "POS MANAGEMENT";
            this.btnSaleMng.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSaleMng.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaleMng.UseVisualStyleBackColor = false;
            this.btnSaleMng.Click += new System.EventHandler(this.btnProducts_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(64)))));
            this.btnAbout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAbout.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnAbout.FlatAppearance.BorderSize = 2;
            this.btnAbout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnAbout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(64)))));
            this.btnAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbout.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnAbout.Image = ((System.Drawing.Image)(resources.GetObject("btnAbout.Image")));
            this.btnAbout.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAbout.Location = new System.Drawing.Point(53, 359);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(250, 250);
            this.btnAbout.TabIndex = 2;
            this.btnAbout.Text = "ABOUT US";
            this.btnAbout.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAbout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAbout.UseVisualStyleBackColor = false;
            this.btnAbout.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnSupport
            // 
            this.btnSupport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(64)))));
            this.btnSupport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSupport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnSupport.FlatAppearance.BorderSize = 2;
            this.btnSupport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnSupport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(64)))));
            this.btnSupport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSupport.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSupport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnSupport.Image = ((System.Drawing.Image)(resources.GetObject("btnSupport.Image")));
            this.btnSupport.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSupport.Location = new System.Drawing.Point(359, 359);
            this.btnSupport.Name = "btnSupport";
            this.btnSupport.Size = new System.Drawing.Size(250, 250);
            this.btnSupport.TabIndex = 3;
            this.btnSupport.Text = "SUPPORT";
            this.btnSupport.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSupport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSupport.UseVisualStyleBackColor = false;
            this.btnSupport.Click += new System.EventHandler(this.button3_Click);
            // 
            // sidebarPanel
            // 
            this.sidebarPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(20)))), ((int)(((byte)(36)))));
            this.sidebarPanel.Controls.Add(this.userProfilePanel);
            this.sidebarPanel.Controls.Add(this.bottomPanel);
            this.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarPanel.Location = new System.Drawing.Point(0, 100);
            this.sidebarPanel.Name = "sidebarPanel";
            this.sidebarPanel.Size = new System.Drawing.Size(250, 700);
            this.sidebarPanel.TabIndex = 1;
            // 
            // userProfilePanel
            // 
            this.userProfilePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(20)))), ((int)(((byte)(36)))));
            this.userProfilePanel.Controls.Add(this.wlcmlbl);
            this.userProfilePanel.Controls.Add(this.label1);
            this.userProfilePanel.Controls.Add(this.profilePicture);
            this.userProfilePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.userProfilePanel.Location = new System.Drawing.Point(0, 0);
            this.userProfilePanel.Name = "userProfilePanel";
            this.userProfilePanel.Size = new System.Drawing.Size(250, 250);
            this.userProfilePanel.TabIndex = 1;
            // 
            // wlcmlbl
            // 
            this.wlcmlbl.AutoSize = true;
            this.wlcmlbl.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wlcmlbl.ForeColor = System.Drawing.Color.White;
            this.wlcmlbl.Location = new System.Drawing.Point(25, 180);
            this.wlcmlbl.Name = "wlcmlbl";
            this.wlcmlbl.Size = new System.Drawing.Size(0, 38);
            this.wlcmlbl.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.label1.Location = new System.Drawing.Point(25, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 41);
            this.label1.TabIndex = 1;
            this.label1.Text = "WELCOME";
            // 
            // profilePicture
            // 
            this.profilePicture.Image = ((System.Drawing.Image)(resources.GetObject("profilePicture.Image")));
            this.profilePicture.Location = new System.Drawing.Point(50, 20);
            this.profilePicture.Name = "profilePicture";
            this.profilePicture.Size = new System.Drawing.Size(150, 150);
            this.profilePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.profilePicture.TabIndex = 0;
            this.profilePicture.TabStop = false;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.exitbtn);
            this.bottomPanel.Controls.Add(this.button1);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 600);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(250, 100);
            this.bottomPanel.TabIndex = 0;
            // 
            // exitbtn
            // 
            this.exitbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(64)))));
            this.exitbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exitbtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.exitbtn.FlatAppearance.BorderSize = 2;
            this.exitbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitbtn.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitbtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.exitbtn.Location = new System.Drawing.Point(25, 50);
            this.exitbtn.Name = "exitbtn";
            this.exitbtn.Size = new System.Drawing.Size(200, 40);
            this.exitbtn.TabIndex = 1;
            this.exitbtn.Text = "EXIT SYSTEM";
            this.exitbtn.UseVisualStyleBackColor = false;
            this.exitbtn.Click += new System.EventHandler(this.exitbtn_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(64)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.button1.Location = new System.Drawing.Point(25, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(200, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "LOGOUT";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(20)))), ((int)(((byte)(36)))));
            this.topPanel.Controls.Add(this.logoPanel);
            this.topPanel.Controls.Add(this.titleLabel);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1200, 100);
            this.topPanel.TabIndex = 0;
            // 
            // logoPanel
            // 
            this.logoPanel.Controls.Add(this.logoPictureBox);
            this.logoPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.logoPanel.Location = new System.Drawing.Point(0, 0);
            this.logoPanel.Name = "logoPanel";
            this.logoPanel.Size = new System.Drawing.Size(250, 100);
            this.logoPanel.TabIndex = 1;
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logoPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("logoPictureBox.Image")));
            this.logoPictureBox.Location = new System.Drawing.Point(0, 0);
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.Size = new System.Drawing.Size(250, 100);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logoPictureBox.TabIndex = 0;
            this.logoPictureBox.TabStop = false;
            // 
            // titleLabel
            // 
            this.titleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.titleLabel.Location = new System.Drawing.Point(250, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(950, 100);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "POS SYSTEM";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "mainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POS System";
            this.Load += new System.EventHandler(this.mainFrm_Load);
            this.mainPanel.ResumeLayout(false);
            this.featurePanel.ResumeLayout(false);
            this.sidebarPanel.ResumeLayout(false);
            this.userProfilePanel.ResumeLayout(false);
            this.userProfilePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profilePicture)).EndInit();
            this.bottomPanel.ResumeLayout(false);
            this.topPanel.ResumeLayout(false);
            this.logoPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.ResumeLayout(false);

            // Load and resize images (example sizes)
            this.btnSale.Image = ResizeImage(Properties.Resources.point_of_sale_icon_28__1_, 180, 160);
            this.btnSaleMng.Image = ResizeImage(Properties.Resources.sale_mangment, 180, 160);
            this.btnAbout.Image = ResizeImage(Properties.Resources.about_us_icon, 180, 160);
            this.btnSupport.Image = ResizeImage(Properties.Resources.contact, 180, 160);

        }

        private Image ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Panel sidebarPanel;
        private System.Windows.Forms.Panel userProfilePanel;
        private System.Windows.Forms.Label wlcmlbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox profilePicture;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Button exitbtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel featurePanel;
        private System.Windows.Forms.Button btnSale;
        private System.Windows.Forms.Button btnSaleMng;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Button btnSupport;
        private System.Windows.Forms.Panel logoPanel;
        private System.Windows.Forms.PictureBox logoPictureBox;
    }
}