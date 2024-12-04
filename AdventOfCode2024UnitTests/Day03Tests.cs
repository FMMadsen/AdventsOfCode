namespace AdventOfCode2024UnitTests
{
    public class Day03Tests
    {
        [Test]
        public void Part1_example1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay03_example1.txt");
            var solution = new AdventOfCode2024Solutions.Day03.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("2024"));
        }

        [Test]
        public void Part1_example2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay03_example2.txt");
            var solution = new AdventOfCode2024Solutions.Day03.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("492"));
        }

        [Test]
        public void Part1_example3()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay03_example3.txt");
            var solution = new AdventOfCode2024Solutions.Day03.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("0"));
        }

        [Test]
        public void Part1_example4()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay03_example4.txt");
            var solution = new AdventOfCode2024Solutions.Day03.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("0"));
        }

        [Test]
        public void Part1_example5()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay03_example5.txt");
            var solution = new AdventOfCode2024Solutions.Day03.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("0"));
        }

        [Test]
        public void Part1_example6()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay03_example6.txt");
            var solution = new AdventOfCode2024Solutions.Day03.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("0"));
        }

        [Test]
        public void Part1_example7()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay03_example7.txt");
            var solution = new AdventOfCode2024Solutions.Day03.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("161"));
        }

        [Test]
        public void Part1_example8()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay03_example8.txt");
            var solution = new AdventOfCode2024Solutions.Day03.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("999843"));
        }

        [Test]
        public void Part2_example1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay03_part2_example1.txt");
            var solution = new AdventOfCode2024Solutions.Day03.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("48"));
        }

        [Test]
        public void Part2_example2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay03_part2_example2.txt");
            var solution = new AdventOfCode2024Solutions.Day03.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("50"));
        }

        [Test]
        public void Part2_example3()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay03_part2_example3.txt");
            var solution = new AdventOfCode2024Solutions.Day03.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("51"));
        }
    }
}