class Game
{
    readonly string[] availableSigns = { "rock", "paper", "scissors" };
    const string EndGameCommand = "quit";
    private const int DefaultRoundsAmount = 3;
    private const int DefaultPlayersAmount = 2;
    int expectedRoundsAmount;
    bool keepPlaying = true;
    bool playingWithOtherHuman;
    Player[]? players;

    public void Run()
    {
        Console.WriteLine("Let's play Rock-Paper-Scissors!");
        Console.WriteLine("How many players will play?");
        bool result = int.TryParse(Console.ReadLine()?.Trim(), out int playersAmount);
        if (!result)
        {
            Console.WriteLine($"I don't get your math, so it will be {DefaultPlayersAmount} players game.");
            playersAmount = DefaultPlayersAmount;
        }
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
        Console.WriteLine("How many rounds do you want to play?");
        bool result = int.TryParse(Console.ReadLine()?.Trim(), out expectedRoundsAmount);
        if (!result)
        {
            Console.WriteLine($"Ok, so make it {DefaultRoundsAmount} rounds");
            expectedRoundsAmount = DefaultRoundsAmount;
        }
        
        for (int roundNumber = 1; roundNumber <= expectedRoundsAmount; roundNumber++)
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

        List<Player> winningPlayers = new();
        int winningPoints = -1;
        winningPoints = GetWinners(winningPlayers, winningPoints);

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

    private int GetWinners(List<Player> winningPlayers, int winningPoints)
    {
        if (players == null)
        {
            return -1;
        }

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

        return winningPoints;
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

        var signPairs = GetSignPairs();
        if (signPairs == null)
        {
            return false;
        }

        PlayAllDuels(signPairs);
        DisplayRoundPoints();

        return true;
    }

    private void DisplayRoundPoints()
    {
        if (players == null)
        {
            return;
        }
        
        foreach (Player player in players)
        {
            Console.WriteLine($"[{player.Name}]: {player.Points}");
        }
    }

    private void PlayAllDuels(List<(string sign, string winningSign)>? signPairs)
    {
        if (players == null || signPairs == null)
        {
            return;
        }

        for (int firstPlayerIndex = 0; firstPlayerIndex < players.Length - 1; firstPlayerIndex++)
        {
            string firstPlayerSign = signPairs[firstPlayerIndex].sign;
            Player firstPlayer = players[firstPlayerIndex];

            for (int secondPlayerIndex = firstPlayerIndex + 1; secondPlayerIndex < players.Length; secondPlayerIndex++)
            {
                var secondPlayerSigns = signPairs[secondPlayerIndex];
                Player secondPlayer = players[secondPlayerIndex];
                PlayDuel(firstPlayerSign, firstPlayer, secondPlayerSigns, secondPlayer);
            }
        }
    }

    private static void PlayDuel(string firstPlayerSign, Player firstPlayer, (string sign, string winningSign) secondPlayerSigns, Player secondPlayer)
    {
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

    private List<(string sign, string winningSign)>? GetSignPairs()
    {
        if (players == null)
        {
            return null;
        }

        var signPairs = new List<(string sign, string winningSign)>();

        foreach(Player player in players)
        {
            string sign = player.GetSign(availableSigns, EndGameCommand) ?? string.Empty;
            
            if (sign == EndGameCommand)
            {
                keepPlaying = false;
                return null;
            }

            string winningSign = GetSignWinningWith(sign);
            signPairs.Add((sign, winningSign));
        }

        return signPairs;
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