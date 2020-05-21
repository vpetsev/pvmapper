using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Symbology;
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
    public partial class frmPvPanel : Form
    {
        //FeatureSet feSet;
        PvDesktopProject project;
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

        internal PvDesktopProject Project
        {
            get { return project; }
            set { project = value; }
        }
        public RoofPlane RP       
        {
            get { return rp; }
            set { rp = value; }
        }
        /*
        public FeatureSet FeSet
        {
            get { return feSet; }
            set { feSet = value; }
        }
        */
        public frmPvPanel()
        {
            InitializeComponent();
        }
        RoofPlane rp = new RoofPlane();
        
        
        private void frmPvPanel_Load(object sender, EventArgs e)
        {
            //cmbSolarPanelSize.Text = "1956X992";

            txtGridSpacingX.Text = rp.RoofXSpace;
            txtGridSpacingY.Text = rp.RoofYSpace;
            txtPvHeight.Text = rp.RoofPanelHeight;
            txtPvWidth.Text = rp.RoofPanelWidth;
            lblAzimuth2.Text = rp.RoofAzimuth;
            lblHeight2.Text = rp.RoofPanelHeight;
            lblWidth2.Text = rp.RoofPanelWidth;
            lblTilt2.Text = rp.RoofTilt;
            cmdCheck4PvOnRoof.Checked = rp.Orthogonal;
            
        }


        private void txtPvWidth_TextChanged(object sender, EventArgs e)
        {
            lblWidth2.Text = txtPvWidth.Text;
            rp.RoofPanelWidth = lblWidth2.Text;
        }

        private void txtPvHeight_TextChanged(object sender, EventArgs e)
        {
            lblHeight2.Text = txtPvHeight.Text;
            rp.RoofPanelHeight = lblHeight2.Text;
        }

        private void cmbSolarPanelSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] a = cmbSolarPanelSize.Text.Split('X');
            txtPvHeight.Text = (Convert.ToDouble( a[0])/1000).ToString("0.000") ;
            txtPvWidth.Text = (Convert.ToDouble(a[1]) / 1000).ToString("0.000");
        }

        private void cmdSetDefault_Click(object sender, EventArgs e)
        {
            project.PvHeight = Convert.ToDouble(txtPvHeight.Text);
            project.PvWidth = Convert.ToDouble(txtPvWidth.Text);
        }

        private void cmdApplyAllPanel_Click(object sender, EventArgs e)
        {
            project.PvHeight = Convert.ToDouble(txtPvHeight.Text);
            project.PvWidth = Convert.ToDouble(txtPvWidth.Text);
            Michael.CreatePvPanel();
            /*
            List<IFeature> ls1 = new List<IFeature>();
            FeatureLayer fl1 = pvMap.Layers[project.LyrPole] as FeatureLayer;
            ISelection il1 = fl1.Selection;
            FeatureSet FeSet = il1.ToFeatureSet();


            for (int i = 0; i < FeSet.NumRows();i++ )
            {
                IFeature fs = FeSet.GetFeature(i);
                try
                {
                    fs.DataRow.BeginEdit();
                    fs.DataRow["H"] = Convert.ToDouble(txtPvHeight.Text);
                    fs.DataRow["W"] = Convert.ToDouble(txtPvWidth.Text);
                    fs.DataRow.EndEdit();
                }
                catch { }
            }
            */
            if (Project.LyrPole != -1)
            {
                IMapFeatureLayer pvPositionFe = PvMap.Layers[Project.LyrPole] as IMapFeatureLayer;
                List<IFeature> lstFe = new List<IFeature>();
                ISelection selFe = pvPositionFe.Selection;
                lstFe = selFe.ToFeatureList();
                int iRow = 0;
                foreach (IFeature fs in lstFe)
                {
                    try
                    {
                        fs.DataRow.BeginEdit();
                        fs.DataRow["h"] = Convert.ToDouble(txtPvHeight.Text);
                        fs.DataRow["W"] = Convert.ToDouble(txtPvWidth.Text);
                        fs.DataRow.EndEdit();
                        iRow++;
                    }
                    catch { }

                }
                this.Close();
            }
            MessageBox.Show("Data updated successfully");
            this.Close();
        }

        private void txtGridSpacingX_TextChanged(object sender, EventArgs e)
        {
            rp.RoofXSpace = txtGridSpacingX.Text;
        }

        private void txtGridSpacingY_TextChanged(object sender, EventArgs e)
        {
            rp.RoofYSpace = txtGridSpacingY.Text;
        }

        private void cmdCheck4PvOnRoof_CheckedChanged(object sender, EventArgs e)
        {
            rp.Orthogonal = cmdCheck4PvOnRoof.Checked;
        }
    }
}
