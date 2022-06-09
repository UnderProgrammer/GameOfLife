namespace GameEngine
{
    public interface ICell
    {
        /// <summary>
        /// Координаты клетки 
        /// </summary>
        public CellPosition Position { get; set; }
        
        /// <summary>
        /// Property (свойство). Жива ли клетка
        /// </summary>
        bool IsAlive { get; set; }

        /// <summary>
        /// Будет ли клетка жива на следущий ход (вычислимое свойство)
        /// </summary>
        bool IsAliveNextGen { get; }
    }
}
