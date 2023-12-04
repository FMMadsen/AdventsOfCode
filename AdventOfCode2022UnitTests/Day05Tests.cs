namespace AdventOfCode2022UnitTests
{
    public class Day05Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("Day05CraneAndSupplyStacks_test.txt");
            var solution = new AdventOfCode2022Solutions.Day05.Solution(dataset);

            //act
            var result = solution.SolvePart1();

            //assert
            Assert.That(result, Is.EqualTo("CMZ"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("Day05CraneAndSupplyStacks_test.txt");
            var solution = new AdventOfCode2022Solutions.Day05.Solution(dataset);

            //act
            var result = solution.SolvePart2();

            //assert
            Assert.That(result, Is.EqualTo("MCD"));
        }
    }
}