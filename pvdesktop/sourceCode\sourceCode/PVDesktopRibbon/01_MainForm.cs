// ********************************************************************************************************
// Product Name: DOH HNVAT
// Description:  DOH Highway Network Vulnerability Assessment Tool
// ********************************************************************************************************
//
// Power by:DotSpatial
//
// ********************************************************************************************************

using System;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Topology;
using System.Drawing;
using DotSpatial.Symbology;
using System.Data.OleDb;
using System.Data;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using DotSpatial.Projections;
using System.Xml;
using System.Globalization;
using System.Linq;
using ZedGraph;

namespace PVDESKTOP
{
    /// <summary>
    /// A Form to test the map controls.
    /// </summary>
    public partial class frm01_MainForm : Form
    {
        [Export("Shell", typeof(ContainerControl))]
        private static ContainerControl Shell;

        #region"Variable----------------------------------------------------------------------------------------------------"

        public PvDesktopProject prj = new PvDesktopProject();
        PvDesktopUtilityFunction util = new PvDesktopUtilityFunction();
        //--------------------------------------------------
        struct weatherSta
        {
            public string CODE;
            public string State;
            public string City;
            public double LAT;
            public double LONG;
            public double Elev;
            public string FileName;
            public double LAT2;
            public double LONG2;
        }

        const int nSta = 239;
        double[] wStaDistance = new double[nSta];
        int[] wStaSel = new int[nSta];
        Coordinate lastCoord;
        weatherSta[] wSta = new weatherSta[nSta];
        List<string> listState = new List<string>();
        bool UserDefined = false;
        //--------------------------------------------------
        //string path1 = System.Environment.GetEnvironmentVariable("appdata");
        string pvAppDir = System.Environment.GetEnvironmentVariable("appdata") + "\\pvDesktopData";

        string pvDir = System.IO.Directory.GetCurrentDirectory() + "\\pvDesktopData";
        // prj.Path = "";
        //Tree variable
        int[] SiteAreaLyrs = new int[100];
        double[] whRatio = new double[10];
        string[] treeTypeName = new string[10];
        double[,] treeShape = new double[10, 2];
        int currentImage = 0;
        int activeTreeGrdRow = 0;
        bool[] verify = new bool[6] { false, false, false, false, false, false };
        //-------------------------------------------------
        //public bool pickRoseLocation = false;
        public bool pickBLLocation = false;
        enum mapAction { None, Select, pickRoseLocation, firstRidgePoint, SecondRidgePoint, firstEavePoint, SecondEavePoint,
               BuildingCoord, EndBuildingCoord, SelectBuilding, AreaCoord, AlignmentCoord1, AlignmentCoord2, MovePanelCoord };
        mapAction mapAct = new mapAction();
        List<Coordinate> RidgeLine = new List<Coordinate>();
        List<Coordinate> EaveLine = new List<Coordinate>();
        List<Coordinate> Alignment = new List<Coordinate>();
        bool firstAlignPt = false;
        bool firstBldgPt = false;
        bool firstAreaPt = false;
        bool siteLocatedbyCentroid = false;
        List<Coordinate> BldgCoords = new List<Coordinate>();
        List<Coordinate> AreaCoords = new List<Coordinate>();
        int numSiteArea = 0;
        Coordinate[] imgRoof = new Coordinate[3];
        bool DrawRoof = false;
        int Px = 12;
        int Py = 12;
        int numPvPanel = 0;
        string temp_path = "";
        public IFeatureSet temp_pvfs = new FeatureSet();
        frmCreatePoleBySiteArea formSiteArea;
        
        //---------------------------------------------------------
        #endregion//"Variable------------------------------------------------------------------------------------------------"

        #region "MainformEvent-----------------------------------------------------------------------------------------------"

        //public Map mainMap { get { return map1; } set { } }
        public Map mapMain
        {
            get { return pvMap; }
            set
            {
                pvMap = (Map)value;
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="frm01_MainForm"/> class.
        /// </summary>
        public frm01_MainForm()
        {
            InitializeComponent();

            Shell = this;
            appManager.LoadExtensions();
            //help2.container = splitContainer2;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            try
            {
                splitContainer1.Width = this.Width - 17;
                splitContainer1.Height = this.Height - splitContainer1.Top - 45;
                webBrowser1.Location = splitContainer1.Location;
                webBrowser1.Size = splitContainer1.Size;

                lblHome.Top = 5; lblHome.Left = 306;
                lblTab01.Top = lblHome.Top; lblTab01.Left = 7;
                lblTab02.Top = lblHome.Top; lblTab02.Left = 43;
                lblTab03.Top = lblHome.Top; lblTab03.Left = 109;
                lblTab04.Top = lblHome.Top; lblTab04.Left = 179;
            }
            catch { }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            cmdMapZoomIn.Top = 5; cmdMapZoomIn.Left = 5;
            cmdMapZoomOut.Top = 34; cmdMapZoomOut.Left = 5;
            cmdMapZoomExt.Top = 63; cmdMapZoomExt.Left = 5;
            cmdMapPan.Top = 92; cmdMapPan.Left = 5;
            cmdMapSelection.Top = 121; cmdMapSelection.Left = 5;
            cmdMapSelectionNone.Top = 150; cmdMapSelectionNone.Left = 5;
            temp_path = Path.GetDirectoryName(System.IO.Path.GetTempFileName());
            //--------------------------------------------------------
            //grdAcProduct.Location = pvMap.Location;
            //grdAcProduct.Size = pvMap.Size;
            pnlGrdProduction.Location = pvMap.Location;
            pnlGrdProduction.Size = pvMap.Size;
            TabGOptimize.Location = pvMap.Location;
            TabGOptimize.Size = pvMap.Size;

            cmdSwithToTable.Location = cmdSwithToMap.Location;
            //--------- Roof image -----
            imgRoof[0] = new Coordinate(2, 47);
            imgRoof[1] = new Coordinate(50, 5);
            imgRoof[2] = new Coordinate(98, 47);
            //--------------------------

            //this.FormBorderStyle = FormBorderStyle.None;
            splitContainer1.Top = 135;
            //splitContainer1.Height = this.Height - 400;
            //splitContainer1.Width = this.Width-10; 
            //set empty path for start program
            prj.Path = "";

            tabFakeRibbon.Top = 30;
            tabFakeRibbon.SendToBack();
            //string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //var directory = System.IO.Path.GetDirectoryName(path);
            pvDir = System.IO.Directory.GetCurrentDirectory() + "\\pvDesktopData";
            string weatherShp = pvDir + "\\WeatherSta\\weather_sta.shp";



            if (System.IO.File.Exists(pvDir + "\\WeatherSta\\weather_sta.csv") == true)
            {
                int counter = 0;
                string line;

                System.IO.StreamReader file =
                new System.IO.StreamReader(pvDir + "\\WeatherSta\\weather_sta.csv");
                bool firstLine = true;
                while ((line = file.ReadLine()) != null)
                {

                    //textBox1.Text += line;
                    if (firstLine == false)
                    {
                        string[] StaData = line.Split(',');
                        wSta[counter].CODE = Convert.ToString(StaData[0]);
                        wSta[counter].State = Convert.ToString(StaData[1]);
                        wSta[counter].City = Convert.ToString(StaData[2]);
                        wSta[counter].LAT = Convert.ToDouble(StaData[3]);
                        wSta[counter].LONG = Convert.ToDouble(StaData[4]);
                        wSta[counter].Elev = Convert.ToDouble(StaData[5]);
                        wSta[counter].FileName = Convert.ToString(StaData[6]);
                        wSta[counter].LAT2 = Convert.ToDouble(StaData[7]);
                        wSta[counter].LONG2 = Convert.ToDouble(StaData[8]);
                        bool duppChk = false;
                        if (listState.Count > 0)
                        {
                            for (int i = 0; i < listState.Count; i++) // Loop through List with for
                            {
                                if (wSta[counter].State == listState[i]) duppChk = true;
                            }
                        }
                        if (duppChk == false) listState.Add(wSta[counter].State);
                        counter++;
                    }
                    else
                    { firstLine = false; }
                }
                try
                {
                    pvMap.Layers.Add(weatherShp).LegendText = "Weather Stations";
                }
                catch { }
            }
            else
            {
                MessageBox.Show("Weather station file not found");
            }
            //------------------------------------------------------------------------------------------
            string ChkOnlineMap = "";
            string onlineMapSource = "";
            string ChkLastPath = "";
            string LastPath = "";
            if (File.Exists(pvAppDir + "\\pvdesktop.ini"))
            {
                System.IO.StreamReader iniFile = new System.IO.StreamReader(pvAppDir + "\\pvdesktop.ini");
                ChkOnlineMap = iniFile.ReadLine();
                onlineMapSource = iniFile.ReadLine();
                ChkLastPath = iniFile.ReadLine();
                LastPath = iniFile.ReadLine();
                //
                cmbBruTileLayer.Text = onlineMapSource;
                if (util.internetAccess() == true)
                {
                    if (ChkOnlineMap.ToUpper() == "TRUE")
                    {
                        AddBruTileLayer(cmbBruTileLayer.Text);
                        cmbBruTileLayer.Enabled = true;
                    }
                    else
                    {
                        AddBruTileLayer("None");
                        cmbBruTileLayer.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("Unable to connect to the internet");
                    AddBruTileLayer("None");
                    cmbBruTileLayer.Enabled = false;
                }
                if (ChkOnlineMap.ToUpper() == "TRUE")
                {
                    chkOnlineMap.Checked = true;
                }
                else
                {
                    chkOnlineMap.Checked = false;
                }



                //-------------------------------------------------------
                if (ChkLastPath.ToUpper() == "TRUE")
                {
                    chkUseLastPath.Checked = true;
                    txtWorkingPath.Text = LastPath;
                    prj.Path = @LastPath;
                }
                else
                {
                    chkUseLastPath.Checked = false;
                    prj.Path = "";
                }

                iniFile.Close();
            }
            propertyGrid1.SelectedObject = prj;
            propertyGrid1.Refresh();
        }

        #endregion //"MainformEvent-----------------------------------------------------------------------------------------------"

        #region "TabSelection------------------------------------------------------------------------------------------------"

        private void lblTab01_Click(object sender, EventArgs e)
        {
            this.tabFakeRibbon.SelectedTab = tabPage1;
            panelTab.BackgroundImage = picTab01.Image;
            TabHilight(lblTab01);
            switchtoMap();
        }

        private void lblTab02_Click(object sender, EventArgs e)
        {
            this.tabFakeRibbon.SelectedTab = tabPage2;
            panelTab.BackgroundImage = picTab02.Image;
            TabHilight(lblTab02);
            switchtoMap();
            //picBox

        }


        private void lblTab03_Click(object sender, EventArgs e)
        {
            this.tabFakeRibbon.SelectedTab = tabPage3;
            panelTab.BackgroundImage = picTab03.Image;
            TabHilight(lblTab03);
            switchtoMap();
        }

        private void lblTab04_Click(object sender, EventArgs e)
        {
            this.tabFakeRibbon.SelectedTab = tabPage4;
            panelTab.BackgroundImage = picTab04.Image;
            TabHilight(lblTab04);
            switchtoMap();
        }

        private void lblTab05_Click(object sender, EventArgs e)
        {
            this.tabFakeRibbon.SelectedTab = tabPage5;
            panelTab.BackgroundImage = picTab05.Image;
            TabHilight(lblTab05);
            UpDateRoofShape();
            DrawRoof = true;
            panelDrawRoof.Invalidate();
            switchtoMap();
        }

        private void lblHome_Click(object sender, EventArgs e)
        {
            webBrowser1.Location = splitContainer1.Location;
            webBrowser1.Size = splitContainer1.Size;
            webBrowser1.Visible = true;
            //Display HOME as a modal dialog
            //Form frmHome = new frm00_HomeForm();
            //frmHome.ShowDialog();
            this.tabFakeRibbon.SelectedTab = tabPage7;
            panelTab.BackgroundImage = picTab06.Image;
            TabHilight(lblHome);

        }

        void TabHilight(System.Windows.Forms.Label lblTab)
        {
            lblTab01.ForeColor = Color.Black;
            lblTab02.ForeColor = Color.Black;
            lblTab03.ForeColor = Color.Black;
            lblTab04.ForeColor = Color.Black;
            lblTab05.ForeColor = Color.Black;
            lblHome.ForeColor = Color.Black;
            lblTab.ForeColor = Color.Green;
            if (lblTab.Text == lblHome.Text)
            {
                webBrowser1.Location = splitContainer1.Location;
                webBrowser1.Size = splitContainer1.Size;
                webBrowser1.Visible = true;
                webBrowser1.BringToFront();
            }
            else
            {
                webBrowser1.Visible = false;
            }

            loadLayerList();
        }

        #endregion // "TabSelection-----------------------------------------------------------------------------------------"

        #region "Site data---------------------------------------------------------------------------------------------------"

        private void cmdErrorReport_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://pvdesktop.codeplex.com/workitem/list/basic");
        }

        private void cmbBruTileLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBruTileLayer.Enabled) AddBruTileLayer(cmbBruTileLayer.Text);
        }

        private void chkOnlineMap_CheckedChanged(object sender, EventArgs e)
        {
            if (util.internetAccess() == true)
            {
                if (chkOnlineMap.Checked == true)
                {
                    AddBruTileLayer(cmbBruTileLayer.Text);
                    cmbBruTileLayer.Enabled = true;
                }
                else
                {
                    AddBruTileLayer("None");
                    cmbBruTileLayer.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Unable to connect to the internet");
                AddBruTileLayer("None");
                cmbBruTileLayer.Enabled = false;
            }

        }

        private void cmdSaveConfig_Click(object sender, EventArgs e)
        {

            if (prj.Path != "")
            {
                System.IO.Directory.CreateDirectory(pvAppDir);
                System.IO.StreamWriter congigFile = new System.IO.StreamWriter(pvAppDir + "\\pvdesktop.ini");
                congigFile.WriteLine(chkOnlineMap.Checked.ToString());
                congigFile.WriteLine(cmbBruTileLayer.Text);
                congigFile.WriteLine(chkUseLastPath.Checked.ToString());
                congigFile.WriteLine(prj.Path);
                congigFile.Close();
            }
        }

        private void txtWorkingPath_TextChanged(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(this.txtWorkingPath, txtWorkingPath.Text);
        }

        private void cmdUseCurrentPath_Click(object sender, EventArgs e)
        {
            string tmpPath = prj.Path;
            prj.Path = "";
            CheckWorkingPath();
            if (prj.Path != "")
            {
                txtWorkingPath.Text = prj.Path;
            }
            else
            {
                prj.Path = tmpPath;
                txtWorkingPath.Text = prj.Path;
            }
        }

        private void chkUseLastPath_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseLastPath.Checked == true)
            {
                cmdUseCurrentPath.Enabled = true;
                txtWorkingPath.Enabled = true;
                prj.Path = txtWorkingPath.Text;
            }
            else
            {
                cmdUseCurrentPath.Enabled = false;
                txtWorkingPath.Enabled = false;
                prj.Path = "";
                cmdSaveConfig.Enabled = false;
            }
        }

        private void cmdUseNewWorkingPath_Click(object sender, EventArgs e)
        {

        }

