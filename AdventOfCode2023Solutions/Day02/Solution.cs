using Common;

namespace AdventOfCode2023Solutions.Day02
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 2: Cube Conundrum";

        public string SolvePart1(string[] datasetLines)
        {
            var games = LoadGames(datasetLines);
            var sumOfGameIDsPassed = games.Where(g => IsGamePossibleForPart1Criteria(g)).Sum(g => g.GameNumber);
            return sumOfGameIDsPassed.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            var games = LoadGames(datasetLines);
            var sumOfPowerOfMinimumCubesPrGame = games.Sum(g => g.CalculatePowerOfMinimumCubes());
            return sumOfPowerOfMinimumCubesPrGame.ToString();
        }

        private List<Game> LoadGames(string[] datasetLines)
        {
            List<Game> games = new();
            foreach (var datasetLine in datasetLines)
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
