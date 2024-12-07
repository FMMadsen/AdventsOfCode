using AdventOfCode2024Solutions.Day07;

namespace AdventOfCode2024UnitTests
{
    public class Day07Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay07.txt");
            var solution = new AdventOfCode2024Solutions.Day07.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("3749"));
        }

        [TestCase("190: 10 19", 190)]
        [TestCase("3267: 81 40 27", 3267)]
        [TestCase("83: 17 5", 0)]
        [TestCase("156: 15 6", 0)]
        [TestCase("7290: 6 8 6 15", 0)]
        [TestCase("161011: 16 10 13", 0)]
        [TestCase("192: 17 8 14", 0)]
        [TestCase("21037: 9 7 18 13", 0)]
        [TestCase("292: 11 6 16 20", 292)]
        public void Part1_CalibrationEqueation_CanCalibrate2operators(string input, long expectedOutput)
        {
            //Prepare
            var equation = new CalibrationEquation(input);

            //act
            var result = equation.TryCalibrateWithTwoOperators();

            //assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay07.txt");
            var solution = new AdventOfCode2024Solutions.Day07.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo(""));
        }
    }
}