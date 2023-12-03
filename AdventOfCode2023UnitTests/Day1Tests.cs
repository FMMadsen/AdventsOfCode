namespace AdventOfCode2023UnitTests
{
    public class Day1Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay01Part1.txt");
            var solution = new AdventOfCode2023Solutions.Day01.Solution(dataset);

            //act
            var result = solution.SolvePart1();

            //assert
            Assert.That(result, Is.EqualTo("142"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay01Part2.txt");
            var solution = new AdventOfCode2023Solutions.Day01.Solution(dataset);

            //act
            var result = solution.SolvePart2();

            //assert
            Assert.That(result, Is.EqualTo("281"));
        }
    }
}