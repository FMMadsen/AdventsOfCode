using System.Data;

namespace AdventOfCode2023Solutions.Day13
{
    public class PatternNote
    {
        public int ID { get; private set; }
        public string[] Rows { get; private set; }
        public bool IsHorizontalMirrored { get; private set; } = false;
        public bool IsVerticalMirrored { get; private set; } = false;
        public int mirrorLine { get; private set; } = 0;
        public long Total { get; private set; } = 0;

        public PatternNote(string[] rows, int id)
        {
            ID = id;
            Rows = rows;
        }

        public void FindMirrorTotalsPart1(long rowMultiplier, long columnMultiplier)
        {
            if (FindMirrorLine(Rows, out int noOfRows, noOfSmudgeToFix: 0))
            {
                IsHorizontalMirrored = true;
                mirrorLine = noOfRows;
                Total = noOfRows * rowMultiplier;
                return;
            }

            var Columns = TransposeMatrix90Degrees(Rows);
            if (FindMirrorLine(Columns, out int noOfColumns, noOfSmudgeToFix: 0))
            {
                IsVerticalMirrored = true;
                mirrorLine = noOfColumns;
                Total = noOfColumns * columnMultiplier;
                return;
            }
            throw new Exception($"No mirror line found for pattern {ID}");
        }

        public void FindMirrorTotalsPart2(long rowMultiplier, long columnMultiplier)
        {
            if (FindMirrorLine(Rows, out int noOfRows, noOfSmudgeToFix: 1))
            {
                IsHorizontalMirrored = true;
                mirrorLine = noOfRows;
                Total = noOfRows * rowMultiplier;
                return;
            }

            var Columns = TransposeMatrix90Degrees(Rows);
            if (FindMirrorLine(Columns, out int noOfColumns, noOfSmudgeToFix: 1))
            {
                IsVerticalMirrored = true;
                mirrorLine = noOfColumns;
                Total = noOfColumns * columnMultiplier;
                return;
            }
            throw new Exception($"No mirror line found for pattern {ID}");
        }

        private static bool FindMirrorLine(string[] rows, out int mirrorLinePosition, int noOfSmudgeToFix)
        {
            for (int i = 1; i < rows.Length; i++)
            {
                int iUpper = i-1;
                int iLower = i;
                int iMin = 0;
                int iMax = rows.Length - 1;
                int totalDiffCount = 0;

                while (iUpper >= iMin && iLower <= iMax)
                {
                    totalDiffCount += CountNoOfStringDiff(rows[iUpper], rows[iLower], noOfSmudgeToFix);

                    if (totalDiffCount > noOfSmudgeToFix)
                        break;

                    iUpper--;
                    iLower++;
                }

                if (totalDiffCount == noOfSmudgeToFix)
                {
                    mirrorLinePosition = i;
                    return true;
                }
            }

            mirrorLinePosition = -1;
            return false;
        }

        public static int CountNoOfStringDiff(string strings1, string strings2, int maxNoOfDiff)
        {
            int noOfDiffFound = 0;
            for (int i = 0; i < strings1.Length; i++)
            {
                if (strings1[i] != strings2[i])
                {
                    noOfDiffFound++;
                    if (noOfDiffFound > maxNoOfDiff)
                        break;
                }
            }
            return noOfDiffFound;
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
