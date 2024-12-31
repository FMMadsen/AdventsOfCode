namespace AdventOfCode2024UnitTests
{
    [Ignore("Not implemented yet")]
    [TestFixture]
    public class Day04Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay04.txt");
            var solution = new AdventOfCode2024Solutions.Day04.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("18"));
        }

        [Test]
        public void Part1_custom_case_1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay04_part1_custom_case1.txt");
            var solution = new AdventOfCode2024Solutions.Day04.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("2"));
        }

        [Test]
        public void Part1_custom_case_2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay04_part1_custom_case2.txt");
            var solution = new AdventOfCode2024Solutions.Day04.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("4"));
        }

        [Test]
        public void Part1_custom_case_3()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay04_part1_custom_case3.txt");
            var solution = new AdventOfCode2024Solutions.Day04.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("1"));
        }

        [TestCase("XMAS", "1")]
        [TestCase("XMAS SAMX", "2")]
        [TestCase("XMASSAMX", "2")]
        [TestCase("XMASAMX", "2")]
        [TestCase("oXMASoXMASo", "2")]
        [TestCase(" XMAS XMAS ", "2")]
        [TestCase("ooXMASoo", "1")]
        [TestCase("ooXMASoo", "1")]
        [TestCase("ooXMASSAMXoo", "2")]
        [TestCase("ooXMASAMXoo", "2")]
        [TestCase("ooSAMXMASAMXoo", "3")]
        [TestCase("ooXMASAMXMASAMXoo", "4")]
        [TestCase("ooXMAsoo", "0")]
        public void Part1_custom_examples(string input, string expectedOutput)
        {
            //Prepare
            var dataset = new string[] { input };
            var solution = new AdventOfCode2024Solutions.Day04.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay04.txt");
            var solution = new AdventOfCode2024Solutions.Day04.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("9"));
        }
    }
}