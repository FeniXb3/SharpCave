class Game
{
    string[] availableSigns = { "rock", "paper", "scissors" };
    const string EndGameCommand = "quit";
    int firstPlayerPoints = 0;
    int secondPlayerPoints = 0;
    int expectedRoundNumber = 3;
    bool keepPlaying = true;
    bool playingWithOtherHuman;


    bool PlayRound(int roundNumber)
    {
        Console.WriteLine($"  Round {roundNumber}");

        string? firstPlayerSign = GetPlayerSign("first");

        if (firstPlayerSign == EndGameCommand)
        {
            keepPlaying = false;
            return false;
        }

        string? secondPlayerSign;
        if (playingWithOtherHuman)
        {
            secondPlayerSign = GetPlayerSign("second");

            if (secondPlayerSign == EndGameCommand)
            {
                keepPlaying = false;
                return false;
            }
        }
        else
        {
            secondPlayerSign = GetComputerPlayerSign();
        }

        string winningWithSecondPlayerSign = GetSignWinningWith(secondPlayerSign);

        if (firstPlayerSign == secondPlayerSign)
        {
            Console.WriteLine("It's a draw!");
        }
        else if (firstPlayerSign == winningWithSecondPlayerSign)
        {
            DisplayWinningText("First player", firstPlayerSign, secondPlayerSign);
            firstPlayerPoints += 1;
        }
        else
        {
            DisplayWinningText("Second player", secondPlayerSign, firstPlayerSign);
            secondPlayerPoints += 1;
        }

        Console.WriteLine($"[Player1] {firstPlayerPoints} : {secondPlayerPoints} [Player2]");

        return true;
    }

    private static void DisplayWinningText(string playerName, string? winningSign, string? losingSign)
    {
        Console.WriteLine($"{playerName} won: {winningSign} beats {losingSign}!");
    }

    private string? GetPlayerSign(string playerName)
    {
        string? sign;
        do
        {
            Console.WriteLine($"Provide sign, {playerName} player (or write '{EndGameCommand}' to end game):");
            sign = Console.ReadLine()?.ToLower().Trim();
        } while (!availableSigns.Contains(sign) && sign != EndGameCommand);
        return sign;
    }

    private string GetSignWinningWith(string? sign)
    {
        int signIndex = Array.IndexOf(availableSigns, sign);
        int winningSignIndex = (signIndex + 1) % availableSigns.Length;
        string winningWithProvidedSign = availableSigns[winningSignIndex];
        return winningWithProvidedSign;
    }

    private string GetComputerPlayerSign()
    {
        string? secondPlayerSign;
        Random rng = new Random();
        int randomSignIndex = rng.Next(availableSigns.Length);
        secondPlayerSign = availableSigns[randomSignIndex];
        Console.WriteLine($"Second player provided {secondPlayerSign}");
        return secondPlayerSign;
    }

    public void Run()
    {
        Console.WriteLine("Let's play Rock-Paper-Scissors!");

        Console.WriteLine("Do you have anyone to play with? (yes/no)");
        playingWithOtherHuman = (Console.ReadLine()?.ToLower().Trim() == "yes");

        while (keepPlaying)
        {
            PlayGame();
            DisplayGameSummary();
            ResetGameData();
        }

        Console.WriteLine("Press Enter to close the game...");
        Console.ReadLine();
    }

    private void ResetGameData()
    {
        firstPlayerPoints = 0;
        secondPlayerPoints = 0;
    }

    private void PlayGame()
    {
        for (int roundNumber = 1; roundNumber <= expectedRoundNumber; roundNumber++)
        {
            bool continueGame = PlayRound(roundNumber);
            if (!continueGame)
            {
                break;
            }
        }
    }

    private void DisplayGameSummary()
    {
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
    }
}