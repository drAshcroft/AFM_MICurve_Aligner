using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroupCurves
{
    public class IndividualCurveStats
    {
        public int PrimaryIndex;
        public string Filename;
        public string DataPath;
        public bool Fixed = false;
        public List<CurveInfo> Relations;
        public IndividualCurveStats(int PrimaryIndex, List<CurveInfo> Relations,string Filename,string DataPath)
        {
            this.PrimaryIndex = PrimaryIndex;
            this.Relations = Relations;
            this.Filename = Filename;
            this.DataPath = DataPath;
        }
        private void SortList(ref List<CurveInfo> l)
        {
            l.Sort(delegate(CurveInfo c1, CurveInfo c2) { return c1.LSF.CompareTo(c2.LSF); });

        }

        public double SortParam;//to be used to sort this list by whatever means we like
        public double GetHistogram(GroupBin TestBin, double CutRatio, out double[,] Histogram)
        {
            List<CurveInfo> c = Relations;
            SortList(ref c);
            Histogram = new double[2, c.Count];
            double Sum = 0;

            int HC = 0;
            for (int i = 0; i < c.Count; i++)
            {
                if (TestBin.ContainsCurve(c[i].SecondaryIndex))
                {
                    Histogram[0, HC] = c[i].LSF;
                    Sum++;
                    Histogram[1, HC] = Sum;
                    HC++;
                }
            }
            double CutPoint;
            double CutLSF=1;
            CutPoint  = Sum *CutRatio;
                
            bool Found = false;
            for (int i = 0; i < HC && !Found; i++)
            {
                if (Histogram[1, i] > CutPoint )
                {
                    CutLSF = Histogram[0, i];
                    Found = true;
                }
            }

            return CutLSF;   
         }
            

      }
    }

