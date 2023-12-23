using System.Data;

namespace AdventOfCode2023Solutions.Day13
{
    public class PatternNote
    {
        public string[] Rows { get; private set; }
        public string[] Columns { get; private set; } = [];
        public bool IsHorizontalMirrored { get; private set; } = false;
        public bool IsVerticalMirrored { get; private set; } = false;
        public int mirrorLine { get; private set; } = 0;
        public long Total { get; private set; } = 0;

        public PatternNote(string[] rows)
        {
            Rows = rows;
        }

        public void FindMirrorTotalsPart1(long rowMultiplier, long columnMultiplier)
        {
            if (FindHorizontalMirror(out int noOfRows, diffTolerance: 0))
            {
                IsHorizontalMirrored = true;
                mirrorLine = noOfRows;
                Total = noOfRows * rowMultiplier;
            }
            else if (FindVerticalMirror(out int noOfColumns, diffTolerance: 0))
            {
                IsVerticalMirrored = true;
                mirrorLine = noOfColumns;
                Total = noOfColumns * columnMultiplier;
            }
        }

        public void FindMirrorTotalsPart2(long rowMultiplier, long columnMultiplier)
        {
            if (FindHorizontalMirror(out int noOfRows, diffTolerance:1))
            {
                IsHorizontalMirrored = true;
                mirrorLine = noOfRows;
                Total = noOfRows * rowMultiplier;
            }
            else if (FindVerticalMirror(out int noOfColumns, diffTolerance: 1))
            {
                IsVerticalMirrored = true;
                mirrorLine = noOfColumns;
                Total = noOfColumns * columnMultiplier;
            }
        }

        private bool FindHorizontalMirror(out int noOfRowsAbove, int diffTolerance)
        {
            for (int i = 1; i < Rows.Length; i++)
            {
                if (TestMirrorLine(Rows, i - 1, i, diffTolerance))
                {
                    noOfRowsAbove = i;
                    return true;
                }
            }
            noOfRowsAbove = -1;
            return false;
        }

        private bool FindVerticalMirror(out int noOfColumnsToLeft, int diffTolerance)
        {
            Columns = TransposeMatrix90Degrees(Rows);

            for (int i = 1; i < Columns.Length; i++)
            {
                if (TestMirrorLine(Columns, i - 1, i, diffTolerance))
                {
                    noOfColumnsToLeft = i;
                    return true;
                }
            }

            noOfColumnsToLeft = -1;
            return false;
        }

        private static bool TestMirrorLine(string[] rows, int rowIndex1, int rowIndex2, int diffTolerance)
        {
            if (TestStringsAreDifferent(rows[rowIndex1], rows[rowIndex2], diffTolerance))
                return false;

            int xUpper = rowIndex1;
            int xLower = rowIndex2;
            int xMin = 0;
            int xMax = rows.Length - 1;

            while (xUpper >= xMin && xLower <= xMax)
            {
                if (TestStringsAreDifferent(rows[xUpper], rows[xLower], diffTolerance))
                    return false;

                xUpper--;
                xLower++;
            }

            return true;
        }

        public static bool TestStringsAreDifferent(string strings1, string strings2, int diffTolerance)
        {
            if(diffTolerance == 0)
                return strings1 != strings2;

            int countDiff = 0;
            for (int i = 0; i < strings1.Length; i++)
            {
                if (strings1[i] != strings2[i])
                    countDiff++;
            }

            return countDiff > diffTolerance;
        }

        public static string[] TransposeMatrix90Degrees(string[] input)
        {
            var noOfYs = input.Length;
            var noOfXs = input[0].Length;

            var m = new char[noOfXs][];
            for (int i = 0; i < m.Length; i++)
                m[i] = new char[noOfYs];

            for (int y = 0; y < noOfYs; y++)
            {
                for (int x = 0; x < noOfXs; x++)
                {
                    m[x][noOfYs - 1 - y] = input[y][x];
                }
            }

            return m.Select(i => string.Join("", i)).ToArray();
        }
    }
}
