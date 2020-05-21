using pvMapperPlugin;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using kGEOClassLibrary;
using DotSpatial.Topology;
using DotSpatial.Data;
using DotSpatial.Controls;
using DotSpatial.Symbology;

namespace PvMapperPlugin
{
    /// <summary>
    /// MeasureDialog
    /// </summary>
    public class frmLayoutDialog : Form
    {
        private double _areaIntoSquareMeters;
        private int _areaUnitIndex;
        private double _distIntoMeters;
        private double _distance;
        private int _distanceUnitIndex;
        private LayoutMode _measureMode;
        private double _totalArea;
        private double _totalDistance;
        private Coordinate _BaselineVertex;
        private ComboBox cmbUnits;
        private Label label1;
        private Label label2;
        private Label lblMeasure;
        private Label lblPartialValue;
        private Label lblTotalUnits;
        private Label lblTotalValue;
        private ToolTip ttHelp;

        public IMap pvMap { get; set; }
        public IMap pvMap4Draw
        {
            get { return vMap; }
            set
            {
                vMap = (Map)value;
            }
        }

        public Map vMap; 

        #region Private Variables

        private double[] _areaUnitFactors = new[]
                                                {
                                                    1E-6, 0.0001, 1, .01, 3.86102159E-7, 0.000247105381, 10.7639104,
                                                    1.19599005
                                                };

        private string[] _areaUnitNames = new[]
                                              {
                                                  "Square Kilometers", "Hectares", "Square Meters", "Ares", "Square Miles",
                                                  "Acres", "Square Feet", "Square Yards" };

        private double[] _distanceUnitFactors = new[]
                                                    {
                                                        .001, 1, 10, 100, 1000,
                                                        0.000621371192, 0.000539956803, 1.0936133, 3.2808399, 39.3700787, 8.983152098E-6
                                                    };

        private string[] _distanceUnitNames = new[]
                                                  {
                                                      "Kilometers", "Meters", "Decimeters", "Centimeters", "Millimeters",
                                                      "Miles", "NauticalMiles", "Yards", "Feet", "Inches", "DecimalDegrees"
                                                  };

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;
        private Button CmdBaseLine;
        private Button button1;
        private CheckBox ChkDistance;
        private CheckBox ChkAzimuth;
        private TextBox txtDistance;
        private TextBox txtAzimuth;
        private Label label4;
        private Button cmdCreateShapefile;
        private CheckBox chkOrtho;
        private Button button2;

