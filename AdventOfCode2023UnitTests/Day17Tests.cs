namespace AdventOfCode2023UnitTests
{
    public class Day17Tests
    {
        [TestCase("TestDataSetDay17CustomExample_2x2.txt", 2, 2)]           // 1 ms
        [TestCase("TestDataSetDay17CustomExample_3x3.txt", 4, 12)]          // 1 ms
        [TestCase("TestDataSetDay17CustomExample_4x4.txt", 6, 148)]         // 1 ms
        [TestCase("TestDataSetDay17CustomExample_5x5.txt", 8, 2321)]        // 1 ms
        [TestCase("TestDataSetDay17CustomExample_6x6.txt", 10, 90066)]      // 86 ms
        //[TestCase("TestDataSetDay17CustomExample_7x7.txt", 16, 9712085)]    // 7,7 sec
        //[TestCase("TestDataSetDay17CustomExample_8x8.txt", 28, 90066)]
        //[TestCase("TestDataSetDay17CustomExample_9x9.txt", 24, 90066)]
        public void Part1_CustomExamples(string file, int expectedHeat, int expectedCombinations)
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet(file);
            var solution = new AdventOfCode2023Solutions.Day17.Solution();

            //act
            var result = solution.SolvePart1(dataset);
            var noOfRoutes = solution.numberOfRoutesFound;

            //assert
            Assert.That(noOfRoutes, Is.EqualTo(expectedCombinations));
            Assert.That(result, Is.EqualTo(expectedHeat.ToString()));
        }

        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay17.txt");
            var solution = new AdventOfCode2023Solutions.Day17.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("102"));
        }



        [Test]
        [Ignore("Not implemented")]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay17.txt");
            var solution = new AdventOfCode2023Solutions.Day17.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo(""));
        }
    }
}