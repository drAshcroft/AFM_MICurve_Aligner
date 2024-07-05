using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GroupCurves.Loaders
{
    public class gTextLoader
    {
    
        public static AFMCurve  LoadTXT( string filename)
        {
            AFMCurve  ForceCurve = new AFMCurve() ;
            
            double [,] DataDump = null;
            int ret = 0;
            System.DateTime CreateTime = default(System.DateTime);
           
            ForceCurve = null;
            DataDump = null;
            ret = OpenFile(filename,ref DataDump,ref CreateTime, false,out  ForceCurve);

            if (DataDump[0,0]<DataDump[0,DataDump.GetLength(1)-1])
                DataDump= LoadNIDat.ReverseArray(DataDump);

            ForceCurve.ApproachCurve = DataDump;
            ForceCurve.WidthdrawalCurve = DataDump;
            ForceCurve.Curve = DataDump;
            ForceCurve.SetMaxes();
            return ForceCurve ;
       
        }

        private static double[,] ReverseArray(double[,] inArray)
        {

            int l = inArray.GetLength(1);
            double[,] OutArray = new double[2, l];
            for (int i = 0; i < l; i++)
            {
                OutArray[0, i] = inArray[0, l - i - 1];
                OutArray[1, i] = inArray[1, l - i - 1];
            }
            return OutArray;
        }
        private static int OpenFile(string Filename, ref double [,] temp, ref System.DateTime CreateTime, bool FreqPlot, out  AFMCurve  ForceCurve)
        {
            ForceCurve = new AFMCurve();
            
            string Line = null;
           
            int I = 0;
            FileStream fs = File.OpenRead(Filename);
                
            TextReader IVSFile=new StreamReader( fs );
           
            Line = "";
            string[] parts = null;
            bool EOF=false ;
            double x;
            double y;
            List<string> Lines = new List<string>();
            Line= IVSFile.ReadLine();
            ForceCurve.ExtraInformation.Add("StartGroup", Line.Trim());
            while ((! EOF ) && (Line!=null)) 
            {
                try 
                {
                    Line =IVSFile.ReadLine() ;
                    if (Line!=null)
                        Lines.Add(Line);
                }
                catch 
                {
                    EOF=true;
                }
            }
            IVSFile.Close();
            fs.Close();

            temp = new double[2, Lines.Count ];

            for (int i=0;i<Lines.Count ;i++)
            {
                parts = Lines[i].Split(new char[] { '\t', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                double.TryParse(parts[0],out x);
                double.TryParse(parts[1],out y);
                temp[0, i] = x;
                temp[1, i] = y;
            }
            
            return temp.GetLength(0) ;
            
        }
    }

}

