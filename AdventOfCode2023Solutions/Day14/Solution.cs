using Common;

namespace AdventOfCode2023Solutions.Day14
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 14: Parabolic Reflector Dish";

        public string SolvePart1(string[] datasetLines)
        {
            var platform = LoadPlatform(datasetLines);
            var stones = TiltPlatform(platform, out char[,] newTiltetNorthPlatform);
            var sum = stones.Sum(c => c.Sum(s => s));
            return sum.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            var platform = LoadPlatform(datasetLines);
            var stones = TiltPlatform(platform, out char[,] newTiltetNorthPlatform);
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
            newTiltetPlatform = new char[noOfRows, noOfCols];
            return stoneColumns;
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
    }
}
