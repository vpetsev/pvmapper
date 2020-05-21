using DotSpatial.Data;
using DotSpatial.Topology;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PVDESKTOP
{
    class RoseChart
    {
        private FeatureSet _RoseFrature;
        public FeatureSet RoseFrature {  get{return _RoseFrature;}set{}}

        public RoseChart()
        {
        }

        public RoseChart(double Lat, double Lng, DataTable DT, Int16 noonHour,double RoseScale)
        {
            _RoseFrature = getRoseFeatureSet(Lng, Lat, DT, noonHour, RoseScale);
        }
                
        public static int iDir(double ang) 
        { 
            int ii=0;
            double dAng=22.5;  
            if (ang>360){ang = ang-360;}
            if (ang<360){ang = ang-360;}
            double aAng;
            for(aAng =0; aAng<=(360 - dAng); aAng += dAng)
            {
                ii++; 
                if( aAng <= ang & ang < aAng + dAng ){ return ii;}   
            }   
            return -1;    
        }

        public int iEleAng(double activeEle)
        {
            int ii =0;
            double dEleAng=10.0; // 0-90 degree
            for( double aEle = 0; aEle<70; aEle+=dEleAng)
            {
                ii++;
                if (aEle < activeEle & activeEle <= aEle + dEleAng)
                {
                    return ii;
                }
            }
            return 8;
        }

        public string AzName(double Angle)
        {
            if (Angle > -12.25 & Angle <= 12.25) { return "N"; }
            if (Angle > 347.75 & Angle <= 372.25) { return "N"; }
            if (Angle > 10.25 & Angle <= 34.75) { return "NNE"; }
            if (Angle > 32.75 & Angle <= 57.25) { return "NE"; }
            if (Angle > 55.25 & Angle <= 79.75) { return "ENE"; }
            if (Angle > 77.75 & Angle <= 102.25) { return "E"; }
            if (Angle > 100.25 & Angle <= 124.75) { return "ESE"; }
            if (Angle > 122.75 & Angle <= 147.25) { return "SE"; }
            if (Angle > 145.25 & Angle <= 169.75) { return "SSE"; }
            if (Angle > 167.75 & Angle <= 192.25) { return "S"; }
            if (Angle > 190.25 & Angle <= 214.75) { return "SSW"; }
            if (Angle > 212.75 & Angle <= 237.25) { return "SW"; }
            if (Angle > 235.25 & Angle <= 259.75) { return "WSW"; }
            if (Angle > 257.75 & Angle <= 282.25) { return "W"; }
            if (Angle > 280.25 & Angle <= 304.75) { return "WNW"; }
            if (Angle > 302.75 & Angle <= 327.25) { return "NW"; }
            if (Angle > 325.25 & Angle <= 349.75) { return "NNW"; }
            return null;        
        }

        public double AzName2RCPAng(string angName)
        {
            if(angName == "N" )     { return 0;}
            if(angName == "NNE" )   { return 22.5;}
            if(angName == "NE" )    { return 45;}
            if(angName == "ENE" )   { return 67.5;}
            if(angName == "E" )     { return 90;}
            if(angName == "ESE" )   { return 112.5;}
            if(angName == "SE" )    { return 135;}
            if(angName == "SSE" )   { return 157.5;}
            if(angName == "S" )     { return 180;}
            if(angName == "SSW" )   { return 202.5;}
            if(angName == "SW" )    { return 225;}
            if(angName == "WSW" )   { return 247.5;}
            if(angName == "W" )     { return 270;}
            if(angName == "WNW" )   { return 292.5;}
            if(angName == "NW" )    { return 315;}
            if (angName == "NNW")   { return 337.5;}
            return -1;
        }

        public int AZNameID(string AzName)
        {
            if( AzName == "N" ){ return 4;}
            if( AzName == "NNE" ){ return 3;}
            if( AzName == "NE" ){ return 2;}
            if( AzName == "ENE" ){ return 1;}
            if( AzName == "E" ){ return 0;}
            if( AzName == "ESE" ){ return 15;}
            if( AzName == "SE" ){ return 14;}
            if( AzName == "SSE" ){ return 13;}
            if( AzName == "S" ){ return 12;}
            if( AzName == "SSW" ){ return 11;}
            if( AzName == "SW" ){ return 10;}
            if( AzName == "WSW" ){ return 9;}
            if( AzName == "W" ){ return 8;}
            if( AzName == "WNW" ){ return 7;}
            if( AzName == "NW" ){ return 6;}
            if( AzName == "NNW" ){ return 5;}
            return -1;
        }

        public int AZNameID(double Angle)
        {
            if (Angle > 347.75 & Angle <= 372.25) { return 0; }
            if (Angle > -12.25 & Angle <= 12.25) { return 0; }
            //-----------------------------------------------------------
            if (Angle > 10.25 & Angle <= 34.75) { return 1; }
            if (Angle > 32.75 & Angle <= 57.25) { return 2; }
            if (Angle > 55.25 & Angle <= 79.75) { return 3; }
            if (Angle > 77.75 & Angle <= 102.25) { return 4; }
            if (Angle > 100.25 & Angle <= 124.75) { return 5; }
            if (Angle > 122.75 & Angle <= 147.25) { return 6; }
            if (Angle > 145.25 & Angle <= 169.75) { return 7; }
            if (Angle > 167.75 & Angle <= 192.25) { return 8; }
            if (Angle > 190.25 & Angle <= 214.75) { return 9; }
            if (Angle > 212.75 & Angle <= 237.25) { return 10; }
            if (Angle > 235.25 & Angle <= 259.75) { return 11; }
            if (Angle > 257.75 & Angle <= 282.25) { return 12; }
            if (Angle > 280.25 & Angle <= 304.75) { return 13; }
            if (Angle > 302.75 & Angle <= 327.25) { return 14; }
            if (Angle > 325.25 & Angle <= 349.75) { return 15; }
            return -1;
        }
        public FeatureSet getRoseFeatureSet(double x1, double y1, DataTable solarTab, Int16 noonHour, double RoseScale)
        {
            // RoseShp = Nothing
            Feature f = new Feature();
            FeatureSet fs = new FeatureSet(f.FeatureType);
            //---------------------------------------------------------
            fs.DataTable.Columns.Add(new DataColumn("Azimuth", typeof(string)));
            fs.DataTable.Columns.Add(new DataColumn("Ele_Angle", typeof(string)));
            fs.DataTable.Columns.Add(new DataColumn("Magnitude", typeof(Int16)));
            fs.DataTable.Columns.Add(new DataColumn("Angle", typeof(double)));
            //---------------------------------------------------------
            double dOut = 0;  //out
            double dIn = 100; //In
            int iDirection=0; 
            // Todo: Dim SumHr As Integer = grdWaveH.Item(9, 17).Value
            int i=0;
            int roseTyp = 0;
            string AzName="";
            int SumHr = noonHour;
            //
            foreach(DataRow row in solarTab.Rows)
            {
                dIn = 100;
                double iniP = dIn;
                int j=0;
                foreach(DataColumn column in solarTab.Columns)
                {
                    if(j==0)
                    {
                        AzName = row[column].ToString();
                        iDirection = AZNameID(AzName);
                    }
                    else
                    {
                        if (AzName.ToUpper() != "Sum".ToUpper() ) 
                        { 
                            // % of solar Magnitude
                            Int16 Magnitude = Convert.ToInt16(row[column]);// / (double)SumHr * 100;
                            if (Magnitude > 0 & j <= 8) 
                            {
                                double _Magnitude = (double)Magnitude / (double)SumHr * 100;
                                
                                if (roseTyp ==0)
                                {
                                    double r1 = dIn;
                                    double r2 = dIn + _Magnitude*100;
                                    Polygon poly = plotRose(x1, y1, iDirection, r1, r2, RoseScale);
                                    //Polygon poly = plotRose(x1, y1, iDirection, r1, r2, AzName, getStrMagnitude(j), RoseScale);
                                    dIn = r2;
                                    //Polygon poly = plotRose(x1, y1, iDirection, iniP, _Magnitude, AzName, getStrMagnitude(j), RoseScale);
                                    //iniP += _Magnitude;
                                    IFeature ifea = fs.AddFeature(poly);
                                    //------------------------------------------------------
                                    ifea.DataRow.BeginEdit();
                                    ifea.DataRow["Azimuth"] = AzName;
                                    ifea.DataRow["Ele_Angle"] = getStrMagnitude(j);
                                    ifea.DataRow["Magnitude"] = Magnitude;
                                    ifea.DataRow["Angle"] = AzName2RCPAng(AzName);
                                    //
                                    ifea.DataRow.EndEdit();
                                    //------------------------------------------------------
                                }
                                if (roseTyp == 1)
                                {
                                    double r1 = dIn + (20 + 0) * (j)*RoseScale ;
                                    double r2 = dIn + (20 + 0) * (j + 1)*RoseScale ;
                                    Polygon poly = plotRose(x1, y1, iDirection, r1, r2, RoseScale );
                                    
                                    IFeature ifea = fs.AddFeature(poly);
                                    //------------------------------------------------------
                                    ifea.DataRow.BeginEdit();
                                    ifea.DataRow["Azimuth"] = AzName;
                                    ifea.DataRow["Ele_Angle"] = getStrMagnitude(j);
                                    ifea.DataRow["Magnitude"] = Magnitude;
                                    ifea.DataRow["Angle"] = AzName2RCPAng(AzName);
                                    //
                                    ifea.DataRow.EndEdit();
                                    //------------------------------------------------------
                                }

                            }
                        }
                    }
                    //Console.WriteLine(row[column]);
                    //if(iniP > dOut){ dOut = iniP;}
                    j++;
                }
                i++;
            }
            return fs;
       }

        public string getStrMagnitude(int j)
        {
            if (j == 1) { return "<10"; }
            if (j == 2) { return "10-20"; }
            if (j == 3) { return "20-30"; }
            if (j == 4) { return "30-40"; }
            if (j == 5) { return "40-50"; }
            if (j == 6) { return "50-60"; }
            if (j == 7) { return "60-70"; }
            if (j == 8) { return ">70"; }
            return null;
        }

        private Polygon plotRose(double x1, double y1, int iDIR, double r1, double r2,double RoseScale)
        {
            Polygon[] poly = new Polygon[1];
            double SC = 10* RoseScale;



            double Ang1 = (iDIR) * 22.5 - 22.5 / 2;
            double Ang2 = (iDIR) * 22.5 + 22.5 / 2;

            double L1 = r1 * SC;
            double L2 = r2 * SC;

            string linePath1 = RosePetal(x1, y1, L1, Ang1, Ang2);
            string linePath2 = RosePetal(x1, y1, L2, Ang2, Ang1);
            string linePath = linePath1 + linePath2;

            string[] pts = linePath.Split('|');
            int npt = pts.Length;
            Coordinate[] coord = new Coordinate[npt];
            double x0 = 0; double y0 = 0;
            int ii = 0;
            foreach (string pt in pts)
            {
                string[] cord = pt.Split(',');
                int i = 0;
                double[] xy = new double[2];

                foreach (string strxy in cord)
                {
                    if (strxy.Length > 0)
                    {
                        xy[i] = Convert.ToDouble(strxy);
                        i++;
                    }
                }
                coord[ii] = new Coordinate(xy[0], xy[1]);
                if (ii == 0)
                {
                    x0 = xy[0];
                    y0 = xy[1];
                }
                ii++; //Next point
            }
            coord[ii - 1] = new Coordinate(x0, y0);
            poly[0] = new Polygon(coord);
            return poly[0];
        }

        private Polygon plotRose(double x1, double y1, int iDIR, double pInit, double pValue, string AzName, string eleAngName, double RoseScale)
        {
            Polygon[] poly = new Polygon[1];
            double SC =  RoseScale;

           

            double Ang1  = (iDIR) * 22.5 - 22.5 / 2;
            double Ang2  = (iDIR) * 22.5 + 22.5 / 2;
        
            double L1  = pInit;
            double Magnitude =(double)pValue;

            pValue = Magnitude * SC;
            double L2 = L1 + pValue;
        
            string linePath1 = RosePetal(x1, y1, L1, Ang1, Ang2);
            string linePath2 = RosePetal(x1, y1, L2, Ang2, Ang1);
            string linePath = linePath1 + linePath2;
                        
            string[] pts = linePath.Split('|');
            int npt = pts.Length;
            Coordinate[] coord = new Coordinate[npt];
            double x0=0; double y0=0;
            int ii = 0;
	        foreach (string pt in pts)
	        {
	            string[] cord = pt.Split(',');
                int i=0;
                double[] xy = new double[2];

                foreach (string strxy in cord)
                {
                    if (strxy.Length >0)
                    {                     
                        xy[i] = Convert.ToDouble(strxy);    
                        i++;
                    }
                }
                coord[ii] = new Coordinate(xy[0], xy[1]);
                if (ii == 0) {
                    x0 = xy[0];
                    y0 = xy[1];
                }
                ii++; //Next point
            }
            coord[ii-1] = new Coordinate(x0, y0);
            poly[0] = new Polygon(coord);
            return poly[0];
        }

        private string RosePetal(double x1, double y1, double r, double Ang1, double Ang2)
        {
            double xx1, yy1; 
            double dAng = 0.5;
            string listOfCord="";
            if(Ang1 < Ang2)
            {
                for(double iAng = Ang1; iAng<= Ang2; iAng+=dAng)
                {
                    double Ang = iAng;
                    xx1 = xCircle(Ang, x1, r);
                    yy1 = yCircle(Ang, y1, r);
                    listOfCord += xx1 + "," + yy1 + "|";
                }
            }
            else
            {
                for(double iAng = Ang1; iAng>=Ang2; iAng-=dAng)
                {
                    double Ang = iAng;
                    xx1 = xCircle(Ang, x1, r);
                    yy1 = yCircle(Ang, y1, r);
                    listOfCord += xx1 + "," + yy1 + "|";
                }
            }
            return listOfCord;
        }

        private double xCircle(double Ang , double x0, double  r)
        {
            double xc  = r * Math.Cos(Ang * Math.PI / 180) + x0;
            return xc;
        }
        private double yCircle(double Ang , double y0, double  r)
        {
            double yc  =  r * Math.Sin(Ang * Math.PI / 180) + y0;
            return yc;
        }

        public DataTable SolarTable()
        {
            DataTable table = new DataTable("SolarTab");
            table.Columns.Add(new DataColumn("DIR", typeof(string)));
            table.Columns.Add(new DataColumn("<10", typeof(int)));
            table.Columns.Add(new DataColumn("20", typeof(int)));
            table.Columns.Add(new DataColumn("30", typeof(int)));
            table.Columns.Add(new DataColumn("40", typeof(int)));
            table.Columns.Add(new DataColumn("50", typeof(int)));
            table.Columns.Add(new DataColumn("50", typeof(int)));
            table.Columns.Add(new DataColumn("60", typeof(int)));
            table.Columns.Add(new DataColumn(">60", typeof(int)));
            //------------------------------------------------------------
            table.Rows.Add("N", 1, 2, 3, 4, 5, 6, 7, 8);
            table.Rows.Add("NNE", 1, 2, 3, 4, 5, 6, 7, 8);
            table.Rows.Add("NE", 1, 2, 3, 4, 5, 6, 7, 8);
            table.Rows.Add("E", 1, 2, 3, 4, 5, 6, 7, 8);
            table.Rows.Add("EES", 1, 2, 3, 4, 5, 6, 7, 8);
            table.Rows.Add("ES", 1, 2, 3, 4, 5, 6, 7, 8);
            table.Rows.Add("S", 1, 2, 3, 4, 5, 6, 7, 8);
            table.Rows.Add("SSW", 1, 2, 3, 4, 5, 6, 7, 8);
            table.Rows.Add("SW", 1, 2, 3, 4, 5, 6, 7, 8);
            table.Rows.Add("W", 1, 2, 3, 4, 5, 6, 7, 8);
            table.Rows.Add("WWN", 1, 2, 3, 4, 5, 6, 7, 8);
            table.Rows.Add("WN", 1, 2, 3, 4, 5, 6, 7, 8);

            return table;
        }
        //-----------------------------------------------------------------

    }
}
