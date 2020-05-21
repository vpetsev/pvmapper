namespace PVDESKTOP
{
    partial class frmTrees
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTrees));
            this.lblRecordSel = new System.Windows.Forms.Label();
            this.cmdUpdateShape = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.lblTreeDetail = new System.Windows.Forms.Label();
            this.cmdSetSameHeight = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grdTreeAttribute = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTreeAttribute)).BeginInit();
            this.SuspendLayout();
            // 
            // lblRecordSel
            // 
            this.lblRecordSel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRecordSel.AutoSize = true;
            this.lblRecordSel.Location = new System.Drawing.Point(3, 6);
            this.lblRecordSel.Name = "lblRecordSel";
            this.lblRecordSel.Size = new System.Drawing.Size(35, 13);
            this.lblRecordSel.TabIndex = 161;
            this.lblRecordSel.Text = "label3";
            // 
            // cmdUpdateShape
            // 
            this.cmdUpdateShape.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdUpdateShape.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdUpdateShape.Location = new System.Drawing.Point(251, 3);
            this.cmdUpdateShape.Name = "cmdUpdateShape";
            this.cmdUpdateShape.Size = new System.Drawing.Size(90, 20);
            this.cmdUpdateShape.TabIndex = 162;
            this.cmdUpdateShape.Text = "Save and Close";
            this.toolTip1.SetToolTip(this.cmdUpdateShape, "Update shapefile");
            this.cmdUpdateShape.UseVisualStyleBackColor = true;
            this.cmdUpdateShape.Click += new System.EventHandler(this.cmdUpdateShape_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdCancel.Location = new System.Drawing.Point(196, 2);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(52, 20);
            this.cmdCancel.TabIndex = 162;
            this.cmdCancel.Text = "Cancel";
            this.toolTip1.SetToolTip(this.cmdCancel, "Cancel");
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // lblTreeDetail
            // 
            this.lblTreeDetail.AutoSize = true;
            this.lblTreeDetail.Location = new System.Drawing.Point(3, 211);
            this.lblTreeDetail.Name = "lblTreeDetail";
            this.lblTreeDetail.Size = new System.Drawing.Size(10, 13);
            this.lblTreeDetail.TabIndex = 163;
            this.lblTreeDetail.Text = ":";
            this.lblTreeDetail.Visible = false;
            // 
            // cmdSetSameHeight
            // 
            this.cmdSetSameHeight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdSetSameHeight.Image = ((System.Drawing.Image)(resources.GetObject("cmdSetSameHeight.Image")));
            this.cmdSetSameHeight.Location = new System.Drawing.Point(603, 184);
            this.cmdSetSameHeight.Name = "cmdSetSameHeight";
            this.cmdSetSameHeight.Size = new System.Drawing.Size(27, 27);
            this.cmdSetSameHeight.TabIndex = 164;
            this.toolTip1.SetToolTip(this.cmdSetSameHeight, "Save data to table");
            this.cmdSetSameHeight.UseVisualStyleBackColor = true;
            this.cmdSetSameHeight.Visible = false;
            this.cmdSetSameHeight.Click += new System.EventHandler(this.cmdSetSameHeight_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grdTreeAttribute);
            this.splitContainer1.Panel1.Controls.Add(this.cmdSetSameHeight);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblRecordSel);
            this.splitContainer1.Panel2.Controls.Add(this.lblTreeDetail);
            this.splitContainer1.Panel2.Controls.Add(this.cmdUpdateShape);
            this.splitContainer1.Panel2.Controls.Add(this.cmdCancel);
            this.splitContainer1.Size = new System.Drawing.Size(344, 246);
            this.splitContainer1.SplitterDistance = 217;
            this.splitContainer1.TabIndex = 165;
            // 
            // grdTreeAttribute
            // 
            this.grdTreeAttribute.AllowUserToAddRows = false;
            this.grdTreeAttribute.AllowUserToDeleteRows = false;
            this.grdTreeAttribute.AllowUserToOrderColumns = true;
            this.grdTreeAttribute.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdTreeAttribute.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.Column1,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn8});
            this.grdTreeAttribute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTreeAttribute.Location = new System.Drawing.Point(0, 0);
            this.grdTreeAttribute.Name = "grdTreeAttribute";
            this.grdTreeAttribute.Size = new System.Drawing.Size(344, 217);
            this.grdTreeAttribute.TabIndex = 165;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Tree ID";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Type";
            this.Column1.Name = "Column1";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Height";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Diameter";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // frmTrees
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 246);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTrees";
            this.Text = "Tree Properties";
            this.Load += new System.EventHandler(this.frmTrees_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTreeAttribute)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblRecordSel;
        private System.Windows.Forms.Button cmdUpdateShape;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label lblTreeDetail;
        private System.Windows.Forms.Button cmdSetSameHeight;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView grdTreeAttribute;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
    }
}