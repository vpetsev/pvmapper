using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PVDESKTOP
{
    public class PvDesktopProject
    {
        string path;
        bool[] Checked = new bool[10];
        int numPvPanel;
        //latitude longitude------
        double latiude;
        double longtitude;
        double utmN;
        double utmE;
        //------------------------
        double pvWidth = 0;


        //------------------------
        Int16 timeZome;
        string weatherSta;
        //------------------------        
        int lyrAlignment = -1;
        string lyrAlignmentName = "";
        int lrySiteArea = -1;
        string lyrSiteAreaName = "";
        int lyrBuilding = -1;
        string lyrBuildingName = "";
        int lryTree = -1;
        string lyrTreeName = "";
        int lyrDEM = -1;
        string lyrDEMname = "";
        int lyrPole = -1;
        int lyrPvPanel = -1;
        string lyrPoleName;
        string lyrPvPanelName;
        string pvHeightGlo = "4.0";
        string pvWidthGlo = "2.0";
        string pvTiltGlo = "30";
        string pvAzimuthGlo = "180";
        string horzSpaceGlo = "2";
        string vertSpaceGlo = "8";
        string gridRotAngGlo = "0";
        int numSiteAreas = 0;
        bool useKML = false;
        bool demChecked = false;
        bool assumeDatum = false;
        string poleHeight = "2";
        public string pvHeightEdit = "0";
        public string pvWidthEdit = "0";
        public string pvTiltEdit = "0";
        public string pvAzimuthEdit = "0";

        

        [Browsable(false)]
        public double PvWidth
        {
            get { return pvWidth; }
            set { pvWidth = value; }
        }
        double pvHeight = 0;

        [Browsable(false)]
        public double PvHeight
        {
            get { return pvHeight; }
            set { pvHeight = value; }
        }
        
        [Browsable(false)]
        public bool UseKML
        {
            get { return useKML; }
            set { useKML = value; }
        }

        [Browsable(false)]
        public bool AssumeDatum
        {
            get { return assumeDatum; }
            set { assumeDatum = value; }
        }

        [Browsable(false)]
        public int NumSiteAreas
        {
            get { return numSiteAreas; }
            set { numSiteAreas = value; }
        }

        [Browsable(true)][DisplayName("Use DEM")][Category("Misc.")]
        public bool DemChecked
        {
            get { return demChecked; }
            set { demChecked = value; }
        }

        [Category("Layer Names")]
        [DisplayName("Panel Array")]
        public string LyrPvPanelName
        {
            get { return lyrPvPanelName; }
            set { lyrPvPanelName = value; }
        }

        [Browsable(false)]
        public string LyrPoleName
        {
            get { return lyrPoleName; }
            set { lyrPoleName = value; }
        }

        [Category("Layer Names")]
        [DisplayName("DEM")]
        public string LyrDEMname
        {
            get { return lyrDEMname; }
            set { lyrDEMname = value; }
        }

        [Category("Legend Index")][ReadOnly(true)][DisplayName("PV Panel Index")]
        public int LyrPvPanel
        {
            get { return lyrPvPanel; }
            set { lyrPvPanel = value; }
        }

        [Browsable(false)]
        public int LyrPole
        {
            get { return lyrPole; }
            set { lyrPole = value; }
        }

        [Category("Legend Index")]
        [ReadOnly(true)]
        [DisplayName("Line")]
        public int LyrAlignment
        {
            get { return lyrAlignment; }
            set { lyrAlignment = value; }
        }

        [Category("Layer Names")]
        [DisplayName("Line")]
        public string LyrAlignmentName
        {
            get { return lyrAlignmentName; }
            set { lyrAlignmentName = value; }
        }
        [Category("Legend Index")]
        [ReadOnly(true)]
        [DisplayName("Boundry")]
        public int LyrSiteArea
        {
            get { return lrySiteArea; }
            set { lrySiteArea = value; }
        }
        [Category("Layer Names")]
        [DisplayName("Boundry")]
        public string LyrSiteAreaName
        {
            get { return lyrSiteAreaName; }
            set { lyrSiteAreaName = value; }
        }

        [Category("Legend Index")]
        [ReadOnly(true)]
        [DisplayName("Tree Layer")]
        public int LyrTree
        {
            get { return lryTree; }
            set { lryTree = value; }
        }
        [Category("Layer Names")]
        [DisplayName("Tree Layer")]
        public string LyrTreeName
        {
            get { return lyrTreeName; }
            set { lyrTreeName = value; }
        }

        [Category("Legend Index")]
        [ReadOnly(true)]
        [DisplayName("Building Layer")]
        public int LyrBuilding
        {
            get { return lyrBuilding; }
            set { lyrBuilding = value; }
        }
        [Category("Layer Names")]
        [DisplayName("Building Layer")]
        public string LyrBuildingName
        {
            get { return lyrBuildingName; }
            set { lyrBuildingName = value; }
        }

        [Category("Legend Index")]
        [DisplayName("DEM Layer")]
        [ReadOnly(true)]
        public int LyrDEM
        {
            get { return lyrDEM; }
            set { lyrDEM = value; }
        }

        [Category("Misc.")][Browsable(false)]
        public bool[] Verify
        {
            get { return Checked; }
            set { Checked = value; }
        }

        [Category("Configuration")]          
        public string Path
        {
            get { return path; }
            set { path = value; }
        }
        [Category("Configuration")]
        [DisplayName("Weather Station File")]        
        public string WeatherSta
        {
            get { return weatherSta; }
            set { weatherSta = value; }
        }

        [Category("Misc.")]
        [ReadOnly(true)]
        public double Latiude
        {
            get { return latiude; }
            set { latiude = value; }
        }

        [Category("Misc.")]
        [ReadOnly(true)]
        public double Longtitude
        {
            get { return longtitude; }
            set { longtitude = value; }
        }

        [Category("Misc.")]
        [ReadOnly(true)]
        public double UtmN
        {
            get { return utmN; }
            set { utmN = value; }
        }

        [Category("Misc.")]
        [ReadOnly(true)]
        public double UtmE
        {
            get { return utmE; }
            set { utmE = value; }
        }

        [Category("Misc.")][ReadOnly(true)]
        public Int16 TimeZome
        {
            get { return timeZome; }
            set { timeZome = value; }
        }

        [Category("Panel Property Defaults")][DisplayName("Number of PV Panels")][ReadOnly(true)]
        public int NumPvPanel
        {
            get { return numPvPanel; }
            set { numPvPanel = value; }
        }

        [Category("Panel Property Defaults")][DisplayName("Height")]
        public string PvHeightGlo
        {
            get { return pvHeightGlo; }
            set {pvHeightGlo = value; }
        }

        [Category("Panel Property Defaults")][DisplayName("Width")]
        public string PvWidthGlo
        {
            get { return pvWidthGlo; }
            set { pvWidthGlo = value; }
        }

        [Category("Panel Property Defaults")][DisplayName("Tilt Angle")]
        public string PvTiltGlo
        {
            get { return pvTiltGlo; }
            set { pvTiltGlo = value; }
        }

        [Category("Panel Property Defaults")][DisplayName("Azimuth Angle")]
        public string PvAzimuthGlo
        {
            get { return pvAzimuthGlo; }
            set { pvAzimuthGlo = value; }
        }

        [Category("Panel Property Defaults")][DisplayName("Horizontal Spacing")]
        public string HorzSpaceGlo
        {
            get { return horzSpaceGlo; }
            set { horzSpaceGlo = value; }
        }

        [Category("Panel Property Defaults")][DisplayName("Verticle Spacing")]
        public string VertSpaceGlo
        {
            get { return vertSpaceGlo; }
            set { vertSpaceGlo = value; }
        }

        [Category("Panel Property Defaults")][DisplayName("Grid Rotation Angle")]
        public string GridRotAngGlo
        {
            get { return gridRotAngGlo; }
            set { gridRotAngGlo = value; }
        }

        [Browsable(false)]
        public string PvHeightEdit
        {
            get { return pvHeightEdit; }
            set { pvHeightEdit = value; }
        }

        [Browsable(false)]
        public string PvWidthEdit
        {
            get { return pvWidthEdit; }
            set { pvWidthEdit = value; }
        }

        [Browsable(false)]
        public string PvTiltEdit
        {
            get { return pvTiltEdit; }
            set { pvTiltEdit = value; }
        }

        [Browsable(false)]
        public string PvAzimuthEdit
        {
            get { return pvAzimuthEdit; }
            set { pvAzimuthEdit = value; }
        }

        [DisplayName("Pole Height")]
        [Category("Panel Property Defaults")]
                public string PoleHeight
        {
            get { return poleHeight; }
            set { poleHeight = value; }
        }

    }
}
