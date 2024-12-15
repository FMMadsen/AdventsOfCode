namespace AdventOfCode2024UnitTests
{
    [Ignore("Not implemented yet")]
    [TestFixture]
    public class Day09Tests
    {
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
            Assert.That(result, Is.EqualTo(""));
        }
    }
}