namespace AdventOfCode2024UnitTests
{
    [Ignore("Not implemented yet")]
    [TestFixture]
    public class Day15Tests
    {
        [Test]
        public void Part1_small()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay15_small.txt");
            var solution = new AdventOfCode2024Solutions.Day15.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("2028"));
        }

        [Test]
        public void Part1_large()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay15_large.txt");
            var solution = new AdventOfCode2024Solutions.Day15.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("10092"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay15.txt");
            var solution = new AdventOfCode2024Solutions.Day15.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo(""));
        }
    }
}