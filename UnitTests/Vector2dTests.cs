using Balls;

namespace UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Vector2d_Decompose_Test()
        {
            // Arrange
            var a = new Vector2d(5, 2);
            var b = new Vector2d(1, 3);
            var c = new Vector2d(-3, 2);

            // Action
            var result = Vector2d.Decompose(a, b, c);

            // Assert
            Assert.That(a == result.kb + result.kc);
        }
    }
}