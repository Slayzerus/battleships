using BattleShips.Core.Classes;
using BattleShips.Core.Enums;

namespace BattleShips.Core.Tests
{
    public class AttackTests
    {
        Game game = new Game(new GameConfig());

        [Test]
        public void AttackHitTest()
        {
            Ship ship = game.Board.Ships[0];
            BoardSpace space = ship.Spaces.First();
            AttackResult result = game.Attack(space.Address);
            Assert.IsTrue(result == AttackResult.Hit);
            Assert.IsTrue(game.LastResultText.Contains(space.Address));
        }

        [Test]
        public void AttackMissTest()
        {
            List<string> addresses = GetAllShipAddresses();
            string address = game.Board.Spaces.First(x => !addresses.Contains(x.Address)).Address;
            AttackResult result = game.Attack(address);
            Assert.IsTrue(result == AttackResult.Miss);
            Assert.IsTrue(game.LastResultText.Contains(address));
        }

        [Test]
        public void AttackSunkTest()
        {
            Ship ship = game.Board.Ships[0];
            AttackResult result = AttackResult.Miss;
            string address = string.Empty;

            foreach (BoardSpace space in ship.Spaces)
            {
                address = space.Address;
                result = game.Attack(address);
            }

            Assert.IsTrue(result == AttackResult.Sunk);
            Assert.IsTrue(game.LastResultText.Contains(address));
        }

        [Test]
        public void GameFinishedTest()
        {
            List<string> addresses = GetAllShipAddresses();

            foreach (string address in addresses)
            {
                game.Attack(address);
            }

            Assert.IsTrue(game.IsFinished);
        }

        private List<string> GetAllShipAddresses()
        {
            List<BoardSpace> list = new List<BoardSpace>();

            foreach (Ship ship in game.Board.Ships)
            {
                list.AddRange(ship.Spaces);
            }

            return list.Select(x => x.Address).ToList();
        }
    }
}