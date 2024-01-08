using AdventOfCode2023Solutions.Day18;

namespace AdventOfCode2023UnitTests
{
    public class Day18Tests
    {
        [Test]
        public void Create_Map3x3_startIn0c0()
        {
            //Prepare
            var dataset = new List<DigInstruction>();
            dataset.Add(new DigInstruction("R 3 (#70c710)"));
            dataset.Add(new DigInstruction("D 3 (#70c710)"));
            dataset.Add(new DigInstruction("L 3 (#70c710)"));
            dataset.Add(new DigInstruction("U 3 (#70c710)"));

            //act
            var map = Solution.CreateEmptyMap(dataset.ToArray(), out Position startPosition);
            var noOfYs = map.GetLength(0);
            var noOfXs = map.GetLength(1);

            //assert
            Assert.Multiple(() =>
            {
                Assert.That(startPosition.Y, Is.EqualTo(0));
                Assert.That(startPosition.X, Is.EqualTo(0));
                Assert.That(noOfYs, Is.EqualTo(4));
                Assert.That(noOfXs, Is.EqualTo(4));
            });
        }

        [Test]
        public void Create_Map3x3_startInCenter()
        {
            //Prepare
            var dataset = new List<DigInstruction>();
            dataset.Add(new DigInstruction("R 1 (#70c710)"));
            dataset.Add(new DigInstruction("D 1 (#70c710)"));
            dataset.Add(new DigInstruction("L 2 (#70c710)"));
            dataset.Add(new DigInstruction("U 2 (#70c710)"));
            dataset.Add(new DigInstruction("R 1 (#70c710)"));
            dataset.Add(new DigInstruction("D 1 (#70c710)"));

            //act
            var map = Solution.CreateEmptyMap(dataset.ToArray(), out Position startPosition);
            var noOfYs = map.GetLength(0);
            var noOfXs = map.GetLength(1);

            //assert
            Assert.Multiple(() =>
            {
                Assert.That(startPosition.Y, Is.EqualTo(1));
                Assert.That(startPosition.X, Is.EqualTo(1));
                Assert.That(noOfYs, Is.EqualTo(3));
                Assert.That(noOfXs, Is.EqualTo(3));
            });
        }

        [Test]
        public void Create_Map2x3_startInLowerRight()
        {
            //Prepare
            var dataset = new List<DigInstruction>();
            dataset.Add(new DigInstruction("L 3 (#70c710)"));
            dataset.Add(new DigInstruction("U 1 (#70c710)"));
            dataset.Add(new DigInstruction("R 2 (#70c710)"));
            dataset.Add(new DigInstruction("U 1 (#70c710)"));
            dataset.Add(new DigInstruction("R 1 (#70c710)"));
            dataset.Add(new DigInstruction("D 2 (#70c710)"));

            //act
            var map = Solution.CreateEmptyMap(dataset.ToArray(), out Position startPosition);
            var noOfYs = map.GetLength(0);
            var noOfXs = map.GetLength(1);

            //assert
            Assert.Multiple(() =>
            {
                Assert.That(startPosition.Y, Is.EqualTo(2));
                Assert.That(startPosition.X, Is.EqualTo(3));
                Assert.That(noOfYs, Is.EqualTo(3));
                Assert.That(noOfXs, Is.EqualTo(4));
            });
        }

        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay18.txt");
            var solution = new AdventOfCode2023Solutions.Day18.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("62"));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay18.txt");
            var solution = new AdventOfCode2023Solutions.Day18.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("952408144115"));
        }
    }
}