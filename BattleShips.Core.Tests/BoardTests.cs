using BattleShips.Core.Classes;
using BattleShips.Core.Enums;

namespace BattleShips.Core.Tests
{
    public class BoardTests
    {
        GameConfig config = new GameConfig();

        public Game Game { get; set; }

        [SetUp]
        public void SetUp() 
        {
            Game = new Game(config);
        }

        [Test]
        public void BoardGenerationTest()
        {
            Assert.IsTrue(Game.Board.Spaces.Count == Game.Board.SizeX * Game.Board.SizeY);
            Assert.IsTrue(Game.Board.Spaces.Select(x => x.Address).Distinct().ToList().Count == Game.Board.Spaces.Count);
        }

        [Test]
        public void ShipCountTest()
        {
            Assert.IsTrue(Game.Board.Ships.Count == config.ShipSizes.Length);
        }

        [Test]
        public void ShipLengthTest()
        {
            bool check = true;

            for(int i = 0; i < config.ShipSizes.Length; i++)
            {
                if (Game.Board.Ships[i].Size != config.ShipSizes[i])
                {
                    check = false;
                    break;
                }
            }

            Assert.IsTrue(check);
        }
    }
}
