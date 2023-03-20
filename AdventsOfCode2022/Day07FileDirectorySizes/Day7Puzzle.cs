namespace AdventsOfCode2022.Day07FileDirectorySizes
{
    internal class Day7Puzzle
    {
        internal static string SolvePart1(string[] datasetLines, bool doPrintOut)
        {
            Terminal terminal = new Terminal();

            foreach(string line in datasetLines)
            {
                if(line.StartsWith('$'))
                {
                    string command = line.Substring(2);

                    if(command.ToLower().StartsWith("cd"))
                    {
                        terminal.ChangeDirectory(command);
                    }
                }
                else
                {
                    terminal.CreateChildItems(line);
                }
            }

            terminal.CalculateFileAndDirectorySizes();

            var resultList = terminal.SearchDirectory(100000);

            if (doPrintOut)
            {
                terminal.PrintFilesystem();

                foreach(var result in resultList)
                {
                    Console.WriteLine($"{result.Path}");
                }
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
            return "";
        }
    }
}
