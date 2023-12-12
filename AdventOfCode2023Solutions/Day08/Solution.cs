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
            //return SolutionPart2Version1();
            Console.WriteLine("Number 2 high to handle for C# long or double!");
            return SolutionPart2Version2();
        }

        private string SolutionPart2Version2()
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

        

        //private string SolutionPart2Version1()
        //{
        //    var map = new Map(DatasetLines);
        //    map.Part2_InitializeLocationVariables("A", "Z");

        //    PrintMoveInstructions(map.MoveInstructions);
        //    PrintEntryExitLocations(map.Part2_EntryLocations, map.Part2_ExitLocations);
        //    Console.WriteLine("");
        //    PrintCurrentLocations(map);

        //    int exitLocationsFound = 0;
        //    int maxExitLocationsFoundAtSameTime = 0;
        //    int exitLocationsNeeded = map.Part2_NumberOfParralelLocations;
        //    bool allExitLocationsFound = false;

        //    while (!allExitLocationsFound)
        //    {
        //        map.Part2_Move();

        //        if (map.MoveCounter % 5000000 == 0)
        //            Console.WriteLine($"Completed {map.MoveCounter} moves");

        //        exitLocationsFound = map.CountExitEndings();
        //        if (exitLocationsFound > 0)
        //        {
        //            maxExitLocationsFoundAtSameTime = exitLocationsFound > maxExitLocationsFoundAtSameTime ? exitLocationsFound : maxExitLocationsFoundAtSameTime;
        //            allExitLocationsFound = exitLocationsFound >= exitLocationsNeeded;

        //            //Console.WriteLine($"Exit locations found: {exitLocationsFound}");
        //            //PrintCurrentLocations(map);
        //        }

        //        //PrintCurrentLocations(map);
        //    }

        //    if (allExitLocationsFound)
        //    {
        //        return map.MoveCounter.ToString();
        //    }
        //    else
        //    {
        //        return $"Solution not found. Ran move instructions {map.MoveCounter} times! (found max {maxExitLocationsFoundAtSameTime} exit locations at the same time)";
        //    }
        //}

        //private static void PrintCurrentLocations(Map map)
        //{
        //    var currentLocationLabelsArray = map.Part2_CurrentLocations.Select(l => l.Label).ToArray();
        //    var currentLocationLabelsString = string.Join(',', currentLocationLabelsArray);

        //    Console.WriteLine($"{map.MoveCounter,4} {currentLocationLabelsString} <- {map.MoveInstructions[map.Part2_PreviousDirectionPointer]}({map.Part2_PreviousDirectionPointer,3})");
        //}

        //private static void PrintMoveInstructions(char[] moveInstructions)
        //{
        //    var moveInstructionsString = string.Join("", moveInstructions);

        //    Console.WriteLine("");
        //    Console.WriteLine($"Move instructions: {moveInstructionsString.Length}");
        //    Console.WriteLine(moveInstructionsString);
        //}

        //private static void PrintEntryExitLocations(MapPiece[] entryLocations, MapPiece[] exitLocations)
        //{
        //    var entryLocationLabelsArray = entryLocations.Select(l => l.Label).ToArray();
        //    var entryLocationLabelsString = string.Join(',', entryLocationLabelsArray);

        //    var exitLocationLabelsArray = exitLocations.Select(l => l.Label).ToArray();
        //    var exitLocationLabelsString = string.Join(',', exitLocationLabelsArray);

        //    Console.WriteLine($"Exit locations  ({exitLocationLabelsString.Length})  {exitLocationLabelsString}");
        //    Console.WriteLine($"Entry locations ({entryLocationLabelsString.Length}) {entryLocationLabelsString}");
        //}
    }
}
