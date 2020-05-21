using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Topology;
using DotSpatial.Symbology;
using System.IO;

namespace PVDESKTOP
{
    public partial class Form1 : Form
    {
        Map mMap;
        List<int> nodeID = new List<int>();
        List<double> NodeX = new List<double>();
        List<double> NodeY = new List<double>();
        Hashtable hashNode = new Hashtable();
        int lastNodeID = 0;

        public Form1(frm01_MainForm _mMainForm)
        {
            InitializeComponent();
            mMap = _mMainForm.pvMap;
        }

        private void cmdCountLayer_Click(object sender, EventArgs e)
        {
            MessageBox.Show(mMap.Layers.Count.ToString());  
        }

        private void cmdLoadedLayer_Click(object sender, EventArgs e)
        {
            int nLayer = mMap.Layers.Count;
            if (nLayer >= 1)
            {
                listBox1.Items.Clear();
                cmbProjectLyr.Items.Clear();
                cmbLinkLyr.Items.Clear();
                cmbNode.Items.Clear();
                for (int i = 0; i < nLayer; i++)
                {
                    if (mMap.Layers[i].LegendText != null)
                    {
                        listBox1.Items.Add(mMap.Layers[i].LegendText);
                        cmbProjectLyr.Items.Add(mMap.Layers[i].LegendText);
                        cmbLinkLyr.Items.Add(mMap.Layers[i].LegendText);
                        cmbNode.Items.Add(mMap.Layers[i].LegendText);
                    }
                }
                if (cmbProjectLyr.Items.Count >1 )
                { 
                    cmbProjectLyr.Text = mMap.Layers[0].LegendText;
                    cmbLinkLyr.Text = mMap.Layers[0].LegendText;
                    cmbNode.Text = mMap.Layers[0].LegendText;
                }
            }
            else
            {
                MessageBox.Show("Error"); 
            }
        }

        private void cmdGetLayer_Click(object sender, EventArgs e)
        {
            int activeLyr = getLayerID(mMap, cmbProjectLyr.Text);
            listBox2.Items.Clear(); 
            try 
            {
                IMapFeatureLayer FeLyr = mMap.Layers[activeLyr] as IMapFeatureLayer;
                int nShp = FeLyr.DataSet.NumRows() - 1;

                listBox2.Items.Add("Layer Type:" + FeLyr.GetType().ToString() );
                listBox2.Items.Add("Numner of shape:" + nShp.ToString());
            }
            catch { MessageBox.Show("Error! Please select a shapefile layer"); }
        }

        #region Utility
        double getFeaLength(IFeature ife)
        {
            double L = 0;
            for (int iPt = 0; iPt < ife.NumPoints-1; iPt++)
            {
                double x1 = ife.Coordinates[iPt].X;
                double y1 = ife.Coordinates[iPt].Y;
                double x2 = ife.Coordinates[iPt+1].X;
                double y2 = ife.Coordinates[iPt+1].Y;
                L += Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
            }
            return L;
        }

