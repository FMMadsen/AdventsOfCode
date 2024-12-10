using Common;
using static System.Reflection.Metadata.BlobBuilder;

namespace AdventOfCode2024Solutions.Day09
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 9: ";

        public string SolvePart1(string[] datasetLines)
        {
            long checksum = 0;
            StorageBlock[] blocks = UnpackBlock(datasetLines.First());

            FillLastToFront(blocks);

            int end = IndexFirstEmpty(blocks);
            for (int i = 0; i< end; i++)
            {
                checksum += (long)i * blocks[i].FileId;
            }

            return checksum.ToString();
        }

        public StorageBlock[] UnpackBlock(string storageBlocks)
        {
            // for files only amount: (aStorageBlock.Length + 1) / 2
            
            StorageBlock[] blocks = new StorageBlock[AdditiveString(storageBlocks)];

            for (int bi=0, p, i = 0; i < storageBlocks.Length; i++)
            {
                p = Int32.Parse( storageBlocks[i].ToString() );
                int id = -1;

                if (0 == i % 2)
                {
                    id = (i + 1) / 2;
                }

                while (0 < p)
                {
                    blocks[bi] = new StorageBlock() { FileId = id };

                    bi++;
                    p--;
                }
            }

            return blocks;
        }

        public int AdditiveString(string numbers)
        {
            int count = 0;
            foreach(char num in numbers)
            {
                count += Int32.Parse(num.ToString());
            }
            return count;
        }

        protected int IndexFirstEmpty(StorageBlock[] blocks, int beginAt = 0)
        {
            while (beginAt < blocks.Length)
            {
                if (-1 == blocks[beginAt].FileId)
                {
                    return beginAt;
                }

                beginAt++;
            }

            return beginAt;
        }

        protected int IndexLastFileBlock(StorageBlock[] blocks, int beginAt=-1)
        {
            if (beginAt < 0)
            {
                beginAt = blocks.Length - 1;
            }

            while (-1 < beginAt)
            {
                if (-1 < blocks[beginAt].FileId)
                {
                    return beginAt;
                }

                beginAt--;
            }

            return beginAt;
        }

        protected StorageBlock[] FillLastToFront(StorageBlock[] blocks)
        {
            int firstEmpty = IndexFirstEmpty(blocks); ;
            int lastFile = IndexLastFileBlock(blocks);

            while (firstEmpty < lastFile)
            {
                blocks[firstEmpty] = blocks[lastFile];
                blocks[lastFile] = new StorageBlock() { FileId = -1 };

                firstEmpty = IndexFirstEmpty(blocks, firstEmpty); ;
                lastFile = IndexLastFileBlock(blocks, lastFile);
            }

            return blocks;
        }

        protected void PrintUnpacked(StorageBlock[] blocks)
        {
            char[] toPrint = new char[blocks.Length];

            for (int i = 0; i < blocks.Length; i++)
            {
                if (-1 == blocks[i].FileId)
                {
                    toPrint[i] = '.';
                }
                else
                {
                    toPrint[i] = blocks[i].FileId.ToString()[0];
                }
            }
        }

        public string SolvePart2(string[] datasetLines)
        {
            long checksum = 0;
            StorageBlock[] blocks = UnpackBlock(datasetLines.First());

            FillFilesLastToFront(blocks);

            for (int i = 0; i < blocks.Length; i++)
            {
                if (-1 != blocks[i].FileId)
                {
                    checksum += (long)i * blocks[i].FileId;
                }
            }

            return checksum.ToString();
        }

        protected int IndexFirstAvailableSpace(StorageBlock[] blocks, int length)
        {
            int firstEmpty = IndexFirstEmpty(blocks);
            PrintUnpacked(blocks);
            while (firstEmpty < blocks.Length)
            {
                bool fits = true;
                int nextSearch = firstEmpty;

                for (int i = 0; i < length; i++)
                {
                    nextSearch = firstEmpty + i;

                    if (blocks.Length <= nextSearch || -1 != blocks[nextSearch].FileId)
                    {
                        fits = false; break;
                    }
                }

                if (fits)
                {
                    break;
                }

                firstEmpty = IndexFirstEmpty(blocks, nextSearch);
            }

            return firstEmpty;
        }

        protected StorageBlock[] FillFilesLastToFront(StorageBlock[] blocks)
        {
            int index = blocks.Length - 1;

            while (-1 < index)
            {
                int lastFile = IndexLastFileBlock(blocks, index);
                
                int fileLength = GetFileLengthFromLast(blocks, lastFile);
                int firstAvailable = IndexFirstAvailableSpace(blocks, fileLength);

                if (lastFile < firstAvailable)
                {
                    firstAvailable = blocks.Length;
                }

                if (firstAvailable < blocks.Length)
                {
                    
                    for (int i = lastFile - (fileLength - 1); i <= lastFile; i++)
                    {
                        blocks[firstAvailable] = blocks[i];
                        blocks[i] = new StorageBlock() { FileId = -1};
                        firstAvailable++;
                    }

                }
                
                index = lastFile - fileLength;

            }

            return blocks;
        }

        protected int GetFileLengthFromLast(StorageBlock[] blocks, int lastFile) 
        {
            int id = blocks[lastFile].FileId;
            int count = 0;

            while (-1 < lastFile && id == blocks[lastFile].FileId)
            {
                count++;
                lastFile--;
            }

            return count;
        }


    }
}
