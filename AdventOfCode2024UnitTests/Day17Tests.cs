namespace AdventOfCode2024UnitTests
{
    public class Day17Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay17.txt");
            var solution = new AdventOfCode2024Solutions.Day17.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("4,6,3,5,6,3,5,2,1,0"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay17.txt");
            var solution = new AdventOfCode2024Solutions.Day17.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo(""));
        }
    }
}