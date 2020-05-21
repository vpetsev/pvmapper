using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVDESKTOP
{
    class Shadow
    {
        
#region internal variable
        private double _x = 0;
        private double _y = 0;
        private double _z = 0;
        private double _r = 0;
        private double _l = 0;
        private DotSpatial.Topology.Coordinate _shadowPt;
#endregion
#region external variable
        public DotSpatial.Topology.Coordinate shadowPt { get { return _shadowPt; } set { } }
#endregion
        public Shadow(double Az, double eleAng, DotSpatial.Topology.Coordinate pt)
        {
            _r = ShadowR(eleAng,pt.Z);
            _x = pt.X + ShadowE(_r, eleAng, Az);
            _y = pt.Y + ShadowN(_r, eleAng, Az);
            _z = ShadowZ(_r,eleAng);
            _shadowPt = new DotSpatial.Topology.Coordinate(_x, _y, _z);
        }
        internal static double ShadowL(double EleAng, double H)
        {
            double ang = EleAng * Math.PI /180;
            return H / Math.Tan(ang);
        }
        internal static double ShadowR(double EleAng, double H)
        {
            double ang = EleAng * Math.PI / 180;
            return H / Math.Sin(ang);
        }
        internal static double ShadowN(double r, double EleAng, double AzAng)
        {
            double Elevation = Math.PI * EleAng / 180;
            double azimuth =  Math.PI * AzAng / 180;
            return -r * Math.Cos(Elevation) * Math.Cos(azimuth);
        }

        internal static double ShadowE(double r, double EleAng, double AzAng)
        {
            double Elevation = Math.PI * EleAng / 180;
            double azimuth =  Math.PI * AzAng / 180;
            return-r * Math.Cos(Elevation) * Math.Sin(azimuth);
        }

        internal static double ShadowZ(double r, double EleAng)
        {
            double Elevation = Math.PI * EleAng / 180;
            return  r * Math.Sin(Elevation);
        }
    }
    
}
