namespace Day2RockPaperScissors
{
    public class Day2Puzzle
    {
        /*
            Outcome of the round, score:
                Lost:       0
                Draw:       3
                Win:        6

            Shape selection:    Score   Opponent    You
                Rock:           1       A           X
                Paper:          2       B           Y
                Scissors:       3       C           Z

            Column Left:    Player 1:   Opponent
            Column Right:   Player 2:   You
        */

        public static GameSession Solve(string[] datasetLines)
        {
            return CreateGameSession(datasetLines);
        }

        public static GameSession CreateGameSession(string[] datasetLines)
        {
            GameSession gameSession = new GameSession();

            foreach (var line in datasetLines)
            {
                var round = new GameRound(line);
                gameSession.AddRound(round);
                Console.WriteLine(
                    $"Round winner: {round.RoundWinner}. P1 points:{round.Player1Score}. P2 points:{round.Player2Score}"
                );
            }

            gameSession.CompleteGame();
            return gameSession;
        }
    }
}
