namespace AdventOfCode2023UnitTests
{
    public class Day02Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay02.txt");
            var solution = new AdventOfCode2023Solutions.Day02.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("8"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay02.txt");
            var solution = new AdventOfCode2023Solutions.Day02.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("2286"));
        }
    }
}