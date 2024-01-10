using NUnit.Framework.Internal;

namespace AdventOfCode2023UnitTests
{
    public class Day10Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay10Part1.txt");
            var solution = new AdventOfCode2023Solutions.Day10.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("8"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay10Part2.txt");
            var solution = new AdventOfCode2023Solutions.Day10.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("10"));
        }
    }
}