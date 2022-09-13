namespace GameEngine
{
    public interface IGameField
    {
        byte [] Field { get; set; }
        int Height { get; set; }
        int Width { get; set; }
        byte GetCell(int x, int y);
        void Initialize();
        IGameField NextGen();
    }
}

