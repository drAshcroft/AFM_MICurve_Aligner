using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
using Accord.MachineLearning;
using Accord.Math;

namespace GroupCurves
{
    public partial class SingleCurveComp : Form
    {
        public GroupBin SourceBin;
        public List<GroupBin> OutBins = new List<GroupBin>();
        public GroupBin TrashBin;
        public GroupBins AllGroups;
        Form1 MainForm;
        int CurrentSelectorCurve = 0;
        CurveInfo[,] CurveMatrix;

        private readonly Color[] QColors =        {    Color.Blue, Color.Red, Color.YellowGreen, Color.SeaGreen, Color.LightBlue,   Color.Yellow, Color.Purple, Color.Pink, Color.Orange, Color.Firebrick        };
        
        public SingleCurveComp()
        {
            InitializeComponent();
        }

        public void LoadCurves(Form1 MainForm, GroupBin SortBin, GroupBins AllGroups, GroupBins TrashBins)
        {
            this.MainForm = MainForm;
            this.AllGroups = AllGroups;
            SourceBin = SortBin;
            TrashBin = new GroupBin(AllGroups.GetNextBinNumber());
           
            TrashBins.Bins.Add(TrashBin);

            hsCurveSelector.Maximum = SortBin.GroupsCurves.Count;
          
            PlotCurve(MainForm.GetFilenameFromCurve(SourceBin.GroupsCurves[0]));

            List<CurveInfo> OriginalCurves = MainForm.OriginalCurves;
            int Dim = (int)Math.Sqrt(OriginalCurves.Count);
            CurveMatrix = new CurveInfo[Dim, Dim];
            for (int i = 0; i < OriginalCurves.Count; i++)
            {
                CurveMatrix[OriginalCurves[i].PrimaryIndex, OriginalCurves[i].SecondaryIndex] = OriginalCurves[i];
            }

            
        }

