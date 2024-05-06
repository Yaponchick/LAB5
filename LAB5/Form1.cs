using LAB5.Objects;

namespace LAB5
{
    public partial class Form1 : Form
    {
        List<BaseObject> objects = new(); // ������ ��������
        Player player; //���� ��� ������
        Marker marker; // ���� ��� �������
        MyCircle myCircle; // ���� ��� ������� �����
        RedCircle redCircle; // ���� ��� �������� �����
        int score = 0; // ���� �����

        public Form1()
        {
            InitializeComponent();

            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);

            // ������� ����
            redCircle = new RedCircle(100, 100, 0);
            redCircle.PlayerOverlap += RedCircle_PlayerOverlap;

            // ������� �� �����������
            player.OnOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] ����� ��������� � {obj}\n" + txtLog.Text;
            };
            // ������� �� ����������� � ��������
            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };
          
            // ��������� ������ � ����
            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);
            myCircle = new MyCircle(pbMain.Width / 2 + 100, pbMain.Height / 2 + 100, 0);

            objects.Add(marker);
            objects.Add(player);
            objects.Add(myCircle);
            objects.Add(new MyCircle(pbMain.Width / 2 + 100, pbMain.Height / 2 + 100, 0));
            objects.Add(redCircle);
        }
        // ���������� ������� ����������� ������ � ������� ������.
        private void RedCircle_PlayerOverlap(RedCircle circle)
        {
            score--; // ��������� ���������� �����
            ScoreTXT.Text = "����: " + score; // ��������� ��������� ���� � ����������� �����
            circle.Reset(); // ���������� ������ � ������� �������� �����
            circle.IncreaseSize(); // ����������� ������ �������� �����
        }

        // ���������� ������� ��������� ��������
        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics; // ��������� ������ Graphics, ������� ������������ ��� ���������. �� ����������� �� ��������� e ������� PaintEventArgs.

            g.Clear(Color.White);

            updatePlayer();

            // ������������� �����������
            foreach (var obj in objects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj);
                    obj.Overlap(player);

                    if (obj is MyCircle)
                    {
                        score++;
                        ScoreTXT.Text = score.ToString("���� : " + score); // ��������� ��������� ���� � ����������� �����
                    }
                }
            }

            // �������� �������
            foreach (var obj in objects)
            {
                // ������������� ������� �������������� ������� ��� ����������� ������� � ������ ��� ��������� � ��������
                g.Transform = obj.GetTransform();
                // �������� ����� Render() ��� ����������� ������� �� ����������� ��������
                obj.Render(g);
            }
        }

        // ��������� ������� � �������� ������ � ������������ � ���������� �������
        private void updatePlayer()
        {
            if (marker != null)
            {
                // ��������� ������� ����� ������������ �� ��� X ������� � ������
                float dx = marker.X - player.X;
                // ��������� ������� ����� ������������ �� ��� Y ������� � ������
                float dy = marker.Y - player.Y;
                // �������� ���������� ����� ������� ����� �������� � �������
                float length = MathF.Sqrt(dx * dx + dy * dy);
                // ����������� ������� dx � dy, ����� �������� ����������� �������� ��� ��������� ��������
                dx /= length;
                dy /= length;

                // ��������� ���������� �������������� �������� ������ � ������ ����������� � �������
                // � ������������� �������� ��� �������� ����������� � �������, ����� ����������� � ������� ���� ����� �������
                player.vX += dx * 0.5f;
                // ������������
                player.vY += dy * 0.5f;

                // ����������� ���� �������� ������ 
                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;
            }

            // ���������� ������,
            // ����� �����, ����� ����� ��������� ������� ��������� ����������� ����������
            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;

            // �������� ������� ������ � ������� ������� ��������
            player.X += player.vX;
            player.Y += player.vY;
        }

        //���������� ������� �������
        private void timer1_Tick(object sender, EventArgs e)
        {
            pbMain.Invalidate();
            redCircle.IncreaseSize();

            // ��������� ����� ��� ������� �����
            foreach (var obj in objects.OfType<MyCircle>())
            {
                obj.Tick();
            }
        }

        // ���������� ������� ����� ���� �� ��������
        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            // ��� ������� �������� ������� �� ����� ���� �� ��� �� ������
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
