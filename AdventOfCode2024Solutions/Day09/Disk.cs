namespace AdventOfCode2024Solutions.Day09
{
    public class Disk
    {
        public long[] OriginalDiskMap { get; }
        public long[] DiskBlocks { get; }
        public long[] DiskBlocksCompressed { get; }

        public Disk(string map)
        {
            if (string.IsNullOrWhiteSpace(map))
                throw new ArgumentException("Disk map cannot be empty");

            OriginalDiskMap = StringToIntArray(map);
            DiskBlocks = new long[CountSumOfNumberArray(OriginalDiskMap)];
            DiskBlocksCompressed = new long[CountSumOfEverySecond(OriginalDiskMap)];

            var fileID = 0;
            var isSpaces = false;
            var diskBlockIndex = 0;
            foreach (var mapEntry in OriginalDiskMap)
            {
                for (int i = 0; i < mapEntry; i++)
                {
                    if (isSpaces)
                        DiskBlocks[diskBlockIndex++] = -1;
                    else
                        DiskBlocks[diskBlockIndex++] = fileID;
                }
                if (!isSpaces)
                    fileID++;
                isSpaces = !isSpaces;
            }
        }

        public void CompressDisk()
        {
            Stack<long> lastOut = new Stack<long>();

            foreach (var block in DiskBlocks)
                if (block > -1)
                    lastOut.Push(block);

            for (int i = 0; i < DiskBlocksCompressed.Length; i++)
            {
                if (DiskBlocks[i] > -1)
                    DiskBlocksCompressed[i] = DiskBlocks[i];
                else
                    DiskBlocksCompressed[i] = lastOut.Pop();
            }
        }

        public long CalculateCompressedFilesChecksum()
        {
            long checksum = 0;
            for (int i = 0; i < DiskBlocksCompressed.Length; i++)
                checksum += DiskBlocksCompressed[i] * i;

            return checksum;
        }


        public static long[] StringToIntArray(string stringOfNumbers)
        {
            return stringOfNumbers.Select(x => long.Parse(x.ToString())).ToArray();
        }

        public static long CountSumOfEverySecond(long[] array)
        {
            return array.Where((element, index) => index % 2 == 0).Sum(x => x);
        }

        public static long CountSumOfNumberArray(long[] array)
        {
            return array.Sum();
        }
    }
}
