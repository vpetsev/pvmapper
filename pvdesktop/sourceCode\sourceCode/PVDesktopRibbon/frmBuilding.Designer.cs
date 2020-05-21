namespace PVDESKTOP
{
    partial class frmBuilding
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBuilding));
            this.grdBldg = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBuildingHeight = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdSetSameHeight = new System.Windows.Forms.Button();
            this.cmdUpdateShape = new System.Windows.Forms.Button();
            this.lblRecordSel = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grdBldg)).BeginInit();
            this.SuspendLayout();
            // 
            // grdBldg
            // 
            this.grdBldg.AllowUserToAddRows = false;
            this.grdBldg.AllowUserToDeleteRows = false;
            this.grdBldg.AllowUserToOrderColumns = true;
            this.grdBldg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdBldg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdBldg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn8});
            this.grdBldg.Location = new System.Drawing.Point(0, 29);
            this.grdBldg.Name = "grdBldg";
            this.grdBldg.Size = new System.Drawing.Size(344, 187);
            this.grdBldg.TabIndex = 155;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Bulding ID";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Height";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Remard";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 156;
            this.label1.Text = "Building Height";
            // 
            // txtBuildingHeight
            // 
            this.txtBuildingHeight.Location = new System.Drawing.Point(90, 3);
            this.txtBuildingHeight.Name = "txtBuildingHeight";
            this.txtBuildingHeight.Size = new System.Drawing.Size(59, 20);
            this.txtBuildingHeight.TabIndex = 157;
            this.txtBuildingHeight.Text = "0.0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(152, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 158;
            this.label2.Text = "m";
            // 
            // cmdSetSameHeight
            // 
            this.cmdSetSameHeight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdSetSameHeight.Image = ((System.Drawing.Image)(resources.GetObject("cmdSetSameHeight.Image")));
            this.cmdSetSameHeight.Location = new System.Drawing.Point(167, 0);
            this.cmdSetSameHeight.Name = "cmdSetSameHeight";
            this.cmdSetSameHeight.Size = new System.Drawing.Size(27, 27);
            this.cmdSetSameHeight.TabIndex = 159;
            this.toolTip1.SetToolTip(this.cmdSetSameHeight, "Changes buliding height to all buildings in table");
            this.cmdSetSameHeight.UseVisualStyleBackColor = true;
            this.cmdSetSameHeight.Click += new System.EventHandler(this.cmdSetSameHeight_Click);
            // 
            // cmdUpdateShape
            // 
            this.cmdUpdateShape.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdUpdateShape.Location = new System.Drawing.Point(235, 3);
            this.cmdUpdateShape.Name = "cmdUpdateShape";
            this.cmdUpdateShape.Size = new System.Drawing.Size(102, 23);
            this.cmdUpdateShape.TabIndex = 159;
            this.cmdUpdateShape.Text = "Save and Close";
            this.toolTip1.SetToolTip(this.cmdUpdateShape, "Update shapefile");
            this.cmdUpdateShape.UseVisualStyleBackColor = true;
            this.cmdUpdateShape.Click += new System.EventHandler(this.cmdUpdateShape_Click);
            // 
            // lblRecordSel
            // 
            this.lblRecordSel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRecordSel.AutoSize = true;
            this.lblRecordSel.Location = new System.Drawing.Point(1, 218);
            this.lblRecordSel.Name = "lblRecordSel";
            this.lblRecordSel.Size = new System.Drawing.Size(35, 13);
            this.lblRecordSel.TabIndex = 160;
            this.lblRecordSel.Text = "label3";
            // 
            // frmBuilding
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 232);
            this.Controls.Add(this.lblRecordSel);
            this.Controls.Add(this.cmdUpdateShape);
            this.Controls.Add(this.cmdSetSameHeight);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBuildingHeight);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grdBldg);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBuilding";
            this.Text = "Building Properties";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmBuilding_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdBldg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grdBldg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBuildingHeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdSetSameHeight;
        private System.Windows.Forms.Button cmdUpdateShape;
        private System.Windows.Forms.Label lblRecordSel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}