namespace AdventsOfCode2022.Day07FileDirectorySizes
{
    internal class Day7Puzzle
    {
        public string DataSetFile => @"Datasets\Day07FileDirectorySizes.txt";

        private static int TotalDiscSpace = 70000000;
        private static int SpaceRequiredForUpdate = 30000000;

        private static Terminal DeviceTerminal = new Terminal();

        internal static string SolvePart1(string[] datasetLines, bool doPrintOut)
        {
            DeviceTerminal.AnalyzeTerminalHistory(datasetLines);
            
            var resultList = DeviceTerminal.SearchDirectory(maxSize: 100000);

            if (doPrintOut)
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

        internal static string SolvePart2(string[] datasetLines, bool doPrintOut)
        {
            int InitialDiskSpaceAvailable = TotalDiscSpace - DeviceTerminal.RootDirectory.DirectorySize;
            int SpaceRequiredToClean = SpaceRequiredForUpdate < InitialDiskSpaceAvailable ? 0 : SpaceRequiredForUpdate - InitialDiskSpaceAvailable;

            var directoryToDelete = DeviceTerminal.SearchSmallestDirectory(minSize: SpaceRequiredToClean);

            if (doPrintOut)
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
