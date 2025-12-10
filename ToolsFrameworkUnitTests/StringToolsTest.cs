using ToolsFramework;

namespace ToolsFrameworkUnitTests
{
    [TestFixture]
    public class StringToolsTest
    {
        [TestCase("  ", new string[] { " ", " " })]
        [TestCase("   ", new string[] { " ", "  " })]
        [TestCase("true", new string[] { "tr", "ue" })]
        [TestCase("ABCD", new string[] { "AB", "CD" })]
        [TestCase("ABCDE", new string[] { "AB", "CDE" })]
        public void SplitStringInMiddle(string input, string[] expectedResult)
        {
            //act
            var result = StringTools.SplitStringInMiddle(input);

            //assert
            Assert.That(result, Has.Length.EqualTo(expectedResult.Length), "Length of output");
            Assert.Multiple(() =>
            {
                Assert.That(result[0], Is.EqualTo(expectedResult[0]), $"First item");
                Assert.That(result[1], Is.EqualTo(expectedResult[1]), $"Second item");
            });
        }

        [Test]
        public void SplitStringInMiddle_EmptyString()
        {
            //act
            var result = StringTools.SplitStringInMiddle("");

            //assert
            Assert.That(result, Is.Empty, "No items should be returned");
        }

        [Test]
        public void SplitStringInMiddle_SingleChar()
        {
            //act
            var result = StringTools.SplitStringInMiddle("T");

            //assert
            Assert.That(result, Has.Length.EqualTo(1), "Should have length 1");
            Assert.That(result[0], Is.EqualTo("T"), "First and only element");
        }

        [TestCase("", 1, new string[] { })]
        [TestCase("", 2, new string[] { })]
        [TestCase("A", 1, new string[] { "A" })]
        [TestCase("A", 2, new string[] { "A" })]
        [TestCase("AB", 1, new string[] { "AB" })]
        [TestCase("AB", 2, new string[] { "A", "B" })]
        [TestCase("AB", 3, new string[] { "A", "B" })]
        [TestCase("AB", 4, new string[] { "A", "B" })]
        [TestCase("ABC", 1, new string[] { "ABC" })]
        [TestCase("ABC", 2, new string[] { "A", "BC" })]
        [TestCase("ABC", 3, new string[] { "A", "B", "C" })]
        [TestCase("ABC", 4, new string[] { "A", "B", "C" })]
        [TestCase("ABC", 5, new string[] { "A", "B", "C" })]
        [TestCase("ABCD", 1, new string[] { "ABCD" })]
        [TestCase("ABCD", 2, new string[] { "AB", "CD" })]
        [TestCase("ABCD", 3, new string[] { "A", "B", "CD" })]
        [TestCase("ABCD", 4, new string[] { "A", "B", "C", "D" })]
        [TestCase("ABCD", 5, new string[] { "A", "B", "C", "D" })]
        [TestCase("ABCD", 6, new string[] { "A", "B", "C", "D" })]
        public void SplitIntoNParts(string input, int numberOfPieces, string[] expectedResult)
        {
            //act
            var result = StringTools.SplitIntoNParts(input, numberOfPieces);

            //assert
            Assert.That(result, Is.EqualTo(expectedResult), $"Splitted items. Input:{input}");
        }

        [TestCase("", 1, new string[] { })]
        [TestCase("", 2, new string[] { })]
        [TestCase("A", 1, new string[] { "A" })]
        [TestCase("A", 2, new string[] { "A" })]
        [TestCase("AB", 1, new string[] { "A", "B" })]
        [TestCase("AB", 2, new string[] { "AB" })]
        [TestCase("AB", 3, new string[] { "AB" })]
        [TestCase("AB", 4, new string[] { "AB" })]
        [TestCase("ABC", 1, new string[] { "A", "B", "C" })]
        [TestCase("ABC", 2, new string[] { "AB", "C" })]
        [TestCase("ABC", 3, new string[] { "ABC" })]
        [TestCase("ABC", 4, new string[] { "ABC" })]
        [TestCase("ABCD", 1, new string[] { "A", "B", "C", "D" })]
        [TestCase("ABCD", 2, new string[] { "AB", "CD" })]
        [TestCase("ABCD", 3, new string[] { "ABC","D" })]
        [TestCase("ABCD", 4, new string[] { "ABCD" })]
        [TestCase("ABCD", 5, new string[] { "ABCD" })]
        [TestCase("AAAABBBBC", 4, new string[] { "AAAA", "BBBB", "C" })]
        [TestCase("AAAABBBBCC", 4, new string[] { "AAAA", "BBBB", "CC" })]
        [TestCase("AAAABBBBCCC", 4, new string[] { "AAAA", "BBBB", "CCC" })]
        [TestCase("AAAABBBBCCCC", 4, new string[] { "AAAA", "BBBB", "CCCC" })]
        public void SplitIntoPartsOfSize(string input, int numberOfPieces, string[] expectedResult)
        {
            //act
            var result = StringTools.SplitIntoPartsOfSize(input, numberOfPieces);

            //assert
            Assert.That(result, Is.EqualTo(expectedResult), $"Splitted items. Input:{input}");
        }
    }
}
