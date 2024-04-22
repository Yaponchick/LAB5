using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace LAB5.Objects
{
    class RedCircle : BaseObject
    {
        private float size = 20; // Начальный размер круга
        private const float maxSize = 1000; // Максимальный размер круга

        public event Action<RedCircle> OnPlayerOverlap;

        public RedCircle(float x, float y, float angle) : base(x, y, angle)
        {
        }

        public override void Render(Graphics g)
        {
            // Создаем красный цвет с измененным альфа-каналом (например, 50, чтобы сделать его немного прозрачным)
            Color redWithTransparency = Color.FromArgb(90, Color.Crimson); 
            
            // Отрисовываем круг с измененным цветом
            g.FillEllipse(new SolidBrush(redWithTransparency), -size / 2, -size / 2, size, size);
            g.DrawEllipse(new Pen(Color.Black, 2), -size / 2, -size / 2, size, size);
        }


        public override GraphicsPath GetGraphicsPath()
        {
            var path = new GraphicsPath();
            path.AddEllipse(new RectangleF(-size / 2, -size / 2, size, size));
            return path;
        }

        public override void Overlap(BaseObject obj)
        {
            base.Overlap(obj);
            if (obj is Player)
            {
                // Генерируем событие пересечения с игроком
                OnPlayerOverlap?.Invoke(this);
            }
        }

        public void IncreaseSize()
        {
            // Увеличиваем размер круга на 2 пикселя
            size += 2;
            // Проверяем, не превышен ли максимальный размер
            if (size > maxSize)
            {
                size = maxSize;
            }
            Console.WriteLine($"Circle size: {size}"); // Отладочный вывод
        }


        // Метод для сброса размера и позиции круга
        public void Reset()
        {
            size = 20; // Возвращаем начальный размер круга
            Random rnd = new Random();
            X = rnd.Next(0, 700);
            Y = rnd.Next(100, 350);
        }
    }
}
