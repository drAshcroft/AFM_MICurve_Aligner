using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroupCurves
{
    public class GroupBins
    {
        public List<GroupBin> Bins = new List<GroupBin>();

       
        public void RemoveCurve(ref IndividualCurveStats curve)
        {
            foreach (GroupBin gb in Bins)
            {
               // try
                {
                    gb.GroupsCurves.Remove(curve);
                   
                }
               // catch { }

            }
        }
        public GroupBin GetBin(int BinNumber)
        {
            foreach (GroupBin GB in Bins)
            {
                if (GB.BinNumber == BinNumber)
                    return GB;
            }
            return null;
        }
        public bool ContainsBin(int BinNumber)
        {
            foreach (GroupBin GB in Bins)
            {
                if (GB.BinNumber == BinNumber)
                    return true;
            }
            return false;
        }

        public GroupBin MergeBins(List<GroupBin> SelectedBins)
        {
            List<GroupBin> NewBins = new List<GroupBin>();
            GroupBin Merged = new GroupBin(0);
            NewBins.Add(Merged);
            foreach (GroupBin gb in Bins)
            {
                if (!SelectedBins.Contains(gb))
                {
                    NewBins.Add(gb);
                }
                else
                {
                    Merged.GroupsCurves.AddRange(gb.GroupsCurves);
                }
            }
            Bins = NewBins;
            return Merged;
        }
        int BinNumbers = 0;
        public int GetNextBinNumber()
        {
            BinNumbers++;
            return BinNumbers;
        }
    }

}
