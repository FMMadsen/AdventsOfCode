namespace AdventOfCode2022UnitTests
{
    [TestFixture]
    [Ignore("Challenge not yet started implement")]
    public class Day13Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("Day13DistressSignal_test.txt");
            var solution = new AdventOfCode2022Solutions.Day13.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("13"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("Day13DistressSignal_test.txt");
            var solution = new AdventOfCode2022Solutions.Day13.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo(""));
        }
    }
}