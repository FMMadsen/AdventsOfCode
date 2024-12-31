using ToolsFramework;

namespace AdventOfCode2024Solutions.Day09
{
    public class Disk
    {
        public int[] OriginalDiskMap { get; }
        public int[] DiskFragments { get; private set; }
        public List<DiskBlock> DiskBlocks { get; private set; }

        public Disk(string map)
        {
            if (string.IsNullOrWhiteSpace(map))
                throw new ArgumentException("Disk map cannot be empty");

            OriginalDiskMap = Tools.StringToIntArray(map);
            DiskFragments = new int[Tools.CountSumOfNumberArray(OriginalDiskMap)];
            DiskBlocks = [];

            if (Solution.WriteDebugInfoToConsole)
                Solution.PrintInputDiskMap(OriginalDiskMap);

            var fileID = 0;
            var isSpaces = false;
            var nextDiskBlockIndex = 0;
            foreach (var blockSize in OriginalDiskMap)
            {
                if (isSpaces)
                    DiskBlocks.Add(new FreeSpace(blockSize));
                else
                    DiskBlocks.Add(new File(blockSize, fileID));

                for (int i = 0; i < blockSize; i++)
                {
                    if (isSpaces)
                        DiskFragments[nextDiskBlockIndex++] = -1;
                    else
                        DiskFragments[nextDiskBlockIndex++] = fileID;
                }

                if (!isSpaces)
                    fileID++;

                isSpaces = !isSpaces;
            }

            if (Solution.WriteDebugInfoToConsole)
                Solution.PrintDisk(DiskFragments);

            if (Solution.WriteDebugInfoToConsole)
                Solution.PrintDisk(DiskBlocks);
        }

        public void CompressDiskWithFragmenting()
        {
            var lastOut = new Stack<int>();
            var numberOfCompressedFragments = Tools.CountSumOfEvery2nd(OriginalDiskMap);
            var diskBlocksCompressed = new int[numberOfCompressedFragments];

            foreach (var block in DiskFragments)
                if (block > -1)
                    lastOut.Push(block);

            for (int i = 0; i < diskBlocksCompressed.Length; i++)
            {
                if (DiskFragments[i] > -1)
                    diskBlocksCompressed[i] = DiskFragments[i];
                else
                    diskBlocksCompressed[i] = lastOut.Pop();
            }

            DiskFragments = diskBlocksCompressed;

            if (Solution.WriteDebugInfoToConsole)
                Solution.PrintCompressedDiskMap(DiskFragments);
        }

        public void CompressDiskWithoutFragmenting()
        {
            var highestFileID = DiskBlocks.OfType<File>().Max(x => x.Id);

            for (var i = highestFileID; i >= 0; i--)
            {
                TryMoveFile(i);

                if (Solution.WriteDebugInfoToConsole)
                    Solution.PrintDisk(DiskBlocks);
            }

            UpdateDiskFragments();

            if (Solution.WriteDebugInfoToConsole)
                Solution.PrintDisk(DiskFragments);
        }

        private void TryMoveFile(int id)
        {
            //Get the file to move
            var fileIndex = DiskBlocks.FindIndex(x => x is File && ((File)x).Id == id);
            var file = fileIndex > -1 ? DiskBlocks[fileIndex] : null;
            if (file == null)
                throw new Exception("File not found");

            //Find the first available space that can fit the file
            var firstAvailableSpaceIndex = DiskBlocks.FindIndex(x => x is FreeSpace && x.Size >= file.Size);
            if (firstAvailableSpaceIndex == -1 || firstAvailableSpaceIndex > fileIndex)
                return;
            var firstAvailableSpace = DiskBlocks[firstAvailableSpaceIndex];

            //Check if there are free spaces before and after the file
            var freeSpaceBefore = DiskBlocks[fileIndex - 1] as FreeSpace;
            var freeSpaceAfter = DiskBlocks.Count > fileIndex + 1 ? DiskBlocks[fileIndex + 1] as FreeSpace : null;

            //Extend freespace before or after the file
            if (freeSpaceBefore != null && freeSpaceAfter != null)
            {   //Merge with free space before and after
                freeSpaceBefore.Size += file.Size + freeSpaceAfter.Size;
                DiskBlocks.Remove(freeSpaceAfter);
            }
            else if (freeSpaceBefore != null && freeSpaceAfter == null)
            {   //Merge with free space before
                freeSpaceBefore.Size += file.Size;
            }
            else if (freeSpaceBefore == null && freeSpaceAfter != null)
            {   //Merge with free space after
                freeSpaceAfter.Size += file.Size;
            }
            else
            {   //No free space before or after, insert new
                DiskBlocks.Insert(fileIndex, new FreeSpace(file.Size));
            }

            //Move the file to the new location
            DiskBlocks.Remove(file);
            DiskBlocks.Insert(firstAvailableSpaceIndex, file);

            //Remove or reduce size of the free space
            if (firstAvailableSpace.Size == file.Size)
                DiskBlocks.Remove(firstAvailableSpace);
            else
                firstAvailableSpace.Size -= file.Size;
        }

        private void UpdateDiskFragments()
        {
            DiskFragments = new int[DiskBlocks.Sum(x => x.Size)];

            var nextDiskBlockIndex = 0;
            foreach (var block in DiskBlocks)
            {
                for (int i = 0; i < block.Size; i++)
                {
                    if (block is FreeSpace)
                        DiskFragments[nextDiskBlockIndex++] = -1;
                    else
                        DiskFragments[nextDiskBlockIndex++] = ((File)block).Id;
                }
            }
        }

        public long CalculateCompressedFilesChecksum()
        {
            long checksum = 0;
            for (int i = 0; i < DiskFragments.Length; i++)
                checksum += (DiskFragments[i] < 0 ? 0 : DiskFragments[i] * i);

            return checksum;
        }
    }
}
