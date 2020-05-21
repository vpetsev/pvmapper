using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Symbology;
using DotSpatial.Topology;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

using System.Security.Permissions;
using Microsoft.Win32;
using Microsoft.VisualBasic;
using System.Windows.Forms;

namespace PVDESKTOP
{
    class PvDesktopUtilityFunction
    {
        public Coordinate getPerpend(Coordinate pt, Coordinate pp1, Coordinate pp2, out double Lenght)
        {
            Coordinate p1 = new Coordinate(pp1.X, pp1.Y);
            Coordinate p2 = new Coordinate(pp2.X, pp2.Y);
            Coordinate closest;
            double L = Distance(pt, p2);

            double x1 = (p1.X - p2.X) * L + p1.X;
            double y1 = (p1.Y - p2.Y) * L + p1.Y;
            p1.X = x1;
            p1.Y = y1;
            double x2 = (p2.X - p1.X) * L + p2.X;
            double y2 = (p2.Y - p1.Y) * L + p2.Y;
            p2.X = x2;
            p2.Y = y2;

            double dx = p2.X - p1.X;
            double dy = p2.Y - p1.Y;

            if ((dx == 0) && (dy == 0))
            {
                // It's a point not a line segment.
                closest = p1;
                dx = pt.X - p1.X;
                dy = pt.Y - p1.Y;
                Lenght = Math.Sqrt(dx * dx + dy * dy);
                return closest;
            }

            // Calculate the t that minimizes the distance.
            double t = ((pt.X - p1.X) * dx + (pt.Y - p1.Y) * dy) / (dx * dx + dy * dy);

            // See if this represents one of the segment's
            // end points or a point in the middle.
            if (t < 0)
            {
                closest = new Coordinate(p1.X, p1.Y);
                dx = pt.X - p1.X;
                dy = pt.Y - p1.Y;
            }
            else if (t > 1)
            {
                closest = new Coordinate(p2.X, p2.Y);
                dx = pt.X - p2.X;
                dy = pt.Y - p2.Y;
            }
            else
            {
                closest = new Coordinate(p1.X + t * dx, p1.Y + t * dy);
                dx = pt.X - closest.X;
                dy = pt.Y - closest.Y;
            }
            Lenght = Math.Sqrt(dx * dx + dy * dy);
            return closest;

        }
		
        public bool NummericTextBoxCheck(TextBox txtBox, string txtName, double Default, bool onlyPositiveValue =true, string WarningDeatal="")
        {
            if (IsNumeric(txtBox.Text) == true)
            {
                if (onlyPositiveValue == true & Convert.ToDouble(txtBox.Text) >= 0)
                {
                    return true;
                }
                else 
                {
                    MessageBox.Show(txtName + " must be positive value " + WarningDeatal);
                    if (Default != null) txtBox.Text = Default.ToString();
                    return true;
                }
            }
            else
            {
                MessageBox.Show(txtName + " is not a numeric value " + WarningDeatal);
                if (Default != null) txtBox.Text = Default.ToString();
            }
            return false; 
        }

        public bool IsNumeric(string Text)
        {
            float output;
            return float.TryParse(Text, out output);
        }

        public bool IsOdd(int value)
        {
            return value % 2 != 0;
        }

        public int getLayerHdl(String LyrName, Map pvMap)
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

        public void removeDupplicateLyr(string LayerName, Map pvMap)
        {
            for (int i = pvMap.Layers.Count - 1; i >= 0; i--)
            {
                int remLyr = getLayerHdl(LayerName,pvMap);
                if (remLyr != -1)
                {
                    pvMap.Layers.RemoveAt(remLyr);
                }
            }
        }
        public bool checkLyr(string LayerName, Map pvMap)
        {
            for (int i = pvMap.Layers.Count - 1; i >= 0; i--)
            {
                int remLyr = getLayerHdl(LayerName, pvMap);
                if (remLyr != -1)
                {
                    return true;
                }
            }
            return false;
        }
        public Coordinate Rotate(Coordinate xy, double Angle)
        {
            double ang = Math.PI * Angle / 180;
            double x = xy.X * Math.Cos(ang) - xy.Y * Math.Sin(ang);
            double y = xy.X * Math.Sin(ang) + xy.Y * Math.Cos(ang);
            Coordinate rXY = new Coordinate(x, y);
            return rXY;
        }

