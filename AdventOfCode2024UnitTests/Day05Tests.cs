namespace AdventOfCode2024UnitTests
{
    [Ignore("Not implemented yet")]
    [TestFixture]
    public class Day05Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay05.txt");
            var solution = new AdventOfCode2024Solutions.Day05.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("143"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay05.txt");
            var solution = new AdventOfCode2024Solutions.Day05.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("123"));
        }
    }
}