namespace AdventOfCode2023UnitTests
{
    public class Day08Tests
    {
        [Test]
        public void Part1Example1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay08Example1.txt");
            var solution = new AdventOfCode2023Solutions.Day08.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("2"));
        }

        [Test]
        public void Part1Example2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay08Example2.txt");
            var solution = new AdventOfCode2023Solutions.Day08.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("6"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay08Part2.txt");
            var solution = new AdventOfCode2023Solutions.Day08.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("6"));
        }
    }
}