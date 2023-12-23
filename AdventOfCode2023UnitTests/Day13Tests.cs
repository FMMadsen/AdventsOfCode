using AdventOfCode2023Solutions.Day13;

namespace AdventOfCode2023UnitTests
{
    public class Day13Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay13.txt");
            var solution = new AdventOfCode2023Solutions.Day13.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("405"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay13.txt");
            var solution = new AdventOfCode2023Solutions.Day13.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("400"));
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
    }
}