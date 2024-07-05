using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
using System.IO;
namespace GroupCurves
{
    public partial class GroupVision : Form
    {
        private GroupBins CurrentBins;
        List< double[,]> AllCurves=new List<double[,]>();

        public GroupVision()
        {
            InitializeComponent();       
        }
        public GroupVision(GroupBins DisplayGroups):this()
        {
            CurrentBins = DisplayGroups;
            foreach (GroupBin gb in CurrentBins.Bins)
            {
                double[,] datas = gb.AverageGroup(null);
                double[,] Curve=new double[2,datas.GetLength(1)];
                for (int i = 0; i < datas.GetLength(1); i++)
                {
                    Curve[0, i] = datas[0, i];
                    Curve[1, i] = datas[2, i];
                }
                AFMCurve curve = new AFMCurve();
                curve.Curve = Curve;
                curve.SetMaxes();
                AllCurves.Add (gb.AverageGroup(curve ));
            }
            if (CurrentBins.Bins.Count == 1)
                PlotAverages(true);
            else 
                PlotAverages(false);
        }

        private void GroupVision_Load(object sender, EventArgs e)
        {

        }
        private void PlotAverages(bool ShowStandardDev)
        {
            List<Graphing.LineInfo> Datas = new List<Graphing.LineInfo>();
            Random rnd=new Random();
            foreach (double[,] Curves in AllCurves)
            {

                Color c = Color.Red; // Color.FromArgb(rnd.Next(100000));
                double[,] Average = new double[2, Curves.GetLength(1)];
                double[,] HighLine = new double[2, Curves.GetLength(1)];
                double[,] LowLine = new double[2, Curves.GetLength(1)];

                for (int i = 0; i < Curves.GetLength(1); i++)
                {
                    Average[0, i] = Curves[0, i];
                    Average[1, i] = Curves[2, i];

                    HighLine[0, i] = Curves[0, i];
                    HighLine[1, i] = Curves[3, i];

                    LowLine[0, i] = Curves[0, i];
                    LowLine[1, i] = Curves[1, i];

                }

                Datas.Add(new Graphing.LineInfo(Average, c, "Average"));
                if (ShowStandardDev)
                {
                    Datas.Add(new Graphing.LineInfo(HighLine, c, "HighLine"));
                    Datas.Add(new Graphing.LineInfo(LowLine, c, "LowLine"));
                }
            }
            Graphing.PlotData(zedGraphControl1, Datas,"Extension(nm)","Deflection(V)","");
            this.BringToFront();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            TextWriter TW = new StreamWriter(saveFileDialog1.FileName );
            
            string junk="";
            int MaxX = 0;
            foreach (double[,] curve in AllCurves)
            {
                if (MaxX < curve.GetLength(1)) MaxX = curve.GetLength(1);

            }
            for (int i = 0; i < MaxX ; i+=MaxX /500)
            {
                junk += i.ToString() + "\t";
                for (int j = 0; j < AllCurves.Count; j++)
                {
                    double[,] Curves = AllCurves[j];
                    

                    if (i < Curves.GetLength(1))
                    {
                        junk += Curves[1, i].ToString() + "\t";
                        if (cbStandDev.Checked)
                        {
                            junk += Curves[2, i].ToString() + "\t";
                            junk += Curves[3, i].ToString() + "\t";
                        }
                    }
                    else
                    {
                        junk +=  " \t";
                        if (cbStandDev.Checked)
                        {
                            junk +=  " \t";
                            junk +=  " \t";
                        }
                    }
                    
                   
                }
                TW.WriteLine(junk);
                junk = "";
            }
            TW.Close ();
           
        }

        private void cbStandDev_CheckedChanged(object sender, EventArgs e)
        {
            if (cbStandDev.Checked)
                PlotAverages(true);
            else
                PlotAverages(false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            
            double[,] Curves= AllCurves[0];
            double[,] Average = new double[2, Curves.GetLength(1)];
            double[,] HighLine = new double[2, Curves.GetLength(1)];
            double[,] LowLine = new double[2, Curves.GetLength(1)];
            {

                for (int i = 0; i < Curves.GetLength(1); i++)
                {
                    Average[0, i] = Curves[0, i];
                    Average[1, i] = Curves[2, i];

                    HighLine[0, i] = Curves[0, i];
                    HighLine[1, i] = Curves[3, i];

                    LowLine[0, i] = Curves[0, i];
                    LowLine[1, i] = Curves[1, i];

                }

                
            }
            
            FileHandler.SaveCSBin(saveFileDialog1.FileName,"",0,0,Average);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not yet implemented");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string OutString = "";
            string junk = "";
            int MaxX = 0;
            foreach (double[,] curve in AllCurves)
            {
                if (MaxX < curve.GetLength(1)) MaxX = curve.GetLength(1);

            }
            for (int i = 0; i < MaxX; i += MaxX / 500)
            {
                junk += i.ToString() + "\t";
                for (int j = 0; j < AllCurves.Count; j++)
                {
                    double[,] Curves = AllCurves[j];


                    if (i < Curves.GetLength(1))
                    {
                        junk += Curves[1, i].ToString() + "\t";
                        if (cbStandDev.Checked)
                        {
                            junk += Curves[2, i].ToString() + "\t";
                            junk += Curves[3, i].ToString() + "\t";
                        }
                    }
                    else
                    {
                        junk += " \t";
                        if (cbStandDev.Checked)
                        {
                            junk += " \t";
                            junk += " \t";
                        }
                    }


                }
                OutString += junk + "\n";
                junk = "";
            }
            Clipboard.SetText(OutString);
        }
    }
}
