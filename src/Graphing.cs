using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZedGraph;
using System.Drawing;
namespace GroupCurves
{
    public delegate void PlotDataEvent(Graphing sender, double[,] Data, string Dataname, string XAxis, string YAxis, string Title);
    public class Graphing
    {
        public ZedGraphControl zedGraph;
        public event PlotDataEvent TryPlotData;

        public void PlotData(double[,] Data, string Dataname, string XAxis, string YAxis, string Title)
        {
            if (TryPlotData != null) TryPlotData(this, Data, Dataname,XAxis,YAxis,Title );
        }
        public void DoPlotData(double[,] Data, string DataName, string XAxis, string YAxis, string Title)
        {
            PlotData(zedGraph, Data, DataName,XAxis,YAxis,Title );
        }

        public void DoPlotData(List<LineInfo> Datas, string XAxis, string YAxis, string Title)
        {
            PlotData(zedGraph, Datas, XAxis ,YAxis ,Title );
        }

        public static void PlotData(ZedGraph.ZedGraphControl zGraph, double[,] Data, string DataName, string XAxis, string YAxis, string Title)
        {
            //zedGraphControl1.MasterPane = new MasterPane();
            GraphPane myPane = zGraph.GraphPane;
            myPane.CurveList.Clear();
            // Set the titles and axis labels
            myPane.Title.Text = Title ;
            myPane.XAxis.Title.Text = XAxis ;
            myPane.YAxis.Title.Text = YAxis ;
            // myPane.XAxis.Type = AxisType.Date;
            // Make up some data points based on the Sine function
            PointPairList list = new PointPairList();

            for (int i = 0; i < Data.GetLength(1); i++)
            {
                list.Add(new PointPair(Data[0, i], Data[1, i]));
                //    i += 35;
            }

            // Generate a red curve with diamond symbols, and "Alpha" in the legend
            LineItem myCurve = myPane.AddCurve(DataName,
                list, Color.Red, SymbolType.Diamond);
            // Fill the symbols with white
            myCurve.Symbol.Fill = new Fill(Color.White);

            // Show the x axis grid
            myPane.XAxis.MajorGrid.IsVisible = true;
            //myPane.XAxis.Scale.Type=AxisType.Date ;

            // Make the Y axis scale red
            myPane.YAxis.Scale.FontSpec.FontColor = Color.Red;
            myPane.YAxis.Title.FontSpec.FontColor = Color.Red;
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

            // Fill the axis background with a gradient
            myPane.Chart.Fill = new Fill(Color.White, Color.LightGray, 45.0f);

            // Tell ZedGraph to calculate the axis ranges
            // Note that you MUST call this after enabling IsAutoScrollRange, since AxisChange() sets
            // up the proper scrolling parameters
            zGraph.AxisChange();
            // Make sure the Graph gets redrawn
            zGraph.Invalidate();



        }

        public  class LineInfo
        {
            public double[,] Data;
            public Color Clr;
            public string Dataname;
            public LineInfo(double[,] Data, Color clr, string Dataname)
            {
                this.Data = Data;
                this.Clr = clr;
                this.Dataname = Dataname;
            }
        }
        public  static void PlotData(ZedGraph.ZedGraphControl zGraph, List<LineInfo> Datas, string XAxis,string YAxis, string Title)
        {
            //zedGraphControl1.MasterPane = new MasterPane();
            GraphPane myPane = zGraph.GraphPane;
            myPane.CurveList.Clear();
           
            myPane.Legend.IsVisible = false;
            // Set the titles and axis labels
            myPane.Title.Text = Title ;
            myPane.XAxis.Title.Text = XAxis ;
            myPane.YAxis.Title.Text = YAxis ;
            // myPane.XAxis.Type = AxisType.Date;
            // Make up some data points based on the Sine function
            foreach (LineInfo Data in Datas)
            {
                PointPairList list = new PointPairList();

                for (int i = 0; i < Data.Data.GetLength(1); i++)
                {
                    list.Add(new PointPair(Data.Data[0, i], Data.Data[1, i]));
                    //    i += 35;
                }

                // Generate a red curve with diamond symbols, and "Alpha" in the legend
                LineItem myCurve = myPane.AddCurve(Data.Dataname,
                    list, Data.Clr, SymbolType.None);
                // Fill the symbols with white
                //myCurve.Symbol.Fill = new Fill(Color.White);
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


         /*  myPane.XAxis.Scale.MaxAuto = false;
            myPane.XAxis.Scale.Min = 0;
            myPane.XAxis.Scale.Max = 4000;

            myPane.YAxis.Scale.MaxAuto = false;
            myPane.YAxis.Scale.Min = -5;
            myPane.YAxis.Scale.Max = 10;*/


            // Fill the axis background with a gradient
           // myPane.Chart.Fill = new Fill(Color.White, Color.LightGray, 45.0f);

            // Tell ZedGraph to calculate the axis ranges
            // Note that you MUST call this after enabling IsAutoScrollRange, since AxisChange() sets
            // up the proper scrolling parameters
            zGraph.AxisChange();
            // Make sure the Graph gets redrawn
            zGraph.Invalidate();



        }

        public Graphing()
        {
        }
        public Graphing(ZedGraphControl Graph)
        {
            zedGraph = Graph;
        }
    }
}
