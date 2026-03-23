using System;
using System.Drawing;
using TUSofiaFinalExam.Shapes;

namespace TUSofiaFinalExam.Shapes
{
    public class Circle: Shape
    {
        public int Radius { get; private set; }

        public Circle(Point position, Color color, int radius)
            : base(position, color)
        {
            if (radius <= 0)
                throw new ArgumentException("Radius must be positive.");

            Radius = radius;
        }


        public override double GetArea()
        {
            return Math.PI * Radius * Radius;
        }

        public override void Draw(Graphics g)
        {
            int diameter = Radius * 2;
            using (Brush brush = new SolidBrush(Color))
            {
                g.FillEllipse(brush, Position.X - Radius, Position.Y - Radius, diameter, diameter);
            }

            if (IsSelected)
            {
                using (Pen pen = new Pen(Color.Black, 2))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    g.DrawEllipse(pen, Position.X - Radius, Position.Y - Radius, diameter, diameter);
                }
            }
        }

        public override bool Contains(Point point)
        {
            int dx = point.X - Position.X;
            int dy = point.Y - Position.Y;
            return dx * dx + dy * dy <= Radius * Radius;
        }

        public override Rectangle GetBounds()
        {
            int diameter = Radius * 2;
            return new Rectangle(Position.X - Radius, Position.Y - Radius, diameter, diameter);
        }

        public override void Resize(float scale)
        {
            if (scale <= 0)
                throw new ArgumentException("Scale must be positive.");

            Radius = (int)(Radius * scale);

            OnResized?.Invoke(this);
        }
    }
}