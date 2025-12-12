using AdventOfCode2024Solutions.Day16.SolutionB;
using AdventOfCode2024Solutions.Day16.SolutionB.Tiles;
using Common;
using System.Diagnostics;
using ToolsFramework;
using ToolsFramework.Map;

namespace AdventOfCode2024Solutions.Day16
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 16: Reindeer Maze";

        public static bool WriteDebugInfoToConsole_SolutionsCount { get; set; } = true;
        public static bool WriteDebugInfoToConsole_SolutionsScore { get; set; } = true;
        public static bool WriteDebugInfoToConsole_SolutionsScoreWhenSmaller { get; set; } = true;
        public static bool WriteDebugInfoToConsole_PrintMapEveryStep { get; set; } = false;
        public static bool WriteDebugInfoToConsole_PrintMapEverySolutionWhenSmaller { get; set; } = false;
        public static bool WriteDebugInfoToConsole_PrintMapEverySolution { get; set; } = false;
        public static bool WriteDebugInfoToConsole_PrintMapInitially { get; set; } = false;
        public static bool WriteDebugInfoToConsole_PrintMapCompressed { get; set; } = true;
        public static bool DebugInfo_TrackHistoryPositions { get; set; } = false;

        private static readonly Stopwatch _stopwatch = new();
        private static readonly Stopwatch _stopwatchRound = new();

        public string SolvePart1(string[] datasetLines)
        {
            var map = new RaindeerMaze(datasetLines);

            ConsolePrinterOfCharMap.PrintMapToConsole(map.OriginalSourceMapTiles);

            Raindeer navigator = new(map);
            _stopwatch.Start();
            _stopwatchRound.Start();
            var routeDistance = navigator.CreateRoute(GenericDirection.East);
            _stopwatch.Stop();
            _stopwatchRound.Stop();
            return routeDistance.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            var map = new RaindeerMaze(datasetLines);
            Raindeer navigator = new(map);
            _ = navigator.CreateRoute(GenericDirection.East);
            return navigator.NumberOfBestSeats.ToString();
        }

        public static void PrintSolutionsCount(long latestSolutionScore, long totalSolutionsCount, long score, long raindeerTotalStepsTaken)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Solutions found: {0}. Latest solution score:{5} Lowest score found: {1}. Steps taken total: {4}. Total time spend {2}, round time {3}", totalSolutionsCount, score, _stopwatch.Elapsed, _stopwatchRound.Elapsed, raindeerTotalStepsTaken, latestSolutionScore);
            _stopwatchRound.Restart();
        }

        public static void PrintSolutionPaths(List<string> paths)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Solutions found: {0}.", paths.Count);
            foreach (var path in paths)
            {
                Console.WriteLine(path);
            }
        }

        public static void PrintNewSolutionScore(long score)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("(New solution fount: {0})", score);
        }

        public static void PrintMapToConsole(
            GenericMapTile[,] map,
            long currentScore = 0,
            GenericMapTile? currentPostion = null,
            string? positionHisory = null)
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);

            int currentX = currentPostion?.X ?? -1;
            int currentY = currentPostion?.Y ?? -1;

            var mapSpacing = Solution.WriteDebugInfoToConsole_PrintMapCompressed ? "" : " ";

            Console.WriteLine("");

            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    var currentTile = map[x, y];
                    if (positionHisory?.Contains($":{x},{y}:") ?? false)
                    {
                        if (y == currentY && x == currentX)
                            Console.ForegroundColor = ConsoleColor.Blue;
                        else if (currentTile is PathTile && ((PathTile)currentTile).IsJunction)
                            Console.ForegroundColor = ConsoleColor.Green;
                        else
                            Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("X" + mapSpacing);
                    }
                    else
                    {
                        if (currentTile is StartTile || currentTile is EndTile)
                            Console.ForegroundColor = ConsoleColor.Green;
                        else if (currentTile is PathTile && ((PathTile)currentTile).IsJunction)
                            Console.ForegroundColor = ConsoleColor.Green;
                        else if (currentTile is PathTile && ((PathTile)currentTile).IsBlocked)
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        else if (currentTile is PathTile && ((PathTile)currentTile).IsDeadEnd)
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        else if (currentTile is WallTile)
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                        else
                            Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(currentTile.Source + mapSpacing);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("Current score {0}", currentScore);
        }
    }
}