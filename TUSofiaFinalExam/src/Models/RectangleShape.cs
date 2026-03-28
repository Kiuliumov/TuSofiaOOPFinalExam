using System;
using System.Drawing;
using TUSofiaFinalExam.Models;

namespace TUSofiaFinalExam.Models
{
    public class RectangleShape: Shape
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public RectangleShape(Point position, Color color, int width, int height)
            : base(position, color)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentException("Width and Height must be positive.");

            Width = width;
            Height = height;
        }

        public override double GetArea() => Width * Height;

        public override void Draw(Graphics g)
        {
            using (Brush brush = new SolidBrush(Color))
                g.FillRectangle(brush, Position.X, Position.Y, Width, Height);

            if (IsSelected)
            {
                using (Pen pen = new Pen(Color.Black, 2))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    g.DrawRectangle(pen, Position.X, Position.Y, Width, Height);
                }
            }
        }

        public override bool Contains(Point point)
        {
            return new Rectangle(Position.X, Position.Y, Width, Height).Contains(point);
        }

        public override Rectangle GetBounds()
        {
            return new Rectangle(Position.X, Position.Y, Width, Height);
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