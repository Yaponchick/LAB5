using LAB5.Objects;

namespace LAB5
{
    public partial class Form1 : Form
    {
        List<BaseObject> objects = new(); // Список объектов
        Player player; //Поле для игрока
        Marker marker; // Поле для маркера
        MyCircle myCircle; // Поле для первого круга
        RedCircle redCircle; // Поле для красного круга
        int score = 0; // Счет очков

        public Form1()
        {
            InitializeComponent();

            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);

            // Красный круг
            redCircle = new RedCircle(100, 100, 0);
            redCircle.PlayerOverlap += RedCircle_PlayerOverlap;

            // Реакция на пересечение
            player.OnOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + txtLog.Text;
            };
            // Реакция на пересечение с маркером
            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };
          
            // добавляем маркер и круг
            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);
            myCircle = new MyCircle(pbMain.Width / 2 + 100, pbMain.Height / 2 + 100, 0);

            objects.Add(marker);
            objects.Add(player);
            objects.Add(myCircle);
            objects.Add(new MyCircle(pbMain.Width / 2 + 100, pbMain.Height / 2 + 100, 0));
            objects.Add(redCircle);
        }
        // Обработчик события пересечения игрока с красным кругом.
        private void RedCircle_PlayerOverlap(RedCircle circle)
        {
            score--; // Уменьшаем количество очков
            ScoreTXT.Text = "Очки: " + score; // Обновляем текстовое поле с количеством очков
            circle.Reset(); // Сбрасываем размер и позицию красного круга
            circle.IncreaseSize(); // Увеличиваем размер красного круга
        }

        // Обработчик события отрисовки элемента
        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics; // Создается объект Graphics, который используется для рисования. Он извлекается из аргумента e объекта PaintEventArgs.

            g.Clear(Color.White);

            updatePlayer();

            // пересчитываем пересечения
            foreach (var obj in objects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj);
                    obj.Overlap(player);

                    if (obj is MyCircle)
                    {
                        score++;
                        ScoreTXT.Text = score.ToString("Очки : " + score); // Обновляем текстовое поле с количеством очков
                    }
                }
            }

            // рендерим объекты
            foreach (var obj in objects)
            {
                // Устанавливаем текущее преобразование графики для отображения объекта с учетом его положения и поворота
                g.Transform = obj.GetTransform();
                // Вызываем метод Render() для отображения объекта на графическом элементе
                obj.Render(g);
            }
        }

        // Обновляет позицию и скорость игрока в соответствии с положением маркера
        private void updatePlayer()
        {
            if (marker != null)
            {
                // Вычисляет разницу между координатами по оси X маркера и игрока
                float dx = marker.X - player.X;
                // Вычисляет разницу между координатами по оси Y маркера и игрока
                float dy = marker.Y - player.Y;
                // Содержит вычисление длины вектора между маркером и игроком
                float length = MathF.Sqrt(dx * dx + dy * dy);
                // Нормализуем векторы dx и dy, чтобы получить направление движения без изменения скорости
                dx /= length;
                dy /= length;

                // Обновляем компоненту горизонтальной скорости игрока с учетом направления к маркеру
                // и устанавливаем скорость как половину направления к маркеру, чтобы приближение к маркеру было более плавным
                player.vX += dx * 0.5f;
                // вертикальной
                player.vY += dy * 0.5f;

                // расчитываем угол поворота игрока 
                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;
            }

            // тормозящий момент,
            // нужен чтобы, когда игрок достигнет маркера произошло постепенное замедление
            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;

            // пересчет позиция игрока с помощью вектора скорости
            player.X += player.vX;
            player.Y += player.vY;
        }

        //Обработчик события таймера
        private void timer1_Tick(object sender, EventArgs e)
        {
            pbMain.Invalidate();
            redCircle.IncreaseSize();

            // Обновляем время для каждого круга
            foreach (var obj in objects.OfType<MyCircle>())
            {
                obj.Tick();
            }
        }

        // Обработчик события клика мыши по элементу
        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            // тут добавил создание маркера по клику если он еще не создан
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker); 
            }

            marker.X = e.X;
            marker.Y = e.Y;
        }
    }
}           
