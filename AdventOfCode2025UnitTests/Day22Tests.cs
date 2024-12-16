namespace AdventOfCode2025UnitTests
{
    [Ignore("Not implemented yet")]
    [TestFixture]
    public class Day22Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay22.txt");
            var solution = new AdventOfCode2025Solutions.Day22.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay22.txt");
            var solution = new AdventOfCode2025Solutions.Day22.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo(""));
        }
    }
}