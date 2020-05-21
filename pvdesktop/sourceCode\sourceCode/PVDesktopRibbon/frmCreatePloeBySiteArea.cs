using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Symbology;
using DotSpatial.Topology;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PVDESKTOP
{
    public partial class frmCreatePoleBySiteArea : Form
    {
        PvDesktopProject project;
        PvDesktopUtilityFunction util = new PvDesktopUtilityFunction();
        Map map;
        frm01_MainForm _michael;
        public frm01_MainForm Michael
        {
            get { return _michael; }
            set { _michael = value; }
        }
        public Map pvMap
        {
            get { return map; }
            set { map = value; }
        }

        internal PvDesktopProject Project
        {
            get { return project; }
            set { project = value; }
        }

        public frmCreatePoleBySiteArea()
        {
            InitializeComponent();
        }

        private void pvPanelPoleGridCtl1_Paint(object sender, PaintEventArgs e)
        {
            Michael.CreateGridPole();

        }

        bool FirstRefresh = true;
        
        private void frmCreatePoleBySiteArea_Load(object sender, EventArgs e)
        {
            //#region loads form with previously selected values stored in the PvDesktopProject class---------------------------------

            lblHeight.Text = project.PvHeightGlo;
            lblHeight2.Text = lblHeight.Text;
            lblHeight3.Text = lblHeight.Text;
            lblHeight4.Text = lblHeight.Text;
            txtPvHeight.Text = lblHeight.Text;

            lblWidth.Text = project.PvWidthGlo;
            lblWidth.Text = lblWidth.Text;
            lblWidth2.Text = lblWidth.Text;
            lblWidth3.Text = lblWidth.Text;
            lblWidth4.Text = lblWidth.Text;
            txtPvWidth.Text = lblWidth.Text;

            lblTilt2.Text = project.PvTiltGlo;
            lblTilt3.Text = lblTilt2.Text;
            lblTilt4.Text = lblTilt2.Text;
            pvPanelAngle2.tiltAngle = float.Parse(lblTilt2.Text);

            lblAzimuth2.Text = project.PvAzimuthGlo;
            lblAzimuth3.Text = lblAzimuth2.Text;
            lblAzimuth4.Text = lblAzimuth2.Text;
            pvPanelCompassCtl2.AzimutAngle = float.Parse(lblAzimuth2.Text);

            pvPanelPoleGridCtl1.RotationAngle = float.Parse(project.GridRotAngGlo);
            txtGridSpacingY.Text = project.VertSpaceGlo;
            txtGridSpacingX.Text = project.HorzSpaceGlo;

            txtPoleHeight.Text = project.PoleHeight;

            updateLblNumPanels();


            //-------------------------------------------------------------------------------------------------------------------
            // #endregion

             #region  saves the layers before they're edited so they can be restored if user clicks cancel on form-----------------------
            
            //MessageBox.Show(project.Path);


            /*
            int lyr = util.getLayerHdl(Michael.prj.LyrPoleName, pvMap);

            if (lyr != -1)
            {

                FeatureSet Fe = pvMap.Layers[lyr].DataSet as FeatureSet;
                Fe.SaveAs(Project.Path + "pvArrayPole.shp", true);
                Michael.prj.LyrPoleName = "Panel Position";
                util.removeDupplicateLyr(Michael.prj.LyrPoleName, pvMap);
            }

            int lyr2 = util.getLayerHdl(Michael.prj.LyrPvPanelName, pvMap);
            if (lyr2 != -1)
            {
                //   MessageBox.Show(project.Path);
                FeatureSet Fe2 = pvMap.Layers[lyr2].DataSet as FeatureSet;
                Fe2.SaveAs(Project.Path + "pv Array.shp", true);
                Michael.prj.LyrPvPanelName = "PV Panel Array";
                util.removeDupplicateLyr(Michael.prj.LyrPvPanelName, pvMap);
            }
            */
            #endregion save for cancel

     
        }

        private void updateLblNumPanels()
        {
            lblNoOfPole.Text = "Panels in " + project.LyrSiteAreaName + ": " + project.NumPvPanel.ToString();
            toolTip1.SetToolTip(lblNoOfPole, lblNoOfPole.Text);
        }
        private void cmdPole_in_Area_Click(object sender, EventArgs e)
        {

            Michael.CreateGridPole(true);
            updateLblNumPanels();
         //CreateGridPole(true);
        }



        private void pvPanelPoleGridCtl1_Paint_1(object sender, PaintEventArgs e)
        {
            if (FirstRefresh == false) 
            {
                project.GridRotAngGlo = Convert.ToString(pvPanelPoleGridCtl1.RotationAngle);
                Michael.propertyGrid1.Refresh();
                Michael.CreateGridPole(false);
                updateLblNumPanels();
                  //CreateGridPole(false); 
            }
            FirstRefresh = false;            
        }

        private void btnSavePoleArea_Click(object sender, EventArgs e)
        {
             
            if (chkUseDefaultPnlProp.Checked)
            {
                project.PvHeightGlo = lblHeight2.Text;
                project.PvWidthGlo = lblWidth2.Text;
                project.HorzSpaceGlo = txtGridSpacingX.Text;
                project.VertSpaceGlo = txtGridSpacingY.Text;
                project.PvTiltGlo = lblTilt2.Text;
                project.PvAzimuthGlo = lblAzimuth2.Text;
                project.GridRotAngGlo = Convert.ToString(pvPanelPoleGridCtl1.RotationAngle);
                project.pvHeightEdit = lblHeight2.Text;
                project.pvWidthEdit = lblWidth2.Text;
                project.pvTiltEdit = lblTilt2.Text;
                project.pvAzimuthEdit = lblAzimuth2.Text;
                Michael.CreateGridPole(true, true);
                Michael.CreatePvPanel();
            }
            else
            {
                project.HorzSpaceGlo = txtGridSpacingX.Text;
                project.VertSpaceGlo = txtGridSpacingY.Text;
                project.GridRotAngGlo = Convert.ToString(pvPanelPoleGridCtl1.RotationAngle);
                Michael.CreateGridPole(true, false);
            }
            Michael.propertyGrid1.Refresh();
            Michael.cmdPvPanelAngle.Enabled = true;
            Michael.btnMovePanels.Enabled = true;
            Michael.cmdExportSketchUp.Enabled = true;
            this.Close();
        }

        private void btnCancelPoleArea_Click(object sender, EventArgs e)
        {
            #region Opens saved shapefile
            /*
            IFeatureSet Fe = FeatureSet.Open(Project.Path + "pvArrayPole.shp");
            Fe.Name = "Panel Postion";
            Michael.prj.LyrPoleName = "Panel Position";
            util.removeDupplicateLyr(project.LyrPoleName, pvMap);
            pvMap.Layers.Add(Fe);            
            //util.ClearGraphicMap(pvMap);
            //pvMap.MapFrame.Invalidate();

            IFeatureSet Fe2 = FeatureSet.Open(Project.Path + "pv Array.shp");
            Fe2.Name = "PV Panel Array";
            Michael.prj.LyrPvPanelName = "PV Panel Array";
            util.removeDupplicateLyr(project.LyrPvPanelName, pvMap);
            pvMap.Layers.Add(Fe2);
            */
            
            util.ClearGraphicMap(pvMap);
            pvMap.MapFrame.Invalidate();
            #endregion
            this.Close();
        }

        private void txtGridSpacingX_TextChanged(object sender, EventArgs e)
        {
            project.HorzSpaceGlo = txtGridSpacingX.Text;
            Michael.propertyGrid1.Refresh();
            if(FirstRefresh == false)
            {
                Michael.CreateGridPole();
                updateLblNumPanels();
                //CreateGridPole(false);
            }
               
        }

        private void txtGridSpacingY_TextChanged(object sender, EventArgs e)
        {
            project.VertSpaceGlo = txtGridSpacingY.Text;
            Michael.propertyGrid1.Refresh();
            if (FirstRefresh == false)
            {
                Michael.CreateGridPole(false);
                updateLblNumPanels();
             //CreateGridPole(false);
            }
            
        }

        private void chkUseDefaultPnlProp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseDefaultPnlProp.Checked == true)
            {
                grpboxPanelProps.Visible = true;
            }
            else
            {
                grpboxPanelProps.Visible = false;
            }
        }

        private void pvPanelAngle1_Paint(object sender, PaintEventArgs e)
        {
            DrawPv3D();
            lblTilt.Text = pvPanelAngle1.tiltAngle.ToString();
        }

        //orientation
        int cameraX = 0, cameraY = 0, cameraZ = 0, cubeX = 0, cubeY = 0, cubeZ = 0;
        Bitmap[] bmp = new Bitmap[6];
        int i = 0;
        YLScsDrawing.Drawing3d.Cuboid cub = new YLScsDrawing.Drawing3d.Cuboid(100, 200, 10);
        YLScsDrawing.Drawing3d.Camera cam = new YLScsDrawing.Drawing3d.Camera();

        void DrawPv3D()
        {
            cub = new YLScsDrawing.Drawing3d.Cuboid(100, 200, 10);
            cam = new YLScsDrawing.Drawing3d.Camera();
            cub.Center = new YLScsDrawing.Drawing3d.Point3d(400, 240, 0);
            cam.Location = new YLScsDrawing.Drawing3d.Point3d(400, 240, -500);

            cubeX = (int)(pvPanelAngle1.tiltAngle * -1) - 90;
            cubeY = (int)pvPanelCompassCtl1.AzimutAngle;
            cubeZ = 0;// (int)pvPanelAxisRotationCtl1.AxisAngle;

            labelCrX.Text = cubeX.ToString();
            labelCrY.Text = cubeY.ToString();
            labelCrZ.Text = cubeZ.ToString();

            YLScsDrawing.Drawing3d.Quaternion q = new YLScsDrawing.Drawing3d.Quaternion();
            q.FromAxisAngle(new YLScsDrawing.Drawing3d.Vector3d(1, 0, 0), cubeX * Math.PI / 180.0);
            cub.RotateAt(cub.Center, q);
            q.FromAxisAngle(new YLScsDrawing.Drawing3d.Vector3d(0, 1, 0), cubeY * Math.PI / 180.0);
            cub.RotateAt(cub.Center, q);
            q.FromAxisAngle(new YLScsDrawing.Drawing3d.Vector3d(0, 0, 1), cubeZ * Math.PI / 180.0);
            cub.RotateAt(cub.Center, q);
            if (chkShowTexture.Checked == true)
            {
                texture();
            }
            Invalidate();
        }

        void texture()
        {
            //bmp[0] = new Bitmap((Bitmap)picPv.Image);
            i = 1;
            cub.FaceImageArray = bmp;
            cub.DrawingLine = false;
            cub.DrawingImage = true;
            cub.FillingFace = true;
            //Invalidate();
        }

        private void pvAzimuth_Change(object sender, PaintEventArgs e)
        {
            DrawPv3D();
            lblAzimuth.Text = pvPanelCompassCtl1.AzimutAngle.ToString();
        }

        private void pvPanelCompassCtl1_Paint(object sender, PaintEventArgs e)
        {
            DrawPv3D();
            lblAzimuth.Text = pvPanelCompassCtl1.AzimutAngle.ToString();
        }




        private void txtPvWidth_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblTilt_TextChanged(object sender, EventArgs e)
        {
            lblTilt2.Text = lblTilt.Text;
        }

        private void lblAzimuth_TextChanged(object sender, EventArgs e)
        {
            lblAzimuth2.Text = lblAzimuth.Text;
        }

        private void lblNoOfPole_TextChanged(object sender, EventArgs e)
        {
            /*
            double numPanels = Convert.ToDouble(lblNoOfPole.Text);
            double W = Convert.ToDouble(project.PvWidthGlo);
            double L = Convert.ToDouble(project.PvHeightGlo);
            lblTtlArea.Text = Convert.ToString(numPanels * W * L);
            */
        }

        private void pvPanelAngle2_AngleChanged(object sender, PaintEventArgs e)
        {
            DrawPv3D();
            lblTilt.Text = pvPanelAngle2.tiltAngle.ToString();
        }

     //   private void cmdRefresh_Click(object sender, EventArgs e)
      //  {

      //  }

        private void pvPanelCompassCtl2_Paint(object sender, PaintEventArgs e)
        {
            DrawPv3D();
            lblAzimuth.Text = pvPanelCompassCtl2.AzimutAngle.ToString();
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {

        }

        private void pvPanelAngle2_Paint(object sender, PaintEventArgs e)
        {
           // DrawPv3D();
            lblTilt.Text = pvPanelAngle2.tiltAngle.ToString();
        }

        private void pvPanelCompassCtl2_Paint_1(object sender, PaintEventArgs e)
        {
            lblAzimuth.Text = pvPanelCompassCtl2.AzimutAngle.ToString();
        }

        private void chkUseKML_CheckedChanged(object sender, EventArgs e)
        {
           // project.UseKML = chkUseKML.Checked;
        }

        private void chkDEM_CheckedChanged(object sender, EventArgs e)
        {
            project.DemChecked = chkDEM.Checked;
        }

        private void txtPvHeight_TextChanged_1(object sender, EventArgs e)
        {
            lblHeight2.Text = txtPvHeight.Text;
            lblHeight2.Text = txtPvHeight.Text;
            lblHeight3.Text = txtPvHeight.Text;
            lblHeight4.Text = txtPvHeight.Text;
        }

        private void cmbSolarPanelSize_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string[] a = cmbSolarPanelSize.Text.Split('X');
            txtPvHeight.Text = (Convert.ToDouble(a[0]) / 1000).ToString("0.000");
            txtPvWidth.Text = (Convert.ToDouble(a[1]) / 1000).ToString("0.000");
            lblHeight.Text = (Convert.ToDouble(a[0]) / 1000).ToString("0.000");
            lblWidth.Text = (Convert.ToDouble(a[1]) / 1000).ToString("0.000");
        }

        private void txtPvWidth_TextChanged_1(object sender, EventArgs e)
        {
            lblWidth2.Text = txtPvWidth.Text;
            lblWidth3.Text = txtPvWidth.Text;
            lblWidth4.Text = txtPvWidth.Text;

        }

        private void pvPanelAngle2_Paint_1(object sender, PaintEventArgs e)
        {
            //DrawPv3D();
            lblTilt2.Text = pvPanelAngle2.tiltAngle.ToString();
            lblTilt3.Text = lblTilt2.Text;
            lblTilt4.Text = lblTilt2.Text;
        }

        private void pvPanelCompassCtl2_Paint_2(object sender, PaintEventArgs e)
        {
            lblAzimuth2.Text = pvPanelCompassCtl2.AzimutAngle.ToString();
            lblAzimuth3.Text = lblAzimuth2.Text;
            lblAzimuth4.Text = lblAzimuth2.Text;
        }

        private void txtPoleHeight_TextChanged(object sender, EventArgs e)
        {
            project.PoleHeight = txtPoleHeight.Text;
        }

        private void radioAboveAssumeDatum_CheckedChanged(object sender, EventArgs e)
        {
            project.AssumeDatum = radioAboveAssumeDatum.Checked;
        }
    }
}
