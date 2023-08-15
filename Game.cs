class Game
{
    string[] availableSigns = { "rock", "paper", "scissors" };
    const string EndGameCommand = "quit";
    int expectedRoundNumber = 3;
    bool keepPlaying = true;
    bool playingWithOtherHuman;
    Player[]? players;

    public void Run()
    {
        Console.WriteLine("Let's play Rock-Paper-Scissors!");
        int playersAmount = 3;
        players = new Player[playersAmount];

        Console.WriteLine("Do you have anyone to play with? (yes/no)");
        playingWithOtherHuman = (Console.ReadLine()?.ToLower().Trim() == "yes");

        players[0] = new HumanPlayer("First player");

        for (int i = 1; i < playersAmount; i++)
        {
            string name = $"Player {i + 1}";
            players[i] = playingWithOtherHuman
                ? new HumanPlayer(name)
                : new ComputerPlayer(name);
        }


        while (keepPlaying)
        {
            PlayGame();
            DisplayGameSummary();
            ResetGameData();
        }

        Console.WriteLine("Press Enter to close the game...");
        Console.ReadLine();
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
        if (players == null)
        {
            return;
        }

        int winningPlayerIndex = 0;
        bool totalDraw = true;

        for (int i = 1; i < players.Length; i++)
        {
            if (players[i].Points != players[winningPlayerIndex].Points)
            {
                totalDraw = false;
               
                if (players[i].Points > players[winningPlayerIndex].Points)
                {
                    winningPlayerIndex = i; // TODO: Handle ex aequo
                }
            }
        }

        if (totalDraw)
        {
            Console.WriteLine($"== It's a total draw!");
        }
        else
        {
            Console.WriteLine($"== {players[winningPlayerIndex].Name} crushed others with {players[winningPlayerIndex].Points} points!");
        }
    }

    private void ResetGameData()
    {
        if (players == null)
        {
            return;
        }

        for (int i = 0; i < players.Length; i++)
        {
            players[i].Points = 0;
        }
    }

    private bool PlayRound(int roundNumber)
    {
        if (players == null)
        {
            return false;
        }

        Console.WriteLine($"  Round {roundNumber}");

        string[] signs = new string[players.Length];
        string[] winningSigns = new string[players.Length];

        for (int i = 0; i < players.Length; i++)
        {
            signs[i] = players[i].GetSign(availableSigns, EndGameCommand) ?? String.Empty;

            if (signs[i] == EndGameCommand)
            {
                keepPlaying = false;
                return false;
            }

            winningSigns[i] = GetSignWinningWith(signs[i]);
        }

        for (int firstPlayerIndex = 0; firstPlayerIndex < players.Length - 1; firstPlayerIndex++)
        {
            string firstPlayerSign = signs[firstPlayerIndex];
            Player firstPlayer = players[firstPlayerIndex];

            for (int secondPlayerIndex = firstPlayerIndex + 1; secondPlayerIndex < players.Length; secondPlayerIndex++)
            {
                string secondPlayerSign = signs[secondPlayerIndex];
                string winningWithSecondPlayerSign = winningSigns[secondPlayerIndex];
                Player secondPlayer = players[secondPlayerIndex];

                if (firstPlayerSign == secondPlayerSign)
                {
                    Console.WriteLine("It's a draw!");
                }
                else if (firstPlayerSign == winningWithSecondPlayerSign)
                {
                    DisplayWinningText(firstPlayer.Name, firstPlayerSign, secondPlayerSign);
                    firstPlayer.Points += 1;
                }
                else
                {
                    DisplayWinningText(secondPlayer.Name, secondPlayerSign, firstPlayerSign);
                    secondPlayer.Points += 1;
                }
            }
        }

        for (int i = 0; i < players.Length; i++)
        {
            Console.WriteLine($"[{players[i].Name}]: {players[i].Points}");
        }

        return true;
    }


    private string GetSignWinningWith(string? sign)
    {
        int signIndex = Array.IndexOf(availableSigns, sign);
        int winningSignIndex = (signIndex + 1) % availableSigns.Length;
        string winningWithProvidedSign = availableSigns[winningSignIndex];
        return winningWithProvidedSign;
    }

    private static void DisplayWinningText(string playerName, string? winningSign, string? losingSign)
    {
        Console.WriteLine($"{playerName} won: {winningSign} beats {losingSign}!");
    }
}