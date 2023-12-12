using Common;

namespace AdventOfCode2023Solutions.Day02
{
    public class Solution(string[] datasetLines) : IAOCSolution
    {
        public string PuzzleName => "Day 2: Cube Conundrum";
        public string[] DatasetLines => datasetLines;

        public string SolvePart1()
        {
            var games = LoadGames();
            var sumOfGameIDsPassed = games.Where(g => IsGamePossibleForPart1Criteria(g)).Sum(g => g.GameNumber);
            return sumOfGameIDsPassed.ToString();
        }

        public string SolvePart2()
        {
            var games = LoadGames();
            var sumOfPowerOfMinimumCubesPrGame = games.Sum(g => g.CalculatePowerOfMinimumCubes());
            return sumOfPowerOfMinimumCubesPrGame.ToString();
        }

        private List<Game> LoadGames()
        {
            List<Game> games = new();
            foreach (var datasetLine in DatasetLines)
            {
                games.Add(new Game(datasetLine));
            }
            return games;
        }

        internal bool IsGamePossibleForPart1Criteria(Game game)
        {
            return game.RedCubesRevealed <= 12 && game.GreenCubesRevealed <= 13 && game.BlueCubesRevealed <= 14;
        }
    }
}
