using NUnit.Framework;
using ProductApp;

namespace Product_Test
{
    [TestFixture]
    public class ProductServiceTests
    {
        private ProductService _productService;

        [SetUp]
        public void Setup()
        {
            _productService = new ProductService();
        }

        [Test]
        public void AddProduct_WhenNewProduct_ShouldIncreaseStock()
        {
            // Arrange
            var product = new Product() { Name = "Laptop", Quantity = 10 };

            // Act
            _productService.AddProduct(product);

            // Assert
            Assert.That(_productService.CheckStock("Laptop"), Is.EqualTo(10));
        }

        [Test]
        public void AddProduct_WhenExistingProduct_ShouldIncreaseStock()
        {
            var product = new Product() { Name = "Laptop", Quantity = 10 };
            _productService.AddProduct(product);

            // Act
            _productService.AddProduct(new Product { Name = "Laptop", Quantity = 5 });

            // Assert
            Assert.That(_productService.CheckStock("Laptop"), Is.EqualTo(15));
        }

        [Test]
        public void CheckStock_WhenProductDoesNotExist_ShouldReturnZero()
        {
            var stock = _productService.CheckStock("sample product");

            // Assert
            Assert.AreEqual(0, stock);
        }

        [Test]
        public void UpdateStock_WhenProductExists_ShouldUpdateQuantity()
        {
            var product = new Product() { Name = "Laptop", Quantity = 10 };
            _productService.AddProduct(product);

            var updateResult = _productService.UpdateStock("Laptop", 12);

            // Assert
            Assert.IsTrue(updateResult);
            Assert.That(_productService.CheckStock("Laptop"), Is.EqualTo(12));
        }

        [Test]
        public void UpdateStock_WhenProductDoesNotExist_ShouldReturnFalse()
        {
            var result = _productService.UpdateStock("NoProduct", 12);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
