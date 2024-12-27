namespace AdventOfCode2024UnitTests
{
    public class Day16Tests
    {
        [Test]
        public void Part1Small()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay16small.txt");
            var solution = new AdventOfCode2024Solutions.Day16.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("7036"));
        }

        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay16.txt");
            var solution = new AdventOfCode2024Solutions.Day16.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("11048"));
        }

        [Test]
        public void Part2Small()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay16small.txt");
            var solution = new AdventOfCode2024Solutions.Day16.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("45"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay16.txt");
            var solution = new AdventOfCode2024Solutions.Day16.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("64"));
        }
    }
}