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
            var map = new Map(DatasetLines);
            map.Part2_DetermineEntryLocations("A");
            map.Part2_DetermineExitLocations("Z");

            PrintMoveInstructions(map.MoveInstructions);
            PrintEntryExitLocations(map.Part2_EntryLocations, map.Part2_ExitLocations);
            Console.WriteLine("");
            PrintCurrentLocations(map);

            int exitLocationsFound = 0;
            int maxExitLocationsFoundAtSameTime = 0;
            for (int i = 0; i < 10000000000; i++)
            {
                map.Part2_Move();
                if (i % 1000000 == 0)
                    Console.WriteLine($"Completed {i} moves");

                exitLocationsFound = map.CountExitEndings();
                if (exitLocationsFound > 0)
                {
                    maxExitLocationsFoundAtSameTime = exitLocationsFound > maxExitLocationsFoundAtSameTime ? exitLocationsFound : maxExitLocationsFoundAtSameTime;

                    //Console.WriteLine($"Exit locations found: {exitLocationsFound}");
                    //PrintCurrentLocations(map);
                    if (exitLocationsFound >= map.Part2_NumberOfParralelLocations)
                        break;
                }

                //PrintCurrentLocations(map);
            }

            if (exitLocationsFound >= map.Part2_NumberOfParralelLocations)
            {
                return map.Part2_MoveCounter.ToString();
            }
            else
            {
                return $"Solution not found. Ran move instructions {map.Part2_MoveCounter} times! (found max {maxExitLocationsFoundAtSameTime} exit locations at the same time)";
            }
        }

        private static void PrintCurrentLocations(Map map)
        {
            var currentLocationLabelsArray = map.Part2_CurrentLocations.Select(l => l.Label).ToArray();
            var currentLocationLabelsString = string.Join(',', currentLocationLabelsArray);

            Console.WriteLine($"{map.Part2_MoveCounter,4} {currentLocationLabelsString} <- {map.MoveInstructions[map.Part2_PreviousDirectionPointer]}({map.Part2_PreviousDirectionPointer,3})");
        }

        private static void PrintMoveInstructions(char[] moveInstructions)
        {
            var moveInstructionsString = string.Join("", moveInstructions);

            Console.WriteLine("");
            Console.WriteLine($"Move instructions: {moveInstructionsString.Length}");
            Console.WriteLine(moveInstructionsString);
        }

        private static void PrintEntryExitLocations(MapPiece[] entryLocations, MapPiece[] exitLocations)
        {
            var entryLocationLabelsArray = entryLocations.Select(l => l.Label).ToArray();
            var entryLocationLabelsString = string.Join(',', entryLocationLabelsArray);

            var exitLocationLabelsArray = exitLocations.Select(l => l.Label).ToArray();
            var exitLocationLabelsString = string.Join(',', exitLocationLabelsArray);

            Console.WriteLine($"Exit locations  ({exitLocationLabelsString.Length})  {exitLocationLabelsString}");
            Console.WriteLine($"Entry locations ({entryLocationLabelsString.Length}) {entryLocationLabelsString}");
        }
    }
}
