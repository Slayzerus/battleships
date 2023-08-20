using BattleShips.Core;
using BattleShips.Core.Classes;
using BattleShipsConsole;

Game game = new Game(new GameConfig());

bool playAgain = true;

while(playAgain)
{
    while (!game.IsFinished)
    {
        GameUI.DrawBoard(game.Board);

        if (!string.IsNullOrEmpty(game.LastResultText))
        {
            Console.WriteLine(game.LastResultText);
        }

        Console.Write("Insert coordinates to attack: ");
        string? command = Console.ReadLine()?.ToUpper();
        if (command != null)
        {
            game.Attack(command);
        }
    }

    GameUI.DrawBoard(game.Board);
    Console.WriteLine($"Contratulations! You've won! And it only took you {game.AttackCounter} shots");
    Console.WriteLine("Do you want to play again? (y/n)");

    string? playAgainStr = Console.ReadLine();
    while (playAgainStr == null || (playAgainStr.ToUpper() != "Y" && playAgainStr.ToUpper() != "N"))
    {
        Console.WriteLine("Do you want to play again? (y/n)");
        playAgainStr = Console.ReadLine();
    }

    if (playAgainStr != null && playAgainStr.ToUpper() == "Y") 
    {
        playAgain = true;        
        game = new Game(new GameConfig());
    }
    else
    {
        playAgain = false;
    }
}

