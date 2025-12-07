using ToolsFramework;

namespace ToolsFrameworkUnitTests
{
    [TestFixture]
    public class DialerTest
    {
        [Test]
        public void Costruct_MinimumInitialization_SetsCorrectLowHighNumber()
        {
            //arrange & act
            var dialier = new Dialer(1, 10);

            //assert
            Assert.Multiple(() =>
            {
                Assert.That(dialier.LowEnd, Is.EqualTo(1));
                Assert.That(dialier.HighEnd, Is.EqualTo(10));
            });
        }

        [Test]
        public void Costruct_WithoutExplicitPointerInitialization_PointsToLowNumber()
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

        [TestCase(0, 9, 0, 0)]
        [TestCase(0, 9, 1, 1)]
        [TestCase(1, 3, 1, 0)]
        [TestCase(1, 3, 2, 1)]
        [TestCase(10, 20, 10, 0)]
        [TestCase(10, 20, 15, 5)]
        [TestCase(10, 20, 20, 10)]
        public void Initialize_WithVariousInitials_CorrectRelativePointerCalculated(int low, int high, int pointer, int expectedRelativePointer)
        {
            //arrange & act
            var dialier = new Dialer(low, high, pointer);

            //assert
            Assert.That(dialier.RelativePointer, Is.EqualTo(expectedRelativePointer));
        }

        [TestCase(0, 3, 3, Direction.Right, 0, 3, 0)]
        [TestCase(0, 3, 3, Direction.Right, 1, 0, 1)]
        [TestCase(0, 3, 3, Direction.Right, 2, 1, 1)]
        [TestCase(0, 3, 3, Direction.Right, 3, 2, 1)]
        [TestCase(0, 3, 3, Direction.Right, 4, 3, 1)]
        [TestCase(0, 3, 3, Direction.Right, 5, 0, 2)]
        [TestCase(0, 3, 3, Direction.Right, 6, 1, 2)]
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
        public void RotateV2_Right(int low, int high, int initial, Direction direction, int clicks, int expectedNewPointer, int expectedLowNumberHit)
        {
            //arrange
            var dialier = new Dialer(low, high, initial);

            //act
            dialier.RotateV2(clicks, direction);

            //assert
            Assert.Multiple(() =>
            {
                Assert.That(dialier.Pointer, Is.EqualTo(expectedNewPointer), "Pointer not right");
                Assert.That(dialier.ZeroHits, Is.EqualTo(expectedLowNumberHit), "'Zero' hits not right");
            });
        }

        [TestCase(0, 3, 3, Direction.Left, 0, 3, 0)]
        [TestCase(0, 3, 3, Direction.Left, 1, 2, 0)]
        [TestCase(0, 3, 3, Direction.Left, 2, 1, 0)]
        [TestCase(0, 3, 3, Direction.Left, 3, 0, 1)]
        [TestCase(0, 3, 3, Direction.Left, 4, 3, 1)]
        [TestCase(0, 3, 3, Direction.Left, 5, 2, 1)]

        [TestCase(5, 7, 6, Direction.Left, 0, 6, 0)]
        [TestCase(5, 7, 6, Direction.Left, 1, 5, 1)]
        [TestCase(5, 7, 6, Direction.Left, 2, 7, 1)]
        [TestCase(5, 7, 6, Direction.Left, 3, 6, 1)]
        [TestCase(5, 7, 6, Direction.Left, 4, 5, 2)]
        [TestCase(5, 7, 6, Direction.Left, 5, 7, 2)]

        [TestCase(0, 3, 0, Direction.Left, 0, 0, 0)]
        [TestCase(0, 3, 0, Direction.Left, 1, 3, 0)]
        [TestCase(0, 3, 0, Direction.Left, 2, 2, 0)]
        [TestCase(0, 3, 0, Direction.Left, 3, 1, 0)]
        [TestCase(0, 3, 0, Direction.Left, 4, 0, 1)]
        [TestCase(0, 3, 0, Direction.Left, 5, 3, 1)]
        [TestCase(0, 3, 0, Direction.Left, 6, 2, 1)]
        [TestCase(0, 3, 0, Direction.Left, 7, 1, 1)]
        [TestCase(0, 3, 0, Direction.Left, 8, 0, 2)]
        [TestCase(0, 3, 0, Direction.Left, 9, 3, 2)]
        [TestCase(0, 3, 0, Direction.Left, 10, 2, 2)]
        [TestCase(0, 3, 0, Direction.Left, 11, 1, 2)]
        [TestCase(0, 3, 0, Direction.Left, 12, 0, 3)]
        public void RotateV2_Left(int low, int high, int initial, Direction direction, int clicks, int expectedNewPointer, int expectedLowNumberHit)
        {
            //arrange
            var dialier = new Dialer(low, high, initial);

            //act
            dialier.RotateV2(clicks, direction);

            //assert
            Assert.Multiple(() =>
            {
                Assert.That(dialier.Pointer, Is.EqualTo(expectedNewPointer), "Pointer not right");
                Assert.That(dialier.ZeroHits, Is.EqualTo(expectedLowNumberHit), "'Zero' hits not right");
            });
        }
    }
}