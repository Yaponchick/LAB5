using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB5.Objects //Комментарии + Пределать цвета
{
    class Player : BaseObject
    {
        public Action<Marker> OnMarkerOverlap;
        public float vX, vY;

        public Player(float x, float y, float angle) : base(x, y, angle)
        {
            //это конструктор
        }

        public override void Render(Graphics g)
        {
            // Цвета флага России
            Color white = Color.White;
            Color blue = Color.FromArgb(0, 56, 168); // RGB: 0, 56, 168
            Color red = Color.Red;

            // Создаем кисти для заливки в цвета флага России
            SolidBrush whiteBrush = new SolidBrush(white);
            SolidBrush blueBrush = new SolidBrush(blue);
            SolidBrush redBrush = new SolidBrush(red);

            // Заливаем первую треть шарика белым цветом
            g.FillRectangle(whiteBrush, -15, -15, 30, 10);

            // Заливаем вторую треть шарика синим цветом
            g.FillRectangle(blueBrush, -15, -5, 30, 10);

            // Заливаем третью треть шарика красным цветом
            g.FillRectangle(redBrush, -15, 5, 30, 10);

            // Рисуем контур шарика
            g.DrawEllipse(new Pen(Color.Black, 2), -15, -15, 30, 30);

            // Рисуем линию, чтобы указать направление игрока
            g.DrawLine(new Pen(Color.Black, 2), 0, 0, 25, 0);
        }



        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-15, -15, 30, 30);
            return path;
        }

        public override void Overlap(BaseObject obj)
        {
            base.Overlap(obj);
            if(obj is Marker)
            {
                OnMarkerOverlap(obj as Marker);
            }
        }
    }
}
