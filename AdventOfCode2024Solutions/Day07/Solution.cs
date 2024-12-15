using Common;

namespace AdventOfCode2024Solutions.Day07
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 7: ";

        public string SolvePart1(string[] datasetLines)
        {
            // for each succes
            // total += number
            long count = 0;

            foreach (var datasetLine in datasetLines)
            {
                long testValue;
                int[] numbersList;
                {
                    string[] temp = datasetLine.Split(':');
                    testValue = Int64.Parse(temp[0]);
                    numbersList = temp[1].Split(' ', StringSplitOptions.RemoveEmptyEntries|StringSplitOptions.TrimEntries).Select(a=> Int32.Parse(a)).ToArray();
                }

                Operator currentOperatorString = new() { TestValue = testValue, Numbers = numbersList };
                if (currentOperatorString.TestCombinations())
                {
                    count += currentOperatorString.TestValue;
                }
                
            }

            return count.ToString();
        }


        public string SolvePart2(string[] datasetLines)
        {
            long count = 0;

            foreach (var datasetLine in datasetLines)
            {
                long testValue;
                int[] numbersList;
                {
                    string[] temp = datasetLine.Split(':');
                    testValue = Int64.Parse(temp[0]);
                    numbersList = temp[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(a => Int32.Parse(a)).ToArray();
                }

                Operator currentOperatorString = new() { TestValue = testValue, Numbers = numbersList };
                if (currentOperatorString.TestCombinations(true))
                {
                    count += currentOperatorString.TestValue;
                }

            }

            return count.ToString();
        }
    }
}
