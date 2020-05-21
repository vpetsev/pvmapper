using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace PvMapperPlugin
{
    public partial class frmDailySolarCal : Form
    {
        public frmDailySolarCal()
        {
            InitializeComponent();
        }

        private void frmDailySolarCal_Load(object sender, EventArgs e)
        {

        }

        private void cmdCal_Click(object sender, EventArgs e)
        {
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

            SolarCal solar = new SolarCal(this.dateTimePicker1.Value,TimePassMidnight,Latitude,Longitude,TimeZone_);

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

            //-----------------------------------------------------------------------------------------------
            /*
            double JulianDay_ = SolarCal.getJulianDay(this.dateTimePicker1.Value, TimeZone_, 0.1 / 24); ; 
            double JulianCentury_ = SolarCal.getJulianCentury(JulianDay_);
            double GeomMeanLongSun_ = SolarCal.getGeomMeanLongSun(JulianCentury_ ) ;
            double GeomMeanAnomSun_ = SolarCal.getGeomMeanAnomSun(JulianCentury_ ) ;
            double EccentEarthOrbit_ = SolarCal.getEccentEarthOrbit(JulianCentury_ ) ;
            double SunEqofCtr_ = SolarCal.getSunEqofCtr(JulianCentury_ , GeomMeanAnomSun_ ) ;
            double SunTrueLong_ = SolarCal.getSunTrueLong(GeomMeanLongSun_ , SunEqofCtr_ ) ;
            double SunTrueAnom_ = SolarCal.getSunTrueAnom(GeomMeanAnomSun_ , SunEqofCtr_ ) ;
            double SunRadVector_ = SolarCal.getSunRadVector(EccentEarthOrbit_ , SunTrueAnom_ ) ;
            double SunAppLong_ = SolarCal.getSunAppLong(JulianCentury_ , SunTrueLong_ ) ;
            double MeanObliqEcliptic_ = SolarCal.getMeanObliqEcliptic(JulianCentury_ ) ;
            double ObliqCorr_ = SolarCal.getObliqCorr(JulianCentury_ , MeanObliqEcliptic_ ) ;
            double SunRtAscen_ = SolarCal.getSunRtAscen(SunAppLong_ , ObliqCorr_ ) ;
            double SunDeclin_ = SolarCal.getSunDeclin(SunAppLong_ , ObliqCorr_ ) ;
            double varY_ = SolarCal.getvarY(ObliqCorr_) ;
            double EqOfTime_min_ = SolarCal.getEqOfTime_min(GeomMeanLongSun_, GeomMeanAnomSun_, EccentEarthOrbit_, varY_);
            double HASunrise_ = SolarCal.getHASunrise(Latitude, SunDeclin_);
            double SolarNoon_ = SolarCal.getSolarNoon(Longitude, TimeZone_, EqOfTime_min_);
            double SunriseTime_ = SolarCal.getSunriseTime(HASunrise_ , SolarNoon_ );
            double SunsetTime_ = SolarCal.getSunsetTime(HASunrise_, SolarNoon_);
            double SunlightDuration_min_ = SolarCal.getSunlightDuration_min(HASunrise_ ) ;
            double TrueSolarTime_min_ = SolarCal.getTrueSolarTime_min(Longitude , TimeZone_, TimePassMidnight , EqOfTime_min_ ) ;
            double HourAngle_ = SolarCal.getHourAngle(TrueSolarTime_min_);
            double SolarZenithAngle_ = SolarCal.getSolarZenithAngle(Latitude, SunDeclin_, HourAngle_);
            double SolarElevationAngle_ = SolarCal.getSolarElevationAngle(SolarZenithAngle_ ) ;
            double ApproxAtmosphericRefraction_ = SolarCal.getApproxAtmosphericRefraction(SolarElevationAngle_ ) ;
            double SolarElevationCorrectedForAtmRefraction_ = SolarCal.getSolarElevationCorrectedForAtmRefraction(SolarElevationAngle_, ApproxAtmosphericRefraction_);
            double SolarAzimuthAngle_ = SolarCal.getSolarAzimuthAngle(Latitude , SunDeclin_ , HourAngle_ , SolarZenithAngle_) ;
            */

         }

        private void cmdYearCal_Click(object sender, EventArgs e)
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
            int year = Convert.ToInt32( this.dateTimePicker1.Value.ToString("yyyy"));
            
            this.grdYearResult.Columns[0].HeaderText = "Julian Day";
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
                for(int day=1;day<=month_day;day++)
                {
                    this.grdYearResult.Rows.Add(); 
                    SolarCal ySolar = new SolarCal(day, month, year,0.5, Latitude, Longitude, TimeZone_);
                    this.grdYearResult.Rows[i].Cells[0].Value = month + "/" + day + "/" + year;
                    this.grdYearResult.Rows[i].Cells[1].Value = ySolar.SunriseTime;
                    this.grdYearResult.Rows[i].Cells[2].Value = ySolar.SunsetTime;
                    this.grdYearResult.Rows[i].Cells[3].Value = ySolar.SunlightDuration_min;
                    this.grdYearResult.Rows[i].Cells[4].Value = ySolar.EqOfTime_min;
                    this.grdYearResult.Rows[i].Cells[5].Value = ySolar.SunDeclin;
                    this.grdYearResult.Rows[i].Cells[6].Value = ySolar.SolarElevationAngle;
                    this.grdYearResult.Rows[i].Cells[7].Value = ySolar.SolarAzimuthAngle;
                    //--------------------------------------------------------------------------
                    SolarCal ySunrise = new SolarCal(day, month, year, ySolar.SunriseTime, Latitude, Longitude, TimeZone_);
                    this.grdYearResult.Rows[i].Cells[8].Value = ySunrise.SolarAzimuthAngle;
                    SolarCal ySunset = new SolarCal(day, month, year, ySolar.SunsetTime, Latitude, Longitude, TimeZone_);
                    this.grdYearResult.Rows[i].Cells[9].Value = ySunset.SolarAzimuthAngle;
                    i++;
                    // draw a sin curve Az vs Ele Angle  
                    listAz2EleAng.Add(ySolar.SolarAzimuthAngle, ySolar.SolarElevationAngle);
                    // draw a sin curve Az vs day  
                    listAz2Day.Add(i,ySolar.SolarAzimuthAngle );
                    listSunriseAz2Day.Add(i, ySunrise.SolarAzimuthAngle);
                    listSunsetAz2Day.Add(i, ySunset.SolarAzimuthAngle);
                    // draw a sin curve Ele Angle vs day
                    listEle2Day.Add(i, ySolar.SolarElevationAngle);
                }
            }
            // set lineitem to list of points  
            listAz2EleAng_ = Az2ElePanel.AddCurve("Az vs Ele. Angle (deg.)", listAz2EleAng, Color.Black, SymbolType.Circle);
            //
            listAz2Day_ = Az2DayPanel.AddCurve ( "Noon Az." , listAz2Day, Color.Black, SymbolType.Circle);
            listAz2Day_ = Az2DayPanel.AddCurve("Sunrise Az.", listSunriseAz2Day, Color.Magenta, SymbolType.None);
            listAz2Day_ = Az2DayPanel.AddCurve("Sunset Az.", listSunsetAz2Day, Color.Blue, SymbolType.None);
            //
            listEle2Day_ = Ele2DayPanel.AddCurve("Noon sun ele. Angle.", listEle2Day, Color.Red, SymbolType.Circle);
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

        private void cmdTestGraph_Click(object sender, EventArgs e)
        {
            // pane used to draw your chart
            GraphPane myPane = new GraphPane(); 
            // poing pair lists
            PointPairList listPointsOne = new PointPairList();
            PointPairList listPointsTwo = new PointPairList();
            // line item
            LineItem myCurveOne;
            LineItem myCurveTwo;

            // set your pane    
            myPane = zGraphAz2EleAng.GraphPane;
            // set a title 
            myPane.Title.Text = "Solar Azimuth and Elevation Angle"; 
            // set X and Y axis titles
            myPane.XAxis.Title.Text = "Solar Azimuth (deg)";
            myPane.YAxis.Title.Text = "Elevation Angle (deg)";
            // ---- CURVE ONE ----    
            // draw a sin curve   
            for (int i = 0; i < 100; i++)   
            { 
                listPointsOne.Add(i, Math.Sin(i)); 
            }
            // set lineitem to list of points  
            myCurveOne = myPane.AddCurve(null, listPointsOne, Color.Black, SymbolType.Circle);    
            // ---------------------   
            // ---- CURVE TWO ---- 
            listPointsTwo.Add(10, 50);  
            listPointsTwo.Add(50, 50); 
            // set lineitem to list of points 
            myCurveTwo = myPane.AddCurve(null, listPointsTwo, Color.Blue, SymbolType.None);  
            myCurveTwo.Line.Width = 5;  
            // ---------------------     
            // draw    
            zGraphAz2EleAng.AxisChange();
            zGraphAz2EleAng.Refresh(); 
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

       
    }
}
