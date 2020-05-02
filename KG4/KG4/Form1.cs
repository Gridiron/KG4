using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KG4
{
    public partial class Form1 : Form
    {

        //Пикселей в одном делении оси
        const int PIX_IN_ONE = 10;
        //Длина стрелки
        const int ARR_LEN = 10;

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            int w = pictureBox1.ClientSize.Width / 2;
            int h = pictureBox1.ClientSize.Height / 2;

            
            e.Graphics.TranslateTransform(w, h);
            BresenhamAlgorithm(e.Graphics);
            StepAlgorithm(e.Graphics);
            DDA(e.Graphics);
            BresenhamCircle(e.Graphics);
            DrawXAxis(new Point(-w, 0), new Point(w, 0), e.Graphics);
            DrawYAxis(new Point(0, h), new Point(0, -h), e.Graphics);
            //Центр координат
            e.Graphics.FillEllipse(Brushes.Red, -2, -2, 4, 4);

        }
        private void DrawCircle(Graphics g, int xc, int yc, int x, int y)
        {
            g.FillRectangle(Brushes.Red, (xc + x) * PIX_IN_ONE, -(yc + y+1) * PIX_IN_ONE, PIX_IN_ONE, PIX_IN_ONE);
            g.FillRectangle(Brushes.Red, (xc + x) * PIX_IN_ONE, -(yc - y+1) * PIX_IN_ONE, PIX_IN_ONE, PIX_IN_ONE);
            g.FillRectangle(Brushes.Red, (xc - x) * PIX_IN_ONE, -(yc + y+1) * PIX_IN_ONE, PIX_IN_ONE, PIX_IN_ONE);
            g.FillRectangle(Brushes.Red, (xc - x) * PIX_IN_ONE, -(yc - y+1) * PIX_IN_ONE, PIX_IN_ONE, PIX_IN_ONE);
            g.FillRectangle(Brushes.Red, (xc + y) * PIX_IN_ONE, -(yc + x+1) * PIX_IN_ONE, PIX_IN_ONE, PIX_IN_ONE);
            g.FillRectangle(Brushes.Red, (xc + y) * PIX_IN_ONE, -(yc - x+1) * PIX_IN_ONE, PIX_IN_ONE, PIX_IN_ONE);
            g.FillRectangle(Brushes.Red, (xc - y) * PIX_IN_ONE, -(yc + x+1) * PIX_IN_ONE, PIX_IN_ONE, PIX_IN_ONE);
            g.FillRectangle(Brushes.Red, (xc - y) * PIX_IN_ONE, -(yc - x+1) * PIX_IN_ONE, PIX_IN_ONE, PIX_IN_ONE);
        }
        private void BresenhamCircle(Graphics g)
        {
            int xc = -15;
            int yc = 15;
            int r = 10;
            int x = 0, y = r;
            int d = 3 - 2 * r;
            DrawCircle(g, xc, yc, x, y);
            while (y >= x)
            {
                if (d >= 0)
                {
                    d = d + 4 * (x - y) + 10;
                    y--;
                }
                else
                {
                    d = d + 4 * x + 6;
                }
                x++;
                DrawCircle(g, xc, yc, x, y);
            }
            g.DrawEllipse(Pens.Black, new Rectangle((xc - r)*PIX_IN_ONE, -(yc + r+1) * PIX_IN_ONE, (2*r+1) * PIX_IN_ONE, (2*r+1) * PIX_IN_ONE));
        }
        private void DDA(Graphics g)
        {
            int X0 = 2;
            int Y0 = 2;
            int X1 = 14;
            int Y1 = 16;
            // calculate dx & dy 
            int dx = X1 - X0;
            int dy = Y1 - Y0;

            // calculate steps required for generating pixels 
            int steps = Math.Abs(dx) > Math.Abs(dy) ? Math.Abs(dx) : Math.Abs(dy);

            // calculate increment in x & y for each steps 
            float Xinc = dx / (float)steps;
            float Yinc = dy / (float)steps;

            // Put pixel for each step 
            float X = X0;
            float Y = Y0;
            for (int i = 0; i <= steps; i++)
            {
                g.FillRectangle(Brushes.Green, X * PIX_IN_ONE, -(Y + 1) * PIX_IN_ONE, PIX_IN_ONE, PIX_IN_ONE);
                X += Xinc;           // increment in x at each step 
                Y += Yinc;           // increment in y at each step 
            }

            g.DrawLine(Pens.Black, 2 * PIX_IN_ONE, -2 * PIX_IN_ONE, 14 * PIX_IN_ONE, -16 * PIX_IN_ONE);
        }
        private void BresenhamAlgorithm(Graphics g)
        {
            
            float xs = 3;
            float ys = 21;
            float xe = 31;
            float ye = 30;
            int dx = Math.Abs((int)(xe - xs)), sx = xs < xe ? 1 : -1;
            int dy = Math.Abs((int)(ye - ys)), sy = ys < ye ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2, e2;
            for (; ; )
            {
                g.FillRectangle(Brushes.Yellow, xs * PIX_IN_ONE, -(ys+1) * PIX_IN_ONE, PIX_IN_ONE, PIX_IN_ONE);
                if (xs == xe && ys == ye) break;
                e2 = err;
                if (e2 > -dx) { err -= dy; xs += sx; }
                if (e2 < dy) { err += dx; ys += sy; }
            }

            g.DrawLine(Pens.Black, 3 * PIX_IN_ONE, -21 * PIX_IN_ONE, 31 * PIX_IN_ONE, -30 * PIX_IN_ONE);
        }
        private void StepAlgorithm(Graphics g)
        {

            float xs = -28.3f;
            float ys = -28;
            float xe = -3.7f;
            float ye = -11.7f;
            float k = (ye - ys) / (xe - xs);
            float b = ye - k * xe;
            var minX = xs < xe ? xs : xe;
            var maxX = xs > xe ? xs : xe;
            var minY = ys < ye ? ys : ye;
            var maxY = ys > ye ? ys : ye;
            var xOffset = (int)(minX - (int)minX) * PIX_IN_ONE;
            var yOffset = (int)(minY - (int)minY) * PIX_IN_ONE;
            for (int i=(int)Math.Floor((minX*PIX_IN_ONE+xOffset)/PIX_IN_ONE); i < (int)maxX; i++)
            {
                int yTemp = (int)Math.Round(k * i + b, MidpointRounding.ToEven);
                g.FillRectangle(Brushes.BlueViolet, i * PIX_IN_ONE, -(yTemp+1) * PIX_IN_ONE, PIX_IN_ONE, PIX_IN_ONE);
            }

            g.DrawLine(Pens.Black, xs * PIX_IN_ONE, -ys * PIX_IN_ONE, xe * PIX_IN_ONE, -ye * PIX_IN_ONE);
        }

        #region axis
        //Рисование оси X
        private void DrawXAxis(Point start, Point end, Graphics g)
        {
            //Деления в положительном направлении оси
            for (int i = PIX_IN_ONE; i < end.X - ARR_LEN; i += PIX_IN_ONE)
            {
                g.DrawLine(Pens.Gray, i, start.X, i, end.X);
                DrawText(new Point(i, 5), (i / PIX_IN_ONE).ToString(), g);
            }
            //Деления в отрицательном направлении оси
            for (int i = -PIX_IN_ONE; i > start.X; i -= PIX_IN_ONE)
            {
                g.DrawLine(Pens.Gray, i, start.X, i, end.X);
                DrawText(new Point(i, 5), (i / PIX_IN_ONE).ToString(), g);
            }
            //Ось
            g.DrawLine(Pens.Black, start, end);
            //Стрелка
            g.DrawLines(Pens.Black, GetArrow(start.X, start.Y, end.X, end.Y, ARR_LEN));
        }

        //Рисование оси Y
        private void DrawYAxis(Point start, Point end, Graphics g)
        {
            //Деления в отрицательном направлении оси
            for (int i = PIX_IN_ONE; i < start.Y; i += PIX_IN_ONE)
            {
                g.DrawLine(Pens.Gray, start.Y, i, end.Y, i);
                DrawText(new Point(5, i), (-i / PIX_IN_ONE).ToString(), g, true);
            }
            //Деления в положительном направлении оси
            for (int i = -PIX_IN_ONE; i > end.Y + ARR_LEN; i -= PIX_IN_ONE)
            {
                g.DrawLine(Pens.Gray, start.Y, i, end.Y, i);
                DrawText(new Point(5, i), (-i / PIX_IN_ONE).ToString(), g, true);
            }
            //Ось
            g.DrawLine(Pens.Black, start, end);
            //Стрелка
            g.DrawLines(Pens.Black, GetArrow(start.X, start.Y, end.X, end.Y, ARR_LEN));
        }

        //Рисование текста
        private void DrawText(Point point, string text, Graphics g, bool isYAxis = false)
        {
            var f = new Font(Font.FontFamily, 6);
            var size = g.MeasureString(text, f);
            var pt = isYAxis
                ? new PointF(point.X + 1, point.Y - size.Height / 2)
                : new PointF(point.X - size.Width / 2, point.Y + 1);
            var rect = new RectangleF(pt, size);
            g.DrawString(text, f, Brushes.Black, rect);
        }

        //Вычисление стрелки оси
        private static PointF[] GetArrow(float xe, float ye, float x2, float y2, float len = 10, float width = 4)
        {
            PointF[] result = new PointF[3];
            //направляющий вектор отрезка
            var n = new PointF(x2 - xe, y2 - ye);
            //Длина отрезка
            var l = (float)Math.Sqrt(n.X * n.X + n.Y * n.Y);
            //Единичный вектор
            var v1 = new PointF(n.X / l, n.Y / l);
            //Длина стрелки
            n.X = x2 - v1.X * len;
            n.Y = y2 - v1.Y * len;
            result[0] = new PointF(n.X + v1.Y * width, n.Y - v1.X * width);
            result[1] = new PointF(x2, y2);
            result[2] = new PointF(n.X - v1.Y * width, n.Y + v1.X * width);
            return result;
        }
#endregion
    }
}
