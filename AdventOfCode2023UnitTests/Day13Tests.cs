using AdventOfCode2023Solutions.Day13;

namespace AdventOfCode2023UnitTests
{
    public class Day13Tests
    {
        [Test]
        public void Part1OfficialExample()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay13Example1.txt");
            var solution = new AdventOfCode2023Solutions.Day13.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("405"));
        }

        [Test]
        public void Part1UnofficialExample()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay13Example2.txt");
            var solution = new AdventOfCode2023Solutions.Day13.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("709"));
        }

        [Test]
        public void Part2OfficialExample()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay13Example1.txt");
            var solution = new AdventOfCode2023Solutions.Day13.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("400"));
        }

        [Test]
        public void Part2UnofficialExample()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay13Example2.txt");
            var solution = new AdventOfCode2023Solutions.Day13.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("1400"));
        }

        [Test]
        public void TransposeMatrix90Degrees()
        {
            //Prepare
            string[] input = new string[4];
            input[0] = "YE";
            input[1] = "RC";
            input[2] = "EI";
            input[3] = "VN";

            //act
            var result = PatternNote.TransposeMatrix90Degrees(input);

            //assert
            Assert.That(result.Length, Is.EqualTo(2));
            Assert.That(result[0], Is.EqualTo("VERY"));
            Assert.That(result[1], Is.EqualTo("NICE"));
        }

        [TestCase("ABC","ABX",0,true)]
        [TestCase("SYSTEM", "SYSTEM", 0,false)]
        [TestCase("SYSTEM", "SYSTEM", 1,false)]
        [TestCase("SYSTEM", "SYSTEM", 2,false)]
        [TestCase("ABC", "ABX", 1, false)]
        [TestCase("ABC", "ABX", 2, false)]
        [TestCase("ABC", "AXX", 2, false)]
        [TestCase("ABC", "XXX", 2, true)]
        public void TestStringsAreDifferent_WithDiffTolerance(string string1, string string2, int diffTolerance, bool expectedResult)
        {
            //act
            var result = PatternNote.TestStringsAreDifferent(string1, string2, diffTolerance);

            //assert
            Assert.That(result, Is.EqualTo(expectedResult));

        }
    }
}