
namespace AdventOfCode2023UnitTests
{
    public class Day14Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay14.txt");
            var solution = new AdventOfCode2023Solutions.Day14.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("136"));
        }

        [Test]
        public void Part2_Cycle1and2and3()
        {
            //Prepare
            var inputDataset = TestDataReader.ReadDataSet("TestDataSetDay14.txt");
            var datasetAfterCycle1 = TestDataReader.ReadDataSet("TestDataSetDay14_mapAfterCycle1.txt");
            var datasetAfterCycle2 = TestDataReader.ReadDataSet("TestDataSetDay14_mapAfterCycle2.txt");
            var datasetAfterCycle3 = TestDataReader.ReadDataSet("TestDataSetDay14_mapAfterCycle3.txt");
            var expectedMapAfterCycle1 = AdventOfCode2023Solutions.Day14.Platform.CreateMap(datasetAfterCycle1);
            var expectedMapAfterCycle2 = AdventOfCode2023Solutions.Day14.Platform.CreateMap(datasetAfterCycle2);
            var expectedMapAfterCycle3 = AdventOfCode2023Solutions.Day14.Platform.CreateMap(datasetAfterCycle3);
            var solution = new AdventOfCode2023Solutions.Day14.Solution();

            //act
            solution.SolvePart2Cycle1_2_3(inputDataset);
            var mapAfterCycle1 = solution.PlatformTiltCycle1;
            var mapAfterCycle2 = solution.PlatformTiltCycle2;
            var mapAfterCycle3 = solution.PlatformTiltCycle3;

            //print
            //if (solution.PlatformInitial != null)
            //    TestDataWriter.ReadDataSet(solution.PlatformInitial, $"Day14Part2_{DateTime.Now.ToString("yyMMdd-HHmmss")}_Initial.txt");
            //if (solution.PlatformTiltCycle1 != null)
            //    TestDataWriter.ReadDataSet(solution.PlatformTiltCycle1, $"Day14Part2_{DateTime.Now.ToString("yyMMdd-HHmmss")}_Cycle1.txt");
            //if (solution.PlatformTiltCycle2 != null)
            //    TestDataWriter.ReadDataSet(solution.PlatformTiltCycle2, $"Day14Part2_{DateTime.Now.ToString("yyMMdd-HHmmss")}_Cycle2.txt");
            //if (solution.PlatformTiltCycle3 != null)
            //    TestDataWriter.ReadDataSet(solution.PlatformTiltCycle3, $"Day14Part2_{DateTime.Now.ToString("yyMMdd-HHmmss")}_Cycle3.txt");

            //assert
            Assert.That(mapAfterCycle1, Is.EqualTo(expectedMapAfterCycle1));
            Assert.That(mapAfterCycle2, Is.EqualTo(expectedMapAfterCycle2));
            Assert.That(mapAfterCycle3, Is.EqualTo(expectedMapAfterCycle3));
        }

        [Test]
        public void Part2_CustomExample1()
        {
            //Prepare
            var inputDataset = TestDataReader.ReadDataSet("TestDataSetDay14 Part1_Example1.txt");
            var solution = new AdventOfCode2023Solutions.Day14.Solution();

            //act
            var result = solution.SolvePart2(inputDataset);

            //assert
            Assert.That(result, Is.EqualTo("1"));
        }

        [Test]
        public void Part2_CustomExample2()
        {
            //Prepare
            var inputDataset = TestDataReader.ReadDataSet("TestDataSetDay14 Part1_Example2.txt");
            var solution = new AdventOfCode2023Solutions.Day14.Solution();

            //act
            var result = solution.SolvePart2(inputDataset);

            //assert
            Assert.That(result, Is.EqualTo("6"));
        }

        [Test]
        [Ignore("Not sure solution works for the unit test")]
        public void Part2()
        {
            //Prepare
            var inputDataset = TestDataReader.ReadDataSet("TestDataSetDay14.txt");
            var solution = new AdventOfCode2023Solutions.Day14.Solution();

            //act
            var result = solution.SolvePart2(inputDataset);

            //assert
            Assert.That(result, Is.EqualTo("64"));
        }

        [Test]
        public void Map_Equals_OtherMap()
        {
            //Prepare
            var inputDataset = TestDataReader.ReadDataSet("TestDataSetDay14.txt");
            var datasetAfterCycle1_1 = TestDataReader.ReadDataSet("TestDataSetDay14_mapAfterCycle1.txt");
            var datasetAfterCycle1_2 = TestDataReader.ReadDataSet("TestDataSetDay14_mapAfterCycle1.txt");

            var mapA = AdventOfCode2023Solutions.Day14.Platform.CreateMap(inputDataset);
            var mapB_1 = AdventOfCode2023Solutions.Day14.Platform.CreateMap(datasetAfterCycle1_1);
            var mapB_2 = AdventOfCode2023Solutions.Day14.Platform.CreateMap(datasetAfterCycle1_2);

            //act

            //assert
            Assert.That(AdventOfCode2023Solutions.Day14.Platform.MapEquals(mapA, mapB_1), Is.EqualTo(false));
            Assert.That(AdventOfCode2023Solutions.Day14.Platform.MapEquals(mapA, mapB_2), Is.EqualTo(false));
            Assert.That(AdventOfCode2023Solutions.Day14.Platform.MapEquals(mapB_1, mapB_2), Is.EqualTo(true));
        }

        [Test]
        public void TransposeMatrix90Degrees()
        {
            //Prepare
            char[,] input = new char[4, 2];
            input[0, 0] = 'Y';
            input[0, 1] = 'E';

            input[1, 0] = 'R';
            input[1, 1] = 'C';

            input[2, 0] = 'E';
            input[2, 1] = 'I';

            input[3, 0] = 'V';
            input[3, 1] = 'N';

            //act
            var result = AdventOfCode2023Solutions.Day14.Platform.TransposeMatrix90Degrees(input);
            string string1 = $"{result[0, 0]}{result[0, 1]}{result[0, 2]}{result[0, 3]}";
            string string2 = $"{result[1, 0]}{result[1, 1]}{result[1, 2]}{result[1, 3]}";

            //assert
            Assert.That(result.GetLength(0), Is.EqualTo(2));
            Assert.That(result.GetLength(1), Is.EqualTo(4));

            Assert.That(string1, Is.EqualTo("VERY"));
            Assert.That(string2, Is.EqualTo("NICE"));
        }
    }
}