using AdventOfCode2025Solutions.Day06;

namespace AdventOfCode2025UnitTests
{
    [TestFixture]
    public class Day06Tests
    {
        Solution _sut;

        [SetUp]
        public void Setup() => _sut = new Solution();

        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay06.txt");

            //act
            var result = _sut.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("4277556"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay06.txt");

            //act
            var result = _sut.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("3263827"));
        }
    }
}