using System;
using System.Drawing;
using TUSofiaFinalExam.Interfaces;

namespace TUSofiaFinalExam.Shapes
{
    /// <summary>
    /// Абстрактен базов клас за всички графични фигури.
    /// Дефинира общи свойства, методи за рисуване, преминаване и проверка за selection.
    /// </summary>
    public abstract class Shape : IMovable, IDrawable
    {
        private Point _position;
        private Color _color;

        /// <summary>
        /// Позиция на фигурата на сцената (координати X и Y).
        /// </summary>
        public Point Position
        {
            get => _position;
            set
            {
                if (value.X < 0 || value.Y < 0)
                    throw new ArgumentException("Position cannot be negative.");
                _position = value;
            }
        }

        /// <summary>
        /// Основен цвят на фигурата.
        /// </summary>
        public Color Color
        {
            get => _color;
            set => _color = value;
        }

        /// <summary>
        /// Определя дали фигурата е селектирана (например за highlight при кликане).
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// Инициализира нова фигура с дадена позиция и цвят.
        /// </summary>
        /// <param name="position">Начална позиция на фигурата.</param>
        /// <param name="color">Цвят на фигурата.</param>
        public Shape(Point position, Color color)
        {
            Position = position;
            Color = color;
        }

        /// <summary>
        /// Изчислява лицето (площта) на фигурата.
        /// </summary>
        /// <returns>Площта на фигурата като double.</returns>
        public abstract double GetArea();

        /// <summary>
        /// Рисува фигурата върху графичен обект.
        /// </summary>
        /// <param name="g">Graphics обект от Windows Forms.</param>
        public abstract void Draw(Graphics g);

        /// <summary>
        /// Проверява дали дадена точка се намира в рамките на фигурата.
        /// Използва се за selection или кликане върху фигурата.
        /// </summary>
        /// <param name="point">Точка, която се проверява.</param>
        /// <returns>True ако точката е върху фигурата, иначе false.</returns>
        public abstract bool Contains(Point point);

        /// <summary>
        /// Връща bounding rectangle на фигурата.
        /// Използва се за selection, collision detection или highlight.
        /// </summary>
        /// <returns>Rectangle, който огражда фигурата.</returns>
        public abstract Rectangle GetBounds();

        /// <summary>
        /// Премества фигурата с даден delta по X и Y.
        /// </summary>
        /// <param name="dx">Преместване по X.</param>
        /// <param name="dy">Преместване по Y.</param>
        public virtual void Move(int dx, int dy)
        {
            Position = new Point(Position.X + dx, Position.Y + dy);
        }
    }
}