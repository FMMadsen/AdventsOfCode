namespace AdventOfCode2023UnitTests
{
    public class Day12Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay12.txt");
            var solution = new AdventOfCode2023Solutions.Day12.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("21"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay12.txt");
            var solution = new AdventOfCode2023Solutions.Day12.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("525152"));
        }
    }
}