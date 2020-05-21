namespace PVDESKTOP
{
    partial class frmAreaBoundry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAreaBoundry));
            this.label1 = new System.Windows.Forms.Label();
            this.btnSaveArea = new System.Windows.Forms.Button();
            this.btnRedrawArea = new System.Windows.Forms.Button();
            this.btnCancelArea = new System.Windows.Forms.Button();
            this.btnAddAnotherArea = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.txtAreaName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(274, 52);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // btnSaveArea
            // 
            this.btnSaveArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveArea.Location = new System.Drawing.Point(183, 108);
            this.btnSaveArea.Name = "btnSaveArea";
            this.btnSaveArea.Size = new System.Drawing.Size(90, 23);
            this.btnSaveArea.TabIndex = 1;
            this.btnSaveArea.Text = "Save and Close";
            this.btnSaveArea.UseVisualStyleBackColor = true;
            this.btnSaveArea.Click += new System.EventHandler(this.btnSaveArea_Click);
            // 
            // btnRedrawArea
            // 
            this.btnRedrawArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRedrawArea.Location = new System.Drawing.Point(102, 108);
            this.btnRedrawArea.Name = "btnRedrawArea";
            this.btnRedrawArea.Size = new System.Drawing.Size(75, 23);
            this.btnRedrawArea.TabIndex = 2;
            this.btnRedrawArea.Text = "Redraw";
            this.btnRedrawArea.UseVisualStyleBackColor = true;
            this.btnRedrawArea.Click += new System.EventHandler(this.btnRedrawArea_Click);
            // 
            // btnCancelArea
            // 
            this.btnCancelArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelArea.Location = new System.Drawing.Point(21, 108);
            this.btnCancelArea.Name = "btnCancelArea";
            this.btnCancelArea.Size = new System.Drawing.Size(75, 23);
            this.btnCancelArea.TabIndex = 3;
            this.btnCancelArea.Text = "Cancel";
            this.btnCancelArea.UseVisualStyleBackColor = true;
            this.btnCancelArea.Visible = false;
            this.btnCancelArea.Click += new System.EventHandler(this.btnCancelArea_Click);
            // 
            // btnAddAnotherArea
            // 
            this.btnAddAnotherArea.Image = ((System.Drawing.Image)(resources.GetObject("btnAddAnotherArea.Image")));
            this.btnAddAnotherArea.Location = new System.Drawing.Point(243, 70);
            this.btnAddAnotherArea.Name = "btnAddAnotherArea";
            this.btnAddAnotherArea.Size = new System.Drawing.Size(27, 27);
            this.btnAddAnotherArea.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnAddAnotherArea, "Add a site area boundry");
            this.btnAddAnotherArea.UseVisualStyleBackColor = true;
            this.btnAddAnotherArea.Click += new System.EventHandler(this.btnAddAnotherArea_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(126, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Site Name";
            // 
            // txtAreaName
            // 
            this.txtAreaName.Location = new System.Drawing.Point(22, 80);
            this.txtAreaName.Name = "txtAreaName";
            this.txtAreaName.Size = new System.Drawing.Size(100, 20);
            this.txtAreaName.TabIndex = 10;
            this.txtAreaName.Text = "My Site";
            this.txtAreaName.TextChanged += new System.EventHandler(this.txtAreaName_TextChanged);
            // 
            // frmAreaBoundry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 143);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAreaName);
            this.Controls.Add(this.btnAddAnotherArea);
            this.Controls.Add(this.btnCancelArea);
            this.Controls.Add(this.btnRedrawArea);
            this.Controls.Add(this.btnSaveArea);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAreaBoundry";
            this.Text = "Draw Site Boundry (polygon)";
            this.Load += new System.EventHandler(this.frmAreaBoundry_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSaveArea;
        private System.Windows.Forms.Button btnRedrawArea;
        private System.Windows.Forms.Button btnCancelArea;
        public System.Windows.Forms.Button btnAddAnotherArea;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtAreaName;
    }
}