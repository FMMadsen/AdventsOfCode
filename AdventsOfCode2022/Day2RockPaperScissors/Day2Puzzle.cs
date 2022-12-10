namespace AdventsOfCode2022.Day2RockPaperScissors
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
    ///     Scissor:        3
    ///     
    /// Column Left:    Player 1
    /// Column Right:   Player 2
    /// 
    /// PART 1
    /// 
    ///     A=Rock
    ///     B=Paper
    ///     C=Scissor
    /// 
    ///     X=Rock
    ///     Y=Paper
    ///     Z=Scissor
    /// 
    /// PART 2
    /// 
    ///     A=Rock
    ///     B=Paper
    ///     C=Scissor
    /// 
    ///     X=You lose (Player 1 wins)
    ///     Y=Draw
    ///     Z=You win (Player 2 win)
    ///     
    ///     Time consumption: 3 hours in total
    /// 
    /// </summary>
    internal class Day2Puzzle
    {
        public static int SolvePart1(string[] datasetLines, bool doPrintOut)
        {
            var game = CreateGameSessionPart1(datasetLines);

            if (doPrintOut)
            {
                Console.WriteLine(" - PART 1 -");
                Console.WriteLine(
                    $"Game is completed after {game.GameRoundsCompleted} rounds. Score player 1= {game.Player1CurrentScore}. Score player 2= {game.Player2CurrentScore}."
                );
                Console.WriteLine($"Game winner is {game.Winner}");
            }

            return game.Player2CurrentScore;
        }

        public static int SolvePart2(string[] datasetLines, bool doPrintOut)
        {
            var game = CreateGameSessionPart2(datasetLines);

            if (doPrintOut)
            {
                Console.WriteLine(" - PART 2 -");
                Console.WriteLine(
                    $"Game is completed after {game.GameRoundsCompleted} rounds. Score player 1= {game.Player1CurrentScore}. Score player 2= {game.Player2CurrentScore}."
                );
                Console.WriteLine($"Game winner is {game.Winner}");
            }

            return game.Player2CurrentScore;
        }

        public static GameSession CreateGameSessionPart1(string[] datasetLines)
        {
            GameSession gameSession = new GameSession();
            int roundNumber = 0;

            foreach (var line in datasetLines)
            {
                roundNumber++;

                if (string.IsNullOrWhiteSpace(line))
                    continue;//Skip empty lines

                var player1Action = GetPlayer1ActionFromFirstColumn(line);
                var player2Action = GetPlayer2ActionFromSecondColumn(line);

                var round = new GameRound(roundNumber, player1Action, player2Action);

                //Console.WriteLine(
                //    $"Round:{round.RoundNumber.ToString().PadLeft(4, '0')} | {line} | {round.Player1Action.Print()} vs {round.Player2Action.Print()} | P1 points:{round.Player1Score} | P2 points:{round.Player2Score} | Round winner: {round.RoundWinner}."
                //);

                gameSession.AddRound(round);
            }

            gameSession.CompleteGame();
            return gameSession;
        }

        public static GameSession CreateGameSessionPart2(string[] datasetLines)
        {
            GameSession gameSession = new GameSession();
            int roundNumber = 0;

            foreach (var line in datasetLines)
            {
                roundNumber++;

                if (string.IsNullOrWhiteSpace(line))
                    continue;//Skip empty lines

                var player1Action = GetPlayer1ActionFromFirstColumn(line);
                var player2Action = DeterminePlayer2ActionFromRoundResult(line);

                var round = new GameRound(roundNumber, player1Action, player2Action);

                //Console.WriteLine(
                //    $"Round:{round.RoundNumber.ToString().PadLeft(4, '0')} | {line} | {round.Player1Action.Print()} vs {round.Player2Action.Print()} | P1 points:{round.Player1Score} | P2 points:{round.Player2Score} | Round winner: {round.RoundWinner}."
                //);

                gameSession.AddRound(round);
            }

            gameSession.CompleteGame();
            return gameSession;
        }

        private static ActionEnum GetPlayer1ActionFromFirstColumn(string inputString)
        {
            var player1Selection = inputString.Substring(0, 1);

            if (!(player1Selection == "A" || player1Selection == "B" || player1Selection == "C"))
                throw new Exception($"Player 1 action shoud be either A, B or C. It was {player1Selection}");

            ActionEnum player1Action = player1Selection == "A" ? ActionEnum.Rock : player1Selection == "B" ? ActionEnum.Paper : ActionEnum.Scissor;
            return player1Action;
        }

        private static ActionEnum GetPlayer2ActionFromSecondColumn(string inputString)
        {
            var player2Selection = inputString.Substring(2, 1);

            if (!(player2Selection == "X" || player2Selection == "Y" || player2Selection == "Z"))
                throw new Exception($"Player 2 action shoud be either X, Y or Z. It was {player2Selection}");

            ActionEnum player2Action = player2Selection == "X" ? ActionEnum.Rock : player2Selection == "Y" ? ActionEnum.Paper : ActionEnum.Scissor;
            return player2Action;
        }

        private static PlayerResultEnum GetPlayerResultFromSecondColumn(string inputString)
        {
            var character = inputString.Substring(2, 1);

            if (!(character == "X" || character == "Y" || character == "Z"))
                throw new Exception($"Player 2 action shoud be either X, Y or Z. It was {character}");

            PlayerResultEnum playerResult = character == "X" ? PlayerResultEnum.Lose : character == "Y" ? PlayerResultEnum.Draw : PlayerResultEnum.Win;
            return playerResult;
        }


        private static ActionEnum DeterminePlayer2ActionFromRoundResult(string inputString)
        {
            var player1Action = GetPlayer1ActionFromFirstColumn(inputString);
            var player2Result = GetPlayerResultFromSecondColumn(inputString);
            ActionEnum player2Action = player1Action;

            if (player2Result == PlayerResultEnum.Win && player1Action == ActionEnum.Rock)
                player2Action = ActionEnum.Paper;

            if (player2Result == PlayerResultEnum.Win && player1Action == ActionEnum.Paper)
                player2Action = ActionEnum.Scissor;

            if (player2Result == PlayerResultEnum.Win && player1Action == ActionEnum.Scissor)
                player2Action = ActionEnum.Rock;

            if (player2Result == PlayerResultEnum.Lose && player1Action == ActionEnum.Rock)
                player2Action = ActionEnum.Scissor;

            if (player2Result == PlayerResultEnum.Lose && player1Action == ActionEnum.Paper)
                player2Action = ActionEnum.Rock;

            if (player2Result == PlayerResultEnum.Lose && player1Action == ActionEnum.Scissor)
                player2Action = ActionEnum.Paper;

            return player2Action;
        }
    }
}
