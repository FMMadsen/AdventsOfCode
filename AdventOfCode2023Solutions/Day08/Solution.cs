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

            PrintEntryExitLocations(map.Part2_EntryLocations, map.Part2_ExitLocations);

            for (int i = 0; i < 30; i++)
            {
                map.Part2_Move();
                PrintCurrentLocations(map);

            }


            return "Not running - need fix!";
            //var moveCount = map.CountMovesPart2("A", "Z");
            //return moveCount.ToString();
        }

        private void PrintCurrentLocations(Map map)
        {
            var currentLocationLabelsArray = map.Part2_CurrentLocations.Select(l => l.Label).ToArray();
            var currentLocationLabelsString = string.Join(',', currentLocationLabelsArray);

            Console.WriteLine($"Current locations {currentLocationLabelsString}");
        }

        private void PrintEntryExitLocations(MapPiece[] entryLocations, MapPiece[] exitLocations)
        {
            var entryLocationLabelsArray = entryLocations.Select(l => l.Label).ToArray();
            var entryLocationLabelsString = string.Join(',', entryLocationLabelsArray);


            var exitLocationLabelsArray = exitLocations.Select(l => l.Label).ToArray();
            var exitLocationLabelsString = string.Join(',', exitLocationLabelsArray);

            Console.WriteLine($"Exit locations {exitLocationLabelsString}");
            Console.WriteLine($"Entry locations {entryLocationLabelsString}");
        }
    }
}
