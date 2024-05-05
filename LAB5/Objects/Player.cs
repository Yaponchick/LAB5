using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB5.Objects
{
    class Player : BaseObject
    {
        public Action<Marker> OnMarkerOverlap; // Событие, возникающее при пересечении с маркером
        public float vX, vY; // Скорость игрока по оси X // Скорость игрока по оси Y

        public Player(float x, float y, float angle) : base(x, y, angle)
        {
            //это конструктор
        }
        // Отрисовка игрока    
        public override void Render(Graphics g)
        {
            Color white = Color.White;
            Color blue = Color.FromArgb(0, 56, 168);
            Color red = Color.Red;

            // Кисти для заливки
            SolidBrush whiteBrush = new SolidBrush(white);
            SolidBrush blueBrush = new SolidBrush(blue);
            SolidBrush redBrush = new SolidBrush(red);

            g.FillRectangle(whiteBrush, -15, -15, 30, 10);

            g.FillRectangle(blueBrush, -15, -5, 30, 10);

            g.FillRectangle(redBrush, -15, 5, 30, 10);

            // контур шарика
            g.DrawEllipse(new Pen(Color.Black, 2), -15, -15, 30, 30);

            // Рисуем линию, чтобы указать направление игрока
            g.DrawLine(new Pen(Color.Black, 2), 0, 0, 25, 0);
        }
        // Графический размер игрока
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath(); // Хранение графического пути
            path.AddEllipse(-15, -15, 30, 30);
            return path;
        }
        // Проверка пересечения объекта игрока с другим объектом
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
