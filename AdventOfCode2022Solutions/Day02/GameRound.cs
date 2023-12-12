namespace AdventsOfCode2022.Day02RockPaperScissors
{
    /// <summary>
    /// 
    /// Outcome of the round, score:
    ///     Lost:   0
    ///     Draw:   3
    ///     Win:    6
    /// 
    /// Extra score for shape selection:
    ///     Rock:           1       
    ///     Paper:          2
    ///     Scissors:       3
    ///     
    /// Column Left:    Player 1
    /// Column Right:   Player 2
    /// 
    /// </summary>
    public class GameRound
    {
        public string Player1Selection { get; private set; } = string.Empty;
        public string Player2Selection { get; private set; } = string.Empty;
        public ActionEnum Player1Action { get; private set; }
        public ActionEnum Player2Action { get; private set; }
        public int Player1Score { get; private set; }
        public int Player2Score { get; private set; }
        public bool IsPlayer1Winner { get; private set; }
        public bool IsPlayer2Winner { get; private set; }
        public string RoundWinner { get; private set; } = string.Empty;
        public int RoundNumber { get; private set; }

        public GameRound(int roundNumber, ActionEnum player1Action, ActionEnum player2Action)
        {
            RoundNumber = roundNumber;
            Player1Action = player1Action;
            Player2Action = player2Action;
            CalculateScore();
            DetermineWinner();
        }

        private void CalculateScore()
        {
            Player1Score = Player1Action == ActionEnum.Rock ? 1 : Player1Action == ActionEnum.Paper ? 2 : 3;
            Player2Score = Player2Action == ActionEnum.Rock ? 1 : Player2Action == ActionEnum.Paper ? 2 : 3;

            if (Player1Score == Player2Score)
            {
                Player1Score += 3;
                Player2Score += 3;
                return;
            }

            if (Player1Action == ActionEnum.Rock && Player2Action == ActionEnum.Paper)
                IsPlayer1Winner = false;

            if (Player1Action == ActionEnum.Rock && Player2Action == ActionEnum.Scissor)
                IsPlayer1Winner = true;

            if (Player1Action == ActionEnum.Paper && Player2Action == ActionEnum.Rock)
                IsPlayer1Winner = true;

            if (Player1Action == ActionEnum.Paper && Player2Action == ActionEnum.Scissor)
                IsPlayer1Winner = false;

            if (Player1Action == ActionEnum.Scissor && Player2Action == ActionEnum.Rock)
                IsPlayer1Winner = false;

            if (Player1Action == ActionEnum.Scissor && Player2Action == ActionEnum.Paper)
                IsPlayer1Winner = true;

            IsPlayer2Winner = !IsPlayer1Winner;

            if (IsPlayer1Winner)
            {
                Player1Score += 6;
            }
            else
            {
                Player2Score += 6;
            }
        }

        private void DetermineWinner()
        {
            if (Player1Score == Player2Score)
            {
                RoundWinner = "Draw";
                IsPlayer1Winner = false;
                IsPlayer2Winner = false;
            }
            else if (Player1Score > Player2Score)
            {
                RoundWinner = "Player 1";
                IsPlayer1Winner = true;
                IsPlayer2Winner = false;
            }
            else
            {
                RoundWinner = "Player 2";
                IsPlayer1Winner = false;
                IsPlayer2Winner = true;
            }
        }
    }
}
