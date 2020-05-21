namespace PVDESKTOP
{
    partial class frmPvPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPvPanel));
            this.txtPvWidth = new System.Windows.Forms.TextBox();
            this.txtPvHeight = new System.Windows.Forms.TextBox();
            this.picSolarPanel = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblHeight = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbSolarPanelSize = new System.Windows.Forms.ComboBox();
            this.cmdSetDefault = new System.Windows.Forms.Button();
            this.cmdApplyAllPanel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdCheck4PvOnRoof = new System.Windows.Forms.CheckBox();
            this.txtGridSpacingY = new System.Windows.Forms.TextBox();
            this.label55 = new System.Windows.Forms.Label();
            this.txtGridSpacingX = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grpboxPanelProps = new System.Windows.Forms.GroupBox();
            this.label37 = new System.Windows.Forms.Label();
            this.lblHeight2 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.lblWidth2 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.lblAzimuth2 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.lblTilt2 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picSolarPanel)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpboxPanelProps.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPvWidth
            // 
            this.txtPvWidth.Location = new System.Drawing.Point(53, 47);
            this.txtPvWidth.Name = "txtPvWidth";
            this.txtPvWidth.Size = new System.Drawing.Size(34, 20);
            this.txtPvWidth.TabIndex = 54;
            this.txtPvWidth.Text = "1.0";
            this.txtPvWidth.TextChanged += new System.EventHandler(this.txtPvWidth_TextChanged);
            // 
            // txtPvHeight
            // 
            this.txtPvHeight.Location = new System.Drawing.Point(53, 71);
            this.txtPvHeight.Name = "txtPvHeight";
            this.txtPvHeight.Size = new System.Drawing.Size(34, 20);
            this.txtPvHeight.TabIndex = 55;
            this.txtPvHeight.Text = "2.0";
            this.txtPvHeight.TextChanged += new System.EventHandler(this.txtPvHeight_TextChanged);
            // 
            // picSolarPanel
            // 
            this.picSolarPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picSolarPanel.BackgroundImage")));
            this.picSolarPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picSolarPanel.Location = new System.Drawing.Point(323, 62);
            this.picSolarPanel.Name = "picSolarPanel";
            this.picSolarPanel.Size = new System.Drawing.Size(79, 113);
            this.picSolarPanel.TabIndex = 53;
            this.picSolarPanel.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 56;
            this.label1.Text = "Width";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 56;
            this.label2.Text = "Height";
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(461, 119);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(35, 13);
            this.lblHeight.TabIndex = 57;
            this.lblHeight.Text = "label3";
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(420, 119);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(35, 13);
            this.lblWidth.TabIndex = 57;
            this.lblWidth.Text = "label3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(98, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 13);
            this.label5.TabIndex = 57;
            this.label5.Text = "m";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(98, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 13);
            this.label6.TabIndex = 57;
            this.label6.Text = "m";
            // 
            // cmbSolarPanelSize
            // 
            this.cmbSolarPanelSize.FormattingEnabled = true;
            this.cmbSolarPanelSize.Items.AddRange(new object[] {
            "1956X992",
            "1640X992",
            "1580X808",
            "1482X676",
            "1196X534",
            "1194X676",
            "638X278",
            "634X534",
            "559X407",
            "525X278",
            "450X278",
            ""});
            this.cmbSolarPanelSize.Location = new System.Drawing.Point(12, 23);
            this.cmbSolarPanelSize.Name = "cmbSolarPanelSize";
            this.cmbSolarPanelSize.Size = new System.Drawing.Size(80, 21);
            this.cmbSolarPanelSize.TabIndex = 58;
            this.cmbSolarPanelSize.SelectedIndexChanged += new System.EventHandler(this.cmbSolarPanelSize_SelectedIndexChanged);
            // 
            // cmdSetDefault
            // 
            this.cmdSetDefault.Location = new System.Drawing.Point(423, 81);
            this.cmdSetDefault.Name = "cmdSetDefault";
            this.cmdSetDefault.Size = new System.Drawing.Size(111, 23);
            this.cmdSetDefault.TabIndex = 59;
            this.cmdSetDefault.Text = "Set Default Size";
            this.cmdSetDefault.UseVisualStyleBackColor = true;
            this.cmdSetDefault.Click += new System.EventHandler(this.cmdSetDefault_Click);
            // 
            // cmdApplyAllPanel
            // 
            this.cmdApplyAllPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdApplyAllPanel.Location = new System.Drawing.Point(232, 131);
            this.cmdApplyAllPanel.Name = "cmdApplyAllPanel";
            this.cmdApplyAllPanel.Size = new System.Drawing.Size(49, 23);
            this.cmdApplyAllPanel.TabIndex = 59;
            this.cmdApplyAllPanel.Text = "Apply";
            this.cmdApplyAllPanel.UseVisualStyleBackColor = true;
            this.cmdApplyAllPanel.Click += new System.EventHandler(this.cmdApplyAllPanel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(98, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 57;
            this.label3.Text = "mm";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(337, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 56;
            this.label4.Text = "Panel Size";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(420, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 13);
            this.label7.TabIndex = 60;
            this.label7.Text = "Standard Dimensions";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdCheck4PvOnRoof);
            this.groupBox1.Controls.Add(this.txtGridSpacingY);
            this.groupBox1.Controls.Add(this.label55);
            this.groupBox1.Controls.Add(this.txtGridSpacingX);
            this.groupBox1.Controls.Add(this.label45);
            this.groupBox1.Controls.Add(this.label48);
            this.groupBox1.Controls.Add(this.label54);
            this.groupBox1.Location = new System.Drawing.Point(10, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(122, 100);
            this.groupBox1.TabIndex = 206;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Spacing";
            // 
            // cmdCheck4PvOnRoof
            // 
            this.cmdCheck4PvOnRoof.AutoSize = true;
            this.cmdCheck4PvOnRoof.Checked = true;
            this.cmdCheck4PvOnRoof.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cmdCheck4PvOnRoof.Location = new System.Drawing.Point(19, 65);
            this.cmdCheck4PvOnRoof.Name = "cmdCheck4PvOnRoof";
            this.cmdCheck4PvOnRoof.Size = new System.Drawing.Size(78, 17);
            this.cmdCheck4PvOnRoof.TabIndex = 212;
            this.cmdCheck4PvOnRoof.Text = "Orthogonal";
            this.cmdCheck4PvOnRoof.UseVisualStyleBackColor = true;
            this.cmdCheck4PvOnRoof.CheckedChanged += new System.EventHandler(this.cmdCheck4PvOnRoof_CheckedChanged);
            // 
            // txtGridSpacingY
            // 
            this.txtGridSpacingY.Location = new System.Drawing.Point(66, 44);
            this.txtGridSpacingY.Name = "txtGridSpacingY";
            this.txtGridSpacingY.Size = new System.Drawing.Size(28, 20);
            this.txtGridSpacingY.TabIndex = 208;
            this.txtGridSpacingY.Text = "5";
            this.txtGridSpacingY.TextChanged += new System.EventHandler(this.txtGridSpacingY_TextChanged);
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(94, 47);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(15, 13);
            this.label55.TabIndex = 210;
            this.label55.Text = "m";
            // 
            // txtGridSpacingX
            // 
            this.txtGridSpacingX.Location = new System.Drawing.Point(66, 22);
            this.txtGridSpacingX.Name = "txtGridSpacingX";
            this.txtGridSpacingX.Size = new System.Drawing.Size(28, 20);
            this.txtGridSpacingX.TabIndex = 209;
            this.txtGridSpacingX.Text = "3";
            this.txtGridSpacingX.TextChanged += new System.EventHandler(this.txtGridSpacingX_TextChanged);
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(16, 49);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(42, 13);
            this.label45.TabIndex = 206;
            this.label45.Text = "Vertical";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(14, 26);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(54, 13);
            this.label48.TabIndex = 207;
            this.label48.Text = "Horizontal";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(94, 25);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(15, 13);
            this.label54.TabIndex = 211;
            this.label54.Text = "m";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbSolarPanelSize);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtPvHeight);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtPvWidth);
            this.groupBox2.Location = new System.Drawing.Point(140, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(131, 100);
            this.groupBox2.TabIndex = 207;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dimensions";
            // 
            // grpboxPanelProps
            // 
            this.grpboxPanelProps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.grpboxPanelProps.Controls.Add(this.label37);
            this.grpboxPanelProps.Controls.Add(this.lblHeight2);
            this.grpboxPanelProps.Controls.Add(this.label39);
            this.grpboxPanelProps.Controls.Add(this.lblWidth2);
            this.grpboxPanelProps.Controls.Add(this.label41);
            this.grpboxPanelProps.Controls.Add(this.lblAzimuth2);
            this.grpboxPanelProps.Controls.Add(this.label43);
            this.grpboxPanelProps.Controls.Add(this.label44);
            this.grpboxPanelProps.Controls.Add(this.lblTilt2);
            this.grpboxPanelProps.Controls.Add(this.label46);
            this.grpboxPanelProps.Controls.Add(this.label47);
            this.grpboxPanelProps.Controls.Add(this.label8);
            this.grpboxPanelProps.Location = new System.Drawing.Point(11, 116);
            this.grpboxPanelProps.Name = "grpboxPanelProps";
            this.grpboxPanelProps.Size = new System.Drawing.Size(212, 40);
            this.grpboxPanelProps.TabIndex = 208;
            this.grpboxPanelProps.TabStop = false;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(3, 9);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(24, 13);
            this.label37.TabIndex = 183;
            this.label37.Text = "Tilt:";
            // 
            // lblHeight2
            // 
            this.lblHeight2.AutoSize = true;
            this.lblHeight2.Location = new System.Drawing.Point(160, 23);
            this.lblHeight2.Name = "lblHeight2";
            this.lblHeight2.Size = new System.Drawing.Size(13, 13);
            this.lblHeight2.TabIndex = 189;
            this.lblHeight2.Text = "1";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(73, 22);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(27, 13);
            this.label39.TabIndex = 182;
            this.label39.Text = "Deg";
            // 
            // lblWidth2
            // 
            this.lblWidth2.AutoSize = true;
            this.lblWidth2.Location = new System.Drawing.Point(160, 10);
            this.lblWidth2.Name = "lblWidth2";
            this.lblWidth2.Size = new System.Drawing.Size(13, 13);
            this.lblWidth2.TabIndex = 188;
            this.lblWidth2.Text = "1";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(73, 9);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(27, 13);
            this.label41.TabIndex = 180;
            this.label41.Text = "Deg";
            // 
            // lblAzimuth2
            // 
            this.lblAzimuth2.AutoSize = true;
            this.lblAzimuth2.Location = new System.Drawing.Point(47, 22);
            this.lblAzimuth2.Name = "lblAzimuth2";
            this.lblAzimuth2.Size = new System.Drawing.Size(13, 13);
            this.lblAzimuth2.TabIndex = 179;
            this.lblAzimuth2.Text = "1";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(119, 9);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(38, 13);
            this.label43.TabIndex = 184;
            this.label43.Text = "Width:";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(195, 23);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(15, 13);
            this.label44.TabIndex = 187;
            this.label44.Text = "m";
            // 
            // lblTilt2
            // 
            this.lblTilt2.AutoSize = true;
            this.lblTilt2.Location = new System.Drawing.Point(47, 9);
            this.lblTilt2.Name = "lblTilt2";
            this.lblTilt2.Size = new System.Drawing.Size(13, 13);
            this.lblTilt2.TabIndex = 178;
            this.lblTilt2.Text = "1";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(119, 23);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(41, 13);
            this.label46.TabIndex = 185;
            this.label46.Text = "Height:";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(195, 9);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(15, 13);
            this.label47.TabIndex = 186;
            this.label47.Text = "m";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 177;
            this.label8.Text = "Azimuth:";
            // 
            // frmPvPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(293, 166);
            this.Controls.Add(this.grpboxPanelProps);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblHeight);
            this.Controls.Add(this.cmdApplyAllPanel);
            this.Controls.Add(this.lblWidth);
            this.Controls.Add(this.cmdSetDefault);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.picSolarPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(309, 204);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(309, 204);
            this.Name = "frmPvPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Panel Spacing and Size Properties";
            this.Load += new System.EventHandler(this.frmPvPanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picSolarPanel)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpboxPanelProps.ResumeLayout(false);
            this.grpboxPanelProps.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPvWidth;
        private System.Windows.Forms.TextBox txtPvHeight;
        private System.Windows.Forms.PictureBox picSolarPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbSolarPanelSize;
        private System.Windows.Forms.Button cmdSetDefault;
        private System.Windows.Forms.Button cmdApplyAllPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cmdCheck4PvOnRoof;
        private System.Windows.Forms.TextBox txtGridSpacingY;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.TextBox txtGridSpacingX;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox grpboxPanelProps;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label lblHeight2;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label lblWidth2;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label lblAzimuth2;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label lblTilt2;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label8;
    }
}