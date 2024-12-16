using Common;

namespace AdventOfCode2024Solutions.Day16
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 16: Reindeer Maze";

        public string SolvePart1(string[] datasetLines)
        {
            Map map = new Map(datasetLines);
            PrintCharGrid(map.MapTiles, 0);
            Raindeer.WriteDebugConsoleInfo = true;

            var scoreCount = map.Traverse(Direction.East);
            return scoreCount.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            return "To be implemented";
        }

        public static void PrintCharGrid(char[,] grid, long currentScore, Position? currentPostion = null, List<Position>? positionHisory = null)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            int currentX = currentPostion?.X ?? -1;
            int currentY = currentPostion?.Y ?? -1;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {

                    if (positionHisory?.Any(p => p.X == j && p.Y == i) ?? false)
                    {
                        if (i == currentY && j == currentX)
                            Console.ForegroundColor = ConsoleColor.Blue;
                        else
                            Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("X" + " ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(grid[i, j] + " ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("Current score {0}", currentScore);


        }
    }
}
