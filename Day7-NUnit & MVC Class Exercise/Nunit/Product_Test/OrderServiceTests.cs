using Moq;
using ProductApp;
namespace Product_Test
{
    [TestFixture]
    public class OrderServiceTests
    {
        private Mock<IProductService> _mockProductService;
        private OrderService _orderService;

        [SetUp]
        public void Setup()
        {
            _mockProductService = new Mock<IProductService>();
            _orderService = new OrderService(_mockProductService.Object);
        }

        [Test]
        public void PlaceOrder_WhenStockIsSufficient_ShouldReturnTrueAndReduceStock()

        {
            // Arrange
            _mockProductService.Setup(x => x.CheckStock(It.IsAny<string>())).Returns(10);

            // Act
            var result = _orderService.PlaceOrder("Laptop", 5);

            // Assert
            Assert.IsTrue(result);
            _mockProductService.Verify(x => x.UpdateStock("Laptop", 5), Times.Once);
        }

        [Test]
        public void PlaceOrder_WhenStockIsInSufficient_ShouldReturnFalse()
        {
            // Arrange
            _mockProductService.Setup(x => x.CheckStock(It.IsAny<string>())).Returns(2);

            // Act
            var result = _orderService.PlaceOrder("Laptop", 5);

            // Assert
            Assert.IsFalse(result);
            _mockProductService.Verify(x => x.UpdateStock(It.IsAny<string>(), It.IsAny<int>()), Times.Never);
        }
    }
}
