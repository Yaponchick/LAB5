/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB5.Objects
{
    class MyRectangle : BaseObject
    {
        // создаем конструктор с тем же набором параметров что и в BaseObject
        // base(x, y, angle) -- вызывает конструктор родительского класса
        public MyRectangle(float x, float y, float angle) : base(x, y, angle)
        {
        }

        // переопределяем Render
        public override void Render(Graphics g)
        {
            // и запихиваем туда код из формы
            g.FillRectangle(new SolidBrush(Color.Yellow), -25, -15, 50, 30); // Фон
            g.DrawRectangle(new Pen(Color.Red, 2), -25, -15, 50, 30); // Рамка
        }
    }
}
*/



using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace LAB5.Objects
{
    class MyRectangle : BaseObject
    {
        public MyRectangle(float x, float y, float angle) : base(x, y, angle)
        {

        }

        public override void Render(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Green), -10, -10, 20, 20);
            g.DrawRectangle(new Pen(Color.Black, 2), -10, -10, 20, 20);
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddRectangle(new Rectangle(-10, -10, 20, 20));
            return path;
        }

        public override void Overlap(BaseObject obj)
        {
            base.Overlap(obj);
            if (obj is Player)
            {
                // Перемещаем объект на новое место
                Random rnd = new Random();
                X = rnd.Next(0, 500); // Предположим, что ширина окна игры 500
                Y = rnd.Next(0, 500); // Предположим, что высота окна игры 500
            }
        }
    }
}
