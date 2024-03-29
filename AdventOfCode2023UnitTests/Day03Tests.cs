namespace AdventOfCode2023UnitTests
{
    public class Day03Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay03.txt");
            var solution = new AdventOfCode2023Solutions.Day03.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("4361"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay03.txt");
            var solution = new AdventOfCode2023Solutions.Day03.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("467835"));
        }
    }
}