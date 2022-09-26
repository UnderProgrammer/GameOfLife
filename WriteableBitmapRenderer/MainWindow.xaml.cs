using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private double _fps;
        public MainWindow()
        {
            _field = new GameField(_width, _heigth);
            _field.Initialize();
            var palette = new BitmapPalette(new List<Color> {Colors.Black, Colors.Crimson});
            _buffer = new WriteableBitmap(_width, _heigth, 1, 1, PixelFormats.Indexed8, palette);
            _game = new WriteableBitmapGame(_buffer);
            _stopwatch.Start();
            InitializeComponent();
        }

        private void ViewPort_OnLoaded(object sender, RoutedEventArgs e)
        {
            ViewPort.Source = _buffer;
            CompositionTarget.Rendering += Rendering;
        }


        // todo: convert target frame time value to target fps. Introduce a class field `_fps`
        private void Rendering(object? sender, EventArgs e)
        {
            if (_stopwatch.ElapsedMilliseconds > 50)
            {
                _fps = 1000 / _stopwatch.ElapsedMilliseconds;
                _game.Draw(_field);
                _field.NextGen();
                _stopwatch.Restart();
            }
        }
    }

    public class WriteableBitmapGame : IGame
    {
        private readonly WriteableBitmap _buffer;

        public WriteableBitmapGame(WriteableBitmap buffer)
        {
            _buffer = buffer;
        }

        public void Draw(IGameField gameField)
        {
            _buffer.WritePixels(new Int32Rect(0, 0, _buffer.PixelWidth, _buffer.PixelHeight), gameField.Field, _buffer.PixelWidth, 0);
        }
    }
}


