using Common;

namespace AdventOfCode2024Solutions.Day09
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 9: Disk Fragmenter";

        public static bool WriteDebugInfoToConsole { get; set; } = false;

        public string SolvePart1(string[] datasetLines)
        {
            var disk = new Disk(datasetLines[0]);
            disk.CompressDiskWithFragmenting();
            return disk.CalculateCompressedFilesChecksum().ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            var disk = new Disk(datasetLines[0]);
            disk.CompressDiskWithoutFragmenting();
            return disk.CalculateCompressedFilesChecksum().ToString();
        }

        public static void PrintInputDiskMap(int[] disk)
        {
            Console.WriteLine("Input disk map: " + string.Concat(disk));
        }

        public static void PrintCompressedDiskMap(int[] disk)
        {
            Console.WriteLine("Compressed disk map: " + string.Concat(disk));
        }

        public static void PrintDisk(int[] disk)
        {
            foreach (var block in disk)
            {
                if (block == -1)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(".");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write(block);
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }

        public static void PrintDisk(List<DiskBlock> diskBlocks)
        {
            foreach (var block in diskBlocks)
            {
                if (block is FreeSpace)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    for (int i = 0; i < block.Size; i++)
                        Console.Write(".");
                }
                else if (block is File)
                {
                    var file = (File)block;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = file.Color;
                    for (int i = 0; i < file.Size; i++)
                    {
                        if (file.Id > 9)
                            Console.Write("#");
                        else
                            Console.Write(file.Id);
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }
    }
}
