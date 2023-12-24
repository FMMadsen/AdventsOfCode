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
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay14.txt");
            var solution = new AdventOfCode2023Solutions.Day14.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //print
            //if (solution.PlatformInitial != null)
            //    TestDataWriter.ReadDataSet(solution.PlatformInitial, $"Day14Part2_{DateTime.Now.ToString("yyMMdd-HHmmss")}_Initial.txt");
            //if (solution.PlatformTiltetNorth != null)
            //    TestDataWriter.ReadDataSet(solution.PlatformTiltetNorth, $"Day14Part2_{DateTime.Now.ToString("yyMMdd-HHmmss")}_Tiltet1North.txt");

            //if (solution.PlatformTiltetWest != null)
            //    TestDataWriter.ReadDataSet(solution.PlatformTiltetWest, $"Day14Part2_{DateTime.Now.ToString("yyMMdd-HHmmss")}_Tiltet2West.txt");
            
            //if (solution.PlatformTiltetSouth != null)
            //    TestDataWriter.ReadDataSet(solution.PlatformTiltetSouth, $"Day14Part2_{DateTime.Now.ToString("yyMMdd-HHmmss")}_Tiltet3South.txt");
            
            //if (solution.PlatformTiltetEast != null)
            //    TestDataWriter.ReadDataSet(solution.PlatformTiltetEast, $"Day14Part2_{DateTime.Now.ToString("yyMMdd-HHmmss")}_Tiltet4East.txt");

            //if (solution.PlatformOneCircle != null)
            //    TestDataWriter.ReadDataSet(solution.PlatformOneCircle, $"Day14Part2_{DateTime.Now.ToString("yyMMdd-HHmmss")}_OneCircle.txt");

            //assert
            Assert.That(result, Is.EqualTo("64"));
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
            var result = AdventOfCode2023Solutions.Day14.Solution.TransposeMatrix90Degrees(input);
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