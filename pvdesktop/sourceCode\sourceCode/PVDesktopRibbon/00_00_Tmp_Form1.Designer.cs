namespace PVDESKTOP
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
            this.cmdCountLayer = new System.Windows.Forms.Button();
            this.cmbProjectLyr = new System.Windows.Forms.ComboBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.cmdLoadedLayer = new System.Windows.Forms.Button();
            this.cmdGetLayer = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbLinkLyr = new System.Windows.Forms.ComboBox();
            this.cmdGetSelection = new System.Windows.Forms.Button();
            this.grdSelection = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.cmbNode = new System.Windows.Forms.ComboBox();
            this.grdDemand = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtDistance = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.grdLink = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LinkID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdSelection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDemand)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLink)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdCountLayer
            // 
            this.cmdCountLayer.Location = new System.Drawing.Point(6, 6);
            this.cmdCountLayer.Name = "cmdCountLayer";
            this.cmdCountLayer.Size = new System.Drawing.Size(120, 23);
            this.cmdCountLayer.TabIndex = 0;
            this.cmdCountLayer.Text = "Count layer";
            this.cmdCountLayer.UseVisualStyleBackColor = true;
            this.cmdCountLayer.Click += new System.EventHandler(this.cmdCountLayer_Click);
            // 
            // cmbProjectLyr
            // 
            this.cmbProjectLyr.FormattingEnabled = true;
            this.cmbProjectLyr.Location = new System.Drawing.Point(132, 37);
            this.cmbProjectLyr.Name = "cmbProjectLyr";
            this.cmbProjectLyr.Size = new System.Drawing.Size(121, 21);
            this.cmbProjectLyr.TabIndex = 1;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(6, 67);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 160);
            this.listBox1.TabIndex = 2;
            // 
            // cmdLoadedLayer
            // 
            this.cmdLoadedLayer.Location = new System.Drawing.Point(6, 35);
            this.cmdLoadedLayer.Name = "cmdLoadedLayer";
            this.cmdLoadedLayer.Size = new System.Drawing.Size(120, 23);
            this.cmdLoadedLayer.TabIndex = 3;
            this.cmdLoadedLayer.Text = "List loaded layer";
            this.cmdLoadedLayer.UseVisualStyleBackColor = true;
            this.cmdLoadedLayer.Click += new System.EventHandler(this.cmdLoadedLayer_Click);
            // 
            // cmdGetLayer
            // 
            this.cmdGetLayer.Location = new System.Drawing.Point(132, 64);
            this.cmdGetLayer.Name = "cmdGetLayer";
            this.cmdGetLayer.Size = new System.Drawing.Size(121, 23);
            this.cmdGetLayer.TabIndex = 4;
            this.cmdGetLayer.Text = "get Layer data";
            this.cmdGetLayer.UseVisualStyleBackColor = true;
            this.cmdGetLayer.Click += new System.EventHandler(this.cmdGetLayer_Click);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(132, 93);
            this.listBox2.Name = "listBox2";
            this.listBox2.ScrollAlwaysVisible = true;
            this.listBox2.Size = new System.Drawing.Size(121, 134);
            this.listBox2.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(309, 229);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Get link structure";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-2, 234);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Link layer";
            // 
            // cmbLinkLyr
            // 
            this.cmbLinkLyr.FormattingEnabled = true;
            this.cmbLinkLyr.Location = new System.Drawing.Point(56, 230);
            this.cmbLinkLyr.Name = "cmbLinkLyr";
            this.cmbLinkLyr.Size = new System.Drawing.Size(92, 21);
            this.cmbLinkLyr.TabIndex = 1;
            // 
            // cmdGetSelection
            // 
            this.cmdGetSelection.Location = new System.Drawing.Point(280, 37);
            this.cmdGetSelection.Name = "cmdGetSelection";
            this.cmdGetSelection.Size = new System.Drawing.Size(136, 23);
            this.cmdGetSelection.TabIndex = 8;
            this.cmdGetSelection.Text = "Get selection";
            this.cmdGetSelection.UseVisualStyleBackColor = true;
            this.cmdGetSelection.Click += new System.EventHandler(this.cmdGetSelection_Click);
            // 
            // grdSelection
            // 
            this.grdSelection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdSelection.Location = new System.Drawing.Point(280, 64);
            this.grdSelection.Name = "grdSelection";
            this.grdSelection.Size = new System.Drawing.Size(369, 160);
            this.grdSelection.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(668, 234);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Node layer";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(824, 226);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(216, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "Get Demand Node structure";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cmbNode
            // 
            this.cmbNode.FormattingEnabled = true;
            this.cmbNode.Location = new System.Drawing.Point(726, 228);
            this.cmbNode.Name = "cmbNode";
            this.cmbNode.Size = new System.Drawing.Size(92, 21);
            this.cmbNode.TabIndex = 10;
            // 
            // grdDemand
            // 
            this.grdDemand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grdDemand.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDemand.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.LinkID,
            this.Column11,
            this.Column12});
            this.grdDemand.Location = new System.Drawing.Point(671, 254);
            this.grdDemand.Name = "grdDemand";
            this.grdDemand.Size = new System.Drawing.Size(369, 136);
            this.grdDemand.TabIndex = 13;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1056, 422);
            this.tabControl1.TabIndex = 14;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtDistance);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.grdLink);
            this.tabPage2.Controls.Add(this.cmdCountLayer);
            this.tabPage2.Controls.Add(this.grdDemand);
            this.tabPage2.Controls.Add(this.cmbProjectLyr);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.cmbLinkLyr);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.listBox1);
            this.tabPage2.Controls.Add(this.cmbNode);
            this.tabPage2.Controls.Add(this.listBox2);
            this.tabPage2.Controls.Add(this.grdSelection);
            this.tabPage2.Controls.Add(this.cmdLoadedLayer);
            this.tabPage2.Controls.Add(this.cmdGetSelection);
            this.tabPage2.Controls.Add(this.cmdGetLayer);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1048, 396);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Sample 1";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtDistance
            // 
            this.txtDistance.Location = new System.Drawing.Point(242, 230);
            this.txtDistance.Name = "txtDistance";
            this.txtDistance.Size = new System.Drawing.Size(61, 20);
            this.txtDistance.TabIndex = 16;
            this.txtDistance.Text = "1000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(154, 234);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Distance check";
            // 
            // grdLink
            // 
            this.grdLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grdLink.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdLink.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column9});
            this.grdLink.Location = new System.Drawing.Point(0, 254);
            this.grdLink.Name = "grdLink";
            this.grdLink.Size = new System.Drawing.Size(649, 139);
            this.grdLink.TabIndex = 14;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Spatial_ID";
            this.Column1.Name = "Column1";
            this.Column1.Width = 50;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "From";
            this.Column6.Name = "Column6";
            this.Column6.Width = 40;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "To";
            this.Column7.Name = "Column7";
            this.Column7.Width = 40;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Length";
            this.Column8.Name = "Column8";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Speed";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Lane";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Capacity/Lane";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Alpha";
            this.Column5.Name = "Column5";
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Beta";
            this.Column9.Name = "Column9";
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1048, 396);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "x1";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 80;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "y1";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 80;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "AMP_ID";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 80;
            // 
            // LinkID
            // 
            this.LinkID.HeaderText = "SpatialID";
            this.LinkID.Name = "LinkID";
            // 
            // Column11
            // 
            this.Column11.HeaderText = "X";
            this.Column11.Name = "Column11";
            // 
            // Column12
            // 
            this.Column12.HeaderText = "Y";
            this.Column12.Name = "Column12";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 422);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Temp Form (Sample code for programming)";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdSelection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDemand)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLink)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdCountLayer;
        private System.Windows.Forms.ComboBox cmbProjectLyr;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button cmdLoadedLayer;
        private System.Windows.Forms.Button cmdGetLayer;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbLinkLyr;
        private System.Windows.Forms.Button cmdGetSelection;
        private System.Windows.Forms.DataGridView grdSelection;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cmbNode;
        private System.Windows.Forms.DataGridView grdDemand;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView grdLink;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.TextBox txtDistance;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn LinkID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
    }
}