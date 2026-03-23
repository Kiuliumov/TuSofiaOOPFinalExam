using System.Drawing;

namespace TUSofiaFinalExam.Shapes
{
    public class RightTriangle : Triangle
    {
        public int BaseLength { get; private set; }
        public int Height { get; private set; }

        public RightTriangle(Point position, Color color, int baseLength, int height)
            : base(position, color)
        {
            BaseLength = baseLength;
            Height = height;
            VertexA = position;
            VertexB = new Point(position.X + BaseLength, position.Y);
            VertexC = new Point(position.X, position.Y + Height);
        }

        public override void Draw(Graphics g)
        {
            using (Brush brush = new SolidBrush(Color))
            {
                g.FillPolygon(brush, new Point[] { VertexA, VertexB, VertexC });
            }

            if (IsSelected)
            {
                using (Pen pen = new Pen(Color.Black, 2))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    g.DrawPolygon(pen, new Point[] { VertexA, VertexB, VertexC });
                }
            }
        }

        public override void Resize(float scale)
        {
            if (scale <= 0)
                throw new ArgumentException("Scale must be positive.");

            BaseLength = (int)(BaseLength * scale);
            Height = (int)(Height * scale);

            VertexB = new Point(VertexA.X + BaseLength, VertexA.Y);
            VertexC = new Point(VertexA.X, VertexA.Y + Height);

            OnResized?.Invoke(this);
        }
    }
}