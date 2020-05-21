using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace PVDESKTOP
{
    public partial class frm00_HomeForm : Form
    {
        public frm00_HomeForm()
        {
            InitializeComponent();
        }

        private void picBackToMainProgram_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void HomeForm_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
        }


    }
}
