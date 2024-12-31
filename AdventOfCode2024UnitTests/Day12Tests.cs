namespace AdventOfCode2024UnitTests
{
    [TestFixture]
    public class Day12Tests
    {
        [Test]
        public void Part1_example1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay12_example1.txt");
            var solution = new AdventOfCode2024Solutions.Day12.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("140"));
        }

        [Test]
        public void Part1_example2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay12_example2.txt");
            var solution = new AdventOfCode2024Solutions.Day12.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("772"));
        }

        [Test]
        public void Part1_example3()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay12_example3.txt");
            var solution = new AdventOfCode2024Solutions.Day12.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("1930"));
        }

        [Test]
        public void Part2_example1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay12_example1.txt");
            var solution = new AdventOfCode2024Solutions.Day12.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("80"));
        }

        [Test]
        public void Part2_example2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay12_example2.txt");
            var solution = new AdventOfCode2024Solutions.Day12.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("436"));
        }

        [Test]
        public void Part2_example3()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay12_example3.txt");
            var solution = new AdventOfCode2024Solutions.Day12.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("1206"));
        }

        [Test]
        public void Part2_example4()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay12_example4.txt");
            var solution = new AdventOfCode2024Solutions.Day12.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("236"));
        }

        [Test]
        public void Part2_example5()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay12_example5.txt");
            var solution = new AdventOfCode2024Solutions.Day12.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("368"));
        }
    }
}