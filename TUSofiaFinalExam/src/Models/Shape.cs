using System;
using System.Drawing;
using TUSofiaFinalExam.Contracts;
using TUSofiaFinalExam.Models;

namespace TUSofiaFinalExam.Models
{
    public abstract class Shape : IMovable, IDrawable
    {
        private Point _position;
        private Color _color;

        public Point Position
        {
            get => _position;
            set
            {
                if (value.X < 0 || value.Y < 0) throw new ArgumentException("Position cannot be negative.");
                _position = value;
            }
        }

       public Color Color
        {
            get => _color;
            set
            {
                if (value == Color.Empty) throw new ArgumentException("Color cannot be empty.");
                
                _color = value;
                OnColorChanged?.Invoke(this);

            }
        }

        public bool IsSelected { get; set; }

        public event Action<Shape> OnSelected;
        public event Action<Shape> OnDeselected;
        public event Action<Shape> OnMoved;
        public event Action<Shape> OnResized;
        public event Action<Shape> OnColorChanged;
        public event Action<Shape> OnClicked;
        public event Action<Shape> OnHold;
        
        public Shape(Point position, Color color)
        {
            Position = position;
            Color = color;
        }

        public abstract double GetArea();
        public abstract void Draw(Graphics g);
        public abstract bool Contains(Point point);
        public abstract Rectangle GetBounds();
        
        public virtual void Move(int dx, int dy)
        {
            Position = new Point(Position.X + dx, Position.Y + dy);
            OnMoved?.Invoke(this);
        }

        public virtual void Resize(float scale)
        {
            if (scale <= 0) throw new ArgumentException("Scale must be positive.");

            Width = (int)(Width * scale);
            Height = (int)(Height * scale);

            OnResized?.Invoke(this);
        }

        public void Select()
        {
            IsSelected = true;
            OnSelected?.Invoke(this);
        }

        public void Deselect()
        {
            IsSelected = false;
            OnDeselected?.Invoke(this);
        }


        public virtual bool Intersect(Shape shape)
            {
                return this.Position.X < shape.Position.X + shape.Width &&
                    this.Position.X + this.Width > shape.Position.X &&
                    this.Position.Y < shape.Position.Y + shape.Height &&
                    this.Position.Y + this.Height > shape.Position.Y;
            }
    }
}