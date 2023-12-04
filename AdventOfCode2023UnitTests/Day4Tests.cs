namespace AdventOfCode2023UnitTests
{
    public class Day4Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay04Part1.txt");
            var solution = new AdventOfCode2023Solutions.Day04.Solution(dataset);

            //act
            var result = solution.SolvePart1();

            //assert
            Assert.That(result, Is.EqualTo("13"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay04Part2.txt");
            var solution = new AdventOfCode2023Solutions.Day04.Solution(dataset);

            //act
            var result = solution.SolvePart2();

            //assert
            Assert.That(result, Is.EqualTo("30"));
        }
    }
}