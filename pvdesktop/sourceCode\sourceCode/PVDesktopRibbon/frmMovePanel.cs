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
    public partial class frmMovePanel : Form
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
        public frmMovePanel()
        {
            InitializeComponent();
        }

        private void btnSaveMove_Click(object sender, EventArgs e)
        {
            Michael.resetMapAct();
            this.Close();
        }

        private void btnCancelMove_Click(object sender, EventArgs e)
        {
            Michael.resetMapAct();
            this.Close();
        }
    }
}
