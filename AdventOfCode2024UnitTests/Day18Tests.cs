namespace AdventOfCode2024UnitTests
{
    [Ignore("Not implemented yet")]
    [TestFixture]
    public class Day18Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay18txt");
            var solution = new AdventOfCode2024Solutions.Day18.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay18.txt");
            var solution = new AdventOfCode2024Solutions.Day18.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo(""));
        }
    }
}