        public Coordinate RotateTreeShadow(Coordinate xy, double Az)
        {
                if (Az >= 0 & Az <= 180)
                {
                    double ang = 180 - Az;
                    double rotAng = Math.PI * ang / 180;
                    double x = xy.X * Math.Cos(rotAng) - xy.Y * Math.Sin(rotAng);
                    double y = xy.X * Math.Sin(rotAng) + xy.Y * Math.Cos(rotAng);
                    Coordinate rXY = new Coordinate(x, y);
                    return rXY;
                }

                else 
                {
                    double ang = 360 - Az;
                    double rotAng = Math.PI * ang / 180;
                    double x = xy.X * Math.Cos(rotAng) - xy.Y * Math.Sin(rotAng);
                    double y = xy.X * Math.Sin(rotAng) + xy.Y * Math.Cos(rotAng);
                    Coordinate rXY = new Coordinate(x, y);
                    return rXY;
                }                        
        }

        public Coordinate Rotate(double x, double y, double Angle)
        {
            double ang = Math.PI * Angle / 180;
            double xx = x * Math.Cos(ang) - y * Math.Sin(ang);
            double yy = x * Math.Sin(ang) + y * Math.Cos(ang);
            Coordinate rXY = new Coordinate(xx, yy);
            return rXY;
        }

        public int getLayerID(string LayerName, Map pvMap)
        {
            int iLayer = -1;
            int nLayer = pvMap.Layers.Count;
            if (nLayer >= 1)
            {
                for (int i = 0; i < nLayer; i++)
                {
                    if (LayerName == pvMap.Layers[i].LegendText) { iLayer = i; }
                }
            }
            return iLayer;
        }

