using ProductApp;
namespace Product_Test
{
    public class CalculatorTests
    {
        private Calculator _calculator;
        [SetUp]
        public void SetUp()
        {
            _calculator= new Calculator();
        }
        [Test]
        public void WhenAddTwoNumbers_Should_returnSumofTwoNumber()
        {
            //Arrange
            int num1 = 100;
            int num2 = 900;
            var expected = 1000;

            //Act
            var sum = _calculator.AddAdditionOfTwoNumbers(num1, num2);

            //Assert
            //Assert.AreEqual(expected, sum); ---nunit version 3.0
            Assert.That(sum,Is.EqualTo(expected));
        }
    }
}
