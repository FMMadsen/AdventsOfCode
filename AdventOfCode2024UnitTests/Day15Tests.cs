namespace AdventOfCode2024UnitTests
{
    public class Day15Tests
    {
        [Test]
        public void Part1Small()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay15Small.txt");
            var solution = new AdventOfCode2024Solutions.Day15.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("2028"));
        }

        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay15.txt");
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
            Assert.That(result, Is.EqualTo("9021"));
        }
    }
}