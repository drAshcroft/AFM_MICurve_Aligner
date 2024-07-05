using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GroupCurves
{
    public class Bifurification
    {
        private static  GroupBins ArraingPresorts(GroupBins Presorts, GroupBin GoodSort, GroupBin BadSort, out bool Done)
        {
            if (BadSort.GroupsCurves.Count < 5)
                Presorts.Bins[0].GroupsCurves.AddRange(BadSort.GroupsCurves);
            else
                Presorts.Bins.Add(BadSort);

            Presorts.Bins[1] = GoodSort;
            if (GoodSort.GroupsCurves.Count < 50)
                Done = true;
            else
                Done = false;

            return (Presorts);
        }

        public static  GroupBins Bifuricate(ref double[,] LSFs, ProgressBar progressBar1, GroupBin InBin)
        {
            GroupBins Presorts = new GroupBins();
            Presorts.Bins.Add(new GroupBin(0));
            Presorts.Bins.Add(InBin);
            bool Done = false;
            for (int i = 0; i < 300 && !Done; i++)
            {
                progressBar1.Value = (int)(100 * (double)i / 350);
                GroupBins Presort = GroupCurves.Sorts.ShakeSorter.StaggerShake(null,null,progressBar1, ref LSFs, Presorts.Bins[1], 2, 1);
               
               // GroupBins Presort =SimpleShaker.ShakeAssign(ref LSFs, progressBar1, null, 2, 3,OutBin , false);
                if (Presort.Bins[0].GroupsCurves.Count < Presort.Bins[1].GroupsCurves.Count)
                {
                    Presorts = ArraingPresorts(Presorts, Presort.Bins[1], Presort.Bins[0], out  Done);
                }
                else
                {
                    Presorts = ArraingPresorts(Presorts, Presort.Bins[0], Presort.Bins[1], out  Done);
                }

            }
            List<int> Removes = new List<int>();
            for (int i = Presorts.Bins.Count-1; i >=0 ; i--)
            {
                if (Presorts.Bins[i].GroupsCurves.Count == 0)
                    Removes.Add(i);
            }
            for (int i = 0; i < Removes.Count; i++)
                Presorts.Bins.RemoveAt(Removes[i]);
            return Presorts;
        }
    }
}
