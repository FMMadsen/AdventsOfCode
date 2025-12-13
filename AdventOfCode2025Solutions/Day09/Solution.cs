using Common;
using ToolsFramework.Geometry;

namespace AdventOfCode2025Solutions.Day09
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 9: Movie Theater";

        public string SolvePart1(string[] datasetLines)
        {
            var coordinateList = datasetLines.Select(Coordinate.FromCoordinateStringXY).ToList();
            var rect = IdentifyLargestPossibleRect(coordinateList);
            return rect.Area.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            return "To be implemented";
        }

        private static Rect IdentifyLargestPossibleRect(IList<Coordinate> coordinateList)
        {
            if (coordinateList == null || coordinateList.Count < 2)
                throw new ArgumentException("At least two coordinates are required.", nameof(coordinateList));

            Rect? largestRect = null;
            long largestArea = long.MinValue;

            // Compare every pair of coordinates
            for (int i = 0; i < coordinateList.Count; i++)
            {
                for (int j = i + 1; j < coordinateList.Count; j++)
                {
                    var c1 = coordinateList[i];
                    var c2 = coordinateList[j];

                    var rect = Rect.FromCoordinates(c1, c2);
                    var area = rect.Area;

                    if (area > largestArea)
                    {
                        largestArea = area;
                        largestRect = rect;
                    }
                }
            }

            if (largestRect == null)
                throw new InvalidOperationException("No rectangle could be identified.");

            return largestRect;
        }
    }
}