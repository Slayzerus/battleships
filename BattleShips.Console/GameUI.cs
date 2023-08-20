using BattleShips.Core.Classes;

namespace BattleShipsConsole
{
    public class GameUI
    {
        /// <summary>
        /// Method for drawing the board
        /// </summary>
        /// <param name="board">Board object</param>
        public static void DrawBoard(Board board)
        {
            Console.Clear();
            for (int y = 0; y < board.SizeY; y++)
            {
                if (y == 0)
                {
                    DrawHeader(board);
                }

                DrawLine(board, y);
            }
        }

        /// <summary>
        /// Method for drawing the board header
        /// </summary>
        /// <param name="board">Board object</param>
        private static void DrawHeader(Board board)
        {
            string firstLine = "  ";

            for (int x = 0; x < board.SizeX; x++)
            {

                firstLine += x + 1 > 9 ? $" {x + 1}  " : $" {x + 1}  ";
            }

            Console.WriteLine(firstLine);
        }

        /// <summary>
        /// Method for drawing single board line
        /// </summary>
        /// <param name="board">Board object</param>
        /// <param name="y">Line number</param>
        private static void DrawLine(Board board, int y)
        {
            string line = $"{Board.IntToLetter(y)}|";

            for (int x = 0; x < board.SizeX; x++)
            {
                string address = Board.ConvertAddress(x, y);
                BoardSpace space = board.Spaces.First(s => s.Address == address);
                line += $" {DetermineSpaceChar(space)} |";
            }

            Console.WriteLine(line);
        }

        /// <summary>
        /// Method for determining space symbol
        /// </summary>
        /// <param name="space">Board space object</param>
        /// <returns>Space symbol</returns>
        private static char DetermineSpaceChar(BoardSpace space)
        {
            if (space.IsHit)
            {
                if (space.Ship != null)
                {
                    if (space.Ship.IsDestroyed)
                    {
                        return '#';
                    }
                    else
                    {
                        return 'X';
                    }
                }
                else
                {
                    return 'O';
                }
            }
            else
            {
                return ' ';
            }
        }
    }
}
