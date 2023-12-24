using Common;

namespace AdventOfCode2023Solutions.Day14
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 14: Parabolic Reflector Dish";

        //private List<int>[] Stones = [];
        //private List<int>[] Rocks = [];
        //private Queue<int>[] Available = [];

        public string SolvePart1(string[] datasetLines)
        {
            var stones = LoadAndTiltPlatform(datasetLines);
            var sum = stones.Sum(c => c.Sum(s => s));
            return sum.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            //LoadInitialPlatform(datasetLines);
            return "To be implemented";
        }

        //private void LoadInitialPlatform(string[] datasetLines)
        //{
        //    var noOfRows = datasetLines.Length;
        //    var noOfCols = datasetLines[0].Length;

        //    var Stones = new List<int>[noOfCols];
        //    var Rocks = new List<int>[noOfCols];
        //    var Available = new Queue<int>[noOfCols];

        //    for (int columnIndex = 0; columnIndex < noOfCols; columnIndex++)
        //    {
        //        Stones[columnIndex] = new List<int>();
        //        Rocks[columnIndex] = new List<int>();
        //        Available[columnIndex] = new Queue<int>();

        //        for (int rowIndex = 0; rowIndex < noOfRows; rowIndex++)
        //        {
        //            var contentOnLocation = datasetLines[rowIndex][columnIndex];
        //            var loadOrder = noOfRows - rowIndex;
        //            if (contentOnLocation == '#')
        //                Rocks[columnIndex].Add(loadOrder);
        //            else if (contentOnLocation == 'O')
        //                Stones[columnIndex].Add(loadOrder);
        //            else if (contentOnLocation == '.')
        //                Available[columnIndex].Enqueue(loadOrder);

        //        }
        //    }
        //}

        private List<int>[] LoadAndTiltPlatform(string[] datasetLines)
        {
            var noOfRows = datasetLines.Length;
            var noOfCols = datasetLines[0].Length;

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
                    var contentOnLocation = datasetLines[rowIndex][columnIndex];
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
            return stoneColumns;
        }
    }
}
