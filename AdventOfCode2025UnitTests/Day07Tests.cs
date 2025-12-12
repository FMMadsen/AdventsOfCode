namespace AdventOfCode2025UnitTests
{
    [TestFixture]
    public class Day07Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay07.txt");
            var solution = new AdventOfCode2025Solutions.Day07.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("21"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay07.txt");
            var solution = new AdventOfCode2025Solutions.Day07.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("40"));
        }
    }
}