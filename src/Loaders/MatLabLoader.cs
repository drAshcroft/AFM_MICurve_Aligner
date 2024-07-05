using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GroupCurves.Loaders
{
    public class MatLabLoader
    {
       
   
        public static AFMCurve  LoadMatLabFC( string filename)
        {
            AFMCurve  ForceCurve = new AFMCurve() ;

           
            
            double [,] DataDump = null;
            int ret = 0;
            System.DateTime CreateTime = default(System.DateTime);
           

            ForceCurve = null;
            DataDump = null;
            ret = LoadFC(filename,ref DataDump,ref CreateTime, false,out  ForceCurve);
            try
            {
                if (DataDump[0, 0] < DataDump[0, DataDump.GetLength(1) - 1])
                    DataDump = LoadNIDat.ReverseArray(DataDump);

                ForceCurve.ApproachCurve = DataDump;
                ForceCurve.WidthdrawalCurve = DataDump;
                ForceCurve.Curve = DataDump;
                ForceCurve.SetMaxes();
                return ForceCurve;
            }
            catch
            {
                return null;
            }

       
        }


        private static int LoadFC(string Filename, ref double [,] temp, ref System.DateTime CreateTime, bool FreqPlot, out  AFMCurve  ForceCurve)
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
              
               // if (Line.Contains("data ") || Line.Contains("data") || Line.Contains("Data")) {
               //     HeaderFound = true;
               //     goto Out;
               // }
                if (Line != "" && Line.Length >=4)
                {
                    if (Line.Trim().ToLower() == "data")
                    {
                        HeaderFound = true;
                        goto Out;
                    }

                    if (Line.Substring(1, 4) == "data\t")
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
        
        
        double  JunkD = 0;
        
        List<double> DataPoints = new List<double>();
        //IVSFile.ReadBytes(2);

        int cc = 0;
        {
            cc = -1;
            try
            {
                while ((!EOF))
                {
                    cc = cc + 1;
                    JunkD = IVSFile.ReadDouble();

                    DataPoints.Add(JunkD);
                }
            }
            catch { }
            int nPoints= (int)(cc / 2);
            temp = new double[2, nPoints ];

            for (int i = 0; i < nPoints ; i++)
            {
                temp[0, i] = DataPoints[i];
                temp[1, i] = DataPoints[i + nPoints];

            }
        }

        IVSFile.Close();
        fs.Close();

        return temp.GetLength(0) ;
        
    }
    }

}

 