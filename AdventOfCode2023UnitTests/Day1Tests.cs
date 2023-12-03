using Common;

namespace AdventOfCode2023UnitTests
{
    public class Day1Tests
    {
        private const string dataSetDay1 = @"1abc2
pqr3stu8vwx
a1b2c3d4e5f
treb7uchet";

        private const string dataSetDay2 = @"two1nine
eightwothree
abcone2threexyz
xtwone3four
4nineeightseven2
zoneight234
7pqrstsixteen";

        [SetUp]
        public void Setup()
        {
        }

        [Test(Description = "Day 1: Trebuchet?!")]
        public void Part1()
        {
            //Prepare
            var dataset = DataSetRepository.SplitLinesIntoArray(dataSetDay1);
            var solution = new AdventOfCode2023Solutions.Day1.Solution(dataset);

            //act
            var result = solution.SolvePart1();

            //assert

            Assert.That(result, Is.EqualTo("142"));
        }

        [Test(Description = "Day 1: Trebuchet?!")]
        public void Part2()
        {
            //Prepare
            var dataset = DataSetRepository.SplitLinesIntoArray(dataSetDay2);
            var solution = new AdventOfCode2023Solutions.Day1.Solution(dataset);

            //act
            var result = solution.SolvePart2();

            //assert

            Assert.That(result, Is.EqualTo("281"));
        }
    }
}