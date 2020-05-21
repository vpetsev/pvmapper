namespace PVDESKTOP
{
    partial class frmAddBuilding
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddBuilding));
            this.label2 = new System.Windows.Forms.Label();
            this.txtBuildingHeight = new System.Windows.Forms.TextBox();
            this.lblBuildingHeight = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSaveBuilding = new System.Windows.Forms.Button();
            this.btnCancelBuilding = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(316, 78);
            this.label2.TabIndex = 173;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // txtBuildingHeight
            // 
            this.txtBuildingHeight.Location = new System.Drawing.Point(53, 119);
            this.txtBuildingHeight.Name = "txtBuildingHeight";
            this.txtBuildingHeight.Size = new System.Drawing.Size(43, 20);
            this.txtBuildingHeight.TabIndex = 174;
            this.txtBuildingHeight.Text = "5";
            this.txtBuildingHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblBuildingHeight
            // 
            this.lblBuildingHeight.AutoSize = true;
            this.lblBuildingHeight.Location = new System.Drawing.Point(12, 122);
            this.lblBuildingHeight.Name = "lblBuildingHeight";
            this.lblBuildingHeight.Size = new System.Drawing.Size(38, 13);
            this.lblBuildingHeight.TabIndex = 175;
            this.lblBuildingHeight.Text = "Height";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(99, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 176;
            this.label1.Text = "m";
            // 
            // btnSaveBuilding
            // 
            this.btnSaveBuilding.Location = new System.Drawing.Point(232, 117);
            this.btnSaveBuilding.Name = "btnSaveBuilding";
            this.btnSaveBuilding.Size = new System.Drawing.Size(90, 23);
            this.btnSaveBuilding.TabIndex = 177;
            this.btnSaveBuilding.Text = "Save and Close";
            this.btnSaveBuilding.UseVisualStyleBackColor = true;
            this.btnSaveBuilding.Click += new System.EventHandler(this.btnSaveBuilding_Click);
            // 
            // btnCancelBuilding
            // 
            this.btnCancelBuilding.Location = new System.Drawing.Point(151, 117);
            this.btnCancelBuilding.Name = "btnCancelBuilding";
            this.btnCancelBuilding.Size = new System.Drawing.Size(75, 23);
            this.btnCancelBuilding.TabIndex = 178;
            this.btnCancelBuilding.Text = "Cancel";
            this.btnCancelBuilding.UseVisualStyleBackColor = true;
            this.btnCancelBuilding.Click += new System.EventHandler(this.btnCancelBuilding_Click);
            // 
            // frmAddBuilding
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 152);
            this.Controls.Add(this.btnCancelBuilding);
            this.Controls.Add(this.btnSaveBuilding);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblBuildingHeight);
            this.Controls.Add(this.txtBuildingHeight);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddBuilding";
            this.Text = "Select Building Height";
            this.Load += new System.EventHandler(this.frmAddBuilding_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBuildingHeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSaveBuilding;
        private System.Windows.Forms.Button btnCancelBuilding;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtBuildingHeight;
    }
}