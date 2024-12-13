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
            Pepples stones = new Pepples(input);

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

        /// <summary>
        /// 0 0 12 13
        /// 1 1 1 2 1 3
        /// 2024 2024 2024 4048 2024 6072
        /// 20 24 20 24 20 24 40 48 20 24 30 78
        /// 2 0 2 4 2 0 2 4 2 0 2 4 4 0 4 8 2 0 2 4 3 0 7 8
        /// </summary>
        [TestCase("1 0 12 12", 0, 4)]
        [TestCase("1 0 12 13", 1, 6)]
        [TestCase("1 0 12 13", 2, 7)]
        [TestCase("1 0 12 13", 3, 14)]
        [TestCase("1 0 12 13", 4, 24)]
        public void Part2_my_cases(string input, int numberOfBlinks, int expectedNumberOfStones)
        {
            //Prepare
            Pluto pluto = new Pluto(input);

            //act
            pluto.Blink(numberOfBlinks);

            //assert
            Assert.That(pluto.TotalNumberOfStones, Is.EqualTo(expectedNumberOfStones));
        }

        [TestCase("125 17", 0, 2)]
        [TestCase("125 17", 1, 3)]
        [TestCase("125 17", 2, 4)]
        [TestCase("125 17", 3, 5)]
        [TestCase("125 17", 4, 9)]
        [TestCase("125 17", 5, 13)]
        [TestCase("125 17", 6, 22)]
        public void Part2_various_cases(string input, int numberOfBlinks, int expectedNumberOfStones)
        {
            //Prepare
            Pluto pluto = new Pluto(input);

            //act
            pluto.Blink(numberOfBlinks);

            //assert
            Assert.That(pluto.TotalNumberOfStones, Is.EqualTo(expectedNumberOfStones));
        }
    }
}