        #region Plots
        private void PlotCurve(string Filename)
        {
            AFMCurve ac = FileHandler.LoadCSBin(MainForm.DataPath + "\\" + Filename);

            double dx = ac.MinX;
            ac.OffsetCurve(-1 * dx);
            int nPoints = ac.Curve.GetLength(1);
            double[,] Line = new double[2, nPoints];
            int cc = 0;
            int stepK = 1;//(int)Math.Floor((double)(ac.Curve.GetLength(1) / 25));
            if (stepK == 0) stepK = 1;
            for (int k = 0; k < ac.Curve.GetLength(1); k += stepK)
            {
                if (cc < nPoints)
                {
                    Line[0, cc] = ac.Curve[0, k];
                    Line[1, cc] = ac.Curve[1, k];
                    cc++;
                }
            }
            for (int k = cc - 1; k < nPoints; k++)
            {
                Line[0, k] = Line[0, k - 3];
                Line[1, k] = Line[1, k - 3];
            }
            Color UseColor = Color.Black;
            List<Graphing.LineInfo> DisplayLines = new List<Graphing.LineInfo>();
            DisplayLines.Add(new Graphing.LineInfo(Line, UseColor, ""));
            Graphing.PlotData(zedGraphControl1, DisplayLines, "Extention(nm)", "Deflection(V)", "");

        }
        private void PlotCurve(ZedGraphControl zGraph, double[] X, double[] Y)
        {
            int nPoints = X.Length;
            double[,] Line = new double[2, nPoints];
           
            for (int k = 0; k < X.Length ; k ++)
            {
                    Line[0, k] = X[k];
                    Line[1,k] = Y[k];
            }
          
            Color UseColor = Color.Black;
            List<Graphing.LineInfo> DisplayLines = new List<Graphing.LineInfo>();
            DisplayLines.Add(new Graphing.LineInfo(Line, UseColor, ""));


            //zedGraphControl1.MasterPane = new MasterPane();
            GraphPane myPane = zGraph.GraphPane;
            myPane.CurveList.Clear();

            myPane.Legend.IsVisible = false;
            // Set the titles and axis labels
            myPane.Title.Text = "";
            myPane.XAxis.Title.Text ="Metric";
            myPane.YAxis.Title.Text ="Group";
            // myPane.XAxis.Type = AxisType.Date;
            // Make up some data points based on the Sine function
            foreach (Graphing.LineInfo Data in DisplayLines)
            {
                PointPairList list = new PointPairList();

                for (int i = 0; i < Data.Data.GetLength(1); i++)
                {
                    list.Add(new PointPair(Data.Data[0, i], Data.Data[1, i]));
                    //    i += 35;
                }

                // Generate a red curve with diamond symbols, and "Alpha" in the legend
                LineItem myCurve = myPane.AddCurve(Data.Dataname, list, Data.Clr, SymbolType.Circle );
                // Fill the symbols with white
                myCurve.Symbol.Fill = new Fill(Color.Black );

                
            }

            // Show the x axis grid
            myPane.XAxis.MajorGrid.IsVisible = true;
            //myPane.XAxis.Scale.Type=AxisType.Date ;

            // Make the Y axis scale red
            myPane.YAxis.Scale.FontSpec.FontColor = Color.Black;
            myPane.YAxis.Title.FontSpec.FontColor = Color.Black;
            // turn off the opposite tics so the Y tics don't show up on the Y2 axis
            myPane.YAxis.MajorTic.IsOpposite = false;
            myPane.YAxis.MinorTic.IsOpposite = false;
            // Don't display the Y zero line
            myPane.YAxis.MajorGrid.IsZeroLine = false;
            // Align the Y axis labels so they are flush to the axis
            myPane.YAxis.Scale.Align = AlignP.Inside;
            // Manually set the axis range
            myPane.YAxis.Scale.MaxAuto = true;
            myPane.YAxis.Scale.MinAuto = true;

            zGraph.AxisChange();
            // Make sure the Graph gets redrawn
            zGraph.Invalidate();

        }
        private void PlotHistogram(ZedGraph.ZedGraphControl zgc,  List<double[]>[] Lines )
        {
            try
            {
               
                GraphPane myPane = zgc.GraphPane;

                // Set the title and axis labels
                myPane.Title.Text = "Metric Distribution";
                myPane.XAxis.Title.Text = "Comparison Metric";
                myPane.YAxis.Title.Text = "Number";

               /* myPane.XAxis.Scale.Min = X[0] - Gap;
                myPane.XAxis.Scale.Max = X[X.Length - 1] + Gap;
                myPane.XAxis.Scale.MaxGrace = Gap;
                myPane.XAxis.Scale.MinGrace = Gap;*/
                myPane.CurveList.Clear();

                double xMin = double.MaxValue;
                double xMax = double.MinValue;
                for (int i = 0; i < Lines.Length ; i++)
                {
                    PointPairList list = new PointPairList();
                    List<double[]> Bars = Lines[i];
                    for (int j = 0; j < Bars.Count; j++)
                    {
                        list.Add(Bars[j][0], Bars[j][1]);
                        if (Bars[j][0] < xMin) xMin = Bars[j][0];
                        if (Bars[j][0] > xMax) xMax = Bars[j][0];
                    }
                    // create the curves
                    BarItem myCurve = myPane.AddBar("curve 1", list, QColors[i]);
                }
                
                double Gap = (xMax - xMin) * .05;
                myPane.XAxis.Scale.Min = xMin  - Gap;
                myPane.XAxis.Scale.Max = xMax  + Gap;
                myPane.XAxis.Scale.MaxGrace = Gap;
                myPane.XAxis.Scale.MinGrace = Gap;


                // Fill the axis background with a color gradient
                myPane.Chart.Fill = new Fill(Color.White,
                   Color.FromArgb(255, 255, 166), 45.0F);

                zgc.AxisChange();

                zgc.Invalidate();

            }
            catch { }
        }

        #endregion
        

      
        private void hsCurveSelector_Scroll(object sender, ScrollEventArgs e)
        {
           
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                Plot1D();
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                Plot2D();
            }
            else if (tabControl1.SelectedIndex == 3)
            {

            }

        }

       
        #region RadioButtons
        private void rSquared_CheckedChanged(object sender, EventArgs e)
        {
            Plot1D();
        }

        private void rChiSquared_CheckedChanged(object sender, EventArgs e)
        {
            Plot1D();
        }

        private void rAbsolute_CheckedChanged(object sender, EventArgs e)
        {
            Plot1D();
        }

        private void rdSquared_CheckedChanged(object sender, EventArgs e)
        {
            Plot1D();
        }

        private void rWeightedDiff_CheckedChanged(object sender, EventArgs e)
        {
            Plot1D();
        }

        private void rRegression_CheckedChanged(object sender, EventArgs e)
        {
            Plot1D();
        }

