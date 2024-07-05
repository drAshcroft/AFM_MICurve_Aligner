using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GroupCurves.Loaders
{
    public class LoadNIDat
    {
        public static double[,] LoadDatFile(string Filename)
        {
            FileStream fs = File.OpenRead(Filename);

            BinaryReader DATFile = new BinaryReader(fs);
            int I = 0;
            I = 0;
            double junkD = 0;
            junkD = 1;
            List<double> CurveData = new List<double>();
            double TT = 0;
            bool EOF = false;
            while (!EOF)
            {
                try
                {
                    junkD = DATFile.ReadDouble();
                }
                catch
                {
                    EOF = true;
                }


                if (!(double.IsInfinity(junkD) || double.IsNaN(junkD)))
                {

                    if (junkD > -10 & junkD < 10)
                    {
                        TT = System.Math.Abs(junkD);

                        if (System.Math.Abs(junkD) < 10)
                        {
                            CurveData.Add(junkD);

                        }

                    }
                }
            }

            DATFile.Close();
            fs.Close();
            double[,] outData = new double[2, CurveData.Count];
            for (int i = 0; i < CurveData.Count; i++)
            {
                outData[0, i] = i;
                outData[1, i] = CurveData[i];
            }
            return outData ;
        }
            
    
    public static AFMCurve  CombineIVS2Dat(string filename, string CompanionFilename, CurveSection DesiredSection)
    {

        double[,] NI_Data = LoadDatFile(filename);


        AFMCurve CompData = new AFMCurve();
        
        bool found = false;
        //now get the companion file and all its data
        if (System.IO.Path.GetExtension(CompanionFilename).ToLower() == ".ivs")
        {
            CompData = IVSLoader.RootLoadIVS (CompanionFilename);
        }
        else {
        }
        
        found = ((CompData != null) && ( NI_Data!=null)  );
        if (!found) {
            return null ;
        }
        
        
        double[,] CompApproach;
        double[,] CompWithdraw;
        
        double RMS = 0;
        
        double[,] ReformedApproach = null;
        double[,] ReformedWithdraw = null;

        CompApproach = CompData.ApproachCurve  ;
        CompWithdraw = CompData.WidthdrawalCurve;

        ReformedApproach = CombineArrays(ref CompApproach,ref NI_Data,ref  RMS);

        NI_Data = ReverseArray(NI_Data);
        CompWithdraw = ReverseArray(CompWithdraw);

        ReformedWithdraw = CombineArrays(ref CompWithdraw, ref NI_Data, ref RMS);

        bool BadFit = false;
        
        if (RMS > 0.3) BadFit = true; 
        
        double[,] FinalApproach = null;
        double[,] FinalWithdraw =null;
        int cc2 = 0;
        int ccA = 0;
        int ccW = 0;

        if (!BadFit)
        {
            FinalApproach = new double[2, ReformedApproach.GetLength(1)];
            for (int I = 0; I < ReformedApproach.GetLength(1); I++)
            {
                if (ReformedApproach[1, I] != 0)
                {
                    FinalApproach[0, ccA] = ReformedApproach[0, I];
                    FinalApproach[1, ccA] = ReformedApproach[1, I];
                    ccA = ccA + 1;
                }
            }
            FinalWithdraw = new double[2, ReformedWithdraw.GetLength(1)];
            for (int I = 0; I < ReformedWithdraw.GetLength(1); I++)
            {
                if (ReformedWithdraw[1, I] != 0)
                {
                    FinalWithdraw[0, ccW] = ReformedWithdraw[0, I];
                    FinalWithdraw[1, ccW] = ReformedWithdraw[1, I];
                    ccW = ccW + 1;
                }
            }
        }
        else
        {
            FinalApproach = new double[2, CompApproach.GetLength(1)];
            for (int I = 0; I < CompApproach.GetLength(1); I++)
            {
                if (CompApproach[1, I] != 0)
                {
                    FinalApproach[0, ccA] = CompApproach[0, I];
                    FinalApproach[1, ccA] = CompApproach[1, I];
                    ccA = ccA + 1;
                }

            }
            FinalWithdraw = new double[2, CompWithdraw.GetLength(1)];
            for (int I = 0; I < CompWithdraw.GetLength(1); I++)
            {
                if (CompWithdraw[1, I] != 0)
                {
                    FinalWithdraw[0, ccW] = CompWithdraw[0, I];
                    FinalWithdraw[1, ccW] = CompWithdraw[1, I];
                    ccW = ccW + 1;
                }

            }
        }


        double[,] VeryFinalApproach = new double[2, ccA];
        double  MinX = 0;
        double  MinY = 0;
        MinY = FinalApproach[1, 0];
        MinX = FinalApproach[0, ccA-1];
        for (int I = 0; I < ccA; I++) {
            VeryFinalApproach[0, I] = FinalApproach[0, I] - MinX;
            VeryFinalApproach[1, I] = FinalApproach[1, I] - MinY;
        }

        double[,] VeryFinalWithdraw = new double[2, ccW];
        MinX = 0;
        MinY = 0;
        MinY = FinalWithdraw[1, 0];
        MinX = FinalWithdraw[0, ccW - 1];
        for (int I = 0; I < ccW; I++)
        {
            VeryFinalWithdraw[0, I] = FinalWithdraw[0, I] - MinX;
            VeryFinalWithdraw[1, I] = FinalWithdraw[1, I] - MinY;
        }
        
        AFMCurve  fc = new AFMCurve ();
        fc.WidthdrawalCurve = VeryFinalWithdraw;
        fc.ApproachCurve = VeryFinalApproach;

        if (DesiredSection == CurveSection.Approach)
            fc.Curve = VeryFinalApproach;
        else
            fc.Curve = VeryFinalWithdraw;


        return fc;
    }

    public static double[,] ReverseArray(double[,] inArray)
    {
        
        int l = inArray.GetLength(1);
        double[,] OutArray = new double[2,l];
        for (int i = 0; i < l; i++) {
            OutArray[0, i] = inArray[0, l - i-1];
            OutArray[1, i] = inArray[1, l - i-1];
        }
        return OutArray;
    }
    private static double[,] CombineArrays(ref double[,] CompApproach, ref double[,] NI_Data, ref double RMS)
    {
        double[,] IndexedCurve = new double[2,CompApproach.GetLength(1)];
        int cc = 0;
        double junkD = 0;
        
        
        double MaxComp = 0;
        double MinComp = 0;
        cc = 0;
        MaxComp = -10000000;
        MinComp = 10000000;
        for (int I = 0; I <CompApproach.GetLength(1); I++) {
            
            IndexedCurve[0, cc] = cc;
            IndexedCurve[1, cc] = CompApproach[1, I];
            junkD = IndexedCurve[1, cc];
            if (junkD > MaxComp) MaxComp = junkD; 
            if (junkD < MinComp) MinComp = junkD;
            cc = cc + 1;
        }
        
        
        int MaxIndex = 0;
        double max = 0;
        //find the max of the comp data 
        max = FindMax(ref IndexedCurve,ref  MaxIndex);
        for (int I = 0; I < IndexedCurve.GetLength(1); I++) {
            IndexedCurve[0, I] = IndexedCurve[0, I] - MaxIndex;
        }
        //zoom along the line until you see the same max of the NI data 
        int MaxPoint = 0;
        int ReturnMax = 0;
        MaxPoint = FindNIMax(ref NI_Data,ref  max, true);
        
        for (int I = 0; I < NI_Data.GetLength(1); I++) {
            NI_Data[0, I] = NI_Data[0, I] - MaxPoint + 0;
        }
        ReturnMax = FindNIMax(ref NI_Data,ref  max, false);
        //now convert between the index of the compdata and the index count of the NI data,  This makes it easier to look up the correct x value from
        // the compdata array
        double crosspoint = 0;
        if (MaxIndex - 30 > 0) {
            crosspoint = System.Math.Abs(MaxPoint - FindNIMax(ref NI_Data,ref IndexedCurve[1, MaxIndex - 30], true));
            if (crosspoint != 0) {
                for (int I = 0; I < NI_Data.GetLength (1); I++) {
                    NI_Data[0, I] = NI_Data[0, I] * 30 / crosspoint;
                }
            }
        }
        
        
        
        
        //cut the data into the approach and then compare the result to make sure that 
        //the compiled form is correct.
        double[,] ReformedCurve = new double[2, NI_Data.GetLength(1)];
        
        double maxX = 0;
        double xx = 0;
        double X = 0;
        double u = 0;
        double Y = 0;
        RMS = 0;
        cc = 0;
        maxX = IndexedCurve[0, IndexedCurve.GetLength(1)-1] * 1.1;
        for (int I = 0; I < NI_Data.GetLength(1); I++) {
            if ((NI_Data[0, I] > (-1 * MaxIndex)) && (NI_Data[0, I] < maxX)) 
            {
                
                X = NI_Data[0, I] + MaxIndex;
                u = X - Math.Floor (X);
                if (X + 2 > CompApproach.GetLength(1)) {
                    Y = CompApproach.GetLength (1) - 2;
                    xx = (CompApproach[0, (int)(Y + 1)] - CompApproach[0,(int) Y]) * (X - Y) + CompApproach[0, (int)Y];
                }
                else {
                    xx = (CompApproach[0, (int)(X + 1)] - CompApproach[0, (int)(X)]) * u + CompApproach[0, (int)(X)];
                }
                ReformedCurve[0, cc] = xx;
                ReformedCurve[1, cc] = NI_Data[1, I];
                X = NI_Data[0, I] + MaxIndex;
                u = X - Math.Floor (X);
                if (X + 2 > CompApproach.GetLength(1))
                {
                    Y = CompApproach.GetLength(1) - 2;
                    xx = (CompApproach[1,(int)( Y + 1)] - CompApproach[1,(int) Y]) * (X - Y) + CompApproach[1,(int) Y];
                }
                else {
                    xx = (CompApproach[1,(int)( X + 1)] - CompApproach[1,(int) X]) * u + CompApproach[1, (int)X];
                }
                RMS = RMS + System.Math.Abs(ReformedCurve[1, cc] - xx);
                cc = cc + 1;
            }
        }
        if (cc == 0)
        {
            RMS = 100000;
        }
        else
        {
            RMS = RMS / cc;
        }
        
        if (cc < CompApproach.GetLength(1)) RMS=1000; 
        double[,] FinalCurve = new double[2,cc];
        for (int i = 0; i < cc; i++)
        {
            FinalCurve[0, i] = ReformedCurve[0, i];
            FinalCurve[1, i] = ReformedCurve[1, i];
        }
        return FinalCurve ;
    }
    
    private static int FindNIMax(ref double[,] Data, ref double Point, bool ForwardDirection)
    {
        int functionReturnValue = 0;
        int I = 0;
        int markpoint = 0;
        if (ForwardDirection) {
            for (I = (int)(Data.GetLength(1) * 0.15); I < Data.GetLength(1) - 1; I++)
            {
                if (Data[1, I] <= Point & Data[1, I + 1] > Point) {
                    markpoint = I;
                    return  I;
                    
                }
            }
        }
        else {
            for (I = Data.GetLength(1) - 2; I >= Data.GetLength(1) * 0.15; I += -1)
            {
                if (Data[1, I] <= Point & Data[1, I + 1] > Point) {
                    markpoint = I;
                    return I;
                }
            }
        }
        
        
        return functionReturnValue;
        
    }
    private static double FindMax(ref double[,] Data, ref int MaxIndex)
    {
        double max = 0;
        int I = 0;
        max = 0;
        for (I = 0; I < Data.GetLength(1); I++)
        {
            if (Data[1, I] > max) {
                max = Data[1, I];
                MaxIndex = I;
            }
        }
        return max;
    }


    }
}
