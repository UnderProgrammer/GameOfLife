using System;
using System.ComponentModel;

namespace GameEngine
{
    public class GameField : IGameField
    {
        public GameField(int width, int height)
        {
            Width = width;
            Height = height;
            Field = new byte[Height * Width];
            Initialize();
        }
        public int Width { get; set; } = 120;
        public int Height { get; set; } = 30;
        public byte[] Field { get; set; }
        Random _rnd = new Random();


        private bool TrueOrFalse()
        {
            if (_rnd.Next(0, 2) == 0)
            {
                return false;
            }
            return true;
        }
        //реализовать метод для инициализации игрового поля случайными значениями (t/f) 

        private int GetIndex(int x, int y) => y * Width + x;

        public void Initialize()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var i = GetIndex(x, y);
                    Field[i] = (byte)_rnd.Next(0, 2);
                }
            }

        }

        public byte GetCell(int x, int y)
        {
            return Field[GetIndex(x, y)];
        }

        private int CountNeighbors(int i)
        {
            var _n = Field.Length;
            var ni = _n + i;
            var prevStr = ni - Width;
            var nextStr = ni + Width;
            return (byte)(Field[(prevStr - 1) % _n]
                          + Field[prevStr % _n]
                          + Field[(prevStr + 1) % _n]
                          + Field[(ni - 1) % _n]
                          + Field[(ni + 1) % _n]
                          + Field[(nextStr - 1) % _n]
                          + Field[nextStr % _n]
                          + Field[(nextStr + 1) % _n]);
        }
        //Записать живых на следущий ход соседей в Sup и перезаписать в Field, инициализировать метод NextGen
        public IGameField NextGen()
        {
            var Sup = new byte[Height * Width];
            for (byte i = 0; i < Field.Length; i++)
            {
                byte nbors = (byte)CountNeighbors(Field[i]);
                var IsAlive = Field[i] == 1;
                if (nbors == 2 && IsAlive || nbors == 3)
                {
                    Sup[i] = 1;
                }
                else
                {
                    Sup[i] = 0;
                }
            }
            Field = Sup;
            return this;
        }
    }
}

