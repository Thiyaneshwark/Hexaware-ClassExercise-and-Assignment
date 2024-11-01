using NUnit.Framework;
using ProductApp;

namespace Product_Test
{
    public class CalculatorServiceTest
    {
        private ICalculatorService _calculatorService;

        [SetUp]
        public void Setup()
        {
            // Arrange
            _calculatorService = new CalculatorService();
        }

        [TestCase(10, 30, 40)]
        [TestCase(100, 300, 400)]
        [TestCase(15, 25, 40)]
        public void Add_Numbers_ShouldReturnCorrectSum(int num1, int num2, int expected)
        {
            // Act
            var actualResult = _calculatorService.Add(num1, num2);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expected));
        }

        [Test]
        public void Add_PositiveNumbers_ShouldReturnCorrectSum()
        {
            var expected = 30;
            var actualResult = _calculatorService.Add(10, 20);
            Assert.That(actualResult, Is.EqualTo(expected));
        }

        [Test]
        public void Subtract_PositiveNumbers_ShouldReturnCorrectDifference()
        {
            var expected = 10;
            var actualResult = _calculatorService.Subtract(30, 20);
            Assert.That(actualResult, Is.EqualTo(expected));
        }
    }
}
