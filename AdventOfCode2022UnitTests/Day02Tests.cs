namespace AdventOfCode2022UnitTests
{
    public class Day02Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("Day02RPSGameLog_test.txt");
            var solution = new AdventOfCode2022Solutions.Day02.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("15"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("Day02RPSGameLog_test.txt");
            var solution = new AdventOfCode2022Solutions.Day02.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("12"));
        }
    }
}