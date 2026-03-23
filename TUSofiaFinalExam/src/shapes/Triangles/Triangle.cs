using System;
using System.Drawing;
using TUSofiaFinalExam.Shapes;

namespace TUSofiaFinalExam.Shapes
{
    public class TriangleShape : Shape
    {
        public int BaseLength { get; private set; }
        public int Height { get; private set; }

        public TriangleShape(Point position, Color color, int baseLength, int height)
            : base(position, color)
        {
            if (baseLength <= 0 || height <= 0)
                throw new ArgumentException("Base and height must be positive.");

            BaseLength = baseLength;
            Height = height;
        }

        public override double GetArea()
        {
            return 0.5 * BaseLength * Height;
        }

        public override void Draw(Graphics g)
        {
            Point[] points = new Point[]
            {
                Position,
                new Point(Position.X + BaseLength, Position.Y),
                new Point(Position.X, Position.Y + Height)
            };

            using (Brush brush = new SolidBrush(Color))
            {
                g.FillPolygon(brush, points);
            }

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
            // Using barycentric coordinates for point-in-triangle test
            Point a = Position;
            Point b = new Point(Position.X + BaseLength, Position.Y);
            Point c = new Point(Position.X, Position.Y + Height);

            float denominator = ((b.Y - c.Y) * (a.X - c.X) + (c.X - b.X) * (a.Y - c.Y));
            float alpha = ((b.Y - c.Y) * (point.X - c.X) + (c.X - b.X) * (point.Y - c.Y)) / denominator;
            float beta = ((c.Y - a.Y) * (point.X - c.X) + (a.X - c.X) * (point.Y - c.Y)) / denominator;
            float gamma = 1.0f - alpha - beta;

            return alpha >= 0 && beta >= 0 && gamma >= 0;
        }

        public override Rectangle GetBounds()
        {
            return new Rectangle(Position.X, Position.Y, BaseLength, Height);
        }

        public override void Resize(float scale)
        {
            if (scale <= 0)
                throw new ArgumentException("Scale must be positive.");

            BaseLength = (int)(BaseLength * scale);
            Height = (int)(Height * scale);

            OnResized?.Invoke(this);
        }
    }
}