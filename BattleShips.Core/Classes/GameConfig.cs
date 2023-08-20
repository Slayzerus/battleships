namespace BattleShips.Core.Classes
{
    public class GameConfig
    {
        /// <summary>
        /// Horizontal size of the game board 
        /// </summary>
        public int BoardSizeX { get; set; } = 10;

        /// <summary>
        /// Vertical size of the game board
        /// </summary>
        public int BoardSizeY { get; set; } = 10;

        /// <summary>
        /// Possible ship sizes
        /// </summary>
        public int[] ShipSizes { get; set; } = { 4, 4, 5 };
    }
}
