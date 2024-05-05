using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace LAB5.Objects
{
    class RedCircle : BaseObject
    {
        private float size = 20; // Начальный размер круга
        private const float maxSize = 1000; // Максимальный размер круга

        public  Action<RedCircle> PlayerOverlap; // Событие, которое возникает при пересечении с кругом

        public RedCircle(float x, float y, float angle) : base(x, y, angle)
        {
        }

        // Отображает круг 
        public override void Render(Graphics g)
        {
            Color Transparency = Color.FromArgb(90, Color.Crimson); 
            
            // Отрисовываем круг
            g.FillEllipse(new SolidBrush(Transparency), -size / 2, -size / 2, size, size);
            g.DrawEllipse(new Pen(Color.Black, 2), -size / 2, -size / 2, size, size);
        }

        // Метод для получения графического размера, представляющего круг
        public override GraphicsPath GetGraphicsPath()
        {
            var path = new GraphicsPath(); // Графический путь для круга
            path.AddEllipse(new RectangleF(-size / 2, -size / 2, size, size));
            return path;
        }

        // Обрабатывает пересечение объекта RedCircle с другим объектом
        public override void Overlap(BaseObject obj)
        {
            base.Overlap(obj);
            if (obj is Player)
            {
                // Генерируем событие пересечения с игроком
                PlayerOverlap?.Invoke(this);
            }
        }

        // Увеличивает размер красного круга на 2 пикселя и проверяет, не превышен ли максимальный размер
        public void IncreaseSize()
        {
            // Увеличиваем размер круга на 2 пикселя
            size += 2;
            // Проверяем, не превышен ли максимальный размер
            if (size > maxSize)
            {
                size = maxSize;
            }
        }

        // Метод для сброса размера и позиции круга
        public void Reset()
        {
            size = 20; // Возвращаем начальный размер круга
            Random rnd = new Random(); // Объект для генерации случайных чисел
            X = rnd.Next(0, 700);
            Y = rnd.Next(100, 350);
        }
    }
}
