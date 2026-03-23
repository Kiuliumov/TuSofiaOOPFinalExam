using System;
using System.Drawing;

namespace TUSofiaFinalExam.Shapes
{
    public class SquareShape : RectangleShape
    {
        public SquareShape(Point position, Color color, int sideLength)
            : base(position, color, sideLength, sideLength)
        {
            if (sideLength <= 0)
                throw new ArgumentException("Side length must be positive.");
        }

        public override void Resize(float scale)
        {
            if (scale <= 0)
                throw new ArgumentException("Scale must be positive.");

            int newSide = (int)(Width * scale);
            Width = newSide;
            Height = newSide;

            OnResized?.Invoke(this);
        }
    }
}