using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
using System.Drawing;

namespace GroupCurves
{
    public class GroupBin
    {
        public int BinNumber;
        public bool Locked = false;
        

        public List<IndividualCurveStats> GroupsCurves = new List<IndividualCurveStats>();
        public GroupBin(int BinNumber)
        {
            this.BinNumber = BinNumber;
        }
        public bool ContainsCurve(int PrimaryIndex)
        {
            foreach (IndividualCurveStats ics in GroupsCurves)
            {
                if (ics.PrimaryIndex == PrimaryIndex)
                {
                    return true;
                }

            }
            return false;
        }
        public IndividualCurveStats GetCurve(int PrimaryIndex)
        {
            foreach (IndividualCurveStats ics in GroupsCurves)
            {
                if (ics.PrimaryIndex == PrimaryIndex)
                {
                    return ics;
                }

            }
            return null;
        }

        /// <summary>
        /// Alignes the curves and then averages them and returns the averaged curve and two standard 
        /// deviation curves.  column 0 is x value, 1 is lower SD, 2 is average, 3 is upper SD
        /// </summary>
        /// <param name="StandardCurve">Set to null if you want the algorythm to generate it</param>
        /// <returns></returns>
        public double[,] AverageGroup(AFMCurve StandardCurve)
        {
            //get all the curves in a nicely alligned format
            double MinX =10000;
            double MaxX=-MinX ;
            List<Graphing.LineInfo> Curves = AlignCurves(Color.White, 0,500,null );

            //put all the curves in pointpairlists (has a nice built in interpolation function)
            PointPairList[] CurvePPList = new PointPairList[GroupsCurves.Count];
            for (int i = 0; i < Curves.Count; i++)
            {
                CurvePPList[i] = new PointPairList();
                
               
                double[,] Data = Curves[i].Data ;
                for (int j = Data.GetLength(1)-1; j >=0 ; j--)
                {
                    CurvePPList[i].Add(new PointPair(Data[0, j], Data[1, j]));
                    if (MinX > Data[0, j]) MinX = Data[0, j];
                    if (MaxX < Data[0, j]) MaxX = Data[0, j];
                    //    i += 35;
                }
               // CurvePPList[i].Sort();
            }
            //now build the interpolation using nicely spaced points
            int nPoints=(int)(MaxX-MinX );
            if (nPoints < 0) nPoints = 1;
            double[,] AveragenSD = new double[4, nPoints ];
            for (int i = 0; i < nPoints; i++)
            {
                double sum = 0;
                double count = 0;
                double[] PointPoints = new double[CurvePPList.Length];
                for (int j = 0; j < CurvePPList.Length; j++)
                {
                    //System.Diagnostics.Debug.Print(CurvePPList[j][0].X.ToString());
                    if ((int)(i + MinX) >= (int)(CurvePPList[j][0].X) && (int)(i + MinX) <= (int)(CurvePPList[j][CurvePPList[j].Count -1].X))
                    {
                        PointPoints[j] = CurvePPList[j].InterpolateX(i + MinX);
                        sum += PointPoints[j];
                        count++;
                    }
                    else
                        PointPoints[j] = double.NaN;
                }
                double average =sum/(double) (count) ;
                double d = 0;
                if (double.IsNaN(average) || count==0) average = 0;
                AveragenSD[0, i] = i;
                AveragenSD[2, i] = average ;
                sum = 0;
                for (int j = 0; j < CurvePPList.Length; j++)
                {
                    if (!double.IsNaN(PointPoints[j]))
                    {
                        d = PointPoints[j] - average;
                        sum += d * d;
                    }
                }
                double SD = Math.Sqrt(sum /(double) count );
                if (double.IsNaN(SD) || count==0) SD = 0;
                AveragenSD[1, i] = average - SD;
                AveragenSD[3, i] = average + SD;
            }

            return AveragenSD;
        }

