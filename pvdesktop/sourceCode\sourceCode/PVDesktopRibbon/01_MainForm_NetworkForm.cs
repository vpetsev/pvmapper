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
using Dijkstra.IO;
using Dijkstra.Common;
using System.Data.OleDb;
using System.Data;


namespace PVDESKTOP
{
    public partial class frm01_MainForm_NetworkForm : Form
    {
        Map mMap;
        List<int> DemandNode = new List<int>();
        List<int> AMPNode = new List<int>();
        List<int> nodeID = new List<int>();
        List<double> NodeX = new List<double>();
        List<double> NodeY = new List<double>();
        Hashtable hashNode = new Hashtable();
        Hashtable hashLink = new Hashtable();
        Hashtable hashOD = new Hashtable(); 
        int lastNodeID = 0;
        string LinkLyr;
        string DemandLyr;
        int nNode = 0;
        int nLink = 0;

        bool blPruneGraph = true;
        int iBenchmark = 1;
        //--------------------------------------------------
        private FileManager fm;
        private Algorithm dijkstra;
        private String lastOrigin;

        public frm01_MainForm_NetworkForm(frm01_MainForm _mMainForm)
        {
            InitializeComponent();
            mMap = _mMainForm.pvMap;
        }

        #region Utility
        double getFeaLength(IFeature ife)
        {
            double L = 0;
            for (int iPt = 0; iPt < ife.NumPoints - 1; iPt++)
            {
                double x1 = ife.Coordinates[iPt].X;
                double y1 = ife.Coordinates[iPt].Y;
                double x2 = ife.Coordinates[iPt + 1].X;
                double y2 = ife.Coordinates[iPt + 1].Y;
                L += Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
            }
            return L;
        }

