using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
using System.IO;
namespace GroupCurves
{
    public partial class AllGroupVision : Form
    {
        private GroupBins CurrentBins;
        List< double[,]> AllCurves=new List<double[,]>();

        public AllGroupVision()
        {
            InitializeComponent();       
        }
        public AllGroupVision(Form1 Main, GroupBins DisplayGroups):this()
        {
            CurrentBins = DisplayGroups;
           

            PlotALL(Main, DisplayGroups);
            this.BringToFront();
            this.Show();
        }

        private void GroupVision_Load(object sender, EventArgs e)
        {

        }
        private void PlotALL(Form1 Main, GroupBins DisplayGroups)
        {
            Application.DoEvents();
            List<Graphing.LineInfo> DisplayLines = new List<Graphing.LineInfo>();
            Random rnd = new Random();
            AFMCurve C1 = new AFMCurve();
            Color GroupColor = Color.White;
            for (int i = 0; i < DisplayGroups.Bins.Count ; i++)
            {
                GroupBin gb = (GroupBin)DisplayGroups.Bins [i];
                double MaxCurve = 0;
                int StepJ = 1;
                if (gb.GroupsCurves.Count > 100)
                {
                    StepJ = (int)((double)gb.GroupsCurves.Count / 100);
                }
                for (int j = 0; j < gb.GroupsCurves.Count; j += StepJ)
                {
                    string Filename = Main.CurveNames[gb.GroupsCurves[j].PrimaryIndex];
                    AFMCurve ac = FileHandler.LoadCSBin(Main.DataPath + "\\" + Filename);
                    if (ac.MaxY > MaxCurve)
                    {
                        MaxCurve = ac.MaxY;
                        C1 = ac;
                    }

                }

                if (i > 1) GroupColor = Color.FromArgb((int)(255 * rnd.NextDouble()), (int)(255 * rnd.NextDouble()), (int)(255 * rnd.NextDouble()));

                for (int j = 0; j < gb.GroupsCurves.Count; j += StepJ)
                {
                    string Filename = Main.CurveNames[gb.GroupsCurves[j].PrimaryIndex];
                    AFMCurve ac = FileHandler.LoadCSBin(Main.DataPath + "\\" + Filename);

                    double dx;
                    if (C1.MaxY > ac.MaxY)
                    {
                        dx = ac.GetXFromY(ac.MaxY) - C1.GetXFromY(ac.MaxY);
                    }
                    else
                    {
                        dx = ac.GetXFromY(C1.MaxY) - C1.GetXFromY(C1.MaxY);
                    }
                    ac.OffsetCurve(-1 * dx);
                    int nPoints = 35;
                    double[,] Line = new double[2, nPoints];
                    int cc = 0;
                    int stepK = (int)Math.Floor((double)(ac.Curve.GetLength(1) / 25));
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
                        try
                        {
                            Line[0, k] = Line[0, k - 3];
                            Line[1, k] = Line[1, k - 3];
                        }
                        catch { }
                    }
                    Color UseColor;
                    if (GroupColor == Color.White)
                        UseColor = Color.FromArgb((int)(255 * rnd.NextDouble()), (int)(255 * rnd.NextDouble()), (int)(255 * rnd.NextDouble()));
                    else
                        UseColor = GroupColor;
                    DisplayLines.Add(new Graphing.LineInfo(Line, UseColor, ""));
                }
            }

            Graphing.PlotData(zedGraphControl1, DisplayLines, "Extention(nm)", "Deflection(V)", "");
            
          
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not yet implemented");
        }

        
    }
}
