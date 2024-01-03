using Common;

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
                DirectionIndex++;
                if(DirectionString.Length <= DirectionIndex) { DirectionIndex = 0; }
                return DirectionString[DirectionIndex];
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

        public Directions Directions { get; set; }

        public Solution(string[] datasetLines)
        {
            DatasetLines = datasetLines;

            Directions = new Directions(DatasetLines[0].Trim());
        }

        public string SolvePart1()
        {

            return "To be implemented";
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
