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
    public partial class frmAddTree : Form
    {
        Map pvMap;
        PvDesktopUtilityFunction util = new PvDesktopUtilityFunction();
        PvDesktopProject project;
        int numExisting = 0;
        frm01_MainForm _michael;
        public frm01_MainForm Michael
        {
            get { return _michael; }
            set { _michael = value; }
        }

        string treeType;
        public string TreeType
        {
            get { return treeType; }
            set { treeType = value; }
        }

        double treeHeight;
        public double TreeHeight
        {
            get { return treeHeight; }
            set { treeHeight = value; }
        }

        double treeDiameter;
        public double TreeDiameter
        {
            get { return treeDiameter; }
            set { treeDiameter = value; }
        }

        double treeID;
        public double TreeID
        {
            get { return treeID; }
            set { treeID = value; }
        }

        internal PvDesktopProject ProjectFile
        {
            get { return project; }
            set { project = value; }
        }
        public Map PvMap
        {
            get { return pvMap; }
            set { pvMap = value; }
        }
        public frmAddTree()
        {
            InitializeComponent();
           // pvMap.MouseUp +=pvMap_MouseUp;
        }

        frm01_MainForm mainform = new frm01_MainForm();
        
        private void frmAddTree_Load(object sender, EventArgs e)
        {
            #region
            
            // saves the layers before they're edited so they can be restored if user clicks cancel on form-----------------------
            int lyr3 = util.getLayerHdl(Michael.prj.LyrTreeName, pvMap);
            if (lyr3 != -1)
            {
                
                FeatureSet Fe3 = pvMap.Layers[lyr3].DataSet as FeatureSet;
                Fe3.SaveAs(ProjectFile.Path + "Tree.shp", true);
                Michael.prj.LyrTreeName = "Tree";
               
            }
           
            #endregion



            double D = Convert.ToDouble(txtTreeDiameter.Text); //dVal = System.Convert.ToDouble(sVal)

            int LyrTree = util.getLayerHdl(Michael.prj.LyrTreeName, Michael.pvMap);
            if (LyrTree != -1)
            {
                FeatureSet Fe = Michael.pvMap.Layers[LyrTree].DataSet as FeatureSet;
                numExisting = Fe.DataTable.Rows.Count;
                //MessageBox.Show(numExisting.ToString());
                Fe = null;
            }

        }

        void treefucntion()
        {
            setTreeShape();
        }

        private void btnCancelDrawTree_Click(object sender, EventArgs e)
        {
           // MessageBox.Show(ProjectFile.Path);
            #region opens saved tree and shadow shapefiles
            util.removeDupplicateLyr(Michael.prj.LyrTreeName, PvMap);
            IFeatureSet Fe = FeatureSet.Open(ProjectFile.Path + "Tree.shp");
            Fe.Name = "Tree";
            pvMap.Layers.Add(Fe);
            Michael.prj.LyrTreeName = "Tree";
            util.ClearGraphicMap(pvMap);
            pvMap.MapFrame.Invalidate();
          

            #endregion

            this.Close();
        }

        double[] whRatio = new double[10];
        string[] treeTypeName = new string[10];

        String getTreeName()
        {
            String TreeName = "No Name";
            if (radTypeColumnar.Checked == true) TreeName = "Columnar";
            if (radTypeOpen.Checked == true) TreeName = "Open";
            if (radTypeIrregular.Checked == true) TreeName = "Irregular";
            if (radTypeWeeping.Checked == true) TreeName = "Weeping";
            if (radTypeVase.Checked == true) TreeName = "Vase";
            if (radTypeConical.Checked == true) TreeName = "Conical";
            if (radTypeOval.Checked == true) TreeName = "Oval";
            if (radTypePyramidal.Checked == true) TreeName = "Pyramidal";
            if (radTypeRound.Checked == true) TreeName = "Round";
            if (radTypeSpreading.Checked == true) TreeName = "Spreading";
            return TreeName;
        }

        void setTreeShape()
        {
            whRatio[0] = 1.21; treeTypeName[0] = "Speading shape";
            whRatio[1] = 1.84; treeTypeName[1] = "Round shape";
            whRatio[2] = 2.04; treeTypeName[2] = "Pyramidal shape";
            whRatio[3] = 3.91; treeTypeName[3] = "Oval shape";
            whRatio[4] = 5.52; treeTypeName[4] = "Conical shape";
            whRatio[5] = 1.56; treeTypeName[5] = "Vase shape";
            whRatio[6] = 12.5; treeTypeName[6] = "Columnar shape";
            whRatio[7] = 2.63; treeTypeName[7] = "Open shape";
            whRatio[8] = 2.29; treeTypeName[8] = "Weeping shape";
            whRatio[9] = 3.43; treeTypeName[9] = "Irrigular shape";
        }
        
        private void updateHeight()
        {
            if (util.NummericTextBoxCheck(txtTreeDiameter, "Tree diameter data", 1) == true)
                if (chkAutoHeight.Checked == false)
                {
                    try
                    {
                        setTreeShape();
                        txtTreeHeight.Text = (whRatio[Convert.ToInt32(TreeID)] * Convert.ToDouble(txtTreeDiameter.Text)).ToString();
                        treeHeight = Convert.ToDouble(txtTreeHeight.Text);

                    }
                    catch
                    {
                        txtTreeHeight.Text = "0";
                    }
                }
        }

        private void txtTreeDiameter_TextChanged(object sender, EventArgs e)
        {
            updateHeight();                        
        }

        #region radio button changes

        String TreeNameLabel = "Tree Type:";
        private void radTypeRound_CheckedChanged(object sender, EventArgs e)
        {
            if (radTypeRound.Checked == true)
            {
                treeID = 1;
                treeDiameter = Convert.ToDouble(txtTreeDiameter.Text);
                updateHeight();
                getTreeName();
                lblTreeName.Text = TreeNameLabel + getTreeName(); 
                
            }     
        }

        private void radTypePyramidal_CheckedChanged(object sender, EventArgs e)
        {
            if (radTypePyramidal.Checked == true)
            {
                treeID = 2;
                treeDiameter = Convert.ToDouble(txtTreeDiameter.Text);
                updateHeight();
                lblTreeName.Text = TreeNameLabel + getTreeName(); 
            }  
        }

        private void radTypeOval_CheckedChanged(object sender, EventArgs e)
        {
            if (radTypeOval.Checked == true)
            {
                treeID = 3;
                treeDiameter = Convert.ToDouble(txtTreeDiameter.Text);
                updateHeight();
                lblTreeName.Text = TreeNameLabel + getTreeName(); 
            }  
        }

        private void radTypeOpen_CheckedChanged(object sender, EventArgs e)
        {
            if (radTypeOpen.Checked == true)
            {
                treeID = 7;
                treeDiameter = Convert.ToDouble(txtTreeDiameter.Text);
                updateHeight();
                lblTreeName.Text = TreeNameLabel + getTreeName(); 
            }  
        }

        private void radTypeConical_CheckedChanged(object sender, EventArgs e)
        {
            if (radTypeConical.Checked == true)
            {
                treeID = 4;
                treeDiameter = Convert.ToDouble(txtTreeDiameter.Text);
                updateHeight();
                lblTreeName.Text = TreeNameLabel + getTreeName(); 
            }  
        }

        private void radTypeColumnar_CheckedChanged(object sender, EventArgs e)
        {
            if (radTypeColumnar.Checked == true)
            {
                treeID = 6;
                treeDiameter = Convert.ToDouble(txtTreeDiameter.Text);
                updateHeight();
                lblTreeName.Text = TreeNameLabel + getTreeName(); 
            }  
        }

        private void radTypeSpreading_CheckedChanged(object sender, EventArgs e)
        {
            if (radTypeSpreading.Checked == true)
            {
                treeID = 0;
                treeDiameter = Convert.ToDouble(txtTreeDiameter.Text);
                updateHeight();
                lblTreeName.Text = TreeNameLabel + getTreeName(); 
            }  
        }

        private void radTypeWeeping_CheckedChanged(object sender, EventArgs e)
        {
            if (radTypeWeeping.Checked == true)
            {
                treeID = 8;
                treeDiameter = Convert.ToDouble(txtTreeDiameter.Text);
                updateHeight();
                lblTreeName.Text = TreeNameLabel + getTreeName(); 
            }  
        }

        private void radTypeVase_CheckedChanged(object sender, EventArgs e)
        {
            if (radTypeVase.Checked == true)
            {
                treeID = 5;
                treeDiameter = Convert.ToDouble(txtTreeDiameter.Text);
                updateHeight();
                lblTreeName.Text = TreeNameLabel + getTreeName(); 
            }
        }

        private void radTypeIrregular_CheckedChanged(object sender, EventArgs e)
        {
            if (radTypeIrregular.Checked == true)
            {
                treeID = 9;
                treeDiameter = Convert.ToDouble(txtTreeDiameter.Text);
                updateHeight();
                lblTreeName.Text = TreeNameLabel + getTreeName(); 
            }
        }
        #endregion



        private void btnCloseDrawTree_Click(object sender, EventArgs e)
        {
            if (project.Path != "")
            {
                if (util.getLayerHdl("Solar Radiation Rose", pvMap) == -1)
                {
                    Michael.CentroidAsSite(project.LyrTreeName, pvMap);
                }
            }

            this.Close();
            Michael.EnableTreeEditing();
            //Michael.SolarObstuctionVerification();          
            //Draw tree shadows
            Michael.DrawTreeShadow();
            Michael.ExportBldgAndTrr2SketchUp.Enabled = true;

        }

        #region picture box clicks
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            radTypeRound.Checked = true;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            radTypePyramidal.Checked = true;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            radTypeOval.Checked = true;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            radTypeOpen.Checked = true;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            radTypeConical.Checked = true;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            radTypeColumnar.Checked = true;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            radTypeSpreading.Checked = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            radTypeWeeping.Checked = true;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            radTypeVase.Checked = true;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            radTypeIrregular.Checked = true;
        }
        #endregion





    }
}
