using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace GroupCurves
{
    public class ChainAssign
    {
        private static  void SortList(ref List<CurveInfo> l)
        {
            l.Sort(delegate(CurveInfo c1, CurveInfo c2) { return c1.LSF.CompareTo(c2.LSF); });

        }
        public static GroupBins GroupAssign(ZedGraph.ZedGraphControl zedGraphControl1, ProgressBar progressBar1, int MaxPrimaryIndex, GroupBin OriginalGroups)
        {

            //int Relations = 0;
            double[] CutPoints = new double[MaxPrimaryIndex ];

            //build a frequency integral for each primary point and then find the cutoff point of 
            //curves that are similar at some ratio of the max of the integral.  May be better to 
            //just have a fixed max value?
            double SumCuts = 0;
            double Count = 0;
            foreach (IndividualCurveStats kp in OriginalGroups.GroupsCurves)
            {
                int PrimaryIndex = kp.PrimaryIndex;
                List<CurveInfo> c = kp.Relations;
                SortList(ref c);
                double[,] Histogram = new double[2, c.Count];
                double Sum = 0;

                int HC = 0;
                for (int i = 0; i < c.Count; i++)
                {
                    if (OriginalGroups.ContainsCurve(c[i].SecondaryIndex))
                    {
                        Histogram[0, HC] = c[i].LSF;
                        Sum++;
                        Histogram[1, HC] = Sum;
                        HC++;
                    }
                }
                double d;
                d = Sum / 10;
                Count++;
                progressBar1.Value = (int)(50 * (double)Count / (double)OriginalGroups.GroupsCurves.Count);
                //once we have the max.  walk down the line to find the first point that is above this value.  
                //this is the cutoff lsf value.
                bool Found = false;
                for (int i = 0; i < HC && !Found; i++)
                {
                    if (Histogram[1, i] > d)
                    {
                        try
                        {
                            CutPoints[PrimaryIndex] = Histogram[0, i];
                            SumCuts += Histogram[0, i];
                            Found = true;
                        }
                        catch { }
                    }
                }

                //plotting is fun, but slows down the processing
                Graphing.PlotData(zedGraphControl1, Histogram, "","Standard Deviation","Cummul.","");
                // this.zedGraphControl1.Invalidate();
                // this.zedGraphControl1.Refresh();
               // zedGraphControl1.Copy(false);
                Application.DoEvents();
            }
            SumCuts = SumCuts / OriginalGroups.GroupsCurves.Count;

            AffiliationInfo[] Groups = new AffiliationInfo[MaxPrimaryIndex ];
            //GroupBins Affiliation = new GroupBins();
            //RelationsCount =0;
            int GroupCount = 1;
           // Bitmap GroupsIndicator=new Bitmap(1000,1000);
           
           // double gConv = 1000d / (double)OriginalGroups.GroupsCurves.Count;
            Color[] QColors = new Color[11];
            QColors[0] = Color.Black;
            QColors[1] = Color.Blue;
            QColors[2] = Color.Green;
            QColors[3] = Color.Yellow;
            QColors[4] = Color.Red;
            QColors[5] = Color.PowderBlue;
            QColors[6] = Color.Purple;
            QColors[7] = Color.Tomato;
            QColors[8] = Color.Violet;
            QColors[9] = Color.White;
            QColors[10] = Color.YellowGreen;
            //todo: this assumes that the whole relations dataset is here.  It needs to make primaryindex to something in this
            //groupbin
           // int[,] GroupGrid = new int[1000, 1000];
            foreach (IndividualCurveStats kp in OriginalGroups.GroupsCurves)
            {
                List<CurveInfo> c = kp.Relations;//.Value;
               
                for (int i = 0; i < c.Count; i++)
                {
                    if (c[i].PrimaryIndex != c[i].SecondaryIndex)
                    {
                        //search through all the primary curves and attach those secondary curves that fall below
                        //this primary curves threshold.
                        //if (c[i].LSF < CutPoints[c[i].PrimaryIndex])
                        if (c[i].LSF < CutPoints[kp.PrimaryIndex])
                        {
                            //make sure that the secondary curve is in this group
                            if (OriginalGroups.ContainsCurve(c[i].SecondaryIndex))
                            {
                                int PrimaryIndexCount = c[i].PrimaryIndex;
                                int SecondaryIndexCount = c[i].SecondaryIndex;
                                int CurrentGroup = 0;
                                bool Found = false;
                                //check to make sure that the primary curve has a group before assigning the second curves group
                                if (Groups[PrimaryIndexCount] == null || Groups[PrimaryIndexCount].GroupNumber == 0)
                                {
                                   // Groups[PrimaryIndexCount] = new AffiliationInfo(GroupCount, c[i].LSF);
                                    Groups[PrimaryIndexCount] = new AffiliationInfo(GroupCount, CutPoints[kp.PrimaryIndex]);
                                    CurrentGroup = GroupCount;
                                    GroupCount++;
                                    //  GroupGrid[(int)(PrimaryIndexCount * gConv), (int)(PrimaryIndexCount * gConv)] =CurrentGroup ;
                                    //  GroupsIndicator.SetPixel((int)(PrimaryIndexCount * gConv), (int)(PrimaryIndexCount * gConv), QColors[CurrentGroup % 10]);
                                }
                                else
                                    CurrentGroup = Groups[PrimaryIndexCount].GroupNumber;

                                //if the curve does not already have a group, assign it to this group
                                if (Groups[SecondaryIndexCount] == null || Groups[SecondaryIndexCount].GroupNumber == 0)
                                {
                                    Groups[SecondaryIndexCount] = new AffiliationInfo(CurrentGroup, CutPoints[c[i].SecondaryIndex]);
                                    //   GroupGrid[(int)(PrimaryIndexCount * gConv), (int)(SecondaryIndexCount * gConv)] = CurrentGroup;
                                    //   GroupGrid[(int)(SecondaryIndexCount * gConv),(int)(PrimaryIndexCount * gConv) ] = CurrentGroup;
                                    //   GroupsIndicator.SetPixel((int)(PrimaryIndexCount * gConv), (int)(SecondaryIndexCount * gConv), QColors[CurrentGroup % 10]);
                                    //   GroupsIndicator.SetPixel((int)(SecondaryIndexCount * gConv), (int)(PrimaryIndexCount * gConv), QColors[CurrentGroup % 10]);

                                }
                                else if (Groups[SecondaryIndexCount].Tightness >= c[i].LSF)
                                {
                                   // Groups[SecondaryIndexCount].GroupNumber = CurrentGroup;
                                   // Groups[SecondaryIndexCount].Tightness = CutPoints[c[i].SecondaryIndex ];
                                    //  GroupGrid[(int)(PrimaryIndexCount * gConv), (int)(SecondaryIndexCount * gConv)] = CurrentGroup;
                                    //  GroupsIndicator.SetPixel((int)(PrimaryIndexCount * gConv), (int)(SecondaryIndexCount * gConv), QColors[CurrentGroup % 10]);
                                    //  GroupsIndicator.SetPixel((int)(SecondaryIndexCount * gConv), (int)(PrimaryIndexCount * gConv), QColors[CurrentGroup % 10]);
                                }


                            }
                        }
                    }
                }

                
                
            }
            //Clipboard.Clear();
            //Clipboard.SetImage(GroupsIndicator);

            GroupBins ReturnGroups = new GroupBins();
            for (int i = 0; i < Groups.Length; i++)
            {
                if (i == 169)
                    System.Diagnostics.Debug.Print("");
                if (Groups[i] == null || Groups[i].GroupNumber == 0)
                {
                    if (OriginalGroups.ContainsCurve(i))
                    {
                        ReturnGroups.Bins.Add(new GroupBin(GroupCount));
                        ReturnGroups.Bins[ReturnGroups.Bins.Count - 1].GroupsCurves.Add(OriginalGroups.GetCurve(i));
                        GroupCount++;
                    }
                }
                else
                {
                    bool Found = false;
                    for (int j = 0; j < ReturnGroups.Bins.Count; j++)
                    {
                        if (ReturnGroups.Bins[j].BinNumber == Groups[i].GroupNumber)
                        {
                            ReturnGroups.Bins[j].GroupsCurves.Add(OriginalGroups.GetCurve(i));
                            Found = true;
                        }
                    }
                    if (!Found)
                    {
                        ReturnGroups.Bins.Add(new GroupBin(Groups[i].GroupNumber));
                        ReturnGroups.Bins[ReturnGroups.Bins.Count - 1].GroupsCurves.Add(OriginalGroups.GetCurve(i));
                    }
                }


            }
            GroupBin Stragglers = new GroupBin(GroupCount++);
            List<int> RemoveIndex = new List<int>();
            for (int i = ReturnGroups.Bins.Count-1; i >0; i--)
            {
                if (ReturnGroups.Bins[i].GroupsCurves.Count <= 2)
                {
                    Stragglers.GroupsCurves.AddRange  (ReturnGroups.Bins[i].GroupsCurves.ToArray());
                    RemoveIndex.Add(i);
                }
            }
            for (int i = 0; i < RemoveIndex.Count; i++)
                ReturnGroups.Bins.RemoveAt(RemoveIndex[i]);
            if (Stragglers.GroupsCurves.Count > 10)
            {
                ReturnGroups.Bins.AddRange(GroupAssign(zedGraphControl1, progressBar1, MaxPrimaryIndex, Stragglers).Bins);
                Stragglers = new GroupBin(GroupCount++);
                RemoveIndex = new List<int>();
                for (int i = ReturnGroups.Bins.Count - 1; i > 0; i--)
                {
                    if (ReturnGroups.Bins[i].GroupsCurves.Count <= 2)
                    {
                        Stragglers.GroupsCurves.AddRange(ReturnGroups.Bins[i].GroupsCurves.ToArray());
                        RemoveIndex.Add(i);
                    }
                }
                
            }
            try
            {
                if (Stragglers.GroupsCurves.Count >0)
                     ReturnGroups.Bins.Add(Stragglers);
            }
            catch { }
            return ReturnGroups;

        }

    }
}
