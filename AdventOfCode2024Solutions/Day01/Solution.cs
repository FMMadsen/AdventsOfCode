using Common;

namespace AdventOfCode2024Solutions.Day01
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 1: Historian Hysteria";

        public string SolvePart1(string[] datasetLines)
        {
            List<int> group1;
            List<int> group2;
            SplitListInTwoGroups(datasetLines, out group1, out group2);
            var group1Sorted = group1.OrderBy(x => x).ToList();
            var group2Sorted = group2.OrderBy(x => x).ToList();

            var sumOfDifs = 0;
            var numberOfLocations = group1.Count;

            for (var i = 0; i< numberOfLocations; i++)
            {
                var id1 = group1Sorted[i];
                var id2 = group2Sorted[i];
                sumOfDifs += GetDiff(id1, id2);
            }

            return sumOfDifs.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            List<int> group1;
            List<int> group2;
            SplitListInTwoGroups(datasetLines, out group1, out group2);

            var numberOfLocations = group1.Count;
            var sumOfMatches = 0;

            for (var i = 0; i < numberOfLocations; i++)
            {
                var id1 = group1[i];
                var countMatches = group2.FindAll(x => x == id1).Count;
                sumOfMatches += (id1 * countMatches);
            }

            return sumOfMatches.ToString();
        }

        private void SplitListInTwoGroups(string[] datasetLines, out List<int> group1, out List<int> group2)
        {
            group1 = new List<int>();
            group2 = new List<int>();
            
            foreach (var line in datasetLines)
            {
                if(line == null)
                    continue;

                var numbers = SplitStringInNumbers(line);
                
                group1.Add(numbers[0]);
                group2.Add(numbers[1]);
            }
        }

        private List<int> SplitStringInNumbers(string stringWithNumbersSeparated)
        {
            var numberList = new List<int>();
            var split = stringWithNumbersSeparated.Split(" ");

            foreach (var numberString in split)
            {
                if(int.TryParse(numberString, out var number))
                    numberList.Add(number);
            }
            return numberList;
        }
        
        private int GetDiff(int a, int b)
        {
            return Math.Abs(a - b);
        }
    }
}
