namespace AdventOfCode2023UnitTests
{
    [TestFixture]
    [Ignore("Challenge not yet started implement")]
    public class Day20Tests
    {
        [Test]
        public void Part1Example1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay20Example1.txt");
            var solution = new AdventOfCode2023Solutions.Day20.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("32000000"));
        }

        [Test]
        public void Part1Example2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay20Example2.txt");
            var solution = new AdventOfCode2023Solutions.Day20.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("11687500"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay20.txt");
            var solution = new AdventOfCode2023Solutions.Day20.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo(""));
        }
    }
}