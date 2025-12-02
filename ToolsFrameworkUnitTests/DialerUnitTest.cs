using ToolsFramework;

namespace ToolsFrameworkUnitTests
{
    [TestFixture]
    public class DialerUnitTest
    {
        [Test]
        public void Initialize_WithoutExplicitPointerInitialization_PointsToLowNumber()
        {
            //arrange & act
            var dialier = new Dialer(1, 10);

            //assert
            Assert.That(dialier.Pointer, Is.EqualTo(1));
        }

        [Test]
        public void Initialize_WithExplicitPointerInitialization_PointsToInitial()
        {
            //arrange & act
            var dialier = new Dialer(1, 10, 3);

            //assert
            Assert.That(dialier.Pointer, Is.EqualTo(3));
        }

        [TestCase(0, 9, 10)]
        [TestCase(1, 2, 2)]
        [TestCase(2, 12, 11)]
        public void Initialize_WithVariousInitials_CorrectScaleCalculated(int low, int high, int expectedScale)
        {
            //arrange & act
            var dialier = new Dialer(low, high);

            //assert
            Assert.That(dialier.Scale, Is.EqualTo(expectedScale));
        }

        [Test]
        public void OfficialExample1()
        {
            //arrange
            var dialier = new Dialer(0, 99, 11);

            //act
            dialier.Rotate(8, Direction.Right);
            var firstStop = dialier.Pointer;
            dialier.Rotate(19, Direction.Left);
            var secondStop = dialier.Pointer;


            //assert
            Assert.That(firstStop, Is.EqualTo(19));
            Assert.That(secondStop, Is.EqualTo(0));
        }

        [Test]
        public void OfficialExample2()
        {
            //arrange
            var dialier = new Dialer(0, 99, 0);

            //act
            dialier.Rotate(1, Direction.Left);
            var firstStop = dialier.Pointer;
            dialier.Rotate(1, Direction.Right);
            var secondStop = dialier.Pointer;


            //assert
            Assert.That(firstStop, Is.EqualTo(99));
            Assert.That(secondStop, Is.EqualTo(0));
        }

        [Test]
        public void OfficialExample3()
        {
            //arrange
            var dialier = new Dialer(0, 99, 5);

            //act
            dialier.Rotate(10, Direction.Left);
            var firstStop = dialier.Pointer;

            //assert
            Assert.That(firstStop, Is.EqualTo(95));
        }

        [TestCase(0, 3, 0, Direction.Right, 0, 0)]
        [TestCase(0, 3, 0, Direction.Right, 1, 1)]
        [TestCase(0, 3, 0, Direction.Right, 2, 2)]
        [TestCase(0, 3, 0, Direction.Right, 3, 3)]
        [TestCase(0, 3, 0, Direction.Right, 4, 0)]
        [TestCase(0, 3, 0, Direction.Right, 5, 1)]
        [TestCase(0, 3, 0, Direction.Right, 6, 2)]
        [TestCase(0, 3, 0, Direction.Right, 7, 3)]
        [TestCase(0, 3, 0, Direction.Right, 8, 0)]
        [TestCase(0, 3, 0, Direction.Right, 9, 1)]
        [TestCase(0, 3, 0, Direction.Right, 10, 2)]
        [TestCase(0, 3, 0, Direction.Right, 11, 3)]
        [TestCase(250, 251, 251, Direction.Right, 0, 251)]
        [TestCase(250, 251, 251, Direction.Right, 1, 250)]
        [TestCase(250, 251, 251, Direction.Right, 2, 251)]
        [TestCase(250, 251, 251, Direction.Right, 3, 250)]
        public void Rotate_Right_PointsTo(int low, int high, int initial, Direction direction, int clicks, int expectedNewPointer)
        {
            //arrange
            var dialier = new Dialer(low, high, initial);

            //act
            dialier.Rotate(clicks, direction);

            //assert
            Assert.That(dialier.Pointer, Is.EqualTo(expectedNewPointer));
        }

        [TestCase(0, 3, 0, Direction.Left, 0, 0)]
        [TestCase(0, 3, 0, Direction.Left, 1, 3)]
        [TestCase(0, 3, 0, Direction.Left, 2, 2)]
        [TestCase(0, 3, 0, Direction.Left, 3, 1)]
        [TestCase(0, 3, 0, Direction.Left, 4, 0)]
        [TestCase(0, 3, 0, Direction.Left, 5, 3)]
        [TestCase(0, 3, 0, Direction.Left, 6, 2)]
        [TestCase(0, 3, 0, Direction.Left, 7, 1)]
        [TestCase(0, 3, 0, Direction.Left, 8, 0)]
        [TestCase(0, 3, 0, Direction.Left, 9, 3)]
        [TestCase(0, 3, 0, Direction.Left, 10, 2)]
        [TestCase(0, 3, 0, Direction.Left, 11, 1)]
        [TestCase(10, 15, 12, Direction.Left, 9, 15)]
        [TestCase(250, 251, 251, Direction.Left, 0, 251)]
        [TestCase(250, 251, 251, Direction.Left, 1, 250)]
        [TestCase(250, 251, 251, Direction.Left, 2, 251)]
        [TestCase(250, 251, 251, Direction.Left, 3, 250)]
        public void Rotate_Left_PointsTo(int low, int high, int initial, Direction direction, int clicks, int expectedNewPointer)
        {
            //arrange
            var dialier = new Dialer(low, high, initial);

            //act
            dialier.Rotate(clicks, direction);

            //assert
            Assert.That(dialier.Pointer, Is.EqualTo(expectedNewPointer));
        }

        [TestCase(0, 3, 0, Direction.Right, 0, 0, 0)]
        [TestCase(0, 3, 0, Direction.Right, 1, 1, 0)]
        [TestCase(0, 3, 0, Direction.Right, 2, 2, 0)]
        [TestCase(0, 3, 0, Direction.Right, 3, 3, 0)]
        [TestCase(0, 3, 0, Direction.Right, 4, 0, 1)]
        [TestCase(0, 3, 0, Direction.Right, 5, 1, 1)]
        [TestCase(0, 3, 0, Direction.Right, 6, 2, 1)]
        [TestCase(0, 3, 0, Direction.Right, 7, 3, 1)]
        [TestCase(0, 3, 0, Direction.Right, 8, 0, 2)]
        [TestCase(0, 3, 0, Direction.Right, 9, 1, 2)]
        [TestCase(0, 3, 0, Direction.Right, 10, 2, 2)]
        [TestCase(0, 3, 0, Direction.Right, 11, 3, 2)]
        [TestCase(0, 3, 0, Direction.Right, 12, 0, 3)]
        public void Rotate_Right_LowNumberHits(int low, int high, int initial, Direction direction, int clicks, int expectedNewPointer, int expectedLowNumberHit)
        {
            //arrange
            var dialier = new Dialer(low, high, initial);

            //act
            dialier.RotateV2(clicks, direction);

            //assert
            Assert.That(dialier.Pointer, Is.EqualTo(expectedNewPointer));
            Assert.That(dialier.LowNumberHits, Is.EqualTo(expectedLowNumberHit));
        }

    }
}
