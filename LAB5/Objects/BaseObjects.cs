using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace LAB5.Objects
{
    class BaseObject
    {
        public float X; // Координата X объекта
        public float Y; // Координата Y объекта
        public float Angle; // Угол поворота объекта
        // Поле для привязки событий
        public Action<BaseObject, BaseObject> OnOverlap; // Событие, которое возникает при пересечении объекта с другим объектом


        // Конструктор
        public BaseObject(float x, float y, float angle)
        {
            X = x;
            Y = y;
            Angle = angle;
        }

        //Получение матрицы трансформации объекта
        public Matrix GetTransform()
        {
            var matrix = new Matrix(); // Создание и инициализация матрицы трансформации для объекта
            matrix.Translate(X, Y); // смещаем ее в пространстве
            matrix.Rotate(Angle);

            return matrix;
        }

        // Виртуальный метод для отрисовки
        public virtual void Render(Graphics g)
        {
            // тут пусто
        }

        // Получение графического размера для объекта
        public virtual GraphicsPath GetGraphicsPath()
        {
            return new GraphicsPath();
        }

       // Проверка пересечения текущего объекта с другим объектом
        public virtual bool Overlaps(BaseObject obj, Graphics g)
        {
            // берем информацию о форме
            var path1 = this.GetGraphicsPath(); // Графический путь текущего объекта
            var path2 = obj.GetGraphicsPath(); // Графический путь объекта, с которым проверяется пересечение

            // применяем к объектам матрицы трансформации
            path1.Transform(this.GetTransform());
            path2.Transform(obj.GetTransform());

            // используем класс Region, который позволяет определить 
            // пересечение объектов в данном графическом контексте
            var region = new Region(path1); // Область, определяющая пересечение двух объектов
            region.Intersect(path2); // пересекаем формы
            return !region.IsEmpty(g); // если полученная форма не пуста то значит было пересечение
        }

        // Вызов события пересечения с другим объектом
        public virtual void Overlap(BaseObject obj)
        {
            if (this.OnOverlap != null) // Если к полю есть привязанные функции
            {
                this.OnOverlap(this, obj); // То привязываем их
            }
        }

    }

}
