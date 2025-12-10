namespace AdventOfCode2025UnitTests
{
    [TestFixture]
    public class Day03Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay03.txt");
            var solution = new AdventOfCode2025Solutions.Day03.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("357"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay03.txt");
            var solution = new AdventOfCode2025Solutions.Day03.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("3121910778619"));
        }
    }
}