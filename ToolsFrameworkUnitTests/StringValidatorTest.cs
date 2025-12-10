using Microsoft.VisualBasic;
using ToolsFramework;

namespace ToolsFrameworkUnitTests
{
    [TestFixture]
    public class StringValidatorTest
    {
        [TestCase(true, new string[] { })]
        [TestCase(true, new string[] { "5" })]
        [TestCase(true, new string[] { "2", "2", "2" })]
        [TestCase(false, new string[] { "5", "2", "2" })]
        [TestCase(false, new string[] { "2", "2", "3" })]
        [TestCase(false, new string[] { "2", "22", "2" })]
        public void IsStringsEqual(bool expectedResult, string[] strings)
        {
            //act
            var result = StringValidator.AreStringsEqual(strings);

            //assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(false, "")]
        [TestCase(true, "1")]
        [TestCase(false, "22")]
        [TestCase(true, "333")]
        [TestCase(false, "4444")]
        public void IsUnevenLength(bool expectedResult, string input)
        {
            //act
            var result = StringValidator.IsUnevenLength(input);

            //assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(true, "")]
        [TestCase(true, "1")]
        [TestCase(true, "22")]
        [TestCase(true, "333")]
        [TestCase(true, "4444")]
        [TestCase(false, "04")]
        [TestCase(false, "404")]
        [TestCase(false, "4404")]
        [TestCase(false, "A4404")]
        public void AreAllCharsEqual(bool expectedResult, string input)
        {
            //act
            var result = StringValidator.AreAllCharsEqual(input);

            //assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(true, "11")]
        [TestCase(true, "999")]
        [TestCase(true, "6666")]
        [TestCase(true, "6161")]
        [TestCase(true, "00000")]
        [TestCase(true, "010101")]
        [TestCase(true, "012012")]
        [TestCase(true, "4444444")]
        [TestCase(true, "44444444")]
        [TestCase(true, "83838383")]
        [TestCase(true, "83008300")]
        [TestCase(true, "831831831")]
        [TestCase(false, "")]
        [TestCase(false, "8")]
        [TestCase(false, "12")]
        [TestCase(false, "123")]
        [TestCase(false, "1234")]
        [TestCase(false, "1114")]
        [TestCase(false, "12345")]
        [TestCase(false, "12125")]
        [TestCase(false, "121213")]
        [TestCase(false, "100101")]
        public void CanSplitIntoEqualParts(bool expectedResult, string str)
        {
            //act
            var result = StringValidator.CanSplitIntoEqualParts(str);

            //assert
            Assert.That(result, Is.EqualTo(expectedResult), $"Input:{str}");
        }
    }
}
