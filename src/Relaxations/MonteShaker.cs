using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GroupCurves
{
    public class MonteCarloShaker
    {

        public static GroupBins MonteAssign(ref double[,] LSFs, double MaxLSF, ProgressBar progressBar1, ZedGraph.ZedGraphControl zedGraphControl1, int nBins, int nShakes, GroupBin PrimaryCurves, GroupBins PremadeBinsOptional, bool WriteFile,bool ShakeToo)
        {
            GroupBins GroupBins;
            if (PremadeBinsOptional == null)
            {
                GroupBins = new GroupBins();
                for (int i = 0; i < nBins; i++)
                {
                    GroupBins.Bins.Add(new GroupBin(GroupBins.GetNextBinNumber()));   //(i, new List<IndividualCurveStats>());
                }

                IndividualCurveStats[] ics = new IndividualCurveStats[nBins];
                ics[0] = PrimaryCurves.GroupsCurves[0];
                double FarthestLSF = -10000;
                int FarthestIndex = 0;
                for (int j = 1; j < nBins; j++)
                {

                    for (int i = 0; i < PrimaryCurves.GroupsCurves.Count; i++)
                    {
                        double sum = 0;
                        for (int k = 0; k < nBins; k++)
                        {
                            if (ics[k] != null)
                            {
                                bool found = false;
                                for (int l = 0; l < nBins; l++)
                                {
                                    if (ics[l] != null && PrimaryCurves.GroupsCurves[i].PrimaryIndex == ics[l].PrimaryIndex) found = true;
                                }
                                if (!found) sum += LSFs[ics[k].PrimaryIndex, PrimaryCurves.GroupsCurves[i].PrimaryIndex];
                            }
                        }
                        if (sum > FarthestLSF)
                        {
                            FarthestLSF = sum;
                            FarthestIndex = i;
                        }
                    }
                    ics[j] = PrimaryCurves.GroupsCurves[FarthestIndex];

                }
                for (int i = 0; i < ics.Length; i++)
                    GroupBins.Bins[i].GroupsCurves.Add(ics[i]);

                //randomly distribute the curves through the bins
                int ii = 0;
                foreach (IndividualCurveStats kp in PrimaryCurves.GroupsCurves)
                {
                    ii++;
                    //Random rnd = new Random();
                    int c = (int)((double)ii * (double)(GroupBins.Bins.Count) / (double)PrimaryCurves.GroupsCurves.Count);    // (rnd.Next(GroupBins.Count*10 )/10);
                    if (c >= GroupBins.Bins.Count) c = GroupBins.Bins.Count - 1;
                    bool Found = false;
                    for (int k = 0; k < ics.Length; k++)
                    {
                        if (kp.PrimaryIndex == ics[k].PrimaryIndex)
                            Found = true;
                    }
                    if (!Found)
                    {
                        GroupBins.Bins[c].GroupsCurves.Add(kp);//new IndividualCurveStats(kp.Value.PrimaryIndex, kp.Value));
                        ShakeBins(GroupBins, c, GroupBins.Bins[c].GroupsCurves.Count - 1, ref LSFs);
                    }
                }
            }
            else
            {
                GroupBins = PremadeBinsOptional;

            }

            //shake the bins
            double[,] Energies = new double[2, nShakes];
            int TotalNCurves = 0;
            for (int GroupBinNumber = 0; GroupBinNumber < GroupBins.Bins.Count; GroupBinNumber++)
                TotalNCurves += GroupBins.Bins.Count;

            Random rnd = new Random();

            long tShakes = nShakes;
            long tTotalNCurves = TotalNCurves;

            if (GroupBins.Bins.Count > 50)
            {
                tShakes = nShakes;
                
            }
            if (TotalNCurves > LSFs.GetLength(0))
                tTotalNCurves = LSFs.GetLength(0);
            for (int i = 0; i < tShakes; i++)
            {
                progressBar1.Value = (int)(100 * (double)i / (double)nShakes);
                for (int j = 0; j < tTotalNCurves; j++)
                {
                    int GroupBinNumber = (int)Math.Floor(rnd.NextDouble() * (double)GroupBins.Bins.Count);
                    if (!GroupBins.Bins[GroupBinNumber].Locked)
                    {
                        int ListNumber = (int)Math.Floor(rnd.NextDouble() * (double)GroupBins.Bins[GroupBinNumber].GroupsCurves.Count);
                        if (GroupBins.Bins[GroupBinNumber].GroupsCurves.Count != 0)
                                  MShakeBins(MaxLSF, GroupBins, GroupBinNumber, ListNumber, ref LSFs);
                    }
                    Application.DoEvents();
                }
                double sum = 0;
                for (int GroupBinNumber = 0; GroupBinNumber < GroupBins.Bins.Count; GroupBinNumber++)
                {
                    double d = GroupBins.Bins[GroupBinNumber].GroupSelfEnergyUnNormalized (ref LSFs);
                    if (!Double.IsNaN(d))
                         sum += d;
                }
                Energies[0, i] = i;
                Energies[1, i] = sum;
                Graphing.PlotData(zedGraphControl1, Energies, "Sort Metric","Total Metric","Run Step","");
            }
            //now turn the temperature down to zero and settle all the bins down
            /*for (int i = 0; i < nShakes; i++)
            {
                progressBar1.Value = (int)(100 * (double)i / (double)nShakes);
                for (int j = 0; j < TotalNCurves; j++)
                {
                    int GroupBinNumber = (int)Math.Floor(rnd.NextDouble() * (double)GroupBins.Bins.Count);
                    if (!GroupBins.Bins[GroupBinNumber].Locked)
                    {
                        int ListNumber = (int)Math.Floor(rnd.NextDouble() * (double)GroupBins.Bins[GroupBinNumber].GroupsCurves.Count);
                        if ( GroupBins.Bins[GroupBinNumber].GroupsCurves.Count!=0 && ShakeToo ==true )
                                ShakeBins(GroupBins, GroupBinNumber, ListNumber, ref LSFs);
                    }
                    Application.DoEvents();
                }
                double sum = 0;
                for (int GroupBinNumber = 0; GroupBinNumber < GroupBins.Bins.Count; GroupBinNumber++)
                {
                    double d = GroupBins.Bins[GroupBinNumber].GroupSelfEnergy(ref LSFs);
                    sum += d;
                }
                Energies[0, i] = i;
                Energies[1, i] = sum;
                Graphing.PlotData(zedGraphControl1, Energies, "Energies","Standard Dev.","Cummul.","");
            }*/



            return GroupBins;

        }

        private static void MShakeBins(double MaxLSF, GroupBins GroupBins, int GroupBinNumber, int ListNumber, ref double[,] LSFs)
        {
            double[] SumLSF = new double[GroupBins.Bins.Count];
            IndividualCurveStats TestCurve = GroupBins.Bins[GroupBinNumber].GroupsCurves[ListNumber];
            Random rnd = new Random();
            if (TestCurve.Fixed == false)
            {
                int PrimaryIndex = TestCurve.PrimaryIndex;
                double[] t = new double[GroupBins.Bins.Count];
                for (int k = 0; k < GroupBins.Bins.Count; k++)
                {
                    SumLSF[k] = GroupBins.Bins[k].InteractionEnergy(ref TestCurve, ref LSFs);
                }

                int MinIndex = 0;
                double MinValueLSF = 1000;
                //find the group bin that the curve fits in best 
                for (int k = 0; k < GroupBins.Bins.Count; k++)
                {
                    MinIndex = (int)(rnd.NextDouble() * GroupBins.Bins.Count); 
                    double Threshold = (SumLSF[MinIndex ]) / MaxLSF;
                    if (rnd.NextDouble ()>Threshold  )
                    {
                        MinValueLSF = SumLSF[k];
                        MinIndex = k;
                        k = GroupBins.Bins.Count;
                    }
                }
                if (MinIndex != GroupBinNumber)
                {
                    if (GroupBins.Bins[GroupBinNumber].GroupsCurves.Count > 1)
                    {
                        GroupBins.Bins[GroupBinNumber].GroupsCurves.Remove(TestCurve);
                        GroupBins.Bins[MinIndex].GroupsCurves.Add(TestCurve);
                    }
                }
            }
        }

        private static void ShakeBins(GroupBins GroupBins, int GroupBinNumber, int ListNumber, ref double[,] LSFs)
        {
            double[] SumLSF = new double[GroupBins.Bins.Count];
            IndividualCurveStats TestCurve = GroupBins.Bins[GroupBinNumber].GroupsCurves[ListNumber];
            if (TestCurve.Fixed == false)
            {
                int PrimaryIndex = TestCurve.PrimaryIndex;
                double[] t = new double[GroupBins.Bins.Count];
                for (int k = 0; k < GroupBins.Bins.Count; k++)
                {
                    SumLSF[k] = GroupBins.Bins[k].InteractionEnergy(ref TestCurve, ref LSFs);
                }

                int MinIndex = 0;
                double MinValueLSF = 1000;
                //find the group bin that the curve fits in best 
                for (int k = 0; k < GroupBins.Bins.Count; k++)
                {
                    if (SumLSF[k] < MinValueLSF)
                    {
                        MinValueLSF = SumLSF[k];
                        MinIndex = k;
                    }
                }
                if (MinIndex != GroupBinNumber)
                {
                    if (GroupBins.Bins[GroupBinNumber].GroupsCurves.Count > 1)
                    {
                        GroupBins.Bins[GroupBinNumber].GroupsCurves.Remove(TestCurve);
                        GroupBins.Bins[MinIndex].GroupsCurves.Add(TestCurve);
                    }
                }
            }
        }
    }
}