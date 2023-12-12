using Common;

namespace AdventOfCode2023Solutions.Day08
{
    public class Solution(string[] datasetLines) : IAOCSolution
    {
        public string PuzzleName => "Day 8: Haunted Wasteland";
        public string[] DatasetLines => datasetLines;

        public string SolvePart1()
        {
            var map = new Map(DatasetLines);
            var moveCount = map.CountMovesPart1("AAA", "ZZZ");
            return moveCount.ToString();
        }

        public string SolvePart2()
        {
            return SolutionPart2();
        }

        private string SolutionPart2()
        {
            var map = new Map(DatasetLines);
            map.Part2_InitializeLocationVariables("A", "Z");

            //var moveStatisticsMap = map.CountMovesToFirstXExits(3);
            //PrintCountStatistics(moveStatisticsMap);
            //return "Working on part 2";

            var moveStatisticsMap = map.CountMovesToFirstXExits(1);
            var moveCountsToExitForLocations = moveStatisticsMap.Select(s => s[0]).ToArray();
            var lowestPossibleMoveCountToAllExits = Day8MathSupport.CalculateLowestCommonMultiplier(moveCountsToExitForLocations);

            return lowestPossibleMoveCountToAllExits.ToString();
        }

        private void PrintCountStatistics(List<long>[] moveStatisticsMap)
        {
            Console.WriteLine("Part 2");
            Console.WriteLine("Count-to-exit statistics");
            for (int i = 0; i < moveStatisticsMap.Length; i++)
            {
                var locationMoves = moveStatisticsMap[i].ToArray();

                Console.Write($"Location {i}: ");
                for (int m = 0; m < locationMoves.Length; m++)
                {
                    var lastMoveCount = m == 0 ? 0 : locationMoves[m - 1];
                    var moveCount = locationMoves[m];

                    Console.Write($" +{moveCount - lastMoveCount,-6}-> ");
                    Console.Write($"{moveCount,-10}");
                }
                Console.WriteLine("");
            }
        }
    }
}
