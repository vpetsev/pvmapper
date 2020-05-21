using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace PVDESKTOP
{
    public partial class frmObtimize : Form
    {
        ProjectFileInfo project;

        public ProjectFileInfo Project
        {
            get { return project; }
            set { project = value; }
        }

        public frmObtimize()
        {
            InitializeComponent();
        }

        private void cmdOptimization_Click(object sender, EventArgs e)
        {

        }
       
        private void cmdOptimization_Click_1(object sender, EventArgs e)
        {
            //---------------------------------------------------------------------------
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
            OptimizeGraphPlot(zedGOpti1, ls1, ls1Str, "Energy vs System Angle", "Energy (kWh)", "Angle (Deg)", 180, 45, 15);
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
            OptimizeGraphPlot(zedGOpti2, ls2, ls2Str, "Energy vs Azimuth Angle @Tilts Angle", "Energy (kWh)", "Azimuth Angle (Deg)", 180, 45, 15);
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
            OptimizeGraphPlot(zedGOpti3, ls3, ls3Str, "Energy vs Tilt Angle @Azimuth Angle", "Energy (kWh)", "Tilt Angle (Deg)", 90, 30, 15);
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
            haveOptimizeGraph = true;

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

    }
}
