namespace AdventOfCode2024UnitTests
{
    public class Day10Tests
    {
        [Test]
        public void Part1_example1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay10_example1.txt");
            var solution = new AdventOfCode2024Solutions.Day10.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("2"));
        }

        [Test]
        public void Part1_example2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay10_example2.txt");
            var solution = new AdventOfCode2024Solutions.Day10.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("4"));
        }

        [Test]
        public void Part1_example3()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay10_example3.txt");
            var solution = new AdventOfCode2024Solutions.Day10.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("3"));
        }

        [Test]
        public void Part1_example4()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay10_example4.txt");
            var solution = new AdventOfCode2024Solutions.Day10.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("36"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay10_example4.txt");
            var solution = new AdventOfCode2024Solutions.Day10.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo(""));
        }
    }
}