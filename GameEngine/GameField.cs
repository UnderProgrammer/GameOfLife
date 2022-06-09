using System;

namespace GameEngine
{
    public class GameField : IGameField
    {
        public GameField(int width, int height)
        {
            Width = width;
            Height = height;
            Field = new Cell[Width, Height];
            Initialize();
        }
        public int Width { get; set; } = 120;
        public int Height { get; set; } = 30;
        public ICell[,] Field { get; set; }
        Random _rnd = new Random();


        private bool TrueOrFalse()
        {
            if(_rnd.Next(0, 2) == 0)
            {
                return false;
            }
            return true;
        }
        //реализовать метод для инициализации игрового поля случайными значениями (t/f) 

        public void Initialize()
        {
            for(int i = 0; i < Height; i++)
            {
                for(int j = 0; j < Width; j++)
                {
                    var position = new CellPosition { X = j, Y = i };
                    var isalive = TrueOrFalse();
                    Field[j, i] = new Cell(position, isalive, this); 
                }
            }

        }

        public ICell GetCell(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height)
            {
                return new Cell(new CellPosition(x, y), false, null);
            }
            return Field[x, y];
        }
        public IGameField NextGen()
        {
            var nextGen = new GameField(Width, Height);
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    nextGen.Field[x, y] = new Cell(new CellPosition(x, y), GetCell(x, y).IsAliveNextGen, nextGen);
                }
            }
            Field = nextGen.Field;
            return this;
        }
    }
}

