using System;
using System.Drawing;
using TUSofiaFinalExam.Interfaces;

namespace TUSofiaFinalExam.Shapes
{
    public abstract class Shape : IMovable, IDrawable
    {
        private Point _position;
        private Color _color;

        public Point Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public bool IsSelected { get; set; }

        public abstract double GetArea();
        public abstract void Draw(Graphics g);
        public abstract bool Contains(Point point);
        public abstract Rectangle GetBounds();

        public virtual void Move(int dx, int dy)
        {
            Position = new Point(Position.X + dx, Position.Y + dy);
        }
    }
}