// ********************************************************************************************************
// Product Name: TestViewer.exe
// Description:  A very basic demonstration of the controls.
// ********************************************************************************************************
// The contents of this file are subject to the MIT License (MIT)
// you may not use this file except in compliance with the License. You may obtain a copy of the License at
// http://dotspatial.codeplex.com/license
//
// Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF
// ANY KIND, either expressed or implied. See the License for the specific language governing rights and
// limitations under the License.
//
// The Initial Developer of this Original Code is Ted Dunsford. Created during refactoring 2010.
// ********************************************************************************************************
using DotSpatial.Controls;

namespace PVDESKTOP
{
    /// <summary>
    /// Form
    /// </summary>
    partial class frm01_MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm01_MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabLegend = new System.Windows.Forms.TabPage();
            this.legend1 = new DotSpatial.Controls.Legend();
            this.tabLayerVariables = new System.Windows.Forms.TabPage();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.btnCloseHelp = new System.Windows.Forms.Button();
            this.picboxHelp = new System.Windows.Forms.PictureBox();
            this.cmdSwithToGraph = new System.Windows.Forms.Button();
            this.cmdSwithToMap = new System.Windows.Forms.Button();
            this.cmdSwithToTable = new System.Windows.Forms.Button();
            this.pnlGrdProduction = new System.Windows.Forms.Panel();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.lblGrdTitle = new System.Windows.Forms.Label();
            this.grdAcProduct = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpdateProgressBar = new System.Windows.Forms.ProgressBar();
            this.cmdMapSelectionNone = new System.Windows.Forms.Button();
            this.cmdMapSelection = new System.Windows.Forms.Button();
            this.cmdMapZoomExt = new System.Windows.Forms.Button();
            this.cmdMapZoomOut = new System.Windows.Forms.Button();
            this.cmdMapPan = new System.Windows.Forms.Button();
            this.cmdMapZoomIn = new System.Windows.Forms.Button();
            this.TabGOptimize = new System.Windows.Forms.TabControl();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.txtPvWidth = new System.Windows.Forms.TextBox();
            this.txtPvLength = new System.Windows.Forms.TextBox();
            this.picSolarPanel = new System.Windows.Forms.PictureBox();
            this.pvTilt = new pvPanel3DAngleCtl.pvPanelAngle();
            this.pvAz = new pvPanel3DAngleCtl.pvPanelCompassCtl();
            this.cmdOptimization = new System.Windows.Forms.Button();
            this.tabPage13 = new System.Windows.Forms.TabPage();
            this.zedGOpti1 = new ZedGraph.ZedGraphControl();
            this.tabPage12 = new System.Windows.Forms.TabPage();
            this.zedGOpti2 = new ZedGraph.ZedGraphControl();
            this.tabPage14 = new System.Windows.Forms.TabPage();
            this.zedGOpti3 = new ZedGraph.ZedGraphControl();
            this.tabPage15 = new System.Windows.Forms.TabPage();
            this.zedGOpti4 = new ZedGraph.ZedGraphControl();
            this.pvMap = new DotSpatial.Controls.Map();
            this.ttHelp = new System.Windows.Forms.ToolTip(this.components);
            this.cmdRedrawRoofPlan = new System.Windows.Forms.Button();
            this.cmdExportSketchUp = new System.Windows.Forms.Button();
            this.cmdPickCentroid = new System.Windows.Forms.Button();
            this.cmdUseCurrentPath = new System.Windows.Forms.Button();
            this.ExportBldgAndTrr2SketchUp = new System.Windows.Forms.Button();
            this.cmdShowIdwSta = new System.Windows.Forms.Button();
            this.cmdAddKML = new System.Windows.Forms.Button();
            this.cmdZoomToSite = new System.Windows.Forms.Button();
            this.cmdSaveConfig = new System.Windows.Forms.Button();
            this.cmdSelectTreeLayer = new System.Windows.Forms.Button();
            this.cmdEditTreePropDialog = new System.Windows.Forms.Button();
            this.cmdSelectTree = new System.Windows.Forms.Button();
            this.cmdSelectBuilding = new System.Windows.Forms.Button();
            this.cmdBuilding = new System.Windows.Forms.Button();
            this.cmdWeatherFile = new System.Windows.Forms.Button();
            this.cmbDem = new System.Windows.Forms.ComboBox();
            this.cmbSolarFarmArea = new System.Windows.Forms.ComboBox();
            this.cmbPolePosition = new System.Windows.Forms.ComboBox();
            this.cmbPanel = new System.Windows.Forms.ComboBox();
            this.cmbAlignmentLyr = new System.Windows.Forms.ComboBox();
            this.cmdNewAligmnentShp = new System.Windows.Forms.Button();
            this.cmdEnergyCal = new System.Windows.Forms.Button();
            this.cmdPvPanelAngle = new System.Windows.Forms.Button();
            this.cmdCreatePvPole = new System.Windows.Forms.Button();
            this.cmdCheck4PvOnRoof = new System.Windows.Forms.CheckBox();
            this.cmdPVMapperWeb = new System.Windows.Forms.Button();
            this.cmdErrorReport = new System.Windows.Forms.Button();
            this.cmdRoseModel = new System.Windows.Forms.Button();
            this.cmdSunCalDialog = new System.Windows.Forms.Button();
            this.txtEffectiveAngle = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnDrawArea = new System.Windows.Forms.Button();
            this.btnKML = new System.Windows.Forms.Button();
            this.cmbSiteArea = new System.Windows.Forms.ComboBox();
            this.lblAreaCombo = new System.Windows.Forms.Label();
            this.btnMovePanels = new System.Windows.Forms.Button();
            this.btnAddPanel = new System.Windows.Forms.Button();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label32 = new System.Windows.Forms.Label();
            this.btnDrawAlignment = new System.Windows.Forms.Button();
            this.lblPanelLayer = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.chkDailyExp = new System.Windows.Forms.CheckBox();
            this.rdoAlignment = new System.Windows.Forms.RadioButton();
            this.pictureBox14 = new System.Windows.Forms.PictureBox();
            this.rdoKML = new System.Windows.Forms.RadioButton();
            this.rdoSiteArea = new System.Windows.Forms.RadioButton();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.lblTab05 = new System.Windows.Forms.Label();
            this.lblTab04 = new System.Windows.Forms.Label();
            this.lblTab02 = new System.Windows.Forms.Label();
            this.lblTab01 = new System.Windows.Forms.Label();
            this.lblTab03 = new System.Windows.Forms.Label();
            this.lstTreeImage = new System.Windows.Forms.ImageList(this.components);
            this.cmdRidgeLine = new System.Windows.Forms.Button();
            this.cmdExportRooftopPanetToSkecthUp = new System.Windows.Forms.Button();
            this.cmdCreateRooftopPanel = new System.Windows.Forms.Button();
            this.panelTab = new System.Windows.Forms.Panel();
            this.picTab06 = new System.Windows.Forms.PictureBox();
            this.picTab05 = new System.Windows.Forms.PictureBox();
            this.picTab04 = new System.Windows.Forms.PictureBox();
            this.picTab03 = new System.Windows.Forms.PictureBox();
            this.picTab02 = new System.Windows.Forms.PictureBox();
            this.picTab01 = new System.Windows.Forms.PictureBox();
            this.lblHome = new System.Windows.Forms.Label();
            this.tabFakeRibbon = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRoseScale = new System.Windows.Forms.TextBox();
            this.chkRosePlot = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.cmdForTest = new System.Windows.Forms.Button();
            this.txtWorkingPath = new System.Windows.Forms.TextBox();
            this.cmbBruTileLayer = new System.Windows.Forms.ComboBox();
            this.chkUseLastPath = new System.Windows.Forms.CheckBox();
            this.chkOnlineMap = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtTimeZone = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtLAT = new System.Windows.Forms.TextBox();
            this.txtLNG = new System.Windows.Forms.TextBox();
            this.txtUtmN = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUtmE = new System.Windows.Forms.TextBox();
            this.pvProgressbar = new System.Windows.Forms.ProgressBar();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label52 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.picSpliter1 = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.label59 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.label16 = new System.Windows.Forms.Label();
            this.btnAddTree = new System.Windows.Forms.Button();
            this.btnAddBuilding = new System.Windows.Forms.Button();
            this.pictureBox13 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox11 = new System.Windows.Forms.PictureBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label62 = new System.Windows.Forms.Label();
            this.txtPoa = new System.Windows.Forms.TextBox();
            this.txtTcell = new System.Windows.Forms.TextBox();
            this.lblPvSpec5 = new System.Windows.Forms.Label();
            this.txtTM2 = new System.Windows.Forms.TextBox();
            this.lblPvSpec4 = new System.Windows.Forms.Label();
            this.optSingleWeatherSta = new System.Windows.Forms.RadioButton();
            this.txtDerate = new System.Windows.Forms.TextBox();
            this.optMultiWeatherSta = new System.Windows.Forms.RadioButton();
            this.txtAreaPreSys = new System.Windows.Forms.TextBox();
            this.txtSystem_size = new System.Windows.Forms.TextBox();
            this.lblPvSpec2 = new System.Windows.Forms.Label();
            this.txtPowY = new System.Windows.Forms.TextBox();
            this.txtElev = new System.Windows.Forms.TextBox();
            this.txtPowX = new System.Windows.Forms.TextBox();
            this.label53 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.lblPvSpec1 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.txtNIdwSta = new System.Windows.Forms.TextBox();
            this.lblCity = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbCity = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.cmbState = new System.Windows.Forms.ComboBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox16 = new System.Windows.Forms.PictureBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.cmdCreatePvPosition = new System.Windows.Forms.Button();
            this.txtGridSpacingY = new System.Windows.Forms.TextBox();
            this.label55 = new System.Windows.Forms.Label();
            this.txtGridSpacingX = new System.Windows.Forms.TextBox();
            this.txtRoofAz = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.txtRoofTilt = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.pictureBox15 = new System.Windows.Forms.PictureBox();
            this.label47 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.cmdPvPanelConfig4Roof = new System.Windows.Forms.Button();
            this.cmbRoofTopPanelPosition = new System.Windows.Forms.ComboBox();
            this.label46 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.cmbRoofTopPanelPanel = new System.Windows.Forms.ComboBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.cmdRooftopAcCalculation = new System.Windows.Forms.Button();
            this.label49 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.cmdTiltDown = new System.Windows.Forms.Button();
            this.cmdTiltUp = new System.Windows.Forms.Button();
            this.panelDrawRoof = new System.Windows.Forms.Panel();
            this.label43 = new System.Windows.Forms.Label();
            this.txtPy = new System.Windows.Forms.TextBox();
            this.txtPx = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtBottomDepth = new System.Windows.Forms.TextBox();
            this.txtEaveHeight = new System.Windows.Forms.TextBox();
            this.txtRidgeHeight = new System.Windows.Forms.TextBox();
            this.pictureBox12 = new System.Windows.Forms.PictureBox();
            this.cmdRoofPlane = new System.Windows.Forms.Button();
            this.cmdEaveLine = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.btnStepByStep = new System.Windows.Forms.Button();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.label56 = new System.Windows.Forms.Label();
            this.cmdVDO = new System.Windows.Forms.Button();
            this.lstImgOnOff = new System.Windows.Forms.ImageList(this.components);
            this.appManager = new DotSpatial.Controls.AppManager();
            this.imgLstHelp = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabLegend.SuspendLayout();
            this.tabLayerVariables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picboxHelp)).BeginInit();
            this.pnlGrdProduction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAcProduct)).BeginInit();
            this.TabGOptimize.SuspendLayout();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSolarPanel)).BeginInit();
            this.tabPage13.SuspendLayout();
            this.tabPage12.SuspendLayout();
            this.tabPage14.SuspendLayout();
            this.tabPage15.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            this.panelTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTab06)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTab05)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTab04)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTab03)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTab02)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTab01)).BeginInit();
            this.tabFakeRibbon.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSpliter1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox16)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.panelDrawRoof.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).BeginInit();
            this.tabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 202);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cmdSwithToGraph);
            this.splitContainer1.Panel2.Controls.Add(this.cmdSwithToMap);
            this.splitContainer1.Panel2.Controls.Add(this.cmdSwithToTable);
            this.splitContainer1.Panel2.Controls.Add(this.pnlGrdProduction);
            this.splitContainer1.Panel2.Controls.Add(this.UpdateProgressBar);
            this.splitContainer1.Panel2.Controls.Add(this.cmdMapSelectionNone);
            this.splitContainer1.Panel2.Controls.Add(this.cmdMapSelection);
            this.splitContainer1.Panel2.Controls.Add(this.cmdMapZoomExt);
            this.splitContainer1.Panel2.Controls.Add(this.cmdMapZoomOut);
            this.splitContainer1.Panel2.Controls.Add(this.cmdMapPan);
            this.splitContainer1.Panel2.Controls.Add(this.cmdMapZoomIn);
            this.splitContainer1.Panel2.Controls.Add(this.TabGOptimize);
            this.splitContainer1.Panel2.Controls.Add(this.pvMap);
            this.splitContainer1.Panel2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.splitContainer1_Panel2_MouseMove);
            this.splitContainer1.Size = new System.Drawing.Size(816, 339);
            this.splitContainer1.SplitterDistance = 242;
            this.splitContainer1.TabIndex = 3;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.AutoScroll = true;
            this.splitContainer2.Panel2.AutoScrollMinSize = new System.Drawing.Size(200, 300);
            this.splitContainer2.Panel2.BackgroundImage = global::PVDESKTOP.Properties.Resources.HelpStep12;
            this.splitContainer2.Panel2.Controls.Add(this.btnCloseHelp);
            this.splitContainer2.Panel2.Controls.Add(this.picboxHelp);
            this.splitContainer2.Panel2Collapsed = true;
            this.splitContainer2.Size = new System.Drawing.Size(242, 339);
            this.splitContainer2.SplitterDistance = 162;
            this.splitContainer2.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabLegend);
            this.tabControl1.Controls.Add(this.tabLayerVariables);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(242, 339);
            this.tabControl1.TabIndex = 12;
            // 
            // tabLegend
            // 
            this.tabLegend.Controls.Add(this.legend1);
            this.tabLegend.Location = new System.Drawing.Point(4, 22);
            this.tabLegend.Name = "tabLegend";
            this.tabLegend.Padding = new System.Windows.Forms.Padding(3);
            this.tabLegend.Size = new System.Drawing.Size(234, 313);
            this.tabLegend.TabIndex = 0;
            this.tabLegend.Text = "Legend";
            this.tabLegend.UseVisualStyleBackColor = true;
            // 
            // legend1
            // 
            this.legend1.BackColor = System.Drawing.Color.White;
            this.legend1.ControlRectangle = new System.Drawing.Rectangle(0, 0, 228, 307);
            this.legend1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.legend1.DocumentRectangle = new System.Drawing.Rectangle(0, 0, 187, 428);
            this.legend1.HorizontalScrollEnabled = true;
            this.legend1.Indentation = 30;
            this.legend1.IsInitialized = false;
            this.legend1.Location = new System.Drawing.Point(3, 3);
            this.legend1.MinimumSize = new System.Drawing.Size(5, 5);
            this.legend1.Name = "legend1";
            this.legend1.ProgressHandler = null;
            this.legend1.ResetOnResize = false;
            this.legend1.SelectionFontColor = System.Drawing.Color.Black;
            this.legend1.SelectionHighlight = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(238)))), ((int)(((byte)(252)))));
            this.legend1.Size = new System.Drawing.Size(228, 307);
            this.legend1.TabIndex = 0;
            this.legend1.Text = "legend1";
            this.legend1.VerticalScrollEnabled = true;
            // 
            // tabLayerVariables
            // 
            this.tabLayerVariables.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tabLayerVariables.Controls.Add(this.propertyGrid1);
            this.tabLayerVariables.Location = new System.Drawing.Point(4, 22);
            this.tabLayerVariables.Name = "tabLayerVariables";
            this.tabLayerVariables.Padding = new System.Windows.Forms.Padding(3);
            this.tabLayerVariables.Size = new System.Drawing.Size(234, 313);
            this.tabLayerVariables.TabIndex = 1;
            this.tabLayerVariables.Text = "Project Properties";
            this.tabLayerVariables.UseVisualStyleBackColor = true;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(3, 3);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(228, 307);
            this.propertyGrid1.TabIndex = 0;
            // 
            // btnCloseHelp
            // 
            this.btnCloseHelp.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCloseHelp.Image = global::PVDESKTOP.Properties.Resources.close;
            this.btnCloseHelp.Location = new System.Drawing.Point(0, 0);
            this.btnCloseHelp.Margin = new System.Windows.Forms.Padding(2);
            this.btnCloseHelp.MaximumSize = new System.Drawing.Size(20, 20);
            this.btnCloseHelp.MinimumSize = new System.Drawing.Size(20, 20);
            this.btnCloseHelp.Name = "btnCloseHelp";
            this.btnCloseHelp.Size = new System.Drawing.Size(20, 20);
            this.btnCloseHelp.TabIndex = 2;
            this.btnCloseHelp.UseVisualStyleBackColor = true;
            this.btnCloseHelp.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // picboxHelp
            // 
            this.picboxHelp.BackColor = System.Drawing.SystemColors.Control;
            this.picboxHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picboxHelp.Image = global::PVDESKTOP.Properties.Resources.HelpStep12;
            this.picboxHelp.Location = new System.Drawing.Point(0, 0);
            this.picboxHelp.Name = "picboxHelp";
            this.picboxHelp.Size = new System.Drawing.Size(200, 300);
            this.picboxHelp.TabIndex = 1;
            this.picboxHelp.TabStop = false;
            // 
            // cmdSwithToGraph
            // 
            this.cmdSwithToGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSwithToGraph.Image = ((System.Drawing.Image)(resources.GetObject("cmdSwithToGraph.Image")));
            this.cmdSwithToGraph.Location = new System.Drawing.Point(519, 0);
            this.cmdSwithToGraph.Name = "cmdSwithToGraph";
            this.cmdSwithToGraph.Size = new System.Drawing.Size(26, 24);
            this.cmdSwithToGraph.TabIndex = 53;
            this.ttHelp.SetToolTip(this.cmdSwithToGraph, "Optimization chart");
            this.cmdSwithToGraph.UseVisualStyleBackColor = true;
            this.cmdSwithToGraph.Visible = false;
            this.cmdSwithToGraph.Click += new System.EventHandler(this.cmdSwithToGraph_Click);
            // 
            // cmdSwithToMap
            // 
            this.cmdSwithToMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSwithToMap.Image = ((System.Drawing.Image)(resources.GetObject("cmdSwithToMap.Image")));
            this.cmdSwithToMap.Location = new System.Drawing.Point(544, 0);
            this.cmdSwithToMap.Name = "cmdSwithToMap";
            this.cmdSwithToMap.Size = new System.Drawing.Size(26, 24);
            this.cmdSwithToMap.TabIndex = 2;
            this.ttHelp.SetToolTip(this.cmdSwithToMap, "Map view");
            this.cmdSwithToMap.UseVisualStyleBackColor = true;
            this.cmdSwithToMap.Click += new System.EventHandler(this.cmdSwithToMap_Click);
            // 
            // cmdSwithToTable
            // 
            this.cmdSwithToTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSwithToTable.Image = ((System.Drawing.Image)(resources.GetObject("cmdSwithToTable.Image")));
            this.cmdSwithToTable.Location = new System.Drawing.Point(544, 0);
            this.cmdSwithToTable.Name = "cmdSwithToTable";
            this.cmdSwithToTable.Size = new System.Drawing.Size(26, 24);
            this.cmdSwithToTable.TabIndex = 3;
            this.ttHelp.SetToolTip(this.cmdSwithToTable, "View Table");
            this.cmdSwithToTable.UseVisualStyleBackColor = true;
            this.cmdSwithToTable.Click += new System.EventHandler(this.cmdSwithToTable_Click);
            // 
            // pnlGrdProduction
            // 
            this.pnlGrdProduction.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlGrdProduction.Controls.Add(this.splitContainer3);
            this.pnlGrdProduction.Location = new System.Drawing.Point(395, 45);
            this.pnlGrdProduction.Name = "pnlGrdProduction";
            this.pnlGrdProduction.Size = new System.Drawing.Size(363, 206);
            this.pnlGrdProduction.TabIndex = 12;
            this.pnlGrdProduction.Visible = false;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.lblGrdTitle);
            this.splitContainer3.Panel1MinSize = 20;
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.grdAcProduct);
            this.splitContainer3.Size = new System.Drawing.Size(363, 206);
            this.splitContainer3.SplitterDistance = 25;
            this.splitContainer3.TabIndex = 0;
            // 
            // lblGrdTitle
            // 
            this.lblGrdTitle.AutoSize = true;
            this.lblGrdTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGrdTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrdTitle.Location = new System.Drawing.Point(0, 0);
            this.lblGrdTitle.Name = "lblGrdTitle";
            this.lblGrdTitle.Size = new System.Drawing.Size(241, 20);
            this.lblGrdTitle.TabIndex = 52;
            this.lblGrdTitle.Text = "Energy Production Estimates for:";
            // 
            // grdAcProduct
            // 
            this.grdAcProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdAcProduct.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.Column18,
            this.Column1,
            this.Column2,
            this.Column21,
            this.Column5,
            this.Column6,
            this.Column19,
            this.Column3,
            this.Column20});
            this.grdAcProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAcProduct.Location = new System.Drawing.Point(0, 0);
            this.grdAcProduct.Name = "grdAcProduct";
            this.grdAcProduct.Size = new System.Drawing.Size(363, 177);
            this.grdAcProduct.TabIndex = 51;
            this.grdAcProduct.Visible = false;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Set";
            this.Column4.Name = "Column4";
            this.Column4.Width = 60;
            // 
            // Column18
            // 
            this.Column18.HeaderText = "Month";
            this.Column18.Name = "Column18";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Panel Width (m)";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Panel Lenght (m)";
            this.Column2.Name = "Column2";
            // 
            // Column21
            // 
            this.Column21.HeaderText = "PVWatts AC Energy (kWh)";
            this.Column21.Name = "Column21";
            this.Column21.Width = 120;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Tilt (Deg)";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Azimuth (Deg)";
            this.Column6.Name = "Column6";
            // 
            // Column19
            // 
            this.Column19.HeaderText = "AC (kWh/m2)";
            this.Column19.Name = "Column19";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Amount";
            this.Column3.Name = "Column3";
            this.Column3.Width = 60;
            // 
            // Column20
            // 
            this.Column20.HeaderText = "Total AC (kWh)";
            this.Column20.Name = "Column20";
            // 
            // UpdateProgressBar
            // 
            this.UpdateProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.UpdateProgressBar.Location = new System.Drawing.Point(0, 315);
            this.UpdateProgressBar.Name = "UpdateProgressBar";
            this.UpdateProgressBar.Size = new System.Drawing.Size(570, 24);
            this.UpdateProgressBar.TabIndex = 55;
            this.UpdateProgressBar.Visible = false;
            // 
            // cmdMapSelectionNone
            // 
            this.cmdMapSelectionNone.FlatAppearance.BorderSize = 2;
            this.cmdMapSelectionNone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdMapSelectionNone.ForeColor = System.Drawing.Color.Silver;
            this.cmdMapSelectionNone.Image = ((System.Drawing.Image)(resources.GetObject("cmdMapSelectionNone.Image")));
            this.cmdMapSelectionNone.Location = new System.Drawing.Point(400, 296);
            this.cmdMapSelectionNone.Name = "cmdMapSelectionNone";
            this.cmdMapSelectionNone.Size = new System.Drawing.Size(24, 24);
            this.cmdMapSelectionNone.TabIndex = 62;
            this.cmdMapSelectionNone.UseVisualStyleBackColor = true;
            this.cmdMapSelectionNone.Click += new System.EventHandler(this.cmdMapSelectionNone_Click);
            // 
            // cmdMapSelection
            // 
            this.cmdMapSelection.FlatAppearance.BorderSize = 2;
            this.cmdMapSelection.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdMapSelection.ForeColor = System.Drawing.Color.Silver;
            this.cmdMapSelection.Image = ((System.Drawing.Image)(resources.GetObject("cmdMapSelection.Image")));
            this.cmdMapSelection.Location = new System.Drawing.Point(400, 257);
            this.cmdMapSelection.Name = "cmdMapSelection";
            this.cmdMapSelection.Size = new System.Drawing.Size(24, 24);
            this.cmdMapSelection.TabIndex = 61;
            this.ttHelp.SetToolTip(this.cmdMapSelection, "Select Frame");
            this.cmdMapSelection.UseVisualStyleBackColor = true;
            this.cmdMapSelection.Click += new System.EventHandler(this.cmdMapSelection_Click);
            // 
            // cmdMapZoomExt
            // 
            this.cmdMapZoomExt.FlatAppearance.BorderSize = 2;
            this.cmdMapZoomExt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdMapZoomExt.ForeColor = System.Drawing.Color.Silver;
            this.cmdMapZoomExt.Image = ((System.Drawing.Image)(resources.GetObject("cmdMapZoomExt.Image")));
            this.cmdMapZoomExt.Location = new System.Drawing.Point(401, 181);
            this.cmdMapZoomExt.Name = "cmdMapZoomExt";
            this.cmdMapZoomExt.Size = new System.Drawing.Size(24, 24);
            this.cmdMapZoomExt.TabIndex = 59;
            this.ttHelp.SetToolTip(this.cmdMapZoomExt, "Zoom Frame");
            this.cmdMapZoomExt.UseVisualStyleBackColor = true;
            this.cmdMapZoomExt.Click += new System.EventHandler(this.cmdMapZoomExt_Click);
            // 
            // cmdMapZoomOut
            // 
            this.cmdMapZoomOut.FlatAppearance.BorderSize = 2;
            this.cmdMapZoomOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdMapZoomOut.ForeColor = System.Drawing.Color.Silver;
            this.cmdMapZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("cmdMapZoomOut.Image")));
            this.cmdMapZoomOut.Location = new System.Drawing.Point(401, 142);
            this.cmdMapZoomOut.Name = "cmdMapZoomOut";
            this.cmdMapZoomOut.Size = new System.Drawing.Size(24, 24);
            this.cmdMapZoomOut.TabIndex = 58;
            this.ttHelp.SetToolTip(this.cmdMapZoomOut, "Zoom Out");
            this.cmdMapZoomOut.UseVisualStyleBackColor = true;
            this.cmdMapZoomOut.Click += new System.EventHandler(this.cmdMapZoomOut_Click);
            // 
            // cmdMapPan
            // 
            this.cmdMapPan.FlatAppearance.BorderSize = 2;
            this.cmdMapPan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdMapPan.ForeColor = System.Drawing.Color.Silver;
            this.cmdMapPan.Image = ((System.Drawing.Image)(resources.GetObject("cmdMapPan.Image")));
            this.cmdMapPan.Location = new System.Drawing.Point(383, 169);
            this.cmdMapPan.Name = "cmdMapPan";
            this.cmdMapPan.Size = new System.Drawing.Size(24, 24);
            this.cmdMapPan.TabIndex = 60;
            this.ttHelp.SetToolTip(this.cmdMapPan, "Pan");
            this.cmdMapPan.UseVisualStyleBackColor = true;
            this.cmdMapPan.Click += new System.EventHandler(this.cmdMapPan_Click);
            // 
            // cmdMapZoomIn
            // 
            this.cmdMapZoomIn.FlatAppearance.BorderSize = 2;
            this.cmdMapZoomIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdMapZoomIn.ForeColor = System.Drawing.Color.Silver;
            this.cmdMapZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("cmdMapZoomIn.Image")));
            this.cmdMapZoomIn.Location = new System.Drawing.Point(401, 103);
            this.cmdMapZoomIn.Name = "cmdMapZoomIn";
            this.cmdMapZoomIn.Size = new System.Drawing.Size(24, 24);
            this.cmdMapZoomIn.TabIndex = 56;
            this.ttHelp.SetToolTip(this.cmdMapZoomIn, "Zoom In");
            this.cmdMapZoomIn.UseVisualStyleBackColor = true;
            this.cmdMapZoomIn.Click += new System.EventHandler(this.cmdMapZoomIn_Click);
            // 
            // TabGOptimize
            // 
            this.TabGOptimize.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.TabGOptimize.Controls.Add(this.tabPage6);
            this.TabGOptimize.Controls.Add(this.tabPage13);
            this.TabGOptimize.Controls.Add(this.tabPage12);
            this.TabGOptimize.Controls.Add(this.tabPage14);
            this.TabGOptimize.Controls.Add(this.tabPage15);
            this.TabGOptimize.Location = new System.Drawing.Point(-1, 84);
            this.TabGOptimize.Multiline = true;
            this.TabGOptimize.Name = "TabGOptimize";
            this.TabGOptimize.SelectedIndex = 0;
            this.TabGOptimize.Size = new System.Drawing.Size(394, 249);
            this.TabGOptimize.TabIndex = 52;
            this.TabGOptimize.Visible = false;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.txtPvWidth);
            this.tabPage6.Controls.Add(this.txtPvLength);
            this.tabPage6.Controls.Add(this.picSolarPanel);
            this.tabPage6.Controls.Add(this.pvTilt);
            this.tabPage6.Controls.Add(this.pvAz);
            this.tabPage6.Controls.Add(this.cmdOptimization);
            this.tabPage6.Location = new System.Drawing.Point(4, 4);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(386, 223);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Main";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // txtPvWidth
            // 
            this.txtPvWidth.Location = new System.Drawing.Point(35, 124);
            this.txtPvWidth.Name = "txtPvWidth";
            this.txtPvWidth.Size = new System.Drawing.Size(34, 20);
            this.txtPvWidth.TabIndex = 18;
            this.txtPvWidth.Text = "2.00";
            // 
            // txtPvLength
            // 
            this.txtPvLength.Location = new System.Drawing.Point(1, 60);
            this.txtPvLength.Name = "txtPvLength";
            this.txtPvLength.Size = new System.Drawing.Size(28, 20);
            this.txtPvLength.TabIndex = 17;
            this.txtPvLength.Text = "3.00";
            // 
            // picSolarPanel
            // 
            this.picSolarPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picSolarPanel.BackgroundImage")));
            this.picSolarPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picSolarPanel.Location = new System.Drawing.Point(11, 28);
            this.picSolarPanel.Name = "picSolarPanel";
            this.picSolarPanel.Size = new System.Drawing.Size(79, 113);
            this.picSolarPanel.TabIndex = 16;
            this.picSolarPanel.TabStop = false;
            // 
            // pvTilt
            // 
            this.pvTilt.BackColor = System.Drawing.Color.White;
            this.pvTilt.Location = new System.Drawing.Point(206, 1);
            this.pvTilt.Name = "pvTilt";
            this.pvTilt.Size = new System.Drawing.Size(135, 145);
            this.pvTilt.TabIndex = 14;
            this.pvTilt.tiltAngle = 0F;
            // 
            // pvAz
            // 
            this.pvAz.AzimutAngle = 0F;
            this.pvAz.BackColor = System.Drawing.Color.White;
            this.pvAz.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pvAz.BackgroundImage")));
            this.pvAz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pvAz.Location = new System.Drawing.Point(104, 19);
            this.pvAz.Name = "pvAz";
            this.pvAz.Size = new System.Drawing.Size(100, 125);
            this.pvAz.TabIndex = 0;
            // 
            // cmdOptimization
            // 
            this.cmdOptimization.BackColor = System.Drawing.Color.White;
            this.cmdOptimization.Image = ((System.Drawing.Image)(resources.GetObject("cmdOptimization.Image")));
            this.cmdOptimization.Location = new System.Drawing.Point(14, 150);
            this.cmdOptimization.Name = "cmdOptimization";
            this.cmdOptimization.Size = new System.Drawing.Size(66, 69);
            this.cmdOptimization.TabIndex = 13;
            this.cmdOptimization.UseVisualStyleBackColor = false;
            this.cmdOptimization.Click += new System.EventHandler(this.cmdOptimization_Click);
            // 
            // tabPage13
            // 
            this.tabPage13.Controls.Add(this.zedGOpti1);
            this.tabPage13.Location = new System.Drawing.Point(4, 4);
            this.tabPage13.Name = "tabPage13";
            this.tabPage13.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage13.Size = new System.Drawing.Size(386, 223);
            this.tabPage13.TabIndex = 1;
            this.tabPage13.Text = "Chart1";
            this.tabPage13.UseVisualStyleBackColor = true;
            // 
            // zedGOpti1
            // 
            this.zedGOpti1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGOpti1.Location = new System.Drawing.Point(3, 3);
            this.zedGOpti1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.zedGOpti1.Name = "zedGOpti1";
            this.zedGOpti1.ScrollGrace = 0D;
            this.zedGOpti1.ScrollMaxX = 0D;
            this.zedGOpti1.ScrollMaxY = 0D;
            this.zedGOpti1.ScrollMaxY2 = 0D;
            this.zedGOpti1.ScrollMinX = 0D;
            this.zedGOpti1.ScrollMinY = 0D;
            this.zedGOpti1.ScrollMinY2 = 0D;
            this.zedGOpti1.Size = new System.Drawing.Size(380, 217);
            this.zedGOpti1.TabIndex = 10;
            // 
            // tabPage12
            // 
            this.tabPage12.Controls.Add(this.zedGOpti2);
            this.tabPage12.Location = new System.Drawing.Point(4, 4);
            this.tabPage12.Name = "tabPage12";
            this.tabPage12.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage12.Size = new System.Drawing.Size(386, 223);
            this.tabPage12.TabIndex = 2;
            this.tabPage12.Text = "Chart2";
            this.tabPage12.UseVisualStyleBackColor = true;
            // 
            // zedGOpti2
            // 
            this.zedGOpti2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGOpti2.Location = new System.Drawing.Point(3, 3);
            this.zedGOpti2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.zedGOpti2.Name = "zedGOpti2";
            this.zedGOpti2.ScrollGrace = 0D;
            this.zedGOpti2.ScrollMaxX = 0D;
            this.zedGOpti2.ScrollMaxY = 0D;
            this.zedGOpti2.ScrollMaxY2 = 0D;
            this.zedGOpti2.ScrollMinX = 0D;
            this.zedGOpti2.ScrollMinY = 0D;
            this.zedGOpti2.ScrollMinY2 = 0D;
            this.zedGOpti2.Size = new System.Drawing.Size(380, 217);
            this.zedGOpti2.TabIndex = 11;
            // 
            // tabPage14
            // 
            this.tabPage14.Controls.Add(this.zedGOpti3);
            this.tabPage14.Location = new System.Drawing.Point(4, 4);
            this.tabPage14.Name = "tabPage14";
            this.tabPage14.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage14.Size = new System.Drawing.Size(386, 223);
            this.tabPage14.TabIndex = 3;
            this.tabPage14.Text = "Chart3";
            this.tabPage14.UseVisualStyleBackColor = true;
            // 
            // zedGOpti3
            // 
            this.zedGOpti3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGOpti3.Location = new System.Drawing.Point(3, 3);
            this.zedGOpti3.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.zedGOpti3.Name = "zedGOpti3";
            this.zedGOpti3.ScrollGrace = 0D;
            this.zedGOpti3.ScrollMaxX = 0D;
            this.zedGOpti3.ScrollMaxY = 0D;
            this.zedGOpti3.ScrollMaxY2 = 0D;
            this.zedGOpti3.ScrollMinX = 0D;
            this.zedGOpti3.ScrollMinY = 0D;
            this.zedGOpti3.ScrollMinY2 = 0D;
            this.zedGOpti3.Size = new System.Drawing.Size(380, 217);
            this.zedGOpti3.TabIndex = 11;
            // 
            // tabPage15
            // 
            this.tabPage15.Controls.Add(this.zedGOpti4);
            this.tabPage15.Location = new System.Drawing.Point(4, 4);
            this.tabPage15.Name = "tabPage15";
            this.tabPage15.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage15.Size = new System.Drawing.Size(386, 223);
            this.tabPage15.TabIndex = 4;
            this.tabPage15.Text = "Chart4";
            this.tabPage15.UseVisualStyleBackColor = true;
            // 
            // zedGOpti4
            // 
            this.zedGOpti4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGOpti4.Location = new System.Drawing.Point(3, 3);
            this.zedGOpti4.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.zedGOpti4.Name = "zedGOpti4";
            this.zedGOpti4.ScrollGrace = 0D;
            this.zedGOpti4.ScrollMaxX = 0D;
            this.zedGOpti4.ScrollMaxY = 0D;
            this.zedGOpti4.ScrollMaxY2 = 0D;
            this.zedGOpti4.ScrollMinX = 0D;
            this.zedGOpti4.ScrollMinY = 0D;
            this.zedGOpti4.ScrollMinY2 = 0D;
            this.zedGOpti4.Size = new System.Drawing.Size(380, 217);
            this.zedGOpti4.TabIndex = 11;
            // 
            // pvMap
            // 
            this.pvMap.AllowDrop = true;
            this.pvMap.BackColor = System.Drawing.Color.White;
            this.pvMap.CollectAfterDraw = false;
            this.pvMap.CollisionDetection = true;
            this.pvMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pvMap.ExtendBuffer = false;
            this.pvMap.FunctionMode = DotSpatial.Controls.FunctionMode.None;
            this.pvMap.IsBusy = false;
            this.pvMap.IsZoomedToMaxExtent = true;
            this.pvMap.Legend = this.legend1;
            this.pvMap.Location = new System.Drawing.Point(0, 0);
            this.pvMap.Name = "pvMap";
            this.pvMap.ProgressHandler = null;
            this.pvMap.ProjectionModeDefine = DotSpatial.Controls.ActionMode.PromptOnce;
            this.pvMap.ProjectionModeReproject = DotSpatial.Controls.ActionMode.PromptOnce;
            this.pvMap.RedrawLayersWhileResizing = false;
            this.pvMap.SelectionEnabled = true;
            this.pvMap.Size = new System.Drawing.Size(570, 339);
            this.pvMap.TabIndex = 0;
            this.pvMap.LayerAdded += new System.EventHandler<DotSpatial.Symbology.LayerEventArgs>(this.pvMap_LayerAdded);
            this.pvMap.Load += new System.EventHandler(this.pvMap_Load);
            this.pvMap.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pvMap_KeyDown);
            this.pvMap.KeyUp += new System.Windows.Forms.KeyEventHandler(this.pvMap_KeyUp);
            this.pvMap.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pvMap_MouseDoubleClick);
            this.pvMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pvMap_MouseDown);
            this.pvMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pvMap_MouseMove);
            this.pvMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pvMap_MouseUp);
            this.pvMap.Resize += new System.EventHandler(this.pvMap_Resize);
            // 
            // cmdRedrawRoofPlan
            // 
            this.cmdRedrawRoofPlan.BackColor = System.Drawing.Color.White;
            this.cmdRedrawRoofPlan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdRedrawRoofPlan.Enabled = false;
            this.cmdRedrawRoofPlan.Image = ((System.Drawing.Image)(resources.GetObject("cmdRedrawRoofPlan.Image")));
            this.cmdRedrawRoofPlan.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cmdRedrawRoofPlan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmdRedrawRoofPlan.Location = new System.Drawing.Point(643, 50);
            this.cmdRedrawRoofPlan.Name = "cmdRedrawRoofPlan";
            this.cmdRedrawRoofPlan.Size = new System.Drawing.Size(24, 24);
            this.cmdRedrawRoofPlan.TabIndex = 127;
            this.ttHelp.SetToolTip(this.cmdRedrawRoofPlan, "Redraw roof plane");
            this.cmdRedrawRoofPlan.UseVisualStyleBackColor = false;
            this.cmdRedrawRoofPlan.Visible = false;
            this.cmdRedrawRoofPlan.Click += new System.EventHandler(this.cmdRedrawRoofPlan_Click);
            // 
            // cmdExportSketchUp
            // 
            this.cmdExportSketchUp.BackColor = System.Drawing.Color.White;
            this.cmdExportSketchUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdExportSketchUp.Enabled = false;
            this.cmdExportSketchUp.Image = ((System.Drawing.Image)(resources.GetObject("cmdExportSketchUp.Image")));
            this.cmdExportSketchUp.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdExportSketchUp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmdExportSketchUp.Location = new System.Drawing.Point(303, 4);
            this.cmdExportSketchUp.Name = "cmdExportSketchUp";
            this.cmdExportSketchUp.Size = new System.Drawing.Size(50, 73);
            this.cmdExportSketchUp.TabIndex = 183;
            this.cmdExportSketchUp.Text = "3D Export";
            this.cmdExportSketchUp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttHelp.SetToolTip(this.cmdExportSketchUp, "Export PV panel file to Google SketchUp");
            this.cmdExportSketchUp.UseVisualStyleBackColor = false;
            this.cmdExportSketchUp.Click += new System.EventHandler(this.cmdExportSketchUp_Click);
            // 
            // cmdPickCentroid
            // 
            this.cmdPickCentroid.BackColor = System.Drawing.Color.White;
            this.cmdPickCentroid.Image = ((System.Drawing.Image)(resources.GetObject("cmdPickCentroid.Image")));
            this.cmdPickCentroid.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdPickCentroid.Location = new System.Drawing.Point(80, 1);
            this.cmdPickCentroid.Name = "cmdPickCentroid";
            this.cmdPickCentroid.Size = new System.Drawing.Size(46, 72);
            this.cmdPickCentroid.TabIndex = 93;
            this.cmdPickCentroid.Text = "Select Site";
            this.cmdPickCentroid.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttHelp.SetToolTip(this.cmdPickCentroid, "Select the location of the site");
            this.cmdPickCentroid.UseVisualStyleBackColor = false;
            this.cmdPickCentroid.Click += new System.EventHandler(this.cmdPickCentroid_Click);
            // 
            // cmdUseCurrentPath
            // 
            this.cmdUseCurrentPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdUseCurrentPath.Enabled = false;
            this.cmdUseCurrentPath.Image = ((System.Drawing.Image)(resources.GetObject("cmdUseCurrentPath.Image")));
            this.cmdUseCurrentPath.Location = new System.Drawing.Point(535, 29);
            this.cmdUseCurrentPath.Name = "cmdUseCurrentPath";
            this.cmdUseCurrentPath.Size = new System.Drawing.Size(25, 25);
            this.cmdUseCurrentPath.TabIndex = 171;
            this.ttHelp.SetToolTip(this.cmdUseCurrentPath, "Browse and select local working directory");
            this.cmdUseCurrentPath.UseVisualStyleBackColor = true;
            this.cmdUseCurrentPath.Click += new System.EventHandler(this.cmdUseCurrentPath_Click);
            // 
            // ExportBldgAndTrr2SketchUp
            // 
            this.ExportBldgAndTrr2SketchUp.BackColor = System.Drawing.Color.White;
            this.ExportBldgAndTrr2SketchUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ExportBldgAndTrr2SketchUp.Enabled = false;
            this.ExportBldgAndTrr2SketchUp.Image = ((System.Drawing.Image)(resources.GetObject("ExportBldgAndTrr2SketchUp.Image")));
            this.ExportBldgAndTrr2SketchUp.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.ExportBldgAndTrr2SketchUp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ExportBldgAndTrr2SketchUp.Location = new System.Drawing.Point(369, 7);
            this.ExportBldgAndTrr2SketchUp.Name = "ExportBldgAndTrr2SketchUp";
            this.ExportBldgAndTrr2SketchUp.Size = new System.Drawing.Size(47, 73);
            this.ExportBldgAndTrr2SketchUp.TabIndex = 184;
            this.ExportBldgAndTrr2SketchUp.Text = "3D\r\nExport\r\n";
            this.ExportBldgAndTrr2SketchUp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttHelp.SetToolTip(this.ExportBldgAndTrr2SketchUp, "Export solar obstruction files to Google SketchUp ");
            this.ExportBldgAndTrr2SketchUp.UseVisualStyleBackColor = false;
            this.ExportBldgAndTrr2SketchUp.Click += new System.EventHandler(this.ExportBldgAndTrr2SketchUp_Click);
            // 
            // cmdShowIdwSta
            // 
            this.cmdShowIdwSta.Image = ((System.Drawing.Image)(resources.GetObject("cmdShowIdwSta.Image")));
            this.cmdShowIdwSta.Location = new System.Drawing.Point(423, 2);
            this.cmdShowIdwSta.Name = "cmdShowIdwSta";
            this.cmdShowIdwSta.Size = new System.Drawing.Size(22, 23);
            this.cmdShowIdwSta.TabIndex = 2;
            this.cmdShowIdwSta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttHelp.SetToolTip(this.cmdShowIdwSta, "Redraw lines to reference weather stations");
            this.cmdShowIdwSta.UseVisualStyleBackColor = true;
            this.cmdShowIdwSta.Click += new System.EventHandler(this.cmdShowIdwSta_Click);
            // 
            // cmdAddKML
            // 
            this.cmdAddKML.BackColor = System.Drawing.Color.White;
            this.cmdAddKML.Image = ((System.Drawing.Image)(resources.GetObject("cmdAddKML.Image")));
            this.cmdAddKML.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdAddKML.Location = new System.Drawing.Point(3, 2);
            this.cmdAddKML.Name = "cmdAddKML";
            this.cmdAddKML.Size = new System.Drawing.Size(68, 72);
            this.cmdAddKML.TabIndex = 88;
            this.cmdAddKML.Text = "Import Site from KML";
            this.cmdAddKML.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttHelp.SetToolTip(this.cmdAddKML, "Add site from PVMapper");
            this.cmdAddKML.UseVisualStyleBackColor = false;
            this.cmdAddKML.Click += new System.EventHandler(this.cmdAddKML_Click);
            // 
            // cmdZoomToSite
            // 
            this.cmdZoomToSite.BackColor = System.Drawing.Color.White;
            this.cmdZoomToSite.Enabled = false;
            this.cmdZoomToSite.Image = ((System.Drawing.Image)(resources.GetObject("cmdZoomToSite.Image")));
            this.cmdZoomToSite.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdZoomToSite.Location = new System.Drawing.Point(295, 1);
            this.cmdZoomToSite.Name = "cmdZoomToSite";
            this.cmdZoomToSite.Size = new System.Drawing.Size(46, 72);
            this.cmdZoomToSite.TabIndex = 93;
            this.cmdZoomToSite.Text = "Zoom to Site";
            this.cmdZoomToSite.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttHelp.SetToolTip(this.cmdZoomToSite, "Zoom to site of reference point");
            this.cmdZoomToSite.UseVisualStyleBackColor = false;
            this.cmdZoomToSite.Click += new System.EventHandler(this.cmdZoomToSite_Click);
            // 
            // cmdSaveConfig
            // 
            this.cmdSaveConfig.BackColor = System.Drawing.Color.White;
            this.cmdSaveConfig.Image = ((System.Drawing.Image)(resources.GetObject("cmdSaveConfig.Image")));
            this.cmdSaveConfig.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdSaveConfig.Location = new System.Drawing.Point(564, 1);
            this.cmdSaveConfig.Name = "cmdSaveConfig";
            this.cmdSaveConfig.Size = new System.Drawing.Size(46, 72);
            this.cmdSaveConfig.TabIndex = 88;
            this.cmdSaveConfig.Text = "Save Config";
            this.cmdSaveConfig.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttHelp.SetToolTip(this.cmdSaveConfig, "Save configuration");
            this.cmdSaveConfig.UseVisualStyleBackColor = false;
            this.cmdSaveConfig.Click += new System.EventHandler(this.cmdSaveConfig_Click);
            // 
            // cmdSelectTreeLayer
            // 
            this.cmdSelectTreeLayer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdSelectTreeLayer.Enabled = false;
            this.cmdSelectTreeLayer.Image = ((System.Drawing.Image)(resources.GetObject("cmdSelectTreeLayer.Image")));
            this.cmdSelectTreeLayer.Location = new System.Drawing.Point(1081, 8);
            this.cmdSelectTreeLayer.Name = "cmdSelectTreeLayer";
            this.cmdSelectTreeLayer.Size = new System.Drawing.Size(25, 25);
            this.cmdSelectTreeLayer.TabIndex = 173;
            this.ttHelp.SetToolTip(this.cmdSelectTreeLayer, "Select tree layer as current layer");
            this.cmdSelectTreeLayer.UseVisualStyleBackColor = true;
            this.cmdSelectTreeLayer.Visible = false;
            this.cmdSelectTreeLayer.Click += new System.EventHandler(this.cmdSelectTreeLayer_Click);
            // 
            // cmdEditTreePropDialog
            // 
            this.cmdEditTreePropDialog.BackColor = System.Drawing.Color.White;
            this.cmdEditTreePropDialog.Enabled = false;
            this.cmdEditTreePropDialog.Image = ((System.Drawing.Image)(resources.GetObject("cmdEditTreePropDialog.Image")));
            this.cmdEditTreePropDialog.Location = new System.Drawing.Point(247, 41);
            this.cmdEditTreePropDialog.Name = "cmdEditTreePropDialog";
            this.cmdEditTreePropDialog.Size = new System.Drawing.Size(36, 36);
            this.cmdEditTreePropDialog.TabIndex = 105;
            this.cmdEditTreePropDialog.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttHelp.SetToolTip(this.cmdEditTreePropDialog, "Edit tree attributes");
            this.cmdEditTreePropDialog.UseVisualStyleBackColor = false;
            this.cmdEditTreePropDialog.Click += new System.EventHandler(this.cmdEditTreePropDialog_Click);
            // 
            // cmdSelectTree
            // 
            this.cmdSelectTree.BackColor = System.Drawing.Color.White;
            this.cmdSelectTree.Enabled = false;
            this.cmdSelectTree.Image = ((System.Drawing.Image)(resources.GetObject("cmdSelectTree.Image")));
            this.cmdSelectTree.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdSelectTree.Location = new System.Drawing.Point(247, 5);
            this.cmdSelectTree.Name = "cmdSelectTree";
            this.cmdSelectTree.Size = new System.Drawing.Size(36, 36);
            this.cmdSelectTree.TabIndex = 104;
            this.cmdSelectTree.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttHelp.SetToolTip(this.cmdSelectTree, "Select trees to edit");
            this.cmdSelectTree.UseVisualStyleBackColor = false;
            this.cmdSelectTree.Click += new System.EventHandler(this.cmdSelectTree_Click);
            // 
            // cmdSelectBuilding
            // 
            this.cmdSelectBuilding.BackColor = System.Drawing.Color.White;
            this.cmdSelectBuilding.Enabled = false;
            this.cmdSelectBuilding.Image = ((System.Drawing.Image)(resources.GetObject("cmdSelectBuilding.Image")));
            this.cmdSelectBuilding.Location = new System.Drawing.Point(61, 4);
            this.cmdSelectBuilding.Margin = new System.Windows.Forms.Padding(1);
            this.cmdSelectBuilding.Name = "cmdSelectBuilding";
            this.cmdSelectBuilding.Size = new System.Drawing.Size(36, 36);
            this.cmdSelectBuilding.TabIndex = 104;
            this.cmdSelectBuilding.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttHelp.SetToolTip(this.cmdSelectBuilding, "Select buildings to edit");
            this.cmdSelectBuilding.UseVisualStyleBackColor = false;
            this.cmdSelectBuilding.Click += new System.EventHandler(this.cmdSelectBuilding_Click);
            // 
            // cmdBuilding
            // 
            this.cmdBuilding.BackColor = System.Drawing.Color.White;
            this.cmdBuilding.Enabled = false;
            this.cmdBuilding.Image = ((System.Drawing.Image)(resources.GetObject("cmdBuilding.Image")));
            this.cmdBuilding.Location = new System.Drawing.Point(61, 41);
            this.cmdBuilding.Name = "cmdBuilding";
            this.cmdBuilding.Size = new System.Drawing.Size(36, 36);
            this.cmdBuilding.TabIndex = 104;
            this.cmdBuilding.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttHelp.SetToolTip(this.cmdBuilding, "Edit building attributes");
            this.cmdBuilding.UseVisualStyleBackColor = false;
            this.cmdBuilding.Click += new System.EventHandler(this.cmdBuilding_Click);
            // 
            // cmdWeatherFile
            // 
            this.cmdWeatherFile.BackColor = System.Drawing.Color.White;
            this.cmdWeatherFile.Image = ((System.Drawing.Image)(resources.GetObject("cmdWeatherFile.Image")));
            this.cmdWeatherFile.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdWeatherFile.Location = new System.Drawing.Point(3, 0);
            this.cmdWeatherFile.Name = "cmdWeatherFile";
            this.cmdWeatherFile.Size = new System.Drawing.Size(57, 73);
            this.cmdWeatherFile.TabIndex = 99;
            this.cmdWeatherFile.Text = "Weather File";
            this.cmdWeatherFile.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttHelp.SetToolTip(this.cmdWeatherFile, "Import weather data file (TM2)");
            this.cmdWeatherFile.UseVisualStyleBackColor = false;
            this.cmdWeatherFile.Click += new System.EventHandler(this.cmdWeatherFile_Click);
            // 
            // cmbDem
            // 
            this.cmbDem.Enabled = false;
            this.cmbDem.FormattingEnabled = true;
            this.cmbDem.Location = new System.Drawing.Point(875, 62);
            this.cmbDem.Name = "cmbDem";
            this.cmbDem.Size = new System.Drawing.Size(79, 21);
            this.cmbDem.TabIndex = 185;
            this.ttHelp.SetToolTip(this.cmbDem, "Terrain layer");
            this.cmbDem.Visible = false;
            // 
            // cmbSolarFarmArea
            // 
            this.cmbSolarFarmArea.Enabled = false;
            this.cmbSolarFarmArea.FormattingEnabled = true;
            this.cmbSolarFarmArea.Location = new System.Drawing.Point(765, 29);
            this.cmbSolarFarmArea.Name = "cmbSolarFarmArea";
            this.cmbSolarFarmArea.Size = new System.Drawing.Size(104, 21);
            this.cmbSolarFarmArea.TabIndex = 182;
            this.ttHelp.SetToolTip(this.cmbSolarFarmArea, "Area layer");
            this.cmbSolarFarmArea.Visible = false;
            // 
            // cmbPolePosition
            // 
            this.cmbPolePosition.FormattingEnabled = true;
            this.cmbPolePosition.Location = new System.Drawing.Point(875, 1);
            this.cmbPolePosition.Name = "cmbPolePosition";
            this.cmbPolePosition.Size = new System.Drawing.Size(102, 21);
            this.cmbPolePosition.TabIndex = 181;
            this.ttHelp.SetToolTip(this.cmbPolePosition, "PV panel position layer");
            this.cmbPolePosition.Visible = false;
            // 
            // cmbPanel
            // 
            this.cmbPanel.FormattingEnabled = true;
            this.cmbPanel.Location = new System.Drawing.Point(765, 62);
            this.cmbPanel.Name = "cmbPanel";
            this.cmbPanel.Size = new System.Drawing.Size(104, 21);
            this.cmbPanel.TabIndex = 175;
            this.ttHelp.SetToolTip(this.cmbPanel, "Select the PV Panel Array for which to estimate energy production");
            this.cmbPanel.Visible = false;
            // 
            // cmbAlignmentLyr
            // 
            this.cmbAlignmentLyr.FormattingEnabled = true;
            this.cmbAlignmentLyr.Location = new System.Drawing.Point(898, 29);
            this.cmbAlignmentLyr.Name = "cmbAlignmentLyr";
            this.cmbAlignmentLyr.Size = new System.Drawing.Size(79, 21);
            this.cmbAlignmentLyr.TabIndex = 170;
            this.ttHelp.SetToolTip(this.cmbAlignmentLyr, "Alignment layer");
            this.cmbAlignmentLyr.Visible = false;
            // 
            // cmdNewAligmnentShp
            // 
            this.cmdNewAligmnentShp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdNewAligmnentShp.Location = new System.Drawing.Point(875, 26);
            this.cmdNewAligmnentShp.Name = "cmdNewAligmnentShp";
            this.cmdNewAligmnentShp.Size = new System.Drawing.Size(30, 30);
            this.cmdNewAligmnentShp.TabIndex = 167;
            this.ttHelp.SetToolTip(this.cmdNewAligmnentShp, "Create alignment shapefile (line)");
            this.cmdNewAligmnentShp.UseVisualStyleBackColor = true;
            this.cmdNewAligmnentShp.Visible = false;
            this.cmdNewAligmnentShp.Click += new System.EventHandler(this.cmdNewAligmnentShp_Click);
            // 
            // cmdEnergyCal
            // 
            this.cmdEnergyCal.BackColor = System.Drawing.Color.White;
            this.cmdEnergyCal.Image = ((System.Drawing.Image)(resources.GetObject("cmdEnergyCal.Image")));
            this.cmdEnergyCal.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdEnergyCal.Location = new System.Drawing.Point(370, 4);
            this.cmdEnergyCal.Name = "cmdEnergyCal";
            this.cmdEnergyCal.Size = new System.Drawing.Size(53, 73);
            this.cmdEnergyCal.TabIndex = 119;
            this.cmdEnergyCal.Text = "Energy Product";
            this.cmdEnergyCal.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttHelp.SetToolTip(this.cmdEnergyCal, "Calculate PV array energy production");
            this.cmdEnergyCal.UseVisualStyleBackColor = false;
            this.cmdEnergyCal.Click += new System.EventHandler(this.cmdEnergyCal_Click);
            // 
            // cmdPvPanelAngle
            // 
            this.cmdPvPanelAngle.BackColor = System.Drawing.Color.White;
            this.cmdPvPanelAngle.Enabled = false;
            this.cmdPvPanelAngle.Image = ((System.Drawing.Image)(resources.GetObject("cmdPvPanelAngle.Image")));
            this.cmdPvPanelAngle.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdPvPanelAngle.Location = new System.Drawing.Point(195, 4);
            this.cmdPvPanelAngle.Name = "cmdPvPanelAngle";
            this.cmdPvPanelAngle.Size = new System.Drawing.Size(50, 73);
            this.cmdPvPanelAngle.TabIndex = 119;
            this.cmdPvPanelAngle.Text = "Edit Panels";
            this.cmdPvPanelAngle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttHelp.SetToolTip(this.cmdPvPanelAngle, "Edit orientation of selected panels");
            this.cmdPvPanelAngle.UseVisualStyleBackColor = false;
            this.cmdPvPanelAngle.Click += new System.EventHandler(this.cmdPvPanelAngle_Click);
            // 
            // cmdCreatePvPole
            // 
            this.cmdCreatePvPole.BackColor = System.Drawing.Color.White;
            this.cmdCreatePvPole.Enabled = false;
            this.cmdCreatePvPole.Image = ((System.Drawing.Image)(resources.GetObject("cmdCreatePvPole.Image")));
            this.cmdCreatePvPole.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdCreatePvPole.Location = new System.Drawing.Point(141, 4);
            this.cmdCreatePvPole.Name = "cmdCreatePvPole";
            this.cmdCreatePvPole.Size = new System.Drawing.Size(50, 73);
            this.cmdCreatePvPole.TabIndex = 119;
            this.cmdCreatePvPole.Text = "Panel\r\nData";
            this.cmdCreatePvPole.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttHelp.SetToolTip(this.cmdCreatePvPole, "Select spacing to create PV position");
            this.cmdCreatePvPole.UseVisualStyleBackColor = false;
            this.cmdCreatePvPole.Click += new System.EventHandler(this.cmdCreatePvPole_Click);
            // 
            // cmdCheck4PvOnRoof
            // 
            this.cmdCheck4PvOnRoof.AutoSize = true;
            this.cmdCheck4PvOnRoof.Checked = true;
            this.cmdCheck4PvOnRoof.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cmdCheck4PvOnRoof.Location = new System.Drawing.Point(811, 50);
            this.cmdCheck4PvOnRoof.Name = "cmdCheck4PvOnRoof";
            this.cmdCheck4PvOnRoof.Size = new System.Drawing.Size(78, 17);
            this.cmdCheck4PvOnRoof.TabIndex = 198;
            this.cmdCheck4PvOnRoof.Text = "Orthogonal";
            this.ttHelp.SetToolTip(this.cmdCheck4PvOnRoof, "Orthogonal layout");
            this.cmdCheck4PvOnRoof.UseVisualStyleBackColor = true;
            this.cmdCheck4PvOnRoof.Visible = false;
            // 
            // cmdPVMapperWeb
            // 
            this.cmdPVMapperWeb.BackColor = System.Drawing.Color.White;
            this.cmdPVMapperWeb.Image = ((System.Drawing.Image)(resources.GetObject("cmdPVMapperWeb.Image")));
            this.cmdPVMapperWeb.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdPVMapperWeb.Location = new System.Drawing.Point(58, 2);
            this.cmdPVMapperWeb.Name = "cmdPVMapperWeb";
            this.cmdPVMapperWeb.Size = new System.Drawing.Size(46, 72);
            this.cmdPVMapperWeb.TabIndex = 95;
            this.cmdPVMapperWeb.Text = "Web Page";
            this.cmdPVMapperWeb.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttHelp.SetToolTip(this.cmdPVMapperWeb, "Go to PVMapper website");
            this.cmdPVMapperWeb.UseVisualStyleBackColor = false;
            this.cmdPVMapperWeb.Click += new System.EventHandler(this.cmdPVMapperWeb_Click_1);
            // 
            // cmdErrorReport
            // 
            this.cmdErrorReport.BackColor = System.Drawing.Color.White;
            this.cmdErrorReport.Image = ((System.Drawing.Image)(resources.GetObject("cmdErrorReport.Image")));
            this.cmdErrorReport.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdErrorReport.Location = new System.Drawing.Point(4, 2);
            this.cmdErrorReport.Name = "cmdErrorReport";
            this.cmdErrorReport.Size = new System.Drawing.Size(53, 72);
            this.cmdErrorReport.TabIndex = 94;
            this.cmdErrorReport.Text = "Report Error";
            this.cmdErrorReport.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttHelp.SetToolTip(this.cmdErrorReport, "Report errors to PVDesktop.CodePlex.com");
            this.cmdErrorReport.UseVisualStyleBackColor = false;
            this.cmdErrorReport.Click += new System.EventHandler(this.cmdErrorReport_Click_1);
            // 
            // cmdRoseModel
            // 
            this.cmdRoseModel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdRoseModel.BackgroundImage")));
            this.cmdRoseModel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdRoseModel.Location = new System.Drawing.Point(668, 19);
            this.cmdRoseModel.Name = "cmdRoseModel";
            this.cmdRoseModel.Size = new System.Drawing.Size(27, 27);
            this.cmdRoseModel.TabIndex = 178;
            this.ttHelp.SetToolTip(this.cmdRoseModel, "Create sun-rose diagram");
            this.cmdRoseModel.UseVisualStyleBackColor = true;
            this.cmdRoseModel.Click += new System.EventHandler(this.cmdRoseModel_Click);
            // 
            // cmdSunCalDialog
            // 
            this.cmdSunCalDialog.BackColor = System.Drawing.Color.White;
            this.cmdSunCalDialog.Image = ((System.Drawing.Image)(resources.GetObject("cmdSunCalDialog.Image")));
            this.cmdSunCalDialog.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdSunCalDialog.Location = new System.Drawing.Point(619, 0);
            this.cmdSunCalDialog.Name = "cmdSunCalDialog";
            this.cmdSunCalDialog.Size = new System.Drawing.Size(46, 72);
            this.cmdSunCalDialog.TabIndex = 175;
            this.cmdSunCalDialog.Text = "Sun Path";
            this.cmdSunCalDialog.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttHelp.SetToolTip(this.cmdSunCalDialog, "Solar data tables");
            this.cmdSunCalDialog.UseVisualStyleBackColor = false;
            this.cmdSunCalDialog.Click += new System.EventHandler(this.cmdSunCalDialog_Click);
            // 
            // txtEffectiveAngle
            // 
            this.txtEffectiveAngle.Location = new System.Drawing.Point(464, 58);
            this.txtEffectiveAngle.Name = "txtEffectiveAngle";
            this.txtEffectiveAngle.Size = new System.Drawing.Size(38, 20);
            this.txtEffectiveAngle.TabIndex = 132;
            this.txtEffectiveAngle.Text = "20";
            this.ttHelp.SetToolTip(this.txtEffectiveAngle, "Shadows will not be drawn for sun elevations less than this angle");
            // 
            // tabPage4
            // 
            this.tabPage4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPage4.BackgroundImage")));
            this.tabPage4.Controls.Add(this.btnDrawArea);
            this.tabPage4.Controls.Add(this.btnKML);
            this.tabPage4.Controls.Add(this.cmbSiteArea);
            this.tabPage4.Controls.Add(this.lblAreaCombo);
            this.tabPage4.Controls.Add(this.btnMovePanels);
            this.tabPage4.Controls.Add(this.btnAddPanel);
            this.tabPage4.Controls.Add(this.pictureBox4);
            this.tabPage4.Controls.Add(this.label32);
            this.tabPage4.Controls.Add(this.cmbPolePosition);
            this.tabPage4.Controls.Add(this.btnDrawAlignment);
            this.tabPage4.Controls.Add(this.lblPanelLayer);
            this.tabPage4.Controls.Add(this.label63);
            this.tabPage4.Controls.Add(this.cmbPanel);
            this.tabPage4.Controls.Add(this.chkDailyExp);
            this.tabPage4.Controls.Add(this.rdoAlignment);
            this.tabPage4.Controls.Add(this.pictureBox14);
            this.tabPage4.Controls.Add(this.cmbDem);
            this.tabPage4.Controls.Add(this.cmdExportSketchUp);
            this.tabPage4.Controls.Add(this.cmbSolarFarmArea);
            this.tabPage4.Controls.Add(this.cmbAlignmentLyr);
            this.tabPage4.Controls.Add(this.rdoKML);
            this.tabPage4.Controls.Add(this.rdoSiteArea);
            this.tabPage4.Controls.Add(this.cmdNewAligmnentShp);
            this.tabPage4.Controls.Add(this.pictureBox9);
            this.tabPage4.Controls.Add(this.cmdEnergyCal);
            this.tabPage4.Controls.Add(this.cmdPvPanelAngle);
            this.tabPage4.Controls.Add(this.cmdCreatePvPole);
            this.tabPage4.Controls.Add(this.label33);
            this.tabPage4.Controls.Add(this.label19);
            this.tabPage4.Location = new System.Drawing.Point(4, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1211, 122);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "tabPage4";
            this.ttHelp.SetToolTip(this.tabPage4, "Click to select width, height, azimuth, and tilt of panels");
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnDrawArea
            // 
            this.btnDrawArea.Image = ((System.Drawing.Image)(resources.GetObject("btnDrawArea.Image")));
            this.btnDrawArea.Location = new System.Drawing.Point(75, 4);
            this.btnDrawArea.Name = "btnDrawArea";
            this.btnDrawArea.Size = new System.Drawing.Size(50, 73);
            this.btnDrawArea.TabIndex = 189;
            this.btnDrawArea.Text = "Draw";
            this.btnDrawArea.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttHelp.SetToolTip(this.btnDrawArea, "Use this tool to draw site area boundry on map");
            this.btnDrawArea.UseVisualStyleBackColor = true;
            this.btnDrawArea.Click += new System.EventHandler(this.btnDrawArea_Click);
            // 
            // btnKML
            // 
            this.btnKML.BackColor = System.Drawing.Color.White;
            this.btnKML.Image = ((System.Drawing.Image)(resources.GetObject("btnKML.Image")));
            this.btnKML.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnKML.Location = new System.Drawing.Point(75, 4);
            this.btnKML.Name = "btnKML";
            this.btnKML.Size = new System.Drawing.Size(50, 73);
            this.btnKML.TabIndex = 199;
            this.btnKML.Text = "Import KML";
            this.btnKML.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttHelp.SetToolTip(this.btnKML, "Add site from PVMapper");
            this.btnKML.UseVisualStyleBackColor = false;
            this.btnKML.Visible = false;
            this.btnKML.Click += new System.EventHandler(this.btnKML_Click);
            // 
            // cmbSiteArea
            // 
            this.cmbSiteArea.FormattingEnabled = true;
            this.cmbSiteArea.Location = new System.Drawing.Point(427, 28);
            this.cmbSiteArea.Name = "cmbSiteArea";
            this.cmbSiteArea.Size = new System.Drawing.Size(104, 21);
            this.cmbSiteArea.TabIndex = 198;
            this.cmbSiteArea.SelectedIndexChanged += new System.EventHandler(this.cmbSiteArea_SelectedIndexChanged);
            // 
            // lblAreaCombo
            // 
            this.lblAreaCombo.AutoSize = true;
            this.lblAreaCombo.Location = new System.Drawing.Point(429, 9);
            this.lblAreaCombo.Name = "lblAreaCombo";
            this.lblAreaCombo.Size = new System.Drawing.Size(77, 13);
            this.lblAreaCombo.TabIndex = 195;
            this.lblAreaCombo.Text = "Current Project";
            this.ttHelp.SetToolTip(this.lblAreaCombo, "Select the site area to work with.");
            // 
            // btnMovePanels
            // 
            this.btnMovePanels.BackColor = System.Drawing.Color.White;
            this.btnMovePanels.Enabled = false;
            this.btnMovePanels.Image = ((System.Drawing.Image)(resources.GetObject("btnMovePanels.Image")));
            this.btnMovePanels.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnMovePanels.Location = new System.Drawing.Point(249, 4);
            this.btnMovePanels.Name = "btnMovePanels";
            this.btnMovePanels.Size = new System.Drawing.Size(50, 73);
            this.btnMovePanels.TabIndex = 194;
            this.btnMovePanels.Text = "Move Panels";
            this.btnMovePanels.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttHelp.SetToolTip(this.btnMovePanels, "Select panels to move then while holding Ctrl\r\n use numPad arrows to move selecte" +
        "d panels");
            this.btnMovePanels.UseVisualStyleBackColor = false;
            this.btnMovePanels.Click += new System.EventHandler(this.btnMovePanels_Click);
            // 
            // btnAddPanel
            // 
            this.btnAddPanel.Image = ((System.Drawing.Image)(resources.GetObject("btnAddPanel.Image")));
            this.btnAddPanel.Location = new System.Drawing.Point(564, 4);
            this.btnAddPanel.Name = "btnAddPanel";
            this.btnAddPanel.Size = new System.Drawing.Size(50, 73);
            this.btnAddPanel.TabIndex = 193;
            this.btnAddPanel.Text = "Add Panel";
            this.btnAddPanel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAddPanel.UseVisualStyleBackColor = true;
            this.btnAddPanel.Visible = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(131, 2);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(5, 86);
            this.pictureBox4.TabIndex = 192;
            this.pictureBox4.TabStop = false;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(784, 4);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(90, 13);
            this.label32.TabIndex = 173;
            this.label32.Text = "PV Position Layer";
            this.label32.Visible = false;
            // 
            // btnDrawAlignment
            // 
            this.btnDrawAlignment.Image = ((System.Drawing.Image)(resources.GetObject("btnDrawAlignment.Image")));
            this.btnDrawAlignment.Location = new System.Drawing.Point(75, 4);
            this.btnDrawAlignment.Name = "btnDrawAlignment";
            this.btnDrawAlignment.Size = new System.Drawing.Size(50, 73);
            this.btnDrawAlignment.TabIndex = 191;
            this.btnDrawAlignment.Text = "Draw";
            this.btnDrawAlignment.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttHelp.SetToolTip(this.btnDrawAlignment, "Use this tool to draw alignments on map");
            this.btnDrawAlignment.UseVisualStyleBackColor = true;
            this.btnDrawAlignment.Click += new System.EventHandler(this.btnDrawAlignment_Click);
            // 
            // lblPanelLayer
            // 
            this.lblPanelLayer.AutoSize = true;
            this.lblPanelLayer.Location = new System.Drawing.Point(687, 9);
            this.lblPanelLayer.Name = "lblPanelLayer";
            this.lblPanelLayer.Size = new System.Drawing.Size(80, 13);
            this.lblPanelLayer.TabIndex = 176;
            this.lblPanelLayer.Text = "PV Panel Layer";
            this.lblPanelLayer.Visible = false;
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.ForeColor = System.Drawing.Color.Gray;
            this.label63.Location = new System.Drawing.Point(38, 78);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(58, 13);
            this.label63.TabIndex = 190;
            this.label63.Text = "Array Type";
            // 
            // chkDailyExp
            // 
            this.chkDailyExp.AutoSize = true;
            this.chkDailyExp.Location = new System.Drawing.Point(427, 55);
            this.chkDailyExp.Name = "chkDailyExp";
            this.chkDailyExp.Size = new System.Drawing.Size(120, 17);
            this.chkDailyExp.TabIndex = 184;
            this.chkDailyExp.Text = "Export Daily Results";
            this.ttHelp.SetToolTip(this.chkDailyExp, "Exports a list of daily production estimates to an excel spreadsheet");
            this.chkDailyExp.UseVisualStyleBackColor = true;
            // 
            // rdoAlignment
            // 
            this.rdoAlignment.AutoSize = true;
            this.rdoAlignment.Location = new System.Drawing.Point(6, 33);
            this.rdoAlignment.Name = "rdoAlignment";
            this.rdoAlignment.Size = new System.Drawing.Size(71, 17);
            this.rdoAlignment.TabIndex = 171;
            this.rdoAlignment.Text = "Alignment";
            this.ttHelp.SetToolTip(this.rdoAlignment, "Create a line on which PV panels will be placed at specified spacing");
            this.rdoAlignment.UseVisualStyleBackColor = true;
            this.rdoAlignment.CheckedChanged += new System.EventHandler(this.rdolignment_CheckedChanged);
            // 
            // pictureBox14
            // 
            this.pictureBox14.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox14.Image")));
            this.pictureBox14.Location = new System.Drawing.Point(553, 3);
            this.pictureBox14.Name = "pictureBox14";
            this.pictureBox14.Size = new System.Drawing.Size(5, 86);
            this.pictureBox14.TabIndex = 123;
            this.pictureBox14.TabStop = false;
            // 
            // rdoKML
            // 
            this.rdoKML.AutoSize = true;
            this.rdoKML.Location = new System.Drawing.Point(6, 54);
            this.rdoKML.Name = "rdoKML";
            this.rdoKML.Size = new System.Drawing.Size(47, 17);
            this.rdoKML.TabIndex = 171;
            this.rdoKML.Text = "KML";
            this.rdoKML.UseVisualStyleBackColor = true;
            this.rdoKML.CheckedChanged += new System.EventHandler(this.rdoKML_CheckedChanged);
            // 
            // rdoSiteArea
            // 
            this.rdoSiteArea.AutoSize = true;
            this.rdoSiteArea.Checked = true;
            this.rdoSiteArea.Location = new System.Drawing.Point(6, 12);
            this.rdoSiteArea.Name = "rdoSiteArea";
            this.rdoSiteArea.Size = new System.Drawing.Size(64, 17);
            this.rdoSiteArea.TabIndex = 171;
            this.rdoSiteArea.TabStop = true;
            this.rdoSiteArea.Text = "Boundry";
            this.ttHelp.SetToolTip(this.rdoSiteArea, "Create a polygon boundry in which PV panel array will be located.");
            this.rdoSiteArea.UseVisualStyleBackColor = true;
            this.rdoSiteArea.CheckedChanged += new System.EventHandler(this.rdoSiteArea_CheckedChanged);
            // 
            // pictureBox9
            // 
            this.pictureBox9.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox9.Image")));
            this.pictureBox9.Location = new System.Drawing.Point(359, 2);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(5, 86);
            this.pictureBox9.TabIndex = 123;
            this.pictureBox9.TabStop = false;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.ForeColor = System.Drawing.Color.Gray;
            this.label33.Location = new System.Drawing.Point(404, 78);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(100, 13);
            this.label33.TabIndex = 117;
            this.label33.Text = "Energy Calculations";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.Gray;
            this.label19.Location = new System.Drawing.Point(205, 78);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(90, 13);
            this.label19.TabIndex = 117;
            this.label19.Text = "PV Array Creation";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(461, 42);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(121, 13);
            this.label61.TabIndex = 131;
            this.label61.Text = "Effective shadow angle:";
            this.ttHelp.SetToolTip(this.label61, "Shadows will not be drawn for sun elevations less than this angle");
            // 
            // lblTab05
            // 
            this.lblTab05.AutoSize = true;
            this.lblTab05.BackColor = System.Drawing.Color.Transparent;
            this.lblTab05.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTab05.Location = new System.Drawing.Point(237, 5);
            this.lblTab05.Name = "lblTab05";
            this.lblTab05.Size = new System.Drawing.Size(66, 17);
            this.lblTab05.TabIndex = 7;
            this.lblTab05.Text = "ROOFTOP";
            this.ttHelp.SetToolTip(this.lblTab05, "Create panel layout on roofs");
            this.lblTab05.Click += new System.EventHandler(this.lblTab05_Click);
            // 
            // lblTab04
            // 
            this.lblTab04.AutoSize = true;
            this.lblTab04.BackColor = System.Drawing.Color.Transparent;
            this.lblTab04.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTab04.Location = new System.Drawing.Point(179, 5);
            this.lblTab04.Name = "lblTab04";
            this.lblTab04.Size = new System.Drawing.Size(55, 17);
            this.lblTab04.TabIndex = 7;
            this.lblTab04.Text = "LAYOUT";
            this.ttHelp.SetToolTip(this.lblTab04, "Create array layout and estimate potential energy production");
            this.lblTab04.Click += new System.EventHandler(this.lblTab04_Click);
            // 
            // lblTab02
            // 
            this.lblTab02.AutoSize = true;
            this.lblTab02.BackColor = System.Drawing.Color.Transparent;
            this.lblTab02.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTab02.Location = new System.Drawing.Point(43, 5);
            this.lblTab02.Name = "lblTab02";
            this.lblTab02.Size = new System.Drawing.Size(63, 17);
            this.lblTab02.TabIndex = 7;
            this.lblTab02.Text = "SHADING";
            this.ttHelp.SetToolTip(this.lblTab02, "Identify solar obstructions");
            this.lblTab02.Click += new System.EventHandler(this.lblTab02_Click);
            // 
            // lblTab01
            // 
            this.lblTab01.AutoSize = true;
            this.lblTab01.BackColor = System.Drawing.Color.Transparent;
            this.lblTab01.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTab01.ForeColor = System.Drawing.Color.Green;
            this.lblTab01.Location = new System.Drawing.Point(7, 5);
            this.lblTab01.Name = "lblTab01";
            this.lblTab01.Size = new System.Drawing.Size(32, 17);
            this.lblTab01.TabIndex = 7;
            this.lblTab01.Text = "SITE";
            this.ttHelp.SetToolTip(this.lblTab01, "Select location ");
            this.lblTab01.Click += new System.EventHandler(this.lblTab01_Click);
            // 
            // lblTab03
            // 
            this.lblTab03.AutoSize = true;
            this.lblTab03.BackColor = System.Drawing.Color.Transparent;
            this.lblTab03.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTab03.Location = new System.Drawing.Point(109, 5);
            this.lblTab03.Name = "lblTab03";
            this.lblTab03.Size = new System.Drawing.Size(66, 17);
            this.lblTab03.TabIndex = 7;
            this.lblTab03.Text = "WEATHER";
            this.ttHelp.SetToolTip(this.lblTab03, "Customize weather and panel properities");
            this.lblTab03.Click += new System.EventHandler(this.lblTab03_Click);
            // 
            // lstTreeImage
            // 
            this.lstTreeImage.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("lstTreeImage.ImageStream")));
            this.lstTreeImage.TransparentColor = System.Drawing.Color.Transparent;
            this.lstTreeImage.Images.SetKeyName(0, "tree_form01.jpg");
            this.lstTreeImage.Images.SetKeyName(1, "tree_form02.jpg");
            this.lstTreeImage.Images.SetKeyName(2, "tree_form03.jpg");
            this.lstTreeImage.Images.SetKeyName(3, "tree_form04.jpg");
            this.lstTreeImage.Images.SetKeyName(4, "tree_form05.jpg");
            this.lstTreeImage.Images.SetKeyName(5, "tree_form06.jpg");
            this.lstTreeImage.Images.SetKeyName(6, "tree_form07.jpg");
            this.lstTreeImage.Images.SetKeyName(7, "tree_form08.jpg");
            this.lstTreeImage.Images.SetKeyName(8, "tree_form09.jpg");
            this.lstTreeImage.Images.SetKeyName(9, "tree_form10.jpg");
            // 
            // cmdRidgeLine
            // 
            this.cmdRidgeLine.BackColor = System.Drawing.Color.White;
            this.cmdRidgeLine.Image = ((System.Drawing.Image)(resources.GetObject("cmdRidgeLine.Image")));
            this.cmdRidgeLine.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdRidgeLine.Location = new System.Drawing.Point(3, 3);
            this.cmdRidgeLine.Name = "cmdRidgeLine";
            this.cmdRidgeLine.Size = new System.Drawing.Size(46, 73);
            this.cmdRidgeLine.TabIndex = 126;
            this.cmdRidgeLine.Text = "Ridge Line";
            this.cmdRidgeLine.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdRidgeLine.UseVisualStyleBackColor = false;
            this.cmdRidgeLine.Click += new System.EventHandler(this.cmdRidgeLine_Click);
            // 
            // cmdExportRooftopPanetToSkecthUp
            // 
            this.cmdExportRooftopPanetToSkecthUp.BackColor = System.Drawing.Color.White;
            this.cmdExportRooftopPanetToSkecthUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdExportRooftopPanetToSkecthUp.Enabled = false;
            this.cmdExportRooftopPanetToSkecthUp.Image = ((System.Drawing.Image)(resources.GetObject("cmdExportRooftopPanetToSkecthUp.Image")));
            this.cmdExportRooftopPanetToSkecthUp.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdExportRooftopPanetToSkecthUp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmdExportRooftopPanetToSkecthUp.Location = new System.Drawing.Point(355, 3);
            this.cmdExportRooftopPanetToSkecthUp.Name = "cmdExportRooftopPanetToSkecthUp";
            this.cmdExportRooftopPanetToSkecthUp.Size = new System.Drawing.Size(53, 73);
            this.cmdExportRooftopPanetToSkecthUp.TabIndex = 191;
            this.cmdExportRooftopPanetToSkecthUp.Text = "3-D Export";
            this.cmdExportRooftopPanetToSkecthUp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdExportRooftopPanetToSkecthUp.UseVisualStyleBackColor = false;
            this.cmdExportRooftopPanetToSkecthUp.Click += new System.EventHandler(this.cmdExportRooftopPanetToSkecthUp_Click);
            // 
            // cmdCreateRooftopPanel
            // 
            this.cmdCreateRooftopPanel.BackColor = System.Drawing.Color.White;
            this.cmdCreateRooftopPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.cmdCreateRooftopPanel.Image = ((System.Drawing.Image)(resources.GetObject("cmdCreateRooftopPanel.Image")));
            this.cmdCreateRooftopPanel.Location = new System.Drawing.Point(785, 65);
            this.cmdCreateRooftopPanel.Name = "cmdCreateRooftopPanel";
            this.cmdCreateRooftopPanel.Size = new System.Drawing.Size(24, 24);
            this.cmdCreateRooftopPanel.TabIndex = 195;
            this.cmdCreateRooftopPanel.UseVisualStyleBackColor = false;
            this.cmdCreateRooftopPanel.Visible = false;
            this.cmdCreateRooftopPanel.Click += new System.EventHandler(this.cmdCreateRooftopPanel_Click);
            // 
            // panelTab
            // 
            this.panelTab.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTab.BackColor = System.Drawing.Color.White;
            this.panelTab.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelTab.BackgroundImage")));
            this.panelTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelTab.Controls.Add(this.picTab06);
            this.panelTab.Controls.Add(this.picTab05);
            this.panelTab.Controls.Add(this.picTab04);
            this.panelTab.Controls.Add(this.picTab03);
            this.panelTab.Controls.Add(this.picTab02);
            this.panelTab.Controls.Add(this.picTab01);
            this.panelTab.Controls.Add(this.lblHome);
            this.panelTab.Controls.Add(this.lblTab05);
            this.panelTab.Controls.Add(this.lblTab04);
            this.panelTab.Controls.Add(this.lblTab02);
            this.panelTab.Controls.Add(this.lblTab01);
            this.panelTab.Controls.Add(this.lblTab03);
            this.panelTab.Location = new System.Drawing.Point(0, 3);
            this.panelTab.Name = "panelTab";
            this.panelTab.Size = new System.Drawing.Size(1219, 32);
            this.panelTab.TabIndex = 11;
            // 
            // picTab06
            // 
            this.picTab06.Image = ((System.Drawing.Image)(resources.GetObject("picTab06.Image")));
            this.picTab06.Location = new System.Drawing.Point(601, 2);
            this.picTab06.Name = "picTab06";
            this.picTab06.Size = new System.Drawing.Size(25, 23);
            this.picTab06.TabIndex = 8;
            this.picTab06.TabStop = false;
            this.picTab06.Visible = false;
            // 
            // picTab05
            // 
            this.picTab05.Image = ((System.Drawing.Image)(resources.GetObject("picTab05.Image")));
            this.picTab05.Location = new System.Drawing.Point(570, 3);
            this.picTab05.Name = "picTab05";
            this.picTab05.Size = new System.Drawing.Size(25, 23);
            this.picTab05.TabIndex = 0;
            this.picTab05.TabStop = false;
            this.picTab05.Visible = false;
            // 
            // picTab04
            // 
            this.picTab04.Image = ((System.Drawing.Image)(resources.GetObject("picTab04.Image")));
            this.picTab04.Location = new System.Drawing.Point(539, 3);
            this.picTab04.Name = "picTab04";
            this.picTab04.Size = new System.Drawing.Size(25, 23);
            this.picTab04.TabIndex = 0;
            this.picTab04.TabStop = false;
            this.picTab04.Visible = false;
            // 
            // picTab03
            // 
            this.picTab03.Image = ((System.Drawing.Image)(resources.GetObject("picTab03.Image")));
            this.picTab03.Location = new System.Drawing.Point(508, 3);
            this.picTab03.Name = "picTab03";
            this.picTab03.Size = new System.Drawing.Size(25, 23);
            this.picTab03.TabIndex = 0;
            this.picTab03.TabStop = false;
            this.picTab03.Visible = false;
            // 
            // picTab02
            // 
            this.picTab02.Image = ((System.Drawing.Image)(resources.GetObject("picTab02.Image")));
            this.picTab02.Location = new System.Drawing.Point(477, 3);
            this.picTab02.Name = "picTab02";
            this.picTab02.Size = new System.Drawing.Size(25, 23);
            this.picTab02.TabIndex = 0;
            this.picTab02.TabStop = false;
            this.picTab02.Visible = false;
            // 
            // picTab01
            // 
            this.picTab01.Image = ((System.Drawing.Image)(resources.GetObject("picTab01.Image")));
            this.picTab01.Location = new System.Drawing.Point(446, 3);
            this.picTab01.Name = "picTab01";
            this.picTab01.Size = new System.Drawing.Size(25, 23);
            this.picTab01.TabIndex = 0;
            this.picTab01.TabStop = false;
            this.picTab01.Visible = false;
            // 
            // lblHome
            // 
            this.lblHome.AutoSize = true;
            this.lblHome.BackColor = System.Drawing.Color.Transparent;
            this.lblHome.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHome.Location = new System.Drawing.Point(306, 5);
            this.lblHome.Name = "lblHome";
            this.lblHome.Size = new System.Drawing.Size(37, 17);
            this.lblHome.TabIndex = 7;
            this.lblHome.Text = "HELP";
            this.lblHome.Click += new System.EventHandler(this.lblHome_Click);
            // 
            // tabFakeRibbon
            // 
            this.tabFakeRibbon.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabFakeRibbon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabFakeRibbon.Controls.Add(this.tabPage1);
            this.tabFakeRibbon.Controls.Add(this.tabPage2);
            this.tabFakeRibbon.Controls.Add(this.tabPage3);
            this.tabFakeRibbon.Controls.Add(this.tabPage4);
            this.tabFakeRibbon.Controls.Add(this.tabPage5);
            this.tabFakeRibbon.Controls.Add(this.tabPage7);
            this.tabFakeRibbon.Location = new System.Drawing.Point(0, 31);
            this.tabFakeRibbon.Name = "tabFakeRibbon";
            this.tabFakeRibbon.SelectedIndex = 0;
            this.tabFakeRibbon.Size = new System.Drawing.Size(1219, 148);
            this.tabFakeRibbon.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPage1.BackgroundImage")));
            this.tabPage1.Controls.Add(this.dateTimePicker1);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.cmdRoseModel);
            this.tabPage1.Controls.Add(this.txtRoseScale);
            this.tabPage1.Controls.Add(this.chkRosePlot);
            this.tabPage1.Controls.Add(this.cmdSunCalDialog);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.pictureBox8);
            this.tabPage1.Controls.Add(this.cmdForTest);
            this.tabPage1.Controls.Add(this.cmdUseCurrentPath);
            this.tabPage1.Controls.Add(this.txtWorkingPath);
            this.tabPage1.Controls.Add(this.cmbBruTileLayer);
            this.tabPage1.Controls.Add(this.chkUseLastPath);
            this.tabPage1.Controls.Add(this.chkOnlineMap);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.pvProgressbar);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.cmdZoomToSite);
            this.tabPage1.Controls.Add(this.cmdPickCentroid);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.pictureBox3);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.cmdSaveConfig);
            this.tabPage1.Controls.Add(this.cmdAddKML);
            this.tabPage1.Controls.Add(this.label52);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.picSpliter1);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1211, 122);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(668, 49);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(124, 20);
            this.dateTimePicker1.TabIndex = 180;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(696, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 13);
            this.label5.TabIndex = 179;
            this.label5.Text = "Sun Rose Diagram";
            // 
            // txtRoseScale
            // 
            this.txtRoseScale.Location = new System.Drawing.Point(758, 1);
            this.txtRoseScale.Name = "txtRoseScale";
            this.txtRoseScale.Size = new System.Drawing.Size(29, 20);
            this.txtRoseScale.TabIndex = 176;
            this.txtRoseScale.Text = "1";
            // 
            // chkRosePlot
            // 
            this.chkRosePlot.AutoSize = true;
            this.chkRosePlot.Checked = true;
            this.chkRosePlot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRosePlot.Location = new System.Drawing.Point(667, 4);
            this.chkRosePlot.Name = "chkRosePlot";
            this.chkRosePlot.Size = new System.Drawing.Size(91, 17);
            this.chkRosePlot.TabIndex = 177;
            this.chkRosePlot.Text = "Plotting Scale";
            this.chkRosePlot.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Gray;
            this.label15.Location = new System.Drawing.Point(668, 77);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(81, 13);
            this.label15.TabIndex = 174;
            this.label15.Text = "Solar Properties";
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox8.Image")));
            this.pictureBox8.Location = new System.Drawing.Point(799, 0);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(5, 86);
            this.pictureBox8.TabIndex = 173;
            this.pictureBox8.TabStop = false;
            // 
            // cmdForTest
            // 
            this.cmdForTest.Location = new System.Drawing.Point(1074, 6);
            this.cmdForTest.Name = "cmdForTest";
            this.cmdForTest.Size = new System.Drawing.Size(60, 27);
            this.cmdForTest.TabIndex = 172;
            this.cmdForTest.Text = "test";
            this.cmdForTest.UseVisualStyleBackColor = true;
            this.cmdForTest.Visible = false;
            this.cmdForTest.Click += new System.EventHandler(this.cmdForTest_Click);
            // 
            // txtWorkingPath
            // 
            this.txtWorkingPath.Enabled = false;
            this.txtWorkingPath.Location = new System.Drawing.Point(375, 32);
            this.txtWorkingPath.Name = "txtWorkingPath";
            this.txtWorkingPath.Size = new System.Drawing.Size(158, 20);
            this.txtWorkingPath.TabIndex = 160;
            this.txtWorkingPath.TextChanged += new System.EventHandler(this.txtWorkingPath_TextChanged);
            // 
            // cmbBruTileLayer
            // 
            this.cmbBruTileLayer.FormattingEnabled = true;
            this.cmbBruTileLayer.Items.AddRange(new object[] {
            "None",
            "BingAerialLayer",
            "BingHybridLayer",
            "GoogleMapLayer",
            "GoogleSatelliteLayer",
            "GoogleTerrainLayer",
            "OsmLayer"});
            this.cmbBruTileLayer.Location = new System.Drawing.Point(353, 6);
            this.cmbBruTileLayer.Name = "cmbBruTileLayer";
            this.cmbBruTileLayer.Size = new System.Drawing.Size(204, 21);
            this.cmbBruTileLayer.TabIndex = 159;
            this.cmbBruTileLayer.Text = "None";
            this.cmbBruTileLayer.SelectedIndexChanged += new System.EventHandler(this.cmbBruTileLayer_SelectedIndexChanged);
            // 
            // chkUseLastPath
            // 
            this.chkUseLastPath.AutoSize = true;
            this.chkUseLastPath.Location = new System.Drawing.Point(355, 35);
            this.chkUseLastPath.Name = "chkUseLastPath";
            this.chkUseLastPath.Size = new System.Drawing.Size(15, 14);
            this.chkUseLastPath.TabIndex = 158;
            this.chkUseLastPath.UseVisualStyleBackColor = true;
            this.chkUseLastPath.CheckedChanged += new System.EventHandler(this.chkUseLastPath_CheckedChanged);
            // 
            // chkOnlineMap
            // 
            this.chkOnlineMap.AutoSize = true;
            this.chkOnlineMap.Location = new System.Drawing.Point(353, 7);
            this.chkOnlineMap.Name = "chkOnlineMap";
            this.chkOnlineMap.Size = new System.Drawing.Size(107, 17);
            this.chkOnlineMap.TabIndex = 157;
            this.chkOnlineMap.Text = "Load Online Map";
            this.chkOnlineMap.UseVisualStyleBackColor = true;
            this.chkOnlineMap.CheckedChanged += new System.EventHandler(this.chkOnlineMap_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel1.Location = new System.Drawing.Point(130, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(160, 72);
            this.panel1.TabIndex = 148;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.txtTimeZone);
            this.panel2.Controls.Add(this.label22);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.txtLAT);
            this.panel2.Controls.Add(this.txtLNG);
            this.panel2.Controls.Add(this.txtUtmN);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtUtmE);
            this.panel2.Location = new System.Drawing.Point(3, -2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(127, 110);
            this.panel2.TabIndex = 0;
            // 
            // txtTimeZone
            // 
            this.txtTimeZone.Location = new System.Drawing.Point(53, 80);
            this.txtTimeZone.Name = "txtTimeZone";
            this.txtTimeZone.Size = new System.Drawing.Size(70, 20);
            this.txtTimeZone.TabIndex = 18;
            this.txtTimeZone.Text = "0";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.ForeColor = System.Drawing.Color.Black;
            this.label22.Location = new System.Drawing.Point(0, 83);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(53, 13);
            this.label22.TabIndex = 17;
            this.label22.Text = "Timezone";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Long:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(22, 49);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(25, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "Lat:";
            // 
            // txtLAT
            // 
            this.txtLAT.Enabled = false;
            this.txtLAT.Location = new System.Drawing.Point(53, 42);
            this.txtLAT.Name = "txtLAT";
            this.txtLAT.Size = new System.Drawing.Size(70, 20);
            this.txtLAT.TabIndex = 13;
            // 
            // txtLNG
            // 
            this.txtLNG.Enabled = false;
            this.txtLNG.Location = new System.Drawing.Point(53, 62);
            this.txtLNG.Name = "txtLNG";
            this.txtLNG.Size = new System.Drawing.Size(70, 20);
            this.txtLNG.TabIndex = 14;
            // 
            // txtUtmN
            // 
            this.txtUtmN.Enabled = false;
            this.txtUtmN.Location = new System.Drawing.Point(53, 1);
            this.txtUtmN.Name = "txtUtmN";
            this.txtUtmN.Size = new System.Drawing.Size(70, 20);
            this.txtUtmN.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "UTM E:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "UTM N:";
            // 
            // txtUtmE
            // 
            this.txtUtmE.Enabled = false;
            this.txtUtmE.Location = new System.Drawing.Point(53, 21);
            this.txtUtmE.Name = "txtUtmE";
            this.txtUtmE.Size = new System.Drawing.Size(70, 20);
            this.txtUtmE.TabIndex = 10;
            // 
            // pvProgressbar
            // 
            this.pvProgressbar.Location = new System.Drawing.Point(91, 78);
            this.pvProgressbar.Name = "pvProgressbar";
            this.pvProgressbar.Size = new System.Drawing.Size(59, 12);
            this.pvProgressbar.TabIndex = 147;
            this.pvProgressbar.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1074, 32);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 145;
            this.button2.Text = "Sample code";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(464, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 92;
            this.label4.Text = "Configuration";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(344, 0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(5, 86);
            this.pictureBox3.TabIndex = 91;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(612, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(5, 86);
            this.pictureBox1.TabIndex = 91;
            this.pictureBox1.TabStop = false;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.ForeColor = System.Drawing.Color.Gray;
            this.label52.Location = new System.Drawing.Point(14, 77);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(57, 13);
            this.label52.TabIndex = 87;
            this.label52.Text = "PVMapper";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(175, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 87;
            this.label1.Text = "Reference Location";
            // 
            // picSpliter1
            // 
            this.picSpliter1.Image = ((System.Drawing.Image)(resources.GetObject("picSpliter1.Image")));
            this.picSpliter1.Location = new System.Drawing.Point(74, 0);
            this.picSpliter1.Name = "picSpliter1";
            this.picSpliter1.Size = new System.Drawing.Size(5, 86);
            this.picSpliter1.TabIndex = 86;
            this.picSpliter1.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPage2.BackgroundImage")));
            this.tabPage2.Controls.Add(this.webBrowser1);
            this.tabPage2.Controls.Add(this.label59);
            this.tabPage2.Controls.Add(this.label60);
            this.tabPage2.Controls.Add(this.cmdSelectTree);
            this.tabPage2.Controls.Add(this.cmdEditTreePropDialog);
            this.tabPage2.Controls.Add(this.cmdSelectBuilding);
            this.tabPage2.Controls.Add(this.label58);
            this.tabPage2.Controls.Add(this.label57);
            this.tabPage2.Controls.Add(this.prgBar);
            this.tabPage2.Controls.Add(this.label16);
            this.tabPage2.Controls.Add(this.ExportBldgAndTrr2SketchUp);
            this.tabPage2.Controls.Add(this.cmdSelectTreeLayer);
            this.tabPage2.Controls.Add(this.btnAddTree);
            this.tabPage2.Controls.Add(this.btnAddBuilding);
            this.tabPage2.Controls.Add(this.cmdBuilding);
            this.tabPage2.Controls.Add(this.pictureBox13);
            this.tabPage2.Controls.Add(this.pictureBox2);
            this.tabPage2.Controls.Add(this.pictureBox11);
            this.tabPage2.Controls.Add(this.label31);
            this.tabPage2.Controls.Add(this.label17);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1211, 122);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(442, 8);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(84, 67);
            this.webBrowser1.TabIndex = 12;
            this.webBrowser1.Url = new System.Uri("https://pvdesktop.codeplex.com/", System.UriKind.Absolute);
            this.webBrowser1.Visible = false;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(285, 17);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(67, 13);
            this.label59.TabIndex = 186;
            this.label59.Text = "Select Trees";
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(285, 50);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(51, 13);
            this.label60.TabIndex = 185;
            this.label60.Text = "Attributes";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(99, 50);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(51, 13);
            this.label58.TabIndex = 185;
            this.label58.Text = "Attributes";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(99, 17);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(82, 13);
            this.label57.TabIndex = 185;
            this.label57.Text = "Select Buildings";
            // 
            // prgBar
            // 
            this.prgBar.Location = new System.Drawing.Point(433, 77);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(82, 15);
            this.prgBar.TabIndex = 175;
            this.prgBar.Visible = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.Gray;
            this.label16.Location = new System.Drawing.Point(366, 81);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(55, 13);
            this.label16.TabIndex = 97;
            this.label16.Text = "SketchUp";
            // 
            // btnAddTree
            // 
            this.btnAddTree.BackColor = System.Drawing.Color.White;
            this.btnAddTree.Image = ((System.Drawing.Image)(resources.GetObject("btnAddTree.Image")));
            this.btnAddTree.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAddTree.Location = new System.Drawing.Point(198, 4);
            this.btnAddTree.Name = "btnAddTree";
            this.btnAddTree.Size = new System.Drawing.Size(47, 73);
            this.btnAddTree.TabIndex = 105;
            this.btnAddTree.Text = "Add Tree";
            this.btnAddTree.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAddTree.UseVisualStyleBackColor = false;
            this.btnAddTree.Click += new System.EventHandler(this.btnAddTree_Click);
            // 
            // btnAddBuilding
            // 
            this.btnAddBuilding.BackColor = System.Drawing.Color.White;
            this.btnAddBuilding.Image = ((System.Drawing.Image)(resources.GetObject("btnAddBuilding.Image")));
            this.btnAddBuilding.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAddBuilding.Location = new System.Drawing.Point(5, 4);
            this.btnAddBuilding.Name = "btnAddBuilding";
            this.btnAddBuilding.Size = new System.Drawing.Size(54, 73);
            this.btnAddBuilding.TabIndex = 104;
            this.btnAddBuilding.Text = "Add Building";
            this.btnAddBuilding.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAddBuilding.UseVisualStyleBackColor = false;
            this.btnAddBuilding.Click += new System.EventHandler(this.btnAddBuilding_Click);
            // 
            // pictureBox13
            // 
            this.pictureBox13.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox13.Image")));
            this.pictureBox13.Location = new System.Drawing.Point(422, 6);
            this.pictureBox13.Name = "pictureBox13";
            this.pictureBox13.Size = new System.Drawing.Size(5, 86);
            this.pictureBox13.TabIndex = 92;
            this.pictureBox13.TabStop = false;
            this.pictureBox13.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(358, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(5, 86);
            this.pictureBox2.TabIndex = 92;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox11
            // 
            this.pictureBox11.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox11.Image")));
            this.pictureBox11.Location = new System.Drawing.Point(187, 6);
            this.pictureBox11.Name = "pictureBox11";
            this.pictureBox11.Size = new System.Drawing.Size(5, 86);
            this.pictureBox11.TabIndex = 92;
            this.pictureBox11.TabStop = false;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.ForeColor = System.Drawing.Color.Gray;
            this.label31.Location = new System.Drawing.Point(206, 81);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(118, 13);
            this.label31.TabIndex = 97;
            this.label31.Text = "Solar Obstruction-Trees";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.Gray;
            this.label17.Location = new System.Drawing.Point(28, 80);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(133, 13);
            this.label17.TabIndex = 97;
            this.label17.Text = "Solar Obstruction-Buildings";
            // 
            // tabPage3
            // 
            this.tabPage3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPage3.BackgroundImage")));
            this.tabPage3.Controls.Add(this.label62);
            this.tabPage3.Controls.Add(this.txtEffectiveAngle);
            this.tabPage3.Controls.Add(this.label61);
            this.tabPage3.Controls.Add(this.txtPoa);
            this.tabPage3.Controls.Add(this.txtTcell);
            this.tabPage3.Controls.Add(this.lblPvSpec5);
            this.tabPage3.Controls.Add(this.txtTM2);
            this.tabPage3.Controls.Add(this.lblPvSpec4);
            this.tabPage3.Controls.Add(this.optSingleWeatherSta);
            this.tabPage3.Controls.Add(this.txtDerate);
            this.tabPage3.Controls.Add(this.optMultiWeatherSta);
            this.tabPage3.Controls.Add(this.txtAreaPreSys);
            this.tabPage3.Controls.Add(this.txtSystem_size);
            this.tabPage3.Controls.Add(this.cmdShowIdwSta);
            this.tabPage3.Controls.Add(this.lblPvSpec2);
            this.tabPage3.Controls.Add(this.txtPowY);
            this.tabPage3.Controls.Add(this.txtElev);
            this.tabPage3.Controls.Add(this.txtPowX);
            this.tabPage3.Controls.Add(this.label53);
            this.tabPage3.Controls.Add(this.label51);
            this.tabPage3.Controls.Add(this.lblPvSpec1);
            this.tabPage3.Controls.Add(this.label28);
            this.tabPage3.Controls.Add(this.txtNIdwSta);
            this.tabPage3.Controls.Add(this.lblCity);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.lblState);
            this.tabPage3.Controls.Add(this.label50);
            this.tabPage3.Controls.Add(this.label14);
            this.tabPage3.Controls.Add(this.cmbCity);
            this.tabPage3.Controls.Add(this.label29);
            this.tabPage3.Controls.Add(this.cmbState);
            this.tabPage3.Controls.Add(this.pictureBox6);
            this.tabPage3.Controls.Add(this.cmdWeatherFile);
            this.tabPage3.Controls.Add(this.pictureBox16);
            this.tabPage3.Controls.Add(this.label21);
            this.tabPage3.Controls.Add(this.label23);
            this.tabPage3.Location = new System.Drawing.Point(4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1211, 122);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(502, 61);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(25, 13);
            this.label62.TabIndex = 133;
            this.label62.Text = "deg";
            // 
            // txtPoa
            // 
            this.txtPoa.Enabled = false;
            this.txtPoa.Location = new System.Drawing.Point(901, 55);
            this.txtPoa.Name = "txtPoa";
            this.txtPoa.Size = new System.Drawing.Size(49, 20);
            this.txtPoa.TabIndex = 7;
            this.txtPoa.Text = "84.5";
            this.txtPoa.TextChanged += new System.EventHandler(this.txtPoa_TextChanged);
            // 
            // txtTcell
            // 
            this.txtTcell.Enabled = false;
            this.txtTcell.Location = new System.Drawing.Point(901, 33);
            this.txtTcell.Name = "txtTcell";
            this.txtTcell.Size = new System.Drawing.Size(49, 20);
            this.txtTcell.TabIndex = 8;
            this.txtTcell.Text = "6.9";
            this.txtTcell.TextChanged += new System.EventHandler(this.txtTcell_TextChanged);
            // 
            // lblPvSpec5
            // 
            this.lblPvSpec5.AutoSize = true;
            this.lblPvSpec5.Enabled = false;
            this.lblPvSpec5.Location = new System.Drawing.Point(618, 58);
            this.lblPvSpec5.Name = "lblPvSpec5";
            this.lblPvSpec5.Size = new System.Drawing.Size(271, 13);
            this.lblPvSpec5.TabIndex = 2;
            this.lblPvSpec5.Text = "Plane of array irradiance (W/m2) from previous time step";
            // 
            // txtTM2
            // 
            this.txtTM2.Location = new System.Drawing.Point(315, 49);
            this.txtTM2.Name = "txtTM2";
            this.txtTM2.Size = new System.Drawing.Size(130, 20);
            this.txtTM2.TabIndex = 130;
            // 
            // lblPvSpec4
            // 
            this.lblPvSpec4.AutoSize = true;
            this.lblPvSpec4.Enabled = false;
            this.lblPvSpec4.Location = new System.Drawing.Point(617, 42);
            this.lblPvSpec4.Name = "lblPvSpec4";
            this.lblPvSpec4.Size = new System.Drawing.Size(286, 13);
            this.lblPvSpec4.TabIndex = 3;
            this.lblPvSpec4.Text = "Calculated cell temperature from previous timestep, (deg. C)";
            // 
            // optSingleWeatherSta
            // 
            this.optSingleWeatherSta.AutoSize = true;
            this.optSingleWeatherSta.Location = new System.Drawing.Point(462, 22);
            this.optSingleWeatherSta.Name = "optSingleWeatherSta";
            this.optSingleWeatherSta.Size = new System.Drawing.Size(144, 17);
            this.optSingleWeatherSta.TabIndex = 129;
            this.optSingleWeatherSta.TabStop = true;
            this.optSingleWeatherSta.Text = "Specific weather location";
            this.optSingleWeatherSta.UseVisualStyleBackColor = true;
            // 
            // txtDerate
            // 
            this.txtDerate.Location = new System.Drawing.Point(901, 3);
            this.txtDerate.Name = "txtDerate";
            this.txtDerate.Size = new System.Drawing.Size(49, 20);
            this.txtDerate.TabIndex = 10;
            this.txtDerate.Text = "0.77";
            this.txtDerate.TextChanged += new System.EventHandler(this.txtDerate_TextChanged);
            // 
            // optMultiWeatherSta
            // 
            this.optMultiWeatherSta.AutoSize = true;
            this.optMultiWeatherSta.Checked = true;
            this.optMultiWeatherSta.Location = new System.Drawing.Point(462, 4);
            this.optMultiWeatherSta.Name = "optMultiWeatherSta";
            this.optMultiWeatherSta.Size = new System.Drawing.Size(128, 17);
            this.optMultiWeatherSta.TabIndex = 128;
            this.optMultiWeatherSta.TabStop = true;
            this.optMultiWeatherSta.Text = "Multi-weather location";
            this.optMultiWeatherSta.UseVisualStyleBackColor = true;
            // 
            // txtAreaPreSys
            // 
            this.txtAreaPreSys.Location = new System.Drawing.Point(722, 23);
            this.txtAreaPreSys.Name = "txtAreaPreSys";
            this.txtAreaPreSys.Size = new System.Drawing.Size(42, 20);
            this.txtAreaPreSys.TabIndex = 11;
            this.txtAreaPreSys.Text = "25.6";
            this.txtAreaPreSys.TextChanged += new System.EventHandler(this.txtAreaPreSys_TextChanged);
            // 
            // txtSystem_size
            // 
            this.txtSystem_size.Location = new System.Drawing.Point(787, 3);
            this.txtSystem_size.Name = "txtSystem_size";
            this.txtSystem_size.Size = new System.Drawing.Size(34, 20);
            this.txtSystem_size.TabIndex = 11;
            this.txtSystem_size.Text = "4";
            this.txtSystem_size.TextChanged += new System.EventHandler(this.txtSystem_size_TextChanged);
            // 
            // lblPvSpec2
            // 
            this.lblPvSpec2.AutoSize = true;
            this.lblPvSpec2.Location = new System.Drawing.Point(827, 6);
            this.lblPvSpec2.Name = "lblPvSpec2";
            this.lblPvSpec2.Size = new System.Drawing.Size(67, 13);
            this.lblPvSpec2.TabIndex = 5;
            this.lblPvSpec2.Text = "derate factor";
            // 
            // txtPowY
            // 
            this.txtPowY.Location = new System.Drawing.Point(379, 24);
            this.txtPowY.Name = "txtPowY";
            this.txtPowY.Size = new System.Drawing.Size(40, 20);
            this.txtPowY.TabIndex = 1;
            this.txtPowY.Text = "2";
            this.txtPowY.TextChanged += new System.EventHandler(this.txtPowY_TextChanged);
            // 
            // txtElev
            // 
            this.txtElev.Location = new System.Drawing.Point(155, 49);
            this.txtElev.Name = "txtElev";
            this.txtElev.Size = new System.Drawing.Size(63, 20);
            this.txtElev.TabIndex = 127;
            // 
            // txtPowX
            // 
            this.txtPowX.Location = new System.Drawing.Point(315, 24);
            this.txtPowX.Name = "txtPowX";
            this.txtPowX.Size = new System.Drawing.Size(38, 20);
            this.txtPowX.TabIndex = 1;
            this.txtPowX.Text = "2";
            this.txtPowX.TextChanged += new System.EventHandler(this.txtPowX_TextChanged);
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(763, 26);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(87, 13);
            this.label53.TabIndex = 6;
            this.label53.Text = "m^2/KW System";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(617, 26);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(100, 13);
            this.label51.TabIndex = 6;
            this.label51.Text = "PV System per area";
            // 
            // lblPvSpec1
            // 
            this.lblPvSpec1.AutoSize = true;
            this.lblPvSpec1.Location = new System.Drawing.Point(617, 6);
            this.lblPvSpec1.Name = "lblPvSpec1";
            this.lblPvSpec1.Size = new System.Drawing.Size(166, 13);
            this.lblPvSpec1.TabIndex = 6;
            this.lblPvSpec1.Text = "System DC nameplate rating (kW)";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(63, 52);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(93, 13);
            this.label28.TabIndex = 122;
            this.label28.Text = "Elevation (m.MSL)";
            // 
            // txtNIdwSta
            // 
            this.txtNIdwSta.Location = new System.Drawing.Point(391, 3);
            this.txtNIdwSta.Name = "txtNIdwSta";
            this.txtNIdwSta.Size = new System.Drawing.Size(28, 20);
            this.txtNIdwSta.TabIndex = 1;
            this.txtNIdwSta.Text = "5";
            this.txtNIdwSta.TextChanged += new System.EventHandler(this.txtNIdwSta_TextChanged);
            // 
            // lblCity
            // 
            this.lblCity.AutoSize = true;
            this.lblCity.Location = new System.Drawing.Point(65, 28);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(24, 13);
            this.lblCity.TabIndex = 123;
            this.lblCity.Text = "City";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(357, 28);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(20, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "(Y)";
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(65, 7);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(32, 13);
            this.lblState.TabIndex = 124;
            this.lblState.Text = "State";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(237, 52);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(73, 13);
            this.label50.TabIndex = 0;
            this.label50.Text = "Weather File :";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(237, 28);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(78, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "IDW Power (X)";
            // 
            // cmbCity
            // 
            this.cmbCity.FormattingEnabled = true;
            this.cmbCity.Location = new System.Drawing.Point(97, 27);
            this.cmbCity.Name = "cmbCity";
            this.cmbCity.Size = new System.Drawing.Size(121, 21);
            this.cmbCity.TabIndex = 119;
            this.cmbCity.Text = "Select data";
            this.cmbCity.SelectedIndexChanged += new System.EventHandler(this.cmbCity_SelectedIndexChanged);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(237, 7);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(148, 13);
            this.label29.TabIndex = 0;
            this.label29.Text = "Number of Weighting Stations";
            // 
            // cmbState
            // 
            this.cmbState.FormattingEnabled = true;
            this.cmbState.Location = new System.Drawing.Point(97, 3);
            this.cmbState.Name = "cmbState";
            this.cmbState.Size = new System.Drawing.Size(121, 21);
            this.cmbState.TabIndex = 120;
            this.cmbState.Text = "Select data";
            this.cmbState.SelectedIndexChanged += new System.EventHandler(this.cmbState_SelectedIndexChanged);
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.Location = new System.Drawing.Point(226, 0);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(5, 86);
            this.pictureBox6.TabIndex = 116;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox16
            // 
            this.pictureBox16.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox16.Image")));
            this.pictureBox16.Location = new System.Drawing.Point(954, 3);
            this.pictureBox16.Name = "pictureBox16";
            this.pictureBox16.Size = new System.Drawing.Size(5, 86);
            this.pictureBox16.TabIndex = 98;
            this.pictureBox16.TabStop = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.ForeColor = System.Drawing.Color.Gray;
            this.label21.Location = new System.Drawing.Point(532, 77);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(115, 13);
            this.label21.TabIndex = 95;
            this.label21.Text = "Calculation Parameters";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.ForeColor = System.Drawing.Color.Gray;
            this.label23.Location = new System.Drawing.Point(75, 77);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(84, 13);
            this.label23.TabIndex = 97;
            this.label23.Text = "Weather Station";
            // 
            // tabPage5
            // 
            this.tabPage5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPage5.BackgroundImage")));
            this.tabPage5.Controls.Add(this.cmdCreatePvPosition);
            this.tabPage5.Controls.Add(this.cmdCheck4PvOnRoof);
            this.tabPage5.Controls.Add(this.txtGridSpacingY);
            this.tabPage5.Controls.Add(this.label55);
            this.tabPage5.Controls.Add(this.txtGridSpacingX);
            this.tabPage5.Controls.Add(this.txtRoofAz);
            this.tabPage5.Controls.Add(this.label45);
            this.tabPage5.Controls.Add(this.txtRoofTilt);
            this.tabPage5.Controls.Add(this.label48);
            this.tabPage5.Controls.Add(this.pictureBox15);
            this.tabPage5.Controls.Add(this.label47);
            this.tabPage5.Controls.Add(this.label54);
            this.tabPage5.Controls.Add(this.cmdPvPanelConfig4Roof);
            this.tabPage5.Controls.Add(this.cmbRoofTopPanelPosition);
            this.tabPage5.Controls.Add(this.cmdCreateRooftopPanel);
            this.tabPage5.Controls.Add(this.label46);
            this.tabPage5.Controls.Add(this.checkBox1);
            this.tabPage5.Controls.Add(this.cmdExportRooftopPanetToSkecthUp);
            this.tabPage5.Controls.Add(this.label20);
            this.tabPage5.Controls.Add(this.label26);
            this.tabPage5.Controls.Add(this.cmbRoofTopPanelPanel);
            this.tabPage5.Controls.Add(this.pictureBox5);
            this.tabPage5.Controls.Add(this.cmdRooftopAcCalculation);
            this.tabPage5.Controls.Add(this.label49);
            this.tabPage5.Controls.Add(this.label44);
            this.tabPage5.Controls.Add(this.cmdTiltDown);
            this.tabPage5.Controls.Add(this.cmdTiltUp);
            this.tabPage5.Controls.Add(this.panelDrawRoof);
            this.tabPage5.Controls.Add(this.label38);
            this.tabPage5.Controls.Add(this.label35);
            this.tabPage5.Controls.Add(this.label42);
            this.tabPage5.Controls.Add(this.label40);
            this.tabPage5.Controls.Add(this.label34);
            this.tabPage5.Controls.Add(this.label37);
            this.tabPage5.Controls.Add(this.label24);
            this.tabPage5.Controls.Add(this.label41);
            this.tabPage5.Controls.Add(this.label39);
            this.tabPage5.Controls.Add(this.label10);
            this.tabPage5.Controls.Add(this.txtBottomDepth);
            this.tabPage5.Controls.Add(this.txtEaveHeight);
            this.tabPage5.Controls.Add(this.txtRidgeHeight);
            this.tabPage5.Controls.Add(this.pictureBox12);
            this.tabPage5.Controls.Add(this.cmdRedrawRoofPlan);
            this.tabPage5.Controls.Add(this.cmdRoofPlane);
            this.tabPage5.Controls.Add(this.cmdEaveLine);
            this.tabPage5.Controls.Add(this.cmdRidgeLine);
            this.tabPage5.Controls.Add(this.label18);
            this.tabPage5.Controls.Add(this.label7);
            this.tabPage5.Location = new System.Drawing.Point(4, 4);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(1211, 122);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "tabPage5";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // cmdCreatePvPosition
            // 
            this.cmdCreatePvPosition.BackColor = System.Drawing.Color.White;
            this.cmdCreatePvPosition.Enabled = false;
            this.cmdCreatePvPosition.Image = ((System.Drawing.Image)(resources.GetObject("cmdCreatePvPosition.Image")));
            this.cmdCreatePvPosition.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdCreatePvPosition.Location = new System.Drawing.Point(761, 1);
            this.cmdCreatePvPosition.Name = "cmdCreatePvPosition";
            this.cmdCreatePvPosition.Size = new System.Drawing.Size(55, 73);
            this.cmdCreatePvPosition.TabIndex = 125;
            this.cmdCreatePvPosition.Text = "Panel Position";
            this.cmdCreatePvPosition.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdCreatePvPosition.UseVisualStyleBackColor = false;
            this.cmdCreatePvPosition.Visible = false;
            this.cmdCreatePvPosition.Click += new System.EventHandler(this.cmdCreatePvPosition_Click);
            // 
            // txtGridSpacingY
            // 
            this.txtGridSpacingY.Location = new System.Drawing.Point(858, 29);
            this.txtGridSpacingY.Name = "txtGridSpacingY";
            this.txtGridSpacingY.Size = new System.Drawing.Size(28, 20);
            this.txtGridSpacingY.TabIndex = 1;
            this.txtGridSpacingY.Text = "5";
            this.txtGridSpacingY.Visible = false;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(892, 34);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(15, 13);
            this.label55.TabIndex = 132;
            this.label55.Text = "m";
            this.label55.Visible = false;
            // 
            // txtGridSpacingX
            // 
            this.txtGridSpacingX.Location = new System.Drawing.Point(858, 7);
            this.txtGridSpacingX.Name = "txtGridSpacingX";
            this.txtGridSpacingX.Size = new System.Drawing.Size(28, 20);
            this.txtGridSpacingX.TabIndex = 1;
            this.txtGridSpacingX.Text = "3";
            this.txtGridSpacingX.Visible = false;
            // 
            // txtRoofAz
            // 
            this.txtRoofAz.Enabled = false;
            this.txtRoofAz.Location = new System.Drawing.Point(702, 28);
            this.txtRoofAz.Name = "txtRoofAz";
            this.txtRoofAz.Size = new System.Drawing.Size(42, 20);
            this.txtRoofAz.TabIndex = 130;
            this.txtRoofAz.Visible = false;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(808, 34);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(42, 13);
            this.label45.TabIndex = 0;
            this.label45.Text = "Vertical";
            this.label45.Visible = false;
            // 
            // txtRoofTilt
            // 
            this.txtRoofTilt.Enabled = false;
            this.txtRoofTilt.Location = new System.Drawing.Point(702, 5);
            this.txtRoofTilt.Name = "txtRoofTilt";
            this.txtRoofTilt.Size = new System.Drawing.Size(42, 20);
            this.txtRoofTilt.TabIndex = 130;
            this.txtRoofTilt.Visible = false;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(806, 11);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(54, 13);
            this.label48.TabIndex = 0;
            this.label48.Text = "Horizontal";
            this.label48.Visible = false;
            // 
            // pictureBox15
            // 
            this.pictureBox15.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox15.Image")));
            this.pictureBox15.Location = new System.Drawing.Point(597, 6);
            this.pictureBox15.Name = "pictureBox15";
            this.pictureBox15.Size = new System.Drawing.Size(5, 86);
            this.pictureBox15.TabIndex = 187;
            this.pictureBox15.TabStop = false;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(812, 71);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(85, 13);
            this.label47.TabIndex = 199;
            this.label47.Text = "Create PV Panel";
            this.label47.Visible = false;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(892, 12);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(15, 13);
            this.label54.TabIndex = 132;
            this.label54.Text = "m";
            this.label54.Visible = false;
            // 
            // cmdPvPanelConfig4Roof
            // 
            this.cmdPvPanelConfig4Roof.BackColor = System.Drawing.Color.White;
            this.cmdPvPanelConfig4Roof.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdPvPanelConfig4Roof.Image = ((System.Drawing.Image)(resources.GetObject("cmdPvPanelConfig4Roof.Image")));
            this.cmdPvPanelConfig4Roof.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdPvPanelConfig4Roof.Location = new System.Drawing.Point(297, 3);
            this.cmdPvPanelConfig4Roof.Name = "cmdPvPanelConfig4Roof";
            this.cmdPvPanelConfig4Roof.Size = new System.Drawing.Size(53, 73);
            this.cmdPvPanelConfig4Roof.TabIndex = 193;
            this.cmdPvPanelConfig4Roof.Text = "Panel Data";
            this.cmdPvPanelConfig4Roof.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdPvPanelConfig4Roof.UseVisualStyleBackColor = false;
            this.cmdPvPanelConfig4Roof.Click += new System.EventHandler(this.cmdPvPanelConfig4Roof_Click);
            // 
            // cmbRoofTopPanelPosition
            // 
            this.cmbRoofTopPanelPosition.FormattingEnabled = true;
            this.cmbRoofTopPanelPosition.Location = new System.Drawing.Point(891, 67);
            this.cmbRoofTopPanelPosition.Name = "cmbRoofTopPanelPosition";
            this.cmbRoofTopPanelPosition.Size = new System.Drawing.Size(103, 21);
            this.cmbRoofTopPanelPosition.TabIndex = 197;
            this.cmbRoofTopPanelPosition.Visible = false;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(895, 51);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(90, 13);
            this.label46.TabIndex = 194;
            this.label46.Text = "PV Position Layer";
            this.label46.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(482, 55);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(115, 17);
            this.checkBox1.TabIndex = 192;
            this.checkBox1.Text = "Export Daily Result";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(913, 8);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(137, 13);
            this.label20.TabIndex = 190;
            this.label20.Text = "Export to Google SketchUp";
            this.label20.Visible = false;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(479, 12);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(103, 13);
            this.label26.TabIndex = 189;
            this.label26.Text = "Current Roof Project";
            // 
            // cmbRoofTopPanelPanel
            // 
            this.cmbRoofTopPanelPanel.FormattingEnabled = true;
            this.cmbRoofTopPanelPanel.Location = new System.Drawing.Point(482, 28);
            this.cmbRoofTopPanelPanel.Name = "cmbRoofTopPanelPanel";
            this.cmbRoofTopPanelPanel.Size = new System.Drawing.Size(92, 21);
            this.cmbRoofTopPanelPanel.TabIndex = 188;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(413, 6);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(5, 86);
            this.pictureBox5.TabIndex = 187;
            this.pictureBox5.TabStop = false;
            // 
            // cmdRooftopAcCalculation
            // 
            this.cmdRooftopAcCalculation.BackColor = System.Drawing.Color.White;
            this.cmdRooftopAcCalculation.Image = ((System.Drawing.Image)(resources.GetObject("cmdRooftopAcCalculation.Image")));
            this.cmdRooftopAcCalculation.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdRooftopAcCalculation.Location = new System.Drawing.Point(423, 3);
            this.cmdRooftopAcCalculation.Name = "cmdRooftopAcCalculation";
            this.cmdRooftopAcCalculation.Size = new System.Drawing.Size(53, 73);
            this.cmdRooftopAcCalculation.TabIndex = 186;
            this.cmdRooftopAcCalculation.Text = "Energy Product";
            this.cmdRooftopAcCalculation.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdRooftopAcCalculation.UseVisualStyleBackColor = false;
            this.cmdRooftopAcCalculation.Click += new System.EventHandler(this.cmdRooftopAcCalculation_Click);
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.ForeColor = System.Drawing.Color.Gray;
            this.label49.Location = new System.Drawing.Point(313, 79);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(75, 13);
            this.label49.TabIndex = 185;
            this.label49.Text = "Edit Properties";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.ForeColor = System.Drawing.Color.Gray;
            this.label44.Location = new System.Drawing.Point(465, 79);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(95, 13);
            this.label44.TabIndex = 185;
            this.label44.Text = "Energy Calculation";
            // 
            // cmdTiltDown
            // 
            this.cmdTiltDown.Image = ((System.Drawing.Image)(resources.GetObject("cmdTiltDown.Image")));
            this.cmdTiltDown.Location = new System.Drawing.Point(251, 42);
            this.cmdTiltDown.Name = "cmdTiltDown";
            this.cmdTiltDown.Size = new System.Drawing.Size(32, 32);
            this.cmdTiltDown.TabIndex = 134;
            this.cmdTiltDown.UseVisualStyleBackColor = true;
            this.cmdTiltDown.Click += new System.EventHandler(this.cmdTiltDown_Click);
            // 
            // cmdTiltUp
            // 
            this.cmdTiltUp.Image = ((System.Drawing.Image)(resources.GetObject("cmdTiltUp.Image")));
            this.cmdTiltUp.Location = new System.Drawing.Point(251, 5);
            this.cmdTiltUp.Name = "cmdTiltUp";
            this.cmdTiltUp.Size = new System.Drawing.Size(32, 32);
            this.cmdTiltUp.TabIndex = 134;
            this.cmdTiltUp.UseVisualStyleBackColor = true;
            this.cmdTiltUp.Click += new System.EventHandler(this.cmdTiltUp_Click);
            // 
            // panelDrawRoof
            // 
            this.panelDrawRoof.BackColor = System.Drawing.Color.White;
            this.panelDrawRoof.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDrawRoof.Controls.Add(this.label43);
            this.panelDrawRoof.Controls.Add(this.txtPy);
            this.panelDrawRoof.Controls.Add(this.txtPx);
            this.panelDrawRoof.Controls.Add(this.label36);
            this.panelDrawRoof.Location = new System.Drawing.Point(148, 3);
            this.panelDrawRoof.Name = "panelDrawRoof";
            this.panelDrawRoof.Size = new System.Drawing.Size(100, 73);
            this.panelDrawRoof.TabIndex = 137;
            this.panelDrawRoof.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.BackColor = System.Drawing.Color.Transparent;
            this.label43.Location = new System.Drawing.Point(20, 34);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(57, 13);
            this.label43.TabIndex = 140;
            this.label43.Text = "Roof Pitch";
            this.label43.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtPy
            // 
            this.txtPy.Location = new System.Drawing.Point(53, 50);
            this.txtPy.Name = "txtPy";
            this.txtPy.Size = new System.Drawing.Size(29, 20);
            this.txtPy.TabIndex = 139;
            this.txtPy.Text = "12";
            this.txtPy.TextChanged += new System.EventHandler(this.txtPy_TextChanged);
            // 
            // txtPx
            // 
            this.txtPx.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.txtPx.Enabled = false;
            this.txtPx.Location = new System.Drawing.Point(18, 50);
            this.txtPx.Name = "txtPx";
            this.txtPx.Size = new System.Drawing.Size(29, 20);
            this.txtPx.TabIndex = 139;
            this.txtPx.Text = "12";
            this.txtPx.TextChanged += new System.EventHandler(this.txtPx_TextChanged);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(45, 53);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(10, 13);
            this.label36.TabIndex = 138;
            this.label36.Text = ":";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(1255, 71);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(15, 13);
            this.label38.TabIndex = 132;
            this.label38.Text = "m";
            this.label38.Visible = false;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(1255, 49);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(15, 13);
            this.label35.TabIndex = 132;
            this.label35.Text = "m";
            this.label35.Visible = false;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(742, 31);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(27, 13);
            this.label42.TabIndex = 132;
            this.label42.Text = "Deg";
            this.label42.Visible = false;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(742, 8);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(27, 13);
            this.label40.TabIndex = 132;
            this.label40.Text = "Deg";
            this.label40.Visible = false;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(1255, 26);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(15, 13);
            this.label34.TabIndex = 132;
            this.label34.Text = "m";
            this.label34.Visible = false;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(1001, 72);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(67, 13);
            this.label37.TabIndex = 131;
            this.label37.Text = "Base Length";
            this.label37.Visible = false;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(1001, 50);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(66, 13);
            this.label24.TabIndex = 131;
            this.label24.Text = "Eave Height";
            this.label24.Visible = false;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(638, 31);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(66, 13);
            this.label41.TabIndex = 131;
            this.label41.Text = "Azimuth Ang";
            this.label41.Visible = false;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(638, 8);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(51, 13);
            this.label39.TabIndex = 131;
            this.label39.Text = "Tilt Angle";
            this.label39.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1001, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 13);
            this.label10.TabIndex = 131;
            this.label10.Text = "Ridge Height";
            this.label10.Visible = false;
            // 
            // txtBottomDepth
            // 
            this.txtBottomDepth.Location = new System.Drawing.Point(1217, 68);
            this.txtBottomDepth.Name = "txtBottomDepth";
            this.txtBottomDepth.Size = new System.Drawing.Size(37, 20);
            this.txtBottomDepth.TabIndex = 130;
            this.txtBottomDepth.Visible = false;
            this.txtBottomDepth.TextChanged += new System.EventHandler(this.txtBottomDepth_TextChanged);
            // 
            // txtEaveHeight
            // 
            this.txtEaveHeight.Location = new System.Drawing.Point(1217, 46);
            this.txtEaveHeight.Name = "txtEaveHeight";
            this.txtEaveHeight.Size = new System.Drawing.Size(37, 20);
            this.txtEaveHeight.TabIndex = 130;
            this.txtEaveHeight.Text = "0.0";
            this.txtEaveHeight.Visible = false;
            this.txtEaveHeight.TextChanged += new System.EventHandler(this.txtEaveHeight_TextChanged);
            // 
            // txtRidgeHeight
            // 
            this.txtRidgeHeight.Location = new System.Drawing.Point(1217, 23);
            this.txtRidgeHeight.Name = "txtRidgeHeight";
            this.txtRidgeHeight.Size = new System.Drawing.Size(37, 20);
            this.txtRidgeHeight.TabIndex = 130;
            this.txtRidgeHeight.Visible = false;
            this.txtRidgeHeight.TextChanged += new System.EventHandler(this.txtRidgeHeight_TextChanged);
            // 
            // pictureBox12
            // 
            this.pictureBox12.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox12.Image")));
            this.pictureBox12.Location = new System.Drawing.Point(287, 6);
            this.pictureBox12.Name = "pictureBox12";
            this.pictureBox12.Size = new System.Drawing.Size(5, 86);
            this.pictureBox12.TabIndex = 128;
            this.pictureBox12.TabStop = false;
            // 
            // cmdRoofPlane
            // 
            this.cmdRoofPlane.BackColor = System.Drawing.Color.White;
            this.cmdRoofPlane.Enabled = false;
            this.cmdRoofPlane.Image = ((System.Drawing.Image)(resources.GetObject("cmdRoofPlane.Image")));
            this.cmdRoofPlane.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdRoofPlane.Location = new System.Drawing.Point(99, 3);
            this.cmdRoofPlane.Name = "cmdRoofPlane";
            this.cmdRoofPlane.Size = new System.Drawing.Size(46, 73);
            this.cmdRoofPlane.TabIndex = 125;
            this.cmdRoofPlane.Text = "Finsih Plane";
            this.cmdRoofPlane.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdRoofPlane.UseVisualStyleBackColor = false;
            this.cmdRoofPlane.Click += new System.EventHandler(this.cmdRoofPlane_Click);
            // 
            // cmdEaveLine
            // 
            this.cmdEaveLine.BackColor = System.Drawing.Color.White;
            this.cmdEaveLine.Image = ((System.Drawing.Image)(resources.GetObject("cmdEaveLine.Image")));
            this.cmdEaveLine.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdEaveLine.Location = new System.Drawing.Point(51, 3);
            this.cmdEaveLine.Name = "cmdEaveLine";
            this.cmdEaveLine.Size = new System.Drawing.Size(46, 73);
            this.cmdEaveLine.TabIndex = 125;
            this.cmdEaveLine.Text = "Eave Line";
            this.cmdEaveLine.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdEaveLine.UseVisualStyleBackColor = false;
            this.cmdEaveLine.Click += new System.EventHandler(this.cmdEaveLine_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.Gray;
            this.label18.Location = new System.Drawing.Point(608, 78);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(81, 13);
            this.label18.TabIndex = 124;
            this.label18.Text = "Roof Dimesions";
            this.label18.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Gray;
            this.label7.Location = new System.Drawing.Point(87, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 13);
            this.label7.TabIndex = 124;
            this.label7.Text = "Rooftop Boundry Design";
            // 
            // tabPage7
            // 
            this.tabPage7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPage7.BackgroundImage")));
            this.tabPage7.Controls.Add(this.btnStepByStep);
            this.tabPage7.Controls.Add(this.pictureBox7);
            this.tabPage7.Controls.Add(this.label56);
            this.tabPage7.Controls.Add(this.cmdVDO);
            this.tabPage7.Controls.Add(this.cmdPVMapperWeb);
            this.tabPage7.Controls.Add(this.cmdErrorReport);
            this.tabPage7.Location = new System.Drawing.Point(4, 4);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(1211, 122);
            this.tabPage7.TabIndex = 5;
            this.tabPage7.Text = "tabPage7";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // btnStepByStep
            // 
            this.btnStepByStep.BackColor = System.Drawing.Color.White;
            this.btnStepByStep.Image = ((System.Drawing.Image)(resources.GetObject("btnStepByStep.Image")));
            this.btnStepByStep.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnStepByStep.Location = new System.Drawing.Point(161, 3);
            this.btnStepByStep.Name = "btnStepByStep";
            this.btnStepByStep.Size = new System.Drawing.Size(51, 72);
            this.btnStepByStep.TabIndex = 131;
            this.btnStepByStep.Text = "Step by Step";
            this.btnStepByStep.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnStepByStep.UseVisualStyleBackColor = false;
            this.btnStepByStep.Click += new System.EventHandler(this.btnStepByStep_Click);
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox7.Image")));
            this.pictureBox7.Location = new System.Drawing.Point(230, 6);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(5, 86);
            this.pictureBox7.TabIndex = 130;
            this.pictureBox7.TabStop = false;
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.ForeColor = System.Drawing.Color.Gray;
            this.label56.Location = new System.Drawing.Point(63, 77);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(29, 13);
            this.label56.TabIndex = 129;
            this.label56.Text = "Help";
            // 
            // cmdVDO
            // 
            this.cmdVDO.BackColor = System.Drawing.Color.White;
            this.cmdVDO.Image = ((System.Drawing.Image)(resources.GetObject("cmdVDO.Image")));
            this.cmdVDO.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdVDO.Location = new System.Drawing.Point(108, 2);
            this.cmdVDO.Name = "cmdVDO";
            this.cmdVDO.Size = new System.Drawing.Size(51, 72);
            this.cmdVDO.TabIndex = 95;
            this.cmdVDO.Text = "Tutorial Video";
            this.cmdVDO.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdVDO.UseVisualStyleBackColor = false;
            this.cmdVDO.Click += new System.EventHandler(this.cmdVDO_Click);
            // 
            // lstImgOnOff
            // 
            this.lstImgOnOff.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("lstImgOnOff.ImageStream")));
            this.lstImgOnOff.TransparentColor = System.Drawing.Color.Transparent;
            this.lstImgOnOff.Images.SetKeyName(0, "SwitchOff.png");
            this.lstImgOnOff.Images.SetKeyName(1, "SwitchOn.png");
            // 
            // appManager
            // 
            this.appManager.CompositionContainer = null;
            this.appManager.Directories = ((System.Collections.Generic.List<string>)(resources.GetObject("appManager.Directories")));
            this.appManager.DockManager = null;
            this.appManager.HeaderControl = null;
            this.appManager.Legend = this.legend1;
            this.appManager.Map = this.pvMap;
            this.appManager.ProgressHandler = null;
            this.appManager.ShowExtensionsDialog = DotSpatial.Controls.ShowExtensionsDialog.MapGlyph;
            // 
            // imgLstHelp
            // 
            this.imgLstHelp.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgLstHelp.ImageStream")));
            this.imgLstHelp.TransparentColor = System.Drawing.Color.Transparent;
            this.imgLstHelp.Images.SetKeyName(0, "HelpStep1.PNG");
            this.imgLstHelp.Images.SetKeyName(1, "Help Step 2.PNG");
            // 
            // frm01_MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1219, 560);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panelTab);
            this.Controls.Add(this.tabFakeRibbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm01_MainForm";
            this.Text = "PVMapper Site Designer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.frm01_MainForm_Activated);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabLegend.ResumeLayout(false);
            this.tabLayerVariables.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picboxHelp)).EndInit();
            this.pnlGrdProduction.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdAcProduct)).EndInit();
            this.TabGOptimize.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSolarPanel)).EndInit();
            this.tabPage13.ResumeLayout(false);
            this.tabPage12.ResumeLayout(false);
            this.tabPage14.ResumeLayout(false);
            this.tabPage15.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            this.panelTab.ResumeLayout(false);
            this.panelTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTab06)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTab05)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTab04)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTab03)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTab02)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTab01)).EndInit();
            this.tabFakeRibbon.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSpliter1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox16)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.panelDrawRoof.ResumeLayout(false);
            this.panelDrawRoof.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).EndInit();
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AppManager appManager;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolTip ttHelp;
        private System.Windows.Forms.Panel panelTab;
        private System.Windows.Forms.PictureBox picTab04;
        private System.Windows.Forms.PictureBox picTab03;
        private System.Windows.Forms.PictureBox picTab02;
        private System.Windows.Forms.PictureBox picTab01;
        private System.Windows.Forms.Label lblHome;
        private System.Windows.Forms.Label lblTab04;
        private System.Windows.Forms.Label lblTab02;
        private System.Windows.Forms.Label lblTab01;
        private System.Windows.Forms.Label lblTab03;
        private System.Windows.Forms.TabControl tabFakeRibbon;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.PictureBox pictureBox11;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Button cmdWeatherFile;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button cmdEditTreePropDialog;
        private System.Windows.Forms.Button cmdBuilding;
        //---------------------------------------------------------------------------
        // public component

        public Map pvMap;
        private System.Windows.Forms.Label lblTab05;
        private System.Windows.Forms.PictureBox picTab05;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.PictureBox pictureBox12;
        internal System.Windows.Forms.Button cmdRedrawRoofPlan;
        private System.Windows.Forms.Button cmdEaveLine;
        private System.Windows.Forms.Button cmdRidgeLine;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtElev;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.ComboBox cmbCity;
        private System.Windows.Forms.ComboBox cmbState;
        private System.Windows.Forms.Button cmdShowIdwSta;
        private System.Windows.Forms.TextBox txtPowY;
        private System.Windows.Forms.TextBox txtPowX;
        private System.Windows.Forms.TextBox txtNIdwSta;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.ImageList lstTreeImage;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.CheckBox chkDailyExp;
        private System.Windows.Forms.Label lblPanelLayer;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.RadioButton rdoKML;
        private System.Windows.Forms.RadioButton rdoSiteArea;
        private System.Windows.Forms.RadioButton rdoAlignment;
        private System.Windows.Forms.Button cmdNewAligmnentShp;
        private System.Windows.Forms.PictureBox pictureBox14;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.Button cmdEnergyCal;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtEaveHeight;
        private System.Windows.Forms.TextBox txtRidgeHeight;
        private System.Windows.Forms.Button cmdRoofPlane;
        private System.Windows.Forms.Button cmdTiltDown;
        private System.Windows.Forms.Button cmdTiltUp;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panelDrawRoof;
        private System.Windows.Forms.TextBox txtPy;
        private System.Windows.Forms.TextBox txtPx;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.TextBox txtBottomDepth;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TextBox txtRoofAz;
        private System.Windows.Forms.TextBox txtRoofTilt;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Button cmdSelectTreeLayer;
        private System.Windows.Forms.Button cmdSelectTree;
        private System.Windows.Forms.Button cmdSelectBuilding;
        private System.Windows.Forms.ImageList lstImgOnOff;
        private System.Windows.Forms.PictureBox pictureBox13;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ComboBox cmbBruTileLayer;
        private System.Windows.Forms.CheckBox chkUseLastPath;
        private System.Windows.Forms.CheckBox chkOnlineMap;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtTimeZone;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtLAT;
        private System.Windows.Forms.TextBox txtLNG;
        private System.Windows.Forms.TextBox txtUtmN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUtmE;
        private System.Windows.Forms.ProgressBar pvProgressbar;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button cmdZoomToSite;
        private System.Windows.Forms.Button cmdPickCentroid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button cmdAddKML;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picSpliter1;
        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.ComboBox cmbDem;
        private System.Windows.Forms.Button cmdSaveConfig;
        private System.Windows.Forms.TextBox txtWorkingPath;
        private System.Windows.Forms.Button cmdUseCurrentPath;
        internal System.Windows.Forms.Button ExportBldgAndTrr2SketchUp;
        private System.Windows.Forms.CheckBox checkBox1;
        internal System.Windows.Forms.Button cmdExportRooftopPanetToSkecthUp;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox cmbRoofTopPanelPanel;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Button cmdRooftopAcCalculation;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.CheckBox cmdCheck4PvOnRoof;
        private System.Windows.Forms.ComboBox cmbRoofTopPanelPosition;
        internal System.Windows.Forms.Button cmdCreateRooftopPanel;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Button cmdPvPanelConfig4Roof;
        private System.Windows.Forms.PictureBox pictureBox15;
        private System.Windows.Forms.Button cmdCreatePvPosition;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.TextBox txtGridSpacingY;
        private System.Windows.Forms.TextBox txtGridSpacingX;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.RadioButton optSingleWeatherSta;
        private System.Windows.Forms.RadioButton optMultiWeatherSta;
        private System.Windows.Forms.TextBox txtTM2;
        private System.Windows.Forms.PictureBox pictureBox16;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.TextBox txtPoa;
        private System.Windows.Forms.TextBox txtTcell;
        private System.Windows.Forms.Label lblPvSpec5;
        private System.Windows.Forms.Label lblPvSpec4;
        private System.Windows.Forms.TextBox txtDerate;
        private System.Windows.Forms.TextBox txtSystem_size;
        private System.Windows.Forms.Label lblPvSpec2;
        private System.Windows.Forms.Label lblPvSpec1;
        private System.Windows.Forms.Button cmdForTest;
        private System.Windows.Forms.Button cmdSwithToMap;
        private System.Windows.Forms.Button cmdSwithToTable;
        private System.Windows.Forms.DataGridView grdAcProduct;
        private System.Windows.Forms.TabControl TabGOptimize;
        private System.Windows.Forms.TabPage tabPage6;
        private pvPanel3DAngleCtl.pvPanelAngle pvTilt;
        private pvPanel3DAngleCtl.pvPanelCompassCtl pvAz;
        private System.Windows.Forms.Button cmdOptimization;
        private System.Windows.Forms.TabPage tabPage13;
        private ZedGraph.ZedGraphControl zedGOpti1;
        private System.Windows.Forms.TabPage tabPage12;
        private ZedGraph.ZedGraphControl zedGOpti2;
        private System.Windows.Forms.TabPage tabPage14;
        private ZedGraph.ZedGraphControl zedGOpti3;
        private System.Windows.Forms.TabPage tabPage15;
        private ZedGraph.ZedGraphControl zedGOpti4;
        private System.Windows.Forms.Button cmdSwithToGraph;
        private System.Windows.Forms.ProgressBar UpdateProgressBar;
        private System.Windows.Forms.TextBox txtPvWidth;
        private System.Windows.Forms.TextBox txtPvLength;
        private System.Windows.Forms.PictureBox picSolarPanel;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Button cmdMapZoomIn;
        private System.Windows.Forms.Button cmdMapSelectionNone;
        private System.Windows.Forms.Button cmdMapSelection;
        private System.Windows.Forms.Button cmdMapPan;
        private System.Windows.Forms.Button cmdMapZoomExt;
        private System.Windows.Forms.Button cmdMapZoomOut;
        private System.Windows.Forms.TextBox txtAreaPreSys;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column18;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column21;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column19;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column20;
        private System.Windows.Forms.PictureBox picTab06;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cmdRoseModel;
        private System.Windows.Forms.TextBox txtRoseScale;
        private System.Windows.Forms.CheckBox chkRosePlot;
        private System.Windows.Forms.Button cmdSunCalDialog;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Button cmdPVMapperWeb;
        private System.Windows.Forms.Button cmdErrorReport;
        private System.Windows.Forms.Button cmdVDO;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private Legend legend1;
        private System.Windows.Forms.Button btnStepByStep;
        public System.Windows.Forms.SplitContainer splitContainer2;
        public System.Windows.Forms.PictureBox picboxHelp;
        private System.Windows.Forms.ImageList imgLstHelp;
        public System.Windows.Forms.Button btnCloseHelp;
        private System.Windows.Forms.Button btnAddTree;
        private System.Windows.Forms.Button btnAddBuilding;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.TextBox txtEffectiveAngle;
        private System.Windows.Forms.Label label61;
        public System.Windows.Forms.ComboBox cmbPanel;
        public System.Windows.Forms.ComboBox cmbPolePosition;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Button btnDrawArea;
        private System.Windows.Forms.Button btnDrawAlignment;
        public System.Windows.Forms.ComboBox cmbSolarFarmArea;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Button btnAddPanel;
        public System.Windows.Forms.ComboBox cmbAlignmentLyr;
        public System.Windows.Forms.Button cmdExportSketchUp;
        public System.Windows.Forms.Button cmdPvPanelAngle;
        public System.Windows.Forms.Button cmdCreatePvPole;
        public System.Windows.Forms.Button btnMovePanels;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabLegend;
        private System.Windows.Forms.TabPage tabLayerVariables;
        public System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Label lblAreaCombo;
        public System.Windows.Forms.ComboBox cmbSiteArea;
        private System.Windows.Forms.Panel pnlGrdProduction;
        private System.Windows.Forms.Label lblGrdTitle;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button btnKML;
    }
}