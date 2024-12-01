using AdventsOfCode2022.Day07FileDirectorySizes;
using Common;

namespace AdventOfCode2022Solutions.Day07
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 7: No Space Left On Device";
        public bool DoPrintOut => false;

        private static int TotalDiscSpace = 70000000;
        private static int SpaceRequiredForUpdate = 30000000;
        private static Terminal DeviceTerminal = new Terminal();


        public string SolvePart1(string[] datasetLines)
        {
            DeviceTerminal.AnalyzeTerminalHistory(datasetLines);

            var resultList = DeviceTerminal.SearchDirectory(maxSize: 100000);

            if (DoPrintOut)
            {
                DeviceTerminal.PrintFilesystem();
            }

            int response = 0;
            foreach (var result in resultList)
            {
                response += result.DirectorySize;
            }

            return response.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            int InitialDiskSpaceAvailable = TotalDiscSpace - DeviceTerminal.RootDirectory.DirectorySize;
            int SpaceRequiredToClean = SpaceRequiredForUpdate < InitialDiskSpaceAvailable ? 0 : SpaceRequiredForUpdate - InitialDiskSpaceAvailable;

            var directoryToDelete = DeviceTerminal.SearchSmallestDirectory(minSize: SpaceRequiredToClean);

            if (DoPrintOut)
            {
                Console.WriteLine("---- PART 2 ----");
                Console.WriteLine($"Total disc space: {TotalDiscSpace:N0}");
                Console.WriteLine($"Space required for update: {SpaceRequiredForUpdate:N0}");
                Console.WriteLine($"Initially total space used: {DeviceTerminal.RootDirectory.DirectorySize:N0}");
                Console.WriteLine($"Initially space available: {InitialDiskSpaceAvailable:N0}");
                Console.WriteLine($"Initially space required to clean: {SpaceRequiredToClean:N0}");
                Console.WriteLine($"Found directory to delete: {directoryToDelete?.Path} {directoryToDelete?.DirectorySize:N0}");

                Console.WriteLine("----------------");
            }

            return directoryToDelete != null ? directoryToDelete.DirectorySize.ToString() : "[none found]";
        }
    }
}
