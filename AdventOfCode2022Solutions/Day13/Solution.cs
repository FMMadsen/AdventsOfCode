using AdventsOfCode2022.Day13DistressSignal;
using Common;

namespace AdventOfCode2022Solutions.Day13
{
    /// <summary>
    /// Task introduction: 08:00 - 08:15
    /// Time consumption PART 1: 23:43 - 
    /// Time consumption PART 2: 
    /// Time consumption TOTAL: 
    /// </summary>
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 13: Distress Signal";
        public bool DoPrintOut => false;

        public string SolvePart1(string[] datasetLines)
        {
            var distressSignal = new DistressSignal(datasetLines);

            if (DoPrintOut)
                PrintSolutionPartDetail(distressSignal);

            return "Not implemented";
        }

        public string SolvePart2(string[] datasetLines)
        {
            if (DoPrintOut)
                PrintSolutionPartDetail(datasetLines);

            return "Not implemented";
        }

        private static void PrintSolutionPartDetail(DistressSignal distressSignal)
        {
            Console.WriteLine($"Print solution details - DistressSignal objects:");

            int count = 0;
            foreach (var packagePair in distressSignal.PackagePairs)
            {
                count++;
                Console.WriteLine($"Pair {count}: {packagePair.PackageLeft.PackageString} {packagePair.PackageRight.PackageString}");
            }
        }

        private static void PrintSolutionPartDetail(string[] datasetLines)
        {
            Console.WriteLine($"Print solution details - raw values:");

            foreach (var line in datasetLines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
