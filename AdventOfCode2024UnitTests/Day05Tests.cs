namespace AdventOfCode2024UnitTests
{
    [TestFixture]
    public class Day05Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay05.txt");
            var solution = new AdventOfCode2024Solutions.Day05.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo("143"));
        }

        [Test]
        public void Manual_LoadingDataSet_CreateListOfCorrectOrderedUpdates()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay05.txt");
            var manual = new AdventOfCode2024Solutions.Day05.Manual(dataset);

            //act
            var numberOfCorrectOrderdUpdates = manual.UpdatesSubsetCorrect.Count;
            var firstCorrecOrderedUpdate = string.Join(",", manual.UpdatesSubsetCorrect[0]);
            var secondCorrecOrderedUpdate = string.Join(",", manual.UpdatesSubsetCorrect[1]);
            var thirdCorrecOrderedUpdate = string.Join(",", manual.UpdatesSubsetCorrect[2]);

            //assert
            Assert.That(numberOfCorrectOrderdUpdates, Is.EqualTo(3));
            Assert.That(firstCorrecOrderedUpdate, Is.EqualTo("75,47,61,53,29"));
            Assert.That(secondCorrecOrderedUpdate, Is.EqualTo("97,61,53,29,13"));
            Assert.That(thirdCorrecOrderedUpdate, Is.EqualTo("75,29,13"));
        }

        [Test]
        public void Manual_LoadingDataSet_CreateListOfIncorrectOrderedUpdates()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay05.txt");
            var manual = new AdventOfCode2024Solutions.Day05.Manual(dataset);

            //act
            var numberOfIncorrectOrderdUpdates = manual.UpdatesSubsetIncorrect.Count;
            var firstIncorrecOrderedUpdate = string.Join(",", manual.UpdatesSubsetIncorrect[0]);
            var secondIncorrecOrderedUpdate = string.Join(",", manual.UpdatesSubsetIncorrect[1]);
            var thirdIncorrecOrderedUpdate = string.Join(",", manual.UpdatesSubsetIncorrect[2]);

            //assert
            Assert.That(numberOfIncorrectOrderdUpdates, Is.EqualTo(3));
            Assert.That(firstIncorrecOrderedUpdate, Is.EqualTo("75,97,47,61,53"));
            Assert.That(secondIncorrecOrderedUpdate, Is.EqualTo("61,13,29"));
            Assert.That(thirdIncorrecOrderedUpdate, Is.EqualTo("97,13,75,29,47"));
        }

        [Test]
        public void Manual_FixUnorderedUpdates_CreateCorrectOrderedUpdateList()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay05.txt");
            var manual = new AdventOfCode2024Solutions.Day05.Manual(dataset);

            //act
            manual.FixIncorrectUpdates();
            var numberOfFixedIncorrectOrderdUpdates = manual.UpdatesSubsetIncorrectFixed.Count;
            var firstFixedIncorrecOrderedUpdate = string.Join(",", manual.UpdatesSubsetIncorrectFixed[0]);
            var secondFixedIncorrecOrderedUpdate = string.Join(",", manual.UpdatesSubsetIncorrectFixed[1]);
            var thirdFixedIncorrecOrderedUpdate = string.Join(",", manual.UpdatesSubsetIncorrectFixed[2]);

            //assert
            Assert.That(numberOfFixedIncorrectOrderdUpdates, Is.EqualTo(3));
            Assert.That(firstFixedIncorrecOrderedUpdate, Is.EqualTo("97,75,47,61,53"));
            Assert.That(secondFixedIncorrecOrderedUpdate, Is.EqualTo("61,29,13"));
            Assert.That(thirdFixedIncorrecOrderedUpdate, Is.EqualTo("97,75,47,29,13"));
        }

        [Test]
        public void Manual_MiddleNumberCorrectOrdered_Identified()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay05.txt");
            var manual = new AdventOfCode2024Solutions.Day05.Manual(dataset);
            manual.FixIncorrectUpdates();

            //act
            var middleNumberCorrectUpdates = manual.GetSumMiddleNumbersOfCorrectOrdered();

            //assert
            Assert.That(middleNumberCorrectUpdates, Is.EqualTo(143));
        }

        [Test]
        public void Manual_FixUnorderedList_CorrectOrdered()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay05.txt");
            var manual = new AdventOfCode2024Solutions.Day05.Manual(dataset);
            manual.FixIncorrectUpdates();

            //act
            var middleNumberIncorrectFixedUpdates = manual.GetSumMiddleNumbersOfIncorrectOrderedFixed();

            //assert
            Assert.That(middleNumberIncorrectFixedUpdates, Is.EqualTo(123));
        }

        [Test]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay05.txt");
            var solution = new AdventOfCode2024Solutions.Day05.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo("123"));
        }
    }
}