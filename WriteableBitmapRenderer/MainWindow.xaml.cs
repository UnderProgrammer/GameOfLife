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
using GameEngine;

namespace WriteableBitmapRenderer
{
    public partial class MainWindow : Window
    {
        private IGameField _field;
        private IGame _game;
        public int _width = 100;
        public int _heigth = 100;
        private WriteableBitmap _buffer;
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
                    if(gameField.GetCell(x, y).IsAlive)
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

