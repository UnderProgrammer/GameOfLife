namespace GameEngine
{
    public class Cell : ICell
    {
        private readonly IGameField _field;

        public Cell(CellPosition position, bool isalive, GameField field)
        {
            _field = field;
            Position = position;
            IsAlive = isalive;
        }
        public bool IsAlive { get; set; }

        public bool IsAliveNextGen
        {
            get
            {
                int neal = GetNeighboursCount(); 
                //Если клетка жива и у нее два или три соседа, то она будет жить на следущий ход
                //Если у мертвой клетки ровно три соседа, то клетка оживет.
                if (IsAlive && neal == 2 || neal == 3) //Логическое "И", логическое умножение &&
                {                                      //Логическое "ИЛИ", логическое сложение ||
                    return true;                       //A&&(B||C) == A&&B || A&&C (Аналогично математике)
                }
                return false;
            }
        }

        public CellPosition Position { get; set; }

        private int GetNeighboursCount()
        {
            int x = Position.X;
            int y = Position.Y;
            int c = 0;
            c += _field.GetCell(x- 1,  y- 1).IsAlive ? 1 : 0; //Прочитать про тернарный оператор и зарубить на носу и всем рассказать чтобы блять я усвоил?
            c += _field.GetCell(x, y - 1).IsAlive ? 1 : 0;
            c += _field.GetCell(x + 1, y - 1).IsAlive ? 1 : 0;
            c += _field.GetCell(x - 1, y).IsAlive ? 1 : 0;
            c += _field.GetCell(x + 1, y).IsAlive ? 1 : 0;
            c += _field.GetCell(x - 1, y + 1).IsAlive ? 1 : 0;
            c += _field.GetCell(x, y + 1).IsAlive ? 1 : 0;
            c += _field.GetCell(x + 1, y + 1).IsAlive ? 1 : 0;
            return c;
            //Реализовать метод до конца
        }
    }
}
