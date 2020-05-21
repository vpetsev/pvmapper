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
    public partial class frmPanelSelection : Form
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
        public frmPanelSelection()
        {
            InitializeComponent();
        }

        private void frmPanelSelection_Load(object sender, EventArgs e)
        {

        }

        private void btnPanelSelectionOK_Click(object sender, EventArgs e)
        {
            Michael.propertyGrid1.Refresh();
            //frmPanelSelection formPanelSel = new frmPanelSelection();
            //formPanelSel.Michael = this;
            frmPvPanelSetup formPVPanel = new frmPvPanelSetup();
            formPVPanel.Michael = Michael;
            formPVPanel.PvMap = pvMap;
            Michael.prj.LyrPole = util.getLayerHdl( Michael.prj.LyrPoleName,pvMap);
            //Michael.prj.LyrPole = util.getLayerHdl(Michael.cmbPolePosition.Text, pvMap);
            formPVPanel.Project = Michael.prj;            
            formPVPanel.Show();
            formPVPanel.TopMost = true;
             this.Close();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            
            //formPanelSel.Michael = this;
            frmPvPanelSetup formPVPanel = new frmPvPanelSetup();
            formPVPanel.Michael = Michael;
            formPVPanel.PvMap = pvMap;
            //Michael.prj.CmbPvPole = Michael.cmbPolePosition;
            Michael.prj.LyrPole = util.getLayerHdl(Michael.prj.LyrPoleName, pvMap);            
            formPVPanel.Project = Michael.prj;         
            formPVPanel.Show();
            formPVPanel.TopMost = true;
            this.Close();
        }

        private void frmPanelSelection_FormClosing(object sender, FormClosingEventArgs e)
        {
            //pvMap.FunctionMode = FunctionMode.None;
        }
    }
}
