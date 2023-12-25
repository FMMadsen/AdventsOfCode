using Common;

namespace AdventOfCode2023Solutions.Day14
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 14: Parabolic Reflector Dish";

        public char[,]? PlatformTiltCycle1 { get; set; } = null;
        public char[,]? PlatformTiltCycle2 { get; set; } = null;
        public char[,]? PlatformTiltCycle3 { get; set; } = null;

        public string SolvePart1(string[] datasetLines)
        {
            var platform = new Platform(datasetLines);
            platform.TiltNorth();
            return platform.CalculateStonesSumWeight().ToString();
        }

        public void SolvePart2Cycle1_2_3(string[] datasetLines)
        {
            var platform = new Platform(datasetLines);

            platform.TiltCycle();
            PlatformTiltCycle1 = new char[platform.noOfRows, platform.noOfCols];
            Array.Copy(platform.map, PlatformTiltCycle1, platform.map.Length);

            platform.TiltCycle();
            PlatformTiltCycle2 = new char[platform.noOfRows, platform.noOfCols];
            Array.Copy(platform.map, PlatformTiltCycle2, platform.map.Length);

            platform.TiltCycle();
            PlatformTiltCycle3 = new char[platform.noOfRows, platform.noOfCols];
            Array.Copy(platform.map, PlatformTiltCycle3, platform.map.Length);
        }

        public string SolvePart2(string[] datasetLines)
        {
            var platform = new Platform(datasetLines);

            // NO! THIS WILL TAKE 7 FULL DAYS TO COMPLETE
            //for (int i = 0; i < 1000000000; i++)
            //    platform.TiltCycle();

            //Run cycles untill the sum no longer changes. Verify by check sum is equal to previous 3 sums
            var sumList = new Dictionary<int, string>();

            for (int i = 0; i <= 1000; i++)
                platform.TiltCycle();

            return platform.sumWeight.ToString();
        }
    }
}
