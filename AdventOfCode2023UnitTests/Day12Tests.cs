using AdventOfCode2023Solutions.Day12;

namespace AdventOfCode2023UnitTests
{
    public class Day12Tests
    {
        [Test]
        [Ignore("not yet ready")]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay12.txt");
            var solution = new AdventOfCode2023Solutions.Day12.Solution(dataset);

            //act
            var result = solution.SolvePart1();

            //assert
            Assert.That(result, Is.EqualTo("21"));
        }

        [Test]
        [Ignore("not yet ready")]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay12.txt");
            var solution = new AdventOfCode2023Solutions.Day12.Solution(dataset);

            //act
            var result = solution.SolvePart2();

            //assert
            Assert.That(result, Is.EqualTo(""));
        }

        [TestCase("####", "4")]
        public void SpringRow(string input, string expectedOutput)
        {
            //Prepare
            SpringRow springRow = new SpringRow(input);

            //act
            var sections = springRow.IdentifySectionsForTarget();
            var result = sections[0].SpringsString;

            //assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }
    }
}