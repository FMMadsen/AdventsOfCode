using Common;

namespace AdventOfCode2024Solutions.Day10
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 10: Hoof It";

        public string SolvePart1(string[] datasetLines)
        {
            var map = new Map3D(datasetLines);
            var trails = map.IdentifyTrails();
            trails.ForEach(x => x.InitializeTrails());
            var sumScore = trails.Sum(x => x.Score);
            return sumScore.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            var map = new Map3D(datasetLines);
            var trails = map.IdentifyTrails();
            trails.ForEach(x => x.InitializeTrails());
            var sumRatings = trails.Sum(x => x.Rating);
            return sumRatings.ToString();
        }
    }
}
