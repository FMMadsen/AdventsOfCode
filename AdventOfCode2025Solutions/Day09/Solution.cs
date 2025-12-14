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

        private static Rect IdentifyLargestPossibleRect(List<Coordinate> coordinateList)
        {
            if (coordinateList == null || coordinateList.Count < 2)
                throw new ArgumentException("At least two coordinates are required.", nameof(coordinateList));

            var coordinates = coordinateList.ToArray();
            Rect? largestRect = null;
            long largestArea = long.MinValue;

            // Compare every pair of coordinates
            // Conceptually the source is the full list iteration, and target is the subset of the list comparing with to
            // avoid comparing same twice. E.g. 4 items (0-3) need compared like: 0:1 0:2 0:3 1:2 1:3 2:3 -> Done! not all vs all
            // source goes from 0->2
            // target goes from s+1 -> 3
            var sMax = coordinates.Length;
            for (int s = 0; s < sMax - 1; s++)
            {
                for (int t = s + 1; t < sMax; t++)
                {
                    var rect = Rect.FromCoordinates(coordinates[s], coordinates[t], true);
                    var area = rect.Area;
                    //Console.WriteLine($"Testing: {s} vs {t} ({coordinates[s].X},{coordinates[s].Y} vs {coordinates[t].X},{coordinates[t].Y}) : {area}");
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