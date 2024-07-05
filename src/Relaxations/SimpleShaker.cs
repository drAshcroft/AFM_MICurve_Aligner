using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GroupCurves
{
    public class SimpleShaker
    {
        private static Random rnd = new Random(DateTime.Now.Millisecond);
        public static  GroupBins ShakeAssign(ref double[,] LSFs,ProgressBar progressBar1, ZedGraph.ZedGraphControl zedGraphControl1,  int nShakes,  GroupBins PremadeBinsOptional, bool WriteFile)
        {

            GroupBins GroupBins = PremadeBinsOptional;

            //shake the bins
            double[,] Energies = new double[2, nShakes];
            double sum=0;
            long TotalCurves = 0;
            for (int GroupBinNumber = 0; GroupBinNumber < GroupBins.Bins.Count; GroupBinNumber++)
                TotalCurves += GroupBins.Bins[GroupBinNumber].GroupsCurves.Count;

            for (int i = 0; i < nShakes; i++)
            {
                progressBar1.Value = (int)(100 * (double)i / (double)nShakes);

                //for (int GroupBinNumber = 0; GroupBinNumber < GroupBins.Bins.Count; GroupBinNumber++)
                  //  for (int ListNumber = 0; ListNumber < GroupBins.Bins[GroupBinNumber].GroupsCurves.Count; ListNumber++)
                for (int j=0;j<TotalCurves ;j++)
                    {
                        int GroupBinNumber= rnd.Next(GroupBins.Bins.Count);
                        int ListNumber = rnd.Next(GroupBins.Bins[GroupBinNumber].GroupsCurves.Count);
                        sum=ShakeBins(GroupBins, GroupBinNumber, ListNumber,ref LSFs );
                        Application.DoEvents();
                    }

                sum = 0;
                for (int GroupBinNumber = 0; GroupBinNumber < GroupBins.Bins.Count; GroupBinNumber++)
                {
                    double d = GroupBins.Bins[GroupBinNumber].GroupSelfEnergyUnNormalized (ref LSFs);
                    sum += d;
                }
                Energies[0, i] = i;
                Energies[1, i] = sum;
                if (zedGraphControl1 !=null) Graphing.PlotData(zedGraphControl1, Energies, "Relaxation","Iteration","Total Metric","");
            }

            return GroupBins;

        }

        public static  double  ShakeBins(GroupBins GroupBins, int GroupBinNumber, int ListNumber, ref double[,] LSFs)
        {
            try
            {
                double[] SumLSF = new double[GroupBins.Bins.Count];
                IndividualCurveStats TestCurve = GroupBins.Bins[GroupBinNumber].GroupsCurves[ListNumber];

                if (TestCurve.Fixed == false)
                {
                    GroupBins.Bins[GroupBinNumber].GroupsCurves.RemoveAt(ListNumber);
                    int PrimaryIndex = TestCurve.PrimaryIndex;
                    double[] t = new double[GroupBins.Bins.Count];
                    for (int k = 0; k < GroupBins.Bins.Count; k++)
                    {
                        SumLSF[k] = GroupBins.Bins[k].InteractionEnergyUnNormalized(ref TestCurve, ref LSFs);
                    }

                    int MinIndex = 0;
                    double MinValueLSF = 1000000;
                    //find the group bin that the curve fits in best 
                    for (int k = 0; k < GroupBins.Bins.Count; k++)
                    {
                        if (SumLSF[k] < MinValueLSF)
                        {
                            MinValueLSF = SumLSF[k];
                            MinIndex = k;
                        }
                    }

                    //if (MinIndex != GroupBinNumber)
                    {
                        if (MinIndex == 0)
                            System.Diagnostics.Debug.Print("");
                        //if (GroupBins.Bins[GroupBinNumber].GroupsCurves.Count > 1)
                        {
                            GroupBins.Bins[MinIndex].GroupsCurves.Add(TestCurve);
                        }
                    }
                    return 0;
                }
            }
            catch { }
           
            return 0;
        }

    }
}