        public Coordinate circleCoord(double x1, double y1, double x2, double y2, double radius) //function finds coordinates for intersection of line and circle
        {
            double ratio = radius / (Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2))); //calculates the ratio of the radius of circle to distance to weather station
            double changex = ratio * (x2 - x1); //distance in x direction to circle
            double changey = ratio * (y2 - y1); //distance in y direction to circle
            double newx = (x1 + changex);
            double newy = (y1 + changey);
            Coordinate newcoord = new Coordinate(newx, newy);
            return newcoord;
        }

        public void kDrawCircle(Double x0, Double y0, Double r, Int16 numVertex, IMap MapCanvas, Color color)
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

        public void kDrawCircle(Coordinate pt, Double r, Int16 numVertex, IMap MapCanvas, Color color)
        {
            Double dAng = 360 / numVertex;
            Coordinate[] cr = new Coordinate[numVertex + 1]; //x-axis
            for (int iAng = 0; iAng <= numVertex; iAng++)
            {
                Double ang1 = iAng * dAng;
                Double x1 = Math.Sin(ang1 * Math.PI / 180) * r + pt.X;
                Double y1 = Math.Cos(ang1 * Math.PI / 180) * r + pt.Y;
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
      
        public void kDrawArc(Double x0, Double y0, Double r, double AzStrat,double AzFinish,  Int16 numVertex, IMap MapCanvas, Color color)
        {
            if (AzFinish < AzStrat) 
            {
                AzFinish += 360; 
            }
            Double dAng = (AzFinish-AzStrat) / numVertex;
            Coordinate[] cr = new Coordinate[numVertex + 1]; //x-axis
            
            for (int iAng = 0; iAng <= numVertex; iAng++)
            {
                Double ang1 = AzStrat + iAng * dAng;
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

        public void DrawLineCross(Double x, Double y, double size, Double width, System.Drawing.Color col, IMap MapCanvas)
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

        public void DrawLine(Double x1, Double y1, Double x2, Double y2, Double width, System.Drawing.Color col, IMap MapCanvas)
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


            //MapCanvas.MapFrame.DrawingLayers.Add(rangeRingAxis);

            // Request a redraw
            MapCanvas.MapFrame.Invalidate();
            // MapCanvas.MapFrame.Invalidate();
        }

        public void DrawLine(Coordinate p1,Coordinate p2, Double width, System.Drawing.Color col, IMap MapCanvas)
        {
            Coordinate[] L = new Coordinate[2]; //x-axis

            L[0] = p1;
            L[1] = p2;

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


            //MapCanvas.MapFrame.DrawingLayers.Add(rangeRingAxis);

            // Request a redraw
            MapCanvas.MapFrame.Invalidate();
            // MapCanvas.MapFrame.Invalidate();
        }

        public Coordinate PointOnTheLine(Coordinate pt1, Coordinate pt2, double dL)
        {
            Coordinate xy;
            double L = Math.Sqrt(Math.Pow(pt2.X - pt1.X, 2) + Math.Pow(pt2.Y - pt1.Y, 2));
            if (dL > L)
            {
                return null;
            }
            if (pt1.X == pt2.X)
            {
                xy = new Coordinate(pt1.X, pt1.Y + dL);
                return xy;
            }
            double ratio = dL / L;
            double x = pt1.X + (pt2.X - pt1.X) * ratio;
            double y = pt1.Y + (pt2.Y - pt1.Y) * ratio;
            xy = new Coordinate(x, y);
            return xy;
        }

        public Coordinate PointOnTheLine(double x1, double y1, double x2, double y2, double dL)
        {
            Coordinate xy;
            double L = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
            if (dL > L)
            {
                return null;
            }
            if (x1 == x2)
            {
                xy = new Coordinate(x1, y1 + dL);
                return xy;
            }
            double ratio = dL / L;
            double x = x1 + (x2 - x1) * ratio;
            double y = y1 + (y2 - y1) * ratio;
            xy = new Coordinate(x, y);
            return xy;
        }

        public void ClearGraphicMap(Map pvMap)
        {
            pvMap.MapFrame.DrawingLayers.Clear();
        }

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

        public bool PointInPolygon(Coordinate [] points, double X, double Y)
        {
            int max_point = points.Length - 1;
            double total_angle = GetAngle(points[max_point].X, points[max_point].Y, X, Y, points[0].X, points[0].Y);
            for (int i = 0; i < max_point - 1; i++)
            {
                total_angle += GetAngle(points[i].X, points[i].Y, X, Y, points[i + 1].X, points[i + 1].Y);
            }
            return Math.Abs(total_angle) > 0.000001;
        }

        public double GetAngle(double Ax, double Ay, double Bx, double By, double Cx, double Cy)
        {
            double dot_product;
            double cross_product;
            dot_product = DotProduct(Ax, Ay, Bx, By, Cx, Cy);
            cross_product = CrossProductLength(Ax, Ay, Bx, By, Cx, Cy);
            return Math.Atan2(cross_product, dot_product);
        }

        public double DotProduct(double Ax, double Ay, double Bx, double By, double Cx, double Cy)
        {
            double BAx = Ax - Bx;
            double BAy = Ay - By;
            double BCx = Cx - Bx;
            double BCy = Cy - By;
            return BAx * BCx + BAy * BCy;
        }

        public double CrossProductLength(double Ax, double Ay, double Bx, double By, double Cx, double Cy)
        {
            double BAx = Ax - Bx;
            double BAy = Ay - By;
            double BCx = Cx - Bx;
            double BCy = Cy - By;
            return BAx * BCy - BAy * BCx;
        }

        //Compute the dot product AB . AC
        private double DotProduct(Coordinate pointA, Coordinate pointB, Coordinate pointC)
        {
            double[] AB = new double[2];
            double[] BC = new double[2];
            AB[0] = pointB.X - pointA.X;
            AB[1] = pointB.Y - pointA.Y;
            BC[0] = pointC.X - pointB.X;
            BC[1] = pointC.Y - pointB.Y;
            double dot = AB[0] * BC[0] + AB[1] * BC[1];

            return dot;
        }

        //Compute the cross product AB x AC
        private double CrossProduct(Coordinate pointA, Coordinate pointB, Coordinate pointC)
        {
            double[] AB = new double[2];
            double[] AC = new double[2];
            AB[0] = pointB.X - pointA.X;
            AB[1] = pointB.Y - pointA.Y;
            AC[0] = pointC.X - pointA.X;
            AC[1] = pointC.Y - pointA.Y;
            double cross = AB[0] * AC[1] - AB[1] * AC[0];

            return cross;
        }

        //Compute the distance from A to B
        public double Distance(Coordinate pointA, Coordinate pointB)
        {
            double d1 = pointA.X - pointB.X;
            double d2 = pointA.Y - pointB.Y;

            return Math.Sqrt(d1 * d1 + d2 * d2);
        }

        //Compute the distance from AB to C
        //if isSegment is true, AB is a segment, not a line.
        double LineToPointDistance2D(Coordinate pointA, Coordinate pointB, Coordinate pointC,
            bool isSegment)
        {
            double dist = CrossProduct(pointA, pointB, pointC) / Distance(pointA, pointB);
            if (isSegment)
            {
                double dot1 = DotProduct(pointA, pointB, pointC);
                if (dot1 > 0)
                    return Distance(pointB, pointC);

                double dot2 = DotProduct(pointB, pointA, pointC);
                if (dot2 > 0)
                    return Distance(pointA, pointC);
            }
            return Math.Abs(dist);
        }

        public double FindDistanceToSegment(Coordinate pt, Coordinate pp1, Coordinate pp2, out Coordinate closest)
        {
            Coordinate p1 = new Coordinate(pp1.X,pp1.Y);
            Coordinate p2 = new Coordinate(pp2.X, pp2.Y);

            double L = Distance(pt, p2);

            double x1 = (p1.X - p2.X) * L + p1.X;
            double y1 = (p1.Y - p2.Y) * L + p1.Y;
            p1.X = x1;
            p1.Y = y1;
            double x2 = (p2.X - p1.X) * L + p2.X;
            double y2 = (p2.Y - p1.Y) * L + p2.Y;
            p2.X = x2;
            p2.Y = y2;

            double dx = p2.X - p1.X;
            double dy = p2.Y - p1.Y;

            if ((dx == 0) && (dy == 0))
            {
                // It's a point not a line segment.
                closest = p1;
                dx = pt.X - p1.X;
                dy = pt.Y - p1.Y;
                return Math.Sqrt(dx * dx + dy * dy);
            }

            // Calculate the t that minimizes the distance.
            double t = ((pt.X - p1.X) * dx + (pt.Y - p1.Y) * dy) / (dx * dx + dy * dy);

            // See if this represents one of the segment's
            // end points or a point in the middle.
            if (t < 0)
            {
                closest = new Coordinate(p1.X, p1.Y);
                dx = pt.X - p1.X;
                dy = pt.Y - p1.Y;
            }
            else if (t > 1)
            {
                closest = new Coordinate(p2.X, p2.Y);
                dx = pt.X - p2.X;
                dy = pt.Y - p2.Y;
            }
            else
            {
                closest = new Coordinate(p1.X + t * dx, p1.Y + t * dy);
                dx = pt.X - closest.X;
                dy = pt.Y - closest.Y;
            }

            return Math.Sqrt(dx * dx + dy * dy);
        }

        public double getAzimuth(double x1, double y1, double x2, double y2)
        {
            double Az = 0;
            double dx;
            double dy;

            dx = (x2 - x1);
            dy = (y2 - y1);

            if (dx == 0)
            {
                if (y2 < y1)
                { Az = 180; }
                else
                { Az = 0; }
            }
            else
            {
                if (dy == 0)
                {
                    if (x2 < x1)
                    { Az = 270; }
                    else
                    { Az = 90; }
                }

                double m = Convert.ToDouble(dx / dy);
                Az = Math.Atan(m) * 180 / Math.PI;
                if (y2 > y1 & x2 < x1)
                {

                    Az += 360;
                }
                if (y2 < y1)
                {
                    Az += 180;
                }
            }
            return Az;
        }

        public double getAzimuth(Coordinate p1,Coordinate p2)
        {
            double x1 = p1.X;
            double y1 = p1.Y;
            double x2 = p2.X; 
            double y2 = p2.Y;
            double Az = 0;
            double dx;
            double dy;

            dx = (x2 - x1);
            dy = (y2 - y1);

            if (dx == 0)
            {
                if (y2 < y1)
                { Az = 180; }
                else
                { Az = 0; }
            }
            else
            {
                if (dy == 0)
                {
                    if (x2 < x1)
                    { Az = 270; }
                    else
                    { Az = 90; }
                }

                double m = Convert.ToDouble(dx / dy);
                Az = Math.Atan(m) * 180 / Math.PI;
                if (y2 > y1 & x2 < x1)
                {

                    Az += 360;
                }
                if (y2 < y1)
                {
                    Az += 180;
                }
            }
            return Az;
        }

        public Coordinate midLine(Coordinate p1, Coordinate p2)
        {
            double midX = (p2.X - p1.X) / 2 + p1.X;
            double midY = (p2.Y - p1.Y) / 2 + p1.Y;
            Coordinate c=new Coordinate(midX,midY);
            return c;
        }

        public void DrawPolygonShape(IFeature fe, System.Drawing.Color FillColor, System.Drawing.Color OutlineColor, double OutlineWidth, Map MapCanvas)
        {
            FeatureSet fs = new FeatureSet(FeatureType.Polygon);
            fs.Features.Add(fe);

            MapPolygonLayer rangeRingAxis;
            rangeRingAxis = new MapPolygonLayer(fs);
            rangeRingAxis.Symbolizer = new PolygonSymbolizer(FillColor, OutlineColor, OutlineWidth);
            MapCanvas.MapFrame.DrawingLayers.Add(rangeRingAxis);
            MapCanvas.MapFrame.Invalidate();
            // MapCanvas.MapFrame.Invalidate();       
        }

        public double Azm2Qurdrant(double Azm)
        {
            double AngQurdrant = 0;
            if (Azm >= 0 & Azm < 90) AngQurdrant = 90 - Azm;
            if (Azm >= 90 & Azm < 180) AngQurdrant = 360 - (Azm - 90);
            if (Azm >= 180 & Azm < 270) AngQurdrant = 270 - (Azm - 180);
            if (Azm >= 270 & Azm < 360) AngQurdrant = 90 + (360 - Azm);
            return AngQurdrant;
        }

        #region InternetCheck
        
        public bool internetAccess()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private void myPingCompletedCallback(object sender, PingCompletedEventArgs e)
        {
            if (e.Cancelled)
                return;

            if (e.Error != null)
                return;

            if (e.Reply.Status == IPStatus.Success)
            {
                //ok connected to internet, do something...
            }
        }

        #endregion InternetCheck

        #region "Registry Key function"

        public void SavePVDesktopSetting(string Key, string Val)
        {
            var rkS = Registry.CurrentUser.OpenSubKey("Software", true);
            if (rkS == null) throw new System.NotImplementedException();

            Microsoft.Win32.RegistryKey rk;
            rkS.CreateSubKey("pvdesktop");
            rk = rkS.OpenSubKey("pvdesktop", true);
            //save setting to registry

            rk.SetValue(Key, Val);

            rk.Close();
            rk = null;

            //MsgBox("All setting was saved in registry.")
        }

        public void CheckRegistry()
        {
            try
            {
                var rkS = Registry.CurrentUser.OpenSubKey("Software", true);
                if (rkS == null) throw new System.NotImplementedException();

                Microsoft.Win32.RegistryKey rk = rkS.OpenSubKey("pvdesktop", true);
                if (rk == null && rk.ValueCount <= 0)
                {
                    CreateDefaultSetting();
                    rk.Close();
                    rk = null;
                }
            }
            catch
            {
                Exception ex;
            }

        }

        public void CreateDefaultSetting()
        {
            var rkS = Registry.CurrentUser.OpenSubKey("Software", true);
            if (rkS == null) throw new System.NotImplementedException();
            Microsoft.Win32.RegistryKey rk;
            rkS.CreateSubKey("pvdesktop");
            rk = rkS.OpenSubKey("pvdesktop", true);

            rk.SetValue("ProjectPath", "");

            rk.Close();
            rk = null;
        }

        public string getProjectPathFromRegistry()
        {
            try
            {
                CheckRegistry();
                var rkS = Registry.CurrentUser.OpenSubKey("Software", true);
                if (rkS == null) throw new System.NotImplementedException();

                Microsoft.Win32.RegistryKey rk = rkS.OpenSubKey("pvdesktop", true);
                if (rk.GetValue("ProjectPath").ToString().Length > 0)
                {
                    //txtProjectPath.Text = rk.GetValue("ProjectPath").ToString();
                    return rk.GetValue("ProjectPath").ToString();
                }                  
            }
            catch {return ""; }
            return "";
        }

        #endregion//"Registry Key function"

        public void SetPointColor(FeatureSet fs, Color pointColor, Map pvmap)
        {
            IFeatureLayer fl = pvmap.Layers.Add(fs);
            fl.SetShapeSymbolizer(2, new PointSymbolizer(pointColor,0,3));
        }

    }
}
