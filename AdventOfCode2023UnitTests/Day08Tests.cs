namespace AdventOfCode2023UnitTests
{
    [TestFixture]
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

        [TestCase(new long[] { 2, 3}, 6)]
        [TestCase(new long[] { 2, 5}, 10)]
        [TestCase(new long[] { 3, 5}, 15)]
        [TestCase(new long[] { 5, 5}, 5)]
        [TestCase(new long[] { 4, 6}, 12)]
        [TestCase(new long[] { 5, 10}, 10)]
        [TestCase(new long[] { 8, 10}, 40)]
        [TestCase(new long[] { 8, 10, 4}, 40)]
        [TestCase(new long[] { 2, 4, 5, 10}, 20)]
        public void Day8MathSupport_CalculateLowestCommonMultiplier_VariousTestCases(long[] input, long expectedOutout)
        {
            //act
            var result = AdventOfCode2023Solutions.Day08.Day8MathSupport.CalculateLowestCommonMultiplier(input);

            //assert
            Assert.That(result, Is.EqualTo(expectedOutout));
        }

        [TestCase(new long[] { 5, 12 }, 1)]
        [TestCase(new long[] { 12, 18 }, 6)]
        [TestCase(new long[] { 8, 12 }, 4)]
        [TestCase(new long[] { 8, 12, 2 }, 2)]
        [TestCase(new long[] { 8, 12, 4 }, 4)]
        [TestCase(new long[] { 8, 12, 16 }, 4)]
        [TestCase(new long[] { 8, 12, 16, 4 }, 4)]
        [TestCase(new long[] { 50, 125, 100, 1075 }, 25)]
        public void Day8MathSupport_CalculateGreatestCommonDivisor_ForArrayOfNumbers(long[] input, long expectedOutout)
        {
            // https://en.wikipedia.org/wiki/Greatest_common_divisor

            //act
            var result = AdventOfCode2023Solutions.Day08.Day8MathSupport.CalculateGreatestCommonDivisor(input);

            //assert
            Assert.That(result, Is.EqualTo(expectedOutout));
        }

        [TestCase(5, 12, 1)]
        [TestCase(12, 18, 6)]
        [TestCase(8, 12, 4)]
        public void Day8MathSupport_CalculateGreatestCommonDivisor_ForTwoNumber(long inputX, long inputY, long expectedOutout)
        {
            // https://en.wikipedia.org/wiki/Greatest_common_divisor

            //act
            var result = AdventOfCode2023Solutions.Day08.Day8MathSupport.CalculateGreatestCommonDivisor(inputX, inputY);

            //assert
            Assert.That(result, Is.EqualTo(expectedOutout));
        }
    }
}