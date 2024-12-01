
using Common;

namespace AdventOfCode2024Solutions.Day01
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 1: Historian Hysteria";
        protected int[] LeftList { get; set; } = Array.Empty<int>();
        protected int[] RightList { get; set; } = Array.Empty<int>();


        public string SolvePart1(string[] datasetLines)
        {
            Split(datasetLines);
            SortLists();

            return CountDifferences().ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            Split(datasetLines);

            return CalcSimilarities().ToString();
        }

        protected void Split(string[] datasetLines)
        {
            
            LeftList = new int[datasetLines.Length];
            RightList = new int[datasetLines.Length];

            for (int i = 0; i < datasetLines.Length; i++)
            {
                var temp = datasetLines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                LeftList[i] = Int32.Parse(temp[0]);
                RightList[i] = Int32.Parse(temp[1]);
            }
        }

        protected void SortLists()
        {
            LeftList = LeftList.Order().ToArray();
            RightList = RightList.Order().ToArray();
        }

        protected int CountDifferences()
        {
            int temp = 0;
            int total = 0;

            for (int i = 0; i < LeftList.Length; i++)
            {
                temp = LeftList[i] - RightList[i];
                if (0 > temp)
                {
                    temp *= -1;
                }
                total += temp;
            }

            return total;
        }

        protected int CalcSimilarities()
        {
            int total = 0;

            foreach (int temp in LeftList)
            {
                total += temp * RightList.Where(a=> a == temp).Count();
            }

            return total;
        }
    }
}
