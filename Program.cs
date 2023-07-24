Console.WriteLine("Let's play Rock-Paper-Scissors!");

while (true)
{
    string? firstPlayerSign;
    do
    {
        Console.WriteLine("Provide sign, first player (or write 'quit' to end game):");
        firstPlayerSign = Console.ReadLine();
    } while (firstPlayerSign != "rock" && firstPlayerSign != "paper" && firstPlayerSign != "scissors" && firstPlayerSign != "quit");

    if (firstPlayerSign == "quit")
    {
        break;
    }

    string? secondPlayerSign;
    do
    {
        Console.WriteLine("Provide sign, second player (or write 'quit' to end game):");
        secondPlayerSign = Console.ReadLine();
    } while (secondPlayerSign != "rock" && secondPlayerSign != "paper" && secondPlayerSign != "scissors" && secondPlayerSign != "quit");

    if (secondPlayerSign == "quit")
    {
        break;
    }

    if (firstPlayerSign == secondPlayerSign)
    {
        Console.WriteLine("It's a draw!");
    }
    else if ((firstPlayerSign == "rock" && secondPlayerSign == "scissors")
        || (firstPlayerSign == "paper" && secondPlayerSign == "rock")
        || (firstPlayerSign == "scissors" && secondPlayerSign == "paper"))
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