        private void rCrossCorrelation_CheckedChanged(object sender, EventArgs e)
        {
            Plot1D();
        }
        #endregion
        private void hsCurveSelector_ValueChanged(object sender, EventArgs e)
        {
            CurrentSelectorCurve = hsCurveSelector.Value;
            PlotCurve(MainForm.GetFilenameFromCurve(SourceBin.GroupsCurves[CurrentSelectorCurve]));
        }

        

        #region RadioButtons_2D
        private void xSquared_CheckedChanged(object sender, EventArgs e)
        {
            Plot2D();
        }

        private void xChi_CheckedChanged(object sender, EventArgs e)
        {
            Plot2D();
        }

        private void xAbsolute_CheckedChanged(object sender, EventArgs e)
        {
            Plot2D();
        }

        private void xdSquare_CheckedChanged(object sender, EventArgs e)
        {
            Plot2D();
        }

        private void xWeighted_CheckedChanged(object sender, EventArgs e)
        {
            Plot2D();
        }

        private void xRegression_CheckedChanged(object sender, EventArgs e)
        {
            Plot2D();
        }

        private void xCross_CheckedChanged(object sender, EventArgs e)
        {
            Plot2D();
        }

        private void ySquare_CheckedChanged(object sender, EventArgs e)
        {
            Plot2D();
        }

        private void yChi_CheckedChanged(object sender, EventArgs e)
        {
            Plot2D();
        }

        private void yAbsolute_CheckedChanged(object sender, EventArgs e)
        {
            Plot2D();
        }

        private void ydSquare_CheckedChanged(object sender, EventArgs e)
        {
            Plot2D();
        }

        private void yWeighted_CheckedChanged(object sender, EventArgs e)
        {
            Plot2D();
        }

        private void yRegression_CheckedChanged(object sender, EventArgs e)
        {
            Plot2D();
        }

        private void yCross_CheckedChanged(object sender, EventArgs e)
        {
            Plot2D();
        }
        #endregion

        #region Mechanics_2D
        SelectableGraph.DataPoint[] datapoints2D = null;
        private void Plot2D()
        {
            double[] X = new double[CurveMatrix.GetLength(1)];
            for (int i = 0; i < CurveMatrix.GetLength(1); i++)
            {
                if (xSquared.Checked)
                    X[i] = CurveMatrix[CurrentSelectorCurve, i].correlation.LeastSquaresFit;
                if (xAbsolute.Checked)
                    X[i] = CurveMatrix[CurrentSelectorCurve, i].correlation.AbsoluteFit;
                if (xChi.Checked)
                    X[i] = CurveMatrix[CurrentSelectorCurve, i].correlation.ChiSquared;
                if (xCross.Checked)
                    X[i] = CurveMatrix[CurrentSelectorCurve, i].correlation.CrossCorrelation;
                if (xdSquare.Checked)
                    X[i] = CurveMatrix[CurrentSelectorCurve, i].correlation.dLeastSquaresFit;
                if (xRegression.Checked)
                    X[i] = CurveMatrix[CurrentSelectorCurve, i].correlation.LinearRegression;
                if (xWeighted.Checked)
                    X[i] = CurveMatrix[CurrentSelectorCurve, i].correlation.WeightedLeastSquarsFit;
            }
            double[] Y = new double[CurveMatrix.GetLength(1)];
            for (int i = 0; i < CurveMatrix.GetLength(1); i++)
            {
                if (ySquare.Checked)
                    Y[i] = CurveMatrix[CurrentSelectorCurve, i].correlation.LeastSquaresFit;
                if (yAbsolute.Checked)
                    Y[i] = CurveMatrix[CurrentSelectorCurve, i].correlation.AbsoluteFit;
                if (yChi.Checked)
                    Y[i] = CurveMatrix[CurrentSelectorCurve, i].correlation.ChiSquared;
                if (yCross.Checked)
                    Y[i] = CurveMatrix[CurrentSelectorCurve, i].correlation.CrossCorrelation;
                if (ydSquare.Checked)
                    Y[i] = CurveMatrix[CurrentSelectorCurve, i].correlation.dLeastSquaresFit;
                if (yRegression.Checked)
                    Y[i] = CurveMatrix[CurrentSelectorCurve, i].correlation.LinearRegression;
                if (yWeighted.Checked)
                    Y[i] = CurveMatrix[CurrentSelectorCurve, i].correlation.WeightedLeastSquarsFit;
            }

            if (datapoints2D == null)
            {
                datapoints2D = new SelectableGraph.DataPoint[Y.Length];
                for (int i = 0; i < Y.Length; i++)
                {
                    SelectableGraph.DataPoint dp = new SelectableGraph.DataPoint();
                    dp.X = X[i];
                    dp.Y = Y[i];
                    datapoints2D[i] = dp;
                }
            }
            else
            {
                for (int i = 0; i < Y.Length; i++)
                {
                    SelectableGraph.DataPoint dp = datapoints2D[i];
                    dp.X = X[i];
                    dp.Y = Y[i];
                    datapoints2D[i] = dp;
                }

            }

            selectableGraph1.PlotDots(datapoints2D);
        }

