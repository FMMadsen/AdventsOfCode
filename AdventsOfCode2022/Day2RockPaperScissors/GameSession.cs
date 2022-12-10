namespace AdventsOfCode2022.Day2RockPaperScissors
{
    public class GameSession
    {
        public int Player1CurrentScore { get; private set; }
        public int Player2CurrentScore { get; private set; }
        public bool isPlayer1Winner { get; private set; }
        public bool isPlayer2Winner { get; private set; }
        public string Winner { get; private set; }
        public bool IsGameFinished { get; private set; }
        public int GameRoundsCompleted { get; private set; }
        public List<GameRound> Rounds { get; private set; }

        public GameSession()
        {
            Player1CurrentScore = 0;
            Player2CurrentScore = 0;
            isPlayer1Winner = false;
            isPlayer2Winner = false;
            Winner = string.Empty;
            IsGameFinished = false;
            GameRoundsCompleted = 0;
            Rounds = new List<GameRound>();
        }

        public void AddRound(GameRound round)
        {
            if (IsGameFinished)
                throw new Exception(
                    "Round already completed, cannot add more rounds to current game"
                );

            GameRoundsCompleted++;
            Rounds.Add(round);
            Player1CurrentScore += round.Player1Score;
            Player2CurrentScore += round.Player2Score;
        }

        public void CompleteGame()
        {
            IsGameFinished = true;
            if (Player1CurrentScore == Player2CurrentScore)
            {
                Winner = "Draw";
            }
            else if (Player1CurrentScore > Player2CurrentScore)
            {
                Winner = "Player 1";
                isPlayer1Winner = true;
                isPlayer1Winner = false;
            }
            else
            {
                Winner = "Player 2";
                isPlayer1Winner = false;
                isPlayer1Winner = true;
            }
        }
    }
}
