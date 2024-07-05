using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GroupCurves.Loaders
{
    public class IVSLoader
    {
    
        public static AFMCurve  RootLoadIVS( string filename)
        {
            AFMCurve  ForceCurve = new AFMCurve() ;

            
            int N = 0;
            int I = 0;
            float max = 0;
            float Min = 0;
            float X = 0;
            float maxX = 0;
            float MinX = 0;
            float Y = 0;
            
            double [,] DataDump = null;
            int ret = 0;
            System.DateTime CreateTime = default(System.DateTime);
            double[,] SectionData = null;
            double[,] SectionData2 = null;
            

            ForceCurve = null;
            DataDump = null;
            ret = LoadIVS(filename,ref DataDump,ref CreateTime, false,out  ForceCurve);
            if (ret == -1)
            {
                return null;
            }
            int HalfPoint = 0;
            string[] parts = null;
            int cc = 0;
            int cc2 = 0;
            int startDirection = 0;
            double  Min1 = 0;
            double  Min2 = 0;
           
            {
               
               // ForceCurve.CreateTime = CreateTime;
                parts = filename.Split(new char[] {'\\'});
               
                max = -100000;
                Min = 100000;
                maxX = max;
                MinX = Min;
                cc = -1;
                cc2 = -1;
               
                HalfPoint = 0;
                startDirection = (int)DataDump[1, 2];
                Min1 = 10000;
                Min2 = 10000;
                SectionData = new double[2, ret];
                SectionData2 = new double[2, ret];
                for (I = 0; I < ret; I++)
                {
                    if (HalfPoint == 0)
                    {
                        if (!(DataDump[I, 0] == 0 | DataDump[I, 1] == 0 | DataDump[I, 1] > 9.5))
                        {
                            cc = cc + 1;
                            SectionData[1, cc] = DataDump[I, 1];
                            SectionData[0, cc] = DataDump[I, 0];
                            if (DataDump[I, 0] < Min1) Min1 = DataDump[I, 0];
                        }
                    }
                    else
                    {
                        if (!(DataDump[I, 0] == 0 | DataDump[I, 1] == 0 | DataDump[I, 1] > 9.5))
                        {
                            cc2 = cc2 + 1;
                           
                            SectionData2[1, cc2] = DataDump[I, 1];
                            SectionData2[0, cc2] = DataDump[I, 0];
                            if (DataDump[I, 0] < Min2) Min2 = DataDump[I, 0];
                            
                        }
                    }
                    if (DataDump[I, 2] != startDirection & HalfPoint == 0)
                    {
                        HalfPoint = I;
                    }
                }
                if (cc < 1) return null;
                double[,] FinalApproach = new double[2, cc];
                double[,] FinalWithdraw = new double[2, cc2];
                for (int i = 0; i < cc; i++)
                {
                    FinalApproach[0, i] = SectionData[0, i];
                    FinalApproach[1, i] = SectionData[1, i];
                }
                for (int i = 0; i < cc2; i++)
                {
                    FinalWithdraw[0, i] = SectionData2[0, i];
                    FinalWithdraw[1, i] = SectionData2[1, i];
                }

                ForceCurve.ApproachCurve = FinalApproach;
                ForceCurve.WidthdrawalCurve = FinalWithdraw;
                
                ForceCurve.Curve = FinalApproach;
                if (HalfPoint == 0 | ret == 0) return  null;
            }


            if (cc <= 0) return null;

            return ForceCurve ;
       
        }


        private static int LoadIVS(string Filename, ref double [,] temp, ref System.DateTime CreateTime, bool FreqPlot, out  AFMCurve  ForceCurve)
    {
        ForceCurve = new AFMCurve();
        
        byte junk = 0;
        string Line = null;
        bool TextFormat = false;
        int I = 0;
        bool HeaderFound = false;
        HeaderFound = false;

        FileStream fs = File.OpenRead(Filename);
            
        BinaryReader IVSFile=new BinaryReader( fs );
       
        Line = "";
        string[] parts = null;
        bool EOF=false ;
        while ((! EOF ) & (!HeaderFound)) {
            try 
            {
                junk=IVSFile.ReadByte();
            }
            catch 
            {
                EOF=true;
            }
            if (junk == 10) {
                if (Line.Contains( "ascii_format ")) {
                    if (Line.Contains( "true")) TextFormat = true; 
                }
                if (Line.Contains(  "acq_tip_pos_x")) {
                    string junkStr=Line.Replace("acq_tip_pos_x", "");
                    ForceCurve.X =Int16.Parse(junkStr);
                }
                if (Line.Contains(  "acq_tip_pos_y")) {
                    string junkStr=Line.Replace("acq_tip_pos_y", "");
                    ForceCurve.Y =Int16.Parse(junkStr);
                }
                
                if (Line.Contains("data ")) {
                    HeaderFound = true;
                    goto Out;
                }
                if (Line != "" && Line.Length >5)
                {
                    if (Line.Substring(1, 5) == "data ")
                    {
                        HeaderFound = true;
                        goto Out;
                    }

                    if (Line.Substring(1, 5) == "data\t")
                    {
                        HeaderFound = true;
                        goto Out;
                    }
                }
                
                
                
                Line = "";
            }
            else {
                Line = Line + (char) junk;
            }
            
        }
        
        
        
        if (!HeaderFound) {
             IVSFile.Close();
            fs.Close();
            return -1 ;
        }
        Out:
        string[] chunks = null;
        int N = 0;
        parts =Line.Split(new char[] {'a'});
         
        N = Int16.Parse (parts[parts.Length-1]);
        temp = new double[N, 4];
        
        
        string junkS = null;
        double  JunkD = 0;
        byte junkL = 0;
        byte JunkBool = 0;
        short junkI = 0;
        int cc = 0;
        {
            cc = -1;
            while ((!EOF ) && (cc < N-1)) {
                cc = cc + 1;
                JunkD= IVSFile.ReadSingle ();
                temp[cc, 0] = JunkD;

                JunkD = IVSFile.ReadSingle();
                temp[cc, 1] = JunkD;
                
                JunkD = IVSFile.ReadSingle();
                junkL = IVSFile.ReadByte();
                
                temp[cc, 2] = junkL;
                
                for (I = 0; I <= 6; I++) {
                    JunkBool= IVSFile.ReadByte();
                    if (I == 3) temp[cc, 3] = JunkBool; 
                }
                
            }
            
        }
               IVSFile.Close();
               fs.Close();
        return temp.GetLength(0) ;
        
    }
    }

}
/*if (TextFormat) {
            junkS = FileSystem.LineInput(fff);
            parts = Strings.Split(junkS, Strings.Chr(10), , CompareMethod.Binary);
            for (I = 0; I <= N - 1; I++) {
                if (Strings.InStr(1, parts(I), "last_entry", CompareMethod.Text)) break; // TODO: might not be correct. Was : Exit For
 
                chunks = Strings.Split(parts(I), Strings.Chr(9), , CompareMethod.Binary);
                
                temp(I, 0) = Conversion.Val(chunks(0));
                temp(I, 1) = Conversion.Val(chunks(1));
                if (Strings.Asc(chunks(2)) == 171) {
                    temp(I, 2) = 0;
                }
                else {
                    temp(I, 2) = 1;
                }
                if (chunks(2) == "x") temp(I, 3) = 0;                 else temp(I, 3) = 1; 
            }
            
        }
        else */