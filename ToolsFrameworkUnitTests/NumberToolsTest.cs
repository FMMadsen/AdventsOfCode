using ToolsFramework;

namespace ToolsFrameworkUnitTests
{
    [TestFixture]
    public class NumberToolsTest
    {
        [TestCase(new long[] { 5 }, 5)]
        [TestCase(new long[] { 5, 5 }, 5)]
        [TestCase(new long[] { 1, 2, 3 }, 4)]
        [TestCase(new long[] { 1, 2, 3, 5 }, 4)]
        public void CountSumOfEverySecond(long[] input, long expectedOutput)
        {
            //act
            var result = NumberTools.CountSumOfEvery2nd(input);

            //assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [TestCase(new long[] { 5 }, 5)]
        [TestCase(new long[] { 5, 5 }, 10)]
        [TestCase(new long[] { 1, 2, 3 }, 6)]
        [TestCase(new long[] { 1, 2, 3, 5 }, 11)]
        public void CountSumOfNumberArray(long[] input, long expectedOutput)
        {
            //act
            var result = NumberTools.CountSumOfNumberArray(input);

            //assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [TestCase("5", new long[] { 5 })]
        [TestCase("512", new long[] { 5, 1, 2 })]
        public void StringToIntArray(string input, long[] expectedOutput)
        {
            //act
            var result = NumberTools.StringToIntArray(input);

            //assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }
    }
}