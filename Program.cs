Console.WriteLine("Let's play Rock-Paper-Scissors!");

string[] availableSigns = { "rock", "paper", "scissors" };
const string EndGameCommand = "quit";

while (true)
{
    string? firstPlayerSign;
    do
    {
        Console.WriteLine($"Provide sign, first player (or write '{EndGameCommand}' to end game):");
        firstPlayerSign = Console.ReadLine();
    } while (!availableSigns.Contains(firstPlayerSign) && firstPlayerSign != EndGameCommand);

    if (firstPlayerSign == EndGameCommand)
    {
        break;
    }

    string? secondPlayerSign;
    do
    {
        Console.WriteLine($"Provide sign, second player (or write '{EndGameCommand}' to end game):");
        secondPlayerSign = Console.ReadLine();
    } while (!availableSigns.Contains(secondPlayerSign) && secondPlayerSign != EndGameCommand);

    if (secondPlayerSign == EndGameCommand)
    {
        break;
    }

    int secondPlayerSignIndex = Array.IndexOf(availableSigns, secondPlayerSign);
    int winningWithSecondPlayerSignIndex = (secondPlayerSignIndex + 1) % availableSigns.Length;
    string winningWithSecondPlayerSign = availableSigns[winningWithSecondPlayerSignIndex];

    if (firstPlayerSign == secondPlayerSign)
    {
        Console.WriteLine("It's a draw!");
    }
    else if (firstPlayerSign == winningWithSecondPlayerSign)
    {
        Console.WriteLine($"First player won: {firstPlayerSign} beats {secondPlayerSign}!");
    }
    else
    {
        Console.WriteLine($"Second player won {secondPlayerSign} beats {firstPlayerSign}!");
    }
}

Console.WriteLine("Press Enter to close the game...");
Console.ReadLine();