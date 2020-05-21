using DotSpatial.Topology;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PVDESKTOP
{
    public class roofLine
    {
        Coordinate p1;
        Coordinate p2;
        double elev;
 
        public double Elevation
        {
            get { return elev; }
            set { elev = value; }
        }
        public Coordinate Point2
        {
            get { return p2; }
            set { p2 = value; }
        }

        public Coordinate Point1
        {
            get { return p1; }
            set { p1 = value; }
        }
        
        public double getDX()
        {
            return Point2.X - Point1.X;
        }
    
        public double getDY()
        {
            return Point2.Y - Point1.Y;
        }
        
        public Coordinate getMidLine()
        {
            double x = getDX()/2 + Point1.X;
            double y = getDY()/2 + Point1.Y;
            return new Coordinate(x, y); 
        }

        public int getQuadrant()
        {
            if (getDX() > 0 & getDY() > 0) { return 1; }
            if (getDX() > 0 & getDY() < 0) { return 4; }
            if (getDX() < 0 & getDY() > 0) { return 2; }
            if (getDX() < 0 & getDY() < 0) { return 3; }
            return -1;
        }
    }

   public class RoofPlane
    {
        private roofLine ridgeLine;
        private roofLine eaveLine;

        public roofLine RidgeLine
        {
            get { return ridgeLine; }
            set { ridgeLine = value; }
        }

        public roofLine EaveLine
        {
            get { return eaveLine; }
            set { eaveLine = value; }
        } 

        private Coordinate RidgePt1;
        private Coordinate RidgePt2;
        private double RidgeEle;
        private Coordinate EavePt1;
        private Coordinate EavePt2;
        private double EaveEle;
/*
        public RoofPlane(Coordinate RidgePt1, Coordinate RidgePt2, double RidgeEle, Coordinate EavePt1, Coordinate EavePt2, double EaveEle)
        {
            this.RidgePt1 = RidgePt1;
            this.RidgePt2 = RidgePt2;
            this.RidgeEle = RidgeEle;
            this.EavePt1 = EavePt1;
            this.EavePt2 = EavePt2;
            this.EaveEle = EaveEle;
            AddEaveLine(EavePt1, EavePt2, EaveEle);
            AddRidgeLine(RidgePt1, RidgePt2, RidgeEle);
        }
*/
        public RoofPlane(List<Coordinate> RidgeLine, double RidgeEle, List<Coordinate> EaveLine, double EaveEle)
        {
            if (EaveLine.Count >= 2 & RidgeLine.Count >= 2)
            {
                Coordinate p1 = RidgeLine[0];
                Coordinate p2 = RidgeLine[1];
                PvDesktopUtilityFunction util = new PvDesktopUtilityFunction();
                Coordinate[] site = new Coordinate[5];  
                //Darw edge
                double L1, L2;               
                L1 = util.Distance(RidgeLine[0], EaveLine[0]);
                L2 = util.Distance(RidgeLine[0], EaveLine[1]);
                if (L1 <= L2)
                {
                    site[0] = RidgeLine[0];
                    site[1] = RidgeLine[1];
                    site[2] = EaveLine[1];
                    site[3] = EaveLine[0];
                    site[4] = RidgeLine[0];
                    AddRidgeLine(RidgeLine[0], RidgeLine[1], RidgeEle);
                    AddEaveLine(EaveLine[0], EaveLine[1], EaveEle);
                }
                else
                {
                    site[0] = RidgeLine[0];
                    site[1] = RidgeLine[1];
                    site[2] = EaveLine[0];
                    site[3] = EaveLine[1];
                    site[4] = RidgeLine[0];
                    AddRidgeLine(RidgeLine[0], RidgeLine[1], RidgeEle);
                    AddEaveLine(EaveLine[1], EaveLine[0], EaveEle);
                } 
            }
        }

        public Coordinate getMidRoof()
        {
            Coordinate p1 = RidgeLine.getMidLine();
            Coordinate p2 = EaveLine.getMidLine();

            double dx = (p2.X - p1.X) / 2 + p1.X;
            double dy = (p2.Y - p1.Y) / 2 + p1.Y;
            return new Coordinate(dx, dy); 
        }

        public void AddRidgeLine(List<Coordinate> LineCoord, double ele)
        {
            RidgeLine = createRoofL(LineCoord[0], LineCoord[1], ele);
        }
  
        public RoofPlane()
        {
            
        }

        public roofLine createRoofL(Coordinate pt1,Coordinate pt2,double ele)
        {
            roofLine RL = new roofLine();
            RL.Point1 = pt1;
            RL.Point2 = pt2;
            RL.Elevation = ele;
            return RL;
        }

        public void AddEaveLine(Coordinate pt1, Coordinate pt2, double ele)
        {
            EaveLine = createRoofL(pt1, pt2, ele);
        }

        public void AddRidgeLine(Coordinate pt1, Coordinate pt2, double ele)
        {
            RidgeLine = createRoofL(pt1, pt2, ele);
        }        

        public double getAzimuth()
        {
            double az = 0;
            double dx = RidgeLine.getDX();
            double dy = RidgeLine.getDY();
            if (dx == 0)
            {
                if (RidgeLine.Point1.Y > RidgeLine.Point2.Y)
                {
                    az = 180;
                }
                else
                {
                    az = 0;
                }
            }
            else
            {
                az = Math.Atan(dy / dx) * 180 / Math.PI;
                int quardant = RidgeLine.getQuadrant();
                if (quardant == 1) { az = 90 - az; }
                if (quardant == 2) { az = 270 - az; }
                if (quardant == 3) { az = 270 - az; }
                if (quardant == 4) { az = 90 - az; }
            }
            return az;
        }

        internal void DrawRoofPland(DotSpatial.Controls.Map pvMap)
        {
            PvDesktopUtilityFunction util = new PvDesktopUtilityFunction();
            util.DrawLine(RidgeLine.Point1, RidgeLine.Point2, 1, Color.Red, pvMap);
            util.DrawLine(RidgeLine.Point2, EaveLine.Point2, 1, Color.Magenta, pvMap);
            util.DrawLine(EaveLine.Point2, EaveLine.Point1, 1, Color.Magenta, pvMap);
            util.DrawLine(EaveLine.Point1, RidgeLine.Point1, 1, Color.Magenta, pvMap);
        }

        public List<Coordinate> getPvPointOnRoofPlane(double SpacingX, double SpacingY, double ShiftX, double ShiftY, double roofAngle,double RefEle, bool swith =false )
        {
            List<Coordinate> mC = new List<Coordinate>();
            PvDesktopUtilityFunction util = new PvDesktopUtilityFunction();
            double ang =  0;
            if (ridgeLine.Point1.X != ridgeLine.Point2.X)
            {
                ang =  Math.Atan((ridgeLine.Point2.Y - ridgeLine.Point1.Y) / (ridgeLine.Point2.X - ridgeLine.Point1.X)) * 180 / Math.PI; 
            }
            Coordinate c = getMidRoof();
            Coordinate[] site = new Coordinate[5];
            //Darw edge
            double L1, L2;
            L1 = util.Distance(RidgeLine.Point1 , EaveLine.Point1 );
            L2 = util.Distance(RidgeLine.Point1, EaveLine.Point2);
            if (L1 <= L2)
            {
                site[0] = new Coordinate(RidgeLine.Point1);
                site[1] = new Coordinate(RidgeLine.Point2);
                site[2] = new Coordinate(EaveLine.Point2);
                site[3] = new Coordinate(EaveLine.Point1);
                site[4] = new Coordinate(RidgeLine.Point1);
            }
            else
            {
                site[0] = new Coordinate(RidgeLine.Point1);
                site[1] = new Coordinate(RidgeLine.Point2);
                site[2] = new Coordinate(EaveLine.Point1);
                site[3] = new Coordinate(EaveLine.Point2);
                site[4] = new Coordinate(RidgeLine.Point1);
            }
            for (int i = 0; i < 5; i++)
            {
                site[i].X -= c.X;
                site[i].Y  -= c.Y;
            }
            
            
            
            
            mC.Clear();
            int iSwitch = 0;
            double dSwitch=0;
           
            for (double y = -50; y <= 50; y += SpacingY)
            {
                for (double x = -50; x <= 50; x += SpacingX)
                {
                    if (swith == false & util.IsOdd(iSwitch))
                    { dSwitch = 0; }
                    else
                    { dSwitch = SpacingX / 2; }
                    double actualX = x + dSwitch;
                    double actualY = y * Math.Cos(roofAngle * Math.PI / 180);
                    double actualZ = RefEle + y * Math.Sin(roofAngle * Math.PI / 180);
                    Coordinate xy = util.Rotate(actualX + ShiftX, actualY + ShiftY, ang);
                    double xx = xy.X;
                    double yy = xy.Y;
                    //if (util.PointInPolygon(site, xx, yy) == true)
                    if (IsPointInPolygon(site, xy) == true)
                    {
                        mC.Add(new Coordinate(xx + c.X, yy + c.Y, actualZ));
                    }
                }
                iSwitch++;
            }
            return mC; 
        }

        private bool IsPointInPolygon(Coordinate [] polygon, Coordinate  point)
        {
            bool isInside = false;
            for (int i = 0, j = polygon.Length - 1; i < polygon.Length; j = i++)
            {
                if (((polygon[i].Y > point.Y) != (polygon[j].Y > point.Y)) &&
                (point.X < (polygon[j].X - polygon[i].X) * (point.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X))
                {
                    isInside = !isInside;
                }
            }
            return isInside;
        }

        int lyrRoofArray = -1;
        public int LyrRoofArray
        {
            get { return lyrRoofArray; }
            set { lyrRoofArray = value; }
        }

        string lyrRoofArrayName = "";
        public string LyrRoofArrayName
        {
            get { return lyrRoofArrayName; }
            set { lyrRoofArrayName = value; }
        }

        string roofXSpace = "1";
        public string RoofXSpace
        {
            get { return roofXSpace; }
            set { roofXSpace = value; }
        }

        string roofYSpace = "1";
        public string RoofYSpace
        {
            get { return roofYSpace; }
            set { roofYSpace = value; }
        }

        string roofPanelWidth = "1";
        public string RoofPanelWidth
        {
            get { return roofPanelWidth; }
            set { roofPanelWidth = value; }
        }

        string roofPanelHeight = "2";
        public string RoofPanelHeight
        {
            get { return roofPanelHeight; }
            set { roofPanelHeight = value; }
        }

        string roofAzimuth = "0";
        public string RoofAzimuth
        {
            get { return roofAzimuth; }
            set { roofAzimuth = value; }
        }

        string roofTilt = "0";
        public string RoofTilt
        {
            get { return roofTilt; }
            set { roofTilt = value; }
        }

        string roofName = "";
        public string RoofName
        {
            get { return roofName; }
            set { roofName = value; }
        }

        bool orthogonal = true;
        public bool Orthogonal
        {
            get { return orthogonal; }
            set { orthogonal = value; }
        }
    }
}
