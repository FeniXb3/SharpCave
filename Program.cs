Console.WriteLine("Let's play Rock-Paper-Scissors!");

string[] availableSigns = { "rock", "paper", "scissors" };
const string EndGameCommand = "quit";
int firstPlayerPoints = 0;
int secondPlayerPoints = 0;
int expectedRoundNumber = 3;

bool keepPlaying = true;

while (keepPlaying)
{
    for (int roundNumber = 1; roundNumber <= expectedRoundNumber; roundNumber++)
    {
        Console.WriteLine($"  Round {roundNumber}");

        string? firstPlayerSign;
        do
        {
            Console.WriteLine($"Provide sign, first player (or write '{EndGameCommand}' to end game):");
            firstPlayerSign = Console.ReadLine()?.ToLower();
        } while (!availableSigns.Contains(firstPlayerSign) && firstPlayerSign != EndGameCommand);

        if (firstPlayerSign == EndGameCommand)
        {
            keepPlaying = false;
            break;
        }

        string? secondPlayerSign;
        do
        {
            Console.WriteLine($"Provide sign, second player (or write '{EndGameCommand}' to end game):");
            secondPlayerSign = Console.ReadLine()?.ToLower();
        } while (!availableSigns.Contains(secondPlayerSign) && secondPlayerSign != EndGameCommand);

        if (secondPlayerSign == EndGameCommand)
        {
            keepPlaying = false;
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
            firstPlayerPoints += 1;
        }
        else
        {
            Console.WriteLine($"Second player won {secondPlayerSign} beats {firstPlayerSign}!");
            secondPlayerPoints += 1;
        }

        Console.WriteLine($"[Player1] {firstPlayerPoints} : {secondPlayerPoints} [Player2]");
    }

    if (firstPlayerPoints > secondPlayerPoints)
    {
        Console.WriteLine($"== First player crushed second player {firstPlayerPoints} to {secondPlayerPoints}!");
    }
    else if (secondPlayerPoints > firstPlayerPoints)
    {
        Console.WriteLine($"== Second player crushed first player {secondPlayerPoints} to {firstPlayerPoints}!");
    }
    else
    {
        Console.WriteLine($"== It's a total draw {firstPlayerPoints} to {secondPlayerPoints}!");
    }

    firstPlayerPoints = 0;
    secondPlayerPoints = 0;
}

Console.WriteLine("Press Enter to close the game...");
Console.ReadLine();