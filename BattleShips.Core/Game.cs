using BattleShips.Core.Classes;
using BattleShips.Core.Enums;

namespace BattleShips.Core
{
    public class Game
    {
        /// <summary>
        /// Creates new instance of the class <see cref="Game"/>
        /// </summary>
        /// <param name="config">Game configuration object</param>
        public Game(GameConfig config)
        {
            Board = new Board(config);
        }

        /// <summary>
        /// Game board object
        /// </summary>
        public Board Board { get; set; }

        /// <summary>
        /// Information if the game is finished
        /// </summary>
        public bool IsFinished { get; set; } = false;

        /// <summary>
        /// Counter for the attacks
        /// </summary>
        public int AttackCounter { get; set; }

        /// <summary>
        /// Last attack's result as text
        /// </summary>
        public string LastResultText { get; set; } = string.Empty;

        /// <summary>
        /// Method for marking shots
        /// </summary>
        /// <param name="address">Board space address</param>
        /// <returns>????</returns>
        public AttackResult Attack(string address)
        {
            BoardSpace? space = Board[address];            

            if (space == null)
            {
                LastResultText = $"{address}: incorrect board address";
                return AttackResult.WrongAddress;
            }

            AttackCounter++;

            space.IsHit = true;
            if (space.Ship == null)
            {
                LastResultText = $"{address}: miss";
                return AttackResult.Miss;
            }

            space.Ship.CheckIfDestroyed();

            if (!space.Ship.IsDestroyed)
            {
                LastResultText = $"{address}: hit";
                return AttackResult.Hit;
            }

            int destroyCnt = 0;

            foreach (Ship ship in Board.Ships)
            {
                if (ship.IsDestroyed)
                {
                    destroyCnt++;
                }
            }

            IsFinished = Board.Ships.Count == destroyCnt;

            LastResultText = $"{address}: sunk";
            return AttackResult.Sunk;
        }
    }
}