using Moq;
using AssetManagement.Interface;
using NUnit.Framework;

namespace Tests
{
    public class SubCategoryTests
    {
        private ISubCategory _subCategory;
        private Mock<ISubCategory> _subCategoryMock;

        [SetUp]
        public void SetUp()
        {
            _subCategoryMock = new Mock<ISubCategory>();
            _subCategory = _subCategoryMock.Object;
        }

        [TestCase]
        public async Task ReturnsAllSubCategoriesAsync()
        {
            // Arrange
            var expectedSubCategories = new List<SubCategory>
            {
                new SubCategory { SubCategoryId = 1, SubCategoryName = "Laptop" },
                new SubCategory { SubCategoryId = 2, SubCategoryName = "Headphones" }
            };

            _subCategoryMock.Setup(c => c.GetAllSubCategories())
                .ReturnsAsync(expectedSubCategories);

            // Act
            var result = await _subCategory.GetAllSubCategories();

            // Assert
            Assert.That(result, Is.Not.Null, "Result should not be null");
            Assert.That(result.Count, Is.EqualTo(2), "SubCategory count should be 2");
            Assert.That(result[0].SubCategoryName, Is.EqualTo("Laptop"));
            Assert.That(result[1].SubCategoryName, Is.EqualTo("Headphones"));
        }

        [TestCase]
        public async Task AddSubCategory_ShouldAddSubCategory()
        {
            // Arrange
            var newSubCategory = new SubCategory { SubCategoryId = 3, SubCategoryName = "Keyboard" };

            _subCategoryMock.Setup(repo => repo.AddSubCategory(It.IsAny<SubCategory>())).Callback((SubCategory subcategory) => { });

            // Act
            await _subCategory.AddSubCategory(newSubCategory);

            // Assert
            _subCategoryMock.Verify(repo => repo.AddSubCategory(It.Is<SubCategory>(s =>
                s.SubCategoryId == newSubCategory.SubCategoryId && s.SubCategoryName == newSubCategory.SubCategoryName)), Times.Once);
        }

        [TestCase]
        public async Task Save_ShouldCallSaveChanges()
        {
            // Act
            await _subCategory.Save();

            // Assert
            _subCategoryMock.Verify(repo => repo.Save(), Times.Once);
        }

        [TestCase]
        public async Task DeleteSubCategory_ShouldRemoveCategory()
        {
            // Arrange
            var subcategoryIdToDelete = 1;

            _subCategoryMock.Setup(repo => repo.DeleteSubCategory(It.IsAny<int>())).Callback<int>(id => { });

            // Act
            await _subCategory.DeleteSubCategory(subcategoryIdToDelete);

            // Assert
            _subCategoryMock.Verify(repo => repo.DeleteSubCategory(It.Is<int>(id => id == subcategoryIdToDelete)), Times.Once);
        }

        [TestCase]
        public async Task UpdateSubCategory_ShouldUpdateSubCategory()
        {
            // Arrange
            var updatedSubCategory = new SubCategory { SubCategoryId = 1, SubCategoryName = "Mouse" };

            _subCategoryMock.Setup(repo => repo.UpdateSubCategory(It.IsAny<SubCategory>())).ReturnsAsync((SubCategory cat) => cat);

            // Act
            var result = await _subCategory.UpdateSubCategory(updatedSubCategory);

            // Assert
            Assert.That(result, Is.Not.Null, "Result should not be null");
            Assert.That(result.SubCategoryId, Is.EqualTo(updatedSubCategory.SubCategoryId), "Sub Category ID should match");
            Assert.That(result.SubCategoryName, Is.EqualTo(updatedSubCategory.SubCategoryName), "Sub Category name should be updated");

            _subCategoryMock.Verify(repo => repo.UpdateSubCategory(It.Is<SubCategory>(s =>
                s.SubCategoryId == updatedSubCategory.SubCategoryId && s.SubCategoryName == updatedSubCategory.SubCategoryName)), Times.Once);
        }
    }
}
