namespace AdventOfCode2022UnitTests
{
    public class Day01Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("Day01ElvesInventoryList_test.txt");
            var solution = new AdventOfCode2022Solutions.Day01.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("24000"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("Day01ElvesInventoryList_test.txt");
            var solution = new AdventOfCode2022Solutions.Day01.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("25000"));
        }
    }
}