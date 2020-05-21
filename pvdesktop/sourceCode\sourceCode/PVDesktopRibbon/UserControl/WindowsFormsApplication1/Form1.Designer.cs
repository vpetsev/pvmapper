namespace WindowsFormsApplication1
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pvPanelCompassCtl2 = new pvPanel3DAngleCtl.pvPanelPoleGridCtl();
            this.pvPanelAxisRotationCtl3 = new pvPanel3DAngleCtl.pvPanelAxisRotationCtl();
            this.pvPanelAngle3 = new pvPanel3DAngleCtl.pvPanelAngle();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pvPanelCompassCtl2
            // 
            this.pvPanelCompassCtl2.RotationAngle = 0F;
            this.pvPanelCompassCtl2.BackColor = System.Drawing.Color.White;
            this.pvPanelCompassCtl2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pvPanelCompassCtl2.BackgroundImage")));
            this.pvPanelCompassCtl2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pvPanelCompassCtl2.Location = new System.Drawing.Point(277, 125);
            this.pvPanelCompassCtl2.Name = "pvPanelCompassCtl2";
            this.pvPanelCompassCtl2.Size = new System.Drawing.Size(100, 125);
            this.pvPanelCompassCtl2.TabIndex = 0;
            // 
            // pvPanelAxisRotationCtl3
            // 
            this.pvPanelAxisRotationCtl3.AxisAngle = 0F;
            this.pvPanelAxisRotationCtl3.BackColor = System.Drawing.Color.White;
            this.pvPanelAxisRotationCtl3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pvPanelAxisRotationCtl3.BackgroundImage")));
            this.pvPanelAxisRotationCtl3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pvPanelAxisRotationCtl3.Location = new System.Drawing.Point(143, 125);
            this.pvPanelAxisRotationCtl3.Name = "pvPanelAxisRotationCtl3";
            this.pvPanelAxisRotationCtl3.Size = new System.Drawing.Size(100, 125);
            this.pvPanelAxisRotationCtl3.TabIndex = 1;
            // 
            // pvPanelAngle3
            // 
            this.pvPanelAngle3.BackColor = System.Drawing.Color.White;
            this.pvPanelAngle3.Location = new System.Drawing.Point(3, 109);
            this.pvPanelAngle3.Name = "pvPanelAngle3";
            this.pvPanelAngle3.Size = new System.Drawing.Size(137, 141);
            this.pvPanelAngle3.TabIndex = 2;
            this.pvPanelAngle3.TileAngle = 0F;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(13, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(143, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(100, 100);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(277, 3);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(100, 100);
            this.pictureBox3.TabIndex = 3;
            this.pictureBox3.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this.pvPanelCompassCtl2);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pvPanelAxisRotationCtl3);
            this.panel1.Controls.Add(this.pvPanelAngle3);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(399, 257);
            this.panel1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 409);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private pvPanel3DAngleCtl.pvPanelAxisRotationCtl pvPanelAxisRotationCtl1;
        private pvPanel3DAngleCtl.pvPanelAngle pvPanelAngle1;
        private pvPanel3DAngleCtl.pvPanelAngle pvPanelAngle2;
        private pvPanel3DAngleCtl.pvPanelAxisRotationCtl pvPanelAxisRotationCtl2;
        private pvPanel3DAngleCtl.pvPanelPoleGridCtl pvPanelCompassCtl1;
        private pvPanel3DAngleCtl.pvPanelPoleGridCtl pvPanelCompassCtl2;
        private pvPanel3DAngleCtl.pvPanelAxisRotationCtl pvPanelAxisRotationCtl3;
        private pvPanel3DAngleCtl.pvPanelAngle pvPanelAngle3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel panel1;
    }
}

