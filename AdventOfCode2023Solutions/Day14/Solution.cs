using Common;

namespace AdventOfCode2023Solutions.Day14
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 14: Parabolic Reflector Dish";

        public char[,]? PlatformInitial { get; set; } = null;
        public char[,]? PlatformTiltetNorth { get; set; } = null;
        public char[,]? PlatformTiltetWest { get; set; } = null;
        public char[,]? PlatformTiltetSouth { get; set; } = null;
        public char[,]? PlatformTiltetEast { get; set; } = null;
        public char[,]? PlatformOneCircle { get; set; } = null;


        public string SolvePart1(string[] datasetLines)
        {
            var platform = LoadPlatform(datasetLines);
            TiltPlatform(platform, out char[,] newTiltetNorthPlatform);
            var stones = CalculateStonesWeight(newTiltetNorthPlatform);
            var sum = stones.Sum(c => c.Sum(s => s));
            return sum.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            var platform = LoadPlatform(datasetLines);
            PlatformInitial = platform;

            char[,]? newTiltetNorthPlatform, newTiltetWestPlatform, newTiltetSouthPlatform, newTiltetEastPlatform, platformTransposed = null;

            for (int i = 0; i < 1000000000; i++)
            {
                TiltPlatform(platform, out newTiltetNorthPlatform);

                platformTransposed = TransposeMatrix90Degrees(newTiltetNorthPlatform);
                TiltPlatform(platformTransposed, out newTiltetWestPlatform);

                platformTransposed = TransposeMatrix90Degrees(newTiltetWestPlatform);
                TiltPlatform(platformTransposed, out newTiltetSouthPlatform);

                platformTransposed = TransposeMatrix90Degrees(newTiltetSouthPlatform);
                TiltPlatform(platformTransposed, out newTiltetEastPlatform);

                platformTransposed = TransposeMatrix90Degrees(newTiltetEastPlatform);

                if (i % 10000 == 0)
                    Console.WriteLine($"Counted {i:#,##0} out of 1.000.000.000");
            }

            var stones = CalculateStonesWeight(platformTransposed);

            //PlatformTiltetNorth = newTiltetNorthPlatform;
            //PlatformTiltetWest = newTiltetWestPlatform;
            //PlatformTiltetSouth = newTiltetSouthPlatform;
            //PlatformTiltetEast = newTiltetEastPlatform;
            //PlatformOneCircle = platformTransposed;

            var sum = stones.Sum(c => c.Sum(s => s));
            return sum.ToString();
        }

        private static List<int>[] TiltPlatform(char[,] platform, out char[,] newTiltetPlatform)
        {
            var noOfRows = platform.GetLength(0);
            var noOfCols = platform.GetLength(1);

            var stoneColumns = new List<int>[noOfCols];
            var rockColumns = new List<int>[noOfCols];
            var availableSpaceColumns = new Queue<int>[noOfCols];

            for (int columnIndex = 0; columnIndex < noOfCols; columnIndex++)
            {
                stoneColumns[columnIndex] = new List<int>();
                rockColumns[columnIndex] = new List<int>();
                availableSpaceColumns[columnIndex] = new Queue<int>();

                for (int rowIndex = 0; rowIndex < noOfRows; rowIndex++)
                {
                    var contentOnLocation = platform[rowIndex, columnIndex];
                    var loadOrder = noOfRows - rowIndex;
                    switch (contentOnLocation)
                    {
                        case '#':
                            rockColumns[columnIndex].Add(loadOrder);
                            availableSpaceColumns[columnIndex].Clear();
                            break;
                        case 'O':
                            if (availableSpaceColumns[columnIndex].TryDequeue(out int rollUpTo))
                            {
                                stoneColumns[columnIndex].Add(rollUpTo);
                                availableSpaceColumns[columnIndex].Enqueue(loadOrder);
                            }
                            else
                                stoneColumns[columnIndex].Add(loadOrder);
                            break;
                        case '.':
                            availableSpaceColumns[columnIndex].Enqueue(loadOrder);
                            break;
                    }
                }
            }
            newTiltetPlatform = MapOutPlatform(noOfRows, noOfCols, stoneColumns, rockColumns);
            return stoneColumns;
        }

        private static List<int>[] CalculateStonesWeight(char[,]? platform)
        {
            if (platform == null)
                return new List<int>[0];

            var noOfRows = platform.GetLength(0);
            var noOfCols = platform.GetLength(1);

            var stoneColumns = new List<int>[noOfCols];
            for (int columnIndex = 0; columnIndex < noOfCols; columnIndex++)
            {
                stoneColumns[columnIndex] = new List<int>();

                for (int rowIndex = 0; rowIndex < noOfRows; rowIndex++)
                {
                    var contentOnLocation = platform[rowIndex, columnIndex];
                    var loadOrder = noOfRows - rowIndex;
                    if (contentOnLocation == 'O')
                        stoneColumns[columnIndex].Add(loadOrder);
                }
            }
            return stoneColumns;
        }

        private static char[,] MapOutPlatform(int noOfRows, int noOfCols, List<int>[] stoneColumns, List<int>[] rockColumns)
        {
            var platform = new char[noOfRows, noOfCols];
            for (int columnIndex = 0; columnIndex < noOfCols; columnIndex++)
                for (int rowIndex = 0; rowIndex < noOfRows; rowIndex++)
                    platform[rowIndex, columnIndex] = '.';

            for (int c = 0; c < stoneColumns.Length; c++)
                foreach (var stoneValue in stoneColumns[c])
                    platform[noOfRows - stoneValue, c] = 'O';

            for (int c = 0; c < rockColumns.Length; c++)
                foreach (var rockValue in rockColumns[c])
                    platform[noOfRows - rockValue, c] = '#';

            return platform;
        }

        private static char[,] LoadPlatform(string[] datasetLines)
        {
            var noOfRows = datasetLines.Length;
            var noOfCols = datasetLines[0].Length;
            var platform = new char[noOfRows, noOfCols];
            for (int columnIndex = 0; columnIndex < noOfCols; columnIndex++)
                for (int rowIndex = 0; rowIndex < noOfRows; rowIndex++)
                    platform[rowIndex, columnIndex] = datasetLines[rowIndex][columnIndex];
            return platform;
        }

        public static char[,] TransposeMatrix90Degrees(char[,] input)
        {
            var noOfYs = input.GetLength(0);
            var noOfXs = input.GetLength(1);

            var m = new char[noOfXs, noOfYs];

            for (int y = 0; y < noOfYs; y++)
                for (int x = 0; x < noOfXs; x++)
                    m[x, noOfYs - 1 - y] = input[y, x];

            return m;
        }
    }
}
