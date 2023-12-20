namespace AdventOfCode2022UnitTests
{
    public class Day07Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("Day07FileDirectorySizes_test.txt");
            var solution = new AdventOfCode2022Solutions.Day07.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("95437"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("Day07FileDirectorySizes_test.txt");
            var solution = new AdventOfCode2022Solutions.Day07.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("24933642"));
        }
    }
}