        int getLayerID(Map tmpMap,string LayerName)
        {
            int iLayer = -1;
            int nLayer = mMap.Layers.Count;
            if (nLayer >= 1)
            {
                for (int i = 0; i < nLayer; i++)
                {
                    if (LayerName == mMap.Layers[i].LegendText){ iLayer = i;}
                }
            }
            return iLayer;
        }
        void ReadExistingNodeID()
        {
            hashNode.Clear(); 
            try 
            {
                lastNodeID = 0;
                var reader = new StreamReader(File.OpenRead(@"C:\Users\Kasem\Dropbox\DOH_Project\NETWORK DATA\BUU_Net_V1.1\BUU NODE_World_coordinate.csv"));
                var line = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    var values = line.Split(',');

                       int id = Convert.ToInt32(values[0]);
                       double x = Convert.ToDouble(values[1]);
                       double y = Convert.ToDouble(values[2]);
                       bool check = AddNode(x, y, id);
                       if (lastNodeID < id) lastNodeID = id;
                       //hashNode.Add(values[3], values[0]); 
                }
            }
            catch 
            {
                MessageBox.Show("Existing Node data file not found, crate new node id");  
                lastNodeID = 0; hashNode.Clear(); 
            }
        }
        int GetNode(double x, double y)
        {
            int id = -1;
            string key = Math.Round(x, 1).ToString() + "|" + Math.Round(y, 1).ToString();
            object hashValue = hashNode[key];
            if (hashValue == null) return -1;
            id = Convert.ToInt32(hashValue);
            return id;
        }
        bool AddNode(double x, double y,int id)
        {
            try
            {
                string key = Math.Round(x, 1).ToString() + "|" + Math.Round(y, 1).ToString();
                hashNode.Add(key,id);
                return true;
            }
            catch 
            {
                return false; 
            }
        }
        int newID(double x,double y)
        {
            try
            {
                lastNodeID++;
                AddNode(x, y, lastNodeID);
                return lastNodeID; 
            }
            catch 
            { 
                lastNodeID--;
                return -1;            
            }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            //read existing node
            //ReadExistingNodeID();
            //
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            
            double dist = Convert.ToDouble(txtDistance.Text);
  
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog1.FileName))
                {
                    string st = "";
                    int activeLyr = getLayerID(mMap, cmbLinkLyr.Text);
                    grdLink.Rows.Clear();
                    //try
                    {
                        //IMapFeatureLayer FeLyr = mMap.Layers[activeLyr] as IMapFeatureLayer;
                        IFeatureSet FeLyr = mMap.Layers[activeLyr].DataSet as IFeatureSet;

                        int nShp = FeLyr.DataTable.Rows.Count;
                        for (int i = 0; i < nShp; i++)
                        {
                            IFeature fs = FeLyr.GetFeature(i);
                            /*
                            Coordinate[] c = new Coordinate[fs.NumPoints];
                            for (int iPt = 0; iPt < fs.NumPoints; iPt++)
                            {
                                double x1 = fs.Coordinates[iPt].X;
                                double y1 = fs.Coordinates[iPt].Y;
                                c[iPt] = new Coordinate(x1, y1);
                            }
                            */
                            Coordinate[] c = new Coordinate[2];
                            double x1 = fs.Coordinates[0].X;
                            double y1 = fs.Coordinates[0].Y;
                            c[0] = new Coordinate(x1, y1);
                            double x2 = fs.Coordinates[fs.NumPoints - 1].X;
                            double y2 = fs.Coordinates[fs.NumPoints - 1].Y;
                            c[1] = new Coordinate(x2, y2);
                            //object remark = fs.DataRow["remark"];

                            grdLink.Rows.Add();
                            grdLink.Rows[i].Cells[0].Value = i;

                            //Node From
                            bool demandNode = false;
                            double minR = 1000000000000;
                            int iiSel = -1;
                            for (int ii = 0; ii < grdDemand.RowCount; ii++)
                            {
                                double x = Convert.ToDouble(grdDemand.Rows[ii].Cells[1].Value);
                                double y = Convert.ToDouble(grdDemand.Rows[ii].Cells[2].Value);
                                double r = Math.Sqrt(Math.Pow(x1 - x, 2) + Math.Pow(y1 - y, 2));
                                if (r < minR)
                                {
                                    iiSel = ii;
                                    minR = r;
                                }
                            }
                            if (demandNode = true & minR <= dist)
                            {
                                grdLink.Rows[i].Cells[1].Value = grdDemand.Rows[iiSel].Cells[0].Value;
                                grdDemand.Rows[iiSel].Cells[1].Value = x1;
                                grdDemand.Rows[iiSel].Cells[2].Value = y1;
                                writer.WriteLine(grdDemand.Rows[iiSel].Cells[0].Value.ToString() + ", " + x1.ToString() + ", " + y1.ToString() + ", " + grdDemand.Rows[iiSel].Cells[3].Value.ToString());
                            }
                            else
                            {
                                if (GetNode(x1, y1) != -1)
                                {
                                    grdLink.Rows[i].Cells[1].Value = GetNode(x1, y1);
                                }
                                else
                                {
                                    grdLink.Rows[i].Cells[1].Value = newID(x1, y1);
                                }
                            }
                            //Node To
                            demandNode = false;
                            minR = 1000000000000;
                            iiSel = -1;
                            for (int ii = 0; ii < grdDemand.RowCount; ii++)
                            {
                                double x = Convert.ToDouble(grdDemand.Rows[ii].Cells[1].Value);
                                double y = Convert.ToDouble(grdDemand.Rows[ii].Cells[2].Value);
                                double r = Math.Sqrt(Math.Pow(x2 - x, 2) + Math.Pow(y2 - y, 2));
                                if (r < minR)
                                {
                                    iiSel = ii;
                                    minR = r;
                                    demandNode = true;
                                }
                            }
                            if (demandNode = true & minR <= dist)
                            {
                                grdLink.Rows[i].Cells[2].Value = grdDemand.Rows[iiSel].Cells[0].Value;
                                grdDemand.Rows[iiSel].Cells[1].Value = x2;
                                grdDemand.Rows[iiSel].Cells[2].Value = y2;
                                writer.WriteLine(grdDemand.Rows[iiSel].Cells[0].Value.ToString() + ", " + x2.ToString() + ", " + y2.ToString() + ", " + grdDemand.Rows[iiSel].Cells[3].Value.ToString());
                            }
                            else
                            {
                                if (GetNode(x2, y2) != -1)
                                {
                                    grdLink.Rows[i].Cells[2].Value = GetNode(x2, y2);
                                }
                                else
                                {
                                    grdLink.Rows[i].Cells[2].Value = newID(x2, y2);
                                }
                            }
                            grdLink.Rows[i].Cells[3].Value = getFeaLength(fs);

                        }
                    }
                    //catch { MessageBox.Show("Error! Please select a line shape layer"); }
                }
            }
        }

        private void cmdGetSelection_Click(object sender, EventArgs e)
        {
            int activeLyr = getLayerID(mMap, cmbLinkLyr.Text);
            grdSelection.Rows.Clear(); 
            if(activeLyr !=-1)
            {             
                grdSelection.Rows.Clear();
                List<IFeature> ls1 = new List<IFeature>();
                FeatureLayer fl1 = mMap.Layers[activeLyr] as FeatureLayer;
                ISelection il1 = fl1.Selection;
                IFeatureSet iFe = il1.ToFeatureSet();
                grdSelection.DataSource = iFe.DataTable; 
               /*
                ls1 = il1.ToFeatureList();
                this.Text = "";
                for (int i = 0; i < il1.Count; i++)
                {
                    this.Text += ls1[i].Fid.ToString() + " , "; //FID
                    this.Text += ls1[i].DataRow.ItemArray.GetValue(0).ToString() + " , "; //attribute 

                }
                */ 
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int activeLyr = getLayerID(mMap, cmbNode.Text);
            int linkLyr = getLayerID(mMap, cmbLinkLyr.Text);
            grdDemand.Rows.Clear();
            //try
            {
                //IMapFeatureLayer FeLyr = mMap.Layers[activeLyr] as IMapFeatureLayer;
                IFeatureSet FeLyr = mMap.Layers[activeLyr].DataSet as IFeatureSet;
                IFeatureSet FeLinkLyr = mMap.Layers[linkLyr].DataSet as IFeatureSet;
                 
                int nShp = FeLyr.DataTable.Rows.Count;
                for (int i = 0; i < nShp; i++)
                {
                    IFeature fs = FeLyr.GetFeature(i);
                    /*
                    Coordinate[] c = new Coordinate[fs.NumPoints];
                    for (int iPt = 0; iPt < fs.NumPoints; iPt++)
                    {
                        double x1 = fs.Coordinates[iPt].X;
                        double y1 = fs.Coordinates[iPt].Y;
                        c[iPt] = new Coordinate(x1, y1);
                    }
                    */
                    double x1 = fs.Coordinates[0].X;
                    double y1 = fs.Coordinates[0].Y;
                    object remark = fs.DataRow["AMP_ID"];

                    grdDemand.Rows.Add();
                    grdDemand.Rows[i].Cells[0].Value = i;
                    grdDemand.Rows[i].Cells[1].Value = x1;
                    grdDemand.Rows[i].Cells[2].Value = y1;
                    grdDemand.Rows[i].Cells[3].Value = remark.ToString();

                    double x=0;
                    double y=0;
                    double min = 100000000000;
                    double r = 1;
                    int linkID = -1;
                    int nLink = FeLinkLyr.DataTable.Rows.Count;
                    for (int j = 0; j < nLink; j++)
                    {
                        IFeature fsLink = FeLinkLyr.GetFeature(j);
                        int lastVertex = fsLink.NumPoints - 1;
                        double x2 = fsLink.Coordinates[0].X;
                        double y2 = fsLink.Coordinates[0].Y;
                        double x3 = fsLink.Coordinates[lastVertex].X;
                        double y3 = fsLink.Coordinates[lastVertex].Y;
                        r = Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2));
                        if (r < min)
                        { 
                            min=r;
                            x = x2;
                            y = y2;
                            linkID = j;
                        }
                        r = Math.Sqrt(Math.Pow((x1 - x3), 2) + Math.Pow((y1 - y3), 2));
                        if (r < min)
                        {
                            min=r;
                            x = x3;
                            y = y3;
                            linkID = j;
                        }
                    }
                    grdDemand.Rows[i].Cells[4].Value = linkID;
                    grdDemand.Rows[i].Cells[5].Value = x;
                    grdDemand.Rows[i].Cells[6].Value = y;
                    //-move demand point
                    Coordinate c = new Coordinate(x,y);
                    fs.Coordinates[0].X = x;
                    fs.Coordinates[0].Y = y;
                }
                //FeLyr.Save(); 
            }

        }
    }
}
