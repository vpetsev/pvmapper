namespace PVDESKTOP
{
    partial class frm01_MainForm_NetworkForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm01_MainForm_NetworkForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.grdLink = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.grdNode = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtLinkError = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.lblAmpD = new System.Windows.Forms.Label();
            this.lblAmpO = new System.Windows.Forms.Label();
            this.cmbD = new System.Windows.Forms.ComboBox();
            this.cmbO = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdPath = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.cmbDestinationAmp = new System.Windows.Forms.ComboBox();
            this.cmbOriginAmp = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.cmdSaveNetwork = new System.Windows.Forms.Button();
            this.cmdCreateNetworkData = new System.Windows.Forms.Button();
            this.cmdGetExtingNode = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdUpdateDB = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.button4 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLink)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNode)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(0, 32);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(357, 157);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.grdLink);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(278, 131);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Link Data";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // grdLink
            // 
            this.grdLink.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdLink.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column10,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column9});
            this.grdLink.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLink.Location = new System.Drawing.Point(3, 3);
            this.grdLink.Name = "grdLink";
            this.grdLink.Size = new System.Drawing.Size(272, 125);
            this.grdLink.TabIndex = 6;
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
            this.Column8.HeaderText = "LengthFT";
            this.Column8.Name = "Column8";
            // 
            // Column10
            // 
            this.Column10.HeaderText = "LengthTF";
            this.Column10.Name = "Column10";
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
            this.tabPage1.Controls.Add(this.grdNode);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(278, 131);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Node data";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // grdNode
            // 
            this.grdNode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdNode.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7});
            this.grdNode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdNode.Location = new System.Drawing.Point(3, 3);
            this.grdNode.Name = "grdNode";
            this.grdNode.Size = new System.Drawing.Size(272, 125);
            this.grdNode.TabIndex = 7;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Node_ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "X";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Y";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Type";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "AMP_ID";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtLinkError);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(278, 131);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Error";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtLinkError
            // 
            this.txtLinkError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLinkError.Location = new System.Drawing.Point(3, 3);
            this.txtLinkError.Multiline = true;
            this.txtLinkError.Name = "txtLinkError";
            this.txtLinkError.Size = new System.Drawing.Size(272, 125);
            this.txtLinkError.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.button4);
            this.tabPage4.Controls.Add(this.lblAmpD);
            this.tabPage4.Controls.Add(this.lblAmpO);
            this.tabPage4.Controls.Add(this.cmbD);
            this.tabPage4.Controls.Add(this.cmbO);
            this.tabPage4.Controls.Add(this.button2);
            this.tabPage4.Controls.Add(this.button1);
            this.tabPage4.Controls.Add(this.listBox1);
            this.tabPage4.Controls.Add(this.Label2);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.cmdPath);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(349, 131);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Test SP";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // lblAmpD
            // 
            this.lblAmpD.AutoSize = true;
            this.lblAmpD.Location = new System.Drawing.Point(209, 28);
            this.lblAmpD.Name = "lblAmpD";
            this.lblAmpD.Size = new System.Drawing.Size(28, 13);
            this.lblAmpD.TabIndex = 31;
            this.lblAmpD.Text = "Amp";
            // 
            // lblAmpO
            // 
            this.lblAmpO.AutoSize = true;
            this.lblAmpO.Location = new System.Drawing.Point(209, 8);
            this.lblAmpO.Name = "lblAmpO";
            this.lblAmpO.Size = new System.Drawing.Size(28, 13);
            this.lblAmpO.TabIndex = 31;
            this.lblAmpO.Text = "Amp";
            // 
            // cmbD
            // 
            this.cmbD.FormattingEnabled = true;
            this.cmbD.Location = new System.Drawing.Point(82, 25);
            this.cmbD.Name = "cmbD";
            this.cmbD.Size = new System.Drawing.Size(121, 21);
            this.cmbD.TabIndex = 30;
            this.cmbD.SelectedIndexChanged += new System.EventHandler(this.cmbD_SelectedIndexChanged);
            // 
            // cmbO
            // 
            this.cmbO.FormattingEnabled = true;
            this.cmbO.Location = new System.Drawing.Point(82, 3);
            this.cmbO.Name = "cmbO";
            this.cmbO.Size = new System.Drawing.Size(121, 21);
            this.cmbO.TabIndex = 30;
            this.cmbO.SelectedIndexChanged += new System.EventHandler(this.cmbO_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(371, 23);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 29;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(371, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 23);
            this.button1.TabIndex = 28;
            this.button1.Text = "Load Network";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox1.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(7, 47);
            this.listBox1.Margin = new System.Windows.Forms.Padding(2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(196, 76);
            this.listBox1.TabIndex = 27;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(7, 25);
            this.Label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(60, 13);
            this.Label2.TabIndex = 24;
            this.Label2.Text = "Destination";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 3);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "origin";
            // 
            // cmdPath
            // 
            this.cmdPath.Enabled = false;
            this.cmdPath.Location = new System.Drawing.Point(207, 75);
            this.cmdPath.Margin = new System.Windows.Forms.Padding(2);
            this.cmdPath.Name = "cmdPath";
            this.cmdPath.Size = new System.Drawing.Size(68, 23);
            this.cmdPath.TabIndex = 22;
            this.cmdPath.Text = "Path";
            this.cmdPath.UseVisualStyleBackColor = true;
            this.cmdPath.Click += new System.EventHandler(this.cmdPath_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.cmbDestinationAmp);
            this.tabPage5.Controls.Add(this.cmbOriginAmp);
            this.tabPage5.Controls.Add(this.button3);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(278, 131);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Database";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // cmbDestinationAmp
            // 
            this.cmbDestinationAmp.FormattingEnabled = true;
            this.cmbDestinationAmp.Location = new System.Drawing.Point(82, 32);
            this.cmbDestinationAmp.Name = "cmbDestinationAmp";
            this.cmbDestinationAmp.Size = new System.Drawing.Size(100, 21);
            this.cmbDestinationAmp.TabIndex = 144;
            // 
            // cmbOriginAmp
            // 
            this.cmbOriginAmp.FormattingEnabled = true;
            this.cmbOriginAmp.Location = new System.Drawing.Point(82, 8);
            this.cmbOriginAmp.Name = "cmbOriginAmp";
            this.cmbOriginAmp.Size = new System.Drawing.Size(100, 21);
            this.cmbOriginAmp.TabIndex = 145;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // cmdSaveNetwork
            // 
            this.cmdSaveNetwork.BackColor = System.Drawing.Color.White;
            this.cmdSaveNetwork.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdSaveNetwork.Image = ((System.Drawing.Image)(resources.GetObject("cmdSaveNetwork.Image")));
            this.cmdSaveNetwork.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cmdSaveNetwork.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmdSaveNetwork.Location = new System.Drawing.Point(52, 4);
            this.cmdSaveNetwork.Name = "cmdSaveNetwork";
            this.cmdSaveNetwork.Size = new System.Drawing.Size(24, 24);
            this.cmdSaveNetwork.TabIndex = 95;
            this.toolTip1.SetToolTip(this.cmdSaveNetwork, "Save network data");
            this.cmdSaveNetwork.UseVisualStyleBackColor = false;
            this.cmdSaveNetwork.Click += new System.EventHandler(this.cmdSaveNetwork_Click);
            // 
            // cmdCreateNetworkData
            // 
            this.cmdCreateNetworkData.BackColor = System.Drawing.Color.White;
            this.cmdCreateNetworkData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdCreateNetworkData.Image = ((System.Drawing.Image)(resources.GetObject("cmdCreateNetworkData.Image")));
            this.cmdCreateNetworkData.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cmdCreateNetworkData.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmdCreateNetworkData.Location = new System.Drawing.Point(4, 4);
            this.cmdCreateNetworkData.Name = "cmdCreateNetworkData";
            this.cmdCreateNetworkData.Size = new System.Drawing.Size(24, 24);
            this.cmdCreateNetworkData.TabIndex = 96;
            this.toolTip1.SetToolTip(this.cmdCreateNetworkData, "Genterate network data");
            this.cmdCreateNetworkData.UseVisualStyleBackColor = false;
            this.cmdCreateNetworkData.Click += new System.EventHandler(this.cmdCreateNetworkData_Click);
            // 
            // cmdGetExtingNode
            // 
            this.cmdGetExtingNode.BackColor = System.Drawing.Color.White;
            this.cmdGetExtingNode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdGetExtingNode.Image = ((System.Drawing.Image)(resources.GetObject("cmdGetExtingNode.Image")));
            this.cmdGetExtingNode.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cmdGetExtingNode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmdGetExtingNode.Location = new System.Drawing.Point(112, 4);
            this.cmdGetExtingNode.Name = "cmdGetExtingNode";
            this.cmdGetExtingNode.Size = new System.Drawing.Size(24, 24);
            this.cmdGetExtingNode.TabIndex = 97;
            this.toolTip1.SetToolTip(this.cmdGetExtingNode, "Get initial node ID");
            this.cmdGetExtingNode.UseVisualStyleBackColor = false;
            this.cmdGetExtingNode.Visible = false;
            this.cmdGetExtingNode.Click += new System.EventHandler(this.cmdGetExtingNode_Click);
            // 
            // cmdUpdateDB
            // 
            this.cmdUpdateDB.BackColor = System.Drawing.Color.White;
            this.cmdUpdateDB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdUpdateDB.Image = ((System.Drawing.Image)(resources.GetObject("cmdUpdateDB.Image")));
            this.cmdUpdateDB.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cmdUpdateDB.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmdUpdateDB.Location = new System.Drawing.Point(28, 4);
            this.cmdUpdateDB.Name = "cmdUpdateDB";
            this.cmdUpdateDB.Size = new System.Drawing.Size(24, 24);
            this.cmdUpdateDB.TabIndex = 97;
            this.toolTip1.SetToolTip(this.cmdUpdateDB, "Update Network Database");
            this.cmdUpdateDB.UseVisualStyleBackColor = false;
            this.cmdUpdateDB.Click += new System.EventHandler(this.cmdUpdateDB_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(145, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 98;
            this.label1.Text = "Network layer:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 192);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(357, 22);
            this.statusStrip1.TabIndex = 99;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(207, 47);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(109, 23);
            this.button4.TabIndex = 32;
            this.button4.Text = "readNET from DB";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // frm01_MainForm_NetworkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 214);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdSaveNetwork);
            this.Controls.Add(this.cmdCreateNetworkData);
            this.Controls.Add(this.cmdUpdateDB);
            this.Controls.Add(this.cmdGetExtingNode);
            this.Controls.Add(this.tabControl1);
            this.Name = "frm01_MainForm_NetworkForm";
            this.Text = "Network table";
            this.Load += new System.EventHandler(this.frm01_MainForm_NetworkForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdLink)).EndInit();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdNode)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        internal System.Windows.Forms.Button cmdSaveNetwork;
        private System.Windows.Forms.ToolTip toolTip1;
        internal System.Windows.Forms.Button cmdCreateNetworkData;
        internal System.Windows.Forms.Button cmdGetExtingNode;
        private System.Windows.Forms.DataGridView grdNode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridView grdLink;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox txtLinkError;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button button1;
        internal System.Windows.Forms.ListBox listBox1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Button cmdPath;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.ComboBox cmbD;
        private System.Windows.Forms.ComboBox cmbO;
        private System.Windows.Forms.Label lblAmpO;
        private System.Windows.Forms.Label lblAmpD;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox cmbDestinationAmp;
        private System.Windows.Forms.ComboBox cmbOriginAmp;
        internal System.Windows.Forms.Button cmdUpdateDB;
        private System.Windows.Forms.Button button4;
    }
}