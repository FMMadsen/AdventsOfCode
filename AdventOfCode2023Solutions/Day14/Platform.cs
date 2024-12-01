namespace AdventOfCode2023Solutions.Day14
{
    public class Platform
    {
        internal readonly char[,] map;
        internal readonly int noOfRows;
        internal readonly int noOfCols;
        private readonly Queue<int> queue;
        internal int sumWeight;

        public Platform(string[] datasetLines)
        {
            noOfRows = datasetLines.Length;
            noOfCols = datasetLines[0].Length;
            map = CreateMap(datasetLines);
            queue = new Queue<int>();
            sumWeight = 0;
        }

        public void TiltCycle()
        {
            TiltNorth();
            TiltWest();
            TiltSouth();
            TiltEast();
        }

        public void TiltNorth()
        {
            for (int columnIndex = 0; columnIndex < noOfCols; columnIndex++)
            {
                queue.Clear();
                for (int rowIndex = 0; rowIndex < noOfRows; rowIndex++)
                {
                    var item = map[rowIndex, columnIndex];
                    if (item == '.')
                    {
                        queue.Enqueue(rowIndex);
                        continue;
                    }
                    if (item == '#')
                    {
                        queue.Clear();
                        continue;
                    }
                    if (item == 'O')
                    {
                        if (queue.TryDequeue(out int availableRow))
                        {
                            map[availableRow, columnIndex] = 'O';
                            map[rowIndex, columnIndex] = '.';
                            queue.Enqueue(rowIndex);
                            continue;
                        }
                    }
                }
            }
        }

        public void TiltWest()
        {
            for (int rowIndex = 0; rowIndex < noOfRows; rowIndex++)
            {
                queue.Clear();
                for (int columnIndex = 0; columnIndex < noOfCols; columnIndex++)
                {
                    var item = map[rowIndex, columnIndex];
                    if (item == '.')
                    {
                        queue.Enqueue(columnIndex);
                        continue;
                    }
                    if (item == '#')
                    {
                        queue.Clear();
                        continue;
                    }
                    if (item == 'O')
                    {
                        if (queue.TryDequeue(out int availableColumn))
                        {
                            map[rowIndex, availableColumn] = 'O';
                            map[rowIndex, columnIndex] = '.';
                            queue.Enqueue(columnIndex);
                            continue;
                        }
                    }
                }
            }
        }

        public void TiltSouth()
        {
            for (int columnIndex = 0; columnIndex < noOfCols; columnIndex++)
            {
                queue.Clear();
                for (int rowIndex = noOfRows - 1; rowIndex > -1; rowIndex--)
                {
                    var item = map[rowIndex, columnIndex];
                    if (item == '.')
                    {
                        queue.Enqueue(rowIndex);
                        continue;
                    }
                    if (item == '#')
                    {
                        queue.Clear();
                        continue;
                    }
                    if (item == 'O')
                    {
                        if (queue.TryDequeue(out int availableRow))
                        {
                            map[availableRow, columnIndex] = 'O';
                            map[rowIndex, columnIndex] = '.';
                            queue.Enqueue(rowIndex);
                            continue;
                        }
                    }
                }
            }
        }

        public void TiltEast()
        {
            sumWeight = 0;
            for (int rowIndex = 0; rowIndex < noOfRows; rowIndex++)
            {
                queue.Clear();
                for (int columnIndex = noOfCols - 1; columnIndex > -1; columnIndex--)
                {
                    var item = map[rowIndex, columnIndex];
                    if (item == '.')
                    {
                        queue.Enqueue(columnIndex);
                        continue;
                    }
                    if (item == '#')
                    {
                        queue.Clear();
                        continue;
                    }
                    if (item == 'O')
                    {
                        if (queue.TryDequeue(out int availableColumn))
                        {
                            map[rowIndex, availableColumn] = 'O';
                            map[rowIndex, columnIndex] = '.';
                            queue.Enqueue(columnIndex);
                        }
                        sumWeight += noOfRows - rowIndex;
                    }
                }
            }
        }

        public int CalculateStonesSumWeight()
        {
            int sum = 0;
            for (int columnIndex = 0; columnIndex < noOfCols; columnIndex++)
                for (int rowIndex = 0; rowIndex < noOfRows; rowIndex++)
                    if (map[rowIndex, columnIndex] == 'O')
                        sum += noOfRows - rowIndex;
            return sum;
        }

        public static char[,] CreateMap(string[] datasetLines)
        {
            int noOfRows = datasetLines.Length;
            int noOfCols = datasetLines[0].Length;
            char[,] map = new char[noOfRows, noOfCols];
            for (int columnIndex = 0; columnIndex < noOfCols; columnIndex++)
                for (int rowIndex = 0; rowIndex < noOfRows; rowIndex++)
                    map[rowIndex, columnIndex] = datasetLines[rowIndex][columnIndex];
            return map;
        }

        public bool MapIsEqualTo(char[,] otherMap)
        {
            return MapEquals(map, otherMap);
        }

        public static bool MapEquals(char[,] map1, char[,] map2)
        {
            int map1NoOfRows = map1.GetLength(0);
            int map1NoOfCols = map1.GetLength(1);
            int map2NoOfRows = map1.GetLength(0);
            int map2NoOfCols = map1.GetLength(1);

            if (map1NoOfRows != map2NoOfRows)
                return false;

            if (map1NoOfCols != map2NoOfCols)
                return false;

            for (int columnIndex = 0; columnIndex < map1NoOfCols; columnIndex++)
                for (int rowIndex = 0; rowIndex < map1NoOfRows; rowIndex++)
                    if (!(map1[rowIndex, columnIndex] == map2[rowIndex, columnIndex]))
                        return false;
            return true;
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
