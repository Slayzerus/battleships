using BattleShips.Core.Enums;

namespace BattleShips.Core.Classes
{
    public class Ship
    {
        #region Constructors

        /// <summary>
        /// Creates new instance of the class <see cref="Ship/>
        /// </summary>
        /// <param name="startX">Ship's first space X coordinates</param>
        /// <param name="startY">Ship's first space Y coordinates</param>
        /// <param name="size">Ship size</param>
        /// <param name="orientation">Ship orientation</param>
        public Ship(int startX, int startY, int size, ShipOrientation orientation)
        {
            StartX = startX;
            StartY = startY;
            Size = size;
            Orientation = orientation;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Ship's first space X coordinates
        /// </summary>
        public int StartX { get; set; }

        /// <summary>
        /// Ship's first space Y coordinates
        /// </summary>
        public int StartY { get; set; }

        /// <summary>
        /// Ship size
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Ship orientation
        /// </summary>
        public ShipOrientation Orientation { get; set; }

        /// <summary>
        /// List of spaces the ship occupies
        /// </summary>
        public List<BoardSpace> Spaces { get; set; } = new();

        /// <summary>
        /// Information if ship has been destroyed
        /// </summary>
        public bool IsDestroyed { get; set; }

        #endregion Properties

        #region Methods

        public void CheckIfDestroyed()
        {
            foreach (BoardSpace space in Spaces)
            {
                if (!space.IsHit)
                {
                    IsDestroyed = false;
                    return;
                }
            }

            IsDestroyed = true;
        }

        #endregion Methods
    }
}
