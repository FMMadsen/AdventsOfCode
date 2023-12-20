namespace AdventOfCode2022UnitTests
{
    public class Day04Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("Day04SectionAssignments_test.txt");
            var solution = new AdventOfCode2022Solutions.Day04.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("2"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("Day04SectionAssignments_test.txt");
            var solution = new AdventOfCode2022Solutions.Day04.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("4"));
        }
    }
}