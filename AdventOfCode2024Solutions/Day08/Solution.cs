using Common;
using ToolsFramework;

namespace AdventOfCode2024Solutions.Day08
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 8: Resonant Collinearity";

        public static bool PrintInitialMapToConsole => true;
        public static bool PrintFinalMapToConsole => true;
        public static bool PrintMapToConsoleEveryStep => false;

        

        public string SolvePart1(string[] datasetLines)
        {
            var antennaMap = new AntennaMap(datasetLines);
            return antennaMap.IdentifyAndCountAntinodes().ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            var antennaMap = new AntennaMap(datasetLines);
            return antennaMap.IdentifyAndCountAntinodes(true).ToString();
        }
    }
}
