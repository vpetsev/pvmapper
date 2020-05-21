namespace PVDESKTOP
{
    partial class frmObtimize
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmObtimize));
            this.TabGOptimize = new System.Windows.Forms.TabControl();
            this.tabPage13 = new System.Windows.Forms.TabPage();
            this.zedGOpti1 = new ZedGraph.ZedGraphControl();
            this.tabPage12 = new System.Windows.Forms.TabPage();
            this.zedGOpti2 = new ZedGraph.ZedGraphControl();
            this.tabPage14 = new System.Windows.Forms.TabPage();
            this.zedGOpti3 = new ZedGraph.ZedGraphControl();
            this.tabPage15 = new System.Windows.Forms.TabPage();
            this.zedGOpti4 = new ZedGraph.ZedGraphControl();
            this.cmdOptimization = new System.Windows.Forms.Button();
            this.UpdateProgressBar = new System.Windows.Forms.ProgressBar();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pvAz = new pvPanel3DAngleCtl.pvPanelCompassCtl();
            this.pvTilt = new pvPanel3DAngleCtl.pvPanelAngle();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.TabGOptimize.SuspendLayout();
            this.tabPage13.SuspendLayout();
            this.tabPage12.SuspendLayout();
            this.tabPage14.SuspendLayout();
            this.tabPage15.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // TabGOptimize
            // 
            this.TabGOptimize.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.TabGOptimize.Controls.Add(this.tabPage1);
            this.TabGOptimize.Controls.Add(this.tabPage13);
            this.TabGOptimize.Controls.Add(this.tabPage12);
            this.TabGOptimize.Controls.Add(this.tabPage14);
            this.TabGOptimize.Controls.Add(this.tabPage15);
            this.TabGOptimize.Location = new System.Drawing.Point(102, 80);
            this.TabGOptimize.Multiline = true;
            this.TabGOptimize.Name = "TabGOptimize";
            this.TabGOptimize.SelectedIndex = 0;
            this.TabGOptimize.Size = new System.Drawing.Size(405, 285);
            this.TabGOptimize.TabIndex = 12;
            // 
            // tabPage13
            // 
            this.tabPage13.Controls.Add(this.zedGOpti1);
            this.tabPage13.Location = new System.Drawing.Point(4, 4);
            this.tabPage13.Name = "tabPage13";
            this.tabPage13.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage13.Size = new System.Drawing.Size(804, 419);
            this.tabPage13.TabIndex = 1;
            this.tabPage13.Text = "Chart1";
            this.tabPage13.UseVisualStyleBackColor = true;
            // 
            // zedGOpti1
            // 
            this.zedGOpti1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGOpti1.Location = new System.Drawing.Point(0, 0);
            this.zedGOpti1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.zedGOpti1.Name = "zedGOpti1";
            this.zedGOpti1.ScrollGrace = 0D;
            this.zedGOpti1.ScrollMaxX = 0D;
            this.zedGOpti1.ScrollMaxY = 0D;
            this.zedGOpti1.ScrollMaxY2 = 0D;
            this.zedGOpti1.ScrollMinX = 0D;
            this.zedGOpti1.ScrollMinY = 0D;
            this.zedGOpti1.ScrollMinY2 = 0D;
            this.zedGOpti1.Size = new System.Drawing.Size(801, 411);
            this.zedGOpti1.TabIndex = 10;
            // 
            // tabPage12
            // 
            this.tabPage12.Controls.Add(this.zedGOpti2);
            this.tabPage12.Location = new System.Drawing.Point(4, 4);
            this.tabPage12.Name = "tabPage12";
            this.tabPage12.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage12.Size = new System.Drawing.Size(804, 419);
            this.tabPage12.TabIndex = 2;
            this.tabPage12.Text = "Chart2";
            this.tabPage12.UseVisualStyleBackColor = true;
            // 
            // zedGOpti2
            // 
            this.zedGOpti2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGOpti2.Location = new System.Drawing.Point(0, 0);
            this.zedGOpti2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.zedGOpti2.Name = "zedGOpti2";
            this.zedGOpti2.ScrollGrace = 0D;
            this.zedGOpti2.ScrollMaxX = 0D;
            this.zedGOpti2.ScrollMaxY = 0D;
            this.zedGOpti2.ScrollMaxY2 = 0D;
            this.zedGOpti2.ScrollMinX = 0D;
            this.zedGOpti2.ScrollMinY = 0D;
            this.zedGOpti2.ScrollMinY2 = 0D;
            this.zedGOpti2.Size = new System.Drawing.Size(801, 411);
            this.zedGOpti2.TabIndex = 11;
            // 
            // tabPage14
            // 
            this.tabPage14.Controls.Add(this.zedGOpti3);
            this.tabPage14.Location = new System.Drawing.Point(4, 4);
            this.tabPage14.Name = "tabPage14";
            this.tabPage14.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage14.Size = new System.Drawing.Size(804, 419);
            this.tabPage14.TabIndex = 3;
            this.tabPage14.Text = "Chart3";
            this.tabPage14.UseVisualStyleBackColor = true;
            // 
            // zedGOpti3
            // 
            this.zedGOpti3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGOpti3.Location = new System.Drawing.Point(0, 0);
            this.zedGOpti3.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.zedGOpti3.Name = "zedGOpti3";
            this.zedGOpti3.ScrollGrace = 0D;
            this.zedGOpti3.ScrollMaxX = 0D;
            this.zedGOpti3.ScrollMaxY = 0D;
            this.zedGOpti3.ScrollMaxY2 = 0D;
            this.zedGOpti3.ScrollMinX = 0D;
            this.zedGOpti3.ScrollMinY = 0D;
            this.zedGOpti3.ScrollMinY2 = 0D;
            this.zedGOpti3.Size = new System.Drawing.Size(801, 411);
            this.zedGOpti3.TabIndex = 11;
            // 
            // tabPage15
            // 
            this.tabPage15.Controls.Add(this.zedGOpti4);
            this.tabPage15.Location = new System.Drawing.Point(4, 4);
            this.tabPage15.Name = "tabPage15";
            this.tabPage15.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage15.Size = new System.Drawing.Size(804, 419);
            this.tabPage15.TabIndex = 4;
            this.tabPage15.Text = "Chart4";
            this.tabPage15.UseVisualStyleBackColor = true;
            // 
            // zedGOpti4
            // 
            this.zedGOpti4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGOpti4.Location = new System.Drawing.Point(0, 0);
            this.zedGOpti4.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.zedGOpti4.Name = "zedGOpti4";
            this.zedGOpti4.ScrollGrace = 0D;
            this.zedGOpti4.ScrollMaxX = 0D;
            this.zedGOpti4.ScrollMaxY = 0D;
            this.zedGOpti4.ScrollMaxY2 = 0D;
            this.zedGOpti4.ScrollMinX = 0D;
            this.zedGOpti4.ScrollMinY = 0D;
            this.zedGOpti4.ScrollMinY2 = 0D;
            this.zedGOpti4.Size = new System.Drawing.Size(801, 411);
            this.zedGOpti4.TabIndex = 11;
            // 
            // cmdOptimization
            // 
            this.cmdOptimization.Enabled = false;
            this.cmdOptimization.Image = ((System.Drawing.Image)(resources.GetObject("cmdOptimization.Image")));
            this.cmdOptimization.Location = new System.Drawing.Point(288, 210);
            this.cmdOptimization.Name = "cmdOptimization";
            this.cmdOptimization.Size = new System.Drawing.Size(66, 69);
            this.cmdOptimization.TabIndex = 13;
            this.cmdOptimization.UseVisualStyleBackColor = true;
            this.cmdOptimization.Click += new System.EventHandler(this.cmdOptimization_Click_1);
            // 
            // UpdateProgressBar
            // 
            this.UpdateProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UpdateProgressBar.Location = new System.Drawing.Point(238, 425);
            this.UpdateProgressBar.Name = "UpdateProgressBar";
            this.UpdateProgressBar.Size = new System.Drawing.Size(569, 18);
            this.UpdateProgressBar.TabIndex = 14;
            this.UpdateProgressBar.Visible = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.pvTilt);
            this.tabPage1.Controls.Add(this.pvAz);
            this.tabPage1.Controls.Add(this.cmdOptimization);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(397, 259);
            this.tabPage1.TabIndex = 5;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pvAz
            // 
            this.pvAz.AzimutAngle = 0F;
            this.pvAz.BackColor = System.Drawing.Color.White;
            this.pvAz.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pvAz.BackgroundImage")));
            this.pvAz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pvAz.Location = new System.Drawing.Point(8, 169);
            this.pvAz.Name = "pvAz";
            this.pvAz.Size = new System.Drawing.Size(100, 125);
            this.pvAz.TabIndex = 0;
            // 
            // pvTilt
            // 
            this.pvTilt.BackColor = System.Drawing.Color.White;
            this.pvTilt.Location = new System.Drawing.Point(121, 149);
            this.pvTilt.Name = "pvTilt";
            this.pvTilt.Size = new System.Drawing.Size(135, 145);
            this.pvTilt.TabIndex = 14;
            this.pvTilt.tiltAngle = 0F;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(619, 140);
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // frmObtimize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 445);
            this.Controls.Add(this.UpdateProgressBar);
            this.Controls.Add(this.TabGOptimize);
            this.Name = "frmObtimize";
            this.Text = "frmObtimize";
            this.TabGOptimize.ResumeLayout(false);
            this.tabPage13.ResumeLayout(false);
            this.tabPage12.ResumeLayout(false);
            this.tabPage14.ResumeLayout(false);
            this.tabPage15.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabGOptimize;
        private System.Windows.Forms.TabPage tabPage13;
        private ZedGraph.ZedGraphControl zedGOpti1;
        private System.Windows.Forms.TabPage tabPage12;
        private ZedGraph.ZedGraphControl zedGOpti2;
        private System.Windows.Forms.TabPage tabPage14;
        private ZedGraph.ZedGraphControl zedGOpti3;
        private System.Windows.Forms.TabPage tabPage15;
        private ZedGraph.ZedGraphControl zedGOpti4;
        private System.Windows.Forms.Button cmdOptimization;
        private System.Windows.Forms.ProgressBar UpdateProgressBar;
        private System.Windows.Forms.TabPage tabPage1;
        private pvPanel3DAngleCtl.pvPanelAngle pvTilt;
        private pvPanel3DAngleCtl.pvPanelCompassCtl pvAz;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}