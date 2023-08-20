using BattleShips.Core.Enums;

namespace BattleShips.Core.Classes
{
    public class Board
    {
        #region Fields

        private const string _letters = "ABCDEFGHIJKLMNOPRSTUWYZ";

        #endregion Fields

        #region Properties

        public int SizeX { get; set; } = 0;

        public int SizeY { get; set; } = 0;

        public List<BoardSpace> Spaces { get; set; } = new();

        public List<Ship> Ships { get; set; } = new();

        #endregion Properties

        #region Constructors

        public Board(GameConfig config)
        {
            SizeX = config.BoardSizeX;
            SizeY = config.BoardSizeY;

            for (int x = 0; x < config.BoardSizeX; x++)
            {
                for (int y = 0; y < config.BoardSizeY; y++)
                {
                    Spaces.Add(new BoardSpace(ConvertAddress(x, y)));
                }
            }

            foreach (int shipSize in config.ShipSizes)
            {
                Ship ship = GenerateShip(shipSize);
                PlaceShip(ship);
            }
        }

        #endregion Constructors

        #region Methods (Static)

        public static char IntToLetter(int x)
        {
            return _letters[x];
        }

        public static string ConvertAddress(int x, int y)
        {
            return IntToLetter(y) + (x + 1).ToString();
        }

        #endregion Methods (Static)

        #region Methods (Private)

        private Ship GenerateShip(int shipSize)
        {
            Random random = new Random();

            int orientationInt = random.Next(0, 2);
            ShipOrientation orientation = (ShipOrientation)orientationInt;
            int x = orientation == ShipOrientation.Horizontal
                ? random.Next(0, SizeX - shipSize + 1)
                : random.Next(0, SizeX);
            int y = orientation == ShipOrientation.Vertical
                ? random.Next(0, SizeY - shipSize + 1)
                : random.Next(0, SizeY);

            bool locationIsValid = CheckShipLocation(x, y, shipSize, orientation);

            while (!locationIsValid)
            {
                orientationInt = random.Next(0, 1);
                orientation = (ShipOrientation)orientationInt;
                x = orientation == ShipOrientation.Horizontal
                    ? random.Next(0, SizeX - shipSize - 1)
                    : random.Next(0, SizeX - 1);
                y = orientation == ShipOrientation.Vertical
                    ? random.Next(0, SizeY - shipSize - 1)
                    : random.Next(0, SizeY - 1);

                locationIsValid = CheckShipLocation(x, y, shipSize, orientation);
            }

            return new Ship(x, y, shipSize, orientation);
        }

        private bool CheckShipLocation(int startX, int startY, int shipSize, ShipOrientation orientation)
        {
            List<BoardSpace> boardSpaces = GetBoardSpaces(startX, startY, shipSize, orientation, true);

            foreach (BoardSpace space in boardSpaces)
            {
                if (space.Ship != null)
                {
                    return false;
                }
            }

            return true;
        }

        private List<BoardSpace> GetBoardSpaces(Ship ship, bool includeAdjacent = false)
        {
            return GetBoardSpaces(ship.StartX, ship.StartY, ship.Size, ship.Orientation);
        }

        private List<BoardSpace> GetBoardSpaces(int startX, int startY, int shipSize, ShipOrientation orientation, bool includeAdjacent = false)
        {
            List<BoardSpace> boardSpaces = new();
            int maxX = orientation == ShipOrientation.Horizontal ? startX + shipSize - 1 : startX;
            int maxY = orientation == ShipOrientation.Vertical ? startY + shipSize - 1 : startY;

            if (includeAdjacent)
            {
                startX = startX > 0 ? startX - 1 : startX;
                startY = startY > 0 ? startY - 1 : startY;
                maxX = maxX < SizeX ? maxX + 1 : maxX;
                maxY = maxY < SizeY ? maxY + 1 : maxY;
            }

            for (int x = startX; x <= maxX; x++)
            {
                for (int y = startY; y <= maxY; y++)
                {
                    string address = ConvertAddress(x, y);
                    BoardSpace? space = Spaces.FirstOrDefault(s => s.Address == address);
                    if (space != null)
                    {
                        boardSpaces.Add(space);
                    }
                }
            }

            return boardSpaces;
        }

        private void PlaceShip(Ship ship)
        {
            List<BoardSpace> spaces = GetBoardSpaces(ship);

            foreach (BoardSpace space in spaces)
            {
                space.Ship = ship;
            }

            ship.Spaces = spaces;
            Ships.Add(ship);
        }

        #endregion Methods (Private)

        public BoardSpace? this[string address]
        {
            get
            {
                return Spaces.FirstOrDefault(x => x.Address == address);
            }
        }

        public BoardSpace? this[int x, int y]
        {
            get
            {
                string address = ConvertAddress(x, y);
                return this[address];//Spaces.FirstOrDefault(x => x.Address == address);
            }
        }
    }
}
