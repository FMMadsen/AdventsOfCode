namespace AdventOfCode2023UnitTests
{
    public class Day16Tests
    {
        [Test]
        public void Part1CustomExample1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay16CustomExample1.txt");
            var solution = new AdventOfCode2023Solutions.Day16.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("7"));
        }

        [Test]
        public void Part1CustomExample2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay16CustomExample2.txt");
            var solution = new AdventOfCode2023Solutions.Day16.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("8"));
        }

        [Test]
        public void Part1CustomExample3()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay16CustomExample3.txt");
            var solution = new AdventOfCode2023Solutions.Day16.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("28"));
        }

        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay16.txt");
            var solution = new AdventOfCode2023Solutions.Day16.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("46"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay16.txt");
            var solution = new AdventOfCode2023Solutions.Day16.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("51"));
        }
    }
}