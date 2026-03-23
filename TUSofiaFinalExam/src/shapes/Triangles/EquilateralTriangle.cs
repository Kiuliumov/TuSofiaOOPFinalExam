using System;
using System.Drawing;

namespace TUSofiaFinalExam.Shapes
{
    public class EquilateralTriangle : Triangle
    {
        public int SideLength { get; private set; }

        public EquilateralTriangle(Point position, Color color, int sideLength)
            : base(position, color)
        {
            SideLength = sideLength;
            double height = Math.Sqrt(3) / 2 * sideLength;

            VertexA = position;
            VertexB = new Point(position.X - sideLength / 2, position.Y + (int)height);
            VertexC = new Point(position.X + sideLength / 2, position.Y + (int)height);
        }

        public override void Draw(Graphics g)
        {
            using (Brush brush = new SolidBrush(Color))
                g.FillPolygon(brush, new Point[] { VertexA, VertexB, VertexC });

            if (IsSelected)
            {
                using (Pen pen = new Pen(Color.Black, 2))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    g.DrawPolygon(pen, new Point[] { VertexA, VertexB, VertexC });
                }
            }
        }

        public override void Resize(float scale)
        {
            if (scale <= 0)
                throw new ArgumentException("Scale must be positive.");

            SideLength = (int)(SideLength * scale);
            double height = Math.Sqrt(3) / 2 * SideLength;

            VertexB = new Point(VertexA.X - SideLength / 2, VertexA.Y + (int)height);
            VertexC = new Point(VertexA.X + SideLength / 2, VertexA.Y + (int)height);

            OnResized?.Invoke(this);
        }
    }
}