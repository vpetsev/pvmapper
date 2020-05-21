namespace PvMapperPlugin
{
    partial class frmDailySolarCal
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.zGraphEleAng2Day = new ZedGraph.ZedGraphControl();
            this.zGraphAz2Day = new ZedGraph.ZedGraphControl();
            this.zGraphAz2EleAng = new ZedGraph.ZedGraphControl();
            this.cmdYearCal = new System.Windows.Forms.Button();
            this.grdSolarDay = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdCal = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTimeZone = new System.Windows.Forms.TextBox();
            this.txtLNG = new System.Windows.Forms.TextBox();
            this.txtLAT = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
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
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSolarDay)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdYearResult)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1204, 429);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.zGraphEleAng2Day);
            this.tabPage1.Controls.Add(this.zGraphAz2Day);
            this.tabPage1.Controls.Add(this.zGraphAz2EleAng);
            this.tabPage1.Controls.Add(this.cmdYearCal);
            this.tabPage1.Controls.Add(this.grdSolarDay);
            this.tabPage1.Controls.Add(this.cmdCal);
            this.tabPage1.Controls.Add(this.dateTimePicker1);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1196, 403);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Parameter";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // zGraphEleAng2Day
            // 
            this.zGraphEleAng2Day.Location = new System.Drawing.Point(874, 6);
            this.zGraphEleAng2Day.Name = "zGraphEleAng2Day";
            this.zGraphEleAng2Day.ScrollGrace = 0D;
            this.zGraphEleAng2Day.ScrollMaxX = 0D;
            this.zGraphEleAng2Day.ScrollMaxY = 0D;
            this.zGraphEleAng2Day.ScrollMaxY2 = 0D;
            this.zGraphEleAng2Day.ScrollMinX = 0D;
            this.zGraphEleAng2Day.ScrollMinY = 0D;
            this.zGraphEleAng2Day.ScrollMinY2 = 0D;
            this.zGraphEleAng2Day.Size = new System.Drawing.Size(305, 269);
            this.zGraphEleAng2Day.TabIndex = 7;
            // 
            // zGraphAz2Day
            // 
            this.zGraphAz2Day.Location = new System.Drawing.Point(563, 6);
            this.zGraphAz2Day.Name = "zGraphAz2Day";
            this.zGraphAz2Day.ScrollGrace = 0D;
            this.zGraphAz2Day.ScrollMaxX = 0D;
            this.zGraphAz2Day.ScrollMaxY = 0D;
            this.zGraphAz2Day.ScrollMaxY2 = 0D;
            this.zGraphAz2Day.ScrollMinX = 0D;
            this.zGraphAz2Day.ScrollMinY = 0D;
            this.zGraphAz2Day.ScrollMinY2 = 0D;
            this.zGraphAz2Day.Size = new System.Drawing.Size(305, 269);
            this.zGraphAz2Day.TabIndex = 7;
            // 
            // zGraphAz2EleAng
            // 
            this.zGraphAz2EleAng.Location = new System.Drawing.Point(232, 4);
            this.zGraphAz2EleAng.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.zGraphAz2EleAng.Name = "zGraphAz2EleAng";
            this.zGraphAz2EleAng.ScrollGrace = 0D;
            this.zGraphAz2EleAng.ScrollMaxX = 0D;
            this.zGraphAz2EleAng.ScrollMaxY = 0D;
            this.zGraphAz2EleAng.ScrollMaxY2 = 0D;
            this.zGraphAz2EleAng.ScrollMinX = 0D;
            this.zGraphAz2EleAng.ScrollMinY = 0D;
            this.zGraphAz2EleAng.ScrollMinY2 = 0D;
            this.zGraphAz2EleAng.Size = new System.Drawing.Size(323, 271);
            this.zGraphAz2EleAng.TabIndex = 7;
            // 
            // cmdYearCal
            // 
            this.cmdYearCal.Location = new System.Drawing.Point(100, 150);
            this.cmdYearCal.Name = "cmdYearCal";
            this.cmdYearCal.Size = new System.Drawing.Size(124, 26);
            this.cmdYearCal.TabIndex = 6;
            this.cmdYearCal.Text = "Year Calculate";
            this.cmdYearCal.UseVisualStyleBackColor = true;
            this.cmdYearCal.Click += new System.EventHandler(this.cmdYearCal_Click);
            // 
            // grdSolarDay
            // 
            this.grdSolarDay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grdSolarDay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdSolarDay.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.grdSolarDay.Location = new System.Drawing.Point(3, 182);
            this.grdSolarDay.Name = "grdSolarDay";
            this.grdSolarDay.Size = new System.Drawing.Size(221, 218);
            this.grdSolarDay.TabIndex = 5;
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
            // cmdCal
            // 
            this.cmdCal.Location = new System.Drawing.Point(100, 121);
            this.cmdCal.Name = "cmdCal";
            this.cmdCal.Size = new System.Drawing.Size(124, 23);
            this.cmdCal.TabIndex = 2;
            this.cmdCal.Text = "Day Calculate";
            this.cmdCal.UseVisualStyleBackColor = true;
            this.cmdCal.Click += new System.EventHandler(this.cmdCal_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(6, 95);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(218, 20);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTimeZone);
            this.groupBox1.Controls.Add(this.txtLNG);
            this.groupBox1.Controls.Add(this.txtLAT);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(218, 83);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Site location";
            // 
            // txtTimeZone
            // 
            this.txtTimeZone.Location = new System.Drawing.Point(107, 56);
            this.txtTimeZone.Name = "txtTimeZone";
            this.txtTimeZone.Size = new System.Drawing.Size(100, 20);
            this.txtTimeZone.TabIndex = 1;
            this.txtTimeZone.Text = "-6";
            // 
            // txtLNG
            // 
            this.txtLNG.Location = new System.Drawing.Point(107, 34);
            this.txtLNG.Name = "txtLNG";
            this.txtLNG.Size = new System.Drawing.Size(100, 20);
            this.txtLNG.TabIndex = 1;
            this.txtLNG.Text = "-105";
            // 
            // txtLAT
            // 
            this.txtLAT.Location = new System.Drawing.Point(107, 13);
            this.txtLAT.Name = "txtLAT";
            this.txtLAT.Size = new System.Drawing.Size(100, 20);
            this.txtLAT.TabIndex = 1;
            this.txtLAT.Text = "30";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Longitude (+ to E)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Longitude (+ to E)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Latitude (+ to N)";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.grdYearResult);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1196, 403);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Table";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            this.grdYearResult.Size = new System.Drawing.Size(1190, 397);
            this.grdYearResult.TabIndex = 4;
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
            // frmDailySolarCal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1204, 429);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmDailySolarCal";
            this.Text = "Solar Calculations (day)";
            this.Load += new System.EventHandler(this.frmDailySolarCal_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSolarDay)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdYearResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtTimeZone;
        private System.Windows.Forms.TextBox txtLNG;
        private System.Windows.Forms.TextBox txtLAT;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button cmdCal;
        private System.Windows.Forms.DataGridView grdYearResult;
        private System.Windows.Forms.Button cmdYearCal;
        private System.Windows.Forms.DataGridView grdSolarDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private ZedGraph.ZedGraphControl zGraphAz2EleAng;
        private ZedGraph.ZedGraphControl zGraphEleAng2Day;
        private ZedGraph.ZedGraphControl zGraphAz2Day;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
    }
}