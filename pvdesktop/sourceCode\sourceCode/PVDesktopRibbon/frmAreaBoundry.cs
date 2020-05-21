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
    
    public partial class frmAreaBoundry : Form
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
        public frmAreaBoundry()
        {
            InitializeComponent();
        }

        private void btnSaveArea_Click(object sender, EventArgs e)
        {
            Michael.cmdCreatePvPole.Enabled = true;
            this.Close();
        }

        private void btnRedrawArea_Click(object sender, EventArgs e)
        {
          util.removeDupplicateLyr(Michael.prj.LyrSiteAreaName, pvMap);
          this.Close();
          Michael.DrawArea();
        }

        private void btnCancelArea_Click(object sender, EventArgs e)
        {
            util.removeDupplicateLyr(Michael.prj.LyrSiteAreaName, pvMap);
         this.Close();
        }

        private void btnAddAnotherArea_Click(object sender, EventArgs e)
        {
            int lyrIdx = util.getLayerHdl(txtAreaName.Text, pvMap);
            if (lyrIdx != -1)
            {
                MessageBox.Show("A site boundry with this name already exists, please enter a different name.", "Enter Unique Boundry Name");
                return;
            }
            Michael.DrawArea();

        }

        private void txtAreaName_TextChanged(object sender, EventArgs e)
        {
            project.LyrSiteAreaName = txtAreaName.Text;
        }

        private void frmAreaBoundry_Load(object sender, EventArgs e)
        {
            /*
            int lyrExists = 0;
            int i = 0;
            while (lyrExists != -1)
            {                 

                i++;
                lyrExists = util.getLayerHdl(txtAreaName.Text, pvMap);
            }
             */ 
            //txtAreaName.Text = txtAreaName.Text;
            project.LyrSiteAreaName = txtAreaName.Text;
          
        }
    }
}
