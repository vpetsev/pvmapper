namespace PVDESKTOP
{
    partial class frmSunPath
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSunPath));
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cmdSaveAsCSV = new System.Windows.Forms.Button();
            this.grdSolarRose = new System.Windows.Forms.DataGridView();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.cmdRoseModel = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.cmdSolar_YearlyCal = new System.Windows.Forms.Button();
            this.grdSolarDay = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.grdYearResult = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSolarRose)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSolarDay)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdYearResult)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(634, 476);
            this.tabControl2.TabIndex = 42;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.cmdSaveAsCSV);
            this.tabPage3.Controls.Add(this.grdSolarRose);
            this.tabPage3.Controls.Add(this.prgBar);
            this.tabPage3.Controls.Add(this.cmdRoseModel);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(626, 450);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Sun Rose Table";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // cmdSaveAsCSV
            // 
            this.cmdSaveAsCSV.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdSaveAsCSV.BackgroundImage")));
            this.cmdSaveAsCSV.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdSaveAsCSV.Location = new System.Drawing.Point(6, 3);
            this.cmdSaveAsCSV.Name = "cmdSaveAsCSV";
            this.cmdSaveAsCSV.Size = new System.Drawing.Size(27, 27);
            this.cmdSaveAsCSV.TabIndex = 34;
            this.toolTip1.SetToolTip(this.cmdSaveAsCSV, "Save as CSV file");
            this.cmdSaveAsCSV.UseVisualStyleBackColor = true;
            this.cmdSaveAsCSV.Click += new System.EventHandler(this.cmdSaveAsCSV_Click);
            // 
            // grdSolarRose
            // 
            this.grdSolarRose.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdSolarRose.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdSolarRose.Location = new System.Drawing.Point(2, 36);
            this.grdSolarRose.Name = "grdSolarRose";
            this.grdSolarRose.Size = new System.Drawing.Size(621, 623);
            this.grdSolarRose.TabIndex = 15;
            // 
            // prgBar
            // 
            this.prgBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prgBar.Location = new System.Drawing.Point(70, 7);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(550, 20);
            this.prgBar.TabIndex = 10;
            this.prgBar.Visible = false;
            // 
            // cmdRoseModel
            // 
            this.cmdRoseModel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdRoseModel.BackgroundImage")));
            this.cmdRoseModel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdRoseModel.Location = new System.Drawing.Point(38, 3);
            this.cmdRoseModel.Name = "cmdRoseModel";
            this.cmdRoseModel.Size = new System.Drawing.Size(27, 27);
            this.cmdRoseModel.TabIndex = 9;
            this.cmdRoseModel.UseVisualStyleBackColor = true;
            this.cmdRoseModel.Visible = false;
            this.cmdRoseModel.Click += new System.EventHandler(this.cmdRoseModel_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.cmdSolar_YearlyCal);
            this.tabPage4.Controls.Add(this.grdSolarDay);
            this.tabPage4.Controls.Add(this.dateTimePicker1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(626, 450);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Sun Path Calculations";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // cmdSolar_YearlyCal
            // 
            this.cmdSolar_YearlyCal.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdSolar_YearlyCal.BackgroundImage")));
            this.cmdSolar_YearlyCal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdSolar_YearlyCal.Location = new System.Drawing.Point(164, 3);
            this.cmdSolar_YearlyCal.Name = "cmdSolar_YearlyCal";
            this.cmdSolar_YearlyCal.Size = new System.Drawing.Size(27, 27);
            this.cmdSolar_YearlyCal.TabIndex = 33;
            this.cmdSolar_YearlyCal.UseVisualStyleBackColor = true;
            this.cmdSolar_YearlyCal.Visible = false;
            this.cmdSolar_YearlyCal.Click += new System.EventHandler(this.cmdSolar_YearlyCal_Click);
            // 
            // grdSolarDay
            // 
            this.grdSolarDay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdSolarDay.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.grdSolarDay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSolarDay.Location = new System.Drawing.Point(3, 3);
            this.grdSolarDay.Name = "grdSolarDay";
            this.grdSolarDay.Size = new System.Drawing.Size(620, 444);
            this.grdSolarDay.TabIndex = 32;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Parameter";
            this.Column1.Name = "Column1";
            this.Column1.Width = 200;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Value";
            this.Column2.Name = "Column2";
            this.Column2.Width = 200;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(8, 8);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(150, 20);
            this.dateTimePicker1.TabIndex = 31;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.grdYearResult);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(626, 450);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "Time Series Data";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // grdYearResult
            // 
            this.grdYearResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdYearResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12});
            this.grdYearResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdYearResult.Location = new System.Drawing.Point(3, 3);
            this.grdYearResult.Name = "grdYearResult";
            this.grdYearResult.Size = new System.Drawing.Size(620, 444);
            this.grdYearResult.TabIndex = 5;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Column5";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Column6";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Column7";
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Column8";
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Column9";
            this.Column9.Name = "Column9";
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Column10";
            this.Column10.Name = "Column10";
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Column11";
            this.Column11.Name = "Column11";
            // 
            // Column12
            // 
            this.Column12.HeaderText = "Column12";
            this.Column12.Name = "Column12";
            // 
            // frmSunPath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 476);
            this.Controls.Add(this.tabControl2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSunPath";
            this.Text = "Sun Path Properties";
            this.Load += new System.EventHandler(this.frmSunPath_Load);
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSolarRose)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSolarDay)).EndInit();
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdYearResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView grdSolarRose;
        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.Button cmdRoseModel;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button cmdSolar_YearlyCal;
        private System.Windows.Forms.DataGridView grdSolarDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.DataGridView grdYearResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.Button cmdSaveAsCSV;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}