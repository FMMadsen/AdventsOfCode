using Common;
using System.Data;
using ToolsFramework.Geometry;

namespace AdventOfCode2025Solutions.Day09
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 9: Movie Theater";

        public string SolvePart1(string[] datasetLines)
        {
            var redTiles = datasetLines.Select(Coordinate.FromCoordinateStringXY<RedTile>).ToList();
            var redTilesOrdered = redTiles.OrderBy(c => c.Y).ThenBy(c => c.X).ToList();
            SetLeftRight(redTilesOrdered);
            var rect = IdentifyLargestPossibleRectPart1(redTilesOrdered);
            return rect.Area.ToString();
        }

        private static Rect IdentifyLargestPossibleRectPart1(List<RedTile> coordinateList, bool validateGreenRedTiles = false)
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
                var coordinate1 = coordinates[s];
                if (coordinate1.IsRight)
                    continue;

                for (int t = s + 1; t < sMax; t++)
                {
                    var coordinate2 = coordinates[t];
                    if (coordinate2.IsLeft)
                        continue;

                    var rect = Rect.FromCoordinates(coordinate1, coordinate2, true);
                    var area = rect.Area;
                    //Console.WriteLine($"Testing: {s} vs {t} ({coordinate1.X},{coordinate1.Y} vs {coordinate2.X},{coordinate2.Y}) : {area}");

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

        public string SolvePart2(string[] datasetLines)
        {
            var redTiles = datasetLines.Select(Coordinate.FromCoordinateStringXY<RedTile>).ToList();
            var redTilesOrdered = redTiles.OrderBy(c => c.Y).ThenBy(c => c.X).ToList();
            SetLeftRight(redTilesOrdered);
            //Console.WriteLine("Red tiles ordered:");
            //int t = 0;
            //redTilesOrdered.ForEach(c => Console.WriteLine($"{t++}: " + c.ToString() + " (" + (c.IsLeft ? "left" : "right") + ")"));

            var rows = InitializeTileRows(redTilesOrdered);
            //int rIndex = 0;
            //Console.WriteLine("Rows ordered:");
            //rows.ForEach(r => Console.WriteLine($"{rIndex++}: row:{r.Row}  {r.LeftMost}-{r.RightMost}  ({r.LeftRedTile.X}-{r.RightRedTile.X})"));

            //Console.WriteLine("Try rects:");
            var rect = IdentifyLargestPossibleRect([.. rows]);
            return rect.Area.ToString();
        }

        private static void SetLeftRight(List<RedTile> tilesOrdered)
        {
            bool isLeft = true;
            foreach (var tile in tilesOrdered)
            {
                if (isLeft) tile.SetLeft();
                else tile.SetRight();
                isLeft = !isLeft;
            }
        }

        private static List<TileRow> InitializeTileRows(List<RedTile> tilesOrdered)
        {
            List<TileRow> rows = [];
            var leftMostTile = tilesOrdered[0].X;
            var rightMostTile = tilesOrdered[1].X;

            for (int i = 1; i < tilesOrdered.Count; i += 2)
            {
                var tileLeft = tilesOrdered[i - 1];
                var tileRight = tilesOrdered[i];
                var row = new TileRow(tileLeft, tileRight);
                rows.Add(row);

                //Extend if polygon width: turns left, and green tiles are extending rightmost red tile
                if (row.RightRedTile.X == leftMostTile)
                {
                    row.RightMost = rightMostTile;
                    leftMostTile = row.LeftMost;
                }

                //Extend if polygon width: turns right, and green tiles are extending leftmost red tile
                if (row.LeftRedTile.X == rightMostTile)
                {
                    row.LeftMost = leftMostTile;
                    rightMostTile = row.RightMost;
                }

                //Reduce Polygon width: left corner turns inward
                if (row.LeftRedTile.X == leftMostTile)
                {
                    if (rightMostTile > row.RightRedTile.X)
                        row.RightMost = rightMostTile;
                }

                //Reduce Polygon width: right corner turns inward
                if (row.RightRedTile.X == rightMostTile)
                {
                    if (leftMostTile > row.LeftRedTile.X)
                        row.LeftMost = leftMostTile;
                }
            }
            return rows;
        }

        private Rect? _largestRect = null;
        private long _largestArea = long.MinValue;

        private Rect IdentifyLargestPossibleRect(TileRow[] rows)
        {
            for (int s = 0; s < rows.Length; s++)
            {
                for (int t = s; t < rows.Length; t++)
                {
                    var left = rows[s].LeftRedTile;
                    var right = rows[t].RightRedTile;
                    TestCorners(left, right, rows);

                    //Upper left corner of test row could also be the right red tile actually
                    if(rows[s].RightRedTile.X < rows[t].RightRedTile.X)
                    {
                        var left2 = rows[s].RightRedTile;
                        TestCorners(left2, right, rows);
                    }

                    //Upper right corner of test row could also be the right red tile actually
                    if (rows[s].LeftRedTile.X < rows[t].LeftRedTile.X)
                    {
                        var right2 = rows[t].LeftRedTile;
                        TestCorners(left, right2, rows);
                    }

                    //try right of upper ald left of lower..??? maybe possible
                    if (rows[s].RightRedTile.X < rows[t].LeftRedTile.X)
                    {
                        var left2 = rows[s].RightRedTile;
                        var right2 = rows[t].LeftRedTile;
                        TestCorners(left2, right2, rows);
                    }
                }
            }

            if (_largestRect == null)
                throw new InvalidOperationException("No rectangle could be identified.");

            return _largestRect;
        }

        private void TestCorners(RedTile left, RedTile right, TileRow[] rows)
        {

            var rect = Rect.FromCoordinates(left, right, true);
            var area = rect.Area;
            bool isEligeble = IsEligeble(left, right, rows);
            //bool isEligeble2 = IsEligeble( rows[s], rows[t], rows);

            if (!isEligeble)
            {
                //Console.WriteLine($"Skiping: {left.X},{left.Y} vs {right.X},{right.Y} : {area}");
                return;
            }

            //Console.WriteLine($"Testing: {left.X},{left.Y} vs {right.X},{right.Y}) : {area}");

            if (area > _largestArea)
            {
                _largestArea = area;
                _largestRect = rect;
            }
        }

        private static bool IsEligeble(RedTile tileLeft, RedTile tileRight, TileRow[] rows)
        {
            long top = tileLeft.Y;
            long bottom = tileRight.Y;
            long left = tileLeft.X;
            long right = tileRight.X;

            foreach (TileRow row in rows)
            {
                bool isRelevantRow = row.Row >= top && row.Row <= bottom;

                if (isRelevantRow)
                {
                    if (left < row.LeftMost || right > row.RightMost)
                        return false;
                }
            }

            return true;
        }
    }
}