using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB5.Objects
{
    class Marker : BaseObject
    {
        public Marker(float x, float y, float angle) : base(x, y, angle)
        {

        }
        // Отображает маркер 
        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Red), -3, -3, 6, 6);
            g.DrawEllipse(new Pen(Color.Red, 2), -6, -6, 12, 12);
            g.DrawEllipse(new Pen(Color.Red, 2), -10, -10, 20, 20);
        }
        // Возвращает объект GraphicsPath, представляющий форму маркера
        public override GraphicsPath GetGraphicsPath()
        {
            var patch = base.GetGraphicsPath(); // Используется для создания маркера
            patch.AddEllipse(-3, -3, 6, 6);
            return patch;
        }
    }
}
