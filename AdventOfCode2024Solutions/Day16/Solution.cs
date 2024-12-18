using Common;

namespace AdventOfCode2024Solutions.Day16
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 16: Reindeer Maze";


        public static bool WriteDebugInfoToConsole_SolutionsCount { get; set; } = true;
        public static bool WriteDebugInfoToConsole_SolutionsScore { get; set; } = false;
        public static bool WriteDebugInfoToConsole_SolutionsScoreWhenSmaller { get; set; } = true;
        public static bool WriteDebugInfoToConsole_PrintMapEveryStep { get; set; } = false;
        public static bool WriteDebugInfoToConsole_PrintMapEverySolutionWhenSmaller { get; set; } = true;
        public static bool WriteDebugInfoToConsole_PrintMapEverySolution { get; set; } = false;
        public static bool WriteDebugInfoToConsole_PrintMapInitially { get; set; } = false;
        public static bool WriteDebugInfoToConsole_PrintMapComrimized { get; set; } = true;

        public static long solutionsCount = 0;

        public string SolvePart1(string[] datasetLines)
        {
            Map map = new Map(datasetLines);

            if (Solution.WriteDebugInfoToConsole_PrintMapInitially)
                PrintMapToConsole(map.MapTiles, 0);

            var scoreCount = map.StartRace();

            if (WriteDebugInfoToConsole_SolutionsCount)
                Console.WriteLine("Solutions found: {0}", solutionsCount);

            return scoreCount.ToString();
            //return "Implemented - but disabled. Too long runtime.";
        }

        public string SolvePart2(string[] datasetLines)
        {
            return "To be implemented";
        }

        public static void PrintSolutionsCount()
        {
            if (!(solutionsCount % 100000 == 0))
                return;
            Console.WriteLine("Solutions found: {0}", solutionsCount);
        }

        public static void PrintNewSolutionScore(long score)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("(Solution scores: {0})", score);
        }

        public static void PrintMapToConsole(
            char[,] grid,
            long currentScore,
            Position? currentPostion = null,
            List<Position>? positionHisory = null)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            int currentX = currentPostion?.X ?? -1;
            int currentY = currentPostion?.Y ?? -1;

            var mapSpacing = Solution.WriteDebugInfoToConsole_PrintMapComrimized ? "" : " ";

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    var currentTile = grid[i, j];
                    if (positionHisory?.Any(p => p.X == j && p.Y == i) ?? false)
                    {
                        if (i == currentY && j == currentX)
                            Console.ForegroundColor = ConsoleColor.Blue;
                        else
                            Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("X" + mapSpacing);
                    }
                    else
                    {
                        if (currentTile == 'S' || currentTile == 'E')
                            Console.ForegroundColor = ConsoleColor.Magenta;
                        else
                            Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(currentTile + mapSpacing);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("Current score {0}", currentScore);
            Console.WriteLine("");
        }
    }
}
