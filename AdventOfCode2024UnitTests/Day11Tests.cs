using AdventOfCode2024Solutions.Day11;

namespace AdventOfCode2024UnitTests
{
    public class Day11Tests
    {
        [TestCase("125 17", 1, 3)]
        [TestCase("125 17", 2, 4)]
        [TestCase("125 17", 3, 5)]
        [TestCase("125 17", 4, 9)]
        [TestCase("125 17", 5, 13)]
        [TestCase("125 17", 6, 22)]
        public void Part1_various_cases(string input, int numberOfBlinks, int expectedNumberOfStones)
        {
            //Prepare
            Stones stones = new Stones(input);

            //act
            stones.Blink(numberOfBlinks);
            
            //assert
            Assert.That(stones.Count, Is.EqualTo(expectedNumberOfStones));
        }

        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay11.txt");
            var solution = new AdventOfCode2024Solutions.Day11.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("55312"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay11.txt");
            var solution = new AdventOfCode2024Solutions.Day11.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo(""));
        }
    }
}