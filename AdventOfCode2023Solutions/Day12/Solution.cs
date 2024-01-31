using Common;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2023Solutions.Day12
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 12: Hot Springs";

        public string SolvePart1(string[] datasetLines)
        {
            SpringStatusSheet[] springStatusSheets = BuildSpringStatusSheets(datasetLines);

            int sumArrangements = 0;
            int sumArrangements2 = 0;

            foreach (SpringStatusSheet sheet in springStatusSheets)
            {
                sumArrangements += sheet.ArrangementsCount;
                sumArrangements2 += sheet.Arrangements.Count;
            }

            return sumArrangements2.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            return "To be implemented";
        }

        private SpringStatusSheet[] BuildSpringStatusSheets(string[] datasetLines)
        {
            SpringStatusSheet[] result = new SpringStatusSheet[datasetLines.Length];

            for (int i = 0; i < datasetLines.Length; i++)
            {
                string[] data = datasetLines[i].Split(' ');
                DamagedGroup[] damagedGroups = data[1].Split(',').Select(a=> new DamagedGroup(int.Parse(a)) ).ToArray();

                result[i] = (new SpringStatusSheet(data[0],damagedGroups));
            }


            return result;
        }

    }
}
