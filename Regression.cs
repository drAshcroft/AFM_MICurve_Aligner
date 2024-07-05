using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
namespace GroupCurves
{
    public class Regression
    {
        public static  double DoRegresssion(Graphing graph, double[,] points, int polynomialOrder)
        {
            double[] x=new double[points.GetLength(1)] ;
            double[] y = new double[points.GetLength(1)];
            return DoRegresssion(graph, x, y, polynomialOrder);
        }

        private static Matrix GetSolution(double[] x, double[] y, int polynomialOrder, ref bool noSolution)
        {
            // Build the matrix for the least-squares fitting
            double[][] m = Matrix.CreateMatrixData(x.Length, polynomialOrder + 1);
            for (int i = 0; i < x.Length; i++)
            {
                double xi = x[i];
                double[] xrow = m[i];
                xrow[0] = 1d;
                for (int j = 1; j < xrow.Length; j++)
                {
                    xrow[j] = xrow[j - 1] * xi;
                }
            }

            // Find the least-squares solution
            noSolution = false;
            Matrix matrix = new Matrix(m);
            Matrix solution = null;
            try
            {
                solution = matrix.Solve(new Matrix(y, y.Length));
            }
            catch
            {
                noSolution = true;
            }
            return solution;
        }

        public static AFMCurve FlattenBackGround(Graphing graph, ref AFMCurve TestCurve, int polynomialOrder)
        {
            int nPoints = (int)(TestCurve.Curve.GetLength(1) * 0.3);
            int nTestPoints = 7;
            double[,] testSeg = new double[2, nPoints];

            double[] x = new double[nTestPoints];
            double[] y = new double[nTestPoints];

            for (int i = 0; i < nTestPoints; i++) y[i] = double.MinValue;

            int Index = 0;
            for (int i = 0; i < nPoints; i++)
            {
                Index = (int)((double)i / (double)nPoints * nTestPoints);
                if (TestCurve.Curve[1, i] > y[ Index])
                {
                    x[ Index] = TestCurve.Curve[0, i];
                    y[ Index] = TestCurve.Curve[1, i];
                }

            }
            bool noCorrection=false ;
            Matrix solution=GetSolution(x,y,polynomialOrder,ref noCorrection);
          
            //return solution.GetColumnVector(0);

            if (!noCorrection && solution.RowCount != 0)
            {
                // Extract the values (in our case into a polynomial for fast evaluation)
                Polynomial polynomial = new Polynomial(solution.GetColumnVector(0));
                double d = 0;


                double[] Fits = new double[nTestPoints];
                double Max1 = 0;
                int    MaxIndex1 = -1;
                double Max2 = 0;
                int    MaxIndex2 = -1;
                for (int i = 0; i < nTestPoints; i++)
                {
                    Fits[i]=Math.Abs( y[i] -  polynomial.Evaluate(x[i]) );
                    if (Fits[i] > Max1)
                    {
                        Max1 = Fits[i];
                        MaxIndex1 = i;
                    }
                }
                for (int i = 0; i < nTestPoints; i++)
                {
                    
                    if (Fits[i] > Max2 && Fits[i]<Max1 )
                    {
                        Max2 = Fits[i];
                        MaxIndex2 = i;
                    }
                }
                double[] x2 = new double[nTestPoints-2];
                double[] y2 = new double[nTestPoints-2];
                int i2=0;
                for (int i = 0; i < nTestPoints && i2<(nTestPoints-2); i++)
                {
                    if (i != MaxIndex1 && i != MaxIndex2)
                    {
                        x2[i2] = x[i];
                        y2[i2] = y[i];
                        i2++;
                    }
                }

                solution = GetSolution(x2, y2, polynomialOrder, ref noCorrection);
                // remove the background
                polynomial = new Polynomial(solution.GetColumnVector(0));
               
                double[,] OutLine = new double[2, TestCurve.Curve.GetLength(1)];
                double[,] OrigLine = TestCurve.Curve;
                for (int i = 0; i < OrigLine.GetLength(1); i++)
                {
                    OutLine[0, i] = OrigLine[0, i];
                    d = polynomial.Evaluate(OrigLine[0, i]);
                    OutLine[1, i] = OrigLine[1, i] - d;
                }

                if (graph != null)
                {
                    List<Graphing.LineInfo> Lines = new List<Graphing.LineInfo>();
                    Lines.Add(new Graphing.LineInfo(OrigLine, System.Drawing.Color.Blue, "Original"));
                    Lines.Add(new Graphing.LineInfo(testSeg, System.Drawing.Color.Green , "Original"));
                    Lines.Add(new Graphing.LineInfo(OutLine, System.Drawing.Color.Red, "Flattened"));
                    graph.DoPlotData(Lines, "Extension (nm)", "Deflection (V)", "");
                }
                TestCurve.Curve = OutLine;
                return TestCurve;
            }
            else
                return TestCurve;

        }

