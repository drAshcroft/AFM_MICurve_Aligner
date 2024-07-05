using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GroupCurves.Sorts
{
    public class ShakeSorter
    {
        public static GroupBins StaggerShake(TextWriter tw, ZedGraph.ZedGraphControl zedGraphControl1, ProgressBar progressBar1, ref double[,] LSFs, GroupBin StartBin, int NumBins, int Depth)
        {
            Depth--;

          
                GroupBins  GroupBins = new GroupBins();
                for (int i = 0; i < NumBins; i++)
                {
                    GroupBins.Bins.Add(new GroupBin(GroupBins.GetNextBinNumber()));   //(i, new List<IndividualCurveStats>());
                }

                IndividualCurveStats[] ics = new IndividualCurveStats[NumBins];
                ics[0] = StartBin.GroupsCurves[0];
                double FarthestLSF = -10000;
                int FarthestIndex = 0;
                for (int j = 1; j < NumBins; j++)
                {

                    for (int i = 0; i < StartBin.GroupsCurves.Count; i++)
                    {
                        double sum = 0;
                        for (int k = 0; k < NumBins; k++)
                        {
                            if (ics[k] != null)
                            {
                                bool found = false;
                                for (int l = 0; l < NumBins && !found; l++)
                                {
                                    if (ics[l] != null && StartBin.GroupsCurves[i].PrimaryIndex == ics[l].PrimaryIndex) found = true;
                                }
                                if (!found) sum += LSFs[ics[k].PrimaryIndex, StartBin.GroupsCurves[i].PrimaryIndex];
                            }
                        }
                        if (sum > FarthestLSF)
                        {
                            FarthestLSF = sum;
                            FarthestIndex = i;
                        }
                    }
                    ics[j] = StartBin.GroupsCurves[FarthestIndex];

                }
                for (int i = 0; i < ics.Length; i++)
                    GroupBins.Bins[i].GroupsCurves.Add(ics[i]);

                //randomly distribute the curves through the bins
                int ii = 0;
                foreach (IndividualCurveStats kp in StartBin.GroupsCurves)
                {
                    ii++;
                    //Random rnd = new Random();
                    int c = (int)((double)ii * (double)(GroupBins.Bins.Count) / (double)StartBin.GroupsCurves.Count);    // (rnd.Next(GroupBins.Count*10 )/10);
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
                        SimpleShaker.ShakeBins(GroupBins, c, GroupBins.Bins[c].GroupsCurves.Count - 1, ref LSFs);
                    }
                }
            

            GroupBins stage1 = SimpleShaker.ShakeAssign(ref LSFs, progressBar1, zedGraphControl1,  10,GroupBins , false);
            GroupBins NewBins = new GroupBins();

            if (Depth > 0)
            {
                for (int i = 0; i < stage1.Bins.Count; i++)
                {
                    if (stage1.Bins[i].GroupsCurves.Count > 30)
                    {
                        GroupBins Groups = StaggerShake(tw, zedGraphControl1, progressBar1, ref LSFs, stage1.Bins[i], NumBins, Depth);
                        NewBins.Bins.AddRange(Groups.Bins);
                    }
                    else
                        NewBins.Bins.Add(stage1.Bins[i]);

                }
            }
            else
            {
                return stage1;
            }
            return NewBins;
        }

    }
}
