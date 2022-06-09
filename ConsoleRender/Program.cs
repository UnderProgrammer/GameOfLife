using System;
using System.Threading;
using GameEngine;

namespace ConsoleApp12
{
    class Program
    {
        static void Main(string[] args)
        {
            var field = new GameField(60, 29);
            field.Initialize();
            var game = new ConsoleGame();
            while (true)
            {
                game.Draw(field);
                Thread.Sleep(TimeSpan.FromMilliseconds(75));
                field.NextGen();
            }
        }
    }

    class ConsoleGame : IGame
    {
        public void Draw(IGameField gameField)
        {
            Console.CursorVisible = false;
            for (int y = 0; y < gameField.Height; y++)
            {
                for (int x = 0; x < gameField.Width; x++)
                {
                    //Первая итерация цикла: 
                    if (gameField.GetCell(x, y).IsAlive)
                    {
                        Console.Write("O");
                    }
                    else
                    {
                        Console.Write("#");
                    }
                }

                Console.WriteLine();
            }

            Console.SetCursorPosition(0, Console.WindowTop);
        }
    }
}
