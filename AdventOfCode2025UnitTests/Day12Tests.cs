using AdventOfCode2025UnitTests;

namespace AdventOfCode2025UnitTests
{
    public class Day12Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay12.txt");
            var solution = new AdventOfCode2025Solutions.Day12.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay12.txt");
            var solution = new AdventOfCode2025Solutions.Day12.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo(""));
        }
    }
}