using Common;

namespace AdventOfCode2024Solutions.Day09
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 9: Disk Fragmenter";

        public string SolvePart1(string[] datasetLines)
        {
            var disk = new Disk(datasetLines[0]);
            disk.CompressDisk();
            return disk.CalculateCompressedFilesChecksum().ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            return "To be implemented";
        }
    }
}
