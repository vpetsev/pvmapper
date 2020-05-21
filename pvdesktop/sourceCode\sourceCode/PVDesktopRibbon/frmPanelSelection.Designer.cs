namespace PVDESKTOP
{
    partial class frmPanelSelection
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
            this.btnPanelSelectionOK = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnPanelSelectionOK
            // 
            this.btnPanelSelectionOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPanelSelectionOK.Location = new System.Drawing.Point(178, 59);
            this.btnPanelSelectionOK.Name = "btnPanelSelectionOK";
            this.btnPanelSelectionOK.Size = new System.Drawing.Size(47, 23);
            this.btnPanelSelectionOK.TabIndex = 0;
            this.btnPanelSelectionOK.Text = "OK";
            this.btnPanelSelectionOK.UseVisualStyleBackColor = true;
            this.btnPanelSelectionOK.Click += new System.EventHandler(this.btnPanelSelectionOK_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectAll.Location = new System.Drawing.Point(108, 59);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(64, 23);
            this.btnSelectAll.TabIndex = 1;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Visible = false;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select panels on map to edit then click OK\r\nor click Panel Position to edit all p" +
    "anels.\r\n";
            // 
            // frmPanelSelection
            // 
            this.AcceptButton = this.btnPanelSelectionOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 88);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnPanelSelectionOK);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(250, 126);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(250, 126);
            this.Name = "frmPanelSelection";
            this.Text = "Select Panels to Edit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPanelSelection_FormClosing);
            this.Load += new System.EventHandler(this.frmPanelSelection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPanelSelectionOK;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Label label1;
    }
}