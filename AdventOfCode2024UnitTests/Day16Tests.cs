using AdventOfCode2024Solutions.Day16;

namespace AdventOfCode2024UnitTests
{
    [TestFixture]
    public class Day16Tests
    {
        [Test]
        public void Part1_example1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay16_example1.txt");
            Map map = new Map(dataset);

            //act
            var score = map.StartRace();

            //assert
            Assert.That(score, Is.EqualTo(7036));
        }

        [Test]
        public void Part1_example2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay16_example2.txt");
            Map map = new Map(dataset);

            //act
            var score = map.StartRace();

            //assert
            Assert.That(score, Is.EqualTo(11048));
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
            Assert.That(result, Is.EqualTo(""));
        }
    }
}