using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Symbology;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PVDESKTOP
{
    public partial class frmPvPanelSetup : Form
    {

        Map pvMap;
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
        PvDesktopUtilityFunction util = new PvDesktopUtilityFunction();
        PvDesktopProject project;

        internal PvDesktopProject Project
        {
            get { return project; }
            set { project = value; }
        }

        //orientation
        int cameraX=0, cameraY=0, cameraZ=0, cubeX=0, cubeY=0, cubeZ=0;
        Bitmap[] bmp = new Bitmap[6];
        int i = 0;
        YLScsDrawing.Drawing3d.Cuboid cub = new YLScsDrawing.Drawing3d.Cuboid(100, 200, 10);
        YLScsDrawing.Drawing3d.Camera cam = new YLScsDrawing.Drawing3d.Camera();
        
        public frmPvPanelSetup()
        {
            InitializeComponent();

        }
 
        private void Form1_Load(object sender, EventArgs e)
        {
            lblPvHeight.Text=project.PvHeightEdit;
            lblHeight.Text = lblPvHeight.Text;
            txtPvHeight.Text = lblPvHeight.Text;

            lblPvWidth.Text=project.PvWidthEdit;  
            lblWidth.Text = lblPvWidth.Text;
            txtPvWidth.Text = lblPvWidth.Text;

            lblTilt.Text = project.PvTiltEdit;
            pvPanelAngle1.tiltAngle = float.Parse(lblTilt.Text);
            lblAzimuth.Text=project.PvAzimuthEdit;
            pvPanelCompassCtl1.AzimutAngle = float.Parse(lblAzimuth.Text);

            //--------------------------------------------------------------------           
            cub.Center = new YLScsDrawing.Drawing3d.Point3d(400, 240, 0);
            cam.Location = new YLScsDrawing.Drawing3d.Point3d(400, 240, -500);
            //--------------------------------------------------------------------

            bmp[0] = new Bitmap((Bitmap)picPv.Image);
            Michael.propertyGrid1.Refresh();
            project.LyrPvPanel = util.getLayerHdl(project.LyrPoleName, pvMap);
            Invalidate();
            if (Project.LyrPvPanel != -1)
            {
                List<IFeature> ls1 = new List<IFeature>();
                FeatureLayer fl1 = pvMap.Layers[project.LyrPvPanel] as FeatureLayer;
                ISelection il1 = fl1.Selection;
                FeatureSet fe = il1.ToFeatureSet();
                grdPvPoleSelected.DataSource = fe.DataTable; 
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            cub.Draw(e.Graphics, cam);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            cubeX += 1;
            /*
            labelCrX.Text = cubeX.ToString();
            YLScsDrawing.Drawing3d.Quaternion q = new YLScsDrawing.Drawing3d.Quaternion();
            q.FromAxisAngle(new YLScsDrawing.Drawing3d.Vector3d(1, 0, 0), 5*Math.PI/180.0);
            cub.RotateAt(cub.Center, q);
            Invalidate();*/
            pvPanelAngle1.tiltAngle = cubeX+90;
            DrawPv3D();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            cubeX -= 1;
            pvPanelAngle1.tiltAngle = (float)cubeX+90;
            DrawPv3D();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            cubeY+= 1;
            pvPanelCompassCtl1.AzimutAngle = cubeY;
            DrawPv3D();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            cubeY -= 1;
            pvPanelCompassCtl1.AzimutAngle = cubeY;
            DrawPv3D();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            cubeZ += 1;
            //pvPanelAxisRotationCtl1.AxisAngle  = cubeZ;
            DrawPv3D();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            cubeZ -= 1;
            //pvPanelAxisRotationCtl1.AxisAngle = cubeZ;
            DrawPv3D();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cam.MoveLeft(10);
            Invalidate();
            labelMx.Text = cam.Location.X.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cam.MoveRight(10);
            Invalidate();
            labelMx.Text = cam.Location.X.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cam.MoveUp(10);
            Invalidate();
            labelMy.Text = cam.Location.Y.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cam.MoveDown(10);
            Invalidate();
            labelMy.Text = cam.Location.Y.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cam.MoveIn(10);
            Invalidate();
            labelMz.Text = cam.Location.Z.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            cam.MoveOut(10);
            Invalidate();
            labelMz.Text = cam.Location.Z.ToString();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            cameraY -= 1;
            labelMrY.Text = cameraY.ToString();
            cam.TurnLeft(1);
            Invalidate();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            cameraY += 1;
            labelMrY.Text = cameraY.ToString();
            cam.TurnRight(1);
            Invalidate();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            cameraX -= 1;
            labelMrX.Text = cameraX.ToString();
            cam.TurnUp(1);
            Invalidate();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            cameraX+= 1;
            labelMrX.Text = cameraX.ToString();
            cam.TurnDown(1);
            Invalidate();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            cameraZ += 5;
            labelMrZ.Text = cameraZ.ToString();
            cam.Roll(-5);
            Invalidate();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            cameraZ -= 5;
            labelMrZ.Text = cameraZ.ToString();
            cam.Roll(5);
            Invalidate();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            cub = new YLScsDrawing.Drawing3d.Cuboid(100, 200, 10);
            cam = new YLScsDrawing.Drawing3d.Camera();
            cub.Center = new YLScsDrawing.Drawing3d.Point3d(400, 240, 0);
            cam.Location = new YLScsDrawing.Drawing3d.Point3d(400, 240, -500);
            Invalidate();
            i = 0;
            bmp = new Bitmap[6];

            labelMx.Text = cam.Location.X.ToString();
            labelMy.Text = cam.Location.Y.ToString();
            labelMz.Text = cam.Location.Z.ToString();
            labelCx.Text = cub.Center.X.ToString();
            labelCy.Text = cub.Center.Y.ToString();
            labelCz.Text = cub.Center.Z.ToString();
            cameraX = 0; cameraY = 0; cameraZ = 0;  cubeX = 0; cubeY = 0; cubeZ = 0;
            labelCrX.Text = "0";
            labelCrY.Text = "0";
            labelCrZ.Text = "0";
            labelMrX.Text = "0";
            labelMrY.Text = "0";
            labelMrZ.Text = "0";
        }


        private void button13_Click(object sender, EventArgs e)
        {
            if (i == 6) return;
            OpenFileDialog o = new OpenFileDialog();
            if (o.ShowDialog() == DialogResult.OK)
            {
                bmp[i] = new Bitmap(o.FileName);
                i++;
            }
            cub.FaceImageArray = bmp;
            cub.DrawingLine = false;
            cub.DrawingImage = true;
            cub.FillingFace = true;
            Invalidate();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            cub.Center = new YLScsDrawing.Drawing3d.Point3d(cub.Center.X - 5, cub.Center.Y, cub.Center.Z);
            labelCx.Text = cub.Center.X.ToString();
            Invalidate();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            cub.Center = new YLScsDrawing.Drawing3d.Point3d(cub.Center.X+ 5, cub.Center.Y, cub.Center.Z);
            labelCx.Text = cub.Center.X.ToString();
            Invalidate();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            cub.Center = new YLScsDrawing.Drawing3d.Point3d(cub.Center.X, cub.Center.Y-5, cub.Center.Z);
            labelCy.Text = cub.Center.Y.ToString();
            Invalidate();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            cub.Center = new YLScsDrawing.Drawing3d.Point3d(cub.Center.X, cub.Center.Y + 5, cub.Center.Z);
            labelCy.Text = cub.Center.Y.ToString();
            Invalidate();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            cub.Center = new YLScsDrawing.Drawing3d.Point3d(cub.Center.X, cub.Center.Y , cub.Center.Z+5);
            labelCz.Text = cub.Center.Z.ToString();
            Invalidate();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            cub.Center = new YLScsDrawing.Drawing3d.Point3d(cub.Center.X, cub.Center.Y, cub.Center.Z - 5);
            labelCz.Text = cub.Center.Z.ToString();
            Invalidate();
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            bmp[i] = new Bitmap((Bitmap)picPv.Image);
            i = 1;
            cub.FaceImageArray = bmp;
            cub.DrawingLine = false;
            cub.DrawingImage = true;
            cub.FillingFace = true;
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

        private void pvPanelAngle1_AngleChanged(object sender, PaintEventArgs e)
        {
            DrawPv3D();
            lblTilt.Text = pvPanelAngle1.tiltAngle.ToString();
        }

        private void pvAxisLevel_Change(object sender, PaintEventArgs e)
        {
            DrawPv3D();
        }

        private void pvAzimuth_Change(object sender, PaintEventArgs e)
        {
            DrawPv3D();
            lblAzimuth.Text = pvPanelCompassCtl1.AzimutAngle.ToString();
        }

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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void cmdUpdateShape_Click(object sender, EventArgs e)
        {
            project.PvHeightEdit = lblPvHeight.Text;
            project.PvWidthEdit = lblPvWidth.Text;
            project.PvTiltEdit = lblTilt.Text;
            project.PvAzimuthEdit = lblAzimuth.Text;
           /*
            try
            {
                tabControl1.SelectedTab = tabPage0;
                for (int iRow = 0; iRow < grdPvPoleSelected.RowCount; iRow++)
                {
                    grdPvPoleSelected.Rows[iRow].Cells["Ele_Angle"].Value = lblTilt.Text;
                    grdPvPoleSelected.Rows[iRow].Cells["Azimuth"].Value = lblAzimuth.Text;
                    grdPvPoleSelected.Rows[iRow].Cells["w"].Value = lblPvWidth.Text;
                    grdPvPoleSelected.Rows[iRow].Cells["h"].Value = lblPvHeight.Text;
                }
            }
            catch { }
            */
            if (Project.LyrPole != -1)
            {
                
                IMapFeatureLayer LocationFe = PvMap.Layers[Project.LyrPole] as IMapFeatureLayer;
                List<IFeature> lstFe = new List<IFeature>();
                ISelection selFe = LocationFe.Selection;
                lstFe = selFe.ToFeatureList();

                int iRow = 0;
                foreach (IFeature fs in lstFe)
                {
                    try
                    {
                        fs.DataRow.BeginEdit();
                        fs.DataRow["Ele_Angle"] = grdPvPoleSelected.Rows[iRow].Cells["Ele_Angle"].Value;
                        fs.DataRow["Azimuth"] = grdPvPoleSelected.Rows[iRow].Cells["Azimuth"].Value;
                        fs.DataRow["h"] = grdPvPoleSelected.Rows[iRow].Cells["h"].Value;
                        fs.DataRow["w"] = grdPvPoleSelected.Rows[iRow].Cells["w"].Value;
                        fs.DataRow["x"] = grdPvPoleSelected.Rows[iRow].Cells["x"].Value;
                        fs.DataRow["y"] = grdPvPoleSelected.Rows[iRow].Cells["y"].Value;
                        fs.DataRow.EndEdit();
                        iRow++;
                    }
                    catch { }

                }
                this.Close();
            }
            Michael.CreatePvPanel();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void cmdSetSameHeight_Click(object sender, EventArgs e)
        {
            try
            {
                tabControl1.SelectedTab = tabPage0;
                for (int iRow = 0; iRow < grdPvPoleSelected.RowCount; iRow++)
                {
                    grdPvPoleSelected.Rows[iRow].Cells["Ele_Angle"].Value = lblTilt.Text;
                    grdPvPoleSelected.Rows[iRow].Cells["Azimuth"].Value = lblAzimuth.Text;
                    grdPvPoleSelected.Rows[iRow].Cells["w"].Value = lblPvWidth.Text;
                    grdPvPoleSelected.Rows[iRow].Cells["h"].Value = lblPvHeight.Text;
                }
            }
            catch { }
        }

        private void cmbSolarPanelSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] a = cmbSolarPanelSize.Text.Split('X');
            txtPvHeight.Text = (Convert.ToDouble(a[0]) / 1000).ToString("0.000");
            txtPvWidth.Text = (Convert.ToDouble(a[1]) / 1000).ToString("0.000");
            lblPvHeight.Text = (Convert.ToDouble(a[0]) / 1000).ToString("0.000");
            lblPvWidth.Text = (Convert.ToDouble(a[1]) / 1000).ToString("0.000");
        }

        private void txtPvHeight_TextChanged(object sender, EventArgs e)
        {
            lblHeight.Text = txtPvHeight.Text;
            lblPvHeight.Text = txtPvHeight.Text;
        }

        private void txtPvWidth_TextChanged(object sender, EventArgs e)
        {
            lblWidth.Text = txtPvWidth.Text;
            lblPvWidth.Text = txtPvWidth.Text;
        }

        private void lblTilt_TextChanged(object sender, EventArgs e)
        {
            for (int iRow = 0; iRow < grdPvPoleSelected.RowCount; iRow++)
            {
            grdPvPoleSelected.Rows[iRow].Cells["Ele_Angle"].Value = lblTilt.Text;
            }
        }

        private void lblAzimuth_TextChanged(object sender, EventArgs e)
        {
            for (int iRow = 0; iRow < grdPvPoleSelected.RowCount; iRow++)
            {
                grdPvPoleSelected.Rows[iRow].Cells["Azimuth"].Value = lblAzimuth.Text;
            }
        }

        private void lblPvWidth_TextChanged(object sender, EventArgs e)
        {
            for (int iRow = 0; iRow < grdPvPoleSelected.RowCount; iRow++)
            {
                grdPvPoleSelected.Rows[iRow].Cells["w"].Value = lblPvWidth.Text;
            }
        }

        private void lblPvHeight_TextChanged(object sender, EventArgs e)
        {
            for (int iRow = 0; iRow < grdPvPoleSelected.RowCount; iRow++)
            {
                grdPvPoleSelected.Rows[iRow].Cells["h"].Value = lblPvHeight.Text;
            }
        }


       
    }
}