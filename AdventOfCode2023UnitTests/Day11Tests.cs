namespace AdventOfCode2023UnitTests
{
    public class Day11Tests
    {
        [Test]
        public void Part1_Example2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay11.txt");
            var solution = new AdventOfCode2023Solutions.Day11.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("374"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay11.txt");
            var solution = new AdventOfCode2023Solutions.Day11.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo(""));
        }
    }
}