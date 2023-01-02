namespace AdventsOfCode2022.Day13DistressSignal
{
    /// <summary>
    /// Task introduction: 08:00 - 08:15
    /// Time consumption PART 1: 23:43 - 
    /// Time consumption PART 2: 
    /// Time consumption TOTAL: 
    /// </summary>
    internal class Day13Puzzle
    {
        internal static string SolvePart1(string[] distressSignalStrings, bool doPrintOut)
        {
            var distressSignal = new DistressSignal(distressSignalStrings);

            if (doPrintOut)
                PrintSolutionPartDetail(distressSignal);

            return string.Empty;
        }

        internal static string SolvePart2(string[] datasetLines, bool doPrintOut)
        {


            if (doPrintOut)
                PrintSolutionPartDetail(datasetLines);

            return string.Empty;
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
