using Moq;
using NUnit.Framework;
using AssetManagement.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    public class AssetServiceTests
    {
        private IAsset _assetService;
        private Mock<IAsset> _assetServiceMock;

        [SetUp]
        public void SetUp()
        {
            _assetServiceMock = new Mock<IAsset>();
            _assetService = _assetServiceMock.Object;
        }

        [TestCase]
        public async Task GetAllAssets_ShouldReturnAllAssets()
        {
            // Arrange
            var expectedAssets = new List<Asset>
            {
                new Asset { AssetId = 1, AssetName = "Laptop" },
                new Asset { AssetId = 2, AssetName = "Mouse" }
            };

            _assetServiceMock.Setup(a => a.GetAllAssets()).ReturnsAsync(expectedAssets);

            // Act
            var result = await _assetService.GetAllAssets();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].AssetName, Is.EqualTo("Laptop"));
            Assert.That(result[1].AssetName, Is.EqualTo("Mouse"));
        }

        [TestCase]
        public async Task GetAssetById_ShouldReturnCorrectAsset()
        {
            // Arrange
            var assetId = 1;
            var expectedAsset = new Asset { AssetId = assetId, AssetName = "Laptop" };

            _assetServiceMock.Setup(a => a.GetAssetById(assetId)).ReturnsAsync(expectedAsset);

            // Act
            var result = await _assetService.GetAssetById(assetId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.AssetId, Is.EqualTo(assetId));
        }

        [TestCase]
        public async Task AddAsset_ShouldAddAsset()
        {
            // Arrange
            var newAsset = new Asset { AssetId = 3, AssetName = "Keyboard" };

            _assetServiceMock.Setup(a => a.AddAsset(It.IsAny<Asset>())).Callback<Asset>(a => { });

            // Act
            await _assetService.AddAsset(newAsset);

            // Assert
            _assetServiceMock.Verify(a => a.AddAsset(It.Is<Asset>(x => x.AssetId == newAsset.AssetId && x.AssetName == newAsset.AssetName)), Times.Once);
        }

        [TestCase]
        public async Task UpdateAsset_ShouldUpdateAsset()
        {
            // Arrange
            var updatedAsset = new Asset { AssetId = 1, AssetName = "Updated Laptop" };

            _assetServiceMock.Setup(a => a.UpdateAsset(It.IsAny<Asset>())).ReturnsAsync(updatedAsset);

            // Act
            var result = await _assetService.UpdateAsset(updatedAsset);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.AssetName, Is.EqualTo("Updated Laptop"));
        }

        [TestCase]
        public async Task DeleteAsset_ShouldDeleteAsset()
        {
            // Arrange
            var assetId = 1;

            _assetServiceMock.Setup(a => a.DeleteAsset(It.IsAny<int>())).Callback<int>(id => { });

            // Act
            await _assetService.DeleteAsset(assetId);

            // Assert
            _assetServiceMock.Verify(a => a.DeleteAsset(It.Is<int>(id => id == assetId)), Times.Once);
        }

        [TestCase]
        public async Task Save_ShouldSaveChanges()
        {
            // Act
            await _assetService.Save();

            // Assert
            _assetServiceMock.Verify(a => a.Save(), Times.Once);
        }
    }
}