        int getLayerID(Map tmpMap, string LayerName)
        {
            int iLayer = -1;
            int nLayer = mMap.Layers.Count;
            if (nLayer >= 1)
            {
                for (int i = 0; i < nLayer; i++)
                {
                    if (LayerName == mMap.Layers[i].LegendText) { iLayer = i; }
                }
            }
            return iLayer;
        }
        void ReadExistingNodeID()
        {
           OpenFileDialog openFileDialog1 = new OpenFileDialog();

           openFileDialog1.Filter = "Demand Node files (*.csv)|*.txt|All files (*.*)|*.*";
           openFileDialog1.FilterIndex = 2;
           openFileDialog1.RestoreDirectory = true;

           if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                hashNode.Clear();
                try
                {
                    lastNodeID = 0;
                    var reader = new StreamReader(File.OpenRead(openFileDialog1.FileName));
                    var line = reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        line = reader.ReadLine();
                        var values = line.Split(',');

                        int id = Convert.ToInt32(values[0]);
                        double x = Convert.ToDouble(values[1]);
                        double y = Convert.ToDouble(values[2]);
                        bool check = AddNode(x, y, id);
                        DemandNode.Add(id);
                        AMPNode.Add(Convert.ToInt32(values[3]));
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
           MessageBox.Show("Demand Node: " + lastNodeID.ToString() );  
        }
        
        int GetOD(string AMP_ID)
        {
            int id = -1;
            string key = AMP_ID;
            object hashValue = hashOD[key];
            if (hashValue == null) return -1;
            id = Convert.ToInt32(hashValue);
            return id;
        }

        int GetLink(int F, int T)
        {
            int id = -1;
            string key = F.ToString() + "-" + T.ToString();
            object hashValue = hashLink[key];
            if (hashValue==null)
            {
                key = T.ToString() + "-" + F.ToString();
                hashValue = hashLink[key];
            }
            if (hashValue == null) return -1;
            id = Convert.ToInt32(hashValue);
            return id;
        }
        int GetNode(double x, double y)
        {
            int id = -1;
            string key = Math.Round(x, 0).ToString() + "|" + Math.Round(y, 0).ToString();
            object hashValue = hashNode[key];
            if (hashValue == null) return -1;
            id = Convert.ToInt32(hashValue);
            return id;
        }
        bool AddNode(double x, double y, int id)
        {
            try
            {
                string key = Math.Round(x, 0).ToString() + "|" + Math.Round(y, 0).ToString();
                hashNode.Add(key, id);
                return true;
            }
            catch
            {
                return false;
            }
        }
        int newID(double x, double y)
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


        private void cmdCreateNetworkData_Click(object sender, EventArgs e)
        {

            //
            toolStripProgressBar1.Visible = true;
            int activeLyr = getLayerID(mMap, LinkLyr);
            grdLink.Rows.Clear();
            grdNode.Rows.Clear();
            int nLinkError = 0;
            List<int> LinkErr = new List<int>();
            txtLinkError.Clear(); 
            //try
            {
                //IMapFeatureLayer FeLyr = mMap.Layers[activeLyr] as IMapFeatureLayer;
                IFeatureSet FeLyr = mMap.Layers[activeLyr].DataSet as IFeatureSet;
                nLink = 0;
                int nShp = FeLyr.DataTable.Rows.Count;
                toolStripProgressBar1.Maximum = nShp; 
                for (int i = 0; i < nShp; i++)
                {
                    toolStripProgressBar1.Value = i;
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
                    double x1 = Math.Round(fs.Coordinates[0].X,0);
                    double y1 = Math.Round(fs.Coordinates[0].Y,0);
                    c[0] = new Coordinate(x1, y1);
                    double x2 = Math.Round(fs.Coordinates[fs.NumPoints - 1].X,0);
                    double y2 = Math.Round(fs.Coordinates[fs.NumPoints - 1].Y,0);
                    c[1] = new Coordinate(x2, y2);
                    //----------------------------------------------------------
                    //FeLyr.Features[i].Coordinates[0].X = x1;
                    //FeLyr.Features[i].Coordinates[0].Y = y1;
                    //FeLyr.Features[i].Coordinates[fs.NumPoints - 1].X = x2;
                    //FeLyr.Features[i].Coordinates[fs.NumPoints - 1].Y = y2;  
                    //----------------------------------------------------------
                    //object remark = fs.DataRow["remark"];

                    int F = -1;
                    int T = -1;
                    //Node From
                    if (GetNode(x1, y1) != -1)
                    {
                        F = GetNode(x1, y1);
                    }
                    else
                    {
                        F = newID(x1, y1);
                    }

                    //Node To
                    if (GetNode(x2, y2) != -1)
                    {
                        T = GetNode(x2, y2);
                    }
                    else
                    {
                        T = newID(x2, y2);
                    }
                    if(F!=T)
                    {
                        grdLink.Rows.Add();
                        grdLink.Rows[nLink].Cells[0].Value = i;
                        grdLink.Rows[nLink].Cells[1].Value = F;
                        grdLink.Rows[nLink].Cells[2].Value = T;
                        grdLink.Rows[nLink].Cells[3].Value = Math.Round(getFeaLength(fs), 2);
                        grdLink.Rows[nLink].Cells[4].Value = Math.Round(getFeaLength(fs), 2);
                        object lane = fs.DataRow["lane"];
                        try { grdLink.Rows[nLink].Cells[6].Value = Convert.ToInt16(lane); }
                        catch { grdLink.Rows[nLink].Cells[6].Value = "No data"; }
                        nLink++;
                    }else
                    {
                        nLinkError++;
                        LinkErr.Add(i);
                        txtLinkError.Text += i.ToString() + Environment.NewLine;
                    }
                    
                }
                if (nLinkError > 0)
                {
                    string st = "";
                    for (int i = 0; i < nLinkError; i++)
                    {
                        st = st + " "+ LinkErr[i].ToString();  
                    }
                    MessageBox.Show("No. of error link :" + nLinkError.ToString() + st);
   
                }
                //-----------------------------------------------------------------------------
                nNode = hashNode.Count;
                grdNode.Rows.Add(nNode);
                int ii=0;
                foreach (string xy in hashNode.Keys)
                {
                    var values = xy.Split('|');
                    grdNode.Rows[ii].Cells[0].Value =hashNode[xy];
                    grdNode.Rows[ii].Cells[1].Value = values[0];
                    grdNode.Rows[ii].Cells[2].Value = values[1];
                    bool DemandNodeChk = false;
                    for (int j = 0; j < DemandNode.Count; j++)
                    {
                        if (DemandNode[j].ToString() == grdNode.Rows[ii].Cells[0].Value.ToString() ) 
                        {  
                            grdNode.Rows[ii].Cells[3].Value ="DEMAND NODE";
                            grdNode.Rows[ii].Cells[4].Value=AMPNode[j].ToString();
                            DemandNodeChk = true;
                        } 
                    }
                    if (DemandNodeChk==false)
                    {
                        grdNode.Rows[ii].Cells[3].Value = "DUMMY NODE";
                        grdNode.Rows[ii].Cells[4].Value = "";
                    }
                        ii++;
                }
                this.grdNode.Sort(this.grdNode.Columns[0], ListSortDirection.Ascending);

                /*
                foreach (string value in hashNode.Values)
                {
                    grdNode.Rows[ii].Cells[1].Value = value;
                    ii++;
                }
                 */ 
                //-----------------------------------------------------------------------------
                
            }
            
            toolStripProgressBar1.Visible = false;
           
            int iDemandLyr = getLayerID(mMap, DemandLyr);
            IFeatureSet FeDemandLyr = mMap.Layers[iDemandLyr].DataSet as IFeatureSet;

            cmbO.Items.Clear();
            cmbD.Items.Clear();

            hashOD.Clear();
            int nDemand = FeDemandLyr.DataTable.Rows.Count;
            for (int i = 0; i < nDemand; i++)
            {
                //IFeature fs = FeDemandLyr.GetFeature(i);
                double x1 = FeDemandLyr.Features[i].Coordinates[0].X;
                double y1 = FeDemandLyr.Features[i].Coordinates[0].Y;
                object AMP_ID = FeDemandLyr.Features[i].DataRow["AMP_ID"];
                cmbO.Items.Add(AMP_ID);
                cmbD.Items.Add(AMP_ID);
                double x = 0;
                double y = 0;
                double min = 100000000000;
                double r = 1;
                int NodeID = -1;
                for (int j = 0; j < grdNode.RowCount; j++)
                {
                    double x2 = Convert.ToDouble(grdNode.Rows[j].Cells[1].Value);
                    double y2 = Convert.ToDouble(grdNode.Rows[j].Cells[2].Value);
                    r = Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2));
                    if (r < min)
                    {
                        min = r;
                        x = x2;
                        y = y2;
                        NodeID = j;
                    }
                }
                //fs.Coordinates[0].X = x;
                //fs.Coordinates[0].Y = y;
                grdNode.Rows[NodeID].Cells[3].Value = "Demand Node";
                grdNode.Rows[NodeID].Cells[4].Value = AMP_ID;
                hashOD.Add(AMP_ID, NodeID);
            }
            //FeDemandLyr.Save();
            this.fm = new FileManager();
            if (fm.readNetwork(grdLink))
            {
                MessageBox.Show("Load network complete");
                cmdPath.Enabled = true;
            }
            else
            {
                MessageBox.Show("Error load network incomplete");
                cmdPath.Enabled = false;
            }

        }

