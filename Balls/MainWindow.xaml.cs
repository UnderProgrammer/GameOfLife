using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Balls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int _width = 500;
        public int _heigth = 500;
        private readonly WriteableBitmap _buffer;
        private Random _randomSeed = new Random();
        private Ball[] _balls;
        //дз: метод generate должен возвращать массив
        public MainWindow()
        {
            _buffer = BitmapFactory.New(_width, _heigth);
            _balls = GenerateBalls(50);
            InitializeComponent();
        }

        private Ball[] GenerateBalls(int count)
        {
            Ball[] ball = new Ball[count];
            for (int i = 0; i < ball.Length; i++)
            {
                int r = _randomSeed.Next(0, 30);
                int posx = _randomSeed.Next(r, _width - r);
                int posy = _randomSeed.Next(r, _heigth - r);
                decimal vx = _randomSeed.Next(1, 5);
                decimal vy = _randomSeed.Next(1, 5);
                var rd = _randomSeed.Next(0, 256);
                var grn = _randomSeed.Next(0, 256);
                var bl = _randomSeed.Next(0, 256);
                ball[i] = new Ball(posx, posy, r, vx, vy, Color.FromRgb((byte)rd, (byte)grn, (byte)bl), _buffer);
            }

            return ball;
        }

        private void ViewPort_OnLoaded(object sender, RoutedEventArgs e)
        {
            ViewPort.Source = _buffer;
            CompositionTarget.Rendering += Rendering;
        }

        private void Rendering(object sender, EventArgs e)
        {
            _buffer.FillRectangle(0, 0, _width, _heigth, Colors.Black);
            foreach (var ball in _balls)
            {
                ball.Move();
            }
        }
    }

    public class Ball
    {
        private readonly WriteableBitmap _buffer;
        public int X { get; set; }
        public int Y { get; set; }
        public int R { get; set; }
        public decimal Vx { get; set; }
        public decimal Vy { get; set; }
        public Color Color { get; set; }

        public Ball(int x, int y, int r, decimal vx, decimal vy, Color color, WriteableBitmap buffer)
        {
            X = x;
            Y = y;
            R = r;
            Vx = vx;
            Vy = vy;
            Color = color;
            _buffer = buffer;
        }

        public void Draw()
        {
            var x1 = X - R;
            var y1 = Y - R;
            var x2 = X + R;
            var y2 = Y + R;
            _buffer.FillEllipse(x1, y1, x2, y2, Color);
        }

        public void Move()
        {
            Draw();
            X = (int) (X + Vx);
            Y = (int) (Y + Vy);
            Collision();
        }

        private void Collision()
        {
            if (X + R >= _buffer.PixelWidth || X - R <= 0)
            {
                Vx = - Vx;
            }
            if (Y + R >= _buffer.PixelHeight || Y - R <= 0)
            {
                Vy = -Vy;
            }
        }
    }
}
