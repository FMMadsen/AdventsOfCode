namespace AdventOfCode2025UnitTests
{
    [TestFixture]
    public class Day04Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay04.txt");
            var solution = new AdventOfCode2025Solutions.Day04.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("13"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay04.txt");
            var solution = new AdventOfCode2025Solutions.Day04.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("43"));
        }
    }
}