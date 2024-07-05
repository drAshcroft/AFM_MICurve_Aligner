using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ZedGraph;
using System.Threading;
namespace GroupCurves
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
      
        //List<CurveInfo> Curves = new List<CurveInfo>();
        Dictionary<string, int> CurveNumbers = new Dictionary<string, int>();
        public Dictionary<int, string> CurveNames = new Dictionary<int, string>();
        List< IndividualCurveStats > Relations = new List < IndividualCurveStats >();
        double MaxLSF = .1;
        double AverageLSF = .5;
        GroupBins AllGroups=new GroupBins ();
        public  double[,] CurveLookup;
        public List<CurveInfo> OriginalCurves = null;
        GroupBins TrashedGroups = new GroupBins();
        GroupBins LockedGroups = new GroupBins();
        GroupBins ManualGroups = new GroupBins();
        public string DataPath = "";

        double WholeInteractionEnergy = 0;
        string EnergyType = "Square Difference";

        private void ListBins()
        {
            int i = 1;
            listMain.Items.Clear();
            foreach (GroupBin gb in AllGroups.Bins)
            {
                gb.BinNumber = i;
                listMain.Items.Add(gb);
                i++;
            }
            i = 1;
            listTrash.Items.Clear();
            foreach (GroupBin gb in TrashedGroups.Bins)
            {
                gb.BinNumber = i;
                listTrash.Items.Add(gb);
                i++;
            }

            i = 1;
            listLocked.Items.Clear();
            foreach (GroupBin gb in LockedGroups.Bins)
            {
                gb.BinNumber = i;
                listLocked.Items.Add(gb);
                i++;
            }

            i = 1;
            lManual.Items.Clear();
            foreach (GroupBin gb in ManualGroups.Bins)
            {
                gb.BinNumber = i;
                lManual.Items.Add(gb);
                i++;
            }
        }
        
        private void DoLoadSorting(List<CurveInfo> Curves )
        {
            OriginalCurves = Curves;
            Relations = new List<IndividualCurveStats>();
            //this is called in a sub to make debugging easier.  delegates seem to cause problems
           // SortList(ref Curves);
            //sort out the curves based on the primary index so they are in nice lists.  This helps to
            //break out the groups
            Dictionary<int, IndividualCurveStats> TempRelations = new Dictionary<int, IndividualCurveStats>();
            foreach (CurveInfo c in Curves)
            {
                int PrimaryInfoName = c.PrimaryIndex;

                if (!TempRelations.ContainsKey(PrimaryInfoName))
                {
                    TempRelations.Add(PrimaryInfoName, new IndividualCurveStats(PrimaryInfoName, new List<CurveInfo>(),c.PrimaryFilename,DataPath  ));
                    TempRelations[PrimaryInfoName].Relations.Add(c);
                }
                else
                {
                    TempRelations[PrimaryInfoName].Relations.Add(c);
                }
            }
            foreach (KeyValuePair<int, IndividualCurveStats> kp in TempRelations)
            {
                Relations.Add(kp.Value);// = TempRelations.ToList<IndividualCurveStats>();
            }

            CurveLookup = new double[Relations.Count, Relations.Count];
            
            foreach (CurveInfo c in Curves)
            {
                c.LSF = GetCompareTerm(c);
                int PrimaryInfoName = c.PrimaryIndex;
                int SecInfoName = c.SecondaryIndex;
                CurveLookup[PrimaryInfoName, SecInfoName] = c.LSF;
            }

          

            GroupBin BigBin = new GroupBin(AllGroups.GetNextBinNumber());
            BigBin.GroupsCurves = Relations;
            AllGroups.Bins.Clear();
            AllGroups.Bins.Add(BigBin);
            listMain.Items.Add(BigBin);
            ListBins();


            WholeInteractionEnergy = BigBin.GroupSelfEnergy(ref CurveLookup);
           /* GroupBin[] bs = new GroupBin[3];
            bs[0] = new GroupBin(1);
            bs[1] = new GroupBin(2);
            bs[2] = new GroupBin(3);
            int cN=0;
            string junk;
            foreach (IndividualCurveStats ics in Relations)
            {
                junk =ics.Filename.ToLower().Replace("curve","");
                junk = junk.Replace(".csb", "");
                int.TryParse(junk,out cN);
                cN =(int)( (double)cN / 500.0d * 3);
                bs[cN].GroupsCurves.Add(ics);
                
            }

            double[,] Energy = new double[3, 3];
            string junkout = "";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Energy[i, j] = CrossEnergy(bs[i], bs[j], ref CurveLookup);
                    junkout += Energy[i, j].ToString() + "\t";
                }
                junkout += "\n";
            }
            System.Diagnostics.Debug.Print(junkout);
            

            AllGroups.Bins.Clear();
            AllGroups.Bins.Add(bs[0]);
            AllGroups.Bins.Add(bs[1]);
            AllGroups.Bins.Add(bs[2]);
            listMain.Items.Add(bs[0]);
            listMain.Items.Add(bs[1]);
            listMain.Items.Add(bs[2]);

            ListBins();*/

        }
  
        

        #region FileHandling
        public string GetFilenameFromCurve(IndividualCurveStats Curve)
        {

            return CurveNames[Curve.PrimaryIndex];
        }

        private void LoadDat(string[] Filenames, CurveSection DesiredSection, CurveSection FlattenStandard)
        {
            bool CombineFiles = true;
            string path = System.IO.Path.GetDirectoryName(Filenames[0]);
            DataPath = path;
            string[] ComboFiles = { "" };
            for (int i = 0; i < Filenames.Length; i++)
            {
                if (System.IO.Path.GetExtension(Filenames [i]).ToLower() == ".dat")
                {
                    DialogResult dr = MessageBox.Show("Do you wish to combine the files?", "", MessageBoxButtons.YesNo);// mb = new MessageBox();
                    if (dr == DialogResult.Yes)
                    {
                        i = Filenames.Length;

                    }
                    else
                        CombineFiles = false;
                }
            }
            string[] files;
            if (CombineFiles)
            {
                files = System.IO.Directory.GetFiles(path, "*.dat");
                string[] tComboFiles = System.IO.Directory.GetFiles(path, "*.ivs");
                ComboFiles = new string[files.Length];
                for (int i = 0; i < files.Length; i++)
                {
                    string tFilename = Path.GetFileNameWithoutExtension(files[i]);
                    for (int j = 0; j < tComboFiles.Length; j++)
                    {
                        if (tComboFiles[j].Contains(tFilename) == true)
                        {
                            ComboFiles[i] = tComboFiles[j];
                            j = tComboFiles.Length;
                        }
                    }
                }
            }
            else
                files = System.IO.Directory.GetFiles(path, "*.ivs");

            for (int i = 0; i < files.Length; i++)
            {
                AFMCurve aCurve = null;
                string OriginalFilename = files[i];
                if (CombineFiles)
                {
                    for (int j = 0; j < ComboFiles.Length; j++)
                    {
                        if (ComboFiles[j].Contains(System.IO.Path.GetFileNameWithoutExtension(files[i])))
                        {
                            aCurve = FileHandler.LoadDat(files[i], ComboFiles[j],DesiredSection);
                            OriginalFilename = files[i] + "|" + ComboFiles[j];
                            j = ComboFiles.Length;

                        }
                    }
                }
                else
                    aCurve = FileHandler.LoadIVS(files[i],DesiredSection );

                if (aCurve != null)
                {
                    aCurve = Regression.FlattenBackGround(new Graphing(zedGraphControl1), ref aCurve, 1);

                    Application.DoEvents();
                    string FileOut =
                        System.IO.Path.GetFileNameWithoutExtension(files[i]);
                    FileHandler.SaveCSBin(path + "\\" + FileOut + ".CSb", OriginalFilename, aCurve.X, aCurve.Y, aCurve.Curve);
                }
            }

        }
        
        private void LoadDat(string FileName, CurveSection DesiredSection, CurveSection FlattenSection)
        {
            string path = System.IO.Path.GetDirectoryName(FileName);
            string[] files = System.IO.Directory.GetFiles(path);
            LoadDat(files, DesiredSection , FlattenSection  );
        }

        private void LoadFC(string FileName)
        {
            
            string path = System.IO.Path.GetDirectoryName(FileName);
            DataPath = path;
            string[] files = System.IO.Directory.GetFiles(path);
            string[] ComboFiles = { "" };
            for (int i = 0; i < files.Length; i++)
            {
                if (System.IO.Path.GetExtension(files[i]).ToLower() == ".lsf")
                {
                    DialogResult dr = MessageBox.Show("Array already exists.  Use Pre existing?", "", MessageBoxButtons.YesNo);// mb = new MessageBox();
                    if (dr == DialogResult.Yes)
                    {
                        DoLoadSorting(FileHandler.LoadLSF(files[i], out AverageLSF, out MaxLSF, ref CurveNumbers, ref CurveNames, progressBar1));
                        return;
                    }
                }
               
            }

            files = System.IO.Directory.GetFiles(path, "*.fc");

            for (int i = 0; i < files.Length; i++)
            {
                AFMCurve aCurve = null;
                
                for (int j = 0; j < ComboFiles.Length; j++)
                {
                        aCurve = Loaders.MatLabLoader.LoadMatLabFC(files[i]);
                        j = ComboFiles.Length;
                }
                
                if (aCurve != null)
                {
                    aCurve = Regression.FlattenBackGround(new Graphing(zedGraphControl1), ref aCurve, 1);

                    Application.DoEvents();
                    string FileOut =
                        System.IO.Path.GetFileNameWithoutExtension(files[i]);
                    FileHandler.SaveCSBin(path + "\\" + FileOut + ".CSb", FileName, aCurve.X, aCurve.Y, aCurve.Curve);
                }
            }


        }

        private void LoadTxt(string FileName)
        {

            string path = System.IO.Path.GetDirectoryName(FileName);
            DataPath = path;
            string[] files = System.IO.Directory.GetFiles(path);
           
            for (int i = 0; i < files.Length; i++)
            {
                if (System.IO.Path.GetExtension(files[i]).ToLower() == ".lsf")
                {
                    DialogResult dr = MessageBox.Show("Array already exists.  Use Pre existing?", "", MessageBoxButtons.YesNo);// mb = new MessageBox();
                    if (dr == DialogResult.Yes)
                    {
                        DoLoadSorting(FileHandler.LoadLSF(files[i], out AverageLSF, out MaxLSF, ref CurveNumbers, ref CurveNames, progressBar1));
                        return;
                    }
                }

            }

            files = System.IO.Directory.GetFiles(path, "*.txt");

            for (int i = 0; i < files.Length; i++)
            {
                AFMCurve aCurve = null;

                aCurve = Loaders.TextLoader.LoadTXT(files[i]);

                if (aCurve != null)
                {
                    aCurve = Regression.FlattenBackGround(new Graphing(zedGraphControl1), ref aCurve, 1);

                    Application.DoEvents();
                    string FileOut =
                        System.IO.Path.GetFileNameWithoutExtension(files[i]);
                    FileHandler.SaveCSBin(path + "\\" + FileOut + ".CSb", FileName, aCurve.X, aCurve.Y, aCurve.Curve);
                }
            }


        }

        private void LoadgTxt(string FileName)
        {

            string path = System.IO.Path.GetDirectoryName(FileName);
            DataPath = path;
            string[] files = System.IO.Directory.GetFiles(path);

            for (int i = 0; i < files.Length; i++)
            {
                if (System.IO.Path.GetExtension(files[i]).ToLower() == ".lsf")
                {
                    DialogResult dr = MessageBox.Show("Array already exists.  Use Pre existing?", "", MessageBoxButtons.YesNo);// mb = new MessageBox();
                    if (dr == DialogResult.Yes)
                    {
                        DoLoadSorting(FileHandler.LoadLSF(files[i], out AverageLSF, out MaxLSF, ref CurveNumbers, ref CurveNames, progressBar1));
                        return;
                    }
                }

            }

            files = System.IO.Directory.GetFiles(path, "*.gxt");

            for (int i = 0; i < files.Length; i++)
            {
                AFMCurve aCurve = null;

                aCurve = Loaders.gTextLoader.LoadTXT(files[i]);

                if (aCurve != null)
                {
                    aCurve = Regression.FlattenBackGround(new Graphing(zedGraphControl1), ref aCurve, 1);

                    Application.DoEvents();
                    string FileOut =
                        System.IO.Path.GetFileNameWithoutExtension(files[i]);
                    FileHandler.SaveCSBin(path + "\\" + FileOut + ".CSb", FileName, aCurve.X, aCurve.Y, aCurve.Curve);
                }
            }


        }

        private void LoadDI(string FileName)
        {
          
        }

        private void LoadCSB(string FileName,bool CheckForArray)
        {
            Correlation[,]  LSFs;
            //openFileDialog1.ShowDialog();
             
            string path=  System.IO.Path.GetDirectoryName(FileName);
            DataPath = path;
            string [] files= System.IO.Directory.GetFiles(path);
            for (int i = 0; i < files.Length; i++)
            {
                if (System.IO.Path.GetExtension(files[i]).ToLower() == ".lsf" && CheckForArray )
                {
                    DialogResult dr= MessageBox.Show("Array already exists.  Use Pre existing?","",MessageBoxButtons.YesNo );// mb = new MessageBox();
                    if (dr == DialogResult.Yes)
                    {
                        DoLoadSorting(FileHandler.LoadLSF(files[i], out AverageLSF, out MaxLSF, ref CurveNumbers, ref CurveNames, progressBar1));
                        return;
                    }
                }
            }
            files = System.IO.Directory.GetFiles(path,"*.csb");
            LSFs = new Correlation[files.Length, files.Length];
            Random rnd = new Random();
            long square =(long)( files.Length * files.Length*1.1);
            long cc = 0;
            for (int i = 0; i < files.Length; i++)
            {
                AFMCurve C1 =FileHandler.LoadCSBin(files[i]);
                PlotAFMCurve(C1);
                for (int j = 0; j < files.Length; j++ )
                {
                    if (i == j)
                        LSFs[i, j] = new Correlation();
                    else if (LSFs[i, j] == null)
                    {
                        AFMCurve C2= FileHandler. LoadCSBin(files[j]);
                        Correlation cor =  CompareCurves(C1, C2);
                       
                        LSFs[i, j] = cor;
                        //LSFs[j, i] = cor;
                        Application.DoEvents();
                    }
                    cc++;
                    progressBar1.Value = (int)((95 * (double)i / (double)files.Length) + (5 * (double)j / (double)files.Length));
                }
            }

            FileHandler.WriteLSFArray(ref LSFs , path, files );

            string[] ff = Directory.GetFiles(path, "*.lsf");
            DoLoadSorting(FileHandler.LoadLSF(ff[0], out AverageLSF, out MaxLSF, ref CurveNumbers, ref CurveNames, progressBar1));
            progressBar1.Value = 100;
        }

        public void SaveBin(TextWriter tw, GroupBins GroupBins, string GroupTag)
        {
            for (int GroupBinNumber = 0; GroupBinNumber < GroupBins.Bins.Count; GroupBinNumber++)
            {
                GroupBin GB = GroupBins.Bins[GroupBinNumber];
                List<IndividualCurveStats> BinCurves = GB.GroupsCurves; //GroupBins[GroupBinNumber][ListNumber];
                for (int i = 0; i < BinCurves.Count; i++)
                {
                    int PrimaryIndex = BinCurves[i].PrimaryIndex;
                    try
                    {
                        tw.WriteLine(GetFilenameFromCurve(BinCurves[i]) + "\t" + GroupTag + (GB.BinNumber).ToString());
                    }
                    catch { }

                }
            }
            tw.WriteLine(" ");
            tw.WriteLine(" ");
            tw.WriteLine(" ");
            tw.WriteLine(" ");
            for (int GroupBinNumber = 0; GroupBinNumber < GroupBins.Bins.Count; GroupBinNumber++)
            {
                GroupBin GB = GroupBins.Bins[GroupBinNumber];
                List<IndividualCurveStats> BinCurves = GB.GroupsCurves; //GroupBins[GroupBinNumber][ListNumber];
                for (int i = 0; i < BinCurves.Count; i++)
                {
                    int PrimaryIndex = BinCurves[i].PrimaryIndex;
                    try
                    {
                        tw.WriteLine(CurveNames[PrimaryIndex] + "\t" + GroupTag + (GB.BinNumber).ToString());
                    }
                    catch { }

                }
            }
        }

        public void SaveBin(TextWriter tw, GroupBin SingleGroupBin, string GroupTag)
        {


            GroupBin GB = SingleGroupBin;
            List<IndividualCurveStats> BinCurves = GB.GroupsCurves; //GroupBins[GroupBinNumber][ListNumber];
            for (int i = 0; i < BinCurves.Count; i++)
            {
                int PrimaryIndex = BinCurves[i].PrimaryIndex;
                try
                {
                    tw.WriteLine(CurveNames[PrimaryIndex] + "\t" + GroupTag + (GB.BinNumber).ToString());
                }
                catch { }

            }
        }

        public void ConvertAFMtoCSB(string FileDirectory, ProgressBar PB, CurveSection DesiredSection, CurveSection FlattenSection, int PolynomialOrder, bool AutoRun)
        {
            string[] Files = Directory.GetFiles(FileDirectory, "*.dat");
            if (Files.Length > 0)
            {
                LoadDat(Files[0], DesiredSection, FlattenSection);
            }
            else
            {
                Files = Directory.GetFiles(FileDirectory, "*.ivs");
                if (Files.Length > 0)
                {
                    LoadDat(Files[0], DesiredSection, FlattenSection);
                }
                else
                {
                    Files = Directory.GetFiles(FileDirectory, "*.0*");
                    if (Files.Length > 0)
                    {
                        LoadDI(Files[0]);
                    }
                    else
                    {
                        Files = Directory.GetFiles(FileDirectory, "*.fc");
                        if (Files.Length > 0)
                        {
                            LoadFC(Files[0]);
                        }
                        else
                        {
                            Files = Directory.GetFiles(FileDirectory, "*.txt");
                            if (Files.Length > 0)
                            {
                                LoadTxt(Files[0]);
                            }
                            else
                            {
                                Files = Directory.GetFiles(FileDirectory, "*.gxt");
                                if (Files.Length > 0)
                                {
                                    LoadgTxt(Files[0]);
                                }
                                else
                                {

                                }
                            }
                        }
                    }

                }
            }
            DataPath = FileDirectory;
            if (AutoRun == false)
            {
                DialogResult ret = MessageBox.Show("Finished Converting Files.  Do you wish to build the LSF Array now?", "Full Conversion", MessageBoxButtons.YesNo);
                if (ret == DialogResult.Yes)
                {
                    string[] ff = Directory.GetFiles(DataPath, "*.csb");
                    LoadCSB(ff[0], false);
                }
            }
            else
            {
                string[] ff = Directory.GetFiles(DataPath, "*.csb");
                LoadCSB(ff[0], false);
            }

        }

        public string ConvertAFMToCSB(string FileName)
        {

            double FileExnNumber;
            Double.TryParse(System.IO.Path.GetExtension(FileName).ToLower().Substring(2), out FileExnNumber);
            if (System.IO.Path.GetExtension(FileName).ToLower() == ".000" || FileExnNumber > 0)
            {
                LoadDI(FileName);
                DataPath = System.IO.Path.GetDirectoryName(FileName);
                FileName = DataPath + "\\" + System.IO.Path.GetFileNameWithoutExtension(FileName) + ".csb";
            }
            if (System.IO.Path.GetExtension(FileName).ToLower() == ".fc")
            {
                LoadFC(FileName);
                DataPath = System.IO.Path.GetDirectoryName(FileName);
                FileName = DataPath + "\\" + System.IO.Path.GetFileNameWithoutExtension(FileName) + ".csb";
            }
            if (System.IO.Path.GetExtension(FileName).ToLower() == ".dat" ||
                System.IO.Path.GetExtension(FileName).ToLower() == ".ivs")
            {
                LoadDat(FileName, CurveSection.Withdrawl, CurveSection.Approach);
                DataPath = System.IO.Path.GetDirectoryName(FileName);
                FileName = DataPath + "\\" + System.IO.Path.GetFileNameWithoutExtension(FileName) + ".csb";
                DialogResult ret = MessageBox.Show("Finished Converting Files.  Do you wish to build the LSF Array now?");
                if (ret == DialogResult.Yes)
                {
                    string[] ff = Directory.GetFiles(DataPath, "*.csb");
                    LoadCSB(ff[0],false );
                }
            }
            if (System.IO.Path.GetExtension(FileName).ToLower() == ".csb")
            {
                LoadCSB(FileName,true );
                return "";
            }
            if (System.IO.Path.GetExtension(FileName).ToLower() == ".lsf")
            {

                DataPath = System.IO.Path.GetDirectoryName(FileName);
                DoLoadSorting(FileHandler.LoadLSF(FileName, out AverageLSF, out MaxLSF, ref CurveNumbers, ref CurveNames, progressBar1));
                return "";
            }

            return FileName;
        }

        private void CopyFiles(string OriginalFilename, string CSBinFilename, string DestDirectory)
        {
            if (OriginalFilename.Contains('|') == true)
            {
                string[] Files = OriginalFilename.Split(new char[] { '|' });
                for (int i = 0; i < Files.Length; i++)
                {
                    File.Copy(Files[i], DestDirectory + "\\" + Path.GetFileName(Files[i]), true);
                }
            }
            else
            {
                File.Copy(OriginalFilename, DestDirectory + "\\" + Path.GetFileName(OriginalFilename), true);
            }
            //File.Copy(CSBinFilename , DestDirectory  + "\\" + Path.GetFileName(CSBinFilename  ), true);
        }

        #endregion

        #region FormCorrelation
        private double CrossEnergy(GroupBin g1, GroupBin g2, ref  double[,] LSFs)
        {
            double sum = 0;
            int count = 0;
            for (int i = 0; i < g1.GroupsCurves.Count; i++)
                for (int j = i + 1; j < g2.GroupsCurves.Count; j++)
                {
                    int k = g1.GroupsCurves[i].PrimaryIndex;
                    int l = g2.GroupsCurves[j].PrimaryIndex;
                    sum += LSFs[k, l];
                    count++;
                }
            return sum / count;

        }

        private double GetCompareTerm(CurveInfo c)
        {
            if (rSquared.Checked)
                return c.correlation.LeastSquaresFit;
            if (rdSquared.Checked)
                return c.correlation.dLeastSquaresFit;
            if (rAbsolute.Checked)
                return c.correlation.AbsoluteFit;
            if (rChiSquared.Checked)
                return c.correlation.ChiSquared;
            if (rRegression.Checked)
                return c.correlation.LinearRegression;
            if (rWeightedDiff.Checked)
                return c.correlation.WeightedLeastSquarsFit;
            if (rCrossCorrelation.Checked)
                return c.correlation.CrossCorrelation;
            return 0;
        }

        private Correlation CompareCurves(AFMCurve C1, AFMCurve C2)
        {
            Correlation cor = new Correlation();
            
            double dx;
            if (C1.MaxY > C2.MaxY)
            {
                dx  = C2.GetXFromY(C2.MaxY) -C1.GetXFromY(C2.MaxY );
            }
            else
            {
                dx = C2.GetXFromY(C1.MaxY)- C1.GetXFromY(C1.MaxY) ;
            }
            C2.OffsetCurve(-1*dx);
            cor = DoLSF(C1, C2);
           
            return cor;
        }
        
        private Correlation DoLSF(AFMCurve C1, AFMCurve C2)//,out AFMCurve InterpCurve)
        {
            double Sum = 0;
            double SumBigger = 0;
            double CountBigger = 0;
            double SumAbs = 0;
            double SumDeriv = 0;
            double sumChi = 0;
            double sumCorrelation = 0;
            double sumSqrt1 = 0;
            double sumSqrt2 = 0;
            double count = 0;
            double countChi = 0;
            double d = 0;
            //InterpCurve = new AFMCurve();
            List<PointF> IPoints = new List<PointF>();
            double ly=0; double lx=0;
            double CorrCount = 0;
            for (int i=0;i<C2.Curve.GetLength(1);i++)
            {
                
                    double x = C2.Curve[0, i];
                    double y = C1.GetYfromX(x);
                    if (y != -1)
                    {
                        d = (C2.Curve[1, i] - y);
                        SumAbs += Math.Abs(d);
                        Sum += d * d;
                        sumSqrt1 +=  y * C2.Curve[1, i] ;
                        CorrCount++;
                        //sumSqrt2 += C2.Curve[1, i] * C2.Curve[1, i];
                        sumCorrelation += y * C2.Curve[1, i];
                        if (y != 0)
                        {
                            sumChi += d * d / (Math.Abs( y)+1) ;
                            countChi++;
                        }
                        count++;
                        if ( y*y > .25)
                        {
                            double w;
                            w =( Math.Abs(y) + Math.Abs(C2.Curve[1, i]));
                            SumBigger += d*d * w ;
                            CountBigger += w;
                        }
                        IPoints.Add(new PointF((float)x,(float) d));
                        if (i > 0)
                        {
                            double derive1;
                            double derive2;
                            derive1 = (y - ly) / (x - lx);
                            derive2 = (C2.Curve[1, i] - C2.Curve[1, i - 1]) / (C2.Curve[0, i] - C2.Curve[0, i - 1]);
                            d = derive1 - derive2  ;
                            SumDeriv += d * d;
                        }
                    }

                    ly = y;
                    lx = x;
                    
                
            }
            Sum = Math.Sqrt (Sum) / (double)count ;
            SumAbs = SumAbs / (double)count;
            SumBigger = Math.Sqrt(SumBigger) / (double)CountBigger;
            SumDeriv = Math.Sqrt(SumDeriv) / (double)count;
            sumChi = sumChi / (double)countChi;
            try
            {
                sumCorrelation = CorrCount / Math.Sqrt(sumSqrt1);// *Math.Sqrt(sumSqrt2) / sumCorrelation;
            }
            catch { }
            Correlation c = new Correlation();
            c.LeastSquaresFit = Sum;
            c.dLeastSquaresFit = SumDeriv;
            c.AbsoluteFit = SumAbs;
            c.WeightedLeastSquarsFit = SumBigger;
            c.ChiSquared = sumChi;
            c.CrossCorrelation = sumCorrelation;
            double[] CurveX = new double[IPoints.Count];
            double[] CurveY = new double[IPoints.Count];
            for (int i = 0; i < IPoints.Count; i++)
            {
                CurveX[ i] = IPoints[i].X;
                CurveY[ i] = IPoints[i].Y;
            }
            c.LinearRegression  = Regression.DoRegresssion(null, CurveX, CurveY, 1);
            //zedGraphControl1.Copy(false );
            return c;
        }
        #endregion

        #region PlotData
        private delegate void TryPlotData(Graphing sender, double[,] Data, string Dataname, string XAxis, string YAxis, string Title);
        public void graph_TryPlotData(Graphing sender, double[,] Data, string Dataname, string XAxis, string YAxis, string Title)
        {
            if (zedGraphControl1.InvokeRequired)
            {

                zedGraphControl1.Invoke(new TryPlotData(this.graph_TryPlotData), new object[] { sender, Data, Dataname ,XAxis ,YAxis ,Title  });
            }
            else
            {
                Graphing.PlotData(zedGraphControl1, Data, Dataname,XAxis,YAxis,Title );
            }
        }
        public void PlotAFMCurve(AFMCurve curve)
        {
            AFMCurve ac = curve;

            double dx = ac.MinX;
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
            Color UseColor = Color.Black;
            List<Graphing.LineInfo> DisplayLines = new List<Graphing.LineInfo>();
            DisplayLines.Add(new Graphing.LineInfo(Line, UseColor, ""));
            Graphing.PlotData(zedGraphControl1, DisplayLines, "Extention(nm)", "Deflection(V)", "");

        }
        #endregion

        #region RunSorts
        public void ChainSort()
        {
            TextWriter tw;
            progressBar1.Value = 0;
            tw = new StreamWriter(Path.GetDirectoryName(Application.ExecutablePath) + "\\test.txt");
            GroupBins NewGroups = new GroupBins();
            int cc = 0;
            GroupBins SelBins = new GroupBins();
            ListBox.SelectedIndexCollection selCol = listMain.SelectedIndices;
            GroupBins NotSelBins = new GroupBins();
            for (int i = 0; i < listMain.Items.Count; i++)
            {
                if (!selCol.Contains(i))
                {
                    NotSelBins.Bins.Add((GroupBin)listMain.Items[i]);
                }
            }
            for (int i = 0; i < selCol.Count; i++)
            {
                GroupBin selGB = ((GroupBin)listMain.Items[selCol[i]]);
                SelBins.Bins.Add(selGB);
            }
            if (selCol.Count == 0)
            {
                SelBins = AllGroups;
                NotSelBins = null;
            }

            foreach (GroupBin gb in SelBins.Bins)
            {
                GroupBins Groups;
                if (gb.GroupsCurves.Count > 100 && gb.Locked == false)
                {
                    Groups = ChainAssign.GroupAssign(zedGraphControl1, progressBar1, Relations.Count, gb);
                }
                else
                {
                    Groups = new GroupBins();
                    Groups.Bins.Add(gb);
                }
                NewGroups.Bins.AddRange(Groups.Bins);
                progressBar1.Value = cc / AllGroups.Bins.Count;
                cc++;
            }
            if (NotSelBins != null)
                NewGroups.Bins.AddRange(NotSelBins.Bins);
            SaveBin(tw, NewGroups, "");
            tw.Close();
            AllGroups = NewGroups;
            ListBins();
            //LTrashed.Text = AllGroups.Bins.Count.ToString();
            progressBar1.Value = 100;
        }
        public void HistoSort()
        {
            TextWriter tw;
            progressBar1.Value = 0;
            tw = new StreamWriter(Path.GetDirectoryName(Application.ExecutablePath) + "\\testHistoSort.txt");
            GroupBins NewGroups = new GroupBins();
            int cc = 0;
            Graphing graph = new Graphing(zedGraphControl1);
            graph.TryPlotData += new PlotDataEvent(graph_TryPlotData);

            GroupBins SelBins = new GroupBins();
            ListBox.SelectedIndexCollection selCol = listMain.SelectedIndices;
            GroupBins NotSelBins = new GroupBins();
            for (int i = 0; i < listMain.Items.Count; i++)
            {
                if (!selCol.Contains(i))
                {
                    NotSelBins.Bins.Add((GroupBin)listMain.Items[i]);
                }
            }
            for (int i = 0; i < selCol.Count; i++)
            {
                GroupBin selGB = ((GroupBin)listMain.Items[selCol[i]]);
                SelBins.Bins.Add(selGB);
            }
            if (selCol.Count == 0)
            {
                SelBins = AllGroups;
                NotSelBins = null;
            }
            GroupBins Groups = GroupCurves.Sorts.HistogramSort.DoHistoSort(tw, graph, progressBar1,ref  CurveLookup, SelBins);
            NewGroups.Bins.AddRange(Groups.Bins);
            progressBar1.Value = cc / AllGroups.Bins.Count;
            cc++;
            if (NotSelBins != null)
                NewGroups.Bins.AddRange(NotSelBins.Bins);
            SaveBin(tw, NewGroups, "");
            tw.Close();
            AllGroups = NewGroups;
            ListBins();
            //LTrashed.Text = AllGroups.Bins.Count.ToString();
            progressBar1.Value = 100;
        }
        public void BifurificationSort()
        {
            progressBar1.Value = 0;
            TextWriter tw = new StreamWriter(Path.GetDirectoryName(Application.ExecutablePath) + "\\testPresort.txt");
            GroupBins NewBins = new GroupBins();
            GroupBins SelBins = new GroupBins();
            ListBox.SelectedIndexCollection selCol = listMain.SelectedIndices;
            GroupBins NotSelBins = new GroupBins();
            for (int i = 0; i < listMain.Items.Count; i++)
            {
                if (!selCol.Contains(i))
                {
                    NotSelBins.Bins.Add((GroupBin)listMain.Items[i]);
                }
            }
            for (int i = 0; i < selCol.Count; i++)
            {
                GroupBin selGB = ((GroupBin)listMain.Items[selCol[i]]);
                SelBins.Bins.Add(selGB);
            }
            if (selCol.Count == 0)
            {
                SelBins = AllGroups;
                NotSelBins = null;
            }

            foreach (GroupBin gb in SelBins.Bins)
            {
                if (gb.Locked == false)
                {
                    GroupBins groups = Bifurification.Bifuricate(ref CurveLookup, progressBar1, gb);
                    NewBins.Bins.AddRange(groups.Bins);
                }
                else
                    NewBins.Bins.Add(gb);
            }
            if (NotSelBins != null)
                NewBins.Bins.AddRange(NotSelBins.Bins);
            SaveBin(tw, NewBins, "");
            tw.Close();
            AllGroups = NewBins;
            ListBins();
            //LTrashed.Text = AllGroups.Bins.Count.ToString();
            progressBar1.Value = 100;
        }
        public void ShakeSort()
        {
            progressBar1.Value = 0;
            TextWriter tw = new StreamWriter(Path.GetDirectoryName(Application.ExecutablePath) + "\\testShakiest.txt");
            GroupBins newGroups = new GroupBins();
            GroupBins SelBins = new GroupBins();
            ListBox.SelectedIndexCollection selCol = listMain.SelectedIndices;
            GroupBins NotSelBins = new GroupBins();
            for (int i = 0; i < listMain.Items.Count; i++)
            {
                if (!selCol.Contains(i))
                {
                    NotSelBins.Bins.Add((GroupBin)listMain.Items[i]);
                }
            }
            for (int i = 0; i < selCol.Count; i++)
            {
                GroupBin selGB = ((GroupBin)listMain.Items[selCol[i]]);
                SelBins.Bins.Add(selGB);
            }
            if (selCol.Count == 0)
            {
                SelBins = AllGroups;
                NotSelBins = null;
            }

            foreach (GroupBin gb in SelBins.Bins)
            {
                if (gb.Locked == false)
                {
                    GroupBins Groups = GroupCurves.Sorts.ShakeSorter.StaggerShake(tw, zedGraphControl1, progressBar1, ref CurveLookup, gb, 4, 1);
                    newGroups.Bins.AddRange(Groups.Bins);
                }
                else
                    newGroups.Bins.Add(gb);
            }
            if (NotSelBins != null)
                newGroups.Bins.AddRange(NotSelBins.Bins);

            SaveBin(tw, newGroups, "");
            AllGroups = newGroups;
            ListBins();
            tw.Close();
            progressBar1.Value = 100;
            //LTrashed.Text = AllGroups.Bins.Count.ToString();
        }
        #endregion

        #region RunRelaxations
        public void Shake()
        {
            progressBar1.Value = 0;
            TextWriter tw = new StreamWriter(Path.GetDirectoryName(Application.ExecutablePath) + "\\testShakier.txt");
            GroupBins newBins = new GroupBins();
            foreach (GroupBin gb in AllGroups.Bins)
            {
                if (gb.Locked == false)
                {
                    newBins.Bins.Add(gb);
                }
            }

            SimpleShaker.ShakeAssign(ref CurveLookup, progressBar1, zedGraphControl1,  100,  newBins, false);

            foreach (GroupBin gb in AllGroups.Bins)
            {
                if (gb.Locked == true)
                {
                    newBins.Bins.Add(gb);
                }
            }
            AllGroups = newBins;
            progressBar1.Value = 50;
            SaveBin(tw, AllGroups, "");
            tw.Close();
            //LTrashed.Text = AllGroups.Bins.Count.ToString();
            ListBins();
            progressBar1.Value = 100;
        }
        public void MontoCarlo()
        {
            progressBar1.Value = 0;
            TextWriter tw = new StreamWriter(Path.GetDirectoryName(Application.ExecutablePath) + "\\testShakier.txt");
            GroupBins newBins = new GroupBins();
            foreach (GroupBin gb in AllGroups.Bins)
            {
                if (gb.Locked == false)
                {
                    newBins.Bins.Add(gb);
                }
            }
            MonteCarloShaker.MonteAssign(ref CurveLookup, 1.1 * AverageLSF, progressBar1, zedGraphControl1, newBins.Bins.Count, 100, null, newBins, false,false );
            foreach (GroupBin gb in AllGroups.Bins)
            {
                if (gb.Locked == true)
                {
                    newBins.Bins.Add(gb);
                }
            }
            AllGroups = newBins;
            progressBar1.Value = 50;
            SaveBin(tw, AllGroups, "");
            tw.Close();
            //LTrashed.Text = AllGroups.Bins.Count.ToString();
            ListBins();
            progressBar1.Value = 100;
        }
        #endregion

        public void ReturnAllTo1Group()
        {
            GroupBin BigBin = new GroupBin(1);
            BigBin.GroupsCurves = Relations;
            AllGroups.Bins.Clear();
            AllGroups.Bins.Add(BigBin);
            WholeInteractionEnergy = BigBin.GroupSelfEnergy(ref CurveLookup);
            ListBins();
        }

        #region Events
        
        private void ChainSort_Click(object sender, EventArgs e)
        {
            ChainSort();
        }

        /*private void HistoSort(object sender, EventArgs e)
        {
            GroupVision gv = new GroupVision();
            gv.Show();
        }*/

      
        private void BifurificationButton_click(object sender, EventArgs e)
        {
            BifurificationSort();
        }

        
        private void ShakeSort_Click(object sender, EventArgs e)
        {
            ShakeSort();   
        }

        private void ShakeBin_Click(object sender, EventArgs e)
        {
            Shake();

        }

        private void MontoCarlo_Click(object sender, EventArgs e)
        {
            MontoCarlo();
        }

        private void bResetGroup_Click(object sender, EventArgs e)
        {
            ReturnAllTo1Group();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listMain.SelectedItem == null) return;
            label4.Text = ((GroupBin)listMain.SelectedItem).GroupsCurves.Count.ToString();
            Application.DoEvents();
            ListBox.SelectedIndexCollection selCol = listMain.SelectedIndices;
            List<Graphing.LineInfo> DisplayLines = new List<Graphing.LineInfo>();
            Random rnd = new Random();
            AFMCurve C1 = new AFMCurve();
            Color GroupColor = Color.White;
            for (int i = 0; i < selCol.Count; i++)
            {
                GroupBin gb = (GroupBin)listMain.Items[selCol[i]];
                double MaxCurve = 0;
                int StepJ = 1;
                if (gb.GroupsCurves.Count > 100)
                {
                    StepJ = (int)((double)gb.GroupsCurves.Count / 300);
                }
                if (StepJ < 1) StepJ = 1;
                for (int j = 0; j < gb.GroupsCurves.Count; j += StepJ)
                {
                    string Filename = CurveNames[gb.GroupsCurves[j].PrimaryIndex];
                    AFMCurve ac = FileHandler.LoadCSBin(DataPath + "\\" + Filename);
                    if (ac.MaxY > MaxCurve)
                    {
                        MaxCurve = ac.MaxY;
                        C1 = ac;
                    }

                }

                if (selCol.Count > 1) GroupColor = Color.FromArgb((int)(255 * rnd.NextDouble()), (int)(255 * rnd.NextDouble()), (int)(255 * rnd.NextDouble()));
                //GroupColor = Color.Black;
                for (int j = 0; j < gb.GroupsCurves.Count; j += StepJ)
                {
                    string Filename = CurveNames[gb.GroupsCurves[j].PrimaryIndex];
                    AFMCurve ac = FileHandler.LoadCSBin(DataPath + "\\" + Filename);

                    double dx;
                    if (C1.MaxY > ac.MaxY)
                    {
                        dx = ac.GetXFromY(ac.MaxY) - C1.GetXFromY(ac.MaxY);
                    }
                    else
                    {
                        dx = ac.GetXFromY(C1.MaxY) - C1.GetXFromY(C1.MaxY);
                    }
                    ac.OffsetCurve(-1 * dx);
                    int nPoints = 65;
                    double[,] Line = new double[2, nPoints];
                    int cc = 0;
                    int stepK = (int)Math.Floor((double)(ac.Curve.GetLength(1) / 25));
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
                        try
                        {
                            Line[0, k] = Line[0, k - 3];
                            Line[1, k] = Line[1, k - 3];
                        }
                        catch { }
                    }
                    Color UseColor;
                    if (GroupColor == Color.White)
                        UseColor = Color.FromArgb((int)(255 * rnd.NextDouble()), (int)(255 * rnd.NextDouble()), (int)(255 * rnd.NextDouble()));
                    else
                        UseColor = GroupColor;
                    DisplayLines.Add(new Graphing.LineInfo(Line, UseColor, ""));
                }
            }
            Graphing.PlotData(zedGraphControl1, DisplayLines, "Extention(nm)", "Deflection(V)", "");
        }


        private void bSaveBins_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            DialogResult ret = saveFileDialog1.ShowDialog();
            if (ret == DialogResult.OK)
            {
                TextWriter tw = new StreamWriter(saveFileDialog1.FileName);

                progressBar1.Value = 50;
                SaveBin(tw, AllGroups, "");
                //tw.WriteLine("Locked");
                SaveBin(tw, LockedGroups, "L");
                //tw.WriteLine("Trash");
                SaveBin(tw, TrashedGroups, "T");
                SaveBin(tw, ManualGroups, "M");
                tw.Close();
                //LTrashed.Text = AllGroups.Bins.Count.ToString();
                ListBins();
                progressBar1.Value = 100;
            }
        }

        private void bLock_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            ListBox.SelectedIndexCollection selCol = listMain.SelectedIndices;

            for (int i = 0; i < selCol.Count; i++)
            {
                GroupBin selGB = ((GroupBin)listMain.Items[selCol[i]]);
                selGB.Locked = true;

                LockedGroups.Bins.Add(selGB);
                AllGroups.Bins.Remove(selGB);
            }


            ListBins();
            //LTrashed.Text = AllGroups.Bins.Count.ToString();
            progressBar1.Value = 100;


        }

        private void FixateCurves_Click(object sender, EventArgs e)
        {
            ListBox.SelectedIndexCollection selCol = listMain.SelectedIndices;

            for (int i = 0; i < selCol.Count; i++)
            {
                GroupBin gb = ((GroupBin)listMain.Items[selCol[i]]);
                foreach (IndividualCurveStats ics in gb.GroupsCurves)
                    ics.Fixed = true;
            }
        }


        private void HistoSort_Click(object sender, EventArgs e)
        {
            HistoSort();
        }
        
        private void bTrash_Click(object sender, EventArgs e)
        {
            ListBox.SelectedIndexCollection selCol = listMain.SelectedIndices;
            for (int i = 0; i < selCol.Count; i++)
            {
                GroupBin selGB = ((GroupBin)listMain.Items[selCol[i]]);
                TrashedGroups.Bins.Add(selGB);
                AllGroups.Bins.Remove(selGB);
            }


            ListBins();
            //LTrashed.Text = AllGroups.Bins.Count.ToString();
            progressBar1.Value = 100;
        }

        private void MergeGroups_Click(object sender, EventArgs e)
        {
            TextWriter tw;
            progressBar1.Value = 0;
            tw = new StreamWriter(Path.GetDirectoryName(Application.ExecutablePath) +  "\\testHistoSort.txt");

            List<GroupBin> SelectedBins = new List<GroupBin>();
            for (int i = 0; i < listMain.SelectedItems.Count; i++)
            {
                GroupBin gb = (GroupBin)listMain.SelectedItems[i];
                SelectedBins.Add(gb);
            }

            AllGroups.MergeBins(SelectedBins);


            SaveBin(tw, AllGroups, "");
            tw.Close();

            ListBins();
            //LTrashed.Text = AllGroups.Bins.Count.ToString();
            progressBar1.Value = 100;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listTrash.SelectedItem == null) return;
            ListBox.SelectedIndexCollection selCol = listTrash.SelectedIndices;
            List<Graphing.LineInfo> DisplayLines = new List<Graphing.LineInfo>();
            Random rnd = new Random();
            AFMCurve C1 = new AFMCurve();
            Color GroupColor = Color.White;
            for (int i = 0; i < selCol.Count; i++)
            {
                GroupBin gb = (GroupBin)listTrash.Items[selCol[i]];
                double MaxCurve = 0;
                int StepJ = 1;
                if (gb.GroupsCurves.Count > 100)
                {
                    StepJ = (int)((double)gb.GroupsCurves.Count / 100);
                }
                for (int j = 0; j < gb.GroupsCurves.Count; j += StepJ)
                {
                    string Filename = CurveNames[gb.GroupsCurves[j].PrimaryIndex];
                    AFMCurve ac = FileHandler.LoadCSBin(DataPath + "\\" + Filename);
                    if (ac.MaxY > MaxCurve)
                    {
                        MaxCurve = ac.MaxY;
                        C1 = ac;
                    }

                }

                if (selCol.Count > 1) GroupColor = Color.FromArgb((int)(255 * rnd.NextDouble()), (int)(255 * rnd.NextDouble()), (int)(255 * rnd.NextDouble()));

                for (int j = 0; j < gb.GroupsCurves.Count; j += StepJ)
                {
                    string Filename = CurveNames[gb.GroupsCurves[j].PrimaryIndex];
                    AFMCurve ac = FileHandler.LoadCSBin(DataPath + "\\" + Filename);

                    double dx;
                    if (C1.MaxY > ac.MaxY)
                    {
                        dx = ac.GetXFromY(ac.MaxY) - C1.GetXFromY(ac.MaxY);
                    }
                    else
                    {
                        dx = ac.GetXFromY(C1.MaxY) - C1.GetXFromY(C1.MaxY);
                    }
                    ac.OffsetCurve(-1 * dx);
                    int nPoints = 35;
                    double[,] Line = new double[2, nPoints];
                    int cc = 0;
                    int stepK = (int)Math.Floor((double)(ac.Curve.GetLength(1) / 25));
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
                    Color UseColor;
                    if (GroupColor == Color.White)
                        UseColor = Color.FromArgb((int)(255 * rnd.NextDouble()), (int)(255 * rnd.NextDouble()), (int)(255 * rnd.NextDouble()));
                    else
                        UseColor = GroupColor;
                    DisplayLines.Add(new Graphing.LineInfo(Line, UseColor, ""));
                }
            }
            Graphing.PlotData(zedGraphControl1, DisplayLines, "Extension (nm)", "Deflection (V)", "");

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listLocked.SelectedItem == null) return;
            ListBox.SelectedIndexCollection selCol = listLocked.SelectedIndices;
            List<Graphing.LineInfo> DisplayLines = new List<Graphing.LineInfo>();
            Random rnd = new Random();
            AFMCurve C1 = new AFMCurve();
            Color GroupColor = Color.White;
            for (int i = 0; i < selCol.Count; i++)
            {
                GroupBin gb = (GroupBin)listLocked.Items[selCol[i]];
                double MaxCurve = 0;
                int StepJ = 1;
                if (gb.GroupsCurves.Count > 100)
                {
                    StepJ = (int)((double)gb.GroupsCurves.Count / 100);
                }
                for (int j = 0; j < gb.GroupsCurves.Count; j += StepJ)
                {
                    string Filename = CurveNames[gb.GroupsCurves[j].PrimaryIndex];
                    AFMCurve ac = FileHandler.LoadCSBin(DataPath + "\\" + Filename);
                    if (ac.MaxY > MaxCurve)
                    {
                        MaxCurve = ac.MaxY;
                        C1 = ac;
                    }

                }

                if (selCol.Count > 1) GroupColor = Color.FromArgb((int)(255 * rnd.NextDouble()), (int)(255 * rnd.NextDouble()), (int)(255 * rnd.NextDouble()));

                for (int j = 0; j < gb.GroupsCurves.Count; j += StepJ)
                {
                    string Filename = CurveNames[gb.GroupsCurves[j].PrimaryIndex];
                    AFMCurve ac = FileHandler.LoadCSBin(DataPath + "\\" + Filename);

                    double dx;
                    if (C1.MaxY > ac.MaxY)
                    {
                        dx = ac.GetXFromY(ac.MaxY) - C1.GetXFromY(ac.MaxY);
                    }
                    else
                    {
                        dx = ac.GetXFromY(C1.MaxY) - C1.GetXFromY(C1.MaxY);
                    }
                    ac.OffsetCurve(-1 * dx);
                    int nPoints = 35;
                    double[,] Line = new double[2, nPoints];
                    int cc = 0;
                    int stepK = (int)Math.Floor((double)(ac.Curve.GetLength(1) / 25));
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
                    Color UseColor;
                    if (GroupColor == Color.White)
                        UseColor = Color.FromArgb((int)(255 * rnd.NextDouble()), (int)(255 * rnd.NextDouble()), (int)(255 * rnd.NextDouble()));
                    else
                        UseColor = GroupColor;
                    DisplayLines.Add(new Graphing.LineInfo(Line, UseColor, ""));
                }
            }
            Graphing.PlotData(zedGraphControl1, DisplayLines, "Extension(nm)", "Deflection (V)", "");

        }

        private void bAverageGroup_Click(object sender, EventArgs e)
        {


            ListBox.SelectedIndexCollection selCol = listMain.SelectedIndices;
            GroupBins SelGroups = new GroupBins();
            for (int i = 0; i < selCol.Count; i++)
            {
                GroupBin selGB = ((GroupBin)listMain.Items[selCol[i]]);
                SelGroups.Bins.Add(selGB);
                //GroupVision gv = new GroupVision(selGB);
                //gv.Show();
            }
            if (SelGroups.Bins.Count == 0)
                SelGroups = AllGroups;
            GroupVision gv = new GroupVision(SelGroups);
            gv.Show();
            //if (selCol.Count == 0) MessageBox.Show("Please select a group from the group listbox"); 
        }

        private void bDoYoungs_Click(object sender, EventArgs e)
        {
            ListBox.SelectedIndexCollection selCol = listMain.SelectedIndices;
            GroupBins SelGroups = new GroupBins();
            for (int i = 0; i < selCol.Count; i++)
            {
                GroupBin selGB = ((GroupBin)listMain.Items[selCol[i]]);
                SelGroups.Bins.Add(selGB);
                //GroupVision gv = new GroupVision(selGB);
                //gv.Show();
            }
            if (SelGroups.Bins.Count == 0)
                SelGroups = AllGroups;
            Youngs gv = new Youngs(SelGroups);
            gv.Show();
        }

        private void bReturnToMain_Click(object sender, EventArgs e)
        {
            ListBox.SelectedIndexCollection selCol = listLocked.SelectedIndices;
            for (int i = 0; i < selCol.Count; i++)
            {
                GroupBin selGB = ((GroupBin)listLocked.Items[selCol[i]]);
                AllGroups.Bins.Add(selGB);
                LockedGroups.Bins.Remove(selGB);
            }


            ListBins();
            // LTrashed.Text = AllGroups.Bins.Count.ToString();
            progressBar1.Value = 100;
        }

        private void importAFMFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*openFileDialog1.FileName = "";
            openFileDialog1.Filter = "*.ivs|*.ivs|DI Files|*.0*";
            openFileDialog1.ShowDialog();
            
            string FileName = openFileDialog1.FileName;
            if (FileName == "") return;
            
            ConvertAFMToCSB(FileName);*/
            ImportCurves ic = new ImportCurves(this);
            if (ic.IsDisposed != true) ic.ShowDialog();
        }
        
        private void createLSFCubeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "*.csb|*.csb";
            openFileDialog1.ShowDialog();

            string FileName = openFileDialog1.FileName;
            if (FileName == "") return;

            FileName= ConvertAFMToCSB(FileName);
            if (FileName == "") return;

            DoLoadSorting(FileHandler.LoadVB(FileName, ref CurveNumbers, ref CurveNames, progressBar1));

            this.BringToFront();
            this.Show();
            progressBar1.Value = 100;
        }

        private void openLSFCubeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "*.lsf|*.lsf";
            openFileDialog1.ShowDialog();

            string FileName = openFileDialog1.FileName;
            if (FileName == "") return;

            FileName = ConvertAFMToCSB(FileName);
            if (FileName == "") return;

            DoLoadSorting(FileHandler.LoadVB(FileName, ref CurveNumbers, ref CurveNames, progressBar1));

            this.BringToFront();
            this.Show();
            progressBar1.Value = 100;
        }

        private void copyGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zedGraphControl1.Copy(false);
        }

        private void bManualSort_Click(object sender, EventArgs e)
        {
            TextWriter tw;
            progressBar1.Value = 0;
            tw = new StreamWriter(Path.GetDirectoryName(Application.ExecutablePath) +  "\\testHistoSort.txt");

            List<GroupBin> SelectedBins = new List<GroupBin>();
            for (int i = 0; i < listMain.SelectedItems.Count; i++)
            {
                GroupBin gb = (GroupBin)listMain.SelectedItems[i];
                SelectedBins.Add(gb);
            }

            if ( SelectedBins.Count == 0 )
            {
                for (int i = 0; i < listMain.Items.Count; i++)
                {
                    GroupBin gb = (GroupBin)listMain.Items[i];
                    SelectedBins.Add(gb);
                }
            }

            GroupBin Merged= AllGroups.MergeBins(SelectedBins);
            AllGroups.Bins.Remove(Merged);

            ManualSort SortForm = new ManualSort();
            SortForm.LoadCurves(this, Merged, AllGroups, TrashedGroups);
            SortForm.ShowDialog(this);

            if (SortForm.OutBins != null)
            {
                for (int i = 0; i < SortForm.OutBins.Count ; i++)
                {
                    if (SortForm.OutBins[i].GroupsCurves.Count != 0)
                    {
                        lManual.Items.Add(SortForm.OutBins[i].BinNumber);
                        ManualGroups.Bins.Add(SortForm.OutBins[i]);
                    }
                }
            }

            SaveBin(tw, AllGroups, "");
            tw.Close();

            ListBins();
            //LTrashed.Text = AllGroups.Bins.Count.ToString();
            progressBar1.Value = 100;

        }
        
        
        private void arrangeFilesWithGroupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
             for (int i=0;i< AllGroups.Bins.Count ;i++)
             {
                 string OutPath = DataPath + "\\Group_" + AllGroups.Bins[i].BinNumber;
                 Directory.CreateDirectory(OutPath );
                 
                 GroupBin CBin=AllGroups.Bins[i];
                 for (int j = 0; j < CBin.GroupsCurves.Count; j++)
                 {
                     string CurveFilename=  GetFilenameFromCurve(CBin.GroupsCurves[j]);
                     AFMCurve C2= FileHandler.LoadCSBin(DataPath + "\\" + CurveFilename  );
                     CopyFiles(C2.OriginalFilename, CurveFilename, OutPath);
                 }
                  
             }

             string TrashPath = DataPath + "\\Trash" ;
             Directory.CreateDirectory(TrashPath);
             for (int i = 0; i < TrashedGroups.Bins.Count; i++)
             {
                 string OutPath = TrashPath + "\\Group_" + TrashedGroups.Bins[i].BinNumber;
                 Directory.CreateDirectory(OutPath);

                 GroupBin CBin = TrashedGroups.Bins[i];
                 for (int j = 0; j < CBin.GroupsCurves.Count; j++)
                 {
                     string CurveFilename = GetFilenameFromCurve(CBin.GroupsCurves[j]);
                     AFMCurve C2 = FileHandler.LoadCSBin(CurveFilename);
                     CopyFiles(C2.OriginalFilename, CurveFilename, OutPath);
                    // File.Copy(C2.OriginalFilename, OutPath + "\\" + Path.GetFileName(C2.OriginalFilename), true);
                    // File.Copy(CurveFilename, OutPath + "\\" + Path.GetFileName(CurveFilename), true);
                 }

             }

             string LockedPath = DataPath + "\\Locked";
             Directory.CreateDirectory(LockedPath);
             for (int i = 0; i < LockedGroups.Bins.Count; i++)
             {
                 string OutPath = LockedPath + "\\Group_" + LockedGroups.Bins[i].BinNumber;
                 Directory.CreateDirectory(OutPath);

                 GroupBin CBin = LockedGroups.Bins[i];
                 for (int j = 0; j < CBin.GroupsCurves.Count; j++)
                 {
                     string CurveFilename = GetFilenameFromCurve(CBin.GroupsCurves[j]);
                     AFMCurve C2 = FileHandler.LoadCSBin(CurveFilename);
                     CopyFiles(C2.OriginalFilename, CurveFilename, OutPath);
                     //File.Copy(C2.OriginalFilename, OutPath + "\\" + Path.GetFileName(C2.OriginalFilename), true);
                     //File.Copy(CurveFilename, OutPath + "\\" + Path.GetFileName(CurveFilename), true);
                 }

             }

             string ManualPath = DataPath + "\\Manual";
             Directory.CreateDirectory(ManualPath);
             for (int i = 0; i < ManualGroups.Bins.Count; i++)
             {
                 string OutPath = ManualPath + "\\Group_" + LockedGroups.Bins[i].BinNumber;
                 Directory.CreateDirectory(OutPath);

                 GroupBin CBin = ManualGroups.Bins[i];
                 for (int j = 0; j < CBin.GroupsCurves.Count; j++)
                 {
                     string CurveFilename = GetFilenameFromCurve(CBin.GroupsCurves[j]);
                     AFMCurve C2 = FileHandler.LoadCSBin(CurveFilename);
                     CopyFiles(C2.OriginalFilename, CurveFilename, OutPath);
                     //File.Copy(C2.OriginalFilename, OutPath + "\\" + Path.GetFileName(C2.OriginalFilename), true);
                     //File.Copy(CurveFilename, OutPath + "\\" + Path.GetFileName(CurveFilename), true);
                 }

             }
        }
        public void CheckSquared()
        {
            rSquared.Checked = true;
        }

        public void CheckChiSquared()
        {
           rChiSquared .Checked = true;
        }

        public void CheckdSquared()
        {
            rdSquared.Checked = true;
        }

        public void CheckRegression()
        {
            rRegression.Checked = true;
        }

        public void CheckAbsolute()
        {
            rAbsolute.Checked = true;
        }

        public void CheckWeightedDiff()
        {
            rWeightedDiff.Checked = true;
        }

        public void CheckCrossCorrelation()
        {
            rCrossCorrelation.Checked = true;
        }


        public void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            EnergyType = "Square Difference";
            DoLoadSorting(OriginalCurves);
        }

        public void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            EnergyType = "DSquare";
            DoLoadSorting(OriginalCurves);
        }

        public void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            EnergyType = "Absolute";
            DoLoadSorting(OriginalCurves);
        }

        public void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            EnergyType = "Chi";
            DoLoadSorting(OriginalCurves);
        }

        public void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            EnergyType = "Regression";
            DoLoadSorting(OriginalCurves);
        }

        public void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            
            DoLoadSorting(OriginalCurves);
        }
        
        private void exportStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListBox.SelectedIndexCollection selCol = listMain.SelectedIndices;
            GroupBins SelGroups = new GroupBins();
            for (int i = 0; i < selCol.Count; i++)
            {
                GroupBin selGB = ((GroupBin)listMain.Items[selCol[i]]);
                SelGroups.Bins.Add(selGB);
                //GroupVision gv = new GroupVision(selGB);
                //gv.Show();
            }
            if (SelGroups.Bins.Count == 0)
                SelGroups = AllGroups;
            ExportStats(SelGroups,true );
            
        }

        public void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
             
            EnergyType = "Cross";
            DoLoadSorting(OriginalCurves);
        }

        public void button3_Click_2(object sender, EventArgs e)
        {
            InterEnergyCheck = "";
            string junk="";
            

            rSquared.Checked = true;
            radioButton1_CheckedChanged(this, EventArgs.Empty);
            junk += RunSorts() + "\n";

            rdSquared.Checked = true;
            radioButton2_CheckedChanged(this, EventArgs.Empty);
            junk += RunSorts() + "\n";

            rAbsolute.Checked = true;
            radioButton3_CheckedChanged(this, EventArgs.Empty);
            junk += RunSorts() + "\n";

            rChiSquared.Checked = true;
            radioButton4_CheckedChanged(this, EventArgs.Empty);
            junk += RunSorts() + "\n";

            rRegression.Checked = true;
            radioButton5_CheckedChanged(this, EventArgs.Empty);
            junk += RunSorts() + "\n";

            rWeightedDiff.Checked = true;
            radioButton6_CheckedChanged(this, EventArgs.Empty);
            junk += RunSorts() + "\n";

            rCrossCorrelation.Checked = true;
            radioButton7_CheckedChanged(this, EventArgs.Empty);
            junk += RunSorts() + "\n";

            Clipboard.SetText(junk);
            //Clipboard.SetText(InterEnergyCheck);
        }

        private void bUntrashButton_Click(object sender, EventArgs e)
        {
            ListBox.SelectedIndexCollection selCol = listTrash.SelectedIndices;
            for (int i = 0; i < selCol.Count; i++)
            {
                GroupBin selGB = ((GroupBin)listTrash.Items[selCol[i]]);
                AllGroups.Bins.Add(selGB);
                LockedGroups.Bins.Remove(selGB);
            }

            ListBins();
            // LTrashed.Text = AllGroups.Bins.Count.ToString();
            progressBar1.Value = 100;
        }

        private void panel2_Resize(object sender, EventArgs e)
        {
            groupBox4.Left = 0;
            groupBox4.Top = 0;
            groupBox4.Height = (int)((double)panel2.Height / 3 - 3);
            groupBox4.Width = panel2.Width - 4;

            groupBox6.Left = 0;
            groupBox6.Top = (int)((double)panel2.Height / 3);
            groupBox6.Height = (int)((double)panel2.Height / 3 - 3);
            groupBox6.Width = panel2.Width - 4;

            groupBox7.Left = 0;
            groupBox7.Top = (int)(2 * (double)panel2.Height / 3);
            groupBox7.Height = (int)((double)panel2.Height / 3 - 3);
            groupBox7.Width = panel2.Width - 4;

        }

        private void UnSort_Click(object sender, EventArgs e)
        {
            ListBox.SelectedIndexCollection selCol = lManual.SelectedIndices;
            for (int i = 0; i < selCol.Count; i++)
            {
                GroupBin selGB = ((GroupBin)lManual.Items[selCol[i]]);
                AllGroups.Bins.Add(selGB);
                ManualGroups.Bins.Remove(selGB);
            }


            ListBins();
            // LTrashed.Text = AllGroups.Bins.Count.ToString();
            progressBar1.Value = 100;
        }

        private void saveBinDescriptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            DialogResult ret = saveFileDialog1.ShowDialog();
            if (ret == DialogResult.OK)
            {
                TextWriter tw = new StreamWriter(saveFileDialog1.FileName);

                progressBar1.Value = 50;
                SaveBin(tw, AllGroups, "");
                //tw.WriteLine("Locked");
                SaveBin(tw, LockedGroups, "L");
                //tw.WriteLine("Trash");
                SaveBin(tw, TrashedGroups, "T");
                tw.Close();
                //LTrashed.Text = AllGroups.Bins.Count.ToString();
                ListBins();
                progressBar1.Value = 100;
            }
        }

        #endregion

        #region SortStats
        string InterEnergyCheck = "";
        private string RunSorts()
        {
            //InterEnergyCheck = "";
            string junk = "";
            string junk2 = "";
            string junk3 = "";
            GroupBin BigBin = new GroupBin(AllGroups.GetNextBinNumber());
            BigBin.GroupsCurves = Relations;
            AllGroups.Bins.Clear();
            AllGroups.Bins.Add(BigBin);
            listMain.Items.Add(BigBin);
            ListBins();

            junk += "Chain \t";
            junk2 += "Chain \t";
            junk3 += "Chain \t";
            ChainSort_Click(this, EventArgs.Empty);
            junk += QuanitfySorting().ToString() + "\t";

            Shake();
            junk2 += QuanitfySorting().ToString() + "\t";

            MontoCarlo();
            MontoCarlo();
            Shake();
            junk3 += QuanitfySorting().ToString() + "\t";
            // button9_Click_1(this, EventArgs.Empty);
            // button5_Click(this, EventArgs.Empty);
            

            BigBin = new GroupBin(AllGroups.GetNextBinNumber());
            BigBin.GroupsCurves = Relations;
            AllGroups.Bins.Clear();
            AllGroups.Bins.Add(BigBin);
            listMain.Items.Add(BigBin);
            ListBins();

            junk += "Histosort\t";
            junk2 += "Histosort\t";
            junk3 += "Histosort\t";
            HistoSort_Click(this, EventArgs.Empty);
            junk += QuanitfySorting().ToString() + "\t";

            Shake();
            junk2 += QuanitfySorting().ToString() + "\t";

            MontoCarlo();
            MontoCarlo();
            Shake();
            junk3 += QuanitfySorting().ToString() + "\t";

            BigBin = new GroupBin(AllGroups.GetNextBinNumber());
            BigBin.GroupsCurves = Relations;
            AllGroups.Bins.Clear();
            AllGroups.Bins.Add(BigBin);
            listMain.Items.Add(BigBin);
            ListBins();

            junk += "Bifurification\t";
            junk2 += "Bifurification\t";
            junk3 += "Bifurification\t";
            BifurificationButton_click(this, EventArgs.Empty);
            junk += QuanitfySorting().ToString() + "\t";

            Shake();
            junk2 += QuanitfySorting().ToString() + "\t";

            MontoCarlo();
            MontoCarlo();
            Shake();
            junk3 += QuanitfySorting().ToString() + "\t";

            BigBin = new GroupBin(AllGroups.GetNextBinNumber());
            BigBin.GroupsCurves = Relations;
            AllGroups.Bins.Clear();
            AllGroups.Bins.Add(BigBin);
            listMain.Items.Add(BigBin);
            ListBins();

            junk += "ShakeAndDivide\t";
            junk2 += "ShakeAndDivide\t";
            junk3 += "ShakeAndDivide\t";
            ShakeSort_Click(this, EventArgs.Empty);
           // ShakeSort_Click(this, EventArgs.Empty);
            junk += QuanitfySorting().ToString() + "\t";

            Shake();
            junk2 += QuanitfySorting().ToString() + "\t";

            MontoCarlo();
            MontoCarlo();
            Shake();
            junk3 += QuanitfySorting().ToString() + "\t";

            return junk + "\t" + junk2 + "\t" + junk3 ;
        }
     
        private string QuanitfySorting()
        {
            //return QuantifyStats();
            int[] Sums = new int[5];
            int[] Wrongs = new int[5];
            int[] nG = new int[5];
            int[,] Rating = new int[5, AllGroups.Bins.Count];
            double AveEnergy = 0;
            double CountEnergy = 0;
            System.Diagnostics.Debug.Print("Method Readout");

            int cN = 0; int jj = 0;
            Application.DoEvents();
            string junk = "";
            string OutText = "MaxBin\tAccuracy\tNumCurves\tGroup Energy\n";
            foreach (GroupBin gb in AllGroups.Bins)
            {
                for (int i = 0; i < nG.Length; i++) nG[i] = 0;

                foreach (IndividualCurveStats ics in gb.GroupsCurves)
                {
                    junk = ics.Filename.ToLower().Replace("curve", "");
                    junk = junk.Replace(".csb", "");
                    int.TryParse(junk, out cN);
                    cN = (int)((double)cN / 1000.0d * 4);
                    nG[cN]++;
                }

                int MaxNg = 0, MCount = 0;
                int SumBins = 0;
                for (int i = 0; i < nG.Length; i++)
                {
                    if (MCount < nG[i])
                    {
                        MCount = nG[i];
                        MaxNg = i;
                    }
                    SumBins += nG[i];
                }

                double Accuracy;
                if (SumBins == 1)
                    Accuracy = 0;
                else 
                    Accuracy = 1 - (double)(SumBins - MCount) / (double)SumBins;


                System.Diagnostics.Debug.Print(MaxNg.ToString() + "\t" + Accuracy.ToString() + "\t" + SumBins.ToString() + "\t");
                double E = gb.GroupSelfEnergy(ref CurveLookup);
                OutText += MaxNg.ToString() + "\t" + Accuracy.ToString() + "\t" + SumBins.ToString() + "\t" +  E.ToString()  +"\n";
                //InterEnergyCheck +="-" +EnergyType +  "\t" + Accuracy.ToString() + "\t" + (E / WholeInteractionEnergy).ToString() + "\t" + E.ToString() + "\n";
                if (double.IsNaN(E) == false)
                {
                    AveEnergy += E * gb.GroupsCurves.Count;
                    CountEnergy += gb.GroupsCurves.Count;
                }
                jj++;
                Sums[MaxNg] += SumBins;
                Wrongs[MaxNg] += (SumBins - MCount);
            }
            double WholeAccuracy = 0;
            double Count = 0;
            OutText += "\n Whole Averages \n Group\tNumberOfCurves\tAccuracy\n";

            for (int i = 0; i < Sums.Length; i++)
            {
                OutText += i.ToString() + "\t" + Sums[i].ToString() + "\t" + (1 - (double)Wrongs[i] / (double)Sums[i]) + "\n";
                if (Sums[i] != 0)
                {
                    WholeAccuracy += (1 - (double)Wrongs[i] / (double)Sums[i]) * Sums[i];
                    Count += Sums[i];
                }
            }
            double d = WholeAccuracy / Count;
            OutText += "Whole Accuracy\n" + d.ToString() + "\nInteraction Energy\n" + ((AveEnergy/CountEnergy)/WholeInteractionEnergy ).ToString() ;
            InterEnergyCheck +=EnergyType + "\tWhole\t" + d.ToString() + "\t" + ((AveEnergy / CountEnergy) / WholeInteractionEnergy).ToString() + "\t" + (AveEnergy / CountEnergy).ToString() + "\n";
            System.Diagnostics.Debug.Print(OutText);
            return d.ToString();
        }

        public double GetWholeInteractionEnergy()
        {
            double Average = 0;
            double Count = 0;
            foreach (GroupBin gb in AllGroups.Bins)
            {
                Average += gb.GroupSelfEnergy(ref CurveLookup) * gb.GroupsCurves.Count ;
                Count += gb.GroupsCurves.Count;
            }
            Average = Average / (double)Count;
            if (AllGroups.Bins.Count > 50)
                Average = Average * 10;
            if (AllGroups.Bins.Count > 150)
                Average = Average * 10;
            return Average;
        }

        private double ExportStats(GroupBins DisplayGroups, bool Display)
        {
            GroupBins CurrentBins;
            List<double[,]> AllCurves = new List<double[,]>();
            CurrentBins = DisplayGroups;
            foreach (GroupBin gb in CurrentBins.Bins)
            {
                double[,] datas = gb.AverageGroup(null);
                double[,] Curve = new double[2, datas.GetLength(1)];
                for (int i = 0; i < datas.GetLength(1); i++)
                {
                    Curve[0, i] = datas[0, i];
                    Curve[1, i] = datas[2, i];
                }
                AFMCurve curve = new AFMCurve();
                curve.Curve = Curve;
                curve.SetMaxes();
                AllCurves.Add(gb.AverageGroup(curve));
            }
            double[] SDs = new double[AllCurves.Count];
            for (int i = 0; i < AllCurves.Count; i++)
            {
                double[,] Curve = AllCurves[i];
                for (int j = 0; j < Curve.GetLength(1); j++)
                {
                    SDs[i] += Curve[1, j] + Curve[3, j];

                }
                SDs[i] = SDs[i] / 2 / Curve.GetLength(1);
            }
            string junk = "";
            double AllSD = 0;
            for (int i = 0; i < AllCurves.Count; i++)
            {
                junk += "Group " + i.ToString() + "\t" + SDs[i].ToString() + "\n";
                AllSD += SDs[i];
            }
            AllSD = AllSD / AllCurves.Count;
            System.Diagnostics.Debug.Print(junk);
            if (Display)
            {
                DataMessageBox dmb = new DataMessageBox();
                dmb.DataMessage(junk);
                dmb.ShowDialog();
            }
            return AllSD;
        }

        private double QuantifyStats()
        {
            ListBox.SelectedIndexCollection selCol = listMain.SelectedIndices;
            GroupBins SelGroups = new GroupBins();
            for (int i = 0; i < selCol.Count; i++)
            {
                GroupBin selGB = ((GroupBin)listMain.Items[selCol[i]]);
                SelGroups.Bins.Add(selGB);
                //GroupVision gv = new GroupVision(selGB);
                //gv.Show();
            }
            if (SelGroups.Bins.Count == 0)
                SelGroups = AllGroups;
            double AllSD = ExportStats(SelGroups, false);
            return AllSD;
        }
        #endregion

        public void rWeightedDiff_CheckedChanged(object sender, EventArgs e)
        {
            EnergyType = "WeightedDiff";
            DoLoadSorting(OriginalCurves);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InterEnergyCheck = "";
            string junk = "";


            rSquared.Checked = true;
            radioButton1_CheckedChanged(this, EventArgs.Empty);
            junk += CheckAccuracy() + "\n";

            rdSquared.Checked = true;
            radioButton2_CheckedChanged(this, EventArgs.Empty);
            junk += CheckAccuracy() + "\n";

            rAbsolute.Checked = true;
            radioButton3_CheckedChanged(this, EventArgs.Empty);
            junk += CheckAccuracy() + "\n";

            rChiSquared.Checked = true;
            radioButton4_CheckedChanged(this, EventArgs.Empty);
            junk += CheckAccuracy() + "\n";

            rRegression.Checked = true;
            radioButton5_CheckedChanged(this, EventArgs.Empty);
            junk += CheckAccuracy() + "\n";

            rWeightedDiff.Checked = true;
            radioButton6_CheckedChanged(this, EventArgs.Empty);
            junk += CheckAccuracy() + "\n";

            rCrossCorrelation.Checked = true;
            radioButton7_CheckedChanged(this, EventArgs.Empty);
            junk += CheckAccuracy() + "\n";

            Clipboard.SetText(junk);
        }
        private string  CheckAccuracy()
        {
            GroupBin[] PreDetermined = new GroupBin[4];
            for (int i = 0; i < 4; i++)
                PreDetermined[i] = new GroupBin(i);
            string junk = "";
            int cN;
            foreach (IndividualCurveStats ics in AllGroups.Bins[0].GroupsCurves)
            {
                junk = ics.Filename.ToLower().Replace("curve", "");
                junk = junk.Replace(".csb", "");
                int.TryParse(junk, out cN);
                cN = (int)((double)cN / 1000.0d * 4);
                PreDetermined[cN].GroupsCurves.Add(ics);
            }
            GroupBin TestBin;
            Random rnd = new Random(DateTime.Now.Millisecond);
            junk = "";
            for (int i = 25; i <= 100; i += 10)
            {
                TestBin = new GroupBin(10);
                for (int j = 0; j < i; j++)
                {
                    TestBin.GroupsCurves.Add(PreDetermined[0].GroupsCurves[rnd.Next(PreDetermined[0].GroupsCurves.Count)]);
                }
                for (int j = i; j < 100; j++)
                {
                    int GroupN = 1 + rnd.Next(3);
                    TestBin.GroupsCurves.Add(PreDetermined[GroupN].GroupsCurves[rnd.Next(PreDetermined[GroupN].GroupsCurves.Count)]);
                }
                junk+=EnergyType + "\t\t" + ((double)i/100.0d).ToString() + "\t" + (TestBin.GroupSelfEnergy(ref CurveLookup) / WholeInteractionEnergy).ToString() + "\n";

            }
            junk += EnergyType + "\t\t1.00\t" + (PreDetermined[0].GroupSelfEnergy(ref CurveLookup) / WholeInteractionEnergy).ToString() + "\n";
            return junk +"\n\n";
        }

        private void SingleCurve_Click(object sender, EventArgs e)
        {
            TextWriter tw;
            progressBar1.Value = 0;
            tw = new StreamWriter(Path.GetDirectoryName(Application.ExecutablePath) + "\\testHistoSort.txt");

            List<GroupBin> SelectedBins = new List<GroupBin>();
            for (int i = 0; i < listMain.SelectedItems.Count; i++)
            {
                GroupBin gb = (GroupBin)listMain.SelectedItems[i];
                SelectedBins.Add(gb);
            }

            if (SelectedBins.Count == 0)
            {
                MessageBox.Show("You must select a group to edit", "");
                return;
            }

            GroupBin Merged = AllGroups.MergeBins(SelectedBins);
            AllGroups.Bins.Remove(Merged);

            SingleCurveComp  SortForm = new SingleCurveComp ();
            SortForm.LoadCurves(this, Merged, AllGroups, TrashedGroups);
            SortForm.ShowDialog(this);

            if (SortForm.OutBins != null)
            {
                for (int i = 0; i < SortForm.OutBins.Count; i++)
                {
                    if (SortForm.OutBins[i].GroupsCurves.Count != 0)
                    {
                        listMain.Items.Add(SortForm.OutBins[i].BinNumber);
                        AllGroups.Bins.Add(SortForm.OutBins[i]);
                    }
                }
            }

            SaveBin(tw, AllGroups, "");
            tw.Close();

            ListBins();
            //LTrashed.Text = AllGroups.Bins.Count.ToString();
            progressBar1.Value = 100;

        }

        private void lManual_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lManual.SelectedItem == null) return;
            ListBox.SelectedIndexCollection selCol = lManual.SelectedIndices;
            List<Graphing.LineInfo> DisplayLines = new List<Graphing.LineInfo>();
            Random rnd = new Random();
            AFMCurve C1 = new AFMCurve();
            Color GroupColor = Color.White;
            for (int i = 0; i < selCol.Count; i++)
            {
                GroupBin gb = (GroupBin)lManual.Items[selCol[i]];
                double MaxCurve = 0;
                int StepJ = 1;
                if (gb.GroupsCurves.Count > 100)
                {
                    StepJ = (int)((double)gb.GroupsCurves.Count / 100);
                }
                for (int j = 0; j < gb.GroupsCurves.Count; j += StepJ)
                {
                    string Filename = CurveNames[gb.GroupsCurves[j].PrimaryIndex];
                    AFMCurve ac = FileHandler.LoadCSBin(DataPath + "\\" + Filename);
                    if (ac.MaxY > MaxCurve)
                    {
                        MaxCurve = ac.MaxY;
                        C1 = ac;
                    }

                }

                if (selCol.Count > 1) GroupColor = Color.FromArgb((int)(255 * rnd.NextDouble()), (int)(255 * rnd.NextDouble()), (int)(255 * rnd.NextDouble()));

                for (int j = 0; j < gb.GroupsCurves.Count; j += StepJ)
                {
                    string Filename = CurveNames[gb.GroupsCurves[j].PrimaryIndex];
                    AFMCurve ac = FileHandler.LoadCSBin(DataPath + "\\" + Filename);

                    double dx;
                    if (C1.MaxY > ac.MaxY)
                    {
                        dx = ac.GetXFromY(ac.MaxY) - C1.GetXFromY(ac.MaxY);
                    }
                    else
                    {
                        dx = ac.GetXFromY(C1.MaxY) - C1.GetXFromY(C1.MaxY);
                    }
                    ac.OffsetCurve(-1 * dx);
                    int nPoints = 35;
                    double[,] Line = new double[2, nPoints];
                    int cc = 0;
                    int stepK = (int)Math.Floor((double)(ac.Curve.GetLength(1) / 25));
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
                    Color UseColor;
                    if (GroupColor == Color.White)
                        UseColor = Color.FromArgb((int)(255 * rnd.NextDouble()), (int)(255 * rnd.NextDouble()), (int)(255 * rnd.NextDouble()));
                    else
                        UseColor = GroupColor;
                    DisplayLines.Add(new Graphing.LineInfo(Line, UseColor, ""));
                }
            }
            Graphing.PlotData(zedGraphControl1, DisplayLines, "Extension (nm)", "Deflection (V)", "");


        }

        private void button2_Click(object sender, EventArgs e)
        {
            bManualSort_Click(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            arrangeFilesWithGroupsToolStripMenuItem_Click(sender, e);
        }


    }
}