        private void kKmeans2D_Click(object sender, EventArgs e)
        {
            int nGroups =(int) nNumberofGroups2D.Value;

            // Transform the image into an array of pixel values
            double[][] pixels = new double[datapoints2D.Length][];

            float minX = selectableGraph1.minX;
            float MaxX = selectableGraph1.MaxX;
            float minY = selectableGraph1.minY;
            float MaxY = selectableGraph1.MaxY;

            for (int i = 0; i < datapoints2D.Length; i++)
            {
                pixels[i] = new double[] { (datapoints2D[i].X - minX) / (MaxX - minX) * 100, (datapoints2D[i].Y - minY) / (MaxY - minY) * 100 };

            }

            // Create a K-Means algorithm using given k and a
            //  square euclidean distance as distance metric.
            KMeans kmeans = new KMeans(nGroups , Distance.SquareEuclidean);

            // Compute the K-Means algorithm until the difference in
            //  cluster centroids between two iterations is below 0.05
            int[] idx = kmeans.Compute(pixels );

            for (int i = 0; i < idx.Length; i++)
            {
                datapoints2D[i].AssignColor(idx[i]);
            }

            Plot2D();
        }

        private void bGMM2D_Click(object sender, EventArgs e)
        {
            int nGroups = (int)nNumberofGroups2D.Value;

            // Transform the image into an array of pixel values
            double[][] pixels = new double[datapoints2D.Length][];

            float minX = selectableGraph1.minX;
            float MaxX = selectableGraph1.MaxX;
            float minY = selectableGraph1.minY;
            float MaxY = selectableGraph1.MaxY;

            for (int i = 0; i < datapoints2D.Length; i++)
            {
                pixels[i] = new double[] { (datapoints2D[i].X-minX )/ (MaxX-minX )  *100, (datapoints2D[i].Y-minY )/(MaxY-minY )*100 };

            }

            // Create a new Gaussian Mixture Model
            GaussianMixtureModel gmm = new GaussianMixtureModel(nGroups );

            // Compute the model
           // gmm.Compute(pixels );
            gmm.Learn(pixels );
            var c = gmm.Compute(pixels);
            for (int j = 0; j < pixels.Length; j++)
            {
                datapoints2D[j].AssignColor(c[j]   );
            }

            Plot2D();
        }

        private void rGroupHandSelect2D_CheckedChanged(object sender, EventArgs e)
        {
            selectableGraph1.CurrentMouseMode = SelectableGraph.MouseModes.SelectMode;
        }

        private void rZoom2D_CheckedChanged(object sender, EventArgs e)
        {
            selectableGraph1.CurrentMouseMode = SelectableGraph.MouseModes.ZoomMode;
        }

        private void bInstruct2D_Click(object sender, EventArgs e)
        {
            bInstruct2D.Visible = false;
            pInstruct2D.Visible = false;

            bInstruct2D.SendToBack();
            pInstruct2D.SendToBack();
        }

         private void bSave2DAutos_Click(object sender, EventArgs e)
        {
                OutBins.Clear();
                int nGroups = 0;
                datapoints2D = selectableGraph1.Datapoints;
                for (int i=0;i<datapoints2D .Length  ;i++)
                {
                    if (datapoints2D[i].Group > nGroups) nGroups = datapoints2D[i].Group;
                }
                for (int i = 0; i <nGroups+1 ; i++)
                {
                    OutBins.Add(new GroupBin(i));
                }
                double X = 0;
                for (int i = 0; i < SourceBin.GroupsCurves.Count; i++)
                {
                    int pi = SourceBin.GroupsCurves[i].PrimaryIndex;
                    int GroupNum = datapoints2D[pi].Group;
                    OutBins[GroupNum].GroupsCurves.Add(SourceBin.GroupsCurves[i]);
                }
                this.Close();
        }
        #endregion

