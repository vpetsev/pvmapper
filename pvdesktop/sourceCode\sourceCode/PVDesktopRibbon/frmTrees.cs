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
    public partial class frmTrees : Form
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
        double treeDismeter;
        public double TreeDismeter
        {
            get { return treeDismeter; }
            set { treeDismeter = value; }
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
        
        public frmTrees()
        {
            InitializeComponent();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void frmTrees_Load(object sender, EventArgs e)
        {
            if (ProjectFile.LyrTree != -1)
            {
                lblTreeDetail.Text = "Tree form: " + TreeType + ", H(" + treeHeight + " m.), D(" + TreeDismeter+" m.)";
                FeatureLayer FeLyr = PvMap.Layers[ProjectFile.LyrTree] as FeatureLayer;
                int numShape = FeLyr.DataSet.NumRows();
                ISelection selFe = FeLyr.Selection;
                FeatureSet Fe = selFe.ToFeatureSet();
                grdTreeAttribute.Columns.Clear();
                int numSelShape = Fe.NumRows();
                grdTreeAttribute.DataSource = Fe.DataTable;
                lblRecordSel.Text = "Number of selected shapes are: " + numSelShape.ToString() + " of " + numShape.ToString();
            }
        }

        private void cmdSetSameHeight_Click(object sender, EventArgs e)
        {
            try
            {
                for (int iRow = 0; iRow < grdTreeAttribute.RowCount; iRow++)
                {
                    grdTreeAttribute.Rows[iRow].Cells["Height"].Value = treeHeight;
                    grdTreeAttribute.Rows[iRow].Cells["Diameter"].Value = TreeDismeter;
                    grdTreeAttribute.Rows[iRow].Cells["Type"].Value = TreeType;
                }
            }
            catch{}
        }

        private void cmdUpdateShape_Click(object sender, EventArgs e)
        {

            if (ProjectFile.LyrTree != -1)
            {
                IMapFeatureLayer TreeFe = PvMap.Layers[ProjectFile.LyrTree] as IMapFeatureLayer;
                List<IFeature> lstFe = new List<IFeature>();
                ISelection selFe = TreeFe.Selection;
                lstFe = selFe.ToFeatureList();
                int iRow = 0;
                foreach (IFeature fs in lstFe)
                {
                    try
                    {
                        fs.DataRow.BeginEdit();
                        fs.DataRow["Height"] = grdTreeAttribute.Rows[iRow].Cells["Height"].Value;
                        fs.DataRow["Diameter"] = grdTreeAttribute.Rows[iRow].Cells["Diameter"].Value;
                        fs.DataRow["Type"] = grdTreeAttribute.Rows[iRow].Cells["Type"].Value;
                        fs.DataRow.EndEdit();
                        iRow++;
                        double Radius = Convert.ToDouble( fs.DataRow["Diameter"])/2;
                        //short numVertx = 36;
                        //Michael.kDrawTreeCircle(Convert.ToDouble(fs.DataRow['X']), Convert.ToDouble(fs.DataRow['Y']), Convert.ToDouble(fs.DataRow["Diameter"]), numVertx, pvMap, Color.Green);

                    }
                    catch { }

                }
                if (Michael.SolarObstuctionVerification())
                {
                    TreeFe.Selection.Clear();
                    this.Close();
                    Michael.DrawTreeShadow();
                }
                else 
                {
                    MessageBox.Show("There is an error in the edited properties.");
                }

            }

        }
  
    }
}
