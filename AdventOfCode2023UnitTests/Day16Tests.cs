namespace AdventOfCode2023UnitTests
{
    public class Day16Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay16.txt");
            var solution = new AdventOfCode2023Solutions.Day16.Solution(dataset);

            //act
            var result = solution.SolvePart1();

            //assert
            Assert.That(result, Is.EqualTo("46"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay16.txt");
            var solution = new AdventOfCode2023Solutions.Day16.Solution(dataset);

            //act
            var result = solution.SolvePart2();

            //assert
            Assert.That(result, Is.EqualTo(""));
        }
    }
}