        #region Mechanics_1D

        double[] Datapoints1D = null;
        double[] Mins1D = null;
        double[] Maxs1D = null;
        private void Plot1D()
        {
            double[] X = new double[CurveMatrix.GetLength(1)];
            for (int i = 0; i < CurveMatrix.GetLength(1); i++)
            {
                if (rSquared.Checked)
                    X[i] = CurveMatrix[CurrentSelectorCurve, i].correlation.LeastSquaresFit;
                if (rAbsolute.Checked)
                    X[i] = CurveMatrix[CurrentSelectorCurve, i].correlation.AbsoluteFit;
                if (rChiSquared.Checked)
                    X[i] = CurveMatrix[CurrentSelectorCurve, i].correlation.ChiSquared;
                if (rCrossCorrelation.Checked)
                    X[i] = CurveMatrix[CurrentSelectorCurve, i].correlation.CrossCorrelation;
                if (rdSquared.Checked)
                    X[i] = CurveMatrix[CurrentSelectorCurve, i].correlation.dLeastSquaresFit;
                if (rRegression.Checked)
                    X[i] = CurveMatrix[CurrentSelectorCurve, i].correlation.LinearRegression;
                if (rWeightedDiff.Checked)
                    X[i] = CurveMatrix[CurrentSelectorCurve, i].correlation.WeightedLeastSquarsFit;
            }

            Datapoints1D = X;

            Accord.Statistics.Visualizations.Histogram h = new Accord.Statistics.Visualizations.Histogram(Datapoints1D);


            if (Mins1D == null)
            {
                Mins1D = new double[1];
                Maxs1D = new double[1];
                Mins1D[0] = h.Bins[0].Range.Min;
                Maxs1D[0] = h.Bins[h.Bins. Count - 1].Range.Max;
            }
            int nGroups = Mins1D.Length;
            List<double[]>[] Lines = new List<double[]>[nGroups];
            for (int i = 0; i < nGroups; i++)
                Lines[i] = new List<double[]>();

            X = new double[h.Bins.Count];

            cBCutLines.Items.Clear();
            for (int i = 0; i < h.Bins.Count; i++)
            {
                double binCenter = (h.Bins[i].Range.Max + h.Bins[i].Range.Min) / 2;
                for (int j = 0; j < nGroups; j++)
                {

                    if ((binCenter > Mins1D[j] && binCenter < Maxs1D[j]))
                    {
                        Lines[j].Add(new double[] { h.Bins[i].Range.Min, h.Bins[i].Value });
                    }
                }
                X[i] = h.Bins[i].Range.Min;
                cBCutLines.Items.Add(h.Bins[i].Range.Min);
            }

            PlotHistogram(zedGraphControl2, Lines);

        }

