using System;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GameEngine;

namespace WriteableBitmapRenderer
{
    public partial class MainWindow : Window
    {
        private readonly IGameField _field;
        private readonly IGame _game;
        public int _width = 100;
        public int _heigth = 100;
        private readonly WriteableBitmap _buffer;
        public MainWindow()
        {
            _field = new GameField(_width, _heigth);
            _field.Initialize();
            _buffer = BitmapFactory.New(_width, _heigth);
            _game = new WriteableBitmapGame(_buffer);
            InitializeComponent();
        }

        private void ViewPort_OnLoaded(object sender, RoutedEventArgs e)
        {
            ViewPort.Source = _buffer;
            CompositionTarget.Rendering += Rendering;
        }

        private void Rendering(object? sender, EventArgs e)
        {
            _game.Draw(_field);
            Thread.Sleep(TimeSpan.FromMilliseconds(500));
            _field.NextGen();
        }
    }
    /// <summary>
    /// Посмотреть что такое WriteableBitmap, как отрисовывать изображение с помощью SetPixel, имплементировать метод Draw, 
    /// </summary>
    public class WriteableBitmapGame : IGame
    {
        private readonly WriteableBitmap _buffer;

        public WriteableBitmapGame(WriteableBitmap buffer)
        {
            _buffer = buffer;
        }

        public void Draw(IGameField gameField)
        {
            for (int y = 0; y < _buffer.Height; y++)
            {
                for (int x = 0; x < _buffer.Width; x++)
                {
                    if(gameField.GetCell(x, y) == 1)
                    {
                        _buffer.SetPixel(x, y, Colors.Gray);
                    }
                    else
                    {
                        _buffer.SetPixel(x, y, Colors.Black);
                    }
                   
                }
            }
        }
    }
}

