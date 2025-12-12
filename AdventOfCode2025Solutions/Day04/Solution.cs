using Common;
using ToolsFramework.Map;

namespace AdventOfCode2025Solutions.Day04
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 4: Printing Department";

        public string SolvePart1(string[] datasetLines)
        {
            var map = new PaperGrid(datasetLines);
            //ConsolePrinterOfCharMap.PrintMapToConsole(map.SourceMapTiles);
            var sum = map.MapTileList.Where(t => t.Source == '@').Count(t => t.CountSouroundedBy('@') < 4);
            return sum.ToString();
        }

        private static void PrintTile(PaperGrid map, int x, int y)
        {
            Console.WriteLine(map[x, y].ToStringExtended() + " " + map[x, y].ToStringSouroundingsClockwise());
        }


        public string SolvePart2(string[] datasetLines)
        {
            var map = new PaperGrid(datasetLines);
            int sum = 0;
            List<GenericMapTile> papersRemoved = [];
            bool continueRemovePaper = true;

            while (continueRemovePaper)
            {
                papersRemoved.Clear();
                foreach (var tile in map.MapTileList)
                {
                    if (tile.Source != '@')
                        continue;
                    if (tile.CountSouroundedBy('@') < 4)
                        papersRemoved.Add(tile);
                }
                sum += papersRemoved.Count;
                papersRemoved.ForEach(t => t.Source = 'X');
                if (papersRemoved.Count == 0)
                    continueRemovePaper = false;
            }
            return sum.ToString();
        }
    }
}