        #endregion

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLayoutDialog));
            this.ttHelp = new System.Windows.Forms.ToolTip(this.components);
            this.lblPartialValue = new System.Windows.Forms.Label();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.cmbUnits = new System.Windows.Forms.ComboBox();
            this.CmdBaseLine = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cmdCreateShapefile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMeasure = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTotalUnits = new System.Windows.Forms.Label();
            this.ChkDistance = new System.Windows.Forms.CheckBox();
            this.ChkAzimuth = new System.Windows.Forms.CheckBox();
            this.txtDistance = new System.Windows.Forms.TextBox();
            this.txtAzimuth = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkOrtho = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblPartialValue
            // 
            resources.ApplyResources(this.lblPartialValue, "lblPartialValue");
            this.lblPartialValue.Name = "lblPartialValue";
            this.ttHelp.SetToolTip(this.lblPartialValue, resources.GetString("lblPartialValue.ToolTip"));
            // 
            // lblTotalValue
            // 
            resources.ApplyResources(this.lblTotalValue, "lblTotalValue");
            this.lblTotalValue.Name = "lblTotalValue";
            this.ttHelp.SetToolTip(this.lblTotalValue, resources.GetString("lblTotalValue.ToolTip"));
            // 
            // cmbUnits
            // 
            this.cmbUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnits.FormattingEnabled = true;
            resources.ApplyResources(this.cmbUnits, "cmbUnits");
            this.cmbUnits.Name = "cmbUnits";
            this.ttHelp.SetToolTip(this.cmbUnits, resources.GetString("cmbUnits.ToolTip"));
            this.cmbUnits.SelectedIndexChanged += new System.EventHandler(this.cmbUnits_SelectedIndexChanged);
            // 
            // CmdBaseLine
            // 
            resources.ApplyResources(this.CmdBaseLine, "CmdBaseLine");
            this.CmdBaseLine.Name = "CmdBaseLine";
            this.ttHelp.SetToolTip(this.CmdBaseLine, resources.GetString("CmdBaseLine.ToolTip"));
            this.CmdBaseLine.UseVisualStyleBackColor = true;
            this.CmdBaseLine.Click += new System.EventHandler(this.CmdBaseLine_Click);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.ttHelp.SetToolTip(this.button1, resources.GetString("button1.ToolTip"));
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.cmdDrawPv_Click);
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.ttHelp.SetToolTip(this.button2, resources.GetString("button2.ToolTip"));
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // cmdCreateShapefile
            // 
            resources.ApplyResources(this.cmdCreateShapefile, "cmdCreateShapefile");
            this.cmdCreateShapefile.Name = "cmdCreateShapefile";
            this.ttHelp.SetToolTip(this.cmdCreateShapefile, resources.GetString("cmdCreateShapefile.ToolTip"));
            this.cmdCreateShapefile.UseVisualStyleBackColor = true;
            this.cmdCreateShapefile.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lblMeasure
            // 
            resources.ApplyResources(this.lblMeasure, "lblMeasure");
            this.lblMeasure.Name = "lblMeasure";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // lblTotalUnits
            // 
            this.lblTotalUnits.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblTotalUnits, "lblTotalUnits");
            this.lblTotalUnits.Name = "lblTotalUnits";
            // 
            // ChkDistance
            // 
            resources.ApplyResources(this.ChkDistance, "ChkDistance");
            this.ChkDistance.Name = "ChkDistance";
            this.ChkDistance.UseVisualStyleBackColor = true;
            // 
            // ChkAzimuth
            // 
            resources.ApplyResources(this.ChkAzimuth, "ChkAzimuth");
            this.ChkAzimuth.Name = "ChkAzimuth";
            this.ChkAzimuth.UseVisualStyleBackColor = true;
            // 
            // txtDistance
            // 
            resources.ApplyResources(this.txtDistance, "txtDistance");
            this.txtDistance.Name = "txtDistance";
            // 
            // txtAzimuth
            // 
            resources.ApplyResources(this.txtAzimuth, "txtAzimuth");
            this.txtAzimuth.Name = "txtAzimuth";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // chkOrtho
            // 
            resources.ApplyResources(this.chkOrtho, "chkOrtho");
            this.chkOrtho.Name = "chkOrtho";
            this.chkOrtho.UseVisualStyleBackColor = true;
            // 
            // frmLayoutDialog
            // 
            this.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.chkOrtho);
            this.Controls.Add(this.txtAzimuth);
            this.Controls.Add(this.txtDistance);
            this.Controls.Add(this.ChkAzimuth);
            this.Controls.Add(this.ChkDistance);
            this.Controls.Add(this.cmdCreateShapefile);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CmdBaseLine);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblTotalUnits);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbUnits);
            this.Controls.Add(this.lblTotalValue);
            this.Controls.Add(this.lblPartialValue);
            this.Controls.Add(this.lblMeasure);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLayoutDialog";
            this.ShowIcon = false;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmLayoutDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of MeasureDialog
        /// </summary>
        public frmLayoutDialog()
        {
            InitializeComponent();
            _measureMode = LayoutMode.Distance;
            cmbUnits.Items.AddRange(_distanceUnitNames);
            cmbUnits.SelectedIndex = 1;
            _distanceUnitIndex = 1;
            _areaUnitIndex = 2;
            _distIntoMeters = 1;
            _areaIntoSquareMeters = 1;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the distance in meters of just one segment
        /// </summary>
        public double Distance
        {
            get { return _distance; }
            set
            {
                _distance = value;
                if (_measureMode == LayoutMode.Distance)
                {
                    lblPartialValue.Text = (_distance * _distIntoMeters).ToString("#, ###");
                    
                }
            }
        }

        /// <summary>
        /// The total distance across all segments in meters
        /// </summary>
        public double TotalDistance
        {
            get { return _totalDistance; }
            set
            {
                _totalDistance = value;
                if (_measureMode == LayoutMode.Distance)
                {
                    lblTotalValue.Text = (_totalDistance * _distIntoMeters).ToString("#, ###");
                }
            }
        }
        /*
        public Coordinate BaselineVertex
        {
            get { return _BaselineVertex; }
            set
            {
                _BaselineVertex = value;
                if (_measureMode == LayoutMode.Distance)
                {
                  
                    this.grdBaselineLayout.Rows.Add();
                    int n = this.grdBaselineLayout.Rows.Count - 2;
                    this.grdBaselineLayout.Rows[n].Cells[0].Value = n;
                    this.grdBaselineLayout.Rows[n].Cells[1].Value = value.X;
                    this.grdBaselineLayout.Rows[n].Cells[2].Value = value.Y;
                   
                }
            }
        }
        */
        /// <summary>
        /// Gets or sets the total area in square meters
        /// </summary>
        public double TotalArea
        {
            get { return _totalArea; }
            set
            {
                _totalArea = value;
                lblTotalValue.Text = (_totalArea * _areaIntoSquareMeters).ToString("#, ###");
            }
        }

        #endregion

        /// <summary>
        /// Gets or sets whether to display the distances or areas.
        /// </summary>
        public LayoutMode MeasureMode
        {
            get { return _measureMode; }
            set
            {
                _measureMode = value;
            }
        }

        /// <summary>
        /// Occurs when the measuring mode has been changed.
        /// </summary>
        public event EventHandler MeasureModeChanged;

        /// <summary>
        /// Occurs when the clear button has been pressed.
        /// </summary>
        public event EventHandler MeasurementsCleared;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        private void OnMeasureModeChanged()
        {
            if (MeasureModeChanged != null) MeasureModeChanged(this, new EventArgs());
        }

        private void cmbUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MeasureMode == LayoutMode.Distance)
            {
                _distIntoMeters = _distanceUnitFactors[cmbUnits.SelectedIndex];
            }
            else
            {
                _areaIntoSquareMeters = _areaUnitFactors[cmbUnits.SelectedIndex];
            }
            lblTotalUnits.Text = cmbUnits.Text;
            lblPartialValue.Text = String.Empty;
            lblTotalValue.Text = String.Empty;
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            if (MeasurementsCleared != null) MeasurementsCleared(this, EventArgs.Empty);
            pvMap4Draw.MapFrame.DrawingLayers.Clear();
            //grdBaselineLayout.Rows.Clear(); 
        }

        private void CmdBaseLine_Click(object sender, EventArgs e)
        {
            //this.grdBaselineLayout.Rows.Clear();
            pvMap4Draw.MapFrame.DrawingLayers.Clear();
            if (MeasurementsCleared != null) MeasurementsCleared(this, EventArgs.Empty);
            if (_measureMode != LayoutMode.Distance)
            {
                MeasureMode = LayoutMode.Distance;
                _areaUnitIndex = cmbUnits.SelectedIndex;
                cmbUnits.SuspendLayout();
                cmbUnits.Items.Clear();
                cmbUnits.Items.AddRange(_distanceUnitNames);
                cmbUnits.SelectedIndex = _distanceUnitIndex;
                cmbUnits.ResumeLayout();
                OnMeasureModeChanged();
                Text = "Draw baseline";
                lblMeasure.Text = "Baseline";
            }
        }

        private void cmdDrawPv_Click(object sender, EventArgs e)
        {
            kGeoFunc.LineType BL = new kGeoFunc.LineType();
            kGeoFunc.LineType dL = new kGeoFunc.LineType();
            kGeoFunc.CircleType CL = new kGeoFunc.CircleType();
            double dx = 10.0; //m.
            //  Feature f = new Feature(axisLines);
            IFeatureSet fs ;//= new FeatureSet(FeatureType.Line);
            fs = new FeatureSet(FeatureType.Polygon);
           /*
            for (int n = 0; n < grdBaselineLayout.Rows.Count - 2; n++)
            {
                BL.Pt1.X = (double)this.grdBaselineLayout.Rows[n].Cells[1].Value;
                BL.Pt1.Y = (double)this.grdBaselineLayout.Rows[n].Cells[2].Value;
                BL.Pt2.X = (double)this.grdBaselineLayout.Rows[n+1].Cells[1].Value;
                BL.Pt2.Y = (double)this.grdBaselineLayout.Rows[n+1].Cells[2].Value;
                double L = kGeoFunc.Length(BL);
                //DrawLine(BL.Pt1.X, BL.Pt1.Y, BL.Pt2.X, BL.Pt2.Y, 3, System.Drawing.Color.Magenta);

                for (double r = dx; r < L-dx/2; r += dx)
                { 
                    //Todo: add PV array
                    CL.pt.X = BL.Pt1.X;
                    CL.pt.Y = BL.Pt1.Y;
                    CL.R = r;
                    double x1 = 0; double y1 = 0;
                    double x2 = 0; double y2 = 0;
                    //dL.Pt1.X = BL.Pt1.X;
                    double xt1 = 0; double yt1 = 0;
                    double xt2 = 0; double yt2 = 0;
                    double xt3 = 0; double yt3 = 0;
                    double xt4 = 0; double yt4 = 0;
                    //dL.Pt1.Y = BL.Pt1.Y;
                    CL.R = r - (dx / 2 - 0.5);
                    int mm = kGeoFunc.Line_Circle(BL, CL, ref xt1, ref yt1, ref xt2, ref yt2);
                    CL.R = r + (dx / 2 - 0.5);
                    int nn = kGeoFunc.Line_Circle(BL, CL, ref xt3, ref yt3, ref xt4, ref yt4);
                    //---------------------------------------------------
                    dL.Pt1.X = xt1;
                    dL.Pt1.Y = yt1;
                    dL.Pt2.X = xt3;
                    dL.Pt2.Y = yt3;
                    //---------------------------------------------------
                   
                    CL.R = r;
                    if (kGeoFunc.Line_Circle(BL, CL, ref x1, ref y1, ref x2, ref y2) >= 1)
                    {
                        kGeoFunc.POINTAPI pt1; 
                        kGeoFunc.POINTAPI pt2;
                        pt1.X = x1;
                        pt1.Y = y1;
                        pt2.X = x2;
                        pt2.Y = y2;
                        double dy = 60.0;
                        kGeoFunc.LineType Lt =kGeoFunc.Perpend2Point(BL, x1, y1, dy);
                        //Todo: add PV array
                        kGeoFunc.CircleType CLL = new kGeoFunc.CircleType();
                        CLL.pt.X = Lt.Pt1.X;
                        CLL.pt.Y = Lt.Pt1.Y;
                        double w = 25;
                        for (double r2 = 0; r2 <= dy; r2 += w)
                        {
                            CLL.R = r2;
                            if (kGeoFunc.Line_Circle(Lt, CLL, ref x1, ref y1, ref x2, ref y2) >= 1)
                            {
                                fs.Features.Add(pvPanel(w, x1, y1, dL));
                            }
                        }
                        //---------------------------------------------------
                        //DrawLine(Lt.Pt1.X, Lt.Pt1.Y, Lt.Pt2.X, Lt.Pt2.Y, 1, System.Drawing.Color.Red);
                        //Coordinate[] Lshape = new Coordinate[2]; //x-axis
                        //x1 = Lt.Pt1.X;
                        //y1 = Lt.Pt1.Y;
                        //x2 = Lt.Pt1.X;
                        //y2 = Lt.Pt1.Y;
                        //Lshape[0] = new Coordinate(x1, y1);
                        //Lshape[1] = new Coordinate(x2, y2);

                        //LineString ls = new LineString(Lshape);
                        //creates a feature from the linestring
                        // Feature f = new Feature(ls);
                        // fs.Features.Add(f);
                    }
                    
                }
            }
            */
            fs.Projection = pvMap4Draw.Projection;
            //MessageBox.Show(fea.ToString);  
            fs.Name = "pv Array";
            //pvMap4Draw.Layers.Add(fs);
            fs.SaveAs("..\\pvArray.shp", true);
            pvMap4Draw.MapFrame.DrawingLayers.Clear();
            MapPolygonLayer rangeRingAxis;
            rangeRingAxis = new MapPolygonLayer(fs);// MapPolygonLayer(fs);
            //rangeRingAxis.Symbolizer = new PolygonSymbolizer(System.Drawing.Color.AliceBlue, System.Drawing.Color.LightBlue);
            pvMap4Draw.MapFrame.DrawingLayers.Add(rangeRingAxis);
            pvMap4Draw.MapFrame.Invalidate();
        }
        
        IPolygon pvPanel(double w,double x0,double y0, kGeoFunc.LineType BL)
        {
            BL.Pt2.X = (BL.Pt2.X - BL.Pt1.X);
            BL.Pt2.Y = (BL.Pt2.Y - BL.Pt1.Y);
            BL.Pt1.X =  0;
            BL.Pt1.Y =  0;
            //kGeoFunc.CircleType CL1 = new kGeoFunc.CircleType();
            //kGeoFunc.CircleType CL2 = new kGeoFunc.CircleType();
            kGeoFunc.LineType L1 = kGeoFunc.Perpend2Point(BL, BL.Pt1.X, BL.Pt1.Y, w-2.5);
            kGeoFunc.LineType L2 = kGeoFunc.Perpend2Point(BL, BL.Pt2.X, BL.Pt2.Y, w-2.5);
            //int n = kGeoFunc.Line_Circle(L1, CL1, ref x1, ref y1, ref x2, ref y2);
            //int m = kGeoFunc.Line_Circle(L2, CL2, ref x1, ref y1, ref x2, ref y2);
            Coordinate[] Lshape = new Coordinate[5]; //x-axis

            Lshape[0] = new Coordinate(BL.Pt1.X + x0, BL.Pt1.Y + y0);
            Lshape[1] = new Coordinate(L1.Pt1.X + x0, L1.Pt1.Y + y0);
            Lshape[2] = new Coordinate(L2.Pt1.X + x0, L2.Pt1.Y + y0);
            Lshape[3] = new Coordinate(BL.Pt2.X + x0, BL.Pt2.Y + y0);
            Lshape[4] = new Coordinate(BL.Pt1.X + x0, BL.Pt1.Y + y0);
            
            // Polygon Featture
            //creates a polygon feature from the list of coordinate
            IPolygon fe = new Polygon(Lshape);
            
            // Line Feature
            //LineString ls = new LineString(Lshape); //  for line shape
            //creates a feature from the linestring
            // fe = new Polygon(ls); // for line shape
 
            return fe;
        }
        private void frmLayoutDialog_Load(object sender, EventArgs e)
        {

        }

        public bool chkDistance 
        { 
            get
            {
                return this.ChkDistance.Checked; 
            }  
            set{}
        }
        public bool chkOrthonality
        {
            get
            {
                return this.chkOrtho.Checked;
            }
            set { }
        }

    }
}