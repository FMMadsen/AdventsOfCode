namespace AdventOfCode2023UnitTests
{
    public class Day15Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay15.txt");
            var solution = new AdventOfCode2023Solutions.Day15.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("1320"));
        }

        [Test]
        public void Hash()
        {
            //Prepare

            //act
            var result = AdventOfCode2023Solutions.Day15.Solution.RaindeerHashing("HASH");

            //assert
            Assert.That(result, Is.EqualTo(52));
        }

        [TestCase("HASH", 52)]
        [TestCase("rn", 0)]
        [TestCase("cm", 0)]
        [TestCase("qp", 1)]
        [TestCase("pc", 3)]
        [TestCase("ot", 3)]
        [TestCase("ab", 3)]
        public void Hashed_Labels(string input, int expectedOutput)
        {
            //act
            var result = AdventOfCode2023Solutions.Day15.Solution.RaindeerHashing(input);

            //assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay15.txt");
            var solution = new AdventOfCode2023Solutions.Day15.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("145"));
        }
    }
}