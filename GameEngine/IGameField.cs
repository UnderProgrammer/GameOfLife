namespace GameEngine
{
    public interface IGameField
    {
        ICell[,] Field { get; set; }
        int Height { get; set; }
        int Width { get; set; }
        ICell GetCell(int x, int y);
        void Initialize();
        IGameField NextGen();
    }
}

