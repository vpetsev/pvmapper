using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PVDESKTOP
{
    public partial class Help : UserControl
    {
        public Help()
        {
            InitializeComponent();
        }

        private void listboxHelp_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCloseHelper_Click(object sender, EventArgs e)
        {
            container.Panel2Collapsed = true;
        }
         public SplitContainer container;

    }
}
