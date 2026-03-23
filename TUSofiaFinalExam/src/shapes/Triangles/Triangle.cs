using System;
using System.Drawing;

namespace TUSofiaFinalExam.Shapes
{
    public abstract class Triangle : Shape
    {
        public Point VertexA { get; protected set; }
        public Point VertexB { get; protected set; }
        public Point VertexC { get; protected set; }

        protected Triangle(Point position, Color color)
            : base(position, color)
        { }

        public override double GetArea()
        {
            return Math.Abs((VertexA.X * (VertexB.Y - VertexC.Y) +
                             VertexB.X * (VertexC.Y - VertexA.Y) +
                             VertexC.X * (VertexA.Y - VertexB.Y)) / 2.0); // Полезна формула от аналитичната геометрия
        }

        public override Rectangle GetBounds()
        {
            int minX = Math.Min(VertexA.X, Math.Min(VertexB.X, VertexC.X));
            int minY = Math.Min(VertexA.Y, Math.Min(VertexB.Y, VertexC.Y));
            int maxX = Math.Max(VertexA.X, Math.Max(VertexB.X, VertexC.X));
            int maxY = Math.Max(VertexA.Y, Math.Max(VertexB.Y, VertexC.Y));

            return new Rectangle(minX, minY, maxX - minX, maxY - minY);
        }

        public override bool Contains(Point point)
        {
            // Barycentric coordinates
            float denominator = ((VertexB.Y - VertexC.Y) * (VertexA.X - VertexC.X) + // https://en.wikipedia.org/wiki/Barycentric_coordinate_system
                                 (VertexC.X - VertexB.X) * (VertexA.Y - VertexC.Y));
            float alpha = ((VertexB.Y - VertexC.Y) * (point.X - VertexC.X) +
                           (VertexC.X - VertexB.X) * (point.Y - VertexC.Y)) / denominator;
            float beta = ((VertexC.Y - VertexA.Y) * (point.X - VertexC.X) +
                          (VertexA.X - VertexC.X) * (point.Y - VertexC.Y)) / denominator;
            float gamma = 1.0f - alpha - beta;

            return alpha >= 0 && beta >= 0 && gamma >= 0;
        }

        public override void Move(int dx, int dy)
        {
            VertexA = new Point(VertexA.X + dx, VertexA.Y + dy);
            VertexB = new Point(VertexB.X + dx, VertexB.Y + dy);
            VertexC = new Point(VertexC.X + dx, VertexC.Y + dy);
            Position = new Point(Position.X + dx, Position.Y + dy);

            OnMoved?.Invoke(this);
        }

        public override abstract void Draw(Graphics g);
        public override abstract void Resize(float scale);
    }
}