        /// <summary>
        /// Aligns the curves and returns graphable curves
        /// </summary>
        /// <param name="GroupColor"></param>
        /// <param name="NumRepresentiveCurves"></param>
        /// <param name="NumDataPointsPerCurve"></param>
        /// <param name="StandardCurve">set to null if you do not have a set alignment curve</param>
        /// <returns></returns>
        public List<Graphing.LineInfo> AlignCurves(Color GroupColor, int NumRepresentiveCurves, int NumDataPointsPerCurve, AFMCurve StandardCurve)
        {
            List<Graphing.LineInfo> DisplayLines = new List<Graphing.LineInfo>();
            Random rnd = new Random();
            AFMCurve C1 = new AFMCurve();
            GroupBin gb =this ;
            double MaxCurve = 0;
            int StepJ = 1;
            if (NumRepresentiveCurves !=0)
                if (gb.GroupsCurves.Count > NumRepresentiveCurves )
                {
                    StepJ = (int)((double)gb.GroupsCurves.Count / NumRepresentiveCurves );
                }

            if (StandardCurve == null)
            {
                for (int j = 0; j < gb.GroupsCurves.Count; j += StepJ)
                {
                    string Filename = gb.GroupsCurves[j].DataPath + "\\" + gb.GroupsCurves[j].Filename;// CurveNames[gb.GroupsCurves[j].PrimaryIndex];
                    AFMCurve ac = FileHandler.LoadCSBin(Filename);
                    if (ac.MaxY > MaxCurve)
                    {
                        MaxCurve = ac.MaxY;
                        C1 = ac;
                    }
                }
            }
            else
            {
                C1 = StandardCurve;
                MaxCurve = StandardCurve.MaxY;
            }

          
            for (int j = 0; j < gb.GroupsCurves.Count; j += StepJ)
            {
                string Filename =gb.GroupsCurves[j].DataPath + "\\" + gb.GroupsCurves[j].Filename;// CurveNames[gb.GroupsCurves[j].PrimaryIndex];
                AFMCurve ac = FileHandler.LoadCSBin(Filename);

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
                int nPoints = NumDataPointsPerCurve ;
                double[,] Line = new double[2, nPoints];
                int cc = 0;
                int stepK = (int)Math.Floor((double)(ac.Curve.GetLength(1) / NumDataPointsPerCurve ));
                if (stepK == 0) stepK = 1;
                for (int k = 0; k < ac.Curve.GetLength(1); k += stepK)
                {
                    if (cc < nPoints)
                    {
                        Line[0, cc] = ac.Curve[0, k];
                        //if (MinX > Line[0, cc]) MinX = Line[0,cc];
                        //if (MaxX < Line[0, cc]) MaxX = Line[0, cc];
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
               
                DisplayLines.Add(new Graphing.LineInfo(Line, GroupColor, "DataName"));
            }

            return DisplayLines;

        }
        private void SortList(ref List<IndividualCurveStats> l)
        {
            l.Sort(delegate(IndividualCurveStats c1, IndividualCurveStats c2)
            { return c1.SortParam.CompareTo(c2.SortParam ); });

        }
        public void SortByHisto(Graphing ZedGraph)
        {
            for (int i = 0; i < GroupsCurves.Count; i++)
            {
                double[,] Histogram;
                double LSF= GroupsCurves[i].GetHistogram(this, .5,out  Histogram);
                if (ZedGraph != null)
                    ZedGraph.PlotData(Histogram, "LSF","Standard Dev.","Cummul.","");
                GroupsCurves[i].SortParam = LSF;
                Application.DoEvents();
            }
            SortList(ref GroupsCurves);

        }

        public double InteractionEnergy(ref IndividualCurveStats TestCurve, ref double[,] LSFs)
        {
            double sum = 0;
            int count = 0;
            int i = TestCurve.PrimaryIndex;
            for (int j = 0; j < GroupsCurves.Count; j++)
            {
                int l = GroupsCurves[j].PrimaryIndex;
                sum += LSFs[i, l];
                count++;
            }
            return sum / count;
        }

        public double InteractionEnergyUnNormalized(ref IndividualCurveStats TestCurve, ref double[,] LSFs)
        {
            double sum = 0;
            int count = 0;
            int i = TestCurve.PrimaryIndex;
            for (int j = 0; j < GroupsCurves.Count; j++)
            {
                int l = GroupsCurves[j].PrimaryIndex;
                sum += LSFs[i, l];
                count++;
            }
            return sum ;
        }
        public double InteractionGroupSelfEnergy(ref IndividualCurveStats TestCurve, ref double[,] LSFs)
        {

            double sum = 0;
            int count = 0;
            for (int i = 0; i < GroupsCurves.Count; i++)
                for (int j = i + 1; j < GroupsCurves.Count; j++)
                {
                    int k = GroupsCurves[i].PrimaryIndex;
                    int l = GroupsCurves[j].PrimaryIndex;
                    sum += LSFs[k, l];
                    count++;
                }

           

            return sum / count;
        }
        public double GroupSelfEnergy(ref double[,] LSFs)
        {
           
            double sum=0;
            int count = 0;
            for (int i=0;i<GroupsCurves.Count ;i++)
              for (int j=0 ;j<GroupsCurves.Count ;j++)
              {
                  int k=GroupsCurves[i].PrimaryIndex ;
                  int l=GroupsCurves[j].PrimaryIndex ;
                  sum += LSFs[k, l];
                  count++;
              }
            return sum / count;
        }
        public double GroupSelfEnergyUnNormalized(ref double[,] LSFs)
        {

            double sum = 0;
            //int count = 0;
            for (int i = 0; i < GroupsCurves.Count; i++)
                for (int j = 0; j < GroupsCurves.Count; j++)
                {
                    int k = GroupsCurves[i].PrimaryIndex;
                    int l = GroupsCurves[j].PrimaryIndex;
                    sum += LSFs[k, l];
                   // count++;
                }
            return sum ;/// count;
        }
        public override string ToString()
        {
            return BinNumber.ToString();
        }
    }
}
