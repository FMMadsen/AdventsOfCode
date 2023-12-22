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

            //print
            var resultData = solution.ResultData;
            if (resultData != null)
                TestDataWriter.ReadDataSet(resultData, $"Day10Part2_{DateTime.Now.ToString("yyMMdd-HHmmss")}_example1.txt");

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

            //print
            var resultData = solution.ResultData;
            if (resultData != null)
                TestDataWriter.ReadDataSet(resultData, $"Day10Part2_{DateTime.Now.ToString("yyMMdd-HHmmss")}_example2.txt");

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

            //print
            var resultData = solution.ResultData;
            if (resultData != null)
                TestDataWriter.ReadDataSet(resultData, $"Day10Part2_{DateTime.Now.ToString("yyMMdd-HHmmss")}_example3.txt");

            //assert
            Assert.That(result, Is.EqualTo("8"));
        }

        [Test]
        public void Part2Example4()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay10Part2Example4.txt");
            var solution = new AdventOfCode2023Solutions.Day10.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //print
            var resultData = solution.ResultData;
            if (resultData != null)
                TestDataWriter.ReadDataSet(resultData, $"Day10Part2_{DateTime.Now.ToString("yyMMdd-HHmmss")}_example4.txt");

            //assert
            Assert.That(result, Is.EqualTo("10"));
        }

        [Test]
        public void Part2Example5()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay10Part2Example5.txt");
            var solution = new AdventOfCode2023Solutions.Day10.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //print
            var resultData = solution.ResultData;
            if(resultData != null )
                TestDataWriter.ReadDataSet(resultData, $"Day10Part2_{DateTime.Now.ToString("yyMMdd-HHmmss")}_example5.txt");

            //assert
            Assert.That(result, Is.EqualTo("2"));
        }
    }
}