        private void cmdPVMapperWeb_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://pvmapper.org/app");
        }

        private void cmdLayerMangemant_Click(object sender, EventArgs e)
        {
            loadLayerList();
        }

        private void cmdPickCentroid_Click(object sender, EventArgs e)
        {
            CheckWorkingPath();
            if (prj.Path != "")
            {
                /*
                string projectFileName= @"C:\Users\Kasem Pinthong\Desktop\#PvDesktop Data for Make DVO\usa.dspx";
                appManager.ProgressHandler.Progress("Opening Project", 0, "Opening Project");
                appManager.SerializationManager.OpenProject(projectFileName);
                appManager.ProgressHandler.Progress("Project opened", 0, "");
                */
                mapAct = mapAction.pickRoseLocation;// pickRoseLocation = true;
                pvMap.FunctionMode = FunctionMode.None;
                appManager.ProgressHandler.Progress("Pick the reference location", 0, "Pick the reference location");

            }
        }

        private void cmdNewAligmnentShp_Click(object sender, EventArgs e)
        {
            CheckWorkingPath();

            if (prj.Path != "")
            {
                IFeatureSet pvfs;
                pvfs = new FeatureSet(FeatureType.Line);
                pvfs.Projection = pvMap.Projection;
                pvfs.DataTable.Columns.Add(new DataColumn("Spacing", typeof(double)));
                pvfs.DataTable.Columns.Add(new DataColumn("Remark", typeof(string)));

                //++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                pvfs.Name = "Alignment";
                pvfs.Filename = prj.Path + "\\Temp\\" + pvfs.Name + ".shp";
                //++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                pvfs.SaveAs(pvfs.Filename, true);
                util.removeDupplicateLyr(pvfs.Name, pvMap);

                pvMap.Layers.Add(pvfs);

                cmbAlignmentLyr.Text = pvfs.Name;
                loadLayerList();

                setCurrrentLayer(cmbAlignmentLyr.Text);
            }

        }

        private void chkDEM_CheckedChanged(object sender, EventArgs e)
        {
            cmbDem.Enabled = prj.DemChecked;
        }

        private void cmbAlignmentLyr_TextChanged(object sender, EventArgs e)
        {
            checkAlignmentFieldPropeties();
        }

        #endregion//"Site data-----------------------------------------------------------------------------------------------"

        #region"SUN Properties----------------------------------------------------------------------------------------------"

        private void cmdSunCalDialog_Click(object sender, EventArgs e)
        {
            frmSunPath frmSunpath = new frmSunPath();
            frmSunpath.Project = prj;
            frmSunpath.ShowDialog();
            lblTab03.Enabled = true;
            lblTab04.Enabled = true;
            lblTab05.Enabled = true;

        }

        private void cmdRoseModel_Click(object sender, EventArgs e)
        {
            DrawRoseDiagram();
        }

        void DrawRoseDiagram()
        {
            CheckWorkingPath();

            if (prj.Path != "")
            {
                lblTab03.Enabled = true;
                lblTab04.Enabled = true;
                lblTab05.Enabled = true;
                prj.Verify[3] = true;

                pickBLLocation = false;
                appManager.ProgressHandler.Progress("Creating Sun Rose Diagram", 0, "Creating Sun Rose Diagram");
                RoseChart rc = new RoseChart();

                System.Data.DataTable table = new System.Data.DataTable("SolarTab");
                table.Columns.Add(new DataColumn("DIR", typeof(string)));
                for (int i = 1; i <= 8; i++)
                {
                    table.Columns.Add(new DataColumn(rc.getStrMagnitude(i), typeof(int)));
                }
                table.Columns.Add(new DataColumn("Total", typeof(int)));
                //------------------------------------------------------------
                Int16[,] sunHr = new Int16[9, 17];
                int year = dateTimePicker1.Value.Year;

                //Todo: Check site location data before run

                double Latitude = Convert.ToDouble(this.txtLAT.Text);
                double Longitude = Convert.ToDouble(this.txtLNG.Text);
                double UtmN = Convert.ToDouble(this.txtUtmN.Text);
                double UtmE = Convert.ToDouble(this.txtUtmE.Text);
                if (util.IsNumeric(this.txtRoseScale.Text) == false) txtRoseScale.Text = "1";  //Set scale = 1 for default
                double RoseScale = Convert.ToDouble(this.txtRoseScale.Text);

                short TimeZone = -7;
                Int32 nightHr = 0;
                Int32 noonHr = 0;
                for (int month = 1; month <= 12; month++)
                {
                    int month_day = System.DateTime.DaysInMonth(2001, month);
                    for (int day = 1; day <= month_day; day++)
                    {
                        for (int hr = 0; hr < 24; hr++)
                        {
                            double hrPassMidnight = (double)hr / 24.0;
                            SolarCal ySolar = new SolarCal(day, month, year, hrPassMidnight, Latitude, Longitude, TimeZone);
                            //SolarCal ySolar = new SolarCal(day, month, year, hrPassMidnight, Latitude, Longitude, TimeZone);
                            double eleAng = ySolar.SolarElevationAngle;
                            double AzAng = ySolar.SolarAzimuthAngle;
                            //--------------------------------------------------------------
                            if (eleAng > 0)
                            {
                                noonHr++;
                                string AzName = rc.AzName(AzAng);
                                int iRow = rc.AZNameID(AzAng);
                                int iCol = rc.iEleAng(eleAng);
                                sunHr[iCol, iRow]++;
                            }
                            else // Night hour
                            {
                                nightHr++;
                            }
                        }
                    }
                }
                //txtNoonHr.Text = noonHr.ToString();
                //txtNightHr.Text = nightHr.ToString();
                Int16 sumTotal = 0;
                for (int i = 0; i <= 15; i++)
                {
                    Int16 Total = 0;
                    for (int j = 1; j <= 8; j++) { Total += sunHr[j, i]; }
                    sumTotal += Total;
                    double Ang = i * 22.5;
                    table.Rows.Add(rc.AzName(Ang), sunHr[1, i], sunHr[2, i], sunHr[3, i], sunHr[4, i], sunHr[5, i], sunHr[6, i], sunHr[7, i], sunHr[8, i], Total);
                }
                table.Rows.Add("Sum", sunHr[1, 16], sunHr[2, 16], sunHr[3, 16], sunHr[4, 16], sunHr[5, 16], sunHr[6, 16], sunHr[7, 16], sunHr[8, 16], sumTotal);

                //-----------------------------------------------------
                //                grdSolarRose.DataSource = table;
                //-----------------------------------------------------
                if (chkRosePlot.Checked == true)
                {
                    Feature f = new Feature();
                    FeatureSet fea = new FeatureSet(f.FeatureType);
                    //RoseChart roseFeature = new RoseChart(Latitude, Longitude, table, sumTotal);
                    RoseChart roseFeature = new RoseChart(UtmN, UtmE, table, sumTotal, RoseScale);
                    fea = roseFeature.RoseFrature;
                    fea.Projection = pvMap.Projection;
                    //MessageBox.Show(fea.ToString);  

                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++
                    fea.Name = "Solar Radiation Rose";
                    fea.Filename = prj.Path + "\\Temp\\" + fea.Name + ".shp";
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++

                    fea.SaveAs(fea.Filename, true);
                    util.removeDupplicateLyr(fea.Name, pvMap);
                    /*
                    PolygonCategory polyCatalog = new PolygonCategory(Color.Red,Color.Yellow,1);
                    polyCatalog.FilterExpression = "Ele_angle";
                    PolygonScheme scheme = new PolygonScheme();
                    scheme.ClearCategories();
                    scheme.AddCategory(polyCatalog);

                    MapPolygonLayer RoseDiagram;
                    RoseDiagram = new MapPolygonLayer(fea);// MapPolygonLayer(fs);
                    RoseDiagram.SetCategory(fea,polyCatalog);
                    
                     */

                    pvMap.Layers.Add(fea);

                    int iLyr = util.getLayerHdl(fea.Name, pvMap);

                    IMapPolygonLayer RoseLayer = pvMap.Layers[iLyr] as IMapPolygonLayer;

                    PolygonScheme PolyScheme = new PolygonScheme();
                    //linkScheme.Categories.Clear();
                    PolyScheme.EditorSettings.ClassificationType = ClassificationType.UniqueValues;
                    /*
                    if (j == 1) { return "<10"; }
                    if (j == 2) { return "10-20"; }
                    if (j == 3) { return "20-30"; }
                    if (j == 4) { return "30-40"; }
                    if (j == 5) { return "40-50"; }
                    if (j == 6) { return "50-60"; }
                    if (j == 7) { return "60-70"; }
                    if (j == 8) { return ">70"; }
              */
                    PolyScheme.EditorSettings.FieldName = "Ele_angle";
                    PolyScheme.CreateCategories(RoseLayer.DataSet.DataTable);

                    //--------------------------------------------------------------------------------
                    PolygonSymbolizer _Level00 = new PolygonSymbolizer(Color.RoyalBlue, Color.Black, 1);
                    _Level00.ScaleMode = ScaleMode.Simple;                 
                    PolygonCategory Level00 = new PolygonCategory(Color.RoyalBlue, Color.Black, 1);
                    Level00.FilterExpression = "[Ele_angle] = '<10'";
                    Level00.LegendText = "<10 degrees";
                    
                    //--------------------------------------------------------------------------------
                    PolygonSymbolizer _Level01 = new PolygonSymbolizer(Color.PaleTurquoise, Color.Black, 1);
                    _Level01.ScaleMode = ScaleMode.Simple;
                    PolygonCategory Level01 = new PolygonCategory(Color.PaleTurquoise, Color.Black, 1);
                    Level01.FilterExpression = "[Ele_angle] = '10-20'";
                    Level01.LegendText = "10-20 degrees";
                    //--------------------------------------------------------------------------------
                    PolygonSymbolizer _Level02 = new PolygonSymbolizer(Color.PaleGreen, Color.Black, 1);
                    _Level02.ScaleMode = ScaleMode.Simple;
                    PolygonCategory Level02 = new PolygonCategory(Color.PaleGreen, Color.Black, 1);
                    Level02.FilterExpression = "[Ele_angle] = '20-30'";
                    Level02.LegendText = "20-30 degrees";
                    //--------------------------------------------------------------------------------
                    PolygonSymbolizer _Level03 = new PolygonSymbolizer(Color.PaleGoldenrod, Color.Black, 1);
                    _Level03.ScaleMode = ScaleMode.Simple;
                    PolygonCategory Level03 = new PolygonCategory(Color.PaleGoldenrod, Color.Black, 1);
                    Level03.FilterExpression = "[Ele_angle] = '30-40'";
                    Level03.LegendText = "30-40 degrees";
                    //--------------------------------------------------------------------------------
                    PolygonSymbolizer _Level04 = new PolygonSymbolizer(Color.Orange, Color.Black, 1);
                    _Level04.ScaleMode = ScaleMode.Simple;
                    PolygonCategory Level04 = new PolygonCategory(Color.Orange, Color.Black, 1);
                    Level04.FilterExpression = "[Ele_angle] = '40-50'";
                    Level04.LegendText = "40-50 degrees";
                    //--------------------------------------------------------------------------------
                    PolygonSymbolizer _Level05 = new PolygonSymbolizer(Color.Red, Color.Black, 1);
                    _Level05.ScaleMode = ScaleMode.Simple;
                    PolygonCategory Level05 = new PolygonCategory(Color.Red, Color.Black, 1);
                    Level05.FilterExpression = "[Ele_angle] = '50-60'";
                    Level05.LegendText = "50-60 degrees";
                    //--------------------------------------------------------------------------------
                    PolygonSymbolizer _Level06 = new PolygonSymbolizer(Color.Crimson, Color.Black, 1);
                    _Level06.ScaleMode = ScaleMode.Simple;
                    PolygonCategory Level06 = new PolygonCategory(Color.Crimson, Color.Black, 1);
                    Level06.FilterExpression = "[Ele_angle] = '60-70'";
                    Level06.LegendText = "60-70 degrees";
                    //--------------------------------------------------------------------------------
                    PolygonSymbolizer _Level07 = new PolygonSymbolizer(Color.DarkRed, Color.Black, 1);
                    _Level07.ScaleMode = ScaleMode.Simple;
                    PolygonCategory Level07 = new PolygonCategory(Color.DarkRed, Color.Black, 1);
                    Level07.FilterExpression = "[Ele_angle] = '>70'";
                    Level07.LegendText = ">70 degrees";
                    //--------------------------------------------------------------------------------
                    PolyScheme.ClearCategories();
                    PolyScheme.AddCategory(Level00);
                    PolyScheme.AddCategory(Level01);
                    PolyScheme.AddCategory(Level02);
                    PolyScheme.AddCategory(Level03);
                    PolyScheme.AddCategory(Level04);
                    PolyScheme.AddCategory(Level05);
                    PolyScheme.AddCategory(Level06);
                    PolyScheme.AddCategory(Level07);


                    RoseLayer.Symbology = PolyScheme;
                    RoseLayer.DataSet.InvalidateVertices();
                    //-------------------------------------------------------------------
                    //pvMap.MapFrame.DrawingLayers.Clear();
                    //pvMap.MapFrame.DrawingLayers.Add(RoseLayer);
                    //pvMap.MapFrame.Invalidate();

                }
                //------------------------------
                //                YearlyCal();
                verify[3] = true;
                appManager.ProgressHandler.Progress("Sun Rose created", 0, "");
                cmdSwithToGraph.Visible = true;
            }
        }

        #region "Building"

        private void cmdSelectBuilding_Click(object sender, EventArgs e)
        {
            prj.LyrBuilding = util.getLayerHdl(prj.LyrBuildingName, pvMap);
            if (prj.LyrBuilding != -1)
            {
                if (setCurrrentLayer(prj.LyrBuildingName))
                {
                    IMapFeatureLayer blgdFe = pvMap.Layers[prj.LyrBuilding] as IMapFeatureLayer;
                    blgdFe.Selection.Clear();
                    blgdFe = null;
                    pvMap.FunctionMode = FunctionMode.Select;

                }
            }
        }

        private void cmdBuilding_Click(object sender, EventArgs e)
        {
            //mapAct = mapAction.None;
            if (util.getLayerHdl(prj.LyrBuildingName, pvMap) != -1)
            {
                frmBuilding frmBldg = new frmBuilding();
                frmBldg.Michael = this;
                prj.LyrBuilding = util.getLayerHdl(prj.LyrBuildingName, pvMap);
                frmBldg.ProjectFile = prj;
                frmBldg.PvMap = pvMap;
                frmBldg.ShowDialog();
            }
        }


        #endregion

        #region "Tree"

        private void cmdSelectTreeLayer_Click(object sender, EventArgs e)
        {
            setCurrrentLayer(prj.LyrTreeName);
        }

        private void cmdEditTreePropDialog_Click(object sender, EventArgs e)
        {
            if (util.getLayerHdl(prj.LyrTreeName, pvMap) != -1)
            {
                frmTrees frmTrees = new frmTrees();
                frmTrees.Michael = this;
                prj.LyrTree = util.getLayerHdl(prj.LyrTreeName, pvMap);
                frmTrees.ProjectFile = prj;
                frmTrees.PvMap = pvMap;
                frmTrees.ShowDialog();
            }

            /*
            {
                
                int errNo = 0;
                frmTrees frmTree = new frmTrees();
                prj.LyrTree = util.getLayerHdl(cmbTree.Text, pvMap);
                frmTree.ProjectFile = prj;
                frmTree.PvMap = pvMap;
                //if (lblTreeTypeIndex.Text == "None") { errNo = 1; }
                // if (util.IsNumeric(FormAddTree.txtTreeHeight.Text) == false) { errNo = 2; } 
                // if (util.IsNumeric(FormAddTree.txtTreeDiameter.Text) == false) { errNo = 3; }
                //if (errNo == 1) MessageBox.Show("Tree form must be selected before continuing");
                //if (errNo == 2) MessageBox.Show("Height data needs to be numeric");
                //if (errNo == 3) MessageBox.Show("Diameter data needs to be numeric");

                if (errNo == 0)
                {
                    frmTree.TreeType = lblTreeTypeIndex.Text;
                    frmTree.TreeHeight = Convert.ToDouble(txtTreeHeight.Text);
                    frmTree.TreeDismeter = Convert.ToDouble(txtTreeDiameter.Text);
                    frmTree.ShowDialog();
                }
            }
             */
        }


        private void cmdSelectTree_Click(object sender, EventArgs e)
        {

            prj.LyrTree = util.getLayerHdl(prj.LyrTreeName, pvMap);
            if (prj.LyrTree != -1)
            {
                if (setCurrrentLayer(prj.LyrTreeName))
                {
                    IMapFeatureLayer treeFe = pvMap.Layers[prj.LyrTree] as IMapFeatureLayer;
                    treeFe.Selection.Clear();
                    treeFe = null;
                    pvMap.FunctionMode = FunctionMode.Select;
                }
            }
        }

        public void EnableTreeEditing()
        {
            cmdSelectTree.Enabled = true;
            cmdEditTreePropDialog.Enabled = true;
        }

        void setTreeShape()
        {
            whRatio[0] = 1.21; treeTypeName[0] = "Speading";
            whRatio[1] = 1.84; treeTypeName[1] = "Round";
            whRatio[2] = 2.04; treeTypeName[2] = "Pyramidal";
            whRatio[3] = 3.91; treeTypeName[3] = "Oval";
            whRatio[4] = 5.52; treeTypeName[4] = "Conical";
            whRatio[5] = 1.56; treeTypeName[5] = "Vase";
            whRatio[6] = 12.5; treeTypeName[6] = "Columnar";
            whRatio[7] = 2.63; treeTypeName[7] = "Open";
            whRatio[8] = 2.29; treeTypeName[8] = "Weeping";
            whRatio[9] = 3.43; treeTypeName[9] = "Irregular";
        }

        String getTreeName()
        {
            String TreeName = "No Name";
            if (FormAddTree.radTypeColumnar.Checked == true) TreeName = "Columnar";
            if (FormAddTree.radTypeOpen.Checked == true) TreeName = "Open";
            if (FormAddTree.radTypeIrregular.Checked == true) TreeName = "Irregular";
            if (FormAddTree.radTypeWeeping.Checked == true) TreeName = "Weeping";
            if (FormAddTree.radTypeVase.Checked == true) TreeName = "Vase";
            if (FormAddTree.radTypeConical.Checked == true) TreeName = "Conical";
            if (FormAddTree.radTypeOval.Checked == true) TreeName = "Oval";
            if (FormAddTree.radTypePyramidal.Checked == true) TreeName = "Pyramidal";
            if (FormAddTree.radTypeRound.Checked == true) TreeName = "Round";
            if (FormAddTree.radTypeSpreading.Checked == true) TreeName = "Spreading";
            return TreeName;
        }

        int getTreeTypeId(string treeTypeName)
        {
            int id = -1;
            if (treeTypeName.ToUpper() == "Spreading".ToUpper()) id = 0;
            if (treeTypeName.ToUpper() == "Round".ToUpper()) id = 1;
            if (treeTypeName.ToUpper() == "Pyramidal".ToUpper()) id = 2;
            if (treeTypeName.ToUpper() == "Oval".ToUpper()) id = 3;
            if (treeTypeName.ToUpper() == "Conical".ToUpper()) id = 4;
            if (treeTypeName.ToUpper() == "Vase".ToUpper()) id = 5;
            if (treeTypeName.ToUpper() == "Columnar".ToUpper()) id = 6;
            if (treeTypeName.ToUpper() == "Open".ToUpper()) id = 7;
            if (treeTypeName.ToUpper() == "Weeping".ToUpper()) id = 8;
            if (treeTypeName.ToUpper() == "Irrigular".ToUpper()) id = 9;
            return id;
        }

        void setTreeShape(int TreeForm)
        {
            whRatio[0] = 1.21; treeTypeName[0] = "Speading shape";
            whRatio[1] = 1.84; treeTypeName[1] = "Round shape";
            whRatio[2] = 2.04; treeTypeName[2] = "Pyramidal shape";
            whRatio[3] = 3.91; treeTypeName[3] = "Oval shape";
            whRatio[4] = 5.52; treeTypeName[4] = "Conical shape";
            whRatio[5] = 1.56; treeTypeName[5] = "Vase shape";
            whRatio[6] = 12.5; treeTypeName[6] = "Columnar shape";
            whRatio[7] = 2.63; treeTypeName[7] = "Open shape";
            whRatio[8] = 2.29; treeTypeName[8] = "Weeping shape";
            whRatio[9] = 3.43; treeTypeName[9] = "Irrigular shape";
            if (TreeForm == 1)
            {
                //Form1 Speading
                treeShape[0, 0] = 0; treeShape[0, 1] = 0.0566037735849057;
                treeShape[1, 0] = 0.154411764705882; treeShape[1, 1] = 0.0660377358490566;
                treeShape[2, 0] = 0.198529411764706; treeShape[2, 1] = 0.405660377358491;
                treeShape[3, 0] = 0.227941176470588; treeShape[3, 1] = 0.679245283018868;
                treeShape[4, 0] = 0.308823529411765; treeShape[4, 1] = 0.905660377358491;
                treeShape[5, 0] = 0.470588235294118; treeShape[5, 1] = 1;
                treeShape[6, 0] = 0.647058823529412; treeShape[6, 1] = 0.905660377358491;
                treeShape[7, 0] = 0.779411764705882; treeShape[7, 1] = 0.707547169811321;
                treeShape[8, 0] = 0.875; treeShape[8, 1] = 0.471698113207547;
                treeShape[9, 0] = 1; treeShape[9, 1] = 0;
            }
            if (TreeForm == 2)
            {
                //Form02 Round
                treeShape[0, 0] = 0; treeShape[0, 1] = 0.0847457627118644;
                treeShape[1, 0] = 0.208695652173913; treeShape[1, 1] = 0.0508474576271186;
                treeShape[2, 0] = 0.304347826086957; treeShape[2, 1] = 0.271186440677966;
                treeShape[3, 0] = 0.339130434782609; treeShape[3, 1] = 0.644067796610169;
                treeShape[4, 0] = 0.408695652173913; treeShape[4, 1] = 0.949152542372881;
                treeShape[5, 0] = 0.547826086956522; treeShape[5, 1] = 1;
                treeShape[6, 0] = 0.71304347826087; treeShape[6, 1] = 0.915254237288136;
                treeShape[7, 0] = 0.852173913043478; treeShape[7, 1] = 0.677966101694915;
                treeShape[8, 0] = 0.947826086956522; treeShape[8, 1] = 0.372881355932203;
                treeShape[9, 0] = 1; treeShape[9, 1] = 0;
            }
            if (TreeForm == 3)
            {
                //Form03 Pyramidal
                treeShape[0, 0] = 0; treeShape[0, 1] = 1;
                treeShape[1, 0] = 0.17741935483871; treeShape[1, 1] = 0.661290322580645;
                treeShape[2, 0] = 0.193548387096774; treeShape[2, 1] = 0.661290322580645;
                treeShape[3, 0] = 0.443548387096774; treeShape[3, 1] = 0.467741935483871;
                treeShape[4, 0] = 0.459677419354839; treeShape[4, 1] = 0.596774193548387;
                treeShape[5, 0] = 0.67741935483871; treeShape[5, 1] = 0.225806451612903;
                treeShape[6, 0] = 0.685483870967742; treeShape[6, 1] = 0.354838709677419;
                treeShape[7, 0] = 0.82258064516129; treeShape[7, 1] = 0.0967741935483871;
                treeShape[8, 0] = 0.82258064516129; treeShape[8, 1] = 0.193548387096774;
                treeShape[9, 0] = 1; treeShape[9, 1] = 0;
            }
            if (TreeForm == 4)
            {
                //Form04 Oval
                treeShape[0, 0] = 0; treeShape[0, 1] = 0.268292682926829;
                treeShape[1, 0] = 0.136363636363636; treeShape[1, 1] = 0.0975609756097561;
                treeShape[2, 0] = 0.220779220779221; treeShape[2, 1] = 0.0731707317073171;
                treeShape[3, 0] = 0.279220779220779; treeShape[3, 1] = 0.585365853658537;
                treeShape[4, 0] = 0.357142857142857; treeShape[4, 1] = 0.951219512195122;
                treeShape[5, 0] = 0.512987012987013; treeShape[5, 1] = 1;
                treeShape[6, 0] = 0.616883116883117; treeShape[6, 1] = 0.853658536585366;
                treeShape[7, 0] = 0.74025974025974; treeShape[7, 1] = 0.926829268292683;
                treeShape[8, 0] = 0.974025974025974; treeShape[8, 1] = 0.341463414634146;
                treeShape[9, 0] = 1; treeShape[9, 1] = 0;
            }
            if (TreeForm == 5)
            {
                //Form05 Conical
                treeShape[0, 0] = 0; treeShape[0, 1] = 0.571428571428571;
                treeShape[1, 0] = 0.0625; treeShape[1, 1] = 0.964285714285714;
                treeShape[2, 0] = 0.178571428571429; treeShape[2, 1] = 1;
                treeShape[3, 0] = 0.375; treeShape[3, 1] = 0.785714285714286;
                treeShape[4, 0] = 0.544642857142857; treeShape[4, 1] = 0.571428571428571;
                treeShape[5, 0] = 0.660714285714286; treeShape[5, 1] = 0.392857142857143;
                treeShape[6, 0] = 0.767857142857143; treeShape[6, 1] = 0.214285714285714;
                treeShape[7, 0] = 0.839285714285714; treeShape[7, 1] = 0.107142857142857;
                treeShape[8, 0] = 0.910714285714286; treeShape[8, 1] = 0;
                treeShape[9, 0] = 1; treeShape[9, 1] = 0;
            }
            if (TreeForm == 6)
            {
                //Form06 Vese
                treeShape[0, 0] = 0; treeShape[0, 1] = 0.106060606060606;
                treeShape[1, 0] = 0.151785714285714; treeShape[1, 1] = 0.0606060606060606;
                treeShape[2, 0] = 0.25; treeShape[2, 1] = 0.181818181818182;
                treeShape[3, 0] = 0.276785714285714; treeShape[3, 1] = 0.378787878787879;
                treeShape[4, 0] = 0.401785714285714; treeShape[4, 1] = 0.636363636363636;
                treeShape[5, 0] = 0.517857142857143; treeShape[5, 1] = 0.863636363636364;
                treeShape[6, 0] = 0.651785714285714; treeShape[6, 1] = 1;
                treeShape[7, 0] = 0.785714285714286; treeShape[7, 1] = 0.893939393939394;
                treeShape[8, 0] = 0.9375; treeShape[8, 1] = 0.484848484848485;
                treeShape[9, 0] = 1; treeShape[9, 1] = 0;
            }
            if (TreeForm == 7)
            {
                //Form07 Columnar
                treeShape[0, 0] = 0; treeShape[0, 1] = 0.258064516129032;
                treeShape[1, 0] = 0.111111111111111; treeShape[1, 1] = 0.161290322580645;
                treeShape[2, 0] = 0.163398692810458; treeShape[2, 1] = 0.451612903225806;
                treeShape[3, 0] = 0.215686274509804; treeShape[3, 1] = 0.870967741935484;
                treeShape[4, 0] = 0.320261437908497; treeShape[4, 1] = 1;
                treeShape[5, 0] = 0.477124183006536; treeShape[5, 1] = 0.870967741935484;
                treeShape[6, 0] = 0.61437908496732; treeShape[6, 1] = 0.838709677419355;
                treeShape[7, 0] = 0.718954248366013; treeShape[7, 1] = 0.741935483870968;
                treeShape[8, 0] = 0.869281045751634; treeShape[8, 1] = 0.548387096774194;
                treeShape[9, 0] = 1; treeShape[9, 1] = 0;
            }
            if (TreeForm == 8)
            {
                //Form08 Open
                treeShape[0, 0] = 0; treeShape[0, 1] = 0.0925925925925926;
                treeShape[1, 0] = 0.18796992481203; treeShape[1, 1] = 0.0555555555555556;
                treeShape[2, 0] = 0.218045112781955; treeShape[2, 1] = 0.777777777777778;
                treeShape[3, 0] = 0.293233082706767; treeShape[3, 1] = 0.981481481481482;
                treeShape[4, 0] = 0.406015037593985; treeShape[4, 1] = 1;
                treeShape[5, 0] = 0.511278195488722; treeShape[5, 1] = 0.981481481481482;
                treeShape[6, 0] = 0.714285714285714; treeShape[6, 1] = 0.814814814814815;
                treeShape[7, 0] = 0.834586466165414; treeShape[7, 1] = 0.685185185185185;
                treeShape[8, 0] = 0.932330827067669; treeShape[8, 1] = 0.333333333333333;
                treeShape[9, 0] = 1; treeShape[9, 1] = 0;
            }
            if (TreeForm == 9)
            {
                //Form09 Weeping
                treeShape[0, 0] = 0; treeShape[0, 1] = 0.103448275862069;
                treeShape[1, 0] = 0.0839160839160839; treeShape[1, 1] = 0.103448275862069;
                treeShape[2, 0] = 0.125874125874126; treeShape[2, 1] = 0.741379310344828;
                treeShape[3, 0] = 0.230769230769231; treeShape[3, 1] = 0.913793103448276;
                treeShape[4, 0] = 0.461538461538462; treeShape[4, 1] = 1;
                treeShape[5, 0] = 0.678321678321678; treeShape[5, 1] = 0.793103448275862;
                treeShape[6, 0] = 0.811188811188811; treeShape[6, 1] = 0.586206896551724;
                treeShape[7, 0] = 0.916083916083916; treeShape[7, 1] = 0.379310344827586;
                treeShape[8, 0] = 0.972027972027972; treeShape[8, 1] = 0.189655172413793;
                treeShape[9, 0] = 1; treeShape[9, 1] = 0;

            }
            if (TreeForm == 10)
            {
                //Form10 Irrigular
                treeShape[0, 0] = 0; treeShape[0, 1] = 0.0566037735849057;
                treeShape[1, 0] = 0.154411764705882; treeShape[1, 1] = 0.0660377358490566;
                treeShape[2, 0] = 0.198529411764706; treeShape[2, 1] = 0.405660377358491;
                treeShape[3, 0] = 0.227941176470588; treeShape[3, 1] = 0.679245283018868;
                treeShape[4, 0] = 0.308823529411765; treeShape[4, 1] = 0.905660377358491;
                treeShape[5, 0] = 0.470588235294118; treeShape[5, 1] = 1;
                treeShape[6, 0] = 0.647058823529412; treeShape[6, 1] = 0.905660377358491;
                treeShape[7, 0] = 0.779411764705882; treeShape[7, 1] = 0.707547169811321;
                treeShape[8, 0] = 0.875; treeShape[8, 1] = 0.471698113207547;
                treeShape[9, 0] = 1; treeShape[9, 1] = 0;
            }

        }

        double treeRedius(double z, int TreeForm)
        {
            setTreeShape(TreeForm);
            double r = 0.001;
            for (int i = 0; i < 9; i++)
            {
                if (treeShape[i, 0] <= z & z < treeShape[i + 1, 0])
                {
                    double x1 = treeShape[i, 0];
                    double y1 = treeShape[i, 1];
                    double x2 = treeShape[i + 1, 0];
                    double y2 = treeShape[i + 1, 1];
                    r = (y2 - y1) / (x2 - x1) * (z - x1) + y1;
                }
            }
            if (r == 0) r = 0.001;
            return r / 40;
        }

        #endregion //Tree

       public void EnableBuildingEdit()
        {
            cmdSelectBuilding.Enabled = true;
            cmdBuilding.Enabled = true;
        }

        private void cmdSolarObstuctionVerification_Click(object sender, EventArgs e)
        {}
        
        public bool SolarObstuctionVerification()
        {
            int NumBuildingErr = 0;
            int NumTreeErr = 0;

            util.ClearGraphicMap(pvMap);

            #region "Building"

            prj.LyrBuilding = util.getLayerHdl(prj.LyrBuildingName, pvMap);
            if (prj.LyrBuilding != -1)
            {
                //1 Structure verification
                if (checkBuildingFieldPropeties(false) == false)
                {
                    MessageBox.Show("Layer " + prj.LyrBuildingName + " Data structure incorrect");
                    prj.LyrBuildingName = "";
                }
                //2 Verify data
                FeatureSet feSet = pvMap.Layers[prj.LyrBuilding].DataSet as FeatureSet;

                for (int i = 0; i < feSet.DataTable.Rows.Count; i++)
                {
                    IFeature fe = feSet.GetFeature(i);
                    object val = fe.DataRow["height"];
                    if (util.IsNumeric(val.ToString()) == false)
                    {
                        NumBuildingErr++;
                        util.DrawPolygonShape(fe, Color.Yellow, Color.Red, 3, pvMap);
                    }
                }

            }
            #endregion

            #region "Tree"
            setTreeShape();
            prj.LyrTree = util.getLayerHdl(prj.LyrTreeName, pvMap);
            if (prj.LyrTree != -1)
            {
                //1 Structure verification
                if (checkTreeFieldPropeties(false) == false)
                {
                    MessageBox.Show("Layer " + prj.LyrTreeName + " Data structure incorrect");
                    prj.LyrTreeName = "";
                   
                }
                //2 Verify data
                FeatureSet feSet = pvMap.Layers[prj.LyrTree].DataSet as FeatureSet;

                for (int i = 0; i < feSet.DataTable.Rows.Count; i++)
                {
                    bool firstErr = true;
                    IFeature fe = feSet.GetFeature(i);
                    object val = fe.DataRow["height"];
                    if (util.IsNumeric(val.ToString()) == false)
                    {
                        if (firstErr)
                        {
                            NumTreeErr++;
                            firstErr = false;
                            util.kDrawCircle(fe.BasicGeometry.Coordinates[0], 2, 16, pvMap, Color.Red);
                        }
                    }
                    val = fe.DataRow["Diameter"];
                    if (util.IsNumeric(val.ToString()) == false)
                    {
                        if (firstErr)
                        {
                            NumTreeErr++;
                            firstErr = false;
                            util.kDrawCircle(fe.BasicGeometry.Coordinates[0], 2, 16, pvMap, Color.Red);
                        }
                    }
                    val = fe.DataRow["Type"];
                    bool chk = false;
                    for (int j = 0; j < 10; j++)
                    {
                        if (treeTypeName[j] == val.ToString()) chk = true;
                    }
                    if (chk == false)
                    {
                        if (firstErr)
                        {
                            NumTreeErr++;
                            firstErr = false;
                            util.kDrawCircle(fe.BasicGeometry.Coordinates[0], 2, 16, pvMap, Color.Red);
                        }
                    }
                }
            #endregion
            }
            pvMap.MapFrame.Invalidate();

            if (NumBuildingErr == 0 & NumTreeErr == 0)
            {
                ExportBldgAndTrr2SketchUp.Enabled = true;
                return true;
            }
            else
            {
                ExportBldgAndTrr2SketchUp.Enabled = false;
                return false;
            }

        }
        
        public void drawBuildingShadow()
        {
            CheckWorkingPath();

            if (prj.Path != "")
            {
                if (prj.Verify[3] == true)
                {// int[] dat = new int[mRow, mCol];

                    int year = dateTimePicker1.Value.Year;
                    double Latitude = Convert.ToDouble(this.txtLAT.Text);
                    double Longitude = Convert.ToDouble(this.txtLNG.Text);
                    double UtmN = Convert.ToDouble(this.txtUtmN.Text);
                    double UtmE = Convert.ToDouble(this.txtUtmE.Text);
                    prj.LyrBuilding = util.getLayerHdl(prj.LyrBuildingName, pvMap);
                    if (prj.LyrBuilding != -1)
                    {
                        IFeatureSet fs = new FeatureSet(FeatureType.Polygon);
                        //---------------------------------------------------------
                        fs.DataTable.Columns.Add(new DataColumn("Azimuth", typeof(double)));
                        fs.DataTable.Columns.Add(new DataColumn("Ele_Angle", typeof(double)));
                        //---------------------------------------------------------
                        IMapFeatureLayer mp = pvMap.Layers[prj.LyrBuilding] as IMapFeatureLayer;
                        //MessageBox.Show("Number of shape = " + mp.DataSet.NumRows());

                        //int nShp = mp.DataSet.NumRows() - 1;
                        IFeatureSet myFe;
                        myFe = new FeatureSet(FeatureType.Polygon);

                        IFeatureSet fea = ((IFeatureLayer)pvMap.GetLayers().ToArray()[prj.LyrBuilding]).DataSet;
                        System.Data.DataTable dt = fea.DataTable;

                        for (int ibldg = 0; ibldg < mp.DataSet.NumRows(); ibldg++)
                        {
                            int numBldgPt = mp.DataSet.GetFeature(ibldg).NumPoints;

                            Coordinate[] pts = new Coordinate[numBldgPt];
                            Coordinate[] ptss = new Coordinate[numBldgPt * 2];
                            IFeature blgdFs = mp.DataSet.GetFeature(ibldg);
                            string h1 = blgdFs.DataRow["Height"].ToString();
                            if (util.IsNumeric(h1))
                            {
                                double h = Convert.ToDouble(h1);//dt.Rows[ibldg]["Height"]);

                                for (int i = 0; i < numBldgPt; i++)
                                {
                                    pts[i] = new Coordinate(blgdFs.Coordinates[i].X, blgdFs.Coordinates[i].Y, h);
                                    ptss[i] = new Coordinate(blgdFs.Coordinates[i].X, blgdFs.Coordinates[i].Y);
                                }
                                //--------------------------------------------
                                // Assign building data
                                double x0 = UtmE;
                                double y0 = UtmN;
                                double dx = 20;
                                double dy = 50;
                                //---------------------------------------------

                                short TimeZone = -7;
                                Int32 nightHr = 0;
                                Int32 noonHr = 0;
                                prgBar.Maximum = 12 * 2 * 24; // frequency about 15 day
                                prgBar.Visible = true;

                                for (int month = 1; month <= 12; month++)
                                //Parallel.For(1, 12, month =>
                                {
                                    int month_day = System.DateTime.DaysInMonth(2001, month);
                                    for (int day = 1; day <= month_day; day += 7)
                                    {
                                        for (int hr = 0; hr < 24; hr++)
                                        {
                                            //prgBar.PerformStep();

                                            double hrPassMidnight = (double)hr / 24.0;
                                            SolarCal ySolar = new SolarCal(day, month, year, hrPassMidnight, Latitude, Longitude, TimeZone);
                                            //SolarCal ySolar = new SolarCal(day, month, year, hrPassMidnight, Latitude, Longitude, TimeZone);
                                            double eleAng = ySolar.SolarElevationAngle;
                                            double AzAng = ySolar.SolarAzimuthAngle;
                                            //--------------------------------------------------------------
                                            if (eleAng >= 15) // efficetive elevation angle
                                            {
                                                noonHr++;
                                                //Shadow point
                                                for (int i = 0; i < pts.Length; i++)
                                                {
                                                    Shadow shadow = new Shadow(AzAng, eleAng, pts[i]);
                                                    Coordinate tmp = shadow.shadowPt;
                                                    ptss[i + numBldgPt] = new Coordinate(tmp.X, tmp.Y);
                                                }
                                                // CONVEX HULL Algorithm for make a shadow area
                                                var multiPoint = new MultiPoint(ptss);
                                                var convexHull = (Polygon)multiPoint.ConvexHull();
                                                myFe.AddFeature(convexHull);
                                            }
                                            else // Night hour
                                            {
                                                nightHr++;
                                            }

                                        }
                                    }

                                }//); // Parallel.For
                            }//End if numeric check
                        }

                        Console.WriteLine(DateTime.Now.ToString());
                        Console.ReadLine();

                        prgBar.Visible = false;
                        //IFeatureSet result = myFe.UnionShapes(ShapeRelateType.Intersecting);
                        myFe.Projection = pvMap.Projection;
                        //result.Projection = pvMap.Projection;

                        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                        myFe.Name = "BuildingShadowMap";
                        myFe.Filename = prj.Path + "\\Temp\\" + myFe.Name + ".shp";
                        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


                        myFe.SaveAs(myFe.Filename, true);
                        //result.Filename =prj.Path + "\\Temp\\shadow map union.shp";
                        //result.SaveAs(result.Filename, true);
                        //pvMap.Layers.Add(myFe);

                        MapPolygonLayer ShadowArea;
                        ShadowArea = new MapPolygonLayer(myFe);// MapPolygonLayer(fs);
                        PolygonSymbolizer ShadowSymboize = new PolygonSymbolizer(Color.Black, Color.Red);
                        // set transparent
                        SimplePattern sp = new SimplePattern(Color.Black);
                        sp.Opacity = 0.5f;
                        ShadowSymboize.Patterns.Clear();
                        ShadowSymboize.Patterns.Add(sp);
                        ShadowArea.Symbolizer = ShadowSymboize;
                        util.removeDupplicateLyr(ShadowArea.Name, pvMap);
                        pvMap.Layers.Add(ShadowArea);
                        MessageBox.Show("Building shadows have been successfully drawn.");
                    }
                    else
                    {
                        MessageBox.Show("Please create a building Layer first.");
                    }
                }
                else
                {
                    MessageBox.Show("Please calculate sun path statistic first.");
                }
            }
            mapAct = mapAction.None;
        }

        private void ExportBldgAndTrr2SketchUp_Click(object sender, EventArgs e)
        {

            pvVerify();
            if (verify[0] == false)
            {
                MessageBox.Show("Please assign a reference location before exporting data to SketchUp");
                return;
            }

            FolderBrowserDialog folderSel = new FolderBrowserDialog();
            folderSel.Description = "Select file location to export Sketchup files:";
            //folderSel.ShowDialog();
            DialogResult result = folderSel.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (folderSel.SelectedPath != null)
                {

                    //Export2SketchUp4PvPanel
                    int treeLyr = util.getLayerHdl(prj.LyrTreeName, pvMap);
                    if (treeLyr != -1)
                    {
                        IMapFeatureLayer treeFs = pvMap.Layers[treeLyr] as IMapFeatureLayer;
                        Export2SketchUp4Tree(treeFs, folderSel.SelectedPath);
                    }
                    else
                    {
                        MessageBox.Show("Cannot export tree model. Please select tree layer first.");
                    }
                    //Export2SketchUp4Building
                    int bldgLyr = util.getLayerHdl(prj.LyrBuildingName, pvMap);
                    if (bldgLyr != -1)
                    {
                        IMapFeatureLayer bldgFs = pvMap.Layers[bldgLyr] as IMapFeatureLayer;
                        Export2SketchUp4Bldg(bldgFs, folderSel.SelectedPath);
                    }
                    else
                    {
                        MessageBox.Show("Cannot export building model. Please select building layer first.");
                    }
                    MessageBox.Show("Google SketchUp file export completed");
                }
                else
                {
                    MessageBox.Show("Select file location to export Sketchup files");
                }
            }
            else
            {
                result = DialogResult.Cancel;
            }


        }

        #endregion //"SUN Properties----------------------------------------------------------------------------------------------"

        #region"Weather-----------------------------------------------------------------------------------------------------"

        private void cmdShowIdwSta_Click(object sender, EventArgs e)
        {
            pvVerify();
            if (verify[0] == false)
            {
                MessageBox.Show("Please assign a reference location");
                return;
            }
            //-----------------------------------------------------------------------
            double radius = 1000;
            Coordinate c = new Coordinate(Convert.ToDouble(txtUtmE.Text), Convert.ToDouble(txtUtmN.Text));
            pvMap.MapFrame.DrawingLayers.Clear();
            int nIDW = Convert.ToInt16(txtNIdwSta.Text);
            util.kDrawCircle(c.X, c.Y, radius, 360, pvMap, Color.Magenta);
            for (int ii = 0; ii < nIDW; ii++)
            {
                double lat = wSta[wStaSel[ii]].LAT2;
                double lng = wSta[wStaSel[ii]].LONG2;
                double x = 0;
                double y = 0;
                double[] mapCoordinate = new double[] { lng, lat };
                Reproject.ReprojectPoints(mapCoordinate, new double[] { 0 }, KnownCoordinateSystems.Geographic.World.WGS1984, pvMap.Projection, 0, 1);
                Coordinate coord = util.circleCoord(c.X, c.Y, mapCoordinate[0], mapCoordinate[1], radius); //returns coordinate of point on inner circle
                Coordinate coord2 = util.circleCoord(mapCoordinate[0], mapCoordinate[1], c.X, c.Y, radius); //returns coordinates of point on outer circle

                util.kDrawCircle(mapCoordinate[0], mapCoordinate[1], radius, 360, pvMap, Color.Magenta);
                util.DrawLine(coord2.X, coord2.Y, coord.X, coord.Y, 2, Color.Magenta, pvMap);
            }
        }

        #endregion //"Weather-----------------------------------------------------------------------------------------------"

        #region "Layout------------------------------------------------------------------------------------------------------"

        private void CreatePvPosition()
        {

            if (util.IsNumeric(rp.RoofXSpace) == false) { MessageBox.Show("Spacing X data incorrect"); return; }
            if (util.IsNumeric(rp.RoofYSpace) == false) { MessageBox.Show("Spacing Y data incorrect"); return; }
            if (util.IsNumeric(rp.RoofTilt) == false) { MessageBox.Show("Roof tilt data incorrect"); return; }
            if (util.IsNumeric(rp.RoofAzimuth) == false) { MessageBox.Show("Roof azimuth data incorrect"); return; }
            if (util.IsNumeric(txtRidgeHeight.Text) == false) { MessageBox.Show("Ridge height data incorrect"); return; }
            if (util.IsNumeric(txtEaveHeight.Text) == false) { MessageBox.Show("Eave height data incorrect"); return; }

            //----------------------------------------------------------------------------
            if (RidgeLine.Count < 2 & EaveLine.Count < 2)
            {
                MessageBox.Show("Plane data incorrect");
                return;
            }
            else
            {
                util.ClearGraphicMap(pvMap);
                double RidgeEle = 1;// Convert.ToDouble(txtRidgeHeight.Text);
                double EaveEle = 1;// Convert.ToDouble(txtEaveHeight.Text);
                rp = new RoofPlane(RidgeLine, RidgeEle, EaveLine, EaveEle);
                Coordinate midRidge = rp.RidgeLine.getMidLine();
                Coordinate midREave = rp.EaveLine.getMidLine();
                Coordinate midRoof = rp.getMidRoof();
                // rp.
                //util.kDrawCircle(midRidge.X, midRidge.Y, 1, 12, pvMap, Color.Blue);
                //util.kDrawCircle(midREave.X, midREave.Y, 1, 12, pvMap, Color.Magenta);
                util.DrawLineCross(midRoof.X, midRoof.Y, 2, 1, Color.Magenta, pvMap);
                //util.kDrawCircle(midRoof.X, midRoof.Y, 1, 12, pvMap, Color.Red);
                double spacingX = Convert.ToDouble(txtGridSpacingX.Text);
                double spacingY = Convert.ToDouble(txtGridSpacingY.Text);
                double Tilt = Convert.ToDouble(txtRoofTilt.Text);
                double RoofAz = Convert.ToDouble(rp.RoofAzimuth);
                double RidgeH = Convert.ToDouble(txtRidgeHeight.Text);
                double EaveH = Convert.ToDouble(txtEaveHeight.Text);
                double refEle = (RidgeH + EaveH) / 2;
                List<Coordinate> pvPosition = rp.getPvPointOnRoofPlane(spacingX, spacingY, 0, 0, Tilt, refEle, cmdCheck4PvOnRoof.Checked);

                FeatureSet fs = new FeatureSet(FeatureType.Point);
                //---------------------------------------------------------
                fs.DataTable.Columns.Add(new DataColumn("x", typeof(double)));
                fs.DataTable.Columns.Add(new DataColumn("y", typeof(double)));
                fs.DataTable.Columns.Add(new DataColumn("w", typeof(double)));
                fs.DataTable.Columns.Add(new DataColumn("h", typeof(double)));
                fs.DataTable.Columns.Add(new DataColumn("Azimuth", typeof(double)));
                fs.DataTable.Columns.Add(new DataColumn("Ele_Angle", typeof(double)));
                fs.DataTable.Columns.Add(new DataColumn("ele", typeof(double)));
                //---------------------------------------------------------

                foreach (Coordinate c in pvPosition)
                {
                    IPoint poleFe = new DotSpatial.Topology.Point(c);
                    IFeature ifea = fs.AddFeature(poleFe);

                    ifea.DataRow.BeginEdit();
                    ifea.DataRow["x"] = c.X;
                    ifea.DataRow["y"] = c.Y;
                    ifea.DataRow["w"] = 0;
                    ifea.DataRow["h"] = 0;
                    ifea.DataRow["Azimuth"] = RoofAz;
                    ifea.DataRow["Ele_Angle"] = Tilt;
                    ifea.DataRow["ele"] = c.Z;

                    ifea.DataRow.EndEdit();
                }

                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++
                fs.Name = "RooftopPanel";
                fs.Filename = prj.Path + "\\Temp\\" + fs.Name + ".shp";
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++

                util.removeDupplicateLyr(fs.Name, pvMap);
                fs.Projection = pvMap.Projection;
                fs.Save();

                pvMap.Layers.Add(fs);
                cmbRoofTopPanelPosition.Text = fs.Name;
                loadLayerList();

                // rp.DrawRoofPland(pvMap);

                // Request a redraw
                //pvMap.MapFrame.Invalidate();

            }
        }

        private void cmdCreatePvPosition_Click(object sender, EventArgs e)
        {
            CreatePvPosition();
        }

        private void rdoKML_CheckedChanged(object sender, EventArgs e)
        {
            radioCheck4PolesCreation();
        }

        private void rdolignment_CheckedChanged(object sender, EventArgs e)
        {
            radioCheck4PolesCreation();
        }

        private void rdoSiteArea_CheckedChanged(object sender, EventArgs e)
        {
            radioCheck4PolesCreation();
        }

        private void rdoManual_CheckedChanged(object sender, EventArgs e)
        {
            radioCheck4PolesCreation();
        }

        frmPanelSelection formSelectPanel;
        public frmPvPanelSetup formPVPanel; 

        private void cmdPvPanelAngle_Click(object sender, EventArgs e)
        {
            //prj.LyrPole = cmbPolePosition;
            if (util.getLayerHdl(prj.LyrPvPanelName, pvMap) != -1)
            {

                setCurrrentLayer(prj.LyrPvPanelName);                
                formSelectPanel = new frmPanelSelection();
                formSelectPanel.Michael = this;
                formSelectPanel.PvMap = pvMap;
                formSelectPanel.Project = prj;
                formSelectPanel.Show();
                formSelectPanel.TopMost = true;
                pvMap.FunctionMode = FunctionMode.Select;
                formPVPanel = new frmPvPanelSetup();
                formPVPanel.Michael = this;
                formPVPanel.PvMap = pvMap;
                //prj.CmbPvPole = cmbPolePosition;
                prj.LyrPvPanel = util.getLayerHdl(prj.LyrPvPanelName, pvMap);
                formPVPanel.Project = prj;  
               
            }
            else
            {
                MessageBox.Show("Select panel locations before editing panels");
            }


        }

        frmCreatePoleByAlignment frmAlignment;
        frmCreatePoleBySiteArea frmSiteArea;
        private void cmdCreatePvPole_Click(object sender, EventArgs e)
        {
            prj.UseKML = false;
            mapAct = mapAction.None;
            if (rdoAlignment.Checked == true)
            {
                frmAlignment = new frmCreatePoleByAlignment();
                frmAlignment.Michael = this;
                frmAlignment.TopMost = true;
                prj.LyrAlignment = util.getLayerHdl(cmbAlignmentLyr.Text, pvMap);
                prj.LyrDEM = util.getLayerHdl(prj.LyrDEMname, pvMap);
                //prj.CmbPvPole = cmbPolePosition;
                //prj.CmbPvPanel = cmbPanel;
                frmAlignment.ProjectFile = prj;

                // frmAlignment.MainForm = this; 
                if (prj.LyrAlignment != -1)
                {
                    frmAlignment.PvMap = pvMap;
                    frmAlignment.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please select alignment layer to continue");
                }
            }
            if (rdoSiteArea.Checked == true)
            {
                frmSiteArea = new frmCreatePoleBySiteArea();
                frmSiteArea.Michael = this;
                frmSiteArea.TopMost = true;

                prj.LyrSiteArea = util.getLayerHdl(prj.LyrSiteAreaName, pvMap);
                prj.LyrDEM = util.getLayerHdl(prj.LyrDEMname, pvMap);
                //prj.CmbPvPole = cmbPolePosition;
                //prj.CmbPvPanel = cmbPanel;


                if (prj.LyrSiteArea != -1)
                {
                    frmSiteArea.Project = prj;
                    frmSiteArea.pvMap = pvMap;
                    frmSiteArea.Show();
                }
                else
                {
                    MessageBox.Show("Please select site layer to continue");
                }
            }
        }

        #endregion//Layout----------------------------------------------------------------------------------------------

        #region"Rooftop----------------------------------------------------------------------------------------------"

        private void cmdCreatePvPanelShp_Click(object sender, EventArgs e)
        { }

        public void CreatePvPanel()
        {
            CheckWorkingPath();

            if (prj.Path != "")
            {
                /*
                if (verify[3] == false)
                {
                    MessageBox.Show("Please assign solar properties before continuing");
                    return;
                }
                 */ 
                //if (formSiteArea.cmbTrack_mode.SelectedIndex == -1) 
               // {
               //     MessageBox.Show("Please select tracking mode before calculating");
               //     //return;
               // }
                //cmdReport.Enabled = false;
                pvMap.MapFrame.DrawingLayers.Clear();

                //------------------------------------------------------------------
                //------------------------------------------------------------------
                //------------------------------------------------------------------

                double w = 1;// Convert.ToDouble(txtPvWidth.Text);
                double h = 2;// Convert.ToDouble(txtPvLength.Text);
                double tilt = 45;
                double azimuth = 180;
                double z = 0;
                int lyrId = 0;
                //for (int k = 1; k <= 100; k++)
                //{
                    lyrId = util.getLayerHdl(prj.LyrPoleName, pvMap); //("Panel Position_"+k, pvMap);
                    if (lyrId != -1)
                    {
                        //-----------------------------------------------------
                        IFeatureSet pvfs;
                        pvfs = new FeatureSet(FeatureType.Polygon);
                        //---------------------------------------------------------
                        pvfs.DataTable.Columns.Add(new DataColumn("x", typeof(double)));
                        pvfs.DataTable.Columns.Add(new DataColumn("y", typeof(double)));
                        pvfs.DataTable.Columns.Add(new DataColumn("w", typeof(double)));
                        pvfs.DataTable.Columns.Add(new DataColumn("h", typeof(double)));
                        pvfs.DataTable.Columns.Add(new DataColumn("Azimuth", typeof(double)));
                        pvfs.DataTable.Columns.Add(new DataColumn("Ele_Angle", typeof(double)));
                        pvfs.DataTable.Columns.Add(new DataColumn("ele", typeof(double)));
                        //---------------------------------------------------------

                        //-----------------------------------------------------
                        IMapFeatureLayer poleFe = pvMap.Layers[lyrId] as IMapFeatureLayer;
                        //MessageBox.Show("Number of pole = " + poleFe.DataSet.NumRows());
                        int nShp = poleFe.DataSet.NumRows() - 1;
                        Extent ext = poleFe.DataSet.GetFeature(nShp).Envelope.ToExtent();
                        numPvPanel = poleFe.DataSet.NumRows();
                        //MessageBox.Show(ext.X.ToString() + "," + ext.Y.ToString() + "," + ext.Width.ToString() + "," + ext.Height.ToString());
                        for (int i = 0; i < poleFe.DataSet.NumRows(); i++)
                        {
                            IFeature fs = poleFe.DataSet.GetFeature(i);
                            double x1 = Convert.ToDouble(fs.DataRow["X"]); //fs.Coordinates[0].X;
                            double y1 = Convert.ToDouble(fs.DataRow["Y"]); //fs.Coordinates[0].Y;
                            w = Convert.ToDouble(fs.DataRow["W"]);
                            h = Convert.ToDouble(fs.DataRow["H"]);
                            z = Convert.ToDouble(fs.DataRow["ele"]);
                            tilt = Convert.ToDouble(fs.DataRow["Ele_angle"]);
                            azimuth = Convert.ToDouble(fs.DataRow["Azimuth"]);

                            IFeature ifea = pvfs.AddFeature(pvPanelFe(w, h, x1, y1, z, tilt, azimuth));
                            ifea.DataRow.BeginEdit();
                            ifea.DataRow["x"] = x1;
                            ifea.DataRow["y"] = y1;
                            ifea.DataRow["w"] = w;
                            ifea.DataRow["h"] = h;
                            ifea.DataRow["Azimuth"] = azimuth;
                            ifea.DataRow["Ele_Angle"] = tilt;
                            ifea.DataRow["ele"] = z;
                            ifea.DataRow.EndEdit();

                        }

                        pvfs.Projection = pvMap.Projection;
                        String AreaName = prj.LyrSiteAreaName;
                        //++++++++++++++++++++++++++++++++++++++++++++++++++
                        pvfs.Name = AreaName + " PV Array"; 
                        pvfs.Filename = prj.Path + "\\Temp\\" + pvfs.Name + ".shp";
                        //++++++++++++++++++++++++++++++++++++++++++++++++++
                        poleFe.DataSet.IndexMode = false;
                        pvfs.SaveAs(pvfs.Filename, true);
                        util.removeDupplicateLyr(pvfs.Name, pvMap);
                        pvMap.Layers.Add(pvfs);
                        prj.LyrPvPanelName = pvfs.Name;
                        cmbPanel.Text = prj.LyrPvPanelName;
                        //loadLayerList();
                        cmdExportSketchUp.Enabled = true;
                        /*
                                            if (optPvWattFunc.Checked == true)
                                            {
                                                EnergyProduction(numPvPanel);
                                                cmdReport.Enabled = true;
                                                cmdOptimization.Enabled = true;
                                            }
                                            if (optMultiWeatherSta.Checked == true | optSingleWeatherSta.Checked == true)
                                            {
                                                if (System.IO.File.Exists(txtTM2.Text) == true)
                                                {
                                                    EnergyProduction(numPvPanel, txtTM2.Text);
                                                    cmdReport.Enabled = true;
                                                    cmdOptimization.Enabled = true;
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Weather file is incorrect");
                                                }
                                            }
                    
                                                pvMap.MapFrame.DrawingLayers.Clear();
                                                MapPolygonLayer rangeRingAxis;
                                                rangeRingAxis = new MapPolygonLayer(pvfs);
                                                rangeRingAxis.Symbolizer = new PolygonSymbolizer(System.Drawing.Color.AliceBlue, System.Drawing.Color.LightBlue);
                                                pvMap.MapFrame.DrawingLayers.Add(rangeRingAxis);
                                                pvMap.MapFrame.Invalidate();
                                             */
                        //updateArea();
                    }
                    //else
                    //{
                       // MessageBox.Show("Please select the pole layer before continuing");
                    //}  
                //}      for loop k           

                //------------------------------------------------------------------
                //------------------------------------------------------------------
                //------------------------------------------------------------------


                //++++++++
            }
        }

        private void cmdRidgeLine_Click(object sender, EventArgs e)
        {
            mapAct = mapAction.firstRidgePoint;
            pvMap.FunctionMode = FunctionMode.None;
            pvMap.Cursor = Cursors.Cross;
            DrawRoof = true;
            UpDateRoofShape();
            panelDrawRoof.Invalidate();

        }

        private void cmdZoomToSite_Click(object sender, EventArgs e)
        {
            Envelope env = new Envelope();
            env.SetExtents(prj.UtmE - 1000, prj.UtmN - 1000, prj.UtmE + 1000, prj.UtmN + 1000);
            pvMap.ViewExtents = env.ToExtent();
        }

        private void cmdEaveLine_Click(object sender, EventArgs e)
        {
            mapAct = mapAction.firstEavePoint;
            pvMap.FunctionMode = FunctionMode.None;
            pvMap.Cursor = Cursors.Cross;
            DrawRoof = true;
            UpDateRoofShape();
            panelDrawRoof.Invalidate();
        }

        private void cmdRoofPlane_Click(object sender, EventArgs e)
        {
            RoofPlane();
        }

        private void RoofPlane()
        {
            if (EaveLine.Count >= 2 & RidgeLine.Count >= 2)
            {
                Coordinate p1 = RidgeLine[0];
                Coordinate p2 = RidgeLine[1];
                //Darw edge
                double L1, L2;
                L1 = util.Distance(RidgeLine[0], EaveLine[0]);
                L2 = util.Distance(RidgeLine[0], EaveLine[1]);
                if (L1 <= L2)
                {
                    util.DrawLine(RidgeLine[0], EaveLine[0], 1, Color.Magenta, pvMap);
                    util.DrawLine(RidgeLine[1], EaveLine[1], 1, Color.Magenta, pvMap);
                }
                else
                {
                    util.DrawLine(RidgeLine[1], EaveLine[0], 1, Color.Magenta, pvMap);
                    util.DrawLine(RidgeLine[0], EaveLine[1], 1, Color.Magenta, pvMap);
                }
                Coordinate intersecPt = new Coordinate();
                Coordinate mid = util.midLine(p1, p2);
                double L = util.FindDistanceToSegment(mid, EaveLine[0], EaveLine[1], out intersecPt);
                double az = util.getAzimuth(mid, intersecPt);
                // util.kDrawCircle(mid.X, mid.Y, 1, 360, pvMap, Color.Yellow);
                Coordinate NothLine = new Coordinate(mid.X, mid.Y + 1);
                util.DrawLine(mid, NothLine, 2, Color.Magenta, pvMap);
                util.DrawLine(mid, intersecPt, 2, Color.Magenta, pvMap);
                Int16 dv = Convert.ToInt16(az);
                util.kDrawArc(mid.X, mid.Y, 1, 0, az, dv, pvMap, Color.Magenta);
                //MessageBox.Show("L = " +L.ToString()+" m." );  
                txtBottomDepth.Text = Math.Round(L, 2).ToString();
            }
            panelDrawRoof.Invalidate();
            cmdCreatePvPosition.Enabled = true;
            DrawRoof = true;
            UpDateRoofShape();
            panelDrawRoof.Invalidate();
            rp.RoofAzimuth = RoofAzimuth().ToString();
            double RoofTilt = 0;
            RoofTilt = Convert.ToInt16(Math.Atan((double)Py / (double)Px) * 180 / Math.PI);
            txtRoofTilt.Text = RoofTilt.ToString();
            rp.RoofTilt = Convert.ToString(RoofTilt);
            CreatePvPosition();
        }



        private void txtPy_TextChanged(object sender, EventArgs e)
        {
            if (util.NummericTextBoxCheck(txtPy, "Roof Pitch", 12) == true)
            {
                Py = Convert.ToInt16(txtPy.Text);
                UpDateRoofShape();
                DrawRoof = true;
                panelDrawRoof.Invalidate();
                RoofPlane();
            }
        }
        RoofPlane rp = new RoofPlane();
        private void cmdTiltUp_Click(object sender, EventArgs e)
        {

            Py = Convert.ToInt16(txtPy.Text) + 1;
            txtPy.Text = Py.ToString();
            DrawRoof = true;
            UpDateRoofShape();
            panelDrawRoof.Invalidate();
            Double L = BottomDepth();
            txtBottomDepth.Text = L.ToString();
            double h = Math.Round(L * Py / Px, 2);
            double eaveH = Convert.ToDouble(txtEaveHeight.Text);
            txtRidgeHeight.Text = (eaveH + h).ToString();
            if (RoofAzimuth() == -1)
            {
                txtRoofAz.Text = "NULL";
            }
            else
            {
                txtRoofAz.Text = RoofAzimuth().ToString();
            }
            double RoofTilt = 0;
            RoofTilt = Convert.ToInt16(Math.Atan((double)Py / (double)Px) * 180 / Math.PI);
            txtRoofTilt.Text = RoofTilt.ToString();
            rp.RoofTilt = Convert.ToString(RoofTilt);
            RoofPlane();
        }

        private void cmdTiltDown_Click(object sender, EventArgs e)
        {
            Py = Convert.ToInt16(txtPy.Text) - 1;
            txtPy.Text = Py.ToString();
            DrawRoof = true;
            UpDateRoofShape();
            panelDrawRoof.Invalidate();
            Double L = BottomDepth();
            txtBottomDepth.Text = L.ToString();
            double h = Math.Round(L * Py / Px, 2);
            double eaveH = Convert.ToDouble(txtEaveHeight.Text);
            txtRidgeHeight.Text = (eaveH + h).ToString();
            if (RoofAzimuth() == -1)
            {
                txtRoofAz.Text = "NULL";
            }
            else
            {
                txtRoofAz.Text = RoofAzimuth().ToString();
                rp.RoofAzimuth = RoofAzimuth().ToString();
            }
            double RoofTilt = 0;
            RoofTilt = Convert.ToInt16(Math.Atan((double)Py / (double)Px) * 180 / Math.PI);
            txtRoofTilt.Text = RoofTilt.ToString();
            cmdRedrawRoofPlan.Enabled = true;
            rp.RoofTilt = Convert.ToString(RoofTilt);
            RoofPlane();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            if (DrawRoof == true)
            {
                Pen blackpen = new Pen(Color.Black, 3);
                Graphics g = e.Graphics;
                int x1 = Convert.ToInt16(imgRoof[0].X);
                int x2 = Convert.ToInt16(imgRoof[1].X);
                int x3 = Convert.ToInt16(imgRoof[2].X);
                int y1 = Convert.ToInt16(imgRoof[0].Y);
                int y2 = Convert.ToInt16(imgRoof[1].Y);
                int y3 = Convert.ToInt16(imgRoof[2].Y);
                //Roof
                g.DrawLine(blackpen, x1, y1, x2, y2);
                g.DrawLine(blackpen, x2, y2, x3, y3);
                //Column
                g.DrawLine(blackpen, 10, 62, 10, 40);
                g.DrawLine(blackpen, 90, 62, 90, 40);
                g.Dispose();
                DrawRoof = false;
            }

        }

        private void txtBottomDepth_TextChanged(object sender, EventArgs e)
        {
            if (util.NummericTextBoxCheck(txtBottomDepth, "Base Length", 10, true))
            {
                double L = Convert.ToDouble(txtBottomDepth.Text);
                double h = Math.Round(L * Py / Px, 2);
                double eaveH = Convert.ToDouble(txtEaveHeight.Text);
                txtRidgeHeight.Text = (eaveH + h).ToString();
            }
        }

        private void cmdRedrawRoofPlan_Click(object sender, EventArgs e)
        {
            double L = Convert.ToDouble(txtBottomDepth.Text);
            double h = Math.Round(L * Py / Px, 2);
            double eaveH = Convert.ToDouble(txtEaveHeight.Text);

            Coordinate pt1 = new Coordinate();
            Coordinate pt2 = new Coordinate();
            double L1 = util.FindDistanceToSegment(EaveLine[0], RidgeLine[0], RidgeLine[1], out pt1);
            double L2 = util.FindDistanceToSegment(EaveLine[1], RidgeLine[0], RidgeLine[1], out pt2);
            double ratio = -L / L1;
            EaveLine[0].X = (pt1.X - EaveLine[0].X) * ratio + pt1.X;
            EaveLine[0].Y = (pt1.Y - EaveLine[0].Y) * ratio + pt1.Y;

            EaveLine[1].X = (pt2.X - EaveLine[1].X) * ratio + pt2.X;
            EaveLine[1].Y = (pt2.Y - EaveLine[1].Y) * ratio + pt2.Y;
            util.ClearGraphicMap(pvMap);
            util.DrawLine(RidgeLine[0], RidgeLine[1], 1, Color.Red, pvMap);
            util.DrawLine(EaveLine[0], EaveLine[1], 1, Color.Magenta, pvMap);
        }


        #endregion //"Rooftop----------------------------------------------------------------------------------------------"

        //---------------------------------------------------------------------------------------------------------------------
        // UTILITIES
        //---------------------------------------------------------------------------------------------------------------------

        #region "Utilities---------------------------------------------------------------------------------------------------"

        Polygon pvPanelFe(double w, double h, double x0, double y0, double z0, double tilt, double az)
        {
            //az = 10;
            double rotationAngle = util.Azm2Qurdrant(az) + 90;
            Coordinate[] pvShape = new Coordinate[5]; //x-axis
            Coordinate[] pvShapeR = new Coordinate[5]; //x-axis
            double hr = h * Math.Cos(tilt * Math.PI / 180);
            double zr = h * Math.Sin(tilt * Math.PI / 180);
            pvShape[0] = new Coordinate(-w / 2, -hr / 2, -zr / 2);
            pvShape[1] = new Coordinate(+w / 2, -hr / 2, -zr / 2);
            pvShape[2] = new Coordinate(+w / 2, +hr / 2, +zr / 2);
            pvShape[3] = new Coordinate(-w / 2, +hr / 2, +zr / 2);
            for (int i = 0; i < 4; i++)
            {
                Coordinate xy = util.Rotate(pvShape[i].X, pvShape[i].Y, rotationAngle);
                //double x = kGeoFunc.Rx(pvShape[i].X, pvShape[i].Y, az);
                //double y = kGeoFunc.Ry(pvShape[i].X, pvShape[i].Y, az);
                double x = xy.X;
                double y = xy.Y;
                pvShapeR[i] = new Coordinate(x0 + x, y0 + y, z0 + pvShape[i].Z);

            }
            pvShapeR[4] = new Coordinate(pvShapeR[0]);
            Polygon Poly = new Polygon(pvShapeR);
            return Poly;
        }

        String pvPanel(double w, double h, double x0, double y0, double z0, double tilt, double az)
        {
            Coordinate[] pvShape = new Coordinate[4]; //x-axis
            Coordinate[] pvShapeR = new Coordinate[4]; //x-axis
            double hr = h * Math.Cos(tilt * Math.PI / 180);
            double zr = h * Math.Sin(tilt * Math.PI / 180);

            //double zr = h * Math.Sin(tilt * Math.PI / 180);
            //pvShape[0] = new Coordinate(-w / 2, -hr / 2, -zr / 2);

            pvShape[0] = new Coordinate(-w / 2, -hr / 2, -zr / 2);
            pvShape[1] = new Coordinate(+w / 2, -hr / 2, -zr / 2);
            pvShape[2] = new Coordinate(+w / 2, +hr / 2, +zr / 2);
            pvShape[3] = new Coordinate(-w / 2, +hr / 2, +zr / 2);
            for (int i = 0; i < 4; i++)
            {
                Coordinate xy = util.Rotate(pvShape[i].X, pvShape[i].Y, az);
                //double x = kGeoFunc.Rx(pvShape[i].X, pvShape[i].Y, az);
                //double y = kGeoFunc.Ry(pvShape[i].X, pvShape[i].Y, az);
                double x = xy.X;
                double y = xy.Y;
                pvShapeR[i] = new Coordinate(x0 + x, y0 + y, z0 + pvShape[i].Z);

            }
            string vertex1 = pvShapeR[2].X.ToString() + " " + pvShapeR[2].Y.ToString() + " " + pvShapeR[2].Z.ToString();
            string vertex2 = pvShapeR[0].X.ToString() + " " + pvShapeR[0].Y.ToString() + " " + pvShapeR[0].Z.ToString();
            string vertex3 = pvShapeR[1].X.ToString() + " " + pvShapeR[1].Y.ToString() + " " + pvShapeR[1].Z.ToString();
            string vertex4 = pvShapeR[3].X.ToString() + " " + pvShapeR[3].Y.ToString() + " " + pvShapeR[3].Z.ToString();
            String fe = vertex1 + " " + vertex2 + " " + vertex3 + " " + vertex4;
            return fe;
        }

        void AddBruTileLayer(string LayerSource)
        {
            try
            {
                for (int i = pvMap.Layers.Count - 1; i >= 0; i--)
                {
                    string str = pvMap.Layers[i].LegendText;
                    if (str.Length >= 11)
                    {
                        string str2 = str.Substring(0, 11);
                        if (str2.ToUpper() == "Brutile.Web".ToUpper())
                        {
                            pvMap.Layers.RemoveAt(i);
                        }
                    }
                }
                try
                {
                    int OnlineLyr = -1;
                    if (LayerSource == "BingAerialLayer") pvMap.Layers.Add(BruTileLayer.CreateBingAerialLayer());
                    if (LayerSource == "BingHybridLayer") pvMap.Layers.Add(BruTileLayer.CreateBingHybridLayer());
                    if (LayerSource == "GoogleMapLayer") pvMap.Layers.Add(BruTileLayer.CreateGoogleMapLayer());
                    if (LayerSource == "GoogleSatelliteLayer") pvMap.Layers.Add(BruTileLayer.CreateGoogleSatelliteLayer());
                    if (LayerSource == "GoogleTerrainLayer") pvMap.Layers.Add(BruTileLayer.CreateGoogleTerrainLayer());
                    if (LayerSource == "OsmLayer") pvMap.Layers.Add(BruTileLayer.CreateOsmLayer());
                }
                catch { }

                for (int i = pvMap.Layers.Count - 1; i >= 0; i--)
                {
                    string str = pvMap.Layers[i].LegendText;
                    if (str.Length >= 11)
                    {
                        string str2 = str.Substring(0, 11);
                        if (str2.ToUpper() == "Brutile.Web".ToUpper())
                        {
                            //pvMap.Layers[i].LegendText = "Online map";

                            IMapLayer iMapLayer = pvMap.Layers[i];
                            for (int j = i; j > 0; j--)
                            {
                                pvMap.Layers[j] = pvMap.Layers[j - 1];
                            }
                            try
                            {
                                pvMap.Layers[0] = iMapLayer;
                                iMapLayer.LegendText = "XXX";
                            }
                            catch { }
                        }
                    }
                    else
                    {
                        //pvMap.Layers[i].LegendText = "Online mapZZ";
                    }
                }
                pvMap.Invalidate();
            }
            catch { }

        }

        void CheckWorkingPath()
        {
            if (prj.Path == "")
            {
                FolderBrowserDialog folderSel = new FolderBrowserDialog();
                folderSel.Description = "Please select working directory location :";
                folderSel.ShowDialog();
                prj.Path = folderSel.SelectedPath;
                txtWorkingPath.Text = prj.Path;
                cmdSaveConfig.Enabled = true;
            }
        }

        void radioCheck4PolesCreation()
        {
            cmbAlignmentLyr.Enabled = rdoAlignment.Checked;
            cmdNewAligmnentShp.Enabled = rdoAlignment.Checked;
            btnDrawAlignment.Visible = rdoAlignment.Checked;
            //---------------------------------------------------------------
            cmbSolarFarmArea.Enabled = rdoSiteArea.Checked;
            btnDrawArea.Visible = rdoSiteArea.Checked;
            //---------------------------------------------------------------
            btnKML.Visible = rdoKML.Checked;
            btnAddPanel.Visible = rdoKML.Checked;
            //---------------------------------------------------------------
            
            if (rdoKML.Checked == true)
            {
                cmdCreatePvPole.Enabled = false;
            }
            else
            {
                cmdCreatePvPole.Enabled = true;
            }
        }

        public void pvVerify()
        {
            //Reference location
            if (txtLAT.Text != "" & txtLNG.Text != "") verify[0] = true; else verify[0] = false;
            //Building Layer
            if (prj.LyrBuilding != -1)
            {
                if (util.getLayerHdl(prj.LyrBuildingName, pvMap) != -1) verify[1] = true; else verify[1] = false;
            }
            else // doesn't use building layer
            {
                verify[1] = true;
            }
            //Tree Layer
            if (prj.LyrTree != -1)
            {
                if (util.getLayerHdl(prj.LyrTreeName, pvMap) != -1) verify[2] = true; else verify[2] = false;
            }
            else // doesn't use tree layer
            {
                verify[2] = true;
            }
            //Alignment Layer
            //    if (getLayerHdl(cmbAlignmentLyr.Text) != -1) verify[4] = true; else verify[4] = false;
            // MessageBox.Show("XXXX");  
        }

        void checkAlignmentFieldPropeties()
        {
            if (cmbAlignmentLyr.Text != "")
            {
                int alignmentLyr = util.getLayerHdl(cmbAlignmentLyr.Text, pvMap);
                if (alignmentLyr == -1)
                {
                    MessageBox.Show("Cannot open the selected layer");
                    cmbAlignmentLyr.Text = ""; cmbAlignmentLyr.Refresh();
                    verify[4] = false;
                }
                else
                {
                    bool field1Chk = false;
                    IMapFeatureLayer alignmentFs = pvMap.Layers[alignmentLyr] as IMapFeatureLayer;
                    for (int i = 0; i < alignmentFs.DataSet.DataTable.Columns.Count; i++)
                    {
                        if (alignmentFs.DataSet.DataTable.Columns[i].ColumnName == "spacing") field1Chk = true;
                    }
                    if (field1Chk == false)
                    {
                        MessageBox.Show("Error: Layer " + cmbAlignmentLyr.Text + " does not have field [spacing]");
                        cmbAlignmentLyr.Text = ""; cmbAlignmentLyr.Refresh();
                        cmbAlignmentLyr.Text = ""; cmbAlignmentLyr.Refresh();
                        verify[4] = false;
                    }
                    else
                    {
                        verify[4] = true;
                    }
                }
            }
        }

        bool checkBuildingFieldPropeties(bool ShowWarning)
        {
            if (prj.LyrBuildingName != "")
            {
                int bldgLyr = util.getLayerHdl(prj.LyrBuildingName, pvMap);
                if (bldgLyr == -1)
                {
                    if (ShowWarning) MessageBox.Show("Cannot open the selected layer");

                    prj.LyrBuildingName = ""; 
                    return false;
                }
                else
                {
                    bool field1Chk = false;
                    bool field2Chk = false;
                    IMapFeatureLayer bldgFs = pvMap.Layers[bldgLyr] as IMapFeatureLayer;
                    for (int i = 0; i < bldgFs.DataSet.DataTable.Columns.Count; i++)
                    {
                        if (bldgFs.DataSet.DataTable.Columns[i].ColumnName == "Height") field1Chk = true;
                        if (bldgFs.DataSet.DataTable.Columns[i].ColumnName == "Remark") field2Chk = true;
                    }
                    if (ShowWarning)
                    {
                        if (field1Chk == false) MessageBox.Show("Error: Layer " + prj.LyrBuildingName + " does not have field [Height]");
                        if (field2Chk == false) MessageBox.Show("Error: Layer " + prj.LyrBuildingName + " does not have field [Remark]");
                    }
                    if (field1Chk == false | field2Chk == false)
                    {
                        prj.LyrBuildingName = "";
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        bool checkTreeFieldPropeties(bool ShowWarning)
        {
            if (util.getLayerHdl(prj.LyrTreeName, pvMap) != -1)
            {
                int treeLyr = util.getLayerHdl(prj.LyrTreeName, pvMap);
                if (treeLyr == -1)
                {
                    if (ShowWarning) MessageBox.Show("Cannot open the selected layer");
                    prj.LyrTreeName = "";
                    return false;
                }
                else
                {
                    bool field1Chk = false;
                    bool field2Chk = false;
                    bool field3Chk = false;
                    IMapFeatureLayer treeFs = pvMap.Layers[treeLyr] as IMapFeatureLayer;
                    for (int i = 0; i < treeFs.DataSet.DataTable.Columns.Count; i++)
                    {
                        if (treeFs.DataSet.DataTable.Columns[i].ColumnName.ToUpper() == "Diameter".ToUpper()) field1Chk = true;
                        if (treeFs.DataSet.DataTable.Columns[i].ColumnName.ToUpper() == "Height".ToUpper()) field2Chk = true;
                        if (treeFs.DataSet.DataTable.Columns[i].ColumnName.ToUpper() == "Type".ToUpper()) field3Chk = true;
                    }
                    if (ShowWarning)
                    {
                        if (field1Chk == false) MessageBox.Show("Error: Layer " + prj.LyrTreeName + " does not have field [Diameter]");
                        if (field2Chk == false) MessageBox.Show("Error: Layer " + prj.LyrTreeName + " does not have field [Height]");
                        if (field3Chk == false) MessageBox.Show("Error: Layer " + prj.LyrTreeName + " does not have field [Type]");
                    }
                    if (field1Chk == false | field2Chk == false | field3Chk == false)
                    {
                        prj.LyrTreeName = "";
                        
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        bool setCurrrentLayer(string LyrName)
        {
            int i = util.getLayerHdl(LyrName, pvMap);
            if (i == -1)
            {
                MessageBox.Show("Please select layer to assign as current layer first");
                return false;
            }
            else
            {
                for (int j = 0; j < appManager.Legend.RootNodes.Count; j++)
                    foreach (ILegendItem lb in appManager.Legend.RootNodes[j].LegendItems)
                    {
                        if (lb.LegendText == LyrName)
                        {
                            lb.IsSelected = true;
                        }
                        else
                        {
                            lb.IsSelected = false;
                        }
                    }

                pvMap.Layers.SelectedLayer = pvMap.Layers[i];
                pvMap.Layers.SelectLayer(i);// = i;
                //MessageBox.Show(  appManager.Legend.RootNodes.Count.ToString()) ;   
                //MessageBox.Show(appManager.Legend.RootNodes[0].LegendText);
                //MessageBox.Show(appManager.Legend.RootNodes[0].LegendItems.ToString());
                //appManager.Legend.RootNodes[0].LegendItems. =null;
                /**/
                appManager.Legend.RefreshNodes();
                /**/
                return true;
            }
        }

        private void loadLayerList()
        {
            /*
            int LyrPvPoleIndex = util.getLayerHdl(prj.LyrPoleName, pvMap);
            if (LyrPvPoleIndex == -1) { prj.LyrPoleName = ""; }
            else { prj.LyrPole = LyrPvPoleIndex; }

            int LyrPvPanelIndex = util.getLayerHdl(prj.LyrPvPanelName, pvMap);
            if (LyrPvPanelIndex == -1) { prj.LyrPvPanelName = ""; }
            else { prj.LyrPvPanel = LyrPvPanelIndex; }
           */

            //MessageBox.Show("No. of layer" + pvMap.Layers.Count);
            //cmbBldg.Items.Clear();
           // cmbTree.Items.Clear();
            cmbAlignmentLyr.Items.Clear();
            //cmbPolePosition.Items.Clear();
            cmbPanel.Items.Clear();
            cmbSolarFarmArea.Items.Clear();
            cmbDem.Items.Clear();
            cmbRoofTopPanelPanel.Items.Clear();
            cmbRoofTopPanelPosition.Items.Clear();
            //cmbSolarFarmArea.Items.Clear();

            for (int i = 0; i < pvMap.Layers.Count; i++)
            {
                if (pvMap.Layers[i].LegendText != null)
                {
                    //cmbBldg.Items.Add(pvMap.Layers[i].LegendText);
                    //cmbTree.Items.Add(pvMap.Layers[i].LegendText);

                    cmbAlignmentLyr.Items.Add(pvMap.Layers[i].LegendText);
                    //cmbPolePosition.Items.Add(pvMap.Layers[i].LegendText);
                    cmbPanel.Items.Add(pvMap.Layers[i].LegendText);
                    cmbSolarFarmArea.Items.Add(pvMap.Layers[i].LegendText);

                    cmbDem.Items.Add(pvMap.Layers[i].LegendText);
                    cmbRoofTopPanelPanel.Items.Add(pvMap.Layers[i].LegendText);
                    cmbRoofTopPanelPosition.Items.Add(pvMap.Layers[i].LegendText);
                    //cmbSolarFarmArea.Items.Add(pvMap.Layers[i].LegendText);
                }

            }
            cmbState.Items.Clear();
            for (int i = 0; i < listState.Count; i++) // Loop through List with for
            {
                cmbState.Items.Add(listState[i]);
            }
        }

        private void Legend_Activive()
        {
            loadLayerList();

        }

        int RoofAzimuth()
        {
            if (EaveLine.Count >= 2 & RidgeLine.Count >= 2)
            {
                Coordinate p1 = RidgeLine[0];
                Coordinate p2 = RidgeLine[1];
                //Darw edge
                double L1, L2;
                L1 = util.Distance(RidgeLine[0], EaveLine[0]);
                L2 = util.Distance(RidgeLine[0], EaveLine[1]);
                Coordinate intersecPt = new Coordinate();
                Coordinate mid = util.midLine(p1, p2);
                double L = util.FindDistanceToSegment(mid, EaveLine[0], EaveLine[1], out intersecPt);
                double az = util.getAzimuth(mid, intersecPt);
                Coordinate NothLine = new Coordinate(mid.X, mid.Y + 1);
                Int16 dv = Convert.ToInt16(az);
                return dv;
            }
            return -1;
        }

        double BottomDepth()
        {
            if (EaveLine.Count >= 2 & RidgeLine.Count >= 2)
            {
                Coordinate p1 = RidgeLine[0];
                Coordinate p2 = RidgeLine[1];
                //Darw edge
                double L1, L2;
                L1 = util.Distance(RidgeLine[0], EaveLine[0]);
                L2 = util.Distance(RidgeLine[0], EaveLine[1]);
                Coordinate intersecPt = new Coordinate();
                Coordinate mid = util.midLine(p1, p2);
                double L = util.FindDistanceToSegment(mid, EaveLine[0], EaveLine[1], out intersecPt);
                double az = util.getAzimuth(mid, intersecPt);
                Coordinate NothLine = new Coordinate(mid.X, mid.Y + 1);
                Int16 dv = Convert.ToInt16(az);

                return Math.Round(L, 2);
            }
            return 0;
        }

        void UpDateRoofShape()
        {
            imgRoof[0] = new Coordinate(2, 40 + 7 * Py / Px);
            imgRoof[1] = new Coordinate(50, 40 - 40 * Py / Px);
            imgRoof[2] = new Coordinate(98, 40 + 7 * Py / Px);
        }

        #endregion //"Utilities-----------------------------------------------------------------------------------------------"

        #region "Map command-------------------------------------------------------------------------------------------------"


        private void pvMap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            /*   if (FormAddBuilding.Visible == true)
               {
                   mapAct = mapAction.EndBuildingCoord;
               }

               */
        }


        private void pvMap_MouseDown(object sender, MouseEventArgs e)
        {
            
            #region "Add Building Coordinate--------------------------------------------------------"

            if (mapAct == mapAction.BuildingCoord)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (FormAddBuilding.Visible == true)
                    {
                        prj.LyrBuildingName = "Building";
                        if (firstBldgPt == true)
                        {
                            BldgCoords.Clear();
                            firstBldgPt = false;
                        }
                        Coordinate c = new Coordinate();
                        c = pvMap.PixelToProj(e.Location);
                        if (Control.ModifierKeys == Keys.Shift & BldgCoords.Count >= 2)
                        {
                            BldgCoords.Add(new Coordinate(lastCoord));
                        }
                        else
                        {
                            BldgCoords.Add(new Coordinate(c));
                        }
                    }
                }
                if (e.Button == MouseButtons.Right)
                {
                    IFeatureSet fs;
                    int idl = util.getLayerID("Building", pvMap);
                    if (idl == -1) return;
                    setCurrrentLayer("Building");
                    fs = pvMap.Layers[idl].DataSet as IFeatureSet;
                    IPolygon BuildingFe = new DotSpatial.Topology.Polygon(BldgCoords);
                    IFeature ifeaBuilding = fs.AddFeature(BuildingFe);
                    BldgCoords.Clear();
                    firstBldgPt = true;
                    mapAct = mapAction.BuildingCoord;
                    ifeaBuilding.DataRow.BeginEdit();
                    ifeaBuilding.DataRow["Height"] = Convert.ToDouble(FormAddBuilding.txtBuildingHeight.Text);
                    ifeaBuilding.DataRow.EndEdit();
                    fs.Save();
                    util.ClearGraphicMap(pvMap);
                    //-----------------------------------------------------------------------------------------------

                    pvMap.MapFrame.Invalidate();
                }
            }

            if (mapAct == mapAction.AreaCoord)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (FormAreaBoundry.Visible == true)
                    {
                        if (firstAreaPt == true)
                        {
                            AreaCoords.Clear();
                            firstAreaPt = false;
                        }
                        Coordinate c = new Coordinate();
                        c = pvMap.PixelToProj(e.Location);
                        if (Control.ModifierKeys == Keys.Shift & AreaCoords.Count >= 2)
                        {
                            AreaCoords.Add(new Coordinate(lastCoord));
                        }
                        else
                        {
                            AreaCoords.Add(new Coordinate(c));
                        }
                    }
                }
                if (e.Button == MouseButtons.Right)
                {
                    IFeatureSet fs;
                    int idl = util.getLayerID(prj.LyrSiteAreaName, pvMap);
                    if (idl == -1) return;
                    //setCurrrentLayer("Area");
                    fs = pvMap.Layers[idl].DataSet as IFeatureSet;
                    fs.Name = prj.LyrSiteAreaName;
                    prj.LyrSiteArea = util.getLayerHdl(prj.LyrSiteAreaName, pvMap);
                    IPolygon AreaFe = new DotSpatial.Topology.Polygon(AreaCoords);
                    IFeature ifeaArea = fs.AddFeature(AreaFe);
                    AreaCoords.Clear();
                    firstAreaPt = true;
                    mapAct = mapAction.None;
                    // ifeaArea.DataRow.BeginEdit();
                    // ifeaArea.DataRow["Height"] = Convert.ToDouble(FormAddBuilding.txtBuildingHeight.Text);
                    // ifeaArea.DataRow.EndEdit();
                    fs.Save();
                 
                    util.ClearGraphicMap(pvMap);
                    //-----------------------------------------------------------------------------------------------

                    pvMap.MapFrame.Invalidate();
                    CreateGridPole(true,true);
                    CreatePvPanel();
                   // frmAreaBoundry FormAreaBound = new frmAreaBoundry();
                   // FormAreaBound.btnAddAnotherArea.Enabled = true;
                }
            }

            #endregion //"Add Building Coordinate--------------------------------------------------------"

            #region "Add Ridge line--------------------------------------------------------"
            if (mapAct == mapAction.SecondRidgePoint)
            {
                Coordinate c = new Coordinate();
                c = pvMap.PixelToProj(e.Location);
                RidgeLine.Add(new Coordinate(c));
                mapAct = mapAction.None;
            }
            if (mapAct == mapAction.firstRidgePoint)
            {
                RidgeLine.Clear();
                mapAct = mapAction.SecondRidgePoint;
                Coordinate c = new Coordinate();
                c = pvMap.PixelToProj(e.Location);
                RidgeLine.Add(new Coordinate(c));
            }



            #endregion //"Add Ridge line--------------------------------------------------------"

            #region "Add Eave line---------------------------------------------------------"
            if (mapAct == mapAction.SecondEavePoint)
            {
                Coordinate c = new Coordinate();
                c = pvMap.PixelToProj(e.Location);
                EaveLine.Add(new Coordinate(c));
                mapAct = mapAction.None;
                RoofPlane();
            }
            if (mapAct == mapAction.firstEavePoint)
            {
                EaveLine.Clear();
                mapAct = mapAction.SecondEavePoint;
                Coordinate c = new Coordinate();
                c = pvMap.PixelToProj(e.Location);
                EaveLine.Add(new Coordinate(c));
            }
            #endregion //"Add Eave line---------------------------------------------------------"

            #region "ROSE Chart // if (pickRoseLocation == true)"
            if (mapAct == mapAction.pickRoseLocation)
            {
                appManager.ProgressHandler.Progress("Pick the reference location on map", 0, "Pick the reference location on map");

                Pen MyPen = new Pen(Color.Black);
                Coordinate c = new Coordinate();
                c = pvMap.PixelToProj(e.Location);
                Extent ext = pvMap.ViewExtents;
                double dx = (ext.MaxX - ext.MinX);
                double dy = (ext.MaxX - ext.MinX);
                ext.SetCenter(c);
                pvMap.ViewExtents = ext;// layer.DataSet.Extent;

                //pvMap.ViewExtents.SetCenter(c, 100, 100); 
                //-----------------------------------------------------------------------
                double[] latlong = new double[] { c.X, c.Y };
                Reproject.ReprojectPoints(latlong, new double[] { 0 }, pvMap.Projection, KnownCoordinateSystems.Geographic.World.WGS1984, 0, 1);
                txtLNG.Text = latlong[0].ToString();
                txtLAT.Text = latlong[1].ToString();
                double minDist = 1000000.00;

                for (int i = 0; i < nSta; i++)
                {
                    double rr;
                    rr = Math.Sqrt(Math.Pow((latlong[0] - wSta[i].LONG2), 2) + Math.Pow((latlong[1] - wSta[i].LAT2), 2));
                    wStaDistance[i] = rr;
                    wStaSel[i] = i;
                    if (rr < minDist)
                    {
                        minDist = rr;
                        cmbState.Text = wSta[i].State;
                        cmbCity.Text = wSta[i].City;
                    }
                }

                double temp;
                int temp1;
                for (int i = (nSta - 2); i >= 0; i--)
                {
                    for (int j = 1; j <= i; j++)
                    {
                        if (wStaDistance[j - 1] > wStaDistance[j])
                        {
                            temp = wStaDistance[j - 1];
                            wStaDistance[j - 1] = wStaDistance[j];
                            wStaDistance[j] = temp;
                            temp1 = wStaSel[j - 1];
                            wStaSel[j - 1] = wStaSel[j];
                            wStaSel[j] = temp1;
                        }
                    }
                }

                //-----------------------------------------------------------------------
                double radius = 1000;
                txtUtmN.Text = c.Y.ToString();
                txtUtmE.Text = c.X.ToString();
                pvMap.MapFrame.DrawingLayers.Clear();
                util.kDrawCircle(c.X, c.Y, radius, 360, pvMap, Color.Magenta);
                //MessageBox.Show(c.X + "," + c.Y);
                Double r = 0.25;
                int nIDW = Convert.ToInt16(txtNIdwSta.Text);
                util.kDrawCircle(c.X, c.Y, r, 360, pvMap, Color.Magenta);
                for (int ii = 0; ii < nIDW; ii++)
                {
                    double lat = wSta[wStaSel[ii]].LAT2;
                    double lng = wSta[wStaSel[ii]].LONG2;
                    double x = 0;
                    double y = 0;
                    double[] mapCoordinate = new double[] { lng, lat };
                    Reproject.ReprojectPoints(mapCoordinate, new double[] { 0 }, KnownCoordinateSystems.Geographic.World.WGS1984, pvMap.Projection, 0, 1);
                    Coordinate coord = util.circleCoord(c.X, c.Y, mapCoordinate[0], mapCoordinate[1], radius); //returns coordinate of point on inner circle
                    Coordinate coord2 = util.circleCoord(mapCoordinate[0], mapCoordinate[1], c.X, c.Y, radius); //returns coordinates of point on outer circle

                    util.kDrawCircle(mapCoordinate[0], mapCoordinate[1], radius, 360, pvMap, Color.Magenta);
                    util.DrawLine(coord2.X, coord2.Y, coord.X, coord.Y, 2, Color.Magenta, pvMap);
                }

                mapAct = mapAction.None;
                try
                {
                    IFeatureSet fea = FeatureSet.Open(pvDir + "\\timezone\\world_timezones.shp"); // = ((IFeatureLayer)pvMap.GetLayers().ToArray()[2]).DataSet;
                    pvProgressbar.Minimum = 0;
                    pvProgressbar.Maximum = fea.NumRows();
                    pvProgressbar.Visible = true;
                    pvProgressbar.Value = 0;
                    foreach (var item in fea.Features)
                    {
                        pvProgressbar.PerformStep();
                        if (item.Contains(new DotSpatial.Topology.Point(latlong[0], latlong[1])))
                        {
                            string tzs = item.DataRow["UTC_OFFSET"].ToString();
                            string[] tz = tzs.Split(':');
                            txtTimeZone.Text = tz[0].Substring(tz[0].Length - 3); //value.Substring(value.Length - length) 
                            break;
                        }
                        pvProgressbar.Refresh();
                    }
                }
                catch { }
                pvProgressbar.Visible = false;
                //-----------------------------------------------------------------------
                // update project data
                //-----------------------------------------------------------------------
                prj.UtmE = c.X;
                prj.UtmN = c.Y;
                prj.Latiude = latlong[1];
                prj.Longtitude = latlong[0];
                prj.TimeZome = Convert.ToInt16(txtTimeZone.Text);
                lblTab02.Enabled = true;
                cmdZoomToSite.Enabled = true;
                prj.Verify[0] = true;
                cmdSaveConfig.Enabled = true;
                //zoom to site
                Envelope env = new Envelope();
                env.SetExtents(prj.UtmE - 1000, prj.UtmN - 1000, prj.UtmE + 1000, prj.UtmN + 1000);
                pvMap.ViewExtents = env.ToExtent();

                //Draw Rose diagram
                DrawRoseDiagram();
                //-----------------------------------------------------------------------                
            }

            #endregion

            if (EaveLine.Count == 2 & RidgeLine.Count == 2)
            { cmdRoofPlane.Enabled = true; }
            else
            { cmdRoofPlane.Enabled = false; }

            #region "Add Alingment--------------------------------------------------------"
            if (mapAct == mapAction.AlignmentCoord2)
            {  
                prj.LyrAlignment = util.getLayerHdl(prj.LyrAlignmentName, pvMap);           
                IFeatureSet fs;
                int idl = util.getLayerID(prj.LyrAlignmentName, pvMap);
                if (idl == -1) return;
                fs = pvMap.Layers[idl].DataSet as IFeatureSet;
                fs.Name = prj.LyrAlignmentName;               
                Coordinate c = new Coordinate();
                c = pvMap.PixelToProj(e.Location);
                Alignment.Add(new Coordinate(c));
                ILineString LineFe = new DotSpatial.Topology.LineString(Alignment);
                IFeature ifeaLine = fs.AddFeature(LineFe);                
                //firstAreaPt = true;               
                fs.Features.Add(ifeaLine);
                fs.Save();
                mapAct = mapAction.None;
                Alignment.Clear();                                                                
            } 

            if (mapAct == mapAction.AlignmentCoord1)
            {                                
                Coordinate c = new Coordinate();
                c = pvMap.PixelToProj(e.Location);
                Alignment.Add(new Coordinate(c));
                mapAct = mapAction.AlignmentCoord2;
            }


            #endregion //"Add Alignment--------------------------------------------------------"
        }

        private void pvMap_Load(object sender, EventArgs e)
        {

        }

        private void pvMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (mapAct == mapAction.BuildingCoord)
            {
                if (firstBldgPt == false)
                {
                    util.ClearGraphicMap(pvMap);
                    double x1 = 0;
                    double y1 = 0;
                    double x2 = 0;
                    double y2 = 0;
                    bool piza = false;
                    foreach (Coordinate c in BldgCoords)
                    {
                        x1 = x2;
                        y1 = y2;
                        x2 = c.X;
                        y2 = c.Y;
                        if (piza == true) util.DrawLine(x1, y1, x2, y2, 1, Color.Magenta, pvMap);
                        piza = true;
                    }
                    if (piza == true)
                    {
                        System.Drawing.Point pt = new System.Drawing.Point(e.X, e.Y);
                        Coordinate m = pvMap.PixelToProj(pt);
                        Coordinate p1 = new Coordinate(x1, y1);
                        Coordinate p2 = new Coordinate(x2, y2);
                        double r = Math.Sqrt(Math.Pow((m.X - x2), 2) + Math.Pow((m.Y - y2), 2));
                        double L;
                        if (Control.ModifierKeys == Keys.Shift & BldgCoords.Count >= 2)
                        {
                            Coordinate mm = util.getPerpend(m, p1, p2, out L);
                            double dx = p2.X - mm.X;
                            double dy = p2.Y - mm.Y;
                            util.kDrawCircle(p2, L, 36, pvMap, Color.Yellow);
                            util.DrawLine(p2.X, p2.Y, m.X + dx, m.Y + dy, 1, Color.Magenta, pvMap);
                            lastCoord = new Coordinate(m.X + dx, m.Y + dy);
                            appManager.ProgressHandler.Progress("Length(m) ", 0, "Length(m): " + L.ToString());
                        }
                        else
                        {
                            util.DrawLine(m.X, m.Y, x2, y2, 1, Color.Magenta, pvMap);
                            appManager.ProgressHandler.Progress("Length(m) ", 0, "Length(m): " + r.ToString());
                        }
                    }
                }
            }

            if (mapAct == mapAction.AreaCoord)
            {
                if (firstAreaPt == false)
                {
                    util.ClearGraphicMap(pvMap);
                    double x1 = 0;
                    double y1 = 0;
                    double x2 = 0;
                    double y2 = 0;
                    bool piza = false;
                    foreach (Coordinate c in AreaCoords)
                    {
                        x1 = x2;
                        y1 = y2;
                        x2 = c.X;
                        y2 = c.Y;
                        if (piza == true) util.DrawLine(x1, y1, x2, y2, 3, Color.Magenta, pvMap);
                        piza = true;
                    }
                    if (piza == true)
                    {
                        System.Drawing.Point pt = new System.Drawing.Point(e.X, e.Y);
                        Coordinate m = pvMap.PixelToProj(pt);
                        Coordinate p1 = new Coordinate(x1, y1);
                        Coordinate p2 = new Coordinate(x2, y2);
                        double r = Math.Sqrt(Math.Pow((m.X - x2), 2) + Math.Pow((m.Y - y2), 2));
                        double L;
                        if (Control.ModifierKeys == Keys.Shift & AreaCoords.Count >= 2)
                        {
                            Coordinate mm = util.getPerpend(m, p1, p2, out L);
                            double dx = p2.X - mm.X;
                            double dy = p2.Y - mm.Y;
                            util.kDrawCircle(p2, L, 36, pvMap, Color.Yellow);
                            util.DrawLine(p2.X, p2.Y, m.X + dx, m.Y + dy, 2, Color.Magenta, pvMap);
                            lastCoord = new Coordinate(m.X + dx, m.Y + dy);
                            appManager.ProgressHandler.Progress("Length(m) ", 0, "Length(m): " + L.ToString());
                        }
                        else
                        {
                            util.DrawLine(m.X, m.Y, x2, y2, 2, Color.Magenta, pvMap);
                            appManager.ProgressHandler.Progress("Length(m) ", 0, "Length(m): " + r.ToString());
                        }
                    }
                }
            }

            // send mouse position to panel
            splitContainer1_Panel2_MouseMove(sender, e);
            //------------------------------------------------------------

            if (mapAct == mapAction.SecondRidgePoint)
            {
                Coordinate c = new Coordinate();
                c = pvMap.PixelToProj(e.Location);
                util.ClearGraphicMap(pvMap);
                if (EaveLine.Count == 2)
                {
                    util.DrawLine(EaveLine[0], EaveLine[1], 1, Color.Magenta, pvMap);
                }
                util.DrawLine(RidgeLine[0], c, 1, Color.Red, pvMap);
            }
            if (mapAct == mapAction.SecondEavePoint)
            {
                Coordinate c = new Coordinate();
                c = pvMap.PixelToProj(e.Location);
                util.ClearGraphicMap(pvMap);
                if (RidgeLine.Count == 2)
                {
                    util.DrawLine(RidgeLine[0], RidgeLine[1], 1, Color.Red, pvMap);
                }
                util.DrawLine(EaveLine[0], c, 1, Color.Magenta, pvMap);
            }
            if (FormAddTree != null)
            {
                if (FormAddTree.Visible == true)
                { pvMap.FunctionMode = FunctionMode.Select; }
            }
            if (mapAct == mapAction.AlignmentCoord2)
            {
                Coordinate c = new Coordinate();
                c = pvMap.PixelToProj(e.Location);
                util.ClearGraphicMap(pvMap);             
                util.DrawLine(Alignment[0], c, 2, Color.Aquamarine, pvMap);
            }
        }

        private void pvMap_LayerAdded(object sender, LayerEventArgs e)
        {
            loadLayerList();
        }

        #endregion //"Map command--------------------------------------------------------------------------------------------"

        #region SketchUp



        string GeoTree4SketchUp(double radius, double scaleZ, double shiftX, double shiftY, double shiftZ)
        {
            double[] xPt = new double[24];
            double[] yPt = new double[24];
            double[] zPt = new double[24];
            xPt[0] = -149.828; yPt[0] = -48.492; zPt[0] = 0;
            xPt[1] = -154.001; yPt[1] = 32.919; zPt[1] = 0;
            xPt[2] = -157.274; yPt[2] = -8.062; zPt[2] = 0;
            xPt[3] = -140.234; yPt[3] = 71.655; zPt[3] = 0;
            xPt[4] = -132.172; yPt[4] = -85.618; zPt[4] = 0;
            xPt[5] = -116.91; yPt[5] = 105.509; zPt[5] = 0;
            xPt[6] = -105.509; yPt[6] = -116.91; zPt[6] = 0;
            xPt[7] = -85.618; yPt[7] = 132.172; zPt[7] = 0;
            xPt[8] = -71.655; yPt[8] = -140.234; zPt[8] = 0;
            xPt[9] = -48.492; yPt[9] = 149.828; zPt[9] = 0;
            xPt[10] = -32.919; yPt[10] = -154.001; zPt[10] = 0;
            xPt[11] = -8.062; yPt[11] = 157.274; zPt[11] = 0;
            xPt[12] = 8.062; yPt[12] = -157.274; zPt[12] = 0;
            xPt[13] = 32.919; yPt[13] = 154.001; zPt[13] = 0;
            xPt[14] = 48.492; yPt[14] = -149.828; zPt[14] = 0;
            xPt[15] = 71.655; yPt[15] = 140.234; zPt[15] = 0;
            xPt[16] = 85.618; yPt[16] = -132.172; zPt[16] = 0;
            xPt[17] = 105.509; yPt[17] = 116.91; zPt[17] = 0;
            xPt[18] = 116.91; yPt[18] = -105.509; zPt[18] = 0;
            xPt[19] = 132.172; yPt[19] = 85.618; zPt[19] = 0;
            xPt[20] = 140.234; yPt[20] = -71.655; zPt[20] = 0;
            xPt[21] = 149.828; yPt[21] = 48.492; zPt[21] = 0;
            xPt[22] = 154.001; yPt[22] = -32.919; zPt[22] = 0;
            xPt[23] = 157.274; yPt[23] = 8.062; zPt[23] = 0;
            string Cord72 = "";
            double x, y, z;
            for (int i = 0; i <= 23; i++)
            {
                x = xPt[i] * radius + shiftX;
                y = yPt[i] * radius + shiftY;
                z = zPt[i] * scaleZ + shiftZ;
                if (i == 23)
                {
                    Cord72 = Cord72 + x + " " + y + " " + z;
                }
                else
                {
                    Cord72 = Cord72 + x + " " + y + " " + z + " ";
                }
            }

            return Cord72;
        }

        private void Export2SketchUp4Tree(IMapFeatureLayer treeFe, string exportfolder)
        {
            IMapRasterLayer demLyr;
            Raster dem4Pv = new Raster();
            double z0 = 0;
            int iDemLyr = util.getLayerHdl(prj.LyrDEMname, pvMap);
            if (iDemLyr != -1 &  prj.DemChecked == true)
            {
                demLyr = pvMap.Layers[iDemLyr] as IMapRasterLayer;
                int mRow = demLyr.Bounds.NumRows;
                int mCol = demLyr.Bounds.NumColumns;
                dem4Pv = (Raster)demLyr.DataSet;
                Coordinate ptReference = new Coordinate(Convert.ToDouble(txtUtmE.Text), Convert.ToDouble(txtUtmN.Text));
                RcIndex rc = dem4Pv.ProjToCell(ptReference);
                //double z0 = demLyr.DataSet.Value[rc.Row, rc.Column];
                if (rc.Column < 0 | rc.Row < 0)
                {
                    z0 = 0;
                }
                else
                {
                    z0 = dem4Pv.Value[rc.Row, rc.Column];
                }
            }

            int nShp = treeFe.DataSet.NumRows() - 1;
            //Extent ext = treeFe.DataSet.GetFeature(nShp).Envelope.ToExtent();
            /*
            for (int i = 0; i < treeFe.DataSet.NumRows(); i++)
            {
                IFeature fs = treeFe.DataSet.GetFeature(i);
                double x1 = fs.Coordinates[0].X;
                double y1 = fs.Coordinates[0].Y;
            }
             */
            int nTree = treeFe.DataSet.NumRows() - 1;
            //
            //int nNode = 40;
            int i = 0;
            //double y0 = 10;
            //double x0 = 0;
            double ii = 0;
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            //object obj;
            //XmlWriter writer = XmlWriter.Create(pvDir + "\\SketchUp\\temp.xml", settings); temp_path
            XmlWriter writer = XmlWriter.Create(temp_path + "\\temp.xml", settings);

            //-------------------------------------------------------
            //Header
            //-------------------------------------------------------
            writer.WriteStartElement("COLLADA");
            {
                writer.WriteAttributeString("xmlns", "xsi", null, "http://www.collada.org/2005/11/COLLADASchema");
                writer.WriteAttributeString("version", "1.4.1");
                writer.WriteStartElement("asset");
                {
                    writer.WriteStartElement("contributor");
                    {
                        writer.WriteStartElement("authoring_tool");
                        {
                            writer.WriteString("SketchUp 8.0.16846");
                        } writer.WriteEndElement(); //"authoring_tool"
                    } writer.WriteEndElement(); //"contributor"
                    writer.WriteStartElement("created");
                    {
                        writer.WriteString("2013-07-22T15:46:40Z");
                    } writer.WriteEndElement(); //"created"                    
                    writer.WriteStartElement("modified");
                    {
                        writer.WriteString("2013-07-22T15:46:40Z");
                    } writer.WriteEndElement(); //"modified"
                    writer.WriteStartElement("unit");
                    {
                        writer.WriteAttributeString("meter", "0.2539999969303608");
                        writer.WriteAttributeString("name", "inch");
                    } writer.WriteEndElement(); //"unit"
                    writer.WriteStartElement("up_axis");
                    {
                        writer.WriteString("Z_UP");
                    } writer.WriteEndElement(); //"up_axis"                    
                } writer.WriteEndElement(); //"asset"
                writer.WriteStartElement("library_visual_scenes");
                {
                    writer.WriteStartElement("visual_scene");
                    {
                        writer.WriteAttributeString("id", "ID1");
                        //--------------------------------------------------------------------------
                        // Node
                        //--------------------------------------------------------------------------
                        writer.WriteStartElement("node");
                        {
                            writer.WriteAttributeString("name", "SketchUp");
                            //Number of trees
                            i = 0;
                            //for (int t = 0; t < nTree; t++)
                            for (int t = 0; t < treeFe.DataSet.NumRows(); t++)
                            {
                                for (int iTree = 1; iTree <= 40; iTree++)
                                {
                                    i++;// = iTree;
                                    writer.WriteStartElement("instance_geometry");
                                    {
                                        writer.WriteAttributeString("url", "#ID" + getSketchUpId(i).ToString());
                                        writer.WriteStartElement("bind_material");
                                        {
                                            writer.WriteStartElement("technique_common");
                                            {
                                                writer.WriteStartElement("instance_material");
                                                {
                                                    writer.WriteAttributeString("symbol", "Material2");
                                                    writer.WriteAttributeString("target", "#ID3");
                                                    writer.WriteStartElement("bind_vertex_input");
                                                    {
                                                        writer.WriteAttributeString("semantic", "UVSET0");
                                                        writer.WriteAttributeString("input_semantic", "TEXCOORD");
                                                        writer.WriteAttributeString("input_set", "0");
                                                    } writer.WriteEndElement();
                                                } writer.WriteEndElement();
                                            } writer.WriteEndElement();
                                        } writer.WriteEndElement();
                                    } writer.WriteEndElement();
                                }
                            }
                        } writer.WriteEndElement();//"Node"
                        //--------------------------------------------------------------------------
                    } writer.WriteEndElement(); //"visual_scene"
                } writer.WriteEndElement(); //"library_visual_scenes"


                //----------------------------------------------------------------------------------
                // BEGIN Geometry
                //----------------------------------------------------------------------------------
                writer.WriteStartElement("library_geometries");
                //-----------------------------------------------------------------------------------
                //-----------------------------------------------------------------------------------
                i = 0;
                //for (int t = 0; t < nTree; t++)
                double ToMeter = 3.96874995203687; // Convert To Meter for sketchUp;
                for (int t = 0; t < treeFe.DataSet.NumRows(); t++)
                {
                    IFeature fs = treeFe.DataSet.GetFeature(t);
                    double x0 = prj.UtmE;// Convert.ToDouble(txtUtmE.Text);
                    double y0 = prj.UtmN;// Convert.ToDouble(txtUtmN.Text);


                    double x1 = (fs.Coordinates[0].X - x0) * ToMeter;
                    double y1 = (fs.Coordinates[0].Y - y0) * ToMeter;
                    object objDiameter = fs.DataRow["diameter"];
                    object objHeight = fs.DataRow["height"];
                    object objType = fs.DataRow["type"];
                    double treeRadius = (double)objDiameter / 2;
                    double treeHeight = (double)objHeight;
                    int treeTypeId = getTreeTypeId((string)objType);

                    for (int iTree = 1; iTree <= 40; iTree++)
                    {
                        i++;// = iTree;
                        writer.WriteStartElement("geometry");
                        {
                            writer.WriteAttributeString("id", "ID" + getSketchUpId(i).ToString());
                            writer.WriteStartElement("mesh");
                            {
                                //--------
                                //1st Set
                                //--------
                                writer.WriteStartElement("source");
                                {
                                    writer.WriteAttributeString("id", "ID" + getSketchUpSourceId(i).ToString());
                                    writer.WriteStartElement("float_array");
                                    {
                                        writer.WriteAttributeString("id", "ID" + (getSketchUpSourceId(i) + 3).ToString());
                                        writer.WriteAttributeString("count", "72");
                                        //writer.WriteString(get3Dcord(i));
                                        //TreeRadius[t] = 10;
                                        //TreeHeight[t] = 15;
                                        //treeForm[t] = 1;
                                        //treeE[t] = 15;
                                        //treeN[t] = 0;
                                        double radius = treeRadius; //TreeRadius[t];
                                        int tForm = treeTypeId;// treeForm[t];
                                        double H = treeHeight;
                                        double r = treeRedius((double)iTree / 40, tForm) * radius;
                                        double z = 0;
                                        if (iDemLyr != -1 & prj.DemChecked == true)
                                        {
                                            Coordinate ptReference = new Coordinate(fs.Coordinates[0].X, fs.Coordinates[0].Y);
                                            RcIndex rc = dem4Pv.ProjToCell(ptReference);
                                            if (rc.Column < 0 | rc.Row < 0)
                                            {
                                                z = z0;
                                            }
                                            else
                                            {
                                                //double z0 = demLyr.DataSet.Value[rc.Row, rc.Column];
                                                z = dem4Pv.Value[rc.Row, rc.Column] - z0;
                                            }
                                        }
                                        writer.WriteString(GeoTree4SketchUp(r, H * ToMeter, x1, y1, z + H * (iTree - 1) / 40 * (3.96874995203687)));

                                    } writer.WriteEndElement();//"float_array"    

                                    writer.WriteStartElement("technique_common");
                                    {
                                        writer.WriteStartElement("accessor");
                                        {// <accessor count="24" source="#ID8" stride="3">
                                            writer.WriteAttributeString("count", "24");
                                            writer.WriteAttributeString("source", "#ID" + (getSketchUpSourceId(i) + 3).ToString());
                                            writer.WriteAttributeString("stride", "3");
                                            //<param name="X" type="float" />
                                            writer.WriteStartElement("param");
                                            {
                                                writer.WriteAttributeString("name", "X");
                                                writer.WriteAttributeString("type", "float");
                                            } writer.WriteEndElement();//"param"
                                            writer.WriteStartElement("param");
                                            {
                                                writer.WriteAttributeString("name", "Y");
                                                writer.WriteAttributeString("type", "float");
                                            } writer.WriteEndElement();//"param"
                                            writer.WriteStartElement("param");
                                            {
                                                writer.WriteAttributeString("name", "Z");
                                                writer.WriteAttributeString("type", "float");
                                            } writer.WriteEndElement(); //"param"
                                        } writer.WriteEndElement(); //"accessor"
                                    } writer.WriteEndElement(); //"technique_common"
                                } writer.WriteEndElement();//"source"
                                //--------
                                //2nd Set
                                //--------
                                writer.WriteStartElement("source");
                                {
                                    writer.WriteAttributeString("id", "ID" + (getSketchUpSourceId(i) + 1).ToString());
                                    writer.WriteStartElement("float_array");
                                    {
                                        writer.WriteAttributeString("id", "ID" + (getSketchUpSourceId(i) + 4).ToString());
                                        writer.WriteAttributeString("count", "72");
                                        writer.WriteString("0 0 1 0 0 1 0 0 1 0 0 1 0 0 1 0 0 1 0 0 1 0 0 1 0 0 1 0 0 1 0 0 1 0 0 1 0 0 1 0 0 1 0 0 1 0 0 1 0 0 1 0 0 1 0 0 1 0 0 1 0 0 1 0 0 1 0 0 1 0 0 1");
                                        //writer.WriteString(get3DCam(1));
                                    } writer.WriteEndElement();//"float_array"    

                                    writer.WriteStartElement("technique_common");
                                    {
                                        writer.WriteStartElement("accessor");
                                        {// <accessor count="24" source="#ID8" stride="3">
                                            writer.WriteAttributeString("count", "24");
                                            writer.WriteAttributeString("source", "#ID" + (getSketchUpSourceId(i) + 4).ToString());
                                            writer.WriteAttributeString("stride", "3");
                                            //<param name="X" type="float" />
                                            writer.WriteStartElement("param");
                                            {
                                                writer.WriteAttributeString("name", "X");
                                                writer.WriteAttributeString("type", "float");
                                            } writer.WriteEndElement();//"param"
                                            writer.WriteStartElement("param");
                                            {
                                                writer.WriteAttributeString("name", "Y");
                                                writer.WriteAttributeString("type", "float");
                                            } writer.WriteEndElement();//"param"
                                            writer.WriteStartElement("param");
                                            {
                                                writer.WriteAttributeString("name", "Z");
                                                writer.WriteAttributeString("type", "float");
                                            } writer.WriteEndElement(); //"param"
                                        } writer.WriteEndElement(); //"accessor"
                                    } writer.WriteEndElement(); //"technique_common"
                                } writer.WriteEndElement();//"source"
                                //--------
                                //3rd Set
                                //--------
                                writer.WriteStartElement("vertices");
                                {
                                    writer.WriteAttributeString("id", "ID" + (getSketchUpSourceId(i) + 2).ToString());
                                    writer.WriteStartElement("input");
                                    {// <input semantic="POSITION" source="#ID11" />
                                        writer.WriteAttributeString("semantic", "POSITION");
                                        writer.WriteAttributeString("source", "#ID" + (getSketchUpSourceId(i) + 0).ToString());
                                    } writer.WriteEndElement(); //"input"
                                    writer.WriteStartElement("input");
                                    {// <input semantic="NORMAL" source="#ID12" />
                                        writer.WriteAttributeString("semantic", "NORMAL");
                                        writer.WriteAttributeString("source", "#ID" + (getSketchUpSourceId(i) + 1).ToString());
                                    } writer.WriteEndElement(); //"input"
                                } writer.WriteEndElement();//"vertices"
                                writer.WriteStartElement("triangles");
                                {//<triangles count="12" material="Material2">
                                    writer.WriteAttributeString("count", "22");
                                    writer.WriteAttributeString("material", "Material2");
                                    writer.WriteStartElement("input");
                                    {//<input offset="0" semantic="VERTEX" source="#ID13" />
                                        writer.WriteAttributeString("offset", "0");
                                        writer.WriteAttributeString("semantic", "VERTEX");
                                        writer.WriteAttributeString("source", "#ID" + (getSketchUpSourceId(i) + 2).ToString());
                                    } writer.WriteEndElement(); //"input"
                                    writer.WriteStartElement("p");
                                    {//
                                        writer.WriteString("0 1 2 1 0 3 3 0 4 3 4 5 5 4 6 5 6 7 7 6 8 7 8 9 9 8 10 9 10 11 11 10 12 11 12 13 13 12 14 13 14 15 15 14 16 15 16 17 17 16 18 17 18 19 19 18 20 19 20 21 21 20 22 21 22 23");
                                    } writer.WriteEndElement(); //"p"

                                } writer.WriteEndElement();//"triangles"                        
                            } writer.WriteEndElement();//"mesh"
                        } writer.WriteEndElement(); //"geometry"
                    }
                } writer.WriteEndElement();//library_geometries
                //-----------------------------------------------------------------------------------
                // END GEOMETRY
                //-----------------------------------------------------------------------------------
                //-----------------------------------------------------------------------------------


                //----------------------------------------------------------------------------------
                //Footer
                //----------------------------------------------------------------------------------
                writer.WriteStartElement("library_materials");
                {
                    writer.WriteStartElement("material");
                    {
                        writer.WriteAttributeString("id", "ID3");
                        writer.WriteAttributeString("name", "__auto_");
                        writer.WriteStartElement("instance_effect");
                        {
                            writer.WriteAttributeString("url", "#ID4");
                        } writer.WriteEndElement(); //"instance_effect"
                    } writer.WriteEndElement(); //"material"
                } writer.WriteEndElement(); //"library_materials"

                writer.WriteStartElement("library_effects");
                {
                    writer.WriteStartElement("effect");
                    {
                        writer.WriteAttributeString("id", "ID4");
                        writer.WriteStartElement("profile_COMMON");
                        {
                            writer.WriteStartElement("technique");
                            {
                                writer.WriteAttributeString("sid", "COMMON");
                                writer.WriteStartElement("lambert");
                                {
                                    writer.WriteStartElement("diffuse");
                                    {
                                        writer.WriteStartElement("color");
                                        {
                                            writer.WriteString("1 1 1 1");

                                        } writer.WriteEndElement(); //"color"   
                                    } writer.WriteEndElement(); //"diffuse"   
                                } writer.WriteEndElement(); //"lambert"      
                            } writer.WriteEndElement(); //"technique"      
                        } writer.WriteEndElement(); //"profile_COMMON"       
                    } writer.WriteEndElement(); //"effect"                
                } writer.WriteEndElement(); //"library_effects"

                writer.WriteStartElement("scene");
                {
                    writer.WriteStartElement("instance_visual_scene");
                    {
                        writer.WriteAttributeString("url", "#ID1");

                    } writer.WriteEndElement(); //"instance_visual_scene"   
                } writer.WriteEndElement(); //"scene"   
            } writer.WriteEndElement(); //"COLLADA"
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();



            String line;
            try
            {
                StreamReader sh = new StreamReader(pvDir + "\\SketchUp\\COLLADASH.h");
                //StreamReader sr = new StreamReader(pvDir + "\\SketchUp\\temp.xml");
                StreamReader sr = new StreamReader(temp_path + "\\temp.xml");
                StreamWriter sw = new StreamWriter(exportfolder + "\\treeModel.dae");
                line = sh.ReadLine(); sw.WriteLine(line); line = sr.ReadLine();
                line = sh.ReadLine(); sw.WriteLine(line); line = sr.ReadLine();
                line = sr.ReadLine();
                while (line != null)
                {
                    sw.WriteLine(line);
                    line = sr.ReadLine();
                }

                //close the file
                sh.Close();
                sr.Close();
                sw.Close();
                //MessageBox.Show("ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }

        }

        private void Export2SketchUp4PvPanel(IMapFeatureLayer poleFe, string exportfolder)
        {

            int nShp = poleFe.DataSet.NumRows() - 1;
            //Extent ext = poleFe.DataSet.GetFeature(nShp).Envelope.ToExtent();
            // for (int i = 0; i < poleFe.DataSet.NumRows(); i++)
            //{
            //IFeature fs = poleFe.DataSet.GetFeature(i);
            //double x1 = fs.Coordinates[0].X - Convert.ToDouble(txtUtmE.Text);
            //double y1 = fs.Coordinates[0].Y - Convert.ToDouble(txtUtmN.Text);
            //}
            int nNode = poleFe.DataSet.NumRows() - 1;
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            //object obj;
            //
            //XmlWriter writer = XmlWriter.Create(pvDir + "\\SketchUp\\temp.xml", settings);
            XmlWriter writer = XmlWriter.Create(temp_path + "\\temp.xml", settings);
            //-------------------------------------------------------
            //Header
            //-------------------------------------------------------
            writer.WriteStartElement("COLLADA");
            {
                writer.WriteAttributeString("xmlns", "xsi", null, "http://www.collada.org/2005/11/COLLADASchema");
                writer.WriteAttributeString("version", "1.4.1");
                writer.WriteStartElement("asset");
                {
                    writer.WriteStartElement("contributor");
                    {
                        writer.WriteStartElement("authoring_tool");
                        {
                            writer.WriteString("SketchUp 8.0.16846");
                        } writer.WriteEndElement(); //"authoring_tool"
                    } writer.WriteEndElement(); //"contributor"
                    writer.WriteStartElement("created");
                    {
                        writer.WriteString("2013-07-22T15:46:40Z");
                    } writer.WriteEndElement(); //"created"                    
                    writer.WriteStartElement("modified");
                    {
                        writer.WriteString("2013-07-22T15:46:40Z");
                    } writer.WriteEndElement(); //"modified"
                    writer.WriteStartElement("unit");
                    {
                        writer.WriteAttributeString("meter", "0.2539999969303608");
                        writer.WriteAttributeString("name", "inch");
                    } writer.WriteEndElement(); //"unit"
                    writer.WriteStartElement("up_axis");
                    {
                        writer.WriteString("Z_UP");
                    } writer.WriteEndElement(); //"up_axis"                    
                } writer.WriteEndElement(); //"asset"
                writer.WriteStartElement("library_visual_scenes");
                {
                    writer.WriteStartElement("visual_scene");
                    {
                        writer.WriteAttributeString("id", "ID1");
                        //--------------------------------------------------------------------------
                        // Node
                        //--------------------------------------------------------------------------
                        writer.WriteStartElement("node");
                        {
                            writer.WriteAttributeString("name", "SketchUp");
                            for (int i = 0; i < poleFe.DataSet.NumRows(); i++)
                            {
                                writer.WriteStartElement("instance_geometry");
                                {
                                    writer.WriteAttributeString("url", "#ID" + getSketchUpId(i).ToString());
                                    writer.WriteStartElement("bind_material");
                                    {
                                        writer.WriteStartElement("technique_common");
                                        {
                                            writer.WriteStartElement("instance_material");
                                            {
                                                writer.WriteAttributeString("symbol", "Material2");
                                                writer.WriteAttributeString("target", "#ID3");
                                                writer.WriteStartElement("bind_vertex_input");
                                                {
                                                    writer.WriteAttributeString("semantic", "UVSET0");
                                                    writer.WriteAttributeString("input_semantic", "TEXCOORD");
                                                    writer.WriteAttributeString("input_set", "0");
                                                } writer.WriteEndElement();
                                            } writer.WriteEndElement();
                                        } writer.WriteEndElement();
                                    } writer.WriteEndElement();
                                } writer.WriteEndElement();
                            }
                        } writer.WriteEndElement();//"Node"
                        //--------------------------------------------------------------------------
                    } writer.WriteEndElement(); //"visual_scene"
                } writer.WriteEndElement(); //"library_visual_scenes"
                //----------------------------------------------------------------------------------
                // Geometry
                //----------------------------------------------------------------------------------
                writer.WriteStartElement("library_geometries");
                double ToMeter = 3.96874995203687;


                //---------------------------------------------------------------
                //---------------------------------------------------------------
                //---------------------------------------------------------------


                //---------------------------------------------------------------
                //---------------------------------------------------------------
                //---------------------------------------------------------------

                double xx0 = prj.UtmE; // Convert.ToDouble(txtUtmE.Text);
                double yy0 = prj.UtmN; // Convert.ToDouble(txtUtmN.Text);
                for (int i = 0; i < poleFe.DataSet.NumRows(); i++)
                {
                    IFeature fs = poleFe.DataSet.GetFeature(i);
                    object val = fs.DataRow["ele"];
                    double poleEle = Convert.ToDouble(val);
                    double x0 = (fs.Coordinates[0].X - xx0) * ToMeter;
                    double y0 = (fs.Coordinates[0].Y - yy0) * ToMeter;
                    double z0 = (fs.Coordinates[0].Z) * ToMeter;

                    object val1 = fs.DataRow["w"];
                    object val2 = fs.DataRow["h"];
                    object val3 = fs.DataRow["Azimuth"];
                    object val4 = fs.DataRow["Ele_angle"];

                    double w = Convert.ToDouble(val1) * ToMeter; //Convert.ToDouble(txtPvWidth.Text) * ToMeter;
                    double h = Convert.ToDouble(val2) * ToMeter; //Convert.ToDouble(txtPvLength.Text) * ToMeter;
                    double az = Convert.ToDouble(val3);        // pvAz.AzimutAngle;
                    double tilt = Convert.ToDouble(val4);       // pvTilt.tiltAngle;

                    if (i == 0)
                    {
                        // xx0 = x0; yy0 = y0;
                        // x0 = 0; y0 = 0;
                    }
                    writer.WriteStartElement("geometry");
                    {
                        writer.WriteAttributeString("id", "ID" + getSketchUpId(i).ToString());
                        writer.WriteStartElement("mesh");
                        {
                            //--------
                            //1st Set
                            //--------
                            writer.WriteStartElement("source");
                            {
                                writer.WriteAttributeString("id", "ID" + getSketchUpSourceId(i).ToString());
                                writer.WriteStartElement("float_array");
                                {
                                    writer.WriteAttributeString("id", "ID" + (getSketchUpSourceId(i) + 3).ToString());
                                    writer.WriteAttributeString("count", "12");
                                    //writer.WriteString(get3Dcord(i));
                                    if (prj.DemChecked == true)
                                    {
                                        writer.WriteString(pvPanel(w, h, x0, y0, z0, tilt, az));
                                    }
                                    else
                                    {
                                        writer.WriteString(pvPanel(w, h, x0, y0, z0, tilt, az));
                                    }

                                } writer.WriteEndElement();//"float_array"    

                                writer.WriteStartElement("technique_common");
                                {
                                    writer.WriteStartElement("accessor");
                                    {// <accessor count="24" source="#ID8" stride="3">
                                        writer.WriteAttributeString("count", "4");
                                        writer.WriteAttributeString("source", "#ID" + (getSketchUpSourceId(i) + 3).ToString());
                                        writer.WriteAttributeString("stride", "3");
                                        //<param name="X" type="float" />
                                        writer.WriteStartElement("param");
                                        {
                                            writer.WriteAttributeString("name", "X");
                                            writer.WriteAttributeString("type", "float");
                                        } writer.WriteEndElement();//"param"
                                        writer.WriteStartElement("param");
                                        {
                                            writer.WriteAttributeString("name", "Y");
                                            writer.WriteAttributeString("type", "float");
                                        } writer.WriteEndElement();//"param"
                                        writer.WriteStartElement("param");
                                        {
                                            writer.WriteAttributeString("name", "Z");
                                            writer.WriteAttributeString("type", "float");
                                        } writer.WriteEndElement(); //"param"
                                    } writer.WriteEndElement(); //"accessor"
                                } writer.WriteEndElement(); //"technique_common"
                            } writer.WriteEndElement();//"source"
                            //--------
                            //2nd Set
                            //--------
                            writer.WriteStartElement("source");
                            {
                                writer.WriteAttributeString("id", "ID" + (getSketchUpSourceId(i) + 1).ToString());
                                writer.WriteStartElement("float_array");
                                {
                                    writer.WriteAttributeString("id", "ID" + (getSketchUpSourceId(i) + 4).ToString());
                                    writer.WriteAttributeString("count", "12");
                                    // writer.WriteString("0 0 -1 0 0 -1 0 0 -1 0 0 -1 -1 0 0 -1 0 0 -1 0 0 -1 0 0 -0 1 0 -0 1 0 -0 1 0 -0 1 0 1 0 0 1 0 0 1 0 0 1 0 0 -0 -1 -0 -0 -1 -0 -0 -1 -0 -0 -1 -0 0 0 1 0 0 1 0 0 1 0 0 1");
                                    writer.WriteString(getSketchUp3DCam(1));
                                } writer.WriteEndElement();//"float_array"    

                                writer.WriteStartElement("technique_common");
                                {
                                    writer.WriteStartElement("accessor");
                                    {// <accessor count="24" source="#ID8" stride="3">
                                        writer.WriteAttributeString("count", "4");
                                        writer.WriteAttributeString("source", "#ID" + (getSketchUpSourceId(i) + 4).ToString());
                                        writer.WriteAttributeString("stride", "3");
                                        //<param name="X" type="float" />
                                        writer.WriteStartElement("param");
                                        {
                                            writer.WriteAttributeString("name", "X");
                                            writer.WriteAttributeString("type", "float");
                                        } writer.WriteEndElement();//"param"
                                        writer.WriteStartElement("param");
                                        {
                                            writer.WriteAttributeString("name", "Y");
                                            writer.WriteAttributeString("type", "float");
                                        } writer.WriteEndElement();//"param"
                                        writer.WriteStartElement("param");
                                        {
                                            writer.WriteAttributeString("name", "Z");
                                            writer.WriteAttributeString("type", "float");
                                        } writer.WriteEndElement(); //"param"
                                    } writer.WriteEndElement(); //"accessor"
                                } writer.WriteEndElement(); //"technique_common"
                            } writer.WriteEndElement();//"source"
                            //--------
                            //3rd Set
                            //--------
                            writer.WriteStartElement("vertices");
                            {
                                writer.WriteAttributeString("id", "ID" + (getSketchUpSourceId(i) + 2).ToString());
                                writer.WriteStartElement("input");
                                {// <input semantic="POSITION" source="#ID11" />
                                    writer.WriteAttributeString("semantic", "POSITION");
                                    writer.WriteAttributeString("source", "#ID" + (getSketchUpSourceId(i) + 0).ToString());
                                } writer.WriteEndElement(); //"input"
                                writer.WriteStartElement("input");
                                {// <input semantic="NORMAL" source="#ID12" />
                                    writer.WriteAttributeString("semantic", "NORMAL");
                                    writer.WriteAttributeString("source", "#ID" + (getSketchUpSourceId(i) + 1).ToString());
                                } writer.WriteEndElement(); //"input"
                            } writer.WriteEndElement();//"vertices"
                            writer.WriteStartElement("triangles");
                            {//<triangles count="12" material="Material2">
                                writer.WriteAttributeString("count", "2");
                                writer.WriteAttributeString("material", "Material2");
                                writer.WriteStartElement("input");
                                {//<input offset="0" semantic="VERTEX" source="#ID13" />
                                    writer.WriteAttributeString("offset", "0");
                                    writer.WriteAttributeString("semantic", "VERTEX");
                                    writer.WriteAttributeString("source", "#ID" + (getSketchUpSourceId(i) + 2).ToString());
                                } writer.WriteEndElement(); //"input"
                                writer.WriteStartElement("p");
                                {//
                                    writer.WriteString("0 1 2 1 0 3");
                                } writer.WriteEndElement(); //"p"

                            } writer.WriteEndElement();//"triangles"                        
                        } writer.WriteEndElement();//"mesh"
                    } writer.WriteEndElement(); //"geometry"
                } writer.WriteEndElement();//library_geometries
                //----------------------------------------------------------------------------------
                //Footer
                //----------------------------------------------------------------------------------
                writer.WriteStartElement("library_materials");
                {
                    writer.WriteStartElement("material");
                    {
                        writer.WriteAttributeString("id", "ID3");
                        writer.WriteAttributeString("name", "__auto_");
                        writer.WriteStartElement("instance_effect");
                        {
                            writer.WriteAttributeString("url", "#ID4");
                        } writer.WriteEndElement(); //"instance_effect"
                    } writer.WriteEndElement(); //"material"
                } writer.WriteEndElement(); //"library_materials"

                writer.WriteStartElement("library_effects");
                {
                    writer.WriteStartElement("effect");
                    {
                        writer.WriteAttributeString("id", "ID4");
                        writer.WriteStartElement("profile_COMMON");
                        {
                            writer.WriteStartElement("technique");
                            {
                                writer.WriteAttributeString("sid", "COMMON");
                                writer.WriteStartElement("lambert");
                                {
                                    writer.WriteStartElement("diffuse");
                                    {
                                        writer.WriteStartElement("color");
                                        {
                                            writer.WriteString("1 1 1 1");

                                        } writer.WriteEndElement(); //"color"   
                                    } writer.WriteEndElement(); //"diffuse"   
                                } writer.WriteEndElement(); //"lambert"      
                            } writer.WriteEndElement(); //"technique"      
                        } writer.WriteEndElement(); //"profile_COMMON"       
                    } writer.WriteEndElement(); //"effect"                
                } writer.WriteEndElement(); //"library_effects"

                writer.WriteStartElement("scene");
                {
                    writer.WriteStartElement("instance_visual_scene");
                    {
                        writer.WriteAttributeString("url", "#ID1");

                    } writer.WriteEndElement(); //"instance_visual_scene"   
                } writer.WriteEndElement(); //"scene"   
            } writer.WriteEndElement(); //"COLLADA"
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();



            String line;
            try
            {
                StreamReader sh = new StreamReader(pvDir + "\\SketchUp\\COLLADASH.h");
                //StreamReader sr = new StreamReader(pvDir + "\\SketchUp\\temp.xml");
                StreamReader sr = new StreamReader(temp_path + "\\temp.xml");
                StreamWriter sw = new StreamWriter(exportfolder + "\\pvPanel.dae");
                line = sh.ReadLine(); sw.WriteLine(line); line = sr.ReadLine();
                line = sh.ReadLine(); sw.WriteLine(line); line = sr.ReadLine();
                line = sr.ReadLine();
                while (line != null)
                {
                    sw.WriteLine(line);
                    line = sr.ReadLine();
                }

                //close the file
                sh.Close();
                sr.Close();
                sw.Close();
                //MessageBox.Show("Google SketchUp file Export completet");  
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }

        private void Export2SketchUp4Bldg(IMapFeatureLayer bldgFe, string exportfolder)
        {
            int nShp = bldgFe.DataSet.NumRows() - 1;
            //Extent ext = poleFe.DataSet.GetFeature(nShp).Envelope.ToExtent();
            double x0 = prj.UtmE;// Convert.ToDouble(txtUtmE.Text);
            double y0 = prj.UtmN;// Convert.ToDouble(txtUtmN.Text);
            double z0 = 0;
            IMapRasterLayer demLyr;
            Raster dem4Pv = new Raster();
            int iDemLyr = util.getLayerHdl(prj.LyrDEMname, pvMap);
            if (iDemLyr != -1 & prj.DemChecked == true)
            {
                demLyr = pvMap.Layers[iDemLyr] as IMapRasterLayer;
                int mRow = demLyr.Bounds.NumRows;
                int mCol = demLyr.Bounds.NumColumns;
                dem4Pv = (Raster)demLyr.DataSet;
                //Coordinate ptReference = new Coordinate(Convert.ToDouble(txtUtmE.Text), Convert.ToDouble(txtUtmN.Text));
                Coordinate ptReference = new Coordinate(x0, y0);
                RcIndex rc = dem4Pv.ProjToCell(ptReference);
                if (rc.Column < 0 | rc.Row < 0)
                {
                    z0 = 0;
                }
                else
                {
                    //double z0 = demLyr.DataSet.Value[rc.Row, rc.Column];
                    z0 = 0; //assume Building base level is equal to reference point level
                    //z0 = dem4Pv.Value[rc.Row, rc.Column];
                }
            }

            int nNode = 0;
            for (int iShp = 0; iShp < bldgFe.DataSet.NumRows(); iShp++)
            {
                IFeature fs = bldgFe.DataSet.GetFeature(iShp);
                nNode += fs.NumPoints;
            }
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            //object obj; 
            //XmlWriter writer = XmlWriter.Create(pvDir + "\\SketchUp\\temp.xml", settings);
            XmlWriter writer = XmlWriter.Create(temp_path + "\\temp.xml", settings);
            //-------------------------------------------------------
            //Header
            //-------------------------------------------------------
            writer.WriteStartElement("COLLADA");
            {
                writer.WriteAttributeString("xmlns", "xsi", null, "http://www.collada.org/2005/11/COLLADASchema");
                writer.WriteAttributeString("version", "1.4.1");
                writer.WriteStartElement("asset");
                {
                    writer.WriteStartElement("contributor");
                    {
                        writer.WriteStartElement("authoring_tool");
                        {
                            writer.WriteString("SketchUp 8.0.16846");
                        } writer.WriteEndElement(); //"authoring_tool"
                    } writer.WriteEndElement(); //"contributor"
                    writer.WriteStartElement("created");
                    {
                        writer.WriteString("2013-07-22T15:46:40Z");
                    } writer.WriteEndElement(); //"created"                    
                    writer.WriteStartElement("modified");
                    {
                        writer.WriteString("2013-07-22T15:46:40Z");
                    } writer.WriteEndElement(); //"modified"
                    writer.WriteStartElement("unit");
                    {
                        writer.WriteAttributeString("meter", "0.2539999969303608");
                        writer.WriteAttributeString("name", "inch");
                    } writer.WriteEndElement(); //"unit"
                    writer.WriteStartElement("up_axis");
                    {
                        writer.WriteString("Z_UP");
                    } writer.WriteEndElement(); //"up_axis"                    
                } writer.WriteEndElement(); //"asset"
                writer.WriteStartElement("library_visual_scenes");
                {
                    writer.WriteStartElement("visual_scene");
                    {
                        writer.WriteAttributeString("id", "ID1");
                        //--------------------------------------------------------------------------
                        // Node
                        //--------------------------------------------------------------------------
                        writer.WriteStartElement("node");
                        {
                            writer.WriteAttributeString("name", "SketchUp");
                            for (int iNode = 0; iNode < nNode; iNode++)
                            {
                                writer.WriteStartElement("instance_geometry");
                                {
                                    writer.WriteAttributeString("url", "#ID" + getSketchUpId(iNode).ToString());
                                    writer.WriteStartElement("bind_material");
                                    {
                                        writer.WriteStartElement("technique_common");
                                        {
                                            writer.WriteStartElement("instance_material");
                                            {
                                                writer.WriteAttributeString("symbol", "Material2");
                                                writer.WriteAttributeString("target", "#ID3");
                                                writer.WriteStartElement("bind_vertex_input");
                                                {
                                                    writer.WriteAttributeString("semantic", "UVSET0");
                                                    writer.WriteAttributeString("input_semantic", "TEXCOORD");
                                                    writer.WriteAttributeString("input_set", "0");
                                                } writer.WriteEndElement();
                                            } writer.WriteEndElement();
                                        } writer.WriteEndElement();
                                    } writer.WriteEndElement();
                                } writer.WriteEndElement();
                            }
                        } writer.WriteEndElement();//"Node"
                        //--------------------------------------------------------------------------
                    } writer.WriteEndElement(); //"visual_scene"
                } writer.WriteEndElement(); //"library_visual_scenes"
                //----------------------------------------------------------------------------------
                // Geometry
                //----------------------------------------------------------------------------------
                writer.WriteStartElement("library_geometries");
                double ToMeter = 3.96874995203687;
                /*
                double w = Convert.ToDouble(txtPvWidth.Text) * ToMeter;
                double h = Convert.ToDouble(txtPvLength.Text) * ToMeter;
                double az = pvAz.AzimutAngle;
                double tilt = pvTilt.tiltAngle;
                double xx0 = Convert.ToDouble(txtUtmE.Text);
                double yy0 = Convert.ToDouble(txtUtmN.Text);
                */
                int i = -1;
                for (int iShp = 0; iShp < bldgFe.DataSet.NumRows(); iShp++)
                {
                    IFeature fs = bldgFe.DataSet.GetFeature(iShp);
                    object val = fs.DataRow["height"];
                    double bldgEle = Convert.ToDouble(val);
                    Coordinate[] Side = new Coordinate[4];
                    for (int iSeg = 0; iSeg < fs.NumPoints; iSeg++)
                    {
                        i++;
                        if (iSeg < fs.NumPoints - 1)
                        {
                            Side[0] = new Coordinate((fs.Coordinates[iSeg + 1].X - x0) * ToMeter, (fs.Coordinates[iSeg + 1].Y - y0) * ToMeter, (z0) * ToMeter);
                            Side[1] = new Coordinate((fs.Coordinates[iSeg].X - x0) * ToMeter, (fs.Coordinates[iSeg].Y - y0) * ToMeter, (z0 + bldgEle) * ToMeter);
                            Side[2] = new Coordinate((fs.Coordinates[iSeg].X - x0) * ToMeter, (fs.Coordinates[iSeg].Y - y0) * ToMeter, (z0) * ToMeter);
                            Side[3] = new Coordinate((fs.Coordinates[iSeg + 1].X - x0) * ToMeter, (fs.Coordinates[iSeg + 1].Y - y0) * ToMeter, (z0 + bldgEle) * ToMeter);
                        }
                        else
                        {
                            Side[0] = new Coordinate((fs.Coordinates[0].X - x0) * ToMeter, (fs.Coordinates[0].Y - y0) * ToMeter, (z0) * ToMeter);
                            Side[1] = new Coordinate((fs.Coordinates[iSeg].X - x0) * ToMeter, (fs.Coordinates[iSeg].Y - y0) * ToMeter, (z0 + bldgEle) * ToMeter);
                            Side[2] = new Coordinate((fs.Coordinates[iSeg].X - x0) * ToMeter, (fs.Coordinates[iSeg].Y - y0) * ToMeter, (z0) * ToMeter);
                            Side[3] = new Coordinate((fs.Coordinates[0].X - x0) * ToMeter, (fs.Coordinates[0].Y - y0) * ToMeter, (z0 + bldgEle) * ToMeter);
                        }

                        writer.WriteStartElement("geometry");
                        {
                            writer.WriteAttributeString("id", "ID" + getSketchUpId(i).ToString());
                            writer.WriteStartElement("mesh");
                            {
                                //--------
                                //1st Set
                                //--------
                                writer.WriteStartElement("source");
                                {
                                    writer.WriteAttributeString("id", "ID" + getSketchUpSourceId(i).ToString());
                                    writer.WriteStartElement("float_array");
                                    {
                                        writer.WriteAttributeString("id", "ID" + (getSketchUpSourceId(i) + 3).ToString());
                                        writer.WriteAttributeString("count", "12");
                                        //writer.WriteString(get3Dcord(i));
                                        string p1 = Side[0].X.ToString() + " " + Side[0].Y.ToString() + " " + Side[0].Z.ToString();
                                        string p2 = Side[1].X.ToString() + " " + Side[1].Y.ToString() + " " + Side[1].Z.ToString();
                                        string p3 = Side[2].X.ToString() + " " + Side[2].Y.ToString() + " " + Side[2].Z.ToString();
                                        string p4 = Side[3].X.ToString() + " " + Side[3].Y.ToString() + " " + Side[3].Z.ToString();
                                        writer.WriteString(p1 + " " + p2 + " " + p3 + " " + p4);

                                    } writer.WriteEndElement();//"float_array"    

                                    writer.WriteStartElement("technique_common");
                                    {
                                        writer.WriteStartElement("accessor");
                                        {// <accessor count="24" source="#ID8" stride="3">
                                            writer.WriteAttributeString("count", "4");
                                            writer.WriteAttributeString("source", "#ID" + (getSketchUpSourceId(i) + 3).ToString());
                                            writer.WriteAttributeString("stride", "3");
                                            //<param name="X" type="float" />
                                            writer.WriteStartElement("param");
                                            {
                                                writer.WriteAttributeString("name", "X");
                                                writer.WriteAttributeString("type", "float");
                                            } writer.WriteEndElement();//"param"
                                            writer.WriteStartElement("param");
                                            {
                                                writer.WriteAttributeString("name", "Y");
                                                writer.WriteAttributeString("type", "float");
                                            } writer.WriteEndElement();//"param"
                                            writer.WriteStartElement("param");
                                            {
                                                writer.WriteAttributeString("name", "Z");
                                                writer.WriteAttributeString("type", "float");
                                            } writer.WriteEndElement(); //"param"
                                        } writer.WriteEndElement(); //"accessor"
                                    } writer.WriteEndElement(); //"technique_common"
                                } writer.WriteEndElement();//"source"
                                //--------
                                //2nd Set
                                //--------
                                writer.WriteStartElement("source");
                                {
                                    writer.WriteAttributeString("id", "ID" + (getSketchUpSourceId(i) + 1).ToString());
                                    writer.WriteStartElement("float_array");
                                    {
                                        writer.WriteAttributeString("id", "ID" + (getSketchUpSourceId(i) + 4).ToString());
                                        writer.WriteAttributeString("count", "12");
                                        // writer.WriteString("0 0 -1 0 0 -1 0 0 -1 0 0 -1 -1 0 0 -1 0 0 -1 0 0 -1 0 0 -0 1 0 -0 1 0 -0 1 0 -0 1 0 1 0 0 1 0 0 1 0 0 1 0 0 -0 -1 -0 -0 -1 -0 -0 -1 -0 -0 -1 -0 0 0 1 0 0 1 0 0 1 0 0 1");
                                        writer.WriteString(getSketchUp3DCam(1));
                                    } writer.WriteEndElement();//"float_array"    

                                    writer.WriteStartElement("technique_common");
                                    {
                                        writer.WriteStartElement("accessor");
                                        {// <accessor count="24" source="#ID8" stride="3">
                                            writer.WriteAttributeString("count", "4");
                                            writer.WriteAttributeString("source", "#ID" + (getSketchUpSourceId(i) + 4).ToString());
                                            writer.WriteAttributeString("stride", "3");
                                            //<param name="X" type="float" />
                                            writer.WriteStartElement("param");
                                            {
                                                writer.WriteAttributeString("name", "X");
                                                writer.WriteAttributeString("type", "float");
                                            } writer.WriteEndElement();//"param"
                                            writer.WriteStartElement("param");
                                            {
                                                writer.WriteAttributeString("name", "Y");
                                                writer.WriteAttributeString("type", "float");
                                            } writer.WriteEndElement();//"param"
                                            writer.WriteStartElement("param");
                                            {
                                                writer.WriteAttributeString("name", "Z");
                                                writer.WriteAttributeString("type", "float");
                                            } writer.WriteEndElement(); //"param"
                                        } writer.WriteEndElement(); //"accessor"
                                    } writer.WriteEndElement(); //"technique_common"
                                } writer.WriteEndElement();//"source"
                                //--------
                                //3rd Set
                                //--------
                                writer.WriteStartElement("vertices");
                                {
                                    writer.WriteAttributeString("id", "ID" + (getSketchUpSourceId(i) + 2).ToString());
                                    writer.WriteStartElement("input");
                                    {// <input semantic="POSITION" source="#ID11" />
                                        writer.WriteAttributeString("semantic", "POSITION");
                                        writer.WriteAttributeString("source", "#ID" + (getSketchUpSourceId(i) + 0).ToString());
                                    } writer.WriteEndElement(); //"input"
                                    writer.WriteStartElement("input");
                                    {// <input semantic="NORMAL" source="#ID12" />
                                        writer.WriteAttributeString("semantic", "NORMAL");
                                        writer.WriteAttributeString("source", "#ID" + (getSketchUpSourceId(i) + 1).ToString());
                                    } writer.WriteEndElement(); //"input"
                                } writer.WriteEndElement();//"vertices"
                                writer.WriteStartElement("triangles");
                                {//<triangles count="12" material="Material2">
                                    writer.WriteAttributeString("count", "2");
                                    writer.WriteAttributeString("material", "Material2");
                                    writer.WriteStartElement("input");
                                    {//<input offset="0" semantic="VERTEX" source="#ID13" />
                                        writer.WriteAttributeString("offset", "0");
                                        writer.WriteAttributeString("semantic", "VERTEX");
                                        writer.WriteAttributeString("source", "#ID" + (getSketchUpSourceId(i) + 2).ToString());
                                    } writer.WriteEndElement(); //"input"
                                    writer.WriteStartElement("p");
                                    {//
                                        writer.WriteString("0 1 2 1 0 3");
                                    } writer.WriteEndElement(); //"p"

                                } writer.WriteEndElement();//"triangles"                        
                            } writer.WriteEndElement();//"mesh"
                        } writer.WriteEndElement(); //"geometry"
                    } //Segment
                } writer.WriteEndElement();//library_geometries
                //----------------------------------------------------------------------------------
                //Footer
                //----------------------------------------------------------------------------------
                writer.WriteStartElement("library_materials");
                {
                    writer.WriteStartElement("material");
                    {
                        writer.WriteAttributeString("id", "ID3");
                        writer.WriteAttributeString("name", "__auto_");
                        writer.WriteStartElement("instance_effect");
                        {
                            writer.WriteAttributeString("url", "#ID4");
                        } writer.WriteEndElement(); //"instance_effect"
                    } writer.WriteEndElement(); //"material"
                } writer.WriteEndElement(); //"library_materials"

                writer.WriteStartElement("library_effects");
                {
                    writer.WriteStartElement("effect");
                    {
                        writer.WriteAttributeString("id", "ID4");
                        writer.WriteStartElement("profile_COMMON");
                        {
                            writer.WriteStartElement("technique");
                            {
                                writer.WriteAttributeString("sid", "COMMON");
                                writer.WriteStartElement("lambert");
                                {
                                    writer.WriteStartElement("diffuse");
                                    {
                                        writer.WriteStartElement("color");
                                        {
                                            writer.WriteString("1 1 1 1");

                                        } writer.WriteEndElement(); //"color"   
                                    } writer.WriteEndElement(); //"diffuse"   
                                } writer.WriteEndElement(); //"lambert"      
                            } writer.WriteEndElement(); //"technique"      
                        } writer.WriteEndElement(); //"profile_COMMON"       
                    } writer.WriteEndElement(); //"effect"                
                } writer.WriteEndElement(); //"library_effects"

                writer.WriteStartElement("scene");
                {
                    writer.WriteStartElement("instance_visual_scene");
                    {
                        writer.WriteAttributeString("url", "#ID1");

                    } writer.WriteEndElement(); //"instance_visual_scene"   
                } writer.WriteEndElement(); //"scene"   
            } writer.WriteEndElement(); //"COLLADA"
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();



            String line;
            try
            {
                StreamReader sh = new StreamReader(pvDir + "\\SketchUp\\COLLADASH.h");
                //StreamReader sr = new StreamReader(pvDir + "\\SketchUp\\temp.xml");
                StreamReader sr = new StreamReader(temp_path + "\\temp.xml");
                StreamWriter sw = new StreamWriter(exportfolder + "\\pvBuilding.dae");
                line = sh.ReadLine(); sw.WriteLine(line); line = sr.ReadLine();
                line = sh.ReadLine(); sw.WriteLine(line); line = sr.ReadLine();
                line = sr.ReadLine();
                while (line != null)
                {
                    sw.WriteLine(line);
                    line = sr.ReadLine();
                }

                //close the file
                sh.Close();
                sr.Close();
                sw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }

        //------------------------------------------------------------------------------------------

        int getSketchUpId(int i)
        {
            if (i == 0) { return 2; }
            else { return 10 + (i - 1) * 6; }
        }

        int getSketchUpSourceId(int i)
        {
            return 5 + (i) * 6;
        }

        string getSketchUp3DCam(int i)
        {
            string c;
            c = "0 0 -1 0 0 -1 0 0 -1 0 0 -1 -1 0 0 -1 0 0 -1 0 0 -1 0 0 -0 1 0 -0 1 0 -0 1 0 -0 1 0 1 0 0 1 0 0 1 0 0 1 0 0 -0 -1 -0 -0 -1 -0 -0 -1 -0 -0 -1 -0 -0 -0 1 -0 -0 1 -0 -0 1 -0 -0 1";
            c = "0 -1 1 0 -1 1 0 -1 1 0 -1 1";
            return c;
        }

        #endregion //Sketchup



        private void pvMap_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(e.KeyCode.ToString());
            if (e.KeyCode.ToString() == "Delete")
            {
                FeatureSet fe = pvMap.Layers.SelectedLayer.DataSet as FeatureSet;
                fe.Save();
            }

        }

        private void cmdPvPanelConfig4Roof_Click(object sender, EventArgs e)
        {
            cmdCreatePvPosition.Enabled = true;


            frmPvPanel frmPanelSize = new frmPvPanel();


            prj.LyrPole = util.getLayerHdl(cmbRoofTopPanelPosition.Text, pvMap);
            if (prj.LyrPole != -1)
            {
                setCurrrentLayer(cmbRoofTopPanelPosition.Text);
                FeatureLayer FeLyr = pvMap.Layers[prj.LyrPole] as FeatureLayer;
                FeLyr.SelectAll();

                frmPanelSize.Michael = this;
                frmPanelSize.PvMap = pvMap;
                frmPanelSize.Project = prj;
                frmPanelSize.ShowDialog();
                frmPanelSize.RP = rp;
            }
            else
            {
                MessageBox.Show("Please select PV position layer before continuing");
            }
        }

        private void cmdCreateRooftopPanel_Click(object sender, EventArgs e)
        {
            CheckWorkingPath();

            if (prj.Path != "")
            {
                if (verify[3] == false)
                {
                    MessageBox.Show("Please calculate solar properties before continuing");
                    return;
                }
                if (formSiteArea.cmbTrack_mode.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select tracking mode before calculating");
                    return;
                }
                //cmdReport.Enabled = false;
                pvMap.MapFrame.DrawingLayers.Clear();

                //------------------------------------------------------------------
                //------------------------------------------------------------------
                //------------------------------------------------------------------

                double w = 1;// Convert.ToDouble(txtPvWidth.Text);
                double h = 2;// Convert.ToDouble(txtPvLength.Text);
                double tilt = Convert.ToDouble(txtRoofTilt.Text);
                double azimuth = Convert.ToDouble(txtRoofAz.Text);
                double z = 0;
                int PoleLyr = util.getLayerHdl(cmbRoofTopPanelPosition.Text, pvMap);

                //------------------------------------------------------------------
                //------------------------------------------------------------------
                //------------------------------------------------------------------

                if (PoleLyr != -1)
                {
                    //-----------------------------------------------------
                    IFeatureSet pvfs;
                    pvfs = new FeatureSet(FeatureType.Polygon);
                    //---------------------------------------------------------
                    pvfs.DataTable.Columns.Add(new DataColumn("x", typeof(double)));
                    pvfs.DataTable.Columns.Add(new DataColumn("y", typeof(double)));
                    pvfs.DataTable.Columns.Add(new DataColumn("w", typeof(double)));
                    pvfs.DataTable.Columns.Add(new DataColumn("h", typeof(double)));
                    pvfs.DataTable.Columns.Add(new DataColumn("Azimuth", typeof(double)));
                    pvfs.DataTable.Columns.Add(new DataColumn("Ele_Angle", typeof(double)));
                    pvfs.DataTable.Columns.Add(new DataColumn("ele", typeof(double)));
                    //---------------------------------------------------------

                    //-----------------------------------------------------
                    IMapFeatureLayer poleFe = pvMap.Layers[PoleLyr] as IMapFeatureLayer;
                    //MessageBox.Show("Number of pole = " + poleFe.DataSet.NumRows());
                    int nShp = poleFe.DataSet.NumRows() - 1;
                    Extent ext = poleFe.DataSet.GetFeature(nShp).Envelope.ToExtent();
                    numPvPanel = poleFe.DataSet.NumRows();
                    //MessageBox.Show(ext.X.ToString() + "," + ext.Y.ToString() + "," + ext.Width.ToString() + "," + ext.Height.ToString());
                    for (int i = 0; i < poleFe.DataSet.NumRows(); i++)
                    {
                        IFeature fs = poleFe.DataSet.GetFeature(i);
                        double x1 = fs.Coordinates[0].X;
                        double y1 = fs.Coordinates[0].Y;
                        w = Convert.ToDouble(fs.DataRow["W"]);
                        h = Convert.ToDouble(fs.DataRow["H"]);
                        z = Convert.ToDouble(fs.DataRow["ele"]);
                        //tilt = Convert.ToDouble(fs.DataRow["Ele_angle"]);
                        //azimuth = Convert.ToDouble(fs.DataRow["Azimuth"]);

                        IFeature ifea = pvfs.AddFeature(pvPanelFe(w, h, x1, y1, z, tilt, azimuth));
                        ifea.DataRow.BeginEdit();
                        ifea.DataRow["x"] = x1;
                        ifea.DataRow["y"] = y1;
                        ifea.DataRow["w"] = w;
                        ifea.DataRow["h"] = h;
                        ifea.DataRow["Azimuth"] = azimuth;
                        ifea.DataRow["Ele_Angle"] = tilt;
                        ifea.DataRow["ele"] = z;
                        ifea.DataRow.EndEdit();

                    }

                    pvfs.Projection = pvMap.Projection;

                    //+++++++++++++++++++++++++++++++++++++++++++++++++++
                    pvfs.Name = "pvArray";
                    pvfs.Filename = prj.Path + "\\Temp\\" + pvfs.Name + ".shp";
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++

                    pvfs.SaveAs(pvfs.Filename, true);
                    util.removeDupplicateLyr(pvfs.Name, pvMap);
                    cmbRoofTopPanelPanel.Text = pvfs.Name;
                    pvMap.Layers.Add(pvfs);
                    loadLayerList();
                    cmdExportRooftopPanetToSkecthUp.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Please assign the PV position layer before continuing");
                }
            }
        }

        public void CentroidAsSite(string Layer, Map pvMap)
        {
            if (System.IO.File.Exists(txtTM2.Text) == false)
            {
                try
                {
                    int cLyr = util.getLayerHdl(Layer, pvMap);
                    FeatureSet Fe = pvMap.Layers[cLyr].DataSet as FeatureSet;
                    Fe.UpdateExtent();
                    double xx = Fe.Extent.X + Fe.Extent.Width / 2;
                    double yy = Fe.Extent.Y - Fe.Extent.Height / 2;

                    Pen MyPen = new Pen(Color.Black);
                    Coordinate c = new Coordinate(xx, yy);
                    Extent ext = pvMap.ViewExtents;
                    double dx = (ext.MaxX - ext.MinX);
                    double dy = (ext.MaxX - ext.MinX);
                    ext.SetCenter(c);
                    pvMap.ViewExtents = ext;// layer.DataSet.Extent;

                    double[] latlong = new double[] { c.X, c.Y };
                    Reproject.ReprojectPoints(latlong, new double[] { 0 }, pvMap.Projection, KnownCoordinateSystems.Geographic.World.WGS1984, 0, 1);
                    txtLNG.Text = latlong[0].ToString();
                    txtLAT.Text = latlong[1].ToString();
                    double minDist = 1000000.00;

                    for (int i = 0; i < nSta; i++)
                    {
                        double rr;
                        rr = Math.Sqrt(Math.Pow((latlong[0] - wSta[i].LONG2), 2) + Math.Pow((latlong[1] - wSta[i].LAT2), 2));
                        wStaDistance[i] = rr;
                        wStaSel[i] = i;
                        if (rr < minDist)
                        {
                            minDist = rr;
                            cmbState.Text = wSta[i].State;
                            cmbCity.Text = wSta[i].City;
                        }
                    }

                    double temp;
                    int temp1;
                    for (int i = (nSta - 2); i >= 0; i--)
                    {
                        for (int j = 1; j <= i; j++)
                        {
                            if (wStaDistance[j - 1] > wStaDistance[j])
                            {
                                temp = wStaDistance[j - 1];
                                wStaDistance[j - 1] = wStaDistance[j];
                                wStaDistance[j] = temp;
                                temp1 = wStaSel[j - 1];
                                wStaSel[j - 1] = wStaSel[j];
                                wStaSel[j] = temp1;
                            }
                        }
                    }

                    //-----------------------------------------------------------------------
                    double radius = 1000;
                    txtUtmN.Text = c.Y.ToString();
                    txtUtmE.Text = c.X.ToString();
                    pvMap.MapFrame.DrawingLayers.Clear();
                    util.kDrawCircle(c.X, c.Y, radius, 360, pvMap, Color.Magenta);
                    //MessageBox.Show(c.X + "," + c.Y);
                    Double r = 0.25;
                    int nIDW = Convert.ToInt16(txtNIdwSta.Text);
                    util.kDrawCircle(c.X, c.Y, r, 360, pvMap, Color.Magenta);
                    prj.UtmE = c.X;
                    prj.UtmN = c.Y;
                    for (int ii = 0; ii < nIDW; ii++)
                    {
                        double lat = wSta[wStaSel[ii]].LAT2;
                        double lng = wSta[wStaSel[ii]].LONG2;
                        double x = 0;
                        double y = 0;
                        double[] mapCoordinate = new double[] { lng, lat };
                        Reproject.ReprojectPoints(mapCoordinate, new double[] { 0 }, KnownCoordinateSystems.Geographic.World.WGS1984, pvMap.Projection, 0, 1);
                        Coordinate coord = util.circleCoord(c.X, c.Y, mapCoordinate[0], mapCoordinate[1], radius); //returns coordinate of point on inner circle
                        Coordinate coord2 = util.circleCoord(mapCoordinate[0], mapCoordinate[1], c.X, c.Y, radius); //returns coordinates of point on outer circle

                        util.kDrawCircle(mapCoordinate[0], mapCoordinate[1], radius, 360, pvMap, Color.Magenta);
                        util.DrawLine(coord2.X, coord2.Y, coord.X, coord.Y, 2, Color.Magenta, pvMap);
                    }

                    Envelope env = new Envelope();
                    env.SetExtents(prj.UtmE - 1000, prj.UtmN - 1000, prj.UtmE + 1000, prj.UtmN + 1000);
                    pvMap.ViewExtents = env.ToExtent();
                    //prj.UseKML = true;
                    DrawRoseDiagram();
                    //cmdCreatePvPole.Enabled = true;
                    //----------------------------------------------------------------------
                    pvMap.ViewExtents = ext;
                    siteLocatedbyCentroid = true;
                }
                catch
                {
                }
            }
        }

        private void cmdEnergyCal_Click(object sender, EventArgs e)
        {
            lblGrdTitle.Text = "Energy Production Estimates for: " + prj.LyrSiteAreaName;
            splitContainer3.SplitterDistance = 20;
            formSiteArea = new frmCreatePoleBySiteArea();
            CheckWorkingPath();

            if (prj.Path != "")
            {         
                CentroidAsSite(prj.LyrSiteAreaName,pvMap);
            }
            if (prj.Path != "")
            {
                if(siteLocatedbyCentroid == false)
                {
                    if (verify[3] == false)
                    {
                        MessageBox.Show("Please calculate solar properties before continuing");
                        return;
                    }               
                }

                if (formSiteArea.cmbTrack_mode.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select tracking mode before calculating");
                    return;
                }
                pvMap.MapFrame.DrawingLayers.Clear();

                //todo : Change to adept to several areas
                prj.LyrPvPanel = util.getLayerHdl(prj.LyrPvPanelName, pvMap);
                if (prj.LyrPvPanel != -1)
                {
                    //-----------------------------------------------------
                    //IFeatureSet pvfs;
                    //pvfs = new FeatureSet(FeatureType.Polygon);
                    //-----------------------------------------------------
                    FeatureSet pvPanelFe = pvMap.Layers[prj.LyrPvPanel].DataSet as FeatureSet;
                    //MessageBox.Show("Number of pole = " + poleFe.DataSet.NumRows());
                    int nShp = pvPanelFe.NumRows();
                    string[] pvCase = new string[nShp];
                    for (int i = 0; i < nShp; i++)
                    {
                        IFeature iFe = pvPanelFe.GetFeature(i);
                        object valH = iFe.DataRow["h"];
                        object valW = iFe.DataRow["w"];
                        object valAz = iFe.DataRow["Azimuth"];
                        object valTilt = iFe.DataRow["Ele_Angle"];
                        //---------------------------------------------------
                        string h = Convert.ToString(valH);
                        string w = Convert.ToString(valW);
                        string Az = Convert.ToString(valAz);
                        string tilt = Convert.ToString(valTilt);
                        //---------------------------------------------------
                        pvCase[i] = w + "," + h + "," + Az + "," + tilt;
                    }
                    string[] case4run = pvCase.Distinct().ToArray();
                    int[] numPanel = new int[case4run.Length];
                    double[] width = new double[case4run.Length];
                    double[] height = new double[case4run.Length];
                    double[] Azimuth = new double[case4run.Length];
                    double[] Tilt = new double[case4run.Length];

                    for (int i = 0; i < case4run.Length; i++)
                    {
                        numPanel[i] = 0;
                        string[] v = case4run[i].Split(',');
                        width[i] = Convert.ToDouble(v[0]);
                        height[i] = Convert.ToDouble(v[1]);
                        Azimuth[i] = Convert.ToDouble(v[2]);
                        Tilt[i] = Convert.ToDouble(v[3]);
                        if (prj.Latiude >= 0) //
                        {
                            if (Tilt[i] < 0)
                            {
                                MessageBox.Show("The location of site is above the mercator, tilt direction was wrong but has been corrected!!", "PV Data set-" + i.ToString() + " error has been corrected");
                                Tilt[i] = Math.Abs(Tilt[i]);
                            }
                        }
                        else
                        {
                            if (Tilt[i] > 0)
                            {
                                MessageBox.Show("The location of site is below the mercator, tilt direction was wrong but has been corrected!!", "PV Data set-" + i.ToString() + " error has been corrected");
                                Tilt[i] = Math.Abs(Tilt[i]);
                            }
                        }

                        //Make sure Tilt must be positvie value
                        for (int j = 0; j < pvCase.Length; j++)
                        {
                            if (case4run[i] == pvCase[j])
                            {
                                numPanel[i]++;
                            }
                        }
                    }

                    /*
                    if (optPvWattFunc.Checked == true)
                    {
                        EnergyProduction(numPvPanel);
                        cmdReport.Enabled = true;
                        cmdOptimization.Enabled = true;
                    }
                     */
                    if (optMultiWeatherSta.Checked == true | optSingleWeatherSta.Checked == true)
                    {
                        if (System.IO.File.Exists(txtTM2.Text) == true)
                        {
                            grdAcProduct.Rows.Clear();
                            for (int i = 0; i < case4run.Length; i++)
                            {

                                EnergyProduction(i, width[i], height[i], Tilt[i], Azimuth[i], numPanel[i], txtTM2.Text);
                                //--------------------------------
                                //Todo: Add accumulate data result 

                            }


                            pnlGrdProduction.Visible = true;
                            cmdSwithToMap.Visible = true;
                            cmdSwithToTable.Visible = false;
                            grdAcProduct.Visible = true;
                            SwitchtoTable();
                        }
                        else
                        {
                            MessageBox.Show("Weather file is incorrect");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please assign the PV position layer before continuing");
                }
            }

        }

        private void txtNIdwSta_TextChanged(object sender, EventArgs e)
        {
            util.NummericTextBoxCheck(txtNIdwSta, "The number of weather station for weighting", 1);
            if (Convert.ToInt16(txtNIdwSta.Text) >= nSta)
            {
                MessageBox.Show("The number of specific weather stations is greater than the number of stations");
                return;
            }
            if (txtNIdwSta.Text == "1")
            {
                optSingleWeatherSta.Checked = true;
            }
            else
            {
                optMultiWeatherSta.Checked = true;
            }
        }

        private void cmdWeatherFile_Click(object sender, EventArgs e)
        {
            optSingleWeatherSta.Checked = true;
            OpenFileDialog openF = new OpenFileDialog();
            openF.Filter = "*.tm2|*.tm2";
            openF.ShowDialog();
            txtTM2.Text = openF.FileName;
        }

        #region "AC calculation--------------------------------------------------------"

        void EnergyProduction(int set, double panelWidth, double panelHeight, double Tilt, double Azimuth, int numPvPanel, string weatherFile)
        {
            pvVerify();
            if (verify[0] == false)
            {
                MessageBox.Show("Please assign the reference location before calculating energy production");
                return;
            }
            float system_size = (float)Convert.ToDouble(txtSystem_size.Text);
            double APerSys = Convert.ToDouble(txtAreaPreSys.Text);
            double DCNamplatePerSqrMeter = APerSys;//25.6 / 4 * Convert.ToDouble(system_size);
            double panelA = panelWidth * panelHeight / DCNamplatePerSqrMeter; //AC factor per panel area


            float derate = (float)Convert.ToDouble(txtDerate.Text);
            int track_mode = formSiteArea.cmbTrack_mode.SelectedIndex;
            //double Latitude = Convert.ToDouble(this.txtLAT.Text);
            //double Longitude = Convert.ToDouble(this.txtLNG.Text);
            //double UtmN = Convert.ToDouble(this.txtUtmN.Text);
            //double UtmE = Convert.ToDouble(this.txtUtmE.Text);
            //int TimeZone = Convert.ToInt16(this.txtTimeZone.Text);

            #region "Multiple Sta"

            //----------------------------------------------------
            //MULTIPLE WEATHER STATION
            //----------------------------------------------------
            if (optMultiWeatherSta.Checked == true)
            {
                int nIdwSta = Convert.ToInt16(txtNIdwSta.Text);
                double[] rX = new double[nIdwSta];
                double[] rY = new double[nIdwSta];
                double[] rR = new double[nIdwSta];
                float[,] mAC = new float[nIdwSta, 12];
                float[,] dAC = new float[nIdwSta, 365];
                double SiteX = Convert.ToDouble(txtUtmE.Text);
                double SiteY = Convert.ToDouble(txtUtmN.Text);
                double SiteLng = Convert.ToDouble(txtLNG.Text);
                double SiteLat = Convert.ToDouble(txtLAT.Text);
                //----------------------------
                // time  shift
                int siteGMT = Convert.ToInt16(12 / 24 * SiteLng);
                //---------------------------


                for (int idwSta = 0; idwSta < nIdwSta; idwSta++)
                {
                    //---------------------------
                    double lat = wSta[wStaSel[idwSta]].LAT2;
                    double lng = wSta[wStaSel[idwSta]].LONG2;
                    //----------------------------
                    // time  shift
                    int staGMT = Convert.ToInt16(12 / 24 * lng);
                    //---------------------------
                    double[] StaCoord = new double[] { lng, lat };
                    Reproject.ReprojectPoints(StaCoord, new double[] { 0 }, KnownCoordinateSystems.Geographic.World.WGS1984, pvMap.Projection, 0, 1);
                    rR[idwSta] = Math.Sqrt(Math.Pow((SiteX - StaCoord[0]), 2) + Math.Pow((SiteY - StaCoord[1]), 2));
                    rX[idwSta] = Math.Abs(SiteX - StaCoord[0]);
                    rY[idwSta] = Math.Abs(SiteY - StaCoord[1]);
                    //---------------------------
                    SSC.Data data = new SSC.Data();
                    data.SetString("file_name", pvDir + "\\WeatherSta\\tm2\\" + wSta[wStaSel[idwSta]].FileName); //TM2 file
                    data.SetNumber("system_size", system_size);
                    data.SetNumber("derate", derate);
                    data.SetNumber("track_mode", track_mode);
                    data.SetNumber("tilt", (int)Tilt);
                    data.SetNumber("azimuth", (int)Azimuth);

                    SSC.Module mod = new SSC.Module("pvwattsv1");
                    if (mod.Exec(data))
                    {
                        float tot = data.GetNumber("ac_annual");
                        float[] ac = data.GetArray("ac_monthly");
                        float[] hourlyAc = data.GetArray("ac");
                        for (int m = 0; m < 12; m++)
                        {
                            mAC[idwSta, m] = ac[m];
                        }
                        //------ daily AC
                        int hh = 0;
                        for (int d = 0; d < 365; d++)
                        {
                            int nHr = 0;
                            float energy = 0;
                            for (int h = 0; h < 24; h++)
                            {
                                if (hourlyAc[h] > 0) nHr++;
                                energy += hourlyAc[hh];
                                hh++;
                            }
                            if (nHr > 0)
                            {
                                dAC[idwSta, d] = energy / 1000; //Change unit W to KW
                            }
                            else
                            {
                                dAC[idwSta, d] = 0;
                            }
                        }
                    }
                }
                //-------------------------------------------------------
                // IDW
                //-------------------------------------------------------
                double[] MonthlyProduct = new double[12];
                double[] DailyProduct = new double[365];
                double px = Convert.ToDouble(txtPowX.Text);
                double py = Convert.ToDouble(txtPowY.Text);
                double sumR = 0;
                for (int idwSta = 0; idwSta < nIdwSta; idwSta++)
                {
                    double rPow = 1 / (Math.Pow(rX[idwSta] / 1000, px) * Math.Pow(rY[idwSta] / 1000, py));
                    sumR += rPow;
                }
                for (int m = 0; m < 12; m++)
                {
                    double sumRZ = 0;
                    for (int idwSta = 0; idwSta < nIdwSta; idwSta++)
                    {
                        double rPowZ = mAC[idwSta, m] / (Math.Pow(rX[idwSta] / 1000, px) * Math.Pow(rY[idwSta] / 1000, py));
                        sumRZ += rPowZ;
                    }
                    MonthlyProduct[m] = sumRZ / sumR;
                }
                for (int d = 0; d < 365; d++)
                {
                    double sumRZ = 0;
                    for (int idwSta = 0; idwSta < nIdwSta; idwSta++)
                    {
                        double rPowZ = dAC[idwSta, d] / (Math.Pow(rX[idwSta] / 1000, px) * Math.Pow(rY[idwSta] / 1000, py));
                        sumRZ += rPowZ;
                    }
                    DailyProduct[d] = sumRZ / sumR;
                }
                //-------------------------------------------------------
                // Reprot
                //-------------------------------------------------------
                //this.grdAcProduct.Rows.Clear();
                for (int month = 1; month <= 12; month++)
                {
                    //---------------------------------------------
                    double MonthAc = 0;
                    //MonthAc = MonthAc / dOfMonth(month);
                    MonthAc = MonthlyProduct[month - 1];
                    //acProduction[month - 1] = MonthAc;
                    //---------------------------------------------
                    int activeRow = grdAcProduct.Rows.Add();
                    this.grdAcProduct.Rows[activeRow].Cells[0].Value = "SET-" + set.ToString("00");
                    this.grdAcProduct.Rows[activeRow].Cells[1].Value = month.ToString("00") + " " + DateTimeFormatInfo.CurrentInfo.GetMonthName(month).ToString();
                    this.grdAcProduct.Rows[activeRow].Cells[2].Value = panelWidth.ToString("0.000");
                    this.grdAcProduct.Rows[activeRow].Cells[3].Value = panelHeight.ToString("0.000");
                    this.grdAcProduct.Rows[activeRow].Cells[4].Value = Math.Round(MonthAc, 0);//PVWatts
                    this.grdAcProduct.Rows[activeRow].Cells[5].Value = Tilt.ToString();
                    this.grdAcProduct.Rows[activeRow].Cells[6].Value = Azimuth.ToString();
                    this.grdAcProduct.Rows[activeRow].Cells[7].Value = Math.Round(MonthAc / DCNamplatePerSqrMeter, 2); // per Sqr.Meter
                    this.grdAcProduct.Rows[activeRow].Cells[8].Value = numPvPanel.ToString();
                    this.grdAcProduct.Rows[activeRow].Cells[9].Value = Math.Round(MonthAc * panelA * numPvPanel, 0); // System AC
                    //Kasem check 03-13-2014
                }
                //-----------------------------------------------------------------
                if (chkDailyExp.Checked == true)
                {
                    DialogResult dialogResult = MessageBox.Show("Do you want to export daily production result of data set-" + set.ToString() + " ?", "Hourly energy production", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        SaveFileDialog saveFileDlg = new SaveFileDialog();

                        saveFileDlg.Filter = "csv files (*.csv)|*.csv";
                        saveFileDlg.FilterIndex = 1;
                        saveFileDlg.RestoreDirectory = true;
                        if (saveFileDlg.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                int ii = 0;
                                StreamWriter sw = new StreamWriter(saveFileDlg.FileName);
                                sw.WriteLine("Time (Month/day), Daily production (kW/sqr.m)");
                                for (int month = 1; month <= 12; month++)
                                {
                                    int month_day = System.DateTime.DaysInMonth(2001, month);
                                    for (int day = 1; day <= month_day; day++)
                                    {
                                        sw.WriteLine(DateTimeFormatInfo.CurrentInfo.GetMonthName(month) + "/" + day + ", " + DailyProduct[ii].ToString());
                                        ii++;
                                    }
                                }
                                sw.Close();
                            }
                            catch
                            {
                                MessageBox.Show("The process cannot assess the file '" + saveFileDlg.FileName + "' because it is being used by another process");
                                return;
                            }
                        }
                    }
                }
            }
            #endregion

            #region "Single Station"

            //----------------------------------------------------
            //SINGLE WEATHER STATION
            //----------------------------------------------------
            if (optSingleWeatherSta.Checked == true)
            {
                SSC.Data data = new SSC.Data();
                data.SetString("file_name", weatherFile); //TM2 file
                data.SetNumber("system_size", system_size);
                data.SetNumber("derate", derate);
                data.SetNumber("track_mode", track_mode);
                data.SetNumber("tilt", (int)Tilt);
                data.SetNumber("azimuth", (int)Azimuth);

                SSC.Module mod = new SSC.Module("pvwattsv1");
                if (mod.Exec(data))
                {
                    float tot = data.GetNumber("ac_annual");
                    float[] ac = data.GetArray("ac_monthly");
                    float[] hourlyAc = data.GetArray("ac");
                    float[] DailyProduct = new float[365];
                    //------ daily AC
                    int hh = 0;
                    for (int d = 0; d < 365; d++)
                    {
                        int nHr = 0;
                        float energy = 0;
                        for (int h = 0; h < 24; h++)
                        {
                            if (hourlyAc[h] > 0) nHr++;
                            energy += hourlyAc[hh];
                            hh++;
                        }
                        if (nHr > 0)
                        {
                            DailyProduct[d] = energy / 1000; //Change unit W to KW
                        }
                        else
                        {
                            DailyProduct[d] = 0;
                        }
                    }

                    //this.grdAcProduct.Rows.Clear();
                    for (int month = 1; month <= 12; month++)
                    {
                        int activeRow = grdAcProduct.Rows.Add();
                        this.grdAcProduct.Rows[activeRow].Cells[0].Value = "SET-" + set.ToString("00");
                        this.grdAcProduct.Rows[activeRow].Cells[1].Value = month.ToString("00") + " " + DateTimeFormatInfo.CurrentInfo.GetMonthName(month).ToString();
                        this.grdAcProduct.Rows[activeRow].Cells[2].Value = panelWidth.ToString("0.000");
                        this.grdAcProduct.Rows[activeRow].Cells[3].Value = panelHeight.ToString("0.000");
                        this.grdAcProduct.Rows[activeRow].Cells[4].Value = Math.Round(ac[month - 1], 0);// PVWatts
                        this.grdAcProduct.Rows[activeRow].Cells[5].Value = Tilt.ToString();
                        this.grdAcProduct.Rows[activeRow].Cells[6].Value = Azimuth.ToString();
                        this.grdAcProduct.Rows[activeRow].Cells[7].Value = Math.Round(ac[month - 1] / DCNamplatePerSqrMeter, 2); // per Sqr.Meter
                        this.grdAcProduct.Rows[activeRow].Cells[8].Value = numPvPanel.ToString();
                        this.grdAcProduct.Rows[activeRow].Cells[9].Value = Math.Round(ac[month - 1] * panelA * numPvPanel, 0); // System Ac.


                        //this.grdAcProduct.Rows.Add();
                        //this.grdAcProduct.Rows[month - 1].Cells[0].Value = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
                        //this.grdAcProduct.Rows[month - 1].Cells[1].Value = Math.Round(ac[month - 1], 0);// PVWatts
                        //this.grdAcProduct.Rows[month - 1].Cells[2].Value = Math.Round(ac[month - 1] / DCNamplatePerSqrMeter, 2); // per Sqr.Meter
                        //this.grdAcProduct.Rows[month - 1].Cells[3].Value = Math.Round(ac[month - 1] * panelA * numPvPanel, 0); // System Ac.
                    }
                    //this.grdAcProduct.Refresh();

                    //-----------------------------------------------------------------
                    if (chkDailyExp.Checked == true)
                    {
                        DialogResult dialogResult = MessageBox.Show("Do you want to export the daily production results of data set-" + set.ToString() + " ?", "Hourly energy production", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            SaveFileDialog saveFileDlg = new SaveFileDialog();

                            saveFileDlg.Filter = "csv files (*.csv)|*.csv";
                            saveFileDlg.FilterIndex = 1;
                            saveFileDlg.RestoreDirectory = true;
                            if (saveFileDlg.ShowDialog() == DialogResult.OK)
                            {
                                try
                                {
                                    int ii = 0;
                                    StreamWriter sw = new StreamWriter(saveFileDlg.FileName);
                                    sw.WriteLine("Time (Month/day), Daily production (kW/sqr.m)");
                                    for (int month = 1; month <= 12; month++)
                                    {
                                        int month_day = System.DateTime.DaysInMonth(2001, month);
                                        for (int day = 1; day <= month_day; day++)
                                        {
                                            sw.WriteLine(DateTimeFormatInfo.CurrentInfo.GetMonthName(month) + "/" + day + ", " + DailyProduct[ii].ToString());
                                            ii++;
                                        }
                                    }
                                    sw.Close();
                                }
                                catch
                                {
                                    MessageBox.Show("The process cannot assess the file '" + saveFileDlg.FileName + "' because it is being used by aother process");
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    int idx = 0;
                    String msg;
                    int type;
                    float time;
                    string errStr = "";
                    while (mod.Log(idx, out msg, out type, out time))
                    {
                        String stype = "NOTICE";
                        if (type == SSC.API.WARNING) stype = "WARNING";
                        else if (type == SSC.API.ERROR) stype = "ERROR";
                        errStr += "[ " + stype + " at time:" + time + " ]: " + msg + "\n";
                        idx++;
                    }
                    MessageBox.Show(errStr);
                }
            }
            #endregion

        }

        double[] MonthlyEnergyProduction(double panelWidth, double panelHeight, int numPvPanel, string weatherFile, double tilt, double az)
        {
            double[] MonthlyProduction = new double[12];
            double aunalProduction = 0;
            pvVerify();
            if (verify[0] == false)
            {
                return MonthlyProduction;
            }
            //updateArea();
            float system_size = (float)Convert.ToDouble(txtSystem_size.Text);
            //double panelW = Convert.ToDouble(txtPvWidth.Text);
            //double panelH = Convert.ToDouble(txtPvLength.Text);
            double APerSys = Convert.ToDouble(txtAreaPreSys.Text);
            double DCNamplatePerSqrMeter = APerSys;//25.6 / 4 * Convert.ToDouble(system_size);

            double panelA = panelWidth * panelHeight / DCNamplatePerSqrMeter; //AC factor per panel area
            float derate = (float)Convert.ToDouble(txtDerate.Text);
            int track_mode = formSiteArea.cmbTrack_mode.SelectedIndex;

            #region "Multiple Sta"

            //----------------------------------------------------
            //MULTIPLE WEATHER STATION
            //----------------------------------------------------
            if (optMultiWeatherSta.Checked == true)
            {
                int nIdwSta = Convert.ToInt16(txtNIdwSta.Text);
                double[] rX = new double[nIdwSta];
                double[] rY = new double[nIdwSta];
                double[] rR = new double[nIdwSta];
                float[,] mAC = new float[nIdwSta, 12];
                float[,] dAC = new float[nIdwSta, 365];
                double SiteX = Convert.ToDouble(txtUtmE.Text);
                double SiteY = Convert.ToDouble(txtUtmN.Text);
                double SiteLng = Convert.ToDouble(txtLNG.Text);
                double SiteLat = Convert.ToDouble(txtLAT.Text);
                //----------------------------
                // time  shift
                int siteGMT = Convert.ToInt16(12 / 24 * SiteLng);
                //---------------------------


                for (int idwSta = 0; idwSta < nIdwSta; idwSta++)
                {
                    //---------------------------
                    double lat = wSta[wStaSel[idwSta]].LAT2;
                    double lng = wSta[wStaSel[idwSta]].LONG2;
                    //----------------------------
                    // time  shift
                    int staGMT = Convert.ToInt16(12 / 24 * lng);
                    //---------------------------
                    double[] StaCoord = new double[] { lng, lat };
                    Reproject.ReprojectPoints(StaCoord, new double[] { 0 }, KnownCoordinateSystems.Geographic.World.WGS1984, pvMap.Projection, 0, 1);
                    rR[idwSta] = Math.Sqrt(Math.Pow((SiteX - StaCoord[0]), 2) + Math.Pow((SiteY - StaCoord[1]), 2));
                    rX[idwSta] = Math.Abs(SiteX - StaCoord[0]);
                    rY[idwSta] = Math.Abs(SiteY - StaCoord[1]);
                    //---------------------------
                    SSC.Data data = new SSC.Data();
                    data.SetString("file_name", pvDir + "\\WeatherSta\\tm2\\" + wSta[wStaSel[idwSta]].FileName); //TM2 file
                    data.SetNumber("system_size", system_size);
                    data.SetNumber("derate", derate);
                    data.SetNumber("track_mode", track_mode);
                    data.SetNumber("tilt", (int)tilt);
                    data.SetNumber("azimuth", (int)az);

                    SSC.Module mod = new SSC.Module("pvwattsv1");
                    if (mod.Exec(data))
                    {
                        float tot = data.GetNumber("ac_annual");
                        float[] ac = data.GetArray("ac_monthly");
                        float[] hourlyAc = data.GetArray("ac");
                        for (int m = 0; m < 12; m++)
                        {
                            mAC[idwSta, m] = ac[m];
                        }
                        //------ daily AC
                        int hh = 0;
                        for (int d = 0; d < 365; d++)
                        {
                            int nHr = 0;
                            float energy = 0;
                            for (int h = 0; h < 24; h++)
                            {
                                if (hourlyAc[h] > 0) nHr++;
                                energy += hourlyAc[hh];
                                hh++;
                            }
                            if (nHr > 0)
                            {
                                dAC[idwSta, d] = energy / 1000; //Change unit W to KW
                            }
                            else
                            {
                                dAC[idwSta, d] = 0;
                            }
                        }
                    }
                }
                //-------------------------------------------------------
                // IDW
                //-------------------------------------------------------
                double[] MonthlyProduct = new double[12];
                double[] DailyProduct = new double[365];
                double px = Convert.ToDouble(txtPowX.Text);
                double py = Convert.ToDouble(txtPowY.Text);
                double sumR = 0;
                for (int idwSta = 0; idwSta < nIdwSta; idwSta++)
                {
                    double rPow = 1 / (Math.Pow(rX[idwSta] / 1000, px) * Math.Pow(rY[idwSta] / 1000, py));
                    sumR += rPow;
                }
                for (int m = 0; m < 12; m++)
                {
                    double sumRZ = 0;
                    for (int idwSta = 0; idwSta < nIdwSta; idwSta++)
                    {
                        double rPowZ = mAC[idwSta, m] / (Math.Pow(rX[idwSta] / 1000, px) * Math.Pow(rY[idwSta] / 1000, py));
                        sumRZ += rPowZ;
                    }
                    MonthlyProduct[m] = sumRZ / sumR;
                }
                for (int d = 0; d < 365; d++)
                {
                    double sumRZ = 0;
                    for (int idwSta = 0; idwSta < nIdwSta; idwSta++)
                    {
                        double rPowZ = dAC[idwSta, d] / (Math.Pow(rX[idwSta] / 1000, px) * Math.Pow(rY[idwSta] / 1000, py));
                        sumRZ += rPowZ;
                    }
                    DailyProduct[d] = sumRZ / sumR;
                }
                //-------------------------------------------------------
                // Reprot
                //-------------------------------------------------------
                for (int month = 1; month <= 12; month++)
                {
                    //---------------------------------------------
                    double MonthAc = 0;
                    //MonthAc = MonthAc / dOfMonth(month);
                    MonthAc = MonthlyProduct[month - 1];
                    //acProduction[month - 1] = MonthAc;
                    //---------------------------------------------
                    MonthlyProduction[month - 1] = Math.Round(MonthAc * panelA * numPvPanel, 0); // System AC
                }
                return MonthlyProduction;
            }
            #endregion

            #region "Single Station"

            //----------------------------------------------------
            //SINGLE WEATHER STATION
            //----------------------------------------------------
            if (optSingleWeatherSta.Checked == true)
            {
                SSC.Data data = new SSC.Data();
                data.SetString("file_name", weatherFile); //TM2 file
                data.SetNumber("system_size", system_size);
                data.SetNumber("derate", derate);
                data.SetNumber("track_mode", track_mode);
                data.SetNumber("tilt", (int)tilt);
                data.SetNumber("azimuth", (int)az);

                SSC.Module mod = new SSC.Module("pvwattsv1");
                if (mod.Exec(data))
                {
                    float tot = data.GetNumber("ac_annual");
                    float[] ac = data.GetArray("ac_monthly");
                    float[] hourlyAc = data.GetArray("ac");
                    float[] DailyProduct = new float[365];
                    //------ daily AC
                    int hh = 0;
                    for (int d = 0; d < 365; d++)
                    {
                        int nHr = 0;
                        float energy = 0;
                        for (int h = 0; h < 24; h++)
                        {
                            if (hourlyAc[h] > 0) nHr++;
                            energy += hourlyAc[hh];
                            hh++;
                        }
                        if (nHr > 0)
                        {
                            DailyProduct[d] = energy / 1000; //Change unit W to KW
                        }
                        else
                        {
                            DailyProduct[d] = 0;
                        }
                    }

                    for (int month = 1; month <= 12; month++)
                    {
                        MonthlyProduction[month - 1] = Math.Round(ac[month - 1] * panelA * numPvPanel, 0); // System Ac.
                    }
                    return MonthlyProduction;
                }
            #endregion
            }
            return MonthlyProduction;
        }

        double AnualEnergyProduction(double panelWidth, double panelHeight, int numPvPanel, string weatherFile, double tilt, double az)
        {
            double aunalProduction = 0;
            pvVerify();
            if (verify[0] == false)
            {
                return 0;
            }
            float system_size = (float)Convert.ToDouble(txtSystem_size.Text);
            // = Convert.ToDouble(txtPvWidth.Text);
            // = Convert.ToDouble(txtPvLength.Text);
            double APerSys = Convert.ToDouble(txtAreaPreSys.Text);
            double DCNamplatePerSqrMeter = APerSys;//25.6 / 4 * Convert.ToDouble(system_size);

            double panelA = panelWidth * panelHeight / DCNamplatePerSqrMeter; //AC factor per panel area
            float derate = (float)Convert.ToDouble(txtDerate.Text);
            int track_mode = formSiteArea.cmbTrack_mode.SelectedIndex;

            #region "Multiple Sta"

            //----------------------------------------------------
            //MULTIPLE WEATHER STATION
            //----------------------------------------------------
            if (optMultiWeatherSta.Checked == true)
            {
                int nIdwSta = Convert.ToInt16(txtNIdwSta.Text);
                double[] rX = new double[nIdwSta];
                double[] rY = new double[nIdwSta];
                double[] rR = new double[nIdwSta];
                float[,] mAC = new float[nIdwSta, 12];
                float[,] dAC = new float[nIdwSta, 365];
                double SiteX = Convert.ToDouble(txtUtmE.Text);
                double SiteY = Convert.ToDouble(txtUtmN.Text);
                double SiteLng = Convert.ToDouble(txtLNG.Text);
                double SiteLat = Convert.ToDouble(txtLAT.Text);
                //----------------------------
                // time  shift
                int siteGMT = Convert.ToInt16(12 / 24 * SiteLng);
                //---------------------------


                for (int idwSta = 0; idwSta < nIdwSta; idwSta++)
                {
                    //---------------------------
                    double lat = wSta[wStaSel[idwSta]].LAT2;
                    double lng = wSta[wStaSel[idwSta]].LONG2;
                    //----------------------------
                    // time  shift
                    int staGMT = Convert.ToInt16(12 / 24 * lng);
                    //---------------------------
                    double[] StaCoord = new double[] { lng, lat };
                    Reproject.ReprojectPoints(StaCoord, new double[] { 0 }, KnownCoordinateSystems.Geographic.World.WGS1984, pvMap.Projection, 0, 1);
                    rR[idwSta] = Math.Sqrt(Math.Pow((SiteX - StaCoord[0]), 2) + Math.Pow((SiteY - StaCoord[1]), 2));
                    rX[idwSta] = Math.Abs(SiteX - StaCoord[0]);
                    rY[idwSta] = Math.Abs(SiteY - StaCoord[1]);
                    //---------------------------
                    SSC.Data data = new SSC.Data();
                    data.SetString("file_name", pvDir + "\\WeatherSta\\tm2\\" + wSta[wStaSel[idwSta]].FileName); //TM2 file
                    data.SetNumber("system_size", system_size);
                    data.SetNumber("derate", derate);
                    data.SetNumber("track_mode", track_mode);
                    data.SetNumber("tilt", (int)tilt);
                    data.SetNumber("azimuth", (int)az);

                    SSC.Module mod = new SSC.Module("pvwattsv1");
                    if (mod.Exec(data))
                    {
                        float tot = data.GetNumber("ac_annual");
                        float[] ac = data.GetArray("ac_monthly");
                        float[] hourlyAc = data.GetArray("ac");
                        for (int m = 0; m < 12; m++)
                        {
                            mAC[idwSta, m] = ac[m];
                        }
                        //------ daily AC
                        int hh = 0;
                        for (int d = 0; d < 365; d++)
                        {
                            int nHr = 0;
                            float energy = 0;
                            for (int h = 0; h < 24; h++)
                            {
                                if (hourlyAc[h] > 0) nHr++;
                                energy += hourlyAc[hh];
                                hh++;
                            }
                            if (nHr > 0)
                            {
                                dAC[idwSta, d] = energy / 1000; //Change unit W to KW
                            }
                            else
                            {
                                dAC[idwSta, d] = 0;
                            }
                        }
                    }
                }
                //-------------------------------------------------------
                // IDW
                //-------------------------------------------------------
                double[] MonthlyProduct = new double[12];
                double[] DailyProduct = new double[365];
                double px = Convert.ToDouble(txtPowX.Text);
                double py = Convert.ToDouble(txtPowY.Text);
                double sumR = 0;
                for (int idwSta = 0; idwSta < nIdwSta; idwSta++)
                {
                    double rPow = 1 / (Math.Pow(rX[idwSta] / 1000, px) * Math.Pow(rY[idwSta] / 1000, py));
                    sumR += rPow;
                }
                for (int m = 0; m < 12; m++)
                {
                    double sumRZ = 0;
                    for (int idwSta = 0; idwSta < nIdwSta; idwSta++)
                    {
                        double rPowZ = mAC[idwSta, m] / (Math.Pow(rX[idwSta] / 1000, px) * Math.Pow(rY[idwSta] / 1000, py));
                        sumRZ += rPowZ;
                    }
                    MonthlyProduct[m] = sumRZ / sumR;
                }
                for (int d = 0; d < 365; d++)
                {
                    double sumRZ = 0;
                    for (int idwSta = 0; idwSta < nIdwSta; idwSta++)
                    {
                        double rPowZ = dAC[idwSta, d] / (Math.Pow(rX[idwSta] / 1000, px) * Math.Pow(rY[idwSta] / 1000, py));
                        sumRZ += rPowZ;
                    }
                    DailyProduct[d] = sumRZ / sumR;
                }
                //-------------------------------------------------------
                // Reprot
                //-------------------------------------------------------
                for (int month = 1; month <= 12; month++)
                {
                    //---------------------------------------------
                    double MonthAc = 0;
                    //MonthAc = MonthAc / dOfMonth(month);
                    MonthAc = MonthlyProduct[month - 1];
                    //acProduction[month - 1] = MonthAc;
                    //---------------------------------------------
                    aunalProduction += Math.Round(MonthAc * panelA * numPvPanel, 0); // System AC
                }
                return aunalProduction;
            }
            #endregion

            #region "Single Station"

            //----------------------------------------------------
            //SINGLE WEATHER STATION
            //----------------------------------------------------
            if (optSingleWeatherSta.Checked == true)
            {
                SSC.Data data = new SSC.Data();
                data.SetString("file_name", weatherFile); //TM2 file
                data.SetNumber("system_size", system_size);
                data.SetNumber("derate", derate);
                data.SetNumber("track_mode", track_mode);
                data.SetNumber("tilt", (int)tilt);
                data.SetNumber("azimuth", (int)az);

                SSC.Module mod = new SSC.Module("pvwattsv1");
                if (mod.Exec(data))
                {
                    float tot = data.GetNumber("ac_annual");
                    float[] ac = data.GetArray("ac_monthly");
                    float[] hourlyAc = data.GetArray("ac");
                    float[] DailyProduct = new float[365];
                    //------ daily AC
                    int hh = 0;
                    for (int d = 0; d < 365; d++)
                    {
                        int nHr = 0;
                        float energy = 0;
                        for (int h = 0; h < 24; h++)
                        {
                            if (hourlyAc[h] > 0) nHr++;
                            energy += hourlyAc[hh];
                            hh++;
                        }
                        if (nHr > 0)
                        {
                            DailyProduct[d] = energy / 1000; //Change unit W to KW
                        }
                        else
                        {
                            DailyProduct[d] = 0;
                        }
                    }

                    for (int month = 1; month <= 12; month++)
                    {
                        aunalProduction += Math.Round(ac[month - 1] * panelA * numPvPanel, 0); // System Ac.
                    }
                    return aunalProduction;
                }
            #endregion
            }
            return 0;
        }

        double AnualEnergyProduction(int numPvPanel, string weatherFile, double tilt, double az)
        {
            double aunalProduction = 0;
            pvVerify();
            if (verify[0] == false)
            {
                return 0;
            }
            //updateArea();
            float system_size = (float)Convert.ToDouble(txtSystem_size.Text);
            double panelW = Convert.ToDouble(txtPvWidth.Text);
            double panelH = Convert.ToDouble(txtPvLength.Text);
            double APerSys = Convert.ToDouble(txtAreaPreSys.Text);
            double DCNamplatePerSqrMeter = APerSys;//25.6 / 4 * Convert.ToDouble(system_size);

            double panelA = panelW * panelH / DCNamplatePerSqrMeter; //AC factor per panel area
            float derate = (float)Convert.ToDouble(txtDerate.Text);
            int track_mode = formSiteArea.cmbTrack_mode.SelectedIndex;

            #region "Multiple Sta"

            //----------------------------------------------------
            //MULTIPLE WEATHER STATION
            //----------------------------------------------------
            if (optMultiWeatherSta.Checked == true)
            {
                int nIdwSta = Convert.ToInt16(txtNIdwSta.Text);
                double[] rX = new double[nIdwSta];
                double[] rY = new double[nIdwSta];
                double[] rR = new double[nIdwSta];
                float[,] mAC = new float[nIdwSta, 12];
                float[,] dAC = new float[nIdwSta, 365];
                double SiteX = Convert.ToDouble(txtUtmE.Text);
                double SiteY = Convert.ToDouble(txtUtmN.Text);
                double SiteLng = Convert.ToDouble(txtLNG.Text);
                double SiteLat = Convert.ToDouble(txtLAT.Text);
                //----------------------------
                // time  shift
                int siteGMT = Convert.ToInt16(12 / 24 * SiteLng);
                //---------------------------


                for (int idwSta = 0; idwSta < nIdwSta; idwSta++)
                {
                    //---------------------------
                    double lat = wSta[wStaSel[idwSta]].LAT2;
                    double lng = wSta[wStaSel[idwSta]].LONG2;
                    //----------------------------
                    // time  shift
                    int staGMT = Convert.ToInt16(12 / 24 * lng);
                    //---------------------------
                    double[] StaCoord = new double[] { lng, lat };
                    Reproject.ReprojectPoints(StaCoord, new double[] { 0 }, KnownCoordinateSystems.Geographic.World.WGS1984, pvMap.Projection, 0, 1);
                    rR[idwSta] = Math.Sqrt(Math.Pow((SiteX - StaCoord[0]), 2) + Math.Pow((SiteY - StaCoord[1]), 2));
                    rX[idwSta] = Math.Abs(SiteX - StaCoord[0]);
                    rY[idwSta] = Math.Abs(SiteY - StaCoord[1]);
                    //---------------------------
                    SSC.Data data = new SSC.Data();
                    data.SetString("file_name", pvDir + "\\WeatherSta\\tm2\\" + wSta[wStaSel[idwSta]].FileName); //TM2 file
                    data.SetNumber("system_size", system_size);
                    data.SetNumber("derate", derate);
                    data.SetNumber("track_mode", track_mode);
                    data.SetNumber("tilt", (int)tilt);
                    data.SetNumber("azimuth", (int)az);

                    SSC.Module mod = new SSC.Module("pvwattsv1");
                    if (mod.Exec(data))
                    {
                        float tot = data.GetNumber("ac_annual");
                        float[] ac = data.GetArray("ac_monthly");
                        float[] hourlyAc = data.GetArray("ac");
                        for (int m = 0; m < 12; m++)
                        {
                            mAC[idwSta, m] = ac[m];
                        }
                        //------ daily AC
                        int hh = 0;
                        for (int d = 0; d < 365; d++)
                        {
                            int nHr = 0;
                            float energy = 0;
                            for (int h = 0; h < 24; h++)
                            {
                                if (hourlyAc[h] > 0) nHr++;
                                energy += hourlyAc[hh];
                                hh++;
                            }
                            if (nHr > 0)
                            {
                                dAC[idwSta, d] = energy / 1000; //Change unit W to KW
                            }
                            else
                            {
                                dAC[idwSta, d] = 0;
                            }
                        }
                    }
                }
                //-------------------------------------------------------
                // IDW
                //-------------------------------------------------------
                double[] MonthlyProduct = new double[12];
                double[] DailyProduct = new double[365];
                double px = Convert.ToDouble(txtPowX.Text);
                double py = Convert.ToDouble(txtPowY.Text);
                double sumR = 0;
                for (int idwSta = 0; idwSta < nIdwSta; idwSta++)
                {
                    double rPow = 1 / (Math.Pow(rX[idwSta] / 1000, px) * Math.Pow(rY[idwSta] / 1000, py));
                    sumR += rPow;
                }
                for (int m = 0; m < 12; m++)
                {
                    double sumRZ = 0;
                    for (int idwSta = 0; idwSta < nIdwSta; idwSta++)
                    {
                        double rPowZ = mAC[idwSta, m] / (Math.Pow(rX[idwSta] / 1000, px) * Math.Pow(rY[idwSta] / 1000, py));
                        sumRZ += rPowZ;
                    }
                    MonthlyProduct[m] = sumRZ / sumR;
                }
                for (int d = 0; d < 365; d++)
                {
                    double sumRZ = 0;
                    for (int idwSta = 0; idwSta < nIdwSta; idwSta++)
                    {
                        double rPowZ = dAC[idwSta, d] / (Math.Pow(rX[idwSta] / 1000, px) * Math.Pow(rY[idwSta] / 1000, py));
                        sumRZ += rPowZ;
                    }
                    DailyProduct[d] = sumRZ / sumR;
                }
                //-------------------------------------------------------
                // Reprot
                //-------------------------------------------------------
                for (int month = 1; month <= 12; month++)
                {
                    //---------------------------------------------
                    double MonthAc = 0;
                    //MonthAc = MonthAc / dOfMonth(month);
                    MonthAc = MonthlyProduct[month - 1];
                    //acProduction[month - 1] = MonthAc;
                    //---------------------------------------------
                    aunalProduction += Math.Round(MonthAc * panelA * numPvPanel, 0); // System AC
                }
                return aunalProduction;
            }
            #endregion

            #region "Single Station"

            //----------------------------------------------------
            //SINGLE WEATHER STATION
            //----------------------------------------------------
            if (optSingleWeatherSta.Checked == true)
            {
                SSC.Data data = new SSC.Data();
                data.SetString("file_name", weatherFile); //TM2 file
                data.SetNumber("system_size", system_size);
                data.SetNumber("derate", derate);
                data.SetNumber("track_mode", track_mode);
                data.SetNumber("tilt", (int)tilt);
                data.SetNumber("azimuth", (int)az);

                SSC.Module mod = new SSC.Module("pvwattsv1");
                if (mod.Exec(data))
                {
                    float tot = data.GetNumber("ac_annual");
                    float[] ac = data.GetArray("ac_monthly");
                    float[] hourlyAc = data.GetArray("ac");
                    float[] DailyProduct = new float[365];
                    //------ daily AC
                    int hh = 0;
                    for (int d = 0; d < 365; d++)
                    {
                        int nHr = 0;
                        float energy = 0;
                        for (int h = 0; h < 24; h++)
                        {
                            if (hourlyAc[h] > 0) nHr++;
                            energy += hourlyAc[hh];
                            hh++;
                        }
                        if (nHr > 0)
                        {
                            DailyProduct[d] = energy / 1000; //Change unit W to KW
                        }
                        else
                        {
                            DailyProduct[d] = 0;
                        }
                    }

                    for (int month = 1; month <= 12; month++)
                    {
                        aunalProduction += Math.Round(ac[month - 1] * panelA * numPvPanel, 0); // System Ac.
                    }
                    return aunalProduction;
                }
            #endregion
            }
            return 0;
        }

        double[] MonthlyEnergyProduction(int numPvPanel, string weatherFile, double tilt, double az)
        {
            double[] MonthlyProduction = new double[12];
            double aunalProduction = 0;
            pvVerify();
            if (verify[0] == false)
            {
                return MonthlyProduction;
            }
            //updateArea();
            float system_size = (float)Convert.ToDouble(txtSystem_size.Text);
            double panelW = Convert.ToDouble(txtPvWidth.Text);
            double panelH = Convert.ToDouble(txtPvLength.Text);
            double APerSys = Convert.ToDouble(txtAreaPreSys.Text);
            double DCNamplatePerSqrMeter = APerSys;// 25.6/ 4 * Convert.ToDouble(system_size);

            double panelA = panelW * panelH / DCNamplatePerSqrMeter; //AC factor per panel area
            float derate = (float)Convert.ToDouble(txtDerate.Text);
            int track_mode = formSiteArea.cmbTrack_mode.SelectedIndex;

            #region "Multiple Sta"

            //----------------------------------------------------
            //MULTIPLE WEATHER STATION
            //----------------------------------------------------
            if (optMultiWeatherSta.Checked == true)
            {
                int nIdwSta = Convert.ToInt16(txtNIdwSta.Text);
                double[] rX = new double[nIdwSta];
                double[] rY = new double[nIdwSta];
                double[] rR = new double[nIdwSta];
                float[,] mAC = new float[nIdwSta, 12];
                float[,] dAC = new float[nIdwSta, 365];
                double SiteX = Convert.ToDouble(txtUtmE.Text);
                double SiteY = Convert.ToDouble(txtUtmN.Text);
                double SiteLng = Convert.ToDouble(txtLNG.Text);
                double SiteLat = Convert.ToDouble(txtLAT.Text);
                //----------------------------
                // time  shift
                int siteGMT = Convert.ToInt16(12 / 24 * SiteLng);
                //---------------------------


                for (int idwSta = 0; idwSta < nIdwSta; idwSta++)
                {
                    //---------------------------
                    double lat = wSta[wStaSel[idwSta]].LAT2;
                    double lng = wSta[wStaSel[idwSta]].LONG2;
                    //----------------------------
                    // time  shift
                    int staGMT = Convert.ToInt16(12 / 24 * lng);
                    //---------------------------
                    double[] StaCoord = new double[] { lng, lat };
                    Reproject.ReprojectPoints(StaCoord, new double[] { 0 }, KnownCoordinateSystems.Geographic.World.WGS1984, pvMap.Projection, 0, 1);
                    rR[idwSta] = Math.Sqrt(Math.Pow((SiteX - StaCoord[0]), 2) + Math.Pow((SiteY - StaCoord[1]), 2));
                    rX[idwSta] = Math.Abs(SiteX - StaCoord[0]);
                    rY[idwSta] = Math.Abs(SiteY - StaCoord[1]);
                    //---------------------------
                    SSC.Data data = new SSC.Data();
                    data.SetString("file_name", pvDir + "\\WeatherSta\\tm2\\" + wSta[wStaSel[idwSta]].FileName); //TM2 file
                    data.SetNumber("system_size", system_size);
                    data.SetNumber("derate", derate);
                    data.SetNumber("track_mode", track_mode);
                    data.SetNumber("tilt", (int)tilt);
                    data.SetNumber("azimuth", (int)az);

                    SSC.Module mod = new SSC.Module("pvwattsv1");
                    if (mod.Exec(data))
                    {
                        float tot = data.GetNumber("ac_annual");
                        float[] ac = data.GetArray("ac_monthly");
                        float[] hourlyAc = data.GetArray("ac");
                        for (int m = 0; m < 12; m++)
                        {
                            mAC[idwSta, m] = ac[m];
                        }
                        //------ daily AC
                        int hh = 0;
                        for (int d = 0; d < 365; d++)
                        {
                            int nHr = 0;
                            float energy = 0;
                            for (int h = 0; h < 24; h++)
                            {
                                if (hourlyAc[h] > 0) nHr++;
                                energy += hourlyAc[hh];
                                hh++;
                            }
                            if (nHr > 0)
                            {
                                dAC[idwSta, d] = energy / 1000; //Change unit W to KW
                            }
                            else
                            {
                                dAC[idwSta, d] = 0;
                            }
                        }
                    }
                }
                //-------------------------------------------------------
                // IDW
                //-------------------------------------------------------
                double[] MonthlyProduct = new double[12];
                double[] DailyProduct = new double[365];
                double px = Convert.ToDouble(txtPowX.Text);
                double py = Convert.ToDouble(txtPowY.Text);
                double sumR = 0;
                for (int idwSta = 0; idwSta < nIdwSta; idwSta++)
                {
                    double rPow = 1 / (Math.Pow(rX[idwSta] / 1000, px) * Math.Pow(rY[idwSta] / 1000, py));
                    sumR += rPow;
                }
                for (int m = 0; m < 12; m++)
                {
                    double sumRZ = 0;
                    for (int idwSta = 0; idwSta < nIdwSta; idwSta++)
                    {
                        double rPowZ = mAC[idwSta, m] / (Math.Pow(rX[idwSta] / 1000, px) * Math.Pow(rY[idwSta] / 1000, py));
                        sumRZ += rPowZ;
                    }
                    MonthlyProduct[m] = sumRZ / sumR;
                }
                for (int d = 0; d < 365; d++)
                {
                    double sumRZ = 0;
                    for (int idwSta = 0; idwSta < nIdwSta; idwSta++)
                    {
                        double rPowZ = dAC[idwSta, d] / (Math.Pow(rX[idwSta] / 1000, px) * Math.Pow(rY[idwSta] / 1000, py));
                        sumRZ += rPowZ;
                    }
                    DailyProduct[d] = sumRZ / sumR;
                }
                //-------------------------------------------------------
                // Reprot
                //-------------------------------------------------------
                for (int month = 1; month <= 12; month++)
                {
                    //---------------------------------------------
                    double MonthAc = 0;
                    //MonthAc = MonthAc / dOfMonth(month);
                    MonthAc = MonthlyProduct[month - 1];
                    //acProduction[month - 1] = MonthAc;
                    //---------------------------------------------
                    MonthlyProduction[month - 1] = Math.Round(MonthAc * panelA * numPvPanel, 0); // System AC
                }
                return MonthlyProduction;
            }
            #endregion

            #region "Single Station"

            //----------------------------------------------------
            //SINGLE WEATHER STATION
            //----------------------------------------------------
            if (optSingleWeatherSta.Checked == true)
            {
                SSC.Data data = new SSC.Data();
                data.SetString("file_name", weatherFile); //TM2 file
                data.SetNumber("system_size", system_size);
                data.SetNumber("derate", derate);
                data.SetNumber("track_mode", track_mode);
                data.SetNumber("tilt", (int)tilt);
                data.SetNumber("azimuth", (int)az);

                SSC.Module mod = new SSC.Module("pvwattsv1");
                if (mod.Exec(data))
                {
                    float tot = data.GetNumber("ac_annual");
                    float[] ac = data.GetArray("ac_monthly");
                    float[] hourlyAc = data.GetArray("ac");
                    float[] DailyProduct = new float[365];
                    //------ daily AC
                    int hh = 0;
                    for (int d = 0; d < 365; d++)
                    {
                        int nHr = 0;
                        float energy = 0;
                        for (int h = 0; h < 24; h++)
                        {
                            if (hourlyAc[h] > 0) nHr++;
                            energy += hourlyAc[hh];
                            hh++;
                        }
                        if (nHr > 0)
                        {
                            DailyProduct[d] = energy / 1000; //Change unit W to KW
                        }
                        else
                        {
                            DailyProduct[d] = 0;
                        }
                    }

                    for (int month = 1; month <= 12; month++)
                    {
                        MonthlyProduction[month - 1] = Math.Round(ac[month - 1] * panelA * numPvPanel, 0); // System Ac.
                    }
                    return MonthlyProduction;
                }
            #endregion
            }
            return MonthlyProduction;
        }


        #endregion "AC calculation-----------------------------------------------------"

        #region "Graph"

        void OptimizeGraphPlot(ZedGraphControl zedG, List<PointPairList> ls, List<string> legend, string title, string YAxis, string XAxis, int MaxX, int MajorStep, int MinorStep)
        {

            UpdateProgressBar.Visible = true;
            GraphPane myPane = zedG.GraphPane;
            zedG.GraphPane.CurveList.Clear();
            zedG.GraphPane.GraphObjList.Clear();
            // Set the titles and axis labels
            myPane.Title.Text = title;
            myPane.YAxis.Title.Text = YAxis;
            myPane.XAxis.Title.Text = XAxis;

            //List<PointPairList> ls = new List<PointPairList>();
            LineItem myCurve;
            int jj = 0;
            UpdateProgressBar.Value = 0;
            UpdateProgressBar.Maximum = 9 * 37;
            for (int i = 0; i < ls.Count; i++)
            {
                double az = i * 250 / ls.Count;
                myCurve = myPane.AddCurve(legend[i], ls[i], Color.FromArgb((int)az, 255 - (int)az, 255), (ZedGraph.SymbolType)jj);
                jj++;
                if (jj > 10) jj = 0;
            }
            UpdateProgressBar.Visible = false;
            // Show the x axis grid
            myPane.XAxis.Scale.MajorStep = MajorStep;
            myPane.XAxis.Scale.MinorStep = MinorStep;
            myPane.XAxis.MajorGrid.DashOff = 2;
            myPane.XAxis.MajorGrid.DashOn = 5;
            myPane.YAxis.MajorGrid.IsVisible = true;
            if (MaxX == 0)
            {
                myPane.XAxis.Scale.MaxAuto = true;
            }
            else
            {
                myPane.XAxis.Scale.MaxAuto = false;
                myPane.XAxis.Scale.Max = MaxX;
            }
            // Make the Y axis scale red
            myPane.YAxis.Scale.FontSpec.FontColor = Color.Red;
            myPane.YAxis.Title.FontSpec.FontColor = Color.Red;
            // turn off the opposite tics so the Y tics don't show up on the Y2 axis
            myPane.YAxis.MajorTic.IsOpposite = false;
            myPane.YAxis.MinorTic.IsOpposite = false;
            // Don't display the Y zero line
            myPane.YAxis.MajorGrid.IsZeroLine = false;
            // Align the Y axis labels so they are flush to the axis
            myPane.YAxis.Scale.Align = AlignP.Inside;
            // Fill the axis background with a gradient
            myPane.Chart.Fill = new Fill(Color.White, Color.LightGray, 45.0f);

            // Calculate the Axis Scale Ranges
            zedG.AxisChange();
            zedG.Refresh();
        }

        #endregion

        #region "Switch map/table"

        private void SwitchtoTable()
        {
            TabGOptimize.Visible = false;
            cmdSwithToMap.Visible = true;
            cmdSwithToGraph.Visible = true;
            cmdSwithToTable.Visible = false;
            grdAcProduct.Visible = true;
            pnlGrdProduction.Visible = true;
            cmdMapZoomIn.Visible = false;
            cmdMapPan.Visible = false;
            cmdMapZoomOut.Visible = false;
            cmdMapZoomExt.Visible = false;
            cmdMapSelection.Visible = false;
            cmdMapSelectionNone.Visible = false;
            splitContainer3.SplitterDistance = 20;
        }

        private void cmdSwithToTable_Click(object sender, EventArgs e)
        {
            SwitchtoTable();
        }

        private void cmdSwithToMap_Click(object sender, EventArgs e)
        {
            switchtoMap();
        }
        private void switchtoMap()
        {
            TabGOptimize.Visible = false;
            cmdSwithToTable.Visible = true;
            cmdSwithToGraph.Visible = false;
            cmdSwithToMap.Visible = false;
            grdAcProduct.Visible = false;
            pnlGrdProduction.Visible = false;
            cmdMapZoomIn.Visible = true;
            cmdMapPan.Visible = true;
            cmdMapZoomOut.Visible = true;
            cmdMapZoomExt.Visible = true;
            cmdMapSelection.Visible = true;
            cmdMapSelectionNone.Visible = true;
        }
        #endregion "Switch map/table"

        private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            optSingleWeatherSta.Checked = true;
            txtTM2.Clear();
            for (int i = 0; i < nSta; i++)
            {
                if (wSta[i].State == cmbState.Text & wSta[i].City == cmbCity.Text)
                {
                    txtTM2.Text = System.IO.Path.GetFullPath(pvDir + "\\WeatherSta\\tm2\\" + wSta[i].FileName);
                    txtElev.Text = wSta[i].Elev.ToString();
                }
            }
        }

        private void cmbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCity.Items.Clear();
            UserDefined = false;
            for (int i = 0; i < nSta; i++)
            {
                if (wSta[i].State == cmbState.Text)
                {
                    cmbCity.Items.Add(wSta[i].City);
                    cmbCity.Text = wSta[i].City;
                }
            }

        }

        private void cmdForTest_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Add(this.cmdMapZoomIn);

            // MessageBox.Show(pvDir);
            /* 
             if (optMultiWeatherSta.Checked == true | optSingleWeatherSta.Checked == true)
             {
                 if (System.IO.File.Exists(txtTM2.Text) == true)
                 {
                    // EnergyProduction(2, 2, 45, 180, 100, txtTM2.Text);
                 }
                 else
                 {
                     MessageBox.Show("Weather file is incorrect");
                 }
             }
             */
        }

        private void cmdRooftopAcCalculation_Click(object sender, EventArgs e)
        {
            CheckWorkingPath();

            if (prj.Path != "")
            {
                if (verify[3] == false)
                {
                    MessageBox.Show("Please calculate solar properties first");
                    return;
                }
                if (formSiteArea.cmbTrack_mode.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select tracking mode before calculating");
                    return;
                }
                pvMap.MapFrame.DrawingLayers.Clear();
                prj.LyrPvPanel = util.getLayerHdl(cmbRoofTopPanelPanel.Text, pvMap);
                if (prj.LyrPvPanel != -1)
                {
                    //-----------------------------------------------------
                    //IFeatureSet pvfs;
                    //pvfs = new FeatureSet(FeatureType.Polygon);
                    //-----------------------------------------------------
                    FeatureSet pvPanelFe = pvMap.Layers[prj.LyrPvPanel].DataSet as FeatureSet;
                    //MessageBox.Show("Number of pole = " + poleFe.DataSet.NumRows());
                    int nShp = pvPanelFe.NumRows();
                    string[] pvCase = new string[nShp];
                    for (int i = 0; i < nShp; i++)
                    {
                        IFeature iFe = pvPanelFe.GetFeature(i);
                        object valH = iFe.DataRow["h"];
                        object valW = iFe.DataRow["w"];
                        object valAz = iFe.DataRow["Azimuth"];
                        object valTilt = iFe.DataRow["Ele_Angle"];
                        //---------------------------------------------------
                        string h = Convert.ToString(valH);
                        string w = Convert.ToString(valW);
                        string Az = Convert.ToString(valAz);
                        string tilt = Convert.ToString(valTilt);
                        //---------------------------------------------------
                        pvCase[i] = w + "," + h + "," + Az + "," + tilt;
                    }
                    string[] case4run = pvCase.Distinct().ToArray();
                    int[] numPanel = new int[case4run.Length];
                    double[] width = new double[case4run.Length];
                    double[] height = new double[case4run.Length];
                    double[] Azimuth = new double[case4run.Length];
                    double[] Tilt = new double[case4run.Length];

                    for (int i = 0; i < case4run.Length; i++)
                    {
                        numPanel[i] = 0;
                        string[] v = case4run[i].Split(',');
                        width[i] = Convert.ToDouble(v[0]);
                        height[i] = Convert.ToDouble(v[1]);
                        Azimuth[i] = Convert.ToDouble(v[2]);
                        Tilt[i] = Convert.ToDouble(v[3]);
                        if (prj.Latiude >= 0) //
                        {

                            if (Tilt[i] < 0)
                            {
                                MessageBox.Show("The location of site is above the mercator, tilt direction was wrong but has been corrected!!", "PV Data set-" + i.ToString() + " error has been corrected");
                                Tilt[i] = Math.Abs(Tilt[i]);
                            }
                        }
                        else
                        {
                            if (Tilt[i] > 0)
                            {
                                MessageBox.Show("The location of site is below the mercator, tilt direction was wrong but has been corrected!!", "PV Data set-" + i.ToString() + " error has been corrected");
                                Tilt[i] = Math.Abs(Tilt[i]);
                            }
                        }

                        for (int j = 0; j < pvCase.Length; j++)
                        {
                            if (case4run[i] == pvCase[j])
                            {
                                numPanel[i]++;
                            }
                        }
                    }

                    /*
                    if (optPvWattFunc.Checked == true)
                    {
                        EnergyProduction(numPvPanel);
                        cmdReport.Enabled = true;
                        cmdOptimization.Enabled = true;
                    }
                     */
                    if (optMultiWeatherSta.Checked == true | optSingleWeatherSta.Checked == true)
                    {
                        if (System.IO.File.Exists(txtTM2.Text) == true)
                        {
                            grdAcProduct.Rows.Clear();
                            for (int i = 0; i < case4run.Length; i++)
                            {
                                //Make sure Tilt must be positvie value
                                Tilt[i] = Math.Abs(Tilt[i]);
                                EnergyProduction(i, width[i], height[i], Tilt[i], Azimuth[i], numPanel[i], txtTM2.Text);
                                //--------------------------------
                                //Todo: Add accumulate data result 

                            }


                            cmdSwithToMap.Visible = true;
                            cmdSwithToTable.Visible = false;
                            grdAcProduct.Visible = true;
                            pnlGrdProduction.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("Weather file is incorrect");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please assign the PV position layer before continuing");
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void cmdExportSketchUp_Click(object sender, EventArgs e)
        {
            pvVerify();
            if (verify[0] == false)
            {
                MessageBox.Show("Please assign a reference location before exporting data to SketchUp");
                return;
            }

            FolderBrowserDialog folderSel = new FolderBrowserDialog();
            folderSel.Description = "Select file location to export Sketchup files:";
            folderSel.ShowDialog();
            if (folderSel.SelectedPath != null)
            {
                //Export2SketchUp4PvPanel
                int pvPanelLyr = util.getLayerHdl(prj.LyrPoleName, pvMap);
                if (pvPanelLyr != -1)
                {
                    IMapFeatureLayer PanelFs = pvMap.Layers[pvPanelLyr] as IMapFeatureLayer;
                    Export2SketchUp4PvPanel(PanelFs, folderSel.SelectedPath);
                    MessageBox.Show("Google SketchUp file export completed");
                }
                else
                {
                    MessageBox.Show("Cannot export PV Panel model, please select PV panel layer first.");
                }

            }
        }

        private void cmdExportRooftopPanetToSkecthUp_Click(object sender, EventArgs e)
        {
            pvVerify();
            if (verify[0] == false)
            {
                MessageBox.Show("Please assign a reference location before exporting data to SketchUp");
                return;
            }

            FolderBrowserDialog folderSel = new FolderBrowserDialog();
            folderSel.Description = "Select file location to export Sketchup files:";
            folderSel.ShowDialog();
            if (folderSel.SelectedPath != null)
            {
                //Export2SketchUp4PvPanel
                int pvPanelLyr = util.getLayerHdl(cmbRoofTopPanelPosition.Text, pvMap);
                if (pvPanelLyr != -1)
                {
                    IMapFeatureLayer pvPanelFs = pvMap.Layers[pvPanelLyr] as IMapFeatureLayer;
                    Export2SketchUp4PvPanel(pvPanelFs, folderSel.SelectedPath);
                }
                else
                {
                    MessageBox.Show("Cannot export Pv Panel model, please select Pv Panel layer first");
                }
                MessageBox.Show("Google SketchUp file export completed");
            }
        }

        private void cmdSwithToGraph_Click(object sender, EventArgs e)
        {
            cmdSwithToGraph.Visible = false;
            TabGOptimize.Visible = true;
            cmdSwithToTable.Visible = true;
            grdAcProduct.Visible = false;
            pnlGrdProduction.Visible = false;
            cmdMapZoomIn.Visible = false;
            cmdMapPan.Visible = false;
            cmdMapZoomOut.Visible = false;
            cmdMapZoomExt.Visible = false;
            cmdMapSelection.Visible = false;
            cmdMapSelectionNone.Visible = false;

            txtPvLength.Text = prj.PvHeightGlo;
            txtPvWidth.Text = prj.PvWidthGlo;
            pvAz.AzimutAngle = float.Parse(prj.PvAzimuthGlo);
            pvTilt.tiltAngle = float.Parse(prj.PvTiltGlo);
            
        }
     
        private void cmdOptimization_Click(object sender, EventArgs e)
        {
            //---------------------------------------------------------------------------
            int numPvPanel = 1;
            TabGOptimize.SelectedIndex = 0;
            UpdateProgressBar.Visible = true;
            List<string> ls1Str = new List<string>();
            List<PointPairList> ls1 = new List<PointPairList>();
            UpdateProgressBar.Value = 0;
            UpdateProgressBar.Maximum = 9 * 38 + 1;
            int kk = 0;
            double tiltAngle = pvTilt.tiltAngle;
            double azAngle = pvAz.AzimutAngle;
            PointPairList list1 = new PointPairList();
            for (double az = 0; az <= 180; az += 10)
            {
                double x = az;
                double y = AnualEnergyProduction(numPvPanel, txtTM2.Text, tiltAngle, az);
                list1.Add(x, y);
                UpdateProgressBar.Value = kk;
                kk++;
            }
            ls1Str.Add("Tilt = " + tiltAngle.ToString() + " Deg");
            ls1.Add(list1);
            PointPairList list2 = new PointPairList();
            for (double tilt = 0; tilt < 90; tilt += 5)
            {
                double x = tilt;
                double y = AnualEnergyProduction(numPvPanel, txtTM2.Text, tilt, azAngle);
                list2.Add(x, y);
                UpdateProgressBar.Value = kk;
                kk++;
            }
            ls1Str.Add("Azimuth = " + azAngle.ToString() + " Deg");
            ls1.Add(list2);
            OptimizeGraphPlot(zedGOpti1, ls1, ls1Str, "Energy vs System Angle", "Energy (kWh)", "Angle (Deg)", 180, 15, 5);
            UpdateProgressBar.Visible = false;
            //---------------------------------------------------------------------------
            UpdateProgressBar.Maximum = 342;
            TabGOptimize.SelectedIndex = 1;
            UpdateProgressBar.Visible = true;
            List<string> ls2Str = new List<string>();
            List<PointPairList> ls2 = new List<PointPairList>();
            kk = 0;
            for (double tilt = 0; tilt < 90; tilt += 5)
            {
                PointPairList list = new PointPairList();
                for (double az = 0; az <= 180; az += 10)
                {
                    double x = az;
                    double y = AnualEnergyProduction(numPvPanel, txtTM2.Text, tilt, az);
                    list.Add(x, y);
                    UpdateProgressBar.Value = kk;
                    kk++;
                }
                ls2Str.Add(tilt.ToString() + "Deg");
                ls2.Add(list);
            }
            OptimizeGraphPlot(zedGOpti2, ls2, ls2Str, "Energy vs Azimuth Angle @Tilts Angle", "Energy (kWh)", "Azimuth Angle (Deg)", 180, 15, 5);
            UpdateProgressBar.Visible = false;
            //---------------------------------------------------------------------------        
            TabGOptimize.SelectedIndex = 2;
            UpdateProgressBar.Visible = true;
            List<string> ls3Str = new List<string>();
            List<PointPairList> ls3 = new List<PointPairList>();
            UpdateProgressBar.Value = 0;
            UpdateProgressBar.Maximum = 9 * 38;
            kk = 0;
            for (double az = 0; az <= 180; az += 10)
            {
                PointPairList list = new PointPairList();
                for (double tilt = 0; tilt < 90; tilt += 5)
                {
                    double x = tilt;
                    double y = AnualEnergyProduction(numPvPanel, txtTM2.Text, tilt, az);
                    list.Add(x, y);
                    UpdateProgressBar.Value = kk;
                    kk++;
                }
                ls3Str.Add(az.ToString() + "Deg");
                ls3.Add(list);
            }
            OptimizeGraphPlot(zedGOpti3, ls3, ls3Str, "Energy vs Tilt Angle @Azimuth Angle", "Energy (kWh)", "Tilt Angle (Deg)", 90, 15, 5);
            UpdateProgressBar.Visible = false;

            // */
            //---------------------------------------------------------------------------        
            string[] months = { "Jan, Feb, Mar, April, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec" };
            int[] monthCount = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

            TabGOptimize.SelectedIndex = 3;
            UpdateProgressBar.Visible = true;
            List<string> ls4Str = new List<string>();
            List<PointPairList> ls4 = new List<PointPairList>();
            UpdateProgressBar.Value = 0;
            UpdateProgressBar.Maximum = 90;
            //int kk = 0;
            //double azAngle = pvAz.AzimutAngle;
            kk = 0;
            double[,] monthlyEnergy = new double[12, 90];
            double[] maxEnergy = new double[12];
            for (int tilt = 0; tilt < 90; tilt++)
            {
                double[] y = MonthlyEnergyProduction(numPvPanel, txtTM2.Text, (double)tilt, azAngle);
                for (int m = 0; m < 12; m++)
                {
                    monthlyEnergy[m, tilt] = y[m];
                }
                UpdateProgressBar.Value = kk;
                kk++;
            }
            //
            int[,] maxTilt = new int[30, 12];
            for (int m = 0; m < 12; m++)
            {
                double maxE = -1000000000;
                for (int tilt = 0; tilt < 90; tilt++)
                {
                    if (monthlyEnergy[m, tilt] > maxE)
                    {
                        maxE = monthlyEnergy[m, tilt];
                        for (int odr = 29; odr >= 1; odr--)
                        {
                            maxTilt[odr, m] = maxTilt[odr - 1, m];
                        }
                        maxTilt[0, m] = tilt;
                        maxEnergy[m] = maxE;
                    }
                }
            }

            for (int l = 0; l < 30; l += 5)
            {
                PointPairList list = new PointPairList();
                for (int m = 0; m < 12; m++)
                {
                    double x = m + 1;
                    double y = maxTilt[l, m];
                    list.Add(x, y);
                }
                if (l == 0) ls4Str.Add("1st");
                if (l == 1) ls4Str.Add("2nd");
                if (l == 2) ls4Str.Add("3rd");
                if (l > 2) ls4Str.Add((l + 1).ToString() + "th");
                ls4.Add(list);
            }
            OptimizeGraphPlot(zedGOpti4, ls4, ls4Str, "Monthly vs Tilt Angle (Maximum Energy)", "Tilt Angle (Deg)", "Time (Month)", 13, 3, 1);
            GraphPane myPane = zedGOpti4.GraphPane;
            myPane.Y2Axis.Title.Text = "Energy (kWh)";
            // Make up some data points based on the Sine function
            PointPairList list4 = new PointPairList();
            for (int i = 0; i < 12; i++)
            {
                double x = (double)i + 1;
                double y = maxEnergy[i];
                list4.Add(x, y);
            }
            // Generate a blue curve with circle symbols, and "Beta" in the legend
            //LineItem myCurve = myPane.AddCurve("Energy", list2, Color.Blue, ZedGraph.SymbolType.Circle);
            BarItem myCurve = myPane.AddBar("Energy", list4, Color.Red);
            // Fill the symbols with white
            //myCurve.Symbol.Fill = new Fill(Color.White);
            // Associate this curve with the Y2 axis
            myCurve.IsY2Axis = true;
            // Enable the Y2 axis display
            myPane.Y2Axis.IsVisible = true;
            // Make the Y2 axis scale blue
            myPane.Y2Axis.Scale.FontSpec.FontColor = Color.Blue;
            myPane.Y2Axis.Title.FontSpec.FontColor = Color.Blue;
            // turn off the opposite tics so the Y2 tics don't show up on the Y axis
            myPane.Y2Axis.MajorTic.IsOpposite = false;
            myPane.Y2Axis.MinorTic.IsOpposite = false;
            // Display the Y2 axis grid lines
            myPane.Y2Axis.MajorGrid.IsVisible = true;
            // Align the Y2 axis labels so they are flush to the axis
            myPane.Y2Axis.Scale.Align = AlignP.Inside;

            UpdateProgressBar.Visible = false;
            zedGOpti4.AxisChange();
            zedGOpti4.Refresh();
            //
            //haveOptimizeGraph = true;

        }

        private void pvMap_Resize(object sender, EventArgs e)
        {
            try
            {
                //grdAcProduct.Location = pvMap.Location;
                //grdAcProduct.Size = pvMap.Size;
                pnlGrdProduction.Location = pvMap.Location;
                pnlGrdProduction.Size = pvMap.Size;
                TabGOptimize.Location = pvMap.Location;
                TabGOptimize.Size = pvMap.Size;

                cmdSwithToTable.Location = cmdSwithToMap.Location;
            }
            catch
            { }
        }

        private void cmdAddKML_Click(object sender, EventArgs e)
        {
            AddKML();
        }

        private void AddKML()
        {
            OpenFileDialog kmlFileDialog = new OpenFileDialog();
            kmlFileDialog.DefaultExt = "kml";
            kmlFileDialog.Filter = "PVMapper site data (kml file format)|*.kml";
            kmlFileDialog.ShowDialog();
            if (kmlFileDialog.FileName.Length > 0)
            {
                try
                {
                    //FeatureSet fe = new FeatureSet();
                    int cLyr = pvMap.Layers.Count;
                    pvMap.Layers.Add(kmlFileDialog.FileName);
                    string LegendName =Path.GetFileName(kmlFileDialog.FileName).Replace(".kml",String.Empty);
                    prj.LyrSiteAreaName = LegendName;
                    cmbSiteArea.Items.Add(LegendName);
                    cmbSiteArea.SelectedItem = LegendName;
                    FeatureSet Fe = pvMap.Layers[cLyr].DataSet as FeatureSet;
                    double xx = Fe.Extent.X + Fe.Extent.Width / 2;
                    double yy = Fe.Extent.Y - Fe.Extent.Height / 2;
                    util.kDrawCircle(xx, yy, 5, 36, pvMap, Color.Red);
                    //----------------------------------------------------------------------
                    appManager.ProgressHandler.Progress("Pick the reference location on map", 0, "Pick the reference location on map");

                    Pen MyPen = new Pen(Color.Black);
                    Coordinate c = new Coordinate(xx, yy);
                    //c = pvMap.PixelToProj(e.Location);
                    Extent ext = pvMap.ViewExtents;
                    double dx = (ext.MaxX - ext.MinX);
                    double dy = (ext.MaxX - ext.MinX);
                    ext.SetCenter(c);
                    pvMap.ViewExtents = ext;// layer.DataSet.Extent;

                    //pvMap.ViewExtents.SetCenter(c, 100, 100); 
                    //-----------------------------------------------------------------------
                    double[] latlong = new double[] { c.X, c.Y };
                    Reproject.ReprojectPoints(latlong, new double[] { 0 }, pvMap.Projection, KnownCoordinateSystems.Geographic.World.WGS1984, 0, 1);
                    txtLNG.Text = latlong[0].ToString();
                    txtLAT.Text = latlong[1].ToString();
                    double minDist = 1000000.00;

                    for (int i = 0; i < nSta; i++)
                    {
                        double rr;
                        rr = Math.Sqrt(Math.Pow((latlong[0] - wSta[i].LONG2), 2) + Math.Pow((latlong[1] - wSta[i].LAT2), 2));
                        wStaDistance[i] = rr;
                        wStaSel[i] = i;
                        if (rr < minDist)
                        {
                            minDist = rr;
                            cmbState.Text = wSta[i].State;
                            cmbCity.Text = wSta[i].City;
                        }
                    }

                    double temp;
                    int temp1;
                    for (int i = (nSta - 2); i >= 0; i--)
                    {
                        for (int j = 1; j <= i; j++)
                        {
                            if (wStaDistance[j - 1] > wStaDistance[j])
                            {
                                temp = wStaDistance[j - 1];
                                wStaDistance[j - 1] = wStaDistance[j];
                                wStaDistance[j] = temp;
                                temp1 = wStaSel[j - 1];
                                wStaSel[j - 1] = wStaSel[j];
                                wStaSel[j] = temp1;
                            }
                        }
                    }

                    //-----------------------------------------------------------------------
                    double radius = 1000;
                    txtUtmN.Text = c.Y.ToString();
                    txtUtmE.Text = c.X.ToString();
                    pvMap.MapFrame.DrawingLayers.Clear();
                    util.kDrawCircle(c.X, c.Y, radius, 360, pvMap, Color.Magenta);
                    //MessageBox.Show(c.X + "," + c.Y);
                    Double r = 0.25;
                    int nIDW = Convert.ToInt16(txtNIdwSta.Text);
                    util.kDrawCircle(c.X, c.Y, r, 360, pvMap, Color.Magenta);
                    prj.UtmE = c.X;
                    prj.UtmN = c.Y;
                    for (int ii = 0; ii < nIDW; ii++)
                    {
                        double lat = wSta[wStaSel[ii]].LAT2;
                        double lng = wSta[wStaSel[ii]].LONG2;
                        double x = 0;
                        double y = 0;
                        double[] mapCoordinate = new double[] { lng, lat };
                        Reproject.ReprojectPoints(mapCoordinate, new double[] { 0 }, KnownCoordinateSystems.Geographic.World.WGS1984, pvMap.Projection, 0, 1);
                        Coordinate coord = util.circleCoord(c.X, c.Y, mapCoordinate[0], mapCoordinate[1], radius); //returns coordinate of point on inner circle
                        Coordinate coord2 = util.circleCoord(mapCoordinate[0], mapCoordinate[1], c.X, c.Y, radius); //returns coordinates of point on outer circle

                        util.kDrawCircle(mapCoordinate[0], mapCoordinate[1], radius, 360, pvMap, Color.Magenta);
                        util.DrawLine(coord2.X, coord2.Y, coord.X, coord.Y, 2, Color.Magenta, pvMap);
                    }

                    Envelope env = new Envelope();
                    env.SetExtents(prj.UtmE - 1000, prj.UtmN - 1000, prj.UtmE + 1000, prj.UtmN + 1000);
                    pvMap.ViewExtents = env.ToExtent();
                    prj.UseKML = true;                    
                    DrawRoseDiagram();
                    cmdCreatePvPole.Enabled = true;
                    //----------------------------------------------------------------------
                }
                catch
                {
                }
            }
        }

        private void splitContainer1_Panel2_MouseMove(object sender, MouseEventArgs e)
        {
            /*
            if (e.X < 200 & e.Y < 40)
            {
                cmdMapZoomIn.Visible = true;
                cmdMapZoomOut.Visible = true;
                cmdMapZoomExt.Visible = true;
                cmdMapPan.Visible = true;
                cmdMapSelection.Visible = true;
                cmdMapSelectionNone.Visible = true;
            }
            else
            {
                cmdMapZoomIn.Visible = false;
                cmdMapZoomOut.Visible = false;
                cmdMapZoomExt.Visible = false;
                cmdMapPan.Visible = false;
                cmdMapSelection.Visible = false;
                cmdMapSelectionNone.Visible = false;
            }
             */
        }

        #region "MapView Control"
        private void cmdMapZoomIn_Click(object sender, EventArgs e)
        {
            pvMap.FunctionMode = FunctionMode.ZoomIn;
        }

        private void cmdMapZoomOut_Click(object sender, EventArgs e)
        {
            pvMap.FunctionMode = FunctionMode.ZoomOut;
        }

        private void cmdMapZoomExt_Click(object sender, EventArgs e)
        {
            pvMap.ViewExtents = pvMap.Extent;
        }

        private void cmdMapPan_Click(object sender, EventArgs e)
        {
            pvMap.FunctionMode = FunctionMode.Pan;
        }

        private void cmdMapSelection_Click(object sender, EventArgs e)
        {
            pvMap.FunctionMode = FunctionMode.Select;
        }

        private void cmdMapSelectionNone_Click(object sender, EventArgs e)
        {
            pvMap.FunctionMode = FunctionMode.None;
        }
        #endregion //"MapView Control"

        private void txtRoseScale_TextChanged(object sender, EventArgs e)
        {
            util.NummericTextBoxCheck(txtRoseScale, "Scale value", 1, true);
        }


        private void txtPowX_TextChanged(object sender, EventArgs e)
        {
            util.NummericTextBoxCheck(txtPowX, "The x-power", 2, true);
        }

        private void txtPowY_TextChanged(object sender, EventArgs e)
        {
            util.NummericTextBoxCheck(txtPowY, "The y-power", 2, true);
        }

        private void txtAreaPreSys_TextChanged(object sender, EventArgs e)
        {
            util.NummericTextBoxCheck(txtAreaPreSys, "PV System per area", 2, true);
        }

        private void txtSystem_size_TextChanged(object sender, EventArgs e)
        {
            util.NummericTextBoxCheck(txtSystem_size, "System DC nameplate rating (kW)", 4, true);
        }

        private void txtDerate_TextChanged(object sender, EventArgs e)
        {
            util.NummericTextBoxCheck(txtDerate, "derate factor", 0.77, true);
        }

        private void txtTcell_TextChanged(object sender, EventArgs e)
        {
            util.NummericTextBoxCheck(txtTcell, "Calculated cell temperature from previous timestep, (deg. C)", 6.9, true);
        }

        private void txtPoa_TextChanged(object sender, EventArgs e)
        {
            util.NummericTextBoxCheck(txtPoa, "Plane of array irradiance (W/m2) from previous time step", 84.5, true);
        }

        private void txtPx_TextChanged(object sender, EventArgs e)
        {
            util.NummericTextBoxCheck(txtPx, "Roof Pitch", 12, true);
        }

        private void frm01_MainForm_Activated(object sender, EventArgs e)
        {
            // UpDateRoofShape();
            // DrawRoof = true;
            // panelDrawRoof.Invalidate();
        }

        private void txtRidgeHeight_TextChanged(object sender, EventArgs e)
        {
            util.NummericTextBoxCheck(txtRidgeHeight, "Ridge Height", 0, true);
        }

        private void txtEaveHeight_TextChanged(object sender, EventArgs e)
        {
            util.NummericTextBoxCheck(txtEaveHeight, "Eave Height", 0, true);
        }

        private void cmdVDO_Click(object sender, EventArgs e)
        {
            //string address = "http://pvdesktop.codeplex.com/";
            string address = "https://www.youtube.com/watch?v=-DEF2YyAoyk&feature=youtu.be";
            webBrowser1.Navigate(new Uri(address));
            webBrowser1.Visible = true;

        }

        private void cmdPVMapperWeb_Click_1(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to open PVMapper with default web browser?", "PVMapper -Site Designer", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start("http://pvmapper.org/app");
            }
            else if (dialogResult == DialogResult.No)
            {
                string address = "http://pvmapper.org/app";
                webBrowser1.Navigate(new Uri(address));
                webBrowser1.Visible = true;
            }
        }

        private void cmdErrorReport_Click_1(object sender, EventArgs e)
        {
            string address = "https://pvdesktop.codeplex.com/workitem/list/basic";
            webBrowser1.Navigate(new Uri(address));
            webBrowser1.Visible = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnStepByStep_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel2Collapsed = false;
            this.tabFakeRibbon.SelectedTab = tabPage1;
            panelTab.BackgroundImage = picTab01.Image;
            TabHilight(lblTab01);
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            splitContainer2.Panel2Collapsed = true;
        }


        public void DrawTreeShadow()
        {
            CheckWorkingPath();

            if (prj.Path != "")
            {
                if (prj.Verify[3] == true)
                {// int[] dat = new int[mRow, mCol];

                    int year = dateTimePicker1.Value.Year;
                    double Latitude = Convert.ToDouble(this.txtLAT.Text);
                    double Longitude = Convert.ToDouble(this.txtLNG.Text);
                    double UtmN = Convert.ToDouble(this.txtUtmN.Text);
                    double UtmE = Convert.ToDouble(this.txtUtmE.Text);
                    prj.LyrTree = util.getLayerHdl(prj.LyrTreeName, pvMap);
                    int TreeLyr = prj.LyrTree;
                    TreeShadow(year, Latitude, Longitude, UtmN, UtmE, TreeLyr);
                }  // end if
                else
                {
                    MessageBox.Show("Please calculate sun path statistic first.");
                }  // end msgbox
            } //end if

        }

        public void TreeShadow(int year, double Latitude, double Longitude, double UtmN, double UtmE, int TreeLyr)
        {
            if (TreeLyr != -1)
            {
                IFeatureSet fs = new FeatureSet(FeatureType.Point);
                //---------------------------------------------------------
                fs.DataTable.Columns.Add(new DataColumn("Azimuth", typeof(double)));
                fs.DataTable.Columns.Add(new DataColumn("Ele_Angle", typeof(double)));
                //---------------------------------------------------------
                IMapFeatureLayer Treemp = pvMap.Layers[TreeLyr] as IMapFeatureLayer;
                //MessageBox.Show("Number of shape = " + mp.DataSet.NumRows());

                //int nShp = mp.DataSet.NumRows() - 1;
                IFeatureSet TreemyFe;
                TreemyFe = new FeatureSet(FeatureType.Polygon);

                IFeatureSet fea = ((IFeatureLayer)pvMap.GetLayers().ToArray()[TreeLyr]).DataSet;
                System.Data.DataTable dt = fea.DataTable;

                #region initial sun data
                short TimeZone = -7;
                double[,] sun = new double[1440, 2];
                int ii = 0;

                for (int month = 1; month <= 12; month++)
                {
                    for (int day = 1; day <= 28; day += 7)
                    {
                        for (int hour = 1; hour <= 24; hour++)
                        {
                            double hrPassMidnight = (double)hour / 24.0;
                            SolarCal ySun = new SolarCal(day, month, year, hrPassMidnight, Latitude, Longitude, TimeZone);

                            double eleAng = ySun.SolarElevationAngle;
                            double azmAng = ySun.SolarAzimuthAngle;

                            sun[ii, 0] = eleAng;
                            sun[ii, 1] = azmAng;
                            ii++;
                        } // end for hour
                    } // end for day
                } //end for month
                int numHr = ii;
                #endregion //-----initial sun



                //--------------------------------------------
                // Assign building data
                double x0 = UtmE;
                double y0 = UtmN;
                double dx = 20;
                double dy = 50;
                //---------------------------------------------
                UpdateProgressBar.Maximum = Treemp.DataSet.NumRows(); // frequency about 15 day
                UpdateProgressBar.Visible = true;
                //UpdateProgressBar
                int PrgValue = 0;
                for (int iTree = 0; iTree < Treemp.DataSet.NumRows(); iTree++) // Tree loop
                {
                    #region Tree loop variables
                    //int numTreePt = Treemp.DataSet.GetFeature(iTree).NumPoints;                                
                    Coordinate[] TreeSpan = new Coordinate[20];
                    Coordinate[] TreePntCoord = new Coordinate[20];
                    Coordinate[] TreeSpanRotated = new Coordinate[20];
                    Coordinate[] Treeptss = new Coordinate[20];
                    IFeature TreeFs = Treemp.DataSet.GetFeature(iTree);
                    string h1 = TreeFs.DataRow["Height"].ToString();
                    string d1 = TreeFs.DataRow["Diameter"].ToString();
                    string t1 = TreeFs.DataRow["Type"].ToString();
                    int TreeId = getTreeTypeId(t1);
                    setTreeShape(TreeId + 1);
                    #endregion //Tree loop variables
                    //Tree origin
                    double xOrigin;
                    double yOrigin;
                    yOrigin = TreeFs.Coordinates[0].Y;
                    xOrigin = TreeFs.Coordinates[0].X;
                    if (util.IsNumeric(h1) == true & util.IsNumeric(d1) == true)
                        PrgValue++;
                    UpdateProgressBar.Value = PrgValue;
                    {
                        //Convert h1 and d1 to double type
                        double h = Convert.ToDouble(h1);
                        double d = Convert.ToDouble(d1);

                        for (ii = 0; ii < numHr; ii++) // Hourly loop
                        {

                            double eleAng = sun[ii, 0];
                            double AzAng = sun[ii, 1];
                            if (util.IsNumeric(txtEffectiveAngle.Text) != true)
                            {
                                MessageBox.Show("Effective angle must be numeric.");
                                return;
                            }
                            if (eleAng >= Convert.ToDouble(txtEffectiveAngle.Text)) // efficetive elevation angle
                            {
                                #region initial tree data
                                for (int i = 0; i < 10; i++)
                                {
                                    int ia = i * 2;
                                    int ib = i * 2 + 1;
                                    TreeSpan[ia] = new Coordinate(d / 2 * treeShape[i, 0], 0);
                                    TreeSpan[ib] = new Coordinate(-d / 2 * treeShape[i, 0], 0);
                                    TreeSpanRotated[ia] = util.RotateTreeShadow(TreeSpan[ia], sun[ii, 1]);
                                    TreeSpanRotated[ib] = util.RotateTreeShadow(TreeSpan[ib], sun[ii, 1]);
                                    double zRatio = treeShape[i, 1];
                                    TreePntCoord[ia] = new Coordinate(xOrigin + TreeSpanRotated[ia].X, yOrigin + TreeSpanRotated[ia].Y, h * zRatio);
                                    TreePntCoord[ib] = new Coordinate(xOrigin + TreeSpanRotated[ib].X, yOrigin + TreeSpanRotated[ib].Y, h * zRatio);
                                }
                                #endregion //initial tree data

                                #region Shadow calculation
                                //Shadow point
                                for (int i = 0; i < TreePntCoord.Length; i++)
                                {
                                    Shadow Treeshadow = new Shadow(AzAng, eleAng, TreePntCoord[i]);
                                    Coordinate Treetmp = Treeshadow.shadowPt;
                                    Treeptss[i] = new Coordinate(Treetmp.X, Treetmp.Y);
                                }
                                // CONVEX HULL Algorithm to make a shadow polygon
                                var multiPoint = new MultiPoint(Treeptss);
                                var convexHull = (Polygon)multiPoint.ConvexHull();
                                TreemyFe.AddFeature(convexHull);
                                //
                                #endregion //shadow calculation
                            }  // end if
                            else // Night hour
                            {
                            }
                        }// Hourly loop
                    }

                }//Tree loop
                UpdateProgressBar.Visible = false;



                //----------------------------------------------------------------------------------------------------------------------------
                Console.WriteLine(DateTime.Now.ToString());
                Console.ReadLine();

                prgBar.Visible = false;
                //IFeatureSet result = myFe.UnionShapes(ShapeRelateType.Intersecting);
                TreemyFe.Projection = pvMap.Projection;
                //result.Projection = pvMap.Projection;

                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                TreemyFe.Name = "TreeShadowMap";
                TreemyFe.Filename = prj.Path + "\\Temp\\" + TreemyFe.Name + ".shp";
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


                TreemyFe.SaveAs(TreemyFe.Filename, true);
                //result.Filename =prj.Path + "\\Temp\\shadow map union.shp";
                //result.SaveAs(result.Filename, true);
                //pvMap.Layers.Add(myFe);

                MapPolygonLayer TreeShadowArea;
                TreeShadowArea = new MapPolygonLayer(TreemyFe);// MapPolygonLayer(fs);
                PolygonSymbolizer ShadowSymbolize = new PolygonSymbolizer(Color.Black, Color.Red);
                // set transparent
                SimplePattern sp = new SimplePattern(Color.Black);
                sp.Opacity = 0.5f;
                ShadowSymbolize.Patterns.Clear();
                ShadowSymbolize.Patterns.Add(sp);
                TreeShadowArea.Symbolizer = ShadowSymbolize;
                util.removeDupplicateLyr(TreeShadowArea.Name, pvMap);
                pvMap.Layers.Add(TreeShadowArea);

                MessageBox.Show("Tree shadows have been successfully drawn.");
            } // end if tree
            else
            {
                MessageBox.Show("Please create a tree Layer first.");
            }   // end else
        }



        frmAddTree FormAddTree;

        private void btnAddTreeTest_Click_1(object sender, EventArgs e)
        {
        }


        private void pvMap_MouseUp(object sender, MouseEventArgs e)
        {


            if (FormAddTree == null) return;

            if (FormAddTree.Visible == true)
            {

                IFeatureSet pvfs;
                int idl = util.getLayerID("Tree", pvMap);
                if (idl == -1) return;
                setCurrrentLayer("Tree");
                pvfs = pvMap.Layers[idl].DataSet as IFeatureSet;
                prj.LyrTreeName = pvfs.Name;

                Coordinate c = new Coordinate();
                c = pvMap.PixelToProj(e.Location);



                //----------------------------------------------------------------------------------------

                IPoint TreeFe = new DotSpatial.Topology.Point(c);
                IFeature ifeaTree = pvfs.AddFeature(TreeFe);
                // project.NumPvPanel++;

                /*
                        IPoint poleFe = new DotSpatial.Topology.Point(poleLocation);
                        IFeature ifea = fs.AddFeature(poleFe);
                        project.NumPvPanel++;
                */

                //------------------------------------------------------

                ifeaTree.DataRow.BeginEdit();
                ifeaTree.DataRow["Type"] = getTreeName();
                ifeaTree.DataRow["Diameter"] = Convert.ToDouble(FormAddTree.txtTreeDiameter.Text);
                ifeaTree.DataRow["Height"] = Convert.ToDouble(FormAddTree.txtTreeHeight.Text);
                ifeaTree.DataRow["X"] = c.X;
                ifeaTree.DataRow["Y"] = c.Y;
                ifeaTree.DataRow.EndEdit();

                //-----------------------------------------------------------------------------------------------
                //DrawTreeCircle();
                double d = Convert.ToDouble(FormAddTree.txtTreeDiameter.Text);
                kDrawTreeCircle(c.X, c.Y, d / 2, 36, pvMap, Color.Green);

                pvMap.MapFrame.Invalidate();

            }



            //FormAddTree.Show();
        }


        private void btnAddTree_Click(object sender, EventArgs e)
        {
            int lyr3 = util.getLayerHdl(prj.LyrTreeName, pvMap);
            if (lyr3 != -1)
            {
                //MessageBox.Show(prj.Path);
                FeatureSet Fe3 = pvMap.Layers[lyr3].DataSet as FeatureSet;
                Fe3.SaveAs(prj.Path + "Tree.shp", true);
            }
            FormAddTree = new frmAddTree();
            FormAddTree.TopMost = true;
            FormAddTree.Michael = this;
            FormAddTree.PvMap = pvMap;
            FormAddTree.Show();
            FormAddTree.ProjectFile = prj;
            //setCurrrentLayer(prj.LyrTreeName); 
            CheckWorkingPath();

            if (prj.Path != "")
            {
                bool lyrExists = util.checkLyr("Tree", pvMap);
                if (lyrExists == false)
                {

                    IFeatureSet pvfs;
                    pvfs = new FeatureSet(FeatureType.Point);
                    pvfs.Projection = pvMap.Projection;
                    pvfs.DataTable.Columns.Add(new DataColumn("Diameter", typeof(double)));
                    pvfs.DataTable.Columns.Add(new DataColumn("Height", typeof(double)));
                    pvfs.DataTable.Columns.Add(new DataColumn("Type", typeof(string)));
                    pvfs.DataTable.Columns.Add(new DataColumn("X", typeof(string)));
                    pvfs.DataTable.Columns.Add(new DataColumn("Y", typeof(string)));

                    //++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    pvfs.Name = "Tree";
                    pvfs.Filename = prj.Path + "\\Temp\\" + pvfs.Name + ".shp";
                    //++++++++++++++++++++++++++++++++++++++++++++++++++++++

                    pvfs.SaveAs(pvfs.Filename, true);
                    util.removeDupplicateLyr(pvfs.Name, pvMap);
                    pvMap.Layers.Add(pvfs);
                    prj.LyrTreeName = pvfs.Name;
                    loadLayerList();
                    // set current layer to tree layer
                    setCurrrentLayer(prj.LyrTreeName);
                }
            }
            else
            {
                MessageBox.Show("Please create working path prior to adding trees.");
            }

        }

        frmAddBuilding FormAddBuilding;

        private void btnAddBuilding_Click(object sender, EventArgs e)
        {
            int lyr = util.getLayerHdl(prj.LyrBuildingName, pvMap);
            if (lyr != -1)
            {
                //MessageBox.Show(prj.Path);
                FeatureSet Fe = pvMap.Layers[lyr].DataSet as FeatureSet;
                Fe.SaveAs(prj.Path + "Building.shp", true);
            }
            pvMap.FunctionMode = FunctionMode.Select;
            firstBldgPt = true;
            mapAct = mapAction.BuildingCoord;
            FormAddBuilding = new frmAddBuilding();
            FormAddBuilding.TopMost = true;
            FormAddBuilding.Michael = this;
            FormAddBuilding.PvMap = pvMap;
            FormAddBuilding.ProjectFile = prj;
            FormAddBuilding.Show();
            prj.LyrBuildingName = "Building";
            CheckWorkingPath();

            if (prj.Path != "")
            {
                bool lyrExists = util.checkLyr("Building", pvMap);
                if (lyrExists == false)
                {

                    IFeatureSet fs;
                    fs = new FeatureSet(FeatureType.Polygon);
                    fs.Projection = pvMap.Projection;
                    fs.DataTable.Columns.Add(new DataColumn("Height", typeof(double)));
                    fs.DataTable.Columns.Add(new DataColumn("Remark", typeof(double)));

                    //++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    fs.Name = "Building";
                    fs.Filename = prj.Path + "\\Temp\\" + fs.Name + ".shp";
                    //++++++++++++++++++++++++++++++++++++++++++++++++++++++

                    fs.SaveAs(fs.Filename, true);
                    //util.removeDupplicateLyr(fs.Name, pvMap);
                    pvMap.Layers.Add(fs);
                    prj.LyrBuildingName = fs.Name;
                    loadLayerList();
                    // set current layer to building layer
                    setCurrrentLayer(prj.LyrBuildingName);
                }
            }
            else
            {
                MessageBox.Show("Please create working path prior to adding buildings.");
            }

        }

        public void kDrawTreeCircle(Double x0, Double y0, Double r, Int16 numVertex, IMap MapCanvas, Color color)
        {
            Double dAng = 360 / numVertex;
            Coordinate[] cr = new Coordinate[numVertex + 1]; //x-axis
            Random rnd = new Random();
            for (int iAng = 0; iAng <= numVertex; iAng++)
            {
                Double ang1 = iAng * dAng;
                Double x1 = Math.Sin(ang1 * Math.PI / 180) * r + x0 + rnd.Next(rnd.Next(100)) * (0.5 * r) / 100;
                Double y1 = Math.Cos(ang1 * Math.PI / 180) * r + y0 + rnd.Next(rnd.Next(100)) * (0.5 * r) / 100;
                cr[iAng] = new Coordinate(x1, y1);
            }
            cr[numVertex] = new Coordinate(cr[0].X, cr[0].Y);
            IPolygon ls = new Polygon(cr);
            Feature f = new Feature(ls);
            FeatureSet fs = new FeatureSet(f.FeatureType);
            fs.Features.Add(f);

            MapPolygonLayer circleShp;
            circleShp = new MapPolygonLayer(fs);
            circleShp.Symbolizer = new PolygonSymbolizer(Color.YellowGreen, color, 2);
            //circleShp.Symbolizer.SetFillColor(Color.Red);
            SimplePattern sp = new SimplePattern(color);
            sp.Opacity = 0.5f;
            circleShp.Symbolizer.Patterns.Clear();
            circleShp.Symbolizer.Patterns.Add(sp);
            //circleShp.Symbolizer.SetOutline(color, 2);
            MapCanvas.MapFrame.DrawingLayers.Add(circleShp);

            // Request a redraw
            MapCanvas.MapFrame.Invalidate();
        }

        public void DrawTreeCircle()
        {
            int treeLyr = util.getLayerHdl(prj.LyrTreeName, pvMap);
            if (treeLyr != -1)
            {
                pvMap.MapFrame.DrawingLayers.Clear();
                IMapFeatureLayer treeFe = pvMap.Layers[treeLyr] as IMapFeatureLayer;
                for (int row = 0; row < treeFe.DataSet.NumRows(); row++)
                {
                    try
                    {
                        IFeature fs = treeFe.DataSet.GetFeature(row);
                        object val = fs.DataRow["diameter"];
                        double r = Convert.ToDouble(val);
                        double x1 = fs.Coordinates[0].X;
                        double y1 = fs.Coordinates[0].Y;
                        kDrawTreeCircle(x1, y1, r, 36, pvMap, Color.Green);
                    }
                    catch
                    { }
                }

            }
        }
        frmAreaBoundry FormAreaBoundry;
        private void btnDrawArea_Click(object sender, EventArgs e)
        {
            int lyr = util.getLayerHdl(prj.LyrSiteAreaName, pvMap);
            /*
            if (lyr != -1)
            {

                DialogResult result = MessageBox.Show("Opening this form will delete current Site Boundry polygon. Do you want to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DrawArea();
                }
            }
            else
            {
                DrawArea();
            }

            */
            refreshSiteAreaCmb();
            openFormBoundry();
            
        }

        public void refreshSiteAreaCmb()
        {
            
            int listLength = cmbSiteArea.Items.Count;
            for (int i = 0; i < listLength; i++)
            {       
                       
                    string text = cmbSiteArea.Items[i].ToString();                
                
                int lyr = util.getLayerHdl(text ,pvMap);                
                if (lyr == -1)
                {
                    cmbSiteArea.Items.Remove(text);
                    listLength--;
                    i = -1;
                }
            }
        }

        public void refreshPrjLyrNames()
        {
            refreshSiteAreaCmb();
            string AreaName = cmbSiteArea.SelectedItem.ToString();
            prj.LyrSiteAreaName = AreaName;
            int PvArayLyr = util.getLayerHdl(AreaName + " PV Array",pvMap);
            if (PvArayLyr != -1)
            {
                prj.LyrPvPanelName = AreaName + " PV Array";
            }
            else
            {
                prj.LyrPvPanelName = "";
            }
            int PoleLyr = util.getLayerHdl(AreaName + " Panel Position", pvMap);
            if (PoleLyr != -1)
            {
                prj.LyrPoleName = AreaName + " Panel Position";
            }
            else
            {
                prj.LyrPoleName = "";
            }
            propertyGrid1.Refresh();
        }

        public void openFormBoundry()
        {

            FormAreaBoundry = new frmAreaBoundry();
            FormAreaBoundry.TopMost = true;
            FormAreaBoundry.Michael = this;
            FormAreaBoundry.PvMap = pvMap;
            FormAreaBoundry.ProjectFile = prj;
            FormAreaBoundry.Show();
        }

        public void DrawArea()
        {
            //FormAreaBoundry = new frmAreaBoundry();
            pvMap.FunctionMode = FunctionMode.Select;
            firstAreaPt = true;
            mapAct = mapAction.AreaCoord;
            CheckWorkingPath();

            if (prj.Path != "")
            { 
                    int areaLyr = util.getLayerHdl(prj.LyrSiteAreaName, pvMap);                
                    IFeatureSet pvfs;
                    pvfs = new FeatureSet(FeatureType.Polygon);
                    pvfs.Projection = pvMap.Projection;
                    pvfs.DataTable.Columns.Add(new DataColumn("X-Spacing", typeof(double)));
                    pvfs.DataTable.Columns.Add(new DataColumn("Y-Spacing", typeof(string)));
                    int i = 1;   
                    bool lyrExists = false;
                    do 
                    {
                        string AreaName = FormAreaBoundry.txtAreaName.Text;
                        int lyr = util.getLayerHdl(AreaName, pvMap); //("Area_" + i,pvMap) 
                        if(lyr == -1 | lyr != -1 ) //temp |lyr!=-1 while testing ###########
                        {
                            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                            pvfs.Name = AreaName; //"Area_" + i;
                            pvfs.Filename = prj.Path + "\\Temp\\" + pvfs.Name + ".shp";
                            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++      
                            lyrExists = true;
                        }
                        else
                        {
                         lyrExists = false;
                        }
                            i++;
                    } while (lyrExists == false);

                    prj.LyrSiteAreaName = pvfs.Name;
                    pvfs.SaveAs(pvfs.Filename, true);
                    // util.removeDupplicateLyr(pvfs.Name, pvMap);
                    pvMap.Layers.Add(pvfs);
                    cmbSiteArea.Items.Add(prj.LyrSiteAreaName);
                    cmbSiteArea.SelectedItem = prj.LyrSiteAreaName;
                    
                    //loadLayerList();
                    setCurrrentLayer(prj.LyrSiteAreaName);                
            }
        }

        frmAlignmentBoundry FormAlignmentBoundry;
        private void btnDrawAlignment_Click(object sender, EventArgs e)
        {
            DrawAlignment();
        }
        public void DrawAlignment()
        {
            pvMap.FunctionMode = FunctionMode.Select;
            firstAlignPt = true;
            mapAct = mapAction.AlignmentCoord1;
            FormAlignmentBoundry = new frmAlignmentBoundry();
            FormAlignmentBoundry.TopMost = true;
            FormAlignmentBoundry.Michael = this;
            FormAlignmentBoundry.PvMap = pvMap;
            FormAlignmentBoundry.ProjectFile = prj;
            FormAlignmentBoundry.Show();
            cmbAlignmentLyr.Text = "Alignment";
            CheckWorkingPath();

            if (prj.Path != "")
            {
                IFeatureSet pvfs;
                pvfs = new FeatureSet(FeatureType.Line);
                pvfs.Projection = pvMap.Projection;
                pvfs.DataTable.Columns.Add(new DataColumn("Spacing", typeof(double)));
                pvfs.DataTable.Columns.Add(new DataColumn("Comment", typeof(string)));

                //++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                pvfs.Name = "Alignment";
                pvfs.Filename = prj.Path + "\\Temp\\" + pvfs.Name + ".shp";
                //++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                pvfs.SaveAs(pvfs.Filename, true);
                util.removeDupplicateLyr(pvfs.Name, pvMap);

                pvMap.Layers.Add(pvfs);

                cmbAlignmentLyr.Text = pvfs.Name;
                prj.LyrAlignmentName = pvfs.Name;
                loadLayerList();

                setCurrrentLayer(cmbAlignmentLyr.Text);
            }
        }

        frmMovePanel FormMovePanel;
        private void btnMovePanels_Click(object sender, EventArgs e)
        {
            pvMap.FunctionMode = FunctionMode.Select; 
            mapAct = mapAction.MovePanelCoord;
            FormMovePanel = new frmMovePanel();
            FormMovePanel.TopMost = true;
            FormMovePanel.Michael = this;
            FormMovePanel.PvMap = pvMap;
            FormMovePanel.ProjectFile = prj;
            FormMovePanel.Show();            
        }

        public void MoveSelectedPanels(bool left = false, bool right = false, bool up = false, bool down = false)
        {
            prj.LyrPole = util.getLayerHdl(prj.LyrPoleName, pvMap);
            prj.LyrPvPanel = util.getLayerHdl(prj.LyrPvPanelName, pvMap);
            if (prj.LyrPole != -1)
            {

                IMapFeatureLayer LocationFe = pvMap.Layers[prj.LyrPole] as IMapFeatureLayer;
                List<IFeature> lstFe = new List<IFeature>();
                ISelection selFe = LocationFe.Selection;
                lstFe = selFe.ToFeatureList();

                int iRow = 0;
                foreach (IFeature fs in lstFe)
                {
                    try
                    {
                        double Xspace = Convert.ToDouble(prj.HorzSpaceGlo);
                        double x = Convert.ToDouble(fs.DataRow["x"]);
                        double newX = x;

                        double Yspace = Convert.ToDouble(prj.VertSpaceGlo);
                        double y = Convert.ToDouble(fs.DataRow["y"]);
                        double newY = y;
                        if (left == true) 
                        {
                            newX = x - Xspace / 2;
                            newY = y;
                        }
                        if (right == true)
                        {
                            newX = x + Xspace / 2;
                            newY = y;
                        }
                        if (up == true) 
                        {
                            newY = y + Yspace / 4;
                            newX = x;
                        }
                        if (down == true)
                        {
                            newY = y - Yspace / 4;
                            newX = x;
                        }

                        
                        fs.DataRow.BeginEdit();
                        fs.DataRow["X"] = newX;
                        fs.DataRow["y"] = newY;
                        fs.DataRow.EndEdit();
                        LocationFe.DataSet.IndexMode = false;
                        LocationFe.DataSet.Save();
                        bool p = LocationFe.DataSet.AttributesPopulated;
                        // LocationFe.DataSet.

                        iRow++;
                        string xx = Convert.ToString(fs.DataRow["X"]);
                        //MessageBox.Show(xx);
                    }
                    catch { }

                }
                pvMap.ResetBuffer();
                    
               // this.Close();
            }
            CreatePvPanel();
        }

        public void resetMapAct()
        { mapAct = mapAction.None; }

        private void pvMap_KeyUp(object sender, KeyEventArgs e)
        {
            if (mapAct == mapAction.MovePanelCoord)
            {
                //MessageBox.Show(e.Modifiers.ToString() + e.KeyCode.ToString());
                if (e.KeyCode.ToString() == "NumPad4")
                {
                    MoveSelectedPanels(true, false, false, false);
                }
                if (e.KeyCode.ToString() == "NumPad8")
                {
                    MoveSelectedPanels(false, false, true, false);
                }
                if (e.KeyCode.ToString() == "NumPad6")
                {
                    //MessageBox.Show("Right");
                    MoveSelectedPanels(false,true,false,false);
                }
                if (e.KeyCode.ToString() == "NumPad2")
                {
                    MoveSelectedPanels(false, false, false, true);
                }

            }

                         if (e.Modifiers.ToString() + e.KeyCode.ToString() == "ControlNumPad4")
                    {
                        MoveSelectedPanels(true, false, false, false);
                    }
                         if (e.Modifiers.ToString() + e.KeyCode.ToString() == "ControlNumPad8")
                    {
                        MoveSelectedPanels(false, false, true, false);
                    }
                         if (e.Modifiers.ToString() + e.KeyCode.ToString() == "ControlNumPad6")
                    {
                        //MessageBox.Show("Right");
                        MoveSelectedPanels(false, true, false, false);
                    }
                         if (e.Modifiers.ToString() + e.KeyCode.ToString() == "ControlNumPad2")
                    {
                        MoveSelectedPanels(false, false, false, true);
                    }
        }

        private void cmbSiteArea_SelectedIndexChanged(object sender, EventArgs e)
        {

            refreshPrjLyrNames();
            lblGrdTitle.Text = "Energy Production Estimates for: " + prj.LyrSiteAreaName;
            splitContainer3.SplitterDistance = 20;
            if (splitContainer3.Visible == true)
            {
               cmdEnergyCal_Click(sender, e);
            }
            
            
        }

        private void btnKML_Click(object sender, EventArgs e)
        {
            AddKML();
        }

        #region create pole location layer

        public void CreateGridPole(bool SaveToShapefile = false, bool DefaultPanelProperties = true)
        {
            frmSiteArea = new frmCreatePoleBySiteArea();
           
            if (prj.Path == "")
            {
                FolderBrowserDialog folderSel = new FolderBrowserDialog();
                folderSel.Description = "Please select temporary directory location :";
                folderSel.ShowDialog();
                prj.Path = folderSel.SelectedPath;
            }
            if (prj.Path != "")
            {

                // for (int k = 1; k <= 100; k++)
                //{
                prj.NumPvPanel = 0;
                int lyrId = util.getLayerHdl(prj.LyrSiteAreaName, pvMap); //("Area_" + k, pvMap);
                if (prj.UseKML == true) lyrId = util.getLayerHdl(prj.LyrSiteAreaName, pvMap);
                if (lyrId != -1)
                {

                    IMapRasterLayer demLyr;
                    Raster dem4Pv = new Raster();
                    double poleH = 1; //Default pole height = 1 m.
                    double z0 = 0;
                    try { poleH = Convert.ToDouble(prj.PoleHeight); }
                    catch
                    {
                        MessageBox.Show("Pole height value error");
                        //txtPoleHeight.Text = "1";
                        poleH = 1;
                    }

                    int iDemLyr = prj.LyrDEM;// util.getLayerHdl(cmbDem.Text, pvMap);
                    if (iDemLyr != -1 & prj.DemChecked == true)   //chkDEM.Checked
                    {
                        demLyr = pvMap.Layers[iDemLyr] as IMapRasterLayer;
                        if (demLyr == null)
                        {
                            MessageBox.Show("Error: Dem data is not correct.");
                            return;
                        }
                        int mRow = demLyr.Bounds.NumRows;
                        int mCol = demLyr.Bounds.NumColumns;
                        dem4Pv = (Raster)demLyr.DataSet;
                        Coordinate ptReference = new Coordinate(prj.UtmE, prj.UtmN);
                        RcIndex rc = dem4Pv.ProjToCell(ptReference);
                        //RcIndex rc = demLyr.DataSet.ProjToCell(ptReference);
                        if (rc.Column < 0 | rc.Row < 0)
                        {
                            z0 = 0;
                        }
                        else
                        {
                            z0 = dem4Pv.Value[rc.Row, rc.Column];
                            //z0 = demLyr.DataSet.Value[rc.Row, rc.Column];
                        }
                    }
                    int alingmentLyr = lyrId;// getLayerHdl(cmbSolarFarmArea.Text);
                    if (alingmentLyr == -1)
                    {
                        MessageBox.Show("Site layer is incorrect.");
                        return;
                    }

                    if (alingmentLyr != -1)
                    {
                        IMapFeatureLayer siteFeLyr = pvMap.Layers[lyrId] as IMapFeatureLayer;
                        IMapFeatureLayer fSet;
                        try
                        {
                            fSet = pvMap.Layers[lyrId] as IMapPolygonLayer;
                        }
                        catch
                        {
                            MessageBox.Show("Site layer is incorrect.");
                            return;
                        }
                        if (fSet == null)
                        {
                            MessageBox.Show("Site layer is incorrect.");
                            return;
                        }


                        Coordinate c = new Coordinate();
                        IFeature siteFe = siteFeLyr.DataSet.GetFeature(0);
                        IEnvelope env = siteFe.Envelope;
                        c = (Coordinate)siteFe.Centroid().Coordinates[0];
                        DotSpatial.Topology.Point[] site = new DotSpatial.Topology.Point[siteFe.NumPoints + 1];
                        for (int i = 0; i < siteFe.NumPoints; i++)
                        {
                            site[i] = new DotSpatial.Topology.Point(siteFe.Coordinates[i].X, siteFe.Coordinates[i].Y);
                        }
                        site[siteFe.NumPoints] = new DotSpatial.Topology.Point(siteFe.Coordinates[0].X, siteFe.Coordinates[0].Y);

                        //MessageBox.Show(c.X.ToString()+","+c.Y.ToString());

                        FeatureSet fs = new FeatureSet(FeatureType.Point);
                        fs = new FeatureSet(FeatureType.Point);
                        //---------------------------------------------------------
                        fs.DataTable.Columns.Add(new DataColumn("x", typeof(double)));
                        fs.DataTable.Columns.Add(new DataColumn("y", typeof(double)));
                        fs.DataTable.Columns.Add(new DataColumn("w", typeof(double)));
                        fs.DataTable.Columns.Add(new DataColumn("h", typeof(double)));
                        fs.DataTable.Columns.Add(new DataColumn("Azimuth", typeof(double)));
                        fs.DataTable.Columns.Add(new DataColumn("Ele_Angle", typeof(double)));
                        fs.DataTable.Columns.Add(new DataColumn("ele", typeof(double)));
                        //---------------------------------------------------------

                        util.DrawLineCross(c.X, c.Y, 10, 1, Color.Red, pvMap);
                        double dx = Convert.ToDouble(prj.HorzSpaceGlo);   //txtGridSpacingX.Text
                        double dy = Convert.ToDouble(prj.VertSpaceGlo);   //txtGridSpacingY.Text
                        double ang = Convert.ToDouble(prj.GridRotAngGlo);   //pvPanelPoleGridCtl1.RotationAngle + 90;
                        //double size = 0.5;
                        pvMap.MapFrame.DrawingLayers.Clear();

                        double x0 = env.Minimum.X - c.X;
                        double xn = env.Maximum.X - c.X;
                        double y0 = env.Minimum.Y - c.Y;
                        double yn = env.Maximum.Y - c.Y;
                        double g1, g2;
                        if (Math.Abs(env.Maximum.X - env.Minimum.X) > Math.Abs(env.Maximum.Y - env.Minimum.Y))
                        { g1 = x0 * 1.5; g2 = xn * 1.5; }
                        else { g1 = y0 * 1.5; g2 = yn * 1.5; }


                        for (double x = g1; x <= g2; x += dx)
                        {
                            for (double y = g1; y <= g2; y += dy)
                            {
                                //double xx = c.X + kGeoFunc.Rx(x, y, ang);
                                //double yy = c.Y + kGeoFunc.Ry(x, y, ang);
                                Coordinate xy = util.Rotate(x, y, ang);
                                double xx = xy.X + c.X;
                                double yy = xy.Y + c.Y;
                                if (util.PointInPolygon(site, xx, yy) == true)
                                {
                                    //DrawLineCross(xx + c.X, yy + c.Y, 0.5, 1, Color.Magenta, pvMap);
                                    /*
                                    Coordinate[] L1 = new Coordinate[2]; //x-axis
                                    L1[0] = new Coordinate(xx - size, yy);
                                    L1[1] = new Coordinate(xx + size, yy);
                                    Coordinate[] L2 = new Coordinate[2]; //x-axis
                                    L2[0] = new Coordinate(xx, yy - size);
                                    L2[1] = new Coordinate(xx, yy + size);

                                    LineString ls1 = new LineString(L1);
                                    LineString ls2 = new LineString(L2);
                                    Feature f1 = new Feature(ls1);
                                    Feature f2 = new Feature(ls2);
                                    fs.Features.Add(f1);
                                    fs.Features.Add(f2);
                                   */
                                    double poleHeight = 1.5;
                                    Coordinate poleLocation = new Coordinate(xx, yy, poleHeight);
                                    IPoint poleFe = new DotSpatial.Topology.Point(poleLocation);
                                    IFeature ifea = fs.AddFeature(poleFe);
                                    prj.NumPvPanel++;
                                    if (prj.NumPvPanel > 40000)
                                    {
                                        MessageBox.Show("Site is too large, please draw a smaller site boundry.");
                                        return;
                                    }
                                    //------------------------------------------------------
                                    ifea.DataRow.BeginEdit();
                                    ifea.DataRow["x"] = xx;
                                    ifea.DataRow["y"] = yy;
                                    if (DefaultPanelProperties == true)
                                    {
                                        ifea.DataRow["w"] = prj.PvWidthGlo;   //lblWidth2.Text;
                                        ifea.DataRow["h"] = prj.PvHeightGlo;  //lblHeight2.Text;
                                        ifea.DataRow["Azimuth"] = prj.PvAzimuthGlo;   //lblAzimuth2.Text;
                                        ifea.DataRow["Ele_Angle"] = prj.PvTiltGlo;   //lblTilt2.Text;

                                    }
                                    else
                                    {
                                        ifea.DataRow["w"] = 0;
                                        ifea.DataRow["h"] = 0;
                                        ifea.DataRow["Azimuth"] = 0;
                                        ifea.DataRow["Ele_Angle"] = 0;
                                    }

                                    if (iDemLyr != -1 & prj.DemChecked == true) //chkDEM.Checked
                                    {
                                        Coordinate ptReference = new Coordinate(xx, yy);
                                        RcIndex rc = dem4Pv.ProjToCell(ptReference);
                                        double z = 0;
                                        if (rc.Column < 0 | rc.Row < 0)
                                        {
                                            z = 0;
                                        }
                                        else
                                        {
                                            z = dem4Pv.Value[rc.Row, rc.Column];
                                        }
                                        //demLyr.DataSet
                                        if (prj.AssumeDatum == true)   //radioAboveAssumeDatum.Checked
                                        {
                                            ifea.DataRow["ele"] = poleH;
                                        }
                                        else
                                        {
                                            ifea.DataRow["ele"] = z - z0 + poleH;
                                        }
                                    }
                                    else
                                    {
                                        ifea.DataRow["ele"] = poleH;
                                    }
                                    //
                                    ifea.DataRow.EndEdit();
                                }

                            }
                        }
                        fs.Projection = pvMap.Projection;
                        String AreaName = prj.LyrSiteAreaName;
                        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                        fs.Name = AreaName + " Panel Position"; //"Panel Position_" + k;                            
                        fs.Filename = prj.Path + "\\Temp\\" + fs.Name + ".shp";  //"\\Temp\\Panel Position_" + k + ".shp";
                        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                        fs.SaveAs(prj.Path + "\\Temp\\" + fs.Name + ".shp", true);
                        util.removeDupplicateLyr(fs.Name, pvMap);
                        MapPointLayer drawing;
                        drawing = new MapPointLayer(fs);
                        drawing.Symbolizer = new PointSymbolizer(Color.Blue, DotSpatial.Symbology.PointShape.Triangle, 5);
                        util.removeDupplicateLyr(AreaName + " Panel Position", pvMap);
                        if (SaveToShapefile == true)
                        {
                            pvMap.Layers.Add(fs.Filename);
                            prj.LyrPoleName = fs.Name;

                        }
                        else
                        {

                            pvMap.MapFrame.DrawingLayers.Add(drawing);
                            prj.LyrPoleName = "";
                        }
                        prj.Verify[5] = true;
                        frmSiteArea.lblNoOfPole.Visible = true;

                       // frmSiteArea.lblNoOfPole.Text = "Panels in " + prj.LyrSiteAreaName + ": " + prj.NumPvPanel.ToString();
                        //frmSiteArea.toolTip1.SetToolTip(frmSiteArea.lblNoOfPole, frmSiteArea.lblNoOfPole.Text);
                        /*
                                        drawing = new MapLineLayer(fs);
                                        drawing.Symbolizer = new LineSymbolizer(Color.Magenta, 1);
                                        pvMap.MapFrame.DrawingLayers.Add(drawing);
                                        // Request a redraw
                                        pvMap.MapFrame.Invalidate();
                                        grpBLineInfo.Visible = false;
                                        loadLayerList();
                        */
                        //updateArea();

                    }


                }
                //}
                //pvMap.MapFrame.DrawingLayers.Clear();
                //-----------------------------------------------------------------------------------



            }
        }
        #endregion

    }
}