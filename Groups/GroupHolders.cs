using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroupCurves
{
    class GroupHolders
    {
    }
    public class AffiliationInfo
    {
        public int GroupNumber;
        public double Tightness;
        public AffiliationInfo(int GroupNumber, double Tightness)
        {
            this.GroupNumber = GroupNumber;
            this.Tightness = Tightness;
        }
    }
    public class CurveInfo
    {
        //public string PrimaryFile;
        public int PrimaryIndex;
        public string PrimaryFilename;
        //public string SecondaryFile;
        public int SecondaryIndex;
        public double LSF;
        public Correlation correlation;
        public CurveInfo(int PrimaryIndex, int SecondaryIndex, Correlation correlation,string Filename)
        {
            //this.PrimaryFile=PrimaryFile ;
            this.PrimaryFilename=Filename;
            this.PrimaryIndex = PrimaryIndex;
            //this.SecondaryFile=SecondaryFile ;
            this.SecondaryIndex = SecondaryIndex;
            this.correlation = correlation;

        }


    }

   

    public class AFMCurve 
    {
        public int X;
        public int Y;
        public string OriginalFilename;
        public double MinY;
        public double MaxY;
        public double MaxX;
        public double MinX;

        public double[,] ApproachCurve;
        public double[,] WidthdrawalCurve;
        public double[,] Curve;
        public int[] XIndexs;
        public double  IndexZero;

        public Dictionary<string, object> ExtraInformation = new Dictionary<string, object>();

        public double GetXFromY(double Y)
        {
            if (Curve == null)
                return 0;
            for (int i = Curve.GetLength(1)-1; i > 0; i--)
                if (Curve[1, i] <= Y) return Curve[0, i];
            return 0;
        }
        public void OffsetCurve(double dx)
        {
            for (int i = 0; i < Curve.GetLength(1); i++)
                Curve[0, i] += dx;
        }

        private bool CurveIndexed = false;
        private  void IndexCurve()
        {
            double MinX=100000;
            double MaxX=-1*MinX ;
            for (int i = 0; i < Curve.GetLength(1); i++)
            {
                if (Curve[0, i] > MaxX) MaxX = Curve[0, i];
                if (Curve[0, i] < MinX) MinX = Curve[0, i];
            }
            IndexZero = MinX;
            int count=(int)Math.Floor (MaxX-MinX );
            XIndexs = new int[count+1];

            for (int i = 0; i < Curve.GetLength(1); i++)
            {
                int d=(int)Math.Floor (Curve[0,i]-MinX );
                XIndexs[d] = i;
            }
            if (XIndexs[0] == 0) XIndexs[0] = Curve.GetLength(2);
            for (int i = 1; i < count; i++)
            {
                if (XIndexs[i] == 0) XIndexs[i] = XIndexs[i - 1];
            }

        }
        public double GetYfromX(double X)
        {
            if (CurveIndexed == false)
            {
                IndexCurve();
                CurveIndexed = true;
            }
            int d = (int)Math.Floor(X-IndexZero );
            if (d <= 0 || d >= XIndexs.Length) return -1;

            int i=XIndexs[d];

            
            double X1=Curve[0,i];

            if (Math.Abs(X1 - X) > 10) return -1;
            if (i >= (Curve.GetLength(1) - 1)) return (Curve[1, i]);
            double Y1=Curve[1,i];
            double X2=Curve[0,i+1];
            double Y2=Curve[1,i+1];

            double u = (X - X1) / (X2 - X1);
            return (Y2 - Y1) * u + Y1;
        }
        public void SetMaxes()
        {
            double minY = 100000;
            double maxY = -1 * minY;
            double minX = minY;
            double maxX = maxY;
            for (int i = 0; i < Curve.GetLength(1); i++)
            {
                double xx =Curve[0,i];
                double yy =Curve[1,i];
                
                if (yy > maxY) maxY = yy;
                if (yy < minY) minY = yy;
                if (xx > maxX) maxX = xx;
                if (xx < MinX) MinX = xx;
            }
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;

        }
    }

    public class Correlation
    {
        public double LeastSquaresFit;
        public double AbsoluteFit;
        public double dLeastSquaresFit;
        public double ChiSquared;
        public double LinearRegression;
        public double WeightedLeastSquarsFit;
        public double CrossCorrelation;
    }

}
