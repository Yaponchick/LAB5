using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace LAB5.Objects
{
    class MyCircle : BaseObject
    {
        private int timeLeft; // Переменная для отслеживания времени
        private const int maxTime = 150; // Максимальное время
        public event Action<MyCircle> OnTimeExpired;


        public MyCircle(float x, float y, float angle) : base(x, y, angle)
        {
        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Orange), -10, -10, 40, 40);
            g.DrawEllipse(new Pen(Color.Black, 2), -10, -10, 40, 40);
            // Отображение оставшегося времени
            g.DrawString(
                $"{timeLeft} с",
                new Font("Verdana", 10),
                new SolidBrush(Color.Blue),
                25, 15
            );
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(new Rectangle(-10, -10, 20, 20));
            return path;
        }

        public override void Overlap(BaseObject obj)
        {
            base.Overlap(obj);
            if (obj is Player)
            {
                // Перемещаем объект на новое место
                Random rnd = new Random();
                X = rnd.Next(0, 400);
                Y = rnd.Next(0, 400);
                ResetTimer(); // Перезапуск таймера при касании игроком
            }
        }

        public void Tick()
        {
            timeLeft--;
            if (timeLeft <= 0)
            {
                // Перерождение кругов
                OverlapTick();
            }
        }

        private void OverlapTick()
        {
            Random rnd = new Random();
            X = rnd.Next(0, 700);
            Y = rnd.Next(100, 350);
            ResetTimer(); // Перезапуск таймера
        }
        private void ResetTimer()
        {
            timeLeft = maxTime;
        }

    }
}
