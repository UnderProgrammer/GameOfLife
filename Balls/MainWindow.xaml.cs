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
        private int _posx;
        private int _posy; 
        private int _rad;
        private decimal _randvx; 
        private decimal _randvy;
        private int _randcolr;

        //private int _dy;
        //дз: сделать, чтобы, дойдя до края, шарики меняли своё направление
        //1. найти левую и правую границу шариков
        //2. сравнить границы шариков и 0 или _width
        //дз: сделать генератор шариков
        public MainWindow()
        {
            _buffer = BitmapFactory.New(_width, _heigth);
            InitializeComponent();
        }

        private void GenerateBall()
        {
            for (int i = 0; i < 50; i++)
            {
                _posx = new Random().Next(0, _width);
                _posy = new Random().Next(0, _heigth);
                _rad = new Random().Next(0, 30);
                _randvx = new Random().Next(-3, 3);
                _randvy = new Random().Next(-3, 3);
                _randcolr = new Random().Next(0, 256);
                Ball ball = new Ball(_posx, _posy, _rad, _randvx, _randvy, Color.FromRgb((byte)_randcolr, (byte)_randcolr, (byte)_randcolr), _buffer);
                ball.Move();
            }
        }

        private void ViewPort_OnLoaded(object sender, RoutedEventArgs e)
        {
            ViewPort.Source = _buffer;
            CompositionTarget.Rendering += Rendering;
        }

        private void Rendering(object sender, EventArgs e)
        {
            _buffer.FillRectangle(0, 0, _width, _heigth, Colors.Black);
            GenerateBall();
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
