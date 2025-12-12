using Common;

namespace AdventOfCode2025Solutions.Day07
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 7: ";

        public string SolvePart1(string[] datasetLines)
        {
            var map = new TachyonManifold(datasetLines);
            SearchSplitterV1(map.Start, map);
            return countSplit.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            var map = new TachyonManifold(datasetLines);
            SearchSplitterV2(map.Start, map);
            return map.Start.TimerValue.ToString();
        }

        private long countSplit = 0;

        private void SearchSplitterV1(Location currentTile, TachyonManifold diagram)
        {
            if (currentTile.Down is SpaceTile && currentTile.Down.Source == '.')
            {
                currentTile.Down.Source = '|';
                SearchSplitterV1(currentTile.Down, diagram);
            }
            else if (currentTile.Down is SplitterTile)
            {
                countSplit++;
                if (currentTile.Down.Left != null && currentTile.Down.Left.Source == '.')
                {
                    currentTile.Down.Left.Source = '|';
                    SearchSplitterV1(currentTile.Down.Left, diagram);
                }
                if (currentTile.Down.Right != null && currentTile.Down.Right.Source == '.')
                {
                    currentTile.Down.Right.Source = '|';
                    SearchSplitterV1(currentTile.Down.Right, diagram);
                }
            }
            return;//bottom
        }

        private static void SearchSplitterV2(Location currentTile, TachyonManifold diagram)
        {
            //ConsolePrinterOfCharMap.PrintMapToConsole(diagram.MapTiles);

            if (currentTile.Down is SpaceTile)
            {
                if (currentTile.Down.TimerValue == 0)
                    SearchSplitterV2(currentTile.Down, diagram);
                currentTile.TimerValue += currentTile.Down.TimerValue;
                //currentTile.Source = currentTile.TimerValue.ToString()[0];
                return;
            }
            else if (currentTile.Down is SplitterTile)
            {

                if (currentTile.Down.Left != null)
                {
                    if (currentTile.Down.Left.TimerValue == 0)
                        SearchSplitterV2(currentTile.Down.Left, diagram);
                    currentTile.TimerValue += currentTile.Down.Left.TimerValue;
                }
                if (currentTile.Down.Right != null)
                {
                    if (currentTile.Down.Right.TimerValue == 0)
                        SearchSplitterV2(currentTile.Down.Right, diagram);
                    currentTile.TimerValue += currentTile.Down.Right.TimerValue;
                }
                //currentTile.Source = currentTile.TimerValue.ToString()[0];
                return;
            }

            //bottom found
            currentTile.TimerValue = 1;
            //currentTile.Source = currentTile.TimerValue.ToString()[0];
            return;
        }
    }
}