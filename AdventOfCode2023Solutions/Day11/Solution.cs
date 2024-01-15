using Common;

namespace AdventOfCode2023Solutions.Day11
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 11: Cosmic Expansion";

        public string SolvePart1(string[] datasetLines)
        {
            string[] expandedDatasetLines = ExpandLines([.. datasetLines]);

            IntVector2[] galaxies = GetGalaxies(expandedDatasetLines);

            long galaxyDistancesSum = SumGalaxyDistances(galaxies);

            return galaxyDistancesSum.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            IntVector2[] galaxies = GetExpandedGalaxies(datasetLines);

            long galaxyDistancesSum = SumGalaxyDistances(galaxies);

            return galaxyDistancesSum.ToString();
        }

        private IntVector2[] GetGalaxies(string[] dataset)
        {
            List<IntVector2> galaxies = new();

            int xLength = dataset.FirstOrDefault("").Length;
            for (int y = 0; y < dataset.Length; y++)
            {
                for(int x = 0; x < xLength; x++)
                {
                    if ('#' == dataset[y][x])
                    {
                        galaxies.Add(new IntVector2() { X=x,Y=y});
                    }
                }
            }

            return galaxies.ToArray();
        }

        private string[] ExpandLines(string[] datasetLines)
        {
            int expandSize = 1;
            char[] expandChars = new char[expandSize];
            for (int i = 0; i < expandSize; i++)
            {
                expandChars[i] = '.';
            }
            string expandString = new String(expandChars);

            int[] duplicateRows = datasetLines.Select((value, key) => new { value, key }).Where(a => -1 == a.value.IndexOf('#')).Select(a => a.key).ToArray();

            int columns = datasetLines.FirstOrDefault("").Length;
            int galaxyRows = 0;
            for (int iCol = 0; iCol < columns; iCol++)
            {
                galaxyRows = datasetLines.Count(a => '#' == a[iCol]);

                if (galaxyRows < 1) 
                {
                    for (int iRow = 0; iRow < datasetLines.Length; iRow++)
                    {
                        
                        datasetLines[iRow] = datasetLines[iRow].Insert(iCol, expandString);
                    }
                    iCol = iCol + expandSize;
                    columns = columns + expandSize;
                }
            }

            datasetLines = DuplicateRows(datasetLines, duplicateRows, expandSize);

            return datasetLines;
        }

        private string[] DuplicateRows(string[] dataset, int[] rowsToDuplicate, int expandSize)
        {
            string[] newDataset = new string[dataset.Length + (rowsToDuplicate.Length * expandSize)];
            rowsToDuplicate = [.. rowsToDuplicate, -1];

            for (int iN = 0, iD = 0, iO = 0; iN < newDataset.Length; iN++)
            {
                iO = iN - iD;
                newDataset[iN] = dataset[iO];
                if (iO == rowsToDuplicate[iD]) { iD++; }
            }

            return newDataset;
        }

        private IntVector2[] GetExpandedGalaxies(string[] datasetLines)
        {
            int expandSize = 999999;

            List<IntVector2> galaxies = new();
            int columns = datasetLines.FirstOrDefault("").Length;
            bool[] notExpandedRows = new bool[datasetLines.Length];
            bool[] notExpandedColumns = new bool[columns];

            for (int iRow = 0; iRow <  datasetLines.Length; iRow++)
            {
                for (int iCol = 0; iCol < columns; iCol++)
                {
                    if ('#' == datasetLines[iRow][iCol])
                    {
                        galaxies.Add(new IntVector2() { X = iCol, Y = iRow });
                        notExpandedRows[iRow] = true;
                        notExpandedColumns[iCol] = true;
                    }
                }
            }

            IntVector2[] expandedGalaxies = new IntVector2[galaxies.Count];

            int expandX = 0;
            int expandY = 0;
            for(int iG = 0; iG < expandedGalaxies.Length; iG++)
            {
                expandX = notExpandedColumns.Select((value, index) => new { value, index }).Count(a => a.index < galaxies[iG].X && !a.value);
                expandY = notExpandedRows.Select((value, index) => new { value, index }).Count(a => a.index < galaxies[iG].Y && !a.value);
                expandedGalaxies[iG] = new IntVector2() {X = galaxies[iG].X + (expandX * expandSize), Y = galaxies[iG].Y + (expandY * expandSize) };
            }

            return expandedGalaxies;
        }

        private long SumGalaxyDistances(IntVector2[] galaxies)
        {
            long sum = 0;
            int newX = 0, newY = 0;
            for(int iMain = 0; iMain < galaxies.Length; iMain++)
            {
                for (int iSecond = iMain+1; iSecond < galaxies.Length; iSecond++)
                {
                    newX = galaxies[iMain].X - galaxies[iSecond].X;
                    newY = galaxies[iMain].Y - galaxies[iSecond].Y;
                    if (newX < 0) 
                    { 
                        newX = -newX; 
                    }
                    if (newY < 0) 
                    { 
                        newY = -newY; 
                    }
                    sum += newX+newY;
                }
            }

            // iterate all lower.
            // main - second
            // distance = x + y
            return sum;
        }
    }
}
