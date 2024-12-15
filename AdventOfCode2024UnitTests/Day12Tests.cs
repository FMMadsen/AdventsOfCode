namespace AdventOfCode2024UnitTests
{
    [Ignore("Not implemented yet")]
    [TestFixture]
    public class Day12Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay12.txt");
            var solution = new AdventOfCode2024Solutions.Day12.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("1930"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay12.txt");
            var solution = new AdventOfCode2024Solutions.Day12.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo(""));
        }
    }
}