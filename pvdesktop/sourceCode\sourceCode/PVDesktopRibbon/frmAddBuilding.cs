using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Symbology;

namespace PVDESKTOP
{
    public partial class frmAddBuilding : Form
    {
        Map pvMap;
        PvDesktopProject project;
        PvDesktopUtilityFunction util = new PvDesktopUtilityFunction();
        frm01_MainForm _michael;
        public frm01_MainForm Michael
        {
            get { return _michael; }
            set { _michael = value; }
        }
        public Map PvMap
        {
            get { return pvMap; }
            set { pvMap = value; }
        }
        internal PvDesktopProject ProjectFile
        {
            get { return project; }
            set { project = value; }
        }
        public frmAddBuilding()
        {
            InitializeComponent();
        }
        
        private void btnCancelBuilding_Click(object sender, EventArgs e)
        {
            #region opens saved building shapefile

            util.removeDupplicateLyr(Michael.prj.LyrBuildingName, PvMap);
            IFeatureSet Fe = FeatureSet.Open(ProjectFile.Path + "Building.shp");
            Fe.Name = "Building";
            pvMap.Layers.Add(Fe);
            Michael.prj.LyrBuildingName = "Building";
            util.ClearGraphicMap(pvMap);
            pvMap.MapFrame.Invalidate(); 

            #endregion
            
            
            this.Close();
        }

        private void btnSaveBuilding_Click(object sender, EventArgs e)
        {
            if (project.Path != "")
            {
                if (util.getLayerHdl("Solar Radiation Rose", pvMap) == -1)
                {
                    Michael.CentroidAsSite(project.LyrBuildingName,pvMap);
                }
            }
            Michael.drawBuildingShadow();
            Michael.ExportBldgAndTrr2SketchUp.Enabled = true;
            Michael.EnableBuildingEdit();
            this.Close();
        }

        private void frmAddBuilding_Load(object sender, EventArgs e)
        {
            int lyr = util.getLayerHdl(project.LyrBuildingName, pvMap);
            if (lyr != -1)
            {
                //MessageBox.Show(ProjectFile.Path);
                FeatureSet Fe = pvMap.Layers[lyr].DataSet as FeatureSet;
                Fe.SaveAs(ProjectFile.Path + "Building.shp", true);
                Michael.prj.LyrBuildingName = "Building";
                //util.removeDupplicateLyr(Michael.prj.lyrBuildingName, pvMap);
            }
        }
    }
}
