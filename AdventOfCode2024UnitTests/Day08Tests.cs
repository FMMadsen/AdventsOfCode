namespace AdventOfCode2024UnitTests
{
    [TestFixture]
    public class Day08Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay08.txt");
            var solution = new AdventOfCode2024Solutions.Day08.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("14")); 
        }

        [Test]
        public void Part2_example_1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay08_p2_example1.txt");
            var solution = new AdventOfCode2024Solutions.Day08.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("9"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay08.txt");
            var solution = new AdventOfCode2024Solutions.Day08.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("34"));
        }
    }
}
