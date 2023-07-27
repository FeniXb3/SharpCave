Console.WriteLine("Let's play Rock-Paper-Scissors!");

string[] availableSigns = { "rock", "paper", "scissors" };

while (true)
{
    string? firstPlayerSign;
    do
    {
        Console.WriteLine("Provide sign, first player (or write 'quit' to end game):");
        firstPlayerSign = Console.ReadLine();
    } while (!availableSigns.Contains(firstPlayerSign) && firstPlayerSign != "quit");

    if (firstPlayerSign == "quit")
    {
        break;
    }

    string? secondPlayerSign;
    do
    {
        Console.WriteLine("Provide sign, second player (or write 'quit' to end game):");
        secondPlayerSign = Console.ReadLine();
    } while (!availableSigns.Contains(secondPlayerSign) && secondPlayerSign != "quit");

    if (secondPlayerSign == "quit")
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
        Console.WriteLine("First player won!");
    }
    else
    {
        Console.WriteLine("Second player won!");
    }
}

Console.WriteLine("Press Enter to close the game...");
Console.ReadLine();