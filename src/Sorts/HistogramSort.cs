using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GroupCurves.Sorts
{
    public class HistogramSort
    {
        public static GroupBins DoHistoSort(TextWriter tw, Graphing graph, ProgressBar progressBar1, ref double[,] LSFs, GroupBins SelBins)
        {
            GroupBins Groups=null ;
            foreach (GroupBin gb in SelBins.Bins)
            {
                if (gb.GroupsCurves.Count > 100 && gb.Locked == false)
                {
                    Groups = new GroupBins();
                    //Thread t=new Thread( delegate (){ gb.SortByHisto(graph ); } );
                    gb.SortByHisto(graph);
                    //t.Start();
                    //t.Join();
                    double MinLSF = double.NaN;
                    for (int i = 0; !(double.IsInfinity(MinLSF) == false && double.IsNaN(MinLSF) == false) && i < gb.GroupsCurves.Count; i++)
                        MinLSF = gb.GroupsCurves[i].SortParam;
                    double MaxLSF = double.NaN;
                    for (int i = 1; !(double.IsInfinity(MaxLSF) == false && double.IsNaN(MaxLSF) == false) && i < gb.GroupsCurves.Count; i++)
                        MaxLSF = gb.GroupsCurves[gb.GroupsCurves.Count - i].SortParam;

                    double Power = .5;
                    MinLSF = Math.Pow(Math.Abs(MinLSF), Power);
                    MaxLSF = Math.Pow(Math.Abs(MaxLSF), Power);
                    int nBins = 10;
                    for (int i = 0; i < nBins; i++)
                        Groups.Bins.Add(new GroupBin(Groups.GetNextBinNumber()));

                    for (int i = 0; i < gb.GroupsCurves.Count; i++)
                    {
                        int binN = (int)(nBins * .99 * ((Math.Pow(gb.GroupsCurves[i].SortParam, Power) - MinLSF) / (MaxLSF - MinLSF)));
                        if (binN < Groups.Bins.Count && binN >= 0)
                            Groups.Bins[binN].GroupsCurves.Add(gb.GroupsCurves[i]);
                    }
                }
                else
                {
                    Groups = new GroupBins();
                    Groups.Bins.Add(gb);
                }
                
            }
            return Groups;
        }
    }

}