        public static AFMCurve  LinearregressionFlattenBackGround(Graphing graph,ref AFMCurve TestCurve, int polynomialOrder)
        {
            
            int nPoints = (int)(TestCurve.Curve.GetLength(1)*0.3);
            double[] x = new double[nPoints ];
            double[] y = new double[nPoints ];

            double[,] testSeg = new double[2, nPoints];
            for (int i = 0; i < nPoints ; i++)
            {
                x[i] = TestCurve.Curve[0, i];
                y[i] = TestCurve.Curve[1, i];
                testSeg[0, i] = x[i];
                testSeg[1, i] = y[i];
            }
            // Build the matrix for the least-squares fitting
            double[][] m = Matrix.CreateMatrixData(x.Length, polynomialOrder + 1);
            for (int i = 0; i < x.Length; i++)
            {
                double xi = x[i];
                double[] xrow = m[i];
                xrow[0] = 1d;
                for (int j = 1; j < xrow.Length; j++)
                {
                    xrow[j] = xrow[j - 1] * xi;
                }
            }

            // Find the least-squares solution
            bool noCorrection = false;
            Matrix matrix = new Matrix(m);
            Matrix solution=null;
            try
            {
                solution  = matrix.Solve(new Matrix(y, y.Length));
            }
            catch
            {
                noCorrection=true;
            }

            //return solution.GetColumnVector(0);

            if (!noCorrection && solution.RowCount!=0)
            {
                // Extract the values (in our case into a polynomial for fast evaluation)
                Polynomial polynomial = new Polynomial(solution.GetColumnVector(0));


                // remove the background

                double d = 0;
                double[,] OutLine = new double[2, TestCurve.Curve.GetLength(1)];
                double[,] OrigLine = TestCurve.Curve;
                for (int i = 0; i < OrigLine.GetLength(1); i++)
                {
                    OutLine[0, i] = OrigLine[0, i];
                    d = polynomial.Evaluate(OrigLine[0, i]);
                    OutLine[1, i] = OrigLine[1, i] - d;
                }

                if (graph != null)
                {
                    List<Graphing.LineInfo> Lines = new List<Graphing.LineInfo>();
                    //Lines.Add(new Graphing.LineInfo(OrigLine, System.Drawing.Color.Blue, "Original"));
                    Lines.Add(new Graphing.LineInfo(testSeg , System.Drawing.Color.Blue, "Original"));
                    Lines.Add(new Graphing.LineInfo(OutLine, System.Drawing.Color.Red, "Flattened"));
                    graph.DoPlotData(Lines,"Extension (nm)","Deflection (V)","");
                }
                TestCurve.Curve = OutLine;
                return TestCurve;
            }
            else
                return TestCurve;
        }

        public static  double DoRegresssion(Graphing graph, double[] x, double[] y, int polynomialOrder)
        {
            //double[ ] x = new double[ ] { 1000, 2000, 3000, 4000, 5000, 6000, 7000 };
            //double[ ] y = new double[ ] { -30, -60, -88, -123, -197, -209, -266 };
            //int polynomialOrder = 3;

            // Build the matrix for the least-squares fitting
            double[][] m = Matrix.CreateMatrixData(x.Length, polynomialOrder + 1);
            for (int i = 0; i < x.Length; i++)
            {
                double xi = x[i];
                double[] xrow = m[i];
                xrow[0] = 1d;
                for (int j = 1; j < xrow.Length; j++)
                {
                    xrow[j] = xrow[j - 1] * xi;
                }
            }
            Polynomial polynomial;
            try
            {
                // Find the least-squares solution
                Matrix matrix = new Matrix(m);
                Matrix solution = matrix.Solve(new Matrix(y, y.Length));

               
                // Extract the values (in our case into a polynomial for fast evaluation)
                polynomial  = new Polynomial(solution.GetColumnVector(0));
            }
            catch
            { return 500; }


            // Verify that the polynomial fits with less than 10% error for all given value pairs.
            double sum=0;
            double d=0;
            double[,] Line = new double[2, x.Length];
            double[,] OriginalLine = new double[2, x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                Line[0, i] = x[i];
                OriginalLine[0, i] = x[i];
                Line[1, i] =polynomial.Evaluate(x[i]);
                OriginalLine[1, i] = y[i];
                d= ( y[i]- Line[1,i]);

                sum += d * d;
                
            }

            if (graph != null)
            {
                List<Graphing.LineInfo > Lines=new List<Graphing.LineInfo>();
                Lines.Add(new Graphing.LineInfo(Line,System.Drawing.Color.Red,"Least Squared Fit"));
                Lines.Add(new Graphing.LineInfo(OriginalLine,System.Drawing.Color.Black,"Orig Curve"));
                graph.DoPlotData(Lines,"Extension (nm)","Deflection (V)","" );
            }
            sum =Math.Sqrt( sum) / (double)x.Length ;
            return sum;
        }
    }
}
