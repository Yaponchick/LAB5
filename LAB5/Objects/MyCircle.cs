using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace LAB5.Objects
{
    class MyCircle : BaseObject
    {
        private int timeLeft; // Переменная для отслеживания времени
        private const int maxTime = 450; // Максимальное время
        public event Action<MyCircle> OnTimeExpired; // Событие, которое возникает при истечении времени таймера 


        public MyCircle(float x, float y, float angle) : base(x, y, angle)
        {
        }

        // Отображает круг 
        public override void Render(Graphics g)
        {
            // Вычисляем текущий размер круга
            float circleSize = 50 * ((float)timeLeft / maxTime); // Содержит текущий размер круга

            // Отрисовываем круг
            g.FillEllipse(new SolidBrush(Color.Aqua), -circleSize / 2, -circleSize / 2, circleSize, circleSize);
            g.DrawEllipse(new Pen(Color.Black, 2), -circleSize / 2, -circleSize / 2, circleSize, circleSize);

            // Отображаем оставшееся время
            g.DrawString(
                $"{timeLeft} с",
                new Font("Verdana", 10),
                new SolidBrush(Color.Blue),
                25, 15
            );
        }

        // Метод для получения графического пути, представляющего круг
        public override GraphicsPath GetGraphicsPath()
        {
            var path = new GraphicsPath(); // Используется для создания маркера
            float circleSize = 20 * ((float)timeLeft / maxTime); // Содержит текущий размер круга
            path.AddEllipse(new RectangleF(-circleSize / 2, -circleSize / 2, circleSize, circleSize));
            return path;
        }

        // Обрабатывает пересечение объекта MyCircle с другим объектом
        public override void Overlap(BaseObject obj)
        {
            base.Overlap(obj);
            if (obj is Player)
            {
                // Перемещаем объект на новое место
                Random rnd = new Random(); // Объект для генерации случайных чисел
                X = rnd.Next(0, 400);
                Y = rnd.Next(0, 400);
                ResetTimer(); // Перезапуск таймера при касании игроком
            }
        }

        // Выполняет отсчет времени жизни круга
        public void Tick()
        {
            timeLeft--;
            if (timeLeft <= 0)
            {
                // Перерождение кругов
                OverlapTick();
            }
        }

        // Пересоздание кружка при истечении времени
        private void OverlapTick()
        {
            Random rnd = new Random(); // Объект для генерации случайных чисел
            X = rnd.Next(0, 700);
            Y = rnd.Next(100, 350);
            ResetTimer(); // Перезапуск таймера
        }

        // Рестарт времени
        private void ResetTimer()
        {
            timeLeft = maxTime;
        }

    }
}
