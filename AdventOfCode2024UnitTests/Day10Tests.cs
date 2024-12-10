using AdventOfCode2024Solutions.Day10;

namespace AdventOfCode2024UnitTests
{
    public class Day10Tests
    {
        [TestCase("TestDataSetDay10_example1.txt", 1, 2)]
        [TestCase("TestDataSetDay10_example2.txt", 1, 5)]
        [TestCase("TestDataSetDay10_example3.txt", 2, 2)]
        [TestCase("TestDataSetDay10_example4.txt", 9, 7)]
        public void Map3d_initialize_correct(string inputMap, int expectedHeads, int expectedTops)
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet(inputMap);

            //act
            Map3D map = new(dataset);

            //assert
            Assert.That(map.NumberOfHeads, Is.EqualTo(expectedHeads));
            Assert.That(map.NumberOfTops, Is.EqualTo(expectedTops));
        }

        [TestCase("TestDataSetDay10_example1.txt", 2)]
        [TestCase("TestDataSetDay10_example2.txt", 4)]
        [TestCase("TestDataSetDay10_example3.txt", 3)]
        [TestCase("TestDataSetDay10_example4.txt", 36)]
        public void Trails_count_scopre_correct(string inputMap, int expectedSumScore)
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet(inputMap);
            Map3D map = new(dataset);
            var trails = map.IdentifyTrails();

            //act
            trails.ForEach(x => x.InitializeTrails());
            var sumScopre = trails.Sum(x => x.Score);

            //assert
            Assert.That(sumScopre, Is.EqualTo(expectedSumScore));
        }

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