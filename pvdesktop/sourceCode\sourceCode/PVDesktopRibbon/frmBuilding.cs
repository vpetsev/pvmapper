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
    public partial class frmBuilding : Form
    {
        Map pvMap;
        PvDesktopUtilityFunction util = new PvDesktopUtilityFunction();
        PvDesktopProject project;
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
        int numExisting = 0;
        frm01_MainForm _michael;
        public frm01_MainForm Michael
        {
            get { return _michael; }
            set { _michael = value; }
        }
        public frmBuilding()
        {
            InitializeComponent();
        }

        private void frmBuilding_Load(object sender, EventArgs e)
        {
            if (ProjectFile.LyrBuilding != -1)
            {
                FeatureLayer FeLyr = PvMap.Layers[ProjectFile.LyrBuilding] as FeatureLayer;
                int numShape = FeLyr.DataSet.NumRows();
                ISelection selFe = FeLyr.Selection;
                FeatureSet Fe = selFe.ToFeatureSet();
                grdBldg.Columns.Clear();  
                int numSelShape = Fe.NumRows();
                grdBldg.DataSource = Fe.DataTable;
                lblRecordSel.Text = "Number of selected shapes are: " + numSelShape.ToString()+" of "+numShape.ToString();  
            }

        }

        private void cmdSetSameHeight_Click(object sender, EventArgs e)
        {
            try
            {
                double height = Convert.ToDouble(txtBuildingHeight.Text);
                for (int iRow = 0; iRow < grdBldg.RowCount ; iRow++)
                {
                    grdBldg.Rows[iRow].Cells["Height"].Value = height;
                }
            }
            catch 
            {
                MessageBox.Show("Building height data incorrect"); 
            }
        }

        private void cmdUpdateShape_Click(object sender, EventArgs e)
        {
            

            if (ProjectFile.LyrBuilding != -1)
            {
                IMapFeatureLayer blgdFe = PvMap.Layers[ProjectFile.LyrBuilding] as IMapFeatureLayer;
                List<IFeature> lstFe = new List<IFeature>();
                ISelection selFe = blgdFe.Selection;
                lstFe = selFe.ToFeatureList();
                int iRow = 0;
                foreach (IFeature fs in lstFe)
                {
                    try
                    {
                        fs.DataRow.BeginEdit();
                        fs.DataRow["Height"] = grdBldg.Rows[iRow].Cells["Height"].Value;
                        fs.DataRow["Remark"] = grdBldg.Rows[iRow].Cells["Remark"].Value;
                        fs.DataRow.EndEdit();
                        iRow++;
                    }
                    catch { }

                }
                blgdFe.Selection.Clear();  
                this.Close();
                Michael.drawBuildingShadow();
            }
        }
    }
}
