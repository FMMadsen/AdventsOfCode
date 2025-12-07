using ToolsFramework;

namespace ToolsFrameworkUnitTests
{
    [TestFixture]
    public class NumberRangesTest
    {
        [Test]
        public void Costruct_OrderedInput_OrderedRanges()
        {
            //arrange
            string[] initializeStrings = [
                "1-2",
                "6-7"];

            //act
            var sut = NumberRanges.FromInitializationStringArray(initializeStrings);

            //assert
            Assert.That(sut.Ranges, Is.Not.Empty, "Ranges was empty");
            Assert.That(sut.Ranges, Has.Length.EqualTo(2), "There should be 2 elements");
            Assert.Multiple(() =>
            {
                Assert.That(sut.Ranges[0].From, Is.EqualTo(1), "First item should be 1-2");
                Assert.That(sut.Ranges[0].To, Is.EqualTo(2), "First item should be 1-2");
                Assert.That(sut.Ranges[1].From, Is.EqualTo(6), "First item should be 6-7");
                Assert.That(sut.Ranges[1].To, Is.EqualTo(7), "First item should be 6-7");
            });
        }

        [Test]
        public void Costruct_UnorganizedInput_OrganizeRanges()
        {
            //arrange
            string[] initializeStrings = [
                "11-15",
                "1-2",
                "5-7"];

            //act
            var sut = NumberRanges.FromInitializationStringArray(initializeStrings);

            //assert
            Assert.That(sut.Ranges, Is.Not.Empty);
            Assert.That(sut.Ranges, Has.Length.EqualTo(3), "There should be 3 elements");
            Assert.Multiple(() =>
            {
                Assert.That(sut.Ranges[0].From, Is.EqualTo(1), "First item FROM value");
                Assert.That(sut.Ranges[0].To, Is.EqualTo(2), "First item TO value");
                Assert.That(sut.Ranges[1].From, Is.EqualTo(5), "Second item FROM value");
                Assert.That(sut.Ranges[1].To, Is.EqualTo(7), "Second item TO value");
                Assert.That(sut.Ranges[2].From, Is.EqualTo(11), "Third item FROM value");
                Assert.That(sut.Ranges[2].To, Is.EqualTo(15), "Third item TO value");
            });
        }

        [Test]
        public void OfficialExamplePart1()
        {
            //arrange
            string[] initializeStrings = [
                "3-5",
                "10-14",
                "16-20",
                "12-18"];

            //act
            var sut = NumberRanges.FromInitializationStringArray(initializeStrings);

            //assert
            Assert.That(sut.Ranges, Is.Not.Empty);
            Assert.That(sut.Ranges, Has.Length.EqualTo(2), "There should be 2 elements");
            Assert.Multiple(() =>
            {
                Assert.That(sut.Ranges[0].From, Is.EqualTo(3), "First item FROM value");
                Assert.That(sut.Ranges[0].To, Is.EqualTo(5), "First item TO value");
                Assert.That(sut.Ranges[1].From, Is.EqualTo(10), "Second item FROM value");
                Assert.That(sut.Ranges[1].To, Is.EqualTo(20), "Second item TO value");
            });
        }


        [TestCase("1-4", "5-8", 1, 8)]
        [TestCase("1-4", "4-8", 1, 8)]
        [TestCase("1-4", "3-8", 1, 8)]
        [TestCase("5-8", "1-4", 1, 8)]
        [TestCase("4-8", "1-4", 1, 8)]
        [TestCase("3-8", "1-4", 1, 8)]
        [TestCase("1-8", "1-8", 1, 8)]
        [TestCase("1-4", "1-8", 1, 8)]
        [TestCase("3-5", "1-8", 1, 8)]
        [TestCase("5-8", "1-8", 1, 8)]
        public void Costruct_Overlapping_OrganizedAndMerged(
            string str1, string str2, int from, int to)
        {
            //arrange
            string[] initializeStrings = [str1, str2];

            //act
            var sut = NumberRanges.FromInitializationStringArray(initializeStrings);

            //assert
            Assert.That(sut.Ranges, Is.Not.Empty);
            Assert.That(sut.Ranges, Has.Length.EqualTo(1), "There should be 1 elements");
            Assert.Multiple(() =>
            {
                Assert.That(sut.Ranges[0].From, Is.EqualTo(from), "First item From value");
                Assert.That(sut.Ranges[0].To, Is.EqualTo(to), "First item TO value");
            });
        }

        [TestCase("1-4", "7-8", "5-6", 1, 8)]
        [TestCase("1-4", "7-8", "4-7", 1, 8)]
        [TestCase("1-4", "7-8", "1-8", 1, 8)]
        [TestCase("1-4", "6-8", "3-7", 1, 8)]
        public void Costruct_Overlapping_OrganizedAndMerged(
            string str1, string str2, string str3, int from, int to)
        {
            //arrange
            string[] initializeStrings = [str1, str2, str3];

            //act
            var sut = NumberRanges.FromInitializationStringArray(initializeStrings);

            //assert
            Assert.That(sut.Ranges, Is.Not.Empty);
            Assert.That(sut.Ranges, Has.Length.EqualTo(1), "There should be 1 elements");
            Assert.Multiple(() =>
            {
                Assert.That(sut.Ranges[0].From, Is.EqualTo(from), "First item From value");
                Assert.That(sut.Ranges[0].To, Is.EqualTo(to), "First item TO value");
            });
        }

        [TestCase("2-5", "26-28", "20-23", "15-25", 2, 5, 15, 28)]
        [TestCase("2-5", "26-28", "20-23", "15-30", 2, 5, 15, 30)]
        [TestCase("2-5", "26-28", "20-23", "1-19", 1, 23, 26, 28)]
        [TestCase("2-5", "26-28", "20-23", "1-20", 1, 23, 26, 28)]
        [TestCase("2-5", "26-28", "20-23", "1-21", 1, 23, 26, 28)]
        [TestCase("2-5", "26-28", "20-23", "1-22", 1, 23, 26, 28)]
        [TestCase("2-5", "26-28", "20-23", "1-23", 1, 23, 26, 28)]
        [TestCase("2-5", "26-28", "20-23", "1-24", 1, 24, 26, 28)]
        public void Costruct_Overlapping_OrganizedAndMerged(
            string str1, string str2, string str3, string str4, int from1, int to1, int from2, int to2)
        {
            //arrange
            string[] initializeStrings = [str1, str2, str3, str4];

            //act
            var sut = NumberRanges.FromInitializationStringArray(initializeStrings);

            //assert
            Assert.That(sut.Ranges, Is.Not.Empty);
            Assert.That(sut.Ranges, Has.Length.EqualTo(2), "Number of elements");
            Assert.Multiple(() =>
            {
                Assert.That(sut.Ranges[0].From, Is.EqualTo(from1), "First item From value");
                Assert.That(sut.Ranges[0].To, Is.EqualTo(to1), "First item TO value");
                Assert.That(sut.Ranges[1].From, Is.EqualTo(from2), "Second item From value");
                Assert.That(sut.Ranges[1].To, Is.EqualTo(to2), "Second item TO value");
            });
        }
    }
}
