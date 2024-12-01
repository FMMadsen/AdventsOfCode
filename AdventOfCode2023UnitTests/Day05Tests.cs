namespace AdventOfCode2023UnitTests
{
    public class Day05Tests
    {
        [Test]
        public void Part1Version1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay05.txt");

            //act
            var result = AdventOfCode2023Solutions.Day05.Solution.SolvePart1Version1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("35"));
        }

        [Test]
        public void Part1Version2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay05.txt");

            //act
            var result = AdventOfCode2023Solutions.Day05.Solution.SolvePart1Version2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("35"));
        }

        [Test]
        public void Part1Version3()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay05.txt");

            //act
            var result = AdventOfCode2023Solutions.Day05.Solution.SolvePart1Version3(dataset);

            //assert
            Assert.That(result, Is.EqualTo("35"));
        }

        [Test]
        public void Part2Version1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay05.txt");

            //act
            var result = AdventOfCode2023Solutions.Day05.Solution.SolvePart2Version1Runtime45Min(dataset);

            //assert
            Assert.That(result, Is.EqualTo("46"));
        }

        [Test]
        public void Part2Version2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay05.txt");

            //act
            var result = AdventOfCode2023Solutions.Day05.Solution.SolvePart2Version2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("46"));
        }

        [Test]
        public void Part2Version3()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay05.txt");

            //act
            var result = AdventOfCode2023Solutions.Day05.Solution.SolvePart2Version3(dataset);

            //assert
            Assert.That(result, Is.EqualTo("46"));
        }
    }
}