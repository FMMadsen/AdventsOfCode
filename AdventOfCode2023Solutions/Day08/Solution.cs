using Common;
using System.Text.RegularExpressions;

namespace AdventOfCode2023Solutions.Day08
{
    public class Directions
    {
        private int[] DirectionString;
        private int DirectionIndex = 0;

        public int NextDirection
        {
            get
            {
                int index = DirectionIndex;
                DirectionIndex++;
                if(DirectionString.Length <= DirectionIndex) { DirectionIndex = 0; }
                return DirectionString[index];
            }
        }

        public Directions(int[] directions)
        {
            DirectionString = directions;
        }

        public Directions(string directions)
        {
            DirectionString = directions.ToCharArray().Select(a => a=='L'?0: a=='R'?1:-1).ToArray();
        }
    }

    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 8: ";
        string[] DatasetLines;

        public readonly Directions Directions;

        public readonly Dictionary<string, string[]> Map = new Dictionary<string, string[]>();

        public Solution(string[] datasetLines)
        {
            DatasetLines = datasetLines;

            Directions = new Directions(DatasetLines[0].Trim());

            Regex pattern = new Regex(@"\w\w\w");

            int i = 0;
            while (!DatasetLines[i].StartsWith("AAA")){ i++; }
            while(i < DatasetLines.Length)
            {
                MatchCollection match = pattern.Matches(DatasetLines[i]);
                Map.Add(match[0].Value, [match[1].Value, match[2].Value]);

                i++;
            }
        }

        public string SolvePart1()
        {
            int attempts = 0;
            string key = "AAA";
            while ("ZZZ" != key)
            {
                attempts++;
                key = Map[key][Directions.NextDirection];
            }
            return attempts.ToString();
        }

        public string SolvePart2()
        {
            return "To be implemented";
        }

        private void BuildInstructions()
        {

        }
    }
}
