using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GroupCurves
{
    public partial class SelectableGraph : UserControl
    {
        public class DataPoint
        {
            public double X;
            public double Y;
            public object Data;
            public Color clr=Color.Black ;
            public int Group;
            public void AssignColor(int Group)
            {
                clr = QColors[Group];
                this.Group = Group;
            }
            // Colors used in the pie graphics
            private readonly Color[] QColors =
        { 
            Color.Blue, Color.Red, Color.YellowGreen, Color.SeaGreen, Color.LightBlue,
            Color.Yellow, Color.Purple, Color.Pink, Color.Orange, Color.Firebrick
        };
        }

        public DataPoint [] Datapoints;

        RectangleF OriginalScreenCoords;
        RectangleF ScreenCoords;

        Bitmap BackGround;
        Bitmap SelectBitmap;

        // Colors used in the pie graphics
        private readonly Color[] QColors =
        { 
            Color.Blue, Color.Red, Color.YellowGreen, Color.SeaGreen, Color.LightBlue,
            Color.Yellow, Color.Purple, Color.Pink, Color.Orange, Color.Firebrick
        };

        int NextGroupColor = 0;

        public enum MouseModes
        {
            ZoomMode, SelectMode

        }

        public MouseModes CurrentMouseMode=MouseModes.SelectMode ;

        public SelectableGraph()
        {
            InitializeComponent();
        }

        private void SelectableGraph_Resize(object sender, EventArgs e)
        {
            LX1.Left = (LX0.Left + LX2.Left) / 2;
            LY1.Top = (LY0.Top + LY2.Left) / 2;
            BackGround = null;
            DrawGraph();
        }

#region Coord_Transforms
        private int ConvertXToScreen(double x)
        {
            return (int)( pictureBox1.Width * (x-ScreenCoords.X ) / ScreenCoords.Width   );

        }
        private int ConvertYToScreen(double y)
        {

            return (int)(pictureBox1.Height  *( 1- (y - ScreenCoords.Y) / ScreenCoords.Height) );
        }

        private float  ConvertXToData(int X)
        {

            return (float)( (double)X/(double)pictureBox1.Width * ScreenCoords.Width + ScreenCoords.X);
        }
        private float  ConvertYToData(int Y)
        {

            return (float)( (1-(double)Y/(double)pictureBox1.Height) * ScreenCoords.Height + ScreenCoords.Y);
        }
#endregion       
       
        private void DrawGraph()
        {
            if (Datapoints == null || Datapoints.Length == 0)
                return;
            if (BackGround == null)
                BackGround = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(BackGround);
            g.Clear(Color.White);
            for (int i = 0; i < Datapoints.Length; i++)
            {
                
                int X = ConvertXToScreen( Datapoints[i].X );
                int Y=  ConvertYToScreen( Datapoints[i].Y );
                if ( (X>0 && X< BackGround.Width ) && ( Y>0 && Y<BackGround.Height ) )
                {
                    SolidBrush sb = new SolidBrush(  Datapoints[i].clr );
                    g.FillEllipse(sb, X ,Y, 4, 4);
                }

            }

            pictureBox1.Image =(Image) BackGround.Clone();
            pictureBox1.Invalidate();
        }
        public float minX = float.MaxValue;
        public float MaxX = float.MinValue;
        public float minY = float.MaxValue;
        public float MaxY = float.MinValue;
        public void PlotDots(DataPoint[] Spots)
        {
            minX = float.MaxValue;
            MaxX = float.MinValue;
            minY = float.MaxValue;
            MaxY = float.MinValue;
            for (int i = 0; i < Spots.Length; i++)
            {
                if (Spots[i].X > MaxX) MaxX =(float) Spots[i].X;
                if (Spots[i].X < minX) minX =(float) Spots[i].X;

                if (Spots[i].Y > MaxY) MaxY =(float) Spots[i].Y;
                if (Spots[i].Y < minY) minY =(float) Spots[i].Y;
            }

            if (minX == MaxX)
            {
                minX = -.1f;
                MaxX = .1f;
            }
            if (minY == MaxY)
            {
                minY = -.1f;
                MaxY = .1f;

            }
            //pad the axis
            float lX =(float)( (MaxX - minX)*.05);
            float  lY =(float)(( MaxY - minY)*.05);

            minY -= lY;
            MaxY += lY;
            minX -= lX;
            MaxX += lX;

            OriginalScreenCoords = new RectangleF(minX, minY, MaxX - minX, MaxY - minY);
            ScreenCoords = new RectangleF(minX, minY, MaxX - minX, MaxY - minY);

            Datapoints = Spots;

            LX0.Text  = minX.ToString();
            LY0.Text  = minY.ToString();

            LX2.Text = MaxX.ToString();
            LY2.Text = MaxY.ToString();

            LX1.Text = ((minX + MaxX) / 2).ToString();
            LY1.Text = ((minY + MaxY) / 2).ToString();


            DrawGraph();
                

        }

        private void DoGroupPoints()
        {
            SelectBitmap = new Bitmap(pictureBox1.Image);
            Graphics g = Graphics.FromImage(SelectBitmap);
            g.Clear(Color.White);
            List<byte> types = new List<byte>();
            for (int i = 0; i < SelectLines.Count; i++)
            {
                types.Add((byte)System.Drawing.Drawing2D.PathPointType.Line);
            }
            types[0] = (byte)System.Drawing.Drawing2D.PathPointType.Start;

            SolidBrush sb = new SolidBrush(Color.Black);
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath(SelectLines.ToArray(), types.ToArray());
            g.FillPath(sb, path);

            int MaxGroup = 0;
            for (int i = 0; i < Datapoints.Length; i++)
            {
                if (MaxGroup < Datapoints[i].Group) MaxGroup = Datapoints[i].Group;
            }

            MaxGroup++;

            int BlackC = Color.Black.ToArgb();
            for (int i = 0; i < Datapoints.Length; i++)
            {

                int X = ConvertXToScreen(Datapoints[i].X);
                int Y = ConvertYToScreen(Datapoints[i].Y);
                if ((X > 0 && X < BackGround.Width) && (Y > 0 && Y < BackGround.Height))
                {
                    int PixVal = SelectBitmap.GetPixel(X, Y).ToArgb();
                    if (PixVal == BlackC)
                    {
                        Datapoints[i].clr = QColors[MaxGroup ];
                        Datapoints[i].Group = MaxGroup ;
                    }
                }
            }


            NextGroupColor++;
            DrawGraph();
        }

        #region MouseActions
        Rectangle LastRectangle ;
        Rectangle CurRectangle;
        Point MouseDown;

        List<Point> SelectLines = null;
        private Rectangle CreateRectangle(int sX, int Sy, int eX, int eY)
        {
            int tX, tY;
            if (sX > eX)
            {
                tX = sX;
                sX = eX;
                eX = tX;
            }
            if (Sy > eY)
            {
                tY = Sy;
                Sy = eY;
                eY = tY;
            }
            return new Rectangle(sX, Sy, eX - sX, eY - Sy);

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (CurrentMouseMode == MouseModes.ZoomMode )
            {
                LastRectangle = CreateRectangle(e.X, e.Y, e.X + 2, e.Y + 2);

                MouseDown = new Point(e.X,e.Y);
            }
            else if (CurrentMouseMode == MouseModes.SelectMode)
            {
                if (SelectLines == null)
                {
                    SelectLines = new List<Point>();
                    SelectLines.Add(new Point(e.X, e.Y));
                    SelectLines.Add(new Point(e.X, e.Y));
                    LastRectangle = CreateRectangle(e.X, e.Y, e.X + 2, e.Y + 2);
                }
                else
                {
                    Point p1 = SelectLines[SelectLines.Count - 2];
                    Point p2 = SelectLines[SelectLines.Count - 1];
                    p2.X = e.X;
                    p2.Y = e.Y;
                    SelectLines[SelectLines.Count - 1] = p2;
                    LastRectangle = Rectangle.Union(LastRectangle, CreateRectangle(p1.X, p1.Y, p2.X, p2.Y));

                    SelectLines.Add(new Point(e.X, e.Y));
                }
                
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left )
            {
                 if (CurrentMouseMode == MouseModes.ZoomMode )
                 {

                     Graphics g= Graphics.FromImage(pictureBox1.Image  );

                     g.DrawImage( BackGround , LastRectangle,LastRectangle,GraphicsUnit.Pixel );
                     LastRectangle = CreateRectangle (MouseDown.X, MouseDown.Y,e.X,e.Y);

                     g.DrawRectangle(Pens.Gray,LastRectangle );
                     
                     LastRectangle.Inflate(4,4);
                     pictureBox1.Invalidate();
                     Application.DoEvents();
                 }
                 
            }

            if (CurrentMouseMode == MouseModes.SelectMode && SelectLines != null )
            {
                Graphics g = Graphics.FromImage(pictureBox1.Image);

                g.DrawImage(BackGround, CurRectangle, CurRectangle, GraphicsUnit.Pixel);

                Point p1 = SelectLines[SelectLines.Count - 2];
                Point p2 = SelectLines[SelectLines.Count - 1];
                p2.X = e.X;
                p2.Y = e.Y;
                SelectLines[SelectLines.Count - 1] = p2;
                CurRectangle = Rectangle.Union(LastRectangle, CreateRectangle(p1.X, p1.Y, p2.X, p2.Y));
                for (int i = 1; i < SelectLines.Count; i++)
                {
                    g.DrawLine(Pens.Blue, SelectLines[i-1], SelectLines[i]);
                }
                CurRectangle.Inflate(4, 4);

                pictureBox1.Invalidate();
                Application.DoEvents();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left )
            {
                 if (CurrentMouseMode == MouseModes.ZoomMode )
                 {
                     LastRectangle = CreateRectangle(MouseDown.X, MouseDown.Y, e.X, e.Y);
                     float mX = ConvertXToData( LastRectangle.X );
                     float mY = ConvertYToData( LastRectangle.Y );
                     float eX = ConvertXToData( LastRectangle.Right );
                     float eY = ConvertYToData( LastRectangle.Bottom );
                     ScreenCoords = new RectangleF( mX,eY,eX-mX,mY-eY);
                     DrawGraph();
                 }
                 
            }
            else 
            {
                if (CurrentMouseMode == MouseModes.ZoomMode )
                 {
                    ScreenCoords = new RectangleF ( OriginalScreenCoords.X, OriginalScreenCoords.Y, OriginalScreenCoords.Width,OriginalScreenCoords.Height );
                    DrawGraph();
                 }
                else if (CurrentMouseMode == MouseModes.SelectMode)
                {
                    pictureBox1_MouseDown(sender, e);
                    DoGroupPoints();
                    SelectLines = null;

                }
            }
        }

        

        
        private void SelectableGraph_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DoGroupPoints();       
            }
        }

        #endregion
    }
}
