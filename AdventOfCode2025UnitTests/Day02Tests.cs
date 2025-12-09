using AdventOfCode2025Solutions.Day02;

namespace AdventOfCode2025UnitTests
{
    [TestFixture]
    public class Day02Tests
    {
        Solution _sut;

        [SetUp]
        public void Setup() => _sut = new Solution();

        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay02.txt");

            //act
            var result = _sut.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("1227775554"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay02.txt");

            //act
            var result = _sut.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("4174379265"));
        }

        [TestCase("1")]
        [TestCase("12")]
        [TestCase("1011")]
        public void Validate_VALID_ProductNumbers(string productNumber)
        {
            // Act
            var result = Solution.ValidateProductNumberV1(productNumber);
            // Assert
            Assert.That(result, Is.True);
        }

        [TestCase("11")]
        [TestCase("22")]
        [TestCase("4040")]
        [TestCase("4444")]
        [TestCase("654654")]
        public void Validate_INVALID_ProductNumbers(string productNumber)
        {
            // Act
            var result = Solution.ValidateProductNumberV1(productNumber);
            // Assert
            Assert.That(result, Is.False);
        }
    }
}