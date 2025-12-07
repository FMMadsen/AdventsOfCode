using ToolsFramework;

namespace ToolsFrameworkUnitTests
{
    [TestFixture]
    public class WorkSheetTest
    {
        private static readonly string[] _initializeStrings = [
            "123 328  51 64 ",
            " 45 64  387 23 ",
            "  6 98  215 314",
            ];
        private readonly WorkSheet _sut = new(_initializeStrings);

        [TestCase(0, 0, 123)]
        [TestCase(1, 0, 328)]
        [TestCase(2, 0, 51)]
        [TestCase(3, 0, 64)]
        [TestCase(0, 1, 45)]
        [TestCase(1, 1, 64)]
        [TestCase(2, 1, 387)]
        [TestCase(3, 1, 23)]
        [TestCase(0, 2, 6)]
        [TestCase(1, 2, 98)]
        [TestCase(2, 2, 215)]
        [TestCase(3, 2, 314)]
        public void Construct_AfterInitialization_CellsCorrectSet(int column, int row, long expectedContent)
        {
            //arrange + act + assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut[column, row], Is.EqualTo(expectedContent), "Wrong cell content");
                Assert.That(_sut.GetCell(column, row).Content, Is.EqualTo(expectedContent), "Wrong cell content");
                Assert.That(_sut.GetCell(column, row).ColumnIndex, Is.EqualTo(column), "Wrong cell content for ColumnIndex");
                Assert.That(_sut.GetCell(column, row).RowIndex, Is.EqualTo(row), "Wrong cell content for RowIndex");
                Assert.That(_sut.GetCell(column, row).ToString(), Is.EqualTo(expectedContent.ToString()), "Wrong cell ToString");
            });
        }

        [TestCase("+ + + +", new long[4] { 174, 490, 653, 401 })]
        [TestCase("- - - -", new long[4] { 72, 166, -551, -273 })]
        [TestCase("* * * *", new long[4] { 33210, 2057216, 4243455, 462208 })]
        [TestCase("- + * +", new long[4] { 72, 490, 4243455, 401 })]
        public void ColumnCalculation_VariousInstructions(string instructions, long[] expectedResult)
        {
            //arrange + act
            long[] result = _sut.ColumnCalculation(instructions);

            //assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Costruct_TinyGridInitialization_DiminsionCorrectSet()
        {
            // arrange
            string[] initializeStrings = ["1"];

            // act
            var sut = new WorkSheet(initializeStrings);

            // assert
            Assert.Multiple(() =>
            {
                Assert.That(sut.NumberOfColumns, Is.EqualTo(1), "Wrong number of columns");
                Assert.That(sut.NumberOfRows, Is.EqualTo(1), "Wrong number of rows");
            });
        }

        [Test]
        public void Costruct_HorizontalGridInitialization_DiminsionCorrectSet()
        {
            // arrange
            string[] initializeStrings = [
                " 123 1 328  51 64 ",
                "  6 888998       98  215 314   "];

            // act
            var sut = new WorkSheet(initializeStrings);

            // assert
            Assert.Multiple(() =>
            {
                Assert.That(sut.NumberOfColumns, Is.EqualTo(5), "Wrong number of columns");
                Assert.That(sut.NumberOfRows, Is.EqualTo(2), "Wrong number of rows");
            });
        }

        [Test]
        public void Costruct_VerticalGridInitialization_DiminsionCorrectSet()
        {
            // arrange
            string[] initializeStrings = [
                "1 2",
                "3 4",
                "5 6",
                "7 8"];

            // act
            var sut = new WorkSheet(initializeStrings);

            // assert
            Assert.Multiple(() =>
            {
                Assert.That(sut.NumberOfColumns, Is.EqualTo(2), "Wrong number of columns");
                Assert.That(sut.NumberOfRows, Is.EqualTo(4), "Wrong number of rows");
            });
        }
    }
}