using DotSpatial.Controls;
using DotSpatial.Data;
using System.Collections.Generic;
using DotSpatial.Topology;
using DotSpatial.Projections;
using PvMapperPlugin;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using DotSpatial.Symbology;
using System.Data;
using System.Threading.Tasks;
using kGEOClassLibrary;
using ZedGraph;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Xml;
using PvDesktopPlugin;
using Microsoft.Office.Interop.Word;
using System.Data.OleDb;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace pvMapperPlugin
{

    partial class pvMapForm : Form
    {

        #region "- VARIABLE"
        //
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
        
        weatherSta[] wSta = new weatherSta[nSta];
        List<string> listState = new List<string>();
        bool UserDefined = false; 
        //--------------------------------------------------
        public bool pickRoseLocation = false;
        public bool pickBLLocation = false;
        private IMap PvMap;
        private Color color;
        private int b;
        int tab1Height;
        int tab2Height;
        int tab3Height;
        int tab4Height;
        int tab5Height;
        int treeIndex = 0;
        bool loaded = true;
        private List<Coordinate> _baselinePath;
        bool[] verify = new bool[6] { false, false, false, false, false, false };
        public Map vMap;
        //public IMapFeatureLayer[] layers;
        //public MapFunctionLayout _Painter;
        //var _Painter = new frmLayoutDialog();
        double BLineLengthTotal = 0;
        double BLineLength = 0;
        double lasetAng = 0;
        double iX, iY;
        int currentImage = 0;
        int activeTreeGrdRow = 0;
        //Tree variable
        double[] whRatio = new double[10];
        string[] treeTypeName = new string[10];
        double[,] treeShape = new double[10, 2];
        int numPvPanel = 0;
        double[] dailyAcStore = new double[366];
        bool GrdPole = false;
        bool haveOptimizeGraph = false;
        string pvDir = "";
        string pvTmpDir = "";
        #endregion

        #region "- ADD EVENTs"
        //Add mouse down event
        public IMap pvMap
        {
            get { return PvMap; }
            set
            {
                PvMap = value;
                vMap = (Map)value;
                vMap.MouseClick += Map_MouseDown;
                vMap.MouseMove += Map_MouseMove;
            }
        }
        #endregion

        #region "- Assembly Attribute Accessors"

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        #endregion Assembly Attribute Accessors

        #region "- FORM OBJECT EVENTS"

        //---------------------------------------------------------
        public pvMapForm()
        {
            InitializeComponent();
            this.Text = String.Format("PvMapper Desktop");

            string CurrentDir = System.IO.Directory.GetCurrentDirectory();
            int n = (CurrentDir.ToUpper()).IndexOf("PLUGINS");
            if (n == -1)
            {
                pvDir = CurrentDir + "\\Plugins\\pvDesktop";
            }
            else
            {
                pvDir = CurrentDir;
            }

           // MessageBox.Show(pvDir); 

            //Panel configuration
            #region "Panel configuration"
            panelTab1.Width = panelTabMain.Width - 5;
            tab1Height = 175;// panelTab1.Height;
            tab2Height = 375;// panelTab2.Height;
            tab3Height = 250;// panelTab3.Height;
            tab4Height = 184;// panelTab4.Height;
            tab5Height = 350;// panelTab5.Height;
            panelTab2.Left = panelTab1.Left;
            panelTab2.Width = panelTab1.Width;
            panelTab3.Left = panelTab1.Left;
            panelTab3.Width = panelTab1.Width;
            panelTab4.Left = panelTab1.Left;
            panelTab4.Width = panelTab1.Width;
            panelTab5.Left = panelTab1.Left;
            panelTab5.Width = panelTab1.Width;
            lblTab1.Tag = "-";
            lblTab2.Tag = "-";
            lblTab4.Tag = "-";
            lblTab5.Tag = "-";
            updateTab(lblTab1, panelTab1, tab1Height, picButtTab1);
            updateTab(lblTab2, panelTab2, tab2Height, picButtTab2);
            updateTab(lblTab3, panelTab3, tab3Height, picButtTab3);
            updateTab(lblTab4, panelTab4, tab4Height, picButtTab4);
            updateTab(lblTab5, panelTab5, tab5Height, picButtTab5);
            updatePanelTab();
            #endregion

            //Solar calculation table
            #region "Solar calculation table"
            this.grdYearResult.Columns[0].HeaderText = "Day";
            this.grdYearResult.Columns[1].HeaderText = "Sunrise Time";
            this.grdYearResult.Columns[2].HeaderText = "Sunset Time";
            this.grdYearResult.Columns[3].HeaderText = "Sunlight Duration (min)";
            this.grdYearResult.Columns[4].HeaderText = "Equation of Time (min)";
            this.grdYearResult.Columns[5].HeaderText = "Solar Declination (Deg)";
            this.grdYearResult.Columns[6].HeaderText = "Solar Elevation angle (Deg)";
            this.grdYearResult.Columns[7].HeaderText = "Solar Azimuth Angle (Deg)";
            this.grdYearResult.Columns[8].HeaderText = "Sunrise Azimuth Angle (Deg)";
            this.grdYearResult.Columns[9].HeaderText = "Sunset Azimuth Angle (Deg)";
            #endregion
            //
            lblOther.Tag = "Off";
            //grpBLineInfo.Top = grpSpacing.Top;
            //grpBLineInfo.Left = grpSpacing.Left;
            lblLengthCmd.Tag = "None";
            lblAngCmd.Tag = "None";
            lblAzimuthCmd.Tag = "None";
            pvAz.AzimutAngle = 0;
            pvTilt.tiltAngle = 0;

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
                 
            }
            else
            {
                MessageBox.Show("Weather station file not found");
                panelTab1.Enabled = false;
                panelTab2.Enabled = false;
                panelTab3.Enabled = false;
                panelTab4.Enabled = false;
                panelTab5.Enabled = false;
            }
            optMultiWeatherSta.Checked = true;

            //Initial Azimuth Setup
            try {pvAz.AzimutAngle = 180;} catch { }

        }

        private void cmdSelect_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(pvMap.Layers[0].LegendText);  
            IMapLayer lyr = pvMap.Layers[0];
            MessageBox.Show(lyr.DataSet.Name);
        }

        private void Map_MouseMove(object sender, MouseEventArgs e)
        {
            if (pickBLLocation == true & grdBaseline.RowCount > 0)
            {
                pvMap.MapFrame.DrawingLayers.Clear();
                Double r = 0.25;
                Coordinate c = new Coordinate();
                c = vMap.PixelToProj(e.Location);

                if (_baselinePath.Count >= 1)
                {
                    Coordinate[] L = new Coordinate[_baselinePath.Count + 1];
                    int n = 0;
                    foreach (Coordinate cBL in _baselinePath)
                    {
                        L[n] = new Coordinate(cBL);
                        n++;
                    }

                    #region "-- Other check"
                    if (lblOther.Tag == "On")
                    {
                        double dx = Math.Abs(c.X - L[n - 1].X);
                        double dy = Math.Abs(c.Y - L[n - 1].Y);
                        if (dx >= dy)
                        {
                            L[n] = new Coordinate(c.X, L[n - 1].Y);
                        }
                        else
                        {
                            L[n] = new Coordinate(L[n - 1].X, c.Y);
                        }
                        iX = L[n].X;
                        iY = L[n].Y;
                    }
                    else
                    {
                        L[n] = new Coordinate(c.X, c.Y);
                    }
                    #endregion

                    LineString ls = new LineString(L);
                    Feature f = new Feature(ls);
                    FeatureSet fs = new FeatureSet(f.FeatureType);
                    fs.Features.Add(f);
                    MapLineLayer rangeRingAxis;
                    rangeRingAxis = new MapLineLayer(fs);// MapPolygonLayer(fs);
                    rangeRingAxis.Symbolizer = new LineSymbolizer(System.Drawing.Color.Blue, 2);
                    pvMap.MapFrame.DrawingLayers.Add(rangeRingAxis);
                    pvMap.MapFrame.Invalidate();

                    #region "--Specific Length"
                    if (lblLengthCmd.Tag == "Select")
                    {
                        double rBL = Convert.ToDouble(txtBLineDumy.Text);
                        double x0 = L[n - 1].X;
                        double y0 = L[n - 1].Y;
                        iX = x0; iY = y0;
                        kDrawCircle(x0, y0, rBL, 120, pvMap, Color.Gray);
                        if (lblOther.Tag == "On")
                        {
                            if (L[n].X > x0) iX = x0 + rBL;
                            if (L[n].X < x0) iX = x0 - rBL;
                            if (L[n].Y > y0) iY = y0 + rBL;
                            if (L[n].Y < y0) iY = y0 - rBL;
                        }
                        else
                        {
                            kGeoFunc.LineType tmpL = new kGeoFunc.LineType();
                            tmpL.Pt1.X = 0;
                            tmpL.Pt1.Y = 0;
                            tmpL.Pt2.X = L[n].X - x0;
                            tmpL.Pt2.Y = L[n].Y - y0;
                            double Ang2 = kGeoFunc.AzmAngle(tmpL);
                            double x1 = Math.Sin(Math.PI * Ang2 / 180) * rBL + x0;
                            double y1 = Math.Cos(Math.PI * Ang2 / 180) * rBL + y0;
                            iX = x1;
                            iY = y1;
                        }
                        kDrawCircle(iX, iY, 1, 10, pvMap, Color.Magenta);
                        DrawLine(x0, y0, iX, iY, 2, Color.Magenta);
                    }
                    #endregion

                    #region"--Specific Azimuth"
                    if (lblAzimuthCmd.Tag == "Select" | lblAngCmd.Tag == "Select")
                    {
                        double Ang2 = Convert.ToDouble(txtBLineDumy.Text);
                        if (lblAngCmd.Tag == "Select") Ang2 = lasetAng - Ang2;
                        double x0 = L[n - 1].X;
                        double y0 = L[n - 1].Y;
                        double rBL = Math.Sqrt(Math.Pow(c.X - x0, 2) + Math.Pow(c.Y - y0, 2));
                        iX = x0; iY = y0;
                        iX = Math.Sin(Math.PI * Ang2 / 180) * rBL + x0;
                        iY = Math.Cos(Math.PI * Ang2 / 180) * rBL + y0;
                        kDrawCircle(x0, y0, rBL, 72, pvMap, Color.Magenta);
                        DrawLine(x0, y0, iX, iY, 2, Color.Magenta);
                    }
                    #endregion


                    kGeoFunc.LineType bLine = new kGeoFunc.LineType();
                    bLine.Pt1.X = L[n - 1].X;
                    bLine.Pt1.Y = L[n - 1].Y;
                    bLine.Pt2.X = L[n].X;
                    bLine.Pt2.Y = L[n].Y;
                    double AzAng = Math.Round(kGeoFunc.AzmAngle(bLine), 3);
                    double Ang = Math.Round(AzAng - lasetAng, 3);
                    double Length = Math.Round(kGeoFunc.Length(bLine), 3);
                    lblBlineAngle.Text = Ang.ToString();
                    lblBlineAzimuth.Text = AzAng.ToString();
                    lblBlineLength.Text = Length.ToString();
                    lblBLineTotal1.Text = (BLineLengthTotal + Length).ToString();
                }
                // MapCanvas.MapFrame.Invalidate();
            }
        }

        private void Map_MouseDown(object sender, MouseEventArgs e)
        {

            #region "ROSE Chart // if (pickRoseLocation == true)"
            if (pickRoseLocation == true)
            {
                Pen MyPen = new Pen(Color.Black);
                Coordinate c = new Coordinate();
                c = vMap.PixelToProj(e.Location);
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
                    rr = Math.Sqrt(Math.Pow((latlong[0] - wSta[i].LONG2), 2) + Math.Pow((latlong[1] - wSta[i].LAT2),2));
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
                txtUtmN.Text = c.Y.ToString();
                txtUtmE.Text = c.X.ToString();
                pvMap.MapFrame.DrawingLayers.Clear();
                kDrawCircle(c.X, c.Y, 20000, 360, pvMap, Color.Magenta);
                //MessageBox.Show(c.X + "," + c.Y);
                Double r = 0.25;
                int nIDW = Convert.ToInt16(txtNIdwSta.Text);  
                kDrawCircle(c.X, c.Y, r, 360, pvMap, Color.Magenta);
                for (int ii = 0; ii < nIDW; ii++)
                {
                    double lat = wSta[wStaSel[ii]].LAT2;
                    double lng = wSta[wStaSel[ii]].LONG2;
                    double x = 0;
                    double y = 0;
                    double[] mapCoordinate = new double[] { lng, lat };
                    Reproject.ReprojectPoints(mapCoordinate, new double[] { 0 }, KnownCoordinateSystems.Geographic.World.WGS1984, pvMap.Projection, 0, 1);
                    kDrawCircle(mapCoordinate[0], mapCoordinate[1], 10000, 360, pvMap, Color.Magenta);
                    DrawLine(mapCoordinate[0], mapCoordinate[1], c.X, c.Y, 2, Color.Magenta);
                }

                pickRoseLocation = false;
                try
                {
                    picLoading.Visible = true; picLoading.Refresh();
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
                picLoading.Visible = false; picLoading.Refresh();


            }
            #endregion

            #region"Baseline //if (pickBLLocation == true)"
            if (pickBLLocation == true)
            {
                if (e.Button != MouseButtons.Right)
                {
                    Pen MyPen = new Pen(Color.Black);
                    Coordinate c = new Coordinate();
                    Coordinate c2 = new Coordinate();
                    c = vMap.PixelToProj(e.Location);
                    this.grdBaseline.Rows.Add();
                    int n = this.grdBaseline.Rows.Count - 2;
                    this.grdBaseline.Rows[n].Cells[0].Value = n;
                    this.grdBaseline.Rows[n].Cells[1].Value = c.X;
                    this.grdBaseline.Rows[n].Cells[2].Value = c.Y;

                    #region "-- Ortho check"
                    if (lblOther.Tag == "On")
                    {
                        c2 = new Coordinate(c.X, c.Y);
                        if (_baselinePath.Count > 0)
                        {
                            c2 = new Coordinate(iX, iY);
                        }
                    }
                    else
                    {
                        c2 = new Coordinate(c.X, c.Y);
                    }
                    #endregion

                    #region "-- Layout command Check"

                    if (lblLengthCmd.Tag == "Select" | lblAzimuthCmd.Tag == "Select" | lblAngCmd.Tag == "Select")
                    {
                        c2 = new Coordinate(iX, iY);
                        ClearBLineCmd();
                    }
                    #endregion

                    //------------------------------------------
                    this.grdBaseline.Rows[n].Cells[1].Value = c2.X;
                    this.grdBaseline.Rows[n].Cells[2].Value = c2.Y;

                    _baselinePath.Add(c2);

                    //------------------------------------------

                    #region "--Add path coordinate table"
                    if (_baselinePath.Count > 1)
                    {
                        n = _baselinePath.Count - 1;
                        kGeoFunc.LineType bLine = new kGeoFunc.LineType();
                        bLine.Pt1.X = _baselinePath[n - 1].X;
                        bLine.Pt1.Y = _baselinePath[n - 1].Y;
                        bLine.Pt2.X = _baselinePath[n].X;
                        bLine.Pt2.Y = _baselinePath[n].Y;
                        double AzAng = Math.Round(kGeoFunc.AzmAngle(bLine), 3);
                        double Ang = Math.Round(AzAng - lasetAng, 3);
                        double Length = Math.Round(kGeoFunc.Length(bLine), 3);
                        BLineLength = Length;
                        lasetAng = AzAng;
                        BLineLengthTotal += BLineLength;
                        lblBLineTotal1.Text = BLineLengthTotal.ToString();
                        //lblBLineTotal2.Text = BLineLengthTotal.ToString();
                    }
                    #endregion
                }
                else
                {
                    pickBLLocation = false;
                    grpBLineInfo.Visible = false;
                    if (_baselinePath.Count > 1)
                    {
                        pvMap.MapFrame.DrawingLayers.Clear();
                        Coordinate[] L = _baselinePath.ToArray();
                        LineString ls = new LineString(L);
                        Feature f = new Feature(ls);
                        FeatureSet fs = new FeatureSet(f.FeatureType);
                        fs.Features.Add(f);
                        MapLineLayer rangeRingAxis;
                        rangeRingAxis = new MapLineLayer(fs);// MapPolygonLayer(fs);
                        rangeRingAxis.Symbolizer = new LineSymbolizer(System.Drawing.Color.Magenta, 1);
                        pvMap.MapFrame.DrawingLayers.Add(rangeRingAxis);
                        pvMap.MapFrame.Invalidate();
                    }
                }
            }

            #endregion
        }

        private void pvMapForm_Load(object sender, EventArgs e)
        {
            panelTab1.Width = panelTabMain.Width - 5;
            tab1Height = 140;// panelTab1.Height;
            tab2Height = 375;// panelTab2.Height;
            tab3Height = 275;// panelTab3.Height;
            tab4Height = 175;// panelTab4.Height;
            tab5Height = 350;// panelTab5.Height;
            panelTab2.Left = panelTab1.Left;
            panelTab2.Width = panelTab1.Width;
            panelTab3.Left = panelTab1.Left;
            panelTab3.Width = panelTab1.Width;
            panelTab4.Left = panelTab1.Left;
            panelTab4.Width = panelTab1.Width;
            panelTab5.Left = panelTab1.Left;
            panelTab5.Width = panelTab1.Width;
            lblTab1.Tag = "-";
            lblTab2.Tag = "-";
            lblTab4.Tag = "-";
            lblTab5.Tag = "-";
            updateTab(lblTab1, panelTab1, tab1Height, picButtTab1);
            updateTab(lblTab2, panelTab2, tab2Height, picButtTab2);
            updateTab(lblTab3, panelTab3, tab3Height, picButtTab3);
            updateTab(lblTab4, panelTab4, tab4Height, picButtTab4);
            updateTab(lblTab5, panelTab5, tab5Height, picButtTab5);
            updatePanelTab();
        }

        private void cmdRoseModel_Click(object sender, EventArgs e)
        {
            if (pvTmpDir == "")
            {
                FolderBrowserDialog folderSel = new FolderBrowserDialog();
                folderSel.Description = "Please select temporary directory location :";
                folderSel.ShowDialog();
                pvTmpDir = folderSel.SelectedPath;
            }
            if (pvTmpDir != "")
            {

                pickBLLocation = false;

                RoseChart rc = new RoseChart();
                prgBar.Maximum = 366 * 24;
                prgBar.Minimum = 0;
                prgBar.Value = 0;
                prgBar.Visible = true;
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
                            prgBar.PerformStep();
                        }
                    }
                }
                prgBar.Visible = false;
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
                grdSolarRose.DataSource = table;
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
                    fea.SaveAs(pvTmpDir + "\\Temp\\Solar rose diagram.shp", true);
                    fea.Name = "Solar radiation rose";
                    removeDupplicateLyr(fea.Name);
                    pvMap.Layers.Add(fea);
                }
                //------------------------------
                YearlyCal();
                verify[3] = true;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(dateTimePicker1.Value.ToString());
            #region  input data
            //--------------------------------------------------------------
            double Latitude = Convert.ToDouble(this.txtLAT.Text);
            double Longitude = Convert.ToDouble(this.txtLNG.Text);
            Int16 TimeZone_ = Convert.ToInt16(this.txtTimeZone.Text);
            double TimePassMidnight = 0.1 / 24;
            //--------------------------------------------------------------
            #endregion
            this.grdSolarDay.Rows.Clear();
            this.grdSolarDay.Rows.Add(28);

            //this.Text = this.dateTimePicker1.Value.ToString("yyyy");

            SolarCal solar = new SolarCal(this.dateTimePicker1.Value, TimePassMidnight, Latitude, Longitude, TimeZone_);

            this.grdSolarDay.Rows[0].Cells[0].Value = "Julian Day";
            this.grdSolarDay.Rows[1].Cells[0].Value = "Julian Century";
            this.grdSolarDay.Rows[2].Cells[0].Value = "Geom Mean Long Sun (deg)";
            this.grdSolarDay.Rows[3].Cells[0].Value = "Geom Mean Anom Sun (deg)";
            this.grdSolarDay.Rows[4].Cells[0].Value = "Eccent Earth Orbit";
            this.grdSolarDay.Rows[5].Cells[0].Value = "Sun Eq of Ctr";
            this.grdSolarDay.Rows[6].Cells[0].Value = "Sun True Long (deg)";
            this.grdSolarDay.Rows[7].Cells[0].Value = "Sun True Anom (deg)";
            this.grdSolarDay.Rows[8].Cells[0].Value = "Sun Rad Vector (AUs)";
            this.grdSolarDay.Rows[9].Cells[0].Value = "Sun App Long (deg)";
            this.grdSolarDay.Rows[10].Cells[0].Value = "Mean Obliq Ecliptic (deg)";
            this.grdSolarDay.Rows[11].Cells[0].Value = "Obliq Corr (deg)";
            this.grdSolarDay.Rows[12].Cells[0].Value = "Sun Right Ascension (deg)";
            this.grdSolarDay.Rows[13].Cells[0].Value = "Sun Declination (deg)";
            this.grdSolarDay.Rows[14].Cells[0].Value = "var y";
            this.grdSolarDay.Rows[15].Cells[0].Value = "Equation of Time (minutes)";
            this.grdSolarDay.Rows[16].Cells[0].Value = "Hour Angle of Sunrise (deg)";
            this.grdSolarDay.Rows[17].Cells[0].Value = "Solar Noon (LST)";
            this.grdSolarDay.Rows[18].Cells[0].Value = "Sunrise Time (LST)";
            this.grdSolarDay.Rows[19].Cells[0].Value = "Sunset Time (LST)";
            this.grdSolarDay.Rows[20].Cells[0].Value = "Sunlight Duration (minutes)";
            this.grdSolarDay.Rows[21].Cells[0].Value = "True Solar Time (min)";
            this.grdSolarDay.Rows[22].Cells[0].Value = "Hour Angle (deg)";
            this.grdSolarDay.Rows[23].Cells[0].Value = "Solar Zenith Angle (deg)";
            this.grdSolarDay.Rows[24].Cells[0].Value = "Solar Elevation Angle (deg)";
            this.grdSolarDay.Rows[25].Cells[0].Value = "Approx Atmospheric Refraction (deg)";
            this.grdSolarDay.Rows[26].Cells[0].Value = "Solar Elevation corrected for atm refraction (deg)";
            this.grdSolarDay.Rows[27].Cells[0].Value = "Solar Azimuth Angle (deg cw from N)";
            //
            this.grdSolarDay.Rows[0].Cells[1].Value = solar.JulianDay;//Julian Day;
            this.grdSolarDay.Rows[1].Cells[1].Value = solar.JulianCentury;//Julian Century;
            this.grdSolarDay.Rows[2].Cells[1].Value = solar.GeomMeanLongSun;//Geom Mean Long Sun (deg);
            this.grdSolarDay.Rows[3].Cells[1].Value = solar.GeomMeanAnomSun;//Geom Mean Anom Sun (deg);
            this.grdSolarDay.Rows[4].Cells[1].Value = solar.EccentEarthOrbit;//Eccent Earth Orbit;
            this.grdSolarDay.Rows[5].Cells[1].Value = solar.SunEqofCtr;//Sun Eq of Ctr;
            this.grdSolarDay.Rows[6].Cells[1].Value = solar.SunTrueLong;//Sun True Long (deg);
            this.grdSolarDay.Rows[7].Cells[1].Value = solar.SunTrueAnom;//Sun True Anom (deg);
            this.grdSolarDay.Rows[8].Cells[1].Value = solar.SunRadVector;//Sun Rad Vector (AUs);
            this.grdSolarDay.Rows[9].Cells[1].Value = solar.SunAppLong;//Sun App Long (deg);
            this.grdSolarDay.Rows[10].Cells[1].Value = solar.MeanObliqEcliptic;//Mean Obliq Ecliptic (deg);
            this.grdSolarDay.Rows[11].Cells[1].Value = solar.ObliqCorr;//Obliq Corr (deg);
            this.grdSolarDay.Rows[12].Cells[1].Value = solar.SunRtAscen;//Sun Rt Ascen (deg);
            this.grdSolarDay.Rows[13].Cells[1].Value = solar.SunDeclin;//Sun Declin (deg);
            this.grdSolarDay.Rows[14].Cells[1].Value = solar.varY;//var y;
            this.grdSolarDay.Rows[15].Cells[1].Value = solar.EqOfTime_min;//Eq of Time (minutes);
            this.grdSolarDay.Rows[16].Cells[1].Value = solar.HASunrise;//HA Sunrise (deg);
            this.grdSolarDay.Rows[17].Cells[1].Value = solar.SolarNoon;//Solar Noon (LST);
            this.grdSolarDay.Rows[18].Cells[1].Value = solar.SunriseTime;//Sunrise Time (LST);
            this.grdSolarDay.Rows[19].Cells[1].Value = solar.SunsetTime;//Sunset Time (LST);
            this.grdSolarDay.Rows[20].Cells[1].Value = solar.SunlightDuration_min;//Sunlight Duration (minutes);
            this.grdSolarDay.Rows[21].Cells[1].Value = solar.TrueSolarTimemin;//True Solar Time (min);
            this.grdSolarDay.Rows[22].Cells[1].Value = solar.HourAngle;//Hour Angle (deg);
            this.grdSolarDay.Rows[23].Cells[1].Value = solar.SolarZenithAngle;//Solar Zenith Angle (deg);
            this.grdSolarDay.Rows[24].Cells[1].Value = solar.SolarElevationAngle;//Solar Elevation Angle (deg);
            this.grdSolarDay.Rows[25].Cells[1].Value = solar.ApproxAtmosphericRefraction;//Approx Atmospheric Refraction (deg);
            this.grdSolarDay.Rows[26].Cells[1].Value = solar.SolarElevationCorrectedForAtmRefraction;//Solar Elevation corrected for atm refraction (deg);
            this.grdSolarDay.Rows[27].Cells[1].Value = solar.SolarAzimuthAngle;//Solar Azimuth Angle (deg cw from N);
        }

        private void cmdSolar_YearlyCal_Click(object sender, EventArgs e)
        {
            YearlyCal();
        }

        void YearlyCal()
        {
            #region  input data
            //--------------------------------------------------------------
            double Latitude = Convert.ToDouble(this.txtLAT.Text);
            double Longitude = Convert.ToDouble(this.txtLNG.Text);
            Int16 TimeZone_ = Convert.ToInt16(this.txtTimeZone.Text);
            //--------------------------------------------------------------
            #endregion
            zGraphAz2EleAng.GraphPane.CurveList.Clear();
            zGraphAz2EleAng.GraphPane.GraphObjList.Clear();
            zGraphAz2Day.GraphPane.CurveList.Clear();
            zGraphAz2Day.GraphPane.GraphObjList.Clear();
            zGraphEleAng2Day.GraphPane.CurveList.Clear();
            zGraphEleAng2Day.GraphPane.GraphObjList.Clear();
            // --------------------------------------------------------------
            GraphPane Az2ElePanel = new GraphPane();
            PointPairList listAz2EleAng = new PointPairList();
            LineItem listAz2EleAng_;
            Az2ElePanel = zGraphAz2EleAng.GraphPane;
            Az2ElePanel.Title.Text = "Solar Azimuth and Elevation Angle";
            Az2ElePanel.XAxis.Title.Text = "Solar Azimuth (deg)";
            Az2ElePanel.YAxis.Title.Text = "Elevation Angle (deg)";
            // --------------------------------------------------------------
            // --------------------------------------------------------------
            GraphPane Az2DayPanel = new GraphPane();
            PointPairList listAz2Day = new PointPairList();
            LineItem listAz2Day_;
            PointPairList listSunriseAz2Day = new PointPairList();
            //LineItem listSunriseAz2Day_;
            PointPairList listSunsetAz2Day = new PointPairList();
            //LineItem listSunsetAz2Day_; 
            Az2DayPanel = zGraphAz2Day.GraphPane;
            Az2DayPanel.Title.Text = "Solar Azimuth vs Day";
            Az2DayPanel.XAxis.Title.Text = "Day of year";
            Az2DayPanel.XAxis.Scale.Max = 366;
            Az2DayPanel.YAxis.Title.Text = "Solar Azimuth (deg)";

            // --------------------------------------------------------------
            // --------------------------------------------------------------
            GraphPane Ele2DayPanel = new GraphPane();
            PointPairList listEle2Day = new PointPairList();
            LineItem listEle2Day_;
            Ele2DayPanel = zGraphEleAng2Day.GraphPane;
            Ele2DayPanel.Title.Text = "Solar Elevation Angle vs Day";
            Ele2DayPanel.XAxis.Title.Text = "Day of year";
            Ele2DayPanel.XAxis.Scale.Max = 366;
            Ele2DayPanel.YAxis.Title.Text = "Elevation Angle (deg)";
            // --------------------------------------------------------------

            this.grdYearResult.Rows.Clear();

            int i = 0;
            int year = Convert.ToInt32(this.dateTimePicker1.Value.ToString("yyyy"));

            this.grdYearResult.Columns[0].HeaderText = "Date";
            this.grdYearResult.Columns[1].HeaderText = "Sunrise Time";
            this.grdYearResult.Columns[2].HeaderText = "Sunset Time";
            this.grdYearResult.Columns[3].HeaderText = "Sunlight Duration (min)";
            this.grdYearResult.Columns[4].HeaderText = "Equation of Time (min)";
            this.grdYearResult.Columns[5].HeaderText = "Solar Declination (Deg)";
            this.grdYearResult.Columns[6].HeaderText = "Solar Elevation angle (Deg)";
            this.grdYearResult.Columns[7].HeaderText = "Solar Azimuth Angle (Deg)";
            this.grdYearResult.Columns[8].HeaderText = "Sunrise Azimuth Angle (Deg)";
            this.grdYearResult.Columns[9].HeaderText = "Sunset Azimuth Angle (Deg)";
            for (int month = 1; month <= 12; month++)
            {
                int month_day = System.DateTime.DaysInMonth(2001, month);
                for (int day = 1; day <= month_day; day++)
                {
                    this.grdYearResult.Rows.Add();
                    SolarCal ySolar = new SolarCal(day, month, year, 0.5, Latitude, Longitude, TimeZone_);
                    this.grdYearResult.Rows[i].Cells[0].Value = month + "/" + day + "/" + year;
                    this.grdYearResult.Rows[i].Cells[1].Value = Math.Round(ySolar.SunriseTime, 3);
                    this.grdYearResult.Rows[i].Cells[2].Value = Math.Round(ySolar.SunsetTime, 3);
                    this.grdYearResult.Rows[i].Cells[3].Value = Math.Round(ySolar.SunlightDuration_min, 3);
                    this.grdYearResult.Rows[i].Cells[4].Value = Math.Round(ySolar.EqOfTime_min, 3);
                    this.grdYearResult.Rows[i].Cells[5].Value = Math.Round(ySolar.SunDeclin, 3);
                    this.grdYearResult.Rows[i].Cells[6].Value = Math.Round(ySolar.SolarElevationAngle, 3);
                    this.grdYearResult.Rows[i].Cells[7].Value = Math.Round(ySolar.SolarAzimuthAngle, 3);
                    //--------------------------------------------------------------------------
                    SolarCal ySunrise = new SolarCal(day, month, year, ySolar.SunriseTime, Latitude, Longitude, TimeZone_);
                    this.grdYearResult.Rows[i].Cells[8].Value = Math.Round(ySunrise.SolarAzimuthAngle, 3);
                    SolarCal ySunset = new SolarCal(day, month, year, ySolar.SunsetTime, Latitude, Longitude, TimeZone_);
                    this.grdYearResult.Rows[i].Cells[9].Value = Math.Round(ySunset.SolarAzimuthAngle, 3);
                    i++;
                    // draw a sin curve Az vs Ele Angle  
                    listAz2EleAng.Add(ySolar.SolarAzimuthAngle, ySolar.SolarElevationAngle);
                    // draw a sin curve Az vs day  
                    listAz2Day.Add(i, ySolar.SolarAzimuthAngle);
                    listSunriseAz2Day.Add(i, ySunrise.SolarAzimuthAngle);
                    listSunsetAz2Day.Add(i, ySunset.SolarAzimuthAngle);
                    // draw a sin curve Ele Angle vs day
                    listEle2Day.Add(i, ySolar.SolarElevationAngle);
                }
            }
            // set lineitem to list of points  
            listAz2EleAng_ = Az2ElePanel.AddCurve("Az vs Ele. Angle (deg.)", listAz2EleAng, Color.Black, ZedGraph.SymbolType.Circle);
            //
            listAz2Day_ = Az2DayPanel.AddCurve("Noon Az.", listAz2Day, Color.Black, ZedGraph.SymbolType.Circle);
            listAz2Day_ = Az2DayPanel.AddCurve("Sunrise Az.", listSunriseAz2Day, Color.Magenta, ZedGraph.SymbolType.None);
            listAz2Day_ = Az2DayPanel.AddCurve("Sunset Az.", listSunsetAz2Day, Color.Blue, ZedGraph.SymbolType.None);
            //
            listEle2Day_ = Ele2DayPanel.AddCurve("Noon sun ele. Angle.", listEle2Day, Color.Red, ZedGraph.SymbolType.Circle);
            // ---------------------   
            // draw graph    
            zGraphAz2EleAng.AxisChange();
            zGraphAz2Day.AxisChange();
            zGraphEleAng2Day.AxisChange();
            //--
            zGraphAz2EleAng.Refresh();
            zGraphAz2Day.Refresh();
            zGraphEleAng2Day.Refresh();
        }

        private void cmdNewTreeShp_Click(object sender, EventArgs e)
        {
            if (pvTmpDir == "")
            {
                FolderBrowserDialog folderSel = new FolderBrowserDialog();
                folderSel.Description = "Please select temporary directory location :";
                folderSel.ShowDialog();
                pvTmpDir = folderSel.SelectedPath;
            }
            if (pvTmpDir != "")
            {
                IFeatureSet pvfs;
                pvfs = new FeatureSet(FeatureType.Point);
                pvfs.Projection = pvMap.Projection;
                pvfs.DataTable.Columns.Add(new DataColumn("diameter", typeof(double)));
                pvfs.DataTable.Columns.Add(new DataColumn("Height", typeof(double)));
                pvfs.DataTable.Columns.Add(new DataColumn("type", typeof(string)));
                pvfs.Name = "Tree";
                pvfs.SaveAs(pvTmpDir + "\\Temp\\Tree.shp", true);
                removeDupplicateLyr(pvfs.Name);
                pvMap.Layers.Add(pvfs);
                loadLayerList();
                cmbTree.Text = pvfs.Name;
                chkTree.Checked = true;
            }
        }

        private void cmdNewBldgShp_Click(object sender, EventArgs e)
        {
            if (pvTmpDir == "")
            {
                FolderBrowserDialog folderSel = new FolderBrowserDialog();
                folderSel.Description = "Please select temporary directory location :";
                folderSel.ShowDialog();
                pvTmpDir = folderSel.SelectedPath;
            }
            if (pvTmpDir != "")
            {
                IFeatureSet pvfs;
                pvfs = new FeatureSet(FeatureType.Polygon);
                pvfs.Projection = pvMap.Projection;
                pvfs.DataTable.Columns.Add(new DataColumn("Height", typeof(double)));
                pvfs.DataTable.Columns.Add(new DataColumn("Remark", typeof(string)));
                pvfs.Name = "Building";
                pvfs.SaveAs(pvTmpDir + "\\Temp\\Building.shp", true);
                removeDupplicateLyr(pvfs.Name);
                pvMap.Layers.Add(pvfs);
                loadLayerList();
                cmbBldg.Text = pvfs.Name;
                chkBuilding.Checked = true;
            }
        }

        private void cmdNewAligmnentShp_Click(object sender, EventArgs e)
        {
            if (pvTmpDir == "")
            {
                FolderBrowserDialog folderSel = new FolderBrowserDialog();
                folderSel.Description = "Please select temporary directory location :";
                folderSel.ShowDialog();
                pvTmpDir = folderSel.SelectedPath;
            }
            if (pvTmpDir != "")
            {
                IFeatureSet pvfs;
                pvfs = new FeatureSet(FeatureType.Line);
                pvfs.Projection = pvMap.Projection;
                pvfs.DataTable.Columns.Add(new DataColumn("spacing", typeof(double)));
                pvfs.DataTable.Columns.Add(new DataColumn("remark", typeof(string)));
                pvfs.Name = "pv. Alignment";
                pvfs.SaveAs(pvTmpDir + "\\Temp\\Alignment.shp", true);
                removeDupplicateLyr(pvfs.Name);
                pvMap.Layers.Add(pvfs);
                loadLayerList();
                cmbAlignmentLyr.Text = pvfs.Name;
            }
        }

        private void cmdPickCentroid_Click(object sender, EventArgs e)
        {
            //removeDupplicateLyr("world_timezones");
            //pvMap.AddLayer(pvDir + "\\Time zone\\world_timezones.shp");
            pickRoseLocation = true;
            pvMap.FunctionMode = FunctionMode.None;
            cmdClearGraphic.Enabled = true;  
        }

        private void cmdExportSketchup_Click(object sender, EventArgs e)
        {
                       
            pvVerify();
            if (verify[0] == false)
            {
                MessageBox.Show("Please assign a reference location before exporting data to SketchUp.");
                return;
            }
            
            FolderBrowserDialog folderSel = new FolderBrowserDialog();
            folderSel.Description = "Select the directory that you want to export Sketchup files:";
            folderSel.ShowDialog();
            if (folderSel.SelectedPath != null)
            {
                int PoleLyr = getLayerHdl(cmbPolePosition.Text);
                if (PoleLyr != -1)
                {
                    //-----------------------------------------------------
                    //IFeatureSet pvfs;
                    //pvfs = new FeatureSet(FeatureType.Polygon);
                    //-----------------------------------------------------
                    IMapFeatureLayer poleFe = pvMap.Layers[PoleLyr] as IMapFeatureLayer;
                    Export2SketchUp4PvPanel(poleFe, folderSel.SelectedPath);
                }
                else
                {
                    MessageBox.Show("Please Select pv. pole layer before");
                }
                //Export2SketchUp4PvPanel
                int treeLyr = getLayerHdl(cmbTree.Text);
                if (treeLyr != -1)
                {
                    IMapFeatureLayer treeFs = pvMap.Layers[treeLyr] as IMapFeatureLayer;
                    Export2SketchUp4Tree(treeFs, folderSel.SelectedPath);
                }
                else
                {
                    MessageBox.Show("Cannot export tree model. Please Select tree layer before");
                }
                //Export2SketchUp4Building
                int bldgLyr = getLayerHdl(cmbBldg.Text);
                if (bldgLyr != -1)
                {
                    IMapFeatureLayer bldgFs = pvMap.Layers[bldgLyr] as IMapFeatureLayer;
                    Export2SketchUp4Bldg(bldgFs, folderSel.SelectedPath);
                }
                else
                {
                    MessageBox.Show("Cannot export building model. Please Select building layer before");
                }
                MessageBox.Show("Google SketchUp file Export completet");
            }
        }

        private void CmdBaseLine_Click(object sender, EventArgs e)
        {
            pickRoseLocation = false;

            if (_baselinePath != null) _baselinePath.Clear();
            pickBLLocation = true;
            grpBLineInfo.Visible = true;

            pvMap.FunctionMode = FunctionMode.None;
            _baselinePath = new List<Coordinate>();
            grdBaseline.Rows.Clear();
            BLineLength = 0;
            BLineLengthTotal = 0;
            lasetAng = 0;
        }

        private void lblOther_Click(object sender, EventArgs e)
        {
            if (lblOther.Tag == "On")
            {
                lblOther.Tag = "Off";
                lblOther.Text = "Other off";
            }
            else
            {
                lblOther.Tag = "On";
                lblOther.Text = "Other on";
            }
        }

        private void cmdDrawPvPanel_Click(object sender, EventArgs e)
        {
            saveAlignmentAttribute();
            double xSpacing = 1;

            int iLyr = getLayerHdl(cmbAlignmentLyr.Text);
            IMapFeatureLayer bLineLyr = pvMap.Layers[iLyr] as IMapFeatureLayer;

            CratePvPole(xSpacing, bLineLyr);
            grpBLineInfo.Visible = false;
            loadLayerList();
            MessageBox.Show("Pv. ploe created complete (Total Panel: " + numPvPanel.ToString() + ")");
            updateArea();
        }

        private void cmdOpenProject_Click(object sender, EventArgs e)
        {
            MessageBox.Show("be incomplete");
        }

        private void cmdSaveProject_Click(object sender, EventArgs e)
        {
            MessageBox.Show("be incomplete");
        }

        private void cmdFeatureSelection_Click(object sender, EventArgs e)
        {
            MessageBox.Show("be incomplete");
        }

        private void cmdShadowAnalysis_Click(object sender, EventArgs e)
        {
             if (pvTmpDir == "")
            {
                FolderBrowserDialog folderSel = new FolderBrowserDialog();
                folderSel.Description = "Please select temporary directory location :";
                folderSel.ShowDialog();
                pvTmpDir = folderSel.SelectedPath;
            }
             if (pvTmpDir != "")
             {
                 if (verify[3] == true)
                 {// int[] dat = new int[mRow, mCol];

                     int year = dateTimePicker1.Value.Year;
                     double Latitude = Convert.ToDouble(this.txtLAT.Text);
                     double Longitude = Convert.ToDouble(this.txtLNG.Text);
                     double UtmN = Convert.ToDouble(this.txtUtmN.Text);
                     double UtmE = Convert.ToDouble(this.txtUtmE.Text);
                     int bldgLyr = getLayerHdl(cmbBldg.Text);
                     if (bldgLyr != -1)
                     {
                         IFeatureSet fs = new FeatureSet(FeatureType.Polygon);
                         //---------------------------------------------------------
                         fs.DataTable.Columns.Add(new DataColumn("Azimuth", typeof(double)));
                         fs.DataTable.Columns.Add(new DataColumn("Ele_Angle", typeof(double)));
                         //---------------------------------------------------------
                         IMapFeatureLayer mp = pvMap.Layers[bldgLyr] as IMapFeatureLayer;
                         //MessageBox.Show("Number of shape = " + mp.DataSet.NumRows());

                         //int nShp = mp.DataSet.NumRows() - 1;
                         IFeatureSet myFe;
                         myFe = new FeatureSet(FeatureType.Polygon);

                         IFeatureSet fea = ((IFeatureLayer)pvMap.GetLayers().ToArray()[bldgLyr]).DataSet;
                         System.Data.DataTable dt = fea.DataTable;

                         for (int ibldg = 0; ibldg < mp.DataSet.NumRows(); ibldg++)
                         {
                             int numBldgPt = mp.DataSet.GetFeature(ibldg).NumPoints;

                             DotSpatial.Topology.Coordinate[] pts = new DotSpatial.Topology.Coordinate[numBldgPt];
                             DotSpatial.Topology.Coordinate[] ptss = new DotSpatial.Topology.Coordinate[numBldgPt * 2];
                             IFeature blgdFs = mp.DataSet.GetFeature(ibldg);
                             double h = Convert.ToDouble(dt.Rows[ibldg]["Height"]);

                             for (int i = 0; i < numBldgPt; i++)
                             {
                                 pts[i] = new DotSpatial.Topology.Coordinate(blgdFs.Coordinates[i].X, blgdFs.Coordinates[i].Y, h);
                                 ptss[i] = new DotSpatial.Topology.Coordinate(blgdFs.Coordinates[i].X, blgdFs.Coordinates[i].Y);
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
                                 for (int day = 1; day <= month_day; day += 16)
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
                                         if (eleAng >= 10) // efficetive elevation angle
                                         {
                                             noonHr++;
                                             //Shadow point
                                             for (int i = 0; i < pts.Length; i++)
                                             {
                                                 PvShadow shadow = new PvShadow(AzAng, eleAng, pts[i]);
                                                 Coordinate tmp = shadow.shadowPt;
                                                 ptss[i + numBldgPt] = new Coordinate(tmp.X, tmp.Y);
                                             }
                                             // CONVEX HULL Algorithm for make a shadow area
                                             var multiPoint = new MultiPoint(ptss);
                                             var convexHull = (Polygon)multiPoint.ConvexHull();
                                             myFe.AddFeature(convexHull);
                                             //Grid
                                             /*     
                                                 for (int iRow = 0; iRow < grd.Bounds.NumRows; iRow++)
                                                 {
                                                     for (int iCol = 0; iCol < grd.Bounds.NumColumns; iCol++)
                                                     {
                                                         Coordinate xy = grd.CellToProj(iRow, iCol);
                                                         if (convexHull.Envelope.X <= xy.X & xy.X <= convexHull.Envelope.X + convexHull.Envelope.Width)
                                                         {
                                                             if (convexHull.Intersects(xy.X, xy.Y) == true)
                                                             {
                                                                 grd.Value[iRow, iCol] += 1;
                                                             }
                                                         }
                                                 
                                                     }
                                                 }
                                             */
                                         }
                                         else // Night hour
                                         {
                                             nightHr++;
                                         }

                                     }
                                 }

                             }//); // Parallel.For
                         }

                         Console.WriteLine(DateTime.Now.ToString());
                         Console.ReadLine();

                         prgBar.Visible = false;
                         IFeatureSet result = myFe.UnionShapes(ShapeRelateType.Intersecting);
                         myFe.Projection = pvMap.Projection;
                         myFe.Name = "shadow map";
                         myFe.SaveAs(pvTmpDir + "\\Temp\\shadow map.shp", true);
                         result.SaveAs(pvTmpDir + "\\Temp\\shadow map union.shp", true);
                         //pvMap.Layers.Add(result);
                         pvMap.Layers.Add(myFe);

                         MessageBox.Show("Okay");
                     }
                     else
                     {
                         MessageBox.Show("Please create a Building layer first.");
                     }
                 }
                 else
                 {
                     MessageBox.Show("Please create Calculate sun path statistic before");
                 }
             }
        }

        private void cmdOpenBLinePath_Click(object sender, EventArgs e)
        {
            MessageBox.Show("be incomplete");
        }

        private void cmdSaveBLinePath_Click(object sender, EventArgs e)
        {
            MessageBox.Show("be incomplete");
        }

        private void cmdCreateShapefile_Click(object sender, EventArgs e)
        {
            MessageBox.Show("be incomplete");
        }

        private void cmdClearDrawing_Click(object sender, EventArgs e)
        {
            //pvMap.MapFrame.DrawingLayers.Clear();
            MessageBox.Show("be incomplete");
        }

        private void lblAzimuthCmd_Click(object sender, EventArgs e)
        {
            selectBLineCmd(lblAzimuthCmd);
        }

        private void lblAngCmd_Click(object sender, EventArgs e)
        {
            selectBLineCmd(lblAngCmd);
        }

        private void lblLengthCmd_Click(object sender, EventArgs e)
        {
            selectBLineCmd(lblLengthCmd);
        }

        private void cmdDrawPvArray_Click(object sender, EventArgs e)
        {
              if (pvTmpDir == "")
            {
                FolderBrowserDialog folderSel = new FolderBrowserDialog();
                folderSel.Description = "Please select temporary directory location :";
                folderSel.ShowDialog();
                pvTmpDir = folderSel.SelectedPath;
            }
              if (pvTmpDir != "")
              {
                  if (verify[3] == false)
                  {
                      MessageBox.Show("Please calculate solar properties first.");
                      return;
                  }
                  if (cmbTrack_mode.SelectedIndex == -1)
                  {
                      MessageBox.Show("Please select tracking mode before calculate.");
                      return;
                  }
                  cmdReport.Enabled = false;
                  pvMap.MapFrame.DrawingLayers.Clear();
                  double w = Convert.ToDouble(txtPvWidth.Text);
                  double h = Convert.ToDouble(txtPvLength.Text);
                  int PoleLyr = getLayerHdl(cmbPolePosition.Text);
                  if (PoleLyr != -1)
                  {
                      //-----------------------------------------------------
                      IFeatureSet pvfs;
                      pvfs = new FeatureSet(FeatureType.Polygon);
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
                          pvfs.Features.Add(pvPanel(w, h, x1, y1, pvTilt.tiltAngle, pvAz.AzimutAngle));
                      }

                      pvfs.Projection = pvMap.Projection;
                      pvfs.Name = "pv Array";
                      pvfs.SaveAs(pvTmpDir + "\\Temp\\pvArray.shp", true);
                      removeDupplicateLyr(pvfs.Name);
                      pvMap.Layers.Add(pvfs);
                      loadLayerList();

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
                      /*
                          pvMap.MapFrame.DrawingLayers.Clear();
                          MapPolygonLayer rangeRingAxis;
                          rangeRingAxis = new MapPolygonLayer(pvfs);
                          rangeRingAxis.Symbolizer = new PolygonSymbolizer(System.Drawing.Color.AliceBlue, System.Drawing.Color.LightBlue);
                          pvMap.MapFrame.DrawingLayers.Add(rangeRingAxis);
                          pvMap.MapFrame.Invalidate();
                       */
                      updateArea();
                  }
                  else
                  {
                      MessageBox.Show("Please assign the pole layer first.");
                  }
              }
        }

        private void cmdSelectBldgLayer_Click(object sender, EventArgs e)
        {
            setCurrrentLayer(cmbBldg.Text);
        }

        private void cmdSelectTreeLayer_Click(object sender, EventArgs e)
        {
            setCurrrentLayer(cmbTree.Text);
        }

        private void cmdSelectAlignmentLayer_Click(object sender, EventArgs e)
        {
            setCurrrentLayer(cmbAlignmentLyr.Text);
        }

        private void cmdSelectPoleLayer_Click(object sender, EventArgs e)
        {
            setCurrrentLayer(cmbPolePosition.Text);
        }

        private void picTree_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (lstTreeImage.Images.Count - 1 > currentImage)
                {
                    currentImage++;
                }
                else
                {
                    currentImage = 0;
                }
            }
            else
            {
                if (currentImage > 0)
                {
                    currentImage--;
                }
                else
                {
                    currentImage = lstTreeImage.Images.Count - 1;
                }
            }

            //Update height ratio

            if (chkAutoHeight.Checked == false)
            {
                try
                {
                    setTreeShape();
                    int TreeTypeIndex = currentImage;
                    txtTreeHeight.Text = (whRatio[TreeTypeIndex] * Convert.ToDouble(txtTreeDiameter.Text) / 2).ToString();
                }
                catch
                {
                    txtTreeHeight.Text = "0";
                }
            }

            picTree.Image = lstTreeImage.Images[currentImage];
            lblTreeTypeIndex.Text = treeTypeName[currentImage];
        }

        private void grdTreeAttribute_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;
            int treeLyr = getLayerHdl(cmbTree.Text);
            if (treeLyr != -1 & iRow != -1)
            {
                //-----------------------------------------------------
                IFeatureSet pvfs;
                pvfs = new FeatureSet(FeatureType.Line);
                //-----------------------------------------------------
                IMapFeatureLayer poleFe = pvMap.Layers[treeLyr] as IMapFeatureLayer;
                int nShp = poleFe.DataSet.NumRows();
                //Extent ext = poleFe.DataSet.GetFeature(nShp).Envelope.ToExtent();
                if (iRow < nShp)
                {
                    IFeature fs = poleFe.DataSet.GetFeature(iRow);
                    object val = fs.DataRow["type"];
                    try
                    {
                        int treeTypeId = getTreeTypeId((string)val);
                        if (treeTypeId != -1)
                        {
                            currentImage = treeTypeId;
                            picTree.Image = lstTreeImage.Images[currentImage];
                        }
                        else
                        { picTree.Image = null; }
                    }
                    catch { }

                    double x1 = fs.Coordinates[0].X;
                    double y1 = fs.Coordinates[0].Y;
                    pvfs.Features.Add(LineFeaH(x1, y1, 10000));
                    pvfs.Features.Add(LineFeaV(x1, y1, 10000));
                    pvfs.Projection = pvMap.Projection;
                    pvMap.MapFrame.DrawingLayers.Clear();
                    MapLineLayer rangeRingAxis;
                    rangeRingAxis = new MapLineLayer(pvfs);
                    rangeRingAxis.Symbolizer = new LineSymbolizer(System.Drawing.Color.AliceBlue, 2);
                    pvMap.MapFrame.DrawingLayers.Add(rangeRingAxis);
                    pvMap.MapFrame.Invalidate();
                }
            }
        }

        private void chkAutoHeight_CheckedChanged(object sender, EventArgs e)
        {
            txtTreeHeight.Enabled = chkAutoHeight.Checked;
        }

        private void txtTreeDiameter_TextChanged(object sender, EventArgs e)
        {

            if (chkAutoHeight.Checked == false)
            {
                try
                {
                    setTreeShape();
                    int TreeTypeIndex = currentImage;
                    txtTreeHeight.Text = (whRatio[TreeTypeIndex] * Convert.ToDouble(txtTreeDiameter.Text) / 2).ToString();
                }
                catch
                {
                    txtTreeHeight.Text = "0";
                }
            }
        }

        private void cmdSaveEditTreeShp_Click(object sender, EventArgs e)
        {
            int treeLyr = getLayerHdl(cmbTree.Text);
            if (treeLyr != -1)
            {
                IMapFeatureLayer poleFe = pvMap.Layers[treeLyr] as IMapFeatureLayer;
                int nShp = poleFe.DataSet.NumRows();

                for (int iRow = 0; iRow < nShp; iRow++)
                {
                    IFeature fs = poleFe.DataSet.GetFeature(iRow);
                    fs.DataRow.BeginEdit();
                    try
                    {
                        fs.DataRow["Type"] = grdTreeAttribute.Rows[iRow].Cells[1].Value;
                        fs.DataRow["Height"] = grdTreeAttribute.Rows[iRow].Cells[2].Value;
                        fs.DataRow["diameter"] = grdTreeAttribute.Rows[iRow].Cells[3].Value;
                        fs.DataRow.EndEdit();
                    }
                    catch { }

                }
            }
            DrawTreeCircle();
        }

        private void cmdDrawTreeMap_Click(object sender, EventArgs e)
        {
            DrawTreeCircle();
        }

        private void cmdSetSelectedTree_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDouble(txtTreeDiameter.Text) > 0.0 & Convert.ToDouble(txtTreeHeight.Text) > 0.0)
                {
                    grdTreeAttribute.Rows[grdTreeAttribute.CurrentRow.Index].Cells[1].Value = treeTypeName[currentImage];
                    grdTreeAttribute.Rows[grdTreeAttribute.CurrentRow.Index].Cells[2].Value = txtTreeHeight.Text;
                    grdTreeAttribute.Rows[grdTreeAttribute.CurrentRow.Index].Cells[3].Value = txtTreeDiameter.Text;
                }
            }
            catch
            {
                MessageBox.Show("Error: Tree diameter and height are empty or incorrect.");
            }

        }

        private void grdTreeAttribute_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmdReloadTreeData_Click(object sender, EventArgs e)
        {
            int treeLyr = getLayerHdl(cmbTree.Text);
            if (treeLyr != -1)
            {
                IMapFeatureLayer treeFe = pvMap.Layers[treeLyr] as IMapFeatureLayer;
                int nShp = treeFe.DataSet.NumRows() - 1;
                grdTreeAttribute.Rows.Clear();
                for (int i = 0; i < treeFe.DataSet.NumRows(); i++)
                {
                    grdTreeAttribute.Rows.Add();
                    IFeature fs = treeFe.DataSet.GetFeature(i);
                    object d = fs.DataRow["diameter"];
                    object typ = fs.DataRow["type"];
                    object h = fs.DataRow["height"];
                    double x1 = fs.Coordinates[0].X;
                    double y1 = fs.Coordinates[0].Y;
                    grdTreeAttribute.Rows[i].Cells[0].Value = i;
                    grdTreeAttribute.Rows[i].Cells[1].Value = typ;
                    grdTreeAttribute.Rows[i].Cells[2].Value = h;
                    grdTreeAttribute.Rows[i].Cells[3].Value = d;
                }
            }
        }

        private void cmdReloadBldgData_Click(object sender, EventArgs e)
        {
            int bldgLyr = getLayerHdl(cmbBldg.Text);
            if (bldgLyr != -1)
            {
                IMapFeatureLayer bldgFe = pvMap.Layers[bldgLyr] as IMapFeatureLayer;
                int nShp = bldgFe.DataSet.NumRows() - 1;
                grdBldg.Rows.Clear();
                for (int i = 0; i < bldgFe.DataSet.NumRows(); i++)
                {
                    grdBldg.Rows.Add();
                    IFeature fs = bldgFe.DataSet.GetFeature(i);
                    object h = fs.DataRow["height"];
                    object remark = fs.DataRow["remark"];
                    grdBldg.Rows[i].Cells[0].Value = i;
                    grdBldg.Rows[i].Cells[1].Value = h;
                    grdBldg.Rows[i].Cells[2].Value = remark;
                }
            }

        }

        private void grdBldg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;

            int bldgLyr = getLayerHdl(cmbBldg.Text);
            if (bldgLyr != -1 & iRow != -1)
            {
                //-----------------------------------------------------
                IFeatureSet bldgfs;
                bldgfs = new FeatureSet(FeatureType.Polygon);
                //-----------------------------------------------------
                IMapFeatureLayer bldgFe = pvMap.Layers[bldgLyr] as IMapFeatureLayer;
                int nShp = bldgFe.DataSet.NumRows();
                //Extent ext = poleFe.DataSet.GetFeature(nShp).Envelope.ToExtent();
                if (iRow < nShp)
                {
                    IFeature fs = bldgFe.DataSet.GetFeature(iRow);
                    Coordinate[] c = new Coordinate[fs.NumPoints];
                    for (int iPt = 0; iPt < fs.NumPoints - 1; iPt++)
                    {
                        double x1 = fs.Coordinates[iPt].X;
                        double y1 = fs.Coordinates[iPt].Y;
                        c[iPt] = new Coordinate(x1, y1);
                    }
                    c[fs.NumPoints - 1] = new Coordinate(c[0].X, c[0].Y);
                    bldgfs.Features.Add(c);
                    bldgfs.Projection = pvMap.Projection;
                    pvMap.MapFrame.DrawingLayers.Clear();
                    MapPolygonLayer rangeRingAxis;
                    rangeRingAxis = new MapPolygonLayer(bldgfs);
                    rangeRingAxis.Symbolizer = new PolygonSymbolizer(Color.AliceBlue, Color.Blue);
                    pvMap.MapFrame.DrawingLayers.Add(rangeRingAxis);
                    pvMap.MapFrame.Invalidate();
                }
            }
        }

        private void cmdSaveBldg_Click(object sender, EventArgs e)
        {
            int BldgLyr = getLayerHdl(cmbBldg.Text);
            if (BldgLyr != -1)
            {
                IMapFeatureLayer blgdFe = pvMap.Layers[BldgLyr] as IMapFeatureLayer;
                int nShp = blgdFe.DataSet.NumRows();

                for (int iRow = 0; iRow < nShp; iRow++)
                {
                    IFeature fs = blgdFe.DataSet.GetFeature(iRow);
                    try
                    {
                        fs.DataRow.BeginEdit();
                        fs.DataRow["Height"] = grdBldg.Rows[iRow].Cells[1].Value;
                        fs.DataRow["Remark"] = grdBldg.Rows[iRow].Cells[2].Value;
                        fs.DataRow.EndEdit();
                    }
                    catch { }

                }
                cmdShadowAnalysis.Enabled = true; 
            }
        }

        private void cmdTreeEraser_Click(object sender, EventArgs e)
        {
            pvMap.MapFrame.DrawingLayers.Clear();
            pvMap.MapFrame.Invalidate();
        }

        private void cmdBldgEraser_Click(object sender, EventArgs e)
        {
            pvMap.MapFrame.DrawingLayers.Clear();
            pvMap.MapFrame.Invalidate();
        }

        private void cmdReloadAlignmentData_Click(object sender, EventArgs e)
        {
            int alingmentLyr = getLayerHdl(cmbAlignmentLyr.Text);
            if (alingmentLyr != -1)
            {
                IMapFeatureLayer alingmentFe = pvMap.Layers[alingmentLyr] as IMapFeatureLayer;
                int nShp = alingmentFe.DataSet.NumRows() - 1;
                grdAlignment.Rows.Clear();
                for (int i = 0; i < alingmentFe.DataSet.NumRows(); i++)
                {
                    grdAlignment.Rows.Add();
                    IFeature fs = alingmentFe.DataSet.GetFeature(i);
                    object sp = fs.DataRow["spacing"];
                    object remark = fs.DataRow["remark"];
                    grdAlignment.Rows[i].Cells[0].Value = i;
                    grdAlignment.Rows[i].Cells[1].Value = sp;
                    grdAlignment.Rows[i].Cells[2].Value = remark;
                }
            }

        }

        private void chkSystemSpacing_CheckedChanged(object sender, EventArgs e)
        {
            txtDx.Visible = chkSystemSpacing.Checked;
            lblSpacingUnit.Visible = chkSystemSpacing.Checked;
        }

        private void cmdSaveAlignmentData_Click(object sender, EventArgs e)
        {
            saveAlignmentAttribute();
        }

        void saveAlignmentAttribute()
        {
            int AlignmentLyr = getLayerHdl(cmbAlignmentLyr.Text);
            if (AlignmentLyr != -1)
            {
                IMapFeatureLayer alignmentFe = pvMap.Layers[AlignmentLyr] as IMapFeatureLayer;
                int nShp = alignmentFe.DataSet.NumRows();

                for (int iRow = 0; iRow < nShp; iRow++)
                {
                    IFeature fs = alignmentFe.DataSet.GetFeature(iRow);
                    try
                    {
                        fs.DataRow.BeginEdit();
                        fs.DataRow["spacing"] = grdAlignment.Rows[iRow].Cells[1].Value;
                        fs.DataRow["Remark"] = grdAlignment.Rows[iRow].Cells[2].Value;
                        fs.DataRow.EndEdit();
                    }
                    catch { }

                }
            }
        }
        #region "-- Expand"

        void pvVerify()
        {
            //Reference location
            if (txtLAT.Text != "" & txtLNG.Text != "") verify[0] = true; else verify[0] = false;
            //Building Layer
            if (chkBuilding.Checked == true)
            {
                if (getLayerHdl(cmbBldg.Text) != -1) verify[1] = true; else verify[1] = false;
            }
            else // doesn't use building layer
            {
                verify[1] = true;
            }
            //Tree Layer
            if (chkTree.Checked == true)
            {
                if (getLayerHdl(cmbTree.Text) != -1) verify[2] = true; else verify[2] = false;
            }
            else // doesn't use tree layer
            {
                verify[2] = true;
            }
            //Alignment Layer
            //    if (getLayerHdl(cmbAlignmentLyr.Text) != -1) verify[4] = true; else verify[4] = false;
        }
        string verificationMessage()
        {

            return "okay";
        }
        private void lblTab1_Click(object sender, EventArgs e)
        {
            updateTab(lblTab1, panelTab1, tab1Height, picButtTab1);
            loadLayerList();
            //Todo: add refresh layer
        }

        private void lblTab2_Click(object sender, EventArgs e)
        {
            pvVerify();
            if (verify[0] == true)
            {
                updateTab(lblTab2, panelTab2, tab2Height, picButtTab2);
            }
            else
            {
                MessageBox.Show("Please assign the reference location first.");
            }
        }

        private void lblTab3_Click(object sender, EventArgs e)
        {
            updateTab(lblTab3, panelTab3, tab3Height, picButtTab3);

            //Load tree attribute
            int treeLyr = getLayerHdl(cmbTree.Text);
            if (treeLyr != -1)
            {
                IMapFeatureLayer treeFe = pvMap.Layers[treeLyr] as IMapFeatureLayer;
                //grdTreeAttribute.DataSource = treeFe.DataSet; 
                int nShp = treeFe.DataSet.NumRows() - 1;
                grdTreeAttribute.Rows.Clear();

                //Extent ext = treeFe.DataSet.GetFeature(nShp).Envelope.ToExtent();
                for (int i = 0; i < treeFe.DataSet.NumRows(); i++)
                {
                    grdTreeAttribute.Rows.Add();
                    IFeature fs = treeFe.DataSet.GetFeature(i);
                    object d = fs.DataRow["diameter"];
                    object typ = fs.DataRow["type"];
                    object h = fs.DataRow["height"];
                    double x1 = fs.Coordinates[0].X;
                    double y1 = fs.Coordinates[0].Y;
                    grdTreeAttribute.Rows[i].Cells[0].Value = i;
                    grdTreeAttribute.Rows[i].Cells[1].Value = typ;
                    grdTreeAttribute.Rows[i].Cells[2].Value = h;
                    grdTreeAttribute.Rows[i].Cells[3].Value = d;
                }
            }
            //Load Buildind attribute
            int bldgLyr = getLayerHdl(cmbBldg.Text);
            if (bldgLyr != -1)
            {
                IMapFeatureLayer bldgFe = pvMap.Layers[bldgLyr] as IMapFeatureLayer;
                int nShp = bldgFe.DataSet.NumRows() - 1;
                grdBldg.Rows.Clear();
                for (int i = 0; i < bldgFe.DataSet.NumRows(); i++)
                {
                    grdBldg.Rows.Add();
                    IFeature fs = bldgFe.DataSet.GetFeature(i);
                    object h = fs.DataRow["height"];
                    object remark = fs.DataRow["remark"];
                    grdBldg.Rows[i].Cells[0].Value = i;
                    grdBldg.Rows[i].Cells[1].Value = h;
                    grdBldg.Rows[i].Cells[2].Value = remark;
                }
            }

        }

        private void lblTab4_Click(object sender, EventArgs e)
        {
            pvVerify();
            if (verify[4] == true)
            {
                updateTab(lblTab4, panelTab4, tab4Height, picButtTab4);
                loadLayerList();
                if (panelTab4.Height > 15) lblOther.Visible = true; else lblOther.Visible = false;
                //
                int alingmentLyr = getLayerHdl(cmbAlignmentLyr.Text);
                if (alingmentLyr != -1)
                {
                    IMapFeatureLayer alingmentFe = pvMap.Layers[alingmentLyr] as IMapFeatureLayer;
                    int nShp = alingmentFe.DataSet.NumRows() - 1;
                    grdBldg.Rows.Clear();
                    for (int i = 0; i < alingmentFe.DataSet.NumRows(); i++)
                    {
                        grdAlignment.Rows.Add();
                        IFeature fs = alingmentFe.DataSet.GetFeature(i);
                        object sp = fs.DataRow["spacing"];
                        object remark = fs.DataRow["remark"];
                        grdAlignment.Rows[i].Cells[0].Value = i;
                        grdAlignment.Rows[i].Cells[1].Value = sp;
                        grdAlignment.Rows[i].Cells[2].Value = remark;
                    }
                }

            }
            else
            {
                MessageBox.Show("Error: Alignment layer not found or field structure incorrect.");
            }
        }

        private void lblTab5_Click(object sender, EventArgs e)
        {
            if (verify[5] == true)
            {
                updateTab(lblTab5, panelTab5, tab5Height, picButtTab5);
                loadLayerList();
            }
            else
            {
                MessageBox.Show("Plese enable the pole data layer first.");
            }
        }

        private void hindTab_Click(object sender, EventArgs e)
        {
            lblTab1.Tag = "-";
            lblTab2.Tag = "-";
            lblTab4.Tag = "-";
            lblTab5.Tag = "-";
            updateTab(lblTab1, panelTab1, tab1Height, picButtTab1);
            updateTab(lblTab2, panelTab2, tab2Height, picButtTab2);
            updateTab(lblTab3, panelTab3, tab3Height, picButtTab3);
            updateTab(lblTab4, panelTab4, tab4Height, picButtTab4);
            updateTab(lblTab5, panelTab5, tab5Height, picButtTab5);
            updatePanelTab();
            if (lblTab4.Height > 15) lblOther.Visible = true; else lblOther.Visible = false;
        }

        private void expandTab_Click(object sender, EventArgs e)
        {
            lblTab1.Tag = "+";
            lblTab2.Tag = "+";
            lblTab4.Tag = "+";
            lblTab5.Tag = "+";
            updateTab(lblTab1, panelTab1, tab1Height, picButtTab1);
            updateTab(lblTab2, panelTab2, tab2Height, picButtTab2);
            updateTab(lblTab3, panelTab3, tab3Height, picButtTab3);
            updateTab(lblTab4, panelTab4, tab4Height, picButtTab4);
            updateTab(lblTab5, panelTab5, tab5Height, picButtTab5);
            updatePanelTab();
            panelTabMain.AutoScroll = true;
            loadLayerList();
            if (lblTab4.Height > 15) lblOther.Visible = true; else lblOther.Visible = false;
        }

        #endregion

        #endregion

        #region "- UTILITIES"

        void selectBLineCmd(System.Windows.Forms.Label lblCmd)
        {
            if (lblCmd.Tag == "Select")
            {
                ClearBLineCmd();
            }
            else
            {
                ClearBLineCmd();
                lblCmd.Tag = "Select";
                txtBLineDumy.Top = lblCmd.Top;
                txtBLineDumy.Visible = true;
                txtBLineDumy.Text = "0.00";
                lblCmd.ForeColor = Color.Green;
            }
        }

        void ClearBLineCmd()
        {
            lblAngCmd.Tag = "None";
            lblAzimuthCmd.Tag = "None";
            lblLengthCmd.Tag = "None";
            lblAngCmd.ForeColor = Color.Black;
            lblAzimuthCmd.ForeColor = Color.Black;
            lblLengthCmd.ForeColor = Color.Black;
            txtBLineDumy.Visible = false;
        }

        private void DrawTreeCircle()
        {
            int treeLyr = getLayerHdl(cmbTree.Text);
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

        private void kDrawTreeCircle(Double x0, Double y0, Double r, Int16 numVertex, IMap MapCanvas, Color color)
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
            sp.Opacity = 0.25f;
            circleShp.Symbolizer.Patterns.Clear();
            circleShp.Symbolizer.Patterns.Add(sp);
            //circleShp.Symbolizer.SetOutline(color, 2);
            MapCanvas.MapFrame.DrawingLayers.Add(circleShp);

            // Request a redraw
            MapCanvas.MapFrame.Invalidate();
        }

        private void kDrawCircle(Double x0, Double y0, Double r, Int16 numVertex, IMap MapCanvas, Color color)
        {
            Double dAng = 360 / numVertex;
            Coordinate[] cr = new Coordinate[numVertex + 1]; //x-axis
            for (int iAng = 0; iAng <= numVertex; iAng++)
            {
                Double ang1 = iAng * dAng;
                Double x1 = Math.Sin(ang1 * Math.PI / 180) * r + x0;
                Double y1 = Math.Cos(ang1 * Math.PI / 180) * r + y0;
                cr[iAng] = new Coordinate(x1, y1);
            }
            LineString ls = new LineString(cr);
            Feature f = new Feature(ls);
            FeatureSet fs = new FeatureSet(f.FeatureType);
            fs.Features.Add(f);

            MapLineLayer circleShp;
            circleShp = new MapLineLayer(fs);
            circleShp.Symbolizer = new LineSymbolizer(color, 1);

            MapCanvas.MapFrame.DrawingLayers.Add(circleShp);

            // Request a redraw
            MapCanvas.MapFrame.Invalidate();
        }

        private IFeature LineFeaH(Double x, Double y, double size)
        {
            Coordinate[] L1 = new Coordinate[2]; //x-axis
            L1[0] = new Coordinate(x - size, y);
            L1[1] = new Coordinate(x + size, y);
            LineString ls1 = new LineString(L1);
            IFeature f1 = new Feature(ls1);
            return f1;
        }

        private IFeature LineFeaV(Double x, Double y, double size)
        {
            Coordinate[] L1 = new Coordinate[2]; //y-axis
            L1[0] = new Coordinate(x, y - size);
            L1[1] = new Coordinate(x, y + size);
            LineString ls1 = new LineString(L1);
            IFeature f1 = new Feature(ls1);
            return f1;
        }

        private void DrawLineCross(Double x, Double y, double size, Double width, System.Drawing.Color col, IMap MapCanvas)
        {
            Coordinate[] L1 = new Coordinate[2]; //x-axis
            L1[0] = new Coordinate(x - size, y);
            L1[1] = new Coordinate(x + size, y);
            Coordinate[] L2 = new Coordinate[2]; //x-axis
            L2[0] = new Coordinate(x, y - size);
            L2[1] = new Coordinate(x, y + size);

            LineString ls1 = new LineString(L1);
            LineString ls2 = new LineString(L2);
            Feature f1 = new Feature(ls1);
            Feature f2 = new Feature(ls2);
            FeatureSet fs = new FeatureSet(FeatureType.Line);

            fs.Features.Add(f1);
            fs.Features.Add(f2);

            MapLineLayer rangeRingAxis;
            rangeRingAxis = new MapLineLayer(fs);
            rangeRingAxis.Symbolizer = new LineSymbolizer(col, width);

            MapCanvas.MapFrame.DrawingLayers.Add(rangeRingAxis);

            // Request a redraw
            MapCanvas.MapFrame.Invalidate();
        }

        private void DrawLine(Double x1, Double y1, Double x2, Double y2, Double width, System.Drawing.Color col)
        {
            Coordinate[] L = new Coordinate[2]; //x-axis

            L[0] = new Coordinate(x1, y1);
            L[1] = new Coordinate(x2, y2);

            LineString ls = new LineString(L);
            //creates a feature from the linestring
            Feature f = new Feature(ls);
            //  Feature f = new Feature(axisLines);
            FeatureSet fs = new FeatureSet(f.FeatureType);
            fs.Features.Add(f);

            MapLineLayer rangeRingAxis;
            rangeRingAxis = new MapLineLayer(fs);// MapPolygonLayer(fs);
            rangeRingAxis.Symbolizer = new LineSymbolizer(col, width);
            pvMap.MapFrame.DrawingLayers.Add(rangeRingAxis);


            //MapCanvas.MapFrame.DrawingLayers.Add(rangeRingAxis);

            // Request a redraw
            pvMap.MapFrame.Invalidate();
            // MapCanvas.MapFrame.Invalidate();
        }

        private void DrawLine(Double x1, Double y1, Double x2, Double y2, Double width, System.Drawing.Color col, IMap MapCanvas)
        {
            Coordinate[] L = new Coordinate[2]; //x-axis

            L[0] = new Coordinate(x1, y1);
            L[1] = new Coordinate(x2, y2);

            LineString ls = new LineString(L);
            //creates a feature from the linestring
            Feature f = new Feature(ls);
            //  Feature f = new Feature(axisLines);
            FeatureSet fs = new FeatureSet(f.FeatureType);
            fs.Features.Add(f);

            MapLineLayer rangeRingAxis;
            rangeRingAxis = new MapLineLayer(fs);// MapPolygonLayer(fs);
            rangeRingAxis.Symbolizer = new LineSymbolizer(col, width);

            MapCanvas.MapFrame.DrawingLayers.Add(rangeRingAxis);

            // Request a redraw
            MapCanvas.MapFrame.Invalidate();
        }

        int getLayerHdl(String LyrName)
        {
            for (int i = 0; i < pvMap.Layers.Count; i++)
            {
                if (pvMap.Layers[i].LegendText != null)
                {
                    if (pvMap.Layers[i].LegendText.ToLower() == LyrName.ToLower())
                    { return i; }
                }
            }
            return -1;
        }

        void removeDupplicateLyr(String LyrName)
        {
            for (int i = pvMap.Layers.Count - 1; i >= 0; i--)
            {
                int remLyr = getLayerHdl(LyrName);
                if (remLyr != -1)
                {
                    pvMap.Layers.RemoveAt(remLyr);
                }
            }
        }

        public double CrossProductLength(double Ax, double Ay, double Bx, double By, double Cx, double Cy)
        {
            double BAx = Ax - Bx;
            double BAy = Ay - By;
            double BCx = Cx - Bx;
            double BCy = Cy - By;
            return BAx * BCy - BAy * BCx;
        }

        private double DotProduct(double Ax, double Ay, double Bx, double By, double Cx, double Cy)
        {
            double BAx = Ax - Bx;
            double BAy = Ay - By;
            double BCx = Cx - Bx;
            double BCy = Cy - By;
            return BAx * BCx + BAy * BCy;
        }

        public double GetAngle(double Ax, double Ay, double Bx, double By, double Cx, double Cy)
        {
            double dot_product;
            double cross_product;
            dot_product = DotProduct(Ax, Ay, Bx, By, Cx, Cy);
            cross_product = CrossProductLength(Ax, Ay, Bx, By, Cx, Cy);
            return Math.Atan2(cross_product, dot_product);
        }

        //Return True if the point is in the polygon.
        public bool PointInPolygon(DotSpatial.Topology.Point[] points, double X, double Y)
        {
            int max_point = points.Length - 1;
            double total_angle = GetAngle(points[max_point].X, points[max_point].Y, X, Y, points[0].X, points[0].Y);
            for (int i = 0; i < max_point - 1; i++)
            {
                total_angle += GetAngle(points[i].X, points[i].Y, X, Y, points[i + 1].X, points[i + 1].Y);
            }
            return Math.Abs(total_angle) > 0.000001;
        }

        void updatePanelTab()
        {
            panelTab2.Top = panelTab1.Top + panelTab1.Height;
            panelTab3.Top = panelTab2.Top + panelTab2.Height;
            panelTab4.Top = panelTab3.Top + panelTab3.Height;
            panelTab5.Top = panelTab4.Top + panelTab4.Height;
        }

        void updateTab(System.Windows.Forms.Label lblTab, System.Windows.Forms.Panel panelTab, int TabHeight, PictureBox butt)
        {
            if (lblTab.Tag == "+")
            {
                panelTab.Height = TabHeight;
                lblTab.Tag = "-";
                butt.Image = picMinus.Image;
            }
            else
            {
                panelTab.Height = lblTab.Height;
                lblTab.Tag = "+";
                butt.Image = picPlus.Image;
            }
            updatePanelTab();
        }

        bool verify4Tab2()
        {
            //Bldg Check
            if (getLayerHdl(cmbBldg.Text) == -1)
            { }

            return false;
        }

        public Color RandomColor()
        {
            Random r = new Random();
            b = r.Next(1, 5);
            switch (b)
            {
                case 1:
                    {
                        color = Color.Red;
                    }
                    break;
                case 2:
                    {
                        color = Color.Blue;
                    }
                    break;
                case 3:
                    {
                        color = Color.Green;
                    }
                    break;
                case 4:
                    {
                        color = Color.Yellow;
                    }
                    break;
            }

            return color;
        }

        private void loadLayerList()
        {
            //MessageBox.Show("No. of layer" + pvMap.Layers.Count);
            cmbBldg.Items.Clear();
            cmbTree.Items.Clear();
            cmbAlignmentLyr.Items.Clear();
            cmbPolePosition.Items.Clear();
            cmbPanel.Items.Clear();
            cmbDem.Items.Clear();
            cmbSolarFarmArea.Items.Clear();
            for (int i = 0; i < pvMap.Layers.Count; i++)
            {
                if (pvMap.Layers[i].LegendText != null)
                {
                    cmbBldg.Items.Add(pvMap.Layers[i].LegendText);
                    cmbTree.Items.Add(pvMap.Layers[i].LegendText);
                    cmbAlignmentLyr.Items.Add(pvMap.Layers[i].LegendText);
                    cmbPolePosition.Items.Add(pvMap.Layers[i].LegendText);
                    cmbPanel.Items.Add(pvMap.Layers[i].LegendText);
                    cmbDem.Items.Add(pvMap.Layers[i].LegendText);
                    cmbSolarFarmArea.Items.Add(pvMap.Layers[i].LegendText);
                }

            }
            cmbState.Items.Clear();
            for (int i = 0; i < listState.Count; i++) // Loop through List with for
            {
                cmbState.Items.Add(listState[i]);
            }
        }

        IPolygon pvPanel(double w, double x0, double y0, kGeoFunc.LineType BL)
        {
            BL.Pt2.X = (BL.Pt2.X - BL.Pt1.X);
            BL.Pt2.Y = (BL.Pt2.Y - BL.Pt1.Y);
            BL.Pt1.X = 0;
            BL.Pt1.Y = 0;
            //kGeoFunc.CircleType CL1 = new kGeoFunc.CircleType();
            //kGeoFunc.CircleType CL2 = new kGeoFunc.CircleType();
            kGeoFunc.LineType L1 = kGeoFunc.Perpend2Point(BL, BL.Pt1.X, BL.Pt1.Y, w - 2.5);
            kGeoFunc.LineType L2 = kGeoFunc.Perpend2Point(BL, BL.Pt2.X, BL.Pt2.Y, w - 2.5);
            //int n = kGeoFunc.Line_Circle(L1, CL1, ref x1, ref y1, ref x2, ref y2);
            //int m = kGeoFunc.Line_Circle(L2, CL2, ref x1, ref y1, ref x2, ref y2);
            Coordinate[] Lshape = new Coordinate[5]; //x-axis

            Lshape[0] = new Coordinate(BL.Pt1.X + x0, BL.Pt1.Y + y0);
            Lshape[1] = new Coordinate(L1.Pt1.X + x0, L1.Pt1.Y + y0);
            Lshape[2] = new Coordinate(L2.Pt1.X + x0, L2.Pt1.Y + y0);
            Lshape[3] = new Coordinate(BL.Pt2.X + x0, BL.Pt2.Y + y0);
            Lshape[4] = new Coordinate(BL.Pt1.X + x0, BL.Pt1.Y + y0);

            // Polygon Featture
            //creates a polygon feature from the list of coordinate
            IPolygon fe = new Polygon(Lshape);

            // Line Feature
            //LineString ls = new LineString(Lshape); //  for line shape
            //creates a feature from the linestring
            // fe = new Polygon(ls); // for line shape

            return fe;
        }

        IPolygon pvPanel(double w, double h, double x0, double y0, double tilt, double az)
        {
            Coordinate[] pvShape = new Coordinate[4]; //x-axis
            Coordinate[] pvShapeR = new Coordinate[4]; //x-axis
            double hr = h * Math.Cos(tilt * Math.PI / 180);
            pvShape[0] = new Coordinate(-w / 2, +hr / 2);
            pvShape[1] = new Coordinate(-w / 2, -hr / 2);
            pvShape[2] = new Coordinate(+w / 2, -hr / 2);
            pvShape[3] = new Coordinate(+w / 2, +hr / 2);
            for (int i = 0; i < 4; i++)
            {
                double x = kGeoFunc.Rx(pvShape[i].X, pvShape[i].Y, az);
                double y = kGeoFunc.Ry(pvShape[i].X, pvShape[i].Y, az);
                pvShapeR[i] = new Coordinate(x0 + x, y0 + y);
            }
            IPolygon fe = new Polygon(pvShapeR);
            return fe;
        }

        List<Coordinate> PolePosition(IFeature fe,double spacing)
        { 
            List<Coordinate> poles = new List<Coordinate>();
            poles.Add(fe.Coordinates[0]);
            double L = 0;
            if (fe.NumPoints == 2)
            {
                L += Math.Sqrt(Math.Pow(fe.Coordinates[0].X - fe.Coordinates[1].X, 2) + Math.Pow(fe.Coordinates[0].Y - fe.Coordinates[1].Y, 2));
            }
            else
            { 
                for (int i = 0; i < fe.NumPoints - 2; i++)
                {
                    L += Math.Sqrt(Math.Pow(fe.Coordinates[i].X - fe.Coordinates[i + 1].X, 2) + Math.Pow(fe.Coordinates[i].Y - fe.Coordinates[i + 1].Y,2));
                }
            }
            double lastdL = 0;
            for (double dL = spacing; dL <= L; dL += spacing)
            {
                double lSegmemt = 0;
                double lastlSegmemt = 0;
                for (int i = 0; i <= fe.NumPoints - 2; i++)
                {
                    lastlSegmemt = lSegmemt;
                    lSegmemt += Math.Sqrt(Math.Pow(fe.Coordinates[i].X - fe.Coordinates[i + 1].X, 2) + Math.Pow(fe.Coordinates[i].Y - fe.Coordinates[i + 1].Y, 2));
                    if (lastdL<dL & dL<=lSegmemt)
                    {
                        if (lSegmemt == dL)
                        {
                            poles.Add(fe.Coordinates[i+1]);
                            break;
                        }
                        else
                        {
                            double Segmemt = Math.Sqrt(Math.Pow(fe.Coordinates[i].X - fe.Coordinates[i + 1].X, 2) + Math.Pow(fe.Coordinates[i].Y - fe.Coordinates[i + 1].Y, 2));
                            double ddL = dL - lastlSegmemt;
                            double ratio = ddL / Segmemt;
                            double x = (fe.Coordinates[i + 1].X - fe.Coordinates[i].X) * ratio + fe.Coordinates[i].X;
                            double y = (fe.Coordinates[i + 1].Y - fe.Coordinates[i].Y) * ratio + fe.Coordinates[i].Y;
                            Coordinate c = new Coordinate(x, y);
                            poles.Add(c);
                            break;
                        }
                    }
                    lastdL = dL;
                }
            }
            return poles;
        }

        //--------------------------------------------------------------------------------------
        // New funtion 2014/01/15
        //
        //--------------------------------------------------------------------------------------
        void CratePvPole(double xSpacing, IMapFeatureLayer BLineFeLyr)
        {
            if (pvTmpDir == "")
            {
                FolderBrowserDialog folderSel = new FolderBrowserDialog();
                folderSel.Description = "Please select temporary directory location :";
                folderSel.ShowDialog();
                pvTmpDir = folderSel.SelectedPath;
            }

            if (pvTmpDir != "")
            {
                numPvPanel = 0;
                IMapRasterLayer demLyr;
                Raster dem4Pv = new Raster();
                double poleH = 1; //Default pole height = 1 m.
                double z0 = 0;
                try { poleH = Convert.ToDouble(txtPoleHeight.Text); }
                catch
                {
                    MessageBox.Show("Pole height value error");
                    txtPoleHeight.Text = "1";
                    poleH = 1;
                }

                int iDemLyr = getLayerHdl(cmbDem.Text);
                if (iDemLyr != -1 & chkDEM.Checked == true)
                {
                    demLyr = pvMap.Layers[iDemLyr] as IMapRasterLayer;

                    if (demLyr == null)
                    {
                        MessageBox.Show("Error: DEM Data is not correct");
                        return;
                    }

                    int mRow = demLyr.Bounds.NumRows;
                    int mCol = demLyr.Bounds.NumColumns;
                    dem4Pv = (Raster)demLyr.DataSet;
                    Coordinate ptReference = new Coordinate(Convert.ToDouble(txtUtmE.Text), Convert.ToDouble(txtUtmN.Text));
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
                //------------------------------------------------------------------------------------------------
                // Create pole posion from baseline shapefile
                //------------------------------------------------------------------------------------------------

                int nShp = BLineFeLyr.DataSet.NumRows(); // get Number of Feature
                double dx = xSpacing; //m.

                IFeatureSet fs;
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

                for (int i = 0; i < nShp; i++) //Line shape
                {
                    IFeature BLineFe = BLineFeLyr.DataSet.GetFeature(i);
                    List<Coordinate> poles = new List<Coordinate>(); 
                    if (chkSystemSpacing.Checked == true)
                    { dx = Convert.ToDouble(txtDx.Text); }
                    else
                    {
                        try
                        {
                            object val = BLineFe.DataRow["spacing"];
                            dx = (double)val;
                            if (dx <= 0)
                            {
                                MessageBox.Show("Error: Spacing data incorrect.");
                                return;
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Error: Spacing data not found.");
                            verify[5] = false;
                            return;
                        }
                    }
                    xSpacing = dx;
                    poles = PolePosition(BLineFe, xSpacing);
                    for (int n = 0; n < poles.Count; n++) //Line segment
                    {
                        double poleHeight = Convert.ToDouble(txtPoleHeight.Text);
                        Coordinate pt = poles[n];
                        Coordinate poleLocation = new Coordinate(pt.X, pt.Y, poleHeight);
                        IPoint poleFe = new DotSpatial.Topology.Point(poleLocation);
                        IFeature ifea = fs.AddFeature(poleFe);
                        numPvPanel++;

                        //------------------------------------------------------
                        ifea.DataRow.BeginEdit();
                        ifea.DataRow["x"] = pt.X;
                        ifea.DataRow["y"] = pt.Y;
                        ifea.DataRow["w"] = Convert.ToDouble(txtPvWidth.Text);
                        ifea.DataRow["h"] = Convert.ToDouble(txtPvLength.Text);
                        ifea.DataRow["Azimuth"] = pvAz.AzimutAngle;
                        ifea.DataRow["Ele_Angle"] = pvTilt.tiltAngle;
                        if (iDemLyr != -1 & chkDEM.Checked == true)
                        {
                            Coordinate ptReference = new Coordinate(pt.X, pt.Y);
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
                            if (radioAboveAssumeDatum.Checked == true)
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
                    } // next segment
                } // next shape
                fs.Projection = pvMap.Projection;
                fs.Name = "pv. Array pole";
                fs.SaveAs(pvTmpDir + "\\Temp\\pvArrayPole.shp", true);
                removeDupplicateLyr(fs.Name);
                pvMap.Layers.Add(fs);
                loadLayerList();
                //pvMap.MapFrame.DrawingLayers.Clear();
                cmbPolePosition.Text = fs.Name;
                /*
                MapPointLayer rangeRingAxis;
                rangeRingAxis = new MapPointLayer(fs);
                pvMap.MapFrame.DrawingLayers.Add(rangeRingAxis);
                pvMap.MapFrame.Invalidate();
                 */
                verify[5] = true;
                updateArea();
            }
        }

/*        
        void CratePvPole(double xSpacing, IMapFeatureLayer BLineFeLyr)
        {
             if (pvTmpDir == "")
            {
                FolderBrowserDialog folderSel = new FolderBrowserDialog();
                folderSel.Description = "Please select temporary directory location :";
                folderSel.ShowDialog();
                pvTmpDir = folderSel.SelectedPath;
            }
             if (pvTmpDir != "")
             {
                 numPvPanel = 0;
                 IMapRasterLayer demLyr;
                 Raster dem4Pv = new Raster();
                 double poleH = 1; //Default pole height = 1 m.
                 double z0 = 0;
                 try { poleH = Convert.ToDouble(txtPoleHeight.Text); }
                 catch
                 {
                     MessageBox.Show("Pole height value error");
                     txtPoleHeight.Text = "1";
                     poleH = 1;
                 }

                 int iDemLyr = getLayerHdl(cmbDem.Text);
                 if (iDemLyr != -1 & chkDEM.Checked == true)
                 {
                     demLyr = pvMap.Layers[iDemLyr] as IMapRasterLayer;

                     if (demLyr == null)
                     {
                         MessageBox.Show("Error: DEM Data is not correct");
                         return;
                     }

                     int mRow = demLyr.Bounds.NumRows;
                     int mCol = demLyr.Bounds.NumColumns;
                     dem4Pv = (Raster)demLyr.DataSet;
                     Coordinate ptReference = new Coordinate(Convert.ToDouble(txtUtmE.Text), Convert.ToDouble(txtUtmN.Text));
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
                 //------------------------------------------------------------------------------------------------
                 // Create pole posion from baseline shapefile
                 //------------------------------------------------------------------------------------------------

                 int nShp = BLineFeLyr.DataSet.NumRows(); // get Number of Feature
                 kGeoFunc.LineType BL = new kGeoFunc.LineType();
                 double dx = xSpacing; //m.

                 IFeatureSet fs;
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

                 for (int i = 0; i < nShp; i++) //Line shape
                 {
                     double lastL = 0;
                     double residualL = 0;
                     double L = 0;
                     IFeature BLineFe = BLineFeLyr.DataSet.GetFeature(i);
                     if (chkSystemSpacing.Checked == true)
                     { dx = Convert.ToDouble(txtDx.Text); }
                     else
                     {
                         try
                         {
                             object val = BLineFe.DataRow["spacing"];
                             dx = (double)val;
                             if (dx <= 0)
                             {
                                 MessageBox.Show("Error: Spacing data incorrect.");
                                 return;
                             }
                         }
                         catch
                         {
                             MessageBox.Show("Error: Spacing data not found.");
                             verify[5] = false;
                             return;
                         }
                     }
                     xSpacing = dx;
                     residualL = 0;
                     double sumSegment = 0;
                     for (int n = 0; n < BLineFe.NumPoints - 1; n++) //Line segment
                     {
                         BL.Pt1.X = BLineFe.Coordinates[n].X;
                         BL.Pt1.Y = BLineFe.Coordinates[n].Y;
                         BL.Pt2.X = BLineFe.Coordinates[n + 1].X;
                         BL.Pt2.Y = BLineFe.Coordinates[n + 1].Y;
                         //Coordinate LineStart = new Coordinate(BLineFe.Coordinates[n]);
                         //Coordinate LineEnd = new Coordinate(BLineFe.Coordinates[n+1]);
                         L = kGeoFunc.Length(BL);
                         residualL = 0;
                         if (sumSegment > 0)
                         {
                             residualL = xSpacing - sumSegment;
                         }
                         sumSegment += L;
                         double LL;
                         if (xSpacing <= sumSegment | n == 0) // make sure the active segment is longer than the residual spcaing
                         {
                             sumSegment = 0;
                             for (LL = residualL; LL <= L; LL += dx)
                             {
                                 sumSegment = L - LL;

                                 //kGeoFunc.POINTAPI pt = kGeoFunc.PointInLine(BL, LL);
                                 Coordinate pt = new Coordinate();
                                 Coordinate LineEnd = new Coordinate(BLineFe.Coordinates[n + 1]);
                                 int numIntesec = CircleLineIntersect(BLineFe.Coordinates[n], LL, BLineFe.Coordinates[n], BLineFe.Coordinates[n + 1], ref pt);
                                 if (LL == 0)
                                 {
                                     numIntesec = 1;
                                     pt = BLineFe.Coordinates[n];
                                 }
                                 if (numIntesec != -1)
                                 {
                                     double poleHeight = Convert.ToDouble(txtPoleHeight.Text);

                                     Coordinate poleLocation = new Coordinate(pt.X, pt.Y, poleHeight);
                                     IPoint poleFe = new DotSpatial.Topology.Point(poleLocation);
                                     IFeature ifea = fs.AddFeature(poleFe);
                                     numPvPanel++;

                                     //------------------------------------------------------
                                     ifea.DataRow.BeginEdit();
                                     ifea.DataRow["x"] = pt.X;
                                     ifea.DataRow["y"] = pt.Y;
                                     ifea.DataRow["w"] = Convert.ToDouble(txtPvWidth.Text);
                                     ifea.DataRow["h"] = Convert.ToDouble(txtPvLength.Text);
                                     ifea.DataRow["Azimuth"] = pvAz.AzimutAngle;
                                     ifea.DataRow["Ele_Angle"] = pvTilt.tiltAngle;
                                     if (iDemLyr != -1 & chkDEM.Checked == true)
                                     {
                                         Coordinate ptReference = new Coordinate(pt.X, pt.Y);
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
                                         if (radioAboveAssumeDatum.Checked == true)
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
                                 //------------------------------------------------------                       
                                 //fs.Features.Add(poleFe);

                                 lastL = LL;
                             }
                             residualL += L - lastL;
                         }
                     } // next segment
                 } // next shape
                 fs.Projection = pvMap.Projection;
                 fs.Name = "pv. Array pole";
                 fs.SaveAs(pvTmpDir + "\\Temp\\pvArrayPole.shp", true);
                 removeDupplicateLyr(fs.Name);
                 pvMap.Layers.Add(fs);
                 loadLayerList();
                 //pvMap.MapFrame.DrawingLayers.Clear();
                 cmbPolePosition.Text = fs.Name;
                 verify[5] = true;
                 updateArea();
             }
        }
*/

        void CratePvPole(double xSpacing)
        {
             if (pvTmpDir == "")
            {
                FolderBrowserDialog folderSel = new FolderBrowserDialog();
                folderSel.Description = "Please select temporary directory location :";
                folderSel.ShowDialog();
                pvTmpDir = folderSel.SelectedPath;
            }
             if (pvTmpDir != "")
             {
                 kGeoFunc.LineType BL = new kGeoFunc.LineType();
                 double dx = xSpacing; //m.
                 double dy = xSpacing; //m.

                 IFeatureSet fs;
                 fs = new FeatureSet(FeatureType.Point);
                 //---------------------------------------------------------
                 fs.DataTable.Columns.Add(new DataColumn("x", typeof(double)));
                 fs.DataTable.Columns.Add(new DataColumn("y", typeof(double)));
                 fs.DataTable.Columns.Add(new DataColumn("w", typeof(double)));
                 fs.DataTable.Columns.Add(new DataColumn("h", typeof(double)));
                 fs.DataTable.Columns.Add(new DataColumn("Azimuth", typeof(double)));
                 fs.DataTable.Columns.Add(new DataColumn("Ele_Angle", typeof(double)));
                 //---------------------------------------------------------
                 double lastL = 0;
                 double residualL = 0;
                 double L = 0;

                 for (int n = 0; n < grdBaseline.Rows.Count - 2; n++)
                 {
                     BL.Pt1.X = (double)this.grdBaseline.Rows[n].Cells[1].Value;
                     BL.Pt1.Y = (double)this.grdBaseline.Rows[n].Cells[2].Value;
                     BL.Pt2.X = (double)this.grdBaseline.Rows[n + 1].Cells[1].Value;
                     BL.Pt2.Y = (double)this.grdBaseline.Rows[n + 1].Cells[2].Value;

                     L = kGeoFunc.Length(BL);
                     Double sumL = 0;
                     double LL;
                     if (xSpacing <= L + residualL) // make sure the active segment is longer than the residual spcaing
                     {
                         for (LL = 0; LL <= L; LL += dx)
                         {
                             if (LL == 0 & residualL > 0)
                             {
                                 LL = xSpacing - residualL;
                                 residualL = 0;
                             }
                             kGeoFunc.POINTAPI pt = kGeoFunc.PointInLine(BL, LL);
                             Coordinate poleLocation = new Coordinate(pt.X, pt.Y);
                             IPoint poleFe = new DotSpatial.Topology.Point(poleLocation);
                             IFeature ifea = fs.AddFeature(poleFe);
                             //------------------------------------------------------
                             ifea.DataRow.BeginEdit();
                             ifea.DataRow["x"] = pt.X;
                             ifea.DataRow["y"] = pt.Y;
                             ifea.DataRow["w"] = Convert.ToDouble(txtPvWidth.Text);
                             ifea.DataRow["h"] = Convert.ToDouble(txtPvLength.Text);
                             ifea.DataRow["Azimuth"] = 45;
                             ifea.DataRow["Ele_Angle"] = 15;
                             //
                             ifea.DataRow.EndEdit();
                             //------------------------------------------------------                       
                             //fs.Features.Add(poleFe);

                             lastL = LL;
                         }
                         residualL += L - lastL;
                     }
                     else
                     {
                         residualL += L;
                     }
                 }

                 fs.Projection = pvMap.Projection;
                 fs.Name = "pv Array pole";
                 fs.SaveAs(pvTmpDir + "\\Temp\\pvArrayPole.shp", true);
                 pvMap.Layers.Add(fs);
                 //pvMap.MapFrame.DrawingLayers.Clear();
                 MapPointLayer rangeRingAxis;
                 rangeRingAxis = new MapPointLayer(fs);
                 pvMap.MapFrame.DrawingLayers.Add(rangeRingAxis);
                 pvMap.MapFrame.Invalidate();
             }
        }

 
        #region "Circle_Line_intersection_Function"

        private bool IsInsideCircle(Coordinate CirclePos, double CircleRad, Coordinate checkPoint)
        {
            if (Math.Sqrt(Math.Pow((CirclePos.X - checkPoint.X), 2) +
                          Math.Pow((CirclePos.Y - checkPoint.Y), 2)) < CircleRad)
            { return true; }
            else return false;
        }

        private bool IsIntersecting(Coordinate CirclePos, double CircleRad, Coordinate LineStart, Coordinate LineEnd)
        {
            if (IsInsideCircle(CirclePos, CircleRad, LineStart) ^
                IsInsideCircle(CirclePos, CircleRad, LineEnd))
            { return true; }
            else return false;
        }

        private int CircleLineIntersect(Coordinate CirclePos, double CircleRad,
                       Coordinate LineStart, Coordinate LineEnd, ref Coordinate Intersection)
        {
            if (IsIntersecting(CirclePos, CircleRad, LineStart, LineEnd))
            {
                //Calculate terms of the linear and quadratic equations
                var M = (LineEnd.Y - LineStart.Y) / (LineEnd.X - LineStart.X);
                var B = LineStart.Y - M * LineStart.X;
                var a = 1 + M * M;
                var b = 2 * (M * B - M * CirclePos.Y - CirclePos.X);
                var c = CirclePos.X * CirclePos.X + B * B + CirclePos.Y * CirclePos.Y -
                        CircleRad * CircleRad - 2 * B * CirclePos.Y;
                // solve quadratic equation
                var sqRtTerm = Math.Sqrt(b * b - 4 * a * c);
                var x = ((-b) + sqRtTerm) / (2 * a);
                // make sure we have the correct root for our line segment
                if ((x < LineStart.X) || (x > LineEnd.X)) { x = ((-b) - sqRtTerm) / (2 * a); }
                //solve for the y-component
                var y = M * x + B;
                // Intersection Calculated
                Intersection = new Coordinate(x, y);
                return 0;
            }
            else
            {
                // Line segment does not intersect at one point.  It is either fully outside,
                // fully inside, intersects at two points, is tangential to, or one or more
                // points is exactly on the circle radius.
                Intersection = new Coordinate(0, 0);
                return -1;
            }
        }

        #endregion

        void CratePvPanel()
        {
            if (pvTmpDir == "")
            {
                FolderBrowserDialog folderSel = new FolderBrowserDialog();
                folderSel.Description = "Please select temporary directory location :";
                folderSel.ShowDialog();
                pvTmpDir = folderSel.SelectedPath;
            }

            if (pvTmpDir != "")
            {
                grpBLineInfo.Visible = false;
                kGeoFunc.LineType BL = new kGeoFunc.LineType();
                kGeoFunc.LineType dL = new kGeoFunc.LineType();
                kGeoFunc.CircleType CL = new kGeoFunc.CircleType();
                double dx = 10.0; //m.
                //  Feature f = new Feature(axisLines);
                IFeatureSet fs;//= new FeatureSet(FeatureType.Line);
                fs = new FeatureSet(FeatureType.Polygon);

                for (int n = 0; n < grdBaseline.Rows.Count - 2; n++)
                {
                    BL.Pt1.X = (double)this.grdBaseline.Rows[n].Cells[1].Value;
                    BL.Pt1.Y = (double)this.grdBaseline.Rows[n].Cells[2].Value;
                    BL.Pt2.X = (double)this.grdBaseline.Rows[n + 1].Cells[1].Value;
                    BL.Pt2.Y = (double)this.grdBaseline.Rows[n + 1].Cells[2].Value;
                    double L = kGeoFunc.Length(BL);
                    //DrawLine(BL.Pt1.X, BL.Pt1.Y, BL.Pt2.X, BL.Pt2.Y, 3, System.Drawing.Color.Magenta);

                    for (double r = dx; r < L - dx / 2; r += dx)
                    {
                        //Todo: add PV array
                        CL.pt.X = BL.Pt1.X;
                        CL.pt.Y = BL.Pt1.Y;
                        CL.R = r;
                        double x1 = 0; double y1 = 0;
                        double x2 = 0; double y2 = 0;
                        //dL.Pt1.X = BL.Pt1.X;
                        double xt1 = 0; double yt1 = 0;
                        double xt2 = 0; double yt2 = 0;
                        double xt3 = 0; double yt3 = 0;
                        double xt4 = 0; double yt4 = 0;
                        //dL.Pt1.Y = BL.Pt1.Y;
                        CL.R = r - (dx / 2 - 0.5);
                        int mm = kGeoFunc.Line_Circle(BL, CL, ref xt1, ref yt1, ref xt2, ref yt2);
                        CL.R = r + (dx / 2 - 0.5);
                        int nn = kGeoFunc.Line_Circle(BL, CL, ref xt3, ref yt3, ref xt4, ref yt4);
                        //---------------------------------------------------
                        dL.Pt1.X = xt1;
                        dL.Pt1.Y = yt1;
                        dL.Pt2.X = xt3;
                        dL.Pt2.Y = yt3;
                        //---------------------------------------------------

                        CL.R = r;
                        if (kGeoFunc.Line_Circle(BL, CL, ref x1, ref y1, ref x2, ref y2) >= 1)
                        {
                            kGeoFunc.POINTAPI pt1;
                            kGeoFunc.POINTAPI pt2;
                            pt1.X = x1;
                            pt1.Y = y1;
                            pt2.X = x2;
                            pt2.Y = y2;
                            double dy = 60.0;
                            kGeoFunc.LineType Lt = kGeoFunc.Perpend2Point(BL, x1, y1, dy);
                            //Todo: add PV array
                            kGeoFunc.CircleType CLL = new kGeoFunc.CircleType();
                            CLL.pt.X = Lt.Pt1.X;
                            CLL.pt.Y = Lt.Pt1.Y;
                            double w = 25;
                            for (double r2 = 0; r2 <= dy; r2 += w)
                            {
                                CLL.R = r2;
                                if (kGeoFunc.Line_Circle(Lt, CLL, ref x1, ref y1, ref x2, ref y2) >= 1)
                                {
                                    fs.Features.Add(pvPanel(w, x1, y1, dL));
                                }
                            }
                        }

                    }
                }

                fs.Projection = pvMap.Projection;
                //MessageBox.Show(fea.ToString);  
                fs.Name = "pv Array";
                //pvMap4Draw.Layers.Add(fs);
                fs.SaveAs(pvTmpDir + "\\Temp\\pvArray.shp", true);
                pvMap.MapFrame.DrawingLayers.Clear();
                MapPolygonLayer rangeRingAxis;
                rangeRingAxis = new MapPolygonLayer(fs);// MapPolygonLayer(fs);
                //rangeRingAxis.Symbolizer = new PolygonSymbolizer(System.Drawing.Color.AliceBlue, System.Drawing.Color.LightBlue);
                pvMap.MapFrame.DrawingLayers.Add(rangeRingAxis);
                pvMap.MapFrame.Invalidate();
            }
        }


        private void pvAzAndTileAngle_Change(object sender, PaintEventArgs e)
        {
            try
            {
                double w = Convert.ToDouble(txtPvWidth.Text);
                double h = Convert.ToDouble(txtPvLength.Text);
                int PoleLyr = getLayerHdl(cmbPolePosition.Text);
                if (PoleLyr != -1)
                {
                    //-----------------------------------------------------
                    IFeatureSet pvfs;
                    pvfs = new FeatureSet(FeatureType.Polygon);
                    //-----------------------------------------------------
                    IMapFeatureLayer poleFe = pvMap.Layers[PoleLyr] as IMapFeatureLayer;
                    int nShp = poleFe.DataSet.NumRows() - 1;
                    Extent ext = poleFe.DataSet.GetFeature(nShp).Envelope.ToExtent();
                    for (int i = 0; i < poleFe.DataSet.NumRows(); i++)
                    {
                        IFeature fs = poleFe.DataSet.GetFeature(i);
                        double x1 = fs.Coordinates[0].X;
                        double y1 = fs.Coordinates[0].Y;
                        pvfs.Features.Add(pvPanel(w, h, x1, y1, pvTilt.tiltAngle, pvAz.AzimutAngle));
                    }

                    pvfs.Projection = pvMap.Projection;
                    pvfs.Name = "pv Array";
                    removeDupplicateLyr(pvfs.Name);
                    pvMap.MapFrame.DrawingLayers.Clear();
                    MapPolygonLayer rangeRingAxis;
                    rangeRingAxis = new MapPolygonLayer(pvfs);
                    rangeRingAxis.Symbolizer = new PolygonSymbolizer(System.Drawing.Color.AliceBlue, System.Drawing.Color.LightBlue);
                    pvMap.MapFrame.DrawingLayers.Add(rangeRingAxis);
                    pvMap.MapFrame.Invalidate();
                }
            }
            catch
            { }
        }

        double ac_production(double latitude, double longtitude, int timeZome, double tilt, double azimuth, int year, int month, int day, int hour, int min,
                             float beam, float diffuse, float tamb, float wspd, float snow,
                             float system_size, float derate, int track_mode, float tcell, float poa)
        {
            // SAM Simulation Core (SSC) C# Example
            // Copyright (c) 2012 National Renewable Energy Laboratory
            // author: Steven H. Janzou and Aron P. Dobos
            double ac = 0;
            SSC.Module sscModule = new SSC.Module("pvwattsfunc");
            SSC.Data sscData = new SSC.Data();

            sscData.SetNumber("year", year);                // general year (tiny effect in sun position)
            sscData.SetNumber("month", month);              // 1-12
            sscData.SetNumber("day", day);                  //1-number of days in month
            sscData.SetNumber("hour", hour);                // 0-23
            sscData.SetNumber("minute", min);               // minute of the hour (typically 30 min for midpoint calculation)
            sscData.SetNumber("lat", (float)latitude);      // latitude, degrees
            sscData.SetNumber("lon", (float)longtitude);    // longitude, degrees
            sscData.SetNumber("tz", timeZome);              // timezone from gmt, hours
            sscData.SetNumber("time_step", 1);              // time step, hours

            sscData.SetNumber("beam", beam);                // beam (DNI) irradiance, W/m2
            sscData.SetNumber("diffuse", diffuse);          // diffuse (DHI) horizontal irradiance, W/m2
            sscData.SetNumber("tamb", tamb);                // ambient temp, degree C
            sscData.SetNumber("wspd", wspd);                // wind speed, m/s
            sscData.SetNumber("snow", snow);                // snow depth, cm (0 is default - when there is snow, ground reflectance is increased.  assumes panels have been cleaned off)

            //-- system specifications
            sscData.SetNumber("system_size", system_size);  // system DC nameplate rating (kW)
            sscData.SetNumber("derate", derate);            // derate factor
            sscData.SetNumber("track_mode", track_mode);    // tracking mode 0=fixed, 1=1axis, 2=2axis
            sscData.SetNumber("azimuth", (int)azimuth);     // azimuth angle 0=north, 90=east, 180=south, 270=west
            sscData.SetNumber("tilt", (int)tilt);           // tilt angle from horizontal 0=flat, 90=vertical

            sscData.SetNumber("tcell", tcell);               // calculated cell temperature from previous timestep, degree C, (can default to ambient for morning or if you don't know)
            sscData.SetNumber("poa", poa);                   // plane of array irradiance (W/m2) from previous time step

            if (sscModule.Exec(sscData))
            {
                ac = (double)sscData.GetNumber("ac");
            }

            return ac;
        }

        void EnergyProduction(int numPvPanel)
        {
            pvVerify();
            if (verify[0] == false)
            {
                MessageBox.Show("Please assign the reference location before calculate energy production");
                return;
            }
            float beam = (float)Convert.ToDouble(txtBeam.Text);
            float diffuse = (float)Convert.ToDouble(txtDiffuse.Text);
            float tamb = (float)Convert.ToDouble(txtTemp.Text);
            float wspd = (float)Convert.ToDouble(txtWspd.Text);
            float snow = (float)Convert.ToDouble(txtSnow.Text);
            float system_size = (float)Convert.ToDouble(txtSystem_size.Text);
            float derate = (float)Convert.ToDouble(txtDerate.Text);
            int track_mode = cmbTrack_mode.SelectedIndex;
            float tcell = (float)Convert.ToDouble(txtTcell.Text);
            float poa = (float)Convert.ToDouble(txtPoa.Text);

            int year = dateTimePicker1.Value.Year;
            double Latitude = Convert.ToDouble(this.txtLAT.Text);
            double Longitude = Convert.ToDouble(this.txtLNG.Text);
            double UtmN = Convert.ToDouble(this.txtUtmN.Text);
            double UtmE = Convert.ToDouble(this.txtUtmE.Text);
            int TimeZone = Convert.ToInt16(this.txtTimeZone.Text);
            double tilt = pvTilt.tiltAngle;
            double az = pvAz.AzimutAngle;
            double[] monthlyAc = new double[13];
            double[] monthlyAcBase = new double[13];
            double[,] dailyAc = new double[13, 32];
            double[, ,] hourlyAc = new double[13, 32, 25];
            int iDay = 0;
            double panelW = Convert.ToDouble(txtPvWidth.Text);
            double panelH = Convert.ToDouble(txtPvLength.Text);
            //
            double DCNamplatePerSqrMeter = 25.6 / 4 * Convert.ToDouble(system_size);
            double panelA = panelW * panelH / DCNamplatePerSqrMeter; //AC factor per panel area

            double LastAc = 0;
            //-----------------------------------------------------------------
            SSC.Module sscModule = new SSC.Module("pvwattsfunc");
            SSC.Data sscData = new SSC.Data();
            sscData.SetNumber("lat", (float)Latitude);      // latitude, degrees
            sscData.SetNumber("lon", (float)Longitude);    // longitude, degrees
            sscData.SetNumber("tz", TimeZone);              // timezone from gmt, hours
            sscData.SetNumber("time_step", 1);              // time step, hours

            sscData.SetNumber("beam", beam);                // beam (DNI) irradiance, W/m2
            sscData.SetNumber("diffuse", diffuse);          // diffuse (DHI) horizontal irradiance, W/m2
            sscData.SetNumber("tamb", tamb);                // ambient temp, degree C
            sscData.SetNumber("wspd", wspd);                // wind speed, m/s
            sscData.SetNumber("snow", snow);                // snow depth, cm (0 is default - when there is snow, ground reflectance is increased.  assumes panels have been cleaned off)

            //-- system specifications
            sscData.SetNumber("system_size", system_size);  // system DC nameplate rating (kW)
            sscData.SetNumber("derate", derate);            // derate factor
            sscData.SetNumber("track_mode", track_mode);    // tracking mode 0=fixed, 1=1axis, 2=2axis
            sscData.SetNumber("azimuth", (int)az);     // azimuth angle 0=north, 90=east, 180=south, 270=west
            sscData.SetNumber("tilt", (int)tilt);           // tilt angle from horizontal 0=flat, 90=vertical
            //-----------------------------------------------------------------
            for (int month = 1; month <= 12; month++)
            {
                int month_day = System.DateTime.DaysInMonth(year, month);
                for (int day = 1; day <= month_day; day++)
                {
                    double baseAc = 0;
                    for (int hr = 0; hr < 24; hr++)
                    {
                        //Calculate energy production
                        double ac = 0;
                        /* = ac_production(Latitude, Longitude, TimeZone,
                                                   tilt, az, year, month, day, hr, 30, 
                                                   beam, diffuse, tamb, wspd, snow, 
                                                   system_size, derate, track_mode, tcell, poa);
                        
                         */
                        //-----------------------------------------------------------------

                        sscData.SetNumber("year", year);                // general year (tiny effect in sun position)
                        sscData.SetNumber("month", month);              // 1-12
                        sscData.SetNumber("day", day);                  //1-number of days in month
                        sscData.SetNumber("hour", hr);                  // 0-23
                        sscData.SetNumber("minute", 30);                // minute of the hour (typically 30 min for midpoint calculation)

                        sscData.SetNumber("tcell", tcell);               // calculated cell temperature from previous timestep, degree C, (can default to ambient for morning or if you don't know)
                        sscData.SetNumber("poa", poa);                   // plane of array irradiance (W/m2) from previous time step

                        if (sscModule.Exec(sscData))
                        {
                            ac = (double)sscData.GetNumber("ac");
                            //for previous timestep
                            //poa = sscData.GetNumber("poa");
                            //tcell = sscData.GetNumber("tcell");
                        }
                        //-----------------------------------------------------------------
                        hourlyAc[month, day, hr] = panelA * ac;
                        dailyAc[month, day] += panelA * ac;
                        dailyAcStore[iDay] += panelA * ac;
                        LastAc = ac;

                    }
                    iDay++;
                    monthlyAc[month] += dailyAc[month, day];
                    //monthlyAcBase[month] += baseAc;
                }
            }

            this.grdAcProduct.Rows.Clear();
            for (int month = 1; month <= 12; month++)
            {
                this.grdAcProduct.Rows.Add();
                this.grdAcProduct.Rows[month - 1].Cells[0].Value = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
                // this.grdAcProduct.Rows[month - 1].Cells[1].Value = Math.Round(monthlyAcBase[month] / 1000, 0);
                this.grdAcProduct.Rows[month - 1].Cells[1].Value = Math.Round(monthlyAc[month] / 1000, 0);
                this.grdAcProduct.Rows[month - 1].Cells[2].Value = Math.Round(monthlyAc[month] / 1000 * numPvPanel, 0);
                // this.grdAcProduct.Rows[month - 1].Cells[3].Value = Math.Round((monthlyAc[month] - monthlyAcBase[month]) / monthlyAcBase[month] * 100, 3).ToString() + "%";
            }
            this.grdAcProduct.Refresh();
            updateArea();
        }

        void EnergyProduction(int numPvPanel, string weatherFile)
        {
            pvVerify();
            if (verify[0] == false)
            {
                MessageBox.Show("Please assign the reference location before calculate energy production");
                return;
            }
            updateArea();
            float system_size = (float)Convert.ToDouble(txtSystem_size.Text);
            double panelW = Convert.ToDouble(txtPvWidth.Text);
            double panelH = Convert.ToDouble(txtPvLength.Text);
            double DCNamplatePerSqrMeter =  25.6/4*Convert.ToDouble(system_size) ;
            double panelA = panelW * panelH / DCNamplatePerSqrMeter; //AC factor per panel area

            
            float derate = (float)Convert.ToDouble(txtDerate.Text);
            int track_mode = cmbTrack_mode.SelectedIndex;
            //double Latitude = Convert.ToDouble(this.txtLAT.Text);
            //double Longitude = Convert.ToDouble(this.txtLNG.Text);
            //double UtmN = Convert.ToDouble(this.txtUtmN.Text);
            //double UtmE = Convert.ToDouble(this.txtUtmE.Text);
            //int TimeZone = Convert.ToInt16(this.txtTimeZone.Text);
            double tilt = pvTilt.tiltAngle;
            double az = pvAz.AzimutAngle;

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
                        for (int m=0; m < 12; m++)
                        {
                            mAC[idwSta, m] = ac[m];
                        }
                        //------ daily AC
                        int hh = 0;
                        for (int d = 0; d < 365; d++)
                        {
                            int nHr = 0;
                            float energy = 0;
                            for (int h = 0; h < 24 ; h++)
                            {
                                if (hourlyAc[h] > 0) nHr++;
                                energy += hourlyAc[hh];
                                hh++;
                            }
                            if (nHr>0)
                            {
                                dAC[idwSta, d] = energy / 1000; //Change unit W to KW
                            }else
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
                    double rPow = 1 / (Math.Pow(rX[idwSta] / 1000, px) *  Math.Pow(rY[idwSta] / 1000, py));
                    sumR += rPow;
                }                
                for (int m = 0; m < 12; m++)
                {
                    double sumRZ = 0;
                    for (int idwSta = 0; idwSta < nIdwSta; idwSta++)
                    {
                        double rPowZ =  mAC[idwSta, m] / (Math.Pow(rX[idwSta] / 1000, px) * Math.Pow(rY[idwSta] / 1000, py));
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
                this.grdAcProduct.Rows.Clear();
                for (int month = 1; month <= 12; month++)
                {
                    //---------------------------------------------
                    double MonthAc=0;
                    //MonthAc = MonthAc / dOfMonth(month);
                    MonthAc = MonthlyProduct[month - 1];
                    //acProduction[month - 1] = MonthAc;
                    //---------------------------------------------
                    this.grdAcProduct.Rows.Add();
                    this.grdAcProduct.Rows[month - 1].Cells[0].Value = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
                    this.grdAcProduct.Rows[month - 1].Cells[1].Value = Math.Round(MonthAc, 0);//PVWatts
                    this.grdAcProduct.Rows[month - 1].Cells[2].Value = Math.Round(MonthAc / DCNamplatePerSqrMeter, 2); // per Sqr.Meter
                    this.grdAcProduct.Rows[month - 1].Cells[3].Value = Math.Round(MonthAc * panelA * numPvPanel, 0); // System AC
                }
                this.grdAcProduct.Refresh();
                //-----------------------------------------------------------------
                DialogResult dialogResult = MessageBox.Show("Do you want to export daily production result?", "Hourly energy production", MessageBoxButtons.YesNo);
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
                                    sw.WriteLine(DateTimeFormatInfo.CurrentInfo.GetMonthName(month) + "/" + day + ", " + DailyProduct[ii].ToString() );
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

                    this.grdAcProduct.Rows.Clear();
                    for (int month = 1; month <= 12; month++)
                    {
                        this.grdAcProduct.Rows.Add();
                        this.grdAcProduct.Rows[month - 1].Cells[0].Value = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
                        this.grdAcProduct.Rows[month - 1].Cells[1].Value = Math.Round(ac[month - 1] , 0);// PVWatts
                        this.grdAcProduct.Rows[month - 1].Cells[2].Value = Math.Round(ac[month - 1] / DCNamplatePerSqrMeter, 2); // per Sqr.Meter
                        this.grdAcProduct.Rows[month - 1].Cells[3].Value = Math.Round(ac[month - 1] * panelA * numPvPanel, 0); // System Ac.
                    }
                    this.grdAcProduct.Refresh();

                    //-----------------------------------------------------------------
                    DialogResult dialogResult = MessageBox.Show("Do you want to export daily production result?", "Hourly energy production", MessageBoxButtons.YesNo);
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

        double[] MonthlyEnergyProduction(int numPvPanel, string weatherFile, double tilt, double az)
        {
            double[] MonthlyProduction = new double[12];
            double aunalProduction = 0;
            pvVerify();
            if (verify[0] == false)
            {
                return MonthlyProduction;
            }
            updateArea();
            float system_size = (float)Convert.ToDouble(txtSystem_size.Text);
            double panelW = Convert.ToDouble(txtPvWidth.Text);
            double panelH = Convert.ToDouble(txtPvLength.Text);
            double DCNamplatePerSqrMeter = 25.6 / 4 * Convert.ToDouble(system_size);
            double panelA = panelW * panelH / DCNamplatePerSqrMeter; //AC factor per panel area
            float derate = (float)Convert.ToDouble(txtDerate.Text);
            int track_mode = cmbTrack_mode.SelectedIndex;

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

        double AnualEnergyProduction(int numPvPanel, string weatherFile, double tilt, double az)
        {
            double aunalProduction = 0;
            pvVerify();
            if (verify[0] == false)
            {
                return 0;
            }
            updateArea();
            float system_size = (float)Convert.ToDouble(txtSystem_size.Text);
            double panelW = Convert.ToDouble(txtPvWidth.Text);
            double panelH = Convert.ToDouble(txtPvLength.Text);
            double DCNamplatePerSqrMeter = 25.6 / 4 * Convert.ToDouble(system_size);
            double panelA = panelW * panelH / DCNamplatePerSqrMeter; //AC factor per panel area
            float derate = (float)Convert.ToDouble(txtDerate.Text);
            int track_mode = cmbTrack_mode.SelectedIndex;

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

        int dOfMonth(int m)
        {
            int dOm = -1; 
            if (m == 1) dOm = 31;//JAN
            if (m == 2) dOm = 28;//FEB
            if (m == 3) dOm = 31;//MAR
            if (m == 4) dOm = 30;//APR
            if (m == 5) dOm = 31;//MAY
            if (m == 6) dOm = 30;//JUN
            if (m == 7) dOm = 31;//JUL
            if (m == 8) dOm = 31;//AUG
            if (m == 9) dOm = 30;//SEP
            if (m == 10) dOm = 31;//OCT
            if (m == 11) dOm = 30;//NOV
            if (m == 12) dOm = 31;//DEC
            return dOm; 
        }

        void setCurrrentLayer(string LyrName)
        {
            int i = getLayerHdl(LyrName);
            if (i == -1)
            {
                MessageBox.Show("Please select layer to assign as current layer before");
            }
            else
            {
                pvMap.Layers.SelectedLayer = pvMap.Layers[i];
            }
        }

        String pvPanel(double w, double h, double x0, double y0, double z0, double tilt, double az)
        {
            Coordinate[] pvShape = new Coordinate[4]; //x-axis
            Coordinate[] pvShapeR = new Coordinate[4]; //x-axis
            double hr = h * Math.Cos(tilt * Math.PI / 180);
            double zr = h * Math.Sin(tilt * Math.PI / 180);
            pvShape[0] = new Coordinate(-w / 2, -hr / 2, -zr / 2);
            pvShape[1] = new Coordinate(+w / 2, -hr / 2, -zr / 2);
            pvShape[2] = new Coordinate(+w / 2, +hr / 2, +zr / 2);
            pvShape[3] = new Coordinate(-w / 2, +hr / 2, +zr / 2);
            for (int i = 0; i < 4; i++)
            {
                double x = kGeoFunc.Rx(pvShape[i].X, pvShape[i].Y, az);
                double y = kGeoFunc.Ry(pvShape[i].X, pvShape[i].Y, az);
                pvShapeR[i] = new Coordinate(x0 + x, y0 + y, z0 + pvShape[i].Z);

            }
            string vertex1 = pvShapeR[2].X.ToString() + " " + pvShapeR[2].Y.ToString() + " " + pvShapeR[2].Z.ToString();
            string vertex2 = pvShapeR[0].X.ToString() + " " + pvShapeR[0].Y.ToString() + " " + pvShapeR[0].Z.ToString();
            string vertex3 = pvShapeR[1].X.ToString() + " " + pvShapeR[1].Y.ToString() + " " + pvShapeR[1].Z.ToString();
            string vertex4 = pvShapeR[3].X.ToString() + " " + pvShapeR[3].Y.ToString() + " " + pvShapeR[3].Z.ToString();
            String fe = vertex1 + " " + vertex2 + " " + vertex3 + " " + vertex4;
            return fe;
        }


        #endregion

        #region SketchUp

        #region Tree

        void setTreeShape()
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
        }

        int getTreeTypeId(string treeTypeName)
        {
            int id = -1;
            if (treeTypeName.ToUpper() == "Speading shape".ToUpper()) id = 0;
            if (treeTypeName.ToUpper() == "Round shape".ToUpper()) id = 1;
            if (treeTypeName.ToUpper() == "Pyramidal shape".ToUpper()) id = 2;
            if (treeTypeName.ToUpper() == "Oval shape".ToUpper()) id = 3;
            if (treeTypeName.ToUpper() == "Conical shape".ToUpper()) id = 4;
            if (treeTypeName.ToUpper() == "Vase shape".ToUpper()) id = 5;
            if (treeTypeName.ToUpper() == "Columnar shape".ToUpper()) id = 6;
            if (treeTypeName.ToUpper() == "Open shape".ToUpper()) id = 7;
            if (treeTypeName.ToUpper() == "Weeping shape".ToUpper()) id = 8;
            if (treeTypeName.ToUpper() == "Irrigular shape".ToUpper()) id = 9;
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
                treeShape[2, 0] = 0.193548387096774; treeShape[2, 1] = 0.790322580645161;
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
            int iDemLyr = getLayerHdl(cmbDem.Text);
            if (iDemLyr != -1 & chkDEM.Checked == true)
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
            XmlWriter writer = XmlWriter.Create(pvDir + "\\SketchUp\\temp.xml", settings);
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
                    double x1 = (fs.Coordinates[0].X - Convert.ToDouble(txtUtmE.Text)) * ToMeter;
                    double y1 = (fs.Coordinates[0].Y - Convert.ToDouble(txtUtmN.Text)) * ToMeter;
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
                                        if (iDemLyr != -1 & chkDEM.Checked == true)
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
                StreamReader sr = new StreamReader(pvDir + "\\SketchUp\\temp.xml");
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
            XmlWriter writer = XmlWriter.Create(pvDir + "\\SketchUp\\temp.xml", settings);
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
                double w = Convert.ToDouble(txtPvWidth.Text) * ToMeter;
                double h = Convert.ToDouble(txtPvLength.Text) * ToMeter;
                double az = pvAz.AzimutAngle;
                double tilt = pvTilt.tiltAngle;
                double xx0 = Convert.ToDouble(txtUtmE.Text);
                double yy0 = Convert.ToDouble(txtUtmN.Text);
                for (int i = 0; i < poleFe.DataSet.NumRows(); i++)
                {
                    IFeature fs = poleFe.DataSet.GetFeature(i);
                    object val = fs.DataRow["ele"];
                    double poleEle = Convert.ToDouble(val);
                    double x0 = (fs.Coordinates[0].X - xx0) * ToMeter;
                    double y0 = (fs.Coordinates[0].Y - yy0) * ToMeter;
                    double z0 = (fs.Coordinates[0].Z) * ToMeter;
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
                                    if (chkDEM.Checked == true)
                                    {
                                        writer.WriteString(pvPanel(w, h, x0, y0, poleEle, tilt, az));
                                    }
                                    else
                                    {
                                        writer.WriteString(pvPanel(w, h, x0, y0, 0, tilt, az));
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
                StreamReader sr = new StreamReader(pvDir + "\\SketchUp\\temp.xml");
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
            double x0 = Convert.ToDouble(txtUtmE.Text);
            double y0 = Convert.ToDouble(txtUtmN.Text);
            double z0 = 0;
            IMapRasterLayer demLyr;
            Raster dem4Pv = new Raster();
            int iDemLyr = getLayerHdl(cmbDem.Text);
            if (iDemLyr != -1 & chkDEM.Checked == true)
            {
                demLyr = pvMap.Layers[iDemLyr] as IMapRasterLayer;
                int mRow = demLyr.Bounds.NumRows;
                int mCol = demLyr.Bounds.NumColumns;
                dem4Pv = (Raster)demLyr.DataSet;
                Coordinate ptReference = new Coordinate(Convert.ToDouble(txtUtmE.Text), Convert.ToDouble(txtUtmN.Text));
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
            XmlWriter writer = XmlWriter.Create(pvDir + "\\SketchUp\\temp.xml", settings);
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
                StreamReader sr = new StreamReader(pvDir + "\\SketchUp\\temp.xml");
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

        private void cmdReloadLayerList_Click(object sender, EventArgs e)
        {
            loadLayerList();
        }



        private void chkDEM_CheckedChanged(object sender, EventArgs e)
        {
            cmbDem.Enabled = chkDEM.Checked;
        }

        private void chkBuilding_CheckedChanged(object sender, EventArgs e)
        {
            cmbBldg.Enabled = chkBuilding.Checked;
            if (chkBuilding.Checked == true) checkBuildingFieldPropeties(false);
            //------------------------------------------------
            cmdBldgEraser.Enabled = chkBuilding.Checked;
            //cmdShadowAnalysis.Enabled = chkBuilding.Checked;
            cmdSaveBldg.Enabled = chkBuilding.Checked;
            cmdReloadBldgData.Enabled = chkBuilding.Checked;
            cmdSelBldg.Enabled = chkBuilding.Checked;
            grdBldg.Enabled = chkBuilding.Checked;
        }

        private void chkTree_CheckedChanged(object sender, EventArgs e)
        {
            cmbTree.Enabled = chkTree.Checked;
            if (chkTree.Checked == true) checkTreeFieldPropeties(false);
            //---------------------------------------------
            picTree.Enabled = chkTree.Checked;
            cmdReloadTreeData.Enabled = chkTree.Checked;
            cmdSaveEditTreeShp.Enabled = chkTree.Checked;
            cmdTreeEraser.Enabled = chkTree.Checked;
            cmdFeatureSelection.Enabled = chkTree.Checked;
            cmdDrawTreeMap.Enabled = chkTree.Checked;
            grdTreeAttribute.Enabled = chkTree.Checked;
            cmdSetSelectedTree.Enabled = chkTree.Checked;
        }

        private void grdTreeAttribute_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbBldg_TextChanged(object sender, EventArgs e)
        {
            checkBuildingFieldPropeties(true);
        }

        void checkBuildingFieldPropeties(bool enable)
        {
            if (cmbBldg.Text != "")
            {
                int bldgLyr = getLayerHdl(cmbBldg.Text);
                if (bldgLyr == -1)
                {
                    MessageBox.Show("Cannot open the selected layer");
                    chkBuilding.Checked = false;
                    cmbBldg.Text = ""; cmbBldg.Refresh();
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
                    if (field1Chk == false) MessageBox.Show("Error: Layer " + cmbBldg.Text + " doesn't have field [Height]");
                    if (field2Chk == false) MessageBox.Show("Error: Layer " + cmbBldg.Text + " doesn't have field [Remark]");
                    if (field1Chk == false | field2Chk == false)
                    {
                        if (enable == true) chkBuilding.Checked = false;
                        cmbBldg.Text = ""; cmbBldg.Refresh();
                    }
                }
            }
        }

        private void cmbTree_TextChanged(object sender, EventArgs e)
        {
            checkTreeFieldPropeties(true);
        }

        void checkTreeFieldPropeties(bool enable)
        {
            if (cmbTree.Text != "")
            {
                int treeLyr = getLayerHdl(cmbTree.Text);
                if (treeLyr == -1)
                {
                    MessageBox.Show("Cannot open the selected layer");
                    chkTree.Checked = false;
                    cmbTree.Text = ""; cmbTree.Refresh();
                }
                else
                {
                    bool field1Chk = false;
                    bool field2Chk = false;
                    bool field3Chk = false;
                    IMapFeatureLayer treeFs = pvMap.Layers[treeLyr] as IMapFeatureLayer;
                    for (int i = 0; i < treeFs.DataSet.DataTable.Columns.Count; i++)
                    {
                        if (treeFs.DataSet.DataTable.Columns[i].ColumnName == "diameter") field1Chk = true;
                        if (treeFs.DataSet.DataTable.Columns[i].ColumnName == "Height") field2Chk = true;
                        if (treeFs.DataSet.DataTable.Columns[i].ColumnName == "type") field3Chk = true;
                    }
                    if (field1Chk == false) MessageBox.Show("Error: Layer " + cmbTree.Text + " doesn't have field [diameter]");
                    if (field2Chk == false) MessageBox.Show("Error: Layer " + cmbTree.Text + " doesn't have field [Height]");
                    if (field3Chk == false) MessageBox.Show("Error: Layer " + cmbTree.Text + " doesn't have field [type]");
                    if (field1Chk == false | field2Chk == false | field3Chk == false)
                    {
                        if (enable == true) chkTree.Checked = false;
                        cmbTree.Text = ""; cmbTree.Refresh();
                    }
                }
            }
        }

        private void cmbAlignmentLyr_TextChanged(object sender, EventArgs e)
        {
            checkAlignmentFieldPropeties();
        }

        void checkAlignmentFieldPropeties()
        {
            if (cmbAlignmentLyr.Text != "")
            {
                int alignmentLyr = getLayerHdl(cmbAlignmentLyr.Text);
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
                        MessageBox.Show("Error: Layer " + cmbAlignmentLyr.Text + " doesn't have field [spacing]");
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
       
        private void cmdReport_Click(object sender, EventArgs e)
        {
         if (pvTmpDir == "")
            {
                FolderBrowserDialog folderSel = new FolderBrowserDialog();
                folderSel.Description = "Please select temporary directory location :";
                folderSel.ShowDialog();
                pvTmpDir = folderSel.SelectedPath;
            }
            if (pvTmpDir != "")
            {
                ExportAspdf();
            }
        }

        void ExportAspdf()
        {
            
            SaveFileDialog saveFileDlg = new SaveFileDialog();

            saveFileDlg.Filter = "pdf files (*.pdf)|*.pdf";
            saveFileDlg.FilterIndex = 1;
            saveFileDlg.RestoreDirectory = true;

            if (saveFileDlg.ShowDialog() == DialogResult.OK)
            {
                iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);

               try
                {
                    PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(saveFileDlg.FileName, FileMode.Create));
                    doc.Open();//Open Document to write
                    //Write some content
                }
               catch
                {
                    MessageBox.Show("The process cannot access the file '" + saveFileDlg.FileName +"' because it is being used by another process.");  
                    return; 
                }
                iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph("---");

                #region "Project detail"
                paragraph = new iTextSharp.text.Paragraph("Project detail");
                doc.Add(paragraph);
                paragraph = new iTextSharp.text.Paragraph("Project name: ");
                doc.Add(paragraph);
                paragraph = new iTextSharp.text.Paragraph("Date time: " + DateTime.Now.ToString());
                doc.Add(paragraph);
                paragraph = new iTextSharp.text.Paragraph("Location: " + txtLAT.Text + ", " + txtLNG.Text + " (Lat, Long)");
                doc.Add(paragraph);
                paragraph = new iTextSharp.text.Paragraph("Time zone: UTC" + txtTimeZone.Text);
                doc.Add(paragraph);

                paragraph = new iTextSharp.text.Paragraph("------------------------------------------------------------");
                doc.Add(paragraph);
                paragraph = new iTextSharp.text.Paragraph("Remark: " + lblWeatherDetail1.Text);
                doc.Add(paragraph);
                if (optMultiWeatherSta.Checked == true)
                {
                    paragraph = new iTextSharp.text.Paragraph("  - " + lblWeatherDetail2.Text);
                    doc.Add(paragraph);
                    paragraph = new iTextSharp.text.Paragraph("  - " + lblWeatherDetail3.Text);
                    doc.Add(paragraph);
                    paragraph = new iTextSharp.text.Paragraph("  - " + lblWeatherDetail4.Text);
                    doc.Add(paragraph);
                    paragraph = new iTextSharp.text.Paragraph("  - " + lblWeatherDetail5.Text);
                    doc.Add(paragraph);
                    paragraph = new iTextSharp.text.Paragraph("  - " + "Weather File:" + txtTM2.Text);
                    doc.Add(paragraph);
                }
                #endregion

                #region "Energy production"
                paragraph = new iTextSharp.text.Paragraph("");
                doc.Add(paragraph);
                paragraph = new iTextSharp.text.Paragraph("Energy production");
                doc.Add(paragraph);
                //iTextSharp.text.Paragraph paragraph2 = new iTextSharp.text.Paragraph("");
                paragraph = new iTextSharp.text.Paragraph("Table 1 Energy production");
                // System.Data.DataTable dt = ((DataView)this.grdAcProduct).Table;
                // if (dt != null)
                {
                    //Craete instance of the pdf table and set the number of column in that table
                    PdfPTable PdfTable = new PdfPTable(grdAcProduct.Columns.Count);
                    PdfPCell PdfPCell = null;

                    iTextSharp.text.Font font8 = FontFactory.GetFont("ARIAL", 7);
                    iTextSharp.text.Font font8h = FontFactory.GetFont("ARIAL", 10);
                    //How add the data from datatable to pdf table
                    for (int rows = 0; rows < grdAcProduct.Rows.Count; rows++)
                    {
                        if (rows == 0)
                        {
                            for (int column = 0; column < grdAcProduct.Columns.Count; column++)
                            {
                                string st = "";
                                try
                                {
                                    st = grdAcProduct.Columns[column].HeaderText;
                                }
                                catch { }
                                PdfPCell = new PdfPCell(new Phrase(new Chunk(st, font8h)));
                                PdfTable.AddCell(PdfPCell);
                            }
                        }
                        for (int column = 0; column < grdAcProduct.Columns.Count; column++)
                        {
                            // grdAcProduct.Rows[r - 1].Cells[c - 1].Value.ToString()
                            string st = "";
                            try
                            {
                                st= grdAcProduct.Rows[rows].Cells[column].Value.ToString();
                            } catch{}

                            PdfPCell = new PdfPCell(new Phrase(new Chunk(st, font8)));
                            PdfTable.AddCell(PdfPCell);
                        }
                    }

                    PdfTable.SpacingBefore = 15f; // Give some space after the text or it may overlap the table

                    doc.Add(PdfTable); // add pdf table to the document
                }
                #endregion


                #region "Design criteria"
                paragraph = new iTextSharp.text.Paragraph("Design criteria");
                doc.Add(paragraph);
                paragraph = new iTextSharp.text.Paragraph("Table 2 Photovoltaic system configurations");
                doc.Add(paragraph);
                //Craete instance of the pdf table and set the number of column in that table
                PdfPTable PdfTable2 = new PdfPTable(2);
                PdfPCell PdfPCell2 = null;

                iTextSharp.text.Font font4Tab2 = FontFactory.GetFont("ARIAL", 7);
                iTextSharp.text.Font font4Tab2h = FontFactory.GetFont("ARIAL", 10);
                iTextSharp.text.Font font4Tab2t = FontFactory.GetFont("ARIAL", 8);
                //Add Header of the pdf table
                PdfPCell2 = new PdfPCell(new Phrase(new Chunk("Parameter", font4Tab2h)));
                PdfTable2.AddCell(PdfPCell2);

                PdfPCell2 = new PdfPCell(new Phrase(new Chunk("Value", font4Tab2h)));
                PdfTable2.AddCell(PdfPCell2);

                //header           
                PdfPCell2 = new PdfPCell(new Phrase(new Chunk("Width (m)", font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);
                PdfPCell2 = new PdfPCell(new Phrase(new Chunk(txtPvWidth.Text, font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);

                PdfPCell2 = new PdfPCell(new Phrase(new Chunk("Height(m)", font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);
                PdfPCell2 = new PdfPCell(new Phrase(new Chunk(txtPvLength.Text, font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);

                PdfPCell2 = new PdfPCell(new Phrase(new Chunk("Number of Pv. panel", font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);
                PdfPCell2 = new PdfPCell(new Phrase(new Chunk(numPvPanel.ToString(), font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);

                PdfPCell2 = new PdfPCell(new Phrase(new Chunk("Azimuth angle(deg)", font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);
                PdfPCell2 = new PdfPCell(new Phrase(new Chunk(pvAz.AzimutAngle.ToString(), font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);

                PdfPCell2 = new PdfPCell(new Phrase(new Chunk("Tilt angle (deg)", font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);
                PdfPCell2 = new PdfPCell(new Phrase(new Chunk(pvTilt.tiltAngle.ToString(), font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);

                PdfPCell2 = new PdfPCell(new Phrase(new Chunk("System specification", font4Tab2t)));
                PdfTable2.AddCell(PdfPCell2);
                PdfPCell2 = new PdfPCell(new Phrase(new Chunk("", font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);

                PdfPCell2 = new PdfPCell(new Phrase(new Chunk(lblPvSpec1.Text, font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);
                PdfPCell2 = new PdfPCell(new Phrase(new Chunk(txtSystem_size.Text, font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);

                PdfPCell2 = new PdfPCell(new Phrase(new Chunk(lblPvSpec2.Text, font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);
                PdfPCell2 = new PdfPCell(new Phrase(new Chunk(txtDerate.Text, font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);

                PdfPCell2 = new PdfPCell(new Phrase(new Chunk(lblPvSpec3.Text, font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);
                PdfPCell2 = new PdfPCell(new Phrase(new Chunk(cmbTrack_mode.Text, font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);

                PdfPCell2 = new PdfPCell(new Phrase(new Chunk(lblPvSpec4.Text, font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);
                PdfPCell2 = new PdfPCell(new Phrase(new Chunk(txtTcell.Text, font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);

                PdfPCell2 = new PdfPCell(new Phrase(new Chunk(lblPvSpec5.Text, font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);
                PdfPCell2 = new PdfPCell(new Phrase(new Chunk(txtPoa.Text, font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);

                PdfPCell2 = new PdfPCell(new Phrase(new Chunk("Weather data", font4Tab2t)));
                PdfTable2.AddCell(PdfPCell2);
                PdfPCell2 = new PdfPCell(new Phrase(new Chunk("", font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);

                PdfPCell2 = new PdfPCell(new Phrase(new Chunk(lblWeather1.Text, font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);
                PdfPCell2 = new PdfPCell(new Phrase(new Chunk(txtBeam.Text, font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);

                PdfPCell2 = new PdfPCell(new Phrase(new Chunk(lblWeather2.Text, font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);
                PdfPCell2 = new PdfPCell(new Phrase(new Chunk(txtDiffuse.Text, font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);

                PdfPCell2 = new PdfPCell(new Phrase(new Chunk(lblWeather3.Text, font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);
                PdfPCell2 = new PdfPCell(new Phrase(new Chunk(txtTemp.Text, font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);

                PdfPCell2 = new PdfPCell(new Phrase(new Chunk(lblWeather4.Text, font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);
                PdfPCell2 = new PdfPCell(new Phrase(new Chunk(txtWspd.Text, font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);

                PdfPCell2 = new PdfPCell(new Phrase(new Chunk(lblWeather5.Text, font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);
                PdfPCell2 = new PdfPCell(new Phrase(new Chunk(txtSnow.Text, font4Tab2)));
                PdfTable2.AddCell(PdfPCell2);

                PdfTable2.SpacingBefore = 15f; // Give some space after the text or it may overlap the table
                doc.Add(PdfTable2); // add pdf table to the document

                #endregion

                #region "Solar distribution table"
                paragraph = new iTextSharp.text.Paragraph("");
                doc.Add(paragraph);
                paragraph = new iTextSharp.text.Paragraph("Table 3 Solar radiation distribution");
                doc.Add(paragraph);
                // Insert table
                //Craete instance of the pdf table and set the number of column in that table
                PdfPTable PdfTable3 = new PdfPTable(grdSolarRose.Columns.Count);
                PdfPCell PdfPCell3 = null;

                iTextSharp.text.Font font4Tab3 = FontFactory.GetFont("ARIAL", 7);
                iTextSharp.text.Font font4Tab3H = FontFactory.GetFont("ARIAL", 10);
                //Add Header of the pdf table
                //How add the data from datatable to pdf table
                for (int rows = 0; rows < grdSolarRose.Rows.Count; rows++)
                {
                    if (rows == 0)
                    {
                        for (int column = 0; column < grdSolarRose.Columns.Count; column++)
                        {
                            string st = "";
                            try
                            {
                                st = grdSolarRose.Columns[column].HeaderText;
                            }
                            catch { }
                            PdfPCell3 = new PdfPCell(new Phrase(new Chunk(st, font4Tab3H)));
                            PdfTable3.AddCell(PdfPCell3);
                        }
                    }
                    for (int column = 0; column < grdSolarRose.Columns.Count; column++)
                    {
                        string st = "";
                        try
                        {
                            st = grdSolarRose.Rows[rows].Cells[column].Value.ToString();
                        }
                        catch { }
                            PdfPCell3 = new PdfPCell(new Phrase(new Chunk(st, font4Tab3)));

                        PdfTable3.AddCell(PdfPCell3);
                    }
                }

                PdfTable3.SpacingBefore = 15f; // Give some space after the text or it may overlap the table

                doc.Add(PdfTable3); // add pdf table to the document
                #endregion "Solar distribution table"


                #region "Daily data"

                #endregion

                #region "Picture"
                // Insert picture
                //---------------------------------------------------------
                System.Drawing.Rectangle re = new System.Drawing.Rectangle(0, 0, pvMap.Width, pvMap.Height);
                Bitmap b = new Bitmap(pvMap.Width, pvMap.Height);
                Graphics g = Graphics.FromImage(b);
                pvMap.MapFrame.Print(g, re);
                string path = pvTmpDir;// System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string absolute_path = System.IO.Path.GetTempFileName().Replace(".tmp", ".bmp");
                //var absolute_path = pvTmpDir + "\\Temp\\snapshot\\map-" + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".bmp";
                b.Save(absolute_path);
                System.Drawing.Image image = System.Drawing.Image.FromFile(absolute_path);
                iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Bmp);
                if (pic.Height > pic.Width)
                {
                    //Maximum height is 800 pixels.
                    float percentage = 0.0f;
                    percentage = 700 / pic.Height;
                    pic.ScalePercent(percentage * 100);
                }
                else
                {
                    //Maximum width is 600 pixels.
                    float percentage = 0.0f;
                    percentage = 540 / pic.Width;
                    pic.ScalePercent(percentage * 100);
                }
                
                doc.Add(pic);
                //---------------------------------------------------------
                var graph1_path = System.IO.Path.GetTempFileName().Replace(".tmp", ".bmp"); //pvTmpDir + "\\Temp\\snapshot\\graph1-" + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".bmp";
                var graph2_path = System.IO.Path.GetTempFileName().Replace(".tmp", ".bmp"); //pvTmpDir + "\\Temp\\snapshot\\graph2-" + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".bmp";
                var graph3_path = System.IO.Path.GetTempFileName().Replace(".tmp", ".bmp"); //pvTmpDir + "\\Temp\\snapshot\\graph3-" + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".bmp";
                zGraphAz2EleAng.GetImage().Save(graph1_path);
                zGraphAz2Day.GetImage().Save(graph2_path);
                zGraphEleAng2Day.GetImage().Save(graph3_path);
                //
                System.Drawing.Image imageG1 = System.Drawing.Image.FromFile(graph1_path);
                iTextSharp.text.Image picG1 = iTextSharp.text.Image.GetInstance(imageG1, System.Drawing.Imaging.ImageFormat.Bmp);
                doc.Add(picG1);
                System.Drawing.Image imageG2 = System.Drawing.Image.FromFile(graph2_path);
                iTextSharp.text.Image picG2 = iTextSharp.text.Image.GetInstance(imageG2, System.Drawing.Imaging.ImageFormat.Bmp);
                doc.Add(picG2);
                System.Drawing.Image imageG3 = System.Drawing.Image.FromFile(graph3_path);
                iTextSharp.text.Image picG3 = iTextSharp.text.Image.GetInstance(imageG3, System.Drawing.Imaging.ImageFormat.Bmp);
                doc.Add(picG3);
                //----------------------------------------------------------
                if (haveOptimizeGraph == true)
                {
                    var gOpti1_path = System.IO.Path.GetTempFileName().Replace(".tmp", ".bmp"); //path + "\\Temp\\snapshot\\gOpti1-" + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".bmp";
                    var gOpti2_path = System.IO.Path.GetTempFileName().Replace(".tmp", ".bmp"); //path + "\\Temp\\snapshot\\gOpti2-" + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".bmp";
                    var gOpti3_path = System.IO.Path.GetTempFileName().Replace(".tmp", ".bmp"); //path + "\\Temp\\snapshot\\gOpti3-" + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".bmp";
                    var gOpti4_path = System.IO.Path.GetTempFileName().Replace(".tmp", ".bmp"); //path + "\\Temp\\snapshot\\gOpti4-" + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".bmp";
                    zedGOpti1.GetImage().Save(gOpti1_path);
                    zedGOpti2.GetImage().Save(gOpti2_path);
                    zedGOpti3.GetImage().Save(gOpti3_path);
                    zedGOpti4.GetImage().Save(gOpti4_path);
                    //
                    System.Drawing.Image imageGOpti1 = System.Drawing.Image.FromFile(gOpti1_path);
                    iTextSharp.text.Image picGOpti1 = iTextSharp.text.Image.GetInstance(imageGOpti1, System.Drawing.Imaging.ImageFormat.Bmp);
                    doc.Add(picGOpti1);
                    System.Drawing.Image imageGOpti2 = System.Drawing.Image.FromFile(gOpti2_path);
                    iTextSharp.text.Image picGOpti2 = iTextSharp.text.Image.GetInstance(imageGOpti2, System.Drawing.Imaging.ImageFormat.Bmp);
                    doc.Add(picGOpti2);
                    System.Drawing.Image imageGOpti3 = System.Drawing.Image.FromFile(gOpti3_path);
                    iTextSharp.text.Image picGOpti3 = iTextSharp.text.Image.GetInstance(imageGOpti3, System.Drawing.Imaging.ImageFormat.Bmp);
                    doc.Add(picGOpti3);
                    System.Drawing.Image imageGOpti4 = System.Drawing.Image.FromFile(gOpti4_path);
                    iTextSharp.text.Image picGOpti4 = iTextSharp.text.Image.GetInstance(imageGOpti4, System.Drawing.Imaging.ImageFormat.Bmp);
                    doc.Add(picGOpti4);
                }
                //----------------------------------------------------------
                #endregion "picture"


                System.Threading.Thread.Sleep(5000);  //Wait for 5 more seconds
                doc.Close(); //Close document

                MessageBox.Show("Peport complete");
            }
        }

        void ExportAsWord()
        {
            var wordApp = new Microsoft.Office.Interop.Word.Application();
            wordApp.Visible = true;
            wordApp.Documents.Add();
            object template = Missing.Value; //No template.
            object newTemplate = Missing.Value; //Not creating a template.
            object documentType = Missing.Value; //Plain old text document.
            object visible = true;  //Show the doc while we work.
            _Document doc = wordApp.Documents.Add(ref template,
                     ref newTemplate,
                     ref documentType,
                     ref visible);

            #region "Project detail"
            textParagraph(doc, "Arial", 18, true, "Project detail");
            textParagraph(doc, "Arial", 11, "Project name: ");
            textParagraph(doc, "Arial", 11, "Date time: " + DateTime.Now.ToString());
            textParagraph(doc, "Arial", 11, "Location: " + txtLAT.Text + ", " + txtLNG.Text + " (Lat, Long)");
            textParagraph(doc, "Arial", 11, "Time zone: UTC" + txtTimeZone.Text);
            #endregion

            #region "Energy production"
            textParagraph(doc, "Arial", 11, true, "");
            textParagraph(doc, "Arial", 18, true, "Energy production");
            textParagraph(doc, "Arial", 11, true, "Table 1 Energy production");
            var pACTable = doc.Paragraphs.Add();
            pACTable.Format.SpaceAfter = 10f;
            int numACRow = grdAcProduct.RowCount;
            int numACCol = grdAcProduct.ColumnCount;
            if (numACRow > 0 & numACCol > 0)
            {
                var ACtable = doc.Tables.Add(pACTable.Range, numACRow, numACCol, WdDefaultTableBehavior.wdWord9TableBehavior);
                for (var r = 0; r < ACtable.Rows.Count; r++)
                    for (var c = 1; c <= ACtable.Columns.Count; c++)
                    {
                        if (r == 0)
                        {
                            ACtable.Cell(r + 1, c).Range.Text = grdAcProduct.Columns[c - 1].HeaderText;
                        }
                        else
                        {
                            if (grdAcProduct.Rows[r - 1].Cells[c - 1].Value != null)
                            {
                                ACtable.Cell(r + 1, c).Range.Text = grdAcProduct.Rows[r - 1].Cells[c - 1].Value.ToString();
                            }
                            else
                            {
                                ACtable.Cell(r + 1, c).Range.Text = "";
                            }
                        }
                    }
            }

            #endregion


            #region "Design criteria"
            doc.Words.Last.InsertBreak(Microsoft.Office.Interop.Word.WdBreakType.wdPageBreak);
            textParagraph(doc, "Arial", 18, true, "Design criteria");
            textParagraph(doc, "Arial", 11, true, "Table 2 Photovoltaic system configurations");
            var pTable2 = doc.Paragraphs.Add();
            pTable2.Format.SpaceAfter = 10f;
            int numTab2Row = 18;
            int numTab2Col = 2;
            var table2 = doc.Tables.Add(pTable2.Range, numTab2Row, numTab2Col, WdDefaultTableBehavior.wdWord9TableBehavior);
            //header
            table2.Cell(1, 1).Range.Text = "Parameter"; table2.Cell(1, 2).Range.Text = "Value";
            table2.Cell(2, 1).Range.Text = "Width (m)"; table2.Cell(2, 2).Range.Text = txtPvWidth.Text;
            table2.Cell(3, 1).Range.Text = "Height(m)"; table2.Cell(3, 2).Range.Text = txtPvLength.Text;
            table2.Cell(4, 1).Range.Text = "Number of Pv. panel"; table2.Cell(4, 2).Range.Text = numPvPanel.ToString();
            table2.Cell(5, 1).Range.Text = "Azimuth angle(deg)"; table2.Cell(5, 2).Range.Text = pvAz.AzimutAngle.ToString();
            table2.Cell(6, 1).Range.Text = "Tilt angle (deg)"; table2.Cell(6, 2).Range.Text = pvTilt.tiltAngle.ToString();
            table2.Cell(7, 1).Range.Text = "System specification";
            table2.Cell(8, 1).Range.Text = lblPvSpec1.Text; table2.Cell(8, 2).Range.Text = txtSystem_size.Text;
            table2.Cell(9, 1).Range.Text = lblPvSpec2.Text; table2.Cell(9, 2).Range.Text = txtDerate.Text;
            table2.Cell(10, 1).Range.Text = lblPvSpec3.Text; table2.Cell(10, 2).Range.Text = cmbTrack_mode.Text;
            table2.Cell(11, 1).Range.Text = lblPvSpec4.Text; table2.Cell(11, 2).Range.Text = txtTcell.Text;
            table2.Cell(12, 1).Range.Text = lblPvSpec5.Text; table2.Cell(12, 2).Range.Text = txtPoa.Text;
            table2.Cell(13, 1).Range.Text = "Weather data";
            table2.Cell(14, 1).Range.Text = lblPvSpec5.Text; table2.Cell(14, 2).Range.Text = txtBeam.Text;
            table2.Cell(15, 1).Range.Text = lblPvSpec5.Text; table2.Cell(15, 2).Range.Text = txtDiffuse.Text;
            table2.Cell(16, 1).Range.Text = lblPvSpec5.Text; table2.Cell(16, 2).Range.Text = txtTemp.Text;
            table2.Cell(17, 1).Range.Text = lblPvSpec5.Text; table2.Cell(17, 2).Range.Text = txtWspd.Text;
            table2.Cell(18, 1).Range.Text = lblPvSpec5.Text; table2.Cell(18, 2).Range.Text = txtSnow.Text;
            #endregion

            #region "Solar distribution table"
            textParagraph(doc, "Arial", 11, true, "");
            textParagraph(doc, "Arial", 11, true, "Table 3 Solar radiation distribution");
            // Insert table
            var pTable = doc.Paragraphs.Add();
            pTable.Format.SpaceAfter = 10f;
            int numTab3Row = grdSolarRose.RowCount;
            int numTab3Col = grdSolarRose.ColumnCount;
            if (numTab3Row > 0 & numTab3Col > 0)
            {
                var table3 = doc.Tables.Add(pTable.Range, numTab3Row, numTab3Col, WdDefaultTableBehavior.wdWord9TableBehavior);
                for (var r = 0; r < table3.Rows.Count; r++)
                    for (var c = 1; c <= table3.Columns.Count; c++)
                    {
                        if (r == 0)
                        {
                            table3.Cell(r + 1, c).Range.Text = grdSolarRose.Columns[c - 1].HeaderText;
                        }
                        else
                        {
                            table3.Cell(r + 1, c).Range.Text = grdSolarRose.Rows[r - 1].Cells[c - 1].Value.ToString();
                        }
                    }
            }

            #endregion "Solar distribution table"

            doc.Words.Last.InsertBreak(Microsoft.Office.Interop.Word.WdBreakType.wdPageBreak);
            textParagraph(doc, "Arial", 11, true, "\n\n\n\n\n\n\n\n\n\n\n\n\n");
            textParagraph(doc, "Arial", 18, true, "Appendix");
            doc.Words.Last.InsertBreak(Microsoft.Office.Interop.Word.WdBreakType.wdPageBreak);

            #region "Daily data"
            textParagraph(doc, "Arial", 11, true, "Table Appendix 1 Sun path calculation result (daily result at noon)");
            // Insert table
            var pTable4 = doc.Paragraphs.Add();
            pTable.Format.SpaceAfter = 10f;
            int numTab4Row = grdYearResult.RowCount;
            int numTab4Col = 4;
            if (numTab4Row > 0 & numTab3Col > 0)
            {
                var table4 = doc.Tables.Add(pTable.Range, numTab4Row, numTab4Col, WdDefaultTableBehavior.wdWord9TableBehavior);
                table4.Range.Font.Size = 8;
                pvProgressbar.Maximum = table4.Rows.Count;
                pvProgressbar.Visible = true;

                for (var r = 0; r < table4.Rows.Count; r++)
                {
                    pvProgressbar.Value = r;
                    pvProgressbar.Refresh();
                    if (r == 0)
                    {
                        table4.Cell(1, 1).Range.Text = "Date";
                        table4.Cell(1, 2).Range.Text = grdYearResult.Columns[8].HeaderText;
                        table4.Cell(1, 3).Range.Text = grdYearResult.Columns[9].HeaderText;
                        table4.Cell(1, 4).Range.Text = "Ac production (KW/day)";
                        // table4.Cell(1, 5).Range.Text = grdYearResult.Columns[7].HeaderText;
                    }
                    else
                    {
                        if (grdYearResult.Rows[r - 1].Cells[0].Value != null) table4.Cell(r + 1, 1).Range.Text = grdYearResult.Rows[r - 1].Cells[0].Value.ToString();
                        if (grdYearResult.Rows[r - 1].Cells[8].Value != null) table4.Cell(r + 1, 2).Range.Text = grdYearResult.Rows[r - 1].Cells[8].Value.ToString();
                        if (grdYearResult.Rows[r - 1].Cells[9].Value != null) table4.Cell(r + 1, 3).Range.Text = grdYearResult.Rows[r - 1].Cells[9].Value.ToString();
                        table4.Cell(r + 1, 4).Range.Text = Math.Round((dailyAcStore[r - 1] / 1000), 3).ToString();
                        //if (grdYearResult.Rows[r - 1].Cells[7].Value != null) table4.Cell(r + 1, 5).Range.Text = grdYearResult.Rows[r - 1].Cells[7].Value.ToString();
                    }
                }
                pvProgressbar.Visible = false;
            }


            #endregion
            #region "Picture"
            // Insert picture
            var pPicture = doc.Paragraphs.Add();
            pPicture.Format.SpaceAfter = 10f;
            //---------------------------------------------------------
            System.Drawing.Rectangle re = new System.Drawing.Rectangle(0, 0, pvMap.Width, pvMap.Height);
            Bitmap b = new Bitmap(pvMap.Width, pvMap.Height);
            Graphics g = Graphics.FromImage(b);
            pvMap.MapFrame.Print(g, re);
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var absolute_path = System.IO.Path.GetTempFileName().Replace(".tmp", ".bmp");// pvTmpDir + "\\Temp\\snapshot\\map-" + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".bmp";
            b.Save(absolute_path);
            //doc.InlineShapes.AddPicture(fSnapshotPath + fSnapshotNanme, Range: pPicture.Range);
            //MessageBox.Show(path );
            doc.InlineShapes.AddPicture(absolute_path, Range: pPicture.Range);

            var graph1_path = System.IO.Path.GetTempFileName().Replace(".tmp", ".bmp"); //pvTmpDir + "\\Temp\\snapshot\\graph1-" + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".bmp";
            var graph2_path = System.IO.Path.GetTempFileName().Replace(".tmp", ".bmp"); //pvTmpDir + "\\Temp\\snapshot\\graph2-" + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".bmp";
            var graph3_path = System.IO.Path.GetTempFileName().Replace(".tmp", ".bmp"); //pvTmpDir + "\\Temp\\snapshot\\graph3-" + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".bmp";
            zGraphAz2EleAng.GetImage().Save(graph1_path);
            zGraphAz2Day.GetImage().Save(graph2_path);
            zGraphEleAng2Day.GetImage().Save(graph3_path);
            doc.InlineShapes.AddPicture(graph1_path, Range: pPicture.Range);
            doc.InlineShapes.AddPicture(graph2_path, Range: pPicture.Range);
            doc.InlineShapes.AddPicture(graph3_path, Range: pPicture.Range);

            #endregion "picture"


            System.Threading.Thread.Sleep(5000);  //Wait for 5 more seconds
            MessageBox.Show("Peport complete");
            //Save the file, use default values except for filename.
            try
            {
                object saveChanges = Missing.Value;
                object optional = Missing.Value;  //Take default values.
                wordApp.Quit();//(ref saveChanges, ref optional, ref optional);
            }
            catch { }
        }

        void textParagraph(_Document doc, string font, int size, bool bold, string text)
        {
            var Paragraphs1 = doc.Paragraphs.Add();
            Paragraphs1.Range.Font.Bold = Convert.ToInt16(bold);
            Paragraphs1.Range.Font.Size = size;
            Paragraphs1.Range.Font.Name = font;
            //Paragraphs1.Format.SpaceAfter = 10f;
            Paragraphs1.Range.Text = text;
            Paragraphs1.Range.InsertParagraphAfter();
        }

        void textParagraph(_Document doc, string font, int size, string text)
        {
            var Paragraphs1 = doc.Paragraphs.Add();
            Paragraphs1.Range.Font.Bold = Convert.ToInt16(false);
            Paragraphs1.Range.Font.Size = size;
            Paragraphs1.Range.Font.Name = font;
            //Paragraphs1.Format.SpaceAfter = 10f;
            Paragraphs1.Range.Text = text;
            Paragraphs1.Range.InsertParagraphAfter();
        }

        private void cmdPole_in_Area_Click(object sender, EventArgs e)
        {
            if (pvTmpDir == "")
            {
                FolderBrowserDialog folderSel = new FolderBrowserDialog();
                folderSel.Description = "Please select temporary directory location :";
                folderSel.ShowDialog();
                pvTmpDir = folderSel.SelectedPath;
            }
            if (pvTmpDir != "")
            {

                CreateGridPole();
                GrdPole = true;
                MessageBox.Show("Pv. ploe created complete (Total Panel: " + numPvPanel.ToString() + ")");
            }
        }

        private void pvPanelGridAz_Paint(object sender, PaintEventArgs e)
        {
            if (GrdPole == true) CreateGridPole();
        }

        private void CreateGridPole()
        {
            if (pvTmpDir == "")
            {
                FolderBrowserDialog folderSel = new FolderBrowserDialog();
                folderSel.Description = "Please select temporary directory location :";
                folderSel.ShowDialog();
                pvTmpDir = folderSel.SelectedPath;
            }
            if (pvTmpDir != "")
            {


                numPvPanel = 0;
                IMapRasterLayer demLyr;
                Raster dem4Pv = new Raster();
                double poleH = 1; //Default pole height = 1 m.
                double z0 = 0;
                try { poleH = Convert.ToDouble(txtPoleHeight.Text); }
                catch
                {
                    MessageBox.Show("Pole height value error");
                    txtPoleHeight.Text = "1";
                    poleH = 1;
                }

                int iDemLyr = getLayerHdl(cmbDem.Text);
                if (iDemLyr != -1 & chkDEM.Checked == true)
                {
                    demLyr = pvMap.Layers[iDemLyr] as IMapRasterLayer;
                    if (demLyr == null)
                    {
                        MessageBox.Show("Error: Dem data is not correct");
                        return;
                    }
                    int mRow = demLyr.Bounds.NumRows;
                    int mCol = demLyr.Bounds.NumColumns;
                    dem4Pv = (Raster)demLyr.DataSet;
                    Coordinate ptReference = new Coordinate(Convert.ToDouble(txtUtmE.Text), Convert.ToDouble(txtUtmN.Text));
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
                int alingmentLyr = getLayerHdl(cmbSolarFarmArea.Text);
                if (alingmentLyr == -1)
                {
                    MessageBox.Show("Site layer incorrcet");
                    return;
                }

                if (alingmentLyr != -1)
                {
                    IMapFeatureLayer siteFeLyr = pvMap.Layers[alingmentLyr] as IMapFeatureLayer;
                    IMapFeatureLayer fSet;
                    try
                    {
                        fSet = pvMap.Layers[alingmentLyr] as IMapPolygonLayer;
                    }
                    catch
                    {
                        MessageBox.Show("Site layer incorrcet");
                        return;
                    }
                    if (fSet == null)
                    {
                        MessageBox.Show("Site layer incorrcet");
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

                    DrawLineCross(c.X, c.Y, 10, 1, Color.Red, pvMap);
                    double dx = Convert.ToDouble(txtGridSpacingX.Text);
                    double dy = Convert.ToDouble(txtGridSpacingY.Text);
                    double ang = pvPanelPoleGridCtl1.RotationAngle + 90;
                    double size = 0.5;
                    pvMap.MapFrame.DrawingLayers.Clear();
                    //
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
                            double xx = c.X + kGeoFunc.Rx(x, y, ang);
                            double yy = c.Y + kGeoFunc.Ry(x, y, ang);

                            if (PointInPolygon(site, xx, yy) == true)
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
                                numPvPanel++;
                                //------------------------------------------------------
                                ifea.DataRow.BeginEdit();
                                ifea.DataRow["x"] = xx;
                                ifea.DataRow["y"] = yy;
                                ifea.DataRow["w"] = Convert.ToDouble(txtPvWidth.Text);
                                ifea.DataRow["h"] = Convert.ToDouble(txtPvLength.Text);
                                ifea.DataRow["Azimuth"] = pvAz.AzimutAngle;
                                ifea.DataRow["Ele_Angle"] = pvTilt.tiltAngle;
                                if (iDemLyr != -1 & chkDEM.Checked == true)
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
                                    if (radioAboveAssumeDatum.Checked == true)
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
                    fs.Name = "pv. Array pole";
                    fs.SaveAs(pvTmpDir + "\\Temp\\pvArrayPole.shp", true);
                    removeDupplicateLyr(fs.Name);
                    MapPointLayer drawing;
                    drawing = new MapPointLayer(fs);
                    drawing.Symbolizer = new PointSymbolizer(Color.Magenta, DotSpatial.Symbology.PointShape.Triangle, 3);
                    pvMap.Layers.Add(drawing);
                    loadLayerList();
                    pvMap.MapFrame.DrawingLayers.Clear();
                    cmbPolePosition.Text = fs.Name;
                    verify[5] = true;
                    lblNoOfPole.Visible = true;
                    lblNoOfPole.Text = "Total Panel: " + numPvPanel.ToString();
                    /*
                                    drawing = new MapLineLayer(fs);
                                    drawing.Symbolizer = new LineSymbolizer(Color.Magenta, 1);
                                    pvMap.MapFrame.DrawingLayers.Add(drawing);
                                    // Request a redraw
                                    pvMap.MapFrame.Invalidate();
                                    grpBLineInfo.Visible = false;
                                    loadLayerList();
                    */
                    updateArea();
                }
            }
        }

        private void CreateGridPoleByGrdRotate(object sender, PaintEventArgs e)
        {
            CreateGridPole();
        }

        private void optPvWatt_WeatherFile_CheckedChanged(object sender, EventArgs e)
        {
            //groupPvWattSystemSpec.Enabled = false;
            //-------------------------------------
            txtBeam.Enabled = false;
            txtDiffuse.Enabled = false;
            txtTemp.Enabled = false;
            txtWspd.Enabled = false;
            txtSnow.Enabled = false;
            lblWeather1.Enabled = false;
            lblWeather2.Enabled = false;
            lblWeather3.Enabled = false;
            lblWeather4.Enabled = false;
            lblWeather5.Enabled = false;
            //-------------------------------------
            //
            lblState.Enabled = true;
            lblCity.Enabled = true;
            lblWeatherFile.Enabled = true;
            txtTM2.Enabled = true;
            cmbState.Enabled = true;
            cmbCity.Enabled = true;
            label11.Enabled = true;
            txtElev.Enabled = true;
            lblWeatherFile.Enabled = true;
            cmdLoadWeatherFile.Enabled = true;
            //-------------------------------------
            lblWeatherDetail1.Text = "Calculate by weather data:";
            if (UserDefined == false)
            {
                txtTM2.Clear();
                for (int i = 0; i < nSta; i++)
                {
                    if (wSta[i].State == cmbState.Text & wSta[i].City == cmbCity.Text)
                    {
                        txtTM2.Text = System.IO.Path.GetFullPath(pvDir + "\\WeatherSta\\tm2\\" + wSta[i].FileName);
                        txtElev.Text = wSta[i].Elev.ToString();
                        lblWeatherDetail2.Text = "Station Code: " + wSta[i].CODE;
                        lblWeatherDetail3.Text = "Location: " + wSta[i].LAT + "N " + wSta[i].LONG + "E";
                        lblWeatherDetail4.Text = "Elevation:" + wSta[i].Elev.ToString();
                        lblWeatherDetail5.Text = "Address: " + wSta[i].City + " " + wSta[i].State;
                    }
                }
            }
            else //: User drfined
            {
                lblWeatherDetail2.Text = "Station Code: User drfined";
                lblWeatherDetail3.Text = "Location: User drfined";
                lblWeatherDetail4.Text = "Elevation: User drfined";
                lblWeatherDetail5.Text = "Address: User drfined";
            }
            lblWeatherDetail6.Visible = false;
            //-------------------------------------
        }

        private void optPvWattFunc_CheckedChanged(object sender, EventArgs e)
        {
            //groupPvWattSystemSpec.Enabled = true;
            //-------------------------------------
            txtBeam.Enabled = true;
            txtDiffuse.Enabled = true;
            txtTemp.Enabled = true;
            txtWspd.Enabled = true;
            txtSnow.Enabled = true;
            lblWeather1.Enabled = true;
            lblWeather2.Enabled = true;
            lblWeather3.Enabled = true;
            lblWeather4.Enabled = true;
            lblWeather5.Enabled = true;
            //-------------------------------------
            lblState.Enabled = false;
            lblCity.Enabled = false;
            lblWeatherFile.Enabled = false;
            txtTM2.Enabled = false;
            cmbState.Enabled = false;
            cmbCity.Enabled = false;
            label11.Enabled = false;
            txtElev.Enabled = false;
            lblWeatherFile.Enabled = false;
            cmdLoadWeatherFile.Enabled = false;
            //-------------------------------------
            lblWeatherDetail1.Text = "Calculate by PVWatts Function:";
            lblWeatherDetail6.Visible = true;
            lblWeatherDetail2.Text = lblWeather1.Text + " " + txtBeam.Text;
            lblWeatherDetail3.Text = lblWeather2.Text + " " + txtDiffuse.Text;
            lblWeatherDetail4.Text = lblWeather3.Text + " " + txtTemp.Text;
            lblWeatherDetail5.Text = lblWeather4.Text + " " + txtWspd.Text;
            lblWeatherDetail6.Text = lblWeather5.Text + " " + txtSnow.Text;
            //-------------------------------------
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
                    lblWeatherDetail2.Text = "Station Code: " + wSta[i].CODE;
                    lblWeatherDetail3.Text = "Location: " + wSta[i].LAT + "N " + wSta[i].LONG + "E";
                    lblWeatherDetail4.Text = "Elevation:" + wSta[i].Elev.ToString();
                    lblWeatherDetail5.Text = "Address: " + wSta[i].City + " " + wSta[i].State;
                }
            }
        }

        private void cmdLoadWeatherFile_Click(object sender, EventArgs e)
        {
            optSingleWeatherSta.Checked = true; 
            txtTM2.Clear();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "NREL Weather data file (*.tm2)|*.tm2";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtTM2.Text = openFileDialog1.FileName;
                UserDefined = true;
                lblWeatherDetail2.Text = "Station Code: User drfined";
                lblWeatherDetail3.Text = "Location: User drfined";
                lblWeatherDetail4.Text = "Elevation: User drfined";
                lblWeatherDetail5.Text = "Address: User drfined";
                cmbState.Text = "User defined";
                cmbCity.Text = "User defined";
                txtElev.Text = "----";
            }
        }

        private void cmdPlotWeatherSta_Click(object sender, EventArgs e)
        {
            /*
            SaveFileDialog saveFileDlg = new SaveFileDialog();

            saveFileDlg.Filter = "csv files (*.csv)|*.csv";
            saveFileDlg.FilterIndex = 1;
            saveFileDlg.RestoreDirectory = true;
            if (saveFileDlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(saveFileDlg.FileName);


                    IFeatureSet pvfs;
                    pvfs = new FeatureSet(FeatureType.Polygon);
                    int cityLyr = getLayerHdl(cmbAlignmentLyr.Text);
                    //-----------------------------------------------------
                    IMapFeatureLayer cityFe = pvMap.Layers[cityLyr] as IMapFeatureLayer;
                    //MessageBox.Show("Number of pole = " + poleFe.DataSet.NumRows());
                    int nShp = cityFe.DataSet.NumRows() - 1;
                    for (int i = 0; i < cityFe.DataSet.NumRows(); i++)
                    {
                        IFeature fs = cityFe.DataSet.GetFeature(i);
                        double x1 = fs.Coordinates[0].X;
                        double y1 = fs.Coordinates[0].Y;
                        double[] mapCoordinate = new double[] { x1, y1 };
                        Reproject.ReprojectPoints(mapCoordinate, new double[] { 0 }, pvMap.Projection, KnownCoordinateSystems.Geographic.World.WGS1984, 0, 1);

                        string cityname = fs.DataRow["city"].ToString();
                        string statename = fs.DataRow["state"].ToString();
                        sw.WriteLine(statename + "," + cityname + "," + x1.ToString() + "," + y1.ToString() + "," + mapCoordinate[0].ToString() + "," + mapCoordinate[1].ToString());
                    }
                    sw.Close();
                }
                catch
                {
                    MessageBox.Show("The process cannot assess the file '" + saveFileDlg.FileName + "' because it is being used by aother process");
                    return;
                }
            }
            return;
            */
            IFeatureSet fs;
            fs = new FeatureSet(FeatureType.Point);
            //---------------------------------------------------------
            fs.DataTable.Columns.Add(new DataColumn("Code", typeof(string)));
            fs.DataTable.Columns.Add(new DataColumn("state", typeof(string)));
            fs.DataTable.Columns.Add(new DataColumn("city", typeof(string)));
            fs.DataTable.Columns.Add(new DataColumn("LAT", typeof(double)));
            fs.DataTable.Columns.Add(new DataColumn("LNG", typeof(double)));
            fs.DataTable.Columns.Add(new DataColumn("Elev", typeof(double)));
            fs.DataTable.Columns.Add(new DataColumn("FileName", typeof(string)));
            pvMap.MapFrame.DrawingLayers.Clear();
            for (int ii = 0; ii < nSta; ii++)
            {
                double lat = wSta[ii].LAT2;
                double lng = wSta[ii].LONG2;
                double x = 0;
                double y = 0;
                Pen MyPen = new Pen(Color.Black);
                //-----------------------------------------------------------------------
                double[] mapCoordinate = new double[] { lng, lat };
                //Reproject.ReprojectPoints(latlong, new double[] { 0 }, pvMap.Projection, KnownCoordinateSystems.Geographic.World.WGS1984, 0, 1);
                //Reproject.ReprojectPoints(mapCoordinate, new double[] { 0 }, KnownCoordinateSystems.Geographic.World.WGS1984, pvMap.Projection, 0, 1);
                Reproject.ReprojectPoints(mapCoordinate, new double[] { 0 }, KnownCoordinateSystems.Geographic.World.WGS1984, pvMap.Projection, 0, 1);
                //txtUtmE.Text = mapCoordinate[0].ToString();
                //txtUtmN.Text = mapCoordinate[1].ToString();
                //-----------------------------------------------------------------------
                //MessageBox.Show(c.X + "," + c.Y);
                double r = 50000;
                kDrawCircle(mapCoordinate[0], mapCoordinate[1], r, 360, pvMap, Color.Magenta);
                /*

                 * 
                 * 
                 * //------------------------------------------
                Coordinate wStation = new Coordinate(mapCoordinate[0], mapCoordinate[1]);
                IPoint wStationFe = new DotSpatial.Topology.Point(wStation);
                IFeature ifea = fs.AddFeature(wStationFe);
                //------------------------------------------------------
                ifea.DataRow.BeginEdit();
                ifea.DataRow["Code"] = wSta[ii].CODE.ToString();
                ifea.DataRow["state"] = wSta[ii].State;
                ifea.DataRow["city"] = wSta[ii].City ;
                ifea.DataRow["LAT"] = wSta[ii].LAT;
                ifea.DataRow["LNG"] = wSta[ii].LONG;
                ifea.DataRow["Elev"] = wSta[ii].Elev;
                ifea.DataRow["FileName"] = wSta[ii].FileName;
                //
                ifea.DataRow.EndEdit();
                //------------------------------------------------------                       
                //fs.Features.Add(poleFe);
                */
            }
            /*
            fs.Projection = pvMap.Projection;
            fs.Name = "weather_station";
            //fs.SaveAs(pvDir + "\\Temp\\weatherSta.shp", true);
            pvMap.Layers.Add(fs);
            //pvMap.MapFrame.DrawingLayers.Clear();
            MapPointLayer rangeRingAxis;
            rangeRingAxis = new MapPointLayer(fs);
            pvMap.MapFrame.DrawingLayers.Add(rangeRingAxis);
            pvMap.MapFrame.Invalidate();
             */
        }

        private void cmdLoadWeatherSta_Click(object sender, EventArgs e)
        {
            removeDupplicateLyr("weather_Sta"); 
            IFeatureSet fea = FeatureSet.Open(pvDir + "\\WeatherSta\\weather_Sta.shp"); // = ((IFeatureLayer)pvMap.GetLayers().ToArray()[2]).DataSet;
            pvMap.Layers.Add(fea);  
        }

        private void cmdShowIdwSta_Click(object sender, EventArgs e)
        {
            pvVerify();
            if (verify[0] == false)
            {
                MessageBox.Show("Please assign a reference location:");
                return;
            }           
            //-----------------------------------------------------------------------
            Coordinate c = new Coordinate(Convert.ToDouble(txtUtmE.Text), Convert.ToDouble(txtUtmN.Text));
            pvMap.MapFrame.DrawingLayers.Clear();
            Double r = 0.25;
            int nIDW = Convert.ToInt16(txtNIdwSta.Text);
            kDrawCircle(c.X, c.Y, r, 360, pvMap, Color.Magenta);
            for (int ii = 0; ii < nIDW; ii++)
            {
                double lat = wSta[wStaSel[ii]].LAT2;
                double lng = wSta[wStaSel[ii]].LONG2;
                double x = 0;
                double y = 0;
                double[] mapCoordinate = new double[] { lng, lat };
                Reproject.ReprojectPoints(mapCoordinate, new double[] { 0 }, KnownCoordinateSystems.Geographic.World.WGS1984, pvMap.Projection, 0, 1);
                kDrawCircle(mapCoordinate[0], mapCoordinate[1], 10000, 360, pvMap, Color.Magenta);
                DrawLine(mapCoordinate[0], mapCoordinate[1], c.X, c.Y, 2, Color.Magenta);
            }
            kDrawCircle(c.X, c.Y, 20000, 360, pvMap, Color.Magenta);

        }

        void updateArea()
        {
            try 
            {
                double panelW = Convert.ToDouble(txtPvWidth.Text);
                double panelH = Convert.ToDouble(txtPvLength.Text);
                double tArea = panelW * panelH * numPvPanel;
                lblTotalArea.Text = tArea.ToString() + "  (m2)";
            }
            catch { }
        }

        private void txtPvLength_TextChanged(object sender, EventArgs e)
        {
            updateArea();
        }

        private void txtPvWidth_TextChanged(object sender, EventArgs e)
        {
            updateArea();
        }

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
                myCurve = myPane.AddCurve(legend[i],ls[i], Color.FromArgb((int)az, 255 - (int)az, 255), (ZedGraph.SymbolType)jj);
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

        private void cmdOptimization_Click(object sender, EventArgs e)
        {
           
            //---------------------------------------------------------------------------
            TabGOptimize.SelectedIndex = 0;
            UpdateProgressBar.Visible = true;
            List<string> ls1Str = new List<string>();
            List<PointPairList> ls1 = new List<PointPairList>();
            UpdateProgressBar.Value = 0;
            UpdateProgressBar.Maximum = 9 * 38+1;
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
            ls1Str.Add("Tilt = " + tiltAngle.ToString() + " Deg.");
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
            ls1Str.Add("Azimuth = " + azAngle.ToString()+" Deg." );
            ls1.Add(list2);
            OptimizeGraphPlot(zedGOpti1, ls1, ls1Str, "Energy vs System Angle", "Energy (kWh)", "Angle (Deg.)",180,45,15); 
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
                ls2Str.Add(tilt.ToString() + "Deg.");
                ls2.Add(list);
            }
            OptimizeGraphPlot(zedGOpti2, ls2, ls2Str, "Energy vs Azimuth angle @Tilts angle", "Energy (kWh)", "Azimuth angle (Deg.)",180,45,15);
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
                ls3Str.Add(az.ToString() + "Deg.");
                ls3.Add(list);
            }
            OptimizeGraphPlot(zedGOpti3, ls3, ls3Str, "Energy vs Tilt angle @Azimuth angle", "Energy (kWh)", "Tilt angle (Deg.)",90,30,15);
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
                double[] y = MonthlyEnergyProduction(numPvPanel, txtTM2.Text, (double) tilt, azAngle);
                for (int m = 0; m < 12; m++)
                {
                    monthlyEnergy[m, tilt] = y[m];
                }
                UpdateProgressBar.Value = kk;
                kk++;
            }
            //
            int[,] maxTilt = new int[30,12];
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
                            maxTilt[odr, m] = maxTilt[odr-1, m];
                        }
                        maxTilt[0, m] = tilt;
                        maxEnergy[m] = maxE;
                    }
                }
            }

            for (int l = 0; l <30; l+=5)
            {
                PointPairList list = new PointPairList();
                for (int m = 0; m < 12; m++)
                {
                    double x = m+1;
                    double y = maxTilt[l,m];
                    list.Add(x, y);
                }
                if (l == 0) ls4Str.Add("1st");
                if (l == 1) ls4Str.Add("2nd");
                if (l == 2) ls4Str.Add("3rd");
                if (l >2) ls4Str.Add((l+1).ToString()+"th");
                ls4.Add(list);
            }
            OptimizeGraphPlot(zedGOpti4, ls4, ls4Str, "Mounthly vs Tilt angle (Maximum energy)", "Tilt angle (Deg.)", "Time (Month)", 13, 3, 1);
            GraphPane myPane = zedGOpti4.GraphPane;
            myPane.Y2Axis.Title.Text = "Energy (kWh)";
            // Make up some data points based on the Sine function
            PointPairList list4 = new PointPairList();
            for (int i = 0; i < 12; i++)
            {
                double x = (double)i+1;
                double y = maxEnergy[i] ;
                list4.Add(x, y);
            }
            // Generate a blue curve with circle symbols, and "Beta" in the legend
            //LineItem myCurve = myPane.AddCurve("Energy", list2, Color.Blue, ZedGraph.SymbolType.Circle);
            BarItem myCurve = myPane.AddBar("Energy",list4,Color.Red);  
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
            haveOptimizeGraph = true;
        }

        private void cmdClearGraphic_Click(object sender, EventArgs e)
        {
            pvMap.MapFrame.DrawingLayers.Clear();
            pvMap.MapFrame.Invalidate();
        }

        private void tabPage10_Click(object sender, EventArgs e)
        {

        }

        private void picTree_Click(object sender, EventArgs e)
        {

        }

        private void cmbBldg_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pvPanelPoleGridCtl1_Load_1(object sender, EventArgs e)
        {

        }

        private void pvTilt_Load(object sender, EventArgs e)
        {

        }


    }
  }