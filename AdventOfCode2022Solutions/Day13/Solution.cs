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
    public class Solution(string[] datasetLines) : IAOCSolution
    {
        public string PuzzleName => "Day 13: Distress Signal";
        public string[] DatasetLines => datasetLines;
        public bool DoPrintOut => false;

        public string SolvePart1()
        {
            var distressSignal = new DistressSignal(DatasetLines);

            if (DoPrintOut)
                PrintSolutionPartDetail(distressSignal);

            return "Not implemented";
        }

        public string SolvePart2()
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
