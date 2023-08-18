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

        List<Player> winningPlayers = new List<Player>();
        int winningPoints = -1;

        foreach (Player currentPlayer in players)
        {
            if (currentPlayer.Points == winningPoints)
            {
                winningPlayers.Add(currentPlayer);
            }
            else if (currentPlayer.Points > winningPoints)
            {
                winningPlayers.Clear();
                winningPlayers.Add(currentPlayer);
                winningPoints = currentPlayer.Points;
            }
        }

        if (winningPlayers.Count == players.Length)
        {
            Console.WriteLine($"== It's a total draw!");
        }
        else
        {
            string names = String.Join(", ", winningPlayers);
            Console.WriteLine($"== {names} crushed others with {winningPoints} points!");
        }
    }

    private void ResetGameData()
    {
        if (players == null)
        {
            return;
        }

        foreach (Player player in players)
        {
            player.Points = 0;
        }
    }

    private bool PlayRound(int roundNumber)
    {
        if (players == null)
        {
            return false;
        }

        Console.WriteLine($"  Round {roundNumber}");

        var signPairs = new List<(string sign, string winningSign)>();

        foreach(Player player in players)
        {
            string sign = player.GetSign(availableSigns, EndGameCommand) ?? string.Empty;
            
            if (sign == EndGameCommand)
            {
                keepPlaying = false;
                return false;
            }

            string winningSign = GetSignWinningWith(sign);
            signPairs.Add((sign, winningSign));
        }

        for (int firstPlayerIndex = 0; firstPlayerIndex < players.Length - 1; firstPlayerIndex++)
        {
            string firstPlayerSign = signPairs[firstPlayerIndex].sign;
            Player firstPlayer = players[firstPlayerIndex];

            for (int secondPlayerIndex = firstPlayerIndex + 1; secondPlayerIndex < players.Length; secondPlayerIndex++)
            {
                var secondPlayerSigns = signPairs[secondPlayerIndex];
                Player secondPlayer = players[secondPlayerIndex];

                if (firstPlayerSign == secondPlayerSigns.sign)
                {
                    Console.WriteLine("It's a draw!");
                }
                else if (firstPlayerSign == secondPlayerSigns.winningSign)
                {
                    DisplayWinningText(firstPlayer.Name, firstPlayerSign, secondPlayerSigns.sign);
                    firstPlayer.Points += 1;
                }
                else
                {
                    DisplayWinningText(secondPlayer.Name, secondPlayerSigns.sign, firstPlayerSign);
                    secondPlayer.Points += 1;
                }
            }
        }

        foreach(Player player in players)
        {
            Console.WriteLine($"[{player.Name}]: {player.Points}");
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