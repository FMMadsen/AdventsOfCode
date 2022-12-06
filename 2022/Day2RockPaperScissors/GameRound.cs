namespace Day2RockPaperScissors
{
    public enum Action
    {
        Rock,
        Paper,
        Scissors,
    }

    public class GameRound
    {
        public string Player1Selection { get; private set; } = string.Empty;
        public string Player2Selection { get; private set; } = string.Empty;
        public Action Player1Action { get; private set; }
        public Action Player2Action { get; private set; }
        public int Player1Score { get; private set; }
        public int Player2Score { get; private set; }
        public bool isPlayer1Winner { get; private set; }
        public bool isPlayer2Winner { get; private set; }
        public string RoundWinner { get; private set; } = string.Empty;

        public GameRound(string roundString)
        {
            ValidateRoundString(roundString);
            AssignPlayerActions(roundString);
            CalculateScore();
            DetermineWinner();
        }

        private void ValidateRoundString(string roundString)
        {
            if (string.IsNullOrWhiteSpace(roundString))
                throw new Exception("round string is empty");

            if (roundString.Length != 3)
                throw new Exception("round string is not 3 characters (" + roundString + ")");
        }

        private void AssignPlayerActions(string roundString)
        {
            Player1Selection = roundString.Substring(0, 1);
            Player2Selection = roundString.Substring(2, 1);

            if (!(Player1Selection == "A" || Player1Selection == "B" || Player1Selection == "C"))
                throw new Exception(
                    $"Player 1 action shoud be either A, B or C. It was {Player1Selection}"
                );

            if (!(Player2Selection == "X" || Player2Selection == "Y" || Player2Selection == "Z"))
                throw new Exception(
                    $"Player 1 action shoud be either X, Y or Z. It was {Player2Selection}"
                );

            Player1Action =
                Player1Selection == "A"
                    ? Action.Rock
                    : Player1Selection == "B"
                        ? Action.Scissors
                        : Action.Paper;
            Player2Action =
                Player1Selection == "X"
                    ? Action.Rock
                    : Player1Selection == "Y"
                        ? Action.Scissors
                        : Action.Paper;
        }

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
        private void CalculateScore()
        {
            Player1Score =
                Player1Selection == "A"
                    ? 1
                    : Player1Selection == "B"
                        ? 2
                        : 3;
            Player2Score =
                Player2Selection == "X"
                    ? 1
                    : Player2Selection == "Y"
                        ? 2
                        : 3;

            if (Player1Score == Player2Score)
            {
                Player1Score += 3;
                Player2Score += 3;
                return;
            }

            if (Player1Action == Action.Rock && Player2Action == Action.Paper)
                isPlayer1Winner = false;

            if (Player1Action == Action.Rock && Player2Action == Action.Scissors)
                isPlayer1Winner = true;

            if (Player1Action == Action.Paper && Player2Action == Action.Rock)
                isPlayer1Winner = true;

            if (Player1Action == Action.Paper && Player2Action == Action.Scissors)
                isPlayer1Winner = false;

            if (Player1Action == Action.Scissors && Player2Action == Action.Rock)
                isPlayer1Winner = false;

            if (Player1Action == Action.Scissors && Player2Action == Action.Paper)
                isPlayer1Winner = true;

            isPlayer2Winner = !isPlayer1Winner;

            if (isPlayer1Winner)
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
                isPlayer1Winner = false;
                isPlayer2Winner = false;
            }
            else if (Player1Score > Player2Score)
            {
                RoundWinner = "Player 1";
                isPlayer1Winner = true;
                isPlayer2Winner = false;
            }
            else
            {
                RoundWinner = "Player 2";
                isPlayer1Winner = false;
                isPlayer2Winner = true;
            }
        }
    }
}
