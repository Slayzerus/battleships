namespace BattleShips.Core.Classes
{
    public class BoardSpace
    {
        public BoardSpace(string address)
        {
            Address = address;
        }

        public string Address { get; set; } = string.Empty;

        public bool IsHit { get; set; } = false;

        public Ship? Ship { get; set; }
    }
}
