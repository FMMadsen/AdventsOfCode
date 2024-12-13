namespace AdventOfCode2023UnitTests
{
    [TestFixture]
    [Ignore("Challenge not yet started implement")]
    public class Day24Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay24.txt");
            var solution = new AdventOfCode2023Solutions.Day24.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay24.txt");
            var solution = new AdventOfCode2023Solutions.Day24.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo(""));
        }
    }
}