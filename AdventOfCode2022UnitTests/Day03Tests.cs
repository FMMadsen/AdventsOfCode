namespace AdventOfCode2022UnitTests
{
    public class Day03Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("Day03RucksacksContent_test.txt");
            var solution = new AdventOfCode2022Solutions.Day03.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("157"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("Day03RucksacksContent_test.txt");
            var solution = new AdventOfCode2022Solutions.Day03.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("70"));
        }
    }
}