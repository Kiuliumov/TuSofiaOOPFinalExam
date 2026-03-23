using System;
using System.Drawing;

namespace TUSofiaFinalExam.Shapes
{
    public class IsoscelesTriangle : Triangle
    {
        public int SideLength { get; private set; }

        public IsoscelesTriangle(Point position, Color color, int baseLength, int sideLength)
            : base(position, color, baseLength, (int)Math.Sqrt(sideLength * sideLength - (baseLength / 2.0) * (baseLength / 2.0)))
        {
            SideLength = sideLength;
        }

        public override void Draw(Graphics g)
        {
            Point[] points = new Point[]
            {
                Position, // top vertex
                new Point(Position.X - BaseLength / 2, Position.Y + Height),
                new Point(Position.X + BaseLength / 2, Position.Y + Height)
            };

            using (Brush brush = new SolidBrush(Color))
                g.FillPolygon(brush, points);

            if (IsSelected)
            {
                using (Pen pen = new Pen(Color.Black, 2))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    g.DrawPolygon(pen, points);
                }
            }
        }

        public override bool Contains(Point point)
        {
            Point a = Position;
            Point b = new Point(Position.X - BaseLength / 2, Position.Y + Height);
            Point c = new Point(Position.X + BaseLength / 2, Position.Y + Height);

            float denominator = ((b.Y - c.Y) * (a.X - c.X) + (c.X - b.X) * (a.Y - c.Y));
            float alpha = ((b.Y - c.Y) * (point.X - c.X) + (c.X - b.X) * (point.Y - c.Y)) / denominator;
            float beta = ((c.Y - a.Y) * (point.X - c.X) + (a.X - c.X) * (point.Y - c.Y)) / denominator;
            float gamma = 1.0f - alpha - beta;

            return alpha >= 0 && beta >= 0 && gamma >= 0;
        }

        public override void Resize(float scale)
        {
            if (scale <= 0)
                throw new ArgumentException("Scale must be positive.");

            BaseLength = (int)(BaseLength * scale);
            SideLength = (int)(SideLength * scale);
            Height = (int)Math.Sqrt(SideLength * SideLength - (BaseLength / 2.0) * (BaseLength / 2.0));

            OnResized?.Invoke(this);
        }
    }
}