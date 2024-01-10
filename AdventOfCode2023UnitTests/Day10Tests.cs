using NUnit.Framework.Internal;

namespace AdventOfCode2023UnitTests
{
    public class Day10Tests
    {
        [Test]
        public void Part1Example1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay10Part1Example1.txt");
            var solution = new AdventOfCode2023Solutions.Day10.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("4"));
        }

        [Test]
        public void Part1Example2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay10Part1Example2.txt");
            var solution = new AdventOfCode2023Solutions.Day10.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("8"));
        }

        [Test]
        public void Part2Example1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay10Part2Example1.txt");
            var solution = new AdventOfCode2023Solutions.Day10.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("4"));
        }

        [Test]
        public void Part2Example2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay10Part2Example2.txt");
            var solution = new AdventOfCode2023Solutions.Day10.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("4"));
        }

        [Test]
        public void Part2Example3()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay10Part2Example3.txt");
            var solution = new AdventOfCode2023Solutions.Day10.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("8"));
        }

        [Test]
        public void Part2Example4()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay10Part1.txt");
            var solution = new AdventOfCode2023Solutions.Day10.Solution(dataset);

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("8"));
        }

        [Test]
        public void Part2Example5()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay10Part2.txt");
            var solution = new AdventOfCode2023Solutions.Day10.Solution(dataset);

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("10"));
        }
    }
}