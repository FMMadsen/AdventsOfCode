namespace AdventOfCode2024UnitTests
{
    public class Day14Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay14.txt");
            var solution = new AdventOfCode2024Solutions.Day14.Solution();

            //act
            var result = solution.SolvePart1(dataset, true);

            //assert
            Assert.That(result, Is.EqualTo("12"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay14.txt");
            var solution = new AdventOfCode2024Solutions.Day14.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo(""));
        }
    }
}