namespace AdventOfCode2023UnitTests
{
    public class Day05Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay05.txt");
            var solution = new AdventOfCode2023Solutions.Day05.Solution(dataset);

            //act
            var result = solution.SolvePart1();

            //assert
            Assert.That(result, Is.EqualTo("35"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay05.txt");
            var solution = new AdventOfCode2023Solutions.Day05.Solution(dataset);

            //act
            var result = solution.SolvePart2ForUnitTest();

            //assert
            Assert.That(result, Is.EqualTo("46"));
        }
    }
}