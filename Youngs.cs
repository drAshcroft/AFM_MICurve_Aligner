using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GroupCurves
{
    public partial class Youngs : Form
    {
        private GroupBin AllCurves=new GroupBin(0);
        private AFMCurve Standard;
        double[,] StandardIntegration;
        double[] AllYs;
        public Youngs()
        {
            InitializeComponent();
        }
        public Youngs(GroupBins DisplayGroups):this()
        {
            openFileDialog1.ShowDialog();
            try
            {
                Standard = FileHandler.LoadCSBin(openFileDialog1.FileName);
            }
            catch
            {
                return;
            }
            double[,] junk  = new double[2, Standard.Curve.GetLength(1)];

            double sum = 0;
            int cc = 0;
            //pre integrate the standard curve
            for (int i = Standard.Curve.GetLength(1)-2; i >=0 ; i--) 
            {
                if (Standard.Curve[1, i] != 0 && Standard.Curve[0,i]!=0)
                {
                    //System.Diagnostics.Debug.Print(Standard.Curve[0, i].ToString() + " " + Standard.Curve[1, i].ToString());
                    sum += Standard.Curve[1, i]*(Standard.Curve[0,i+1]-Standard.Curve[0,i]);
                    junk[0, cc] = Standard.Curve[1, i];
                    junk[1, cc] = sum;
                    cc++;
                }
            }
            //there is some useless junk in the standard curve, so filter it out.
            StandardIntegration = new double[2, cc];
            for (int i = 0; i < cc; i++)
            {
                StandardIntegration[0, i] = junk[0, i];
                StandardIntegration[1, i] = junk[1, i];
            }
            foreach (GroupBin gb in DisplayGroups.Bins)
            {
                AllCurves.GroupsCurves.AddRange(gb.GroupsCurves);
            }

            DoYoungs();
        }
       
        private void DoYoungs()
        {
            List<double> Ys=new List<double>();
            double MaxStand = StandardIntegration[0, StandardIntegration.GetLength(1)-2];
            for (int j = 0; j < AllCurves.GroupsCurves.Count; j++)
            {
                string Filename = AllCurves.GroupsCurves[j].DataPath 
                    + "\\" + AllCurves.GroupsCurves[j].Filename;
                AFMCurve ac = FileHandler.LoadCSBin(Filename);
                double sum = 0;
                double MaxY = 0;
                for (int i = 0; i < ac.Curve.GetLength(1)-1 && ac.Curve[1,i]<MaxStand ; i++)
                {
                    sum += ac.Curve[1, i]*(ac.Curve[0,i]-ac.Curve[0,i+1] );
                    if (ac.Curve[1,i]>MaxY) MaxY=ac.Curve[1,i];
                }
                double standSum=0;
                if (MaxY >= MaxStand)
                {
                    standSum = StandardIntegration[1,0];
                }
                else
                {
                    for (int i = 0; i < StandardIntegration.GetLength(1) && standSum == 0; i++)
                    {
                        if (StandardIntegration[0, i] > MaxY)
                        {
                            standSum = StandardIntegration[1, i];
                        }
                    }
                }
                if (standSum != 0)
                {
                    Ys.Add( (sum/ standSum) );
                }
            }
            Ys.Sort();
            double[,] Data = new double[2, Ys.Count];
            for (int i = 0; i < Ys.Count; i++)
            {
                Data[0, i] = Ys[i];
                Data[1, i] = i;
            }
            Graphing.PlotData(zedGraphControl1, Data, "Ys","Young's Mod","Cummul.","S Curve of Young's Mod");
            AllYs = Ys.ToArray();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double[,] Data = new double[2, AllYs.Length ];
            for (int i = 0; i <AllYs.Length ; i++)
            {
                if (AllYs[i] > 0)
                {
                    Data[0, i] = Math.Log(AllYs[i]);
                    Data[1, i] = i;
                }
            }
            Graphing.PlotData(zedGraphControl1, Data, "Ys","Log of Standard Dev.","Cummul.","");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double sum=0;
            for (int i = 0; i < AllYs.Length; i++)
            {
                sum = sum + AllYs[i];

            }
            double Average = sum / (double)AllYs.Length;
            double d=0;
            sum = 0;
            for (int i = 0; i < AllYs.Length; i++)
            {
                d=(AllYs[i]-Average);
                sum = sum + d*d ;

            }
            double sd = Math.Sqrt(sum / (double)AllYs.Length);
            double lCut = Average -1.5* sd;
            double hCut = Average + 1.5*sd;

            double[,] Data = new double[2, AllYs.Length];
            for (int i = 0; i < AllYs.Length; i++)
            {
                if (AllYs[i] > lCut && AllYs[i]<hCut )
                {
                    Data[0, i] = AllYs[i];
                    Data[1, i] = i;
                }
            }
            Graphing.PlotData(zedGraphControl1, Data, "Ys","Standard Dev.","Cummul.","");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            TextWriter tw;
            if (saveFileDialog1.FileName != "")
            {
                tw = new StreamWriter(saveFileDialog1.FileName);
                for (int i = 0; i < AllYs.Length; i++)
                {
                    tw.WriteLine(AllYs[i] + "\t" + i);
                }
                tw.Close();
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double lCut =0;
            double hCut = 3;

            double[,] Data = new double[2, AllYs.Length];
            for (int i = 0; i < AllYs.Length; i++)
            {
                if (AllYs[i] > lCut && AllYs[i] < hCut)
                {
                    Data[0, i] = AllYs[i];
                    Data[1, i] = i;
                }
            }
            Graphing.PlotData(zedGraphControl1, Data, "Ys", "Standard Dev.","Cummul.","");
        }
    }
}
