namespace AdventOfCode2024UnitTests
{
    [Ignore("Not implemented yet")]
    [TestFixture]
    public class Day16Tests
    {
        [Test]
        public void Part1_example1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay16_example1.txt");
            var solution = new AdventOfCode2024Solutions.Day16.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("7036"));
        }

        [Test]
        public void Part1_example2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay16_example2.txt");
            var solution = new AdventOfCode2024Solutions.Day16.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("11048"));
        }

        [Test]
        public void Part2_example1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay16_example1.txt");
            var solution = new AdventOfCode2024Solutions.Day16.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("45"));
        }

        [Test]
        public void Part2_example2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay16_example2.txt");
            var solution = new AdventOfCode2024Solutions.Day16.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("64"));
        }
    }
}