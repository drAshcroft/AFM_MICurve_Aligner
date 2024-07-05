using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroupCurves.Loaders
{
    class DILoader
    {

        /*
            static float static_LoadDI_LX = 0;
            static float static_LoadDI_Ly = 0;
            static int nPoints;
            static int DataStart;
            static int DataLength;
            static float senszscan;
            static float ramp;
            static float Xoffset;
            static float YOffset;
            static float Zscale;
            static float ZDeflection;


            public static RootLoader.ForceCurveType RootLoadDI(ref string filename)
            {
                RootLoader.ForceCurveType ForceCurve = default(RootLoader.ForceCurveType);
                ForceCurve = null;
                // ERROR: Not supported in C#: OnErrorStatement

                bool notdisplayed = false;
                int FileN = 0;
                int N = 0;
                int I = 0;
                float max = 0;
                float Min = 0;
                float X = 0;
                float maxX = 0;
                float MinX = 0;
                float Y = 0;
                string junk = null;
                int nPoints = 0;
                int jj = 0;
                float[] DataDump = null;
                int ret = 0;
                System.DateTime CreateTime = default(System.DateTime);
                double[,] SectionData = null;
                double[] SectionData2 = null;

                RootLoadDI = null;
                DataDump = null;
                ret = LoadDI(filename, DataDump, CreateTime, false, ForceCurve);
                if (ret == -1)
                {
                    RootLoadDI = null;
                    return;
                }

                int cc = 0;
                int cc2 = 0;
                int startDirection = 0;
                float Min1 = 0;
                float Min2 = 0;
                string[] parts = null;
                double s = 0;
                double b = 0;
                int cc3 = 0;
                float[,] SlopeTest = new float[2, 11];
                {
                    parts = Strings.Split(filename, "\\");
                    ForceCurve.filename = parts(Information.UBound(parts));

                    nPoints = -1;
                    max = -100000;
                    Min = 100000;
                    maxX = max;
                    MinX = Min;
                    cc = -1;
                    cc2 = -1;

                    Min1 = 10000;
                    Min2 = 10000;

                    SectionData = ForceCurve.Sections(0).Curve;
                    for (I = 0; I <= Information.UBound(SectionData, 2); I++)
                    {

                        if (SectionData(0, I) < Min1) Min1 = SectionData(0, I);
                    }

                    SectionData = ForceCurve.Sections(1).Curve;
                    for (I = 0; I <= Information.UBound(SectionData, 2); I++)
                    {
                        if (SectionData(0, I) < Min2) Min2 = SectionData(0, I);
                        if (cc3 < 11)
                        {

                            SlopeTest(0, cc3) = SectionData(0, I);
                            SlopeTest(1, cc3) = SectionData(1, I);
                            cc3 = cc3 + 1;
                        }
                    }


                }

                RootLoadDI = ForceCurve;
                return;
            errhandl:
                return null;


            }
            private static int LoadDI(ref string filename, ref float[] temp, ref System.DateTime CreateTime, ref bool FreqPlot, ref RootLoader.ForceCurveType ForceCurve)
    {
         // ERROR: Not supported in C#: OnErrorStatement

        FileSystem.FileOpen(1, filename, OpenMode.Input);
        FileSystem.FileClose(1);
        
        FileSystem.FileOpen(1, filename, OpenMode.Binary);
        
        float[] DataArray = null;
        int cc = 0;
        int I = 0;
        int nPoints2 = 0;
        string[] parts = null;
        System.DateTime CreateDate = default(System.DateTime);
        bool FoundY = false;
        //load header
         // ERROR: Not supported in C#: ReDimStatement

        byte c = 0;
        string j = null;
        bool found = false;
        string jj = null;
        while (!FileSystem.EOF(1)) {
            j = FileSystem.LineInput(1);
            //  If Left$(j, 1) = "\" Then Debug.Print j
            if (Strings.InStr(1, j, "\\Data length:", CompareMethod.Text) != 0) {
                j = Strings.Trim(Strings.Replace(j, "\\Data length:", "", , , CompareMethod.Text));
                DataLength = Conversion.Val(j);
            }
            if (Strings.InStr(1, j, "\\Date:", CompareMethod.Text) != 0) {
                j = Strings.Trim(Strings.Replace(j, "\\Date:", "", , , CompareMethod.Text));
                //03:53:39 PM Fri May 23 2008
                parts = Strings.Split(j, " ");
                CreateTime = (System.DateTime)parts(0);
                CreateDate = (System.DateTime)parts(3) + " " + parts(4) + " " + parts(5);
            }
            //\X offset:
            if (Strings.InStr(1, j, "\\X offset:", CompareMethod.Text) != 0) {
                j = Strings.Trim(Strings.Replace(j, "\\X offset:", "", , , CompareMethod.Text));
                Xoffset = Conversion.Val(j);
            }
            if (Strings.InStr(1, j, "\\Y offset:", CompareMethod.Text) != 0) {
                j = Strings.Trim(Strings.Replace(j, "\\Y offset:", "", , , CompareMethod.Text));
                YOffset = Conversion.Val(j);
                FoundY = true;
            }
            
            if (Strings.InStr(1, j, "\\Samps/line:", CompareMethod.Text) != 0) {
                j = Strings.Trim(Strings.Replace(j, "\\Samps/line:", "", , , CompareMethod.Text));
                parts = Strings.Split(j, " ");
                nPoints = Conversion.Val(parts(0));
            }
            
            if (Strings.InStr(1, j, "\\Data offset:", CompareMethod.Text) != 0) {
                DataStart = Conversion.Val(Strings.Replace(j, "\\Data offset:", "", , , CompareMethod.Text));
            }
            
            
            if (Strings.InStr(1, j, "\\@Sens. Zscan: V", CompareMethod.Text) != 0) {
                senszscan = Conversion.Val(Strings.Replace(j, "\\@Sens. Zscan: V", "", , , CompareMethod.Text));
            }
            if (Strings.InStr(1, j, "\\@4:Ramp size Zsweep:", CompareMethod.Text) != 0) {
                j = Strings.Trim(Strings.Replace(j, "\\@4:Ramp size Zsweep:", "", , , CompareMethod.Text));
                parts = Strings.Split(j, " ");
                found = false;
                for (I = Information.UBound(parts); I >= 0; I += -1) {
                    jj = Strings.Replace(parts(I), "(", "");
                    if (Conversion.Val(jj) != 0) {
                        ramp = Conversion.Val(jj);
                        if (found) break; // TODO: might not be correct. Was : Exit For
 
                        found = true;
                    }
                }
                
            }
            if (Strings.InStr(1, j, "\\@Sens. Deflection: V", CompareMethod.Text) != 0) {
                ZDeflection = Conversion.Val(Strings.Replace(j, "\\@Sens. Deflection: V", "", , , CompareMethod.Text));
            }
            
            
            if (Strings.InStr(1, j, "\\@4:Z scale: V [Sens.", CompareMethod.Text) != 0) {
                j = Strings.Trim(Strings.Replace(j, "\\@4:Z scale: V [Sens.", "", , , CompareMethod.Text));
                parts = Strings.Split(j, " ");
                found = false;
                for (I = Information.UBound(parts); I >= 0; I += -1) {
                    jj = Strings.Replace(parts(I), "(", "");
                    if (Conversion.Val(jj) != 0) {
                        Zscale = Conversion.Val(jj);
                        if (found) break; // TODO: might not be correct. Was : Exit For
 
                        found = true;
                    }
                }
                
            }
            
        }
        
        
        
        if (static_LoadDI_LX == 0) {
            static_LoadDI_LX = Xoffset;
        }
        if (static_LoadDI_Ly > 0) {
            if (YOffset < 0) {
                static_LoadDI_LX = static_LoadDI_LX + (-38750 + 41250);
            }
        }
        Xoffset = static_LoadDI_LX;
        //If YOffset < 0 Then Stop
        static_LoadDI_Ly = YOffset;
        if (!FoundY) {
            YOffset = YOffset + 1;
            if (YOffset > 100) {
                YOffset = 0;
                Xoffset = Xoffset + 1;
            }
        }
        
        float hscale = 0;
        float zChange = 0;
        
        hscale = senszscan * ramp * Math.Pow(2, 16) / nPoints;
        zChange = Zscale * ZDeflection;
        j = "";
        
        //load data
        //Get #1, dataoffset, c
         // ERROR: Not supported in C#: OnErrorStatement

        cc = 0;
        short s = 0;
        for (I = 0; I <= DataLength; I++) {
            //UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
            FileSystem.FileGet(1, s, DataStart + cc * 2 + 1);
            cc = cc + 1;
            Array.Resize(ref DataArray, cc + 1);
            DataArray(cc) = s;
        }
        
        // nPoints = nPoints / 4
        nPoints2 = nPoints;
         // ERROR: Not supported in C#: OnErrorStatement

        
        
        
        FileSystem.FileClose(1);
        double[,] Data = null;
        double[,] data2 = null;
         // ERROR: Not supported in C#: ReDimStatement

         // ERROR: Not supported in C#: ReDimStatement

        int ccc = 0;
        bool Overload = false;
        Overload = false;
        for (I = 0; I <= nPoints2; I++) {
            
            if (DataArray(nPoints2 - I) > 32700) {
                Overload = true;
            }
            if (!Overload) {
                ccc = ccc + 1;
                Data(0, I) = (nPoints2 - I + 1) * hscale;
                Data(1, I) = DataArray(nPoints2 - I) * zChange;
            }
            
            data2(0, I) = I * hscale;
            data2(1, I) = DataArray(I + nPoints2) * zChange;
        }
        
        Array.Resize(ref Data, 2, ccc - 1);
        
        {
            ForceCurve.AcqX = Xoffset;
            ForceCurve.AcqY = YOffset;
            ForceCurve.aDate = CreateDate;
            ForceCurve.aTime = (string)CreateTime;
            ForceCurve.filename = filename;
            
             // ERROR: Not supported in C#: ReDimStatement

            RootLoader.CurveInfo c1 = new RootLoader.CurveInfo();
            c1.Curve = Data;
            c1.nPoints = Information.UBound(Data, 2);
            RootLoader.CurveInfo c2 = new RootLoader.CurveInfo();
            c2.Curve = data2;
            c2.nPoints = Information.UBound(data2, 2);
            ForceCurve.Sections(0) = c1;
            ForceCurve.Sections(1) = c2;
        }
        
        
        LoadDI = nPoints2;
        return;
        errhandl:
        //MsgBox Err.Description
        FileSystem.FileClose(1);
        return -1;
    }
         
        
        */
    }
}
