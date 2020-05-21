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
    public partial class frmCreatePoleByAlignment : Form
    {
        Map pvMap;
        PvDesktopUtilityFunction util = new PvDesktopUtilityFunction();
        PvDesktopProject project;
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

        /*
        frm01_MainForm mainForm = new frm01_MainForm();

        public frm01_MainForm MainForm
        {
            get { return mainForm; }
            set { mainForm = value; }
        }
        */

        

        public frmCreatePoleByAlignment()
        {
            InitializeComponent();
        }

        private void frmCreatePoleByAlignment_Load(object sender, EventArgs e)
        {
            int nShp;
            if (ProjectFile.LyrAlignment != -1)
            {
                IMapFeatureLayer alingmentFe = pvMap.Layers[ProjectFile.LyrAlignment] as IMapFeatureLayer;
                nShp = alingmentFe.DataSet.NumRows() - 1;
                grdAlignment.Rows.Clear();
                for (int i = 0; i < alingmentFe.DataSet.NumRows(); i++)
                {
                    grdAlignment.Rows.Add();
                    IFeature fs = alingmentFe.DataSet.GetFeature(i);
                    object fill = 1;
                    object sp = fs.DataRow["Spacing"];
                    object remark = fs.DataRow["remark"];
                    grdAlignment.Rows[i].Cells[0].Value = i;
                    grdAlignment.Rows[i].Cells[1].Value = sp;
                    grdAlignment.Rows[i].Cells[2].Value = remark;
                   
                    if(grdAlignment.Rows[i].Cells[1].Value.ToString().Length  <1)
                    {
                        grdAlignment.Rows[i].Cells[1].Value = fill;
                    }
                }
           
            }

            if (project.LyrDEM!=-1 )
            {
                chkDEM.Enabled = true;
            }
            else
            {
                chkDEM.Enabled = false;  
            }
        }

        private void cmdReloadAlignmentData_Click(object sender, EventArgs e)
        {
            if (ProjectFile.LyrAlignment != -1)
            {
                IMapFeatureLayer alingmentFe = pvMap.Layers[ProjectFile.LyrAlignment] as IMapFeatureLayer;
                int nShp = alingmentFe.DataSet.NumRows() - 1;
                grdAlignment.Rows.Clear();
                for (int i = 0; i < alingmentFe.DataSet.NumRows(); i++)
                {
                    grdAlignment.Rows.Add();
                    IFeature fs = alingmentFe.DataSet.GetFeature(i);
                    object sp = fs.DataRow["spacing"];
                    object remark = fs.DataRow["remark"];
                    grdAlignment.Rows[i].Cells[0].Value = i;
                    grdAlignment.Rows[i].Cells[1].Value = sp;
                    grdAlignment.Rows[i].Cells[2].Value = remark;
                }
            }
        }

        private void cmdSaveAlignmentData_Click(object sender, EventArgs e)
        {
            saveAlignmentAttribute();
        }

        private void cmdCreatePvPole_Click(object sender, EventArgs e)
        {
            saveAlignmentAttribute();
            double xSpacing = 1;

            int iLyr = ProjectFile.LyrAlignment;// getLayerHdl(cmbAlignmentLyr.Text);
            IMapFeatureLayer bLineLyr = pvMap.Layers[iLyr] as IMapFeatureLayer;

            CratePvPole(xSpacing, bLineLyr);
           
            //grpBLineInfo.Visible = false;
            //loadLayerList();
            //MessageBox.Show("Pv. ploe created complete (Total Panel: " + numPvPanel.ToString() + ")");
            //updateArea();
             
            //MainForm.pvVerify(); 
        }

        void saveAlignmentAttribute()
        {
            if (ProjectFile.LyrAlignment != -1)
            {
                IMapFeatureLayer alignmentFe = pvMap.Layers[ProjectFile.LyrAlignment] as IMapFeatureLayer;
                int nShp = alignmentFe.DataSet.NumRows();

                for (int iRow = 0; iRow < nShp; iRow++)
                {
                    IFeature fs = alignmentFe.DataSet.GetFeature(iRow);
                    try
                    {
                        if (chkSystemSpacing.Checked == true) grdAlignment.Rows[iRow].Cells[1].Value = Convert.ToDouble(txtDx.Text);
                        fs.DataRow.BeginEdit();
                        fs.DataRow["spacing"] = grdAlignment.Rows[iRow].Cells[1].Value;
                        fs.DataRow["Remark"] = grdAlignment.Rows[iRow].Cells[2].Value;
                        fs.DataRow.EndEdit();
                    }
                    catch { }

                }
            }
        }

        void CratePvPole(double xSpacing, IMapFeatureLayer BLineFeLyr)
        {
            project.NumPvPanel = 0;
            IMapRasterLayer demLyr;
            Raster dem4Pv = new Raster();
            double poleH = 1; //Default pole height = 1 m.
            double z0 = 0;
            try { poleH = Convert.ToDouble(txtPoleHeight.Text); }
            catch
            {
                MessageBox.Show("Pole height value error");
                txtPoleHeight.Text = "1";
                poleH = 1;
            }

            if (project.LyrDEM   != -1 & chkDEM.Checked == true)
            {
                demLyr = pvMap.Layers[project.LyrDEM] as IMapRasterLayer;

                if (demLyr == null)
                {
                    MessageBox.Show("Error: DEM Data is not correct");
                    return;
                }

                int mRow = demLyr.Bounds.NumRows;
                int mCol = demLyr.Bounds.NumColumns;
                dem4Pv = (Raster)demLyr.DataSet;
                Coordinate ptReference = new Coordinate(project.UtmE , project.UtmN );
                RcIndex rc = dem4Pv.ProjToCell(ptReference);
                //RcIndex rc = demLyr.DataSet.ProjToCell(ptReference);
                if (rc.Column < 0 | rc.Row < 0)
                {
                    z0 = 0;
                }
                else
                {
                    z0 = dem4Pv.Value[rc.Row, rc.Column];
                    //z0 = demLyr.DataSet.Value[rc.Row, rc.Column];
                }
            }
            //------------------------------------------------------------------------------------------------
            // Create pole posion from baseline shapefile
            //------------------------------------------------------------------------------------------------

            int nShp = BLineFeLyr.DataSet.NumRows(); // get Number of Feature
            double dx = xSpacing; //m.

            FeatureSet fs;
            fs = new FeatureSet(FeatureType.Point);
            //---------------------------------------------------------
            fs.DataTable.Columns.Add(new DataColumn("x", typeof(double)));
            fs.DataTable.Columns.Add(new DataColumn("y", typeof(double)));
            fs.DataTable.Columns.Add(new DataColumn("w", typeof(double)));
            fs.DataTable.Columns.Add(new DataColumn("h", typeof(double)));
            fs.DataTable.Columns.Add(new DataColumn("Azimuth", typeof(double)));
            fs.DataTable.Columns.Add(new DataColumn("Ele_Angle", typeof(double)));
            fs.DataTable.Columns.Add(new DataColumn("ele", typeof(double)));
            //---------------------------------------------------------

            for (int i = 0; i < nShp; i++) //Line shape
            {
                IFeature BLineFe = BLineFeLyr.DataSet.GetFeature(i);
                if (chkSystemSpacing.Checked == true)
                { dx = Convert.ToDouble(txtDx.Text); }
                else
                {
                    try
                    {
                        object val = BLineFe.DataRow["spacing"];
                        dx = (double)val;
                        if (dx <= 0)
                        {
                            MessageBox.Show("Error: Spacing data incorrect.");
                            return;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Error: Spacing data not found.");
                        project.Verify[5] = false;
                        return;
                    }
                }
                xSpacing = dx;
                double sumSegment = 0;
                double sumL = 0;
                double iniSegment = 0;
                double LastSegment = 0;
                for (int n = 0; n < BLineFe.NumPoints - 1; n++) //Line segment
                {
                    double x1 = BLineFe.Coordinates[n].X;
                    double y1 = BLineFe.Coordinates[n].Y;
                    double x2 = BLineFe.Coordinates[n + 1].X;
                    double y2 = BLineFe.Coordinates[n + 1].Y;
                    double LastL = sumL;
                    double dL;

                    sumL += Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
                    sumSegment = LastSegment; 
                    if (sumSegment + xSpacing <= sumL | sumSegment == 0)
                    {
                        for (double Segment = sumSegment; Segment <= sumL; Segment += xSpacing)
                        {
                            dL = Segment - LastL; 
                            Coordinate poleLocation = util.PointOnTheLine(x1, y1, x2, y2, dL);
                            if (poleLocation != null)
                            {
                                LastSegment += xSpacing;
                              
                                double poleHeight = Convert.ToDouble(txtPoleHeight.Text);

                                //Coordinate poleLocation = new Coordinate(pt.X, pt.Y, poleHeight);
                                IPoint poleFe = new DotSpatial.Topology.Point(poleLocation);
                                IFeature ifea = fs.AddFeature(poleFe);
                                project.NumPvPanel++;

                                //------------------------------------------------------
                                ifea.DataRow.BeginEdit();
                                ifea.DataRow["x"] = poleLocation.X;
                                ifea.DataRow["y"] = poleLocation.Y;
                                ifea.DataRow["w"] = 0;
                                ifea.DataRow["h"] = 0;
                                ifea.DataRow["Azimuth"] = 0;
                                ifea.DataRow["Ele_Angle"] = 0;

                                if (project.LyrDEM != -1 & chkDEM.Checked == true)
                                {
                                    RcIndex rc = dem4Pv.ProjToCell(poleLocation);
                                    double z = 0;
                                    if (rc.Column < 0 | rc.Row < 0)
                                    {
                                        z = 0;
                                    }
                                    else
                                    {
                                        z = dem4Pv.Value[rc.Row, rc.Column];
                                    }
                                    //demLyr.DataSet
                                    if (radioAboveAssumeDatum.Checked == true)
                                    {
                                        ifea.DataRow["ele"] = poleH;
                                    }
                                    else
                                    {
                                        ifea.DataRow["ele"] = z - z0 + poleH;
                                    }
                                }
                                else
                                {
                                    ifea.DataRow["ele"] = poleH;
                                }
                                ifea.DataRow.EndEdit();
                            }
                        }
                    }
                }
            } // next alignment shape

            fs.Projection = pvMap.Projection;
            
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            fs.Name = "Panel Positon";
            fs.Filename = project.Path + "\\Temp\\" + fs.Name + ".shp";
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            fs.SaveAs(fs.Filename, true);
            util.removeDupplicateLyr(fs.Name, pvMap);
            PointSymbolizer p = new PointSymbolizer(Color.Yellow, DotSpatial.Symbology.PointShape.Hexagon, 6);
            p.ScaleMode = ScaleMode.Simple;
            MapPointLayer kasem = new MapPointLayer(fs);
            kasem.Symbolizer = p;
            pvMap.Layers.Add(kasem);
            
            //loadLayerList();
            //pvMap.MapFrame.DrawingLayers.Clear();
            util.ClearGraphicMap(pvMap);
            project.LyrPoleName = fs.Name;
            

            /*
            MapPointLayer rangeRingAxis;
            rangeRingAxis = new MapPointLayer(fs);
            pvMap.MapFrame.DrawingLayers.Add(rangeRingAxis);
            pvMap.MapFrame.Invalidate();
             */
            //project.Verify[5] = true;
            //updateArea();
        }

        private void chkSystemSpacing_CheckedChanged(object sender, EventArgs e)
        {
            txtDx.Enabled = chkSystemSpacing.Checked;
            lblSpacingUnit.Enabled = chkSystemSpacing.Checked;
        }

        private void lblNoOfPole_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkUseDefaultPnlProp_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pvPanelPoleGridCtl1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void txtGridSpacingY_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtGridSpacingX_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblAzimuth_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblTilt_TextChanged(object sender, EventArgs e)
        {

        }

        private void pvPanelCompassCtl2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void pvPanelAngle2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtPvWidth_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPvHeight_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbSolarPanelSize_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void updateAlignment()
        {
            if (ProjectFile.LyrAlignment != -1)
            {
                IMapFeatureLayer alignmentFe = pvMap.Layers[ProjectFile.LyrAlignment] as IMapFeatureLayer;
                int nShp = alignmentFe.DataSet.NumRows();

                for (int iRow = 0; iRow < nShp; iRow++)
                {
                    IFeature fs = alignmentFe.DataSet.GetFeature(iRow);
                    try
                    {
                        if (chkSystemSpacing.Checked == true) grdAlignment.Rows[iRow].Cells[1].Value = Convert.ToDouble(txtDx.Text);
                        fs.DataRow.BeginEdit();
                        fs.DataRow["spacing"] = grdAlignment.Rows[iRow].Cells[1].Value;
                        fs.DataRow["Remark"] = grdAlignment.Rows[iRow].Cells[2].Value;
                        fs.DataRow.EndEdit();
                        Color pntColor = Color.Yellow;
                    }
                    catch { }                    
                }
            }
            double xSpacing = 1;
            int iLyr = ProjectFile.LyrAlignment;// getLayerHdl(cmbAlignmentLyr.Text);
            IMapFeatureLayer bLineLyr = pvMap.Layers[iLyr] as IMapFeatureLayer;

            CratePvPole(xSpacing, bLineLyr);

            
        }


        private void txtDx_TextChanged(object sender, EventArgs e)
        {
            updateAlignment();
        }

        private void grdAlignment_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            updateAlignment();
        }

        private void btnSaveAlignment_Click(object sender, EventArgs e)
        {
            updateAlignment();
            this.Close();
        }


    }
}
