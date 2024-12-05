using Moq;
using AssetManagement.Interface;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    public class CategoryTests
    {
        private Mock<ICategory> categoryMock;
        private ICategory category;

        [SetUp]
        public void SetUp()
        {
            categoryMock = new Mock<ICategory>();
            category = categoryMock.Object;
        }

        [Test]
        public async Task ReturnsAllCategoriesAsync()
        {
            // Arrange
            var expectedCategories = new List<Category>
            {
                new Category { CategoryId = 1, CategoryName = "Software" },
                new Category { CategoryId = 2, CategoryName = "Hardware" }
            };

            categoryMock.Setup(c => c.GetAllCategories())
                .ReturnsAsync(expectedCategories);

            // Act
            var result = await category.GetAllCategories();

            // Assert
            Assert.That(result, Is.Not.Null, "Result should not be null");
            Assert.That(result.Count, Is.EqualTo(2), "Category count should be 2");
            Assert.That(result[0].CategoryName, Is.EqualTo("Software"));
            Assert.That(result[1].CategoryName, Is.EqualTo("Hardware"));
        }

        [Test]
        public async Task AddCategory_ShouldAddCategory()
        {
            // Arrange
            var newCategory = new Category { CategoryId = 3, CategoryName = "Networking" };

            categoryMock.Setup(repo => repo.AddCategory(It.IsAny<Category>())).Callback((Category category) => { });

            // Act
            await category.AddCategory(newCategory);

            // Assert
            categoryMock.Verify(repo => repo.AddCategory(It.Is<Category>(c => c.CategoryId == newCategory.CategoryId && c.CategoryName == newCategory.CategoryName)), Times.Once);
        }

        [Test]
        public async Task Save_ShouldCallSaveChanges()
        {
            // Act
            await category.Save();

            // Assert
            categoryMock.Verify(repo => repo.Save(), Times.Once);
        }

        [Test]
        public async Task DeleteCategory_ShouldRemoveCategory()
        {
            // Arrange
            var categoryIdToDelete = 1;

            categoryMock.Setup(repo => repo.DeleteCategory(It.IsAny<int>())).Callback<int>(id => { });

            // Act
            await category.DeleteCategory(categoryIdToDelete);

            // Assert
            categoryMock.Verify(repo => repo.DeleteCategory(It.Is<int>(id => id == categoryIdToDelete)), Times.Once);
        }

        [Test]
        public async Task UpdateCategory_ShouldUpdateCategory()
        {
            // Arrange
            var updatedCategory = new Category { CategoryId = 1, CategoryName = "UpdatedSoftware" };

            categoryMock.Setup(repo => repo.UpdateCategory(It.IsAny<Category>())).ReturnsAsync((Category cat) => cat);

            // Act
            var result = await category.UpdateCategory(updatedCategory);

            // Assert
            Assert.That(result, Is.Not.Null, "Result should not be null");
            Assert.That(result.CategoryId, Is.EqualTo(updatedCategory.CategoryId), "Category ID should match");
            Assert.That(result.CategoryName, Is.EqualTo(updatedCategory.CategoryName), "Category name should be updated");

            categoryMock.Verify(repo => repo.UpdateCategory(It.Is<Category>(c =>
                c.CategoryId == updatedCategory.CategoryId && c.CategoryName == updatedCategory.CategoryName)), Times.Once);
        }
    }
}
