using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GroupCurves
{
    public class FileHandler
    {
        public static AFMCurve LoadDat(string Filename, string CompFilename,CurveSection DesiredSection)
        {
            return Loaders.LoadNIDat.CombineIVS2Dat (Filename, CompFilename, DesiredSection );
        }

        public static AFMCurve LoadIVS(string Filename, CurveSection DesiredSection)
        {
            AFMCurve ac=Loaders.IVSLoader.RootLoadIVS(Filename);
            if (DesiredSection==CurveSection.Approach )
                ac.Curve=ac.ApproachCurve;
            else 
                ac.Curve=ac.WidthdrawalCurve ;

            return ac;
          
        }
        
        public static List<CurveInfo> LoadLSF(string Filename,out double AverageLSF,out double MaxLSF, ref Dictionary<string, int> CurveNumbers, ref Dictionary<int, string> CurveNames, ProgressBar progressBar1)
        {

            FileStream fs = File.OpenRead(Filename);
            BinaryReader br = new BinaryReader(fs);
            int n = br.ReadInt32();
            string[] Filenames = new string[n];
            for (int i = 0; i < n; i++)
            {
                Filenames[i] = br.ReadString();
                CurveNumbers.Add(Filenames[i], i);
                CurveNames.Add(i, Filenames[i]);
            }
            int xn = br.ReadInt32();
            int yn = br.ReadInt32();
            int nTypesLSFs = br.ReadInt32();

            List<CurveInfo> Curves = new List<CurveInfo>();
            //LSFs = new Correlation[xn, yn];
            long square = (long)(xn * yn * 1.05);
            long cc = 0;
            MaxLSF = 0;
            AverageLSF = 0;
            double[] ls = new double[nTypesLSFs];
            for (int i = 0; i < xn; i++)
                for (int j = 0; j < yn; j++)
                {
                    progressBar1.Value = (int)(100 * (double)cc / (double)square);
                   
                    for (int k = 0; k < nTypesLSFs; k++)
                        ls[k] = br.ReadDouble();

                    Correlation cor = new Correlation();
                    cor.LeastSquaresFit=ls[0];
                    cor.AbsoluteFit=ls[1];
                    cor.LinearRegression=ls[2];
                    cor.WeightedLeastSquarsFit=ls[3];
                    cor.ChiSquared=ls[4];
                    cor.dLeastSquaresFit = ls[5];
                   // cor.CrossCorrelation = ls[6];

                    CurveInfo CI=new CurveInfo(i, j, cor ,CurveNames[i]);
                    Curves.Add(CI);
                    if (cor.LeastSquaresFit > MaxLSF) MaxLSF = cor.LeastSquaresFit;
                    AverageLSF += cor.LeastSquaresFit;
                    Application.DoEvents();
                    cc++;
                }
            AverageLSF = AverageLSF / cc;
            br.Close();
            fs.Close();

            //DoLoadSorting(Curves);
            progressBar1.Value = 100;
            return Curves;
        }
        
        public static List<CurveInfo> LoadVB(string filename, ref Dictionary<string, int> CurveNumbers, ref Dictionary<int, string> CurveNames, ProgressBar progressBar1)
        {
            TextReader tr = new StreamReader(filename);

            char[] ent = { '\n' };
            char[] seps = { '\t' };
            int RunningIndex = 0;
            progressBar1.Value = 0;
            //string File = tr.ReadToEnd();
            progressBar1.Value = 25;
            //string[] Lines = File.Split(ent);
            string input = null;
            List<CurveInfo> Curves = new List<CurveInfo>();
            while ((input = tr.ReadLine()) != null)
            {

                progressBar1.Value = RunningIndex % 100;
                Application.DoEvents();
                string[] parts = input.Split(seps);
                double LSF = 0;
                double LSFDerive = 0;
                int PI = 0;
                int SI = 0;
                Double.TryParse(parts[4], out LSF);
                Double.TryParse(parts[5], out LSFDerive);
                int.TryParse(parts[1], out PI);
                int.TryParse(parts[3], out SI);
                string FileIndex = parts[0] + PI.ToString();
                string SecondaryFileIndex = parts[2] + SI.ToString();
                Correlation c = new Correlation();
                c.LeastSquaresFit=LSF;
                c.dLeastSquaresFit=LSFDerive;
                int FirstIndex = 0;
                int SecondIndex = 0;
                if (LSF < 10)
                {
                    if (CurveNumbers.ContainsKey(FileIndex))
                        FirstIndex = CurveNumbers[FileIndex];
                    else
                    {
                        CurveNumbers.Add(FileIndex, RunningIndex);
                        CurveNames.Add(RunningIndex, FileIndex);
                        FirstIndex = RunningIndex;
                        RunningIndex++;
                    }
                    if (CurveNumbers.ContainsKey(SecondaryFileIndex))
                        SecondIndex = CurveNumbers[SecondaryFileIndex];
                    else
                    {
                        CurveNumbers.Add(SecondaryFileIndex, RunningIndex);
                        CurveNames.Add(RunningIndex, SecondaryFileIndex);
                        SecondIndex = RunningIndex;
                        RunningIndex++;
                    }

                    Curves.Add(new CurveInfo(FirstIndex, SecondIndex, c,CurveNames[FirstIndex]));
                }
            }
            tr.Close();
            return Curves;
        }

        public static void SaveCSBin(string Filename,string OrigFilename, Int32 x, Int32 y, double[,] Curve)
        {
            FileStream fs = File.OpenWrite (Filename);
            BinaryWriter br = new BinaryWriter(fs);
            br.Write(x);
            br.Write(y);
            br.Write(OrigFilename);
            
            Int32 nPoints = Curve.GetLength(1);
            br.Write(nPoints);
           
            for (int i = 0; i < nPoints; i++)
            {
                double xx = Curve[0,i];
                double yy = Curve[1,i];
                br.Write(xx);
                br.Write(yy);
            }
            br.Close();
            fs.Close();
           
        }

        public static  AFMCurve LoadCSBin(string Filename)
        {
            FileStream fs = File.OpenRead(Filename);
            BinaryReader br = new BinaryReader(fs);
            int x = br.ReadInt32();
            int y = br.ReadInt32();
            string OriginalFile = br.ReadString();
            int nPoints = br.ReadInt32();
            double[,] Curve = new double[2, nPoints];
            double MinC = 100000;
            double MaxC = -1 * MinC;
            double MinX = MinC;
            double maxX = MaxC;
            for (int i = 0; i < nPoints; i++)
            {
                double xx = br.ReadDouble();
                double yy = br.ReadDouble();
                Curve[0, i] = xx;
                Curve[1, i] = yy;
                if (yy > MaxC) MaxC = yy;
                if (yy < MinC) MinC = yy;
                if (xx > maxX) maxX = xx;
                if (xx < MinX) MinX = xx;
            }
            br.Close();
            fs.Close();
            AFMCurve curve = new AFMCurve();
            curve.OriginalFilename = OriginalFile;
            curve.X = x;
            curve.Y = y;
            curve.Curve = Curve;
            curve.MaxY = MaxC;
            curve.MinY = MinC;
            curve.MaxX = maxX;
            curve.MinX = MinX;
            // PlotData(zedGraphControl1, Curve, "AFMCurve");
            return curve;
        }
        
        public static  void WriteLSFArray(ref Correlation[,] LSFs, string path, string[] Files)
        {
            FileStream fs = File.Create(path + "\\Array.LSF");
            fs.Seek(0, SeekOrigin.Begin);
            BinaryWriter bw = new BinaryWriter(fs);

            int n = Files.Length;
            bw.Write(n);

            foreach (string s in Files)
            {
                string filename = System.IO.Path.GetFileName(s);
                bw.Write(filename);
            }
            int xn = LSFs.GetLength(0);
            bw.Write(xn);
            int yn = LSFs.GetLength(1);
            bw.Write(yn);
            int nTypesLSFs = 7;
            bw.Write(nTypesLSFs);
            List<CurveInfo> Curves = new List<CurveInfo>();
            for (int i = 0; i < xn; i++)
                for (int j = 0; j < yn; j++)
                {
                    if (LSFs[i, j] == null)
                    {
                        double dummy = 0;
                        bw.Write(dummy);
                        bw.Write(dummy);
                        bw.Write(dummy);
                        bw.Write(dummy);
                        bw.Write(dummy);
                        bw.Write(dummy);
                        bw.Write(dummy);
                    }
                    else
                    {
                        bw.Write(LSFs[i, j].LeastSquaresFit);
                        bw.Write(LSFs[i, j].AbsoluteFit);
                        bw.Write(LSFs[i, j].LinearRegression);
                        bw.Write(LSFs[i, j].WeightedLeastSquarsFit);
                        bw.Write(LSFs[i, j].ChiSquared);
                        bw.Write(LSFs[i, j].dLeastSquaresFit);
                        bw.Write(LSFs[i, j].CrossCorrelation);
                    }
                }


           

            bw.Close();
            fs.Close();
        }

        

    }
}
