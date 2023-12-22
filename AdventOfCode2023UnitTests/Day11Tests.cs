namespace AdventOfCode2023UnitTests
{
    public class Day11Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay11.txt");
            var solution = new AdventOfCode2023Solutions.Day11.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("374"));
        }

        [TestCase(2, "374")]
        [TestCase(10, "1030")]
        [TestCase(100, "8410")]
        public void Part2(long expandingSize, string expectedDistance)
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay11.txt");
            var solution = new AdventOfCode2023Solutions.Day11.Solution();

            //act
            var result = solution.SolvePart2(dataset, expandingSize);

            //assert
            Assert.That(result, Is.EqualTo(expectedDistance));
        }
    }
}