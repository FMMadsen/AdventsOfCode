using AdventOfCode2024Solutions.Day09;

namespace AdventOfCode2024UnitTests
{
    [TestFixture]
    public class Day09Tests
    {
        [TestCase("5", new long[] { 0, 0, 0, 0, 0 })]
        [TestCase("512", new long[] { 0, 0, 0, 0, 0, -1, 1, 1 })]
        [TestCase("12345", new long[] { 0, -1, -1, 1, 1, 1, -1, -1, -1, -1, 2, 2, 2, 2, 2 })]
        public void Disk_Construct_CreateDiskBlockList(string input, long[] expectedOutput)
        {
            //Prepare
            var disk = new Disk(input);

            //act
            var result = disk.DiskFragments;

            //assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [TestCase("5", new long[] { 0, 0, 0, 0, 0 })]
        [TestCase("512", new long[] { 0, 0, 0, 0, 0, 1, 1 })]
        [TestCase("12345", new long[] { 0, 2, 2, 1, 1, 1, 2, 2, 2 })]
        [TestCase("2333133121414131402", new long[] { 0, 0, 9, 9, 8, 1, 1, 1, 8, 8, 8, 2, 7, 7, 7, 3, 3, 3, 6, 4, 4, 6, 5, 5, 5, 5, 6, 6 })]
        public void Disk_Compress(string input, long[] expectedOutput)
        {
            //Prepare
            var disk = new Disk(input);

            //act
            disk.CompressDiskWithFragmenting();
            var result = disk.DiskFragments;

            //assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [TestCase("5", 0)]      //00000                     --> 00000           --> 0x1 + 1*1 + 2*1 + 3*1 + 4*1 = 0
        [TestCase("55", 0)]     //00000.....                --> 00000           --> 0x1 + 1*1 + 2*1 + 3*1 + 4*1 = 0
        [TestCase("555", 35)]   //00000.....11111           --> 0000011111      --> 5x1 + 6*1 + 7*1 + 8*1 + 9*1 = 5+6+7+8+9 = 35
        [TestCase("5555", 35)]  //00000.....11111.....      --> 0000011111      --> 5x1 + 6*1 + 7*1 + 8*1 + 9*1 = 5+6+7+8+9 = 35
        [TestCase("55555", 130)]//00000.....11111.....22222--> 000002222211111 --> 5x2 + 6*2 + 7*2 + 8*2 + 9*2 + 10x1 + 11*1 + 12*1 + 13*1 + 14*1 = 10+12+14+16+18+10+11+12+13+14 = 130
        public void Part1_CompressDisk_CalculateCorerctChecksum(string input, long expectedOutput)
        {
            //Prepare
            var disk = new Disk(input);
            var nonCompressedBlocks = disk.DiskFragments;

            //act
            disk.CompressDiskWithFragmenting();
            var compressedBlocks = disk.DiskFragments;
            var result = disk.CalculateCompressedFilesChecksum();

            //assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay09.txt");
            var solution = new AdventOfCode2024Solutions.Day09.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("1928"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay09.txt");
            var solution = new AdventOfCode2024Solutions.Day09.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("2858"));
        }
    }
}