        private void cmdGetExtingNode_Click(object sender, EventArgs e)
        {
            //read existing node
            //ReadExistingNodeID();
        }

        private void frm01_MainForm_NetworkForm_Load(object sender, EventArgs e)
        {
            label1.Text = "Network layer: " + LinkLyr;
        }

        private void cmdSaveNetwork_Click(object sender, EventArgs e)
        {
            //OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
            //OleDbCommand command;
            // Create the UpdateCommand.
            //command = new OleDbCommand("UPDATE Parameter SET [Value] = " + nLink.ToString() + " WHERE [Param]='Number of Link';", GlobalVariables.NetworkDBConnection);
            //dataAdapter.UpdateCommand = command;
            //GlobalVariables.NetworkDBConnection.Close(); 
/*
            OleDbCommand cmdUpdateNoLink = new OleDbCommand("UPDATE Parameter SET [Value] = '"+nLink.ToString() +"' WHERE [parameter] = 'Number of Link';", GlobalVariables.NetworkDBConnection);
            dataAdapter.UpdateCommand = cmdUpdateNoLink;
            OleDbCommand cmdUpdateNoNode = new OleDbCommand("UPDATE Parameter SET [Value] = '" + nNode.ToString() + "' WHERE [parameter] = 'Number of Node';", GlobalVariables.NetworkDBConnection);
            dataAdapter.UpdateCommand = cmdUpdateNoNode;
  
 */
            //------------------------------------------------------------------------------            
            
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                
                
                
                using (StreamWriter writer = new StreamWriter(saveFileDialog1.FileName))
                {
                    string st = "";
                    
                    writer.WriteLine("LINK, " + nLink.ToString());
                    writer.WriteLine("Node, " + nNode.ToString());
                    //writer.WriteLine("Spatial_ID,From,To,Length,Speed,Lane,Capacity Per Lane, Alpha, Beta");
                    //---------------------------------------------------------------------
                    //Link Header
                    for (int i = 0; i < grdLink.ColumnCount; i++)
                    {
                        if (i < grdLink.Columns.Count - 1)
                        { st += grdLink.Columns[i].HeaderText + ", "; }
                        else
                        { st += grdLink.Columns[i].HeaderText; }
                    }
                    writer.WriteLine(st);
                    //Link data
                    for (int j = 0; j < nLink; j++)
                    {
                        st = "";
                        for (int i = 0; i < grdLink.ColumnCount; i++)
                        {
                            if (i < grdLink.Columns.Count - 1)
                            { st += grdLink.Rows[j].Cells[i].Value + ", "; }
                            else
                            { st += grdLink.Rows[j].Cells[i].Value; }
                        }
                        writer.WriteLine(st);
                    }
                    //---------------------------------------------------------------------
                    //Node Header
                    st = "";
                    for (int i = 0; i < grdNode.ColumnCount; i++)
                    {
                        if (i < grdNode.Columns.Count - 1)
                        { st += grdNode.Columns[i].HeaderText + ", "; }
                        else
                        { st += grdNode.Columns[i].HeaderText; }
                    }
                    writer.WriteLine(st);
                    //Link data
                    for (int j = 0; j < nNode; j++)
                    {
                        st = "";
                        for (int i = 0; i < grdNode.ColumnCount; i++)
                        {
                            if (i < grdNode.Columns.Count - 1)
                            { st += grdNode.Rows[j].Cells[i].Value + ", "; }
                            else
                            { st += grdNode.Rows[j].Cells[i].Value; }
                        }
                        writer.WriteLine(st);
                    }
                }// close file
            }//Save file dialog                
        }

        private void cmdPath_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();   
            if (fm.Ready)
            {

                String from = GetOD(cmbO.Text).ToString();
                String to = GetOD(cmbD.Text).ToString();
                dijkstra = new Algorithm(fm.Graph);
                LinkedList<Edge> result = dijkstra.findPath(from, to);
                if (result.Count > 0)
                {
                    int activeLyr = getLayerID(mMap, LinkLyr);
                    IFeatureSet FeLyr = mMap.Layers[activeLyr].DataSet as IFeatureSet;
                    nLink = 0;
                    int nShp = FeLyr.DataTable.Rows.Count;

                    System.Drawing.Color col = Color.Red;
                    mMap.MapFrame.DrawingLayers.Clear();

                    //  Feature f = new Feature(axisLines);
                    FeatureSet fs = new FeatureSet(FeatureType.Line);
                    foreach (Edge edge in result)
                    {
                        string sID = edge.Id.ToString();
                        int spIndex = Convert.ToInt32(edge.Id);
                        int spatialIndex = Math.Abs(spIndex);
                        listBox1.Items.Add(spIndex);
                        IFeature f = FeLyr.GetFeature(spatialIndex);
                        fs.Features.Add(f);
                    }
                    MapLineLayer rangeRingAxis;
                    rangeRingAxis = new MapLineLayer(fs);// MapPolygonLayer(fs);
                    rangeRingAxis.Symbolizer = new LineSymbolizer(col, 4);

                    mMap.MapFrame.DrawingLayers.Add(rangeRingAxis);

                    // Request a redraw
                    mMap.MapFrame.Invalidate();
                }
            }
            
            //---------------------------------------------------------------

            List<int> Links =new List<int>();
            for (int i = 0; i < Links.Count; i++)
            {
                //IFeature f = FeLyr.GetFeature(Links[i]);
                //fs.Features.Add(f);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.fm = new FileManager();
            if (fm.readNetwork(grdLink))
            {
                MessageBox.Show("Load network complete");
                cmdPath.Enabled = true; 
            }
            else
            {
                MessageBox.Show("Error load network incomplete");
                cmdPath.Enabled = false;  
            }
        }

        List<int> LinkSel(string st)
        {
            List<int> a= new List<int>();
            var values = st.Split(',');
            for (int i = 0; i < values.Length - 1; i++)
            {
                int id = GetLink(Convert.ToInt16(values[i]), Convert.ToInt16(values[i + 1]));
                a.Add(id);
            }             
            return a;
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }


        private void cmbO_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblAmpO.Text = "Node ID "+ GetOD(cmbO.Text).ToString() ; 
        }

        private void cmbD_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblAmpD.Text = "Node ID " + GetOD(cmbD.Text).ToString(); ;
        }

        void getAllDestinationAMP(string OStr)
        {
            var myAdapptor = new OleDbDataAdapter();
            OleDbCommand command = new OleDbCommand("SELECT DemndNodeID.Amp_NAME FROM DemndNodeID", GlobalVariables.ODDemandDBConnection);
            myAdapptor.SelectCommand = command;
            DataTable ODTable = new DataTable();
            myAdapptor.Fill(ODTable);
            cmbDestinationAmp.Items.Clear();
            foreach (DataRow _dr in ODTable.Rows)
            {
                cmbDestinationAmp.Items.Add(_dr["Amp_NAME"].ToString());
            }
            if (cmbDestinationAmp.Items.Count > 0) cmbDestinationAmp.Text = cmbDestinationAmp.Items[0].ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            getAllDestinationAMP("X");
        }

        private void cmdUpdateDB_Click(object sender, EventArgs e)
        {
            //------------------------------------------------------------------------------            
            GlobalVariables.NetworkDBConnection.Open();
            OleDbCommand cmd = GlobalVariables.NetworkDBConnection.CreateCommand();
            //---------- Update Number of node and  link 
            cmd.CommandText = @"UPDATE Parameter SET [Value] = " + nLink.ToString() + " WHERE [Param]='Number of Link'";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"UPDATE Parameter SET [Value] = " + nNode.ToString() + " WHERE [Param]='Number of Node'";
            cmd.ExecuteNonQuery();
            //-----------------------------------------
            //------ Clear Node and Link table
            cmd.CommandText = @"DELETE*FROM Node;";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"DELETE*FROM Link;";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"DELETE*FROM DEMAND_NODE;";
            cmd.ExecuteNonQuery();
            //----------------------------------------------           
            toolStripProgressBar1.Maximum = grdLink.RowCount - 1;
            toolStripProgressBar1.Visible = true;
            for (int i = 0; i < grdLink.RowCount - 1; i++)
            {
                toolStripProgressBar1.Value = i;
                string sData = "";
                for (int j = 0; j < grdLink.Columns.Count; j++)
                {
                    object a = grdLink.Rows[i].Cells[j].Value;
                    string b;
                    if (a != null)
                    { b = a.ToString(); }
                    else
                    { b = "-9999"; }
                    if (j == 0)
                    {
                        sData += b;
                    }
                    else
                    {
                        sData += "," + b;
                    }
                }
                cmd.CommandText = @"INSERT INTO Link VALUES (" + sData + ")";
                cmd.ExecuteNonQuery();
            }
            toolStripProgressBar1.Maximum = grdNode.RowCount - 1;
            //----------------------------------------------           
            for (int i = 0; i < grdNode.RowCount - 1; i++)
            {
                toolStripProgressBar1.Value = i;
                string sData = "";
                for (int j = 0; j < grdNode.Columns.Count; j++)
                {
                    object a = grdNode.Rows[i].Cells[j].Value;
                    string b;
                    if (a != null)
                    { b = a.ToString(); }
                    else
                    { b = "-"; }
                    if (j > 2) b = "'" + b + "'";
                    if (j == 0)
                    {
                        sData += b;
                    }
                    else
                    {
                        sData += "," + b;
                    }
                }
                cmd.CommandText = @"INSERT INTO NODE VALUES (" + sData + ")";
                cmd.ExecuteNonQuery();
                if (grdNode.Rows[i].Cells[3].Value == "Demand Node")
                {
                    string AMPID = grdNode.Rows[i].Cells[4].Value.ToString();
                    string NODEID = grdNode.Rows[i].Cells[0].Value.ToString();
                    cmd.CommandText = @"INSERT INTO DEMAND_NODE VALUES (" + AMPID + "," + NODEID + ")";
                    cmd.ExecuteNonQuery();
                }
            }
            toolStripProgressBar1.Visible = false;

            GlobalVariables.NetworkDBConnection.Close();
            MessageBox.Show("Network update complete"); 
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

    }
}
