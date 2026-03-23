using System;
using System.Drawing;
using TUSofiaFinalExam.src.interfaces;
public abstract class Shape : IMovable, IDrawable
{
    private Point _position;
    private Color _color;

    public Point Position
    {
        get { return _position; }
        set
        {
            if (value.X < 0 || value.Y < 0)
                throw new ArgumentException("Position cannot be negative.");

            _position = value;
        }
    }

    public Color Color
    {
        get { return _color; }
        set { _color = value; }
    }

    public Shape(Point position, Color color)
    {
        Position = position;
        Color = color;
    }

    public abstract double GetArea();

    public abstract void Draw(Graphics g);

    public virtual void Move(int dx, int dy)
    {
        Position = new Point(_position.X + dx, _position.Y + dy);
    }
}