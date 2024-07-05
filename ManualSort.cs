using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GroupCurves
{
    public partial class ManualSort : Form
    {
        public GroupBin SourceBin;
        public List< GroupBin > OutBins=new List<GroupBin>();
        public GroupBin TrashBin;
        public GroupBins AllGroups;
        Form1 MainForm;
        int CurrentCurveIndex = 0;
        public ManualSort()
        {
            InitializeComponent();
        }
        public void LoadCurves(Form1 MainForm, GroupBin SortBin,GroupBins AllGroups,GroupBins TrashBins)
        {
            this.MainForm = MainForm;
            this.AllGroups = AllGroups;
            SourceBin = SortBin;
            TrashBin = new GroupBin(AllGroups.GetNextBinNumber());
            //OutBins=new GroupBin[3];
            /*for (int i=0;i<OutBins.Length ;i++)
            {
                OutBins[i]=new GroupBin(AllGroups.GetNextBinNumber());
                //AllGroups.Bins.Add(OutBins[i]);
            }*/
            TrashBins.Bins.Add(TrashBin);

            for (int i = 0; i < SourceBin.GroupsCurves.Count; i++)
            {
                lbGroupFiles.Items.Add(SourceBin.GroupsCurves[i].Filename);

            }
            //lbGroupFiles.SelectedIndex = 0;
            PlotCurve(MainForm.GetFilenameFromCurve(SourceBin.GroupsCurves[0]));

            label1.Text =
@"To sort the curves by hand, you must:
1:   Assign names to the groups.  The names will just be hints to help you sort.  You can make more groups at any time
2:   Use the numeric keyboard to select the group you want to put the current curve into.  The program will automatically progress to the next curve
3:   If a curve is bad, you can place it in the trash bin by pressing T or clicking the button.
4:   You may want to wait to sort a curve, you can use the space bar or click the skip button to return to the curve later.
5:   Click Done to save the sorting to the main program.
";
        }

        private void PlotCurve(string Filename)
        {
            AFMCurve ac =FileHandler. LoadCSBin(MainForm.DataPath + "\\" + Filename);
                   
            double dx=ac.MinX ;
            ac.OffsetCurve(-1 * dx);
            int nPoints = ac.Curve.GetLength(1);
            double[,] Line = new double[2, nPoints];
            int cc = 0;
            int stepK = 1;//(int)Math.Floor((double)(ac.Curve.GetLength(1) / 25));
            if (stepK == 0) stepK = 1;
            for (int k = 0; k < ac.Curve.GetLength(1); k += stepK)
            {
                if (cc < nPoints)
                {
                    Line[0, cc] = ac.Curve[0, k];
                    Line[1, cc] = ac.Curve[1, k];
                    cc++;
                }
            }
            for (int k = cc - 1; k < nPoints; k++)
            {
                Line[0, k] = Line[0, k - 3];
                Line[1, k] = Line[1, k - 3];
            }
            Color UseColor=Color.Black ;
            List<Graphing.LineInfo> DisplayLines = new List<Graphing.LineInfo>();
            DisplayLines.Add(new Graphing.LineInfo(Line, UseColor, ""));
            Graphing.PlotData(zedGraphControl1, DisplayLines,"Extention(nm)","Deflection(V)","");

        }

        private void RelistGroup()
        {
            lbGroupFiles.Items.Clear();
            for (int i = 0; i < SourceBin.GroupsCurves.Count; i++)
            {
                lbGroupFiles.Items.Add(SourceBin.GroupsCurves[i].Filename);

            }
            lbGroupFiles.SelectedIndex = CurrentCurveIndex ;

        }
        private void bSkip_Click(object sender, EventArgs e)
        {
            ++CurrentCurveIndex;
            if (CurrentCurveIndex + 1 > SourceBin.GroupsCurves.Count)
                CurrentCurveIndex = 0;
            lbGroupFiles.SelectedIndex = CurrentCurveIndex;
            PlotCurve(MainForm.GetFilenameFromCurve(SourceBin.GroupsCurves[CurrentCurveIndex ]));
        }

        private void bTrash_Click(object sender, EventArgs e)
        {
            IndividualCurveStats ics = SourceBin.GroupsCurves[CurrentCurveIndex ];
            TrashBin.GroupsCurves.Add(ics);
            SourceBin.GroupsCurves.Remove(ics);
            if (CurrentCurveIndex + 1 > SourceBin.GroupsCurves.Count)
                CurrentCurveIndex = 0;
            lbGroupFiles.SelectedIndex = CurrentCurveIndex;
            RelistGroup();
            PlotCurve(MainForm.GetFilenameFromCurve(SourceBin.GroupsCurves[CurrentCurveIndex]));
       
        }

        private void lbGroupFiles_MouseUp(object sender, MouseEventArgs e)
        {
            CurrentCurveIndex = lbGroupFiles.SelectedIndex;
            if (CurrentCurveIndex + 1 > SourceBin.GroupsCurves.Count)
                CurrentCurveIndex = 0;
            PlotCurve(MainForm.GetFilenameFromCurve(SourceBin.GroupsCurves[CurrentCurveIndex]));
       
        }

        private void BGroupA_Click(object sender, EventArgs e)
        {
            IndividualCurveStats ics = SourceBin.GroupsCurves[CurrentCurveIndex];
            OutBins[0].GroupsCurves.Add(ics);
            SourceBin.GroupsCurves.Remove(ics);
            if (CurrentCurveIndex + 1 > SourceBin.GroupsCurves.Count)
                CurrentCurveIndex = 0;
            lbGroupFiles.SelectedIndex = CurrentCurveIndex;
            RelistGroup();
            PlotCurve(MainForm.GetFilenameFromCurve(SourceBin.GroupsCurves[CurrentCurveIndex]));
       
        }

        private void bGroupB_Click(object sender, EventArgs e)
        {
            IndividualCurveStats ics = SourceBin.GroupsCurves[CurrentCurveIndex];
            OutBins[1].GroupsCurves.Add(ics);
            SourceBin.GroupsCurves.Remove(ics);
            if (CurrentCurveIndex + 1 > SourceBin.GroupsCurves.Count)
                CurrentCurveIndex = 0;
            lbGroupFiles.SelectedIndex = CurrentCurveIndex;
            RelistGroup();
            PlotCurve(MainForm.GetFilenameFromCurve(SourceBin.GroupsCurves[CurrentCurveIndex]));
       
        }

        private void bGroupC_Click(object sender, EventArgs e)
        {
            IndividualCurveStats ics = SourceBin.GroupsCurves[CurrentCurveIndex];
            OutBins[2].GroupsCurves.Add(ics);
            SourceBin.GroupsCurves.Remove(ics);
            if (CurrentCurveIndex + 1 > SourceBin.GroupsCurves.Count)
                CurrentCurveIndex = 0;
            lbGroupFiles.SelectedIndex = CurrentCurveIndex;
            RelistGroup();
            PlotCurve(MainForm.GetFilenameFromCurve(SourceBin.GroupsCurves[CurrentCurveIndex]));
       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void bTrash_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           // need to finish the Manual work 

            GroupName gn = new GroupName();
            gn.ShowDialog();
            OutBins.Add(new GroupBin(lGroupNames.Items.Count));
            if (gn.Groupname.Trim() != "")
                lGroupNames.Items.Add(lGroupNames.Items.Count .ToString() + " : " +  gn.Groupname);
            else
                lGroupNames.Items.Add(lGroupNames.Items.Count.ToString());
           
        }

        private void ManualSort_KeyUp(object sender, KeyEventArgs e)
        {
            int BinN =( e.KeyValue - 96 );
           
            if (  BinN  >=0 && BinN  <10 && BinN <OutBins.Count    )
            {
                IndividualCurveStats ics = SourceBin.GroupsCurves[CurrentCurveIndex];
                OutBins[BinN ].GroupsCurves.Add(ics);
                SourceBin.GroupsCurves.Remove(ics);
                if (CurrentCurveIndex + 1 > SourceBin.GroupsCurves.Count)
                    CurrentCurveIndex = 0;
                lbGroupFiles.SelectedIndex = CurrentCurveIndex;
                RelistGroup();
                PlotCurve(MainForm.GetFilenameFromCurve(SourceBin.GroupsCurves[CurrentCurveIndex]));
                e.Handled = true;
            }
            if (e.KeyData == Keys.Space)
            {
                bSkip_Click(sender, e);
                e.Handled = true;
            }
            if (e.KeyData == Keys.T)
            {
                bTrash_Click(sender, e);
                e.Handled = true;
            }
        }

        private void button2_KeyUp(object sender, KeyEventArgs e)
        {
            ManualSort_KeyUp(sender, e);
        }

        private void bSkip_KeyUp(object sender, KeyEventArgs e)
        {
            ManualSort_KeyUp(sender, e);
        }

        private void bTrash_KeyUp(object sender, KeyEventArgs e)
        {
            ManualSort_KeyUp(sender, e);
        }

        private void lGroupNames_KeyUp(object sender, KeyEventArgs e)
        {
            ManualSort_KeyUp(sender, e);
        }

        private void zedGraphControl1_KeyUp(object sender, KeyEventArgs e)
        {
            ManualSort_KeyUp(sender, e);
        }

        private void lbGroupFiles_KeyUp(object sender, KeyEventArgs e)
        {
            ManualSort_KeyUp(sender, e);
        }

        private void button1_KeyUp(object sender, KeyEventArgs e)
        {
            ManualSort_KeyUp(sender, e);
        }

        private void bHint_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            bHint.Visible = false;
        }
    }
}
