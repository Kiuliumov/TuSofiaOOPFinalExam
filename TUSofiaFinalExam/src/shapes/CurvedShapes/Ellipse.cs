using System;
using System.Drawing;

namespace TUSofiaFinalExam.Shapes
{
    public class Ellipse : Shape
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Ellipse(Point position, Color color, int width, int height)
            : base(position, color)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentException("Width and Height must be positive.");

            Width = width;
            Height = height;
        }

        public override double GetArea()
        {
            return Math.PI * (Width / 2.0) * (Height / 2.0);
        }

        public override void Draw(Graphics g)
        {
            using (Brush brush = new SolidBrush(Color))
            {
                g.FillEllipse(brush, Position.X - Width / 2, Position.Y - Height / 2, Width, Height);
            }

            if (IsSelected)
            {
                using (Pen pen = new Pen(Color.Black, 2))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    g.DrawEllipse(pen, Position.X - Width / 2, Position.Y - Height / 2, Width, Height);
                }
            }
        }

        public override bool Contains(Point point)
        {
            // Уравнение на елипса: ((x-h)/a)^2 + ((y-k)/b)^2 <= 1
            double dx = (point.X - Position.X) / (Width / 2.0);
            double dy = (point.Y - Position.Y) / (Height / 2.0);
            return dx * dx + dy * dy <= 1.0;
        }

        public override Rectangle GetBounds()
        {
            return new Rectangle(Position.X - Width / 2, Position.Y - Height / 2, Width, Height);
        }

        public override void Resize(float scale)
        {
            if (scale <= 0)
                throw new ArgumentException("Scale must be positive.");

            Width = (int)(Width * scale);
            Height = (int)(Height * scale);

            OnResized?.Invoke(this);
        }
    }
}