        private void bCut1D_Click(object sender, EventArgs e)
        {
            if (bCut1D.Text == "")
            {
                Plot1D();
                return;
            }
            double min= zedGraphControl2.GraphPane.XAxis.Scale.Min;
            double max = zedGraphControl2.GraphPane.XAxis.Scale.Max;

            double v = min;
            double.TryParse( cBCutLines.Text , out v);
            if (v <= min)
            {
                Plot1D();
                return;

            }
            Mins1D = new double[2];
            Maxs1D = new double[2];
            Mins1D[0] = min;
            Maxs1D[0] = v;

            Mins1D[1] = v;
            Maxs1D[1] = max ;

            Plot1D();
        }
        private void b1DkMeans_Click(object sender, EventArgs e)
        {
            int nGroups = (int)nNum1DGroups.Value;

            // Transform the image into an array of pixel values
            double[][] pixels = new double[Datapoints1D.Length][];

            float minX =(float) zedGraphControl2.GraphPane.YAxis.Scale.Min;
            float MaxX = (float)zedGraphControl2.GraphPane.YAxis.Scale.Max;


            for (int i = 0; i < Datapoints1D.Length; i++)
            {
                pixels[i] = new double[] { ( (Datapoints1D[i]-minX )/(MaxX -minX ) *1000 ) };

            }

            // Create a K-Means algorithm using given k and a
            //  square euclidean distance as distance metric.
            KMeans kmeans = new KMeans(nGroups, Distance.SquareEuclidean);

            // Compute the K-Means algorithm until the difference in
            //  cluster centroids between two iterations is below 0.05
            int[] idx = kmeans.Compute(pixels );

            Mins1D = new double[nGroups];
            Maxs1D = new double[nGroups];
            for (int i=0;i<nGroups ;i++)
            {
                Mins1D[i] = double.MaxValue ;
                Maxs1D[i]=double.MinValue ;
            }

            for (int i = 0; i < idx.Length; i++)
            {
                double md= Mins1D[idx[i]];
                double d =  Datapoints1D[i];
                if ( d < md ) Mins1D[idx[i]] = Datapoints1D[i];
                if (Maxs1D[idx[i]] < Datapoints1D[i]) Maxs1D[idx[i]] = Datapoints1D[i];
            }

            Plot1D();

        }
        private void b1DGaussian_Click(object sender, EventArgs e)
        {
            int nGroups = (int)nNum1DGroups.Value;

            // Transform the image into an array of pixel values
            double[][] pixels = new double[Datapoints1D.Length][];

            float minX = (float)zedGraphControl2.GraphPane.YAxis.Scale.Min;
            float MaxX = (float)zedGraphControl2.GraphPane.YAxis.Scale.Max;


            for (int i = 0; i < Datapoints1D.Length; i++)
            {
                pixels[i] = new double[] { ((Datapoints1D[i] - minX) / (MaxX - minX) * 1000) };

            }

            // Create a new Gaussian Mixture Model
            GaussianMixtureModel gmm = new GaussianMixtureModel(nGroups);

            // Compute the model
            gmm.Compute(pixels);

            int[] idx = new int[pixels.Length];
            var c = gmm.Compute(pixels);
            for (int j = 0; j < pixels.Length; j++)
            {
                idx[j] = c[j];
            }

            Mins1D = new double[nGroups];
            Maxs1D = new double[nGroups];
            for (int i = 0; i < nGroups; i++)
            {
                Mins1D[i] = double.MaxValue;
                Maxs1D[i] = double.MinValue;
            }

            for (int i = 0; i < idx.Length; i++)
            {
                double md = Mins1D[idx[i]];
                double d = Datapoints1D[i];
                if (d < md) Mins1D[idx[i]] = Datapoints1D[i];
                if (Maxs1D[idx[i]] < Datapoints1D[i]) Maxs1D[idx[i]] = Datapoints1D[i];
            }

            Plot1D();
        }
        private void bSave1DGroups_Click(object sender, EventArgs e)
        {
                OutBins.Clear();
                for (int i=0;i < Mins1D.Length ;i++)
                {
                    OutBins.Add(new GroupBin(i));
                }
                double X = 0;
                for (int i = 0; i < SourceBin.GroupsCurves.Count; i++)
                {
                    int pi = SourceBin.GroupsCurves[i].PrimaryIndex;
                    if (rSquared.Checked)
                        X = CurveMatrix[CurrentSelectorCurve, pi].correlation.LeastSquaresFit;
                    if (rAbsolute.Checked)
                        X = CurveMatrix[CurrentSelectorCurve, pi].correlation.AbsoluteFit;
                    if (rChiSquared.Checked)
                        X = CurveMatrix[CurrentSelectorCurve, pi].correlation.ChiSquared;
                    if (rCrossCorrelation.Checked)
                        X = CurveMatrix[CurrentSelectorCurve, pi].correlation.CrossCorrelation;
                    if (rdSquared.Checked)
                        X = CurveMatrix[CurrentSelectorCurve, pi].correlation.dLeastSquaresFit;
                    if (rRegression.Checked)
                        X = CurveMatrix[CurrentSelectorCurve, pi].correlation.LinearRegression;
                    if (rWeightedDiff.Checked)
                        X = CurveMatrix[CurrentSelectorCurve, pi].correlation.WeightedLeastSquarsFit;

                    int GroupNum = 0;
                    for (int j = 0; j < Mins1D.Length ; j++)
                    {

                        if ((X > Mins1D[j] && X < Maxs1D[j]))
                        {
                            GroupNum = j;
                        }
                    }
                    OutBins[GroupNum].GroupsCurves.Add(SourceBin.GroupsCurves[i]);
                   
                }


            

            this.Close();
        }
        #endregion

       

       
    }
}
