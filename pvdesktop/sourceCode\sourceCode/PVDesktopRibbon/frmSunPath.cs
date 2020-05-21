using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PVDESKTOP
{
    public partial class frmSunPath : Form
    {
        PvDesktopProject project;

        internal PvDesktopProject Project
        {
            get { return project; }
            set { project = value; }
        }

        public frmSunPath()
        {
            InitializeComponent();
        }

        private void cmdRoseModel_Click(object sender, EventArgs e)
        {
            CreateSunRoseTable();
        }

        private void CreateSunRoseTable()
        {
            if (Project.Path == "")
            {
                FolderBrowserDialog folderSel = new FolderBrowserDialog();
                folderSel.Description = "Please select temporary directory location :";
                folderSel.ShowDialog();
                Project.Path = folderSel.SelectedPath;
            }
            if (Project.Path != "")
            {
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

                double Latitude = Project.Latiude;
                double Longitude = Project.Longtitude;
                double UtmN = Project.UtmN;
                double UtmE = Project.UtmE;

                short TimeZone = Project.TimeZome;
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

                grdSolarRose.DataSource = table;
            }
        }

        private void cmdSolar_YearlyCal_Click(object sender, EventArgs e)
        {
            YearlyCal();
        }
        void YearlyCal()
        {
            #region  input data
            //--------------------------------------------------------------
            double Latitude = Project.Latiude ;
            double Longitude = Project.Longtitude;
            Int16 TimeZone_ = Project.TimeZome;
            //--------------------------------------------------------------
            #endregion
            //
            /*
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
            Az2DayPanel.XAxis.Title.Text = "Day of Year";
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
            */
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
                    /*
                    // draw a sin curve Az vs Ele Angle  
                    listAz2EleAng.Add(ySolar.SolarAzimuthAngle, ySolar.SolarElevationAngle);
                    // draw a sin curve Az vs day  
                    listAz2Day.Add(i, ySolar.SolarAzimuthAngle);
                    listSunriseAz2Day.Add(i, ySunrise.SolarAzimuthAngle);
                    listSunsetAz2Day.Add(i, ySunset.SolarAzimuthAngle);
                    // draw a sin curve Ele Angle vs day
                    listEle2Day.Add(i, ySolar.SolarElevationAngle);
                     */ 
                }
            }
            // set lineitem to list of points 
            /*
            listAz2EleAng_ = Az2ElePanel.AddCurve("Az vs Ele. Angle (deg)", listAz2EleAng, Color.Black, ZedGraph.SymbolType.Circle);
            //
            listAz2Day_ = Az2DayPanel.AddCurve("Noon Az.", listAz2Day, Color.Black, ZedGraph.SymbolType.Circle);
            listAz2Day_ = Az2DayPanel.AddCurve("Sunrise Az.", listSunriseAz2Day, Color.Magenta, ZedGraph.SymbolType.None);
            listAz2Day_ = Az2DayPanel.AddCurve("Sunset Az.", listSunsetAz2Day, Color.Blue, ZedGraph.SymbolType.None);
            //
            listEle2Day_ = Ele2DayPanel.AddCurve("Noon Sun Ele. Angle.", listEle2Day, Color.Red, ZedGraph.SymbolType.Circle);
            // ---------------------   
            // draw graph    
            zGraphAz2EleAng.AxisChange();
            zGraphAz2Day.AxisChange();
            zGraphEleAng2Day.AxisChange();
            //--
            zGraphAz2EleAng.Refresh();
            zGraphAz2Day.Refresh();
            zGraphEleAng2Day.Refresh();
             */
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DairyCalculation();
        }

        private void DairyCalculation()
        {
            //MessageBox.Show(dateTimePicker1.Value.ToString());
            #region  input data
            //--------------------------------------------------------------
            double Latitude = Project.Latiude;
            double Longitude = Project.Longtitude;
            Int16 TimeZone_ = Project.TimeZome;
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
            this.grdSolarDay.Rows[15].Cells[0].Value = "Equation of Time (min)";
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

        private void frmSunPath_Load(object sender, EventArgs e)
        {
            CreateSunRoseTable();
            DairyCalculation();
            YearlyCal();
        }

        private void cmdSaveAsCSV_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.DefaultExt = "csv";
            saveFile.ShowDialog(); 
            string CsvFpath = saveFile.FileName;
            if (CsvFpath.Length <= 0)
            {
                return;   
            }
            try
            {
                System.IO.StreamWriter csvFileWriter = new StreamWriter(CsvFpath, false);

                string columnHeaderText = "";

                int countColumn = grdSolarRose.ColumnCount - 1;

                if (countColumn >= 0)
                {
                    columnHeaderText = grdSolarRose.Columns[0].HeaderText;
                }

                for (int i = 1; i <= countColumn; i++)
                {
                    columnHeaderText = columnHeaderText + ',' + grdSolarRose.Columns[i].HeaderText;
                }


                csvFileWriter.WriteLine(columnHeaderText);

                foreach (DataGridViewRow dataRowObject in grdSolarRose.Rows)
                {
                    if (!dataRowObject.IsNewRow)
                    {
                        string dataFromGrid = "";

                        dataFromGrid = dataRowObject.Cells[0].Value.ToString();

                        for (int i = 1; i <= countColumn; i++)
                        {
                            dataFromGrid = dataFromGrid + ',' + dataRowObject.Cells[i].Value.ToString();
                        }
                        csvFileWriter.WriteLine(dataFromGrid);
                    }
                }


                csvFileWriter.Flush();
                csvFileWriter.Close();
            }
            catch (Exception exceptionObject)
            {
                MessageBox.Show(exceptionObject.ToString());
            }
        }


    }
}
